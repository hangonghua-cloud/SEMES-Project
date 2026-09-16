using ALP.Application.WebApi.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ALP.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            var json = config.Formatters.JsonFormatter;
            // 解决json序列化时的循环引用问题
            json.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            // 移除XML序列化器
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            ////跨域配置
            //config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
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
