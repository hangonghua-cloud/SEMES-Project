using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.Application.WebApi.Common;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("PL_SAPInternalOrder")]
    public class PL_SAPInternalOrderController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 内部订单接口
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPPL_InternalOrder")]
        public HttpResponseMessage SaveSAPPL_InternalOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try {
                DtoHelper.WriteLogWorkDate("SA内部订单", "内部订单SaveSAPPL_InternalOrder接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                List<PL_InternalOrderEntity> entity_list = JsonConvert.DeserializeObject<List<PL_InternalOrderEntity>>(getValue(jo, "Entity"));
                if (entity_list == null)
                {
                    result.success = false;
                    result.returnMsg = "参数不能为空";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                new PL_InternalOrder_Service().SaveSAPPL_InternalOrder(entity_list);
                result.success = true;
            result.returnMsg = "操作成功";
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;//操作失败：
                result.statusCode = ((int) HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
}
        }
    }
