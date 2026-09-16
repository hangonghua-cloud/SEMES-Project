
using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 系统服务类
    /// </summary>
    public class SystemService : RepositoryFactory
    {

        private static object objLock = new object();
        /// <summary>
        /// 业务单元执行超时时间【单位：毫秒】
        /// </summary>
        private int timeOutSec = Config.GetValue("LockTimeOutSec").ToInt();
        /// <summary>
        /// 根据字典键获取字典集合函数
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> LoadDataDictionary(string keyCode)
        {
            string sql = $@"SELECT * FROM Base_DataItemDetail WHERE ItemId IN (
                                SELECT ItemId FROM Base_DataItem WHERE ItemCode='{keyCode}')";
            return this.BaseRepository().FindList<DataItemDetailEntity>(sql);
        }

        /// <summary>
        ///查询产线(产线的'产品类型'不为空) PDA专用
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_Lines_PDA()
        {
            string sql = $@"select  '' as LineCode,'请选择' as LineName
                            union all
                            SELECT N.ResourceCode LineCode, N.ResourceName LineName
                            FROM [dbo].[BS_ModelResourceExtendInfo] M 
                            inner join [dbo].[BS_ModelWithResource] N on M.ResourceCode=N.ResourceCode
                           -- inner join [dbo].[BS_ModelResourceExtendInfo] O on M.ResourceCode=O.ResourceCode AND O.FieldCode='productType' --属性类型，如：产线产能capacity、产品类型productType
                            WHERE  M.EnabledMark=1  and N.EnabledMark=1 AND N.ModelLeve='productLine' AND M.FieldCode='productType'  AND  M.FieldValue<>'' and  M.FieldValue is not null
                              ";
            return this.BaseRepository().FindTable(sql);
        }
        /// <summary>
        ///获取字典列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_DataItem_PDA(string fatherCode)
        {
            string sql = $@"select ItemCode [value],ItemName label
                            from Base_DataItem 
                            where EnabledMark=1 and ParentId =(select top 1 ItemId from Base_DataItem where ItemCode ='{fatherCode}')  order by SortCode ";
            return this.BaseRepository().FindTable(sql);
        }
        /// <summary>
        ///获取字典列表子明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_DataItemByFather_PDA(string fatherCode, string remark1 = "")
        {
            string sql = $@"select B.ItemName label,B.ItemValue [value]
						from Base_DataItem as A
                        left join Base_DataItemDetail as B on A.ItemId=B.ParentId and B.EnabledMark=1
                        where A.EnabledMark=1 and A.ItemCode='{fatherCode}'";
            if (!string.IsNullOrEmpty(remark1))
                sql += $@" AND b.Remark1 ='{remark1}' ";

            sql += @"order by B.SortCode";
            return this.BaseRepository().FindTable(sql);
        }
        /// <summary>
        /// 获取用户部门和数据隔离标签
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IEnumerable<UserInfoEntity> GetEntity(string code)
        {
            UserInfoEntity enity = new UserInfoEntity();
            string sql = $@"SELECT TOP 1                   
	                            Code AS UserCode,
	                            Department_ID AS DepartmentID,
								ISNULL(IsDeptLimit,0) as IsDeptLimit,
	                            SUM ( GroupLabelValue ) AS GroupLabelValue 
                            FROM
	                            BS_People B
	                            LEFT JOIN [dbo].[Sys_DataSegregateGroupToPerson] P ON B.Code= P.PersonCode
	                            LEFT JOIN [dbo].[Sys_DataSegregateGroup] G ON P.GroupCode= G.GroupCode AND G.EnabledMark=1
                            WHERE
	                            Code = '{code}' 
                            GROUP BY
	                            Code,
	                            Department_ID,
								IsDeptLimit";
            return this.BaseRepository().FindList<UserInfoEntity>(sql);
        }

        /// <summary>
        ///查询供应商
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_Suppliers(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  [ID]
                                  ,[ShortName]
                                  ,[CategoryName]
                                  ,[Code]
                                  ,[Name]
                                  ,[UpdateTime]
                                  ,[IsEffective]
                              FROM [SIT_UA_MES].[dbo].[BS_Suppliers]
                              where IsEffective=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件               
                if (!queryParam["ShortName"].IsEmpty())
                {
                    sql.Append($" AND ShortName like '%{queryParam["ShortName"]}%'");
                }
                if (!queryParam["CategoryName"].IsEmpty())
                {
                    sql.Append($" AND CategoryName like '%{queryParam["CategoryName"]}%'");
                }
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND [Code] like '%{queryParam["Code"]}%'");
                }
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND [Name] like '%{queryParam["Name"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取供应商不分页数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetSuppliersList(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string shortName = queryParam["ShortName"] == null ? "" : queryParam["ShortName"].ToString();
            string categoryName = queryParam["CategoryName"] == null ? "" : queryParam["CategoryName"].ToString();
            string code = queryParam["Code"] == null ? "" : queryParam["Code"].ToString();
            string name = queryParam["Name"] == null ? "" : queryParam["Name"].ToString();
            sql.Append("select ID,ShortName,CategoryName,Code,Name from BS_Suppliers where 1=1 ");
            if (!string.IsNullOrWhiteSpace(shortName))
            {
                sql.Append(" and CHARINDEX('" + shortName + "',ShortName)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                sql.Append(" and CHARINDEX('" + categoryName + "',CategoryName)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(" and CHARINDEX('" + code + "',Code)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(" and CHARINDEX('" + name + "',Name)>0 ");
            }
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetSuppliersPageList(Pagination pagination, string queryJson, ref string msg)
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();
            msg = "";
            try
            {
                var queryParam = queryJson.ToJObject();
                string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
                string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
                sql.Append($@"select Id,SupplierCode,SupplierName from Base_SupplierManage where IsEnabled=1 ");
                if (!string.IsNullOrWhiteSpace(code))
                    sql.Append($@" and CHARINDEX('{code}', SupplierCode)>0 ");
                if (!string.IsNullOrWhiteSpace(name))
                    sql.Append($@" and CHARINDEX('{name}', SupplierName)>0 ");
                if (pagination != null)
                    dt = this.BaseRepository().FindTable(sql.ToString(), pagination);
                else
                    dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return dt;
        }
        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Pwd"></param>
        /// <returns></returns>
        public DataTable Login(string userId, string pwd)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Top 1 U.UserId UserCode,U.APPRole,P.Department_ID,P.[Name],P.ID UserId
                        FROM [dbo].[BS_APPLogUserInfo] U
                        Left join [dbo].[BS_People] P on U.UserId=P.Code  where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(userId) && !string.IsNullOrWhiteSpace(pwd))
            {
                sql.Append(" and UserId='" + userId + "' and PWD='" + Md5Helper.MD5(pwd, 32) + "' ");
            }
            else
            {
                return null;
            }
            var loginModel = this.BaseRepository().FindTable(sql.ToString());
            return loginModel;
        }
        /// <summary>
        /// 根据用户ID获取当前用户对应角色的功能列表
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable GetRoleFunctions(string role)
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();

            sql.Append(@"SELECT  RF.[ModuleCode] as OwnedPage,RF.[ModuleName]as OwnedPageName,RF.[FunctionCode] as ModelCode,RF.[FunctionName] as ModelName,FunctionColor as ModelColor,[FunctionIco] as ModelIco,[Type],[Sort]
                                FROM [dbo].[BS_APPRoleToFunction] RTF
								INNER JOIN [dbo].[BS_APPRoleFunction] RF ON RTF.[FunctionCode]=RF.[FunctionCode]  WHERE 1=1 ");

            if (!string.IsNullOrWhiteSpace(role))
            {
                sql.Append(" and RTF.RoleCode='" + role + "' ");
            }
            sql.Append(" Order by Sort");
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable GetRoles_PDA()
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();

            sql.Append(@"SELECT [RoleCode],[RoleName],[EnabledMark] FROM [SIT_UA_MES].[dbo].[BS_APPRole] where [EnabledMark]=1 ");

            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        ///查询物料
        /// </summary>
        /// <returns></returns> 
        public DataTable GetList_Materials(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT  m.[ID]
                              ,m.[Name]
                              ,m.[Segement1]
                              ,m.[Code]
                              ,m.[SPECS]
                              ,m.[state]
							  ,[dbo].get_dicName('material_state_dic',[state]) [stateName]    
                              ,m.[LotValidDate]
                              ,m.[LotParamID]
                              ,m.[StandardMaterialScale]
                              ,m.[IsDualUOM]
                              ,m.[IsAllowExcessMaterial]
                              ,m.[IsEffective]
                              ,m.[Ratio]
                              ,m.[UpdateTime]
                              ,m.[InventoryUOM_ID]
	                          ,u.[Name] [InventoryUOM_Name]
                              ,m.[MaterialOutUOM_ID]
	                          ,u2.[Name] [MaterialOutUOM_Name]
                              ,m.[Segement1Name]
                              ,m.[ItemFormAttribute]
                              ,m.[ItemFormAttributeName]
                              ,m.[StandardGrade]
                              ,m.[StandardGradeName]
                              ,m.[StandardPotency]
                              ,m.[StandardPotencyName]
                              ,m.[PrivateDescSeg6]
                          FROM [SIT_UA_MES].[dbo].[MR_Materials] m
	                        left join [dbo].[BS_UOMs] u on u.ID=m.InventoryUOM_ID
	                        left join [dbo].[BS_UOMs] u2 on u2.ID=m.MaterialOutUOM_ID 
                          where  1=1 ");//m.IsEffective=1 
            var parameter = new List<DbParameter>();
            string code = "";
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件       
                //品名
                if (!queryParam["Name"].IsEmpty())
                {
                    code = queryParam["Name"].ToString();
                    sql.Append($" AND (m.[Name] like '%{code}%' or m.[Name] like '%{code.ToUpper()}%' or m.[Name] like '%{code.ToLower()}%')");
                }//编码
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND m.Code ='{queryParam["Code"]}'");
                }
                if (!queryParam["queryName"].IsEmpty())
                {
                    code = queryParam["queryName"].ToString();
                    sql.Append($" AND (m.[Name] like '%{code}%' or m.[Name] like '%{code.ToUpper()}%' or m.[Name] like '%{code.ToLower()}%')");
                }//编码
                if (!queryParam["queryCode"].IsEmpty())
                {
                    code = queryParam["queryCode"].ToString();
                    sql.Append($" AND (m.Code like '%{code}%' or m.Code like '%{code.ToUpper()}%' or m.Code like '%{code.ToLower()}%')");
                }
                if (!queryParam["Code2"].IsEmpty())
                {
                    code = queryParam["Code2"].ToString();
                    sql.Append($" AND (m.Code like '%{code}%' or m.Code like '%{code.ToUpper()}%' or m.Code like '%{code.ToLower()}%')");
                }//规格
                if (!queryParam["SPECS"].IsEmpty())
                {
                    sql.Append($" AND m.SPECS ='{queryParam["SPECS"]}'");
                }
                //主分类名称
                if (!queryParam["Segement1Name"].IsEmpty())
                {
                    sql.Append($" AND m.Segement1Name like '%{queryParam["Segement1Name"]}%'");
                }
                //形态属性名称
                if (!queryParam["ItemFormAttributeName"].IsEmpty())
                {
                    sql.Append($" AND m.ItemFormAttributeName like '%{queryParam["ItemFormAttributeName"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetEquipmentLocation(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            sql.Append(@"select ID,Code,Name from EP_EquipmentLocations where 1=1");
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(@" AND Code like '%" + code + "%'");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(@" AND Name like '%" + name + "%'");
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }

        /// <summary>
        /// 检验标准关系维护－－产品选择
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMaterialPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            //  string specs = queryParam["Specs"] == null ? "" : queryParam["Specs"].ToString();

            sql.Append(@"SELECT Segement1,Segement1Name,Code,Name,SPECS  from MR_Materials 
                        WHERE ((Segement1 like 'DC101%') OR (Segement1 like 'DC2%') OR (Segement1 like 'DR3%') OR (Segement1 like 'DR2%'))");
            if (!string.IsNullOrWhiteSpace(code))
            {
                if (code.IndexOf(",") == -1)
                {
                    sql.Append(@" AND Code like '%" + code + "%'");
                }
                else
                {
                    string[] codeArr = code.Split(',');
                    sql.Append(@" and (Code like '%" + codeArr[0] + "%' or Code like '%" + codeArr[1] + "%')");
                }
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(@" AND Name like '%" + name + "%'");
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }
        /// <summary>
        /// 弹框选择人员
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetEmployeePageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string factoryCode = queryParam["FactoryCode"] == null ? "" : queryParam["FactoryCode"].ToString();
            string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            sql.Append(@"select ID,Code,Name,Department_ID from BS_People where 1=1  ");
            if (!string.IsNullOrEmpty(factoryCode))
                sql.Append(@" AND FactoryCode = '" + factoryCode + "'");

            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(@" AND Code like '%" + code + "%'");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(@" AND Name like '%" + name + "%'");
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }
        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetEmployeeInfo(string code)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"select A.ID,A.Sex,A.CertificateCode,A.MobilePhone,A.Department_ID,B.Name as DepartmentName,
                        A.Position_ID,A.PositionName,A.Job_ID,A.JobName,A.Code,A.Name
                        from BS_People as A
                        left join BS_Departments as B on A.Department_ID=B.ID 
                        where A.IsEffective=1 and B.IsEffective=1 and A.Code='{code}' ");
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 获取用户姓名
        /// </summary>
        /// <param name="code"></param>
        /// /// <param name="noLike"></param>
        /// <param name="loginUserCode">登录用户</param>
        /// <returns></returns>
        public DataTable GetUserList(string code, string loginUserCode, string noLike = "")
        {

            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT Code,
                               Name,
                               Code + '-' + Name ShowName
                        FROM dbo.BS_People
                        WHERE IsEnabled = 1
                              AND FactoryCode IN
                                  (
                                      SELECT Code
                                      FROM dbo.fn_Split(
                                           (
                                               SELECT FactoryCode FROM dbo.BS_People WHERE Code = '{loginUserCode}'
                                           ),
                                           ','
                                                       )
                                  ) ");
            if (!string.IsNullOrEmpty(noLike) && !string.IsNullOrEmpty(code))
            {
                sql.Append($@" AND Code= '{code}'");
            }
            else if (!string.IsNullOrEmpty(code))
            {
                sql.Append($@" AND (Code LIKE '%{code}%' OR Name LIKE '%{code}%') ");
            }
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        ///查询仓库
        /// </summary>
        /// <returns></returns> 
        public DataTable GetList_Depositories(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT   d.[ID]
                              ,d.[UpdateTime]
                              ,d.[IsEffective]
                              ,d.[LocationType]
	                          ,d.LocationTypeName
                              ,d.[Department_ID]
	                        ,depart.[Name] DepartName 
                              ,d.[Manager_ID]
	                          ,p.[Name] [Manager_Name]
                              ,d.[TelephoneNumber]
                              ,d.[Code]
                              ,d.[Name]
                          FROM [SIT_UA_MES].[dbo].[BS_Depositories] d 
                              left join [dbo].[BS_Departments] depart on depart.ID= d.[Department_ID]
                            left join BS_People p on p.ID=d.[Manager_ID]
                          where d.IsEffective=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件               
                //部门
                if (!queryParam["Department_ID"].IsEmpty())
                {
                    sql.Append($" AND d.Department_ID = '{queryParam["Department_ID"]}'");
                }
                //位置属性名称
                if (!queryParam["LocationTypeName"].IsEmpty())
                {
                    sql.Append($" AND d.LocationTypeName like '%{queryParam["LocationTypeName"]}%'");
                }
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND d.Name like '%{queryParam["Name"]}%'");
                }
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND d.Code =  '{queryParam["Code"]}'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        ///查询仓库-下拉专用
        /// </summary>
        /// <returns></returns> 
        public DataTable GetList_Depositories_Control()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  [ID],[Code] ItemValue,[Name] ItemName
                        FROM [dbo].[BS_Depositories]
                        where IsEffective=1");

            try
            {

                return this.BaseRepository().FindTable(sql.ToString());

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 功能描述: 查询计量单位 pagination 分页json; queryJson 查询JSO
        /// 创　　建: 刘万军
        /// 创建日期: 2021-01-29 14:57:18
        /// 任务编号:
        /// </summary>       
        /// <returns></returns> 
        public DataTable GetList_BS_UOMs(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT [ID]
                        ,[Code]
                        ,[Name]
                        ,[UpdateTime]
                        ,[IsEffective]
                        ,[UOMClass]
                        FROM [dbo].[BS_UOMs]
                          where IsEffective=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件               
                //编码
                if (!queryParam["ID"].IsEmpty())
                {
                    sql.Append($" AND ID = '{queryParam["ID"]}'");
                }
                //名称                
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND Name like '%{queryParam["Name"]}%'");
                }
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND Code like '%{queryParam["Code"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取退库原因分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetReturnReasonPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string code = queryParam["Code"] == null ? "" : queryParam["Code"].ToString();
            string name = queryParam["Name"] == null ? "" : queryParam["Name"].ToString();
            sql.Append(@"select Id,MaterialReturnReasonCode,MaterialReturnReasonName,Remarks,A.Creator,pe.Name as CreatorName,CreatDate 
                        from PM_MaterialReturnReason  as A
						LEFT JOIN BS_People pe ON pe.Code = A.Creator COLLATE Chinese_PRC_CI_AS
                        where IsEnabled=1 ");
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(" and CHARINDEX('" + code + "',MaterialReturnReasonCode)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(" and CHARINDEX('" + name + "',MaterialReturnReasonName)>0 ");
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }

        public DataTable GetEquipmentDetail(string EquipmentCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT
                            CASE
	                            A.State 
	                            WHEN '1' THEN
	                            '调试' 
	                            WHEN '2' THEN
	                            '空闲' 
	                            WHEN '3' THEN
	                            '报废' ELSE '使用' 
	                            END AS StateName,
	                            A.State,
	                            A.EquipmentmanufacturerCode,
	                            A.EquipmentmanufacturerName,
	                            A.BenefitDept_ID,
	                            A.FactoryCode,
	                            A.FactoryName,
	                            A.BenefitOrgName,
	                            A.EquipmentPositionCode,
	                            A.EquipmentPositionName,
	                            B.Name AS BenefitDeptName 
                            FROM
	                            EP_EquipmentManage AS A
	                            LEFT JOIN BS_Departments B ON A.BenefitDept_ID = B.ID 
                            WHERE
	                            1 = 1");

            if (!string.IsNullOrWhiteSpace(EquipmentCode))
            {
                sql.Append($" AND EquipmentCode = '{EquipmentCode}'");
            }
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 根据页面名称获按钮权限
        /// </summary>
        /// <param name="PageName">页面名称</param>
        /// <returns></returns>
        public DataTable GetButtonAuthByPage(string PageName)
        {
            //string sql = $@"select Name as FullName, ShortName as ButtonCode, Description as ButtonName
            //                from SIT_UA_Eng.dbo.CommandDefinition
            //                where Name like '%ButtonAuthFBApp%'
            //                  and ShortName like '%{PageName}%'";
            string sql = $@"SELECT * FROM V_Sy_CommandDefinition
                            where FullName like '%ButtonAuthFBApp%'
                              and ButtonCode like '{PageName}%'";

            return this.BaseRepository().FindTable(sql);
            //var table = new DataTable();
            //using (SqlConnection conn = new SqlConnection("Server=.;Initial Catalog=FHMESDB;User ID=sa;Password=sa@123;Connect Timeout=45;"))
            //{
            //    conn.Open();
            //    try
            //    {
            //        SqlCommand cmd = conn.CreateCommand();
            //        cmd.CommandText = sql;
            //        cmd.CommandType = CommandType.Text;
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(table);
            //        return table;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }

            //}

        }
    }
}
