using ALP.Application.Service.SystemManage;
using ALP.Application.Busines.SystemManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 基础数据类查询接口控制器
    /// </summary>
    [Auth]
    [RoutePrefix("Base")]
    public class BaseDataController : ApiBaseController
    {

       

       
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDataItemListJson")]
        public HttpResponseMessage GetDataItemListJson(string EnCode)
        {
            DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemDetailBLL.GetDataItemList(EnCode).ToList();

                result.resultData = data;
                result.success = data.Count > 0 ? true : false;
                result.returnMsg = data.Count > 0 ? "获取数据字典详情列表成功" : "数据字典详情中查无[EnCode=" + EnCode + "]的数据";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.resultData = null;
                result.success = false;
                result.returnMsg = "获取数据字典详情数据失败：" + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
    }
}