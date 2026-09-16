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
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PackingPrintMarkService 业务服务类
    /// 4.任务编号: 包装唛头打印
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_PackingPrintMark_Service : RepositoryFactory<PM_PackingPrintMarkEntity>, PM_PackingPrintMarkIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_PackingPrintMarkEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Id,
                               PackingRecordId,
                               PackTransferCode,
                               ProductOrder,
                               WorkOrder,
                               Customer,
                               MaterialCode,
                               ContainerNO,
                               CustomerPO,
                               Spec,
                               Quantity,
                               Mark,
                               PrintStatus,
                               Creator,
                               CreateTime,
                               ModifyBy,
                               ModifyTime,Status,SourceMarkCode,WorkOrderType,
                               MMXH
                               FROM dbo.PM_PackingPrintMark WHERE 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //包装Id 是否为空进行查询
                if (!queryParam["PackingRecordId"].IsEmpty())
                {
                    //sql.Append($" AND PackingRecordId = N'{queryParam["PackingRecordId"]}'");
                    sql.Append($" AND PackingRecordId like N'%{queryParam["PackingRecordId"]}%'");
                }
                //唛头流水号 是否为空进行查询
                if (!queryParam["PackTransferCode"].IsEmpty())
                {
                    //sql.Append($" AND PackTransferCode = N'{queryParam["PackTransferCode"]}'");
                    sql.Append($" AND PackTransferCode like N'%{queryParam["PackTransferCode"]}%'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //PO号 是否为空进行查询
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    //sql.Append($" AND CustomerPO = N'{queryParam["CustomerPO"]}'");
                    sql.Append($" AND CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //唛头 是否为空进行查询
                if (!queryParam["Mark"].IsEmpty())
                {
                    //sql.Append($" AND Mark = N'{queryParam["Mark"]}'");
                    sql.Append($" AND Mark like N'%{queryParam["Mark"]}%'");
                }
                //打印状态 是否为空进行查询
                if (!queryParam["PrintStatus"].IsEmpty())
                {
                    //sql.Append($" AND PrintStatus = N'{queryParam["PrintStatus"]}'");
                    sql.Append($" AND PrintStatus like N'%{queryParam["PrintStatus"]}%'");
                }
                //打印人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //打印时间 是否为空进行查询
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Id,
                               PackingRecordId,
                               PackTransferCode,
                               ProductOrder,
                               WorkOrder,
                               Customer,
                               MaterialCode,
                               ContainerNO,
                               CustomerPO,
                               Spec,
                               Quantity,
                               Mark,
                               PrintStatus,
                               Creator,
                               CreateTime,
                               ModifyBy,
                               ModifyTime,Status,SourceMarkCode,WorkOrderType,BoxDate,
                               MMXH
                               FROM dbo.PM_PackingPrintMark WHERE 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //包装Id 是否为空进行查询
                if (!queryParam["PackingRecordId"].IsEmpty())
                {
                    //sql.Append($" AND PackingRecordId = N'{queryParam["PackingRecordId"]}'");
                    sql.Append($" AND PackingRecordId like N'%{queryParam["PackingRecordId"]}%'");
                }
                //唛头流水号 是否为空进行查询
                if (!queryParam["PackTransferCode"].IsEmpty())
                {
                    //sql.Append($" AND PackTransferCode = N'{queryParam["PackTransferCode"]}'");
                    sql.Append($" AND PackTransferCode like N'%{queryParam["PackTransferCode"]}%'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //PO号 是否为空进行查询
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    //sql.Append($" AND CustomerPO = N'{queryParam["CustomerPO"]}'");
                    sql.Append($" AND CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //唛头 是否为空进行查询
                if (!queryParam["Mark"].IsEmpty())
                {
                    //sql.Append($" AND Mark = N'{queryParam["Mark"]}'");
                    sql.Append($" AND Mark like N'%{queryParam["Mark"]}%'");
                }
                //打印状态 是否为空进行查询
                if (!queryParam["PrintStatus"].IsEmpty())
                {
                    //sql.Append($" AND PrintStatus = N'{queryParam["PrintStatus"]}'");
                    sql.Append($" AND PrintStatus like N'%{queryParam["PrintStatus"]}%'");
                }
                //打印人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //打印时间 是否为空进行查询
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="queryJson">查询条件</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetList(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT b.ProductOrder OldProductOrder,  --旧订单号
                               b.ContainerNO OldContainerNO,     --旧柜号
                               a.ProductOrder NewProductOrder,   --新订单号
                               a.ContainerNO NewContainerNO,     --新柜号
                               a.Operator,                       --更换人
                               CONVERT(VARCHAR(10), a.ChangeTime, 120) ChangeTime, --更换日期
                               COUNT(1) PalletQty                --更换托数
                        FROM dbo.PM_PackingPrintMark a
                            INNER JOIN dbo.PM_PackingPrintMark b
                                ON a.SourceMarkCode = b.PackTransferCode
                        WHERE 1 = 1
                              AND a.SourceMarkCode IS NOT NULL ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //开始日期 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10), a.ChangeTime, 120) >= '{queryParam["StartTime"]}' ");
                }
                //结束日期 是否为空进行查询
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10), a.ChangeTime, 120) <= '{queryParam["StartTime"]}' ");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND (a.ProductOrder LIKE '%{queryParam["ProductOrder"]}%' OR b.ProductOrder LIKE '%{queryParam["ProductOrder"]}%') ");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND (a.ContainerNO LIKE '%%' OR b.ContainerNO LIKE '%{queryParam["ContainerNO"]}%') ");
                }
            }
            sql.Append(@" GROUP BY b.ProductOrder,
                                 b.ContainerNO,
                                 a.ProductOrder,
                                 a.ContainerNO,
                                 a.Operator,
                                 CONVERT(VARCHAR(10), a.ChangeTime, 120) ");

            return this.BaseRepository().FindTable(sql.ToString());

        }
        public IEnumerable<PM_PackingPrintMarkEntity> GetList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_PackingPrintMarkEntity entity, out string msg)
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_PackingPrintMarkEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PackingPrintMarkEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_PackingPrintMark] set ");
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
                                        var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                        if (!hasNotMapped)
                                        {
                                            if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                            {
                                                sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                            }
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
                throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_PackingPrintMarkEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
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
                sql.Append($@"DELETE FROM [dbo].[PM_PackingPrintMark] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_PackingPrintMarkEntity</returns>
        public PM_PackingPrintMarkEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_PackingPrintMarkEntity</returns>
        public PM_PackingPrintMarkEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_PackingPrintMarkEntity 对象</returns>
        public PM_PackingPrintMarkEntity Get_ExpressionEntity(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_PackingPrintMarkEntity 列表</returns>
        public IEnumerable<PM_PackingPrintMarkEntity> Get_ExpressionList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
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
        //    RepositoryFactory<PM_PackingPrintMarkEntity> bomService = new RepositoryFactory<PM_PackingPrintMarkEntity>();

        //    PM_PackingPrintMarkEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_PackingPrintMarkDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_PackingPrintMark_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_PackingPrintMarkEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_PackingPrintMarkEntity> PM_PackingPrintMarkEntity_list = db2.FindList<PM_PackingPrintMarkEntity>(sql.ToString());
                return PM_PackingPrintMarkEntity_list;
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
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
                DataTable PM_PackingPrintMarkEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_PackingPrintMarkEntity_DataTable;
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
        /// <param name="index">生成数量</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, int index, out string returnNum, out string messageCode)
        {
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@Index", SqlDbType.Int),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Value = index;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetMultipleSerialNO", parameters);
                //返回参数值
                returnNum = parameters[2].Value.ToString();
                messageCode = parameters[3].Value.ToString();
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
        /// 创建日期: 2021-08-21 16:09:26
        /// 任务编号: 包装唛头打印
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [PackingRecordId] as '包装Id'
                      ,[PackTransferCode] as '唛头流水号'
                      ,[ProductOrder] as '订单号'
                      ,[WorkOrder] as '工单号'
                      ,[ContainerNO] as '柜号'
                      ,[CustomerPO] as 'PO号'
                      ,[Mark] as '唛头'
                      ,[PrintStatus] as '打印状态'
                      ,[Creator] as '打印人'
                      ,[CreateTime] as '打印时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PM_PackingPrintMark] where 1=1 ");
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
                string saveFileName = "包装唛头打印_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("包装唛头打印", dt, true);
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

        public int RemoveForm(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 根据唛头号查询订单信息
        /// </summary>
        /// <param name="PackTransferCode"></param>
        /// <returns></returns>
        public List<dynamic> GetOrderByTransferCode(string PackTransferCode)
        {
            var sql = $@"select a.PackTransferCode,a.ProductOrder,a.MaterialCode,a.ContainerNO,a.CustomerPO,a.Spec,b.id,b.TestMethodCoading,b.TestMethodName from  (SELECT 
					PM.PackTransferCode,PM.ProductOrder,PM.MaterialCode,PM.ContainerNO,PM.CustomerPO,PM.Spec,
					BM.GroupCode
				  FROM dbo.PM_PackingPrintMark PM
				  left JOIN  dbo.[Base_MaterialGroupBindMaterial] BM ON BM.MaterialCode = PM.MaterialCode) as a
	left join (select a.id, a.TestMethodCoading, a.TestMethodName,a.SmallClass from QC_OQCCheckConfig a where a.SmallClass in (SELECT 
	BM.GroupCode
    FROM dbo.PM_PackingPrintMark PM
	LEFT JOIN  dbo.[Base_MaterialGroupBindMaterial] BM ON BM.MaterialCode = PM.MaterialCode)) as b on a.GroupCode=b.SmallClass
                  WHERE a.PackTransferCode='{PackTransferCode}' ";
            return this.BaseRepository().Query(sql);
        }
        /// <summary>
        /// 获取唛头信息
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetMarkListByWorkOrder(string workOrder)
        {
            var sql = $@"SELECT a.PackTransferCode MarkCode,
                               a.ProductOrder,
                               a.WorkOrder,
                               a.Customer,
                               a.MaterialCode,
                               a.ContainerNO,
                               a.CustomerPO,
                               e.Spec,
                               a.Quantity,
                               a.Mark MarkName,
                               a.PrintStatus,
                               a.Status,
                               v1.ItemName StatusName,
                               a.BoxDate,
                               b.WhsCode,
                               c.ResourceName WhsName,
                               b.LocationCode,
                               d.ResourceName LocationName,
                               b.PieceQty,
                               b.BoxQty
                        FROM dbo.PM_PackingPrintMark a
                            LEFT JOIN dbo.MM_ProductStock b
                                ON a.PackTransferCode = b.MarkCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND b.WhsCode = c.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON d.EnabledMark = 1
                                   AND b.LocationCode = d.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MarkStatus'
                                   AND a.Status = v1.ItemValue
							LEFT JOIN 
							(
							    SELECT DISTINCT e1.WorkOrder,e2.AttrValue Spec FROM dbo.PL_Material e1
								INNER JOIN dbo.PL_MaterialFacet e2 ON e1.Id=e2.MaterialId
								WHERE e2.AttrCode='Spec'
							) e ON a.WorkOrder=e.WorkOrder
                        WHERE a.WorkOrder = '{workOrder}'
						ORDER BY CONVERT(INT,SUBSTRING(a.Mark,CHARINDEX('-',a.Mark)+1,2))";
            return this.BaseRepository().Query(sql);
        }
    }
}
