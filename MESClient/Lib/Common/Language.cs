using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Common
{
    public static class Language
    {
        //private static int LanguageID = 2052;
        private static string LanguageID = "zh-CN";
        private static string LocationID = "CN";
        public static string Default = "zh-CN";


        #region 获取/设置当前工厂所在地
        /// <summary>
        /// 设置工厂所在地
        /// </summary>
        /// <param name="LangID"></param>
        public static void SetLocation(string LangID)
        {
            LocationID = LangID;
        }
        /// <summary>
        /// 获取当前工厂所在地
        /// </summary>
        /// <returns></returns>
        public static string GetLocation()
        {
            return LocationID;
        }
        #endregion

        #region 获取/设置当前语言
        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="LangID"></param>
        public static void SetLanguage(string LangID)
        {
            LanguageID = LangID;
        }
        /// <summary>
        /// 获取当前语言
        /// </summary>
        /// <returns></returns>
        public static string GetLanguage()
        {
            return LanguageID;
        }
        #endregion
        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        private static string GetJsonFile()
        {
            string currDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Resources\";
            string JsonFile = currDir + @"zh-CN.json";
            switch (LanguageID)
            {
                //case 1066://越南语
                case "vi-VN"://越南语
                    JsonFile = currDir + @"vi-VN.json";
                    break;
                //case 2052://简体中文
                case "zh-CN"://简体中文
                    JsonFile = currDir + @"zh-CN.json";
                    break;
                case "th-TH"://泰语
                    JsonFile = currDir + @"th-TH.json";
                    break;
                case "en-US"://英语
                    JsonFile = currDir + @"en-US.json";
                    break;
            }

            string result = "";
            using (StreamReader r = new StreamReader(JsonFile, Encoding.Default))
            {
                result = r.ReadToEnd();
            }
            return result;
        }

        #region 根据不同语言替换不同的方法
        /// <summary>
        /// 根据不同语言替换不同的方法  停用
        /// </summary>
        /// <param name="actions"></param>
        public static void SetVoidLanguage(params Action[] actions)
        {

            switch (LanguageID)
            {
                case "zh-CN"://简体中文
                    actions[0]();
                    break;
                case "vi-VN"://越南语
                    actions[1]();
                    break;
                case "th-TH"://泰语
                    actions[2]();
                    break;
                case "en-US"://英语
                    actions[3]();
                    break;
            }
        }

        #endregion

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static string GetText(string Node, string _default = "")
        {
            string result = _default;
            if (Node != null)
            {
                string[] arr = Node.Split('.');
                JObject jobj = JObject.Parse(GetJsonFile());

                for (int x = 0; x < arr.Length; x++)
                {
                    if (jobj == null)
                    {
                        return result;
                    }
                    if (x == arr.Length - 1)
                    {
                        result = jobj[arr[x]] == null ? _default : jobj[arr[x]].ToString();
                        break;
                    }
                    jobj = (JObject)jobj[arr[x]];
                }
            }

            return result;
        }
        /// <summary>
        /// 获取 带参
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="_default"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetTextWithParams(string Node, string _default = "", params string[] param)
        {
            string result = Node;
            if (Node != null)
            {
                result = GetText(Node, _default);
                result = string.Format(result, param);
            }
            return result;
        }
    }
}
