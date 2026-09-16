using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

using Newtonsoft.Json;

using ALP.WebApi.Models;
using ALP.WebApi.Util;
namespace ALP.WebApi.Filter
{
    public class LoginAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            ///可选择校验必传请求头参数
            IEnumerable<string> udidArray;
            if (!actionContext.Request.Headers.TryGetValues(RequestHeaderKey.UDID.ToString(), out udidArray))
            {
                //return false;
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
                returnMsg = "请求失败，请确保请求参数传递正确"
            }), Encoding.UTF8, "application/json");
        }
    }
}