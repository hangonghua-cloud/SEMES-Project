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
using ALP.Application.UtilExtend.Offices;
using ALP.Application.Code.Model;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ReworkRecord_DetailService 业务服务类
    /// 4.任务编号: PM_生产返工记录明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ReworkRecord_Detail_Service : RepositoryFactory<PM_ReworkRecord_DetailEntity>, PM_ReworkRecord_Detail_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ReworkRecord_DetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ReworkId]
                      ,[CardCode]
                      ,[CardName],BGQty,Unit
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                  FROM [dbo].[PM_ReworkRecord_Detail] where IsEnabled=1 ");
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
                //返工单号 是否为空进行查询
                if (!queryParam["ReworkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ReworkOrder = N'{queryParam["ReworkOrder"]}'");
                    sql.Append($" AND ReworkOrder like N'%{queryParam["ReworkOrder"]}%'");
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
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //当前工序 是否为空进行查询
                if (!queryParam["CurrentProcess"].IsEmpty())
                {
                    //sql.Append($" AND CurrentProcess = N'{queryParam["CurrentProcess"]}'");
                    sql.Append($" AND CurrentProcess like N'%{queryParam["CurrentProcess"]}%'");
                }
                //责任工序 是否为空进行查询
                if (!queryParam["DutyProcess"].IsEmpty())
                {
                    //sql.Append($" AND DutyProcess = N'{queryParam["DutyProcess"]}'");
                    sql.Append($" AND DutyProcess like N'%{queryParam["DutyProcess"]}%'");
                }
                //返工工序 是否为空进行查询
                if (!queryParam["ReworkProcess"].IsEmpty())
                {
                    //sql.Append($" AND ReworkProcess = N'{queryParam["ReworkProcess"]}'");
                    sql.Append($" AND ReworkProcess like N'%{queryParam["ReworkProcess"]}%'");
                }
                //返工状态 是否为空进行查询
                if (!queryParam["Status"].IsEmpty())
                {
                    //sql.Append($" AND Status = N'{queryParam["Status"]}'");
                    sql.Append($" AND Status like N'%{queryParam["Status"]}%'");
                }
                //确认状态 是否为空进行查询
                if (!queryParam["ConfirmStatus"].IsEmpty())
                {
                    //sql.Append($" AND ConfirmStatus = N'{queryParam["ConfirmStatus"]}'");
                    sql.Append($" AND ConfirmStatus like N'%{queryParam["ConfirmStatus"]}%'");
                }
                //质量确认人 是否为空进行查询
                if (!queryParam["QualityConfirmUser"].IsEmpty())
                {
                    //sql.Append($" AND QualityConfirmUser = N'{queryParam["QualityConfirmUser"]}'");
                    sql.Append($" AND QualityConfirmUser like N'%{queryParam["QualityConfirmUser"]}%'");
                }
                //质量确认时间 是否为空进行查询
                if (!queryParam["QualityConfirmTime"].IsEmpty())
                {
                    //sql.Append($" AND QualityConfirmTime = N'{queryParam["QualityConfirmTime"]}'");
                    sql.Append($" AND QualityConfirmTime like N'%{queryParam["QualityConfirmTime"]}%'");
                }
                //创建人编码 是否为空进行查询
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
                //修改人编码 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND ModifyTime like N'%{queryParam["ModifyTime"]}%'");
                }
                //有效标记 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <param name="reworkProductType">返工产品类型 1：正常 2：自制</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson, string reworkProductType)
        {
            StringBuilder sql = new StringBuilder();
            if (reworkProductType == "1")
            {
                sql.Append(@"SELECT a.Id,
                               a.ReworkId,
							   a.FactoryCode,
							   a.FactoryName,
                               a.CardCode,
                               a.CardName,
							   b.BGID,
                               b.Qty,
                               b.BadQty,
                               b.PTeamName,
                               b.BGUser,
                               b.CreateTime,
                               CASE a.ReworkStatus
                                   WHEN '1' THEN
                                       '未报工'
                                   WHEN '3' THEN
                                       '已报工'
                                   WHEN '5' THEN
                                       '已报废'
                                   ELSE
                                       ''
                               END ReworkStatus
                        FROM dbo.PM_ReworkRecord_Detail a
                            LEFT JOIN
                            (
                                SELECT b1.Id BGID,b1.ReworkDId,
                                       b1.CardCode,
                                       b1.Qty,
                                       b1.BadQty,
                                       b1.BGUser,
                                       b1.CreateTime,
                                       (
                                           SELECT TOP 1
                                                  PTeamName
                                           FROM dbo.PM_TransferBGPersonRecord
                                           WHERE BGID = b1.Id
                                       ) PTeamName
                                FROM dbo.PM_TranferCardBGRecord b1 WHERE ISNULL(b1.IsRework,'')='1'
                            ) b
                                ON a.Id = b.ReworkDId
                        WHERE a.IsEnabled = 1 ");
            }
            else
            {
                sql.Append(@"SELECT a.Id,
                               a.ReworkId,
							   a.FactoryCode,
							   a.FactoryName,
                               a.CardCode,
                               a.CardName,
							   b.BGID,
                               b.Qty,
                               b.BadQty,
                               b.PTeamName,
                               b.BGUser,
                               b.CreateTime,
                               CASE a.ReworkStatus
                                   WHEN '1' THEN
                                       '未报工'
                                   WHEN '3' THEN
                                       '已报工'
                                   WHEN '5' THEN
                                       '已报废'
                                   ELSE
                                       ''
                               END ReworkStatus
                        FROM dbo.PM_ReworkRecord_Detail a
                            LEFT JOIN
                            (
								SELECT b2.ReworkDId,
							           b2.Id BGID,
                                       b2.TransferCode,
                                       b2.BGQty Qty,
                                       b2.BadQty,
                                       b2.BGUser,
                                       b2.CreateTime,
                                       (
                                           SELECT TOP 1
                                                  PTeamName
                                           FROM dbo.PM_TransferBGPersonRecord
                                           WHERE BGID = b2.Id
                                       ) PTeamName
                                FROM dbo.PM_OwnProductBG b2 WHERE ISNULL(b2.ReworkDId,'') <>''
                            ) b
                                ON a.Id = b.ReworkDId
                        WHERE a.IsEnabled = 1 ");
            }
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
                //返工主表Id 是否为空进行查询
                if (!queryParam["ReworkId"].IsEmpty())
                {
                    sql.Append($" AND a.ReworkId = N'{queryParam["ReworkId"]}'");
                    //sql.Append($" AND a.ReworkId like N'%{queryParam["ReworkId"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["CardCode"].IsEmpty())
                {
                    //sql.Append($" AND CardCode = N'{queryParam["CardCode"]}'");
                    sql.Append($" AND CardCode like N'%{queryParam["CardCode"]}%'");
                }
                //托号 是否为空进行查询
                if (!queryParam["CardName"].IsEmpty())
                {
                    //sql.Append($" AND CardName = N'{queryParam["CardName"]}'");
                    sql.Append($" AND CardName like N'%{queryParam["CardName"]}%'");
                }
                //创建人编码 是否为空进行查询
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
                //有效标记 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ReworkRecord_DetailEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ReworkId]
                      ,[CardCode]
                      ,[CardName],BGQty,Unit
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                  FROM [dbo].[PM_ReworkRecord_Detail] where IsEnabled=1 ");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_ReworkRecord_DetailEntity entity, out string msg)
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_ReworkRecord_DetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ReworkRecord_DetailEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_ReworkRecord_Detail] set ");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_ReworkRecord_DetailEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.IsEnabled = false;
                this.BaseRepository().Update(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }
        public int RemoveForm(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
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
                sql.Append($@"DELETE FROM [dbo].[PM_ReworkRecord_Detail] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_ReworkRecord_DetailEntity</returns>
        public PM_ReworkRecord_DetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_ReworkRecord_DetailEntity</returns>
        public PM_ReworkRecord_DetailEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ReworkRecord_DetailEntity 对象</returns>
        public PM_ReworkRecord_DetailEntity Get_ExpressionEntity(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ReworkRecord_DetailEntity 列表</returns>
        public IEnumerable<PM_ReworkRecord_DetailEntity> Get_ExpressionList(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition)
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
        //    RepositoryFactory<PM_ReworkRecord_DetailEntity> bomService = new RepositoryFactory<PM_ReworkRecord_DetailEntity>();

        //    PM_ReworkRecord_DetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_ReworkRecord_DetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_ReworkRecord_Detail_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ReworkRecord_DetailEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_ReworkRecord_DetailEntity> PM_ReworkRecord_DetailEntity_list = db2.FindList<PM_ReworkRecord_DetailEntity>(sql.ToString());
                return PM_ReworkRecord_DetailEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
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
                DataTable PM_ReworkRecord_DetailEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_ReworkRecord_DetailEntity_DataTable;
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ReworkId] as '返工主表Id'
                      ,[CardCode] as '流转卡编码'
                      ,[CardName] as '托号'
                      ,[Creator] as '创建人编码'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人编码'
                      ,[ModifyTime] as '修改时间'
                      ,[IsEnabled] as '有效标记'
                  FROM [dbo].[PM_ReworkRecord_Detail] where IsEnabled=1 ");
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
                string saveFileName = "PM_生产返工记录明细_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("PM_生产返工记录明细", dt, true);
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
        /// PDA扫描流转卡获取返工任务
        /// </summary>
        /// <param name="reworkId">返工任务Id</param>
        /// <param name="reworkProductType">返工产品类型</param>
        /// <returns></returns>
        public List<PM_ReworkRecordModel> GetReworkRecordDetail(string reworkId, string reworkProductType)
        {
            string sql = "";
            if (reworkProductType == "1")
            {
                sql = $@" SELECT PR.Id,
                               PR.ReworkOrder,
                               PR.ProductOrder,
                               PR.ContainerNO,
                               PR.ReworkProcess,
                               M.ResourceName ReworkProcessName,
							   PR.PalletQty RePallet,
                               PR.Status,
                               V.ItemName StatusName,
                               PR.QualityConfirmUser,
                               CONVERT(VARCHAR(16), PR.QualityConfirmTime, 120) QualityConfirmTime,
                               PD.CardName,
							   pt.ReworkDId,
                               PT.Qty,
                               PT.BadQty,
                               PT.BGUser,
                               CONVERT(VARCHAR(20), PT.CreateTime, 120) CreateTime,
							   a.CardStatus
                        FROM dbo.PM_ReworkRecord PR
                            INNER JOIN dbo.PM_ReworkRecord_Detail PD
                                ON PR.Id = PD.ReworkId
							INNER JOIN dbo.PM_TransferCard a ON pd.CardCode=a.CardCode
                            LEFT JOIN dbo.PM_TranferCardBGRecord PT
                                ON PD.Id = PT.ReworkDId
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON M.ResourceCode = PR.ReworkProcess
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'ReworkStatus'
                                   AND V.ItemValue = PR.Status
								   WHERE pd.ReworkStatus<>'5' AND  pr.Id='{reworkId}' ";
            }
            else if (reworkProductType == "2")
            {
                sql = $@"SELECT PR.Id,
                           PR.ReworkOrder,
                           pr.WorkOrder,
                           PR.ReworkProcess,
                           M.ResourceName ReworkProcessName,
						   PR.PalletQty RePallet,
                           PR.Status,
                           V.ItemName StatusName,
                           PR.QualityConfirmUser,
                           CONVERT(VARCHAR(16), PR.QualityConfirmTime, 120) QualityConfirmTime,
                           PD.CardName,
						   pt.ReworkDId,
                           pt.BGQty Qty,
                           PT.BadQty,
                           PT.BGUser,
                           CONVERT(VARCHAR(20), PT.CreateTime, 120) CreateTime
                    FROM dbo.PM_ReworkRecord PR
                        INNER JOIN dbo.PM_ReworkRecord_Detail PD
                            ON PR.Id = PD.ReworkId
                        LEFT JOIN dbo.PM_OwnProductBG PT
                            ON PD.Id = PT.ReworkDId
                        LEFT JOIN dbo.BS_ModelWithResource M
                            ON M.ResourceCode = PR.ReworkProcess
                        LEFT JOIN dbo.V_DataDictionary V
                            ON V.EnCode = 'ReworkStatus'
                               AND V.ItemValue = PR.Status
                    WHERE pd.ReworkStatus<>'5' AND  pr.Id='{reworkId}' ";
            }

            return new RepositoryFactory<PM_ReworkRecordModel>().BaseRepository().FindList(sql).ToList();
        }
    }
}
