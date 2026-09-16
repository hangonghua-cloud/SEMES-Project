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
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.MaterialManage;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("MM_SAPReceiptNotice")]
    public class MM_SAPReceiptNoticeController : ApiBaseController
    {
        /// <summary>
        // 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-6 16:27:58
        /// 任务编号: 收料通知单
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPMM_ReceiptNotice")]
        public HttpResponseMessage SaveSAPMM_ReceiptNotice(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<MM_ReceiptNoticeEntity>>(getValue(jo, "Entity"));
                if (data == null)
                {
                    return AjaxResult(false, "参数不能为空");
                }
                DtoHelper.WriteLogWorkDate("SAP收料通知单同步", "收料通知单SaveSAPMM_ReceiptNotice接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);
               
                new MM_ReceiptNotice_Service().SaveSAPMM_ReceiptNotice(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }

        }
    }
}
