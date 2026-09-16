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
namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// [MM_ProductStockAdjust]控制器
    /// 描述:MM_成品库存校准记录
    /// 作者:dragon
    /// 创建时间:2022-05-14 17:54:49
    /// </summary>
    [Auth]
    [RoutePrefix("MMProductStockAdjust")]
    public class MMProductStockAdjustController : ApiBaseController
    {
        private MMProductStockAdjustService _MMProductStockAdjustService = new MMProductStockAdjustService();
        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败
         
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
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
                 
                 var data = _MMProductStockAdjustService.GetPageDataTableList(pagination, queryJson);
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
                 result.returnMsg =SuccessMsg;
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = FaildMsg + ex.Message;
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion
        
        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
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
                 //var entity = JsonConvert.DeserializeObject<MMProductStockAdjustEntity>(entityStr)
                 
                 result.resultData = _MMProductStockAdjustService.GetEntity(t=>t.Id==keyValue);
                 result.success = true;
                 result.returnMsg = SuccessMsg;
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = FaildMsg + ex.Message;
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion
        
        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
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
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_3");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("KeyValue") == null)
             {
                 result.success = false;
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_4");//缺少KeyValue参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("Entity") == null)
             {
                 result.success = false;
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_5");//缺少Entity参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var keyValue = getValue(jo, "KeyValue");
             var entityStr=getValue(jo, "Entity");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMProductStockAdjustEntity>(entityStr);
                 
                 if (!string.IsNullOrEmpty(keyValue))
                 {
                     entity.ModifyBy = userCode;
                     entity.ModifyByName = userName;
                     entity.ModifyTime = DateTime.Now;
                 }
                 else
                 {
                     entity.Creator = userCode;
                     entity.CreatorName = userName;
                     entity.CreateTime = DateTime.Now;
                 }
                 
                 int isok = _MMProductStockAdjustService.SaveEntity(keyValue, entity);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = FaildMsg + ex.Message;
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion
        
        #region 批量保存
        /// <summary>
        ///功能描述:  批量保存表单（新增、修改）
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
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
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_3");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("KeyValue") == null)
             {
                 result.success = false;
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_4");//缺少KeyValue参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var keyValue = getValue(jo, "KeyValue");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMProductStockAdjustEntity>(getValue(jo, "Entity"));
                 var list = JsonConvert.DeserializeObject<List<MMProductStockAdjustEntity>>(getValue(jo, "data"));
                 
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
                 int isok = _MMProductStockAdjustService.SaveEntity_List(isUpdate, list);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = FaildMsg + ex.Message;
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion
        
        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: dragon
        ///创建日期: 2022-05-14 17:54:49
        ///任务编号: MM_成品库存校准记录
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
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_3");//参数不能为空！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             if (jo.SelectToken("Entity") == null)
             {
                 result.success = false;
                 result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMProductStockAdjustController.Tips_5");//缺少Entity参数！
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             
             var entityStr=getValue(jo, "Entity");
             
             try
             {
                 var entity = JsonConvert.DeserializeObject<MMProductStockAdjustEntity>(entityStr);
                 
                 int isok = _MMProductStockAdjustService.RemoveForm(t=>t.Id==entity.Id);
                 result.success = isok > 0 ? true : false;
                 result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
             catch (Exception ex)
             {
                 result.success = false; 
                 result.returnMsg = FaildMsg + ex.Message;
                 result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                 return Request.CreateResponse(HttpStatusCode.OK, result);
             }
        }
        #endregion

    }
}

