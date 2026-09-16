using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Siemens.SimaticIT.BasicDataManageFBLib.CommandHandler
{
    public class LogPost
    {
        public string HttpPostData(string url, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(url) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/json";
                //request.ContentType = "text;charset=UTF-8";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return err;
            }
        }
    }


    public class PostEnt
    {
        public string KeyValue { get; set; }
        public QM_LogManagementEntity Entity { get; set; }
    }

    public class QM_LogManagementEntity
    {
        #region 表: QM_LogManagement 实体类: QM_LogManagement 

        /// <summary>
        /// ID
        /// </summary>
        public string ID { get; set; } = "";

        /// <summary>
        /// 日志编号
        /// </summary>
        public string LogCode { get; set; } = "";

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; } = "";

        /// <summary>
        /// 修改ID
        /// </summary>
        public string ModifyId { get; set; } = "";

        /// <summary>
        /// 修改方式
        /// </summary>
        public string ModifyWay { get; set; } = "";

        /// <summary>
        /// 修改内容
        /// </summary>
        public string ModifyContent { get; set; } = "";

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string UpdateByName { get; set; } = "";

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTimeOffset? UpdateDateTime { get; set; }



        #endregion
    }
}
