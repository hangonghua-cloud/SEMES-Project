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
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.Material;
using ALP.Application.Service.MaterialManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.UtilExtend.Util;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// [PM_OwnSemiProductOrder]控制器
    /// 描述:自制半成品订单管理
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:24:28
    /// </summary>
    [Auth]
    [RoutePrefix("PMOwnSemiProductOrder")]
    public class PMOwnSemiProductOrderController : ApiBaseController
    {
        private PMOwnSemiProductOrderService _PMOwnSemiProductOrderService = new PMOwnSemiProductOrderService();
        MM_RawMaterialStock_Service _rawMaterialStockService = new MM_RawMaterialStock_Service();//原材料库存
        MM_RawMaterialOut_Service _rawMaterialOutService = new MM_RawMaterialOut_Service();//原材料出库
        BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模
        Base_MaterialFactory_Service _baseMaterialFactoryService = new Base_MaterialFactory_Service();//物料工厂

        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
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

                var data = _PMOwnSemiProductOrderService.GetPageDataTableList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"), JsonData);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
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

                var data = _PMOwnSemiProductOrderService.GetEntity(t => t.Id == keyValue);
                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"), data);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
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
                var entity = JsonConvert.DeserializeObject<PMOwnSemiProductOrderEntity>(entityStr);
                if (entity == null)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_4"));//没有要保存的数据!

                var isAny = _PMOwnSemiProductOrderService.Any(t => t.ProductOrder == entity.ProductOrder
                    && t.MaterialCode == entity.MaterialCode && t.Id != keyValue);
                if (isAny)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_5",entity.ProductOrder,entity.MaterialCode));//订单号【{entity.ProductOrder}】、物料【{entity.MaterialCode}】已存在！

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyCode = CurrentAccount.UserCode;
                    entity.ModifyName = CurrentAccount.UserName;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.OrderStatus = "1";
                    entity.DeliveryQty = 0;
                    entity.CreatorCode = CurrentAccount.UserCode;
                    entity.CreatorName = CurrentAccount.UserName;
                    entity.IsDeleted = false;
                    entity.CreateTime = DateTime.Now;
                }

                int isok = _PMOwnSemiProductOrderService.SaveEntity(keyValue, entity);
                if (isok == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_6"));//操作失败

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 批量导入
        /// <summary>
        ///功能描述:  批量导入
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("PMOwnSemiProductOrder_Import")]
        public HttpResponseMessage PMOwnSemiProductOrder_Import(JObject jo)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<List<PMOwnSemiProductOrderEntity>>(getValue(jo, "data"));
                if (data == null && data.Count == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_8"));//没有要保存的数据行！
                //工厂
                var arrFactoryCode = data.Select(t => t.FactoryCode).Distinct();
                var factoryList = _bsModelWithResourceService.GetList(t => arrFactoryCode.Contains(t.ResourceCode)).ToList();
                //工序
                var arrProcessCode = data.Select(t => t.ProcessCode).Distinct();
                var processList = _bsModelWithResourceService.GetList(t => arrProcessCode.Contains(t.ResourceCode)).ToList();
                //物料
                var factoryCode = arrFactoryCode.FirstOrDefault();
                var arrMaterialCode = data.Select(t => t.MaterialCode).Distinct();
                var materialFactoryList = _baseMaterialFactoryService.Get_ExpressionList(t => t.FactoryCode == factoryCode
                    && arrMaterialCode.Contains(t.MaterialCode)).ToList();
                //订单
                var arrProductOrder = data.Select(t => t.ProductOrder).Distinct();
                var ownSemiProductOrderList = _PMOwnSemiProductOrderService.GetList(t => arrProductOrder.Contains(t.ProductOrder));

                foreach (var item in data)
                {
                    if (ownSemiProductOrderList.Any(t => t.ProductOrder == item.ProductOrder))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_9",item.ProductOrder));//订单【{item.ProductOrder}】已存在!

                    if (!factoryList.Any(t => t.ResourceCode == item.FactoryCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_10",item.FactoryCode));//工厂【{item.FactoryCode}】不存在！

                    if (!processList.Any(t => t.ResourceCode == item.ProcessCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_11",item.ProcessCode));//工序【{item.ProcessCode}】不存在！

                    if (!materialFactoryList.Any(t => t.FactoryCode == item.FactoryCode && t.MaterialCode == item.MaterialCode))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_12",item.MaterialCode));//物料【{item.MaterialCode}】不存在！

                    if (string.IsNullOrEmpty(item.ProductOrder))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_13"));//订单号不能为空！

                    if (string.IsNullOrEmpty(item.ContainerNO))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_14"));//柜号不能为空！

                    if (!item.ProductQty.HasValue || item.ProductQty <= 0)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_15"));//订单数量必须大于0！

                    if (string.IsNullOrEmpty(item.UnitName))
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_16"));//单位不能为空！

                    if (!item.PalletNum.HasValue || item.PalletNum <= 0)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_17"));//托数必须大于0！

                    if (!item.VolumeNum.HasValue || item.VolumeNum <= 0)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_18"));//卷数必须大于0！

                    if (!item.Meters.HasValue || item.Meters <= 0)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_19"));//米数必须大于0！

                    item.Id = Guid.NewGuid().ToString();
                    item.DeliveryQty = 0;
                    item.OrderStatus = "1";
                    item.IsDeleted = false;
                    item.CreatorCode = CurrentAccount.UserCode;
                    item.CreatorName = CurrentAccount.UserName;
                    item.CreateTime = DateTime.Now;
                }
                int isok = _PMOwnSemiProductOrderService.SaveEntity_List(false, data);
                if (isok == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_6"));//操作失败

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
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
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_20"));//id参数不能为空

                int isok = _PMOwnSemiProductOrderService.RemoveForm(t => t.Id == id);
                if (isok == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_6"));//操作失败

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 出库
        /// <summary>
        /// 自制半成品订单-出库
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PMOwnSemiProductOrderOut")]
        public HttpResponseMessage PMOwnSemiProductOrderOut(JObject jo)
        {
            try
            {
                var keyValue = getValue(jo, "keyValue");
                if (string.IsNullOrEmpty(keyValue))
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_21"));//keyValue参数不能为空

                var ownSemiProductOrderEntity = _PMOwnSemiProductOrderService.GetEntity(t => t.Id == keyValue);
                if (ownSemiProductOrderEntity == null)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_22"));//订单不存在

                var data = JsonConvert.DeserializeObject<List<MM_RawMaterialStockEntity>>(getValue(jo, "data"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_23"));//没有要保存的数据行

                #region 原材料库存
                var arrStockId = data.Select(t => t.Id);
                var lstRawMaterialStock = _rawMaterialStockService.Get_ExpressionList(t => arrStockId.Contains(t.Id)).ToList();
                foreach (var item in lstRawMaterialStock)
                {
                    if (item.IsFrozen == "1")
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_24",item.MaterialCode,item.BatchNo,item.LocationCode));//库存已冻结，物料【{item.MaterialCode}】、批次【{item.BatchNo}】、库位【{item.LocationCode}】

                    var sourceEntity = data.Find(t => t.Id == item.Id);
                    if (sourceEntity.OutQty > item.Qty)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PMOwnSemiProductOrderController.Tips_25",item.MaterialCode,item.BatchNo,item.LocationCode));//出库数量不能大于库存数量，物料【{item.MaterialCode}】、批次【{item.BatchNo}】、库位【{item.LocationCode}】

                    item.Qty -= sourceEntity.OutQty;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 出库记录
                var lstRawMaterialOut = new List<MM_RawMaterialOutEntity>();
                data.ForEach(item =>
                {
                    var rawMaterialOutEntity = Tools.Mapper<MM_RawMaterialOutEntity, MM_RawMaterialStockEntity>(item);
                    rawMaterialOutEntity.Id = Guid.NewGuid().ToString();
                    rawMaterialOutEntity.BusinessId = keyValue;
                    rawMaterialOutEntity.BusinessTable = "PM_OwnSemiProductOrder";
                    rawMaterialOutEntity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                    rawMaterialOutEntity.OutType = "5";
                    rawMaterialOutEntity.Qty = item.OutQty;
                    rawMaterialOutEntity.Creator = CurrentAccount.UserCode;
                    rawMaterialOutEntity.CreatorName = CurrentAccount.UserName;
                    rawMaterialOutEntity.CreateTime = DateTime.Now;
                    rawMaterialOutEntity.ModifyBy = CurrentAccount.UserCode;
                    rawMaterialOutEntity.ModifyTime = DateTime.Now;
                    lstRawMaterialOut.Add(rawMaterialOutEntity);
                });
                #endregion

                #region 订单更新
                var totalOutQty = data.Sum(t => t.OutQty);
                ownSemiProductOrderEntity.DeliveryQty += totalOutQty;
                ownSemiProductOrderEntity.ModifyCode = CurrentAccount.UserCode;
                ownSemiProductOrderEntity.ModifyName = CurrentAccount.UserName;
                ownSemiProductOrderEntity.ModifyTime = DateTime.Now;
                if (ownSemiProductOrderEntity.DeliveryQty < ownSemiProductOrderEntity.ProductQty)
                    ownSemiProductOrderEntity.OrderStatus = "2";
                else
                    ownSemiProductOrderEntity.OrderStatus = "3";
                #endregion

                //执行事务
                var msg = "";
                using (TransactionScope ts = new TransactionScope())
                {
                    _rawMaterialOutService.SaveEntity_List(false, "", lstRawMaterialOut, out msg);//原材料出库
                    _rawMaterialStockService.SaveEntity_List(true, "", lstRawMaterialStock, out msg);//原材料库存
                    _PMOwnSemiProductOrderService.SaveEntity(keyValue, ownSemiProductOrderEntity);//自制半成品订单

                    ts.Complete();
                }

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion
    }
}

