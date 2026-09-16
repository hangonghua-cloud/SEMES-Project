using ALP.Util.Commons;
using ALP.Util.Entitys;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Util.HttpInterface
{
    public class HttpHelperSAP
    {
        //private readonly string Url = ConfigurationManager.AppSettings["SAPUrl"] ?? "http://sappodev01.easpring.com.cn:50000/RESTAdapter/MES2/";
        /// <summary>
        /// 调用SAP
        /// </summary>
        /// <param name="requestData">数据结构</param>  
        public DataResultEntity PostToSAP(object requestData, SAPSettings SAPDto)
        {
            DataResultEntity result = new DataResultEntity();

            string url = SAPDto.Url;
            HttpHelper httpHelper = new HttpHelper(SAPDto.Username, SAPDto.Pwd, url);

            var res = httpHelper.Post(requestData, url);
            if (!string.IsNullOrEmpty(res))
            {
                result.Result = res;
                result.IsSuccess = true;
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = "返回的参数数据为空";
            }

            return result;
        }
        /// <summary>
        /// 调用接口参数
        /// </summary>
        public class SAPSettings
        {
            /// <summary>
            /// 获取成功
            /// </summary>
            public bool success { get; set; }
            /// <summary>
            /// 地址
            /// </summary>
            public string Url { get; set; }
            /// <summary>
            /// Username
            /// </summary>
            public string Username { get; set; }
            /// <summary>
            /// Pwd
            /// </summary>
            public string Pwd { get; set; }

        }
    }
}
