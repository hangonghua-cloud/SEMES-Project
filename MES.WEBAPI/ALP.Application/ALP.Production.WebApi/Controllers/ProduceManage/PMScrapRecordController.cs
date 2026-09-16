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
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.SystemManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// [PM_ScrapRecord]控制器
    /// 描述:PM_生产报废记录
    /// 作者:Dragon
    /// 创建时间:2022-12-21 09:11:16
    /// </summary>
    [Auth]
    [RoutePrefix("PMScrapRecord")]
    public class PMScrapRecordController : ApiBaseController
    {
        PMScrapRecordService _PMScrapRecordService = new PMScrapRecordService();//生产报废记录
        BaseSequenceService _baseSequenceService = new BaseSequenceService();//序列号
        PM_BGBadRecord_Service _pmBGBadRecordService = new PM_BGBadRecord_Service();//报工不良记录
        DataItemDetailService _dataItemDetailService = new DataItemDetailService();//数据字典
        BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模


        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableList")]
        public HttpResponseMessage GetPageDataTableList(JObject jo)
        {
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

                var data = _PMScrapRecordService.GetPageDataTableList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                return AjaxResult(true, Language.GetText("Common.Success"), JsonData);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值</param>
        ///<returns></returns>
        [HttpPost]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(JObject jo)
        {
            try
            {
                var keyValue = getValue(jo, "KeyValue");

                var data = _PMScrapRecordService.GetEntity(t => t.Id == keyValue);
                return AjaxResult(true, Language.GetText("Common.Success"), data);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                var entityStr = getValue(jo, "Entity");
                var entity = JsonConvert.DeserializeObject<PMScrapRecordEntity>(entityStr);
                if (entity == null)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_4"));//没有要保存的数据!

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyCode = CurrentAccount.UserCode;
                    entity.ModifyName = CurrentAccount.UserName;
                    entity.ModifyTime = DateTime.Now;

                    //查询分摊区间的报工不良记录

                    var cardBGBadList = _pmBGBadRecordService.Get_ExpressionList(t => t.ScrapId == keyValue && t.BadQty != null).ToList();
                    if (cardBGBadList.Count == 0)
                        return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_5"));//没有可分摊的报工不良记录！

                    foreach (var item in cardBGBadList)
                    {
                        item.ShareQty = entity.ScrapQty * item.BadRatio;
                        item.ShareStatus = "1";
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }
                    string msg = "";
                    _pmBGBadRecordService.SaveEntity_List(true, CurrentAccount.UserName, cardBGBadList, out msg);
                }
                else
                {
                    var serialNo = _baseSequenceService.GetSerialNO("ScrapCode");
                    entity.ScrapCode = DateTime.Now.ToString("yyyyMMdd") + "-" + serialNo;
                    entity.Id = Guid.NewGuid().ToString();
                    entity.CreatorCode = CurrentAccount.UserCode;
                    entity.CreatorName = CurrentAccount.UserName;
                    entity.IsDeleted = false;
                    entity.CreateTime = DateTime.Now;

                    //查询分摊区间的报工不良记录
                    var startTime = (entity.StartTime.Value.ToShortDateString() + " 00:00:00").ToDateTime();
                    var endTime = (entity.EndTime.Value.ToShortDateString() + " 23:59:59").ToDateTime();
                    var cardBGBadList = _pmBGBadRecordService.Get_ExpressionList(t => t.CreateTime >= startTime
                         && t.CreateTime <= endTime && t.FactoryCode == entity.FactoryCode && t.BadItemCode == entity.BadItemCode
                         && t.BadQty != null && t.ShareStatus != "1" && t.SmallClassCode == entity.SmallClassCode).ToList();
                    if (cardBGBadList.Count == 0)
                        return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_5"));//没有可分摊的报工不良记录！

                    var totalBadQty = cardBGBadList.Sum(t => t.BadQty);//不良原因 不良总数
                    foreach (var item in cardBGBadList)
                    {
                        item.ScrapCode = entity.ScrapCode;
                        item.ScrapId = entity.Id;
                        item.TotalBadQty = totalBadQty;
                        item.BadRatio = Math.Round(item.BadQty.Value / totalBadQty.Value, 6, MidpointRounding.AwayFromZero);
                        item.ShareQty = entity.ScrapQty * item.BadRatio;
                        item.ShareStatus = "1";
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }
                    string msg = "";
                    _pmBGBadRecordService.SaveEntity_List(true, CurrentAccount.UserName, cardBGBadList, out msg);
                }

                int isok = _PMScrapRecordService.SaveEntity(keyValue, entity);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_6"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 批量保存
        /// <summary>
        ///功能描述:  批量保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveBatchForm")]
        public HttpResponseMessage SaveBatchForm(JObject jo)
        {
            var isUpdate = false;
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                var entity = JsonConvert.DeserializeObject<PMScrapRecordEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PMScrapRecordEntity>>(getValue(jo, "data"));

                if (!string.IsNullOrEmpty(keyValue))
                    isUpdate = true;

                foreach (var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                int isok = _PMScrapRecordService.SaveEntity_List(isUpdate, list);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_6"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
            try
            {
                var id = getValue(jo, "id");
                if (string.IsNullOrEmpty(id))
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_8"));//id参数不能为空

                var cardBGBadList = _pmBGBadRecordService.Get_ExpressionList(t => t.ScrapId == id).ToList();
                foreach (var item in cardBGBadList)
                {
                    item.ScrapId = "";
                    item.ScrapCode = "";
                    item.TotalBadQty = 0;
                    item.BadRatio = 0;
                    item.ShareQty = 0;
                    item.ShareStatus = "0";
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }
                string msg = "";
                _pmBGBadRecordService.SaveEntity_List(true, CurrentAccount.UserName, cardBGBadList, out msg);

                int isok = _PMScrapRecordService.RemoveForm(t => t.Id == id);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_6"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 导入
        /// <summary>
        ///功能描述:  导入
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("Import")]
        public HttpResponseMessage Import(JObject jo)
        {
            try
            {
                var entity = JsonConvert.DeserializeObject<PMScrapRecordEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PMScrapRecordEntity>>(getValue(jo, "data"));
                var importType = getValue(jo, "importType");

                if (list == null || list.Count == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_9"));//没有可导入的数据！

                string msg = "";
                string scrapCode = "";
                List<PM_BGBadRecordEntity> oldCardBGBadList = new List<PM_BGBadRecordEntity>();
                if (importType == "1")//新增
                {
                    var serialNo = _baseSequenceService.GetSerialNO("ScrapCode");
                    scrapCode = DateTime.Now.ToString("yyyyMMdd") + "-" + serialNo;
                }
                else //修改
                {
                    scrapCode = entity.ScrapCode;
                    oldCardBGBadList = _pmBGBadRecordService.Get_ExpressionList(t => t.ScrapCode == scrapCode).ToList();
                    foreach (var item in oldCardBGBadList)
                    {
                        item.ScrapCode = " ";
                        item.ScrapId = " ";
                        item.ShareStatus = "0";
                    }
                    //回退分摊记录
                    _pmBGBadRecordService.SaveEntity_List(true, CurrentAccount.UserName, oldCardBGBadList, out msg);
                    //删除原生产报废记录
                    //_pmBGBadRecordService.RemoveForm(t => t.ScrapCode == scrapCode); //为啥要删除，待确认
                }

                //数据处理
                var lstSmallClass = _dataItemDetailService.GetDataItemList("MaterialSmall").ToList();
                var lstBadItem = _dataItemDetailService.GetDataItemList("PoorWorkReport").Where(t => t.Remark1 == entity.FactoryCode).ToList();
                var lstProcess = _bsModelWithResourceService.GetList(t => t.ModelLeve == "Process").ToList();
                foreach (var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.ScrapCode = scrapCode;
                    item.FactoryCode = entity.FactoryCode;
                    item.FactoryName = entity.FactoryName;
                    item.SmallClassCode = lstSmallClass.Find(t => t.ItemName == item.SmallClassName)?.ItemValue;
                    item.ProcessCode = lstBadItem.Find(t => t.ItemName == item.BadItemName)?.Description;
                    item.ProcessName = lstProcess.Find(t => t.ResourceCode == item.ProcessCode)?.ResourceName;
                    item.BadItemName = lstBadItem.Find(t => t.ItemValue == item.BadItemCode)?.ItemName;
                    item.StartTime = entity.StartTime;
                    item.EndTime = entity.EndTime;
                    item.IsDeleted = false;
                    item.CreatorCode = CurrentAccount.UserCode;
                    item.CreatorName = CurrentAccount.UserName;
                    item.CreateTime = DateTime.Now;
                }


                var arrBadItemCode = list.Select(t => t.BadItemCode).Distinct().ToArray();
                var arrSmallClassCode = list.Select(t => t.SmallClassCode).Distinct().ToArray();

                //查询分摊区间的报工不良记录
                var startTime = (entity.StartTime.Value.ToShortDateString() + " 00:00:00").ToDateTime();
                var endTime = (entity.EndTime.Value.ToShortDateString() + " 23:59:59").ToDateTime();
                var cardBGBadList = _pmBGBadRecordService.Get_ExpressionList(t => t.CreateTime >= startTime && t.CreateTime <= endTime
                    && t.FactoryCode == entity.FactoryCode && arrBadItemCode.Contains(t.BadItemCode) && t.ShareStatus != "1" && t.BadQty != null
                    && arrSmallClassCode.Contains(t.SmallClassCode)).ToList();
                if (cardBGBadList.Count == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_10"));//没有可分摊的报工不良记录！

                foreach (var item in list)
                {
                    var badItemCardBGBadList = cardBGBadList.Where(t => t.BadItemCode == item.BadItemCode && t.SmallClassCode == item.SmallClassCode).ToList();
                    var totalBadQty = badItemCardBGBadList.Sum(t => t.BadQty);//不良原因 不良总数
                    if (totalBadQty == 0)
                        //return AjaxResult(false, $"不良总数不能为0,不良原因【{item.BadItemName}】");
                        continue;

                    foreach (var detail in badItemCardBGBadList)
                    {
                        detail.ScrapCode = item.ScrapCode;
                        detail.ScrapId = item.Id;
                        detail.TotalBadQty = totalBadQty;
                        detail.BadRatio = Math.Round(detail.BadQty.Value / totalBadQty.Value, 6, MidpointRounding.AwayFromZero);
                        detail.ShareQty = item.ScrapQty * detail.BadRatio;
                        detail.ShareStatus = "1";
                        detail.ModifyBy = CurrentAccount.UserCode;
                        detail.ModifyTime = DateTime.Now;
                    }
                }

                _pmBGBadRecordService.SaveEntity_List(true, CurrentAccount.UserName, cardBGBadList, out msg);
                if (importType == "2")
                    _PMScrapRecordService.RemoveForm(t => t.ScrapCode == entity.ScrapCode);
                int isok = _PMScrapRecordService.SaveEntity_List(false, list);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_6"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 获取报废编码
        /// <summary>
        ///功能描述:  获取报废编码
        ///创　　建: Dragon
        ///创建日期: 2022-12-21 09:11:16
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值</param>
        ///<returns></returns>
        [HttpPost]
        [Route("GetScrapCodeList")]
        public HttpResponseMessage GetScrapCodeList(JObject jo)
        {
            try
            {
                var data = _PMScrapRecordService.GetList(t => true).OrderByDescending(t => t.CreateTime)
                    .Select(t => new { t.ScrapCode }).Distinct().Take(10);
                return AjaxResult(true, Language.GetText("Common.Success"), data);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 导入分摊
        /// <summary>
        ///功能描述:  导入分摊
        ///创　　建: Dragon
        ///创建日期: 2024-01-22
        ///任务编号: PM_生产报废记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("ImportShare")]
        public HttpResponseMessage ImportShare(JObject jo)
        {
            try
            {
                var list = JsonConvert.DeserializeObject<List<PM_BGBadRecordEntity>>(getValue(jo, "data"));

                if (list == null || list.Count == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_9"));//没有可导入的数据！

                if (list.Any(t => string.IsNullOrEmpty(t.Id)))
                    return AjaxResult(false, "Id不能为空！");

                string msg = "";
                _pmBGBadRecordService.SaveEntity_List(true, "", list, out msg);

                //int isok = new PM_BGBadShare_Service().Insert(list);
                //if (isok == 0)
                //    return AjaxResult(false, Language.GetText("ProduceManage.PMScrapRecordController.Tips_6"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion
    }
}

