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
using ALP.Application.Entity.PlanManage;
using ALP.Application.IService.ProduceManage;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-11
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TransferCardService 业务服务类
    /// 4.任务编号: 流转卡信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TransferCard_Service : RepositoryFactory<PM_TransferCardEntity>, PM_TransferCardIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[WorkOrderTyp
                      ,[ExeWorkOrder]
                      ,[CardCode]
                      ,[CardName]
                      ,[CardType]
                      ,[PerPalletQty]
                      ,[ContainerNO]
                      ,[BJGY]
                      ,[WorkOrderRemark]
                      ,[MaterialCode]
                      ,[Spec]
                      ,[MMXH]
                      ,[JCGG]
                      ,[BWXH]
                      ,[SCTPSL]
                      ,[SCTPSLP]
                      ,[BZTPSL]
                      ,[KCKX]
                      ,[UV]
                      ,[TotalSheets]
                      ,[ActualSheets]
                      ,[OrderPieces]
                      ,[ProductPieces]
                      ,[TPGG]
                      ,[OrderPallet]
                      ,[PerPallerBox]
                      ,[BZDHSL]
                      ,[Description]
                      ,[PalletNum]
                      ,[PaperBoxModel]
                      ,[BoxDate]
                      ,[FrozenMark]
                      ,[ReworkMark]
                      ,[ScrapMark]
                      ,[PrintStatus]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_TransferCard] where IsDeleted = 0 ");
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
                //工单类型 是否为空进行查询
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrderType = N'{queryParam["WorkOrderType"]}'");
                    sql.Append($" AND WorkOrderType like N'%{queryParam["WorkOrderType"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["CardCode"].IsEmpty())
                {
                    //sql.Append($" AND CardCode = N'{queryParam["CardCode"]}'");
                    sql.Append($" AND CardCode like N'%{queryParam["CardCode"]}%'");
                }
                //流转卡名称 是否为空进行查询
                if (!queryParam["CardName"].IsEmpty())
                {
                    //sql.Append($" AND CardName = N'{queryParam["CardName"]}'");
                    sql.Append($" AND CardName like N'%{queryParam["CardName"]}%'");
                }
                //流转卡类型 是否为空进行查询
                if (!queryParam["CardType"].IsEmpty())
                {
                    //sql.Append($" AND CardType = N'{queryParam["CardType"]}'");
                    sql.Append($" AND CardType like N'%{queryParam["CardType"]}%'");
                }
                //单拖张数 是否为空进行查询
                if (!queryParam["PerPalletQty"].IsEmpty())
                {
                    //sql.Append($" AND PerPalletQty = N'{queryParam["PerPalletQty"]}'");
                    sql.Append($" AND PerPalletQty like N'%{queryParam["PerPalletQty"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //背胶工艺 是否为空进行查询
                if (!queryParam["BJGY"].IsEmpty())
                {
                    //sql.Append($" AND BJGY = N'{queryParam["BJGY"]}'");
                    sql.Append($" AND BJGY like N'%{queryParam["BJGY"]}%'");
                }
                //工单备注 是否为空进行查询
                if (!queryParam["WorkOrderRemark"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrderRemark = N'{queryParam["WorkOrderRemark"]}'");
                    sql.Append($" AND WorkOrderRemark like N'%{queryParam["WorkOrderRemark"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Spec like N'%{queryParam["Spec"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND MMXH = N'{queryParam["MMXH"]}'");
                    sql.Append($" AND MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //挤出规格 是否为空进行查询
                if (!queryParam["JCGG"].IsEmpty())
                {
                    //sql.Append($" AND JCGG = N'{queryParam["JCGG"]}'");
                    sql.Append($" AND JCGG like N'%{queryParam["JCGG"]}%'");
                }
                //板纹型号 是否为空进行查询
                if (!queryParam["BWXH"].IsEmpty())
                {
                    //sql.Append($" AND BWXH = N'{queryParam["BWXH"]}'");
                    sql.Append($" AND BWXH like N'%{queryParam["BWXH"]}%'");
                }
                //生产托盘数量（张） 是否为空进行查询
                if (!queryParam["SCTPSL"].IsEmpty())
                {
                    //sql.Append($" AND SCTPSL = N'{queryParam["SCTPSL"]}'");
                    sql.Append($" AND SCTPSL like N'%{queryParam["SCTPSL"]}%'");
                }
                //生产托盘数量（片） 是否为空进行查询
                if (!queryParam["SCTPSLP"].IsEmpty())
                {
                    //sql.Append($" AND SCTPSLP = N'{queryParam["SCTPSLP"]}'");
                    sql.Append($" AND SCTPSLP like N'%{queryParam["SCTPSLP"]}%'");
                }
                //包装托盘数量 是否为空进行查询
                if (!queryParam["BZTPSL"].IsEmpty())
                {
                    //sql.Append($" AND BZTPSL = N'{queryParam["BZTPSL"]}'");
                    sql.Append($" AND BZTPSL like N'%{queryParam["BZTPSL"]}%'");
                }
                //开槽扣型 是否为空进行查询
                if (!queryParam["KCKX"].IsEmpty())
                {
                    //sql.Append($" AND KCKX = N'{queryParam["KCKX"]}'");
                    sql.Append($" AND KCKX like N'%{queryParam["KCKX"]}%'");
                }
                //UV 是否为空进行查询
                if (!queryParam["UV"].IsEmpty())
                {
                    //sql.Append($" AND UV = N'{queryParam["UV"]}'");
                    sql.Append($" AND UV like N'%{queryParam["UV"]}%'");
                }
                //订单张数 是否为空进行查询
                if (!queryParam["TotalSheets"].IsEmpty())
                {
                    //sql.Append($" AND TotalSheets = N'{queryParam["TotalSheets"]}'");
                    sql.Append($" AND TotalSheets like N'%{queryParam["TotalSheets"]}%'");
                }
                //生产张数 是否为空进行查询
                if (!queryParam["ActualSheets"].IsEmpty())
                {
                    //sql.Append($" AND ActualSheets = N'{queryParam["ActualSheets"]}'");
                    sql.Append($" AND ActualSheets like N'%{queryParam["ActualSheets"]}%'");
                }
                //订单片数 是否为空进行查询
                if (!queryParam["OrderPieces"].IsEmpty())
                {
                    //sql.Append($" AND OrderPieces = N'{queryParam["OrderPieces"]}'");
                    sql.Append($" AND OrderPieces like N'%{queryParam["OrderPieces"]}%'");
                }
                //生产片数 是否为空进行查询
                if (!queryParam["ProductPieces"].IsEmpty())
                {
                    //sql.Append($" AND ProductPieces = N'{queryParam["ProductPieces"]}'");
                    sql.Append($" AND ProductPieces like N'%{queryParam["ProductPieces"]}%'");
                }
                //托盘规格 是否为空进行查询
                if (!queryParam["TPGG"].IsEmpty())
                {
                    //sql.Append($" AND TPGG = N'{queryParam["TPGG"]}'");
                    sql.Append($" AND TPGG like N'%{queryParam["TPGG"]}%'");
                }
                //单柜拖数 是否为空进行查询
                if (!queryParam["OrderPallet"].IsEmpty())
                {
                    //sql.Append($" AND OrderPallet = N'{queryParam["OrderPallet"]}'");
                    sql.Append($" AND OrderPallet like N'%{queryParam["OrderPallet"]}%'");
                }
                //单柜盒数 是否为空进行查询
                if (!queryParam["PerPallerBox"].IsEmpty())
                {
                    //sql.Append($" AND PerPallerBox = N'{queryParam["PerPallerBox"]}'");
                    sql.Append($" AND PerPallerBox like N'%{queryParam["PerPallerBox"]}%'");
                }
                //单盒片数 是否为空进行查询
                if (!queryParam["BZDHSL"].IsEmpty())
                {
                    //sql.Append($" AND BZDHSL = N'{queryParam["BZDHSL"]}'");
                    sql.Append($" AND BZDHSL like N'%{queryParam["BZDHSL"]}%'");
                }
                //说明书 是否为空进行查询
                if (!queryParam["Description"].IsEmpty())
                {
                    //sql.Append($" AND Description = N'{queryParam["Description"]}'");
                    sql.Append($" AND Description like N'%{queryParam["Description"]}%'");
                }
                //托盘编号 是否为空进行查询
                if (!queryParam["PalletNum"].IsEmpty())
                {
                    //sql.Append($" AND PalletNum = N'{queryParam["PalletNum"]}'");
                    sql.Append($" AND PalletNum like N'%{queryParam["PalletNum"]}%'");
                }
                //纸盒型号 是否为空进行查询
                if (!queryParam["PaperBoxModel"].IsEmpty())
                {
                    //sql.Append($" AND PaperBoxModel = N'{queryParam["PaperBoxModel"]}'");
                    sql.Append($" AND PaperBoxModel like N'%{queryParam["PaperBoxModel"]}%'");
                }
                //纸盒日期 是否为空进行查询
                if (!queryParam["BoxDate"].IsEmpty())
                {
                    //sql.Append($" AND BoxDate = N'{queryParam["BoxDate"]}'");
                    sql.Append($" AND BoxDate like N'%{queryParam["BoxDate"]}%'");
                }
                //冻结标记 是否为空进行查询
                if (!queryParam["FrozenMark"].IsEmpty())
                {
                    //sql.Append($" AND FrozenMark = N'{queryParam["FrozenMark"]}'");
                    sql.Append($" AND FrozenMark like N'%{queryParam["FrozenMark"]}%'");
                }
                //返工标记 是否为空进行查询
                if (!queryParam["ReworkMark"].IsEmpty())
                {
                    //sql.Append($" AND ReworkMark = N'{queryParam["ReworkMark"]}'");
                    sql.Append($" AND ReworkMark like N'%{queryParam["ReworkMark"]}%'");
                }
                //报废标记 是否为空进行查询
                if (!queryParam["ScrapMark"].IsEmpty())
                {
                    //sql.Append($" AND ScrapMark = N'{queryParam["ScrapMark"]}'");
                    sql.Append($" AND ScrapMark like N'%{queryParam["ScrapMark"]}%'");
                }
                //打印状态 是否为空进行查询
                if (!queryParam["PrintStatus"].IsEmpty())
                {
                    //sql.Append($" AND PrintStatus = N'{queryParam["PrintStatus"]}'");
                    sql.Append($" AND PrintStatus like N'%{queryParam["PrintStatus"]}%'");
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[WorkOrderType]
                      ,[ExeWorkOrder]
                      ,[CardCode]
                      ,[CardName]
                      ,[CardType]
                      ,[PerPalletQty]
                      ,[ContainerNO]
                      ,[BJGY]
                      ,[WorkOrderRemark]
                      ,[MaterialCode]
                      ,[Spec]
                      ,[MMXH]
                      ,[JCGG]
                      ,[BWXH]
                      ,[SCTPSL]
                      ,[SCTPSLP]
                      ,[BZTPSL]
                      ,[KCKX]
                      ,[UV]
                      ,[TotalSheets]
                      ,[ActualSheets]
                      ,[OrderPieces]
                      ,[ProductPieces]
                      ,[TPGG]
                      ,[OrderPallet]
                      ,[PerPallerBox]
                      ,[BZDHSL]
                      ,[Description]
                      ,[PalletNum]
                      ,[PaperBoxModel]
                      ,[BoxDate]
                      ,[FrozenMark]
                      ,[ReworkMark]
                      ,[ScrapMark]
                      ,[PrintStatus]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_TransferCard] where IsDeleted = 0 ");
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
                //工单类型 是否为空进行查询
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrderType = N'{queryParam["WorkOrderType"]}'");
                    sql.Append($" AND WorkOrderType like N'%{queryParam["WorkOrderType"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["CardCode"].IsEmpty())
                {
                    //sql.Append($" AND CardCode = N'{queryParam["CardCode"]}'");
                    sql.Append($" AND CardCode like N'%{queryParam["CardCode"]}%'");
                }
                //流转卡名称 是否为空进行查询
                if (!queryParam["CardName"].IsEmpty())
                {
                    //sql.Append($" AND CardName = N'{queryParam["CardName"]}'");
                    sql.Append($" AND CardName like N'%{queryParam["CardName"]}%'");
                }
                //流转卡类型 是否为空进行查询
                if (!queryParam["CardType"].IsEmpty())
                {
                    //sql.Append($" AND CardType = N'{queryParam["CardType"]}'");
                    sql.Append($" AND CardType like N'%{queryParam["CardType"]}%'");
                }
                //单拖张数 是否为空进行查询
                if (!queryParam["PerPalletQty"].IsEmpty())
                {
                    //sql.Append($" AND PerPalletQty = N'{queryParam["PerPalletQty"]}'");
                    sql.Append($" AND PerPalletQty like N'%{queryParam["PerPalletQty"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //背胶工艺 是否为空进行查询
                if (!queryParam["BJGY"].IsEmpty())
                {
                    //sql.Append($" AND BJGY = N'{queryParam["BJGY"]}'");
                    sql.Append($" AND BJGY like N'%{queryParam["BJGY"]}%'");
                }
                //工单备注 是否为空进行查询
                if (!queryParam["WorkOrderRemark"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrderRemark = N'{queryParam["WorkOrderRemark"]}'");
                    sql.Append($" AND WorkOrderRemark like N'%{queryParam["WorkOrderRemark"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Spec like N'%{queryParam["Spec"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND MMXH = N'{queryParam["MMXH"]}'");
                    sql.Append($" AND MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //挤出规格 是否为空进行查询
                if (!queryParam["JCGG"].IsEmpty())
                {
                    //sql.Append($" AND JCGG = N'{queryParam["JCGG"]}'");
                    sql.Append($" AND JCGG like N'%{queryParam["JCGG"]}%'");
                }
                //板纹型号 是否为空进行查询
                if (!queryParam["BWXH"].IsEmpty())
                {
                    //sql.Append($" AND BWXH = N'{queryParam["BWXH"]}'");
                    sql.Append($" AND BWXH like N'%{queryParam["BWXH"]}%'");
                }
                //生产托盘数量（张） 是否为空进行查询
                if (!queryParam["SCTPSL"].IsEmpty())
                {
                    //sql.Append($" AND SCTPSL = N'{queryParam["SCTPSL"]}'");
                    sql.Append($" AND SCTPSL like N'%{queryParam["SCTPSL"]}%'");
                }
                //生产托盘数量（片） 是否为空进行查询
                if (!queryParam["SCTPSLP"].IsEmpty())
                {
                    //sql.Append($" AND SCTPSLP = N'{queryParam["SCTPSLP"]}'");
                    sql.Append($" AND SCTPSLP like N'%{queryParam["SCTPSLP"]}%'");
                }
                //包装托盘数量 是否为空进行查询
                if (!queryParam["BZTPSL"].IsEmpty())
                {
                    //sql.Append($" AND BZTPSL = N'{queryParam["BZTPSL"]}'");
                    sql.Append($" AND BZTPSL like N'%{queryParam["BZTPSL"]}%'");
                }
                //开槽扣型 是否为空进行查询
                if (!queryParam["KCKX"].IsEmpty())
                {
                    //sql.Append($" AND KCKX = N'{queryParam["KCKX"]}'");
                    sql.Append($" AND KCKX like N'%{queryParam["KCKX"]}%'");
                }
                //UV 是否为空进行查询
                if (!queryParam["UV"].IsEmpty())
                {
                    //sql.Append($" AND UV = N'{queryParam["UV"]}'");
                    sql.Append($" AND UV like N'%{queryParam["UV"]}%'");
                }
                //订单张数 是否为空进行查询
                if (!queryParam["TotalSheets"].IsEmpty())
                {
                    //sql.Append($" AND TotalSheets = N'{queryParam["TotalSheets"]}'");
                    sql.Append($" AND TotalSheets like N'%{queryParam["TotalSheets"]}%'");
                }
                //生产张数 是否为空进行查询
                if (!queryParam["ActualSheets"].IsEmpty())
                {
                    //sql.Append($" AND ActualSheets = N'{queryParam["ActualSheets"]}'");
                    sql.Append($" AND ActualSheets like N'%{queryParam["ActualSheets"]}%'");
                }
                //订单片数 是否为空进行查询
                if (!queryParam["OrderPieces"].IsEmpty())
                {
                    //sql.Append($" AND OrderPieces = N'{queryParam["OrderPieces"]}'");
                    sql.Append($" AND OrderPieces like N'%{queryParam["OrderPieces"]}%'");
                }
                //生产片数 是否为空进行查询
                if (!queryParam["ProductPieces"].IsEmpty())
                {
                    //sql.Append($" AND ProductPieces = N'{queryParam["ProductPieces"]}'");
                    sql.Append($" AND ProductPieces like N'%{queryParam["ProductPieces"]}%'");
                }
                //托盘规格 是否为空进行查询
                if (!queryParam["TPGG"].IsEmpty())
                {
                    //sql.Append($" AND TPGG = N'{queryParam["TPGG"]}'");
                    sql.Append($" AND TPGG like N'%{queryParam["TPGG"]}%'");
                }
                //单柜拖数 是否为空进行查询
                if (!queryParam["OrderPallet"].IsEmpty())
                {
                    //sql.Append($" AND OrderPallet = N'{queryParam["OrderPallet"]}'");
                    sql.Append($" AND OrderPallet like N'%{queryParam["OrderPallet"]}%'");
                }
                //单柜盒数 是否为空进行查询
                if (!queryParam["PerPallerBox"].IsEmpty())
                {
                    //sql.Append($" AND PerPallerBox = N'{queryParam["PerPallerBox"]}'");
                    sql.Append($" AND PerPallerBox like N'%{queryParam["PerPallerBox"]}%'");
                }
                //单盒片数 是否为空进行查询
                if (!queryParam["BZDHSL"].IsEmpty())
                {
                    //sql.Append($" AND BZDHSL = N'{queryParam["BZDHSL"]}'");
                    sql.Append($" AND BZDHSL like N'%{queryParam["BZDHSL"]}%'");
                }
                //说明书 是否为空进行查询
                if (!queryParam["Description"].IsEmpty())
                {
                    //sql.Append($" AND Description = N'{queryParam["Description"]}'");
                    sql.Append($" AND Description like N'%{queryParam["Description"]}%'");
                }
                //托盘编号 是否为空进行查询
                if (!queryParam["PalletNum"].IsEmpty())
                {
                    //sql.Append($" AND PalletNum = N'{queryParam["PalletNum"]}'");
                    sql.Append($" AND PalletNum like N'%{queryParam["PalletNum"]}%'");
                }
                //纸盒型号 是否为空进行查询
                if (!queryParam["PaperBoxModel"].IsEmpty())
                {
                    //sql.Append($" AND PaperBoxModel = N'{queryParam["PaperBoxModel"]}'");
                    sql.Append($" AND PaperBoxModel like N'%{queryParam["PaperBoxModel"]}%'");
                }
                //纸盒日期 是否为空进行查询
                if (!queryParam["BoxDate"].IsEmpty())
                {
                    //sql.Append($" AND BoxDate = N'{queryParam["BoxDate"]}'");
                    sql.Append($" AND BoxDate like N'%{queryParam["BoxDate"]}%'");
                }
                //冻结标记 是否为空进行查询
                if (!queryParam["FrozenMark"].IsEmpty())
                {
                    //sql.Append($" AND FrozenMark = N'{queryParam["FrozenMark"]}'");
                    sql.Append($" AND FrozenMark like N'%{queryParam["FrozenMark"]}%'");
                }
                //返工标记 是否为空进行查询
                if (!queryParam["ReworkMark"].IsEmpty())
                {
                    //sql.Append($" AND ReworkMark = N'{queryParam["ReworkMark"]}'");
                    sql.Append($" AND ReworkMark like N'%{queryParam["ReworkMark"]}%'");
                }
                //报废标记 是否为空进行查询
                if (!queryParam["ScrapMark"].IsEmpty())
                {
                    //sql.Append($" AND ScrapMark = N'{queryParam["ScrapMark"]}'");
                    sql.Append($" AND ScrapMark like N'%{queryParam["ScrapMark"]}%'");
                }
                //打印状态 是否为空进行查询
                if (!queryParam["PrintStatus"].IsEmpty())
                {
                    //sql.Append($" AND PrintStatus = N'{queryParam["PrintStatus"]}'");
                    sql.Append($" AND PrintStatus like N'%{queryParam["PrintStatus"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[ProductOrder]
                      ,[WorkOrder]
                      ,[WorkOrderType]
                      ,[ExeWorkOrder]
                      ,[CardCode]
                      ,[CardName]
                      ,[CardType]
                      ,[PerPalletQty]
                      ,[ContainerNO]
                      ,[BJGY]
                      ,[WorkOrderRemark]
                      ,[MaterialCode]
                      ,[Spec]
                      ,[MMXH]
                      ,[JCGG]
                      ,[BWXH]
                      ,[SCTPSL]
                      ,[SCTPSLP]
                      ,[BZTPSL]
                      ,[KCKX]
                      ,[UV]
                      ,[TotalSheets]
                      ,[ActualSheets]
                      ,[OrderPieces]
                      ,[ProductPieces]
                      ,[TPGG]
                      ,[OrderPallet]
                      ,[PerPallerBox]
                      ,[BZDHSL]
                      ,[Description]
                      ,[PalletNum]
                      ,[PaperBoxModel]
                      ,[BoxDate]
                      ,[FrozenMark]
                      ,[ReworkMark]
                      ,[ScrapMark]
                      ,[PrintStatus]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PM_TransferCard] where IsDeleted = 0 ");
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
        public IEnumerable<PM_TransferCardEntity> GetList(Expression<Func<PM_TransferCardEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_TransferCardEntity entity, out string msg)
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_TransferCardEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferCardEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            if (IsUpdate)
            {
                //n = this.BaseRepository().Update(entity_list);
                StringBuilder sql = new StringBuilder();
                if (entity_list.Count > 0)
                {
                    foreach (var Save_obj in entity_list)
                    {
                        StringBuilder sql_temp = new StringBuilder();
                        sql_temp.Append("UPDATE [dbo].[PM_TransferCard] set ");
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
            return n;
        }

        public int insertList(List<PM_TransferCardEntity> list)
        {
            return this.BaseRepository().Insert(list);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_TransferCardEntity entity = this.BaseRepository().FindEntity(keyValue);
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
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
                sql.Append($@"DELETE FROM [dbo].[PM_TransferCard] WHERE WorkOrder=N'{keyValue}'");
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_TransferCardEntity</returns>
        public PM_TransferCardEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_TransferCardEntity</returns>
        public PM_TransferCardEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardEntity 对象</returns>
        public PM_TransferCardEntity Get_ExpressionEntity(Expression<Func<PM_TransferCardEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardEntity 列表</returns>
        public IEnumerable<PM_TransferCardEntity> Get_ExpressionList(Expression<Func<PM_TransferCardEntity, bool>> condition)
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
        //    RepositoryFactory<PM_TransferCardEntity> bomService = new RepositoryFactory<PM_TransferCardEntity>();

        //    PM_TransferCardEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_TransferCardDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_TransferCard_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TransferCardEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_TransferCardEntity> PM_TransferCardEntity_list = db2.FindList<PM_TransferCardEntity>(sql.ToString());
                return PM_TransferCardEntity_list;
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
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
                DataTable PM_TransferCardEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_TransferCardEntity_DataTable;
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ProductOrder] as '订单号'
                      ,[WorkOrder] as '工单号'
                      ,[WorkOrderType] as '工单类型'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[CardCode] as '流转卡编码'
                      ,[CardName] as '流转卡名称'
                      ,[CardType] as '流转卡类型'
                      ,[PerPalletQty] as '单拖张数'
                      ,[ContainerNO] as '柜号'
                      ,[BJGY] as '背胶工艺'
                      ,[WorkOrderRemark] as '工单备注'
                      ,[MaterialCode] as '物料编码'
                      ,[Spec] as '规格型号'
                      ,[MMXH] as '面膜型号'
                      ,[JCGG] as '挤出规格'
                      ,[BWXH] as '板纹型号'
                      ,[SCTPSL] as '生产托盘数量（张）'
                      ,[SCTPSLP] as '生产托盘数量（片）'
                      ,[BZTPSL] as '包装托盘数量'
                      ,[KCKX] as '开槽扣型'
                      ,[UV] as 'UV'
                      ,[TotalSheets] as '订单张数'
                      ,[ActualSheets] as '生产张数'
                      ,[OrderPieces] as '订单片数'
                      ,[ProductPieces] as '生产片数'
                      ,[TPGG] as '托盘规格'
                      ,[OrderPallet] as '单柜拖数'
                      ,[PerPallerBox] as '单柜盒数'
                      ,[BZDHSL] as '单盒片数'
                      ,[Description] as '说明书'
                      ,[PalletNum] as '托盘编号'
                      ,[PaperBoxModel] as '纸盒型号'
                      ,[BoxDate] as '纸盒日期'
                      ,[FrozenMark] as '冻结标记'
                      ,[ReworkMark] as '返工标记'
                      ,[ScrapMark] as '报废标记'
                      ,[PrintStatus] as '打印状态'
                      ,[Creator] as '创建人编码'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人编码'
                      ,[ModifyTime] as '修改时间'
                  FROM [dbo].[PM_TransferCard] where IsDeleted = 0 ");
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
                string saveFileName = "流转卡信息_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("流转卡信息", dt, true);
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

        public int InsertList(List<PM_TransferCardEntity> lstEntity)
        {
            return this.BaseRepository().Insert(lstEntity);
        }

        #region PDA接口
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <returns></returns>
        public DataTable GetCardList_PDA(string serialNumber)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT DISTINCT a.CardCode,
                               a.CardName,
                               a.PalletQty,
                               a.CardType,
                               v1.ItemName CardTypeName,
                               a.CardStatus,
                               v2.ItemName CardStatusName,
                               b.ProcessCode,
                               c.ResourceName ProcessName,
                               b.BusinessType,
                               v3.ItemName BusinessTypeName,
                               d.BGQty
                        FROM dbo.PM_TransferCard a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND a.CardType = v1.ItemValue
                            LEFT JOIN dbo.PM_TransferCardResume b
                                ON b.Flag = '1'
                                   AND a.CardCode = b.CardCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND b.ProcessCode = c.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'CirculationCardStatus'
                                   AND a.CardStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'FlowIdentification'
                                   AND b.BusinessType = v3.ItemValue
                            LEFT JOIN
                            (
                                SELECT CardCode,
                                       ProcessCode,
                                       SUM(Qty) BGQty
                                FROM dbo.PM_TranferCardBGRecord
                                WHERE IsEnabled = 1 AND IsRework='0'
                                GROUP BY CardCode,
                                         ProcessCode
                            ) d
                                ON a.CardCode = d.CardCode
                                   AND b.ProcessCode = d.ProcessCode
                        WHERE a.IsEnabled = 1
                              AND a.SerialNumber = '{serialNumber}'
                        ORDER BY a.CardName ");
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="exeWorkOrder">执行工单号</param>
        /// <returns></returns>
        public DataTable GetCardList_PDA2(string exeWorkOrder)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT DISTINCT a.CardCode,
                               a.CardName,
                               a.PalletQty,
                               a.CardType,
                               v1.ItemName CardTypeName,
                               a.CardStatus,
                               v2.ItemName CardStatusName,
                               b.ProcessCode,
                               c.ResourceName ProcessName,
                               b.BusinessType,
                               v3.ItemName BusinessTypeName,
                               d.BGQty
                        FROM dbo.PM_TransferCard a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND a.CardType = v1.ItemValue
                            LEFT JOIN dbo.PM_TransferCardResume b
                                ON b.Flag = '1'
                                   AND a.CardCode = b.CardCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND b.ProcessCode = c.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'CirculationCardStatus'
                                   AND a.CardStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'FlowIdentification'
                                   AND b.BusinessType = v3.ItemValue
                            LEFT JOIN
                            (
                                SELECT CardCode,
                                       ProcessCode,
                                       SUM(Qty) BGQty
                                FROM dbo.PM_TranferCardBGRecord
                                WHERE IsEnabled = 1 AND IsRework='0'
                                GROUP BY CardCode,
                                         ProcessCode
                            ) d
                                ON a.CardCode = d.CardCode
                                   AND b.ProcessCode = d.ProcessCode
                        WHERE a.IsEnabled = 1
                              AND a.CardStatus<> '5'
                              AND a.ExeWorkOrder = '{exeWorkOrder}'
                        ORDER BY a.CardName ");
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 流转卡报废-流转卡扫描
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic TransferCardScrapScan(string cardCode)
        {
            string sql = $@"SELECT a.ProcessCode,
                               b.ResourceName ProcessName,
                               c.ScrapQty
                        FROM dbo.PM_TransferCardResume a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.ModelLeve = 'Process'
                                   AND a.ProcessCode = b.ResourceCode
                            LEFT JOIN
                            (
                                SELECT c1.CardCode,
                                       c1.ProcessCode,
                                       ISNULL(SUM(c1.Qty), 0) ScrapQty
                                FROM dbo.PM_TranferCardBGRecord c1
                                GROUP BY c1.CardCode,
                                         c1.ProcessCode
                            ) c
                                ON a.CardCode = c.CardCode
                                   AND a.ProcessCode = c.ProcessCode
                        WHERE a.CardCode = '{cardCode}'
                              AND a.Flag = 1
                              AND a.IsEnabled = 1  ";
            return this.BaseRepository().QueryFirst(sql);
        }

        /// <summary>
        /// 创建返工任务-流转卡扫描
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<dynamic> ReworkCardScan(string serialNumber, string processCode)
        {
            string sql = $@"SELECT a.CardCode,
                                   a.CardName,
                                   a.ProductOrder,
                                   a.ContainerNO,
                                   ISNULL(b.BGQty, 0) BGQty,
                                   b.Unit,
                                   c.ProcessCode,
                                   d.ResourceName ProcessName,
                                   c.BusinessType,
                                   v1.ItemName BusinessTypeName,
                                   a.CardStatus,
								   v2.ItemName CardStatusName
                            FROM dbo.PM_TransferCard a
                                LEFT JOIN
                                (
                                    SELECT b1.CardCode,
                                           b1.Unit,
                                           SUM(b1.Qty) BGQty
                                    FROM dbo.PM_TranferCardBGRecord b1
                                    WHERE b1.ProcessCode = '{processCode}'
									AND ISNULL(b1.IsRework,'0')='0'
                                    GROUP BY b1.CardCode,
                                             b1.Unit
                                ) b
                                    ON a.CardCode = b.CardCode
                                LEFT JOIN dbo.PM_TransferCardResume c
                                    ON c.Flag = '1'
                                       AND a.CardCode = c.CardCode
                                LEFT JOIN dbo.BS_ModelWithResource d
                                    ON d.EnabledMark = 1
                                       AND c.ProcessCode = d.ResourceCode
                                LEFT JOIN dbo.V_DataDictionary v1
                                    ON v1.EnCode = 'FlowIdentification'
                                       AND c.BusinessType = v1.ItemValue
								LEFT JOIN dbo.V_DataDictionary v2 ON v2.EnCode='CirculationCardStatus'
								AND a.CardStatus=v2.ItemValue
                            WHERE a.IsEnabled = 1 AND a.CardStatus<>'6'
                                  AND a.SerialNumber = '{serialNumber}'
                            ORDER BY a.CardName ";
            return this.BaseRepository().Query(sql);
        }

        /// <summary>
        /// 生产管理-扫一扫
        /// </summary>
        /// <param name="exeWorkOrder">序列号</param>
        /// <returns></returns>
        public List<dynamic> CodeBarScan(string exeWorkOrder)
        {
            string sql = $@"DECLARE @OperationCode VARCHAR(40);
                            DECLARE @WorkOrder VARCHAR(40);
                            DECLARE @ExeWorkOrder VARCHAR(40) = '{exeWorkOrder}';

                            SELECT @WorkOrder = WorkOrder
                            FROM dbo.PL_ExeWorkOrder
                            WHERE ExeWorkOrder = @ExeWorkOrder;
                            SELECT TOP 1
                                   @OperationCode = b.OperationCode
                            FROM dbo.PL_Process a
                                INNER JOIN dbo.PL_ProcessOfOperations b
                                    ON a.Id = b.ProcessId
                            WHERE a.WorkOrder = @WorkOrder
                                  AND b.OperationCode <> 'FHBZ'
                            ORDER BY b.SN DESC;

                            PRINT @OperationCode;

                            SELECT a.ProductOrder,
                                   a.ContainerNO,
                                   a.MaterialCode,
                                   a.Spec,
                                   a.CardCode,
                                   a.CardName,
                                   a.CardType,
                                   v1.ItemName CardTypeName,
                                   a.CardStatus,
                                   ISNULL(v2.ItemName, '') + CASE
                                                                 WHEN a.CardStatus = '3' THEN
                                                                     CASE d.ReworkStatus
                                                                         WHEN '3' THEN
                                                                             '(已报工)'
                                                                         ELSE
                                                                             '(未报工)'
                                                                     END
                                                                 ELSE
                                                                     ''
                                                             END CardStatusName,
                                   b.ProcessCode,
                                   ISNULL(c.ResourceName, '无 ') ProcessName,
                                   b.BusinessType,
                                   ISNULL(v3.ItemName, '无 ') BusinessTypeName,
                                   a.MMXH,
                                   a.PalletQty,
                                   CASE
                                       WHEN a.CardStatus = '6' THEN
                                           CONVERT(VARCHAR(40), CONVERT(DECIMAL(18,0),ISNULL(e.Qty, 0))) + '/' + CONVERT(VARCHAR(40), CONVERT(DECIMAL(18,0),ISNULL(f.Qty, 0)))
                                       ELSE
                                           CONVERT(VARCHAR(40), b.SheetQty) + '/' + CONVERT(VARCHAR(40), b.PieceQty)
                                   END BGQty
                            FROM dbo.PM_TransferCard a
                                LEFT JOIN dbo.V_DataDictionary v1
                                    ON v1.EnCode = 'CirculationCardType'
                                       AND a.CardType = v1.ItemValue
                                LEFT JOIN dbo.PM_TransferCardResume b
                                    ON b.Flag = '1'
                                       AND a.CardCode = b.CardCode
                                LEFT JOIN dbo.BS_ModelWithResource c
                                    ON c.EnabledMark = 1
                                       AND b.ProcessCode = c.ResourceCode
                                LEFT JOIN dbo.V_DataDictionary v2
                                    ON v2.EnCode = 'CirculationCardStatus'
                                       AND a.CardStatus = v2.ItemValue
                                LEFT JOIN dbo.V_DataDictionary v3
                                    ON v3.EnCode = 'FlowIdentification'
                                       AND b.BusinessType = v3.ItemValue
                                LEFT JOIN dbo.PM_ReworkRecord_Detail d
                                    ON a.SerialNumber = d.ReworkId
                                       AND a.CardCode = d.CardCode
                                LEFT JOIN
                                (
                                    SELECT CardCode,
                                           SUM(Qty) Qty
                                    FROM dbo.PM_PackingBGTransferCard
                                    GROUP BY CardCode
                                ) e
                                    ON a.CardCode = e.CardCode
                                LEFT JOIN
                                (
                                    SELECT CardCode,
                                           ProcessCode,
                                           SUM(Qty) Qty
                                    FROM dbo.PM_TranferCardBGRecord
                                    GROUP BY CardCode,
                                             ProcessCode
                                ) f
                                    ON a.CardCode = f.CardCode
                                       AND f.ProcessCode = @OperationCode
                            WHERE a.IsEnabled = 1
                                  AND a.ExeWorkOrder = @ExeWorkOrder
								  AND ISNULL(a.SerialNumber,'')<>''
                            ORDER BY a.CardName ";
            return this.BaseRepository().Query(sql);
        }

        /// <summary>
        /// 生产管理-扫一扫
        /// </summary>
        /// <param name="exeWorkOrder">工单号</param>
        /// <param name="CardStatus">流转卡状态</param>
        /// <param name="BusinessType">流转卡状态</param>
        /// <returns></returns>
        public dynamic CodeBarScan1(string exeWorkOrder, string CardStatus, string BusinessType)
        {
            string sql = $@" SELECT   count(a.CardCode) as NotBGTS
                            FROM dbo.PM_TransferCard a
                                LEFT JOIN dbo.V_DataDictionary v1
                                    ON v1.EnCode = 'CirculationCardType'
                                       AND a.CardType = v1.ItemValue
                                LEFT JOIN dbo.PM_TransferCardResume b
                                    ON b.Flag = '1'
                                       AND a.CardCode = b.CardCode
                                LEFT JOIN dbo.BS_ModelWithResource c
                                    ON c.EnabledMark = 1
                                       AND b.ProcessCode = c.ResourceCode
                                LEFT JOIN dbo.V_DataDictionary v2
                                    ON v2.EnCode = 'CirculationCardStatus'
                                       AND a.CardStatus = v2.ItemValue
                                LEFT JOIN dbo.V_DataDictionary v3
                                    ON v3.EnCode = 'FlowIdentification'
                                       AND b.BusinessType = v3.ItemValue
                            WHERE a.IsEnabled = 1 and v3.ItemValue='{BusinessType}' and v2.ItemValue='{CardStatus}'and a.ExeWorkOrder='{exeWorkOrder}'  ";
            return this.BaseRepository().QueryFirst(sql);
        }

        public dynamic getNoBGTS(string serialNuber, string processCode)
        {
            string sql = $@"SELECT COUNT(1) NotBGTS FROM dbo.PM_TransferCard a WHERE a.SerialNumber='{serialNuber}' 
                          AND  NOT EXISTS(SELECT 1 FROM dbo.PM_TranferCardBGRecord b WHERE a.CardCode=b.CardCode AND b.ProcessCode='{processCode}')  ";
            return this.BaseRepository().QueryFirst(sql);
        }

        /// <summary>
        /// 生产管理-BOM查询
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        public List<dynamic> BOMQuery(string workOrder)
        {
            string sql = $@"DECLARE @WorkOrder VARCHAR(40)='{workOrder}';

                            SELECT *
                            FROM dbo.PL_ProcessOfOperations
                            WHERE ProcessId =
                            (
                                SELECT TOP 1 Id FROM dbo.PL_Process WHERE WorkOrder = @WorkOrder AND IsDeleted = 0
                            )
                                  AND OperationCode IN
                                      (
                                          SELECT DISTINCT
                                                 ConsumeProcess
                                          FROM dbo.PL_BOMItems
                                          WHERE BOMId =
                                          (
                                              SELECT TOP 1 Id FROM dbo.PL_BOM WHERE WorkOrder = @WorkOrder AND IsDeleted = 0
                                          )
                                      )
									  AND IsDeleted=0
                            ORDER BY SN ";
            return this.BaseRepository().Query(sql);
        }

        #endregion

        #region App 质量检测

        #region App根据流转卡获取信息
        public List<dynamic> GetQCTransferCardEntity(string cardCode)
        {
            var sql = $@" SELECT T.Id,
                               b.FactoryCode,
                               b.FactoryName,
                               T.ProductOrder,
                               T.WorkOrder,
                               T.ExeWorkOrder,
                               T.CardCode,
                               T.CardName,
                               T.ContainerNO,
                               T.Spec,
                               PM.GroupCode,
                               a.GroupName,
                               T.CardStatus
                        FROM dbo.PM_TransferCard T
                            INNER JOIN dbo.PL_WorkOrder b
                                ON T.WorkOrder = b.WorkOrder
                            INNER JOIN dbo.[Base_MaterialGroupBindMaterial] PM
                                ON (PM.MaterialCode = (CASE WHEN b.IsVC=1 THEN t.Spec ELSE t.MaterialCode END))
								AND pm.IsVC=(CASE WHEN b.IsVC=1 THEN '1' ELSE '0' END)
                            INNER JOIN dbo.Base_MaterialGroup a
                                ON PM.GroupCode = a.GroupCode
                            WHERE T.CardCode='{cardCode}'";

            var dy = this.BaseRepository().Query(sql);
            return dy;
        }
        public List<dynamic> GetSemiQCTransferCardEntity(string cardCode)
        {
            var sql = $@" SELECT T.Id,
                               a.FactoryCode,
                               a.WorkOrder ProductOrder,
                               T.WorkOrder,
                               T.TransferBatch ExeWorkOrder,
                               T.TransferCode CardCode,
                               T.TransferName CardName,
                               '' ContainerNO,
                               b.Spec,
                               PM.GroupCode,
							   c.GroupName
                        FROM dbo.PM_OwnProductTransfer T
                            INNER JOIN dbo.PM_OwnProductOrder a
                                ON T.WorkOrder = a.WorkOrder
                            INNER JOIN dbo.[Base_MaterialGroupBindMaterial] PM
                                ON PM.MaterialCode = a.MaterialCode
							INNER JOIN dbo.Base_MaterialGroup c ON pm.GroupCode=c.GroupCode
                            LEFT JOIN dbo.Base_Material b
                                ON a.MaterialCode = b.MaterialCode
                        WHERE T.TransferCode = '{cardCode}'";
            var dy = this.BaseRepository().Query(sql);
            return dy;
        }
        #endregion

        #endregion

    }
}
