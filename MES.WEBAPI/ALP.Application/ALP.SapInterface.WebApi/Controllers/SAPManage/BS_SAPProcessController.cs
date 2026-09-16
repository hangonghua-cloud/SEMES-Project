using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using ALP.Application.WebApi.Common;
using ALP.Application.Entity.SAPEntity;
using System.Collections.Generic;
using ALP.Application.Service.Material;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("BS_SAPProcess")]
    public class BS_SAPProcessController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 工艺路线基础数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPBS_Process")]
        public HttpResponseMessage SaveSAPBS_Process(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                DtoHelper.WriteLogWorkDate("SAP工艺路线基础数据", "工艺路线基础数据SaveSAPBS_Process接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                var entity_list = JsonConvert.DeserializeObject<List<SAPBS_Process>>(getValue(jo, "Entity"));
                var Process = entity_list[0].Process;
                var ProcessOfOperations = entity_list[0].ProcessOfOperations;
                var ProcessOfOperation = ProcessOfOperations[0].ProcessOfOperation;
                var ProcessOfOperationsAttrs = ProcessOfOperations[0].ProcessOfOperationsAttrs;
                if (Process == null)
                {
                    result.success = false;
                    result.returnMsg = "参数必填";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (ProcessOfOperation == null)
                {
                    result.success = false;
                    result.returnMsg = "参数必填";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (ProcessOfOperationsAttrs == null)
                {
                    result.success = false;
                    result.returnMsg = "参数必填";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                new BS_Process_Service().SaveSAPBS_Process(entity_list);
                result.success = true;
                result.returnMsg = "操作成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}
