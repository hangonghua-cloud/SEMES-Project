using ALP.Application.UtilExtend.JsonUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Filters;
using WebGrease;

namespace ALP.Application.WebApi.App_Start
{
    public class GlobalCustomHandleErrorAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var code = -1;
            var message = "服务器异常，请求失败!请联系管理员";
            //获取action的请求参数
            var requestParameters = JsonHelper.SerializeObject(actionExecutedContext.ActionContext.ActionArguments.Values);
            actionExecutedContext.Response = GetResponseMessage(code, message);
           // logger.Debug(actionExecutedContext.Exception, "服务器异常!" + actionExecutedContext.Exception.Message + "——堆栈信息：" + actionExecutedContext.Exception.StackTrace + "——请求参数为：{0}", requestParameters);
        }
        private HttpResponseMessage GetResponseMessage(int code, string message)
        {
            var resultModel = new ApiModelsBase() { Code = code, Message = message };

            return new HttpResponseMessage()
            {
                Content = new ObjectContent<ApiModelsBase>(
                    resultModel,
                    new JsonMediaTypeFormatter(),
                    "application/json"
                    )
            };
        }
    }

    internal class ApiModelsBase
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }
}