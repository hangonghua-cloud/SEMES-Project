using ALP.Application.Busines.Material;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Code.Model;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Service.ProduceManage;
using ALP.Application.Service.Resources;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    [Auth]
    [RoutePrefix("PL_PlanStoreIssue")]
    public class PL_PlanStoreIssueController : ApiBaseController
    {
        private readonly object _lockDismantle = new object();//工单拆解发料

        //基础数据管理
        private BsModelWithResourceBLL _bsModelWithResourceBLL = new BsModelWithResourceBLL();//工厂建模
        private Base_MaterialBLL _baseMaterialBLL = new Base_MaterialBLL();//物料主数据
        private Base_MaterialFacetBLL _baseMaterialFacetBLL = new Base_MaterialFacetBLL();//物料主数据属性
        private Base_KeyParameterItemBLL _keyParameterItemBLL = new Base_KeyParameterItemBLL(); //关键参数
        //计划管理
        private PL_PlanStoreIssueBLL _PlanStoreIssueBLL = new PL_PlanStoreIssueBLL();//工单拆解发料
        private PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();//生产工单
        private PL_ExeWorkOrderBLL _ExeWorkOrderBLL = new PL_ExeWorkOrderBLL();//执行工单
        private PL_ProductionOrderBLL _productOrderBLL = new PL_ProductionOrderBLL();//生产订单
        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();//工单物料
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();//工单物料属性
        private PL_BOMBLL _plBomBLL = new PL_BOMBLL();//工单BOM
        private PL_BOMItemsBLL _plBomItemsBLL = new PL_BOMItemsBLL();//工单BOM明细
        private PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();//工单工艺路线
        private PL_ProcessOfOperationsBLL _plOperationsBLL = new PL_ProcessOfOperationsBLL();//工单工序
        //生产管理
        private PM_TransferCardBLL _transferCardBLL = new PM_TransferCardBLL();//流转卡
        private PMOperationPalletNumService _operationPalletNumService = new PMOperationPalletNumService();//工序物料托盘数量
        private PM_TransferCardResume_Service _resumeService = new PM_TransferCardResume_Service();//流转履历
        private PM_StartUpRecord_Service _startUpService = new PM_StartUpRecord_Service();//生产开工
        //物料管理
        private MM_RawMaterialStockBLL _RawMaterialStockBLL = new MM_RawMaterialStockBLL();//原材料库存
        private MM_RawMaterialOutBLL _RawMaterialOutBLL = new MM_RawMaterialOutBLL();//原材料出库
        private MM_RawMaterialInBLL _RawMaterialInBLL = new MM_RawMaterialInBLL();//原材料入库
        private MM_SuperProductStockBLL _SuperProductStockBLL = new MM_SuperProductStockBLL();//超产品库存

        #region 查询生产工单
        [HttpPost]
        [Route("GetListWithPage")]
        public HttpResponseMessage GetListWithPage(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    pagination = null;
                    //result.success = false;
                    //result.returnMsg = "分页参数Pagination不能为空！";
                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var data = _PlanStoreIssueBLL.GetListWithPage(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 查询执行工单
        [HttpPost]
        [Route("GetListWithPageExeWorkOrder")]
        public HttpResponseMessage GetListWithPageExeWorkOrder(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    pagination = null;
                    //result.success = false;
                    //result.returnMsg = "分页参数Pagination不能为空！";
                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var data = _PlanStoreIssueBLL.GetListWithPageExeWorkOrder(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取拆解工单 超产品 应发数量等 -旧版本
        //[HttpPost]
        //[Route("GetStoreIssueWorkOrderOld")]
        //public HttpResponseMessage GetStoreIssueWorkOrderOld(JObject jo)
        //{
        //    var result = new ResponseResult();
        //    result.resultData = null;
        //    try
        //    {

        //        string queryJson = getValue(jo, "queryJson");
        //        var index = getValue(jo, "index");
        //        var watch = CommonHelper.TimerStart();

        //        var data = _PlanStoreIssueBLL.GetStoreIssueWorkOrderOld(index, queryJson);
        //        result.resultData = data;
        //        result.success = true;
        //        result.returnMsg = Language.GetText("Common.Success");
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.success = false;
        //        result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
        //        result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //}
        #endregion

        #region 获取拆解工单 超产品 应发数量等
        [HttpPost]
        [Route("GetStoreIssueWorkOrder")]
        public HttpResponseMessage GetStoreIssueWorkOrder(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                var list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "data"));
                var watch = CommonHelper.TimerStart();

                var data = _PlanStoreIssueBLL.GetStoreIssueWorkOrder(list);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 拆解工单保存
        [HttpPost]
        [Route("SaveWorkOrderDismantle")]
        public HttpResponseMessage SaveWorkOrderDismantle(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var docNum = time.ToString("yyyyMMddHHmmss");
            var msg = "";
            var returnNum = "";
            try
            {
                lock (_lockDismantle)
                {
                    //工单
                    var list1 = JsonConvert.DeserializeObject<List<PL_WorkOrderModel>>(getValue(jo, "rows1"));
                    //面膜消耗
                    var list2 = JsonConvert.DeserializeObject<List<RawMaterialStockModel>>(getValue(jo, "rows2"));
                    //超产品扣除 todo
                    var list3 = JsonConvert.DeserializeObject<List<MM_SuperProductStockEntity>>(getValue(jo, "rows3"));

                    List<PL_PlanStoreIssueEntity> lstPlanStore = new List<PL_PlanStoreIssueEntity>();
                    List<PL_WorkOrderEntity> lstWorkOrder = new List<PL_WorkOrderEntity>();
                    List<PL_ExeWorkOrderEntity> lstAllExeWorkOrder = new List<PL_ExeWorkOrderEntity>();
                    List<PM_TransferCardEntity> lstAllTranferCard = new List<PM_TransferCardEntity>();

                    //生产拆分流水号
                    _PlanStoreIssueBLL.GetSerialNO("PlanStoreIssue", out returnNum, out msg);
                    var sn = time.ToString("yyMMdd") + returnNum.PadLeft(5, '0');

                    #region 面膜发料
                    if (list2 == null || list2.Count == 0)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_3"));//面膜发料信息不能为空！

                    var mrlist = new List<MM_RawMaterialStockEntity>();//剩余
                    var outList = new List<MM_RawMaterialOutEntity>();
                    var inList = new List<MM_RawMaterialInEntity>();
                    var taginsertList = new List<MM_RawMaterialStockEntity>();
                    var tagupdateList = new List<MM_RawMaterialStockEntity>();

                    var arrMMBatch = list2.Select(t => t.BatchNo).Distinct().ToList();//发料的批次
                    var batchNos = string.Join(",", arrMMBatch);
                    var factoryName = list1.First().FactoryName;
                    foreach (var item in list2)
                    {
                        var associateNo = Guid.NewGuid().ToString();//移库关联

                        #region 剩余库存
                        //1.剩余库存
                        mrlist.Add(new MM_RawMaterialStockEntity()
                        {
                            Id = item.Id,
                            Qty = item.Qty,
                            ModifyBy = userCode,
                            ModifyTime = time,
                        });
                        #endregion

                        #region 出库记录
                        //2.出库记录
                        outList.Add(new MM_RawMaterialOutEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            BusinessTable = "PL_PlanStoreIssue",
                            FactoryCode = item.FactoryCode,
                            FactoryName = factoryName,
                            WorkOrder = item.WorkOrder,
                            ExeWorkOrder = item.ExeWorkOrder,
                            DocNum = docNum,
                            WhsCode = item.OldWhsCode,
                            LocationCode = item.OldLocationCode,
                            MaterialCode = item.MaterialCode,
                            MaterialName = item.MaterialName,
                            SupplierCode = item.SupplierCode,
                            BatchNo = item.BatchNo,
                            OutType = "6",//发料
                            Qty = item.ActNum,
                            Unit = item.Unit,
                            Creator = userCode,
                            CreateTime = time,
                            AssociateNo = associateNo
                        });
                        #endregion

                        #region 入库记录
                        //3.入库记录
                        MM_RawMaterialInEntity inEntity = new MM_RawMaterialInEntity();
                        inEntity.Id = Guid.NewGuid().ToString();
                        inEntity.BusinessTable = "PL_PlanStoreIssue";
                        inEntity.FactoryCode = item.FactoryCode;
                        inEntity.FactoryName = factoryName;
                        inEntity.WorkOrder = item.WorkOrder;
                        inEntity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                        inEntity.MaterialCode = item.MaterialCode;
                        inEntity.MaterialName = item.MaterialName;
                        inEntity.Unit = item.Unit;
                        inEntity.BatchNo = item.BatchNo ?? "";
                        inEntity.Spec = item.Spec;
                        inEntity.SmallClass = (item.SmallClass == "面膜" ? "MM" : item.SmallClass);//面膜
                        inEntity.SupplierCode = item.SupplierCode ?? "";
                        inEntity.Qty = item.ActNum;
                        inEntity.InType = "6";//发料
                        inEntity.WhsCode = item.WhsCode;
                        inEntity.LocationCode = item.LocationCode;//入库库位
                        inEntity.Creator = userCode;
                        inEntity.CreateTime = time;
                        inEntity.AssociateNo = associateNo;//移库关联
                        inList.Add(inEntity);
                        #endregion

                        #region 目标库存
                        //4.目标库存
                        var targetEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                            && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo);
                        if (targetEntity == null) //不存在新增
                        {
                            if (taginsertList.Count > 0 && taginsertList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                                                               && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo) != null)//判断insertList是否存在
                            {
                                taginsertList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode && t.MaterialCode == item.MaterialCode
                                    && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                            }
                            else
                            {
                                var newEntity = new MM_RawMaterialStockEntity();
                                newEntity.Id = Guid.NewGuid().ToString();
                                newEntity.FactoryCode = item.FactoryCode;
                                newEntity.FactoryName = factoryName;
                                newEntity.MaterialCode = item.MaterialCode;
                                newEntity.MaterialName = item.MaterialName;
                                newEntity.Spec = item.Spec;
                                newEntity.SmallClass = item.SmallClass;
                                newEntity.BatchNo = item.BatchNo;
                                newEntity.Unit = item.Unit;
                                newEntity.WhsCode = item.WhsCode;
                                newEntity.LocationCode = item.LocationCode;
                                newEntity.SupplierCode = item.SupplierCode;
                                newEntity.Qty = item.ActNum;
                                newEntity.Creator = userCode;
                                newEntity.CreateTime = time;
                                newEntity.IsFrozen = "0";
                                taginsertList.Add(newEntity);
                            }
                        }
                        else
                        {
                            if (tagupdateList.Count > 0 && tagupdateList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                                  && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo) != null)//判断updatetList是否存在
                            {
                                tagupdateList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode && t.MaterialCode == item.MaterialCode
                                    && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                            }
                            else
                            {
                                targetEntity.Qty += item.ActNum;
                                targetEntity.ModifyBy = userCode;
                                targetEntity.ModifyTime = time;
                                tagupdateList.Add(targetEntity);
                            }
                        }
                        #endregion
                    }
                    #endregion

                    #region 工单拆解
                    foreach (var item in list1)
                    {
                        item.ModifyBy = userCode;
                        item.ModifyTime = time;

                        var workOrder = item.WorkOrder;
                        //工单
                        var workOrderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsEnabled == true);
                        if (workOrderEntity == null)
                            return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_5", workOrder));//工单【{workOrder}】不存在！

                        if (workOrderEntity.OrderStatus == "9")
                            return AjaxResult(false, $"工单【{workOrderEntity.WorkOrder}】已结案，无法继续操作");

                        item.FactoryCode = workOrderEntity.FactoryCode;
                        //订单
                        var productOrderEntity = _productOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == workOrderEntity.ProductOrder);
                        //工单物料主数据
                        var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                        //工单工艺路线
                        var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                        var plOperationList = _plOperationsBLL.Get_ExpressionList(t => t.ProcessId == plProcessEntity.Id && t.IsDeleted == false)
                            .OrderBy(t => t.SN).ToList();
                        if (plOperationList.Count == 0)
                            return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_6"));//工艺路线-工序不能为空！

                        //工单物料属性
                        var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id);
                        var DXZHEntity = plMaterialFacetList.ToList().Find(t => t.AttrCode == "DXZH");//大张转小片转换值
                        var DXZH = DXZHEntity == null ? "1" : DXZHEntity.AttrValue;
                        //var SCTPSLEntity = plMaterialFacetList.ToList().Find(t => t.AttrCode == "SCTPSL");//单拖张数转换值
                        //var SCTPSL = SCTPSLEntity == null ? "1" : SCTPSLEntity.AttrValue;
                        //工序物料托盘数量
                        List<PMOperationPalletNumEntity> pmOperationPalletNumList = new List<PMOperationPalletNumEntity>();
                        if (workOrderEntity.IsVC == true)
                        {
                            var arrProcessCode = plOperationList.Take(plOperationList.Count - 1).Select(t => t.OperationCode);
                            pmOperationPalletNumList = _operationPalletNumService.GetList(t => arrProcessCode.Contains(t.ProcessCode)
                                && t.Spec == plMaterialEntity.Spec && t.DocType == "2").ToList();
                            if (pmOperationPalletNumList.Count != arrProcessCode.Count())
                                return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_7"));//部分工序物料托盘数量未维护！
                        }
                        else
                        {
                            var arrProcessCode = plOperationList.Take(plOperationList.Count - 1).Select(t => t.OperationCode);
                            pmOperationPalletNumList = _operationPalletNumService.GetList(t => arrProcessCode.Contains(t.ProcessCode)
                                && t.MaterialCode == plMaterialEntity.MaterialCode && t.DocType == "1").ToList();
                            if (pmOperationPalletNumList.Count != arrProcessCode.Count())
                                return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_7"));//部分工序物料托盘数量未维护！
                        }

                        //工单BOM
                        var plBomEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrderEntity.WorkOrder && t.IsDeleted == false);//一工单一BOM
                                                                                                                                                //工单BOMItems
                        var plBomItemsList = _plBomItemsBLL.Get_ExpressionList(t => t.BOMId == plBomEntity.Id);

                        #region 更新工单发料信息
                        if (item.WorkOrderType != "5")
                        {
                            lstPlanStore.Add(new PL_PlanStoreIssueEntity()
                            {
                                Id = item.Id,
                                FactoryCode = item.FactoryCode,
                                FactoryName = item.FactoryName,
                                OrderOrProduct = item.OrderOrProduct,
                                ShouldNum = item.TheoryShouldNum,
                                StoreIssueNo = sn,
                                WearLayerStatus = item.WearLayerStatus,
                                UnProductNum = item.UnProductNum,
                                MaskStatus = "2",
                                Remark = item.Remark,
                                WhsCode = item.WhsCode,
                                LocationCode = item.LocationCode,
                                ModifyBy = item.ModifyBy,
                                ModifyTime = item.ModifyTime
                            });

                            #region 更新工单信息
                            var ent = _WorkOrderBLL.Get_ExpressionEntity(r => r.WorkOrder == workOrder);
                            if (ent.OrderStatus == "4")
                            {
                                return AjaxResult(false, $"工单已发料：" + ent.WorkOrder);
                            }
                            ent.OrderStatus = "4";//已发料
                            ent.ModifyBy = item.ModifyBy;
                            ent.ModifyTime = item.ModifyTime;
                            lstWorkOrder.Add(ent);
                            if (ent.BatchStatus == 1) //合批工单同步更新原工单状态
                            {
                                var oldWorkOrderList = _WorkOrderBLL.Get_ExpressionList(t => t.BatchWorkOrder == ent.WorkOrder).ToList();
                                foreach (var item2 in oldWorkOrderList)
                                {
                                    item2.OrderStatus = "4";//已发料
                                    item2.ModifyBy = CurrentAccount.UserCode;
                                    item2.ModifyTime = DateTime.Now;
                                }
                                lstWorkOrder.AddRange(oldWorkOrderList);
                            }
                            #endregion
                        }

                        #endregion

                        #region 执行工单、流转卡
                        List<PL_ExeWorkOrderEntity> lstExeWorkOrder = new List<PL_ExeWorkOrderEntity>();
                        List<PM_TransferCardEntity> lstTranferCard = new List<PM_TransferCardEntity>();

                        #region 执行工单
                        var index = 0;
                        //已生成的执行工单
                        var exeWorkOrderlist = _ExeWorkOrderBLL.Get_ExpressionList(m => m.WorkOrder == workOrder).ToList();
                        index = exeWorkOrderlist.Count + 1;
                        //即将生成的执行工单
                        if (item.WorkOrderType != "5")
                        {
                            var pSheetQty = item.PSheetsQty;
                            if (exeWorkOrderlist.Where(t => t.OrderType == "1" && t.IsEnabled == true).ToList().Count > 0)
                            {
                                pSheetQty = workOrderEntity.ActualSheets - exeWorkOrderlist.Where(t => t.OrderType == "1" && t.IsEnabled == true).Sum(t => t.PSheetsQty);
                            }
                            lstExeWorkOrder.Add(new PL_ExeWorkOrderEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                FactoryCode = item.FactoryCode,
                                FactoryName = item.FactoryName,
                                WorkOrder = workOrder,
                                ExeWorkOrder = workOrder + "-Z" + (index.ToString().PadLeft(2, '0')),
                                Status = "1",
                                AssignStatus = "1",
                                OrderType = item.WorkOrderType,
                                SheetsQty = item.ActNum,
                                PSheetsQty = pSheetQty,
                                PiecesQty = item.PSheetsQty.Value * DXZH.ToDecimal(),
                                ActualNum = item.ActualNum,
                                SuperNum = item.SuperNum,
                                ShouldNum = item.ShouldNum,
                                ConsumeNum = item.ConsumeNum,
                                Yield = item.Yield,
                                Process = item.Process,
                                StartOperation = item.StartOperation,
                                IsEnabled = true,
                                TransferBy = item.TransferBy,
                                OrderPiecesAll = workOrderEntity.OrderPiecesAll,
                                OrderPiecesNum = item.PSheetsQty.Value * DXZH.ToDecimal() / workOrderEntity.OrderPiecesAll >= 1 ? 1 : item.PSheetsQty.Value * DXZH.ToDecimal() / workOrderEntity.OrderPiecesAll,
                                Creator = item.ModifyBy,
                                CreateTime = item.ModifyTime,
                                SendOutBatch = batchNos
                            });
                        }

                        //超产品单
                        if (item.WorkOrderType == "5")
                        {
                            index++;
                            lstExeWorkOrder.Add(new PL_ExeWorkOrderEntity()
                            {
                                Id = Guid.NewGuid().ToString(),
                                FactoryCode = item.FactoryCode,
                                FactoryName = item.FactoryName,
                                WorkOrder = workOrder,
                                ExeWorkOrder = workOrder + "-C" + (index.ToString().PadLeft(2, '0')),
                                Status = "1",
                                AssignStatus = "0",//超产品不需要派工
                                SheetsQty = item.ActNum,
                                PSheetsQty = item.PSheetsQty,
                                PiecesQty = item.PieceQty, //超产品片数
                                OrderType = item.WorkOrderType,//超产品单
                                Yield = item.Yield,
                                Process = item.Process,
                                StartOperation = item.StartOperation,
                                TransferBy = item.TransferBy,
                                IsEnabled = true,
                                OrderPiecesAll = workOrderEntity.OrderPiecesAll,
                                OrderPiecesNum = item.PSheetsQty.Value * DXZH.ToDecimal() / workOrderEntity.OrderPiecesAll >= 1 ? 1 : item.PSheetsQty.Value * DXZH.ToDecimal() / workOrderEntity.OrderPiecesAll,
                                Creator = item.ModifyBy,
                                CreateTime = item.ModifyTime,
                                BatchNo = item.BatchNo,
                                SupId = item.SupId
                            });
                        }
                        lstAllExeWorkOrder.AddRange(lstExeWorkOrder);
                        #endregion

                        #region 流转卡
                        List<string> lstExeWorkOrderType = exeWorkOrderlist.Where(t => t.IsEnabled == true).Select(t => t.OrderType).ToList();
                        List<string> lstExeWorkOrderType2 = lstExeWorkOrder.Select(t => t.OrderType).ToList();
                        lstExeWorkOrderType.AddRange(lstExeWorkOrderType2);
                        lstExeWorkOrderType = lstExeWorkOrderType.Distinct().ToList();

                        foreach (var e in lstExeWorkOrder)
                        {
                            var serialNumber = Guid.NewGuid().ToString();
                            //int palletCount = (int)Math.Ceiling(e.PSheetsQty.Value / SCTPSL.ToDecimal());
                            //托数计算逻辑二期更改
                            decimal palletQty = 0;
                            string unitName = "";
                            string splitOperation = "";//拆托工序
                            int palletCount = GetPalletCount(plOperationList, pmOperationPalletNumList, e.PSheetsQty.Value, DXZH.ToInt(), out palletQty, out unitName, out splitOperation);
                            //获取首工序托数
                            var pmOperationPalletNumEntity = pmOperationPalletNumList.Find(t => t.ProcessCode == e.StartOperation);
                            if (pmOperationPalletNumEntity == null)
                                return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_16"));//起始工序没有找到工序物料托盘数量

                            int startOperationPalletCount = GetStartOperationPalletCount(e.PSheetsQty.Value, pmOperationPalletNumEntity, DXZH.ToInt());
                            for (int i = 1; i <= palletCount; i++)
                            {
                                PM_TransferCardEntity cardEntity = new PM_TransferCardEntity();
                                cardEntity.Create();
                                //cardEntity.FactoryCode = e.FactoryCode;
                                cardEntity.CreateTime = DateTime.Now;
                                cardEntity.Creator = CurrentAccount.UserCode;
                                cardEntity.ProductOrder = workOrderEntity.ProductOrder;
                                cardEntity.WorkOrder = workOrder;
                                cardEntity.WorkOrderType = workOrderEntity.WorkOrderType;
                                cardEntity.ExeWorkOrder = e.ExeWorkOrder;
                                cardEntity.TransferBy = e.TransferBy;
                                cardEntity.CardCode = e.ExeWorkOrder + "-" + workOrderEntity.ContainerNO + "C" + palletCount.ToString() + "-" + i.ToString().PadLeft(2, '0');
                                cardEntity.CardName = palletCount.ToString() + "-" + i.ToString().PadLeft(2, '0');
                                cardEntity.CardType = this.GetTransferCardType(lstExeWorkOrderType, e.OrderType);
                                cardEntity.ContainerNO = workOrderEntity.ContainerNO;
                                cardEntity.BJGY = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BJGY")?.AttrValue;
                                cardEntity.WorkOrderRemark = workOrderEntity.Remark;
                                cardEntity.MaterialCode = workOrderEntity.MaterialCode;
                                cardEntity.MaterialName = workOrderEntity.IsVC == true ? workOrderEntity.MMXH : plMaterialEntity.MaterialName;
                                cardEntity.Spec = workOrderEntity.IsVC == true ? workOrderEntity.Spec : plMaterialEntity.Spec;
                                cardEntity.MMXH = workOrderEntity.IsVC == true ? workOrderEntity.MMXH : plMaterialEntity.MaterialName;
                                cardEntity.JCGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "JCGG")?.AttrValue;
                                cardEntity.BWXH = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BWXH")?.AttrValue;
                                cardEntity.DXZH = DXZH.ToDecimalOrNull();
                                cardEntity.SCTPSL = pmOperationPalletNumList.Find(t => t.ProcessCode == e.StartOperation)?.PalletNum;
                                cardEntity.SCTPSLP = cardEntity.SCTPSL * DXZH.ToDecimal();
                                cardEntity.BZTPSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZSCTPSL")?.AttrValue.ToDecimalOrNull();
                                //cardEntity.KCKX = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "KCKX")?.AttrValue;
                                var kckx = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "KCKX")?.AttrValue;
                                cardEntity.KCKX = workOrderEntity.IsVC == true ? workOrderEntity.KCKX : kckx;
                                var uv = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "UV")?.AttrValue;
                                cardEntity.UV = workOrderEntity.IsVC == true ? workOrderEntity.UV : uv;
                                cardEntity.DJ = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "DJ")?.AttrValue;
                                cardEntity.TotalSheets = e.SheetsQty;//执行工单张数
                                cardEntity.ActualSheets = e.PSheetsQty;//执行工单放量张数
                                cardEntity.OrderPieces = workOrderEntity.OrderPieces;
                                cardEntity.ProductPieces = workOrderEntity.OrderPieces * workOrderEntity.Yield;
                                cardEntity.TPGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "TPGG")?.AttrValue;
                                cardEntity.OrderPallet = workOrderEntity.OrderPallet;
                                cardEntity.PerPallerBox = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZTPSL")?.AttrValue.ToDecimalOrNull();
                                cardEntity.BZDHSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimalOrNull();
                                cardEntity.Description = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "SMS")?.AttrValue;
                                if (workOrderEntity.OrderBox != null && workOrderEntity.OrderPallet != null)
                                {
                                    cardEntity.PalletNum = workOrderEntity.OrderStartPallet.Value.ToString() + "-" + workOrderEntity.OrderPallet.Value.ToString();
                                }
                                cardEntity.PaperBoxModel = plBomItemsList.FirstOrDefault(t => t.SmallClass == "BC")?.Spec;
                                cardEntity.BoxDate = productOrderEntity?.BoxDate != null ? productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A" : "";
                                cardEntity.StartProcess = e.StartOperation;
                                cardEntity.StartOperationPalletCount = startOperationPalletCount;//首工序托数
                                cardEntity.CardStatus = "1";
                                cardEntity.IsEnabled = true;
                                //拆托工序
                                if (splitOperation != cardEntity.StartProcess)
                                    cardEntity.SplitProcess = splitOperation;

                                //托盘张数、片数  首工序托数最后一托
                                if (i == startOperationPalletCount)
                                {
                                    //if (unitName == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_8"))//张
                                    //{
                                    cardEntity.PalletQty = e.PSheetsQty.Value - ((startOperationPalletCount - 1) * pmOperationPalletNumEntity.PalletNum.Value);
                                    cardEntity.PieceQty = cardEntity.PalletQty * DXZH.ToDecimal();
                                    //}
                                    //else
                                    //{
                                    //    cardEntity.PieceQty = e.PSheetsQty.Value * DXZH.ToDecimal() - ((startOperationPalletCount - 1) * pmOperationPalletNumEntity.PalletNum.Value);
                                    //    cardEntity.PalletQty = Math.Ceiling(cardEntity.PieceQty.Value / DXZH.ToDecimal());
                                    //}
                                }
                                else
                                {
                                    cardEntity.PalletQty = cardEntity.SCTPSL;
                                    cardEntity.PieceQty = cardEntity.SCTPSLP;
                                }

                                //流水号
                                if (cardEntity.TransferBy == "1")
                                {
                                    if (i <= startOperationPalletCount)
                                    {
                                        cardEntity.SerialNumber = serialNumber;
                                        cardEntity.PrintStatus = "3"; //可打印
                                    }
                                    else
                                        cardEntity.PrintStatus = "1"; //未打印
                                }
                                else
                                    cardEntity.SerialNumber = Guid.NewGuid().ToString();
                                //拆托 标准托盘数量显示
                                if (string.IsNullOrEmpty(cardEntity.SplitProcess))
                                    cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + cardEntity.PieceQty.Value.ToString();
                                else
                                {
                                    var splitOperationNumEntity = pmOperationPalletNumList.Find(t => t.ProcessCode == splitOperation);
                                    if (i == startOperationPalletCount)
                                    {
                                        //if (splitOperationNumEntity.UnitName == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_9"))//张
                                        //{
                                        var remainQty = e.PSheetsQty % splitOperationNumEntity.PalletNum.Value;
                                        cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + remainQty.ToString() + "/" + (remainQty * cardEntity.DXZH).ToString();
                                        //}
                                        //else
                                        //{
                                        //    var remainQty = e.PSheetsQty % Math.Ceiling(splitOperationNumEntity.PalletNum.Value / cardEntity.DXZH.Value);
                                        //    cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + remainQty.ToString() + "/" + (remainQty * cardEntity.DXZH).ToString();
                                        //}
                                    }
                                    else
                                    {
                                        //if (splitOperationNumEntity.UnitName == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_9"))//张
                                        cardEntity.ShowPalletQty = pmOperationPalletNumEntity.PalletNum.Value.ToString() + "/" + splitOperationNumEntity.PalletNum.Value.ToString() + "/" + (splitOperationNumEntity.PalletNum.Value * cardEntity.DXZH).ToString();
                                        //else
                                        //    cardEntity.ShowPalletQty = pmOperationPalletNumEntity.PalletNum.Value.ToString() + "/" + (Math.Ceiling(splitOperationNumEntity.PalletNum.Value / cardEntity.DXZH.Value)).ToString() + "/" + splitOperationNumEntity.PalletNum.Value.ToString();
                                    }
                                }

                                lstTranferCard.Add(cardEntity);
                            }
                        };
                        lstAllTranferCard.AddRange(lstTranferCard);
                        #endregion

                        #endregion
                    };
                    #endregion

                    //执行工单挂上本次发的面膜批次
                    var arrExeWorkOrder = lstAllExeWorkOrder.Where(t => t.OrderType == "1").Select(t => t.ExeWorkOrder).Distinct().ToList();
                    outList.ForEach(item =>
                    {
                        item.ExeWorkOrder = string.Join(",", arrExeWorkOrder);
                    });

                    #region 同步SAP
                    var factoryCode = outList.First().FactoryCode;

                    var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                         && t.Remark1 == factoryCode);
                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        //获取物料信息
                        var arrMaterialCode = outList.Select(t => t.MaterialCode).Distinct().ToArray();
                        var materialList = _baseMaterialBLL.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                        IF102 sapRequest = new IF102();
                        sapRequest.HEAD = new SapHeadDto();
                        sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                        sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                        IF102_HEAD IF102_HEAD = new IF102_HEAD();
                        IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                        IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                        sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                        List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                        foreach (var item in outList)
                        {
                            var rawInEntity = inList.Find(t => t.AssociateNo == item.AssociateNo);

                            IF102_ITEM IF102_ITEM = new IF102_ITEM();
                            IF102_ITEM.BWART = "311";
                            IF102_ITEM.MATNR = materialList.Find(t => t.MaterialCode == item.MaterialCode)?.SAPMaterialCode ?? "";
                            IF102_ITEM.WERKS = item.FactoryCode ?? "";
                            IF102_ITEM.LGORT = item.LocationCode ?? "";
                            IF102_ITEM.CHARG = item.BatchNo ?? "";
                            IF102_ITEM.ERFMG = item.Qty.ToString() ?? "";
                            IF102_ITEM.ERFME = item.Unit ?? "";
                            IF102_ITEM.UMWRK = rawInEntity?.FactoryCode ?? "";
                            IF102_ITEM.UMLGO = rawInEntity?.LocationCode ?? "";
                            IF102_ITEM.UMCHA = rawInEntity?.BatchNo ?? "";
                            IT_ITEM.Add(IF102_ITEM);
                        }
                        sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                        var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);

                        foreach (var item in outList)
                        {
                            item.IsPosted = sapResult.Flag ? "1" : ""; ;
                            item.PostedMsg = sapResult.Msg;
                            item.PostedTime = DateTime.Now;
                            item.PostedUser = "SAP";
                            item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                        }
                        foreach (var item in inList)
                        {
                            item.IsPosted = sapResult.Flag ? "1" : ""; ;
                            item.PostedMsg = sapResult.Msg;
                            item.PostedTime = DateTime.Now;
                            item.PostedUser = "SAP";
                            item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                        }
                    }
                    #endregion

                    #region 执行事务
                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    // 设置事务超时时间为60秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 60);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _PlanStoreIssueBLL.SaveEntity_List(true, userCode, lstPlanStore, out msg);
                        _WorkOrderBLL.SaveEntity_List(true, userCode, lstWorkOrder, out msg);
                        _ExeWorkOrderBLL.InsertList(lstAllExeWorkOrder);
                        _transferCardBLL.InsertList(lstAllTranferCard);
                        _RawMaterialStockBLL.SaveEntity_List(true, userCode, mrlist, out msg);
                        if (taginsertList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(false, userCode, taginsertList, out msg);
                        if (tagupdateList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(true, userCode, tagupdateList, out msg);
                        _RawMaterialOutBLL.SaveEntity_List(false, userCode, outList, out msg);
                        _RawMaterialInBLL.SaveEntity_List(false, userCode, inList, out msg);
                        if (list3.Count > 0) _SuperProductStockBLL.SaveEntity_List(true, userCode, list3, out msg);

                        ts.Complete();
                    }
                    #endregion
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 生产补料保存
        [HttpPost]
        [Route("ProductReissueSave")]
        public HttpResponseMessage ProductReissueSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                var exeWorkOrderEntity = JsonConvert.DeserializeObject<PL_ExeWorkOrderEntity>(getValue(jo, "exeWorkOrder"));
                var materialItem = JsonConvert.DeserializeObject<List<RawMaterialStockModel>>(getValue(jo, "materialItem"));

                #region 1、执行工单
                exeWorkOrderEntity.ModifyBy = userCode;
                exeWorkOrderEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 2、面膜发料
                var time = DateTime.Now;
                var docNum = time.ToString("yyyyMMddHHmmss");
                var mrlist = new List<MM_RawMaterialStockEntity>();//剩余
                var outList = new List<MM_RawMaterialOutEntity>();
                var inList = new List<MM_RawMaterialInEntity>();
                var taginsertList = new List<MM_RawMaterialStockEntity>();
                var tagupdateList = new List<MM_RawMaterialStockEntity>();
                foreach (var item in materialItem)
                {
                    var associateNo = Guid.NewGuid().ToString();//移库关联

                    //1.剩余库存
                    mrlist.Add(new MM_RawMaterialStockEntity()
                    {
                        Id = item.Id,
                        Qty = item.Qty,
                        ModifyBy = userCode,
                        ModifyTime = time,
                    });
                    //2.出库记录
                    outList.Add(new MM_RawMaterialOutEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FactoryCode = item.FactoryCode,
                        FactoryName = item.FactoryName,
                        ProductOrder = exeWorkOrderEntity.ProductOrder,
                        ContainerNO = exeWorkOrderEntity.ContainerNO,
                        WorkOrder = exeWorkOrderEntity.WorkOrder,
                        ExeWorkOrder = exeWorkOrderEntity.ExeWorkOrder,
                        DocNum = docNum,
                        WhsCode = item.OldWhsCode,
                        LocationCode = item.OldLocationCode,
                        MaterialCode = item.MaterialCode,
                        MaterialName = item.MaterialName,
                        SupplierCode = item.SupplierCode,
                        BatchNo = item.BatchNo,
                        OutType = "6",//发料
                        Qty = item.ActNum,
                        Unit = item.Unit,
                        Creator = userCode,
                        CreateTime = time,
                        AssociateNo = associateNo
                    });
                    //3.入库记录
                    MM_RawMaterialInEntity inEntity = new MM_RawMaterialInEntity();
                    inEntity.Id = Guid.NewGuid().ToString();
                    inEntity.FactoryCode = item.FactoryCode;
                    inEntity.FactoryName = item.FactoryName;
                    inEntity.ProductOrder = exeWorkOrderEntity.ProductOrder;
                    inEntity.WorkOrder = exeWorkOrderEntity.WorkOrder;
                    inEntity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                    inEntity.MaterialCode = item.MaterialCode;
                    inEntity.MaterialName = item.MaterialName;
                    inEntity.Unit = item.Unit;
                    inEntity.BatchNo = item.BatchNo;
                    inEntity.Spec = item.Spec;
                    inEntity.SmallClass = (item.SmallClass == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_4") ? "MM" : item.SmallClass);//面膜
                    inEntity.SupplierCode = item.SupplierCode;
                    inEntity.Qty = item.ActNum;
                    inEntity.InType = "6";//发料
                    inEntity.WhsCode = item.WhsCode;
                    inEntity.LocationCode = item.LocationCode;//入库库位
                    inEntity.Creator = userCode;
                    inEntity.CreateTime = time;
                    inEntity.AssociateNo = associateNo;//移库关联
                    inList.Add(inEntity);
                    //4.目标库存
                    var targetEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t =>
                       t.WhsCode == item.WhsCode
                       && t.LocationCode == item.LocationCode
                       && t.MaterialCode == item.MaterialCode
                       && t.BatchNo == item.BatchNo);
                    if (targetEntity == null) //不存在新增
                    {
                        if (taginsertList.Count > 0 && taginsertList.Find(t =>
                          t.WhsCode == item.WhsCode
                          && t.LocationCode == item.LocationCode
                          && t.MaterialCode == item.MaterialCode
                          && t.BatchNo == item.BatchNo) != null)//判断insertList是否存在
                        {
                            taginsertList.Find(t =>
                       t.WhsCode == item.WhsCode
                       && t.LocationCode == item.LocationCode
                       && t.MaterialCode == item.MaterialCode
                       && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                        }
                        else
                        {
                            var newEntity = new MM_RawMaterialStockEntity();
                            newEntity.Id = Guid.NewGuid().ToString();
                            newEntity.FactoryCode = item.FactoryCode;
                            newEntity.FactoryName = item.FactoryName;
                            newEntity.MaterialCode = item.MaterialCode;
                            newEntity.MaterialName = item.MaterialName;
                            newEntity.Spec = item.Spec;
                            newEntity.SmallClass = item.SmallClass;
                            newEntity.BatchNo = item.BatchNo;
                            newEntity.Unit = item.Unit;
                            newEntity.WhsCode = item.WhsCode;
                            newEntity.LocationCode = item.LocationCode;
                            newEntity.SupplierCode = item.SupplierCode;
                            newEntity.Qty = item.ActNum;
                            newEntity.Creator = userCode;
                            newEntity.CreateTime = time;
                            newEntity.IsFrozen = "0";
                            taginsertList.Add(newEntity);
                        }
                    }
                    else
                    {
                        if (tagupdateList.Count > 0 && tagupdateList.Find(t =>
                              t.WhsCode == item.WhsCode
                              && t.LocationCode == item.LocationCode
                              && t.MaterialCode == item.MaterialCode
                              && t.BatchNo == item.BatchNo) != null)//判断updatetList是否存在
                        {
                            tagupdateList.Find(t =>
                           t.WhsCode == item.WhsCode
                           && t.LocationCode == item.LocationCode
                           && t.MaterialCode == item.MaterialCode
                           && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                        }
                        else
                        {
                            targetEntity.Qty += item.ActNum;
                            targetEntity.ModifyBy = userCode;
                            targetEntity.ModifyTime = time;
                            tagupdateList.Add(targetEntity);
                        }
                    }
                }
                #endregion

                #region 同步SAP
                var factoryCode = outList.First().FactoryCode;

                var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    //获取物料信息
                    var arrMaterialCode = outList.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _baseMaterialBLL.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    foreach (var item in outList)
                    {
                        var rawInEntity = inList.Find(t => t.AssociateNo == item.AssociateNo);

                        IF102_ITEM IF102_ITEM = new IF102_ITEM();
                        IF102_ITEM.BWART = "311";
                        IF102_ITEM.MATNR = materialList.Find(t => t.MaterialCode == item.MaterialCode)?.SAPMaterialCode ?? "";
                        IF102_ITEM.WERKS = item.FactoryCode ?? "";
                        IF102_ITEM.LGORT = item.LocationCode ?? "";
                        IF102_ITEM.CHARG = item.BatchNo ?? "";
                        IF102_ITEM.ERFMG = item.Qty.ToString() ?? "";
                        IF102_ITEM.ERFME = item.Unit ?? "";
                        IF102_ITEM.UMWRK = rawInEntity?.FactoryCode ?? "";
                        IF102_ITEM.UMLGO = rawInEntity?.LocationCode ?? "";
                        IF102_ITEM.UMCHA = rawInEntity?.BatchNo ?? "";
                        IT_ITEM.Add(IF102_ITEM);
                    }
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    foreach (var item in outList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }
                    foreach (var item in inList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }
                }
                #endregion

                #region 3、执行事务
                string msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _ExeWorkOrderBLL.SaveEntity(exeWorkOrderEntity.Id, exeWorkOrderEntity, out msg);
                    _RawMaterialStockBLL.SaveEntity_List(true, userCode, mrlist, out msg);
                    if (taginsertList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(false, userCode, taginsertList, out msg);
                    if (tagupdateList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(true, userCode, tagupdateList, out msg);
                    _RawMaterialOutBLL.SaveEntity_List(false, userCode, outList, out msg);
                    _RawMaterialInBLL.SaveEntity_List(false, userCode, inList, out msg);

                    ts.Complete();
                }
                #endregion

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 生产超产品 -旧
        [HttpPost]
        [Route("SaveSuperExWorkOrder")]
        public HttpResponseMessage SaveSuperExWorkOrder(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            var index = 1;
            try
            {
                var workOrder = getValue(jo, "WorkOrder");
                var keyValue = getValue(jo, "KeyValue");
                var unProductNum = getValue(jo, "UnProductNum");
                var list = JsonConvert.DeserializeObject<List<PL_ExeWorkOrderEntity>>(getValue(jo, "data"));
                var exlist = _ExeWorkOrderBLL.Get_ExpressionList(t => t.WorkOrder == workOrder).ToList();
                index = exlist.Count() + 1;
                foreach (var item in list)
                {
                    item.Create();
                    item.Creator = userCode;
                    item.Status = "1";
                    item.ExeWorkOrder = workOrder + "-C" + (index.ToString().PadLeft(2, '0'));
                    item.OrderType = "5";//超产品单
                    item.CreateTime = time;
                }
                _PlanStoreIssueBLL.SaveForm(keyValue, new PL_PlanStoreIssueEntity()
                {
                    Id = keyValue,
                    UnProductNum = unProductNum.ToDecimal()
                });
                _ExeWorkOrderBLL.SaveEntity_List(false, userCode, list, out msg);
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 超产品重置-旧
        [HttpPost]
        [Route("ResetSuperExWorkOrder")]
        public HttpResponseMessage ResetSuperExWorkOrder(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            try
            {

                var entity = JsonConvert.DeserializeObject<PL_PlanStoreIssueEntity>(getValue(jo, "Entity"));
                //var data = JsonConvert.DeserializeObject<PL_PlanStoreIssueEntity>(getValue(jo, "materialData"));
                var workOrder = entity.WorkOrder;
                var exList = _ExeWorkOrderBLL.Get_ExpressionList(t => t.WorkOrder == workOrder && t.OrderType == "5").ToList();
                var num = exList.Sum(t => t.SheetsQty);
                if (num > 0M)
                {
                    var ent = _PlanStoreIssueBLL.GetEntity(t => t.Id == entity.Id);
                    ent.UnProductNum = ent.UnProductNum + num;
                    _PlanStoreIssueBLL.SaveForm(ent.Id, ent);
                }
                //删除超产品
                _ExeWorkOrderBLL.RemoveForm(t => t.WorkOrder == workOrder && t.OrderType == "5");
                //需要给物料回填数据 todo 

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 超发转耗用
        [HttpPost]
        [Route("Save_SuperToConsume")]
        public HttpResponseMessage Save_SuperToConsume(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var workOrder = getValue(jo, "WorkOrder");
            var workOrderType = getValue(jo, "WorkOrderType");
            var consumeNum = getValue(jo, "ConsumeNum");
            var msg = "";
            try
            {
                var ent = _ExeWorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder
                  && t.IsEnabled == true
                  && t.OrderType == workOrderType
                  && t.SuperNum > 0);
                ent.ModifyBy = userCode;
                ent.ModifyTime = DateTime.Now;
                ent.SuperNum = 0;
                ent.ConsumeNum = decimal.Parse(consumeNum);
                _ExeWorkOrderBLL.SaveEntity(ent.Id, ent, out msg);
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        #endregion

        #region 获取退库批次
        [HttpPost]
        [Route("GetTuiKuBatchNo")]
        public HttpResponseMessage GetTuiKuBatchNo(JObject jo)
        {
            try
            {
                var materialCode = getValue(jo, "materialCode");//物料编码
                var factoryCode = getValue(jo, "factoryCode");//工厂编码
                var locationCode = getValue(jo, "locationCode");//库位编码

                var data = _RawMaterialStockBLL.Get_ExpressionList(t => t.MaterialCode == materialCode && t.FactoryCode == factoryCode
                      && t.LocationCode == locationCode && t.Qty > 0).ToList();

                return AjaxResult(true, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_10"), data);//操作成功！
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion


        #region 退库保存
        [HttpPost]
        [Route("Save_CancellingStocks")]
        public HttpResponseMessage Save_CancellingStocks(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            var userCode = CurrentAccount.UserCode;
            var msg = "";
            List<PM_TransferCardResumeEntity> lstResume = new List<PM_TransferCardResumeEntity>();
            try
            {
                /** 
             * 1.工单已经执行，只允许退超发数量
             * 2.工单未开始,必须先退超发数量,然后退实发
             * 3.工单实发数量退到0,执行工单标记为删除,
             * 4.流转履历失效
             * 
             * */
                var model = JsonConvert.DeserializeObject<RawMaterialStockModel>(getValue(jo, "Entity"));

                //1.发料数量
                var workOrder = model.WorkOrder;
                var plEntity = _PlanStoreIssueBLL.GetEntity(t => t.WorkOrder == workOrder);
                var exEntity = _ExeWorkOrderBLL.Get_ExpressionEntity(t => t.Id == model.Id);

                var workorderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);//工单表
                if (workorderEntity.WorkOrderType == "3")
                    return AjaxResult(false, "捡余单不可以退库");

                if (exEntity.CancellingNum == null) exEntity.CancellingNum = 0;
                exEntity.CancellingNum += model.Qty;//退库数量
                exEntity.ConsumeNum = exEntity.ActualNum - exEntity.CancellingNum;
                exEntity.SuperNum = exEntity.ConsumeNum - exEntity.ShouldNum;

                //开工不允许全退
                var startAny = _startUpService.GetList(t => t.ExeWorkOrder == exEntity.ExeWorkOrder).Any();
                if (startAny)
                {
                    if (exEntity.CancellingNum >= exEntity.ActualNum)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_15")); //退库数量需小于发料数量！
                }


                if (exEntity.SuperNum <= 0 || exEntity.SuperNum == null) exEntity.SuperNum = 0;
                exEntity.ModifyBy = userCode;
                exEntity.ModifyTime = time;

                if (exEntity.ActualNum == exEntity.CancellingNum) //退库米数等于实发米数
                {
                    if (workorderEntity.OrderStatus == "4")//已发料
                    {
                        //是否存在其他正常有效的执行工单
                        var exeWorkOrderList = _ExeWorkOrderBLL.Get_ExpressionList(t => t.WorkOrder == workOrder
                            && t.ExeWorkOrder != exEntity.ExeWorkOrder && t.IsEnabled == true);
                        if (exeWorkOrderList.Count() == 0)
                            workorderEntity.OrderStatus = "3";//已发布
                    }

                    exEntity.IsEnabled = false;
                    plEntity.UnProductNum += exEntity.SheetsQty;
                    plEntity.ModifyBy = userCode;
                    plEntity.ModifyTime = time;
                    if ((plEntity.OrderOrProduct == "1" && plEntity.ProductNum <= plEntity.UnProductNum)
                        || (plEntity.OrderOrProduct == "2" && plEntity.OrderNum <= plEntity.UnProductNum))
                    {
                        plEntity.MaskStatus = "1";
                        plEntity.UnProductNum = plEntity.OrderOrProduct == "1" ? plEntity.ProductNum : plEntity.OrderNum;
                    }

                    //流转履历失效
                    var cardList = _transferCardBLL.Get_ExpressionList(t => t.ExeWorkOrder == exEntity.ExeWorkOrder).ToList();
                    var arrCardCode = cardList.Select(t => t.CardCode).ToArray();
                    lstResume = _resumeService.Get_ExpressionList(t => arrCardCode.Contains(t.CardCode) && t.Flag == "1").ToList();
                    foreach (var item in lstResume)
                    {
                        item.Flag = "0";
                        item.ModifyBy = userCode;
                        item.ModifyTime = DateTime.Now;
                    }
                }

                var materialEntity = _baseMaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == model.MaterialCode);
                string associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                //2.0 出库记录
                var outEntity = new MM_RawMaterialOutEntity()
                {
                    FactoryCode = model.FactoryCode,
                    FactoryName = model.FactoryName,
                    WorkOrder = workOrder,
                    DocNum = time.ToString("yyyyMMddHHmmss"),
                    MaterialCode = model.MaterialCode,
                    MaterialName = model.MaterialName,
                    SupplierCode = model.SupplierCode,
                    SmallClass = materialEntity?.SmallClass,
                    OutType = "8",//退库
                    WhsCode = model.OldWhsCode,
                    BatchNo = model.BatchNo,
                    Qty = model.Qty,
                    Unit = model.Unit,
                    UnitName = model.UnitName,
                    LocationCode = model.OldLocationCode,
                    Creator = userCode,
                    CreateTime = time,
                    AssociateNo = associateNo,
                    BusinessId = exEntity.Id,
                    BusinessTable = "PL_ExeWorkOrder"
                };

                //2.入库记录
                var inEntity = new MM_RawMaterialInEntity()
                {
                    FactoryCode = model.FactoryCode,
                    FactoryName = model.FactoryName,
                    WorkOrder = workOrder,
                    DocNum = time.ToString("yyyyMMddHHmmss"),
                    MaterialCode = model.MaterialCode,
                    MaterialName = model.MaterialName,
                    SupplierCode = model.SupplierCode,
                    SmallClass = materialEntity?.SmallClass,
                    InType = "5",//退库
                    WhsCode = model.WhsCode,
                    BatchNo = model.BatchNo,
                    Qty = model.Qty,
                    Unit = model.Unit,
                    UnitName = model.UnitName,
                    LocationCode = model.LocationCode,
                    Creator = userCode,
                    CreateTime = time,
                    AssociateNo = associateNo,
                    BusinessId = exEntity.Id,
                    BusinessTable = "PL_ExeWorkOrder"
                };

                //3.库存添加
                var mrEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == model.BatchNo && t.MaterialCode == model.MaterialCode
                    && t.IsFrozen == "0" && t.WhsCode == model.WhsCode && t.LocationCode == model.LocationCode
                    && t.SupplierCode == model.SupplierCode);
                if (mrEntity != null && !string.IsNullOrEmpty(mrEntity.Id))
                {
                    mrEntity.Qty = mrEntity.Qty + model.Qty;
                    mrEntity.ModifyBy = userCode;
                    mrEntity.ModifyTime = time;
                }
                else
                {
                    mrEntity = new MM_RawMaterialStockEntity();
                    mrEntity.FactoryCode = model.FactoryCode;
                    mrEntity.FactoryName = model.FactoryName;
                    mrEntity.MaterialCode = model.MaterialCode;
                    mrEntity.MaterialName = model.MaterialName;
                    mrEntity.BatchNo = model.BatchNo;
                    mrEntity.SupplierCode = model.SupplierCode;
                    mrEntity.WhsCode = model.WhsCode;
                    mrEntity.Spec = materialEntity?.Spec;
                    mrEntity.SmallClass = materialEntity?.SmallClass;
                    mrEntity.Qty = model.Qty;
                    mrEntity.Unit = model.Unit;
                    mrEntity.LocationCode = model.LocationCode;
                    mrEntity.Creator = userCode;
                    mrEntity.CreateTime = time;
                    mrEntity.IsFrozen = "0";
                }

                //4.线边库库存扣减
                var stockOutEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == model.BatchNo && t.MaterialCode == model.MaterialCode
                    && t.IsFrozen == "0" && t.WhsCode == model.OldWhsCode && t.LocationCode == model.OldLocationCode
                    && t.SupplierCode == model.SupplierCode);
                if (stockOutEntity != null && !string.IsNullOrEmpty(stockOutEntity.Id))
                {
                    stockOutEntity.Qty -= model.Qty;
                    stockOutEntity.ModifyBy = userCode;
                    stockOutEntity.ModifyTime = time;
                }
                else
                    return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_11", model.MaterialCode, model.BatchNo, model.OldWhsCode, model.OldLocationCode));//没有库存，物料【{model.MaterialCode}】、批次【{model.BatchNo}】、仓库【{model.OldWhsCode}】、库位【{model.OldLocationCode}】

                #region 同步SAP
                var factoryCode = workorderEntity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    IF102_ITEM IF102_ITEM = new IF102_ITEM();
                    IF102_ITEM.BWART = "311";
                    IF102_ITEM.MATNR = materialEntity?.SAPMaterialCode ?? "";
                    IF102_ITEM.WERKS = outEntity.FactoryCode ?? "";
                    IF102_ITEM.LGORT = outEntity.LocationCode ?? "";
                    IF102_ITEM.CHARG = outEntity.BatchNo ?? "";
                    IF102_ITEM.ERFMG = outEntity.Qty.ToString() ?? "";
                    IF102_ITEM.ERFME = outEntity.Unit ?? "";
                    IF102_ITEM.UMWRK = inEntity.FactoryCode ?? "";
                    IF102_ITEM.UMLGO = inEntity.LocationCode ?? "";
                    IF102_ITEM.UMCHA = inEntity.BatchNo ?? "";
                    IT_ITEM.Add(IF102_ITEM);
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    outEntity.IsPosted = sapResult.Flag ? "1" : ""; ;
                    outEntity.PostedMsg = sapResult.Msg;
                    outEntity.PostedTime = DateTime.Now;
                    outEntity.PostedUser = "SAP";
                    outEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");

                    inEntity.IsPosted = sapResult.Flag ? "1" : ""; ;
                    inEntity.PostedMsg = sapResult.Msg;
                    inEntity.PostedTime = DateTime.Now;
                    inEntity.PostedUser = "SAP";
                    inEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");

                }
                #endregion

                using (TransactionScope ts = new TransactionScope())
                {
                    _PlanStoreIssueBLL.SaveForm(plEntity.Id, plEntity);
                    _ExeWorkOrderBLL.SaveEntity(exEntity.Id, exEntity, out msg);
                    _RawMaterialOutBLL.SaveEntity("", outEntity, out msg);
                    _RawMaterialInBLL.SaveEntity(null, inEntity, out msg);
                    _RawMaterialStockBLL.SaveEntity(mrEntity.Id, mrEntity, out msg);
                    _RawMaterialStockBLL.SaveEntity(stockOutEntity.Id, stockOutEntity, out msg);
                    _WorkOrderBLL.SaveEntity(workorderEntity.Id, workorderEntity, out msg);

                    if (lstResume.Count > 0)
                        _resumeService.SaveEntity_List(true, userCode, lstResume, out msg);

                    ts.Complete();
                }
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        #endregion

        #region 删除超产品 
        [HttpPost]
        [Route("Delete_SuperProduct")]
        public HttpResponseMessage Delete_SuperProduct(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var msg = "";
            var time = DateTime.Now;
            try
            {
                var entity = JsonConvert.DeserializeObject<PL_ExeWorkOrderEntity>(getValue(jo, "Entity"));
                var exeWorkOrderEntity = _ExeWorkOrderBLL.Get_ExpressionEntity(t => t.Id == entity.Id);
                var wo = exeWorkOrderEntity.WorkOrder;

                var woEntity = _PlanStoreIssueBLL.GetEntity(t => t.WorkOrder == wo);
                woEntity.UnProductNum = woEntity.UnProductNum + exeWorkOrderEntity.SheetsQty;
                woEntity.ModifyBy = userCode;
                woEntity.ModifyTime = time;
                _PlanStoreIssueBLL.SaveForm(woEntity.Id, woEntity);

                exeWorkOrderEntity.IsEnabled = false;
                exeWorkOrderEntity.ModifyBy = userCode;
                exeWorkOrderEntity.ModifyTime = time;
                _ExeWorkOrderBLL.SaveEntity(exeWorkOrderEntity.Id, exeWorkOrderEntity, out msg);

                if (!string.IsNullOrEmpty(exeWorkOrderEntity.SupId))
                {
                    var supStockEntity = _SuperProductStockBLL.Get_ExpressionEntity(t => t.Id == exeWorkOrderEntity.SupId);
                    if (supStockEntity != null)
                    {
                        supStockEntity.LockedQty -= exeWorkOrderEntity.PiecesQty;
                        if (supStockEntity.LockedQty < 0)
                            supStockEntity.LockedQty = 0;

                        _SuperProductStockBLL.SaveEntity(supStockEntity.Id, supStockEntity, out msg);
                    }
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        #endregion

        #region 编辑超产品
        [HttpPost]
        [Route("Update_SuperProduct")]
        public HttpResponseMessage Update_SuperProduct(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var msg = "";
            var time = DateTime.Now;
            try
            {
                var history = decimal.Parse(getValue(jo, "HistoryNum"));
                var entity = JsonConvert.DeserializeObject<PL_ExeWorkOrderEntity>(getValue(jo, "Entity"));
                if (history > entity.PiecesQty)
                {
                    var wo = entity.WorkOrder;
                    var woEntity = _PlanStoreIssueBLL.GetEntity(t => t.WorkOrder == wo);
                    woEntity.UnProductNum = history - entity.PiecesQty;
                    woEntity.ModifyBy = userCode;
                    woEntity.ModifyTime = time;
                    _PlanStoreIssueBLL.SaveForm(woEntity.Id, woEntity);
                }

                entity.ModifyBy = userCode;
                entity.ModifyTime = time;
                _ExeWorkOrderBLL.SaveEntity(entity.Id, entity, out msg);

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 片转张
        /// </summary>
        /// <param name="pieceQty"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int PieceToSheet(int pieceQty, int value)
        {
            int sheetQty = pieceQty / value;
            int remainderQty = pieceQty % value;
            if (remainderQty > 0) sheetQty += 1;

            return sheetQty;
        }

        /// <summary>
        /// 张转拖
        /// </summary>
        /// <param name="sheetQty"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private int SheetToPallet(int sheetQty, int value)
        {
            int palletQty = sheetQty / value;
            int remainderQty = sheetQty % value;
            if (remainderQty > 0) palletQty += 1;

            return palletQty;
        }

        /// <summary>
        /// 流转卡类型
        /// </summary>
        /// <param name="lstExeWorkOrderType">执行工单列表</param>
        /// <param name="currentExeWorkOrderType">当前执行工单类型</param>
        /// <returns></returns>
        private string GetTransferCardType(List<string> lstExeWorkOrderType, string currentExeWorkOrderType)
        {
            if (lstExeWorkOrderType.Contains("5")) return "5";//超产品
            else if (currentExeWorkOrderType == "1") return "1";//正常
            else if (currentExeWorkOrderType == "2") return "2";//补料
            else if (currentExeWorkOrderType == "3") return "3";//拣余单
            else return currentExeWorkOrderType;
        }

        /// <summary>
        /// 获取执行工单生成托数
        /// </summary>
        /// <param name="pmOperationPalletNumList">//工序物料托盘数量集合/param>
        /// <param name="sheetQty">执行工单张数</param>
        /// <param name="spec">规格</param>
        /// <param name="DXZH">大小转换</param>
        /// <returns></returns>
        private int GetPalletCount(List<PL_ProcessOfOperationsEntity> plOperationList, List<PMOperationPalletNumEntity> pmOperationPalletNumList, decimal sheetQty, int DXZH, out decimal palletQty, out string unitName, out string operationCode)
        {
            //取工艺路线-工序的最大托数
            int num = 0;
            palletQty = 0;
            unitName = "";
            operationCode = "";
            var newPLOperationList = plOperationList.Take(plOperationList.Count - 1).OrderBy(t => t.SN).ToList();
            foreach (var operation in newPLOperationList)
            {
                var item = pmOperationPalletNumList.Find(t => t.ProcessCode == operation.OperationCode);
                var palletCount = 0;
                //if (item.UnitName == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_12"))//张
                palletCount = (int)Math.Ceiling(sheetQty / item.PalletNum.ToDecimal());
                //else
                //    palletCount = (int)Math.Ceiling(sheetQty / (item.PalletNum / DXZH).ToDecimal());

                if (palletCount > num)
                {
                    num = palletCount;
                    palletQty = item.PalletNum.Value;
                    unitName = item.UnitName;
                    operationCode = item.ProcessCode;
                }
            }

            return num;
        }

        /// <summary>
        /// 获取工艺路线首工序托数
        /// </summary>
        /// <param name="sheetQty">执行工单张数</param>
        /// <param name="pmOperationPalletNumEntity">实体</param>
        /// <param name="DXZH">大小转换</param>
        /// <returns></returns>
        private int GetStartOperationPalletCount(decimal sheetQty, PMOperationPalletNumEntity pmOperationPalletNumEntity, int DXZH)
        {
            int num = 0;

            var palletCount = 0;
            //if (pmOperationPalletNumEntity.UnitName == Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_13"))//张
            palletCount = (int)Math.Ceiling(sheetQty / pmOperationPalletNumEntity.PalletNum.ToDecimal());
            //else
            //    palletCount = (int)Math.Ceiling(sheetQty / (pmOperationPalletNumEntity.PalletNum / DXZH).ToDecimal());

            num = palletCount;
            return num;
        }
        #endregion

        #region 打印
        [HttpPost]
        [Route("GetPrintInfo")]
        public HttpResponseMessage GetPrintInfo(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string exeWorkOrder = getValue(jo, "exeWorkOrder");

                var rawMaterialOutList = _RawMaterialOutBLL.Get_ExpressionList(t => t.ExeWorkOrder.Contains(exeWorkOrder)
                    && t.OutType == "6");
                var data1 = _bsModelWithResourceBLL.GetList(t => true);
                var query1 = from a in rawMaterialOutList
                             join b in data1 on a.WhsCode equals b.ResourceCode
                             select new
                             {
                                 a.Id,
                                 a.MaterialCode,
                                 a.MaterialName,
                                 a.BatchNo,
                                 a.WhsCode,
                                 WhsName = b.ResourceName,
                                 a.LocationCode,
                                 a.Qty,
                                 a.Unit
                             };

                result.resultData = query1.ToList();
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Print")]
        public HttpResponseMessage Print(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var exeWorkOrder = getValue(jo, "exeWorkOrder");
                var arrId = JsonConvert.DeserializeObject<List<string>>(getValue(jo, "arrId"));

                string ids = string.Join(",", arrId);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", ids);
                dic.Add("ExeWorkOrder", exeWorkOrder);
                string fileName = HttpContext.Current.Server.MapPath("~/") + "Template/面膜批次.frx";
                var resultData = PrintToPDF(fileName, dic);

                result.resultData = resultData;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}