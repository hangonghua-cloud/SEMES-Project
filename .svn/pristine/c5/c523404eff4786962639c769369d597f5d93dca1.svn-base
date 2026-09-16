using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.Application.Service.Resources;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    [Auth]
    [RoutePrefix("PL_ProcessOfOperationsAttr")]
    public class PL_ProcessOfOperationsAttrController : ApiBaseController
    {
        PL_ProcessOfOperationsAttr_Service _plProcessAttrService = new PL_ProcessOfOperationsAttr_Service();

        #region 查询工单工序属性
        [HttpPost]
        [Route("GetPLProcessOfOperationsAttr")]
        public HttpResponseMessage GetPLProcessOfOperationsAttr(JObject jo)
        {
            try
            {
                string queryJson = getValue(jo, "queryJson");

                var data = _plProcessAttrService.GetPageDataTableList(null, queryJson);

                return AjaxResult(true, Language.GetText("Common.SearchSuccess"), data);//查询成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.SearchError2"));//查询失败
            }
        }
        #endregion

        #region 批量保存工单工序属性
        [HttpPost]
        [Route("SaveBatchPLProcessOfOperationsAttr")]
        public HttpResponseMessage SaveBatchPLProcessOfOperationsAttr(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<PL_ProcessOfOperationsAttrEntity>>(getValue(jo, "data"));
                foreach (var item in data)
                {
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                    item.IsEnabled = true;
                }

                string msg = "";
                _plProcessAttrService.SaveEntity_List(true, "", data,out msg);

                return AjaxResult(true, Language.GetText("Common.SearchSuccess"), data);//查询成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.SearchError2"));//查询失败
            }
        }
        #endregion
    }
}