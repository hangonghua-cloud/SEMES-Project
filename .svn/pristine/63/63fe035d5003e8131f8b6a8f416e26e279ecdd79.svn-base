using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Busines.QualityManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;

namespace ALP.Application.WebApi.Controllers.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_IPQCDetailResultController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 过程检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("QC_IPQCDetailResult")]
    public class QC_IPQCDetailResultController : ApiBaseController
    {
        private QC_IPQCDetailResult_BLL bll = new QC_IPQCDetailResult_BLL();
        private QC_IPQCDetail_BLL _IPQCDetailBLL = new QC_IPQCDetail_BLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_IPQCDetailResultPageList")]
        public HttpResponseMessage QC_IPQCDetailResultPageList(JObject jo)
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
                var data = bll.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total: data.Count(),
                    page = pagination != null ? pagination.total: data.Count(),
                    records = pagination != null ? pagination.total: data.Count(),
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_IPQCDetailResultPageDataTableList")]
        public HttpResponseMessage QC_IPQCDetailResultPageDataTableList(JObject jo)
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
                var data = bll.GetPageDataTableList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetCheckPageDataTableList")]
        public HttpResponseMessage GetCheckPageDataTableList(JObject jo)
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
                var data = bll.GetCheckPageDataTableList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetCheckDataTableList")]
        public HttpResponseMessage GetCheckDataTableList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                pagination = null;
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetCheckPageDataTableList(pagination, queryJson);
                
                result.resultData = data;
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetQC_IPQCDetailResultList")]
        public HttpResponseMessage GetQC_IPQCDetailResultList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var list = bll.GetList(checkType, out msg);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQC_IPQCDetailResult")]
        public HttpResponseMessage SaveQC_IPQCDetailResult(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
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
                //参数转实体
                QC_IPQCDetailResultEntity entity = JsonConvert.DeserializeObject<QC_IPQCDetailResultEntity>(getValue(jo, "Entity"));
                //流转卡编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.FlowCardId))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_8");//流转卡编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TestItemCoading))
                {
                    //检验工序 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_9");//检验工序不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TestItemResult))
                {
                    //检验机台 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_10");//检验机台不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                /*else if (string.IsNullOrEmpty(entity.Creator))
                {
                    //检验员 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_11");//检验员不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ModifyBy))
                {
                    //最后修改人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_12");//最后修改人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }*/
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_13");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    //entity.UpdateDateTime = DateTimeOffset.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    //entity.CreatedDateTime = DateTime.Parse(entity.CreatedDateTime.ToString());
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_14");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    /*else if (string.IsNullOrEmpty(entity.CreatedByName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_15");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }*/
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: QC_IPQCDetailResult	FlowCardIdr流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("QC_IPQCDetailResult", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.FlowCardIdr = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.CreatedDateTime = DateTimeOffset.Now;
                    //附件
                    //entity.EnabledMark = false;
                    //检验时间
                    //entity.CreatTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.IsDeleted = false;
                }
                
                string msg = "";
                int isok = bll.SaveEntity(keyValue, entity, out msg);
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQC_IPQCDetailResults")]
        public HttpResponseMessage SaveQC_IPQCDetailResults(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("List") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_18");//缺少List参数！
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
                //参数转实体
                QC_IPQCDetailEntity entity = JsonConvert.DeserializeObject<QC_IPQCDetailEntity>(getValue(jo, "Entity"));
                List<QC_IPQCDetailResultEntity> list = JsonConvert.DeserializeObject<List<QC_IPQCDetailResultEntity>>(getValue(jo, "List"));
                //设备编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                #region
                #endregion

                //string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(entity.Id))
                {

                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_13");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    //entity.UpdateDateTime = DateTimeOffset.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    //entity.CreatedDateTime = DateTime.Parse(entity.CreatedDateTime.ToString());
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Inspector))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_14");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                string msg = "";
                string id = "";
                int isok = 0;// _Service.SaveEntity(entity.Id, entity, out msg);
                /*if (string.IsNullOrWhiteSpace(entity.Id))
                    entity.Id = id;*/
                isok = bll.SaveEntity(entity, list, ref msg);
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchQC_IPQCDetailResult")]
        public HttpResponseMessage SaveBatchQC_IPQCDetailResult(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var time = DateTime.Now;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
 
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var entity = JsonConvert.DeserializeObject<QC_IPQCDetailEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<QC_IPQCDetailResultEntity>>(getValue(jo, "data"));
                
                string keyValue = getValue(jo, "KeyValue");
                
                
                string msg = "";
                int isok = 1;
                foreach(var item in list)
                {
                    item.Create();
                    item.FlowCardId = keyValue;
                    item.CreatTime = time;
                    item.Creator = userCode;
                }
                entity.ModifyBy = userCode;
                entity.ModifyTime = time;

                bll.RemoveForm(t=>t.FlowCardId==keyValue);
                _IPQCDetailBLL.SaveEntity(keyValue, entity, out msg);
                isok= bll.SaveEntity_List(false, userCode, list, out msg);

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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteQC_IPQCDetailResult")]
        public HttpResponseMessage DeleteQC_IPQCDetailResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IPQCDetailResultEntity entity = JsonConvert.DeserializeObject<QC_IPQCDetailResultEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IPQCDetailResultEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = bll.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_21");//删除操作成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveQC_IPQCDetailResult")]
        public HttpResponseMessage RemoveQC_IPQCDetailResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IPQCDetailResultEntity entity = JsonConvert.DeserializeObject<QC_IPQCDetailResultEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IPQCDetailResultEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = bll.RemoveForm(Id, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_21");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_23");//删除操作失败
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

        [HttpPost]
        [Route("RemoveBatchQC_IPQCDetailResult")]
        public HttpResponseMessage RemoveBatchQC_IPQCDetailResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_6");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (jo.SelectToken("UpdateUser") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_25");//缺少UpdateUser参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                string keyValue = getValue(jo, "KeyValue");
                string modifyBy = getValue(jo, "UpdateUser");
                //删除
                int isok = bll.RemoveBatchForm(keyValue, modifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_21");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_23");//删除操作失败
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_IPQCDetailResultEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                var data = bll.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_26");//获取详情数据成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_IPQCDetailResultEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                var data = bll.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_26");//获取详情数据成功
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
        /// 功能描述: 通过N个字段拼写linq查询数组对象 参考
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">条件值</param>
        /// <param name="keyValue2">条件值</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEntityByLinq")]
        public HttpResponseMessage GetEntityByLinq(string keyValue, string keyValue2)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                var data = bll.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.EnabledMark == false).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string OutMes = "";
                var list = bll.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string OutMes = "";
                var list = bll.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("QC_IPQCDetailResult_export")]
        public HttpResponseMessage QC_IPQCDetailResult_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IPQCDetailResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                string msg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }
                
                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = bll.GetList_export(CreatedByCode, out msg);
                
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
