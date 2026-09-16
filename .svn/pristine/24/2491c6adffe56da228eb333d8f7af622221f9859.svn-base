using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Busines.SystemManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;

namespace ALP.Application.WebApi.Controllers.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.4.9 16:19
    /// 描 述：打印服务
    /// </summary>
    [RoutePrefix("BsPeopleByPrintServer")]
    public class BsPeopleByPrintServerController : ApiBaseController
    {
        private BsPeopleByPrintServerBLL bll = new BsPeopleByPrintServerBLL();

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPageListJson")]
        public HttpResponseMessage GetPageList(JObject jo)
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
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_1");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
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

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListJson")]
        public HttpResponseMessage GetList(string code)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = bll.GetList(code);

                result.resultData = data;
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


        /// <summary>获取当前用户的打印服务器
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPrintServer")]
        public HttpResponseMessage GetPrintServerByPersonCode(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var queryParam = queryJson.ToJObject();
                string personCode = queryParam["personCode"] == null ? "" : queryParam["personCode"].ToString();

                var data = bll.GetPrintServerByPersonCode(personCode);
                result.resultData = data;
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


        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteForm")]
        public HttpResponseMessage RemoveForm(string keyValue)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                int returnValue = bll.RemoveForm(keyValue);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_5");//删除失败，没有找到记录
                        result.success = true;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 更改数据权限
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateIsDeptLimit")]
        public HttpResponseMessage UpdateIsDeptLimit(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_7");//缺少Jo参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                int returnValue = bll.UpdateIsDeptLimit(queryJson);
                if (returnValue > 0)
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_8");//更改成功！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_9");//更改失败！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_10");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                BsPeopleByPrintServerEntity entity = JsonConvert.DeserializeObject<BsPeopleByPrintServerEntity>(getValue(jo, "Entity"));
                int returnValue = bll.SaveForm(entity);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_11");//添加成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_12");//修改成功
                        result.success = true;
                        break;
                    case 3:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BsPeopleByPrintServerController.Tips_13");//更新成功。
                        result.success = false;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}