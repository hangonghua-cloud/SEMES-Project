using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.PlanManage;

namespace ALP.Application.Service.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProcessOfOperationsService 业务服务类
    /// 4.任务编号: 订单采购
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ProcessOfOperations_Service : RepositoryFactory<PL_ProcessOfOperationsEntity>, PL_ProcessOfOperationsIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessOfOperationsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProcessId]
                      ,[ProcessCode]
                      ,[OperationCode]
                      ,[OperationName]
                      ,[SN]
                      ,[OutWarehouse]
                      ,[CuringCycle]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperations] where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工艺Id 是否为空进行查询
                if (!queryParam["ProcessId"].IsEmpty())
                {
                    //sql.Append($" AND ProcessId = N'{queryParam["ProcessId"]}'");
                    sql.Append($" AND ProcessId like N'%{queryParam["ProcessId"]}%'");
                }
                //工艺编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["OperationCode"].IsEmpty())
                {
                    //sql.Append($" AND OperationCode = N'{queryParam["OperationCode"]}'");
                    sql.Append($" AND OperationCode like N'%{queryParam["OperationCode"]}%'");
                }
                //工序名称 是否为空进行查询
                if (!queryParam["OperationName"].IsEmpty())
                {
                    //sql.Append($" AND OperationName = N'{queryParam["OperationName"]}'");
                    sql.Append($" AND OperationName like N'%{queryParam["OperationName"]}%'");
                }
                //顺序号 是否为空进行查询
                if (!queryParam["SN"].IsEmpty())
                {
                    //sql.Append($" AND SN = N'{queryParam["SN"]}'");
                    sql.Append($" AND SN like N'%{queryParam["SN"]}%'");
                }
                //产出仓库编码 是否为空进行查询
                if (!queryParam["OutWarehouse"].IsEmpty())
                {
                    //sql.Append($" AND OutWarehouse = N'{queryParam["OutWarehouse"]}'");
                    sql.Append($" AND OutWarehouse like N'%{queryParam["OutWarehouse"]}%'");
                }
                //养生周期(天) 是否为空进行查询
                if (!queryParam["CuringCycle"].IsEmpty())
                {
                    //sql.Append($" AND CuringCycle = N'{queryParam["CuringCycle"]}'");
                    sql.Append($" AND CuringCycle like N'%{queryParam["CuringCycle"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND CreateTime like N'%{queryParam["CreateTime"]}%'");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProcessId]
                      ,[ProcessCode]
                      ,[OperationCode]
                      ,[OperationName]
                      ,[SN]
                      ,[OutWarehouse]
                      ,[CuringCycle]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperations] where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工艺Id 是否为空进行查询
                if (!queryParam["ProcessId"].IsEmpty())
                {
                    //sql.Append($" AND ProcessId = N'{queryParam["ProcessId"]}'");
                    sql.Append($" AND ProcessId like N'%{queryParam["ProcessId"]}%'");
                }
                //工艺编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["OperationCode"].IsEmpty())
                {
                    //sql.Append($" AND OperationCode = N'{queryParam["OperationCode"]}'");
                    sql.Append($" AND OperationCode like N'%{queryParam["OperationCode"]}%'");
                }
                //工序名称 是否为空进行查询
                if (!queryParam["OperationName"].IsEmpty())
                {
                    //sql.Append($" AND OperationName = N'{queryParam["OperationName"]}'");
                    sql.Append($" AND OperationName like N'%{queryParam["OperationName"]}%'");
                }
                //顺序号 是否为空进行查询
                if (!queryParam["SN"].IsEmpty())
                {
                    //sql.Append($" AND SN = N'{queryParam["SN"]}'");
                    sql.Append($" AND SN like N'%{queryParam["SN"]}%'");
                }
                //产出仓库编码 是否为空进行查询
                if (!queryParam["OutWarehouse"].IsEmpty())
                {
                    //sql.Append($" AND OutWarehouse = N'{queryParam["OutWarehouse"]}'");
                    sql.Append($" AND OutWarehouse like N'%{queryParam["OutWarehouse"]}%'");
                }
                //养生周期(天) 是否为空进行查询
                if (!queryParam["CuringCycle"].IsEmpty())
                {
                    //sql.Append($" AND CuringCycle = N'{queryParam["CuringCycle"]}'");
                    sql.Append($" AND CuringCycle like N'%{queryParam["CuringCycle"]}%'");
                }

                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = N'{queryParam["queryName"]}'");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessOfOperationsEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProcessId]
                      ,[ProcessCode]
                      ,[OperationCode]
                      ,[OperationName]
                      ,[SN]
                      ,[OutWarehouse]
                      ,[CuringCycle]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperations] where 1 = 1 ");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_ProcessOfOperationsEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_ProcessOfOperationsEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_ProcessOfOperations] set ");
                            string keyValue = "";
                            //循环实体
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
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1: 0))+ ",");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    n = this.BaseRepository().Insert(entity_list);
                    //StringBuilder sql = new StringBuilder();
                    //sql.Append($@"INSERT INTO [dbo].[PL_ProcessOfOperations] (
                    //                        [Id]
                    //                        ,[ProcessId]
                    //                        ,[ProcessCode]
                    //                        ,[OperationCode]
                    //                        ,[OperationName]
                    //                        ,[SN]
                    //                        ,[OutWarehouse]
                    //                        ,[CuringCycle]
                    //                        ,[Creator]
                    //                        ,[CreateTime]
                    //                        ,[ModifyBy]
                    //                        ,[ModifyTime]
                    //                ) VALUES ");
                    //if (entity_list.Count > 0)
                    //{
                    //    foreach (var Save_obj in entity_list)
                    //    {
                    //        sql.Append($@"(
                    //            N'{Save_obj.Id}'
                    //            ,N'{Save_obj.ProcessId}'
                    //            ,N'{Save_obj.ProcessCode}'
                    //            ,N'{Save_obj.OperationCode}'
                    //            ,N'{Save_obj.OperationName}'
                    //            ,{Save_obj.SN}
                    //            ,N'{Save_obj.OutWarehouse}'
                    //            ,{Save_obj.CuringCycle}
                    //            ,N'{Save_obj.Creator}'
                    //            ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                    //            ,N'{Save_obj.ModifyBy}'
                    //            ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
                    //        ),");
                    //    }
                    //}
                    ////批量执行更新语句
                    //n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        
 
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="condition">主键值</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_ProcessOfOperationsEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: jpf
        /// 创建日期: 2022-11-30
        /// 任务编号: 工艺路线批量更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int newRemoveForm( bool isbool,string usercode, List<PL_ProcessOfOperationsEntity> lstEntity, string UpdateByName = "")
        {
            int result = 0;
          
            if (lstEntity.Count>0)
            {
                foreach (var item in lstEntity)
                {
                    item.IsDeleted = isbool;
                    item.ModifyBy = usercode;
                    item.ModifyTime = DateTime.Now;
                    this.BaseRepository().Update(item);
                }
                //删除禁用标记
           
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }
        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_ProcessOfOperationsEntity</returns>
        public PL_ProcessOfOperationsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_ProcessOfOperationsEntity</returns>
        public PL_ProcessOfOperationsEntity GetEntityByQuery(string QueryField)
        {
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        public PL_ProcessOfOperationsEntity Get_ExpressionEntity(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:03
        /// 任务编号: 订单采购
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_ProcessOfOperationsEntity 列表</returns>
        public IEnumerable<PL_ProcessOfOperationsEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }

        #region 养生周期变更
        /// <summary>
        /// 当前报工的养生周期跟着变动
        /// </summary>
        /// <param name="workOrder"></param>
        public void UpdateHealthTime(string workOrder)
        {
            var sql = $@"UPDATE PB SET PB.HealthTime=DATEADD(HOUR,PPO.CuringCycle*24,PB.CreateTime)
                            FROM dbo.PM_TransferCard PT
                            INNER JOIN dbo.PM_TranferCardBGRecord PB ON PB.CardCode = PT.CardCode
                            INNER JOIN dbo.PL_Process PP ON PP.WorkOrder = PT.WorkOrder 
                            LEFT JOIN dbo.PL_ProcessOfOperations PPO ON PP.Id=PPO.ProcessId AND PB.ProcessCode=PPO.OperationCode
                            LEFT JOIN dbo.PM_TransferCardResume PR ON PR.CardCode = PB.CardCode AND PR.Flag='1'
                            WHERE PT.WorkOrder='{workOrder}'";
            this.BaseRepository().ExecuteBySql(sql);
        }
        #endregion 

        public DataTable GetWorkOrderOperationsItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT PPO.Id,
                               PPO.ProcessId,
	                           PP.FactoryCode,
	                           PP.FactoryName,
                               PPO.ProcessCode,
                               PPO.OperationCode,
                               PPO.OperationName,
                               PPO.SN,
                               PPO.CuringCycle,
							   PPO.WFMark,
							   CASE  PPO.WFMark WHEN '1' THEN '外发' ELSE '自制' END WFMarkName
                        FROM dbo.PL_Process PP
                            INNER JOIN dbo.PL_ProcessOfOperations PPO
                                ON PP.Id = PPO.ProcessId
                        WHERE ppo.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND PP.WorkOrder = N'{queryParam["WorkOrder"]}'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    sql.Append(@" ORDER BY PPO.SN ");
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
    }
}
