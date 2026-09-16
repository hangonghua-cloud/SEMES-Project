using ALP.Application.Busines.Material;
using ALP.Application.Entity.Material;
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
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.Material
{
    [Auth]
    [RoutePrefix("Base_ProcessAttr")]
    public class Base_ProcessAttrController : ApiBaseController
    {
        private Base_ProcessAttrBLL _ProcessAttrBLL = new Base_ProcessAttrBLL();
        private Base_ProcessAttrItemBLL _ProcessAttrItemBLL = new Base_ProcessAttrItemBLL();
        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败

        #region 查询主表明细
        [HttpPost]
        [Route("GetProcessAttrPage")]
        public HttpResponseMessage GetProcessAttr(JObject jo)
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

                var data = _ProcessAttrBLL.GetListWithPage(pagination, queryJson);
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

        #region 保存主表
        [HttpPost]
        [Route("SaveProcessAttrForm")]
        public HttpResponseMessage SaveProcessAttrForm(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            var keyValue = getValue(jo, "KeyValue");
            try
            {
                var entity = JsonConvert.DeserializeObject<Base_ProcessAttrEntity>(getValue(jo, "Entity"));
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var ent1 = _ProcessAttrBLL.GetEntity(t => t.Id != keyValue && t.ItemName == entity.ItemName);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_ProcessAttrController.Tips_3");//存在相同名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                    entity.CreateTime = DateTime.Parse(entity.CreateTime.ToString());
                }
                else
                {
                    var ent = _ProcessAttrBLL.GetEntity(t => t.ItemCode == entity.ItemCode || t.ItemName==entity.ItemName);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_ProcessAttrController.Tips_4");//存在相同编码或者名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.Create();
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
 
                }
                _ProcessAttrBLL.SaveForm(keyValue, entity);
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 删除主表
        [HttpGet]
        [Route("RemoveProcessAttr")]
        public HttpResponseMessage RemoveProcessAttr(string keyValue)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            try
            {
                _ProcessAttrBLL.Remove(t => t.Id == keyValue);
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 查询子表明细
        [HttpPost]
        [Route("GetProcessAttrItemPage")]
        public HttpResponseMessage GetProcessAttrItemPage(JObject jo)
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

                var data = _ProcessAttrItemBLL.GetListWithPage(pagination, queryJson);
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

        #region 保存主子表
        [HttpPost]
        [Route("SaveProcessAttrItemForm")]
        public HttpResponseMessage SaveProcessAttrItemForm(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            var keyValue = getValue(jo, "KeyValue");
            try
            {
                var entity = JsonConvert.DeserializeObject<Base_ProcessAttrItemEntity>(getValue(jo, "Entity"));
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var ent1 = _ProcessAttrItemBLL.GetEntity(t => t.Id != keyValue && t.ProcessAttrId==entity.ProcessAttrId && t.AttrName == entity.AttrName);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_ProcessAttrController.Tips_3");//存在相同名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                    entity.CreateTime = DateTime.Parse(entity.CreateTime.ToString());
                }
                else
                {
                    var ent = _ProcessAttrItemBLL.GetEntity(t =>   t.ProcessAttrId == entity.ProcessAttrId && (t.AttrCode == entity.AttrCode || t.AttrName == entity.AttrName));
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_ProcessAttrController.Tips_4");//存在相同编码或者名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.Create();
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;


                }
                _ProcessAttrItemBLL.SaveForm(keyValue, entity);
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 删除子主表
        [HttpGet]
        [Route("RemoveProcessAttrItem")]
        public HttpResponseMessage RemoveProcessAttrItem(string keyValue)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            try
            {
                _ProcessAttrItemBLL.Remove(t => t.Id == keyValue);
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion
    }
}