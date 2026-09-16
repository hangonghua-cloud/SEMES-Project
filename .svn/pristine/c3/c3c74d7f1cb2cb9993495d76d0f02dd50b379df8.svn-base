using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.SystemManage;
using System.Dynamic;
using ALP.Util;
using ALP.Util.WebControl;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Entity.BaseManage;
using ALP.Util.Extension;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.FileManage
{
    /// <summary>
    /// gzq
    /// 修改: 刘万军 2021-9-28
    /// 2020-6-1 19:50
    /// 文件上传控制器
    /// </summary>
    [RoutePrefix("UploadFile")]
    public class UploadFileController : ApiBaseController
    {

        private Base_Images_Service _imageService = new Base_Images_Service();

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload")]
        public HttpResponseMessage Upload()
        {
            string strAllowFileExtension = Config.GetValue("AllowFileExtension").ToLower();
            string[] arrAllowFileExtension = strAllowFileExtension.Split("|");
            int intMaxFileLength = Convert.ToInt32(Config.GetValue("MaxFileLength"));
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var message = "FileManage.UploadFileController.Tips_1";//上传成功
                var filelist = HttpContext.Current.Request.Files;
                var success = true;
                List<string> listFilePath = new List<string>();
                if (filelist.Count > 0)
                {
                    for (var i = 0; i < filelist.Count; i++)
                    {
                        string sFileName = "";
                        string sFilePath = "";
                        var file = filelist[i];
                        var fileExtention = Path.GetExtension(file.FileName).ToLower();
                        var fileName = file.FileName;
                        sFileName = fileName;
                        string sFileNameNoExt = GetFileNameWithoutExtension(fileName);
                        string sFullExtension = GetExtension(fileName);
                        if (!arrAllowFileExtension.Contains(fileExtention))
                        {
                            message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_2", fileName); //$"上传文件[{fileName}]的格式不符合要求！";
                            success = false;
                            break;
                        }
                        if (file.ContentLength > intMaxFileLength * 1024 * 1024)
                        {
                            message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_3", fileName); //$"上传文件[{fileName}]超过文件大小限制！";
                            success = false;
                            break;
                        }
                        int iCounter = 0;

                        var virtualPath = "~/";
                        var dirPath = "Upload/";
                        string folder = DateTime.Now.ToString("yyMMdd") + "/";
                        var fullDirPath = HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);//文件全路径
                        //var fullDirPath = HttpContext.Current.Server.MapPath(virtualPath + dirPath);//文件全路径

                        string sServerDir = fullDirPath;
                        while (true)
                        {
                            sFilePath = System.IO.Path.Combine(sServerDir, sFileName);

                            if (System.IO.File.Exists(sFilePath))
                            {
                                iCounter++;
                                sFileName = sFileNameNoExt + "(" + iCounter + ")" + sFullExtension;
                            }
                            else
                            {
                                if (!Directory.Exists(fullDirPath))
                                {
                                    Directory.CreateDirectory(fullDirPath);
                                }
                                //var filePath = $"{fullDirPath}{fileName}";
                                try
                                {
                                    file.SaveAs(sFilePath);
                                    listFilePath.Add($@"{dirPath}{folder}{sFileName}");
                                    //listFilePath.Add($@"{dirPath}{sFileName}");
                                }
                                catch (Exception ex)
                                {
                                    message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_4", ex.Message); //"上传文件写入失败：" + ex.Message;
                                    success = false;
                                }

                                break;
                            }
                        }

                    }
                }
                else
                {
                    message = "FileManage.UploadFileController.Tips_5";//没有选择上传文件！
                    success = false;
                }

                if (listFilePath.Count > 0)
                {
                    result.resultData = listFilePath.ToArray().Join(",");
                }
                result.success = success;
                result.returnMsg = message;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Upload2")]//string pId, bool isSave
        public HttpResponseMessage Upload2()
        {
            var msg = "";
            return SaveFiles((files) =>
            {
                if (HttpContext.Current.Request.Form["isSave"] == "1")
                {
                    //var list = new List<Base_ImagesEntity>();

                    foreach (var item in files)
                    {
                        var file = new Base_ImagesEntity();
                        file.Create();
                        file.ParentId = HttpContext.Current.Request.Form["pId"];
                        file.TableName = HttpContext.Current.Request.Form["tableName"];
                        file.Module = HttpContext.Current.Request.Form["module"];
                        file.FileName = item.file?.FileName;
                        file.ImgType = item.file?.FileName.Substring(item.file?.FileName.LastIndexOf('.'));
                        file.FilePath = item.path;
                        file.FileSize = (item.file.ContentLength / 1024 / 1024.00).ToString("0.00");
                        file.Creator = CurrentAccount.UserName;

                        //list.Add(file);

                        _imageService.SaveEntity("", file, out msg);
                    }

                    //_imageService.SaveEntity_List(false, CurrentAccount.UserName, list, out msg);
                }
            });
        }

        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage Save(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            string msg = "";
            var success = true;
            try
            {
                if (jo == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ParamsNotNull");//参数不能为空
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("parentId") == null)
                {
                    result.success = false;
                    result.returnMsg = "parentId Is Null！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var files = getValue(jo, "files").ToList<dynamic>();
                foreach (var item in files)
                {
                    var file = new Base_ImagesEntity();
                    file.Create();
                    file.FactoryCode = getValue(jo, "factoryCode");
                    file.FactoryName = getValue(jo, "factoryName");
                    file.ParentId = getValue(jo, "parentId");
                    file.TableName = getValue(jo, "tableName");
                    file.Module = getValue(jo, "module");
                    file.FileName = item.path?.ToString().Substring(item.path?.ToString().LastIndexOf('/') + 1);
                    file.ImgType = item.path?.ToString().Substring(item.path?.ToString().LastIndexOf('.'));
                    file.FilePath = item.path;
                    file.FileSize = item.size;
                    file.Creator = CurrentAccount.UserName;

                    _imageService.SaveEntity("", file, out msg);
                }

                result.success = success;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        public HttpResponseMessage SaveFiles(Action<List<dynamic>> act)
        {
            string strAllowFileExtension = Config.GetValue("AllowFileExtension").ToLower();
            string[] arrAllowFileExtension = strAllowFileExtension.Split("|");
            int intMaxFileLength = Convert.ToInt32(Config.GetValue("MaxFileLength"));
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var message = "FileManage.UploadFileController.Tips_1";//上传成功
                var filelist = HttpContext.Current.Request.Files;
                var success = true;
                //List<string> listFilePath = new List<string>();
                List<dynamic> files = new List<dynamic>();
                if (filelist.Count > 0)
                {
                    for (var i = 0; i < filelist.Count; i++)
                    {
                        string sFileName = "";
                        string sFilePath = "";
                        var file = filelist[i];
                        var fileExtention = Path.GetExtension(file.FileName).ToLower();
                        var fileName = file.FileName;
                        sFileName = fileName;
                        string sFileNameNoExt = GetFileNameWithoutExtension(fileName);
                        string sFullExtension = GetExtension(fileName);
                        if (!arrAllowFileExtension.Contains(fileExtention))
                        {
                            message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_2", fileName); //$"上传文件[{fileName}]的格式不符合要求！";
                            success = false;
                            break;
                        }
                        if (file.ContentLength > intMaxFileLength * 1024 * 1024)
                        {
                            message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_3", fileName); // $"上传文件[{fileName}]超过文件大小限制(100M)！";
                            success = false;
                            break;
                        }
                        int iCounter = 0;

                        var virtualPath = "~/";
                        var dirPath = "/Upload/";
                        string folder = DateTime.Now.ToString("yyMMdd") + "/";
                        var fullDirPath = HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);//文件全路径
                        //var fullDirPath = HttpContext.Current.Server.MapPath(virtualPath + dirPath);//文件全路径

                        string sServerDir = fullDirPath;
                        while (true)
                        {
                            sFilePath = System.IO.Path.Combine(sServerDir, sFileName);

                            if (System.IO.File.Exists(sFilePath))
                            {
                                iCounter++;
                                sFileName = sFileNameNoExt + "(" + iCounter + ")" + sFullExtension;
                            }
                            else
                            {
                                if (!Directory.Exists(fullDirPath))
                                {
                                    Directory.CreateDirectory(fullDirPath);
                                }
                                //var filePath = $"{fullDirPath}{fileName}";
                                try
                                {
                                    file.SaveAs(sFilePath);
                                    files.Add(new
                                    {
                                        file = file,
                                        size = (file.ContentLength / 1024 / 1024.00).ToString("0.00"),
                                        path = $@"{dirPath}{folder}{sFileName}"
                                    });
                                    //listFilePath.Add($@"{dirPath}{folder}{sFileName}");
                                    //listFilePath.Add($@"{dirPath}{sFileName}");
                                }
                                catch (Exception ex)
                                {
                                    message = ALP.Application.Service.Resources.Language.GetText("FileManage.UploadFileController.Tips_4", ex.Message); //"上传文件写入失败：" + ex.Message;
                                    success = false;
                                }

                                break;
                            }
                        }

                    }
                }
                else
                {
                    message = "FileManage.UploadFileController.Tips_5";//没有选择上传文件！
                    success = false;
                }

                if (files.Any())
                {
                    result.resultData = files.Select(m => new { m.size, m.path });
                    act?.Invoke(files);
                }
                result.success = success;
                result.returnMsg = message;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther", ex.Message);
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        private string GetFileNameWithoutExtension(string fileName)
        {
            int length = fileName.Length - 1, dotPos = fileName.IndexOf(".");

            if (dotPos == -1)
                return fileName;

            return fileName.Substring(0, dotPos);
        }

        private string GetExtension(string fileName)
        {
            int length = fileName.Length - 1, dotPos = fileName.IndexOf(".");

            if (dotPos == -1)
                return "";

            return fileName.Substring(dotPos);
        }
    }
}
