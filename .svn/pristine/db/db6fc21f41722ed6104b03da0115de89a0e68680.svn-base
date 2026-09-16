using ALP.Application.Busines.Material;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Code.Model;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.Application.UtilExtend.Util;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using ALP.Application.Service.Material;
using ALP.Application.Service.Resources;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    [Auth]
    [RoutePrefix("PL_BOM")]
    public class PL_BOMController : ApiBaseController
    {
        private BS_ProcessBLL _BSProcessBLL = new BS_ProcessBLL();
        private BS_ProcessOfOperationsBLL _BSProcessOfOperationsBLL = new BS_ProcessOfOperationsBLL();
        private BS_ProcessOfOperationsAttrBLL _BSProcessOfOperationsAttrBLL = new BS_ProcessOfOperationsAttrBLL();
        private Base_MaterialFactory_Service _materialFactoryService = new Base_MaterialFactory_Service();//物料工厂主表
        private Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();//关键参数
        private Base_Material_Service _materialService = new Base_Material_Service();//物料主数据

        private PL_BOMBLL _PLBOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _PLBOMItemsBLL = new PL_BOMItemsBLL();
        private PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();
        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperations_Service _plOperationsService = new PL_ProcessOfOperations_Service();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private PL_PlanStoreIssue_Service _planStoreService = new PL_PlanStoreIssue_Service();//工单拆解发料表
        private PL_PrdOrderReqMaterials_Service _plReqMaterialsService = new PL_PrdOrderReqMaterials_Service();//物料需求

        #region 查询工单物料数据
        [HttpPost]
        [Route("GetWorkOrderMaterial")]
        public HttpResponseMessage GetWorkOrderMaterial(JObject jo)
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

                var data = _PLBOMBLL.GetWorkOrderMaterial(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询vc工单bom
        /// jpf 2022-11-28 add
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetVCWorkOrderMaterial")]
        public HttpResponseMessage GetVCWorkOrderMaterial(JObject jo)
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

                var data = new PL_BOM_Service().GetVCWorkOrderMaterial(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 修改工艺路线获取最新BOM
        /// jpf 2022-11-29 add
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetVCWorkOrderTraitMaterial")]
        public HttpResponseMessage GetVCWorkOrderTraitMaterial(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                string WorkOrder = getValue(jo, "WorkOrder");
                var watch = CommonHelper.TimerStart();

                var data = new PL_BOM_Service().GetVCWorkOrderTraitMaterial(WorkOrder);
                var JsonData = new
                {
                    rows = data,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 工单BOM修改中保存调用方法
        /// jpf 2022-11-29 add
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveVCworkBOM")]
        public HttpResponseMessage SaveVCworkBOM(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var time = DateTime.Now;
            var msg = "";
            try
            {
                var BomItemlist = JsonConvert.DeserializeObject<List<PL_BOMItemsEntity>>(getValue(jo, "data"));
                var WorkOrder = getValue(jo, "WorkOrder");

                List<PL_PrdOrderReqMaterialsEntity> lstMaterialRequest = new List<PL_PrdOrderReqMaterialsEntity>();
                var workOrderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == WorkOrder && t.IsEnabled == true);

                var BOMentity = _PLBOMBLL.Get_ExpressionEntity(t => t.WorkOrder == WorkOrder);
                BOMentity.ModifyTime = time;
                BOMentity.ModifyBy = userCode;
                var BOMitemList = new PL_BOMItems_Service().Get_ExpressionList(t => t.BOMId == BOMentity.Id).ToList();
                BomItemlist.ForEach(item =>
                {
                    item.BOMId = BOMentity.Id;
                    item.Id = Guid.NewGuid().ToString();
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;

                    #region 重新生成物料需求
                    PL_PrdOrderReqMaterialsEntity reqMaterialsEntity = new PL_PrdOrderReqMaterialsEntity();
                    reqMaterialsEntity.Id = Guid.NewGuid().ToString();
                    reqMaterialsEntity.FactoryCode = BOMentity.FactoryCode;
                    reqMaterialsEntity.FactoryName = BOMentity.FactoryName;
                    reqMaterialsEntity.WorkOrder = WorkOrder;
                    reqMaterialsEntity.MaterialCode = item.MaterialCode;
                    reqMaterialsEntity.MaterialName = item.MaterialName;
                    reqMaterialsEntity.Spec = item.Spec;
                    reqMaterialsEntity.SmallClass = item.SmallClass;
                    reqMaterialsEntity.UnitName = item.UnitName;
                    reqMaterialsEntity.Amount = Math.Round((workOrderEntity.OrderPieces / BOMentity.UnitNum * item.Num).Value, 3);
                    reqMaterialsEntity.PurchaseType = item.ProcureType;
                    reqMaterialsEntity.Creator = userCode;
                    reqMaterialsEntity.CreateTime = time;
                    reqMaterialsEntity.IsDeleted = false;
                    lstMaterialRequest.Add(reqMaterialsEntity);
                    #endregion
                });

                using (var ts = new TransactionScope())
                {
                    _PLBOMBLL.SaveForm(BOMentity.Id, BOMentity);
                    //将原有的BOM明细删除
                    _PLBOMItemsBLL.RemoveForm(t => t.BOMId == BOMentity.Id);
                    _PLBOMItemsBLL.Save_List(false, BomItemlist);

                    _plReqMaterialsService.RemoveForm(t => t.WorkOrder == WorkOrder);
                    _plReqMaterialsService.SaveEntity_List(false, userCode, lstMaterialRequest, out msg);

                    ts.Complete();
                }
                #region 重新计算单耗
                var planStoreEntity = _planStoreService.GetEntity(t => t.FactoryCode == workOrderEntity.FactoryCode && t.WorkOrder == WorkOrder
                    && t.IsDeleted == false);
                if (planStoreEntity != null)
                {
                    var consumeList = _WorkOrderBLL.GetWorkOrderMaterialPieceConsume(workOrderEntity.ProductOrder);
                    //单耗
                    var mastConsume = consumeList.Find(t => t.WorkOrder == WorkOrder && t.TypeName == "Mark")?.DanHao;
                    planStoreEntity.MaskConsume = Convert.IsDBNull(mastConsume) ? null : mastConsume;
                    _planStoreService.SaveForm(planStoreEntity.Id, planStoreEntity);
                }
                #endregion

                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {

                result.success = false;
                result.returnMsg = Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取工单BOM面膜消耗
        [HttpPost]
        [Route("GetWorkOrderBomUnitConsome")]
        public HttpResponseMessage GetWorkOrderBomUnitConsome(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var model = _PLBOMBLL.GetWorkOrderBomUnitConsome(queryJson);

                result.resultData = model;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取工单BOM 编辑界面
        [HttpPost]
        [Route("GetWorkOrderBom")]
        public HttpResponseMessage GetWorkOrderBom(JObject jo)
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

                var data = _PLBOMBLL.GetWorkOrderBom(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取工单BOMItem 编辑界面
        [HttpPost]
        [Route("GetWorkOrderBomItem")]
        public HttpResponseMessage GetWorkOrderBomItem(JObject jo)
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

                var data = _PLBOMBLL.GetWorkOrderBomItem(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 工单BOM修改
        [HttpPost]
        [Route("SaveBatchWorkOrderBomItem")]
        public HttpResponseMessage SaveBatchWorkOrderBomItem(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            try
            {
                var isUpdateProcess = getValue(jo, "IsUpdateProcess");//修改1，不修改 0
                var isUpdateBom = getValue(jo, "IsUpdateBom");//修改1，不修改 0
                var bomCode = getValue(jo, "BOMCode");
                var entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PL_BOMItemsEntity>>(getValue(jo, "data"));
                var workOrderList = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "workOrderList"));//修改工单列表

                if (string.IsNullOrEmpty(entity.StartOperation))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_BOMController.Tips_5"));//起始工序不能为空!

                var plProcessList = new List<PL_ProcessEntity>();
                var plOperationList = new List<PL_ProcessOfOperationsEntity>();
                var plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                var plBOMList = new List<PL_BOMEntity>();
                var plBOMList_Insert = new List<PL_BOMEntity>();
                var plBOMItemList = new List<PL_BOMItemsEntity>();
                var lstMaterialRequest = new List<PL_PrdOrderReqMaterialsEntity>();

                //修改PL_WorkOrder 工序工艺 起始工艺
                //修改PL_Process,PL_ProcessOfOperations,PL_ProcessOfOperationsAttr
                //修改PL_BOM,PL_BOMItems

                var arrWorkOrder = workOrderList.Select(t => t.WorkOrder).Distinct();
                //所有的工单工艺路线
                plProcessList = _PLProcessBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                //新工艺路线(基础数据)
                var bsprocessEntity = _BSProcessBLL.Get_ExpressionEntity(t => t.ProcessCode == entity.Process);
                //获取工艺工序（基础数据）
                var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessCode == bsprocessEntity.ProcessCode).ToList();
                var bsOperationEntity = bsOperationList.Find(t => t.OperationCode == entity.StartOperation);
                bsOperationList = bsOperationList.Where(t => t.SN >= bsOperationEntity.SN).ToList();//只保留起始工序及之后的工序    
                //获取子表属性(基础数据)
                var bsOperationIds = bsOperationList.Select(t => t.Id);
                var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                //所有工单BOM
                plBOMList = _PLBOMBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                //筛选起始工序及之后用到的物料
                var operationCodes = bsOperationList.Select(t => t.OperationCode);
                list = list.Where(t => operationCodes.Contains(t.ConsumeProcess)).ToList();
                var factoryCode = plBOMList.FirstOrDefault()?.FactoryCode;
                if (string.IsNullOrEmpty(factoryCode))
                {
                    factoryCode = workOrderList.FirstOrDefault().FactoryCode;
                }
                var arrMaterialCode = list.Select(t => t.MaterialCode).Distinct().ToArray();
                var materialList = _materialService.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();
                var materialFactoryList = _materialFactoryService.Get_ExpressionList(t => t.FactoryCode == factoryCode && arrMaterialCode.Contains(t.MaterialCode)).ToList();
                //获取外发的工序
                var arrWFOperationCode = list.Where(t => t.WFMark == "1").Select(t => t.ConsumeProcess).ToArray();
                //遍历工单
                foreach (var item in workOrderList)
                {
                    item.Process = entity.Process;
                    item.StartOperation = entity.StartOperation;
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;

                    #region 工艺子表he属性
                    //修改工艺 todo
                    var plProcessEntity = plProcessList.Find(t => t.WorkOrder == item.WorkOrder);
                    plProcessEntity.ProcessCode = entity.Process;
                    plProcessEntity.ModifyBy = userCode;
                    plProcessEntity.ModifyTime = time;
                    plProcessEntity.ProcessName = bsprocessEntity.ProcessName;
                    plProcessEntity.MaterialClass = bsprocessEntity.MaterialClass;
                    plProcessEntity.SmallClass = bsprocessEntity.SmallClass;

                    bsOperationList.ForEach(item2 =>
                    {
                        //工单-工艺-工序
                        var plOperationEntity = new PL_ProcessOfOperationsEntity();
                        plOperationEntity.Id = Guid.NewGuid().ToString();
                        plOperationEntity.ProcessId = plProcessEntity.Id;
                        plOperationEntity.FactoryCode = plProcessEntity.FactoryCode;
                        plOperationEntity.FactoryName = plProcessEntity.FactoryName;
                        plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                        plOperationEntity.OperationCode = item2.OperationCode;
                        plOperationEntity.OperationName = item2.OperationName;
                        plOperationEntity.SN = item2.SN;
                        plOperationEntity.OutWarehouse = item2.OutWarehouse;
                        plOperationEntity.CuringCycle = item2.CuringCycle;
                        plOperationEntity.Creator = userCode;
                        plOperationEntity.CreateTime = DateTime.Now;
                        plOperationEntity.IsDeleted = false;
                        if (arrWFOperationCode.Contains(item2.OperationCode))
                            plOperationEntity.WFMark = "1";
                        else
                            plOperationEntity.WFMark = "2";

                        plOperationList.Add(plOperationEntity);

                        //工单-工艺-属性
                        var bsOperationAttrList = bsAttrList.FindAll(t => t.OperationsId == item2.Id);
                        bsOperationAttrList.ForEach(item3 =>
                        {
                            var plAttrEntity = new PL_ProcessOfOperationsAttrEntity();
                            plAttrEntity.Id = Guid.NewGuid().ToString();
                            plAttrEntity.ProcessId = plProcessEntity.Id;
                            plAttrEntity.OperationsId = plOperationEntity.Id;
                            plAttrEntity.AttrCode = item3.AttrCode;
                            plAttrEntity.AttrName = item3.AttrName;
                            plAttrEntity.AttrType = item3.AttrType;
                            plAttrEntity.AttrTypeName = item3.AttrTypeName;
                            plAttrEntity.SortCode = item3.SortCode;
                            plAttrEntity.AttrValue = item3.AttrValue;
                            plAttrEntity.IsEnabled = true;
                            plAttrEntity.Creator = userCode;
                            plAttrEntity.CreateTime = DateTime.Now;
                            plAttrList.Add(plAttrEntity);
                        });
                    });

                    #endregion

                    #region 工单BOM
                    var bsBomEntity = new BS_BOM_Service().Get_ExpressionEntity(t => t.BOMCode == bomCode);
                    if (bsBomEntity == null)
                        return AjaxResult(false, "基础数据BOM不存在");

                    var plBOMEntity = plBOMList.Find(t => t.WorkOrder == item.WorkOrder && t.IsDeleted == false);
                    if (plBOMEntity == null)
                    {
                        plBOMEntity = new PL_BOMEntity();
                        plBOMEntity.Id = Guid.NewGuid().ToString();
                        plBOMEntity.FactoryCode = item.FactoryCode;
                        plBOMEntity.FactoryName = item.FactoryName;
                        plBOMEntity.WorkOrder = item.WorkOrder;
                        plBOMEntity.BOMCode = bomCode;
                        plBOMEntity.Process = plProcessEntity.ProcessCode;
                        plBOMEntity.MaterialCode = bsBomEntity.MaterialCode;
                        plBOMEntity.MaterialName = bsBomEntity.MaterialName;
                        plBOMEntity.Spec = item.Spec;
                        plBOMEntity.MaterialClass = bsBomEntity.MaterialClass;
                        plBOMEntity.UnitNum = bsBomEntity.UnitNum;
                        plBOMEntity.OrderType = bsBomEntity.OrderType;
                        plBOMEntity.IsDeleted = false;
                        plBOMEntity.Creator = CurrentAccount.UserCode;
                        plBOMEntity.CreateTime = DateTime.Now;
                        plBOMList_Insert.Add(plBOMEntity);
                    }
                    else {
                        plBOMEntity.BOMCode = bomCode;
                        plBOMEntity.Process = plProcessEntity.ProcessCode;
                        plBOMEntity.MaterialCode = bsBomEntity.MaterialCode;
                        plBOMEntity.MaterialName = bsBomEntity.MaterialName;
                        plBOMEntity.MaterialClass = bsBomEntity.MaterialClass;
                        plBOMEntity.UnitNum = bsBomEntity.UnitNum;
                        plBOMEntity.ModifyBy = userCode;
                        plBOMEntity.ModifyTime = DateTime.Now;
                    }

                    foreach (var bItem in list)
                    {
                        var plBomItem = Tools.Clone(bItem);
                        plBomItem.Id = Guid.NewGuid().ToString();
                        plBomItem.BOMId = plBOMEntity.Id;
                        plBomItem.BOMCode = plBOMEntity.BOMCode;
                        plBomItem.FactoryCode = plBOMEntity.FactoryCode;
                        plBomItem.FactoryName = plBOMEntity.FactoryName;
                        plBomItem.BOMId = plBOMEntity.Id;
                        plBomItem.Creator = CurrentAccount.UserCode;
                        plBomItem.CreateTime = DateTime.Now;
                        plBomItem.ModifyBy = userCode;
                        plBomItem.ModifyTime = DateTime.Now;
                        plBomItem.IsUsed = materialFactoryList.Find(t => t.MaterialCode == bItem.MaterialCode)?.IsUsed;
                        plBomItem.WFMark = plBomItem.WFMark ?? "2";
                        plBOMItemList.Add(plBomItem);

                        #region 重新生成物料需求
                        PL_PrdOrderReqMaterialsEntity reqMaterialsEntity = new PL_PrdOrderReqMaterialsEntity();
                        reqMaterialsEntity.Id = Guid.NewGuid().ToString();
                        reqMaterialsEntity.FactoryCode = plBOMEntity.FactoryCode;
                        reqMaterialsEntity.FactoryName = plBOMEntity.FactoryName;
                        reqMaterialsEntity.WorkOrder = plBOMEntity.WorkOrder;
                        reqMaterialsEntity.MaterialCode = bItem.MaterialCode;
                        reqMaterialsEntity.MaterialName = bItem.MaterialName;
                        reqMaterialsEntity.Spec = item.Spec;
                        reqMaterialsEntity.SmallClass = bItem.SmallClass;
                        reqMaterialsEntity.UnitName = bItem.UnitName;
                        reqMaterialsEntity.Amount = Math.Round((item.OrderPieces / plBOMEntity.UnitNum * bItem.Num).Value, 3);
                        reqMaterialsEntity.PurchaseType = bItem.ProcureType;
                        reqMaterialsEntity.Creator = userCode;
                        reqMaterialsEntity.CreateTime = time;
                        reqMaterialsEntity.IsDeleted = false;
                        reqMaterialsEntity.PurchaseType = bItem.ProcureType;
                        lstMaterialRequest.Add(reqMaterialsEntity);
                        #endregion
                    }
                    #endregion
                }

                #region 同步SAP

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1" && plBOMItemList.Any(t => t.WFMark == "1")) // 1：开
                {
                    foreach (var item in workOrderList)
                    {
                        IF111 sapRequest = new IF111();
                        sapRequest.HEAD = new SapHeadDto();
                        sapRequest.HEAD.INIF_ID = SAPInterface.IF111.ToString();

                        sapRequest.RSQ_DATA = new IF111_RSQ_DATA();
                        sapRequest.RSQ_DATA.IV_IFLAG = "2";
                        IF111_DATA IS_DATA = new IF111_DATA();
                        IS_DATA.AUFNR = item.SAP_AUFNR ?? "";
                        IS_DATA.GAMNG = item.OrderPieces.ToString() ?? "";
                        IS_DATA.GSTRP = item.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.GLTRP = item.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.TXT = item.Remark ?? "";
                        sapRequest.RSQ_DATA.IS_DATA = IS_DATA;

                        List<IF111_ITEM1> IT_ITEM1 = new List<IF111_ITEM1>();
                        var sapPLBomEntity = plBOMList.Find(t => t.WorkOrder == item.WorkOrder && t.FactoryCode == item.FactoryCode);
                        var sapPLBomItemList = plBOMItemList.FindAll(t => t.BOMId == sapPLBomEntity.Id && t.WFMark == "1");
                        foreach (var bomItem in sapPLBomItemList)
                        {
                            IF111_ITEM1 IF111_ITEM1 = new IF111_ITEM1();
                            IF111_ITEM1.AUFNR = item.SAP_AUFNR ?? "";
                            IF111_ITEM1.IDNRK = materialList.Find(t => t.MaterialCode == bomItem.MaterialCode)?.SAPMaterialCode ?? "";
                            IF111_ITEM1.MENGE = bomItem.Num.ToString() ?? "";
                            IF111_ITEM1.SORTF = bomItem.ConsumeProcess ?? "";
                            IF111_ITEM1.WEMPF = bomItem.WFMark ?? "";
                            IT_ITEM1.Add(IF111_ITEM1);
                        }
                        sapRequest.RSQ_DATA.IT_ITEM1 = IT_ITEM1;

                        var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);

                    }
                    //工艺路线和bom  SAP无法同时处理  调用两遍接口解决
                    foreach (var item in workOrderList)
                    {
                        IF111 sapRequest = new IF111();
                        sapRequest.HEAD = new SapHeadDto();
                        sapRequest.HEAD.INIF_ID = SAPInterface.IF111.ToString();

                        sapRequest.RSQ_DATA = new IF111_RSQ_DATA();
                        sapRequest.RSQ_DATA.IV_IFLAG = "3";
                        IF111_DATA IS_DATA = new IF111_DATA();
                        IS_DATA.AUFNR = item.SAP_AUFNR ?? "";
                        IS_DATA.GAMNG = item.OrderPieces.ToString() ?? "";
                        IS_DATA.GSTRP = item.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.GLTRP = item.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.TXT = item.Remark ?? "";
                        sapRequest.RSQ_DATA.IS_DATA = IS_DATA;

                        List<IF111_ITEM2> IT_ITEM2 = new List<IF111_ITEM2>();
                        var sapPLProcessEntity = plProcessList.Find(t => t.WorkOrder == item.WorkOrder && t.FactoryCode == item.FactoryCode);
                        var sapPLOperationList = plOperationList.FindAll(t => t.ProcessId == sapPLProcessEntity.Id && t.WFMark == "1");
                        foreach (var operation in sapPLOperationList)
                        {
                            IF111_ITEM2 IF111_ITEM2 = new IF111_ITEM2();
                            IF111_ITEM2.AUFNR = item.SAP_AUFNR ?? "";
                            IF111_ITEM2.PLNNR_ALT = operation.ProcessCode ?? "";
                            IF111_ITEM2.KTSCH = operation.OperationCode ?? "";
                            IF111_ITEM2.CY_SEQNRV = operation.WFMark ?? "";
                            IT_ITEM2.Add(IF111_ITEM2);
                        }
                        sapRequest.RSQ_DATA.IT_ITEM2 = IT_ITEM2;

                        var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);

                    }
                }
                #endregion

                using (var ts = new TransactionScope())
                {
                    //修改工单
                    _WorkOrderBLL.SaveEntity_List(true, userCode, workOrderList, out msg);
                    if (plBOMList_Insert.Count > 0)
                    {
                        _PLBOMBLL.SaveEntity_List(false, plBOMList_Insert);
                    }
                    //工单BOM
                    if (plBOMList != null && plBOMList.Count > 0)
                    {
                        _PLBOMBLL.SaveEntity_List(true, plBOMList);
                        var arrBOMId = plBOMList.Select(t => t.Id);
                        _PLBOMItemsBLL.RemoveForm(t => arrBOMId.Contains(t.BOMId));
                    }
                    _PLBOMItemsBLL.Save_List(false, plBOMItemList);

                    //删除属性
                    var arrplProcessId = plProcessList.Select(t => t.Id);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => arrplProcessId.Contains(t.ProcessId));
                    //删除工艺子表
                    _plOperationsService.RemoveForm(t => arrplProcessId.Contains(t.ProcessId));
                    _PLProcessBLL.SaveEntity_List(true, userCode, plProcessList, out msg);
                    _plOperationsService.SaveEntity_List(false, userCode, plOperationList, out msg);
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                    //更新物料需求
                    if (lstMaterialRequest.Count > 0)
                    {
                        _plReqMaterialsService.RemoveForm(t => arrWorkOrder.Contains(t.WorkOrder));
                        _plReqMaterialsService.SaveEntity_List(false, userCode, lstMaterialRequest, out msg);
                    }

                    ts.Complete();
                }

                #region 重新计算单耗
                var planStoreList = _planStoreService.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                foreach (var item in workOrderList)
                {
                    var planStoreEntity = planStoreList.Find(t => t.FactoryCode == item.FactoryCode && t.WorkOrder == item.WorkOrder);
                    if (planStoreEntity != null)
                    {
                        var consumeList = _WorkOrderBLL.GetWorkOrderMaterialPieceConsume(item.ProductOrder);
                        //单耗
                        var mastConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "Mark")?.DanHao;
                        planStoreEntity.MaskConsume = Convert.IsDBNull(mastConsume) ? null : mastConsume;
                    }
                }
                if (planStoreList.Count > 0)
                    _planStoreService.SaveEntity_List(true, userCode, planStoreList, out msg);
                #endregion

                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取工单工艺 编辑界面
        [HttpPost]
        [Route("GetWorkOrderOperationsItem")]
        public HttpResponseMessage GetWorkOrderOperationsItem(JObject jo)
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

                var data = _plOperationsService.GetWorkOrderOperationsItem(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 保存工单工艺 
        [HttpPost]
        [Route("SaveWorkOrderOperations")]
        public HttpResponseMessage SaveWorkOrderOperations(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            var msg = "";
            try
            {
                var workOrder = getValue(jo, "WorkOrder");
                var list = JsonConvert.DeserializeObject<List<PL_ProcessOfOperationsEntity>>(getValue(jo, "data"));

                _plOperationsService.SaveEntity_List(true, userCode, list, out msg);
                //最新报工的养生周期跟着变动
                _plOperationsService.UpdateHealthTime(workOrder);

                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}