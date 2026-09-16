using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Service.ProduceManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Busines.Material;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.ModelLevel;
using ALP.Application.Entity.MaterialManage;
using System.Transactions;
using ALP.Application.Service.Resources;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;
using ALP.Application.Service.PlanManage;
using ALP.Application.Service.MaterialManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_AbrasiveOrderController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 磨粉料工单管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_AbrasiveBG")]
    public class PM_AbrasiveBGController : ApiBaseController
    {
        private Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();//关键参数

        private PM_AbrasiveBGBLL _AbrasiveBGBLL = new PM_AbrasiveBGBLL();
        private Base_MaterialFactoryBLL _baseMaterialFactoryBLL = new Base_MaterialFactoryBLL();
        private PM_AbrasiveOrderBLL _AbrasiveOrderBLL = new PM_AbrasiveOrderBLL();
        private Base_MaterialBLL _MaterialBLL = new Base_MaterialBLL();
        MM_RawMaterialStock_Service _rawMaterialStockService = new MM_RawMaterialStock_Service();
        MM_RawMaterialOut_Service _rawMaterialOutService = new MM_RawMaterialOut_Service();
        PM_MaterialBatchConsumeRecord_Service _rawMaterialConsumeService = new PM_MaterialBatchConsumeRecord_Service();

        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _PLProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        PL_BOM_Service _plBOMService = new PL_BOM_Service();
        PL_BOMItems_Service _plBOMItemService = new PL_BOMItems_Service();
        PM_MaterialBatchUpRecord_Service _materialBatchUpRecordService = new PM_MaterialBatchUpRecord_Service();//原料批次上机记录

        private Level_BLL _levelBLL = new Level_BLL();

        private MM_RawMaterialInBLL _RawMaterialInBLL = new MM_RawMaterialInBLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_AbrasiveBGPageList")]
        public HttpResponseMessage PM_AbrasiveBGPageList(JObject jo)
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

                var data = _AbrasiveBGBLL.GetPageList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_AbrasiveBGPageDataTableList")]
        public HttpResponseMessage PM_AbrasiveBGPageDataTableList(JObject jo)
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

                var data = _AbrasiveBGBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_AbrasiveBGList")]
        public HttpResponseMessage GetPM_AbrasiveBGList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _AbrasiveBGBLL.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                if (msg != "")
                {
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 磨粉料报工
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_AbrasiveBG")]
        public HttpResponseMessage SavePM_AbrasiveBG(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                string keyValue = getValue(jo, "KeyValue");

                //参数转实体
                PM_AbrasiveBGEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveBGEntity>(getValue(jo, "Entity"));

                var orderEntity = _AbrasiveOrderBLL.GetEntity(entity.AbrasiveId);
                if (orderEntity.OrderStatus == "3")
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_8");//工单已完成，无法报工！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.Id = Guid.NewGuid().ToString();
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }

                var processEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == orderEntity.WorkOrder && t.IsDeleted == false);
                var processId = processEntity?.Id;

                var plOperationsEntity = _PLProcessOfOperationsBLL.Get_ExpressionEntity(t => t.OperationCode == entity.ProcessCode && t.ProcessId == processId);
                var operationsId = plOperationsEntity?.Id;

                var attrList = _PLProcessOfOperationsAttrBLL.Get_ExpressionList(t => t.OperationsId == operationsId).ToList();
                var attrEntity = attrList.Find(t => t.AttrCode == "BGKW"); //取工单工序属性
                var locationCode = attrEntity?.AttrValue; //库位

                if (string.IsNullOrEmpty(locationCode))
                    return AjaxResult(false, Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_26"));//请维护工艺路线报工库位属性！

                var maEntity = _MaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == orderEntity.MaterialCode);
                var stockEntity = _rawMaterialStockService.Get_ExpressionEntity(t => t.MaterialCode == orderEntity.MaterialCode && t.LocationCode == locationCode);
                if (stockEntity == null)
                {
                    stockEntity = new MM_RawMaterialStockEntity();
                    stockEntity.FactoryCode = orderEntity.FactoryCode;
                    stockEntity.FactoryName = entity.FactoryName;
                    stockEntity.MaterialCode = maEntity.MaterialCode;
                    stockEntity.MaterialName = maEntity.MaterialName;
                    stockEntity.Spec = maEntity.Spec;
                    stockEntity.SmallClass = maEntity.SmallClass;
                    stockEntity.Qty = entity.BGQty;
                    stockEntity.Unit = maEntity.UnitName;
                    //SupplierCode = "",//todo 生产厂家未找到
                    stockEntity.WhsCode = _levelBLL.GetModelResourceByChild(attrEntity.AttrValue).ResourceCode;
                    stockEntity.LocationCode = attrEntity.AttrValue;
                    stockEntity.IsFrozen = "0";
                    stockEntity.Creator = userCode;
                    stockEntity.CreateTime = DateTime.Now;

                }
                else
                {
                    stockEntity.Qty += entity.BGQty;
                    stockEntity.ModifyBy = userCode;
                    stockEntity.ModifyTime = DateTime.Now;
                }

                #region 入库记录
                MM_RawMaterialInEntity inEntity = new MM_RawMaterialInEntity();
                inEntity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                inEntity.FactoryCode = orderEntity.FactoryCode;
                inEntity.FactoryName = entity.FactoryName;
                inEntity.WorkOrder = orderEntity.WorkOrder;
                inEntity.MaterialCode = maEntity.MaterialCode;
                inEntity.MaterialName = maEntity.MaterialName;
                inEntity.Unit = orderEntity.Unit;
                inEntity.UnitName = orderEntity.UnitName;
                inEntity.BatchNo = entity.BatchNo;
                inEntity.Spec = orderEntity.Spec;
                inEntity.SmallClass = orderEntity.SmallClass;
                //inEntity.SupplierCode = orderEntity.SupplierCode;
                inEntity.Qty = entity.BGQty;
                inEntity.InType = "9";//报工入库
                inEntity.WhsCode = _levelBLL.GetModelResourceByChild(attrEntity?.AttrValue)?.ResourceCode;
                inEntity.LocationCode = attrEntity?.AttrValue;//入库库位
                inEntity.Creator = userCode;
                inEntity.CreateTime = DateTime.Now;
                inEntity.BusinessId = entity.Id;
                inEntity.BusinessTable = "PM_AbrasiveBG";
                #endregion

                //报工判断工单状态，未开工状态改为正在生产
                if (orderEntity.OrderStatus == "1")
                {
                    orderEntity.OrderStatus = "2";//正在生产
                    orderEntity.ModifyBy = userCode;
                    orderEntity.ModifyTime = DateTime.Now;
                }

                #region 物料消耗信息
                List<PM_MaterialBatchConsumeRecordEntity> mbConsumeRecordList = new List<PM_MaterialBatchConsumeRecordEntity>();
                List<MM_RawMaterialOutEntity> rawMaterialOutList = new List<MM_RawMaterialOutEntity>();
                var rawMaterialStockList = new List<MM_RawMaterialStockEntity>();//原材料库存列表
                var docNum = DateTime.Now.ToString("yyyyMMddHHmmss");

                var plBOMEntity = _plBOMService.Get_ExpressionEntity(t => t.WorkOrder == orderEntity.WorkOrder && t.IsDeleted == false);
                if (plBOMEntity == null)
                    return AjaxResult(false, Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_30"));//工单BOM不能为空

                var plBOMItemList = _plBOMItemService.GetBomItemList(plBOMEntity.Id, orderEntity.ProcessCode).ToList();
                if (plBOMItemList.Count > 0)
                {
                    var operationCode = entity.ProcessCode;
                    var machineCode = entity.Machine;
                    //找到当前工序物料批次上机记录
                    var upRecordList = _materialBatchUpRecordService.Get_ExpressionList(t => t.ProcessCode == operationCode && t.MachineCode == machineCode && t.Flag == 1).ToList();
                    //f.是否有库存
                    var rawMaterialStockTable = _rawMaterialStockService.GetList(t => t.IsFrozen == "0");
                    var query2 = from a in plBOMItemList
                                 join b in rawMaterialStockTable
                                 on new { a.MaterialCode, WhsCode = a.Warehouse, a.LocationCode } equals new { b.MaterialCode, b.WhsCode, b.LocationCode }
                                 select b;
                    var rawMaterialStockSList = query2.ToList();
                    //校验是否库存为负数 1开，0关
                    var keyParamItemEntity2 = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "Backflush"
                        && t.Remark1 == entity.FactoryCode);

                    foreach (var item in plBOMItemList)
                    {
                        // e.启用批次管理 是否存在批次上机记录
                        var batchNo = "";
                        if (item.IsUsed == true)
                        {
                            if (!upRecordList.Any(t => t.MaterialCode == item.MaterialCode))
                            {
                                return AjaxResult(false, Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_27", item.MaterialCode, item.MaterialName));//物料[{item.MaterialCode}{item.MaterialName}]没有上机记录
                            }
                            batchNo = upRecordList.ToList().Find(t => t.MaterialCode == item.MaterialCode)?.BatchNo;
                            if (!rawMaterialStockSList.Any(t => t.MaterialCode == item.MaterialCode && t.BatchNo == batchNo) && keyParamItemEntity2.ItemValue == "1")
                            {
                                return AjaxResult(false, Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_28", item.MaterialCode, item.MaterialName, batchNo));//物料[{item.MaterialCode}{item.MaterialName}]、批次[{batchNo}]没有可用的库存
                            }
                        }
                        else //非批次管理
                        {
                            if (!rawMaterialStockSList.Any(t => t.MaterialCode == item.MaterialCode) && keyParamItemEntity2.ItemValue == "1")
                            {
                                return AjaxResult(false, Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_29", item.MaterialCode, item.MaterialName));//物料[{item.MaterialCode}{item.MaterialName}]没有可用的库存
                            }
                        }

                        #region 物料消耗信息
                        var mbConsumeRecordEntity = new PM_MaterialBatchConsumeRecordEntity();
                        mbConsumeRecordEntity.Id = Guid.NewGuid().ToString();
                        mbConsumeRecordEntity.BGID = entity.Id;
                        mbConsumeRecordEntity.FactoryCode = orderEntity.FactoryCode;
                        mbConsumeRecordEntity.FactoryName = orderEntity.FactoryName;
                        mbConsumeRecordEntity.MaterialCode = item.MaterialCode;
                        mbConsumeRecordEntity.MaterialName = item.MaterialName;
                        mbConsumeRecordEntity.WhsCode = item.Warehouse;
                        mbConsumeRecordEntity.LocationCode = item.LocationCode;
                        mbConsumeRecordEntity.Spec = item.Spec;
                        mbConsumeRecordEntity.BatchNo = batchNo;
                        mbConsumeRecordEntity.RecoilQty = Math.Round(item.Num.Value * (entity.BGQty.Value / plBOMEntity.UnitNum.Value), 2, MidpointRounding.AwayFromZero);
                        mbConsumeRecordEntity.IsEnabled = true;
                        mbConsumeRecordEntity.Creator = userCode;
                        mbConsumeRecordEntity.CreateTime = DateTime.Now;
                        mbConsumeRecordEntity.Unit = item.UnitName;
                        mbConsumeRecordList.Add(mbConsumeRecordEntity);
                        #endregion

                        #region 出库记录
                        MM_RawMaterialOutEntity rawMaterialOutEntity = new MM_RawMaterialOutEntity();
                        rawMaterialOutEntity.Id = Guid.NewGuid().ToString();
                        rawMaterialOutEntity.BusinessId = entity.Id;
                        rawMaterialOutEntity.BusinessTable = "PM_AbrasiveBG";
                        rawMaterialOutEntity.FactoryCode = orderEntity.FactoryCode;
                        rawMaterialOutEntity.FactoryName = orderEntity.FactoryName;
                        rawMaterialOutEntity.BGType = "6";//磨粉料报工
                        rawMaterialOutEntity.BGBatchNo = "";//待确认
                        rawMaterialOutEntity.CardCode = "";
                        rawMaterialOutEntity.ProductOrder = "";
                        rawMaterialOutEntity.WorkOrder = orderEntity.WorkOrder;
                        rawMaterialOutEntity.CustomerPO = "";
                        rawMaterialOutEntity.ContainerNO = "";
                        rawMaterialOutEntity.ExeWorkOrder = "";
                        rawMaterialOutEntity.ProcessCode = orderEntity.ProcessCode;
                        rawMaterialOutEntity.Spec = item.Spec;
                        rawMaterialOutEntity.CustomerModelName = item.MaterialName;
                        rawMaterialOutEntity.CustomerModel = item.MaterialCode;
                        rawMaterialOutEntity.BGQty = entity.BGQty;
                        //rawMaterialOutEntity.ProductUnit = plAttrList.Find(t => t.AttrCode == "CCDW")?.AttrValue;
                        rawMaterialOutEntity.ProductUnit = maEntity.UnitName;//报工单位统一从工序属性里取值
                        rawMaterialOutEntity.DocNum = docNum;
                        rawMaterialOutEntity.WhsCode = item.Warehouse;
                        rawMaterialOutEntity.LocationCode = item.LocationCode;//取虚拟库位
                        rawMaterialOutEntity.MaterialCode = item.MaterialCode;
                        rawMaterialOutEntity.MaterialName = item.MaterialName;
                        rawMaterialOutEntity.BatchNo = mbConsumeRecordEntity.BatchNo;
                        rawMaterialOutEntity.OutType = "1";//报工
                        rawMaterialOutEntity.Qty = mbConsumeRecordEntity.RecoilQty;
                        rawMaterialOutEntity.Unit = item.Unit;
                        rawMaterialOutEntity.UnitName = item.UnitName;
                        rawMaterialOutEntity.Creator = userCode;
                        rawMaterialOutEntity.CreateTime = DateTime.Now;
                        rawMaterialOutList.Add(rawMaterialOutEntity);
                        #endregion
                    }

                    var rawMaterialStockTable1 = _rawMaterialStockService.GetList(t => t.IsFrozen == "0");
                    var query4 = from a in rawMaterialOutList
                                 join b in rawMaterialStockTable1
                                 on new { a.MaterialCode, a.WhsCode, BatchNo = a.BatchNo ?? "" } equals new { b.MaterialCode, b.WhsCode, BatchNo = b.BatchNo ?? "" }
                                 select new MM_RawMaterialStockEntity()
                                 {
                                     Id = b.Id,
                                     FactoryCode = b.FactoryCode,
                                     FactoryName = b.FactoryName,
                                     MaterialCode = b.MaterialCode,
                                     MaterialName = b.MaterialName,
                                     BatchNo = b.BatchNo,
                                     Qty = b.Qty - a.Qty,
                                     Unit = b.Unit,
                                     SupplierCode = b.SupplierCode,
                                     WhsCode = b.WhsCode,
                                     LocationCode = b.LocationCode,
                                     IsFrozen = b.IsFrozen,
                                     Creator = b.Creator,
                                     CreateTime = b.CreateTime,
                                     ModifyBy = userCode,
                                     ModifyTime = DateTime.Now
                                 };
                    rawMaterialStockList = query4.ToList();

                    //校验是否倒冲为负 1开，0关
                    if (keyParamItemEntity2.ItemValue == "1")
                    {

                        // 2024年9月10号 韩总要求库存不能为负数，不限制是否启用批次

                        if (rawMaterialStockList.Where(t => t.Qty < 0).Count() > 0)
                        {
                            var arrMaterialCode = rawMaterialStockList.Select(t => t.MaterialCode).ToArray();
                            var materialFactoryList = _baseMaterialFactoryBLL.Get_ExpressionList(t => t.FactoryCode == orderEntity.FactoryCode
                                && arrMaterialCode.Contains(t.MaterialCode)).ToList();

                            var negativeStockEntity = rawMaterialStockList.FirstOrDefault(t => t.Qty < 0);
                            decimal? outQty = 0;
                            //是否启用批次管理
                            if (materialFactoryList.Find(t => t.MaterialCode == negativeStockEntity.MaterialCode)?.IsUsed == true)
                            {
                                outQty = rawMaterialOutList.Find(t => t.MaterialCode == negativeStockEntity.MaterialCode && t.LocationCode == negativeStockEntity.LocationCode
                                    && t.BatchNo == negativeStockEntity.BatchNo)?.Qty;//出库数量
                            }
                            else
                            {
                                //出库数量
                                outQty = rawMaterialOutList.Find(t => t.MaterialCode == negativeStockEntity.MaterialCode && t.LocationCode == negativeStockEntity.LocationCode)?.Qty;
                            }

                            var remainStockQty = negativeStockEntity.Qty + outQty;//剩余库存数量
                            var plBomItemEntity = plBOMItemList.Find(t => t.MaterialCode == negativeStockEntity.MaterialCode);
                            var remainBGQty = plBOMEntity.UnitNum / plBomItemEntity.Num * remainStockQty;

                            return AjaxResult(false, $"{plBomItemEntity.MaterialName}剩余库存不足,报工数量不允许超过{remainBGQty.ToString()}");
                        }
                    }
                }
                #endregion

                #region 同步SAP
                var factoryCode = entity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    var arrMaterialCode = rawMaterialOutList.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _MaterialBLL.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                    IF121 sapRequest = new IF121();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF121.ToString();

                    sapRequest.RSQ_DATA = new IF121_RSQ_DATA();
                    IF121_HEAD IF121_HEAD = new IF121_HEAD();
                    IF121_HEAD.MATNR = maEntity.SAPMaterialCode ?? "";
                    IF121_HEAD.WERKS = factoryCode ?? "";
                    IF121_HEAD.BOMCODE = orderEntity.BOMCode ?? "";
                    IF121_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF121_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF121_HEAD.ERFMG = inEntity.Qty.ToString() ?? "";
                    IF121_HEAD.ALORT = inEntity.LocationCode ?? "";
                    IF121_HEAD.CHARG = inEntity.BatchNo ?? "";
                    sapRequest.RSQ_DATA.IS_HEAD = IF121_HEAD;

                    List<IF121_ITEM> IT_ITEM = new List<IF121_ITEM>();
                    foreach (var item in rawMaterialOutList)
                    {
                        var matItem = materialList.Find(t => t.MaterialCode == item.MaterialCode);

                        IF121_ITEM IF121_ITEM = new IF121_ITEM();
                        IF121_ITEM.MATNR = matItem?.SAPMaterialCode ?? "";
                        IF121_ITEM.WERKS = item.FactoryCode ?? "";
                        IF121_ITEM.LGORT = item.LocationCode ?? "";
                        IF121_ITEM.ERFMG_R = item.Qty.ToString();
                        IF121_ITEM.ERFME = item.Unit ?? "";
                        IF121_ITEM.CHARG = item.BatchNo ?? "";
                        IT_ITEM.Add(IF121_ITEM);
                    }
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    entity.IsPosted = sapResult.Flag ? "1" : "";
                    entity.PostedMsg = sapResult.Msg;
                    entity.PostedTime = DateTime.Now;
                    entity.PostedUser = "SAP";
                    entity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                }
                #endregion

                string msg = "";
                using (var ts = new TransactionScope())
                {
                    _AbrasiveBGBLL.SaveEntity(keyValue, entity, out msg);
                    _rawMaterialStockService.SaveEntity(stockEntity.Id, stockEntity, out msg);
                    _RawMaterialInBLL.SaveEntity(null, inEntity, out msg);
                    _AbrasiveOrderBLL.SaveEntity(entity.AbrasiveId, orderEntity, out msg);

                    if (plBOMItemList.Count() > 0)
                    {
                        _rawMaterialConsumeService.SaveEntity_List(false, "", mbConsumeRecordList, out msg);
                        _rawMaterialOutService.SaveEntity_List(false, "", rawMaterialOutList, out msg);
                        _rawMaterialStockService.SaveEntity_List(true, "", rawMaterialStockList, out msg);
                    }

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdatePM_AbrasiveBG")]
        public HttpResponseMessage UpdatePM_AbrasiveBG(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                //参数转实体
                PM_AbrasiveBGEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveBGEntity>(getValue(jo, "Entity"));

                var orderEntity = _AbrasiveOrderBLL.GetEntity(entity.AbrasiveId);
                if (orderEntity.OrderStatus == "3")
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_8");//工单已完成，无法报工！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //3130 JC-XB01
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }

                string msg = "";

                int isok = _AbrasiveBGBLL.SaveEntity(keyValue, entity, out msg);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_AbrasiveBG")]
        public HttpResponseMessage SaveBatchPM_AbrasiveBG(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_11");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_12");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");


                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _AbrasiveBGBLL.GetList("", out msg);
                //插入数组
                List<PM_AbrasiveBGEntity> Insert_entity_list = new List<PM_AbrasiveBGEntity>();
                //更新数组
                List<PM_AbrasiveBGEntity> Update_entity_list = new List<PM_AbrasiveBGEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_AbrasiveBGEntity entity = new PM_AbrasiveBGEntity();
                        //磨粉料工单Id
                        entity.AbrasiveId = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_13")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_13")];//磨粉料工单Id
                        //生成批次
                        entity.BatchNo = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_14")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_14")];//生成批次
                        //工序
                        entity.ProcessCode = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_15")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_15")];//工序
                        //生产机台
                        entity.Machine = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_16")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_16")];//生产机台
                        //班次
                        entity.Shift = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_17")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_17")];//班次
                        //报工数量
                        entity.BGQty = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_18")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_18")];//报工数量
                        //人员组别
                        entity.UserGroup = item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_19")] == null ? "" : item[Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_19")];//人员组别
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;


                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_AbrasiveBGEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
                        //判断旧列表中是否存在此编码
                        if (old_entity == null)
                        {
                            entity.Create();
                            //保存数组
                            Insert_entity_list.Add(entity);
                        }
                        else
                        {
                            entity.Id = old_entity.Id;
                            Update_entity_list.Add(entity);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (Insert_entity_list.Count > 0)
                {
                    //批量新增
                    isok = _AbrasiveBGBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _AbrasiveBGBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
                }

                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_AbrasiveBG")]
        public HttpResponseMessage DeletePM_AbrasiveBG(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_AbrasiveBGEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveBGEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_AbrasiveBGEntity model = _AbrasiveBGBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _AbrasiveBGBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_22");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_AbrasiveBG")]
        public HttpResponseMessage RemovePM_AbrasiveBG(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_AbrasiveBGEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveBGEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_AbrasiveBGEntity model = _AbrasiveBGBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _AbrasiveBGBLL.RemoveForm(Id, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_22");//删除操作成功
                else
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_24");//删除操作失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_AbrasiveBGEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _AbrasiveBGBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_25");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_AbrasiveBGEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _AbrasiveBGBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_25");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 通过N个字段拼写linq查询数组对象 参考
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="keyValue">条件值</param>
        /// <param name="keyValue2">条件值</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEntityByLinq")]
        public HttpResponseMessage GetEntityByLinq(string keyValue, string keyValue2)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _AbrasiveBGBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string OutMes = "";
                var list = _AbrasiveBGBLL.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个数据表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string OutMes = "";
                var list = _AbrasiveBGBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_AbrasiveBG_export")]
        public HttpResponseMessage PM_AbrasiveBG_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveBGController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();


                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _AbrasiveBGBLL.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
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


    }
}
