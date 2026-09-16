using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.MaterialManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.MaterialManage;
using ALP.Application.UtilExtend.Offices;
using ALP.Data;
using ALP.Application.Service.SystemManage;

namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-25
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_ReceiptNoticeService 业务服务类
    /// 4.任务编号: 收料通知单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ReceiptNotice_Service : RepositoryFactory<MM_ReceiptNoticeEntity>, MM_ReceiptNoticeIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ReceiptNoticeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[PurchaseId]
                      ,[ReceiptCode]
                      ,[LineNum]
                      ,[ArrivalQty]
                      ,[ArrivalTime]
                      ,[ReceiptStatus]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_ReceiptNotice] where 1=1  ");
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
                //采购订单Id 是否为空进行查询
                if (!queryParam["PurchaseId"].IsEmpty())
                {
                    //sql.Append($" AND PurchaseId = N'{queryParam["PurchaseId"]}'");
                    sql.Append($" AND PurchaseId like N'%{queryParam["PurchaseId"]}%'");
                }
                //收料通知单号 是否为空进行查询
                if (!queryParam["ReceiptCode"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND ReceiptCode like N'%{queryParam["ReceiptCode"]}%'");
                }
                //行号 是否为空进行查询
                if (!queryParam["LineNum"].IsEmpty())
                {
                    //sql.Append($" AND LineNum = N'{queryParam["LineNum"]}'");
                    sql.Append($" AND LineNum like N'%{queryParam["LineNum"]}%'");
                }
                //通知到货数量 是否为空进行查询
                if (!queryParam["ArrivalQty"].IsEmpty())
                {
                    //sql.Append($" AND ArrivalQty = N'{queryParam["ArrivalQty"]}'");
                    sql.Append($" AND ArrivalQty like N'%{queryParam["ArrivalQty"]}%'");
                }
                //到货日期 是否为空进行查询
                if (!queryParam["ArrivalTime"].IsEmpty())
                {
                    //sql.Append($" AND ArrivalTime = N'{queryParam["ArrivalTime"]}'");
                    sql.Append($" AND ArrivalTime like N'%{queryParam["ArrivalTime"]}%'");
                }
                //收料通知单状态 是否为空进行查询
                if (!queryParam["ReceiptStatus"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptStatus = N'{queryParam["ReceiptStatus"]}'");
                    sql.Append($" AND ReceiptStatus like N'%{queryParam["ReceiptStatus"]}%'");
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT MR.Id,
                               MR.FactoryCode,
                               MR.FactoryName,
                               MR.PurchaseId,
                               MR.ReceiptCode,
                               MR.LineNum,
                               MR.ArrivalQty,
                               MR.ArrivalTime,
                               MR.ReceiptStatus,
                               v3.ItemName ReceiptStatusName,
                               MR.Creator,
                               MR.CreateTime,
                               MR.Remark,
                               CASE WHEN ISNULL(PP.ProductOrder,'')=''
							   THEN mr.ProductOrder ELSE PP.ProductOrder END ProductOrder ,
                               PP.PurchaseOrder,
                               CASE
                                   WHEN ISNULL(PP.MaterialCode, '') = '' THEN
                                       MR.MaterialCode
                                   ELSE
                                       PP.MaterialCode
                               END MaterialCode,
                               CASE
                                   WHEN ISNULL(PP.MaterialName, '') = '' THEN
                                       MR.MaterialName
                                   ELSE
                                       PP.MaterialName
                               END MaterialName,
                               CASE
                                   WHEN ISNULL(PP.Spec, '') = '' THEN
                                       MR.Spec
                                   ELSE
                                       PP.Spec
                               END Spec,
                               CASE
                                   WHEN ISNULL(PP.SmallClass, '') = '' THEN
                                       MR.SmallClass
                                   ELSE
                                       PP.SmallClass
                               END SmallClass,
                               CASE
                                   WHEN ISNULL(V.ItemName, '') = '' THEN
                                       MR.SmallClassName
                                   ELSE
                                       V.ItemName
                               END SmallClassName,
                               CASE
                                   WHEN ISNULL(PP.Unit, '') = '' THEN
                                       MR.Unit
                                   ELSE
                                       PP.Unit
                               END Unit,
                               CASE
                                   WHEN ISNULL(PP.Supplier, '') = '' THEN
                                       MR.SupplierCode
                                   ELSE
                                       PP.Supplier
                               END SupplierCode,
                               PP.PurchaseNum,
                               CASE
                                   WHEN ISNULL(BS.SupplierName, '') = '' THEN
                                       MR.SupplierName
                                   ELSE
                                       BS.SupplierName
                               END SupplierName,
                               MR.ArrivalQty - (CASE
                                                    WHEN ISNULL(MRI.InQty, 0) = 0 THEN
                                                        MR.InQty
                                                    ELSE
                                                        ISNULL(MRI.InQty, 0)
                                                END
                                               ) NoArrivalQty,
                               CASE
                                   WHEN ISNULL(MRI.InQty, 0) = 0 THEN
                                       MR.InQty
                                   ELSE
                                       ISNULL(MRI.InQty, 0)
                               END InQty,
                               MF.Warehouse,
                               M.ResourceName,
                               V2.ItemName zj,
                               PO.BoxDate,
                               MF.IsUsed,
                               MR.ManufacturerCode,
                               MR.ManufacturerName,
                               MR.SupplierCode2,
                               MR.SupplierName2,
                               MR.SupplierCode3,
                               MR.SupplierName3,
                               MR.SupplierCode4,
                               MR.SupplierName4,
                               MR.SupplierCode5,
                               MR.SupplierName5,
                               MR.SupplierCode6,
                               MR.SupplierName6,
                               MR.PurchaseOrder SAPPurchaseOrder,
                               MR.PurchaseOrderLineNum SAPPurchaseOrderLineNum
                        FROM dbo.MM_ReceiptNotice MR
                            LEFT JOIN dbo.PL_PurchaseOrder PP
                                ON MR.PurchaseId = PP.Id
                            LEFT JOIN dbo.Base_SupplierManage BS
                                ON BS.SupplierCode = PP.Supplier
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnName = 'MaterialSmall'
                                   AND V.ItemValue = PP.SmallClass
                            LEFT JOIN
                            (
                                SELECT BusinessId,
                                       SUM(Qty) InQty
                                FROM [dbo].[MM_RawMaterialIn]
                                WHERE InType = '1' AND IsDeleted=0
                                GROUP BY BusinessId
                            ) MRI
                                ON MR.Id = MRI.BusinessId
                            LEFT JOIN dbo.Base_MaterialFactory MF
                                ON (
                                       MR.MaterialCode = MF.MaterialCode
                                       OR PP.MaterialCode = MF.MaterialCode
                                   )
                                   AND MR.FactoryCode = MF.FactoryCode
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON M.ResourceCode = MF.Warehouse
                            LEFT JOIN [dbo].[QC_IQCQualityCheck] IQC
                                ON IQC.ReceiptId = MR.Id
                            LEFT JOIN dbo.V_DataDictionary V2
                                ON V2.EnCode = 'QualityJudgement'
                                   AND V2.ItemValue = IQC.TestResult
                            LEFT JOIN dbo.PL_ProductionOrder PO
                                ON PO.ProductOrder = PP.ProductOrder
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'ReceivingNoticeStatus'
                                   AND MR.ReceiptStatus = v3.ItemValue
                        WHERE ISNULL(MR.IsDeleted, 0) = 0 ");

            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND MR.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["PurchaseId"].IsEmpty())
                {
                    sql.Append($" AND PurchaseId = N'{queryParam["PurchaseId"]}'");
                }
                //收料通知单号 是否为空进行查询
                if (!queryParam["ReceiptCode"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND MR.ReceiptCode like N'%{queryParam["ReceiptCode"]}%'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND (PP.ProductOrder like N'%{queryParam["ProductOrder"]}%' OR mr.ProductOrder LIKE '%{queryParam["ProductOrder"]}%')");
                }
                if (!queryParam["PurchaseOrder"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND (PP.PurchaseOrder like N'%{queryParam["PurchaseOrder"]}%' OR mr.PurchaseOrder LIKE N'%{queryParam["PurchaseOrder"]}%')");
                }
                if (!queryParam["ResourceName"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND M.ResourceName like N'%{queryParam["ResourceName"]}%'");
                }

                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND pp.SmallClass = N'{queryParam["SmallClass"]}'");
                }
                if (!queryParam["Supplier"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND (PP.Supplier like N'%{queryParam["Supplier"]}%' OR BS.SupplierName like N'%{queryParam["Supplier"]}%' OR MR.SupplierCode like N'%{queryParam["Supplier"]}%' OR MR.SupplierName like N'%{queryParam["Supplier"]}%') ");
                }
                //if (!queryParam["SupplierName"].IsEmpty())
                //{
                //    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                //    sql.Append($" AND BS.SupplierName like N'%{queryParam["SupplierName"]}%'");
                //}
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND (PP.MaterialCode like N'%{queryParam["MaterialCode"]}%' OR PP.MaterialName like N'%{queryParam["MaterialCode"]}%' OR MR.MaterialCode like N'%{queryParam["MaterialCode"]}%' OR MR.MaterialName like N'%{queryParam["MaterialCode"]}%') ");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND (PP.Spec like N'%{queryParam["Spec"]}%' OR MR.Spec like N'%{queryParam["Spec"]}%') ");
                }
                if (!queryParam["SupplierName"].IsEmpty())
                {
                    //sql.Append($" AND ReceiptCode = N'{queryParam["ReceiptCode"]}'");
                    sql.Append($" AND BS.SupplierName like N'%{queryParam["SupplierName"]}%'");
                }
                if (!queryParam["QualityType"].IsEmpty())
                {
                    sql.Append($" AND  IQC.TestResult = N'{queryParam["QualityType"]}'");
                }
                //行号 是否为空进行查询
                if (!queryParam["LineNum"].IsEmpty())
                {
                    sql.Append($" AND LineNum = N'{queryParam["LineNum"]}'");
                }
                //通知到货数量 是否为空进行查询
                if (!queryParam["ArrivalQty"].IsEmpty())
                {
                    //sql.Append($" AND ArrivalQty = N'{queryParam["ArrivalQty"]}'");
                    sql.Append($" AND ArrivalQty like N'%{queryParam["ArrivalQty"]}%'");
                }
                //到货日期 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND MR.ArrivalTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND MR.ArrivalTime <= N'{queryParam["EndTime"]}'");
                }
                //收料通知单状态 是否为空进行查询
                if (!queryParam["ReceiptStatus"].IsEmpty())
                {
                    sql.Append($" AND MR.ReceiptStatus = N'{queryParam["ReceiptStatus"]}'");
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ReceiptNoticeEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[PurchaseId]
                      ,[ReceiptCode]
                      ,[LineNum]
                      ,[ArrivalQty]
                      ,[ArrivalTime]
                      ,[ReceiptStatus]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_ReceiptNotice] where 1=1  ");
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_ReceiptNoticeEntity entity, out string msg)
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_ReceiptNoticeEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ReceiptNoticeEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_ReceiptNotice] set ");
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_ReceiptNoticeEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                //  entity.IsDeleted = true;
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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
                sql.Append($@"DELETE FROM [dbo].[MM_ReceiptNotice] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_ReceiptNoticeEntity</returns>
        public MM_ReceiptNoticeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_ReceiptNoticeEntity</returns>
        public MM_ReceiptNoticeEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_ReceiptNoticeEntity 对象</returns>
        public MM_ReceiptNoticeEntity Get_ExpressionEntity(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_ReceiptNoticeEntity 列表</returns>
        public IEnumerable<MM_ReceiptNoticeEntity> Get_ExpressionList(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition)
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
        //    RepositoryFactory<MM_ReceiptNoticeEntity> bomService = new RepositoryFactory<MM_ReceiptNoticeEntity>();

        //    MM_ReceiptNoticeEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_ReceiptNoticeDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_ReceiptNotice_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ReceiptNoticeEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<MM_ReceiptNoticeEntity> MM_ReceiptNoticeEntity_list = db2.FindList<MM_ReceiptNoticeEntity>(sql.ToString());
                return MM_ReceiptNoticeEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        public DataTable GET_ArrivalQtyCount(string PurchaseId, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            msg = "";
            sql.Append($@" SELECT sum(ArrivalQty) AS Qty  FROM MM_ReceiptNotice WHERE PurchaseId='{PurchaseId}'");
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                DataTable MM_ReceiptNoticeEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_ReceiptNoticeEntity_DataTable;
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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
                DataTable MM_ReceiptNoticeEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_ReceiptNoticeEntity_DataTable;
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [PurchaseId] as '采购订单Id'
                      ,[ReceiptCode] as '收料通知单号'
                      ,[LineNum] as '行号'
                      ,[ArrivalQty] as '通知到货数量'
                      ,[ArrivalTime] as '到货日期'
                      ,[ReceiptStatus] as '收料通知单状态'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[MM_ReceiptNotice] where 1=1  ");
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
                string saveFileName = "收料通知单表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("收料通知单表", dt, true);
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

        public int RemoveForm(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #region SAP收料通知单同步
        public void SaveSAPMM_ReceiptNotice(List<MM_ReceiptNoticeEntity> lstEntity)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                var smallClassList = new DataItemDetailService().GetDataItemList("MaterialSmall").ToList();
                //SAP状态转为MES状态
                var statusList = new DataItemDetailService().GetDataItemList("ReceivingNoticeStatus").ToList();

                foreach (var item in lstEntity)
                {
                    if (item.IsDeleted == false)
                    {
                        if (string.IsNullOrEmpty(item.ReceiptCode))
                        {
                            throw new Exception("收料通知单号必须传！");
                        }
                        if (item.LineNum.IsEmpty())
                        {
                            throw new Exception("收料通知单行号必须传！");
                        }
                        if (string.IsNullOrEmpty(item.MaterialCode))
                            throw new Exception("物料编码不能为空");

                        //将SAP物料编码转化为MES的物料编码
                        var materSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode='{0}' and IsEnabled=1", item.MaterialCode);
                        var materData = new RepositoryFactory().BaseRepository().FindTable(materSql);
                        if (materData.Rows.Count == 0)
                        {
                            throw new Exception(item.MaterialCode + "物料编码未同步");
                        }
                        item.MaterialCode = materData.Rows[0]["MaterialCode"].ToString();
                        item.MaterialName = materData.Rows[0]["MaterialName"].ToString();
                        item.Spec = materData.Rows[0]["Spec"].ToString();
                        item.SmallClass = materData.Rows[0]["SmallClass"].ToString();
                        item.SmallClassName = smallClassList.Find(t => t.ItemValue == item.SmallClass)?.ItemName;

                        // 将sap传过来的数据进行拆分
                        if (!string.IsNullOrEmpty(item.SupplierName2))
                        {
                            var arrSupplierName = item.SupplierName2.Split(",");
                            if (arrSupplierName.Length > 0)
                            {
                                item.SupplierName2 = arrSupplierName[0];
                            }
                            if (arrSupplierName.Length > 1)
                            {
                                item.SupplierName3 = arrSupplierName[1];
                            }
                            if (arrSupplierName.Length > 2)
                            {
                                item.SupplierName4 = arrSupplierName[2];
                            }
                            if (arrSupplierName.Length > 3)
                            {
                                item.SupplierName5 = arrSupplierName[3];
                            }
                            if (arrSupplierName.Length > 4)
                            {
                                item.SupplierName6 = arrSupplierName[4];
                            }
                        }
                        //判断采购订单是否存在通过工厂、采购订单、物料编码判断
                        var receiptNoticeEntity = Get_ExpressionEntity(t => t.ReceiptCode == item.ReceiptCode && t.LineNum == item.LineNum);
                        if (receiptNoticeEntity == null)
                        {
                            item.CreateTime = DateTime.Now;
                            item.Create();
                            item.Creator = "SAP";
                            item.ReceiptStatus = statusList.Find(t => t.Description == item.ReceiptStatus)?.ItemValue;
                            db.Insert(item);
                        }
                        else
                        {
                            //修改逻辑
                            receiptNoticeEntity.LineNum = item.LineNum;
                            receiptNoticeEntity.FactoryCode = string.IsNullOrEmpty(item.FactoryCode) ? receiptNoticeEntity.FactoryCode : item.FactoryCode;
                            receiptNoticeEntity.FactoryName = string.IsNullOrEmpty(item.FactoryName) ? receiptNoticeEntity.FactoryName : item.FactoryName;
                            receiptNoticeEntity.ArrivalQty = item.ArrivalQty == null ? receiptNoticeEntity.ArrivalQty : item.ArrivalQty;
                            receiptNoticeEntity.PurchaseNum = item.PurchaseNum;
                            receiptNoticeEntity.ManufacturerCode = string.IsNullOrEmpty(item.ManufacturerCode) ? receiptNoticeEntity.ManufacturerCode : item.ManufacturerCode;
                            receiptNoticeEntity.ManufacturerName = string.IsNullOrEmpty(item.ManufacturerName) ? receiptNoticeEntity.ManufacturerName : item.ManufacturerName;
                            receiptNoticeEntity.ArrivalQty = item.ArrivalQty;
                            receiptNoticeEntity.ArrivalTime = item.ArrivalTime;
                            receiptNoticeEntity.SupplierName2 = string.IsNullOrEmpty(item.SupplierName2) ? receiptNoticeEntity.SupplierName2 : item.SupplierName2;
                            receiptNoticeEntity.SupplierName3 = string.IsNullOrEmpty(item.SupplierName3) ? receiptNoticeEntity.SupplierName3 : item.SupplierName3;
                            receiptNoticeEntity.SupplierName4 = string.IsNullOrEmpty(item.SupplierName4) ? receiptNoticeEntity.SupplierName4 : item.SupplierName4;
                            receiptNoticeEntity.SupplierName5 = string.IsNullOrEmpty(item.SupplierName5) ? receiptNoticeEntity.SupplierName5 : item.SupplierName5;
                            receiptNoticeEntity.SupplierName6 = string.IsNullOrEmpty(item.SupplierName6) ? receiptNoticeEntity.SupplierName6 : item.SupplierName6;
                            receiptNoticeEntity.ModifyTime = DateTime.Now;
                            receiptNoticeEntity.PurchaseOrder = string.IsNullOrEmpty(item.PurchaseOrder) ? receiptNoticeEntity.PurchaseOrder : item.PurchaseOrder;
                            receiptNoticeEntity.PurchaseOrderLineNum = string.IsNullOrEmpty(item.PurchaseOrderLineNum) ? receiptNoticeEntity.PurchaseOrderLineNum : item.PurchaseOrderLineNum;
                            receiptNoticeEntity.MaterialCode = item.MaterialCode;
                            receiptNoticeEntity.MaterialName = item.MaterialName;
                            receiptNoticeEntity.Spec = item.Spec;
                            receiptNoticeEntity.SmallClass = item.SmallClass;
                            receiptNoticeEntity.SmallClassName = item.SmallClassName;
                            receiptNoticeEntity.SupplierCode = item.SupplierCode;
                            receiptNoticeEntity.SupplierName = string.IsNullOrEmpty(item.SupplierName) ? receiptNoticeEntity.SupplierName : item.SupplierName;
                            receiptNoticeEntity.ReceiptStatus = statusList.Find(t => t.Description == item.ReceiptStatus)?.ItemValue;
                            receiptNoticeEntity.ProductOrder = item.ProductOrder;
                            db.Update(receiptNoticeEntity);
                        }
                    }
                    else
                    {
                        var receiptNoticeEntity = Get_ExpressionList(t => t.ReceiptCode == item.ReceiptCode && t.FactoryCode == item.FactoryCode);
                        //删除的逻辑
                        if (receiptNoticeEntity == null)
                        {
                            throw new Exception(item.PurchaseOrder + "收料通知单不存在无法进行删除");
                        }
                        else
                        {
                            db.Delete(receiptNoticeEntity);
                        }
                    }
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
        #endregion

    }
}
