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
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.Material;
using ALP.Application.Entity.Material;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// [PM_OperationPalletNum]控制器
    /// 描述:工序物料托盘数量维护
    /// 作者:Dragon
    /// 创建时间:2022-12-02 15:15:55
    /// </summary>
    [Auth]
    [RoutePrefix("PMOperationPalletNum")]
    public class PMOperationPalletNumController : ApiBaseController
    {
        PMOperationPalletNumService _PMOperationPalletNumService = new PMOperationPalletNumService();
        BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂
        Base_Material_Service _baseMaterialService = new Base_Material_Service();//物料主数据


        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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

                var data = _PMOperationPalletNumService.GetPageDataTableList(pagination, queryJson);
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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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

                var data = _PMOperationPalletNumService.GetEntity(t => t.Id == keyValue);
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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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

                if (string.IsNullOrEmpty(entityStr))
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_4"));//缺少Entity参数！

                var entity = JsonConvert.DeserializeObject<PMOperationPalletNumEntity>(entityStr);
                if (entity.DocType == "1")
                {
                    var isAny = _PMOperationPalletNumService.Any(t => t.ProcessCode == entity.ProcessCode && t.MaterialCode == entity.MaterialCode && t.Id != keyValue);
                    if (isAny)
                        return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_5",entity.ProcessName,entity.MaterialCode));//工序【{entity.ProcessName}】、物料【{entity.MaterialCode}】已存在！
                }
                else
                {
                    var isAny = _PMOperationPalletNumService.Any(t => t.ProcessCode == entity.ProcessCode && t.Spec == entity.Spec && t.Id != keyValue);
                    if (isAny)
                        return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_6",entity.ProcessName,entity.Spec));//工序【{entity.ProcessName}】、规格【{entity.Spec}】已存在！
                }

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyCode = CurrentAccount.UserCode;
                    entity.ModifyName = CurrentAccount.UserName;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.CreatorCode = CurrentAccount.UserCode;
                    entity.CreatorName = CurrentAccount.UserName;
                    entity.IsDeleted = false;
                    entity.CreateTime = DateTime.Now;
                }

                int isok = _PMOperationPalletNumService.SaveEntity(keyValue, entity);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_7"));//操作失败

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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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
                var entity = JsonConvert.DeserializeObject<PMOperationPalletNumEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PMOperationPalletNumEntity>>(getValue(jo, "data"));

                if (!string.IsNullOrEmpty(keyValue))
                    isUpdate = true;

                foreach (var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                }
                int isok = _PMOperationPalletNumService.SaveEntity_List(isUpdate, list);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_7"));//操作失败

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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_9"));//id参数不能为空

                int isok = _PMOperationPalletNumService.RemoveForm(t => t.Id == id);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_7"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 批量导入
        /// <summary>
        ///功能描述:  批量导入
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("Import")]
        public HttpResponseMessage Import(JObject jo)
        {
            try
            {
                var lstEntity = JsonConvert.DeserializeObject<List<PMOperationPalletNumEntity>>(getValue(jo, "data"));
                if (lstEntity == null || lstEntity.Count == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_10"));//没有要导入的数据行

                if (lstEntity.Select(t => t.DocType).Distinct().Count() > 1)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_11"));//物料、VC不能同时导入

                var docType = lstEntity.First().DocType;
                //工厂
                var arrFactoryCode = lstEntity.Select(t => t.FactoryCode).Distinct();
                var factoryList = _bsModelWithResourceService.GetList(t => arrFactoryCode.Contains(t.ResourceCode));
                //工序
                var arrProcessCode = lstEntity.Select(t => t.ProcessCode).Distinct();
                var processList = _bsModelWithResourceService.GetList(t => arrProcessCode.Contains(t.ResourceCode));
                //记录
                var lstHistory = _PMOperationPalletNumService.GetList(t => arrProcessCode.Contains(t.ProcessCode) && t.DocType == docType);
                List<Base_MaterialEntity> lstMaterial = new List<Base_MaterialEntity>();

                if (docType == "1")
                {
                    //校验重复
                    foreach (var item in lstEntity.GroupBy(t => new { t.ProcessCode, t.MaterialCode }))
                    {
                        if (item.Count() > 1)
                            return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_12",item.First().ProcessCode,item.First().MaterialCode));//导入的工序【{item.First().ProcessCode}】、物料【{item.First().MaterialCode}】有重复数据！
                    }

                    var arrMaterialCode = lstEntity.Select(t => t.MaterialCode).Distinct();
                    lstMaterial = _baseMaterialService.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();
                }
                else
                {
                    //校验重复
                    foreach (var item in lstEntity.GroupBy(t => new { t.ProcessCode, t.Spec }))
                    {
                        if (item.Count() > 1)
                            return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_13",item.First().ProcessCode,item.First().Spec));//导入的工序【{item.First().ProcessCode}】、规格【{item.First().Spec}】有重复数据！
                    }
                }

                foreach (var item in lstEntity)
                {
                    
                    if (!factoryList.Any(t => t.ResourceCode == item.FactoryCode))
                        return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_14",item.FactoryCode));//工厂【{item.FactoryCode}】不存在

                    if (!processList.Any(t => t.ResourceCode == item.ProcessCode))
                        return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_15",item.ProcessCode));//工序【{item.ProcessCode}】不存在

                    if (item.PalletNum == null)
                        return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_16"));//托盘数量不能为空

                    if (docType == "1")
                    {
                        item.MaterialCode = item.MaterialCode.Trim().ToUpper();
                        if (lstHistory.Any(t => t.ProcessCode == item.ProcessCode && t.MaterialCode == item.MaterialCode))
                            return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_17",item.ProcessCode,item.MaterialCode));//工序【{item.ProcessCode}】、物料【{item.MaterialCode}】已存在！
                    }
                    else
                    {
                        if (lstHistory.Any(t => t.ProcessCode == item.ProcessCode && t.Spec == item.Spec))
                            return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_18",item.ProcessCode,item.Spec));//工序【{item.ProcessCode}】、规格【{item.Spec}】已存在！
                    }
                    var materialEntity = lstMaterial.Find(t => t.MaterialCode == item.MaterialCode);

                    item.Id = Guid.NewGuid().ToString();
                    if (item.DocType == "1")
                    {
                        item.MaterialName = materialEntity?.MaterialName;
                        item.Spec = materialEntity?.Spec;
                    }
                    item.IsDeleted = false;
                    item.CreatorCode = CurrentAccount.UserCode;
                    item.CreatorName = CurrentAccount.UserName;
                    item.CreateTime = DateTime.Now;
                }

                int isok = _PMOperationPalletNumService.SaveEntity_List(false, lstEntity);
                if (isok == 0)
                    return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_7"));//操作失败

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 导出
        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: dragon
        /// 创建日期: 2022-05-16
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("Export")]
        public HttpResponseMessage Export(JObject jo)
        {
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _PMOperationPalletNumService.GetDataTableList_Export(queryJson);

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
                string saveFileName = Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_19") + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";//工序托盘数量维护
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel(Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_20"), data, true);//工序托盘数量维护
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                var filePath = $@"{dirPath}{folder}{saveFileName}";

                return AjaxResult(true, "", filePath);
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("ProduceManage.PMOperationPalletNumController.Tips_21") + ex.Message);//导出失败：
            }
        }
        #endregion

    }
}

