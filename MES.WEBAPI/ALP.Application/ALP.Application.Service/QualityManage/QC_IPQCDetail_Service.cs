using System;
using System.Collections.Generic;
using ALP.Application.UtilExtend.Util;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.QualityManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;

namespace ALP.Application.Service.QualityManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_IPQCDetailService 业务服务类
    /// 4.任务编号: 过程检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_IPQCDetail_Service : RepositoryFactory<QC_IPQCDetailEntity>, QC_IPQCDetail_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardId]
                      ,[ProductionWorkshop]
                      ,[TestMachine]
                      ,[CheckResult]
                      ,[Remark]
                      ,[Attachment]
                      ,[Inspector]
                      ,[InspectionTime]
                      ,[EnabledMark]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IPQCDetail] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //流转卡编号 是否为空进行查询
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    //sql.Append($" AND FlowCardId = N'{queryParam["FlowCardId"]}'");
                    sql.Append($" AND FlowCardId like N'%{queryParam["FlowCardId"]}%'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["ProductionWorkshop"].IsEmpty())
                {
                    //sql.Append($" AND ProductionWorkshop = N'{queryParam["ProductionWorkshop"]}'");
                    sql.Append($" AND ProductionWorkshop like N'%{queryParam["ProductionWorkshop"]}%'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["TestMachine"].IsEmpty())
                {
                    //sql.Append($" AND TestMachine = N'{queryParam["TestMachine"]}'");
                    sql.Append($" AND TestMachine like N'%{queryParam["TestMachine"]}%'");
                }
                //检验结果 是否为空进行查询
                if (!queryParam["CheckResult"].IsEmpty())
                {
                    //sql.Append($" AND CheckResult = N'{queryParam["CheckResult"]}'");
                    sql.Append($" AND CheckResult like N'%{queryParam["CheckResult"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND Attachment like N'%{queryParam["Attachment"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Inspector"].IsEmpty())
                {
                    //sql.Append($" AND Inspector = N'{queryParam["Inspector"]}'");
                    sql.Append($" AND Inspector like N'%{queryParam["Inspector"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["InspectionTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND InspectionTime like N'%{queryParam["InspectionTime"]}%'");
                }
                //删除标志 是否为空进行查询
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    //sql.Append($" AND EnabledMark = N'{queryParam["EnabledMark"]}'");
                    sql.Append($" AND EnabledMark like N'%{queryParam["EnabledMark"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND ModifyTime like N'%{queryParam["ModifyTime"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = '{queryParam["queryName"]}'");
                    //sql.Append($" AND 关键名称 like N'%{queryParam["queryName"]}%'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = N'{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like N'%{queryParam["queryCode"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindList(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT DISTINCT
                               A.Id,
							   A.FactoryCode,
							   A.FactoryName,
                               A.FlowCardId,
                               A.ProductionWorkshop,
                               F.ResourceName AS ProductionWorkshopName,
                               A.TestMachine,
                               H.ResourceName AS TestMachineName,
                               A.CalibrationMethod,
                               E.TestMethodName AS CalibrationMethodName,
                               A.Remark,
                               A.Attachment,
                               A.Inspector,
	                           j.Name Dept,
                               I.Name AS InspectorName,
                               A.InspectionTime,
                               CONVERT(VARCHAR(100), A.InspectionTime, 20) AS InspectionTimeStr,
                               B.ProductOrder,
                               B.ContainerNO,
                               B.Spec,
                               G.ResourceCode AS WorkShopCode,
                               G.ResourceName AS WorkShopName,
                               A.CreateTime,
                               A.EnabledMark,
                               E.SmallClass
                        FROM dbo.QC_IPQCDetail AS A
                            LEFT JOIN dbo.PM_TransferCard AS B
                                ON A.FlowCardId = B.CardCode
                                   AND B.IsEnabled = 1
                            LEFT JOIN dbo.BS_ModelWithResource AS F
                                ON A.ProductionWorkshop = F.ResourceCode
                                   AND F.ModelLeve = 'Process'
                                   AND F.EnabledMark = 1
                            LEFT JOIN dbo.BS_ModelWithResource AS G
                                ON F.ParentResource = G.ResourceCode
                                   AND G.ModelLeve = 'WorkShop'
                                   AND G.EnabledMark = 1
                            LEFT JOIN dbo.BS_ModelWithResource AS H
                                ON A.TestMachine = H.ResourceCode
                                   AND H.ModelLeve = 'Machine'
                                   AND H.EnabledMark = 1
                            LEFT JOIN dbo.QC_TestMaintenance AS E
                                ON A.CalibrationMethod = E.TestMethodCoading
                                   AND E.IsEnabled = 1
                            LEFT JOIN dbo.BS_People AS I
                                ON A.Inspector = I.Code
                                   AND I.IsEnabled = 1
	                        LEFT JOIN dbo.BS_Departments j ON i.Department_ID=j.Code
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                /*
                  and CHARINDEX('', B.ProductOrder)>0 and CHARINDEX('', C.ContainerNO)>0 and CHARINDEX('', B.Spec)>0 
and A.ProductionWorkshop='' and A.TestMachine='' and CHARINDEX('', I.Name)>0 and A.InspectionTime>='' and A.InspectionTime<''
                 */
                JObject queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["ProductOrder"]}', B.ProductOrder)>0 ");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["ContainerNO"]}', B.ContainerNO)>0 ");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["Spec"]}', B.Spec)>0 ");
                }
                //创建人 是否为空进行查询
                if (!queryParam["InspectorName"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["InspectorName"]}', I.Name)>0 ");
                }
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //流转卡编号 是否为空进行查询
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    //sql.Append($" AND FlowCardId = N'{queryParam["FlowCardId"]}'");
                    sql.Append($" AND A.FlowCardId like N'%{queryParam["FlowCardId"]}%'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["ProductionWorkshop"].IsEmpty())
                {
                    sql.Append($" AND ProductionWorkshop = N'{queryParam["ProductionWorkshop"]}'");
                    //sql.Append($" AND A.ProductionWorkshop like N'%{queryParam["ProductionWorkshop"]}%'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["TestMachine"].IsEmpty())
                {
                    sql.Append($" AND TestMachine = N'{queryParam["TestMachine"]}'");
                    //sql.Append($" AND A.TestMachine like N'%{queryParam["TestMachine"]}%'");
                }
                //检验方法 是否为空进行查询
                if (!queryParam["CalibrationMethod"].IsEmpty())
                {
                    //sql.Append($" AND CheckResult = N'{queryParam["CheckResult"]}'");
                    sql.Append($" AND A.CalibrationMethod like N'%{queryParam["CalibrationMethod"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND A.Attachment like N'%{queryParam["Attachment"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Inspector"].IsEmpty())
                {
                    //sql.Append($" AND Inspector = N'{queryParam["Inspector"]}'");
                    sql.Append($" AND A.Inspector like N'%{queryParam["Inspector"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["InspectionTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND A.InspectionTime like N'%{queryParam["InspectionTime"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" and A.InspectionTime>='{Tools.CovertToDateStr(queryParam["StartTime"])}' ");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" and A.InspectionTime<'{Tools.CovertToNextDateStr(queryParam["EndTime"])}' ");
                }
                //部门 是否为空进行查询
                if (!queryParam["Dept"].IsEmpty())
                {
                    sql.Append($" AND j.Name like N'%{queryParam["Dept"]}%'");
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
        /// 获取检验方法下拉列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCalibrationMethodPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string factory = queryParam["Factory"] == null ? "" : queryParam["Factory"].ToString();
            string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            sql.Append($@"select Id,TestType,TestMethodCoading,TestMethodName,TestMethodDescription from QC_TestMaintenance
                          where IsEnabled=1  ");
            if (!string.IsNullOrWhiteSpace(factory))
            {
                sql.Append($" and FactoryCode='{factory}' ");
            }
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append($" and CHARINDEX('{code}', TestMethodCoading)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append($" and CHARINDEX('{name}', TestMethodName)>0 ");
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }

        /// <summary>
        /// 获取检验方法关联工序列表
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public DataTable GetProcessList(string method)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"select distinct A.ProcessCode,A.ProcessName from QC_TestProcessMaintenance as A
                        left join QC_TestMaintenance as B on A.TestMaintenanceId=B.Id and B.IsEnabled=1
                        --left join QC_IPQCDetail as C on C.CalibrationMethod=B.TestMethodCoading and C.EnabledMark=1
                        where B.TestMethodCoading='{method}' ");
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 查询工序列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<QC_TestMaintenanceEntity> GetMainenanceList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<QC_TestMaintenanceEntity> maintenanceList = new RepositoryFactory<QC_TestMaintenanceEntity>();
            IEnumerable<QC_TestMaintenanceEntity> list = null;
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string methodCode = queryParam["MethodCode"] == null ? "" : queryParam["MethodCode"].ToString();
            string methodName = queryParam["MethodName"] == null ? "" : queryParam["MethodName"].ToString();
            string processCode = queryParam["ProcessCode"] == null ? "" : queryParam["ProcessCode"].ToString();
            string processName = queryParam["ProcessName"] == null ? "" : queryParam["ProcessName"].ToString();
            sql.Append($@"select A.TestMethodCoading,A.TestMethodName from QC_TestMaintenance as A
                        left join QC_TestProcessMaintenance as B on A.Id=B.TestMaintenanceId 
                        where 1=1 and A.IsEnabled=1     ");
            if (!string.IsNullOrWhiteSpace(methodCode))
                sql.Append($@" and A.TestMethodCoading='{methodCode}' ");
            if (!string.IsNullOrWhiteSpace(methodName))
                sql.Append($@" and CHARINDEX('{methodName}', A.TestMethodName)>0 ");
            if (!string.IsNullOrWhiteSpace(processCode))
                sql.Append($@" and B.ProcessCode='{processCode}' ");
            if (!string.IsNullOrWhiteSpace(processName))
                sql.Append($@" and CHARINDEX('{processName}', B.ProcessName)>0 ");
            if (pagination != null)
                list = maintenanceList.BaseRepository().FindList(sql.ToString(), pagination);
            else
                list = maintenanceList.BaseRepository().FindList(sql.ToString());
            return list;
        }

        /// <summary>
        /// 查询方法列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetProcessList(Pagination pagination, string queryJson)
        {
            RepositoryFactory<BsModelWithResourceEntity> resourceService = new RepositoryFactory<BsModelWithResourceEntity>();
            IEnumerable<BsModelWithResourceEntity> list = null;
            var expression = LinqExtensions.True<BsModelWithResourceEntity>();
            var queryParam = queryJson.ToJObject();
            string code = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string name = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            if (!string.IsNullOrWhiteSpace(code)) expression = expression.And(t => t.ResourceCode == code);
            if (!string.IsNullOrWhiteSpace(name)) expression = expression.And(t => t.ResourceName.Contains(name));
            expression = expression.And(t => t.EnabledMark == true && t.ModelLeve == "Process");
            if (pagination != null)
                list = resourceService.BaseRepository().FindList(expression, pagination);
            else
                list = resourceService.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
            return list;
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardId]
                      ,[ProductionWorkshop]
                      ,[TestMachine]
                      ,[CheckResult]
                      ,[Remark]
                      ,[Attachment]
                      ,[Inspector]
                      ,[InspectionTime]
                      ,[EnabledMark]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IPQCDetail] where IsDeleted = 0 ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" and Id = N'{checkType}' ");
            }
            msg = "";
            try
            {
                return this.BaseRepository().FindList(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_IPQCDetailEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IPQCDetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IPQCDetailEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (IsUpdate)
                {
                    //n = this.BaseRepository().Update(entity_list);
                    StringBuilder sql = new StringBuilder();
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[QC_IPQCDetail] set ");
                            string keyValue = "";
                            //循环实体
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "ID")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ",");
                                        }
                                    }
                                    else
                                    {
                                        if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                        {
                                            sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                        }
                                    }
                                }

                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    n = this.BaseRepository().Insert(entity_list);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            int n = 0;
            msg = "";
            try
            {
                //删除
                n = this.BaseRepository().Delete(keyValue);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            QC_IPQCDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
                this.BaseRepository().Delete(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"DELETE FROM [dbo].[QC_IPQCDetail] WHERE Id=N'{keyValue}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IPQCDetailEntity</returns>
        public QC_IPQCDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IPQCDetailEntity</returns>
        public QC_IPQCDetailEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailEntity 对象</returns>
        public QC_IPQCDetailEntity Get_ExpressionEntity(Expression<Func<QC_IPQCDetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailEntity 列表</returns>
        public IEnumerable<QC_IPQCDetailEntity> Get_ExpressionList(Expression<Func<QC_IPQCDetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

        ///// <summary>
        ///// 删除主表数据并同步删除子表数据, 假删除更新删除标记
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <returns></returns>
        //public int RemoveForm(string keyValue)
        //{
        //    int result = 0;
        //    StringBuilder sql = new StringBuilder();
        //    //子表服务类
        //    RepositoryFactory<QC_IPQCDetailEntity> bomService = new RepositoryFactory<QC_IPQCDetailEntity>();

        //    QC_IPQCDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IPQCDetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IPQCDetail_Id == entity.Id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        //主表删除标记
        //        entity.IsEnabled = false;
        //        this.BaseRepository().Update(entity);
        //        if (bomEntity != null)
        //        {
        //            //子表删除标记
        //            bomEntity.IsEnabled = false;
        //            bomService.BaseRepository().Update(bomEntity);
        //        }
        //        result = 1;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<QC_IPQCDetailEntity> QC_IPQCDetailEntity_list = db2.FindList<QC_IPQCDetailEntity>(sql.ToString());
                return QC_IPQCDetailEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                DataTable QC_IPQCDetailEntity_DataTable = db2.FindTable(sql.ToString());
                return QC_IPQCDetailEntity_DataTable;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
                messageCode = parameters[2].Value.ToString();
                b = true;
            }
            catch (Exception ex)
            {
                messageCode = ex.Message;
            }
            return b;
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FlowCardId] as '流转卡编号'
                      ,[ProductionWorkshop] as '检验工序'
                      ,[TestMachine] as '检验机台'
                      ,[CheckResult] as '检验结果'
                      ,[Remark] as '备注'
                      ,[Attachment] as '附件'
                      ,[Inspector] as '检验员'
                      ,[InspectionTime] as '检验时间'
                      ,[EnabledMark] as '删除标志'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[QC_IPQCDetail] where IsDeleted = 0 ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and CreatedByCode = '{checkType}' ");
            }
            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());
                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);

                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = "过程检验记录表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                /*ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("过程检验记录表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();*/
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

    }
}
