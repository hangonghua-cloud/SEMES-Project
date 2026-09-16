using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Common
{
    public class PubFunction
    {
        public static string ToJson(object obj)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);

            Newtonsoft.Json.JsonSerializer json = new Newtonsoft.Json.JsonSerializer();
            json.Serialize(new JsonTextWriter(sw), obj);

            return sb.ToString();
        }
        /// <summary>
        /// 指定Post地址使用Get 方式获取全部字符串
        /// </summary>
        /// <param name="content">Post提交数据内容(utf-8编码的)</param>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static string Post(string content, string key)
        {
            string result = "";
            string url = Http.BaseApiUrl + GetApi(key);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.ContentType = "application/json";
            request.Headers.Add("Language", System.Web.HttpUtility.UrlEncode(Lib.Common.Language.GetLanguage()));

            #region 添加Post 参数
            byte[] data = Encoding.UTF8.GetBytes(content);
            request.ContentLength = data.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();
            }
            #endregion

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            Stream stream = resp.GetResponseStream();
            //获取响应内容
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 获取一个类指定的属性值
        /// </summary>
        /// <param name="info">object对象</param>
        /// <param name="field">属性名称</param>
        /// <returns></returns>
        public static object GetPropertyValue(object info, string field)
        {
            if (info == null) return null;
            Type t = info.GetType();
            IEnumerable<System.Reflection.PropertyInfo> property = from pi in t.GetProperties() where pi.Name.ToLower() == field.ToLower() select pi;
            return property.First().GetValue(info, null);
        }

        public static string GetApi(string key)
        {
            string api = "";
            switch (key)
            {
                case "Login":
                    api = @"System/login";
                    break;
                case "UpdatePassword":
                    api = "System/UpdateAppPassword";
                    break;
                case "UpdateLog":
                    api = "Base/GetUpdateLog";
                    break;
                default:
                    break;
            }
            return api;
        }
    }
}
