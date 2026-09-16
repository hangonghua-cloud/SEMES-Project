using ALP.Application.Busines.Material;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Code.Model;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Entity.ProduceManage;
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

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    [Auth]
    [RoutePrefix("PL_BOM")]
    public class PL_BOMController : ApiBaseController
    {
        private PL_BOMBLL _PLBOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _PLBOMItemsBLL = new PL_BOMItemsBLL();
        private BS_BOMBLL _bsBomBLL = new BS_BOMBLL();
        private BS_BOMItemsBLL _bsBomItemBLL = new BS_BOMItemsBLL();
        private Base_MaterialBLL _baseMaterialBLL = new Base_MaterialBLL();//物料基础数据
        private PM_OwnProductOrderBLL _WorkOrderBLL = new PM_OwnProductOrderBLL();
        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _PLProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private BS_ProcessBLL _BSProcessBLL = new BS_ProcessBLL();
        private BS_ProcessOfOperationsBLL _BSProcessOfOperationsBLL = new BS_ProcessOfOperationsBLL();
        private BS_ProcessOfOperationsAttrBLL _BSProcessOfOperationsAttrBLL = new BS_ProcessOfOperationsAttrBLL();
        private PM_AbrasiveOrderBLL _AbrasiveOrderBLL = new PM_AbrasiveOrderBLL();

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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取工单BOM 编辑界面
        [HttpPost]
        [Route("GetOwnProductOrderBom")]
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

                var data = _PLBOMBLL.GetOwnProductOrderBom(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 工单BOM修改(自制半成品)
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
                var bomId = getValue(jo, "BOMId");
                var bomCode = getValue(jo, "BOMCode");
                var entity = JsonConvert.DeserializeObject<PM_OwnProductOrderEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PL_BOMItemsEntity>>(getValue(jo, "data"));

                //修改PL_WorkOrder 工序工艺 起始工艺
                //修改PL_Process,PL_ProcessOfOperations,PL_ProcessOfOperationsAttr
                //修改PL_BOM,PL_BOMItems

                var ent = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                var workOrder = entity.WorkOrder;
                entity.Id = ent.Id;
                entity.ProcessCode = ent.ProcessCode;
                entity.OrderStatus = ent.OrderStatus;
                entity.SmallClass = ent.SmallClass;
                entity.SmallClassName = ent.SmallClassName;
                entity.MaterialCode = ent.MaterialCode;
                entity.MaterialName = ent.MaterialName;
                entity.PlanQty = ent.PlanQty;
                entity.Remark = ent.Remark;
                entity.Creator = ent.Creator;
                entity.CreateTime = ent.CreateTime;
                entity.ModifyBy = userCode;
                entity.ModifyTime = time;

                #region 工艺子表he属性
                var plOperationList = new List<PL_ProcessOfOperationsEntity>();
                var plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                //新工艺路线(基础数据)
                var bsprocessEntity = _BSProcessBLL.Get_ExpressionEntity(t => t.ProcessCode == entity.ProcessRoute);
                var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessCode == bsprocessEntity.ProcessCode).ToList();
                //获取子表属性(基础数据)
                var bsOperationIds = bsOperationList.Select(t => t.Id);
                var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                //修改工艺 todo
                var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder);
                plProcessEntity.ProcessCode = entity.ProcessCode;
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
                    plProcessEntity.FactoryName = plProcessEntity.FactoryName;
                    plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                    plOperationEntity.OperationCode = item2.OperationCode;
                    plOperationEntity.OperationName = item2.OperationName;
                    plOperationEntity.SN = item2.SN;
                    plOperationEntity.OutWarehouse = item2.OutWarehouse;
                    plOperationEntity.CuringCycle = item2.CuringCycle;
                    plOperationEntity.Creator = userCode;
                    plOperationEntity.CreateTime = DateTime.Now;
                    plOperationEntity.IsDeleted = false;
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
                var plBOMItemList = new List<PL_BOMItemsEntity>();

                var plBOMEntity = _PLBOMBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);
                plBOMEntity.BOMCode = bomCode;
                plBOMEntity.Process = plProcessEntity.ProcessCode;
                plBOMEntity.ModifyBy = userCode;
                plBOMEntity.ModifyTime = DateTime.Now;
                foreach (var bItem in list)
                {
                    var plBomItem = Tools.Clone(bItem);
                    bItem.Id = Guid.NewGuid().ToString();
                    bItem.BOMCode = plBOMEntity.BOMCode;
                    bItem.FactoryCode = plBOMEntity.FactoryCode;
                    bItem.FactoryName = plBOMEntity.FactoryName;
                    bItem.BOMId = plBOMEntity.Id;
                    bItem.Creator = CurrentAccount.UserCode;
                    bItem.CreateTime = DateTime.Now;
                    bItem.ModifyBy = userCode;
                    bItem.ModifyTime = DateTime.Now;
                    plBOMItemList.Add(bItem);
                }
                #endregion

                using (var ts = new TransactionScope())
                {
                    _WorkOrderBLL.SaveEntity(entity.Id, entity, out msg);
                    _PLBOMItemsBLL.RemoveForm(t => t.BOMId == plBOMEntity.Id);
                    _PLBOMBLL.SaveForm(plBOMEntity.Id, plBOMEntity);
                    _PLBOMItemsBLL.Save_List(false, plBOMItemList);

                    //删除工单工艺路线-属性
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);
                    //删除工单工艺-工序
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);

                    _PLProcessBLL.SaveEntity(plProcessEntity.Id, plProcessEntity, out msg);
                    _PLProcessOfOperationsBLL.SaveEntity_List(false, userCode, plOperationList, out msg);
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                    ts.Complete();
                }

                result.resultData = null;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
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

                var data = _BSProcessOfOperationsBLL.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Count(),
                    page = pagination != null ? pagination.total : data.Count(),
                    records = pagination != null ? pagination.total : data.Count(),
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
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

                _PLProcessOfOperationsBLL.SaveEntity_List(true, userCode, list, out msg);
                //最新报工的养生周期跟着变动
                _PLProcessOfOperationsBLL.UpdateHealthTime(workOrder);

                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 工单BOM修改(磨粉料工单)
        [HttpPost]
        [Route("AbrasiveOrderBOMEdit")]
        public HttpResponseMessage AbrasiveOrderBOMEdit(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            try
            {
                var bomId = getValue(jo, "BOMId");
                var bomCode = getValue(jo, "BOMCode");
                var entity = JsonConvert.DeserializeObject<PM_AbrasiveOrderEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PL_BOMItemsEntity>>(getValue(jo, "data"));

                //修改PL_WorkOrder 工序工艺 起始工艺
                //修改PL_Process,PL_ProcessOfOperations,PL_ProcessOfOperationsAttr
                //修改PL_BOM,PL_BOMItems

                var ent = _AbrasiveOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                var workOrder = entity.WorkOrder;
                entity.Id = ent.Id;
                entity.ProcessCode = ent.ProcessCode;
                entity.OrderStatus = ent.OrderStatus;
                entity.SmallClass = ent.SmallClass;
                entity.MaterialCode = ent.MaterialCode;
                entity.MaterialName = ent.MaterialName;
                entity.PlanQty = ent.PlanQty;
                entity.Remark = ent.Remark;
                entity.Creator = ent.Creator;
                entity.CreateTime = ent.CreateTime;
                entity.ModifyBy = userCode;
                entity.ModifyTime = time;

                #region 工艺子表he属性
                var plOperationList = new List<PL_ProcessOfOperationsEntity>();
                var plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                //新工艺路线(基础数据)
                var bsprocessEntity = _BSProcessBLL.Get_ExpressionEntity(t => t.ProcessCode == entity.ProcessRoute);
                var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessCode == bsprocessEntity.ProcessCode).ToList();
                //获取子表属性(基础数据)
                var bsOperationIds = bsOperationList.Select(t => t.Id);
                var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                //修改工艺 todo
                var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder);
                plProcessEntity.ProcessCode = entity.ProcessCode;
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
                    plProcessEntity.FactoryName = plProcessEntity.FactoryName;
                    plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                    plOperationEntity.OperationCode = item2.OperationCode;
                    plOperationEntity.OperationName = item2.OperationName;
                    plOperationEntity.SN = item2.SN;
                    plOperationEntity.OutWarehouse = item2.OutWarehouse;
                    plOperationEntity.CuringCycle = item2.CuringCycle;
                    plOperationEntity.Creator = userCode;
                    plOperationEntity.CreateTime = DateTime.Now;
                    plOperationEntity.IsDeleted = false;
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
                var plBOMItemList = new List<PL_BOMItemsEntity>();

                var plBOMEntity = _PLBOMBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);
                plBOMEntity.BOMCode = bomCode;
                plBOMEntity.Process = plProcessEntity.ProcessCode;
                plBOMEntity.ModifyBy = userCode;
                plBOMEntity.ModifyTime = DateTime.Now;
                foreach (var bItem in list)
                {
                    var plBomItem = Tools.Clone(bItem);
                    bItem.Id = Guid.NewGuid().ToString();
                    bItem.BOMCode = plBOMEntity.BOMCode;
                    bItem.FactoryCode = plBOMEntity.FactoryCode;
                    bItem.FactoryName = plBOMEntity.FactoryName;
                    bItem.BOMId = plBOMEntity.Id;
                    bItem.Creator = CurrentAccount.UserCode;
                    bItem.CreateTime = DateTime.Now;
                    bItem.ModifyBy = userCode;
                    bItem.ModifyTime = DateTime.Now;
                    plBOMItemList.Add(bItem);
                }
                #endregion

                using (var ts = new TransactionScope())
                {
                    _AbrasiveOrderBLL.SaveEntity(entity.Id, entity, out msg);
                    _PLBOMItemsBLL.RemoveForm(t => t.BOMId == plBOMEntity.Id);
                    _PLBOMBLL.SaveForm(plBOMEntity.Id, plBOMEntity);
                    _PLBOMItemsBLL.Save_List(false, plBOMItemList);

                    //删除工单工艺路线-属性
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);
                    //删除工单工艺-工序
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);

                    _PLProcessBLL.SaveEntity(plProcessEntity.Id, plProcessEntity, out msg);
                    _PLProcessOfOperationsBLL.SaveEntity_List(false, userCode, plOperationList, out msg);
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                    ts.Complete();
                }

                result.resultData = null;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}