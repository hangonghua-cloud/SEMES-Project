using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Busines.SystemManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Data;

namespace ALP.Application.WebApi.Controllers.SystemManage
{
    public class Sys_DataSegregateLabelController : ApiBaseController
    {
        [HttpGet]
        [Route("Sys_DataSegregate/test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent(ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")//测试成功  0001
            };
            return result1;
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sys_DataSegregate/Get_PageData")]
        public HttpResponseMessage Get_PageData(JObject jo)
        {
            Sys_DataSegregateLabel_BLL bll = new Sys_DataSegregateLabel_BLL();
            var result = new ResponseResult();
            result.resultData = null; 
            try
            {

                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_2");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询不在指定组标签的标签-分页查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sys_DataSegregate/Get_PageData_notGroup")]
        public HttpResponseMessage Get_PageData_notGroup(JObject jo)
        {
            Sys_DataSegregateLabel_BLL bll = new Sys_DataSegregateLabel_BLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_2");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData_notGroup(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 查询-数据隔离标签管理
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sys_DataSegregate/GetDataSegregateLabel")]

        public HttpResponseMessage GetDataSegregateLabel(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string PersonCode = getValue(jo, "PersonCode");
            try
            {
                Sys_DataSegregateLabel_BLL orderBLL = new Sys_DataSegregateLabel_BLL();
                int  personSumLabel = orderBLL.GetDataSegregateLabel(PersonCode);
                result.resultData = personSumLabel;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
     
        /// <summary>
        ///删除-数据隔离标签管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Sys_DataSegregate/Delete")]
        public HttpResponseMessage Delete(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string Id = getValue(jo, "Id");
            string userName = getValue(jo, "userName");
            if (string.IsNullOrEmpty(Id))
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_7");//Id不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (string.IsNullOrEmpty(userName))
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.Sys_DataSegregateLabelController.Tips_8");//userName 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                Sys_DataSegregateLabel_BLL bll = new Sys_DataSegregateLabel_BLL();  
                string msg = "";
                bool bresut = bll.Delete_Entity(Id, userName, out msg);
                result.success = bresut;
                if (bresut)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}