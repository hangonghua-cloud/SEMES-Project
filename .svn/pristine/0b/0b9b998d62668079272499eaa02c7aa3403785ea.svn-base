using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Resources
{
    public static class Language
    {
        //private static int LanguageID = 2052;
        private static string LanguageID = "zh-CN";

        public static void SetLanguage(string LangID)
        {
            LanguageID = LangID;
        }
        /// <summary>
        /// 读取
        /// </summary>
        /// <returns></returns>
        private static string GetJsonFile()
        {
            string currDir = System.AppDomain.CurrentDomain.BaseDirectory + @"Content\Resources\Language\";
            string JsonFile = currDir + @"zh-CN.json";
            Encoding encoder = Encoding.UTF8;

            if (LanguageID == "vi_VN")
                LanguageID = "vi-VN";

            switch (LanguageID)
            {
                //case 1066://越南语
                case "vi-VN"://越南语
                    JsonFile = currDir + @"vi-VN.json";
                    break;
                //case 2052://简体中文
                case "zh-CN"://简体中文
                    JsonFile = currDir + @"zh-CN.json";
                    encoder = Encoding.UTF8;
                    break;
                case "th-TH"://泰国语
                    JsonFile = currDir + @"th-TH.json";
                    break;
                case "en-US"://英语
                    JsonFile = currDir + @"en-US.json";
                    break;
            }

            string result = "";
            using (StreamReader r = new StreamReader(JsonFile, encoder))
            {
                result = r.ReadToEnd();
            }
            return result;
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Node"></param>
        /// <param name="_default"></param>
        /// <returns></returns>
        public static string GetText(string Node)
        {
            string result = Node;
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
                        result = jobj[arr[x]] == null ? Node : jobj[arr[x]].ToString();
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
        public static string GetText(string Node, params string[] param)
        {
            string result = Node;
            if (Node != null)
            {
                result = GetText(Node);
                result = string.Format(result, param);
            }
            return result;
        }

    }
}
