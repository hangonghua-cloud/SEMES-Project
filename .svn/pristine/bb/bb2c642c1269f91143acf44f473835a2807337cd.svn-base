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
    [RoutePrefix("BS_SAPBOM")]
    public class BS_SAPBOMController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: BOM基础数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPBS_BOM")]
        public HttpResponseMessage SaveSAPBS_BOM(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                return AjaxResult(false, "参数不能为空");
            }

            try
            {
                DtoHelper.WriteLogWorkDate("SAP物料BOM", "BOM接口SaveSAPBS_BOM接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);

                var data = JsonConvert.DeserializeObject<List<SAPBS_BOMEntity>>(getValue(jo, "Entity"));
                BS_BOMEntity bomEntity = data[0].BS_BOM;
                List<BS_BOMItemsEntity> bomItemsList = data[0].BS_BOMItems;
                if (bomEntity == null)
                {
                    return AjaxResult(false, "参数必填");
                }
                if (bomItemsList == null)
                {
                    return AjaxResult(false, "参数必填");
                }
                new BS_BOM_Service().SaveSAPBS_BOM(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }
        }
    }

}
