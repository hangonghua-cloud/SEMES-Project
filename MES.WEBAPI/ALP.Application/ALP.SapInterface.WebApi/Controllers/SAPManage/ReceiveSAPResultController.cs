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
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Service.Resources;
using ALP.Application.Service.ToSAP;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("ReceiveSAPResult")]
    public class ReceiveSAPResultController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPresult")]
        public HttpResponseMessage SaveSAPresult(JObject jo)
        {
            try
            {
                DtoHelper.WriteLogWorkDate("SAP返回信息", "SAP返回信息SaveSAPresult接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);

                var data = JsonConvert.DeserializeObject<SAPresultEntity>(getValue(jo, "Entity"));
                //数据写入数据库
                new ToSAPService().SaveSAPresult(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }
        }
    }
}
