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
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.ProduceManage;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PrinterOrderService 业务服务类
    /// 4.任务编号: 印刷计划工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PrinterOrder_Service : RepositoryFactory<PM_PrinterOrderEntity>, PM_PrinterOrderIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_PrinterOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[OrderType]
                      ,[PrinterOrder]
                      ,[PlanOrder]
                      ,[PackageProcess]
                      ,[OrderDate]
                      ,[DeliveryDate]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_PrinterOrder] where 1=1  ");
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
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //订单状态 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND OrderType like N'%{queryParam["OrderType"]}%'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["PrinterOrder"].IsEmpty())
                {
                    //sql.Append($" AND PrinterOrder = N'{queryParam["PrinterOrder"]}'");
                    sql.Append($" AND PrinterOrder like N'%{queryParam["PrinterOrder"]}%'");
                }
                //计划号 是否为空进行查询
                if (!queryParam["PlanOrder"].IsEmpty())
                {
                    //sql.Append($" AND PlanOrder = N'{queryParam["PlanOrder"]}'");
                    sql.Append($" AND PlanOrder like N'%{queryParam["PlanOrder"]}%'");
                }
                //打包工艺 是否为空进行查询
                if (!queryParam["PackageProcess"].IsEmpty())
                {
                    //sql.Append($" AND PackageProcess = N'{queryParam["PackageProcess"]}'");
                    sql.Append($" AND PackageProcess like N'%{queryParam["PackageProcess"]}%'");
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
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //制单人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //制单时间 是否为空进行查询
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT 
						P.[Id], P.[FactoryCode], P.[ProcessCode], P.[OrderType], P.[PrinterOrder], P.[PlanOrder], SUM(PW.[MeterNum]) MeterNum, 
						SUM(PW.[ReelNum]) [ReelNum], SUM(PW.[PalletNum])[PalletNum], P.[PackageProcess], P.[OrderDate], P.[DeliveryDate], P.[Remark], P.[Creator], P.[CreateTime], P.[ModifyBy], 
						P.[ModifyTime],M.ResourceName ProcessName,BP.Name CreatorName,
						SUM(PW.ActReelNum) ActReelNum,SUM(PW.ActMeterNum) ActMeterNum
                  FROM [dbo].[PM_PrinterOrder] P
				  LEFT JOIN dbo.BS_ModelWithResource M ON M.ResourceCode=P.ProcessCode
				  LEFT JOIN dbo.BS_People BP ON BP.Code=BP.Creator 
				  LEFT JOIN (
					SELECT W1.PrinterOrder,W1.MeterNum,W1.ReelNum,W1.PalletNum,SUM(W2.ReelNum) ActReelNum,SUM(W2.MeterNum) ActMeterNum
					FROM  dbo.PM_PrinterWorkOrder W1
					LEFT JOIN dbo.PM_PrinterWorkOrderBG W2 ON W2.WorkOrder = W1.WorkOrder
					GROUP BY  W1.PrinterOrder,W1.MeterNum,W1.ReelNum,W1.PalletNum
				   ) PW ON PW.PrinterOrder = P.PrinterOrder
				  where 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND P.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND P.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                //订单状态 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND P.OrderType = N'{queryParam["OrderType"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["PrinterOrder"].IsEmpty())
                {
                    //sql.Append($" AND PrinterOrder = N'{queryParam["PrinterOrder"]}'");
                    sql.Append($" AND P.PrinterOrder like N'%{queryParam["PrinterOrder"]}%'");
                }
                //计划号 是否为空进行查询
                if (!queryParam["PlanOrder"].IsEmpty())
                {
                    //sql.Append($" AND PlanOrder = N'{queryParam["PlanOrder"]}'");
                    sql.Append($" AND P.PlanOrder like N'%{queryParam["PlanOrder"]}%'");
                }

                //打包工艺 是否为空进行查询
                if (!queryParam["PackageProcess"].IsEmpty())
                {
                    sql.Append($" AND PackageProcess like N'%{queryParam["PackageProcess"]}%'");
                }
                //下单日期 是否为空进行查询
                if (!queryParam["StartOrder"].IsEmpty())
                {
                    sql.Append($" AND P.OrderDate >= N'{queryParam["StartOrder"]}'");
                }
                if (!queryParam["EndOrder"].IsEmpty())
                {
                    //sql.Append($" AND OrderDate = N'{queryParam["OrderDate"]}'");
                    sql.Append($" AND P.OrderDate <= N'{queryParam["EndOrder"]}'");
                }
                //交货日期 是否为空进行查询
                if (!queryParam["StartDelivery"].IsEmpty())
                {
                    sql.Append($" AND DeliveryDate >= N'{queryParam["StartDelivery"]}'");
                }
                if (!queryParam["EndDelivery"].IsEmpty())
                {
                    sql.Append($" AND DeliveryDate <= N'{queryParam["EndDelivery"]}'");
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
                sql.Append(@"  GROUP BY P.[Id], P.[FactoryCode], P.[ProcessCode], P.[OrderType], P.[PrinterOrder], P.[PlanOrder],
				   P.[PackageProcess], P.[OrderDate], P.[DeliveryDate], P.[Remark], P.[Creator], P.[CreateTime], P.[ModifyBy], 
				   P.[ModifyTime],M.ResourceName,BP.Name ");
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_PrinterOrderEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[OrderType]
                      ,[PrinterOrder]
                      ,[PlanOrder]
                      ,[PackageProcess]
                      ,[OrderDate]
                      ,[DeliveryDate]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_PrinterOrder] where 1=1  ");
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_PrinterOrderEntity entity, out string msg)
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_PrinterOrderEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PrinterOrderEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_PrinterOrder] set ");
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
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[PM_PrinterOrder] (
                                            [Id]
                                            ,[FactoryCode]
                                            ,[ProcessCode]
                                            ,[OrderType]
                                            ,[PrinterOrder]
                                            ,[PlanOrder]
                                            ,[PackageProcess]
                                            ,[OrderDate]
                                            ,[DeliveryDate]
                                            ,[Remark]
                                            ,[Creator]
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
                                ,N'{Save_obj.FactoryCode}'
                                ,N'{Save_obj.ProcessCode}'
                                ,N'{Save_obj.OrderType}'
                                ,N'{Save_obj.PrinterOrder}'
                                ,N'{Save_obj.PlanOrder}'
                                ,N'{Save_obj.PackageProcess}'
                                ,'{(Save_obj.OrderDate == null? DateTime.Now:Save_obj.OrderDate)}'
                                ,'{(Save_obj.DeliveryDate == null? DateTime.Now:Save_obj.DeliveryDate)}'
                                ,N'{Save_obj.Remark}'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_PrinterOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                //entity.IsDeleted = true;
                this.BaseRepository().Update(entity);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
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
                sql.Append($@"DELETE FROM [dbo].[PM_PrinterOrder] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_PrinterOrderEntity</returns>
        public PM_PrinterOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_PrinterOrderEntity</returns>
        public PM_PrinterOrderEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_PrinterOrderEntity 对象</returns>
        public PM_PrinterOrderEntity Get_ExpressionEntity(Expression<Func<PM_PrinterOrderEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_PrinterOrderEntity 列表</returns>
        public IEnumerable<PM_PrinterOrderEntity> Get_ExpressionList(Expression<Func<PM_PrinterOrderEntity, bool>> condition)
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
        //    RepositoryFactory<PM_PrinterOrderEntity> bomService = new RepositoryFactory<PM_PrinterOrderEntity>();
        
        //    PM_PrinterOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_PrinterOrderDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_PrinterOrder_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_PrinterOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_PrinterOrderEntity> PM_PrinterOrderEntity_list =  db2.FindList<PM_PrinterOrderEntity>(sql.ToString());
                return PM_PrinterOrderEntity_list;
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
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
                DataTable PM_PrinterOrderEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_PrinterOrderEntity_DataTable;
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
        /// 创建日期: 2021-08-23 16:05:42
        /// 任务编号: 印刷计划工单
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FactoryCode] as '工厂'
                      ,[ProcessCode] as '工序'
                      ,[OrderType] as '订单状态'
                      ,[PrinterOrder] as '订单号'
                      ,[PlanOrder] as '计划号'
                      ,[PackageProcess] as '打包工艺'
                      ,[OrderDate] as '下单日期'
                      ,[DeliveryDate] as '交货日期'
                      ,[Remark] as '备注'
                      ,[Creator] as '制单人'
                      ,[CreateTime] as '制单时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PM_PrinterOrder] where 1=1  ");
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
                string saveFileName = "印刷计划工单_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("印刷计划工单", dt, true);
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

        public int RemoveForm(Expression<Func<PM_PrinterOrderEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
