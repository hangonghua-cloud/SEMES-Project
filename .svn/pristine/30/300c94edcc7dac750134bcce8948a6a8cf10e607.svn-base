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
    /// 3.功能描述: PL_ProcessService 业务服务类
    /// 4.任务编号: 订单采购
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_Process_Service : RepositoryFactory<PL_ProcessEntity>, PL_ProcessIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[WorkOrder]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[ProcessName]
                      ,[MaterialClass]
                      ,[SmallClass]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_Process] where IsDeleted=0 ");
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
                //工单 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //工艺编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //工艺名称 是否为空进行查询
                if (!queryParam["ProcessName"].IsEmpty())
                {
                    //sql.Append($" AND ProcessName = N'{queryParam["ProcessName"]}'");
                    sql.Append($" AND ProcessName like N'%{queryParam["ProcessName"]}%'");
                }
                //物料分类 是否为空进行查询
                if (!queryParam["MaterialClass"].IsEmpty())
                {
                    //sql.Append($" AND MaterialClass = N'{queryParam["MaterialClass"]}'");
                    sql.Append($" AND MaterialClass like N'%{queryParam["MaterialClass"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    //sql.Append($" AND SmallClass = N'{queryParam["SmallClass"]}'");
                    sql.Append($" AND SmallClass like N'%{queryParam["SmallClass"]}%'");
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
        /// 功能描述: 获取工单下的工艺路线
        /// 创　　建: jpf
        /// 创建日期: 2022-12-26 
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="FactoryCode"></param>
        /// <param name="WorkOrder"></param>
        /// <returns></returns>
        public DataTable GetPl_ProcessList(string FactoryCode,string WorkOrder)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                SELECT b.*
                FROM dbo.PL_Process a
                inner JOIN dbo.PL_ProcessOfOperations b ON a.Id=b.ProcessId
                WHERE a.FactoryCode='{FactoryCode}' AND a.WorkOrder='{WorkOrder}'
                ORDER BY  b.SN
                  ");
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
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
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
                      ,[WorkOrder]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[ProcessName]
                      ,[MaterialClass]
                      ,[SmallClass]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_Process] where IsDeleted=0 ");
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
                //工单 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //工艺编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //工艺名称 是否为空进行查询
                if (!queryParam["ProcessName"].IsEmpty())
                {
                    //sql.Append($" AND ProcessName = N'{queryParam["ProcessName"]}'");
                    sql.Append($" AND ProcessName like N'%{queryParam["ProcessName"]}%'");
                }
                //物料分类 是否为空进行查询
                if (!queryParam["MaterialClass"].IsEmpty())
                {
                    //sql.Append($" AND MaterialClass = N'{queryParam["MaterialClass"]}'");
                    sql.Append($" AND MaterialClass like N'%{queryParam["MaterialClass"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    //sql.Append($" AND SmallClass = N'{queryParam["SmallClass"]}'");
                    sql.Append($" AND SmallClass like N'%{queryParam["SmallClass"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
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
        /// 创建日期: 2021-08-18 09:13:55
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[WorkOrder]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[ProcessName]
                      ,[MaterialClass]
                      ,[SmallClass]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_Process] where IsDeleted=0 ");
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
        /// 创建日期: 2021-08-18 09:13:55
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_ProcessEntity entity, out string msg)
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
        /// 创建日期: 2021-08-18 09:13:55
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_ProcessEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_Process] set ");
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
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
  
        /// <summary>
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
        /// </summary>
        /// <param name="condition"></param>
        public int RemoveForm(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        /// <summary>
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
        /// </summary>
        /// <param name="lstEntity"></param>
        public int Delete(List<PL_ProcessEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_ProcessEntity</returns>
        public PL_ProcessEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public PL_ProcessEntity GetEntity(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:13:55
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_ProcessEntity 列表</returns>
        public IEnumerable<PL_ProcessEntity> Get_ExpressionList(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        public bool InsertPLProcess(string factory, string processRoute, string workOrder,string startOperation)
        {

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@FactoryCode", SqlDbType.VarChar,30),
                new SqlParameter("@ProcessRoute", SqlDbType.VarChar,30),
                new SqlParameter("@WorkOrder", SqlDbType.VarChar,30),
                new SqlParameter("@StartOperation", SqlDbType.VarChar,30),
                new SqlParameter("@Resultmsg", SqlDbType.VarChar,100)
            };
            parameters[0].Value = factory;
            parameters[1].Value = processRoute;
            parameters[2].Value = workOrder;
            parameters[3].Value = startOperation;
            parameters[4].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("PL_InsertPLProcess", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
