
using ALP.Application.Service.SystemManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ALP.Util.Extension;
using ALP.Application.Entity.Log;
using ALP.Application.Busines.Material;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 基础数据类查询接口控制器
    /// </summary>

    [RoutePrefix("Base")]
    public class BaseDataController : ApiBaseController
    {
        private Base_MaterialBLL _MaterialBLL = new Base_MaterialBLL();
        BS_People_Service _bsPeopleService = new BS_People_Service();//人员
        Base_UpdateLog_Service _bsUpdateLogService = new Base_UpdateLog_Service();
        BaseSequenceService _baseSequence = new BaseSequenceService();//序列号

        /// <summary>
        /// 获取字典列表-PDA专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetList_DataItem_PDA")]
        public HttpResponseMessage GetList_DataItem_PDA(string fatherCode)
        {
            var result = new ResponseResult();
            try
            {
                SystemService service = new SystemService();
                result.success = true;
                result.resultData = service.GetList_DataItem_PDA(fatherCode);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 获取字典列表子明细-PDA专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_DataItemByFather_PDA")]
        public HttpResponseMessage GetList_DataItemByFather_PDA(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var encode = getValue(jo, "EnCode");
                var remark1 = getValue(jo, "Remark1");
                SystemService service = new SystemService();
                result.success = true;
                result.resultData = service.GetList_DataItemByFather_PDA(encode, remark1);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 查询产线(产线的'产品类型'不为空)-PDA专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetList_Lines_PDA")]
        public HttpResponseMessage GetList_Lines_PDA()//string key
        {
            var result = new ResponseResult();
            try
            {
                SystemService service = new SystemService();
                result.success = true;
                result.resultData = service.GetList_Lines_PDA();
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 查询供应商
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_Suppliers")]
        public HttpResponseMessage GetList_Suppliers(JObject jo)
        {
            SystemService bll = new SystemService();
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
                    result.success = false;
                    result.returnMsg = "BaseManage.BaseDataController.Tips_1";//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetList_Suppliers(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther",ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询物料
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_Materials")]
        public HttpResponseMessage GetList_Materials(JObject jo)
        {
            SystemService bll = new SystemService();
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
                    result.success = false;
                    result.returnMsg = "BaseManage.BaseDataController.Tips_1";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetList_Materials(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询仓库
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_Depositories")]
        public HttpResponseMessage GetList_Depositories(JObject jo)
        {
            SystemService bll = new SystemService();
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
                    result.success = false;
                    result.returnMsg = "BaseManage.BaseDataController.Tips_1";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetList_Depositories(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 查询计量单位 pagination 分页json; queryJson 查询JSO
        /// 创　　建: 刘万军
        /// 创建日期: 2021-01-29 14:57:18
        /// 任务编号:
        /// </summary>       
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_BS_UOMs")]
        public HttpResponseMessage GetList_BS_UOMs(JObject jo)
        {
            SystemService bll = new SystemService();
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
                    result.success = false;
                    result.returnMsg = "BaseManage.BaseDataController.Tips_1";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetList_BS_UOMs(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther",ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 获取退库原因分页列表
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReturnReasonPageListJson")]
        public HttpResponseMessage GetReturnReasonPageList(JObject jo)
        {
            SystemService bll = new SystemService();
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
                    result.success = false;
                    result.returnMsg = "BaseManage.BaseDataController.Tips_1";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetReturnReasonPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther",ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #region 人员信息查询
        /// <summary>
        /// 获取用户下拉列表
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserList")]
        public HttpResponseMessage GetUserList(JObject jo)
        {

            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                SystemService bll = new SystemService();
                var userCode = getValue(jo, "UserCode");
                var noLike = getValue(jo, "NoLike");
                var loginUserCode = getValue(jo, "loginUserCode");
                if (string.IsNullOrEmpty(loginUserCode))
                    loginUserCode = CurrentAccount.UserCode;

                var data = bll.GetUserList(userCode, loginUserCode, noLike);

                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther",ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 模糊查询物料主数据
        /// <summary>
        /// 模糊查询物料主数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBaseMaterialList")]
        public HttpResponseMessage GetBaseMaterialList(JObject jo)
        {

            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                var queryJson = getValue(jo, "queryJson");

                var data = _MaterialBLL.GetBaseMaterialList(queryJson);

                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.ExecutionSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther",ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        /// <summary>
        /// 获取登录人信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetLoginInfo")]
        public HttpResponseMessage GetLoginInfo(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var userCode = getValue(jo, "userCode");//登陆人编码
                var peopleEntity = _bsPeopleService.Get_ExpressionEntity(t => t.Code == userCode);
                result.resultData = peopleEntity;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "Common.Error";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #region 升级日志查询
        [HttpPost]
        [Route("GetUpdateLog")]
        public HttpResponseMessage GetUpdateLog(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var businessType = getValue(jo, "businessType");//业务类型
                var version = getValue(jo, "version");//版本号
                var updateLogEntity = _bsUpdateLogService.Get_ExpressionEntity(t => t.BusinessType == businessType && t.Version == version);
                result.resultData = updateLogEntity;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "Common.Error";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 序列号
        [HttpPost]
        [Route("GetSerialNo")]
        public HttpResponseMessage GetSerialNo(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var seqCode = getValue(jo, "seqCode");
                if (string.IsNullOrEmpty(seqCode))
                    return AjaxResult(false, "BaseManage.BaseDataController.GetSerialNo.Tips_1");//seqCode参数不能为空！

                var serialNo = _baseSequence.GetSerialNO(seqCode);
                result.resultData = serialNo;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "Common.Error";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion
    }
}