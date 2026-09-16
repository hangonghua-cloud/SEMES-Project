using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WeChat_WarningCommon;
using WeChat_WarningDTO;

namespace WHC.Framework.Business.Shared
{
    /// <summary>
    /// http 请求
    /// </summary>
    public class Http
    {
        public static string BaseApiUrl = ConfigurationManager.AppSettings["WebApi"];

        public static HttpResult<T> Post<T>(string url, object param)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("UserName", "admin");
            header.Add("FullName", "admin");
            HttpResult<T> ret = null;
            try
            {
                //ret = HttpHelper.PostApi<HttpResult<T>>(BaseApiUrl + url, param, header);    
                ret = HttpHelper.PostApi<HttpResult<T>>(BaseApiUrl + url, param);

            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    ret = new HttpResult<T> { Success = false, Error = new HttpError { Message = "API连接超时" } };
                }
                ret = new HttpResult<T> { Success = false, Error = new HttpError { Message = "请求被取消" } };
            }
            catch (SocketException ex)
            {
                ret = new HttpResult<T> { Success = false, Error = new HttpError { Message = "网络异常" } };
            }
            catch (Exception e)
            {
                ret = new HttpResult<T> { Success = false, Error = new HttpError { Message = e.Message } };
            }

            if (ret == null)
            {
                ret = new HttpResult<T>();
                ret.Success = false;
                ret.Error = new HttpError { Message = "空数据" };
            }
            if (!ret.Success && ret.Error == null)
            {
                ret.Error = new HttpError { Message = "" };
            }
            return ret;

        }


        public static HttpResult<List<T>> PostList<T>(string url, object param)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            HttpResult<List<T>> list = null;
            try
            {
                //list = HttpHelper.PostApi<HttpResult<List<T>>>(BaseApiUrl + url, param, header);
                list = HttpHelper.PostApi<HttpResult<List<T>>>(BaseApiUrl + url, param);
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    list = new HttpResult<List<T>> { Success = false, Error = new HttpError { Message = "API连接超时" } };
                }
                list = new HttpResult<List<T>> { Success = false, Error = new HttpError { Message = "请求被取消" } };
            }
            catch (SocketException ex)
            {
                list = new HttpResult<List<T>> { Success = false, Error = new HttpError { Message = "网络异常" } };
            }
            catch (Exception e)
            {
                list = new HttpResult<List<T>> { Success = false, Error = new HttpError { Message = e.Message } };
            }

            if (list == null)
            {
                list = new HttpResult<List<T>>();
                list.Success = false;
                list.Error = new HttpError { Message = "空数据" };
            }
            if (list.Result == null)
            {
                list.Result = new List<T>();
            }
            if (!list.Success && list.Error == null)
            {
                list.Error = new HttpError { Message = "" };
            }
            return list;
        }
        public static HttpWeChatSendResult Post<T>(string url, object param, string Access_token)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("UserName", "admin");
            header.Add("FullName", "admin");
            HttpWeChatSendResult ret = null;
            try
            {
                    ret = HttpHelper.PostApi<HttpWeChatSendResult>("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token=" + Access_token + "", param);

            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    ret = new HttpWeChatSendResult { Errcode = -1, Invaliduser = "API连接超时"  };
                }
                ret = new HttpWeChatSendResult { Errcode = -2, Invaliduser =  "请求被取消"  };
            }
            catch (SocketException ex)
            {
                ret = new HttpWeChatSendResult { Errcode = -3, Invaliduser = "网络异常"  };
            }
            catch (Exception e)
            {
                ret = new HttpWeChatSendResult { Errcode = -4, Invaliduser =  e.Message  };
            }

            if (ret == null)
            {
                ret = new HttpWeChatSendResult();
                ret.Errcode = -5;
                ret.Invaliduser = "空数据" ;
            }     
            return ret;

        }

        public static string Post<T>(string url, object param,int m)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            header.Add("UserName", "admin");
            header.Add("FullName", "admin");
            //   HttpWeChatSendResult ret = null;
            string ret = string.Empty;
            try
            {
                ret = HttpHelper.PostApiString("http://it.jushi.com/MessageManger/SendMessage/send_qy_wx_ty.do?json="+url, param);

            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    ret = "API连接超时" ;
                }
                ret ="请求被取消" ;
            }
            catch (SocketException ex)
            {
                ret = "网络异常" ;
            }
            catch (Exception e)
            {
                ret = e.Message;
            }

            if (ret == null)
            {
                ret = "空数据";
            }
            return ret;

        }
        public static HttpWeChatResult Get<T>(AccessToken Accesstoken)
        {
            HttpWeChatResult ret = null;
            try
            {
                ret = HttpHelper.PostApiGet<HttpWeChatResult>("https://qyapi.weixin.qq.com/cgi-bin/gettoken?Corpid=" + Accesstoken.Corpid + "&Corpsecret=" + Accesstoken.Corpsecret + "");
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    ret = new HttpWeChatResult { Errcode = "0", Errmsg = "API连接超时" };
                }
                ret = new HttpWeChatResult { Errcode = "0", Errmsg = "请求被取消" };
            }
            catch (SocketException ex)
            {
                ret = new HttpWeChatResult { Errcode = "0", Errmsg = "网络异常" };
            }
            catch (Exception e)
            {
                ret = new HttpWeChatResult { Errcode = "0", Errmsg = e.Message };
            }

            if (ret == null)
            {
                ret = new HttpWeChatResult();
                ret.Errcode = "0";
                ret.Errmsg = "空数据";
            }
            return ret;

        }

    }
}
