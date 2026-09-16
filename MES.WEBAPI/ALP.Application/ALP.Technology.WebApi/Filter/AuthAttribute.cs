using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using ALP.WebApi.Util;
using Newtonsoft.Json;

using ALP.WebApi.Models;
namespace ALP.WebApi.Filter
{
    /// <summary>
    /// 接口请求认证授权特性
    /// TODO: 实现校验操作用户登录状态，操作权限等
    /// </summary>
    public class AuthAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ///此处可做多种校验拓展
            IEnumerable<string> signArray;
            if (!actionContext.Request.Headers.TryGetValues(RequestHeaderKey.Sign.ToString(), out signArray))
            {
                ///TODO: 签名校验
            }
            ///TODO:目前校验UseCode必传
            IEnumerable<string> userCodeArray;
            if (!actionContext.Request.Headers.TryGetValues(RequestHeaderKey.UserCode.ToString(), out userCodeArray))
            {
                return false;
            }
            return true;
        }
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
            var response = actionContext.Response ?? new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.Forbidden;
            response.Content = new StringContent(JsonConvert.SerializeObject(new BaseResponseMessage
            {
                success = false,
                statusCode = "403",
                returnMsg = "校验失败，无权调用此服务,请确保请求参数传递正确"
            }), Encoding.UTF8, "application/json");
        }
    }
}