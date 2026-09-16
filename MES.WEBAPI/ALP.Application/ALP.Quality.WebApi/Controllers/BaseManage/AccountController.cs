using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Application.WebApi.Models.Account;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 操作工账号管理控制器
    /// </summary>
    [Auth]
    [RoutePrefix("Account")]
    public class AccountController : ApiBaseController
    {
        /// <summary>
        /// 分页获取操作工账号集合接口
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [HttpPost, Route("List")]
        public HttpResponseMessage GetAccountList(GetAccountListParms parms)
        {
            var result = new ResponseResult();
            try
            {
                if (parms != null && parms.PagingParms != null)
                {
                    Pagination pagination = parms.PagingParms;
                    var watch = CommonHelper.TimerStart();
                    BaseAccountService service = new BaseAccountService();
                    var data = service.LoadAccountList(parms.FirstName, parms.LastName, parms.UserEnCode, parms.PagingParms, CurrentAccount.UserCode);
                    var JsonData = new
                    {
                        rows = data,
                        total = pagination.total,
                        page = pagination.page,
                        records = pagination.records,
                        costtime = CommonHelper.TimerEnd(watch)
                    };
                    result.resultData = JsonData;
                }
                else
                {
                    result.success = false;
                    result.returnMsg = "操作失败，请传递请求参数";
                }
            }
            catch (Exception ex)
            {
                WriteLog($@"Account/List接口异常：{ex.ToString()}");
                result.success = false;
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 保存操作工账号接口
        /// </summary>
        /// <param name="parms"></param>
        /// <returns></returns>
        [HttpPost, Route("Save")]
        public HttpResponseMessage SaveAccount(MESBaseAccountEntity parms)
        {
            var result = new ResponseResult<object>();
            try
            {
                if (parms != null)
                {
                    WriteLog($"Account/SaveAccount接口请求参数{JsonConvert.SerializeObject(parms)}");
                    BaseAccountService service = new BaseAccountService();
                    var data = service.SaveEntity(parms.PrimaryKey, parms);
                    result.success = data.result;
                    result.returnMsg = data.msg;
                }
                else
                {
                    result.success = false;
                    result.returnMsg = "操作失败，请传递请求参数";
                }
            }
            catch (Exception ex)
            {
                WriteLog($@"Account/SaveAccount接口异常：{ex.ToString()}");
                result.success = false;
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpPost, Route("Delete")]
        public HttpResponseMessage Delete(DeleteAccountParms parms)
        {
            var result = new ResponseResult<object>();
            try
            {
                if (parms != null && !string.IsNullOrEmpty(parms.ID))
                {
                    BaseAccountService service = new BaseAccountService();
                    var data = service.DeleteEntity(parms.ID, CurrentAccount.UserCode, CurrentAccount.UserName);
                    result.success = data.result;
                    result.returnMsg = data.msg;
                }
                else
                {
                    result.success = false;
                    result.returnMsg = "操作失败，请传递请求参数";
                }
            }
            catch (Exception ex)
            {
                WriteLog($@"Account/SaveAccount接口异常：{ex.ToString()}");
                result.success = false;
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
    }
}