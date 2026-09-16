using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Busines.BaseManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;

namespace ALP.WebApi.Controllers.BaseManage
{
    [Auth]
    [RoutePrefix("BaseManage/BaseShift")]
    /// <summary>
    /// 创 建：gzq
    /// 日 期：2020-05-14 13:53
    /// 描 述：班次
    /// </summary>
    public class BaseShiftController : ApiBaseController
    {
        private BaseShiftBLL baseShiftBLL = new BaseShiftBLL();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetListJson")]
        public HttpResponseMessage GetListJson(string queryJson = "{}")
        {
            var result = new ResponseResult();
            try
            {
                var list = baseShiftBLL.GetList(queryJson);
                result.resultData = list;
                result.success = true;
                result.returnMsg = "执行成功";
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
        /// 获取列表(分页)
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpPost]
        [Route("GetPageListJson")]
        public HttpResponseMessage GetPageListJson(JObject jo)
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
                    result.returnMsg = "分页参数Pagination不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = baseShiftBLL.GetPageList(pagination, queryJson);
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
                result.returnMsg = "执行成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "执行失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(Guid keyValue)
        {
            var result = new ResponseResult();

            try
            {
                var data = baseShiftBLL.GetEntity(keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = "获取详情数据成功";
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
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="jo">json参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Delete")]
        public HttpResponseMessage Delete(JObject jo)
        {
            var result = new ResponseResult<object>();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            try
            {
                string keyValue = getValue(jo, "KeyValue");
                if (string.IsNullOrEmpty(userCode))
                {
                    result.success = false;
                    result.returnMsg = "用户名不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    baseShiftBLL.RemoveForm(keyValue);

                    result.success = true;
                    result.returnMsg = "删除成功";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="jo">json参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage Save(JObject jo)
        {
            var result = new ResponseResult<object>();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少KeyValue参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少Entity参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                if (string.IsNullOrEmpty(userCode))
                {
                    result.success = false;
                    result.returnMsg = "用户名不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    BaseShiftEntity entity = JsonConvert.DeserializeObject<BaseShiftEntity>(getValue(jo, "Entity"));
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        entity.UpdatedByCode = userCode;
                        entity.UpdatedByName = userName;
                        entity.UpdatedOn = DateTime.Now;
                    }
                    else
                    {
                        entity.CreatedByCode = userCode;
                        entity.CreatedByName = userName;
                        entity.CreatedOn = DateTime.Now;
                    }
                    baseShiftBLL.SaveForm(keyValue, entity);
                    result.success = true;
                    result.returnMsg = "操作成功";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}
