using System;
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
using System.Collections.Generic;
using System.Linq;

namespace ALP.Application.WebApi.Controllers.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    [RoutePrefix("SystemManage/Sys_DataSegregateGroupToLabel")]
    public class Sys_DataSegregateGroupToLabelController : ApiBaseController
    {
        private Sys_DataSegregateGroupToLabelBLL groupBLL = new Sys_DataSegregateGroupToLabelBLL();

        #region 获取数据
        /// <summary>
        /// 检验列表
        /// </summary>
        /// <param name="jo">分页</param>
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_1");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string groupCode = getValue(jo, "groupCode");
                var watch = CommonHelper.TimerStart();
                var data = groupBLL.GetPageList(pagination, groupCode);
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
        /// 分页查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Get_PageData")]
        public HttpResponseMessage Get_PageData(JObject jo)
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_1");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = groupBLL.Get_PageData(pagination, queryJson);
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

        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save_Data")]
        public HttpResponseMessage Save_Data(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                List<Sys_DataSegregateLabel> list_groupLable = JsonConvert.DeserializeObject<List<Sys_DataSegregateLabel>>(getValue(jo, "list"));
                if (list_groupLable == null || list_groupLable.Count == 0)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_4");//没有选择值
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string GroupCode = getValue(jo, "GroupCode");
                if (string.IsNullOrEmpty(GroupCode))
                {

                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_5");//GroupCode不能为空
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }


                var watch = CommonHelper.TimerStart();
                string msg = "";
                //保存数据
                var returnValue = groupBLL.GetSaveSql_DataSegregateGroupToLabel(list_groupLable, GroupCode, out msg);
                if (returnValue)
                {
                    //更新标签值
                    returnValue = groupBLL.Update_GroupLabelValue(GroupCode) > 0 ? true : false;
                }
                result.success = returnValue;
                result.returnMsg = msg;
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
        /// 删除检验信息
        /// </summary>
        /// <param name="jo"></param>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.success = false;
            try
            {
                List<Sys_DataSegregateGroupToLabel> list_groupLable = JsonConvert.DeserializeObject<List<Sys_DataSegregateGroupToLabel>>(getValue(jo, "list"));
                if (list_groupLable == null || list_groupLable.Count == 0)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_4");//没有选择值
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string GroupCode = getValue(jo, "GroupCode");
                if (string.IsNullOrEmpty(GroupCode))
                {

                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_5");//GroupCode不能为空
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //删除数据
                var array_ids = list_groupLable.Select(q => "'" + q.Id + "'").ToList();
                string keyValues = string.Join(",", array_ids);
                int returnValue = groupBLL.RemoveForm(keyValues);
                if (returnValue > 0)
                {
                    //更新标签值
                    returnValue = groupBLL.Update_GroupLabelValue(GroupCode);
                }
                if (returnValue > 0)
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                }
                else
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateGroupToLabelController.Tips_7");//删除失败，没有找到记录
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}