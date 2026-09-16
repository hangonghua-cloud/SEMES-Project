using ALP.Application.UtilExtend;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http.Filters;

namespace ALP.WebApi.Filter
{
    public class APIFilterAttribute : ActionFilterAttribute
    {
        string type = "MAPP";
        #region 请求
        /// <summary>
        /// 请求之前
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                string language = this.GetHeaderByKey(actionContext, Util.RequestHeaderKey.Language);
                if (!string.IsNullOrEmpty(language))
                {
                    ALP.Application.Service.Resources.Language.SetLanguage(language);
                }

                base.OnActionExecuting(actionContext);

            }
            catch (Exception ex)
            {
                LogExtends.WriteLog("ActionBefor---StackTrace:" + ex.StackTrace.ToString());
            }


        }
        public string GetHeaderByKey(System.Web.Http.Controllers.HttpActionContext actionContext, Util.RequestHeaderKey key)
        {
            IEnumerable<string> keyValueList;
            string k = key.ToString();
            if (actionContext.Request.Headers.TryGetValues(key.ToString(), out keyValueList))
            {
                return keyValueList.FirstOrDefault();
            }
            return string.Empty;
        }

        #endregion

        #region 响应

        /// <summary>
        /// 请求响应
        /// </summary>
        /// <param name="context"></param>

        public override void OnActionExecuted(System.Web.Http.Filters.HttpActionExecutedContext context)
        {
            try
            {
                //var contextMsg = context.Response.Content.ReadAsStringAsync().Result;
                //if (!string.IsNullOrEmpty(contextMsg))
                //{
                //    var obj = JsonConvert.DeserializeObject<dynamic>(contextMsg);
                //    string msg = obj["returnMsg"];
                //    var newMsg = ALP.Application.Service.Resources.Language.GetText(msg);

                //    obj["returnMsg"] = newMsg;
                //    //context.Response.Content..ReadAsStringAsync().wr = JsonConvert.SerializeObject(obj);
                //}
                base.OnActionExecuted(context);


            }
            catch (Exception ex)
            {
                LogExtends.WriteLog("ActionAfter---StackTrace:" + ex.StackTrace.ToString());
            }
        }


        #endregion
    }
}