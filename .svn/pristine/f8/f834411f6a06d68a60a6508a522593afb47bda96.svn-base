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

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ReworkRecordService 业务服务类
    /// 4.任务编号: PM_生产返工记录明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ReworkRecord_Service : RepositoryFactory<PM_ReworkRecordEntity>, PM_ReworkRecord_IService
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
        public IEnumerable<PM_ReworkRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[ReworkOrder]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder],ContainerNO
                      ,[CurrentProcess]
                      ,[DutyProcess]
                      ,[ReworkProcess]
                      ,[Status]
                      ,[ConfirmStatus]
                      ,[QualityConfirmUser]
                      ,[QualityConfirmTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                      ,PalletQty
                  FROM [dbo].[PM_ReworkRecord] where IsEnabled=1 ");
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
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT *
                            FROM
                            (
                                SELECT a.Id, 
								       a.FactoryCode,
								       a.FactoryName,
								       a.ReworkProductType,              --返工产品类型
                                       a.ReworkOrder,                    --返工单号
                                       a.ProductOrder,                   --订单号
									   a.WorkOrder,
                                       b.ContainerNO,                    --柜号
                                       a.Status,                         --返工状态编码
                                       v1.ItemName StatusName,           --返工状态名称
                                       b.MaterialCode,
                                       c.MaterialName,
                                       c.MMXH,                           --面膜型号
                                       c.Spec,
                                       a.PalletQty,                      --返工托数
                                       a.Operator,
                                       a.CurrentProcess,
                                       d.ResourceName CurrentProcessName,
                                       a.DutyProcess,                    --责任工序编码
                                       e.ResourceName DutyProcessName,   --责任工序名称
                                       a.ReworkProcess,                  --返工工序编码
                                       f.ResourceName ReworkProcessName, --返工工序名称
                                       g.BGPalletyQty,
                                       a.ConfirmStatus,
                                       a.QualityConfirmUser,
                                       a.QualityConfirmTime,
                                       a.CreateTime,
                                       a.Remark
                                FROM dbo.PM_ReworkRecord a
                                    INNER JOIN dbo.PL_WorkOrder b
                                        ON a.WorkOrder = b.WorkOrder
                                    LEFT JOIN dbo.fn_GetMaterialAttrs() c
                                        ON b.MaterialCode = c.MaterialCode
                                           AND b.WorkOrder = c.WorkOrder
                                    LEFT JOIN dbo.BS_ModelWithResource d
                                        ON d.ModelLeve = 'Process'
                                           AND a.CurrentProcess = d.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource e
                                        ON d.ModelLeve = 'Process'
                                           AND a.DutyProcess = e.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource f
                                        ON d.ModelLeve = 'Process'
                                           AND a.ReworkProcess = f.ResourceCode
                                    LEFT JOIN dbo.V_DataDictionary v1
                                        ON v1.EnCode = 'ReworkStatus'
                                           AND a.Status = v1.ItemValue
                                    LEFT JOIN
                                    (
                                        SELECT detail.ReworkId,
                                               COUNT(1) BGPalletyQty
                                        FROM dbo.PM_ReworkRecord_Detail detail
                                        WHERE detail.IsEnabled = 1
                                              AND EXISTS
                                        (
                                            SELECT 1
                                            FROM dbo.PM_TranferCardBGRecord bg
                                            WHERE bg.IsEnabled = 1
                                                  AND bg.ReworkDId = detail.Id
                                        )
                                        GROUP BY detail.ReworkId
                                    ) g
                                        ON a.Id = g.ReworkId
                                WHERE a.IsEnabled = 1
                                      AND a.ReworkProductType = '1'
                                UNION ALL
                                SELECT a.Id,
								       a.FactoryCode,
								       a.FactoryName,
								       a.ReworkProductType,              --返工产品类型
                                       a.ReworkOrder,                    --返工单号
                                       a.ProductOrder,                   --订单号
									   a.WorkOrder,
                                       '' ContainerNO,                   --柜号
                                       a.Status,                         --返工状态编码
                                       v1.ItemName StatusName,           --返工状态名称
                                       b.MaterialCode,
                                       c.MaterialName,
                                       c.MaterialName MMXH,                           --面膜型号
                                       c.Spec,
                                       a.PalletQty,                      --返工托数
                                       a.Operator,
                                       a.CurrentProcess,
                                       d.ResourceName CurrentProcessName,
                                       a.DutyProcess,                    --责任工序编码
                                       e.ResourceName DutyProcessName,   --责任工序名称
                                       a.ReworkProcess,                  --返工工序编码
                                       f.ResourceName ReworkProcessName, --返工工序名称
                                       g.BGPalletyQty,
                                       a.ConfirmStatus,
                                       a.QualityConfirmUser,
                                       a.QualityConfirmTime,
                                       a.CreateTime,
                                       a.Remark
                                FROM dbo.PM_ReworkRecord a
                                    INNER JOIN dbo.PM_OwnProductOrder b
                                        ON a.WorkOrder = b.WorkOrder
                                    LEFT JOIN dbo.Base_Material c
                                        ON b.MaterialCode = c.MaterialCode
                                    LEFT JOIN dbo.BS_ModelWithResource d
                                        ON d.ModelLeve = 'Process'
                                           AND a.CurrentProcess = d.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource e
                                        ON d.ModelLeve = 'Process'
                                           AND a.DutyProcess = e.ResourceCode
                                    LEFT JOIN dbo.BS_ModelWithResource f
                                        ON d.ModelLeve = 'Process'
                                           AND a.ReworkProcess = f.ResourceCode
                                    LEFT JOIN dbo.V_DataDictionary v1
                                        ON v1.EnCode = 'ReworkStatus'
                                           AND a.Status = v1.ItemValue
                                    LEFT JOIN
                                    (
                                        SELECT detail.ReworkId,
                                               COUNT(1) BGPalletyQty
                                        FROM dbo.PM_ReworkRecord_Detail detail
                                        WHERE detail.IsEnabled = 1
                                              AND EXISTS
                                        (
                                            SELECT 1
                                            FROM dbo.PM_TranferCardBGRecord bg
                                            WHERE bg.IsEnabled = 1
                                                  AND bg.ReworkDId = detail.Id
                                        )
                                        GROUP BY detail.ReworkId
                                    ) g
                                        ON a.Id = g.ReworkId
                                WHERE a.IsEnabled = 1
                                      AND a.ReworkProductType = '2'
                            ) a
                            WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //返工单号 是否为空进行查询
                if (!queryParam["ReworkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ReworkOrder = N'{queryParam["ReworkOrder"]}'");
                    sql.Append($" AND a.ReworkOrder like N'%{queryParam["ReworkOrder"]}%'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND a.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND a.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                //返工状态 是否为空进行查询
                if (!queryParam["Status"].IsEmpty())
                {
                    sql.Append($" AND a.Status = N'{queryParam["Status"]}'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND CurrentProcess = N'{queryParam["CurrentProcess"]}'");
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND a.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND a.Spec like N'%{queryParam["Spec"]}%'");
                }
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //当前工序 是否为空进行查询
                if (!queryParam["CurrentProcess"].IsEmpty())
                {
                    //sql.Append($" AND CurrentProcess = N'{queryParam["CurrentProcess"]}'");
                    sql.Append($" AND a.CurrentProcess like N'%{queryParam["CurrentProcess"]}%'");
                }
                //责任工序 是否为空进行查询
                if (!queryParam["DutyProcess"].IsEmpty())
                {
                    sql.Append($" AND DutyProcess = N'{queryParam["DutyProcess"]}'");
                }
                //返工工序 是否为空进行查询
                if (!queryParam["ReworkProcess"].IsEmpty())
                {
                    sql.Append($" AND ReworkProcess = N'{queryParam["ReworkProcess"]}'");
                }

                //确认状态 是否为空进行查询
                if (!queryParam["ConfirmStatus"].IsEmpty())
                {
                    sql.Append($" AND ConfirmStatus = N'{queryParam["ConfirmStatus"]}'");

                }
                //确认状态 是否为空进行查询
                if (!queryParam["Operator"].IsEmpty())
                {
                    //sql.Append($" AND ConfirmStatus = N'{queryParam["ConfirmStatus"]}'");
                    sql.Append($" AND a.Operator like N'%{queryParam["Operator"]}%'");
                }


                //创建开始时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND a.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                //创建结束时间 是否为空进行查询
                if (!queryParam["EndTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND a.CreateTime <= N'{queryParam["EndTime"]}'");
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
        public IEnumerable<PM_ReworkRecordEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[ReworkOrder]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder],ContainerNO
                      ,[CurrentProcess]
                      ,[DutyProcess]
                      ,[ReworkProcess]
                      ,[Status]
                      ,[ConfirmStatus]
                      ,[QualityConfirmUser]
                      ,[QualityConfirmTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                      ,PalletQty
                  FROM [dbo].[PM_ReworkRecord] where IsEnabled=1 ");
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
        public int SaveEntity(string keyValue, PM_ReworkRecordEntity entity, out string msg)
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
        /// <param name="List<PM_ReworkRecordEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ReworkRecordEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_ReworkRecord] set ");
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
                    //StringBuilder sql = new StringBuilder();
                    //sql.Append($@"INSERT INTO [dbo].[PM_ReworkRecord] (
                    //                        [Id],FactoryCode
                    //                        ,[ReworkOrder]
                    //                        ,[ProductOrder]
                    //                        ,[WorkOrder]
                    //                        ,[ExeWorkOrder],ContainerNO
                    //                        ,[CurrentProcess]
                    //                        ,[DutyProcess]
                    //                        ,[ReworkProcess]
                    //                        ,[Status]
                    //                        ,[ConfirmStatus]
                    //                        ,[QualityConfirmUser]
                    //                        ,[QualityConfirmTime]
                    //                        ,[Creator]
                    //                        ,[CreateTime]
                    //                        ,[ModifyBy]
                    //                        ,[ModifyTime]
                    //                        ,[IsEnabled]
                    //                        ,PalletQty
                    //                ) VALUES ");
                    //if (entity_list.Count > 0)
                    //{
                    //    foreach (var Save_obj in entity_list)
                    //    {
                    //        sql.Append($@"(
                    //            N'{Save_obj.Id}',,N'{Save_obj.FactoryCode}'
                    //            ,N'{Save_obj.ReworkOrder}'
                    //            ,N'{Save_obj.ProductOrder}'
                    //            ,N'{Save_obj.WorkOrder}'
                    //            ,N'{Save_obj.ExeWorkOrder}',N'{Save_obj.ContainerNO}'
                    //            ,N'{Save_obj.CurrentProcess}'
                    //            ,N'{Save_obj.DutyProcess}'
                    //            ,N'{Save_obj.ReworkProcess}'
                    //            ,N'{Save_obj.Status}'
                    //            ,N'{Save_obj.ConfirmStatus}'
                    //            ,N'{Save_obj.QualityConfirmUser}'
                    //            ,'{(Save_obj.QualityConfirmTime == null ? DateTime.Now : Save_obj.QualityConfirmTime)}'
                    //            ,N'{Save_obj.Creator}'
                    //            ,'{(Save_obj.CreateTime == null ? DateTime.Now : Save_obj.CreateTime)}'
                    //            ,N'{Save_obj.ModifyBy}'
                    //            ,'{(Save_obj.ModifyTime == null ? DateTime.Now : Save_obj.ModifyTime)}'
                    //            ,'{(Save_obj.IsEnabled == true ? 1 : 0)}'
                    //          ,N'{Save_obj.PalletQty}'
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
            PM_ReworkRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
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

        public int RemoveForm(Expression<Func<PM_ReworkRecordEntity, bool>> condition)
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
                sql.Append($@"DELETE FROM [dbo].[PM_ReworkRecord] WHERE Id=N'{keyValue}'");
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
        /// <returns>返回PM_ReworkRecordEntity</returns>
        public PM_ReworkRecordEntity GetEntity(string keyValue)
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
        /// <returns>返回PM_ReworkRecordEntity</returns>
        public PM_ReworkRecordEntity GetEntityByQuery(string QueryField)
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
        /// <returns>返回PM_ReworkRecordEntity 对象</returns>
        public PM_ReworkRecordEntity Get_ExpressionEntity(Expression<Func<PM_ReworkRecordEntity, bool>> condition)
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
        /// <returns>返回PM_ReworkRecordEntity 列表</returns>
        public IEnumerable<PM_ReworkRecordEntity> Get_ExpressionList(Expression<Func<PM_ReworkRecordEntity, bool>> condition)
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
        //    RepositoryFactory<PM_ReworkRecordEntity> bomService = new RepositoryFactory<PM_ReworkRecordEntity>();

        //    PM_ReworkRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_ReworkRecordDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_ReworkRecord_Id == entity.Id).FirstOrDefault();
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
        public IEnumerable<PM_ReworkRecordEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_ReworkRecordEntity> PM_ReworkRecordEntity_list = db2.FindList<PM_ReworkRecordEntity>(sql.ToString());
                return PM_ReworkRecordEntity_list;
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
                DataTable PM_ReworkRecordEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_ReworkRecordEntity_DataTable;
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
                      [ReworkOrder] as '返工单号'
                      ,[ProductOrder] as '订单号'
                      ,[WorkOrder] as '工单号'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[CurrentProcess] as '当前工序'
                      ,[DutyProcess] as '责任工序'
                      ,[ReworkProcess] as '返工工序'
                      ,[Status] as '返工状态'
                      ,[ConfirmStatus] as '确认状态'
                      ,[QualityConfirmUser] as '质量确认人'
                      ,[QualityConfirmTime] as '质量确认时间'
                      ,[Creator] as '创建人编码'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人编码'
                      ,[ModifyTime] as '修改时间'
                      ,[IsEnabled] as '有效标记'
                  FROM [dbo].[PM_ReworkRecord] where IsEnabled=1 ");
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

        #region PDA
        /// <summary>
        /// 返工任务查询
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public DataTable GetReworkRecord(string processCode)
        {
            StringBuilder strSql = new StringBuilder();
            #region 旧
            //strSql.Append($@"IF OBJECT_ID('tempdb..#bgPerson') IS NOT NULL
            //                    DROP TABLE #bgPerson;
            //                IF OBJECT_ID('tempdb..#reworkMain2') IS NOT NULL
            //                    DROP TABLE #reworkMain2;
            //                IF OBJECT_ID('tempdb..#reworkMain3') IS NOT NULL
            //                    DROP TABLE #reworkMain3;

            //                --报工班组人员，用逗号隔开
            //                SELECT b.BGID,
            //                       UserNames = (STUFF(
            //                                    (
            //                                        SELECT ',' + a.UserName
            //                                        FROM [dbo].PM_TransferBGPersonRecord a
            //                                        WHERE a.BGID = b.BGID
            //                                        FOR XML PATH('')
            //                                    ),
            //                                    1,
            //                                    1,
            //                                    ''
            //                                         )
            //                                   ) --where条件必须加上 
            //                INTO #bgPerson
            //                FROM dbo.PM_TransferBGPersonRecord b
            //                GROUP BY b.BGID;

            //                --返工工序完工时间
            //                SELECT a.Id,
            //                       MAX(c.CreateTime) ReworkFinishedTime
            //                INTO #reworkMain2
            //                FROM dbo.PM_ReworkRecord a
            //                    INNER JOIN dbo.PM_ReworkRecord_Detail b
            //                        ON b.ReworkId = a.Id
            //                    INNER JOIN dbo.PM_TranferCardBGRecord c
            //                        ON b.CardCode = c.CardCode
            //                           AND a.ReworkProcess = c.ProcessCode
            //                           AND c.IsRework = '0'
            //                GROUP BY a.Id;

            //                --责任工序完工记录
            //                SELECT a.Id,
            //                       MAX(c.CreateTime) DutyFinishedTime,
            //                       MAX(d.UserNames) DutyUserNames
            //                INTO #reworkMain3
            //                FROM dbo.PM_ReworkRecord a
            //                    INNER JOIN dbo.PM_ReworkRecord_Detail b
            //                        ON b.ReworkId = a.Id
            //                    INNER JOIN dbo.PM_TranferCardBGRecord c
            //                        ON b.CardCode = c.CardCode
            //                           AND a.DutyProcess = c.ProcessCode
            //                           AND c.IsRework = '0'
            //                    INNER JOIN #bgPerson d
            //                        ON d.BGID = c.Id
            //                GROUP BY a.Id;
            //                --查询结果
            //                SELECT a.ProductOrder,
            //                       a.ContainerNO,
            //                       e.MMXH,
            //                       a.PalletQty,
            //                       a.ReworkProcess,
            //                       c.ResourceName ReworkProcessName,
            //                       a.DutyProcess,
            //                       d.ResourceName DutyProcessName,
            //                       #reworkMain3.DutyUserNames,
            //                       #reworkMain2.ReworkFinishedTime ReworkProcessFinishTime
            //                FROM dbo.PM_ReworkRecord a
            //                    LEFT JOIN dbo.BS_ModelWithResource c
            //                        ON c.ModelLeve = 'Process'
            //                           AND a.ReworkProcess = c.ResourceCode
            //                    LEFT JOIN dbo.BS_ModelWithResource d
            //                        ON c.ModelLeve = 'Process'
            //                           AND a.DutyProcess = d.ResourceCode
            //                    LEFT JOIN dbo.fn_GetMaterialAttrs() e
            //                        ON a.WorkOrder = e.WorkOrder
            //                    LEFT JOIN #reworkMain3
            //                        ON a.Id = #reworkMain3.Id
            //                    LEFT JOIN #reworkMain2
            //                        ON a.Id = #reworkMain2.Id
            //                WHERE a.IsEnabled = 1
            //                      AND a.ConfirmStatus = '0'
            //                      AND #reworkMain2.ReworkFinishedTime IS NOT NULL
            //                      AND a.DutyProcess = '{processCode}'
            //                ORDER BY #reworkMain2.ReworkFinishedTime");
            #endregion

            strSql.Append($@" IF OBJECT_ID('tempdb..#bgPerson') IS NOT NULL
                                DROP TABLE #bgPerson;
                            IF OBJECT_ID('tempdb..#BGRecord') IS NOT NULL
                                DROP TABLE #BGRecord;
                            IF OBJECT_ID('tempdb..#reworkMain1') IS NOT NULL
                                DROP TABLE #reworkMain1;
                            IF OBJECT_ID('tempdb..#reworkMain2') IS NOT NULL
                                DROP TABLE #reworkMain2;
                            IF OBJECT_ID('tempdb..#reworkMain3') IS NOT NULL
                                DROP TABLE #reworkMain3;
                            IF OBJECT_ID('tempdb..#reworkDetail') IS NOT NULL
                                DROP TABLE #reworkDetail;

                            --返工任务主表信息
                            SELECT *
                            INTO #reworkMain1
                            FROM dbo.PM_ReworkRecord
                            WHERE DutyProcess = '{processCode}'
                                  AND ISNULL(ConfirmStatus, '') = '0'
                                  AND IsEnabled = 1;

                            SELECT * INTO #reworkDetail FROM dbo.PM_ReworkRecord_Detail a WHERE EXISTS(SELECT 1 FROM #reworkMain1 e WHERE e.Id=a.ReworkId );

                            --返工工序完工时间
                            SELECT a.Id,
                                   MAX(c.CreateTime) ReworkFinishedTime
                            INTO #reworkMain2
                            FROM #reworkMain1 a
                                INNER JOIN #reworkDetail b
                                    ON b.ReworkId = a.Id
                                INNER JOIN dbo.PM_TranferCardBGRecord c
                                    ON b.CardCode = c.CardCode
                                       AND a.ReworkProcess = c.ProcessCode
                                       AND c.IsRework = '0'
                            GROUP BY a.Id;

                            --责任工序报工时间
                            SELECT c.Id,
                                   c.CreateTime,
                                   a.Id ReworkId
                            INTO #BGRecord
                            FROM #reworkMain1 a
                                INNER JOIN #reworkDetail b
                                    ON b.ReworkId = a.Id
                                INNER JOIN dbo.PM_TranferCardBGRecord c
                                    ON b.CardCode = c.CardCode
                                       AND a.DutyProcess = c.ProcessCode
                                       AND c.IsRework = '0';

                            --责任工序报工班组人员，用逗号隔开
                            SELECT b.BGID,
                                   UserNames = (STUFF(
                                                (
                                                    SELECT ',' + a.UserName
                                                    FROM [dbo].PM_TransferBGPersonRecord a
                                                    WHERE a.BGID = b.BGID
                                                    FOR XML PATH('')
                                                ),
                                                1,
                                                1,
                                                ''
                                                     )
                                               ) --where条件必须加上 
                            INTO #bgPerson
                            FROM dbo.PM_TransferBGPersonRecord b
                            WHERE b.BGID IN
                                  (
                                      SELECT Id FROM #BGRecord
                                  )
                            GROUP BY b.BGID;

                            --责任工序完工时间
                            SELECT c.ReworkId Id,
                                   MAX(c.CreateTime) DutyFinishedTime,
                                   MAX(d.UserNames) DutyUserNames
                            INTO #reworkMain3
                            FROM #BGRecord c
                                INNER JOIN #bgPerson d
                                    ON d.BGID = c.Id
                            GROUP BY c.ReworkId;
                            --查询结果
                            SELECT a.ProductOrder,
                                   a.ContainerNO,
                                   e.MMXH,
                                   a.PalletQty,
                                   a.ReworkProcess,
                                   c.ResourceName ReworkProcessName,
                                   a.DutyProcess,
                                   d.ResourceName DutyProcessName,
                                   #reworkMain3.DutyUserNames,
                                   #reworkMain2.ReworkFinishedTime ReworkProcessFinishTime
                            FROM #reworkMain1 a
                                LEFT JOIN dbo.BS_ModelWithResource c
                                    ON c.ModelLeve = 'Process'
                                       AND a.ReworkProcess = c.ResourceCode
                                LEFT JOIN dbo.BS_ModelWithResource d
                                    ON c.ModelLeve = 'Process'
                                       AND a.DutyProcess = d.ResourceCode
                                LEFT JOIN dbo.fn_GetMaterialAttrs() e
                                    ON a.WorkOrder = e.WorkOrder
                                LEFT JOIN #reworkMain3
                                    ON a.Id = #reworkMain3.Id
                                LEFT JOIN #reworkMain2
                                    ON a.Id = #reworkMain2.Id
                            WHERE #reworkMain2.ReworkFinishedTime IS NOT NULL
                            ORDER BY #reworkMain2.ReworkFinishedTime ");
            return this.BaseRepository().FindTable(strSql.ToString());
        }
        #endregion

    }
}
