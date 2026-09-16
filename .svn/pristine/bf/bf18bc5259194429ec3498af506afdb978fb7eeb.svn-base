using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Entity.Material;
using System.Collections.Generic;
using ALP.Application.WebApi.Common;
using ALP.Application.Service.Material;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("BS_OAPeople")]
    public class BS_OAPeopleController : ApiBaseController
    {
        /// <summary>
        /// 创建：jpf
        /// 时间：2024-3-19 15:59:40
        /// 描述：OA人员接口对接
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveOABS_People")]
        public HttpResponseMessage SaveOABS_People(JObject jo)
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
                DtoHelper.WriteLogWorkDate("OA人员信息", "人员接口SaveOABS_People接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                var entity_list = JsonConvert.DeserializeObject<List<BS_PeopleEntity>>(getValue(jo, "Entity"));

                new BS_People_Service().SaveOABS_People(entity_list);
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

