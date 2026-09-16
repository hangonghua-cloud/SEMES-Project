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
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ExeWorkOrderService 业务服务类
    /// 4.任务编号: 生产执行工单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ExeWorkOrder_Service : RepositoryFactory<PL_ExeWorkOrderEntity>, PL_ExeWorkOrderIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ExeWorkOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[OrderType]
                      ,[Status]
                      ,[SheetsQty]
                      ,[PiecesQty]
                      ,[Yield]
                      ,[ActualSheets]
                      ,[Process]
                      ,[StartOperation]
                      ,[TransferBy]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],FinishTime,Remark
                  FROM [dbo].[PL_ExeWorkOrder] where 1=1  ");
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
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //执行工单类型 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND OrderType like N'%{queryParam["OrderType"]}%'");
                }
                //执行工单状态 是否为空进行查询
                if (!queryParam["Status"].IsEmpty())
                {
                    //sql.Append($" AND Status = N'{queryParam["Status"]}'");
                    sql.Append($" AND Status like N'%{queryParam["Status"]}%'");
                }

                //良率 是否为空进行查询
                if (!queryParam["Yield"].IsEmpty())
                {
                    //sql.Append($" AND Yield = N'{queryParam["Yield"]}'");
                    sql.Append($" AND Yield like N'%{queryParam["Yield"]}%'");
                }
                //放量张数 是否为空进行查询
                if (!queryParam["ActualSheets"].IsEmpty())
                {
                    //sql.Append($" AND ActualSheets = N'{queryParam["ActualSheets"]}'");
                    sql.Append($" AND ActualSheets like N'%{queryParam["ActualSheets"]}%'");
                }
                //工艺路线 是否为空进行查询
                if (!queryParam["Process"].IsEmpty())
                {
                    //sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    sql.Append($" AND Process like N'%{queryParam["Process"]}%'");
                }
                //起始工序 是否为空进行查询
                if (!queryParam["StartOperation"].IsEmpty())
                {
                    //sql.Append($" AND StartOperation = N'{queryParam["StartOperation"]}'");
                    sql.Append($" AND StartOperation like N'%{queryParam["StartOperation"]}%'");
                }
                //流转方式 是否为空进行查询
                if (!queryParam["TransferBy"].IsEmpty())
                {
                    //sql.Append($" AND TransferBy = N'{queryParam["TransferBy"]}'");
                    sql.Append($" AND TransferBy like N'%{queryParam["TransferBy"]}%'");
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
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
                      ,[ExeWorkOrder]
                      ,[OrderType]
                      ,[Status]
                      ,SheetsQty,PiecesQty
                      ,[Yield]
                      ,[ActualSheets]
                      ,[Process]
                      ,[StartOperation]
                      ,[TransferBy]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],FinishTime,Remark
                  FROM [dbo].[PL_ExeWorkOrder] where 1=1  ");
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
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //执行工单类型 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                    sql.Append($" AND OrderType like N'%{queryParam["OrderType"]}%'");
                }
                //执行工单状态 是否为空进行查询
                if (!queryParam["Status"].IsEmpty())
                {
                    //sql.Append($" AND Status = N'{queryParam["Status"]}'");
                    sql.Append($" AND Status like N'%{queryParam["Status"]}%'");
                }

                //良率 是否为空进行查询
                if (!queryParam["Yield"].IsEmpty())
                {
                    //sql.Append($" AND Yield = N'{queryParam["Yield"]}'");
                    sql.Append($" AND Yield like N'%{queryParam["Yield"]}%'");
                }
                //放量张数 是否为空进行查询
                if (!queryParam["ActualSheets"].IsEmpty())
                {
                    //sql.Append($" AND ActualSheets = N'{queryParam["ActualSheets"]}'");
                    sql.Append($" AND ActualSheets like N'%{queryParam["ActualSheets"]}%'");
                }
                //工艺路线 是否为空进行查询
                if (!queryParam["Process"].IsEmpty())
                {
                    //sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    sql.Append($" AND Process like N'%{queryParam["Process"]}%'");
                }
                //起始工序 是否为空进行查询
                if (!queryParam["StartOperation"].IsEmpty())
                {
                    //sql.Append($" AND StartOperation = N'{queryParam["StartOperation"]}'");
                    sql.Append($" AND StartOperation like N'%{queryParam["StartOperation"]}%'");
                }
                //流转方式 是否为空进行查询
                if (!queryParam["TransferBy"].IsEmpty())
                {
                    //sql.Append($" AND TransferBy = N'{queryParam["TransferBy"]}'");
                    sql.Append($" AND TransferBy like N'%{queryParam["TransferBy"]}%'");
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ExeWorkOrderEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[OrderType]
                      ,[Status]
                      ,SheetsQty,PiecesQty
                      ,[Yield]
                      ,[ActualSheets]
                      ,[Process]
                      ,[StartOperation]
                      ,[TransferBy]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],FinishTime,Remark
                  FROM [dbo].[PL_ExeWorkOrder] where 1=1  ");
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_ExeWorkOrderEntity entity, out string msg)
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
                throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_ExeWorkOrderEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ExeWorkOrderEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_ExeWorkOrder] set ");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PL_ExeWorkOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        public int RemoveForm(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }


        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
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
                sql.Append($@"DELETE FROM [dbo].[PL_ExeWorkOrder] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_ExeWorkOrderEntity</returns>
        public PL_ExeWorkOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_ExeWorkOrderEntity</returns>
        public dynamic GetEntityByQuery(string QueryField)
        {
            var sql = $@"SELECT PW.FactoryCode,
                               PW.FactoryName,
                               PW.ProductOrder,
                               PW.ContainerNO,
                               P.WhsCode,
                               P.LocationCode,
                               PE.Id,
                               PE.WorkOrder,
                               PE.ExeWorkOrder,
                               PE.OrderType,
                               PE.Status,
                               PE.AssignStatus,
                               PE.PSheetsQty,
                               PE.SheetsQty,
                               PE.PiecesQty,
                               PE.Yield,
                               PE.Process,
                               PE.StartOperation,
                               PE.TransferBy,
                               PE.ShouldNum,
                               PE.ActualNum,
                               PE.SuperNum,
                               PE.CancellingNum,
                               PE.ConsumeNum,
                               PE.Creator,
                               PE.CreateTime,
                               PE.ModifyBy,
                               PE.ModifyTime,
                               PE.IsEnabled,
                               PE.FinishTime,
                               PE.Remark,
                               PE.OrderPiecesNum,
                               PE.OrderPiecesAll,
                               PE.BatchNo,
                               PE.SupId,
                               PE.SendOutBatch,
                               --FMA.MMXH,
							   PBI.MaterialCode MMXH,
                               FMA.SmallClass,
                               P.DXZH,
                               P.MaskConsume,
                               PBI.Unit,
							   PBI.UnitName,
                               PBI.MaterialCode,
                               PBI.MaterialName
                        FROM dbo.PL_WorkOrder PW
                            INNER JOIN dbo.PL_PlanStoreIssue P
                                ON PW.WorkOrder = P.WorkOrder
                            INNER JOIN dbo.PL_ExeWorkOrder PE
                                ON P.WorkOrder = PE.WorkOrder
                            LEFT JOIN dbo.fn_GetMaterialAttrs() FMA
                                ON FMA.WorkOrder = P.WorkOrder
                            LEFT JOIN dbo.PL_BOM PB
                                ON P.WorkOrder = PB.WorkOrder
                            LEFT JOIN dbo.PL_BOMItems PBI
                                ON PB.Id = PBI.BOMId
                                   AND PBI.SmallClass = 'MM'
                        WHERE PE.Id = '{QueryField}' ";
            return this.BaseRepository().QueryFirst(sql);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_ExeWorkOrderEntity 列表</returns>
        public IEnumerable<PL_ExeWorkOrderEntity> Get_ExpressionList(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
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
        //    RepositoryFactory<PL_ExeWorkOrderEntity> bomService = new RepositoryFactory<PL_ExeWorkOrderEntity>();

        //    PL_ExeWorkOrderEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PL_ExeWorkOrderDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PL_ExeWorkOrder_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ExeWorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PL_ExeWorkOrderEntity> PL_ExeWorkOrderEntity_list = db2.FindList<PL_ExeWorkOrderEntity>(sql.ToString());
                return PL_ExeWorkOrderEntity_list;
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
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
                DataTable PL_ExeWorkOrderEntity_DataTable = db2.FindTable(sql.ToString());
                return PL_ExeWorkOrderEntity_DataTable;
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
        /// 创建日期: 2021-07-27 16:27:38
        /// 任务编号: 生产执行工单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [WorkOrder] as '工单号'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[OrderType] as '执行工单类型'
                      ,[Status] as '执行工单状态'
                      ,[SheetsQty] as '总张数'
                      ,[PiecesQty] as '总片数'
                      ,[Yield] as '良率'
                      ,[ActualSheets] as '放量张数'
                      ,[Process] as '工艺路线'
                      ,[StartOperation] as '起始工序'
                      ,[TransferBy] as '流转方式'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PL_ExeWorkOrder] where 1=1  ");
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
                string saveFileName = "生产执行工单表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("生产执行工单表", dt, true);
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

        public PL_ExeWorkOrderEntity Get_ExpressionEntity(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        public int InsertList(List<PL_ExeWorkOrderEntity> lstEntity)
        {
            return this.BaseRepository().Insert(lstEntity);
        }
    }
}
