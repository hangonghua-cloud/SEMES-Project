using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.UtilExtend;
using ALP.Application.WebApi.Controllers.API;
using ALP.Application.WebApi.Models.System;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        /// 获取车间集合接口
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("WorkShop")]
        public HttpResponseMessage GetWorkShop(GetWorkShopParms parms)
        {
            var result = new ResponseResult<DataItemDetailEntity>();
            try
            {
                var data = bll.LoadWorkShopDictionary();
                result.resultData = data.ToList();
            }
            catch (Exception ex)
            {
                WriteLog(ALP.Application.Service.Resources.Language.GetText("SystemManage.SystemController.Tips_1",ex.ToString()));//System/GetWorkShop接口异常：{ex.ToString()}
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取年月集合接口
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("GetYearMonth")]
        public HttpResponseMessage GetYearMonth(GetWorkShopParms parms)
        {
            var result = new ResponseResult<DataItemDetailEntity>();
            try
            {
                //根据当前月份获取N+3月列表
                var cur_Month = DateTime.Now.Month;


                var data = bll.LoadYearMonthDictionary().OrderBy(u=>u.SortCode);
                result.resultData = data.ToList();
            }
            catch (Exception ex)
            {
                WriteLog(ALP.Application.Service.Resources.Language.GetText("SystemManage.SystemController.Tips_3",ex.ToString()));//System/GetYearMonth接口异常：{ex.ToString()}
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
        }
      
        /// <summary>
        ///获取APP用户角色
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRoles_PDA")]
        public HttpResponseMessage GetRoles_PDA()
        {
            var result = new ResponseResult();
            result.success = false;
            result.resultData = null;
            try
            {
                //查询角色
                var DtRole = bll.GetRoles_PDA();
                result.resultData = DtRole;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)System.Net.HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 获取用户部门和数据隔离标签
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserInfoConfig")]
        public HttpResponseMessage GetUserInfoConfig(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                result.resultData = null;
                if (jo == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.SystemController.Tips_6");//参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var Code = getValue(jo, "Code");
                var model = bll.GetEntity(Code);
                if (model != null)
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.SystemController.Tips_7");//查询成功！
                    result.resultData = model;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.SystemController.Tips_8");//执行失败！改Code不存在！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }



    }
}