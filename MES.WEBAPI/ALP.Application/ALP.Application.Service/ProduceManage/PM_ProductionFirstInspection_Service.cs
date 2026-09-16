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
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_ProductionFirstInspectionService 业务服务类
    /// 4.任务编号: 生产首检
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ProductionFirstInspection_Service : RepositoryFactory<PM_ProductionFirstInspectionEntity>, PM_ProductionFirstInspectionIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[FirstProcessCode]
                      ,[FirstMachine]
                      ,[SecondMark]
                      ,[FirstResult]
                      ,[FirstUser]
                      ,[FirstTime]
                      ,[SecondResult]
                      ,[SecondUser]
                      ,[SecondTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_ProductionFirstInspection] where 1=1 ");
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
                //首检工序编码 是否为空进行查询
                if (!queryParam["FirstProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND FirstProcessCode = N'{queryParam["FirstProcessCode"]}'");
                    sql.Append($" AND FirstProcessCode like N'%{queryParam["FirstProcessCode"]}%'");
                }
                //首检机台 是否为空进行查询
                if (!queryParam["FirstMachine"].IsEmpty())
                {
                    //sql.Append($" AND FirstMachine = N'{queryParam["FirstMachine"]}'");
                    sql.Append($" AND FirstMachine like N'%{queryParam["FirstMachine"]}%'");
                }
                //质量复检标记 是否为空进行查询
                if (!queryParam["SecondMark"].IsEmpty())
                {
                    //sql.Append($" AND SecondMark = N'{queryParam["SecondMark"]}'");
                    sql.Append($" AND SecondMark like N'%{queryParam["SecondMark"]}%'");
                }
                //车间首检结果 是否为空进行查询
                if (!queryParam["FirstResult"].IsEmpty())
                {
                    //sql.Append($" AND FirstResult = N'{queryParam["FirstResult"]}'");
                    sql.Append($" AND FirstResult like N'%{queryParam["FirstResult"]}%'");
                }
                //车间首检人 是否为空进行查询
                if (!queryParam["FirstUser"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND FirstUser like N'%{queryParam["FirstUser"]}%'");
                }

                //质量复检结果 是否为空进行查询
                if (!queryParam["SecondResult"].IsEmpty())
                {
                    //sql.Append($" AND SecondResult = N'{queryParam["SecondResult"]}'");
                    sql.Append($" AND SecondResult like N'%{queryParam["SecondResult"]}%'");
                }
                //质量复检人 是否为空进行查询
                if (!queryParam["SecondUser"].IsEmpty())
                {
                    //sql.Append($" AND SecondUser = N'{queryParam["SecondUser"]}'");
                    sql.Append($" AND SecondUser like N'%{queryParam["SecondUser"]}%'");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PF.FactoryCode,
                               PF.FactoryName,
                               PW.ContainerNO,                    --柜号
                               PE.OrderType,
                               PMA.MaterialCode,
                               PMA.MaterialName,
                               PMA.Spec,                          --规格型号
                               PMA.MMXH,                          --面膜型号
                               PMA.MMCJ,
                               PF.Id,
                               PF.ProductOrder,                   --订单号
                               PF.WorkOrder,
                               PF.ExeWorkOrder,
                               PF.FirstProcessCode,               --工序编码
                               PF.CalibrationMethod,
                               TM.TestMethodName AS CalibrationMethodName,
                               PF.InspectClass,
                               PF.LaboratoryTestStatus,
                               dbo.get_dicName('LaboratoryStatus', PF.LaboratoryTestStatus) AS LaboratoryTestStatusName,
                               PF.Determination,
                               dbo.get_dicName('ComprehensiveJudgement', PF.Determination) AS DeterminationName,
                               PF.Attachment,
                               BM.ResourceName FirstProcessName,  --工序名称
                               PF.FirstMachine,                   --机台编码
                               BM1.ResourceName FirstMachineName, --机台名称
                               PF.SecondMark,
                               CASE
                                   WHEN ISNULL(PF.SecondMark, '0') = '0'
                                        OR PF.SecondMark = '' THEN
                                       '未确认'
                                   ELSE
                                       '已确认'
                               END SecondMarkName,                --车间主任确认状态
                               PF.FirstResult,
                               V.ItemName FirstResultName,
                               PF.FirstUser,
                               PF.FirstTime,
                               PF.SecondResult,
                               PF.SecondUser,
                               PF.SecondTime,
                               PF.Creator,
                               PF.CreateTime,
                               PF.ModifyBy,
                               PF.ModifyTime
                        FROM dbo.PL_WorkOrder PW
                            INNER JOIN dbo.PL_ExeWorkOrder PE
                                ON PE.WorkOrder = PW.WorkOrder
                            INNER JOIN [dbo].[PM_ProductionFirstInspection] PF
                                ON PF.ExeWorkOrder = PE.ExeWorkOrder
                            LEFT JOIN dbo.QC_TestMaintenance TM
                                ON PF.CalibrationMethod = TM.TestMethodCoading
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PMA.WorkOrder = PF.WorkOrder
                            LEFT JOIN dbo.BS_ModelWithResource BM
                                ON BM.ResourceCode = PF.FirstProcessCode
                            LEFT JOIN dbo.BS_ModelWithResource BM1
                                ON BM1.ResourceCode = PF.FirstMachine
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'TestConclusion'
                                   AND V.ItemValue = PF.FirstResult
                            LEFT JOIN dbo.V_DataDictionary G
                                ON G.EnCode = 'FirstTestType'
                                   AND G.ItemValue = PF.InspectClass
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PF.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND PF.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND PF.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
               
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND PW.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND PMA.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND PMA.Spec like N'%{queryParam["Spec"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND PF.ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //首检工序编码 是否为空进行查询
                if (!queryParam["FirstProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND FirstProcessCode = N'{queryParam["FirstProcessCode"]}'");
                    sql.Append($" AND FirstProcessCode like N'%{queryParam["FirstProcessCode"]}%'");
                }
                //首检机台 是否为空进行查询
                if (!queryParam["FirstMachine"].IsEmpty())
                {
                    //sql.Append($" AND FirstMachine = N'{queryParam["FirstMachine"]}'");
                    sql.Append($" AND FirstMachine like N'%{queryParam["FirstMachine"]}%'");
                }
                //质量复检标记 是否为空进行查询
                if (!queryParam["SecondMark"].IsEmpty())
                {
                    if (queryParam["SecondMark"].ToString() == "0")
                        sql.Append($" AND (PF.SecondMark = N'{queryParam["SecondMark"]}' OR pf.SecondMark='')");
                    else
                        sql.Append($" AND PF.SecondMark = N'{queryParam["SecondMark"]}'");
                    //sql.Append($" AND SecondMark like N'%{queryParam["SecondMark"]}%'");
                }
                //车间首检结果 是否为空进行查询
                if (!queryParam["FirstResult"].IsEmpty())
                {
                    //sql.Append($" AND FirstResult = N'{queryParam["FirstResult"]}'");
                    sql.Append($" AND FirstResult like N'%{queryParam["FirstResult"]}%'");
                }
                //车间首检人 是否为空进行查询
                if (!queryParam["FirstUser"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND FirstUser like N'%{queryParam["FirstUser"]}%'");
                }

                //检测类型 是否为空进行查询
                if (!queryParam["InspectClass"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND InspectClass like N'%{queryParam["InspectClass"]}%'");
                }

                //检测类型 是否为空进行查询
                if (!queryParam["LaboratoryTestStatus"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND LaboratoryTestStatus like N'%{queryParam["LaboratoryTestStatus"]}%'");
                }

                //检测类型 是否为空进行查询
                if (!queryParam["Determination"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND Determination like N'%{queryParam["Determination"]}%'");
                }

                //检测类型 是否为空进行查询
                if (!queryParam["FirstMachine"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND FirstMachine like N'%{queryParam["FirstMachine"]}%'");
                }
                //车间首检时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    //sql.Append($" AND FirstTime = N'{queryParam["FirstTime"]}'");
                    sql.Append($" AND FirstTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND FirstTime <= N'{queryParam["EndTime"]}'");
                }
                //质量复检结果 是否为空进行查询
                if (!queryParam["SecondResult"].IsEmpty())
                {
                    //sql.Append($" AND SecondResult = N'{queryParam["SecondResult"]}'");
                    sql.Append($" AND SecondResult like N'%{queryParam["SecondResult"]}%'");
                }
                //质量复检人 是否为空进行查询
                if (!queryParam["SecondUser"].IsEmpty())
                {
                    //sql.Append($" AND SecondUser = N'{queryParam["SecondUser"]}'");
                    sql.Append($" AND SecondUser like N'%{queryParam["SecondUser"]}%'");
                }
                //质量复检时间 是否为空进行查询
                if (!queryParam["SecondTime"].IsEmpty())
                {
                    //sql.Append($" AND SecondTime = N'{queryParam["SecondTime"]}'");
                    sql.Append($" AND SecondTime like N'%{queryParam["SecondTime"]}%'");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[ExeWorkOrder]
                      ,[FirstProcessCode]
                      ,[FirstMachine]
                      ,[SecondMark]
                      ,[FirstResult]
                      ,[FirstUser]
                      ,[FirstTime]
                      ,[SecondResult]
                      ,[SecondUser]
                      ,[SecondTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_ProductionFirstInspection] where 1=1 ");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_ProductionFirstInspectionEntity entity, out string msg)
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_ProductionFirstInspectionEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ProductionFirstInspectionEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_ProductionFirstInspection] set ");
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
                    //StringBuilder sql = new StringBuilder();
                    //sql.Append($@"INSERT INTO [dbo].[PM_ProductionFirstInspection] (
                    //                        [Id]
                    //                        ,[ProductOrder]
                    //                        ,[WorkOrder]
                    //                        ,[ExeWorkOrder]
                    //                        ,[FirstProcessCode]
                    //                        ,[FirstMachine]
                    //                        ,[SecondMark]
                    //                        ,[FirstResult]
                    //                        ,[FirstUser]
                    //                        ,[FirstTime]
                    //                        ,[SecondResult]
                    //                        ,[SecondUser]
                    //                        ,[SecondTime]
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
                    //            ,N'{Save_obj.ProductOrder}'
                    //            ,N'{Save_obj.WorkOrder}'
                    //            ,N'{Save_obj.ExeWorkOrder}'
                    //            ,N'{Save_obj.FirstProcessCode}'
                    //            ,N'{Save_obj.FirstMachine}'
                    //            ,N'{Save_obj.SecondMark}'
                    //            ,N'{Save_obj.FirstResult}'
                    //            ,N'{Save_obj.FirstUser}'
                    //            ,'{(Save_obj.FirstTime == null? DateTime.Now:Save_obj.FirstTime)}'
                    //            ,N'{Save_obj.SecondResult}'
                    //            ,N'{Save_obj.SecondUser}'
                    //            ,'{(Save_obj.SecondTime == null? DateTime.Now:Save_obj.SecondTime)}'
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
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                sql.Append($@"DELETE FROM [dbo].[PM_ProductionFirstInspection] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_ProductionFirstInspectionEntity</returns>
        public PM_ProductionFirstInspectionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_ProductionFirstInspectionEntity</returns>
        public PM_ProductionFirstInspectionEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ProductionFirstInspectionEntity 对象</returns>
        public PM_ProductionFirstInspectionEntity Get_ExpressionEntity(Expression<Func<PM_ProductionFirstInspectionEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ProductionFirstInspectionEntity 列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionEntity> Get_ExpressionList(Expression<Func<PM_ProductionFirstInspectionEntity, bool>> condition)
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
        //    RepositoryFactory<PM_ProductionFirstInspectionEntity> bomService = new RepositoryFactory<PM_ProductionFirstInspectionEntity>();

        //    PM_ProductionFirstInspectionEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_ProductionFirstInspectionDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_ProductionFirstInspection_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_ProductionFirstInspectionEntity> PM_ProductionFirstInspectionEntity_list = db2.FindList<PM_ProductionFirstInspectionEntity>(sql.ToString());
                return PM_ProductionFirstInspectionEntity_list;
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                DataTable PM_ProductionFirstInspectionEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_ProductionFirstInspectionEntity_DataTable;
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProductOrder] as '订单号'
                      ,[WorkOrder] as '工单号'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[FirstProcessCode] as '首检工序编码'
                      ,[FirstMachine] as '首检机台'
                      ,[SecondMark] as '质量复检标记'
                      ,[FirstResult] as '车间首检结果'
                      ,[FirstUser] as '车间首检人'
                      ,[FirstTime] as '车间首检时间'
                      ,[SecondResult] as '质量复检结果'
                      ,[SecondUser] as '质量复检人'
                      ,[SecondTime] as '质量复检时间'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PM_ProductionFirstInspection] where 1=1 ");
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
                string saveFileName = "生产首检_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("生产首检", dt, true);
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

        public int RemoveForm(Expression<Func<PM_ProductionFirstInspectionEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
