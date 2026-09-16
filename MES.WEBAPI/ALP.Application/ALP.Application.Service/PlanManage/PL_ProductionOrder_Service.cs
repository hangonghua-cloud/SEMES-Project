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
using System.Configuration;
using ALP.Application.UtilExtend.Offices;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Service.Material;
using ALP.Application.Service.ProduceManage;
using System.Transactions;
using ALP.Data;
using ALP.Application.Service.SystemManage;

namespace ALP.Application.Service.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProductionOrderService 业务服务类
    /// 4.任务编号: 生产订单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ProductionOrder_Service : RepositoryFactory<PL_ProductionOrderEntity>, PL_ProductionOrderIService
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProductionOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                        [Id]
                      ,[ProductOrder]
                      ,[Customer]
                      ,[OrderType]
                      ,[ProductPlanNo]
                      ,[OrderDate]
                      ,[DeliveryDate]
                      ,[BoxDate]
                      ,[OrderStatus]
                      ,[Technology]
                      ,[CreateBy]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProductionOrder] where 1=1  ");
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
                //生产订单 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //客户 是否为空进行查询
                if (!queryParam["Customer"].IsEmpty())
                {
                    //sql.Append($" AND Customer = N'{queryParam["Customer"]}'");
                    sql.Append($" AND Customer like N'%{queryParam["Customer"]}%'");
                }
                //订单类型 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND OrderType like N'%{queryParam["OrderType"]}%'");
                }
                //生产计划号 是否为空进行查询
                if (!queryParam["ProductPlanNo"].IsEmpty())
                {
                    //sql.Append($" AND ProductPlanNo = N'{queryParam["ProductPlanNo"]}'");
                    sql.Append($" AND ProductPlanNo like N'%{queryParam["ProductPlanNo"]}%'");
                }
                //下单日期 是否为空进行查询
                if (!queryParam["OrderDate"].IsEmpty())
                {
                    //sql.Append($" AND OrderDate = N'{queryParam["OrderDate"]}'");
                    sql.Append($" AND OrderDate like N'%{queryParam["OrderDate"]}%'");
                }
                //交货日期 是否为空进行查询
                if (!queryParam["DeliveryDate"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryDate = N'{queryParam["DeliveryDate"]}'");
                    sql.Append($" AND DeliveryDate like N'%{queryParam["DeliveryDate"]}%'");
                }
                //纸盒日期 是否为空进行查询
                if (!queryParam["BoxDate"].IsEmpty())
                {
                    //sql.Append($" AND BoxDate = N'{queryParam["BoxDate"]}'");
                    sql.Append($" AND BoxDate like N'%{queryParam["BoxDate"]}%'");
                }
                //订单状态 是否为空进行查询
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    //sql.Append($" AND OrderStatus = N'{queryParam["OrderStatus"]}'");
                    sql.Append($" AND OrderStatus like N'%{queryParam["OrderStatus"]}%'");
                }
                //工艺要求 是否为空进行查询
                if (!queryParam["Technology"].IsEmpty())
                {
                    //sql.Append($" AND Technology = N'{queryParam["Technology"]}'");
                    sql.Append($" AND Technology like N'%{queryParam["Technology"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["CreateBy"].IsEmpty())
                {
                    //sql.Append($" AND CreateBy = N'{queryParam["CreateBy"]}'");
                    sql.Append($" AND CreateBy like N'%{queryParam["CreateBy"]}%'");
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="userCode">用户编码</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson, string userCode = "")
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT P.[Id],
                               P.[ProductOrder],
                               P.[Customer],
                               P.[OrderType],
                               P.[ProductPlanNo],
                               P.[OrderDate],
                               P.[DeliveryDate],
                               P.[BoxDate],
                               P.[OrderStatus],
	                           v1.ItemName OrderStatusName,
                               P.[Technology],
                               P.[CreateBy],
                               P.IssueStatus,
                               bp.Name AS 'Codename',
                               P.Remark,
                               P.Salesman,
                               P.[CreateTime],
                               P.[ModifyBy],
                               P.[ModifyTime],
                               P.[AuditName],
                               P.[AuditTime],
                               K.ItemName CustomerName,
                               dbo.Fun_GetWorkOrderStatus(P.ProductOrder) WoStatus,
                               P.CDownloadUser,
                               P.CDownloadTime,
                               P.PDownloadUser,
                               P.PDownloadTime
                        FROM [dbo].[PL_ProductionOrder] P
                            LEFT JOIN dbo.Base_KeyParameterItem K
                                ON K.EnCode = 'client'
                                   AND K.ItemCode = P.Customer
                            LEFT JOIN BS_People bp
                                ON P.CreateBy = bp.Code
	                        LEFT JOIN dbo.V_DataDictionary v1 ON v1.EnCode='OrderStatus' AND p.OrderStatus=v1.ItemValue
                        WHERE P.IsDeleted = 0
                              AND EXISTS
                        (
                            SELECT 1
                            FROM dbo.PL_WorkOrder a
                            WHERE a.ProductOrder = P.ProductOrder
                                  AND a.FactoryCode IN
                                      (
                                          SELECT Code
                                          FROM dbo.fn_Split(
                                               (
                                                   SELECT FactoryCode FROM dbo.BS_People WHERE Code = '{userCode}'
                                               ),
                                               ','
                                                           )
                                      )
                        ) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                if (!queryParam["WoStatus"].IsEmpty())
                {
                    sql.Append($" AND dbo.Fun_GetWorkOrderStatus(P.ProductOrder) = N'{queryParam["WoStatus"]}'");
                }
                //生产订单 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //客户 是否为空进行查询
                if (!queryParam["Customer"].IsEmpty())
                {
                    //sql.Append($" AND Customer = N'{queryParam["Customer"]}'");
                    sql.Append($" AND (Customer like N'%{queryParam["Customer"]}%' OR K.ItemName like N'%{queryParam["Customer"]}%')");
                }
                //业务员
                if (!queryParam["Salesman"].IsEmpty())
                {
                    sql.Append($" AND P.Salesman like N'%{queryParam["Salesman"]}%' ");
                }

                //订单类型 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND OrderType like N'%{queryParam["OrderType"]}%'");
                }
                //生产计划号 是否为空进行查询
                if (!queryParam["ProductPlanNo"].IsEmpty())
                {
                    //sql.Append($" AND ProductPlanNo = N'{queryParam["ProductPlanNo"]}'");
                    sql.Append($" AND ProductPlanNo like N'%{queryParam["ProductPlanNo"]}%'");
                }

                //交货日期 是否为空进行查询
                if (!queryParam["StartPrepay"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryDate = N'{queryParam["DeliveryDate"]}'");
                    sql.Append($" AND DeliveryDate >= N'{queryParam["StartPrepay"]}'");
                }
                if (!queryParam["EndPrepay"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryDate = N'{queryParam["DeliveryDate"]}'");
                    sql.Append($" AND DeliveryDate <= N'{queryParam["EndPrepay"]}'");
                }

                //订单状态 是否为空进行查询
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    //sql.Append($" AND OrderStatus = N'{queryParam["OrderStatus"]}'");
                    sql.Append($" AND OrderStatus = N'{queryParam["OrderStatus"]}'");
                }
                //工艺要求 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND Technology = N'{queryParam["Technology"]}'");
                    sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
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
                //排除待归档的订单
                if (!queryParam["QueryFilter1"].IsEmpty())
                {
                    sql.Append($" AND NOT EXISTS(SELECT 1 FROM dbo.Base_ProductOrderFileCon WHERE ProductOrder=p.ProductOrder) ");
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProductionOrderEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[Customer]
                      ,[OrderType]
                      ,[ProductPlanNo]
                      ,[OrderDate]
                      ,[DeliveryDate]
                      ,[BoxDate]
                      ,[OrderStatus]
                      ,[Technology]
                      ,[CreateBy]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProductionOrder] where 1=1  ");
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_ProductionOrderEntity entity, out string msg)
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_ProductionOrderEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProductionOrderEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_ProductionOrder] set ");
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
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[PL_ProductionOrder] (
                                            [Id]
                                            ,[ProductOrder]
                                            ,[Customer]
                                            ,[OrderType]
                                            ,[ProductPlanNo]
                                            ,[OrderDate]
                                            ,[DeliveryDate]
                                            ,[BoxDate]
                                            ,[OrderStatus]
                                            ,[Technology]
                                            ,[CreateBy]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.ProductOrder}'
                                ,N'{Save_obj.Customer}'
                                ,N'{Save_obj.OrderType}'
                                ,N'{Save_obj.ProductPlanNo}'
                                ,'{(Save_obj.OrderDate == null ? DateTime.Now : Save_obj.OrderDate)}'
                                ,'{(Save_obj.DeliveryDate == null ? DateTime.Now : Save_obj.DeliveryDate)}'
                                ,'{(Save_obj.BoxDate == null ? DateTime.Now : Save_obj.BoxDate)}'
                                ,N'{Save_obj.OrderStatus}'
                                ,N'{Save_obj.Technology}'
                                ,N'{Save_obj.CreateBy}'
                                ,'{(Save_obj.CreateTime == null ? DateTime.Now : Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null ? DateTime.Now : Save_obj.ModifyTime)}'
                            ),");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            var b = 0;

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@keyValue", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = keyValue;
            parameters[1].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("PL_DeleteProductionOrder", parameters);
                //返回参数值
                msg = parameters[1].Value.ToString();
                b = 1;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return b;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }
        public int RemoveForm(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_ProductionOrderEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }
        public int Delete(PL_ProductionOrderEntity entity)
        {
            return this.BaseRepository().Delete(entity);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
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
                sql.Append($@"DELETE FROM [dbo].[PL_ProductionOrder] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_ProductionOrderEntity</returns>
        public PL_ProductionOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_ProductionOrderEntity</returns>
        public PL_ProductionOrderEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_ProductionOrderEntity 列表</returns>
        public IEnumerable<PL_ProductionOrderEntity> Get_ExpressionList(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
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
        //    RepositoryFactory<PL_ProductionOrderEntity> bomService = new RepositoryFactory<PL_ProductionOrderEntity>();

        //    PL_ProductionOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PL_ProductionOrderDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PL_ProductionOrder_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProductionOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PL_ProductionOrderEntity> PL_ProductionOrderEntity_list = db2.FindList<PL_ProductionOrderEntity>(sql.ToString());
                return PL_ProductionOrderEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
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
                DataTable PL_ProductionOrderEntity_DataTable = db2.FindTable(sql.ToString());
                return PL_ProductionOrderEntity_DataTable;
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProductOrder] as '生产订单'
                      ,[Customer] as '客户'
                      ,[OrderType] as '订单类型'
                      ,[ProductPlanNo] as '生产计划号'
                      ,[OrderDate] as '下单日期'
                      ,[DeliveryDate] as '交货日期'
                      ,ISNULL([BoxDate],'') as '纸盒日期'
                      ,[OrderStatus] as '订单状态'
                      ,[Technology] as '工艺要求'                                              
                  FROM [dbo].[PL_ProductionOrder] where 1=1  ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and ProductOrder = '{checkType}' ");
            }

            StringBuilder sql1 = new StringBuilder();
            sql1.Append($@"SELECT *,
                               CASE
                                   WHEN CHARINDEX('/', MaterialCode, 1) > 0 THEN
                                       SUBSTRING(MaterialCode, 1, CHARINDEX('/', MaterialCode) - 1)
                                   ELSE
                                       MaterialCode
                               END NewMaterialCode
                        INTO #WorkOrer
                        FROM dbo.PL_WorkOrder
                        WHERE ProductOrder = '{checkType}'
                              AND WorkOrderType NOT IN ( '2', '3' );

                        SELECT *,
                               CASE
                                   WHEN CHARINDEX('/', MaterialCode, 1) > 0 THEN
                                       SUBSTRING(MaterialCode, 1, CHARINDEX('/', MaterialCode) - 1)
                                   ELSE
                                       MaterialCode
                               END NewMaterialCode
                        INTO #ChildWorkOrder
                        FROM dbo.PL_WorkOrder
                        WHERE ProductOrder = '{checkType}'
                              AND OrderPallet = 0;

                        UPDATE a
                        SET a.OrderStartPallet = b.OrderStartPallet
                        FROM #ChildWorkOrder a
                            INNER JOIN #WorkOrer b
                                ON a.ContainerNO = b.ContainerNO
                                   AND a.NewMaterialCode = b.NewMaterialCode
                                   AND b.OrderStartPallet <> 0;

                        UPDATE a
                        SET a.OrderStartPallet = b.OrderStartPallet
                        FROM #WorkOrer a
                            INNER JOIN #ChildWorkOrder b
                                ON a.Id = b.Id;

                        SELECT PW.ProductOrder AS '订单号',
                               PW.CustomerPO AS 'PO号',
                               CONVERT(VARCHAR(12), OrderDate, 112) AS '下单日期',
                               CONVERT(VARCHAR(12), DeliveryDate, 112) AS '交货日期',
                               PW.MaterialCode AS '客户型号',
                               --,M.ResourceName as '工厂'
							   CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS '面膜型号',
                               PMA.MMCJ AS '面膜厂家',
                               PMA.Spec AS '规格',
                               PMA.BWXH AS '压纹',
                               PW.OrderPieces AS '总片数',
                               PW.PackPalletNum AS '托盘编码',
                               PW.ContainerNO AS '柜号',
                               PMA.UV AS 'UV',
                               PMA.KCKX AS '扣型',
                               PMA.BarCode AS '条码',
                               PW.OrderBox AS '总盒数',
							   CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.BZDHSL + '片/盒,' + CONVERT(VARCHAR(50), PW.PerPalletBoxQty) + '盒/托'
                                   ELSE
                                       PMA.BZDHSL + '片/盒,' + PMA.BZTPSL + '盒/托'
                               END AS '包装',
                               pro.Customer 客户名称
                        FROM #WorkOrer PW
                            INNER JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PMA.WorkOrder = PW.WorkOrder
                            LEFT JOIN dbo.PL_ProductionOrder pro
                                ON PW.ProductOrder = pro.ProductOrder
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON PW.FactoryCode = M.ResourceCode
                            LEFT JOIN dbo.BS_People bp
                                ON pro.CreateBy = bp.Code
                        WHERE 1 = 1
                        ORDER BY CONVERT(INT, PW.ContainerNO),
                                 PW.OrderStartPallet,
                                 PW.MaterialCode;

                        DROP TABLE #WorkOrer;
                        DROP TABLE #ChildWorkOrder; ");

            //if (!checkType.IsEmpty())
            //{
            //    //此处换上你的关键查询条件 也可以为空 查询全部
            //    sql1.Append($@"and PW.ProductOrder='{checkType}' ");
            //}
            //sql1.Append($@" ORDER BY PW.WorkOrder ");
            StringBuilder sql2 = new StringBuilder();
            sql2.Append($@"SELECT pro.AuditName AS '审核人',
                            bp.Name AS '制单人' from  dbo.PL_ProductionOrder pro

                            LEFT JOIN dbo.BS_People bp ON pro.CreateBy=bp.Code where pro.ProductOrder='{checkType}'");

            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());

                DataTable dt1 = this.BaseRepository().FindTable(sql1.ToString());
                DataTable dt2 = this.BaseRepository().FindTable(sql2.ToString());
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
                string workname = "";
                if (checkType.Contains("#"))
                {
                    workname = checkType.Substring(0, checkType.Length - 1);
                }
                else
                {
                    workname = checkType;

                }
                string saveFileName = workname + "_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcelProductionOrder("销售订单表", dt, dt1, dt2, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 生产下载
        /// 创　　建: Dragon
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export2(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProductOrder] as '生产订单'
                      ,[Customer] as '客户'
                      ,[OrderType] as '订单类型'
                      ,[ProductPlanNo] as '生产计划号'
                      ,[OrderDate] as '下单日期'
                      ,[DeliveryDate] as '交货日期'
                      ,[BoxDate] as '纸盒日期'
                      ,[OrderStatus] as '订单状态'
                      ,[Technology] as '工艺要求'                                              
                  FROM [dbo].[PL_ProductionOrder] where 1=1  ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and ProductOrder = '{checkType}' ");
            }

            StringBuilder sql1 = new StringBuilder();
            sql1.Append($@"SELECT *,
                               CASE
                                   WHEN CHARINDEX('/', MaterialCode, 1) > 0 THEN
                                       SUBSTRING(MaterialCode, 1, CHARINDEX('/', MaterialCode) - 1)
                                   ELSE
                                       MaterialCode
                               END NewMaterialCode
                        INTO #WorkOrer
                        FROM dbo.PL_WorkOrder
                        WHERE ProductOrder = '{checkType}'
                              AND WorkOrderType NOT IN ( '2', '3' );

                        SELECT *,
                               CASE
                                   WHEN CHARINDEX('/', MaterialCode, 1) > 0 THEN
                                       SUBSTRING(MaterialCode, 1, CHARINDEX('/', MaterialCode) - 1)
                                   ELSE
                                       MaterialCode
                               END NewMaterialCode
                        INTO #ChildWorkOrder
                        FROM dbo.PL_WorkOrder
                        WHERE ProductOrder = '{checkType}'
                              AND OrderPallet = 0;

                        UPDATE a
                        SET a.OrderStartPallet = b.OrderStartPallet
                        FROM #ChildWorkOrder a
                            INNER JOIN #WorkOrer b
                                ON a.ContainerNO = b.ContainerNO
                                   AND a.NewMaterialCode = b.NewMaterialCode
                                   AND b.OrderStartPallet <> 0;

                        UPDATE a
                        SET a.OrderStartPallet = b.OrderStartPallet
                        FROM #WorkOrer a
                            INNER JOIN #ChildWorkOrder b
                                ON a.Id = b.Id;

                        SELECT PW.ProductOrder AS '订单号',
                               PW.CustomerPO AS 'PO号',
                               CONVERT(VARCHAR(12), OrderDate, 112) AS '下单日期',
                               CONVERT(VARCHAR(12), DeliveryDate, 112) AS '交货日期',
                               PW.MaterialCode AS '客户型号',
                               --,M.ResourceName as '工厂'
							   CASE
                                   WHEN ISNULL(PW.IsVC,0) = 0 THEN
                                       PMA.MaterialName
                                   ELSE
                                       PW.MMXH
                               END AS '面膜型号',
                               PMA.MMCJ AS '面膜厂家',
                               PMA.Spec AS '规格',
                               PMA.BWXH AS '压纹',
                               PW.OrderPieces AS '总片数',
                               PW.OrderBox AS '总盒数',
                               PW.PackPalletNum AS '托盘编码',
                               PW.ContainerNO AS '柜号',
                               PMA.UV AS 'UV',
                               PMA.KCKX AS '扣型',
                               PMA.BarCode AS '条码'
                        FROM #WorkOrer PW
                            INNER JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PMA.WorkOrder = PW.WorkOrder
                            LEFT JOIN dbo.PL_ProductionOrder pro
                                ON PW.ProductOrder = pro.ProductOrder
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON PW.FactoryCode = M.ResourceCode
                            LEFT JOIN dbo.BS_People bp
                                ON pro.CreateBy = bp.Code
                        WHERE 1 = 1
                        ORDER BY CONVERT(INT, PW.ContainerNO),
                                 PW.OrderStartPallet,
                                 PW.MaterialCode;

                        DROP TABLE #WorkOrer;
                        DROP TABLE #ChildWorkOrder; ");

            //if (!checkType.IsEmpty())
            //{
            //    //此处换上你的关键查询条件 也可以为空 查询全部
            //    sql1.Append($@"and PW.ProductOrder='{checkType}' ");
            //}
            //sql1.Append($@" ORDER BY PW.WorkOrder ");
            StringBuilder sql2 = new StringBuilder();
            sql2.Append($@"SELECT pro.AuditName AS '审核人',
                            bp.Name AS '制单人' from  dbo.PL_ProductionOrder pro

                            LEFT JOIN dbo.BS_People bp ON pro.CreateBy=bp.Code where pro.ProductOrder='{checkType}'");

            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());

                DataTable dt1 = this.BaseRepository().FindTable(sql1.ToString());
                DataTable dt2 = this.BaseRepository().FindTable(sql2.ToString());
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
                string workname = "";
                if (checkType.Contains("#"))
                {
                    workname = checkType.Substring(0, checkType.Length - 1);
                }
                else
                {
                    workname = checkType;

                }
                string saveFileName = workname + "_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcelProductionOrder2("销售订单表", dt, dt1, dt2, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        public PL_ProductionOrderEntity Get_ExpressionEntity(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public void DataTableToSQLServer(DataTable dt1, DataTable dt2)
        {
            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                destinationConnection.Open();
                var transaction = destinationConnection.BeginTransaction();
                SqlBulkCopy bulkCopy1 = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.CheckConstraints, transaction);
                SqlBulkCopy bulkCopy2 = new SqlBulkCopy(destinationConnection, SqlBulkCopyOptions.CheckConstraints, transaction);
                try
                {
                    DataTableToOrder(dt1, bulkCopy1);
                    DataTableToWorkOrder(dt2, bulkCopy2);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    throw ex;
                }
                finally
                {
                    bulkCopy1.Close();
                    bulkCopy2.Close();
                    destinationConnection.Dispose();
                }
            }
        }

        private void DataTableToOrder(DataTable dt, SqlBulkCopy bulkCopy)
        {
            bulkCopy.DestinationTableName = "PL_ProductionOrder";//要插入的表的表名
            bulkCopy.BatchSize = dt.Rows.Count;
            //bulkCopy.ColumnMappings.Add("Id", "Id");//映射字段名 DataTable列名 ,数据库 对应的列名
            bulkCopy.ColumnMappings.Add("订单号", "ProductOrder");
            bulkCopy.ColumnMappings.Add("客户", "Customer");
            bulkCopy.ColumnMappings.Add("订单类型", "OrderType");
            bulkCopy.ColumnMappings.Add("生产计划号", "ProductPlanNo");
            bulkCopy.ColumnMappings.Add("下单日期", "OrderDate");
            bulkCopy.ColumnMappings.Add("交货日期", "DeliveryDate");
            bulkCopy.ColumnMappings.Add("纸盒日期", "BoxDate");
            bulkCopy.ColumnMappings.Add("工艺要求", "Technology");
            //bulkCopy.ColumnMappings.Add("Time", "CreateTime");
            bulkCopy.WriteToServer(dt);
            // System.Windows.Forms.MessageBox.Show("插入成功");
        }
        private void DataTableToWorkOrder(DataTable dt, SqlBulkCopy bulkCopy)
        {
            bulkCopy.DestinationTableName = "PL_WorkOrder";//要插入的表的表名
            bulkCopy.BatchSize = dt.Rows.Count;
            //bulkCopy.ColumnMappings.Add("Id", "Id");//映射字段名 DataTable列名 ,数据库 对应的列名
            bulkCopy.ColumnMappings.Add("工厂", "FactoryCode");
            bulkCopy.ColumnMappings.Add("订单号", "ProductOrder");
            bulkCopy.ColumnMappings.Add("客户PO号", "CustomerPO");
            bulkCopy.ColumnMappings.Add("柜号", "ContainerNO");
            bulkCopy.ColumnMappings.Add("客户型号", "MaterialCode");
            bulkCopy.ColumnMappings.Add("总片数", "OrderPieces");
            bulkCopy.ColumnMappings.Add("总盒数", "OrderBox");
            bulkCopy.ColumnMappings.Add("总托数", "OrderPallet");
            bulkCopy.ColumnMappings.Add("起始托号", "OrderStartPallet");
            bulkCopy.ColumnMappings.Add("是否免产", "AvoidProduce");
            //bulkCopy.ColumnMappings.Add("Time", "CreateTime");
            bulkCopy.WriteToServer(dt);
            // System.Windows.Forms.MessageBox.Show("插入成功");
        }
        public string ProductOrderImport(List<PL_ProductionOrderEntity> listPrd)
        {
            DataTable dt = ProductionOrderToDataTable(listPrd);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@PrdOrders", dt),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("P_PrdOrderImport", parameters);
                var Resultmsg = parameters[1].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return "";
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("生产订单导入失败");
                }
            }
            return "";
        }

        /// <summary>
        /// vc工单生成BOM和物料需求
        /// jpf 2022-11-22add
        /// </summary>
        /// <returns></returns>
        public string SaveMaterialRequirement(PL_ProductionOrderEntity entity, string userCode)
        {
            string msg = "";

            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@ProductOrder", SqlDbType.VarChar, 50),
                    new SqlParameter("@Creator", SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[0].Value = entity.ProductOrder;
            parameters[1].Value = userCode;
            parameters[2].Direction = ParameterDirection.Output;
            try
            {

                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_SaveMaterialRequirement", parameters);
                var Resultmsg = parameters[2].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("生成物料需求失败");
                }
            }
            return msg;
        }
        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable ProductionOrderToDataTable(List<PL_ProductionOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("Customer", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderType", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductPlanNo", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("DeliveryDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("BoxDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("OrderStatus", System.Type.GetType("System.String"));
            dt.Columns.Add("Technology", System.Type.GetType("System.String"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("CreateBy", System.Type.GetType("System.String"));
            dt.Columns.Add("CreateTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("ModifyBy", System.Type.GetType("System.String"));
            dt.Columns.Add("ModifyTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Salesman", System.Type.GetType("System.String"));
            foreach (PL_ProductionOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.ProductOrder, item.Customer, item.OrderType, item.ProductPlanNo, item.OrderDate
                    , item.DeliveryDate, item.BoxDate, item.OrderStatus, item.Technology.Trim(), item.Remark, item.CreateBy, DateTime.Now, item.ModifyBy, null, item.Salesman);
            }
            return dt;
        }

        #region SAP销售订单信息接收

        /// <summary>
        /// 工单状态同步
        /// </summary>
        /// <param name="entitys"></param>
        public void UpdateSAP_ProductionOrderSatate(List<PL_WorkOrderEntity> entitys)
        {
            var dicList = new DataItemDetailService().GetDataItemList("WorkOrderStatus").ToList();//工单状态转换成MES定义的

            var SAP_AUFNR = entitys.First().SAP_AUFNR;
            var workOrderEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.SAP_AUFNR == SAP_AUFNR);
            if (workOrderEntity != null) //正常工单
            {
                //开启事务进行数据的插入
                IDatabase db = DbFactory.UABase().BeginTrans();
                try
                {
                    foreach (var item in entitys)
                    {
                        //查看SAP工单是否存在
                        var WorkOrderEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.SAP_AUFNR == item.SAP_AUFNR);
                        if (WorkOrderEntity == null)
                        {
                            throw new Exception(item.SAP_AUFNR + "不存在无法更新");
                        }
                        WorkOrderEntity.OrderStatus = dicList.Find(t => t.Description == item.OrderStatus)?.ItemValue;
                        db.Update(WorkOrderEntity);
                    }

                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
            else  //自制半成品工单
            {
                //开启事务进行数据的插入
                IDatabase db = DbFactory.UABase().BeginTrans();
                try
                {
                    foreach (var item in entitys)
                    {
                        //查看SAP工单是否存在
                        var WorkOrderEntity = new PM_OwnProductOrder_Service().Get_ExpressionEntity(t => t.SAP_AUFNR == item.SAP_AUFNR);
                        if (WorkOrderEntity == null)
                        {
                            throw new Exception(item.SAP_AUFNR + "不存在无法更新");
                        }
                        WorkOrderEntity.OrderStatus = dicList.Find(t => t.Description == item.OrderStatus)?.ItemValue;
                        db.Update(WorkOrderEntity);
                    }

                    db.Commit();
                }
                catch (Exception)
                {
                    db.Rollback();
                    throw;
                }
                finally
                {
                    db.Close();
                }
            }
        }

        /// <summary>
        /// 创建：jpf
        /// 时间：2024-3-20 13:39:51
        /// 描述：SAP销售订单信息接收
        /// </summary>
        /// <param name="lstEntity"></param>
        /// <returns></returns>
        public string SapProductOrderImport(List<SAPPL_ProductionOrderEntity> lstEntity)
        {
            var msg = "";
            var Resultmsg = "";
            List<PL_ProductionOrderEntity> lstProductOrder = new List<PL_ProductionOrderEntity>();
            List<PL_WorkOrderEntity> lstWorkOrder = new List<PL_WorkOrderEntity>();
            List<PL_WorkOrderEntity> lstWorkOrder_Update = new List<PL_WorkOrderEntity>();

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();

                var orderTypeList = new DataItemDetailService().GetDataItemList("OrderType").ToList();
                foreach (var item in lstEntity)
                {
                    var sapProductionOrderEntity = item.ProductionOrderEntity;
                    sapProductionOrderEntity.OrderType = orderTypeList.Find(t => t.Description.Contains(sapProductionOrderEntity.OrderType))?.ItemValue;
                    #region 销售订单
                    if (sapProductionOrderEntity.productionGroup == "10")
                    {
                        //判断订单是否存在，需要修改还是新增
                        var productionOrderEntity = Get_ExpressionEntity(t => t.ProductOrder == sapProductionOrderEntity.ProductOrder);
                        #region 新增
                        if (productionOrderEntity == null)
                        {

                            if (string.IsNullOrEmpty(sapProductionOrderEntity.ProductOrder))
                            {
                                throw new Exception("订单号不能为空");
                            }
                            if (string.IsNullOrEmpty(sapProductionOrderEntity.Customer))
                            {
                                throw new Exception("客户编码不能为空");
                            }
                            if (string.IsNullOrEmpty(sapProductionOrderEntity.OrderType))
                            {
                                throw new Exception("订单类型没有匹配的值");
                            }
                            lstProductOrder.Add(sapProductionOrderEntity);


                            var workOrderList = item.WorkOrders;
                            //循环处理SAP物料号转换为MES物料
                            foreach (var workOrder in workOrderList.Where(t => t.isUpdate == "1"))//1：新增
                            {
                                //将SAP物料编码转化为MES的物料编码
                                var materSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", workOrder.MaterialCode);
                                var materData = new RepositoryFactory().BaseRepository().FindTable(materSql);
                                if (materData.Rows.Count == 0)
                                {
                                    throw new Exception(workOrder.MaterialCode + "物料编码未同步");
                                }
                                workOrder.MaterialCode = materData.Rows[0]["MaterialCode"].ToString();
                                lstWorkOrder.Add(workOrder);
                            }

                            foreach (var workOrder in workOrderList.Where(t => t.isUpdate == "2"))//2：修改
                            {
                                //将SAP物料编码转化为MES的物料编码
                                var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", workOrder.MaterialCode);
                                var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                                if (dtMaterial.Rows.Count == 0)
                                {
                                    throw new Exception(workOrder.MaterialCode + "物料编码未同步");
                                }
                                workOrder.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                                lstWorkOrder_Update.Add(workOrder);
                            }

                            DataTable dt = SAPProductionOrderToDataTable(lstProductOrder);

                            //调用存储过程
                            SqlParameter[] parameters = {
                                new SqlParameter("@PrdOrders", dt),
                                new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
                            };
                            parameters[1].Direction = ParameterDirection.Output;

                            db.ExecuteProcedure("P_SAPPrdOrderImport", parameters);
                            Resultmsg = parameters[1].Value.ToString();
                            if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                            {
                                return @Resultmsg.ToString();
                            }

                            if (lstWorkOrder.Any())
                            {
                                //匹配工厂编码
                                var factoryList = new BsModelWithResourceService().GetList(t => t.ModelLeve == "Factory" && t.EnabledMark == true).ToList();
                                foreach (var Witem in lstWorkOrder)
                                {
                                    Witem.FactoryCode = factoryList.Find(t => t.ResourceName == Witem.FactoryName)?.ResourceCode;
                                    Witem.MaterialCode = Witem.MaterialCode.Trim();
                                }
                                if (lstWorkOrder.Exists(t => string.IsNullOrEmpty(t.FactoryCode)))
                                    throw new Exception("部分工厂编码没有匹配到");

                                lstWorkOrder = lstWorkOrder.OrderBy(t => t.ProductOrder).ThenBy(t => t.CustomerPO).ThenBy(t => t.ContainerNO).ToList();
                                var group = lstWorkOrder.GroupBy(t => new
                                {
                                    t.FactoryCode,
                                    t.ProductOrder,
                                    t.CustomerPO,
                                    t.ContainerNO,
                                    t.MaterialCode,
                                    t.AvoidProduce,
                                    t.PackPalletNum
                                }).ToList();


                                if (group.Count != lstWorkOrder.Count)
                                {
                                    foreach (var itemS in group)
                                    {
                                        if (itemS.Count() > 1)
                                        {
                                            Resultmsg += itemS.Key.MaterialCode + ",";
                                        }
                                    }

                                    throw new Exception(Resultmsg + "导入数据存在重复");

                                }
                                //拆分 PackPalletNum 改为按订单号、柜号分组
                                //foreach (var item in listOrder.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.CustomerPO, t.ContainerNO }).ToList())
                                foreach (var Iitem in lstWorkOrder.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.ContainerNO }).ToList())
                                {
                                    decimal wholePallet = 0;
                                    foreach (var detail in Iitem)
                                    {
                                        var array = detail.PackPalletNum.Split("-");
                                        if (array.Length != 2)
                                        {
                                            throw new Exception("包装托盘码格式不正确！");
                                        }
                                        if (decimal.Parse(array[1]) - decimal.Parse(array[0]) < 0)
                                            throw new Exception($"客户型号[{detail.MaterialCode}]托盘编码[{detail.PackPalletNum}]错误，不允许导入！");

                                        detail.OrderStartPallet = decimal.Parse(array[0]);
                                        detail.newOrderPallet = decimal.Parse(array[1]);
                                        detail.OrderPallet = decimal.Parse(array[1]);
                                        if (detail.OrderPallet != 0)
                                        {
                                            detail.OrderPallet = decimal.Parse(array[1]) - decimal.Parse(array[0]) + 1;
                                        }
                                        //else
                                        //{
                                        //    detail.OrderPallet = decimal.Parse(array[1]);
                                        //}
                                        //if (detail.OrderStartPallet != 0)
                                        //{
                                        //    wholePallet += detail.OrderPallet.Value - detail.OrderStartPallet.Value + 1M;
                                        //}
                                    }
                                    //整柜总托数：下单结束托号的最大值
                                    wholePallet = Iitem.Max(t => t.newOrderPallet.Value);
                                    foreach (var detail in Iitem)
                                    {
                                        // detail.Creator = userCode;
                                        detail.OrderWholePallet = wholePallet;
                                    }
                                }
                                DataTable workdt = SAPWorkOrderToDataTable(lstWorkOrder);
                                //插入工单表
                                //调用存储过程
                                SqlParameter[] wrparameters = {
                                   new SqlParameter("@WorkOrders", workdt),
                                   new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                                   new SqlParameter("@IsAdd",SqlDbType.VarChar, 50),
                                   new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
                                };
                                wrparameters[1].Value = lstWorkOrder[0].Creator;
                                wrparameters[2].Value = "1";
                                wrparameters[3].Direction = ParameterDirection.Output;
                                db.ExecuteProcedure("P_SAPWorkOrderImport", wrparameters);
                                Resultmsg = wrparameters[3].Value.ToString();
                                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                                {
                                    // return @Resultmsg.ToString();
                                    //如果工单导入失败需要回退订单信息
                                    Delete(lstProductOrder);
                                }
                            }

                            if (lstWorkOrder_Update.Any())
                            {
                                foreach (var entity in lstWorkOrder_Update)
                                {
                                    //先判断工单表数据是否存在
                                    var workOrderEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.Orderline == entity.Orderline);
                                    if (workOrderEntity == null)
                                    {
                                        throw new Exception($"订单行号【{entity.Orderline}】不存在，不允许更新");
                                    }

                                    var plOperationList = new List<PL_ProcessOfOperationsEntity>();
                                    var plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                                    var processId = "";
                                    var plProcessEntity = new PL_ProcessEntity();
                                    var markList = new List<PM_PackingPrintMarkEntity>();//已生成唛头
                                    PL_PlanStoreIssueEntity planStoreEntity = null;
                                    List<PM_PackingPrintMarkEntity> newMarkList = new List<PM_PackingPrintMarkEntity>();//将要生成唛头

                                    var flag = false;
                                    //获取物料大小张转换的属性
                                    string sql = string.Format(@"SELECT * FROM  fn_GetMaterialAttrs() WHERE ProductOrder='{0}' AND Orderline='{1}' ", entity.ProductOrder, entity.Orderline);
                                    var data = new RepositoryFactory().BaseRepository().FindTable(sql);
                                    if (data.Rows.Count > 0)
                                    {
                                        entity.TotalSheets = Math.Ceiling((entity.OrderPieces / decimal.Parse(data.Rows[0]["DXZH"].ToString())).Value);
                                        if (entity.Yield.HasValue && entity.Yield != 0)
                                        {
                                            entity.ActualSheets = Math.Ceiling(entity.TotalSheets.Value / entity.Yield.Value);
                                            planStoreEntity = new PL_PlanStoreIssue_Service().GetEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                                            if (planStoreEntity != null)
                                            {
                                                planStoreEntity.OrderNum = entity.TotalSheets;
                                                planStoreEntity.ProductNum = entity.ActualSheets;
                                                planStoreEntity.ModifyBy = "SAP";
                                                planStoreEntity.ModifyTime = DateTime.Now;
                                            }
                                        }
                                    }

                                    entity.ModifyBy = "SAP";
                                    entity.ModifyTime = DateTime.Now;
                                    entity.PackPalletNum = entity.OrderStartPallet.ToString() + '-' + (entity.OrderStartPallet + entity.OrderPallet - 1).ToString();
                                    var oldEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                                    if (oldEntity.WorkOrderType == "1" && entity.OrderPallet != null && entity.OrderPallet != 0)
                                    {
                                        entity.PerPalletPieceQty = Math.Round(entity.OrderPieces.Value / entity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                                        entity.PerPalletBoxQty = Math.Round(entity.OrderBox.Value / entity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                                    }
                                    if (oldEntity.Process != entity.Process)
                                    {
                                        //修改工艺 todo
                                        plProcessEntity = new PL_Process_Service().GetEntity(t => t.WorkOrder == entity.WorkOrder);
                                        plProcessEntity.ProcessCode = entity.Process;
                                        plProcessEntity.ModifyBy = "SAP";
                                        plProcessEntity.ModifyTime = DateTime.Now;
                                        processId = plProcessEntity.Id;
                                        var process = entity.Process;
                                        var bsprocessEntity = new BS_Process_Service().Get_ExpressionEntity(t => t.ProcessCode == process);
                                        plProcessEntity.ProcessName = bsprocessEntity.ProcessName;
                                        plProcessEntity.MaterialClass = bsprocessEntity.MaterialClass;
                                        plProcessEntity.SmallClass = bsprocessEntity.SmallClass;
                                        //获取工艺子表
                                        var bsOperationList = new BS_ProcessOfOperations_Service().Get_ExpressionList(t => t.ProcessCode == process).ToList();
                                        var bsOperationEntity = bsOperationList.Find(t => t.OperationCode == entity.StartOperation);
                                        bsOperationList = bsOperationList.Where(t => t.SN >= bsOperationEntity.SN).ToList();//只保留起始工序及之后的工序
                                                                                                                            //获取子表属性
                                        var bsOperationIds = bsOperationList.Select(t => t.Id);
                                        var bsAttrList = new BS_ProcessOfOperationsAttr_Service().Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                                        bsOperationList.ForEach(newitem =>
                                        {
                                            //工单-工艺-工序
                                            var plOperationEntity = new PL_ProcessOfOperationsEntity();
                                            plOperationEntity.Id = Guid.NewGuid().ToString();
                                            plOperationEntity.ProcessId = processId;
                                            plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                                            plOperationEntity.OperationCode = newitem.OperationCode;
                                            plOperationEntity.OperationName = newitem.OperationName;
                                            plOperationEntity.SN = newitem.SN;
                                            plOperationEntity.OutWarehouse = newitem.OutWarehouse;
                                            plOperationEntity.CuringCycle = newitem.CuringCycle;
                                            plOperationEntity.Creator = "SAP";
                                            plOperationEntity.CreateTime = DateTime.Now;
                                            plOperationList.Add(plOperationEntity);

                                            //工单-工艺-属性
                                            var bsOperationAttrList = bsAttrList.FindAll(t => t.OperationsId == newitem.Id);
                                            bsOperationAttrList.ForEach(item2 =>
                                            {
                                                var plAttrEntity = new PL_ProcessOfOperationsAttrEntity();
                                                plAttrEntity.Id = Guid.NewGuid().ToString();
                                                plAttrEntity.ProcessId = processId;
                                                plAttrEntity.OperationsId = plOperationEntity.Id;
                                                plAttrEntity.AttrCode = item2.AttrCode;
                                                plAttrEntity.AttrName = item2.AttrName;
                                                plAttrEntity.AttrType = item2.AttrType;
                                                plAttrEntity.AttrTypeName = item2.AttrTypeName;
                                                plAttrEntity.SortCode = item2.SortCode;
                                                plAttrEntity.AttrValue = item2.AttrValue;
                                                plAttrEntity.IsEnabled = true;
                                                plAttrEntity.Creator = "SAP";
                                                plAttrEntity.CreateTime = DateTime.Now;
                                                plAttrList.Add(plAttrEntity);
                                            });
                                        });
                                    }
                                    //重新生成唛头
                                    markList = new PM_PackingPrintMark_Service().Get_ExpressionList(t => t.WorkOrder == oldEntity.WorkOrder).ToList();
                                    if (markList.Count > 0 && (oldEntity.OrderWholePallet != entity.OrderWholePallet
                                        || oldEntity.OrderStartPallet != entity.OrderStartPallet || oldEntity.OrderPieces != entity.OrderPieces))
                                    {
                                        flag = true;

                                        #region 唛头
                                        var time = DateTime.Now;
                                        decimal? currentProcessBGQty = 0;//已报工数量
                                        var productOrderEntity = Get_ExpressionEntity(t => t.ProductOrder == entity.ProductOrder);
                                        var plMaterialEntity = new PL_Material_Service().Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.FactoryCode == entity.FactoryCode && t.IsDeleted == false);
                                        var plMaterialFacetList = new PL_MaterialFacet_Service().Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id).ToList();

                                        //var perPalletPieceQty = plMaterialFacetList.Find(t => t.AttrCode == "CPBZTPSL")?.AttrValue.ToDecimal();//单托片数
                                        var perPalletPieceQty = entity.PerPalletPieceQty;//单托片数
                                        if (perPalletPieceQty == null || perPalletPieceQty == 0)
                                        {
                                            throw new Exception("产品包装托盘数量不能为空或者0");
                                        }
                                        //var boxQty = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue;//单托盒数
                                        var boxQty = entity.PerPalletBoxQty;//单托盒数
                                        var packingBGList = new PM_PackingBGTransferCard_Service().Get_ExpressionList(t => t.WorkOrder == entity.WorkOrder && t.FactoryCode == entity.FactoryCode);
                                        if (packingBGList != null && packingBGList.Count() > 0)
                                        {
                                            currentProcessBGQty = packingBGList.Sum(t => t.Qty);//当前工序已报工数量
                                        }
                                        decimal? offsetQty = 0;
                                        if (oldEntity.PackingStatus == "2")
                                            offsetQty = currentProcessBGQty;
                                        else
                                            offsetQty = entity.OrderPieces;

                                        if (offsetQty >= perPalletPieceQty)
                                        {
                                            int palletCount = (int)offsetQty / (int)perPalletPieceQty;
                                            if (palletCount > 0)
                                            {
                                                string returnNum = string.Empty;
                                                new PM_PackingPrintMark_Service().GetSerialNO("PackingPrintMark", palletCount, out returnNum, out msg);
                                                var index = Int32.Parse(returnNum);
                                                for (int i = 0; i < palletCount; i++)
                                                {
                                                    #region 8、唛头信息
                                                    var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                                    packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                                    packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                                    packingPrintMarkEntity.FactoryCode = entity.FactoryCode;
                                                    packingPrintMarkEntity.FactoryName = entity.FactoryName;
                                                    packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMddHHmmss") + (index++).ToString().PadLeft(4, '0');
                                                    packingPrintMarkEntity.Mark = entity.OrderWholePallet + "-" + (entity.OrderStartPallet + i);
                                                    packingPrintMarkEntity.ProductOrder = entity.ProductOrder;
                                                    packingPrintMarkEntity.WorkOrder = entity.WorkOrder;
                                                    packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                                    packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                                    packingPrintMarkEntity.ContainerNO = entity.ContainerNO;
                                                    packingPrintMarkEntity.CustomerPO = entity.CustomerPO;
                                                    //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                                    //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";
                                                    packingPrintMarkEntity.Quantity = entity.PerPalletBoxQty.ToString().TrimEnd('.', '0') + " ctns";
                                                    packingPrintMarkEntity.PrintStatus = "1";//未打印
                                                    packingPrintMarkEntity.Creator = "SAP";
                                                    packingPrintMarkEntity.CreateTime = time;
                                                    packingPrintMarkEntity.WorkOrderType = entity.WorkOrderType;
                                                    packingPrintMarkEntity.Status = "1";//待入库
                                                    packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                                    packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                                    packingPrintMarkEntity.PieceQty = perPalletPieceQty;
                                                    //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                                    packingPrintMarkEntity.Orderline = entity.Orderline;
                                                    newMarkList.Add(packingPrintMarkEntity);
                                                    #endregion
                                                }
                                            }
                                        }

                                        if (offsetQty == entity.OrderPieces)
                                        {
                                            if (perPalletPieceQty * newMarkList.Count < entity.OrderPieces)
                                            {
                                                var BZDHSL = plMaterialFacetList.Find(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimal();
                                                if (BZDHSL == 0 || BZDHSL == null)
                                                    throw new Exception("包装单盒片数没有值");
                                                string returnNum = string.Empty;
                                                new PM_PackingPrintMark_Service().GetSerialNO("PackingPrintMark", 1, out returnNum, out msg);
                                                var index = Int32.Parse(returnNum);
                                                #region 8、唛头信息
                                                var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                                packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                                packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                                packingPrintMarkEntity.FactoryCode = entity.FactoryCode;
                                                packingPrintMarkEntity.FactoryName = entity.FactoryName;
                                                packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMddHHmmss") + (index++).ToString().PadLeft(4, '0');
                                                packingPrintMarkEntity.Mark = entity.OrderWholePallet + "-" + (entity.OrderStartPallet + newMarkList.Count);
                                                packingPrintMarkEntity.ProductOrder = entity.ProductOrder;
                                                packingPrintMarkEntity.WorkOrder = entity.WorkOrder;
                                                packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                                packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                                packingPrintMarkEntity.ContainerNO = entity.ContainerNO;
                                                packingPrintMarkEntity.CustomerPO = entity.CustomerPO;
                                                //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                                //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";

                                                packingPrintMarkEntity.PrintStatus = "1";//未打印
                                                packingPrintMarkEntity.Creator = "SAP";
                                                packingPrintMarkEntity.CreateTime = time;
                                                packingPrintMarkEntity.WorkOrderType = entity.WorkOrderType;
                                                packingPrintMarkEntity.Status = "1";//待入库
                                                packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                                packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                                //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                                packingPrintMarkEntity.PieceQty = entity.OrderPieces - (perPalletPieceQty * newMarkList.Count);
                                                packingPrintMarkEntity.Quantity = (Math.Ceiling(packingPrintMarkEntity.PieceQty.Value / BZDHSL.Value)).ToString().TrimEnd('.', '0') + " ctns";
                                                packingPrintMarkEntity.Orderline = entity.Orderline;
                                                newMarkList.Add(packingPrintMarkEntity);
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }

                                    using (var ts = new TransactionScope())
                                    {
                                        new PL_WorkOrder_Service().SaveEntity(workOrderEntity.Id, entity, out msg);
                                        //删除属性
                                        new PL_ProcessOfOperationsAttr_Service().RemoveForm(t => t.ProcessId == processId);
                                        //删除工艺子表
                                        new PL_ProcessOfOperations_Service().RemoveForm(t => t.ProcessId == processId);
                                        new PL_Process_Service().SaveEntity(plProcessEntity.Id, plProcessEntity, out msg);
                                        new PL_ProcessOfOperations_Service().SaveEntity_List(false, "SAP", plOperationList, out msg);
                                        new PL_ProcessOfOperationsAttr_Service().SaveEntity_List(false, "SAP", plAttrList, out msg);

                                        if (flag)
                                            new PM_PackingPrintMark_Service().RemoveForm(t => t.WorkOrder == entity.WorkOrder);

                                        if (newMarkList.Count > 0)
                                            new PM_PackingPrintMark_Service().SaveEntity_List(false, "SAP", newMarkList, out msg);

                                        if (planStoreEntity != null)
                                            new PL_PlanStoreIssue_Service().SaveForm(planStoreEntity.Id, planStoreEntity);

                                        ts.Complete();
                                    }


                                }
                            }

                            return "";
                        }
                        #endregion

                        #region 修改
                        else
                        {
                            #region 订单信息修改
                            productionOrderEntity.Customer = sapProductionOrderEntity.Customer;
                            productionOrderEntity.OrderType = sapProductionOrderEntity.OrderType;
                            productionOrderEntity.ProductPlanNo = sapProductionOrderEntity.ProductPlanNo;
                            productionOrderEntity.OrderDate = sapProductionOrderEntity.OrderDate;
                            productionOrderEntity.DeliveryDate = sapProductionOrderEntity.DeliveryDate;
                            productionOrderEntity.BoxDate = sapProductionOrderEntity.BoxDate;
                            productionOrderEntity.Technology = sapProductionOrderEntity.Technology;
                            productionOrderEntity.Salesman = sapProductionOrderEntity.Salesman;
                            productionOrderEntity.Remark = sapProductionOrderEntity.Remark;
                            productionOrderEntity.ModifyBy = "SAP";
                            productionOrderEntity.ModifyTime = DateTime.Now;
                            //所有工单都关闭状态事，订单做删除标记
                            var workOrderCount = item.WorkOrders.Count;
                            var workOrderClosedCount = item.WorkOrders.Count(t => t.OrderClosed.ToUpper() == "Z1");
                            if (workOrderCount == workOrderClosedCount)
                            {
                                productionOrderEntity.IsDeleted = true;
                            }

                            this.BaseRepository().Update(productionOrderEntity);
                            #endregion

                            #region 工单信息
                            var workOrderEntities = item.WorkOrders;
                            //循环处理SAP物料号转换为MES物料
                            foreach (var workOrder in workOrderEntities.Where(t => t.isUpdate == "1"))
                            {
                                //将SAP物料编码转化为MES的物料编码
                                var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", workOrder.MaterialCode);
                                var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                                if (dtMaterial.Rows.Count == 0)
                                {
                                    throw new Exception(workOrder.MaterialCode + "物料编码未同步");
                                }
                                workOrder.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                                lstWorkOrder.Add(workOrder);
                            }

                            foreach (var workOrder in workOrderEntities.Where(t => t.isUpdate == "2"))
                            {
                                //将SAP物料编码转化为MES的物料编码
                                var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", workOrder.MaterialCode);
                                var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                                if (dtMaterial.Rows.Count == 0)
                                {
                                    throw new Exception(workOrder.MaterialCode + "物料编码未同步");
                                }
                                workOrder.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                                lstWorkOrder_Update.Add(workOrder);
                            }
                            if (lstWorkOrder.Any())
                            {
                                lstWorkOrder = lstWorkOrder.OrderBy(t => t.ProductOrder).ThenBy(t => t.CustomerPO).ThenBy(t => t.ContainerNO).ToList();
                                var group = lstWorkOrder.GroupBy(t => new
                                {
                                    t.FactoryCode,
                                    t.ProductOrder,
                                    t.CustomerPO,
                                    t.ContainerNO,
                                    t.MaterialCode,
                                    t.AvoidProduce,
                                    t.PackPalletNum
                                }).ToList();

                                if (group.Count != lstWorkOrder.Count)
                                {
                                    foreach (var itemS in group)
                                    {
                                        if (itemS.Count() > 1)
                                        {
                                            Resultmsg += itemS.Key.MaterialCode + ",";
                                        }
                                    }

                                    throw new Exception(Resultmsg + "导入数据存在重复");

                                }
                                //拆分 PackPalletNum 改为按订单号、柜号分组
                                foreach (var Iitem in lstWorkOrder.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.ContainerNO }).ToList())
                                {
                                    decimal wholePallet = 0;
                                    foreach (var detail in Iitem)
                                    {
                                        var array = detail.PackPalletNum.Split("-");
                                        if (array.Length != 2)
                                        {
                                            throw new Exception("包装托盘码格式不正确！");

                                        }
                                        if (decimal.Parse(array[1]) - decimal.Parse(array[0]) < 0)
                                            throw new Exception($@"客户型号[{detail.MaterialCode}]托盘编码[{detail.PackPalletNum}]错误，不允许导入！");

                                        detail.OrderStartPallet = decimal.Parse(array[0]);
                                        detail.newOrderPallet = decimal.Parse(array[1]);
                                        detail.OrderPallet = decimal.Parse(array[1]);
                                        if (detail.OrderPallet != 0)
                                        {
                                            detail.OrderPallet = decimal.Parse(array[1]) - decimal.Parse(array[0]) + 1;
                                        }
                                    }
                                    //整柜总托数：下单结束托号的最大值
                                    wholePallet = Iitem.Max(t => t.newOrderPallet.Value);
                                    foreach (var detail in Iitem)
                                    {
                                        // detail.Creator = userCode;
                                        detail.OrderWholePallet = wholePallet;
                                    }
                                }
                                DataTable workdt = SAPWorkOrderToDataTable(lstWorkOrder);
                                //插入工单表
                                //调用存储过程
                                SqlParameter[] wrparameters = {
                                    new SqlParameter("@WorkOrders", workdt),
                                    new SqlParameter("@Creator",SqlDbType.VarChar, 50),
                                    new SqlParameter("@IsAdd",SqlDbType.VarChar, 50),
                                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
                                 };
                                wrparameters[1].Value = lstWorkOrder[0].Creator;
                                wrparameters[2].Value = "1";
                                wrparameters[3].Direction = ParameterDirection.Output;
                                db.ExecuteProcedure("P_SAPWorkOrderImport", wrparameters);
                                Resultmsg = wrparameters[3].Value.ToString();
                                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                                {
                                    // return @Resultmsg.ToString();
                                    //如果工单导入失败需要回退订单信息
                                    Delete(lstProductOrder);
                                    throw new Exception(@Resultmsg);
                                }
                            }

                            if (lstWorkOrder_Update.Any())
                            {
                                //拆分 PackPalletNum 改为按订单号、柜号分组
                                foreach (var Iitem in lstWorkOrder_Update.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.ContainerNO }).ToList())
                                {
                                    decimal wholePallet = 0;
                                    foreach (var detail in Iitem)
                                    {
                                        var array = detail.PackPalletNum.Split("-");
                                        if (array.Length != 2)
                                        {
                                            throw new Exception("包装托盘码格式不正确！");

                                        }
                                        if (decimal.Parse(array[1]) - decimal.Parse(array[0]) < 0)
                                            throw new Exception($@"客户型号[{detail.MaterialCode}]托盘编码[{detail.PackPalletNum}]错误，不允许导入！");

                                        detail.OrderStartPallet = decimal.Parse(array[0]);
                                        detail.newOrderPallet = decimal.Parse(array[1]);
                                        detail.OrderPallet = decimal.Parse(array[1]);
                                        if (detail.OrderPallet != 0)
                                        {
                                            detail.OrderPallet = decimal.Parse(array[1]) - decimal.Parse(array[0]) + 1;
                                        }
                                    }
                                    //整柜总托数：下单结束托号的最大值
                                    wholePallet = Iitem.Max(t => t.newOrderPallet.Value);
                                    foreach (var detail in Iitem)
                                    {
                                        // detail.Creator = userCode;
                                        detail.OrderWholePallet = wholePallet;
                                    }
                                }

                                foreach (var entity in lstWorkOrder_Update)
                                {
                                    var markList = new List<PM_PackingPrintMarkEntity>();//已生成唛头
                                    PL_PlanStoreIssueEntity planStoreEntity = null;
                                    var newMarkList = new List<PM_PackingPrintMarkEntity>();//将要生成唛头

                                    //先判断工单表数据是否存在
                                    var workOrderEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.ProductOrder == entity.ProductOrder && t.Orderline == entity.Orderline);
                                    if (workOrderEntity == null)
                                    {
                                        throw new Exception("工单不存在，不允许更新");
                                    }

                                    #region 工单信息修改赋值
                                    workOrderEntity.FactoryCode = entity.FactoryCode;
                                    workOrderEntity.MaterialCode = entity.MaterialCode;
                                    workOrderEntity.Orderline = entity.Orderline;
                                    workOrderEntity.CustomerPO = entity.CustomerPO;
                                    workOrderEntity.ContainerNO = entity.ContainerNO;
                                    workOrderEntity.OrderPieces = entity.OrderPieces;
                                    workOrderEntity.OrderBox = entity.OrderBox;
                                    workOrderEntity.PackPalletNum = entity.PackPalletNum;
                                    workOrderEntity.AvoidProduce = entity.AvoidProduce;
                                    workOrderEntity.GiveTime = entity.GiveTime;
                                    workOrderEntity.BoxDate = entity.BoxDate;
                                    workOrderEntity.Harbour = entity.Harbour;
                                    workOrderEntity.Remark = entity.Remark;
                                    workOrderEntity.FactoryName = entity.FactoryName;
                                    workOrderEntity.ProductOrder = entity.ProductOrder;
                                    workOrderEntity.Creator = entity.Creator;
                                    workOrderEntity.ModifyBy = entity.Creator;
                                    workOrderEntity.ModifyTime = DateTime.Now;
                                    workOrderEntity.OrderStartPallet = entity.OrderStartPallet;
                                    workOrderEntity.newOrderPallet = entity.newOrderPallet;
                                    workOrderEntity.OrderPallet = entity.OrderPallet;
                                    workOrderEntity.OrderWholePallet = entity.OrderWholePallet;
                                    workOrderEntity.OrderClosed = entity.OrderClosed;
                                    //修改时默认等于订单片数、托数、盒数
                                    workOrderEntity.DeliveryPieces = entity.OrderPieces;
                                    workOrderEntity.DeliveryPallet = entity.OrderPallet;
                                    workOrderEntity.DeliveryBox = entity.OrderBox;
                                    workOrderEntity.FreezeFlag = entity.FreezeFlag;
                                    if (entity.OrderClosed.ToUpper().Trim() == "Z1")
                                    {
                                        workOrderEntity.IsEnabled = false;
                                    }

                                    var flag = false;
                                    //获取物料大小张转换的属性
                                    string sql = string.Format(@"SELECT * FROM  fn_GetMaterialAttrs() WHERE WorkOrder='{0}' ", workOrderEntity.WorkOrder);
                                    var data = new RepositoryFactory().BaseRepository().FindTable(sql);
                                    if (data.Rows.Count > 0)
                                    {
                                        workOrderEntity.TotalSheets = Math.Ceiling((workOrderEntity.OrderPieces / decimal.Parse(data.Rows[0]["DXZH"].ToString())).Value);
                                        if (workOrderEntity.Yield.HasValue && workOrderEntity.Yield != 0)
                                        {
                                            workOrderEntity.ActualSheets = Math.Ceiling(workOrderEntity.TotalSheets.Value / workOrderEntity.Yield.Value);
                                            planStoreEntity = new PL_PlanStoreIssue_Service().GetEntity(t => t.WorkOrder == workOrderEntity.WorkOrder && t.IsDeleted == false);
                                            if (planStoreEntity != null)
                                            {
                                                planStoreEntity.OrderNum = workOrderEntity.TotalSheets;
                                                planStoreEntity.ProductNum = workOrderEntity.ActualSheets;
                                                planStoreEntity.ModifyBy = "SAP";
                                                planStoreEntity.ModifyTime = DateTime.Now;
                                            }
                                        }
                                    }

                                    if (workOrderEntity.WorkOrderType == "1" && workOrderEntity.OrderPallet != null && workOrderEntity.OrderPallet != 0)
                                    {
                                        workOrderEntity.PerPalletPieceQty = Math.Round(workOrderEntity.OrderPieces.Value / workOrderEntity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                                        workOrderEntity.PerPalletBoxQty = Math.Round(workOrderEntity.OrderBox.Value / workOrderEntity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                                    }
                                    #endregion

                                    //重新生成唛头
                                    markList = new PM_PackingPrintMark_Service().Get_ExpressionList(t => t.WorkOrder == workOrderEntity.WorkOrder).ToList();
                                    var oldEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.WorkOrder == workOrderEntity.WorkOrder && t.FactoryCode == workOrderEntity.FactoryCode);
                                    if (markList.Count > 0 && (workOrderEntity.OrderWholePallet != oldEntity.OrderWholePallet
                                        || workOrderEntity.OrderStartPallet != oldEntity.OrderStartPallet || workOrderEntity.OrderPieces != oldEntity.OrderPieces))
                                    {
                                        flag = true;

                                        #region 唛头
                                        var time = DateTime.Now;
                                        decimal? currentProcessBGQty = 0;//已报工数量
                                        var productOrderEntity = Get_ExpressionEntity(t => t.ProductOrder == workOrderEntity.ProductOrder);
                                        var plMaterialEntity = new PL_Material_Service().Get_ExpressionEntity(t => t.WorkOrder == workOrderEntity.WorkOrder && t.FactoryCode == workOrderEntity.FactoryCode && t.IsDeleted == false);
                                        var plMaterialFacetList = new PL_MaterialFacet_Service().Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id).ToList();

                                        //var perPalletPieceQty = plMaterialFacetList.Find(t => t.AttrCode == "CPBZTPSL")?.AttrValue.ToDecimal();//单托片数
                                        var perPalletPieceQty = workOrderEntity.PerPalletPieceQty;//单托片数
                                        if (perPalletPieceQty == null || perPalletPieceQty == 0)
                                        {
                                            throw new Exception("产品包装托盘数量不能为空或者0");
                                        }
                                        //var boxQty = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue;//单托盒数
                                        var boxQty = workOrderEntity.PerPalletBoxQty;//单托盒数
                                        var packingBGList = new PM_PackingBGTransferCard_Service().Get_ExpressionList(t => t.WorkOrder == workOrderEntity.WorkOrder && t.FactoryCode == workOrderEntity.FactoryCode);
                                        if (packingBGList != null && packingBGList.Count() > 0)
                                        {
                                            currentProcessBGQty = packingBGList.Sum(t => t.Qty);//当前工序已报工数量
                                        }
                                        decimal? offsetQty = 0;
                                        if (workOrderEntity.PackingStatus == "2")
                                            offsetQty = currentProcessBGQty;
                                        else
                                            offsetQty = workOrderEntity.OrderPieces;

                                        if (offsetQty >= perPalletPieceQty)
                                        {
                                            int palletCount = (int)offsetQty / (int)perPalletPieceQty;
                                            if (palletCount > 0)
                                            {
                                                string returnNum = string.Empty;
                                                new PM_PackingPrintMark_Service().GetSerialNO("PackingPrintMark", palletCount, out returnNum, out msg);
                                                var index = Int32.Parse(returnNum);
                                                for (int i = 0; i < palletCount; i++)
                                                {
                                                    #region 8、唛头信息
                                                    var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                                    packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                                    packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                                    packingPrintMarkEntity.FactoryCode = workOrderEntity.FactoryCode;
                                                    packingPrintMarkEntity.FactoryName = workOrderEntity.FactoryName;
                                                    packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMdd") + (index++).ToString().PadLeft(4, '0');
                                                    packingPrintMarkEntity.Mark = workOrderEntity.OrderWholePallet + "-" + (workOrderEntity.OrderStartPallet + i);
                                                    packingPrintMarkEntity.ProductOrder = workOrderEntity.ProductOrder;
                                                    packingPrintMarkEntity.WorkOrder = workOrderEntity.WorkOrder;
                                                    packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                                    packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                                    packingPrintMarkEntity.ContainerNO = workOrderEntity.ContainerNO;
                                                    packingPrintMarkEntity.CustomerPO = workOrderEntity.CustomerPO;
                                                    //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                                    //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";
                                                    packingPrintMarkEntity.Quantity = workOrderEntity.PerPalletBoxQty.ToString().TrimEnd('.', '0') + " ctns";
                                                    packingPrintMarkEntity.PrintStatus = "1";//未打印
                                                    packingPrintMarkEntity.Creator = "SAP";
                                                    packingPrintMarkEntity.CreateTime = time;
                                                    packingPrintMarkEntity.WorkOrderType = workOrderEntity.WorkOrderType;
                                                    packingPrintMarkEntity.Status = "1";//待入库
                                                    //packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                                    packingPrintMarkEntity.BoxDate = (productOrderEntity.BoxDate == null ? time.ToString("ddMMyy") : productOrderEntity.BoxDate.Value.ToString("ddMMyy")) + "A";
                                                    packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                                    packingPrintMarkEntity.PieceQty = perPalletPieceQty;
                                                    //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                                    packingPrintMarkEntity.Orderline = entity.Orderline;
                                                    newMarkList.Add(packingPrintMarkEntity);
                                                    #endregion
                                                }
                                            }
                                        }

                                        if (offsetQty == entity.OrderPieces)
                                        {
                                            if (perPalletPieceQty * newMarkList.Count < workOrderEntity.OrderPieces)
                                            {
                                                var BZDHSL = plMaterialFacetList.Find(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimal();
                                                if (BZDHSL == 0 || BZDHSL == null)
                                                    throw new Exception("包装单盒片数没有值");
                                                string returnNum = string.Empty;
                                                new PM_PackingPrintMark_Service().GetSerialNO("PackingPrintMark", 1, out returnNum, out msg);
                                                var index = Int32.Parse(returnNum);
                                                #region 8、唛头信息
                                                var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                                packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                                packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                                packingPrintMarkEntity.FactoryCode = workOrderEntity.FactoryCode;
                                                packingPrintMarkEntity.FactoryName = workOrderEntity.FactoryName;
                                                packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMdd") + (index++).ToString().PadLeft(4, '0');
                                                packingPrintMarkEntity.Mark = workOrderEntity.OrderWholePallet + "-" + (workOrderEntity.OrderStartPallet + newMarkList.Count);
                                                packingPrintMarkEntity.ProductOrder = workOrderEntity.ProductOrder;
                                                packingPrintMarkEntity.WorkOrder = workOrderEntity.WorkOrder;
                                                packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                                packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                                packingPrintMarkEntity.ContainerNO = workOrderEntity.ContainerNO;
                                                packingPrintMarkEntity.CustomerPO = workOrderEntity.CustomerPO;
                                                //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                                //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";

                                                packingPrintMarkEntity.PrintStatus = "1";//未打印
                                                packingPrintMarkEntity.Creator = "SAP";
                                                packingPrintMarkEntity.CreateTime = time;
                                                packingPrintMarkEntity.WorkOrderType = workOrderEntity.WorkOrderType;
                                                packingPrintMarkEntity.Status = "1";//待入库
                                                //packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                                packingPrintMarkEntity.BoxDate = (productOrderEntity.BoxDate == null ? time.ToString("ddMMyy") : productOrderEntity.BoxDate.Value.ToString("ddMMyy")) + "A";
                                                packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                                //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                                packingPrintMarkEntity.PieceQty = workOrderEntity.OrderPieces - (perPalletPieceQty * newMarkList.Count);
                                                packingPrintMarkEntity.Quantity = (Math.Ceiling(packingPrintMarkEntity.PieceQty.Value / BZDHSL.Value)).ToString().TrimEnd('.', '0') + " ctns";
                                                packingPrintMarkEntity.Orderline = entity.Orderline;
                                                newMarkList.Add(packingPrintMarkEntity);
                                                #endregion
                                            }
                                        }
                                        #endregion
                                    }

                                    using (var ts = new TransactionScope())
                                    {
                                        new PL_WorkOrder_Service().SaveEntity(workOrderEntity.Id, workOrderEntity, out msg);

                                        if (flag)
                                            new PM_PackingPrintMark_Service().RemoveForm(t => t.WorkOrder == entity.WorkOrder);

                                        if (newMarkList.Count > 0)
                                            new PM_PackingPrintMark_Service().SaveEntity_List(false, "SAP", newMarkList, out msg);

                                        if (planStoreEntity != null)
                                            new PL_PlanStoreIssue_Service().SaveForm(planStoreEntity.Id, planStoreEntity);

                                        ts.Complete();
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion
                    }
                    #endregion

                    #region 半成品订单处理
                    else
                    {
                        var workOrderEntities = item.WorkOrders;
                        List<PMOwnSemiProductOrderEntity> lst_Insert = new List<PMOwnSemiProductOrderEntity>();
                        List<PMOwnSemiProductOrderEntity> lst_Update = new List<PMOwnSemiProductOrderEntity>();
                        foreach (var workOrder in workOrderEntities)
                        {

                            var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", workOrder.MaterialCode);
                            var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                            if (dtMaterial.Rows.Count == 0)
                            {
                                throw new Exception(workOrder.MaterialCode + "物料编码未同步");
                            }
                            workOrder.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                            var MaterialName = dtMaterial.Rows[0]["MaterialName"].ToString();

                            //新增
                            if (workOrder.isUpdate == "1")
                            {
                                var pMOwnSemiProductOrderEntity = new PMOwnSemiProductOrderEntity();
                                pMOwnSemiProductOrderEntity.ProductOrder = sapProductionOrderEntity.ProductOrder;
                                pMOwnSemiProductOrderEntity.FactoryCode = workOrder.FactoryCode;
                                pMOwnSemiProductOrderEntity.MaterialCode = workOrder.MaterialCode;
                                pMOwnSemiProductOrderEntity.MaterialName = MaterialName;
                                pMOwnSemiProductOrderEntity.Spec = workOrder.Spec;
                                pMOwnSemiProductOrderEntity.ProductQty = workOrder.OrderPieces;
                                pMOwnSemiProductOrderEntity.ContainerNO = workOrder.ContainerNO;
                                pMOwnSemiProductOrderEntity.OrderStatus = "1";
                                pMOwnSemiProductOrderEntity.Orderline = workOrder.Orderline;
                                pMOwnSemiProductOrderEntity.Remark = workOrder.Remark;
                                pMOwnSemiProductOrderEntity.CustomerCode = sapProductionOrderEntity.Customer;
                                pMOwnSemiProductOrderEntity.Create();
                                pMOwnSemiProductOrderEntity.CreateTime = DateTime.Now;
                                lst_Insert.Add(pMOwnSemiProductOrderEntity);
                            }
                            else if (workOrder.isUpdate == "2")
                            {
                                //判断半成品订单是否存在
                                var ExistpMOwnSemiEntity = new PMOwnSemiProductOrderService().GetEntity(t => t.ProductOrder == sapProductionOrderEntity.ProductOrder && t.Orderline == workOrder.Orderline);
                                if (ExistpMOwnSemiEntity == null)
                                {
                                    return $"订单行{workOrder.Orderline.ToString()}不存在";
                                }

                                ExistpMOwnSemiEntity.ProductOrder = sapProductionOrderEntity.ProductOrder;
                                ExistpMOwnSemiEntity.FactoryCode = workOrder.FactoryCode;
                                ExistpMOwnSemiEntity.MaterialCode = workOrder.MaterialCode;
                                ExistpMOwnSemiEntity.MaterialName = MaterialName;
                                ExistpMOwnSemiEntity.Spec = workOrder.Spec;
                                ExistpMOwnSemiEntity.ProductQty = workOrder.OrderPieces;
                                ExistpMOwnSemiEntity.ContainerNO = workOrder.ContainerNO;
                                ExistpMOwnSemiEntity.ModifyTime = DateTime.Now;
                                ExistpMOwnSemiEntity.Orderline = workOrder.Orderline;
                                ExistpMOwnSemiEntity.Remark = workOrder.Remark;
                                ExistpMOwnSemiEntity.CustomerCode = sapProductionOrderEntity.Customer;
                                lst_Update.Add(ExistpMOwnSemiEntity);
                            }
                        }
                        using (var ts = new TransactionScope())
                        {
                            if (lst_Insert.Count > 0)
                                new PMOwnSemiProductOrderService().SaveEntity_List(false, lst_Insert);

                            if (lst_Update.Count > 0)
                                new PMOwnSemiProductOrderService().SaveEntity_List(true, lst_Update);
                            ts.Complete();
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                var lstWorkOrderDelete = new List<PL_WorkOrderEntity>();

                if (lstWorkOrder.Count > 0) //新增工单删除
                {
                    foreach (var itemWorkOrders in lstWorkOrder.GroupBy(t => t.ProductOrder))
                    {
                        var arrOrderLine = itemWorkOrders.Select(t => t.Orderline);
                        var workOrderRange = new PL_WorkOrder_Service().Get_ExpressionList(t => t.ProductOrder == itemWorkOrders.Key
                            && arrOrderLine.Contains(t.Orderline)).ToList();
                        if (workOrderRange.Count > 0)
                        {
                            lstWorkOrderDelete.AddRange(workOrderRange);
                        }
                    }

                    if (lstWorkOrderDelete.Count > 0)
                    {
                        var arrWorkOrderId = lstWorkOrderDelete.Select(t => t.Id).ToArray();
                        var arrWorkOrder = lstWorkOrderDelete.Select(t => t.WorkOrder).ToArray();
                        //删除工单
                        new PL_WorkOrder_Service().RemoveForm(t => arrWorkOrderId.Contains(t.Id));

                        //删除工单物料
                        var plMaterialList = new PL_Material_Service().Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                        if (plMaterialList.Count > 0)
                        {
                            var arrPLMaterailId = plMaterialList.Select(t => t.Id).ToArray();
                            new PL_Material_Service().RemoveForm(t => arrPLMaterailId.Contains(t.Id));
                            new PL_MaterialFacet_Service().RemoveForm(t => arrPLMaterailId.Contains(t.MaterialId));
                        }
                        //删除工单工艺路线
                        var plProcessList = new PL_Process_Service().Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                        if (plProcessList.Count > 0)
                        {
                            var arrProcessId = plProcessList.Select(t => t.Id).ToArray();
                            new PL_Process_Service().RemoveForm(t => arrProcessId.Contains(t.Id));
                            new PL_ProcessOfOperations_Service().RemoveForm(t => arrProcessId.Contains(t.ProcessId));
                            new PL_ProcessOfOperationsAttr_Service().RemoveForm(t => arrProcessId.Contains(t.ProcessId));
                        }
                    }
                }

                throw ex;
            }
            return "";
        }

        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable SAPWorkOrderToDataTable(List<PL_WorkOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryCode", System.Type.GetType("System.String"));
            dt.Columns.Add("FactoryName", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderLine", System.Type.GetType("System.String"));
            dt.Columns.Add("CustomerPO", System.Type.GetType("System.String"));
            dt.Columns.Add("ContainerNO", System.Type.GetType("System.String"));

            dt.Columns.Add("MaterialCode", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderPieces", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderBox", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderPallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderWholePallet", System.Type.GetType("System.Decimal"));
            dt.Columns.Add("OrderStartPallet", System.Type.GetType("System.Decimal"));

            dt.Columns.Add("AvoidProduce", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Creator", System.Type.GetType("System.String"));
            dt.Columns.Add("GiveTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("PackPalletNum", System.Type.GetType("System.String"));
            dt.Columns.Add("Harbour", System.Type.GetType("System.String"));//港口
            dt.Columns.Add("BoxDate", System.Type.GetType("System.DateTime"));//纸盒日期
            dt.Columns.Add("OrderClosed", System.Type.GetType("System.String"));//订单关闭
            dt.Columns.Add("FreezeFlag", System.Type.GetType("System.Boolean"));//冻结标记

            foreach (PL_WorkOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.FactoryCode, item.FactoryName, item.ProductOrder, item.Orderline, item.CustomerPO,
                    item.ContainerNO, item.MaterialCode, item.OrderPieces, item.OrderBox, item.OrderPallet, item.OrderWholePallet,
                    item.OrderStartPallet, item.AvoidProduce, item.Creator, item.GiveTime, item.Remark, item.PackPalletNum, item.Harbour,
                    item.BoxDate, item.OrderClosed, item.FreezeFlag);
            }
            return dt;
        }
        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        private DataTable SAPProductionOrderToDataTable(List<PL_ProductionOrderEntity> list)
        {
            if (list == null || list.Count == 0) return null;
            //创建一个名为"tableName"的空表
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("Id", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductOrder", System.Type.GetType("System.String"));
            dt.Columns.Add("Customer", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderType", System.Type.GetType("System.String"));
            dt.Columns.Add("ProductPlanNo", System.Type.GetType("System.String"));
            dt.Columns.Add("OrderDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("DeliveryDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("BoxDate", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("OrderStatus", System.Type.GetType("System.String"));
            dt.Columns.Add("Technology", System.Type.GetType("System.String"));
            dt.Columns.Add("Remark", System.Type.GetType("System.String"));
            dt.Columns.Add("CreateBy", System.Type.GetType("System.String"));
            dt.Columns.Add("CreateTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("AuditName", System.Type.GetType("System.String"));
            dt.Columns.Add("AuditTime", System.Type.GetType("System.DateTime"));
            dt.Columns.Add("Salesman", System.Type.GetType("System.String"));
            foreach (PL_ProductionOrderEntity item in list)
            {
                dt.Rows.Add(Guid.NewGuid().ToString(), item.ProductOrder, item.Customer, item.OrderType, item.ProductPlanNo, item.OrderDate
                    , item.DeliveryDate, item.BoxDate, item.OrderStatus, item.Technology.Trim(), item.Remark, "SAP", DateTime.Now, item.AuditName, item.AuditTime, item.Salesman);
            }
            return dt;
        }
        #endregion

    }
}
