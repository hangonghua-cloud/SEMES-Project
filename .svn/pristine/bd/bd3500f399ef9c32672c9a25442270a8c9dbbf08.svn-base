using ALP.Application.Busines.BaseManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.WebApi.Controllers.API;
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
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    [Auth]
    [RoutePrefix("BaseReportGroup")]
    public class BaseReportGroupController : ApiBaseController
    {
        private Base_ReportGroupBLL _ReportGroupBLL = new Base_ReportGroupBLL();

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
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
                var queryJson = getValue(jo, "queryJson");
                var data = new
                {
                    rows = _ReportGroupBLL.GetPageDataTableList(pagination, queryJson),
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 创建表单
        /// <summary>
        ///  创建表单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject json)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            try
            {
                var time = DateTime.Now;
                var entity = JsonConvert.DeserializeObject<Base_ReportGroupEntity>(getValue(json, "Entity"));
                var keyValue = getValue(json, "KeyValue");
                if (string.IsNullOrEmpty(keyValue))
                {
                    var ent = _ReportGroupBLL.GetEntity(t => t.GroupName == entity.GroupName);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportGroupController.Tips_3");//已经存在相同名称的角色
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }
                else
                {
                    var ent = _ReportGroupBLL.GetEntity(t => t.GroupName == entity.GroupName && t.Id!=keyValue);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportGroupController.Tips_3");//已经存在相同名称的角色
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }

                _ReportGroupBLL.SaveForm(keyValue, entity);
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region
        /// <summary>
        ///  删除表单
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject json)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            result.resultData = null;
            try
            {
                var time = DateTime.Now;
                var entity = JsonConvert.DeserializeObject<Base_ReportGroupEntity>(getValue(json, "Entity"));
 
                _ReportGroupBLL.RemoveForm(t=>t.Id==entity.Id);
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion
    }
}