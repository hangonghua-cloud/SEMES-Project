using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using ALP.Application.WebApi.APIModel;
using ALP.Data;
using ALP.WebApi.Models;
using ALP.WebApi.Util;
using FastReport;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALP.Application.WebApi.Controllers.API
{
    public class ApiBaseController : ApiController
    {
        private AccountEntity _accountEntity = null;
        //public static string Constr = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
        //public static string RedisConstr = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;
        /// <summary>
        /// Converts the validation result to error response.
        /// </summary>
        /// <param name="validationResult">The validation result.</param>
        /// <param name="statusCode"> </param>
        /// <returns>Error response</returns>
        protected HttpResponseMessage ConvertValidationResultToErrorResponse(ValidationResult validationResult, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {

            if (validationResult != null && !validationResult.IsValid)
            {
                var httpError = new HttpError();
                var errors = new List<ApiValidationFailure>();

                foreach (var error in validationResult.Errors)
                {
                    errors.Add(new ApiValidationFailure() { PropertyName = error.PropertyName, ErrorMessage = error.ErrorMessage });
                }
                return Request.CreateErrorResponse(statusCode, httpError);
            }

            return null;
        }

        /// <summary>
        /// Converts the Api Errors to error response.
        /// </summary>
        /// <param name="validationResult">The Api Errors.</param>
        /// <param name="statusCode"> </param>
        /// <returns>Error response</returns>
        protected HttpResponseMessage ConvertValidationResultToErrorResponse(List<ApiErrors> apiErrorsList, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {

            if (apiErrorsList != null && apiErrorsList.Count > 0)
            {

                var httpError = new HttpError();
                foreach (var errors in apiErrorsList)
                {
                    var id = errors.Id ?? "0";
                    if (!httpError.ContainsKey(id))
                        httpError.Add(id, errors.Errors);
                }

                return Request.CreateErrorResponse(statusCode, httpError);
            }

            return null;
        }

        protected List<ApiErrors> GetUnhandledExceptionError(Exception exception)
        {
            var validationFailureList = new List<ApiValidationFailure>
                {
                    new ApiValidationFailure() { PropertyName = "Unhandled Exception", ErrorMessage = "An unhandled exception occurred." }
                };
            var apiErrorsList = new List<ApiErrors>();
            var apiErrors = new ApiErrors
            {
                Id = "0",
                Errors = validationFailureList
            };
            apiErrorsList.Add(apiErrors);
            return apiErrorsList;
        }

        protected ApiErrors GetApiError(string propertyName, string errorMessage)
        {
            var validationFailureList = new List<ApiValidationFailure>
            {
                new ApiValidationFailure() { PropertyName = propertyName, ErrorMessage = errorMessage }
            };
            var apiErrors = new ApiErrors
            {
                Id = "0",
                Errors = validationFailureList
            };
            return apiErrors;
        }

        protected ApiErrors ConvertValidationResultToApiError(string Id, ValidationResult validationResult)
        {
            var oneErrorSet = new ApiErrors() { Id = Id, Errors = new List<ApiValidationFailure>() };
            if (validationResult != null && !validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    oneErrorSet.Errors.Add(new ApiValidationFailure() { PropertyName = failure.PropertyName, ErrorMessage = failure.ErrorMessage });
                }
            }

            return oneErrorSet;
        }

        protected HttpResponseMessage HandleException(Exception ex, string message, string Id = "0")
        {
            var httpError = new HttpError();
            httpError.Add(Id, new ApiValidationFailure() { PropertyName = "ErrorMessage", ErrorMessage = message });
            return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, message);
        }

        protected HttpResponseMessage NullEntityMessage(string message, string Id = "0")
        {
            return InvalidParametrMessage("PostEntity", message, Id);
        }

        protected HttpResponseMessage InvalidParametrMessage(string propertyName, string errorMsg, string Id = "InvalidParametr", HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            var httpError = new HttpError();
            httpError.Add(Id, new ApiValidationFailure() { PropertyName = propertyName, ErrorMessage = errorMsg });
            return Request.CreateErrorResponse(statusCode, httpError);
        }

        public static string WriteLog(string loginfor)
        {
            int errorcode = 0;
            string errorinfor = "";
            StreamWriter SW = null;
            //日志存放文件夹路径
            string DirectoryPath = "C:\\AppApiLog";
            //日志记录时间前缀
            string recordlogtime = "操作时间：" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ffff");
            //日志文件名
            string fileName = DateTime.Now.ToString("yyyyMMdd");
            try
            {
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                SW = new StreamWriter(DirectoryPath + "\\" + fileName + ".txt", true, Encoding.UTF8);
                SW.Write(recordlogtime + "\t操作内容：" + loginfor + "\r\n");
                SW.Flush();
            }
            catch (Exception ex)
            {
                return "-1" + "," + "文件写入异常:" + ex.Message.ToString();
            }
            finally
            {
                errorinfor = "文件写入成功！";
                if (SW != null)
                    SW.Close();
            }
            return errorcode.ToString() + "," + errorinfor;
        }

        /// <summary>
        /// 返回结果转JSON函数
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage ToJson(Object obj)
        {
            String str;
            if (obj is String || obj is Char)
            {
                str = obj.ToString();
            }
            else
            {
                str = JsonConvert.SerializeObject(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(str, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }

        /// <summary>
        /// 根据请求头的key获取请求头数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        protected string GetHeaderByKey(RequestHeaderKey key)
        {
            IEnumerable<string> keyValueList;
            string k = key.ToString();
            if (this.Request.Headers.TryGetValues(key.ToString(), out keyValueList))
            {
                return keyValueList.FirstOrDefault();
            }
            return string.Empty;
        }

        /// <summary>
        /// 根据属性名称以字符串格式获取值
        /// </summary>
        /// <param name="job"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string getValue(JObject job, string propertyName)
        {
            string result = string.Empty;
            if (job == null || string.IsNullOrEmpty(propertyName)) return result;
            if (job.Property(propertyName) == null) return result;
            return job[propertyName].ToString();
        }

        /// <summary>
        /// 当前访问用户对象
        /// </summary>
        public AccountEntity CurrentAccount
        {
            get
            {
                if (_accountEntity == null)
                {
                    _accountEntity = new AccountEntity
                    {
                        UserCode = this.GetHeaderByKey(RequestHeaderKey.UserCode),
                        UserName = HttpUtility.UrlDecode(this.GetHeaderByKey(RequestHeaderKey.UserName), System.Text.Encoding.UTF8),
                        UDID = this.GetHeaderByKey(RequestHeaderKey.UDID),
                        TerminalType = this.GetHeaderByKey(RequestHeaderKey.TerminalType)
                    };
                }
                return _accountEntity;
            }
        }

        public HttpResponseMessage AjaxResult(bool flag, string msg)
        {
            var result = new ResponseResult();
            result.success = flag;
            result.returnMsg = msg;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        public HttpResponseMessage AjaxResult(bool flag, string msg, object data)
        {
            var result = new ResponseResult();
            result.success = flag;
            result.returnMsg = msg;
            result.resultData = data;
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string PrintToPDF(string fileName, Dictionary<string, object> dic)
        {
            Report report = new Report();
            report.Report.Load(fileName);
            report.Dictionary.Connections[0].ConnectionString = DbConstSettings.BaseDbString;
            bool flag = dic.Count > 0;
            if (flag)
            {
                foreach (KeyValuePair<string, object> current in dic)
                {
                    report.SetParameterValue(current.Key, current.Value);
                }
            }
            report.Prepare(true);
            var tempFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf";

            FastReport.Export.Pdf.PDFExport pdfExport = new FastReport.Export.Pdf.PDFExport();
            pdfExport.ShowProgress = false;
            pdfExport.Subject = "Subject";
            pdfExport.Title = System.IO.Path.GetFileNameWithoutExtension(tempFileName);
            pdfExport.Compressed = true;
            pdfExport.AllowPrint = true;
            pdfExport.EmbeddingFonts = true;

            MemoryStream strm = new MemoryStream();
            report.Export(pdfExport, strm);

            strm.Position = 0;

            var filePath = @"PrintTempPath/" + DateTime.Now.ToString("yyyy-MM-dd") + @"/";
            var fileFullPath = HttpContext.Current.Server.MapPath("~/") + filePath;
            if (!Directory.Exists(fileFullPath))
            {
                Directory.CreateDirectory(fileFullPath);
            }
            FileStream fs = new FileStream(fileFullPath + tempFileName, FileMode.Create);
            strm.WriteTo(fs);
            strm.Close();
            fs.Close();

            pdfExport.Dispose();
            strm.Dispose();
            fs.Dispose();
            report.Dispose();

            return @"/" + filePath + tempFileName;
        }

    }
    /// <summary>
    /// 当前访问账户对象
    /// </summary>
    public class AccountEntity
    {
        /// <summary>
        /// 用户主键Code
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 当前用户使用的设备标识码
        /// </summary>
        public string UDID { get; set; }
        /// <summary>
        /// 当前用户终端类型
        /// </summary>
        public string TerminalType { get; set; }
    }
}
