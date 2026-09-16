using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.Service.AppManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Application.WebApi.Models.Account;
using ALP.Application.WebApi.Models.System;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.SystemManage
{
    /// <summary>
    /// PDA系统应用控制器
    /// </summary>
    public class SysController : ApiBaseController
    {
        ///// <summary>
        ///// 校验系统版本号，返回是否需要更新
        ///// </summary>
        ///// <param name="jo"></param>
        ///// <returns></returns>
        //[HttpPost, Route("GetVersion")]
        //public HttpResponseMessage GetVersion(JObject jo)
        //{
        //    var result = new AppSystemModel();
        //    result.IsSuccess = true;
        //    result.IsUpdate = false;
        //    int appType = 1;//默认安卓系统
        //    try
        //    {
        //        //判断是否启用自动更新
        //        if (Config.GetValue("OpenAppUpdateOnline").ToString() != "1")
        //        {
        //            result.IsUpdate = false;
        //            result.NewVersion = "";
        //            //result.returnMsg = "不需要更新！";
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //        }

        //        string appVersion = "";
        //        if (jo["AppVersion"].IsEmpty())
        //        {
        //            result.IsSuccess = false;
        //            result.IsUpdate = false;
        //            result.Msg = "无法获取当前版本号！";
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //        }

        //        if (!jo["AppType"].IsEmpty())
        //        {
        //            if (jo["AppType"].ToString() == "2")
        //            {
        //                appType = 2;
        //            }
        //        }

        //        if (appType == 1)
        //        {
        //            appVersion = Config.GetValue("APKVersion").ToString();
        //        }
        //        else
        //        {
        //            appVersion = Config.GetValue("IOSVersion").ToString();
        //        }
        //        //服务器端版本
        //        Version v1_server = new Version(appVersion.ToLower().Replace("v",""));
        //        //手机端版本
        //        Version v2_pda = new Version(jo["AppVersion"].ToString().ToLower().Replace("v", ""));              
        //        if (v1_server > v2_pda) 
        //        {
        //            result.IsUpdate = true;
        //            result.Msg = "请更新系统！";
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //        }
        //        else
        //        {
        //            result.IsUpdate = false;
        //            result.Msg = "";
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.IsSuccess = false;
        //        result.IsUpdate = false;
        //        result.Msg = "获取版本号失败：" + ex.Message;
        //        return Request.CreateResponse(HttpStatusCode.OK, result);
        //    }
        //}
        
 
    }
}
