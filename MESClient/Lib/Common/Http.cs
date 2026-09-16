
using Lib.Model.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// http 请求
    /// </summary>
    public class Http
    {
        private static string Type = Lib.Common.Language.GetLocation();
        /// <summary>
        /// 后台接口地址
        /// </summary>
        public static string BaseApiUrl
        {
            get
            {
                var conns = ConfigurationManager.AppSettings["WebApi" + "_" + (string.IsNullOrEmpty(Type) ? "CN" : Type)]?.ToString();
                return conns;
            }
        }
        /// <summary>
        /// 文件上传接口
        /// </summary>

        public static string AddressUrl
        {
            get
            {
                var conns = ConfigurationManager.AppSettings["address" + "_" + (string.IsNullOrEmpty(Type) ? "CN" : Type)]?.ToString();
                return conns;
            }
        }

        public static HttpResult<T> Post<T>(string url, object param)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            header.Add("Language", Lib.Common.Language.GetLanguage());
            HttpResult<T> ret = null;
            try
            {
                ret = HttpHelper.PostApi<HttpResult<T>>(BaseApiUrl + url, param, header);
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

        public static HttpResult<HttpRows<T>> PostRows<P, T>(string url, HttpListRequest<P> param) where P : new()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            HttpResult<HttpRows<T>> list = null;
            try
            {
                list = HttpHelper.PostApi<HttpResult<HttpRows<T>>>(BaseApiUrl + url, param, header);
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    list = new HttpResult<HttpRows<T>> { Success = false, Error = new HttpError { Message = "API连接超时" } };
                }
                list = new HttpResult<HttpRows<T>> { Success = false, Error = new HttpError { Message = "请求被取消" } };
            }
            catch (SocketException ex)
            {
                list = new HttpResult<HttpRows<T>> { Success = false, Error = new HttpError { Message = "网络异常" } };
            }
            catch (Exception e)
            {
                list = new HttpResult<HttpRows<T>> { Success = false, Error = new HttpError { Message = e.Message } };
            }
            if (list == null)
            {
                list = new HttpResult<HttpRows<T>>();
                list.Success = false;
                list.Error = new HttpError { Message = "空数据" };
            }
            if (list.Result == null)
            {
                list.Result = new HttpRows<T>();
            }
            if (list.Result.rows == null)
            {
                list.Result.rows = new List<T>();
            }
            if (!list.Success && list.Error == null)
            {
                list.Error = new HttpError { Message = "" };
            }
            return list;
        }

        public static HttpResult<HttpRows1<T>> PostRows1<P, T>(string url, HttpListRequest<P> param) where P : new()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            HttpResult<HttpRows1<T>> list = null;
            try
            {
                list = HttpHelper.PostApi<HttpResult<HttpRows1<T>>>(BaseApiUrl + url, param, header);
            }
            catch (TaskCanceledException ex)
            {
                if (!ex.CancellationToken.IsCancellationRequested)
                {
                    list = new HttpResult<HttpRows1<T>> { Success = false, Error = new HttpError { Message = "API连接超时" } };
                }
                list = new HttpResult<HttpRows1<T>> { Success = false, Error = new HttpError { Message = "请求被取消" } };
            }
            catch (SocketException ex)
            {
                list = new HttpResult<HttpRows1<T>> { Success = false, Error = new HttpError { Message = "网络异常" } };
            }
            catch (Exception e)
            {
                list = new HttpResult<HttpRows1<T>> { Success = false, Error = new HttpError { Message = e.Message } };
            }
            if (list == null)
            {
                list = new HttpResult<HttpRows1<T>>();
                list.Success = false;
                list.Error = new HttpError { Message = "空数据" };
            }
            if (list.Result == null)
            {
                list.Result = new HttpRows1<T>();
            }
            if (list.Result.rows == null)
            {
                // list.Result.rows = new List<T>();
            }
            if (!list.Success && list.Error == null)
            {
                list.Error = new HttpError { Message = "" };
            }
            return list;
        }

        public static HttpResult<List<T>> PostList<P, T>(string url, HttpListRequest<P> param) where P : new()
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            HttpResult<List<T>> list = null;
            try
            {
                list = HttpHelper.PostApi<HttpResult<List<T>>>(BaseApiUrl + url, param, header);
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

        public static HttpResult<List<T>> PostList<T>(string url, object param)
        {
            Dictionary<string, string> header = new Dictionary<string, string>();
            //header.Add("UserName", SysInfo.LoginUser.Account);
            //header.Add("FullName", SysInfo.LoginUser.UserName);
            HttpResult<List<T>> list = null;
            try
            {
                list = HttpHelper.PostApi<HttpResult<List<T>>>(BaseApiUrl + url, param, header);
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
    }
}
