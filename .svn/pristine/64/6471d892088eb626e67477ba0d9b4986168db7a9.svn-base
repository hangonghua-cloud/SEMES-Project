using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Service.ProduceManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Transactions;
using ALP.Application.UtilExtend.Offices;
using System.IO;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// [PM_PerformanceManage]控制器
    /// 描述:绩效管理
    /// 作者:Dragon
    /// 创建时间:2022-11-16 14:40:01
    /// </summary>
    [Auth]
    [RoutePrefix("PMPerformanceManage")]
    public class PMPerformanceManageController : ApiBaseController
    {
        private PMPerformanceManageService _PMPerformanceManageService = new PMPerformanceManageService();
        private BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模
        private Base_KeyParameterItem_Service _baseKeyParameterItemService = new Base_KeyParameterItem_Service();//关键参数
        private BS_People_Service _bsPeopleService = new BS_People_Service();//人员管理
        private PM_TranferCardBGRecord_Service _pmTransferCardBGService = new PM_TranferCardBGRecord_Service();//流转卡报工
        private PM_OwnProductBG_Service _pmOwnProductBGService = new PM_OwnProductBG_Service();//自制半成品报工
        private PM_PackingBGTransferCard_Service _pmPackingBGService = new PM_PackingBGTransferCard_Service();//包装报工

        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败

        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableList")]
        public HttpResponseMessage GetPageDataTableList(JObject jo)
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
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var data = _PMPerformanceManageService.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值</param>
        ///<returns></returns>
        [HttpPost]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                //var entity = JsonConvert.DeserializeObject<PMPerformanceManageEntity>(entityStr)

                result.resultData = _PMPerformanceManageService.GetEntity(t => t.Id == keyValue);
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_4");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");
            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<PMPerformanceManageEntity>(entityStr);

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyCode = userCode;
                    entity.ModifyName = userName;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.CreatorCode = userCode;
                    entity.CreatorName = userName;
                    entity.IsDeleted = false;
                    entity.CreateTime = DateTime.Now;
                }

                int isok = _PMPerformanceManageService.SaveEntity(keyValue, entity);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 批量导入
        /// <summary>
        ///功能描述:  批量导入
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("BatchImport")]
        public HttpResponseMessage BatchImport(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult();
            result.resultData = null;
            var isUpdate = false;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var list = JsonConvert.DeserializeObject<List<PMPerformanceManageEntity>>(getValue(jo, "data"));

                #region 数据校验
                var arrFactoryCode = list.Select(t => t.FactoryCode).Distinct();//工厂编码
                var arrWorkshopCode = list.Select(t => t.WorkshopCode).Distinct();//车间编码
                var arrProcessCode = list.Select(t => t.ProcessCode).Distinct();//工序编码
                var arrPostCode = list.Select(t => t.PostCode).Distinct();//岗位编码
                var arrPeople = list.Select(t => t.UserCode).Distinct();//员工工号

                var factoryList = _bsModelWithResourceService.GetList(t => arrFactoryCode.Contains(t.ResourceCode));
                var workShopList = _bsModelWithResourceService.GetList(t => arrWorkshopCode.Contains(t.ResourceCode));
                var processList = _bsModelWithResourceService.GetList(t => arrProcessCode.Contains(t.ResourceCode));
                var postList = _baseKeyParameterItemService.Get_ExpressionList(t => t.EnCode == "ProcessPost" && arrPostCode.Contains(t.Col1));
                var peopleList = _bsPeopleService.Get_ExpressionList(t => arrPeople.Contains(t.Code));
                #endregion

                foreach (var item in list)
                {
                    if (!factoryList.Any(t => t.ResourceCode == item.FactoryCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_6", item.FactoryCode));//工厂编码【{item.FactoryCode}】不存在！

                    if (!workShopList.Any(t => t.ResourceCode == item.WorkshopCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_7", item.WorkshopCode));//车间编码【{item.WorkshopCode}】不存在！

                    if (!processList.Any(t => t.ResourceCode == item.ProcessCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_8", item.ProcessCode));//工序编码【{item.ProcessCode}】不存在！

                    if (!postList.Any(t => t.Col1 == item.PostCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_9", item.PostCode));//岗位编码【{item.PostCode}】不存在！

                    if (!peopleList.Any(t => t.Code == item.UserCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_10", item.UserCode));//员工工号【{item.UserCode}】不存在！

                    item.Id = Guid.NewGuid().ToString();
                    item.CreatorCode = userCode;
                    item.CreatorName = userName;
                    item.CreateTime = DateTime.Now;
                }
                int isok = _PMPerformanceManageService.SaveEntity_List(isUpdate, list);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<PMPerformanceManageEntity>(entityStr);

                int isok = _PMPerformanceManageService.RemoveForm(t => t.Id == entity.Id);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? SuccessMsg : FaildMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 薪资计算失败查询
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableListBySalary")]
        public HttpResponseMessage GetPageDataTableListBySalary(JObject jo)
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
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();

                var data = _PMPerformanceManageService.GetPageDataTableListBySalary(pagination, queryJson);
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
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 重新计算
        /// <summary>
        ///功能描述:  重新计算
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("ReCalculation")]
        public HttpResponseMessage ReCalculation(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "data"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_11"));//没有可操作的数据行！
                _PMPerformanceManageService.ReCalculation(data);

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 忽略
        /// <summary>
        ///功能描述:  忽略
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("Neglect")]
        public HttpResponseMessage Neglect(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<PerformanceManageDto>>(getValue(jo, "data"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_11"));//没有可操作的数据行！

                var arrIds1 = data.Where(t => t.BGType == "1").Select(t => t.Id); //流转卡报工
                var arrIds2 = data.Where(t => t.BGType == "2").Select(t => t.Id);//自制半成品
                var arrIds3 = data.Where(t => t.BGType == "3").Select(t => t.Id);//包装报工

                string msg = "";
                if (arrIds1.Count() > 0)
                {
                    var transferCardBGList = _pmTransferCardBGService.Get_ExpressionList(t => arrIds1.Contains(t.Id)).ToList();
                    foreach (var item in transferCardBGList)
                    {
                        item.IsCalculated = false;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                        item.Remark = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_13");//忽略
                    }
                    _pmTransferCardBGService.SaveEntity_List(true, CurrentAccount.UserName, transferCardBGList, out msg);
                }
                if (arrIds2.Count() > 0)
                {
                    var ownBGList = _pmOwnProductBGService.Get_ExpressionList(t => arrIds2.Contains(t.Id)).ToList();
                    foreach (var item in ownBGList)
                    {
                        item.IsCalculated = false;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                        item.Remark = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_13");//忽略
                    }
                    _pmOwnProductBGService.SaveEntity_List(true, CurrentAccount.UserName, ownBGList, out msg);
                }
                if (arrIds3.Count() > 0)
                {
                    var packingBGList = _pmPackingBGService.Get_ExpressionList(t => arrIds3.Contains(t.Id)).ToList();
                    foreach (var item in packingBGList)
                    {
                        item.IsCalculated = false;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                        item.Remark = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_13");//忽略
                    }
                    _pmPackingBGService.SaveEntity_List(true, CurrentAccount.UserName, packingBGList, out msg);
                }

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: dragon
        /// 创建日期: 2021-07-27 16:28:16
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PMPerformanceManage_Export")]
        public HttpResponseMessage PMPerformanceManage_Export(JObject jo)
        {
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _PMPerformanceManageService.GetDataTableList_Export(queryJson);

                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);
                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_14") + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";//绩效管理
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel(ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_15"), data, true);//绩效管理
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                var filePath = $@"{dirPath}{folder}{saveFileName}";

                return AjaxResult(true, "", filePath);
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMPerformanceManageController.Tips_16") + ex.Message);//导出失败：
            }
        }
        #endregion

        private class PerformanceManageDto
        {
            public string Id { get; set; }
            public string BGType { get; set; }
        }
    }
}

