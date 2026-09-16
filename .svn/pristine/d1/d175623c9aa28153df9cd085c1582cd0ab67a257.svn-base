using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Service.MaterialManage;
using ALP.Application.Service.Resources;
using ALP.Application.WebApi.Common;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("MM_SAPProductDispatchItem")]
    public class MM_SAPProductDispatchItemController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期:2024-3-11 08:50:39
        /// 任务编号: 发货单接口
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPMM_ProductDispatchItem")]
        public HttpResponseMessage SaveSAPMM_ProductDispatchItem(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<SAPMM_ProductDispatchItemEntity>>(getValue(jo, "Entity"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, "参数不能为空");

                DtoHelper.WriteLogWorkDate("SAP发货单接口", "发货单数据SaveSAPMM_ProductDispatchItem接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);

                new MM_ProductDispatchItem_Service().SaveSAPMM_ProductDispatchItem(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }
        }
    }
}
