using ALP.Application.Service.Resources;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;

using System.Collections.Generic;
using ALP.Application.WebApi.Common;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("Base_SAPKeyParameterItem")]
    public class Base_SAPKeyParameterItemController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP客户主数据接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBase_SAPKeyParameter")]
        public HttpResponseMessage SaveBase_SAPKeyParameter(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = "缺少Entity参数";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            DtoHelper.WriteLogWorkDate("SAP客户主数据", "客户主数据Base_SAPKeyParameter接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
            try
            {
                List<Base_KeyParameterItemEntity> entity_list = JsonConvert.DeserializeObject<List<Base_KeyParameterItemEntity>>(getValue(jo, "Entity"));
                //数据写入数据库
               new Base_KeyParameterItem_Service().SaveBase_SAPKeyParameter(entity_list);


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
