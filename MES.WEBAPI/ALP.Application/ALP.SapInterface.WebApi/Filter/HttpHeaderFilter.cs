using ALP.WebApi.Util;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Filters;

namespace ALP.WebApi.Filter
{
    /// <summary>
    /// Swagger接口帮助请求头标注过滤器
    /// </summary>
    public class HttpHeaderFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="schemaRegistry"></param>
        /// <param name="apiDescription"></param>
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();
            var filterLine = apiDescription.ActionDescriptor.GetFilterPipeline();
            var isAuth = filterLine.Select(d => d.Instance).Any(x => x is IAuthorizationFilter);
            var allow = apiDescription.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
            if (isAuth && !allow)
            {
                operation.parameters.Add(new Parameter
                {
                    name = RequestHeaderKey.UserCode.ToString(),
                    @in = "header",
                    description = "用户主键ID",
                    required = true,
                    type = "string"
                });
                operation.parameters.Add(new Parameter
                {
                    name = RequestHeaderKey.UserName.ToString(),
                    @in = "header",
                    description = "用户名称",
                    required = false,
                    type = "string"
                });
                operation.parameters.Add(new Parameter
                {
                    name = RequestHeaderKey.Sign.ToString(),
                    @in = "header",
                    description = "签名",
                    required = false, //TODO: 开启签名验证后需要设置为True
                    type = "string"
                });
                operation.parameters.Add(new Parameter
                {
                    name = RequestHeaderKey.TerminalType.ToString(),
                    @in = "header",
                    description = "终端类型",
                    required = false,
                    type = "string"
                });
                operation.parameters.Add(new Parameter
                {
                    name = RequestHeaderKey.UDID.ToString(),
                    @in = "header",
                    description = "设备标识码",
                    required = false,
                    type = "string"
                });
            }
        }
    }
}