using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using Newtonsoft.Json.Linq;
using ALP.Util.WebControl;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// [PM_PerformanceManage]表数据访问类
    /// 作者:Dragon
    /// 创建时间:2022-11-16 14:40:01
    /// </summary>
    public class PMPerformanceManageService : RepositoryFactory<PMPerformanceManageEntity>
    {
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="pagination">分页</param>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.[FactoryCode],
                               A.[FactoryName],
                               A.[WorkshopCode],
                               A.[WorkshopName],
                               A.[ProcessCode],
                               A.[ProcessName],
                               A.[PostCode],
                               A.[PostName],
                               A.[Coefficient],
                               A.[TotalCoefficient],
                               A.[BGId],
                               A.[CardCode],
                               A.[PTeamCode],
                               A.[PeopleQty],
                               A.[UserCode],
                               A.[UserName],
                               A.[PayrollDate],
                               A.[Qty],
                               A.[UnitName],
                               A.[Price],
                               A.[EquipCoefficient],
                               A.[Salary],
                               CASE A.[InfoSource] WHEN 1 THEN 'MES'
							   ELSE '人工录入' END InfoSource,
                               A.[Description],
	                           A.MaterialCode,
	                           A.MaterialName,
	                           A.Spec,
                               A.[IsDeleted],
                               A.[Remark],
                               A.[CreatorCode],
                               A.[CreatorName],
                               A.[CreateTime],
                               A.[ModifyCode],
                               A.[ModifyName],
                               A.[ModifyTime]
                        FROM [dbo].[PM_PerformanceManage] A
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["WorkshopCode"].IsEmpty())
                {
                    sql.Append($" AND A.WorkshopCode = N'{queryParam["WorkshopCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["PostCode"].IsEmpty())
                {
                    sql.Append($" AND A.PostCode = N'{queryParam["PostCode"]}'");
                }
                if (!queryParam["PostName"].IsEmpty())
                {
                    sql.Append($" AND A.PostName like N'%{queryParam["PostName"]}%'");
                }
                if (!queryParam["BGId"].IsEmpty())
                {
                    sql.Append($" AND A.BGId like N'%{queryParam["BGId"]}%'");
                }
                if (!queryParam["CardCode"].IsEmpty())
                {
                    sql.Append($" AND A.CardCode like N'%{queryParam["CardCode"]}%'");
                }
                if (!queryParam["PTeamCode"].IsEmpty())
                {
                    sql.Append($" AND A.PTeamCode like N'%{queryParam["PTeamCode"]}%'");
                }
                if (!queryParam["UserCode"].IsEmpty())
                {
                    sql.Append($" AND (A.UserCode like N'%{queryParam["UserCode"]}%' OR a.UserName LIKE '%{queryParam["UserCode"]}%' )");
                }
                if (!queryParam["PayrollDate"].IsEmpty())
                {
                    sql.Append($" AND A.PayrollDate like N'%{queryParam["PayrollDate"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
                }
                if (!queryParam["Description"].IsEmpty())
                {
                    sql.Append($" AND A.Description like N'%{queryParam["Description"]}%'");
                }
                if (!queryParam["Remark"].IsEmpty())
                {
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                if (!queryParam["CreatorCode"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorCode like N'%{queryParam["CreatorCode"]}%'");
                }
                if (!queryParam["CreatorName"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorName like N'%{queryParam["CreatorName"]}%'");
                }
                if (!queryParam["ModifyCode"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyCode like N'%{queryParam["ModifyCode"]}%'");
                }
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyName like N'%{queryParam["ModifyName"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) <= N'{queryParam["EndTime"]}'");
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
        #endregion

        #region 查询实体方法
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMPerformanceManageEntity</returns>
        public PMPerformanceManageEntity GetEntity(Expression<Func<PMPerformanceManageEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        #region 查询列表方法
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMPerformanceManageEntity列表</returns>
        public IEnumerable<PMPerformanceManageEntity> GetList(Expression<Func<PMPerformanceManageEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        #endregion

        #region 校验是否存在
        /// <summary>
        ///功能描述: 根据Expression查询实体类是否存在
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMPerformanceManageEntity列表</returns>
        public bool Any(Expression<Func<PMPerformanceManageEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).Any();
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, PMPerformanceManageEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    return this.BaseRepository().Update(entity);
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    return this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 批量保存方法
        /// <summary>
        ///功能描述: 批量 保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<PMPerformanceManageEntity> list)
        {
            try
            {
                if (isUpdate)
                {
                    StringBuilder sql = new StringBuilder();
                    if (list.Count > 0)
                    {
                        foreach (var Save_obj in list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[PM_PerformanceManage] set ");
                            string keyValue = "";
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "Id")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + " = " + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ", ");
                                        }
                                    }
                                    else
                                    {
                                        var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                        if (!hasNotMapped)
                                        {
                                            if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                            {
                                                sql_temp.Append(x.Name + " = N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                            }
                                        }
                                    }
                                }
                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id = '{keyValue}'; ");
                        }
                    }
                    return this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().Insert(list);
                    //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                    //{
                    //   bulkCopy.DestinationTableName = "PM_PerformanceManage";
                    //   //foreach(var item in new MMWhsBindMaterialDetailsEntity().GetType().GetProperties())
                    //   //{
                    //   //   bulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    //   //}
                    //   bulkCopy.WriteToServer(Tools.ToDataTable(list));//将数据源数据写入到数据库中
                    //}
                    //return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 删除方法
        /// <summary>
        ///功能描述: 根据Expression删除实体类
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>PMPerformanceManageEntity</returns>
        public int RemoveForm(Expression<Func<PMPerformanceManageEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion

        #region 工资计算明细失败查询
        /// <summary>
        ///功能描述: 工资计算明细失败查询(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="pagination">分页</param>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetPageDataTableListBySalary(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT *
                            FROM
                            (
                                SELECT a.Id,
								       '1' BGType,
                                       a.FactoryCode,
                                       a.FactoryName,
                                       c.ResourceCode WorkShopCode,
                                       c.ResourceName WorkShopName,
									   a.ProcessCode,
									   a.MachineCode,
                                       a.CardCode,
									   a.WorkOrder,
                                       d.MaterialCode,
                                       d.Spec,
                                       a.EquipCoefficient,
                                       a.ErrorReason,
                                       v1.ItemName ErrorReasonName,
                                       a.CreateTime
                                FROM dbo.PM_TranferCardBGRecord a
                                    LEFT JOIN dbo.BS_ModelWithResource b
                                        ON b.ModelLeve = 'Process'
                                           AND a.ProcessCode = b.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource c
                                        ON c.ModelLeve = 'WorkShop'
                                           AND b.ParentResource = c.ResourceCode
                                    LEFT JOIN dbo.PM_TransferCard d
                                        ON a.CardCode = d.CardCode
                                    LEFT JOIN dbo.V_DataDictionary v1
                                        ON v1.EnCode = 'ErrorReason'
                                           AND a.ErrorReason = v1.ItemValue
                                WHERE a.IsGenerated = 3 AND a.IsCalculated=1
                                UNION ALL
                                SELECT a.Id,
									  '2' BGType,
                                       a.FactoryCode,
                                       a.FactoryName,
                                       c.ResourceCode WorkShopCode,
                                       c.ResourceName WorkShopName,
									   a.BGProcess ProcessCode,
									   a.BGMachine MachineCode,
                                       a.TransferCode,
									   a.WorkOrder,
                                       a.MaterialCode,
                                       a.Spec,
                                       a.EquipCoefficient,
                                       a.ErrorReason,
                                       v1.ItemName ErrorReasonName,
                                       a.CreateTime
                                FROM dbo.PM_OwnProductBG a
                                    LEFT JOIN dbo.BS_ModelWithResource b
                                        ON b.ModelLeve = 'Process'
                                           AND a.BGProcess = b.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource c
                                        ON c.ModelLeve = 'WorkShop'
                                           AND b.ParentResource = c.ResourceCode
                                    LEFT JOIN dbo.V_DataDictionary v1
                                        ON v1.EnCode = 'ErrorReason'
                                           AND a.ErrorReason = v1.ItemValue
                                WHERE a.IsGenerated = 3 AND a.IsCalculated=1
                                UNION ALL
                                SELECT a.Id,
								       '3' BGType,
                                       a.FactoryCode,
                                       a.FactoryName,
                                       c.ResourceCode WorkShopCode,
                                       c.ResourceName WorkShopName,
									   a.ProcessCode,
									   a.MachineCode,
                                       a.CardCode,
									   a.WorkOrder,
                                       a.MaterialCode,
                                       d.Spec,
                                       a.EquipCoefficient,
                                       a.ErrorReason,
                                       v1.ItemName ErrorReasonName,
                                       a.CreateTime
                                FROM dbo.PM_PackingBGTransferCard a
                                    LEFT JOIN dbo.BS_ModelWithResource b
                                        ON b.ModelLeve = 'Process'
                                           AND a.ProcessCode = b.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource c
                                        ON c.ModelLeve = 'WorkShop'
                                           AND b.ParentResource = c.ResourceCode
                                    LEFT JOIN dbo.PM_TransferCard d
                                        ON a.CardCode = d.CardCode
                                    LEFT JOIN dbo.V_DataDictionary v1
                                        ON v1.EnCode = 'ErrorReason'
                                           AND a.ErrorReason = v1.ItemValue
                                WHERE a.IsGenerated = 3 AND a.IsCalculated=1
                            ) a where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");

                if (!queryParam["WorkShopCode"].IsEmpty())
                    sql.Append($" AND a.WorkShopCode = N'{queryParam["WorkShopCode"]}'");

                if (!queryParam["CardCode"].IsEmpty())
                    sql.Append($" AND a.CardCode like N'%{queryParam["CardCode"]}%'");

                //物料编码
                if (!queryParam["MaterialCode"].IsEmpty())
                    sql.Append($" AND a.MaterialCode LIKE '%{queryParam["MaterialCode"]}%'");

                //规格
                if (!queryParam["Spec"].IsEmpty())
                    sql.Append($" AND a.Spec like N'%{queryParam["Spec"]}%'");

                //错误原因
                if (!queryParam["ErrorReason"].IsEmpty())
                    sql.Append($" AND a.ErrorReason = N'{queryParam["ErrorReason"]}'");
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
        #endregion

        #region 导出
        /// <summary>
        ///功能描述: 导出
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetDataTableList_Export(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[FactoryName] 工厂,
                               A.[WorkshopName] 车间,
                               A.[ProcessName] 工序,
                               A.[PostName] 岗位,
                               A.[UserCode] 员工工号,
                               A.[UserName] 员工姓名,
                               A.[PayrollDate] 计薪日期,
                               A.[CardCode] 流转卡号,
                               A.MaterialCode 物料编码,
                               A.MateriaName 面膜型号,
                               A.Spec 规格,
                               A.[Qty] 生产数量,
                               A.[UnitName] 单位,
                               A.[Price] 产品单价,
                               A.[Coefficient] 岗位系数,
                               A.[TotalCoefficient] 总岗位系数,
                               A.[EquipCoefficient] 设备系数,
                               A.[Salary] 工资,
                               CASE A.[InfoSource]
                                   WHEN 1 THEN
                                       'MES'
                                   ELSE
                                       '人工录入'
                               END 信息来源,
                               A.[Description] 事项说明,
                               A.[Remark] 备注,
                               A.[CreatorName] 创建人,
                               CONVERT(VARCHAR(100), A.CreateTime, 20) 创建时间
                        FROM [dbo].[PM_PerformanceManage] A
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["WorkshopCode"].IsEmpty())
                {
                    sql.Append($" AND A.WorkshopCode = N'{queryParam["WorkshopCode"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["PostCode"].IsEmpty())
                {
                    sql.Append($" AND A.PostCode = N'{queryParam["PostCode"]}'");
                }
                if (!queryParam["PostName"].IsEmpty())
                {
                    sql.Append($" AND A.PostName like N'%{queryParam["PostName"]}%'");
                }
                if (!queryParam["BGId"].IsEmpty())
                {
                    sql.Append($" AND A.BGId like N'%{queryParam["BGId"]}%'");
                }
                if (!queryParam["CardCode"].IsEmpty())
                {
                    sql.Append($" AND A.CardCode like N'%{queryParam["CardCode"]}%'");
                }
                if (!queryParam["PTeamCode"].IsEmpty())
                {
                    sql.Append($" AND A.PTeamCode like N'%{queryParam["PTeamCode"]}%'");
                }
                if (!queryParam["UserCode"].IsEmpty())
                {
                    sql.Append($" AND (A.UserCode like N'%{queryParam["UserCode"]}%' OR a.UserName LIKE '%{queryParam["UserCode"]}%' )");
                }
                if (!queryParam["PayrollDate"].IsEmpty())
                {
                    sql.Append($" AND A.PayrollDate like N'%{queryParam["PayrollDate"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
                }
                if (!queryParam["Description"].IsEmpty())
                {
                    sql.Append($" AND A.Description like N'%{queryParam["Description"]}%'");
                }
                if (!queryParam["Remark"].IsEmpty())
                {
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                if (!queryParam["CreatorCode"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorCode like N'%{queryParam["CreatorCode"]}%'");
                }
                if (!queryParam["CreatorName"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorName like N'%{queryParam["CreatorName"]}%'");
                }
                if (!queryParam["ModifyCode"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyCode like N'%{queryParam["ModifyCode"]}%'");
                }
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyName like N'%{queryParam["ModifyName"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) <= N'{queryParam["EndTime"]}'");
                }
            }
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 绩效管理-重新计算
        public void ReCalculation(List<dynamic> list)
        {
            var msg = "";
            DataTable dt = ToDataTable(list);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@ReCalculation", dt)
            };
            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("Pro_ReCalculation", parameters);

            }
            catch (SqlException ex)
            {
                throw new Exception("重新计算失败：" + ex.Message);
            }
        }

        private DataTable ToDataTable(List<dynamic> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("BGId", System.Type.GetType("System.String"));
            dt.Columns.Add("WorkOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("ProcessCode", System.Type.GetType("System.String"));
            dt.Columns.Add("MachineCode", System.Type.GetType("System.String"));
            dt.Columns.Add("BGType", System.Type.GetType("System.String"));

            foreach (var item in list)
            {
                dt.Rows.Add(item.Id, item.WorkOrder, item.ProcessCode, item.MachineCode, item.BGType);
            }
            return dt;
        }
        #endregion
    }
}

