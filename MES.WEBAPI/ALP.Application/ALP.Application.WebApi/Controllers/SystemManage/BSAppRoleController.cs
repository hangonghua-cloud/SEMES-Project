using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Web.Http;


using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Busines.SystemManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;

namespace ALP.Application.WebApi.Controllers.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.3.29 16:19
    /// 描 述：角色管理
    /// </summary>
    [RoutePrefix("BSAppRole")]
    public class BSAppRoleController : ApiBaseController
    {
        private BSAppRoleBLL roleBll = new BSAppRoleBLL();
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPageListJson")]
        public HttpResponseMessage GetPageList(JObject jo)
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
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_1");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = roleBll.GetPageList(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAuthorList")]
        public HttpResponseMessage GetAuthorList(string roleCode)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = roleBll.GetAuthorList(roleCode);

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetRoleFunctionPageListJson")]
        public HttpResponseMessage GetRoleFunctionPageList(JObject jo)
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
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_1");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = roleBll.GetRoleFunctionPageList(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteForm")]
        public HttpResponseMessage RemoveForm(string keyValue, string userId)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                int returnValue = roleBll.RemoveForm(keyValue, userId);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_5");//删除失败，没有找到记录
                        result.success = true;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteRoleFunctionForm")]
        public HttpResponseMessage RemoveRoleFunctionForm(string keyValue, string userId)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                int returnValue = roleBll.RemoveRoleFunctionForm(keyValue, userId);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_5");//删除失败，没有找到记录
                        result.success = true;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 保存数据(新增/修改)
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_7");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");

                APPRoleEntity entity = JsonConvert.DeserializeObject<APPRoleEntity>(getValue(jo, "Entity"));
                int returnValue = roleBll.SaveForm(keyValue, entity);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_9");//添加成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_10");//修改成功
                        result.success = true;
                        break;
                    case 3:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_11");//添加失败，角色编码重复，无法插入数据。
                        result.success = false;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRoleFunctionForm")]
        public HttpResponseMessage SaveRoleFunctionForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_7");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");

                APPRoleFunctionEntity entity = JsonConvert.DeserializeObject<APPRoleFunctionEntity>(getValue(jo, "Entity"));
                int returnValue = roleBll.SaveRoleFunctionForm(keyValue, entity);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_9");//添加成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_10");//修改成功
                        result.success = true;
                        break;
                    case 3:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_12");//添加失败，权限编码重复，无法插入数据。
                        result.success = false;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 保存权限设置
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveFunctions")]
        public HttpResponseMessage SaveFunctions(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Role") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_13");//缺少角色编码参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("UserId") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_14");//缺少用户编码参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("SaveRows") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_15");//缺少功能参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string Role = getValue(jo, "Role");
                string userId = getValue(jo, "UserId");

                List<APPRoleToFunctionEntity> SaveRows = JsonConvert.DeserializeObject<List<APPRoleToFunctionEntity>>(getValue(jo, "SaveRows"));
                int returnValue = roleBll.SaveFunctions(Role, userId, SaveRows);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_9");//添加成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_10");//修改成功
                        result.success = true;
                        break;
                    case 3:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.BSAppRoleController.Tips_16");//添加失败，用户编码重复，无法插入数据。
                        result.success = false;
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message; ;// ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}