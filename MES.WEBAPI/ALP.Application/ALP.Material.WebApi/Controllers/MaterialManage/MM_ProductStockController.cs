using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.MaterialManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using System.Transactions;
using ALP.Application.WebApi.Common;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.PlanManage;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Service.ProduceManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Service.Resources;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;
using ALP.Application.Service.Material;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-10-11
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductStockController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 成品库存详表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_ProductStock")]
    public class MM_ProductStockController : ApiBaseController
    {
        private MM_ProductStock_Service _productStockService = new MM_ProductStock_Service();
        private MM_ProductIn_Service _productInService = new MM_ProductIn_Service(); //成品入库
        private MM_ProductOut_Service _productOutService = new MM_ProductOut_Service(); //成品出库
        private PL_WorkOrder_Service _plWorkOrderService = new PL_WorkOrder_Service();//工单
        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();
        private MMProductStockAdjustService _productStockAdjustService = new MMProductStockAdjustService();//库存校准
        private PM_PackingPrintMark_Service _markService = new PM_PackingPrintMark_Service();//唛头
        PL_ProductionOrderBLL _productionOrderBLL = new PL_ProductionOrderBLL();
        private Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();
        private Base_Material_Service _materialService = new Base_Material_Service();

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object _lockObject = new object();//调柜

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("MaterialManage.MM_ProductStockController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductStockPageList")]
        public HttpResponseMessage MM_ProductStockPageList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetPageList(pagination, queryJson);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductStockPageDataTableList")]
        public HttpResponseMessage MM_ProductStockPageDataTableList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetPageDataTableList(pagination, queryJson);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_ProductStockList")]
        public HttpResponseMessage GetMM_ProductStockList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_ProductStock")]
        public HttpResponseMessage SaveMM_ProductStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //if (string.IsNullOrEmpty(userCode))
            //{
            //result.success = false;
            //result.returnMsg = "用户名不能为空！";
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}

            try
            {
                //业务服务类
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                //参数转实体
                MM_ProductStockEntity entity = JsonConvert.DeserializeObject<MM_ProductStockEntity>(getValue(jo, "Entity"));
                //工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.FactoryCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_8");//工厂编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MarkCode))
                {
                    //唛头码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_9");//唛头码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ProductOrder))
                {
                    //订单号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_10");//订单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ContainerNO))
                {
                    //柜号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_11");//柜号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialCode))
                {
                    //客户型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_12");//客户型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CustomerPO))
                {
                    //客户PO号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_13");//客户PO号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WhsCode))
                {
                    //仓库编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_14");//仓库编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.LocationCode))
                {
                    //库位编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_15");//库位编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = CurrentAccount.UserCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.Creator = CurrentAccount.UserCode;
                    entity.CreateTime = DateTime.Now;
                }

                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg);
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

        #region 成品入库
        /// <summary>
        /// 功能描述: 成品入库
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_ProductStock")]
        public HttpResponseMessage SaveBatchMM_ProductStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var list = JsonConvert.DeserializeObject<List<MM_ProductStockEntity>>(getValue(jo, "Entity")); //库存记录
                var oldlist = JsonConvert.DeserializeObject<List<MM_ProductStockEntity>>(getValue(jo, "OldData"));
                var insetList = new List<MM_ProductStockEntity>();
                var updateList = new List<MM_ProductStockEntity>();
                List<MM_ProductInEntity> productInList = new List<MM_ProductInEntity>(); //入库记录
                List<MM_ProductOutEntity> productOutList = new List<MM_ProductOutEntity>();//出库记录

                var associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                int i = 1;
                foreach (var item in list)
                {
                    var entity = _productStockService.Get_ExpressionEntity(t => t.WorkOrder == item.WorkOrder && t.LocationCode == item.LocationCode);
                    if (entity != null)
                    {
                        //entity.PalletQty += item.PalletQty;
                        //entity.BoxQty += item.BoxQty;
                        entity.PieceQty += item.PieceQty;
                        entity.ModifyBy = userCode;
                        entity.ModifyTime = DateTime.Now;
                        updateList.Add(entity);
                    }
                    else
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.IsEnabled = true;
                        item.Creator = userCode;
                        item.CreateTime = DateTime.Now;
                        insetList.Add(item);
                    }

                    var productInEntity = new MM_ProductInEntity();
                    productInEntity.Create();
                    productInEntity.FactoryCode = item.FactoryCode;
                    productInEntity.FactoryName = item.FactoryName;
                    productInEntity.ProductOrder = item.ProductOrder;
                    //productInEntity.OrderType = item.OrderType;
                    productInEntity.WorkOrder = item.WorkOrder;
                    productInEntity.ContainerNO = item.ContainerNO;
                    productInEntity.MaterialCode = item.MaterialCode;
                    productInEntity.CustomerPO = item.CustomerPO;
                    productInEntity.WhsCode = item.WhsCode;
                    productInEntity.LocationCode = item.LocationCode;
                    //productInEntity.PalletQty = item.PalletQty;
                    //productInEntity.BoxQty = item.BoxQty;
                    productInEntity.PieceQty = item.PieceQty;
                    productInEntity.PerPalletBoxQty = item.PerPalletBoxQty;
                    productInEntity.PerPalletPieceQty = item.PerPalletPieceQty;
                    productInEntity.Creator = userCode;
                    productInEntity.CreateTime = DateTime.Now;
                    productInEntity.InType = "5";//成品入库
                    productInEntity.AssociateNo = associateNo + i.ToString().PadLeft(3, '0');
                    productInEntity.LineNum = item.LineNum;
                    productInList.Add(productInEntity);

                    i += 1;
                }
                foreach (var item in oldlist)
                {
                    if (item.PieceQty <= 0)
                        item.IsEnabled = false;
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                    updateList.Add(item);

                    var productInEntity = productInList.Find(t => t.WorkOrder == item.WorkOrder);

                    var productOutEntity = new MM_ProductOutEntity();
                    productOutEntity.Create();
                    productOutEntity.FactoryCode = item.FactoryCode;
                    productOutEntity.FactoryName = item.FactoryName;
                    productOutEntity.ProductOrder = item.ProductOrder;
                    //productInEntity.OrderType = item.OrderType;
                    productOutEntity.WorkOrder = item.WorkOrder;
                    productOutEntity.ContainerNO = item.ContainerNO;
                    productOutEntity.MaterialCode = item.MaterialCode;
                    productOutEntity.CustomerPO = item.CustomerPO;
                    productOutEntity.WhsCode = item.WhsCode;
                    productOutEntity.LocationCode = item.LocationCode;
                    //productOutEntity.PalletQty = item.PalletQty;
                    //productOutEntity.BoxQty = item.BoxQty;
                    productOutEntity.PieceQty = productInEntity.PieceQty;
                    productOutEntity.PerPalletBoxQty = item.PerPalletBoxQty;
                    productOutEntity.PerPalletPieceQty = item.PerPalletPieceQty;
                    productOutEntity.Creator = userCode;
                    productOutEntity.CreateTime = DateTime.Now;
                    productOutEntity.OutType = "1";//成品入库
                    productOutEntity.AssociateNo = productInEntity?.AssociateNo;
                    productOutEntity.LineNum = item.LineNum;
                    productOutList.Add(productOutEntity);
                }

                #region 同步SAP
                var factoryCode = productOutList.First().FactoryCode;

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    var arrMaterialCode = productOutList.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _materialService.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    foreach (var item in productOutList)
                    {
                        var materialEntity = materialList.Find(t => t.MaterialCode == item.MaterialCode);
                        var productInEntity = productInList.Find(t => t.AssociateNo == item.AssociateNo);

                        IF102_ITEM IF102_ITEM = new IF102_ITEM();
                        IF102_ITEM.BWART = "413";
                        IF102_ITEM.MATNR = materialEntity?.SAPMaterialCode ?? "";
                        IF102_ITEM.WERKS = item.FactoryCode ?? "";
                        IF102_ITEM.LGORT = item.LocationCode ?? "";
                        IF102_ITEM.ERFMG = item.PieceQty.ToString() ?? "";
                        IF102_ITEM.ERFME = materialEntity.Unit ?? "";
                        IF102_ITEM.KDAUF = item.ProductOrder ?? "";
                        IF102_ITEM.KDPOS = item.LineNum ?? "";
                        IF102_ITEM.SOBKZ = "E";
                        IF102_ITEM.UMWRK = productInEntity?.FactoryCode ?? "";
                        IF102_ITEM.UMLGO = productInEntity?.LocationCode ?? "";
                        IF102_ITEM.MAT_KDAUF = productInEntity.ProductOrder ?? "";
                        IF102_ITEM.MAT_KDPOS = productInEntity.LineNum ?? "";
                        IT_ITEM.Add(IF102_ITEM);
                    }
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    foreach (var item in productOutList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }

                    foreach (var item in productInList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }
                }
                #endregion

                string msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {

                    if (insetList.Count > 0) _productStockService.SaveEntity_List(false, userCode, insetList, out msg);
                    if (updateList.Count > 0) _productStockService.SaveEntity_List(true, userCode, updateList, out msg);
                    if (productInList.Count > 0) _productInService.SaveEntity_List(false, userCode, productInList, out msg);
                    if (productOutList.Count > 0) _productOutService.SaveEntity_List(false, userCode, productOutList, out msg);

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
        #endregion

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_ProductStock")]
        public HttpResponseMessage DeleteMM_ProductStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                MM_ProductStockEntity entity = JsonConvert.DeserializeObject<MM_ProductStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductStockEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, userName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_23");//删除操作成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_ProductStock")]
        public HttpResponseMessage RemoveMM_ProductStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                MM_ProductStockEntity entity = JsonConvert.DeserializeObject<MM_ProductStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductStockEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _Service.RemoveForm(Id, userName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_23");//删除操作成功
                else
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_25");//删除操作失败
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductStockEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_26");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductStockEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_26");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.IsEnabled == true).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                string OutMes = "";
                var list = _Service.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                string OutMes = "";
                var list = _Service.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_ProductStock_export")]
        public HttpResponseMessage MM_ProductStock_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _Service.GetList_export(CreatedByCode, out msg);

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


        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 包装周转库存查询(主)
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPackingTransferPageDataTableMainList")]
        public HttpResponseMessage GetPackingTransferPageDataTableMainList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetPackingTransferPageDataTableMainList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 包装周转库存查询(明细)
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPackingTransferPageDataTableDetailList")]
        public HttpResponseMessage GetPackingTransferPageDataTableDetailList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetPackingTransferPageDataTableDetailList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存查询(主)
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetProductStockPageDataTableMainList")]
        public HttpResponseMessage GetProductStockPageDataTableMainList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetProductStockPageDataTableMainList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存查询(明细)
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetProductStockPageDataTableDetailList")]
        public HttpResponseMessage GetProductStockPageDataTableDetailList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetProductStockPageDataTableDetailList(pagination, queryJson);
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

        #region 成品库存管理-移库
        /// <summary>
        /// 功能描述: 移库
        /// 创　　建: admin
        /// 创建日期: 2021-08-20 15:42:02
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("ProductStockMove")]
        public HttpResponseMessage ProductStockMove(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                var keyValue = getValue(jo, "KeyValue");
                var entity = JsonConvert.DeserializeObject<dynamic>(getValue(jo, "Entity"));
                var associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");//移库关联

                #region 1、更新原库位库存
                var sourceEntity = _productStockService.GetEntity(keyValue);
                //sourceEntity.PalletQty -= Convert.ToDecimal(entity.TargetPalletQty);
                //sourceEntity.BoxQty -= Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletBoxQty;
                //sourceEntity.PieceQty -= Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletPieceQty;
                sourceEntity.PieceQty -= Convert.ToDecimal(entity.TargetPieceQty);
                sourceEntity.ModifyBy = userCode;
                sourceEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 2.更新目标库位库存
                MM_ProductStockEntity newEntity = new MM_ProductStockEntity();
                string targetLocationCode = entity.TargetLocationCode;
                var targetEntity = _productStockService.Get_ExpressionEntity(t => t.WhsCode == sourceEntity.WhsCode
                    && t.LocationCode == targetLocationCode && t.WorkOrder == sourceEntity.WorkOrder);
                if (targetEntity == null) //不存在新增
                {
                    newEntity = PubFunction.DeepCopyByReflection(sourceEntity);
                    newEntity.Id = Guid.NewGuid().ToString();
                    newEntity.LocationCode = targetLocationCode;
                    //newEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                    //newEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletBoxQty;
                    //newEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletPieceQty;
                    newEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                    newEntity.Creator = userCode;
                    newEntity.CreateTime = DateTime.Now;
                    newEntity.ModifyBy = userCode;
                    newEntity.ModifyTime = DateTime.Now;
                }
                else
                {
                    //targetEntity.PalletQty += Convert.ToDecimal(entity.TargetPalletQty);
                    //targetEntity.BoxQty += Convert.ToDecimal(entity.TargetPalletQty) * targetEntity.PerPalletBoxQty;
                    //targetEntity.PieceQty += Convert.ToDecimal(entity.TargetPalletQty) * targetEntity.PerPalletPieceQty;
                    targetEntity.PieceQty += Convert.ToDecimal(entity.TargetPieceQty);
                    targetEntity.ModifyBy = userCode;
                    targetEntity.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 3.新增出库记录
                MM_ProductOutEntity outEntity = new MM_ProductOutEntity();
                outEntity.Id = Guid.NewGuid().ToString();
                outEntity.FactoryCode = sourceEntity.FactoryCode;
                outEntity.FactoryName = sourceEntity.FactoryName;
                outEntity.ProductOrder = sourceEntity.ProductOrder;
                outEntity.WorkOrder = sourceEntity.WorkOrder;
                outEntity.ContainerNO = sourceEntity.ContainerNO;
                outEntity.MaterialCode = sourceEntity.MaterialCode;
                outEntity.CustomerPO = sourceEntity.CustomerPO;
                outEntity.WhsCode = sourceEntity.WhsCode;
                outEntity.LocationCode = sourceEntity.LocationCode;
                outEntity.OutType = "2";
                outEntity.PerPalletBoxQty = sourceEntity.PerPalletBoxQty;
                outEntity.PerPalletPieceQty = sourceEntity.PerPalletPieceQty;
                //outEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                //outEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * outEntity.PerPalletBoxQty;
                //outEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * outEntity.PerPalletPieceQty;
                outEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                outEntity.Creator = userCode;
                outEntity.CreateTime = DateTime.Now;
                outEntity.AssociateNo = associateNo;//移库关联
                outEntity.LineNum = sourceEntity.LineNum;
                #endregion

                #region 4.新增入库记录
                MM_ProductInEntity inEntity = new MM_ProductInEntity();
                inEntity.FactoryCode = sourceEntity.FactoryCode;
                inEntity.FactoryName = sourceEntity.FactoryName;
                inEntity.Id = Guid.NewGuid().ToString();
                inEntity.ProductOrder = sourceEntity.ProductOrder;
                inEntity.WorkOrder = sourceEntity.WorkOrder;
                inEntity.ContainerNO = sourceEntity.ContainerNO;
                inEntity.MaterialCode = sourceEntity.MaterialCode;
                inEntity.CustomerPO = sourceEntity.CustomerPO;
                inEntity.WhsCode = sourceEntity.WhsCode;
                inEntity.LocationCode = entity.TargetLocationCode;
                inEntity.InType = "2";
                inEntity.PerPalletBoxQty = sourceEntity.PerPalletBoxQty;
                inEntity.PerPalletPieceQty = sourceEntity.PerPalletPieceQty;
                //inEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                //inEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletBoxQty;
                //inEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletPieceQty;
                inEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                inEntity.Creator = userCode;
                inEntity.CreateTime = DateTime.Now;
                inEntity.AssociateNo = associateNo;//移库关联
                inEntity.LineNum = sourceEntity.LineNum;
                #endregion

                #region 同步SAP
                var factoryCode = outEntity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    var materialEntity = _materialService.Get_ExpressionEntity(t => t.MaterialCode == outEntity.MaterialCode);

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
                    IF102_ITEM.BWART = "413";
                    IF102_ITEM.MATNR = materialEntity?.SAPMaterialCode ?? "";
                    IF102_ITEM.WERKS = outEntity.FactoryCode ?? "";
                    IF102_ITEM.LGORT = outEntity.LocationCode ?? "";
                    IF102_ITEM.ERFMG = outEntity.PieceQty.ToString() ?? "";
                    IF102_ITEM.ERFME = materialEntity.Unit ?? "";
                    IF102_ITEM.KDAUF = outEntity.ProductOrder ?? "";
                    IF102_ITEM.KDPOS = outEntity.LineNum ?? "";
                    IF102_ITEM.SOBKZ = "E";
                    IF102_ITEM.UMWRK = inEntity?.FactoryCode ?? "";
                    IF102_ITEM.UMLGO = inEntity?.LocationCode ?? "";
                    IF102_ITEM.MAT_KDAUF = inEntity.ProductOrder ?? "";
                    IF102_ITEM.MAT_KDPOS = inEntity.LineNum ?? "";
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

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productStockService.SaveEntity(sourceEntity.Id, sourceEntity, out msg);

                    if (targetEntity == null) _productStockService.SaveEntity("", newEntity, out msg);
                    else _productStockService.SaveEntity(targetEntity.Id, targetEntity, out msg);

                    _productOutService.SaveEntity("", outEntity, out msg);
                    _productInService.SaveEntity("", inEntity, out msg);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_30");//移库成功
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
        #endregion

        #region 成品库存管理-调柜获取订单
        /// <summary>
        /// 功能描述: 成品库存管理-调柜获取订单号
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetProductOrderPageDataTableList")]
        public HttpResponseMessage GetProductOrderPageDataTableList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetProductOrderPageDataTableList(pagination, queryJson);
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

        #region 成品库存管理-调柜获取柜号
        /// <summary>
        /// 功能描述: 成品库存管理-调柜获取柜号
        /// 创　　建: admin
        /// 创建日期: 2021-10-11 21:42:27
        /// 任务编号: 成品库存详表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetContainerNOPageDataTableList")]
        public HttpResponseMessage GetContainerNOPageDataTableList(JObject jo)
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
                MM_ProductStock_Service _Service = new MM_ProductStock_Service();
                var data = _Service.GetContainerNOPageDataTableList(pagination, queryJson);
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

        #region 成品库存管理-调柜
        /// <summary>
        /// 功能描述: 成品库存管理-调柜
        /// 创　　建: admin
        /// 创建日期: 2021-08-20 15:42:02
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("ProductStockMove2")]
        public HttpResponseMessage ProductStockMove2(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //加锁
                lock (_lockObject)
                {
                    //业务服务层
                    var keyValue = getValue(jo, "KeyValue");
                    var entity = JsonConvert.DeserializeObject<dynamic>(getValue(jo, "Entity"));
                    var associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");//调柜关联

                    #region 1、原订单、柜号、客户型号出库
                    var sourceEntity = _productStockService.GetEntity(keyValue);
                    if (sourceEntity == null)
                    {
                        return AjaxResult(false, "原订单不存在");
                    }
                    //sourceEntity.PalletQty -= Convert.ToDecimal(entity.TargetPalletQty);
                    //sourceEntity.BoxQty = sourceEntity.PalletQty * sourceEntity.PerPalletBoxQty;
                    sourceEntity.PieceQty -= Convert.ToDecimal(entity.TargetPieceQty);
                    sourceEntity.ModifyBy = userCode;
                    sourceEntity.ModifyTime = DateTime.Now;
                    #endregion

                    #region 2.目标订单、柜号、客户型号入库

                    MM_ProductStockEntity newEntity = null;
                    string factoryCode = entity.FactoryCode;
                    string targetProductOrder = entity.TargetProductOrder;
                    string targetContainerNO = entity.TargetContainerNO;
                    string targetMaterialCode = entity.MaterialCode;
                    string targetWorkOrder = entity.TargetWorkOrder;

                    string[] arrWorkOrderType = new string[] { "1", "4" };
                    //正常工单
                    //var plWorkOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.ProductOrder == targetProductOrder
                    //     && t.ContainerNO == targetContainerNO && t.MaterialCode == targetMaterialCode && arrWorkOrderType.Contains(t.WorkOrderType)
                    //     && t.FactoryCode == factoryCode);
                    //改成用工单号直接获取
                    var plWorkOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.FactoryCode == factoryCode 
                        && t.WorkOrder == targetWorkOrder && t.IsEnabled == true);
                    if (plWorkOrderEntity == null)
                        return AjaxResult(false, Language.GetText("MaterialManage.MM_ProductStockController.Tips_31", targetProductOrder, targetContainerNO, targetMaterialCode));//找不到工单：订单[{targetProductOrder}]、柜号[{targetContainerNO}]、客户型号[{targetMaterialCode}]

                    //订单
                    var productOrderEntity = _productionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == plWorkOrderEntity.ProductOrder);

                    // 工单物料主数据
                    var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == plWorkOrderEntity.WorkOrder
                        && t.MaterialCode == plWorkOrderEntity.MaterialCode && t.IsDeleted == false);
                    //工单物料属性
                    var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id).ToList();
                    var perPallerBox = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZTPSL")?.AttrValue.ToDecimalOrNull();
                    if (perPallerBox != sourceEntity.PerPalletBoxQty)
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_32");//单托盒数不一致，无法调柜
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //调入单托片数
                    var perPalletPieceQty = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "CPBZTPSL")?.AttrValue.ToDecimalOrNull();
                    if (perPalletPieceQty == null)
                        return AjaxResult(false, "包装单托片数属性不能为空");

                    //校验目标工单是否有库存
                    string targetWhsCode = entity.TargetWhsCode;
                    string targetLocationCode = entity.TargetLocationCode;
                    var targetStockEntity = _productStockService.Get_ExpressionEntity(t => t.WorkOrder == plWorkOrderEntity.WorkOrder
                        && t.WhsCode == targetWhsCode && t.LocationCode == targetLocationCode);
                    if (targetStockEntity == null) //不存在新增
                    {
                        if (entity.TargetPieceQty > plWorkOrderEntity.OrderPieces)
                        {
                            result.success = false;
                            //result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_33");//不能超过目标订单总托数
                            result.returnMsg = "不能超过目标订单总片数";
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        newEntity = new MM_ProductStockEntity();
                        newEntity.Id = Guid.NewGuid().ToString();
                        newEntity.FactoryCode = plWorkOrderEntity.FactoryCode;
                        newEntity.FactoryName = plWorkOrderEntity.FactoryName;
                        newEntity.ProductOrder = plWorkOrderEntity.ProductOrder;
                        newEntity.WorkOrder = plWorkOrderEntity.WorkOrder;
                        newEntity.ContainerNO = plWorkOrderEntity.ContainerNO;
                        newEntity.MaterialCode = plWorkOrderEntity.MaterialCode;
                        newEntity.CustomerPO = plWorkOrderEntity.CustomerPO;
                        newEntity.WhsCode = targetWhsCode;
                        newEntity.LocationCode = targetLocationCode;
                        newEntity.PerPalletBoxQty = perPallerBox;
                        newEntity.PerPalletPieceQty = perPalletPieceQty;
                        //newEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                        //newEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * perPallerBox;
                        //newEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletPieceQty;
                        newEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                        newEntity.IsEnabled = true;
                        newEntity.Creator = userCode;
                        newEntity.CreateTime = DateTime.Now;
                        newEntity.ModifyBy = userCode;
                        newEntity.ModifyTime = DateTime.Now;
                        newEntity.LineNum = plWorkOrderEntity.Orderline;
                    }
                    else
                    {
                        //targetEntity.PalletQty = (targetEntity.PalletQty ?? 0) + Convert.ToDecimal(entity.TargetPalletQty);
                        targetStockEntity.PieceQty = (targetStockEntity.PieceQty ?? 0) + Convert.ToDecimal(entity.TargetPieceQty);
                        if (targetStockEntity.PieceQty > plWorkOrderEntity.OrderPieces)
                        {
                            result.success = false;
                            //result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_33");//不能超过目标订单总托数
                            result.returnMsg = "不能超过目标订单总片数";
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        //targetEntity.BoxQty = (targetEntity.BoxQty ?? 0) + Convert.ToDecimal(entity.TargetPalletQty) * perPallerBox;
                        //targetEntity.PieceQty += Convert.ToDecimal(entity.TargetPalletQty) * targetEntity.PerPalletPieceQty;
                        targetStockEntity.ModifyBy = userCode;
                        targetStockEntity.ModifyTime = DateTime.Now;
                    }
                    #endregion

                    #region 3.新增出库记录
                    MM_ProductOutEntity outEntity = new MM_ProductOutEntity();
                    outEntity.Id = Guid.NewGuid().ToString();
                    outEntity.FactoryCode = sourceEntity.FactoryCode;
                    outEntity.FactoryName = sourceEntity.FactoryName;
                    outEntity.ProductOrder = sourceEntity.ProductOrder;
                    outEntity.WorkOrder = sourceEntity.WorkOrder;
                    outEntity.ContainerNO = sourceEntity.ContainerNO;
                    outEntity.MaterialCode = sourceEntity.MaterialCode;
                    outEntity.CustomerPO = sourceEntity.CustomerPO;
                    outEntity.WhsCode = sourceEntity.WhsCode;
                    outEntity.LocationCode = sourceEntity.LocationCode;
                    outEntity.OutType = "3";//调柜
                    outEntity.PerPalletBoxQty = sourceEntity.PerPalletBoxQty;
                    outEntity.PerPalletPieceQty = sourceEntity.PerPalletPieceQty;
                    //outEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                    //outEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletBoxQty;
                    //outEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * outEntity.PerPalletPieceQty;
                    outEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                    outEntity.Creator = userCode;
                    outEntity.CreateTime = DateTime.Now;
                    outEntity.AssociateNo = associateNo;//调柜关联
                    outEntity.LineNum = sourceEntity.LineNum;
                    #endregion

                    #region 4.新增入库记录
                    MM_ProductInEntity inEntity = new MM_ProductInEntity();
                    inEntity.Id = Guid.NewGuid().ToString();
                    inEntity.FactoryCode = plWorkOrderEntity.FactoryCode;
                    inEntity.FactoryName = plWorkOrderEntity.FactoryName;
                    inEntity.ProductOrder = plWorkOrderEntity.ProductOrder;
                    inEntity.WorkOrder = plWorkOrderEntity.WorkOrder;
                    inEntity.ContainerNO = plWorkOrderEntity.ContainerNO;
                    inEntity.MaterialCode = plWorkOrderEntity.MaterialCode;
                    inEntity.CustomerPO = plWorkOrderEntity.CustomerPO;
                    inEntity.WhsCode = targetWhsCode;
                    inEntity.LocationCode = targetLocationCode;
                    inEntity.InType = "3";//调柜
                    inEntity.PerPalletBoxQty = perPallerBox;
                    inEntity.PerPalletPieceQty = sourceEntity.PerPalletPieceQty;
                    //inEntity.PalletQty = Convert.ToDecimal(entity.TargetPalletQty);
                    //inEntity.BoxQty = Convert.ToDecimal(entity.TargetPalletQty) * perPallerBox;
                    //inEntity.PieceQty = Convert.ToDecimal(entity.TargetPalletQty) * sourceEntity.PerPalletPieceQty;
                    inEntity.PieceQty = Convert.ToDecimal(entity.TargetPieceQty);
                    inEntity.Creator = userCode;
                    inEntity.CreateTime = DateTime.Now;
                    inEntity.AssociateNo = associateNo;//调柜关联
                    inEntity.LineNum = plWorkOrderEntity.Orderline;
                    #endregion

                    #region 5、删除旧唛头
                    //int targetPaletQty = Convert.ToInt32(entity.TargetPalletQty);
                    int targetPieceQty = Convert.ToInt32(entity.TargetPieceQty);//调入片数

                    int targetPalletQty = targetPieceQty / (int)perPalletPieceQty;
                    if (targetPieceQty % (int)perPalletPieceQty > 0)
                    {
                        targetPalletQty += 1;
                    }
                    var sourceMarkList = _markService.Get_ExpressionList(t => t.WorkOrder == sourceEntity.WorkOrder)
                        .OrderByDescending(t => Convert.ToInt32(t.Mark.Substring(t.Mark.IndexOf('-') + 1))).Take(targetPalletQty).ToList();
                    #endregion

                    #region 6、生成新唛头
                    List<PM_PackingPrintMarkEntity> markList = new List<PM_PackingPrintMarkEntity>();

                    var targetMarkList = _markService.Get_ExpressionList(t => t.WorkOrder == plWorkOrderEntity.WorkOrder);
                    var targetExistMarkCount = targetMarkList.Count();//已生成唛头数量
                    if (targetExistMarkCount >= plWorkOrderEntity.OrderPallet)
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_36");//唛头数量已足够，无需调柜
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    //本次调入片数+库存片数
                    decimal? targetTotalPieceQty = 0;
                    if (targetStockEntity == null)
                    {
                        targetTotalPieceQty = targetPieceQty;
                    }
                    else
                    {
                        targetTotalPieceQty = targetStockEntity.PieceQty;
                    }

                    var time = DateTime.Now;
                    if (targetTotalPieceQty < plWorkOrderEntity.OrderPieces)
                    {
                        //计算需要生成的唛头数量
                        var needPalletCount = (int)(targetTotalPieceQty - targetExistMarkCount * perPalletPieceQty) / (int)perPalletPieceQty;

                        //需要生成的随机数数量=生成唛头数量
                        string returnNum = string.Empty;
                        _markService.GetSerialNO("PackingPrintMark", needPalletCount, out returnNum, out msg);
                        var index = Int32.Parse(returnNum);

                        for (int i = 1; i <= needPalletCount; i++)
                        {
                            var markEntity = new PM_PackingPrintMarkEntity();
                            markEntity.Id = Guid.NewGuid().ToString();
                            markEntity.FactoryCode = plWorkOrderEntity.FactoryCode;
                            markEntity.FactoryName = plWorkOrderEntity.FactoryName;
                            markEntity.PackTransferCode = "M" + time.ToString("yyMMddHHmmss") + (index++).ToString().PadLeft(4, '0');
                            markEntity.Mark = plWorkOrderEntity.OrderWholePallet + "-" + (plWorkOrderEntity.OrderStartPallet + targetExistMarkCount + (i - 1));
                            markEntity.ProductOrder = plWorkOrderEntity.ProductOrder;
                            markEntity.WorkOrder = plWorkOrderEntity.WorkOrder;
                            markEntity.Customer = productOrderEntity.Customer;
                            markEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                            markEntity.ContainerNO = plWorkOrderEntity.ContainerNO;
                            markEntity.CustomerPO = plWorkOrderEntity.CustomerPO;
                            //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                            markEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";
                            markEntity.PrintStatus = "1";//未打印
                            markEntity.Creator = userCode;
                            markEntity.CreateTime = time;
                            markEntity.WorkOrderType = plWorkOrderEntity.WorkOrderType;
                            markEntity.Status = "1";//待入库
                            markEntity.BoxDate = (productOrderEntity.BoxDate == null ? time.ToString("ddMMyy") : productOrderEntity.BoxDate.Value.ToString("ddMMyy")) + "A";
                            markEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                            //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                            markEntity.PieceQty = perPalletPieceQty;
                            markList.Add(markEntity);
                        }
                    }
                    else if (targetTotalPieceQty == plWorkOrderEntity.OrderPieces)
                    {
                        //计算需要生成的唛头数量
                        var needPalletCount = (int)plWorkOrderEntity.DeliveryPallet - (int)targetExistMarkCount;

                        //需要生成的随机数数量=生成唛头数量
                        string returnNum = string.Empty;
                        _markService.GetSerialNO("PackingPrintMark", needPalletCount, out returnNum, out msg);
                        var index = Int32.Parse(returnNum);

                        decimal? totalPieceQty = 0;
                        for (int i = 1; i <= needPalletCount; i++)
                        {
                            var markEntity = new PM_PackingPrintMarkEntity();
                            markEntity.Id = Guid.NewGuid().ToString();
                            markEntity.FactoryCode = plWorkOrderEntity.FactoryCode;
                            markEntity.FactoryName = plWorkOrderEntity.FactoryName;
                            markEntity.PackTransferCode = "M" + time.ToString("yyMMddHHmmss") + (index++).ToString().PadLeft(4, '0');
                            markEntity.Mark = plWorkOrderEntity.OrderWholePallet + "-" + (plWorkOrderEntity.OrderStartPallet + targetExistMarkCount + (i - 1));
                            markEntity.ProductOrder = plWorkOrderEntity.ProductOrder;
                            markEntity.WorkOrder = plWorkOrderEntity.WorkOrder;
                            markEntity.Customer = productOrderEntity.Customer;
                            markEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                            markEntity.ContainerNO = plWorkOrderEntity.ContainerNO;
                            markEntity.CustomerPO = plWorkOrderEntity.CustomerPO;
                            //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                            markEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";
                            markEntity.PrintStatus = "1";//未打印
                            markEntity.Creator = userCode;
                            markEntity.CreateTime = time;
                            markEntity.WorkOrderType = plWorkOrderEntity.WorkOrderType;
                            markEntity.Status = "1";//待入库
                            markEntity.BoxDate = (productOrderEntity.BoxDate == null ? time.ToString("ddMMyy") : productOrderEntity.BoxDate.Value.ToString("ddMMyy")) + "A";
                            markEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                            //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                            if (i == needPalletCount)
                            {
                                markEntity.PieceQty = plWorkOrderEntity.OrderPieces - (totalPieceQty + targetExistMarkCount * perPalletPieceQty);
                            }
                            else
                            {
                                markEntity.PieceQty = perPalletPieceQty;
                                totalPieceQty += perPalletPieceQty;
                            }
                            markList.Add(markEntity);
                        }
                    }
                    else
                    {
                        return AjaxResult(false, "调入总片数不能超过订单片数");
                    }
                    #endregion

                    #region 同步SAP%

                    var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                         && t.Remark1 == factoryCode);
                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        var materialEntity = _materialService.Get_ExpressionEntity(t => t.MaterialCode == outEntity.MaterialCode);

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
                        IF102_ITEM.BWART = "413";
                        IF102_ITEM.MATNR = materialEntity?.SAPMaterialCode ?? "";
                        IF102_ITEM.WERKS = outEntity.FactoryCode ?? "";
                        IF102_ITEM.LGORT = outEntity.LocationCode ?? "";
                        IF102_ITEM.ERFMG = outEntity.PieceQty.ToString() ?? "";
                        IF102_ITEM.ERFME = materialEntity.Unit ?? "";
                        IF102_ITEM.KDAUF = inEntity.ProductOrder ?? "";
                        IF102_ITEM.KDPOS = inEntity.LineNum ?? "";
                        IF102_ITEM.SOBKZ = "E";
                        IF102_ITEM.UMWRK = inEntity?.FactoryCode ?? "";
                        IF102_ITEM.UMLGO = inEntity?.LocationCode ?? "";
                        IF102_ITEM.MAT_KDAUF = outEntity.ProductOrder ?? "";
                        IF102_ITEM.MAT_KDPOS = outEntity.LineNum ?? "";
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

                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    // 设置事务超时时间为60秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 60);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _productStockService.SaveEntity(sourceEntity.Id, sourceEntity, out msg);

                        if (targetStockEntity == null)
                        {
                            _productStockService.SaveEntity("", newEntity, out msg);
                        }
                        else
                        {
                            _productStockService.SaveEntity(targetStockEntity.Id, targetStockEntity, out msg);
                        }
                        _productOutService.SaveEntity("", outEntity, out msg);
                        _productInService.SaveEntity("", inEntity, out msg);

                        var arrId = sourceMarkList.Select(t => t.Id).ToArray();
                        _markService.RemoveForm(t => arrId.Contains(t.Id));
                        _markService.SaveEntity_List(false, userName, markList, out msg);

                        ts.Complete();
                    }
                }
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_37");//调柜成功
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
        #endregion

        #region 库存校准保存
        /// <summary>
        ///功能描述:  库存校准保存）
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("ProductStockAdjustSave")]
        public HttpResponseMessage ProductStockAdjustSave(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");
            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<MMProductStockAdjustEntity>(entityStr);

                #region 库存信息
                var stockEntity = _productStockService.Get_ExpressionEntity(t => t.Id == keyValue);
                //stockEntity.PalletQty = entity.AdjustPalletQty;
                //stockEntity.BoxQty = entity.AdjustBoxQty;
                stockEntity.PieceQty = entity.AdjustPieceQty;
                stockEntity.ModifyBy = userCode;
                stockEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 库存校准记录
                entity.Id = Guid.NewGuid().ToString();
                entity.Creator = userCode;
                entity.CreatorName = userName;
                entity.CreatorName = userName;
                entity.CreateTime = DateTime.Now;
                entity.ModifyBy = userCode;
                entity.ModifyByName = userName;
                entity.ModifyTime = DateTime.Now;
                #endregion

                #region 出入库记录
                MM_ProductInEntity productInEntity = null;
                MM_ProductOutEntity productOutEntity = null;

                //var offsetPalletQty = entity.AdjustPalletQty - entity.PalletQty;
                //var offsetBoxQty = entity.AdjustBoxQty - entity.BoxQty;
                var offsetPieceQty = entity.AdjustPieceQty - entity.PieceQty;
                if (offsetPieceQty > 0)
                {
                    productInEntity = new MM_ProductInEntity();
                    productInEntity.Id = Guid.NewGuid().ToString();
                    productInEntity.FactoryCode = stockEntity.FactoryCode;
                    productInEntity.FactoryName = stockEntity.FactoryName;
                    productInEntity.ProductOrder = stockEntity.ProductOrder;
                    productInEntity.WorkOrder = stockEntity.WorkOrder;
                    productInEntity.ContainerNO = stockEntity.ContainerNO;
                    productInEntity.MaterialCode = stockEntity.MaterialCode;
                    productInEntity.CustomerPO = stockEntity.CustomerPO;
                    productInEntity.WhsCode = stockEntity.WhsCode;
                    productInEntity.LocationCode = stockEntity.LocationCode;
                    //productInEntity.PalletQty = offsetPalletQty;
                    //productInEntity.BoxQty = offsetBoxQty;
                    productInEntity.PieceQty = offsetPieceQty;
                    productInEntity.PerPalletBoxQty = stockEntity.PerPalletBoxQty;
                    productInEntity.InType = "4";
                    productInEntity.Creator = userCode;
                    productInEntity.CreateTime = DateTime.Now;
                }
                else
                {
                    productOutEntity = new MM_ProductOutEntity();
                    productOutEntity.Id = Guid.NewGuid().ToString();
                    productOutEntity.FactoryCode = stockEntity.FactoryCode;
                    productOutEntity.FactoryName = stockEntity.FactoryName;
                    productOutEntity.ProductOrder = stockEntity.ProductOrder;
                    productOutEntity.WorkOrder = stockEntity.WorkOrder;
                    productOutEntity.ContainerNO = stockEntity.ContainerNO;
                    productOutEntity.MaterialCode = stockEntity.MaterialCode;
                    productOutEntity.CustomerPO = stockEntity.CustomerPO;
                    productOutEntity.WhsCode = stockEntity.WhsCode;
                    productOutEntity.LocationCode = stockEntity.LocationCode;
                    //productOutEntity.PalletQty = Math.Abs(offsetPalletQty.Value);
                    //productOutEntity.BoxQty = Math.Abs(offsetBoxQty.Value);
                    productOutEntity.PieceQty = Math.Abs(offsetPieceQty.Value);
                    productOutEntity.PerPalletBoxQty = stockEntity.PerPalletBoxQty;
                    productOutEntity.OutType = "4";
                    productOutEntity.Creator = userCode;
                    productOutEntity.CreateTime = DateTime.Now;
                }

                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productStockService.SaveEntity(stockEntity.Id, stockEntity, out msg);
                    _productStockAdjustService.SaveEntity("", entity);

                    if (productOutEntity != null)
                        _productOutService.SaveEntity("", productOutEntity, out msg);

                    if (productInEntity != null)
                        _productInService.SaveEntity("", productInEntity, out msg);

                    ts.Complete();
                }
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}
