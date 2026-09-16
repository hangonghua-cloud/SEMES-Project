using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.AppManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.Service.AppManage;
using ALP.Application.Service.Resources;
using ALP.Application.UtilExtend;
using ALP.Application.WebApi.Controllers.API;
using ALP.Application.WebApi.Models.Account;
using ALP.Application.WebApi.Models.System;
using ALP.Util;
using ALP.Util.Extension;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace ALP.Application.WebApi.Controllers.SystemManage
{

    /// <summary>
    /// 系统基础信息接口控制器
    /// </summary>
    //[Auth]
    [RoutePrefix("System")]
    public class SystemController : ApiBaseController
    {
        SystemBLL bll = new SystemBLL();
        /// <summary>
        /// 校验系统版本号，返回是否需要更新
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost, Route("GetVersion")]
        public HttpResponseMessage GetVersion(JObject jo)
        {
            var result = new AppSystemModel();
            result.IsSuccess = true;
            result.IsUpdate = false;
            int appType = 1;//默认安卓系统
            try
            {
                //判断是否启用自动更新
                if (Config.GetValue("OpenAppUpdateOnline").ToString() != "1")
                {
                    result.IsUpdate = false;
                    result.NewVersion = "";
                    //result.returnMsg = "不需要更新！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string appVersion = "";
                if (jo["AppVersion"].IsEmpty())
                {
                    //是否成功
                    result.IsSuccess = false;
                    //返回App版本是否需要更新 
                    result.IsUpdate = false;
                    result.Msg = "SystemManage.SystemController.GetVersion.Tips_1";//无法获取当前版本号！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (!jo["AppType"].IsEmpty())
                {
                    if (jo["AppType"].ToString() == "2")
                    {
                        appType = 2;
                    }
                }

                if (appType == 1)
                {
                    appVersion = Config.GetValue("APKVersion").ToString();
                }
                else
                {
                    appVersion = Config.GetValue("IOSVersion").ToString();
                }

                if (appVersion != jo["AppVersion"].ToString())
                {
                    //返回App版本是否需要更新
                    result.IsUpdate = true;
                    result.Msg = "SystemManage.SystemController.GetVersion.Tips_2";//请更新系统！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    //返回App版本是否需要更新
                    result.IsUpdate = false;
                    result.Msg = "";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.IsUpdate = false;
                result.Msg = Language.GetText("SystemManage.SystemController.GetVersion.Tips_3", ex.Message);//"获取版本号失败：" + ex.Message;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 系统登录
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost, Route("Login")]
        public HttpResponseMessage Login(JObject jo)
        {
            AppRetLogindata ind = new AppRetLogindata();
            AppUserInfo us = new AppUserInfo();
            try
            {
                AppModelService _Service = new AppModelService();
                //校验账号、密码，并查询对应的角色权限
                var user = jo["UserCode"].ToString();
                var pwd = jo["password"].ToString();
                if (!string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(pwd))
                {
                    AppPassWordEntity ap = _Service.GetLogin(user, pwd);
                    if (ap.LoginCode)
                    {
                        ind.code = "100";
                        ind.ret = "SystemManage.SystemController.Login.Tips_1";
                        ind.UserCode = jo["UserCode"].ToString();
                        AppPayloaddata pd = new AppPayloaddata();
                        //Dictionary<string, object> keyValuePairs = new Dictionary<string, object> {
                        //     { "userName",ap.UserName },{ "userCode", jo["UserCode"].ToString() }};//此处需要生成Token
                        //pd.token = JwtHelp.SetJwtEncode(keyValuePairs);
                        pd.nickname = Language.GetText("SystemManage.SystemController.Login.Data_1");
                        pd.profile = "profile";
                        pd.hasLogin = true;
                        pd.isGoogle = true;
                        var list = _Service.GetPageDataTableList(user);
                        us.AppModelPageList = list.AppModelPageList;
                        us.AppModelBtnList = list.AppModelBtnList;
                        us.AppModelList = list.AppModelList;
                        us.AppRoleList = list.AppRoleList;
                        us.UserCode = jo["UserCode"].ToString();
                        us.UserName = ap.UserName;
                        us.PostName = ap.UserPostName;
                        us.LineName = ap.LineName;
                        us.LineCode = ap.LineCode;
                        us.WorkShop = ap.WorkShop;
                        us.JobCode = ap.JobCode;
                        us.JobName = ap.JobName;
                        us.Department_Name = ap.Department_Name;
                        us.WorkShopCode = ap.WorkShopCode;
                        us.FactoryCode = ap.FactoryCode;
                        us.FactoryName = ap.FactoryName;
                        pd.result = us;
                        ind.data = pd;
                    }
                    else
                    {
                        ind.code = "300";
                        ind.ret = ap.LoginMsg;
                        AppPayloaddata pd = new AppPayloaddata();
                        pd.hasLogin = false;
                        ind.data = pd;
                    }
                }
                else
                {
                    ind.code = "500";
                    ind.ret = "SystemManage.SystemController.Login.Tips_2";
                    AppPayloaddata pd = new AppPayloaddata();
                    pd.hasLogin = false;
                    ind.data = pd;
                }

                ////暂时先做测试用
                //if (jo["UserCode"].ToString() == "test" && jo["password"].ToString() == "123")
                //{ 
                //    ind.code = "100";
                //    ind.ret = "登录成功！";
                //    Payloaddata pd = new Payloaddata();
                //    pd.token = "";
                //    pd.nickname = "nickname--登录账号";
                //    pd.profile = "profile";
                //    pd.hasLogin = true;
                //    pd.isGoogle = true;

                //    us.AppModelPageList = _Service.GetPageDataTableList();
                //    us.AppModelBtnList = _Service.GetPageDataTableList1();
                //    us.AppModelList = _Service.GetPageDataTableList2();
                //    us.UserCode = jo["UserCode"].ToString();
                //    us.UserName = "PDA测试账号";
                //    us.RoleNameList = new List<string>();
                //    us.RoleNameList.Add("测试岗位1");
                //    us.RoleNameList.Add("测试岗位2");
                //    pd.result = us;
                //    ind.data = pd;
                //}
                //else
                //{
                //    ind.code = "300";
                //    ind.ret = "登录失败，账号或密码不正确！";
                //    Payloaddata pd = new Payloaddata();
                //    pd.hasLogin = false;
                //    ind.data = pd;
                //}
            }
            catch (Exception ex)

            {
                ind.code = "400";
                ind.ret = Language.GetText("SystemManage.SystemController.Login.Tips_3", ex.Message);// "登录失败：" + ex.Message
                AppPayloaddata pd = new AppPayloaddata();
                pd.hasLogin = false;
                ind.data = pd;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ind);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost, Route("UpdateAppPassword")]
        public HttpResponseMessage UpdateAppPassword(JObject jo)
        {
            AppRetLogindata ind = new AppRetLogindata();
            AppUserInfo us = new AppUserInfo();
            try
            {
                AppModelService _Service = new AppModelService();
                //校验账号、密码，并查询对应的角色权限
                var usercode = jo["UserCode"].ToString();
                var oldpassword = jo["OldPassword"].ToString();
                var newpassword = jo["NewPassword"].ToString();
                string msg = string.Empty;
                int n = 0;
                if (!string.IsNullOrEmpty(usercode) && !string.IsNullOrEmpty(oldpassword))
                {
                    AppPassWordEntity ap = _Service.GetLogin(usercode, oldpassword);
                    if (ap.LoginCode)
                    {
                        ind.code = "100";
                        ind.ret = "SystemManage.SystemController.Login.Tips_1";

                        //修改密码
                        n = _Service.UpdatePwd(usercode, newpassword, out msg);
                    }
                    else
                    {
                        ind.code = "300";
                        ind.ret = ap.LoginMsg;
                    }
                }
                else
                {
                    ind.code = "500";
                    ind.ret = "SystemManage.SystemController.Login.Tips_2";
                    AppPayloaddata pd = new AppPayloaddata();
                    pd.hasLogin = false;
                    ind.data = pd;
                }
            }
            catch (Exception ex)
            {
                ind.code = "400";
                ind.ret = Language.GetText("SystemManage.SystemController.Login.Tips_3", ex.Message);// "登录失败：" + ex.Message
                AppPayloaddata pd = new AppPayloaddata();
                pd.hasLogin = false;
                ind.data = pd;
            }
            return Request.CreateResponse(HttpStatusCode.OK, ind);
        }
    }
}