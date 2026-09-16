using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.QualityManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.QualityManage;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-10
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_IQCQualityCheckService 业务服务类
    /// 4.任务编号: IQC检测记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_IQCQualityCheck_Service : RepositoryFactory<QC_IQCQualityCheckEntity>, QC_IQCQualityCheck_IService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ReceiptId]
                      ,[TestMethodIdId]
                      ,[InspectNo]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[SmallClass]
                      ,[Spec]
                      ,[SupplierCode]
                      ,[LabStatus]
                      ,[QualityStatus]
                      ,[TestResult]
                      ,[Remark]
                      ,[IsEnabled]
                      ,[Attachment]
                      ,[Address]
                      ,[QualityInspector]
                      ,[QualityTime]
                      ,[LabInspector]
                      ,[LabInspectionTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IQCQualityCheck] where IsEnabled=1  ");
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
                //收料通知单Id 是否为空进行查询
                if (!queryParam["ReceiptId"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptId = N'{queryParam["ReceiptId"]}'");
                    sql.Append($" AND ReceiptId like N'%{queryParam["ReceiptId"]}%'");
                }
                //检测方法Id 是否为空进行查询
                if (!queryParam["TestMethodIdId"].IsEmpty())
                {
                    //sql.Append($" AND TestMethodIdId = N'{queryParam["TestMethodIdId"]}'");
                    sql.Append($" AND TestMethodIdId like N'%{queryParam["TestMethodIdId"]}%'");
                }
                //检验单号 是否为空进行查询
                if (!queryParam["InspectNo"].IsEmpty())
                {
                    //sql.Append($" AND InspectNo = N'{queryParam["InspectNo"]}'");
                    sql.Append($" AND InspectNo like N'%{queryParam["InspectNo"]}%'");
                }
                //物料编号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    //sql.Append($" AND SmallClass = N'{queryParam["SmallClass"]}'");
                    sql.Append($" AND SmallClass like N'%{queryParam["SmallClass"]}%'");
                }
                //物料规格 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Spec like N'%{queryParam["Spec"]}%'");
                }
                //供应商编码 是否为空进行查询
                if (!queryParam["SupplierCode"].IsEmpty())
                {
                    //sql.Append($" AND SupplierCode = N'{queryParam["SupplierCode"]}'");
                    sql.Append($" AND SupplierCode like N'%{queryParam["SupplierCode"]}%'");
                }
                //实验室状态 是否为空进行查询
                if (!queryParam["LabStatus"].IsEmpty())
                {
                    //sql.Append($" AND LabStatus = N'{queryParam["LabStatus"]}'");
                    sql.Append($" AND LabStatus like N'%{queryParam["LabStatus"]}%'");
                }
                //质检状态 是否为空进行查询
                if (!queryParam["QualityStatus"].IsEmpty())
                {
                    //sql.Append($" AND QualityStatus = N'{queryParam["QualityStatus"]}'");
                    sql.Append($" AND QualityStatus like N'%{queryParam["QualityStatus"]}%'");
                }
                //判定结果 是否为空进行查询
                if (!queryParam["TestResult"].IsEmpty())
                {
                    //sql.Append($" AND TestResult = N'{queryParam["TestResult"]}'");
                    sql.Append($" AND TestResult like N'%{queryParam["TestResult"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //有效标志 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
                }
                //附件名称 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND Attachment like N'%{queryParam["Attachment"]}%'");
                }
                //附件地址 是否为空进行查询
                if (!queryParam["Address"].IsEmpty())
                {
                    //sql.Append($" AND Address = N'{queryParam["Address"]}'");
                    sql.Append($" AND Address like N'%{queryParam["Address"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["QualityInspector"].IsEmpty())
                {
                    //sql.Append($" AND QualityInspector = N'{queryParam["QualityInspector"]}'");
                    sql.Append($" AND QualityInspector like N'%{queryParam["QualityInspector"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["QualityTime"].IsEmpty())
                {
                    //sql.Append($" AND QualityTime = N'{queryParam["QualityTime"]}'");
                    sql.Append($" AND QualityTime like N'%{queryParam["QualityTime"]}%'");
                }
                //实验室检验人 是否为空进行查询
                if (!queryParam["LabInspector"].IsEmpty())
                {
                    //sql.Append($" AND LabInspector = N'{queryParam["LabInspector"]}'");
                    sql.Append($" AND LabInspector like N'%{queryParam["LabInspector"]}%'");
                }
                //实验室检验时间 是否为空进行查询
                if (!queryParam["LabInspectionTime"].IsEmpty())
                {
                    //sql.Append($" AND LabInspectionTime = N'{queryParam["LabInspectionTime"]}'");
                    sql.Append($" AND LabInspectionTime like N'%{queryParam["LabInspectionTime"]}%'");
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT Q.[Id],
                               Q.FactoryCode,
                               Q.FactoryName,
                               Q.[ReceiptId],
                               Q.[TestMethodIdId],
                               Q.[InspectNo],
                               Q.[MaterialCode],
                               Q.[MaterialName],
                               Q.[SmallClass],
                               V.ItemName SmallClassName,
                               Q.[Spec],
                               Q.[SupplierCode],
                               S.Abbr,
                               Q.[LabStatus],
                               Q.[QualityStatus],
                               Q.[TestResult],
                               Q.[Remark],
                               Q.[IsEnabled],
                               Q.[Attachment],
                               Q.[Address],
                               Q.[QualityInspector],
                               P1.Name QualityInspectorName,
                               Q.[QualityTime],
                               Q.[LabInspector],
                               P2.Name LabInspectorName,
                               Q.[LabInspectionTime],
                               Q.[Creator],
                               Q.[CreateTime],
                               Q.[ModifyBy],
                               Q.[ModifyTime],
                               MN.ReceiptCode,
                               MN.LineNum,
                               MN.ArrivalQty,
                               MN.Remark ReceiptRemark,
							   MN.InWhsTime
                        FROM dbo.MM_ReceiptNotice MN
                            INNER JOIN [dbo].[QC_IQCQualityCheck] Q
                                ON MN.Id = Q.ReceiptId
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = Q.SmallClass
                            LEFT JOIN dbo.BS_People P1
                                ON P1.Code = Q.QualityInspector
                            LEFT JOIN dbo.BS_People P2
                                ON P2.Code = Q.LabInspector
                            LEFT JOIN dbo.Base_SupplierManage S
                                ON S.SupplierCode = Q.SupplierCode
                        WHERE Q.IsEnabled = 1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND Q.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ReceiptCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND MN.ReceiptCode like N'%{queryParam["ReceiptCode"]}%'");
                }
 
                //检验单号 是否为空进行查询
                if (!queryParam["InspectNo"].IsEmpty())
                {
                    //sql.Append($" AND InspectNo = N'{queryParam["InspectNo"]}'");
                    sql.Append($" AND Q.InspectNo like N'%{queryParam["InspectNo"]}%'");
                }
                //物料编号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND Q.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND Q.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND V.ItemName like N'%{queryParam["SmallClass"]}%'");
 
                }
                //物料规格 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Q.Spec like N'%{queryParam["Spec"]}%'");
                }
                //供应商编码 是否为空进行查询
                if (!queryParam["SupplierCode"].IsEmpty())
                {
                    //sql.Append($" AND SupplierCode = N'{queryParam["SupplierCode"]}'");
                    sql.Append($" AND Q.SupplierCode like N'%{queryParam["SupplierCode"]}%'");
                }
                //实验室状态 是否为空进行查询
                if (!queryParam["LabStatus"].IsEmpty())
                {
                     sql.Append($" AND Q.LabStatus = N'{queryParam["LabStatus"]}'");
 
                }
                //质检状态 是否为空进行查询
                if (!queryParam["QualityStatus"].IsEmpty())
                {
                    sql.Append($" AND Q.QualityStatus = N'{queryParam["QualityStatus"]}'");
 
                }
                //判定结果 是否为空进行查询
                if (!queryParam["TestResult"].IsEmpty())
                {
                    //sql.Append($" AND TestResult = N'{queryParam["TestResult"]}'");
                    sql.Append($" AND TestResult like N'%{queryParam["TestResult"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //有效标志 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
                }
  
                //实验室检验时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND Q.LabInspectionTime >= N'{queryParam["StartTime"]}'");
                   
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND Q.LabInspectionTime <= N'{queryParam["EndTime"]}'");

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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ReceiptId]
                      ,[TestMethodIdId]
                      ,[InspectNo]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[SmallClass]
                      ,[Spec]
                      ,[SupplierCode]
                      ,[LabStatus]
                      ,[QualityStatus]
                      ,[TestResult]
                      ,[Remark]
                      ,[IsEnabled]
                      ,[Attachment]
                      ,[Address]
                      ,[QualityInspector]
                      ,[QualityTime]
                      ,[LabInspector]
                      ,[LabInspectionTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IQCQualityCheck] where IsEnabled=1  ");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList1(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ReceiptId]
                      ,[TestMethodIdId]
                      ,[InspectNo]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[SmallClass]
                      ,[Spec]
                      ,[SupplierCode]
                      ,[LabStatus]
                      ,[QualityStatus]
                      ,[TestResult]
                      ,[Remark]
                      ,[IsEnabled]
                      ,[Attachment]
                      ,[Address]
                      ,[QualityInspector]
                      ,[QualityTime]
                      ,[LabInspector]
                      ,[LabInspectionTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IQCQualityCheck] where IsEnabled=1 and TestMethodIdId =''
    or LabStatus ='' 
    or [QualityStatus]='' ");
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_IQCQualityCheckEntity entity, out string msg)
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
                    entity.Create();
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        public int SaveList(List<QC_IQCQualityCheckEntity> list)
        {
           return this.BaseRepository().Insert(list);
        }
        
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IQCQualityCheckEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IQCQualityCheckEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[QC_IQCQualityCheck] set ");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            QC_IQCQualityCheckEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            { 
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
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
                sql.Append($@"DELETE FROM [dbo].[QC_IQCQualityCheck] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IQCQualityCheckEntity</returns>
        public QC_IQCQualityCheckEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IQCQualityCheckEntity</returns>
        public QC_IQCQualityCheckEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IQCQualityCheckEntity 对象</returns>
        public QC_IQCQualityCheckEntity Get_ExpressionEntity(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IQCQualityCheckEntity 列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> Get_ExpressionList(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition)
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
        //    RepositoryFactory<QC_IQCQualityCheckEntity> bomService = new RepositoryFactory<QC_IQCQualityCheckEntity>();
        
        //    QC_IQCQualityCheckEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IQCQualityCheckDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IQCQualityCheck_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<QC_IQCQualityCheckEntity> QC_IQCQualityCheckEntity_list =  db2.FindList<QC_IQCQualityCheckEntity>(sql.ToString());
                return QC_IQCQualityCheckEntity_list;
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
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
                DataTable QC_IQCQualityCheckEntity_DataTable = db2.FindTable(sql.ToString());
                return QC_IQCQualityCheckEntity_DataTable;
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
        ///  <param name="index">生成个数</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode,int index, out string returnNum, out string messageCode)
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
        /// 根据物料编码获取原材料配置信息 存储过程调用示例
        /// </summary>
      
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetTestMethod( out string messageCode)
        {
            bool b = false;
          
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                    
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };              
            parameters[0].Direction = ParameterDirection.Output;

            try
            {

                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("QC_GetTestMethodByMaterialCode", parameters);
                //返回参数值
             
                messageCode = parameters[0].Value.ToString();
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
        /// 创建日期: 2021-09-10 13:29:40
        /// 任务编号: IQC检测记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ReceiptId] as '收料通知单Id'
                      ,[TestMethodIdId] as '检测方法Id'
                      ,[InspectNo] as '检验单号'
                      ,[MaterialCode] as '物料编号'
                      ,[MaterialName] as '物料编码'
                      ,[SmallClass] as '物料小类'
                      ,[Spec] as '物料规格'
                      ,[SupplierCode] as '供应商编码'
                      ,[LabStatus] as '实验室状态'
                      ,[QualityStatus] as '质检状态'
                      ,[TestResult] as '判定结果'
                      ,[Remark] as '备注'
                      ,[IsEnabled] as '有效标志'
                      ,[Attachment] as '附件名称'
                      ,[Address] as '附件地址'
                      ,[QualityInspector] as '检验员'
                      ,[QualityTime] as '检验时间'
                      ,[LabInspector] as '实验室检验人'
                      ,[LabInspectionTime] as '实验室检验时间'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[QC_IQCQualityCheck] where IsEnabled=1  ");
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
                string saveFileName = "IQC检测记录_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("IQC检测记录", dt, true);
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

        public int RemoveForm(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition)
        {
            throw new NotImplementedException();
        }
    }
}
