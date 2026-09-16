using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.QualityManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;

namespace ALP.Application.Service.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_PollingDetailService 业务服务类
    /// 4.任务编号: 巡检检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_PollingDetail_Service : RepositoryFactory<QC_PollingDetailEntity>, QC_PollingDetail_IService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_PollingDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardId]
                      ,[TestProcess]
                      ,[ProductionMachine]
                      ,[LaboratoryTestStatus]
                      ,[Determination]
                      ,[Remark]
                      ,[Attachment]
                      ,[EnabledMark]
                      ,[Inspector]
                      ,[InspectionTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_PollingDetail] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    //sql.Append($" AND FlowCardId = N'{queryParam["FlowCardId"]}'");
                    sql.Append($" AND FlowCardId like N'%{queryParam["FlowCardId"]}%'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["TestProcess"].IsEmpty())
                {
                    //sql.Append($" AND TestProcess = N'{queryParam["TestProcess"]}'");
                    sql.Append($" AND TestProcess like N'%{queryParam["TestProcess"]}%'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["ProductionMachine"].IsEmpty())
                {
                    //sql.Append($" AND ProductionMachine = N'{queryParam["ProductionMachine"]}'");
                    sql.Append($" AND ProductionMachine like N'%{queryParam["ProductionMachine"]}%'");
                }
                //实验室状态 是否为空进行查询
                if (!queryParam["LaboratoryTestStatus"].IsEmpty())
                {
                    //sql.Append($" AND LaboratoryTestStatus = N'{queryParam["LaboratoryTestStatus"]}'");
                    sql.Append($" AND LaboratoryTestStatus like N'%{queryParam["LaboratoryTestStatus"]}%'");
                }
                //判定结果 是否为空进行查询
                if (!queryParam["Determination"].IsEmpty())
                {
                    //sql.Append($" AND Determination = N'{queryParam["Determination"]}'");
                    sql.Append($" AND Determination like N'%{queryParam["Determination"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND Attachment like N'%{queryParam["Attachment"]}%'");
                }
                //有效标志 是否为空进行查询
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    //sql.Append($" AND EnabledMark = N'{queryParam["EnabledMark"]}'");
                    sql.Append($" AND EnabledMark like N'%{queryParam["EnabledMark"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Inspector"].IsEmpty())
                {
                    //sql.Append($" AND Inspector = N'{queryParam["Inspector"]}'");
                    sql.Append($" AND Inspector like N'%{queryParam["Inspector"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["InspectionTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND InspectionTime like N'%{queryParam["InspectionTime"]}%'");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
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
                                SELECT DISTINCT
                                       A.Id,
                                       A.FactoryCode,
                                       A.FactoryName,
                                       A.FlowCardId,
                                       A.CalibrationMethod,
                                       B.TestMethodName AS CalibrationMethodName,
                                       A.TestProcess,
                                       F.ResourceName AS TestProcessName,
                                       A.ProductionMachine,
                                       A.LaboratoryTestStatus,
                                       dbo.get_dicName('LaboratoryStatus', A.LaboratoryTestStatus) AS LaboratoryTestStatusName,
                                       A.Determination,
                                       dbo.get_dicName('ComprehensiveJudgement', A.Determination) AS DeterminationName,
                                       A.Remark,
                                       A.Inspector,
                                       H.Name InspectorName,
                                       A.InspectionTime,
                                       A.LabInspector,
                                       A.LabInspectionTime,
                                       A.EnabledMark,
                                       CASE
                                           WHEN ISNULL(A.ProductOrder, '') <> '' THEN
                                               A.ProductOrder
                                           ELSE
                                               C.ProductOrder
                                       END ProductOrder,
                                       CASE
                                           WHEN ISNULL(A.ContainerNO, '') <> '' THEN
                                               A.ContainerNO
                                           ELSE
                                               C.ContainerNO
                                       END ContainerNO,
                                       CASE
                                           WHEN ISNULL(A.Spec, '') <> '' THEN
                                               A.Spec
                                           ELSE
                                               C.Spec
                                       END Spec,
                                       CASE
                                           WHEN ISNULL(A.MaterialCode, '') <> '' THEN
                                               A.MaterialCode
                                           ELSE
                                               C.MaterialCode
                                       END MaterialCode,
                                       CASE
                                           WHEN ISNULL(A.MMXH, '') <> '' THEN
                                               A.MMXH
                                           ELSE
                                               C.MMXH
                                       END MMXH,
                                       A.CreateTime,
                                       j.Name DeptName
                                FROM dbo.QC_PollingDetail AS A
                                    LEFT JOIN dbo.QC_TestMaintenance AS B
                                        ON A.CalibrationMethod = B.TestMethodCoading
                                           AND B.IsEnabled = 1
                                    LEFT JOIN dbo.PM_TransferCard AS C
                                        ON A.FlowCardId = C.CardCode
                                           AND C.IsEnabled = 1
                                    LEFT JOIN dbo.BS_ModelWithResource AS F
                                        ON A.TestProcess = F.ResourceCode
                                           AND F.EnabledMark = 1
                                           AND F.ModelLeve = 'Process'
                                    LEFT JOIN dbo.BS_People AS H
                                        ON A.Inspector = H.Code
                                           AND H.IsEnabled = 1
                                    LEFT JOIN dbo.BS_Departments j
                                        ON H.Department_ID = j.Code
                            ) a
                            WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["ProductOrder"]}', a.ProductOrder)>0 ");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["ContainerNO"]}', a.ContainerNO)>0 ");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["Spec"]}', a.Spec)>0 ");
                }
                //创建人 是否为空进行查询
                if (!queryParam["InspectorName"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" and CHARINDEX('{queryParam["InspectorName"]}', a.Name)>0 ");
                }
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
 
                //流转卡编码 是否为空进行查询
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    //sql.Append($" AND FlowCardId = N'{queryParam["FlowCardId"]}'");
                    sql.Append($" AND FlowCardId like N'%{queryParam["FlowCardId"]}%'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["TestProcess"].IsEmpty())
                {
                    sql.Append($" AND TestProcess = '{queryParam["TestProcess"]}'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND TestProcess = '{queryParam["ProcessCode"]}'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["ProductionMachine"].IsEmpty())
                {
                    sql.Append($" AND ProductionMachine = '{queryParam["ProductionMachine"]}'");
                }
                if (!queryParam["MachineCode"].IsEmpty())
                {
                    sql.Append($" AND ProductionMachine = '{queryParam["MachineCode"]}'");
                }
                //实验室状态 是否为空进行查询
                if (!queryParam["LaboratoryTestStatus"].IsEmpty())
                {
                    sql.Append($" AND LaboratoryTestStatus = '{queryParam["LaboratoryTestStatus"]}'");
                    //sql.Append($" AND LaboratoryTestStatus like N'%{queryParam["LaboratoryTestStatus"]}%'");
                }
                //判定结果 是否为空进行查询
                if (!queryParam["Determination"].IsEmpty())
                {
                    sql.Append($" AND Determination = '{queryParam["Determination"]}'");
                    //sql.Append($" AND Determination like N'%{queryParam["Determination"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND Attachment like N'%{queryParam["Attachment"]}%'");
                }
                //有效标志 是否为空进行查询
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    //sql.Append($" AND EnabledMark = N'{queryParam["EnabledMark"]}'");
                    sql.Append($" AND EnabledMark like N'%{queryParam["EnabledMark"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Inspector"].IsEmpty())
                {
                    //sql.Append($" AND Inspector = N'{queryParam["Inspector"]}'");
                    sql.Append($" AND Inspector like N'%{queryParam["Inspector"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["InspectionTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND InspectionTime like N'%{queryParam["InspectionTime"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND InspectionTime >= '{Tools.CovertToDateStr(queryParam["StartTime"])}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    //sql.Append($" AND InspectionTime = N'{queryParam["InspectionTime"]}'");
                    sql.Append($" AND InspectionTime < '{Tools.CovertToNextDateStr(queryParam["EndTime"])}'");
                }
                //部门 是否为空进行查询
                if (!queryParam["DeptName"].IsEmpty())
                {
                    sql.Append($" AND Name like N'%{queryParam["DeptName"]}%'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //面膜新高 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND MMXH like N'%{queryParam["MMXH"]}%'");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_PollingDetailEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardId]
                      ,[TestProcess]
                      ,[ProductionMachine]
                      ,[LaboratoryTestStatus]
                      ,[Determination]
                      ,[Remark]
                      ,[Attachment]
                      ,[EnabledMark]
                      ,[Inspector]
                      ,[InspectionTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_PollingDetail] where IsDeleted = 0 ");
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
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataTable GetUserEntity(string keyValue)
        {
            return this.BaseRepository().FindTable($@"select B.Code,B.Name,CONVERT(varchar(10), A.InspectionTime,23) as InspectionTime from QC_PollingDetail as A
                                                    left join BS_People as B on A.Inspector = B.Code
                                                    where A.Id = '{keyValue}'");
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_PollingDetailEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    RepositoryFactory<PM_TransferCardEntity> cardService = new RepositoryFactory<PM_TransferCardEntity>();
                    //查询FlowCardId是否在PM_TransferCard中存在
                    PM_TransferCardEntity cardEntity = cardService.BaseRepository().FindEntity(t => t.CardCode == entity.FlowCardId && t.IsEnabled == true);
                    if (cardEntity != null)
                    {
                        if (string.IsNullOrEmpty(entity.Id))
                        {
                            entity.Create();
                        }
                        n = this.BaseRepository().Insert(entity);
                    }
                    else
                    {
                        n = 3;
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        public int Insert( QC_PollingDetailEntity entity)
        {
            return this.BaseRepository().Insert(entity);
        }

        /// <summary>
        /// 更新判定状态
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="determinationValue"></param>
        /// <param name="inspector"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int SaveDetermination(string keyValue, string determinationValue, string inspector,ref string msg)
        {
            int result = 0;
            msg = "";
            try
            {
                QC_PollingDetailEntity entity = this.BaseRepository().FindEntity(t => t.Id == keyValue && t.EnabledMark == true);
                if (entity != null)
                {
                    entity.Determination = determinationValue;
                    //entity.LaboratoryTestStatus = "3";
                    //entity.LabInspector = inspector;
                    //entity.LabInspectionTime = DateTime.Now;
                    entity.LabInspector = inspector;
                    entity.LabInspectionTime = DateTime.Now;
                    result = this.BaseRepository().Update(entity);
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            return result;
            
        }
        
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_PollingDetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_PollingDetailEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[QC_PollingDetail] set ");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            QC_PollingDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
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
                sql.Append($@"DELETE FROM [dbo].[QC_PollingDetail] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_PollingDetailEntity</returns>
        public QC_PollingDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_PollingDetailEntity</returns>
        public QC_PollingDetailEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_PollingDetailEntity 对象</returns>
        public QC_PollingDetailEntity Get_ExpressionEntity(Expression<Func<QC_PollingDetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_PollingDetailEntity 列表</returns>
        public IEnumerable<QC_PollingDetailEntity> Get_ExpressionList(Expression<Func<QC_PollingDetailEntity, bool>> condition)
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
        //    RepositoryFactory<QC_PollingDetailEntity> bomService = new RepositoryFactory<QC_PollingDetailEntity>();
        
        //    QC_PollingDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_PollingDetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_PollingDetail_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_PollingDetailEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<QC_PollingDetailEntity> QC_PollingDetailEntity_list =  db2.FindList<QC_PollingDetailEntity>(sql.ToString());
                return QC_PollingDetailEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
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
                DataTable QC_PollingDetailEntity_DataTable = db2.FindTable(sql.ToString());
                return QC_PollingDetailEntity_DataTable;
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FlowCardId] as '流转卡编码'
                      ,[TestProcess] as '检验工序'
                      ,[ProductionMachine] as '检验机台'
                      ,[LaboratoryTestStatus] as '实验室状态'
                      ,[Determination] as '判定结果'
                      ,[Remark] as '备注'
                      ,[Attachment] as '附件'
                      ,[EnabledMark] as '有效标志'
                      ,[Inspector] as '检验员'
                      ,[InspectionTime] as '检验时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[QC_PollingDetail] where IsDeleted = 0 ");
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
                string saveFileName = "巡检检验记录表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                /*ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("巡检检验记录表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();*/
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
    }
}
