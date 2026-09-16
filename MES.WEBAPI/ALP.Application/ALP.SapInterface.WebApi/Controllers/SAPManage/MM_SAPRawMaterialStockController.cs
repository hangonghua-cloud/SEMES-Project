using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;
using ALP.Application.WebApi.Common;
using ALP.Application.Entity.MaterialManage;
using System.Collections.Generic;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Service.MaterialManage;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("MM_SAPRawMaterialStock")]
    public class MM_SAPRawMaterialStockController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 原材料库存接口
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPMM_RawMaterialStock")]
        public HttpResponseMessage SaveSAPMM_RawMaterialStock(JObject jo)
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
                DtoHelper.WriteLogWorkDate("SAP原材料库存数据", "原材料库存数据SaveSAPMM_RawMaterialStock接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                List<SAP_MaterialStock> entity_list = JsonConvert.DeserializeObject<List<SAP_MaterialStock>>(getValue(jo, "Entity"));
                if (entity_list == null)
                {
                    result.success = false;
                    result.returnMsg = "参数不能为空";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                new MM_RawMaterialStock_Service().SaveSAPMM_RawMaterialStock(entity_list);
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
