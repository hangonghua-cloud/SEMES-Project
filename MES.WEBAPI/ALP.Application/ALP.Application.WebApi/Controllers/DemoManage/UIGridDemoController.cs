using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.SystemManage;
using System.Dynamic;
using ALP.Util;
using ALP.WebApi.Models;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Busines.BaseManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Application.Entity.BaseManage;
using ALP.WebApi.Filter;

namespace ALP.WebApi.Controllers.DemoManage
{
    [Auth]
    [RoutePrefix("DemoManage/UIGridDemo")]
    public class UIGridDemoController : ApiBaseController
    {
        private PostBLL postBLL = new PostBLL();
        private RoleBLL roleBLL = new RoleBLL();
        #region 获取数据
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
                var data = roleBLL.GetPageList(pagination, queryJson);
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
                result.returnMsg = "操作成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
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
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue = "")
        {
            var result = new ResponseResult();

            try
            {
                var data = postBLL.GetEntity(keyValue);

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
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键值</param>
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
                    roleBLL.RemoveForm(keyValue);

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
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
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
                if (jo.SelectToken("RoleEntity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少RoleEntity参数！";
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
                    RoleEntity entity = JsonConvert.DeserializeObject<RoleEntity>(getValue(jo, "RoleEntity"));
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        entity.ModifyUserName = userCode;
                        entity.ModifyDate = DateTime.Now;
                    }
                    else
                    {
                        entity.CreateUserName = userCode;
                        entity.CreateDate = DateTime.Now;
                    }
                    roleBLL.SaveForm(keyValue, entity);
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
