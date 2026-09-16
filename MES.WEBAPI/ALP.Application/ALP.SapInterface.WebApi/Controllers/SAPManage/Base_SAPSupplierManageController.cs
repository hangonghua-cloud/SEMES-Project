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

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("Base_SAPSupplierManage")]
    public class Base_SAPSupplierManageController : ApiBaseController
    {
        /// <summary>
        // 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-6 16:27:58
        /// 任务编号: 供应商主数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPBase_Supplier")]
        public HttpResponseMessage SaveSAPBase_Supplier(JObject jo)
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
                List<Base_SupplierManageEntity> entity_list = JsonConvert.DeserializeObject<List<Base_SupplierManageEntity>>(getValue(jo, "Entity"));
                if (entity_list==null)
                {
                    result.success = false;
                    result.returnMsg = "参数不能为空";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                DtoHelper.WriteLogWorkDate("SAP供应商主数据", "供应商主数据SaveSAPBase_Supplier接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                new Base_SupplierManage_Service().SaveSAPBase_Supplier(entity_list);

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
