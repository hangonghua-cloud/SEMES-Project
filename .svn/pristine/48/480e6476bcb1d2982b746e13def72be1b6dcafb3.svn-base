using ALP.Application.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ALP.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            var json = config.Formatters.JsonFormatter;
            //api访问响应请求
            config.Filters.Add(new Filter.APIFilterAttribute());
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            // 移除XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Filters.Add(new GlobalCustomHandleErrorAttribute());
        }
    }
}
