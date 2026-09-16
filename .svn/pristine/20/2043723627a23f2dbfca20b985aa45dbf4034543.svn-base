using ALP.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ALP.WebApi.Common
{
    /// <summary>
    /// URL工具类
    /// </summary>
    public class URLTool
    {
        /// <summary>
        /// 资源地址
        /// </summary>
        private static string _resource_url = Config.GetValue("APP_Setup_File_URL");
        /// <summary>
        /// 获取完整的app下载地址
        /// </summary>
        /// <param name="appName"></param>
        /// <returns></returns>
        public static string GetAPPUploadURL(string appName)
        {
            if (string.IsNullOrEmpty(_resource_url))
                return "";
            return $"{_resource_url}/{appName}";
        }
    }
}