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
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("PL_SAPPurchaseOrder")]
    public class PL_SAPPurchaseOrderController : ApiBaseController
    {
        /// <summary>
        // 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-6 16:27:58
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPPL_PurchaseOrder")]
        public HttpResponseMessage SaveSAPPL_PurchaseOrder(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<PL_PurchaseOrderEntity>>(getValue(jo, "Entity"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, "参数不能为空");

                DtoHelper.WriteLogWorkDate("SAP采购订单数据", "采购订单SaveSAPPL_PurchaseOrder接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
                
                new PL_PurchaseOrder_Service().SaveSAPPL_PurchaseOrder(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }

        }
    }
}
