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
using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.Busines.MaterialManage;

namespace ALP.Application.WebApi.Controllers.QualityManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-27
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_TestDetailController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 检验明细表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("QC_IQCQualityCheckItem")]
    public class QC_IQCQualityCheckItemController : ApiBaseController
    {
        private QC_IQCQualityCheckItem_BLL bll = new QC_IQCQualityCheckItem_BLL();
        private QC_IQCQualityCheck_BLL _IQCQualityCheckBLL = new QC_IQCQualityCheck_BLL();
        private MM_ReceiptNoticeBLL _ReceiptNoticeBLL = new MM_ReceiptNoticeBLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_TestDetailPageList")]
        public HttpResponseMessage QC_TestDetailPageList(JObject jo)
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
                    total = pagination != null ? pagination.total : data.Count(),
                    page = pagination != null ? pagination.total : data.Count(),
                    records = pagination != null ? pagination.total : data.Count(),
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_TestDetailPageDataTableList")]
        public HttpResponseMessage QC_TestDetailPageDataTableList(JObject jo)
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetQC_TestDetailList")]
        public HttpResponseMessage GetQC_TestDetailList(string checkType)
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQC_TestDetail")]
        public HttpResponseMessage SaveQC_TestDetail(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_7");//缺少Entity参数！
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
                QC_IQCQualityCheckItemEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckItemEntity>(getValue(jo, "Entity"));


                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {

                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_8");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                else
                {

                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_9");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: QC_TestDetail	ParentId流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("QC_TestDetail", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.ParentId = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;

                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchQC_TestDetailList")]
        public HttpResponseMessage SaveBatchQC_TestDetailList(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var time = DateTime.Now;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<QC_IQCQualityCheckItemEntity>>(getValue(jo, "data"));

                var imglist = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "imgList"));
                string keyValue = getValue(jo, "KeyValue");

                string status = getValue(jo, "TestDepartment");//1实验室，2质量部

                /*
                 * 更改收料通知单状态
                 * 2、检验中：收料通知单+行号已进行IQC检验中，但尚未完成检验；
                 * 3、待入库：收料通知单+行号已完成IQC检验；  TestResult!=null
                 */
                var mmEntity = _ReceiptNoticeBLL.Get_ExpressionEntity(t => t.Id == entity.ReceiptId);
                mmEntity.ModifyBy = userCode;
                mmEntity.ModifyTime = time;
                if (mmEntity.ReceiptStatus=="1" && string.IsNullOrEmpty(entity.TestResult))
                {
                    mmEntity.ReceiptStatus = "2";//检验中
                }
                else if(mmEntity.ReceiptStatus == "2" && !string.IsNullOrEmpty(entity.TestResult))
                {
                    mmEntity.ReceiptStatus = "3";//待入库
                }

                string msg = "";
                int isok = 1;

                entity.ModifyBy = userCode;
                if (status == "1")//实验室
                {
                    entity.LabStatus = "3";
                    entity.LabInspector = userCode;
                    entity.LabInspectionTime = time;
                }
                else
                {
                    entity.QualityStatus = "2";
                    entity.QualityInspector = userCode;
                    entity.QualityTime = time;
                }


                foreach (var item in list)
                {
                    item.Create();
                    item.IQCId = keyValue;
                    item.CreateTime = time;
                    item.Creator = userCode;
                }
                if (imglist.Count > 0)
                {
                    //entity.Attachment = "存在";
                    foreach(var item in imglist)
                    {
                        item.Create();
                        item.ParentId = keyValue;
                        item.Creator = userCode;
                    }
                }

                if (mmEntity.ReceiptStatus == "2" || mmEntity.ReceiptStatus == "3")
                    _ReceiptNoticeBLL.SaveEntity(mmEntity.Id, mmEntity, out msg);

                bll.RemoveForm(test => test.IQCId == keyValue && test.TestDepartment == status);
                _IQCQualityCheckBLL.SaveEntity(keyValue, entity, out msg);
                isok = bll.SaveEntity_List(false, userCode, list, out msg);

                if (imglist.Count > 0)  new Base_Images_Service().SaveEntity_List(false, userCode, imglist, out msg);

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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteQC_TestDetail")]
        public HttpResponseMessage DeleteQC_TestDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IQCQualityCheckItemEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckItemEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IQCQualityCheckItemEntity model = bll.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_14");//删除操作成功
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveQC_TestDetail")]
        public HttpResponseMessage RemoveQC_TestDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IQCQualityCheckItemEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckItemEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IQCQualityCheckItemEntity model = bll.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_14");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_16");//删除操作失败
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveQC_TestDetailList")]
        public HttpResponseMessage RemoveQC_TestDetailList(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Parent") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_17");//缺少Parent参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                string parent = getValue(jo, "Parent");

                string msg = "";

                //删除
                int isok = bll.RemoveForm(parent, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_14");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_16");//删除操作失败
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveResultQC_TestDetail")]
        public HttpResponseMessage RemoveResultQC_TestDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //var userCode = CurrentAccount.UserCode;
                //var userName = CurrentAccount.UserName;

                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_18");//缺少KeyVallue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (jo.SelectToken("UserCode") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_18");//缺少KeyVallue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                string keyValue = getValue(jo, "KeyValue");
                string userCode = getValue(jo, "UserCode");

                string msg = "";

                //删除
                int isok = bll.RemoveForm(keyValue, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_14");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_16");//删除操作失败
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_TestDetailEntity</returns>
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_TestDetailEntity</returns>
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
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
                var data = bll.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
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
        /// 创建日期: 2021-08-27 10:45:43
        /// 任务编号: 检验明细表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("QC_TestDetail_export")]
        public HttpResponseMessage QC_TestDetail_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckItemController.Tips_7");//缺少Entity参数！
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


    }
}
