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
using ALP.Application.Service.Resources;
using ALP.Application.Service.Helper;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// [MM_RawMaterialDispatch]控制器
    /// 描述:原材料半成品发货单主表
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    [Auth]
    [RoutePrefix("MMRawMaterialDispatch")]
    public class MMRawMaterialDispatchController : ApiBaseController
    {
        private MMRawMaterialDispatchService _MMRawMaterialDispatchService = new MMRawMaterialDispatchService();
        private Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();

        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: 原材料半成品发货单主表
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
                 
                 var data = _MMRawMaterialDispatchService.GetPageDataTableList(pagination, queryJson);
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
                 result.returnMsg =Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_1");//操作成功！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_2") + ex.Message;//操作失败！
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
        ///任务编号: 原材料半成品发货单主表
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
                 //var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchEntity>(entityStr)
                 
                 result.resultData = _MMRawMaterialDispatchService.GetEntity(t=>t.Id==keyValue);
                 result.success = true;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_3");//操作成功！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_2") + ex.Message;//操作失败！
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
        ///任务编号: 原材料半成品发货单主表
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
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_4");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("KeyValue") == null)
             {
                 result.success = false;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_5");//缺少KeyValue参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("Entity") == null)
             {
                 result.success = false;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_6");//缺少Entity参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var keyValue = getValue(jo, "KeyValue");
             var entityStr=getValue(jo, "Entity");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchEntity>(entityStr);
                 
                 if (!string.IsNullOrEmpty(keyValue))
                 {
                     entity.ModifyByCode = userCode;
                     entity.ModifyByName = userName;
                     entity.ModifyTime = DateTime.Now;

                    #region 同步SAP
                    var factoryCode = entity.FactoryCode;
                    var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                        && t.Remark1 == factoryCode);
                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        #region IF150 交货单修改
                        IF150 sapRequest1 = new IF150();
                        sapRequest1.HEAD = new SapHeadDto();
                        sapRequest1.HEAD.INIF_ID = SAPInterface.IF150.ToString();

                        sapRequest1.RSQ_DATA = new IF150_RSQ_DATA();
                        IF150_DATA IF150_DATA = new IF150_DATA();
                        IF150_DATA.ZGZ = "";
                        IF150_DATA.VSTEL = factoryCode ?? "";
                        IF150_DATA.VBELN = entity.DeliveryNo ?? "";
                        IF150_DATA.ZFPN = entity.InvoiceNO ?? "";
                        IF150_DATA.ZTDN = entity.LoadingBill ?? "";
                        IF150_DATA.ZJS = "";
                        IF150_DATA.ZJZID = entity.ContainerID ?? "";
                        IF150_DATA.ZFXN = entity.SealingNo ?? "";
                        IF150_DATA.ZCID = entity.CarNumber ?? "";
                        IF150_DATA.ZCCG = entity.ForkliftWorker ?? "";
                        IF150_DATA.ZMG = entity.WoodWorker ?? "";
                        IF150_DATA.ZFHR = entity.DeliveryUserName ?? "";
                        IF150_DATA.ZDAT3 = DateTime.Now.ToString("yyyyMMdd");
                        IF150_DATA.ZTIM3 = DateTime.Now.ToString("HHmmss");
                        IF150_DATA.ZNAM3 = CurrentAccount.UserCode + "-" + CurrentAccount.UserName ?? "";
                        IF150_DATA.WADAT_IST = "";
                        IF150_DATA.ZMFHD = DateTime.Now.ToString("yyyyMMdd");
                        sapRequest1.RSQ_DATA.IS_DATA = IF150_DATA;

                        var sapResult1 = SAPHelper.Instance.PostToSAP(sapRequest1.HEAD.INIF_ID, sapRequest1);
                        if (!sapResult1.Flag)
                            return AjaxResult(false, sapResult1.Msg);

                        entity.SAP_VBELN = getValue(JObject.Parse(sapResult1.Data), "EV_VBLEN");
                        #endregion
                    }
                    #endregion
                }
                else
                 {
                     entity.CreateByCode = userCode;
                     entity.CreateByName = userName;
                     entity.IsDeleted = false;
                     entity.CreateTime = DateTime.Now;
                 }
                 
                 int isok = _MMRawMaterialDispatchService.SaveEntity(keyValue, entity);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_7");//操作失败！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_2") + ex.Message;//操作失败！
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
        ///任务编号: 原材料半成品发货单主表
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
             var isUpdate=false;
             
             if (jo == null)
             {
                 result.success = false;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_4");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("KeyValue") == null)
             {
                 result.success = false;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_5");//缺少KeyValue参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var keyValue = getValue(jo, "KeyValue");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchEntity>(getValue(jo, "Entity"));
                 var list = JsonConvert.DeserializeObject<List<MMRawMaterialDispatchEntity>>(getValue(jo, "data"));
                 
                 if (!string.IsNullOrEmpty(keyValue))
                 {
                     isUpdate=true;
                     //entity.ModifyBy = userCode;
                     //entity.ModifyTime = DateTime.Now;
                 }
                 else
                 {
                     //entity.Creator = userCode;
                     //entity.CreateTime = DateTime.Now;
                 }
                 
                 foreach(var item in list)
                 {
                     item.Id=Guid.NewGuid().ToString();
                 }
                 int isok = _MMRawMaterialDispatchService.SaveEntity_List(isUpdate, list);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_7");//操作失败！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_2") + ex.Message;//操作失败！
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
        ///任务编号: 原材料半成品发货单主表
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
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_4");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("Entity") == null)
             {
                 result.success = false;
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_6");//缺少Entity参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var entityStr=getValue(jo, "Entity");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMRawMaterialDispatchEntity>(entityStr);
                 
                 int isok = _MMRawMaterialDispatchService.RemoveForm(t=>t.Id==entity.Id);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? "操作成功！" : Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_7");//操作失败！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = Language.GetText("MaterialManage.MMRawMaterialDispatchController.Tips_2") + ex.Message;//操作失败！
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion
    }
}

