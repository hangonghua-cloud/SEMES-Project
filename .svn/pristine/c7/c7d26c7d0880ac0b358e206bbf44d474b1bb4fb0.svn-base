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
using ALP.Application.Service.Material;
using ALP.Application.Busines.ModelLevel;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// [PM_ProductPrice]控制器
    /// 描述:产品工价维护
    /// 作者:Dragon
    /// 创建时间:2022-11-08 14:54:29
    /// </summary>
    [Auth]
    [RoutePrefix("PMProductPrice")]
    public class PMProductPriceController : ApiBaseController
    {
        private PMProductPriceService _PMProductPriceService = new PMProductPriceService();
        private Base_Material_Service _baseMaterialService = new Base_Material_Service();//物料
        BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模
        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败

        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
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

                var data = _PMProductPriceService.GetPageDataTableList(pagination, queryJson);
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
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
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
                //var entity = JsonConvert.DeserializeObject<PMProductPriceEntity>(entityStr)

                result.resultData = _PMProductPriceService.GetEntity(t => t.Id == keyValue);
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
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_4");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");
            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<PMProductPriceEntity>(entityStr);
                if (entity.PriceType == "1")
                {
                    //唯一标识：工价类型、工序、物料、岗位、人数
                    var isAny = _PMProductPriceService.Any(t => t.PriceType == entity.PriceType && t.ProcessCode == entity.ProcessCode
                        && t.MaterialCode == entity.MaterialCode && t.PostCode == entity.PostCode && t.PeopleQty == entity.PeopleQty && t.Id != keyValue);
                    if (isAny)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_6"));//当前工序该物料、岗位、班组人数已存在！
                    //默认的唯一标志：工价类型、工序、物料、岗位、是否默认
                    if (entity.IsDefault == true)
                    {
                        var isAny2 = _PMProductPriceService.Any(t => t.PriceType == entity.PriceType && t.ProcessCode == entity.ProcessCode
                        && t.MaterialCode == entity.MaterialCode && t.PostCode == entity.PostCode && t.IsDefault == true && t.Id != keyValue);
                        if (isAny2)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_7"));//当前工序该物料、岗位已有默认值！
                    }
                }
                else
                {
                    //唯一标识：工价类型、工序、规格、岗位、人数
                    var isAny = _PMProductPriceService.Any(t => t.PriceType == entity.PriceType && t.ProcessCode == entity.ProcessCode
                        && t.Spec == entity.Spec && t.PostCode == entity.PostCode && t.PeopleQty == entity.PeopleQty && t.Id != keyValue);
                    if (isAny)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_8"));//当前工序该规格、岗位、班组人数已存在！

                    //默认的唯一标志：工价类型、工序、规格、岗位、是否默认
                    if (entity.IsDefault == true)
                    {
                        var isAny2 = _PMProductPriceService.Any(t => t.PriceType == entity.PriceType && t.ProcessCode == entity.ProcessCode
                        && t.Spec == entity.Spec && t.PostCode == entity.PostCode && t.IsDefault == true && t.Id != keyValue);
                        if (isAny2)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_9"));//当前工序该规格、岗位已有默认值！
                    }
                }

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

                int isok = _PMProductPriceService.SaveEntity(keyValue, entity);
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

        #region 批量保存
        /// <summary>
        ///功能描述:  批量保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveBatchForm")]
        public HttpResponseMessage SaveBatchForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var isUpdate = false;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_4");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var keyValue = getValue(jo, "KeyValue");

            try
            {
                //var entity = JsonConvert.DeserializeObject<PMProductPriceEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PMProductPriceEntity>>(getValue(jo, "data"));

                if (!string.IsNullOrEmpty(keyValue))
                {
                    isUpdate = true;
                    //entity.ModifyBy = userCode;
                    //entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    //entity.Creator = userCode;
                    //entity.CreateTime = DateTime.Now;
                }

                foreach (var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                int isok = _PMProductPriceService.SaveEntity_List(isUpdate, list);
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
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_3");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_5");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var entityStr = getValue(jo, "Entity");

            try
            {
                var entity = JsonConvert.DeserializeObject<PMProductPriceEntity>(entityStr);

                int isok = _PMProductPriceService.RemoveForm(t => t.Id == entity.Id);
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
        ///功能描述: 产品工价导入
        ///创　　建: Dragon
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("ProductPrice_Import")]
        public HttpResponseMessage ProductPrice_Import(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult();
            result.resultData = null;

            try
            {
                var list = JsonConvert.DeserializeObject<List<PMProductPriceEntity>>(getValue(jo, "data"));

                var priceType = list.FirstOrDefault()?.PriceType;
                if (priceType == "1") //物料
                {
                    foreach (var item in list.Where(t => t.IsDefault == true).GroupBy(t => new { t.ProcessCode, t.MaterialCode, t.PostCode }))
                    {
                        if (item.Count() > 1)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_10", item.First().ProcessCode, item.First().MaterialCode));//导入的工序【{item.First().ProcessCode}】、物料【{item.First().MaterialCode}】只能有一行默认数据
                    }
                }
                else  //VC
                {
                    foreach (var item in list.Where(t => t.IsDefault == true).GroupBy(t => new { t.ProcessCode, t.Spec, t.PostCode }))
                    {
                        if (item.Count() > 1)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_11", item.First().ProcessCode, item.First().Spec));//导入的工序【{item.First().ProcessCode}】、规格【{item.First().Spec}】只能有一行默认数据
                    }
                }

                foreach (var item in list)
                {
                    if (!item.PeopleQty.HasValue)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_12"));//班组人数不能为空！

                    var factoryEntity = _bsModelWithResourceService.GetFactoryByProcess(item.ProcessCode);
                    if (factoryEntity == null)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_13", item.ProcessCode));//工序【{item.ProcessCode}】不存在

                    if (factoryEntity.ResourceCode != item.FactoryCode)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_14", item.ProcessCode, item.FactoryCode));//工序【{item.ProcessCode}】不在工厂【{item.FactoryCode}】下！

                    if (priceType == "1")
                    {
                        //唯一标识：工价类型、工序、物料、岗位、人数
                        var isAny = _PMProductPriceService.Any(t => t.PriceType == item.PriceType && t.ProcessCode == item.ProcessCode
                            && t.MaterialCode == item.MaterialCode && t.PostCode == item.PostCode && t.PeopleQty == item.PeopleQty
                            && t.Id != item.Id);
                        if (isAny)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_15", item.ProcessCode, item.MaterialCode, item.PostName, item.PeopleQty.ToString()));//工序[{item.ProcessCode}]、物料[{item.MaterialCode}]、岗位[{item.PostName}]、班组人数[{item.PeopleQty.ToString()}]已存在！
                        //默认的唯一标志：工价类型、工序、物料、是否默认
                        if (item.IsDefault == true)
                        {
                            var isAny2 = _PMProductPriceService.Any(t => t.PriceType == item.PriceType && t.ProcessCode == item.ProcessCode
                            && t.MaterialCode == item.MaterialCode && t.PostCode == item.PostCode && t.IsDefault == true && t.Id != item.Id);
                            if (isAny2)
                                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_16", item.ProcessCode, item.MaterialCode, item.PostName));//工序[{item.ProcessCode}]、物料[{item.MaterialCode}]、岗位[{item.PostName}]已有默认值！
                        }
                    }
                    else
                    {
                        //唯一标识：工价类型、工序、规格、岗位、人数
                        var isAny = _PMProductPriceService.Any(t => t.PriceType == item.PriceType && t.ProcessCode == item.ProcessCode
                            && t.Spec == item.Spec && t.PostCode == item.PostCode && t.PeopleQty == item.PeopleQty && t.Id != item.Id);
                        if (isAny)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_17", item.ProcessCode, item.Spec, item.PostName, item.PeopleQty.ToString()));//工序[{item.ProcessCode}]、规格[{item.Spec}]、岗位[{item.PostName}]、班组人数[{item.PeopleQty.ToString()}]已存在！

                        //默认的唯一标志：工价类型、工序、规格、岗位、是否默认
                        if (item.IsDefault == true)
                        {
                            var isAny2 = _PMProductPriceService.Any(t => t.PriceType == item.PriceType && t.ProcessCode == item.ProcessCode
                            && t.Spec == item.Spec && t.PostCode == item.PostCode && t.IsDefault == true && t.Id != item.Id);
                            if (isAny2)
                                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMProductPriceController.Tips_18", item.ProcessCode, item.Spec, item.PostName));//工序[{item.ProcessCode}]、规格[{item.Spec}]、岗位[{item.PostName}]已有默认值！
                        }
                    }
                }

                if (priceType == "1") //物料
                {
                    var materialList = _baseMaterialService.GetList(t => true);
                    var data = from a in list
                               join b in materialList on a.MaterialCode equals b.MaterialCode
                               select new PMProductPriceEntity()
                               {
                                   Id = Guid.NewGuid().ToString(),
                                   FactoryCode = a.FactoryCode,
                                   FactoryName = a.FactoryName,
                                   ProcessCode = a.ProcessCode,
                                   ProcessName = a.ProcessName,
                                   PostCode = a.PostCode,
                                   PostName = a.PostName,
                                   PriceType = a.PriceType,
                                   MaterialCode = a.MaterialCode,
                                   MaterialName = b.MaterialName,
                                   Spec = b.Spec,
                                   UnitName = b.UnitName,
                                   PeopleQty = a.PeopleQty,
                                   Price = a.Price,
                                   IsDefault = a.IsDefault,
                                   IsDeleted = false,
                                   Remark = a.Remark,
                                   CreatorCode = userCode,
                                   CreatorName = userName,
                                   CreateTime = DateTime.Now
                               };
                    list = data.ToList();
                }
                else
                {
                    foreach (var item in list)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.IsDeleted = false;
                        item.CreatorCode = userCode;
                        item.CreatorName = userName;
                        item.CreateTime = DateTime.Now;
                    }
                }

                int isok = _PMProductPriceService.SaveEntity_List(false, list);
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
    }
}

