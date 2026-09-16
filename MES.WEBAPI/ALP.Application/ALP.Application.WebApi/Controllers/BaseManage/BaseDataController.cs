using ALP.Application.Busines.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ALP.Application.Entity.BaseManage;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 基础数据类查询接口控制器 
    /// </summary>
    //[Auth]
    [RoutePrefix("Base")]
    public class BaseDataController : ApiBaseController
    {
        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        public HttpResponseMessage GetListByParent(JObject jo)
        {
            DataItemBLL bll = new DataItemBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetListByParent(queryJson);

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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
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
        /// 检验标准关系维护－－产品选择
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaterialPageList")]
        public HttpResponseMessage GetMaterialPageList(JObject jo)
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetMaterialPageList(pagination, queryJson);
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
        /// 弹框选择人员
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEmployeePageList")]
        public HttpResponseMessage GetEmployeePageList(JObject jo)
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetEmployeePageList(pagination, queryJson);
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
        /// 获取用户姓名
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeInfo")]
        public HttpResponseMessage GetEmployeeInfo(string code)
        {
            SystemService bll = new SystemService();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = bll.GetEmployeeInfo(code);

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
        /// 获取用户下拉列表
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserList")]
        public HttpResponseMessage GetUserList(string code = "")
        {
            SystemService bll = new SystemService();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var loginUserCode = CurrentAccount.UserCode;
                var data = bll.GetUserList(code, loginUserCode);

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
        //弹窗选择设备位置
        [HttpPost]
        [Route("GetEquipmentLocation")]
        public HttpResponseMessage GetEquipmentLocation(JObject jo)
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetEquipmentLocation(pagination, queryJson);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
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
        /// 查询仓库-下拉框专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetList_Depositories_Control")]
        public HttpResponseMessage GetList_Depositories_Control()
        {
            SystemService bll = new SystemService();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.GetList_Depositories_Control();
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
        /// 获取供应商
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetSuppliersPageList")]
        public HttpResponseMessage GetSuppliersPageList(JObject jo)//Pagination pagination, string queryJson, ref string msg
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
                    pagination = null;
                    /*result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);*/
                }
                string msg = "";
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetSuppliersPageList(pagination, queryJson, ref msg);
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
        /// 根据设备编码带出设备信息   设备台帐新增使用(孙公聚 2021/6/9)  提出人：牛同刚 2021/6/8
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEquipmentDetail")]
        public HttpResponseMessage GetEquipmentDetail(JObject jo)
        {
            SystemService bll = new SystemService();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string EquipmentCode = getValue(jo, "EquipmentCode");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetEquipmentDetail(EquipmentCode);
                var JsonData = new
                {
                    rows = data,

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


        #region  人员管理

        /// <summary>
        /// 查询人员列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_Person")]
        public HttpResponseMessage GetList_Person(JObject jo)
        {
            Sys_PersonsBLL bll = new Sys_PersonsBLL();
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData(pagination, queryJson);
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
        ///  
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListUser")]
        public HttpResponseMessage GetListUser(JObject jo)
        {
            var bll = new Sys_PersonsBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                var queryJson = getValue(jo, "queryJson");
                var name = "";
                var factoryCode = "";
                if (!string.IsNullOrEmpty(queryJson))
                {
                    Newtonsoft.Json.Linq.JObject queryParam = queryJson.ToJObject();
                    if (!queryParam["Name"].IsEmpty()) name = queryParam["Name"].ToString();
                    if (!queryParam["FactoryCode"].IsEmpty()) factoryCode = queryParam["FactoryCode"].ToString();
                }

                var loginUserCode = CurrentAccount.UserCode;
                result.resultData = new
                {
                    rows = bll.GetListUser(name, pagination, loginUserCode, factoryCode),
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                };
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
        /// 新增 修改人员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePeopleForm")]
        public HttpResponseMessage SavePeopleForm(BS_PeopleEntity entity)
        {
            var bll = new Sys_PersonsBLL();
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";
            var returnNum = "";
            try
            {

                if (string.IsNullOrEmpty(entity.ID))
                {
                    BS_PeopleEntity ent = null;

                    //do
                    //{
                    //    bll.GetSerialNO("Poeple", out returnNum, out msg);
                    //    if (entity.FactoryCode.Contains("3001"))
                    //        entity.Code = "FH" + returnNum;
                    //    else
                    //        entity.Code = "HL" + returnNum;

                    //    ent = bll.GetEntity(t => t.Code == entity.Code);
                    //} while (ent != null);

                    ent = bll.GetEntity(t => t.Code == entity.Code);
                    if (ent != null)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_4"));

                    entity.CreateTime = DateTime.Now;
                }
                if (!string.IsNullOrEmpty(entity.FactoryCode))
                {
                    entity.FactoryCode = entity.FactoryCode.TrimEnd(',');
                    entity.FactoryName = entity.FactoryName.TrimEnd(',');
                }
                bll.SaveForm(entity);
                result.resultData = null; ;
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
        /// 删除人员
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeletePeopleForm")]
        public HttpResponseMessage DeletePeopleForm(string ID)
        {
            var bll = new Sys_PersonsBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                bll.DeleteForm(ID);
                result.resultData = null; ;
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

        #region 部门管理
        /// <summary>
        ///部门管理新增修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDepartmentForm")]
        public HttpResponseMessage SaveDepartmentForm(Sys_DepartmentEntity entity)
        {
            var result = new ResponseResult();
            var bll = new Sys_DepartmentBLL();
            try
            {

                //1.查询是否存在相同的编码和名称
                var ent = bll.GetEntity(t => t.Code == entity.Code || t.Name == entity.Name);
                if (ent != null && string.IsNullOrEmpty(entity.ID))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_4");//存在相同编码或者名称
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (!string.IsNullOrEmpty(entity.ID))
                {
                    var ent1 = bll.GetEntity(t => t.ID != entity.ID && t.Name == entity.Name);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_5");//存在相同名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                entity.UpdateTime = DateTime.Now;
                entity.IsEffective = true;
                bll.SaveForm(entity);

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
        ///部门管理新增修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteDepartment")]
        public HttpResponseMessage DeleteDepartment(string ID)
        {
            var result = new ResponseResult();
            var bll = new Sys_DepartmentBLL();
            try
            {
                bll.DeleteForm(ID);
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
        /// 查询部门列表
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_Department")]
        public HttpResponseMessage GetList_Department(JObject jo)
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData(pagination, queryJson);
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
        [HttpPost]
        [Route("GetDepartmentList")]
        public HttpResponseMessage GetDepartmentList(JObject jo)
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var name = getValue(jo, "Name");
                var data = bll.Get_PageData_Control(name);

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
        /// 获取系统资源
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCompanyResourcePageListJson")]
        public HttpResponseMessage GetCompanyResourcePageList(JObject jo)
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.GetCompanyResourcePageList(pagination, queryJson);
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
        /// 查询部门列表-下拉框专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetList_Department_Control")]
        public HttpResponseMessage GetList_Department_Control()
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData_Control();
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
        /// 根据主键查询部门信息
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDeptInfoById")]
        public HttpResponseMessage GetDeptInfoById(string keyValue)
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.GetEntity(keyValue);
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
        /// 获取按钮权限
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("FucntionAuthority")]
        public HttpResponseMessage FucntionAuthority(JObject jo)
        {
            var result = new ResponseResult();
            string userId = getValue(jo, "userId");
            string BtnNames = getValue(jo, "BtnNames");

            if (string.IsNullOrEmpty(userId))
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_6");//userId 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (string.IsNullOrEmpty(BtnNames))
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseDataController.Tips_7");//BtnNames 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData_Control();
                //获取按钮权限
                ALP.Application.Busines.Comm.CommonBLL comm = new Busines.Comm.CommonBLL();
                string errorMsg = "";
                DataTable dt = comm.FucntionAuthority(userId, BtnNames, out errorMsg);
                string[] array_btn = BtnNames.Split(',');
                JArray jarray_list = new JArray();
                foreach (var item in array_btn)
                {
                    JObject jobj = new JObject();
                    jobj.Add("FunctionName", item);
                    DataTable result_Btn = dt.Select($"FunctionName='{item}'").CopyToDataTable();
                    if (result_Btn != null && result_Btn.Rows.Count > 0)
                    {

                        jobj.Add("value", true);
                    }
                    else
                    {
                        jobj.Add("value", false);
                    }
                    jarray_list.Add(jobj);
                }
                result.resultData = jarray_list;
                if (string.IsNullOrEmpty(errorMsg))
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                }
                else
                {
                    result.success = false;
                    result.returnMsg = errorMsg;
                }
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
        /// 查询调入公司 -- 下拉框
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetInOrgCode")]
        public HttpResponseMessage GetInOrgCode()
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.GetInOrgCode();
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
        /// 查询调入设备区域 -- 下拉框
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetInFactoryCode")]
        public HttpResponseMessage GetInFactoryCode()
        {
            Sys_DepartmentBLL bll = new Sys_DepartmentBLL();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var watch = CommonHelper.TimerStart();
                var data = bll.GetInFactoryCode();
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
        #endregion

        #region 获取按钮权限
        /// <summary> 
        /// 获取按钮权限
        /// </summary>
        /// <param name="jo">{"PageName":"ZhuXianPinKong"}</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetButtonAuthList")]
        public HttpResponseMessage GetButtonAuthList(JObject jo)
        {
            SystemService service = new SystemService();
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = new Pagination();
                string PageName = getValue(jo, "PageName");
                var data = service.GetButtonAuthByPage(PageName);
                result.resultData = data;
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
        #endregion
    }
}