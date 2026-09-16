using ALP.Application;
using ALP.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ALP.Application.WebApi.Util;
using ALP.Application.WebApi.App_Start;
using ALP.Application.WebApi.Controllers.BaseManage;

namespace ALP.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //解决The model backing the 'SqlServerDbContext' context has changed since the database was created. Consider using Code First Migrations to update the database...
            System.Data.Entity.Database.SetInitializer<SqlServerDbContext>(null);
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new GlobalCustomHandleErrorAttribute());
            TimeHelper.Start();
            //GlobalConfiguration.Configuration.Filters.Add(new WebApiTrackerAttribute());
        }
    }
}
