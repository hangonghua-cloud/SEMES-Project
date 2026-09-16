using System;
using System.Text;
using System.Collections.Generic;
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
using System.Transactions;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.Material;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// [MM_RawMaterialDispatchDetail]控制器
    /// 描述:MM_原材料半成品发货详情
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    [Auth]
    [RoutePrefix("MMRawMaterialDispatchDetail")]
    public class MMRawMaterialDispatchDetailController : ApiBaseController
    {
        private MMRawMaterialDispatchDetailService _MMRawMaterialDispatchDetailService = new MMRawMaterialDispatchDetailService();
        MMRawMaterialDispatchService _rawDispatchService = new MMRawMaterialDispatchService();
        MMRawMaterialDispatchSubService _rawDispatchSubService = new MMRawMaterialDispatchSubService();
        MM_RawMaterialStock_Service _rawStockService = new MM_RawMaterialStock_Service();
        MM_RawMaterialOut_Service _rawOutService = new MM_RawMaterialOut_Service();
        MM_RawMaterialIn_Service _rawInService = new MM_RawMaterialIn_Service();
        private Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();
        private Base_Material_Service _materialService = new Base_Material_Service();

        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货详情
        ///</summary>
        ///<param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableList")]
        public HttpResponseMessage GetPageDataTableList(JObject jo)
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
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var data = _MMRawMaterialDispatchDetailService.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_1");//操作成功！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货详情
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值</param>
        ///<returns></returns>
        [HttpPost]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                //var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchDetailEntity>(entityStr)

                result.resultData = _MMRawMaterialDispatchDetailService.GetEntity(t => t.Id == keyValue);
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_1");//操作成功！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货详情
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_4");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");
            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchDetailEntity>(entityStr);

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyByCode = userCode;
                    entity.ModifyByName = userName;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.CreateByCode = userCode;
                    entity.CreateByName = userName;
                    entity.IsDeleted = false;
                    entity.CreateTime = DateTime.Now;
                }

                int isok = _MMRawMaterialDispatchDetailService.SaveEntity(keyValue, entity);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_6");//操作失败：
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 批量保存
        /// <summary>
        ///功能描述:  批量保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货详情
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveBatchForm")]
        public HttpResponseMessage SaveBatchForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var isUpdate = false;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_4");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");

            try
            {
                var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchDetailEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<MMRawMaterialDispatchDetailEntity>>(getValue(jo, "data"));

                if (!string.IsNullOrEmpty(keyValue))
                {
                    isUpdate = true;
                    //entity.ModifyBy = userCode;
                    //entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    //entity.Creator = userCode;
                    //entity.CreateTime = DateTime.Now;
                }

                foreach (var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                int isok = _MMRawMaterialDispatchDetailService.SaveEntity_List(isUpdate, list);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_6");//操作失败：
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货详情
        ///</summary>
        ///<param name="jo">json参数, 包含entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchDetailEntity>(entityStr);

                int isok = _MMRawMaterialDispatchDetailService.RemoveForm(t => t.Id == entity.Id);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_6");//操作失败：
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 原材料发货
        /// <summary>
        ///功能描述:  发货
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("Delivery")]
        public HttpResponseMessage Delivery(JObject jo)
        {
            try
            {
                List<MM_RawMaterialStockEntity> lstRawMaterialStock = new List<MM_RawMaterialStockEntity>();
                List<MM_RawMaterialOutEntity> lstRawMaterialOut = new List<MM_RawMaterialOutEntity>();

                var dispatchEntity = JsonConvert.DeserializeObject<MMRawMaterialDispatchEntity>(getValue(jo, "entity"));
                var data = JsonConvert.DeserializeObject<List<MMRawMaterialDispatchDetailEntity>>(getValue(jo, "data"));
                var deliveryDetailList = data.Where(t => t.DeliveryQty != null).ToList();
                if (deliveryDetailList.Count == 0)
                    return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_7"));//没有要保存的行！

                #region 发货单信息
                //更新发货子表行状态
                var subId = deliveryDetailList.FirstOrDefault().DispatchSubId;
                var dispatchSubEntity = _rawDispatchSubService.GetEntity(t => t.Id == subId);
                dispatchSubEntity.SubStatus = "3"; //已完成

                //更新发货主表状态
                var deliveryNo = deliveryDetailList.FirstOrDefault().DeliveryNo;
                //var dispatchEntity = _rawDispatchService.GetEntity(t => t.DeliveryNo == deliveryNo);

                var flag = _rawDispatchSubService.GetList(t => t.DeliveryNo == deliveryNo && t.SubStatus != "3"
                    && t.Id != dispatchSubEntity.Id).Any();
                if (flag)
                {
                    dispatchEntity.Status = "2"; //发货中
                }
                else
                {
                    dispatchEntity.Status = "3"; //已完成
                    //dispatchEntity.PostDate = DateTime.Now; //前端已赋值
                }
                dispatchEntity.ModifyByCode = CurrentAccount.UserCode;
                dispatchEntity.ModifyByName = CurrentAccount.UserName;
                dispatchEntity.ModifyTime = DateTime.Now;
                dispatchEntity.PostMark = "X";
                dispatchEntity.DeliveryUserCode = CurrentAccount.UserCode;
                dispatchEntity.DeliveryUserName = CurrentAccount.UserName;
                dispatchEntity.ActualDeliveryTime = DateTime.Now;
                #endregion

                foreach (var item in deliveryDetailList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.CreateByCode = CurrentAccount.UserCode;
                    item.CreateByName = CurrentAccount.UserName;
                    item.CreateTime = DateTime.Now;
                    item.IsDeleted = false;
                    item.LineNum = dispatchSubEntity.LineNum;
                    item.ProductOrder = dispatchSubEntity.ProductOrder;
                    item.ProductLine = dispatchSubEntity.ProductLine;

                    #region 库存
                    var stockEntity = _rawStockService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                        && t.LocationCode == item.LocationCode && t.BatchNo == item.BatchNo);
                    if (stockEntity == null)
                        return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_8"));//库存不足！

                    stockEntity.Qty -= item.DeliveryQty;
                    stockEntity.ModifyBy = CurrentAccount.UserCode;
                    stockEntity.ModifyTime = DateTime.Now;
                    lstRawMaterialStock.Add(stockEntity);
                    #endregion

                    #region 出库记录
                    var rawOutEntity = new MM_RawMaterialOutEntity();
                    rawOutEntity.Id = Guid.NewGuid().ToString();
                    rawOutEntity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rawOutEntity.BusinessId = item.Id;
                    rawOutEntity.BusinessTable = "MM_RawMaterialDispatchDetail";
                    rawOutEntity.FactoryCode = dispatchEntity.FactoryCode;
                    rawOutEntity.FactoryName = dispatchEntity.FactoryName;
                    rawOutEntity.WhsCode = item.WhsCode;
                    rawOutEntity.LocationCode = item.LocationCode;
                    rawOutEntity.MaterialCode = item.MaterialCode;
                    rawOutEntity.MaterialName = item.MaterialName;
                    rawOutEntity.BatchNo = item.BatchNo;
                    rawOutEntity.OutType = "11";
                    rawOutEntity.Qty = item.DeliveryQty;
                    rawOutEntity.Unit = item.UnitName;
                    rawOutEntity.Creator = CurrentAccount.UserCode;
                    rawOutEntity.CreateTime = DateTime.Now;
                    rawOutEntity.BaseNum = dispatchSubEntity.DeliveryNo;
                    rawOutEntity.BaseLine = dispatchSubEntity.LineNum;
                    rawOutEntity.ProductOrder = dispatchSubEntity.ProductOrder;
                    rawOutEntity.OrderLine = dispatchSubEntity.ProductLine;
                    lstRawMaterialOut.Add(rawOutEntity);
                    #endregion
                }

                #region 同步SAP
                var factoryCode = dispatchEntity.FactoryCode;
                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1" && dispatchEntity.Status == "3")
                {
                    IF150 sapRequest1 = new IF150();
                    sapRequest1.HEAD = new SapHeadDto();
                    sapRequest1.HEAD.INIF_ID = SAPInterface.IF150.ToString();

                    sapRequest1.RSQ_DATA = new IF150_RSQ_DATA();
                    IF150_DATA IF150_DATA = new IF150_DATA();
                    IF150_DATA.ZGZ = dispatchEntity.PostMark;
                    IF150_DATA.VSTEL = factoryCode ?? "";
                    IF150_DATA.VBELN = dispatchEntity.DeliveryNo ?? "";
                    IF150_DATA.ZFPN = dispatchEntity.InvoiceNO ?? "";
                    IF150_DATA.ZTDN = dispatchEntity.LoadingBill ?? "";
                    //IF150_DATA.ZJS = dispatchEntity.BoxNum == null ? "" : dispatchEntity.BoxNum.ToString();
                    IF150_DATA.ZJZID = dispatchEntity.ContainerID ?? "";
                    IF150_DATA.ZCID = dispatchEntity.CarNumber ?? "";
                    IF150_DATA.ZCCG = dispatchEntity.ForkliftWorker ?? "";
                    IF150_DATA.ZMG = dispatchEntity.WoodWorker ?? "";
                    IF150_DATA.ZFHR = dispatchEntity.DeliveryUserCode ?? "";
                    IF150_DATA.ZDAT3 = DateTime.Now.ToString("yyyyMMdd");
                    IF150_DATA.ZTIM3 = DateTime.Now.ToString("HHmmss");
                    IF150_DATA.ZNAM3 = CurrentAccount.UserCode + "-" + CurrentAccount.UserName ?? "";
                    IF150_DATA.WADAT_IST = dispatchEntity.PostDate.Value.ToString("yyyyMMdd");
                    sapRequest1.RSQ_DATA.IS_DATA = IF150_DATA;

                    List<IF150_ITEM> IT_ITEM = new List<IF150_ITEM>();
                    var arrMaterialCode = deliveryDetailList.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _materialService.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();
                    foreach (var groupItem in deliveryDetailList.GroupBy(t => t.MaterialCode))
                    {
                        int i = 1;
                        var matItem = materialList.Find(t => t.MaterialCode == groupItem.Key);
                        foreach (var item in groupItem)
                        {
                            IF150_ITEM IF150_ITEM = new IF150_ITEM();
                            IF150_ITEM.POSNR = item.LineNum ?? "";
                            //if (groupItem.Count() > 1)    //韩总又要求去掉这个条件 2025-01-24
                            //{
                                IF150_ITEM.ZPOSNR = i.ToString() ?? "";
                            //}
                            IF150_ITEM.VGBEL = item.ProductOrder ?? "";
                            IF150_ITEM.VGPOS = item.ProductLine ?? "";
                            IF150_ITEM.MATNR = matItem?.SAPMaterialCode ?? "";
                            IF150_ITEM.LFIMG = item.DeliveryQty.ToString() ?? "";
                            IF150_ITEM.LGORT = item.LocationCode ?? "";
                            IF150_ITEM.CHARG = item.BatchNo ?? "";
                            IT_ITEM.Add(IF150_ITEM);

                            i += 1;
                        }
                    }
                    var deliveryDetailOther = _MMRawMaterialDispatchDetailService.GetList(t => t.DeliveryNo == dispatchEntity.DeliveryNo
                          && t.DispatchSubId != dispatchSubEntity.Id).ToList();
                    if (deliveryDetailOther.Count > 0)
                    {
                        var arrMaterialCode2 = deliveryDetailOther.Select(t => t.MaterialCode).Distinct().ToArray();
                        var materialList2 = _materialService.Get_ExpressionList(t => arrMaterialCode2.Contains(t.MaterialCode)).ToList();
                        foreach (var groupItem in deliveryDetailOther.GroupBy(t => t.MaterialCode))
                        {
                            int i = 1;
                            var matItem2 = materialList2.Find(t => t.MaterialCode == groupItem.Key);
                            foreach (var item in groupItem)
                            {
                                IF150_ITEM IF150_ITEM = new IF150_ITEM();
                                IF150_ITEM.POSNR = item.LineNum ?? "";
                                IF150_ITEM.ZPOSNR = i.ToString() ?? "";
                                IF150_ITEM.VGBEL = item.ProductOrder ?? "";
                                IF150_ITEM.VGPOS = item.ProductLine ?? "";
                                IF150_ITEM.MATNR = matItem2?.SAPMaterialCode ?? "";
                                IF150_ITEM.LFIMG = item.DeliveryQty.ToString() ?? "";
                                IF150_ITEM.LGORT = item.LocationCode ?? "";
                                IF150_ITEM.CHARG = item.BatchNo ?? "";
                                IT_ITEM.Add(IF150_ITEM);

                                i += 1;
                            }
                        }
                    }

                    sapRequest1.RSQ_DATA.IT_ITEM = IT_ITEM;
                    var sapResult1 = SAPHelper.Instance.PostToSAP(sapRequest1.HEAD.INIF_ID, sapRequest1);
                    if (!sapResult1.Flag)
                        return AjaxResult(false, sapResult1.Msg);

                    dispatchEntity.IsPosted = sapResult1.Flag ? "1" : ""; ;
                    dispatchEntity.PostedMsg = sapResult1.Msg;
                    dispatchEntity.PostedTime = DateTime.Now;
                    dispatchEntity.PostedUser = "SAP";
                    dispatchEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult1.Data), "EV_MBLNR");
                    dispatchEntity.SAP_VBELN = getValue(JObject.Parse(sapResult1.Data), "EV_VBLEN");
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
                    _rawDispatchService.SaveEntity(dispatchEntity.Id, dispatchEntity);
                    _rawDispatchSubService.SaveEntity(dispatchSubEntity.Id, dispatchSubEntity);
                    _MMRawMaterialDispatchDetailService.SaveEntity_List(false, deliveryDetailList);

                    if (lstRawMaterialStock.Count > 0)
                        _rawStockService.SaveEntity_List(true, "", lstRawMaterialStock, out msg);
                    if (lstRawMaterialOut.Count > 0)
                        _rawOutService.SaveEntity_List(false, "", lstRawMaterialOut, out msg);

                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_9"));//操作成功！
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 行退货
        /// <summary>
        ///功能描述:  行回退
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("LineBack")]
        public HttpResponseMessage LineBack(JObject jo)
        {
            try
            {
                List<MM_RawMaterialInEntity> lstRawIn = new List<MM_RawMaterialInEntity>();
                List<MM_RawMaterialStockEntity> lstRawStock = new List<MM_RawMaterialStockEntity>();

                var subId = getValue(jo, "Id");

                //更新行状态
                var dispatchSubEntity = _rawDispatchSubService.GetEntity(t => t.Id == subId);
                dispatchSubEntity.SubStatus = "1";
                dispatchSubEntity.ModifyByCode = CurrentAccount.UserCode;
                dispatchSubEntity.ModifyByName = CurrentAccount.UserName;
                dispatchSubEntity.ModifyTime = DateTime.Now;

                //更新主表状态
                var dispatchEntity = _rawDispatchService.GetEntity(t => t.Id == dispatchSubEntity.DispatchId);
                if (dispatchEntity.Status == "3")
                    return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_10"));//发货单已完成，无法回退

                var flag = _rawDispatchSubService.GetList(t => t.DispatchId == dispatchEntity.Id && t.SubStatus == "3"
                    && t.Id != dispatchSubEntity.Id).Any();
                if (!flag)
                {
                    dispatchEntity.Status = "1";
                    dispatchEntity.ModifyByCode = CurrentAccount.UserCode;
                    dispatchEntity.ModifyByName = CurrentAccount.UserName;
                    dispatchEntity.ModifyTime = DateTime.Now;
                }

                //发货明细删除
                var dispatchDetailList = _MMRawMaterialDispatchDetailService.GetList(t => t.DispatchSubId == subId).ToList();
                //库存回退
                var arrDispatchDetailId = dispatchDetailList.Select(t => t.Id).ToArray();
                var rawOutList = _rawOutService.Get_ExpressionList(t => arrDispatchDetailId.Contains(t.BusinessId)).ToList();
                foreach (var item in rawOutList)
                {
                    #region 库存
                    var stockEntity = _rawStockService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                        && t.LocationCode == item.LocationCode && t.BatchNo == item.BatchNo);
                    if (stockEntity == null)
                    {
                        //可能合批了，寻找合并批次
                        var mergeOutEntity = _rawOutService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                             && t.LocationCode == item.LocationCode && t.BatchNo == item.BatchNo && !string.IsNullOrEmpty(t.AssociateNo));
                        if (mergeOutEntity == null)
                            return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_11"));//库存不存在

                        var merageInEntity = _rawInService.Get_ExpressionEntity(t => t.AssociateNo == mergeOutEntity.AssociateNo);

                        stockEntity = _rawStockService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                         && t.LocationCode == merageInEntity.LocationCode && t.BatchNo == merageInEntity.BatchNo);
                        if (stockEntity == null)
                            return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_11"));//库存不存在
                    }

                    stockEntity.Qty += item.Qty;
                    stockEntity.ModifyBy = CurrentAccount.UserCode;
                    stockEntity.ModifyTime = DateTime.Now;
                    lstRawStock.Add(stockEntity);
                    #endregion

                    #region 生成入库记录
                    var inEntity = Tools.Mapper<MM_RawMaterialInEntity>(item);
                    inEntity.Id = Guid.NewGuid().ToString();
                    inEntity.InType = "11";
                    inEntity.WhsCode = stockEntity.WhsCode;
                    inEntity.LocationCode = stockEntity.LocationCode;
                    inEntity.BatchNo = stockEntity.BatchNo;
                    inEntity.Creator = CurrentAccount.UserCode;
                    inEntity.CreateTime = DateTime.Now;
                    inEntity.ModifyBy = CurrentAccount.UserCode;
                    inEntity.ModifyTime = DateTime.Now;
                    inEntity.IsDeleted = false;
                    lstRawIn.Add(inEntity);
                    #endregion
                }

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _rawDispatchService.SaveEntity(dispatchEntity.Id, dispatchEntity);//更新发货单状态
                    _MMRawMaterialDispatchDetailService.RemoveForm(t => t.DispatchSubId == dispatchSubEntity.Id);//删除发货明细

                    if (lstRawStock.Count > 0)
                        _rawStockService.SaveEntity_List(true, "", lstRawStock, out msg);//更新库存
                    if (lstRawIn.Count > 0)
                        _rawInService.SaveEntity_List(false, "", lstRawIn, out msg);//新增入库记录

                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_9"));//操作成功！
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 退货冲销
        /// <summary>
        ///功能描述:  发货单冲销
        ///创　　建: Dragon
        ///创建日期: 2024-03-27
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("BackOff")]
        public HttpResponseMessage BackOff(JObject jo)
        {
            try
            {
                List<MM_RawMaterialInEntity> lstRawIn = new List<MM_RawMaterialInEntity>();
                List<MM_RawMaterialStockEntity> lstRawStock = new List<MM_RawMaterialStockEntity>();

                var dispatchId = getValue(jo, "Id");

                //更新发货单状态
                var dispatchEntity = _rawDispatchService.GetEntity(t => t.Id == dispatchId);
                if (dispatchEntity.Status != "3")
                    return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_12"));//当前状态不允许退货冲销
                if (dispatchEntity.Off_IsPosted == "1")
                    return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_13"));//不允许二次退货

                dispatchEntity.ModifyByCode = CurrentAccount.UserCode;
                dispatchEntity.ModifyByName = CurrentAccount.UserName;
                dispatchEntity.ModifyTime = DateTime.Now;
                dispatchEntity.PostMark = "R";

                //发货明细逻辑删除
                var dispatchDetailList = _MMRawMaterialDispatchDetailService.GetList(t => t.DeliveryNo == dispatchEntity.DeliveryNo).ToList();
                foreach (var item in dispatchDetailList)
                {
                    item.IsDeleted = true;
                    item.ModifyByCode = CurrentAccount.UserCode;
                    item.ModifyByName = CurrentAccount.UserName;
                    item.ModifyTime = DateTime.Now;
                }
                //库存回退
                var arrDispatchDetailId = dispatchDetailList.Select(t => t.Id).ToArray();
                var rawOutList = _rawOutService.Get_ExpressionList(t => arrDispatchDetailId.Contains(t.BusinessId)).ToList();
                foreach (var item in rawOutList)
                {
                    #region 库存
                    var stockEntity = _rawStockService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                        && t.LocationCode == item.LocationCode && t.BatchNo == item.BatchNo);
                    if (stockEntity == null)
                    {
                        //可能合批了，寻找合并批次
                        var mergeOutEntity = _rawOutService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                             && t.LocationCode == item.LocationCode && t.BatchNo == item.BatchNo && !string.IsNullOrEmpty(t.AssociateNo));
                        if (mergeOutEntity == null)
                            return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_11"));//库存不存在

                        var merageInEntity = _rawInService.Get_ExpressionEntity(t => t.AssociateNo == mergeOutEntity.AssociateNo);

                        stockEntity = _rawStockService.Get_ExpressionEntity(t => t.MaterialCode == item.MaterialCode
                         && t.LocationCode == merageInEntity.LocationCode && t.BatchNo == merageInEntity.BatchNo);
                        if (stockEntity == null)
                            return AjaxResult(false, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_11"));//库存不存在
                    }

                    stockEntity.Qty += item.Qty;
                    stockEntity.ModifyBy = CurrentAccount.UserCode;
                    stockEntity.ModifyTime = DateTime.Now;
                    lstRawStock.Add(stockEntity);
                    #endregion

                    #region 生成入库记录
                    var inEntity = Tools.Mapper<MM_RawMaterialInEntity>(item);
                    inEntity.Id = Guid.NewGuid().ToString();
                    inEntity.InType = "11";
                    inEntity.WhsCode = stockEntity.WhsCode;
                    inEntity.LocationCode = stockEntity.LocationCode;
                    inEntity.BatchNo = stockEntity.BatchNo;
                    inEntity.Creator = CurrentAccount.UserCode;
                    inEntity.CreateTime = DateTime.Now;
                    inEntity.ModifyBy = CurrentAccount.UserCode;
                    inEntity.ModifyTime = DateTime.Now;
                    inEntity.IsDeleted = false;
                    lstRawIn.Add(inEntity);
                    #endregion
                }

                #region 同步SAP
                var factoryCode = dispatchEntity.FactoryCode;
                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    IF106 sapRequest = new IF106();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF106.ToString();

                    sapRequest.RSQ_DATA = new IF106_RSQ_DATA();
                    IF106_HEAD IF106_HEAD = new IF106_HEAD();
                    IF106_HEAD.ZGZ = dispatchEntity.PostMark ?? "";
                    IF106_HEAD.VSTEL = dispatchEntity.FactoryCode ?? "";
                    IF106_HEAD.VSTEL = dispatchEntity.DeliveryNo ?? "";
                    IF106_HEAD.WADAT_IST = DateTime.Now.ToString("yyyyMMdd");
                    IF106_HEAD.ZMESDATE = DateTime.Now.ToString("yyyyMMdd");
                    IF106_HEAD.ZMESTIME = DateTime.Now.ToString("HHmmss");
                    sapRequest.RSQ_DATA.IS_HEAD = IF106_HEAD;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    dispatchEntity.Off_IsPosted = sapResult.Flag ? "1" : ""; ;
                    dispatchEntity.Off_PostedMsg = sapResult.Msg;
                    dispatchEntity.Off_PostedTime = DateTime.Now;
                    dispatchEntity.Off_PostedUser = "SAP";
                    dispatchEntity.Off_SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    dispatchEntity.Off_SAP_VBELN = getValue(JObject.Parse(sapResult.Data), "EV_VBLEN");
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
                    _rawDispatchService.SaveEntity(dispatchEntity.Id, dispatchEntity);//更新发货单状态
                    _MMRawMaterialDispatchDetailService.SaveEntity_List(true, dispatchDetailList);//更新发货明细


                    if (lstRawStock.Count > 0)
                        _rawStockService.SaveEntity_List(true, "", lstRawStock, out msg);
                    if (lstRawIn.Count > 0)
                        _rawInService.SaveEntity_List(false, "", lstRawIn, out msg);

                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("MaterialManage.MMRawMaterialDispatchDetailController.Tips_9"));//操作成功！
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion
    }
}

