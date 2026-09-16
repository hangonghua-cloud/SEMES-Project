using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.SAP;
using ALP.Application.Service.SAP;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using System.IO;//文件数据流保存
using ALP.Application.Entity.FileManage;//imgListEntity 实体位置
using System.Web;//HttpContext.Current.Server.MapPath 方法引用

namespace ALP.Application.WebApi.Controllers.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-12-03
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_MarkUploadController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 唛头配置
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_MarkUpload")]
    public class PL_MarkUploadController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_MarkUploadPageList")]
        public HttpResponseMessage PL_MarkUploadPageList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    pagination = null;
                    //result.success = false;
                    //result.returnMsg = "分页参数Pagination不能为空！";
                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                var data = _Service.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total: data.Count(),
                    page = pagination != null ? pagination.total: data.Count(),
                    records = pagination != null ? pagination.records: data.Count(),
                    costtime = CommonHelper.TimerEnd(watch)
                };
                
                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_MarkUploadPageDataTableList")]
        public HttpResponseMessage PL_MarkUploadPageDataTableList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    pagination = null;
                    //result.success = false;
                    //result.returnMsg = "分页参数Pagination不能为空！";
                    //return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                var data = _Service.GetPageDataTableList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };
                
                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_MarkUploadList")]
        public HttpResponseMessage GetPL_MarkUploadList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                if (msg != "")
                {
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_MarkUpload")]
        public HttpResponseMessage SavePL_MarkUpload(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //if (string.IsNullOrEmpty(userCode))
            //{
                //result.success = false;
                //result.returnMsg = "用户名不能为空！";
                //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}
            
            try
            {
                //业务服务类
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                //参数转实体
                PL_MarkUploadEntity entity = JsonConvert.DeserializeObject<PL_MarkUploadEntity>(getValue(jo, "Entity"));
                //唛头编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.MarkCode))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_8");//唛头编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MarkName))
                {
                    //唛头名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_9");//唛头名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //if (jo.SelectToken("imgListEntity") != null)
                    //{
                    //    string Id = keyValue;
                        
                    //    //获取上传文件转实体
                    //    List<imgListEntity> _imgListEntity = JsonConvert.DeserializeObject<List<imgListEntity>>(getValue(jo, "imgListEntity"));
                    //    //上传文件字段集合
                    //    string[] UpListFile = new string[] { "MarkName" };
                        
                    //    //二进制流转为图片保存
                    //    if (_imgListEntity != null  && _imgListEntity.Count > 0 && _imgListEntity[0].ImgData != null)
                    //    {
                    //        var virtualPath = "~/";
                    //        var dirPath = "Upload/";
                    //        string folder = DateTime.Now.ToString("yyyyMM") + "/";
                    //        //文件全路径
                    //        var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);
                    //        if (!Directory.Exists(fullDirPath))
                    //        {
                    //            Directory.CreateDirectory(fullDirPath);
                    //        }
                    //        for (int i = 0; i < _imgListEntity.Count; i++)
                    //        {
                    //            imgListEntity upfile = _imgListEntity[i];
                    //            //检测文件是否存在,存在就删除
                    //            if (File.Exists(fullDirPath + @"\"  + UpListFile[i] + "_" + Id + "." + upfile.ImgType) == true)
                    //            {
                    //                File.Delete(fullDirPath + @"\"  + UpListFile[i] + "_" + Id + "." + upfile.ImgType);
                    //            }
                    //            //保存文件
                    //            //创建文件流
                    //            FileStream fstream = File.Create(fullDirPath + @"\"  + UpListFile[i] + "_" + Id + "." + upfile.ImgType, upfile.ImgData.Length);
                    //            //把二进制流数据写入文件
                    //            fstream.Write(upfile.ImgData, 0, upfile.ImgData.Length);
                    //            //文件上传字段说明
                    //            //entity.CalibrateReport = Config.GetValue("FileServer_URL") + dirPath + folder +  UpListFile[i] + "_" + Id + "." + upfile.ImgType;
                    //            //查找出实体名称相等名称的字段
                    //            System.Reflection.PropertyInfo itemEntity = entity.GetType().GetProperties().Where(x => x.Name.ToUpper() == UpListFile[i].ToUpper()).FirstOrDefault();
                    //            if (itemEntity != null)
                    //            {
                    //                itemEntity.SetValue(entity, Config.GetValue("FileServer_URL") +  dirPath + folder +  UpListFile[i] + "_" + Id + "." + upfile.ImgType);
                    //            }
                    //            fstream.Close();
                    //        }
                    //    }
                    //}
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_10");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.ModifyByName))
                    {
                        //编辑人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_11");//编辑人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                
                   
                }
                else
                {
                    if (jo.SelectToken("imgListEntity") != null)
                    {
                        string Id = entity.Id;
                        
                        //获取上传文件转实体
                        List<imgListEntity> _imgListEntity = JsonConvert.DeserializeObject<List<imgListEntity>>(getValue(jo, "imgListEntity"));
                        //上传文件字段集合
                        string[] UpListFile = new string[] { "MarkName" };
                        var strArr = entity.MarkName.Split(".");
                        //二进制流转为图片保存
                        if (_imgListEntity != null  && _imgListEntity.Count > 0 && _imgListEntity[0].ImgData != null)
                        {
                            var virtualPath = "~/";
                            var dirPath = "Upload/";
                            //string folder = DateTime.Now.ToString("yyyyMM") + "/";
                            //文件全路径
                            var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath );
                           // var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);
                            if (!Directory.Exists(fullDirPath))
                            {
                                Directory.CreateDirectory(fullDirPath);
                            }
                            for (int i = 0; i < _imgListEntity.Count; i++)
                            {
                                imgListEntity upfile = _imgListEntity[i];
                                //检测文件是否存在,存在就删除
                                if (File.Exists(fullDirPath + @"\" + strArr[0]  + "." + upfile.ImgType) == true)
                                {
                                    File.Delete(fullDirPath + @"\" + strArr[0] + "." + upfile.ImgType);
                                }
                                //保存文件
                                //创建文件流
                                FileStream fstream = File.Create(fullDirPath + @"\"  + strArr[0]  + "." + upfile.ImgType, upfile.ImgData.Length);
                                //把二进制流数据写入文件
                                fstream.Write(upfile.ImgData, 0, upfile.ImgData.Length);
                                //文件上传字段说明
                                //entity.CalibrateReport = Config.GetValue("FileServer_URL") + dirPath + folder +  UpListFile[i] + "_" + Id + "." + upfile.ImgType;
                                //查找出实体名称相等名称的字段
                                //System.Reflection.PropertyInfo itemEntity = entity.GetType().GetProperties().Where(x => x.Name.ToUpper() == UpListFile[i].ToUpper()).FirstOrDefault();
                                //if (itemEntity != null)
                                //{
                                //    itemEntity.SetValue(entity, Config.GetValue("FileServer_URL") + dirPath + folder +  UpListFile[i] + "_" + Id + "." + upfile.ImgType);
                                //}
                                fstream.Close();
                            }
                        }
                    }
                    
                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_12");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.CreatorName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_13");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: PL_MarkUpload	MarkCode流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("PL_MarkUpload", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.MarkCode = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    entity.CreateTime = DateTime.Now;
                    //是否有效
                    //entity.IsEnable = false;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
            
                }
                
                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg); 
                if (isok == 1)
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (isok == 2)
                {

                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_15");//唛头编码或者名称重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result.success = false;
                    result.returnMsg = msg;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_MarkUpload")]
        public HttpResponseMessage SaveBatchPL_MarkUpload(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_17");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_18");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<PL_MarkUploadEntity> Insert_entity_list = new List<PL_MarkUploadEntity>();
                //更新数组
                List<PL_MarkUploadEntity> Update_entity_list = new List<PL_MarkUploadEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PL_MarkUploadEntity entity = new PL_MarkUploadEntity();
                        //唛头编码
                        entity.MarkCode =  item[ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_19")];//唛头编码
                        //唛头名称
                        entity.MarkName =  item[ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_20")];//唛头名称
                        //是否有效
                        entity.IsEnabled = false;
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTime.Now;
                        //是否删除
                     
                        entity.Creator = CreatedByCode;
                        entity.CreatorName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PL_MarkUploadEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
                        //判断旧列表中是否存在此编码
                        if (old_entity == null)
                        {
                            entity.Create();
                            //保存数组
                            Insert_entity_list.Add(entity);
                        }
                        else
                        {
                            entity.Id = old_entity.Id;
                            Update_entity_list.Add(entity);
                        }
                    }
                    catch (Exception ex)
                    {
                    
                    }
                }
                
                    if (Insert_entity_list.Count > 0)
                    {
                        //批量新增
                        isok = _Service.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _Service.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
                    }
                    
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_MarkUpload")]
        public HttpResponseMessage DeletePL_MarkUpload(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                PL_MarkUploadEntity entity = JsonConvert.DeserializeObject<PL_MarkUploadEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_MarkUploadEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyByName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_24");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_MarkUpload")]
        public HttpResponseMessage RemovePL_MarkUpload(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                PL_MarkUploadEntity entity = JsonConvert.DeserializeObject<PL_MarkUploadEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_MarkUploadEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, entity.ModifyByName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_24");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_26");//删除操作失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_MarkUploadEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_27");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_MarkUploadEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_27");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                string OutMes = "";
                var list = _Service.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个数据表进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                string OutMes = "";
                var list = _Service.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: jpf
        /// 创建日期: 2022-12-03 09:37:42
        /// 任务编号: 唛头配置
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_MarkUpload_export")]
        public HttpResponseMessage PL_MarkUpload_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.PL_MarkUploadController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                PL_MarkUpload_Service _Service = new PL_MarkUpload_Service();
                string msg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                // var data = _Service.GetList_export(CreatedByCode, out msg);
                var data = "";
                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        
    }
}
