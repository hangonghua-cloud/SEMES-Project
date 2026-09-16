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
using ALP.Application.Service.PlanManage;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("PL_SAPProductionOrder")]
    public class PL_SAPProductionOrderController : ApiBaseController
    {
        PL_ProductionOrder_Service _productOrderService = new PL_ProductionOrder_Service();//订单s


        /// <summary>
        /// 创建：jpf
        /// 时间：2024-3-19 15:59:40
        /// 描述：SAP工单状态同步
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateSAP_ProductionOrderSatate")]
        public HttpResponseMessage UpdateSAP_ProductionOrderSatate(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                DtoHelper.WriteLogWorkDate("SAP工单状态", "工单状态接口UpdateSAP_ProductionOrderSatate接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                var entity_list = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "Entity"));
                new PL_ProductionOrder_Service().UpdateSAP_ProductionOrderSatate(entity_list);
                result.success = true;
                result.returnMsg = "操作成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 创建：jpf
        /// 时间：2024-3-19 15:59:40
        /// 描述：SAP销售订单同步
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPPL_ProductionOrder")]
        public HttpResponseMessage SaveSAPPL_ProductionOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                DtoHelper.WriteLogWorkDate("SAP销售订单信息", "销售接口SaveSAPPL_ProductionOrder接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                var entity_list = JsonConvert.DeserializeObject<List<SAPPL_ProductionOrderEntity>>(getValue(jo, "Entity"));
                if (entity_list == null)
                {
                    result.success = false;
                    result.returnMsg = "参数不能为空";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var Resultmsg = _productOrderService.SapProductOrderImport(entity_list);
                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? "操作成功" : Resultmsg;//条数据;
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
