using ALP.Application.Busines.BaseManage;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.Enum;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Entity.MaterialManage.ViewModel;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.Helper;
using ALP.Application.Service.Material;
using ALP.Application.Service.MaterialManage;
using ALP.Application.Service.Resources;
using ALP.Application.UtilExtend.Util;
using ALP.Application.WebApi.Common;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 物料业务
    /// </summary>
    [RoutePrefix("Material")]
    public class MaterialController : ApiBaseController
    {
        DataItemDetailBLL _dataItemDetialBLL = new DataItemDetailBLL();//数据字典
        Base_KeyParameterItemBLL _baseKeyParameterItemBLL = new Base_KeyParameterItemBLL();//关键参数列表
        BsModelWithResourceBLL _bsModelWithResourceBLL = new BsModelWithResourceBLL(); //工厂建模
        BsModelResourceExtendInfoBLL _bsModelResourceExtendInfoBLL = new BsModelResourceExtendInfoBLL();//工厂属性
        Base_Material_Service _materialService = new Base_Material_Service();//物料主数据

        PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();//工单工艺路线
        PL_ProcessOfOperationsBLL _plProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();//工单工艺-工序
        PL_ProcessOfOperationsAttrBLL _plProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();//工单工艺-工序-属性

        MM_SemiProductMoveRecordBLL _semiProductMoveRecordBLL = new MM_SemiProductMoveRecordBLL();//半成品移库记录
        MM_ProductMoveRecordBLL _productMoveRecordBLL = new MM_ProductMoveRecordBLL();//成品移库记录
        MM_ProductInBLL _productInBLL = new MM_ProductInBLL();//成品入库记录
        MM_ProductStockBLL _productStockBLL = new MM_ProductStockBLL();//成品库存
        MM_ProductDispatchBillBLL _productDispatchBillBLL = new MM_ProductDispatchBillBLL();//成品发货单
        MM_ProductDispatchItemBLL _productDispatchItemBLL = new MM_ProductDispatchItemBLL();//成品发货明细
        MM_ProductDispatchDetailBLL _productDispatchDetailBLL = new MM_ProductDispatchDetailBLL();//成品发货详情
        MM_ProductOut_Service _productOutService = new MM_ProductOut_Service();//成品出库
        MM_RawMaterialStock_Service _rawMaterialStockService = new MM_RawMaterialStock_Service();//原材料库存
        MM_RawMaterialOut_Service _rawMaterialOutService = new MM_RawMaterialOut_Service();//原材料出库
        MM_RawMaterialIn_Service _rawMaterialInService = new MM_RawMaterialIn_Service();//原材料入库

        MM_ProductRework_Service _productReworkService = new MM_ProductRework_Service();//成品返工单
        MM_ProductReworkDetail_Service _productReworkDetailService = new MM_ProductReworkDetail_Service();//成品返工单明细
        PM_PackingPrintMarkBLL _packingPrintMarkBLL = new PM_PackingPrintMarkBLL();//包装唛头
        MM_ProductReworkBG_Service _productReworkBGService = new MM_ProductReworkBG_Service();//成品返工报工
        PM_TransferCardBLL _transferCardBLL = new PM_TransferCardBLL(); //流转卡
        PM_TransferCardResumeBLL _resumeBLL = new PM_TransferCardResumeBLL();//流转履历
        PM_TeamPersonBLL _teamPersonBLL = new PM_TeamPersonBLL();//生产小组
        PM_TeamPerson_ItemsBLL _teamPersonItemBLL = new PM_TeamPerson_ItemsBLL();//生产小组人员
        PM_ProcessBadItemBLL _pmProcessBadItemBLL = new PM_ProcessBadItemBLL();//工序报工不良项目配置
        PM_BGBadRecordBLL _bgBadRecordBLL = new PM_BGBadRecordBLL();//报工不良信息
        PM_TransferBGPersonRecordBLL _bgPersonRecordBLL = new PM_TransferBGPersonRecordBLL();//报工人员

        PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();

        #region 基础数据

        #endregion

        #region 成品入库
        /// <summary>
        /// 成品入库-唛头码扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductInMarkScan")]
        public HttpResponseMessage ProductInMarkScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var markCode = getValue(jo, "markCode");//唛头码
                if (string.IsNullOrEmpty(markCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductInMarkScan.Tips_1");//AjaxResult(false, "唛头码不能为空");
                }
                var markEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == markCode);
                if (markEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductInMarkScan.Tips_2"); //AjaxResult(false, "当前唛头信息不存在");
                }
                if (markEntity.Status != "1")//1：待入库
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductInMarkScan.Tips_3");// AjaxResult(false, "当前唛头不是待入库状态，无法执行入库操作");
                }
                var productStockEntity = _productStockBLL.Get_ExpressionEntity(t => t.MarkCode == markCode && t.IsEnabled == true);
                if (productStockEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductInMarkScan.Tips_4"); //AjaxResult(false, "没有找到线边库上的唛头信息");
                }

                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == productStockEntity.WhsCode);
                var bsModel2 = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == productStockEntity.LocationCode);
                //推荐库位
                var data1 = _productStockBLL.GetList(t => t.MaterialCode == markEntity.MaterialCode && t.IsEnabled == true);
                //工厂层级模板（1：库位管理）
                var data2 = _bsModelResourceExtendInfoBLL.GetList(t => t.EnabledMark == true && t.FieldCode == "GLFS" && t.FieldValue == "1");
                var query1 = from a in data1
                             join b in data2 on a.LocationCode equals b.ResourceCode
                             select a;
                var productSotckList = query1.ToList();
                List<dynamic> lstLoc = new List<dynamic>();
                foreach (var item in productSotckList.GroupBy(t => t.LocationCode))
                {
                    lstLoc.Add(new { LocationCode = item.Key, PalletQty = item.Count() });
                }

                scanResult.MarkCode = markCode;//唛头码
                scanResult.ProductOrder = markEntity.ProductOrder;//订单号
                scanResult.WorkOrder = markEntity.WorkOrder;//工单号
                scanResult.ContainerNO = markEntity.ContainerNO;//柜号
                scanResult.MaterialCode = markEntity.MaterialCode;//客户型号
                scanResult.CustomerPO = markEntity.CustomerPO;//PO号
                scanResult.SourceWhsCode = productStockEntity?.WhsCode;//源仓库编码
                scanResult.SourceWhsName = bsModel?.ResourceName;//源仓库名称
                scanResult.SourceLocationCode = productStockEntity?.LocationCode;//源库位编码
                scanResult.SourceLocationName = bsModel2?.ResourceName;//源库位名称
                scanResult.lstLoc = lstLoc;//推荐库位

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品入库-库位扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductInLocationScan")]
        public HttpResponseMessage ProductInLocationScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var locationCode = getValue(jo, "locationCode");//库位编码
                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == locationCode);
                var bsModel2 = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == bsModel.ParentResource);

                scanResult.WhsCode = bsModel?.ParentResource;//仓库编码
                scanResult.WhsName = bsModel2?.ResourceName;//仓库名称
                scanResult.LocationCode = locationCode;//库位编码
                scanResult.LocationName = bsModel?.ResourceName;//库位名称

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品入库-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductInSave")]
        public HttpResponseMessage ProductInSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var whsCode = getValue(jo, "whsCode");//入库仓库
                var locationCode = getValue(jo, "locationCode");//入库库位
                var inList = JsonConvert.DeserializeObject<List<MM_ProductInEntity>>(getValue(jo, "inList"));
                if (inList.Count == 0)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductInSave.Tips_1");//AjaxResult(false, "没有要保存的数据");
                }

                #region 1、更新库存的仓库、库位
                var data1 = _productStockBLL.GetList(t => true);
                var query1 = from a in data1
                             join b in inList on a.MarkCode equals b.MarkCode
                             select a;
                var productStockList = query1.ToList();//库存列表

                foreach (var item in productStockList)
                {
                    item.WhsCode = whsCode;
                    item.LocationCode = locationCode;
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 2、生成入库记录
                var factoryCode = productStockList.FirstOrDefault().FactoryCode;
                var factoryName = productStockList.FirstOrDefault().FactoryName;
                foreach (var item in inList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = factoryCode;
                    item.FactoryName = factoryName;
                    item.WhsCode = whsCode;
                    item.LocationCode = locationCode;
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                }

                #endregion

                #region 3、更新唛头状态
                var data2 = _packingPrintMarkBLL.GetList(t => true);
                var query2 = from a in data2
                             join b in inList on a.PackTransferCode equals b.MarkCode
                             select a;
                var markList = query2.ToList();
                foreach (var item in markList)
                {
                    item.Status = "2";//已入库
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                }
                //更新PO号状态
                var woList = markList.Select(t => t.WorkOrder).Distinct().ToList();
                var markCodeList = markList.Select(t => t.PackTransferCode).Distinct().ToList();

                var woQuery = from mark in _packingPrintMarkBLL.GetList(t => woList.Contains(t.WorkOrder) &&
                              t.Status == "1" && !markCodeList.Contains(t.PackTransferCode))
                              join wo in _WorkOrderBLL.Get_ExpressionList(t => t.POStatus == "2" && woList.Contains(t.WorkOrder))
                              on mark.WorkOrder equals wo.WorkOrder
                              select new PL_WorkOrderEntity()
                              {
                                  Id = wo.Id,
                                  POStatus = "3",
                                  ModifyBy = userCode,
                                  ModifyTime = DateTime.Now
                              };

                var woList4 = woQuery.ToList();

                // foreach(var wo in woList)
                //{
                //    var markNoInList = _packingPrintMarkBLL.GetList(t => t.WorkOrder==wo && t.Status=="1" && !markCodeList.Contains(t.PackTransferCode));
                //    if (markCodeList.Count > 0)
                //    {
                //        woList1.Add(wo);
                //    }
                //}
                //var woList2 =_WorkOrderBLL.Get_ExpressionList(t => woList1.Contains(t.WorkOrder)).ToList();
                //foreach(var item in woList2)
                //{
                //    item.POStatus = "3";//po号完全入库
                //    item.ModifyBy = userCode;
                //    item.ModifyTime = DateTime.Now;
                //}

                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productStockBLL.SaveEntity_List(true, userName, productStockList, out msg);//更新库存信息
                    _productInBLL.SaveEntity_List(false, userName, inList, out msg);//生成入库记录
                    _packingPrintMarkBLL.SaveEntity_List(true, userName, markList, out msg);//更新唛头状态
                    if (woList4.Count > 0) _WorkOrderBLL.SaveEntity_List(true, null, woList4, out msg);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品入库-记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductInQuery")]
        public HttpResponseMessage ProductInQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _productInBLL.GetPageDataTableList(null, queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchErrorWithOther", ex.Message);//"查询失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 成品发货
        /// <summary>
        /// 成品发货-唛头码扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchMarkScan")]
        public HttpResponseMessage ProductDispatchMarkScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var markCode = getValue(jo, "markCode");//唛头码
                var dispatchId = getValue(jo, "dispatchId");//发货单Id
                if (string.IsNullOrEmpty(markCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_1");//AjaxResult(false, "唛头码不能为空");
                }
                if (string.IsNullOrEmpty(dispatchId))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_6");//AjaxResult(false, "发货单不能为空");
                }
                var markEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == markCode);
                if (markEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_2");//AjaxResult(false, "当前唛头信息不存在");
                }
                //if (markEntity.Status == "1")//未入库
                //{
                //    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_3");//AjaxResult(false, "当前唛头未入库，无法发货");
                //}
                if (markEntity.Status == "3")//已作废
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_4");//AjaxResult(false, "当前唛头已作废，无法发货");
                }
                if (markEntity.Status == "5")//已发货
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_7");//AjaxResult(false, "当前唛头已发货");
                }
                var dispatchDetialEntity = _productDispatchDetailBLL.Get_ExpressionEntity(t => t.DispatchItemId == dispatchId
                    && t.WorkOrder == markEntity.WorkOrder);//发货详情
                if (dispatchDetialEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchMarkScan.Tips_5");//AjaxResult(false, "当前唛头没有发货单");
                }
                var dispatchItemEntity = _productDispatchItemBLL.Get_ExpressionEntity(t => t.Id == dispatchId);

                scanResult.Id = dispatchItemEntity.Id;
                scanResult.DocNum = dispatchItemEntity.DocNum;//发货单号
                scanResult.CreateTime = dispatchItemEntity.CreateTime;//单据日期
                scanResult.ContainerNO = dispatchItemEntity.ContainerNO;//订单号
                scanResult.CustomerPO = dispatchItemEntity.CustomerPO;//PO号
                scanResult.BoxQty = dispatchItemEntity.BoxQty;//盒数
                scanResult.PalletQty = dispatchItemEntity.PalletQty;//托数

                scanResult.MarkCode = markEntity.PackTransferCode;//码头编码
                scanResult.MarkName = markEntity.Mark;//唛头名称

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品发货-发货详情展示
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchDetailShow")]
        public HttpResponseMessage ProductDispatchDetailShow(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var docNum = getValue(jo, "docNum");//发货单号
                var containerNO = getValue(jo, "containerNO");//柜号
                if (string.IsNullOrEmpty(docNum))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchDetailShow.Tips_1");//AjaxResult(false, "发货单不能为空");
                }
                if (string.IsNullOrEmpty(containerNO))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchDetailShow.Tips_2");//AjaxResult(false, "柜号不能为空");
                }
                var dispatchDetailList = _productDispatchDetailBLL.Get_ExpressionList(t => t.DocNum == docNum && t.ContainerNO == containerNO);
                var data = dispatchDetailList.Select(t =>
                    new
                    {
                        MarkCode = t.MarkCode,//唛头号
                        MaterialCode = t.MaterialCode,//客户型号
                        LocationCode = t.LocationCode//库存位置
                    });

                scanResult.dispatchDetailList = data;//成品发货详情

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品发货-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchSave")]
        public HttpResponseMessage ProductDispatchSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var id = getValue(jo, "id");//柜Id（成品发货明细）
                var carNumber = getValue(jo, "carNumber");//车牌号
                var containerID = getValue(jo, "containerID");//集装箱Id
                var forkliftWorker = getValue(jo, "forkliftWorker");//叉车工
                var woodWorker = getValue(jo, "woodWorker");//木工
                var sealingNo = getValue(jo, "sealingNo");//封箱号
                var remark = getValue(jo, "remark");//备注
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "imgList"));//附件列表

                #region 1、发货（柜）
                var entity = _productDispatchItemBLL.GetEntity(id);
                entity.CarNumber = carNumber;
                entity.ContainerID = containerID;
                entity.ForkliftWorker = forkliftWorker;
                entity.WoodWorker = woodWorker;
                entity.SealingNo = sealingNo;
                entity.Remark = remark;
                entity.Operator = userName;
                entity.SendTime = DateTime.Now;
                entity.Status = "3";//已发货
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;
                entity.PostMark = "X";
                entity.PostDate = DateTime.Now;
                #endregion

                #region 2、扣减成品库存
                //发货单明细
                var dispatchDetailList = _productDispatchDetailBLL.Get_ExpressionList(t => t.DispatchItemId == id).ToList();
                var arrMaterialCode = dispatchDetailList.Select(t => t.MaterialCode).ToArray();
                //成品库存
                var productStockList = _productStockBLL.Get_ExpressionList(t => t.ProductOrder == entity.ProductOrder
                    && t.ContainerNO == entity.ContainerNO && arrMaterialCode.Contains(t.MaterialCode)
                    && t.IsEnabled == true && t.PieceQty > 0).ToList();
                if (productStockList.Count == 0)
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchSave.Tips_1");//AjaxResult(false, "库存不存在");

                var lstProductOut = new List<MM_ProductOutEntity>();//成品出库记录
                var lstMark = new List<PM_PackingPrintMarkEntity>();//更新唛头状态
                foreach (var item in dispatchDetailList)
                {
                    var materialStockList = productStockList.FindAll(t => t.MaterialCode == item.MaterialCode);
                    if (materialStockList == null || materialStockList.Count == 0)
                        return AjaxResultWithParams(false, "MaterialManage.MaterialController.ProductDispatchSave.Tips_2", item.MaterialCode);//AjaxResult(false, "客户型号[" + item.MaterialCode + "]没有库存");

                    if (materialStockList.Sum(t => t.PieceQty) < item.PieceQty)
                    {
                        return AjaxResultWithParams(false, "MaterialManage.MaterialController.ProductDispatchSave.Tips_3", item.MaterialCode);//AjaxResult(false, "客户型号[" + item.MaterialCode + "]库存不足");
                    }
                    else if (materialStockList.Sum(t => t.PieceQty) == item.PieceQty)
                    {
                        materialStockList.ForEach(m =>
                        {
                            //新增出库记录
                            MM_ProductOutEntity productOutEntity = new MM_ProductOutEntity();
                            productOutEntity.Id = Guid.NewGuid().ToString();
                            productOutEntity.DocNum = entity.DocNum;
                            productOutEntity.LineNum = item.LineNum;
                            productOutEntity.FactoryCode = m.FactoryCode;
                            productOutEntity.FactoryName = m.FactoryName;
                            productOutEntity.ProductOrder = m.ProductOrder;
                            productOutEntity.OrderLine = m.LineNum;
                            productOutEntity.ContainerNO = m.ContainerNO;
                            productOutEntity.MaterialCode = m.MaterialCode;
                            productOutEntity.WorkOrder = m.WorkOrder;
                            productOutEntity.CustomerPO = m.CustomerPO;
                            productOutEntity.WhsCode = m.WhsCode;
                            productOutEntity.LocationCode = m.LocationCode;
                            productOutEntity.PieceQty = m.PieceQty;
                            productOutEntity.OutType = "5";
                            productOutEntity.CreateTime = DateTime.Now;
                            productOutEntity.Creator = userCode; ;
                            productOutEntity.BusinessId = entity.Id;
                            lstProductOut.Add(productOutEntity);

                            //扣减库存
                            m.PieceQty = 0;
                            m.ModifyBy = userCode;
                            m.ModifyTime = DateTime.Now;
                        });
                    }
                    else if (materialStockList.Sum(t => t.PieceQty) > item.PieceQty)
                    {
                        decimal CycleQty = 0;
                        foreach (var stockItem in materialStockList)  //按照顺序扣减
                        {
                            var offsetQty = item.PieceQty - CycleQty;
                            if (stockItem.PieceQty < offsetQty)
                            {
                                //新增出库记录
                                MM_ProductOutEntity productOutEntity = new MM_ProductOutEntity();
                                productOutEntity.Id = Guid.NewGuid().ToString();
                                productOutEntity.DocNum = entity.DocNum;
                                productOutEntity.LineNum = item.LineNum;
                                productOutEntity.FactoryCode = stockItem.FactoryCode;
                                productOutEntity.FactoryName = stockItem.FactoryName;
                                productOutEntity.OrderLine = stockItem.LineNum;
                                productOutEntity.ProductOrder = stockItem.ProductOrder;
                                productOutEntity.ContainerNO = stockItem.ContainerNO;
                                productOutEntity.MaterialCode = stockItem.MaterialCode;
                                productOutEntity.WorkOrder = stockItem.WorkOrder;
                                productOutEntity.CustomerPO = stockItem.CustomerPO;
                                productOutEntity.WhsCode = stockItem.WhsCode;
                                productOutEntity.LocationCode = stockItem.LocationCode;
                                productOutEntity.PieceQty = stockItem.PieceQty;
                                productOutEntity.OutType = "5";
                                productOutEntity.CreateTime = DateTime.Now;
                                productOutEntity.Creator = userCode;
                                productOutEntity.BusinessId = entity.Id;
                                lstProductOut.Add(productOutEntity);

                                CycleQty += stockItem.PieceQty.Value;//累计出库
                                stockItem.PieceQty = 0;
                            }
                            else if (stockItem.PieceQty >= offsetQty)
                            {
                                //新增出库记录
                                MM_ProductOutEntity productOutEntity = new MM_ProductOutEntity();
                                productOutEntity.Id = Guid.NewGuid().ToString();
                                productOutEntity.DocNum = entity.DocNum;
                                productOutEntity.LineNum = item.LineNum;
                                productOutEntity.FactoryCode = stockItem.FactoryCode;
                                productOutEntity.FactoryName = stockItem.FactoryName;
                                productOutEntity.ProductOrder = stockItem.ProductOrder;
                                productOutEntity.OrderLine = stockItem.LineNum;
                                productOutEntity.ContainerNO = stockItem.ContainerNO;
                                productOutEntity.MaterialCode = stockItem.MaterialCode;
                                productOutEntity.WorkOrder = stockItem.WorkOrder;
                                productOutEntity.CustomerPO = stockItem.CustomerPO;
                                productOutEntity.WhsCode = stockItem.WhsCode;
                                productOutEntity.LocationCode = stockItem.LocationCode;
                                productOutEntity.PieceQty = offsetQty;
                                productOutEntity.OutType = "5";
                                productOutEntity.CreateTime = DateTime.Now;
                                productOutEntity.Creator = userCode;
                                productOutEntity.BusinessId = entity.Id;
                                lstProductOut.Add(productOutEntity);

                                stockItem.PieceQty = stockItem.PieceQty - offsetQty;
                                break;
                            }
                        }
                    }

                    #region 更新唛头状态
                    var workOrderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == item.WorkOrder);
                    if (workOrderEntity != null)
                    {
                        var markList = _packingPrintMarkBLL.Get_ExpressionList(t => t.WorkOrder == workOrderEntity.WorkOrder && t.Status == "6");
                        foreach (var mark in markList)
                        {
                            mark.Status = "5";
                            mark.ModifyBy = CurrentAccount.UserCode;
                            mark.ModifyTime = DateTime.Now;
                        }
                        lstMark.AddRange(markList);
                    }
                    #endregion
                }

                #endregion

                #region 3、附件信息
                if (imgList.Count > 0)
                {
                    foreach (var item in imgList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.FactoryCode = entity.FactoryCode;
                        item.FactoryName = entity.FactoryName;
                        item.Module = "物料模块";//"物料模块";
                        item.TableName = "MM_ProductDispatchItem";
                        item.ParentId = id;
                        item.Creator = userCode;
                        item.CreateTime = DateTime.Now;
                    }
                }
                #endregion

                ////发货单
                //var isUpdate = false;
                //MM_ProductDispatchBillEntity dispatchBillEntity = null;
                //var itemList = _productDispatchItemBLL.Get_ExpressionList(t => t.DocNum == itemEntity.DocNum && t.ContainerNO != itemEntity.ContainerNO && t.Status == "0");
                //if (itemList == null || itemList.Count() == 0)
                //{
                //    isUpdate = true;
                //    dispatchBillEntity = _productDispatchBillBLL.Get_ExpressionEntity(t => t.DocNum == itemEntity.DocNum);
                //    dispatchBillEntity.Status = "3";//已发货
                //    dispatchBillEntity.ModifyBy = userCode;
                //    dispatchBillEntity.ModifyTime = DateTime.Now;
                //}

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productDispatchItemBLL.SaveEntity(entity.Id, entity, out msg);
                    //if (isUpdate) _productDispatchBillBLL.SaveEntity(dispatchBillEntity.Id, dispatchBillEntity, out msg);//更新发货单状态
                    if (imgList.Count > 0) new Base_Images_Service().SaveEntity_List(false, userCode, imgList, out msg);//新增附件
                    _productStockBLL.SaveEntity_List(true, userCode, productStockList, out msg);//扣减库存

                    if (lstProductOut.Count > 0)
                        _productOutService.SaveEntity_List(false, userCode, lstProductOut, out msg);

                    if (lstMark.Count > 0)
                        _packingPrintMarkBLL.SaveEntity_List(true, "", lstMark, out msg);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品发货-记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchQuery")]
        public HttpResponseMessage ProductDispatchQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var docNum = getValue(jo, "docNum");//发货单号
                if (string.IsNullOrEmpty(docNum))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchQuery.Tips_1");//AjaxResult(false, "请输入发货单号");
                }
                List<dynamic> data = new List<dynamic>();
                var itemList = _productDispatchItemBLL.Get_ExpressionList(t => t.DocNum == docNum);
                var detailList = _productDispatchDetailBLL.Get_ExpressionList(t => t.DocNum == docNum);

                foreach (var item in itemList)
                {
                    var detail = detailList.ToList().FindAll(t => t.ContainerNO == item.ContainerNO);
                    data.Add(new { item = item, detail = detail });
                }
                result.resultData = data;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchErrorWithOther", ex.Message);// "查询失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品发货-获取订单号
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchProductOrder")]
        public HttpResponseMessage ProductDispatchProductOrder(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();
                var key = getValue(jo, "key");//订单号

                var msg = "";
                scanResult = _productDispatchItemBLL.Get_ExpressionList(t =>
                    t.Status == "1" && (string.IsNullOrEmpty(key) || t.ProductOrder.Contains(key)))
                    .Select(t => t.ProductOrder)
                    .Distinct().
                    Select(t => new { value = t, label = t });
                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品发货-获取柜号
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchContainerNO")]
        public HttpResponseMessage ProductDispatchContainerNO(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var productOrder = getValue(jo, "productOrder");//订单号
                var containerNo = getValue(jo, "containerNo");//柜号

                //柜号列表
                var containerNOList = _productDispatchItemBLL.Get_ExpressionList(t =>
                    t.ProductOrder == productOrder && (string.IsNullOrEmpty(containerNo) || t.ContainerNO.Contains(containerNo)) && t.Status == "1")
                    .Select(t => t.ContainerNO)
                    .Distinct()
                    .OrderBy(t => Convert.ToInt32(t))
                    .Select(t => new { value = t, label = t });
                scanResult = containerNOList;
                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品发货-获取发货单详情
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchInfo")]
        public HttpResponseMessage ProductDispatchInfo(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var productOrder = getValue(jo, "productOrder");//订单号
                var containerNo = getValue(jo, "containerNo");//柜号

                var dispatchEntity = _productDispatchItemBLL.Get_ExpressionEntity(t =>
                    t.ProductOrder == productOrder && t.ContainerNO == containerNo && t.Status == "1");
                if (dispatchEntity == null)
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductDispatchInfo.Tips_1");//AjaxResult(false, "发货单不存在");
                var dispatchDetailList = _productDispatchDetailBLL.Get_ExpressionList(t => t.DispatchItemId == dispatchEntity.Id);

                scanResult.DispatchEntity = dispatchEntity;
                scanResult.DispatchDetailList = dispatchDetailList;
                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 成品发货-唛头标记
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatchMarkBook")]
        public HttpResponseMessage ProductDispatchMarkBook(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var arrMarkCode = JsonConvert.DeserializeObject<List<string>>(getValue(jo, "arrMarkCode"));
                var markList = _packingPrintMarkBLL.Get_ExpressionList(t => arrMarkCode.Contains(t.PackTransferCode)).ToList();

                foreach (var item in markList)
                {
                    item.Status = "7";
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }


                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    if (markList.Count > 0)
                        _packingPrintMarkBLL.SaveEntity_List(true, "", markList, out msg);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.Error") + ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region 半成品移库
        /// <summary>
        /// 半成品移库-流转卡扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SemiProductMoveCardScan")]
        public HttpResponseMessage SemiProductMoveCardScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                string cardCode = getValue(jo, "cardCode");
                if (string.IsNullOrEmpty(cardCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveCardScan.Tips_1");//AjaxResult(false, "流转卡编码不能为空");
                }
                var cardEntity = _transferCardBLL.Get_ExpressionEntity(t => t.CardCode == cardCode && t.IsEnabled == true);
                if (cardEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveCardScan.Tips_2");//AjaxResult(false, "当前流转卡不存在");
                }
                if (cardEntity.CardStatus == "3")//报废
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveCardScan.Tips_3");//AjaxResult(false, "当前流转卡已报废");
                }
                var resumeEntity = _resumeBLL.Get_ExpressionEntity(t => t.CardCode == cardCode && t.Flag == "1");//流转履历
                if (resumeEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveCardScan.Tips_4");//AjaxResult(false, "当前流转卡没有库存");
                }
                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == resumeEntity.WhsCode);//工厂建模
                //推荐库位
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("ProductOrder", cardEntity.ProductOrder);
                dic.Add("ContainerNO", cardEntity.ContainerNO);
                dic.Add("MaterialCode", cardEntity.MaterialCode);
                dic.Add("WhsCode", resumeEntity.WhsCode);
                dic.Add("Flag", "1");
                var resumeList = _resumeBLL.GetList(dic);
                List<dynamic> lstLoc = new List<dynamic>();
                foreach (var item in resumeList.GroupBy(t => t.LocationCode))
                {
                    lstLoc.Add(new
                    {
                        LocationCode = item.Key,
                        PalletQty = item.Count()
                    });
                }

                scanResult.CardCode = cardEntity.CardCode;//流转卡编码
                scanResult.CardName = cardEntity.CardName;//流转卡名称
                scanResult.ProductOrder = cardEntity.ProductOrder;//订单号
                scanResult.ContainerNO = cardEntity.ContainerNO;//柜号
                scanResult.MaterialCode = cardEntity.MaterialCode;//客户型号
                scanResult.SourceWhsCode = resumeEntity.WhsCode;//仓库编码
                scanResult.SourceWhsName = bsModel?.ResourceName;//仓库名称
                scanResult.SourceLocationCode = resumeEntity.LocationCode;//库位编码
                scanResult.lstLoc = lstLoc;//推荐库位

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 半成品移库-库位扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SemiProductMoveLocationScan")]
        public HttpResponseMessage SemiProductMoveLocationScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var locationCode = getValue(jo, "locationCode");//库位编码
                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == locationCode);
                var bsModel2 = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == bsModel.ParentResource);

                scanResult.WhsCode = bsModel?.ParentResource;//仓库编码
                scanResult.WhsName = bsModel2?.ResourceName;//仓库名称
                scanResult.LocationCode = locationCode;//库位编码
                scanResult.LocationName = bsModel?.ResourceName;//库位名称

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 半成品移库-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SemiProductMoveSave")]
        public HttpResponseMessage SemiProductMoveSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                //流转履历
                List<PM_TransferCardResumeEntity> resumeAddList = new List<PM_TransferCardResumeEntity>();
                List<PM_TransferCardResumeEntity> resumeUpdateList = new List<PM_TransferCardResumeEntity>();

                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var whsCode = getValue(jo, "whsCode");//目标仓库
                var locationCode = getValue(jo, "locationCode");//目标库位
                var moveList = JsonConvert.DeserializeObject<List<MM_SemiProductMoveRecordEntity>>(getValue(jo, "moveList"));
                if (moveList.Count == 0)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveSave.Tips_1");//AjaxResult(false, "没有要保存的数据");
                }
                if (moveList.GroupBy(t => t.WhsCode).Count() > 1)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveSave.Tips_1");//AjaxResult(false, "源仓库有多个，无法移库");
                }
                var sourceWhsCode = moveList.First().WhsCode;
                if (sourceWhsCode != whsCode)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.SemiProductMoveSave.Tips_1");//AjaxResult(false, "源仓库和目标仓库不一致，无法移库");
                }

                //流转履历记录
                var data1 = _resumeBLL.GetList(t => t.Flag == "1");
                var query1 = from a in data1
                             join b in moveList on a.CardCode equals b.CardCode
                             select a;
                resumeUpdateList = query1.ToList();
                string factoryCode = resumeUpdateList.FirstOrDefault()?.FactoryCode;
                string factoryName = resumeUpdateList.FirstOrDefault()?.FactoryName;

                foreach (var item in resumeUpdateList)
                {
                    //源库位信息
                    item.Flag = "0";//标识
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;

                    //目标库位信息
                    var newResumeEntity = Tools.Clone(item);
                    newResumeEntity.Id = Guid.NewGuid().ToString();
                    newResumeEntity.FactoryCode = factoryCode;
                    newResumeEntity.FactoryName = factoryName;
                    newResumeEntity.BusinessType = "6";//移库
                    newResumeEntity.WhsCode = item.WhsCode;
                    newResumeEntity.LocationCode = locationCode;
                    newResumeEntity.Flag = "1";
                    newResumeEntity.Creator = userCode;
                    newResumeEntity.CreateTime = DateTime.Now;
                    newResumeEntity.IsEnabled = true;
                    newResumeEntity.SheetQty = item.SheetQty;
                    newResumeEntity.PieceQty = item.PieceQty;
                    resumeAddList.Add(newResumeEntity);
                }

                foreach (var item in moveList)
                {
                    //移库记录赋值
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = factoryCode;
                    item.FactoryName = factoryName;
                    item.InLocationCode = locationCode;
                    item.Operator = userName;
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                }

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _semiProductMoveRecordBLL.SaveEntity_List(false, userName, moveList, out msg);
                    _resumeBLL.SaveEntity_List(false, userName, resumeAddList, out msg);
                    _resumeBLL.SaveEntity_List(true, userName, resumeUpdateList, out msg);

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 半成品移库-记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SemiProductMoveQuery")]
        public HttpResponseMessage SemiProductMoveQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _semiProductMoveRecordBLL.GetPageDataTableList(null, queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchErrorWithOther", ex.Message);//"查询失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 成品移库
        /// <summary>
        /// 成品移库-唛头码扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductMoveMarkScan")]
        public HttpResponseMessage ProductMoveMarkScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                string markCode = getValue(jo, "markCode");//唛头码
                if (string.IsNullOrEmpty(markCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveMarkScan.Tips_1");//AjaxResult(false, "唛头码不能为空");
                }
                var markEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == markCode);
                if (markEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveMarkScan.Tips_2");//AjaxResult(false, "当前唛头信息不存在");
                }
                if (markEntity.Status == "1")//已入库
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveMarkScan.Tips_3");//AjaxResult(false, "当前唛头未入库");
                }
                else if (markEntity.Status == "3")//已作废
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveMarkScan.Tips_4");//AjaxResult(false, "当前唛头已作废");
                }
                var productStockEntity = _productStockBLL.Get_ExpressionEntity(t => t.MarkCode == markCode && t.IsEnabled == true);
                if (productStockEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveMarkScan.Tips_5");//AjaxResult(false, "当前唛头没有库存");
                }
                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == productStockEntity.WhsCode);//工厂建模
                //推荐库位
                var data1 = _productStockBLL.GetList(t =>
                    t.ProductOrder == productStockEntity.ProductOrder
                    && t.ContainerNO == productStockEntity.ContainerNO
                    && t.MaterialCode == markEntity.MaterialCode
                    && t.WhsCode == productStockEntity.WhsCode
                    && t.IsEnabled == true);
                //工厂层级模板（1：库位管理）
                var data2 = _bsModelResourceExtendInfoBLL.GetList(t => t.EnabledMark == true && t.FieldCode == "GLFS" && t.FieldValue == "1");
                var query1 = from a in data1
                             join b in data2 on a.LocationCode equals b.ResourceCode
                             select a;
                var productSotckList = query1.ToList();
                List<dynamic> lstLoc = new List<dynamic>();
                foreach (var item in productSotckList.GroupBy(t => t.LocationCode))
                {
                    lstLoc.Add(new { LocationCode = item.Key, PalletQty = item.Count() });
                }

                scanResult.MarkCode = markCode; //唛头码
                scanResult.ProductOrder = productStockEntity.ProductOrder;//订单号
                scanResult.ContainerNO = productStockEntity.ContainerNO;//柜号
                scanResult.MaterialCode = productStockEntity.MaterialCode;//客户型号
                scanResult.SourceWhsCode = productStockEntity.WhsCode;//仓库编码
                scanResult.SourceWhsName = bsModel?.ResourceName;//仓库名称
                scanResult.SourceLocationCode = productStockEntity.LocationCode;//库位编码
                scanResult.lstLoc = lstLoc;//推荐库位

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品移库-库位扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductMoveLocationScan")]
        public HttpResponseMessage ProductMoveLocationScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var locationCode = getValue(jo, "locationCode");//库位编码
                var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == locationCode);
                var bsModel2 = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == bsModel.ParentResource);

                scanResult.WhsCode = bsModel?.ParentResource;//仓库编码
                scanResult.WhsName = bsModel2?.ResourceName;//仓库名称
                scanResult.LocationCode = locationCode;//库位编码
                scanResult.LocationName = bsModel?.ResourceName;//库位名称

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品移库-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductMoveSave")]
        public HttpResponseMessage ProductMoveSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var whsCode = getValue(jo, "whsCode");//目标仓库
                var locationCode = getValue(jo, "locationCode");//目标库位
                var moveList = JsonConvert.DeserializeObject<List<MM_ProductMoveRecordEntity>>(getValue(jo, "moveList"));
                if (moveList.Count == 0)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveSave.Tips_1");//AjaxResult(false, "没有要保存的数据");
                }
                if (moveList.GroupBy(t => t.WhsCode).Count() > 1)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveSave.Tips_2");//AjaxResult(false, "源仓库有多个，无法移库");
                }
                var sourceWhsCode = moveList.First().WhsCode;
                if (sourceWhsCode != whsCode)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductMoveSave.Tips_3");//AjaxResult(false, "源仓库和目标仓库不一致，无法移库");
                }

                #region 2、更新库存的库位
                var data1 = _productStockBLL.GetList(t => true);
                var query1 = from a in data1
                             join b in moveList on a.MarkCode equals b.MarkCode
                             select a;
                var productStockList = query1.ToList();
                foreach (var item in productStockList)
                {
                    item.LocationCode = locationCode;
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 移库记录
                string factoryCode = productStockList.First().FactoryCode;
                string factoryName = productStockList.First().FactoryName;
                foreach (var item in moveList)
                {
                    //移库记录赋值
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = factoryCode;
                    item.FactoryName = factoryName;
                    item.InLocationCode = locationCode;
                    item.Operator = userName;
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                }
                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productMoveRecordBLL.SaveEntity_List(false, userName, moveList, out msg);//成品移库记录
                    _productStockBLL.SaveEntity_List(true, userName, productStockList, out msg);//更新库存信息

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品移库-记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductMoveQuery")]
        public HttpResponseMessage ProductMoveQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _productMoveRecordBLL.GetPageDataTableList(null, queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchErrorWithOther", ex.Message);//"查询失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 唛头更换
        /// <summary>
        /// 唛头更换-唛头码扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MarkChangeMarkScan")]
        public HttpResponseMessage MarkChangeMarkScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                string markCode = getValue(jo, "markCode");//唛头码
                if (string.IsNullOrEmpty(markCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.MarkChangeMarkScan.Tips_1");//AjaxResult(false, "唛头码不能为空");
                }
                var markEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == markCode);
                if (markEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.MarkChangeMarkScan.Tips_2");//AjaxResult(false, "当前唛头信息不存在");
                }
                var markList = _packingPrintMarkBLL.Get_ExpressionList(t => t.WorkOrder == markEntity.WorkOrder);
                var dataItemList = _dataItemDetialBLL.GetDataItemList("WorkOrderType").ToList();

                scanResult.MarkCode = markCode; //唛头码
                scanResult.ProductOrder = markEntity.ProductOrder;//订单号
                scanResult.ContainerNO = markEntity.ContainerNO;//柜号
                scanResult.CustomerPO = markEntity.CustomerPO;//PO号
                scanResult.MaterialCode = markEntity.MaterialCode;//客户型号
                scanResult.WorkOrderType = dataItemList.Find(t => t.ItemValue == markEntity.WorkOrderType)?.ItemName;//工单类型
                scanResult.TotalPalletQty = markList.Count();//唛头总数
                scanResult.MarkList = markList.OrderBy(t => Convert.ToInt32(t.Mark.Substring(t.Mark.IndexOf('-') + 1)));

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 唛头更换-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MarkChangeSave")]
        public HttpResponseMessage MarkChangeSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var newMarkCode = getValue(jo, "newMarkCode");//新唛头
                var oldMarkCode = getValue(jo, "oldMarkCode");//旧唛头
                var oldMarkList = JsonConvert.DeserializeObject<List<PM_PackingPrintMarkEntity>>(jo["oldMarkList"].ToString());
                if (string.IsNullOrEmpty(newMarkCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.MarkChangeSave.Tips_1");//AjaxResult(false, "新唛头不能为空");
                }
                //if (string.IsNullOrEmpty(oldMarkCode))
                //{
                //    return AjaxResult(false, "旧唛头不能为空");
                //}
                var newMarkEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == newMarkCode);
                //var oldMarkEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == oldMarkCode);

                #region 1、更新唛头记录
                //新唛头关联旧唛头
                List<PM_PackingPrintMarkEntity> markList = new List<PM_PackingPrintMarkEntity>();
                var newMarkList = _packingPrintMarkBLL.Get_ExpressionList(t => t.WorkOrder == newMarkEntity.WorkOrder);
                //var oldMarkList = _packingPrintMarkBLL.Get_ExpressionList(t => t.WorkOrder == oldMarkEntity.WorkOrder);
                if (newMarkList.Count() != oldMarkList.Count)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.MarkChangeSave.Tips_2");//AjaxResult(false, "新旧唛头数量不一致");
                }
                var i = 0;
                foreach (var item in newMarkList)
                {
                    item.SourceMarkCode = oldMarkList.ToList()[i].PackTransferCode;
                    item.Status = "2";//已入库
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                    item.Operator = userName;
                    item.ChangeTime = DateTime.Now;
                    i++;
                }
                markList.AddRange(newMarkList);
                //作废旧唛头
                foreach (var item in oldMarkList)
                {
                    item.Status = "3";//已作废
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                }
                markList.AddRange(oldMarkList);
                #endregion

                #region 2、更新库存的唛头信息
                var data1 = _productStockBLL.GetList(t => true);
                var query1 = from a in data1
                             join b in oldMarkList on a.MarkCode equals b.PackTransferCode
                             select a;
                var productStockList = query1.ToList();
                foreach (var item in productStockList)
                {
                    var markEntity = markList.Find(t => t.SourceMarkCode == item.MarkCode);
                    item.MarkCode = markEntity.PackTransferCode;
                    item.ProductOrder = markEntity.ProductOrder;
                    item.WorkOrder = markEntity.WorkOrder;
                    item.ContainerNO = markEntity.ContainerNO;
                    item.MaterialCode = markEntity.MaterialCode;
                    item.CustomerPO = markEntity.CustomerPO;
                    item.ModifyBy = userCode;
                    item.ModifyTime = DateTime.Now;
                }
                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _packingPrintMarkBLL.SaveEntity_List(true, userName, markList, out msg);//更新唛头信息
                    _productStockBLL.SaveEntity_List(true, userName, productStockList, out msg);//唛头库存信息

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 唛头更换-记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("MarkChangeQuery")]
        public HttpResponseMessage MarkChangeQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var data = _packingPrintMarkBLL.GetList(queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchErrorWithOther", ex.Message);//"查询失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 创建成品返工单
        /// <summary>
        /// 创建成品返工单-唛头码扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReWorkMarkScan")]
        public HttpResponseMessage ProductReWorkMarkScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                string markCode = getValue(jo, "markCode");//唛头码
                if (string.IsNullOrEmpty(markCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReWorkMarkScan.Tips_1");//AjaxResult(false, "唛头码不能为空");
                }
                var markEntity = _packingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == markCode);
                if (markEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReWorkMarkScan.Tips_2");//AjaxResult(false, "当前唛头信息不存在");
                }
                var markList = _packingPrintMarkBLL.Get_ExpressionList(t => t.WorkOrder == markEntity.WorkOrder);
                var productStockList = _productStockBLL.Get_ExpressionList(t => t.WorkOrder == markEntity.WorkOrder);

                var detail = productStockList.Select(t =>
                    new
                    {
                        t.MarkCode,  //唛头码
                        t.MaterialCode,//客户型号
                        t.LocationCode //库存位置
                    });

                scanResult.ProductOrder = markEntity.ProductOrder;//订单号
                scanResult.ContainerNO = markEntity.ContainerNO;//柜号
                scanResult.CustomerPO = markEntity.CustomerPO;//PO号
                scanResult.MaterialCode = markEntity.MaterialCode;//客户型号
                scanResult.TotalPalletQty = markList.Count();//总托数
                scanResult.TotalBoxQty = productStockList.Sum(t => t.BoxQty);//总盒数
                scanResult.detail = detail;//唛头清单

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 创建成品返工单-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReWorkSave")]
        public HttpResponseMessage ProductReWorkSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }

                var productOrder = getValue(jo, "productOrder");//订单号
                var containerNO = getValue(jo, "containerNO");//柜号
                var customerPO = getValue(jo, "customerPO");//PO号
                var materialCode = getValue(jo, "materialCode");//客户型号
                var dutyProcess = getValue(jo, "dutyProcess");//返工责任工序
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var detail = JsonConvert.DeserializeObject<List<MM_ProductReworkDetailEntity>>(getValue(jo, "detail"));//唛头号、仓库编码、库位编码

                #region 创建成品返工单
                var returnNum = "";
                var message = "";
                var selNo = _productReworkService.GetSerialNO("ProductRework", out returnNum, out message);//流水号

                var data1 = _productStockBLL.GetList(t => true);
                var query1 = from a in data1
                             join b in detail on a.MarkCode equals b.MarkCode
                             select a;
                var productStockList = query1.ToList();
                var workOrder = productStockList.First().WorkOrder;
                var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var plOperationList = _plProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == plProcessEntity.Id).OrderBy(t => t.SN).ToList();
                var index = plOperationList.FindIndex(t => t.OperationCode == "FHBZ");
                var processCode = plOperationList[index - 1].OperationCode;//包装的上一道工序

                //主表
                MM_ProductReworkEntity main = new MM_ProductReworkEntity();
                main.Id = Guid.NewGuid().ToString();
                main.FactoryCode = plProcessEntity.FactoryCode;
                main.FactoryName = plProcessEntity.FactoryName;
                main.ReWorkOrder = "CPFG" + DateTime.Now.ToString("yyMMdd") + selNo;
                main.ProductOrder = productOrder;
                main.ContainerNO = containerNO;
                main.CustomerPO = customerPO;
                main.MaterialCode = materialCode;
                main.DutyProcess = dutyProcess;
                main.ReWorkStatus = "1";//未开始
                main.Creator = userCode;
                main.CreateTime = DateTime.Now;
                main.TotalPieceQty = productStockList.Sum(t => t.PieceQty);
                main.WorkOrder = productStockList.First().WorkOrder;
                main.ReworkProcess = processCode;

                //明细表
                detail.ForEach(item =>
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.ReWorkOrderId = main.Id;
                    item.FactoryCode = plProcessEntity.FactoryCode;
                    item.FactoryName = plProcessEntity.FactoryName;
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                });
                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productReworkService.SaveEntity("", main, out msg);//成品返工单
                    _productReworkDetailService.SaveEntity_List(false, userName, detail, out msg);//返工单明细

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 成品返工报工
        /// <summary>
        /// 成品返工报工-返工单查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReworkBGOrderQuery")]
        public HttpResponseMessage ProductReworkBGOrderQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");//查询条件
                var data = _productReworkService.GetPageDataTableList(null, queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品返工报工-返工单选择页面跳转后显示
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReworkBGShow")]
        public HttpResponseMessage ProductReworkBGShow(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var reworkOrder = getValue(jo, "reworkOrder");//返工单号
                var processCode = getValue(jo, "processCode");//工序编码，默认FHBZ,由前端传入
                if (string.IsNullOrEmpty(reworkOrder))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGShow.Tips_1");//AjaxResult(false, "返工单号不能为空");
                }
                var productReworkEntity = _productReworkService.Get_ExpressionEntity(t => t.ReWorkOrder == reworkOrder);
                if (productReworkEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGShow.Tips_2");//AjaxResult(false, "当前返工单不存在");
                }
                //成品返工报工列表
                var productReworkBGList = _productReworkBGService.Get_ExpressionList(t => t.ReworkOrder == reworkOrder);
                //工序不良
                var msg = "";
                var lstEntity = _pmProcessBadItemBLL.GetList(processCode, out msg);
                var batItemList = lstEntity.ToList().OrderBy(t => t.BadItemCode).Select(t => new { value = t.BadItemCode, label = t.BadItemName });

                scanResult.ReWorkOrder = productReworkEntity.ReWorkOrder;//返工单号
                scanResult.ProductOrder = productReworkEntity.ProductOrder;//订单号
                scanResult.ContainerNO = productReworkEntity.ContainerNO;//柜号
                scanResult.CustomerPO = productReworkEntity.CustomerPO;//PO号
                scanResult.TotalPieceQty = productReworkEntity.TotalPieceQty;//总返工片数
                scanResult.ReworkPieceQty = productReworkBGList.Sum(t => t.PieceQty);//已返工片数
                scanResult.batItemList = batItemList;//工序不良项目列表

                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品返工报工-生产小组扫描
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReworkBGPTeamScan")]
        public HttpResponseMessage ProductReworkBGPTeamScan(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                string pTeamCode = getValue(jo, "pTeamCode");
                if (string.IsNullOrEmpty(pTeamCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGPTeamScan.Tips_1");//AjaxResult(false, "生产小组不能为空");
                }
                var pTeamEntity = _teamPersonBLL.Get_ExpressionEntity(t => t.PTeamCode == pTeamCode);
                if (pTeamEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGPTeamScan.Tips_2");//AjaxResult(false, "当前生产小组不存在");
                }
                var msg = "";
                var pTeamPersonList = _teamPersonItemBLL.GetList(pTeamCode, out msg);
                if (pTeamPersonList == null || pTeamPersonList.Count() == 0)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGPTeamScan.Tips_3");//AjaxResult(false, "当前生产小组没有分配人员");
                }

                scanResult = pTeamPersonList.ToList().Select(t => new { UserCode = t.UserCode, UserName = t.UserName });
                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品返工报工-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReworkBGSave")]
        public HttpResponseMessage ProductReworkBGSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var bgID = Guid.NewGuid().ToString();
                var markList = new List<PM_PackingPrintMarkEntity>();//唛头列表
                var productStockList = new List<MM_ProductStockEntity>();//成品库存列表

                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }

                var reworkOrder = getValue(jo, "reworkOrder");//返工单号
                var qty = getValue(jo, "qty");//报工数量
                var badQty = getValue(jo, "badQty");//不良数量
                badQty = string.IsNullOrEmpty(badQty) ? "0" : badQty;
                var pTeamCode = getValue(jo, "pTeamCode");//生产小组编码
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称
                var badItemDetailList = JsonConvert.DeserializeObject<List<PM_BGBadRecordEntity>>(jo["badItemDetailList"].ToString());

                #region 数据校验
                var pTeamEntity = _teamPersonBLL.Get_ExpressionEntity(t => t.PTeamCode == pTeamCode);
                if (pTeamEntity == null)
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.ProductReworkBGSave.Tips_1");//AjaxResult(false, "该生产小组不存在");
                }

                #endregion

                var productReworkEntity = _productReworkService.Get_ExpressionEntity(t => t.ReWorkOrder == reworkOrder);//成品返工单
                var productReworkDetail = _productReworkDetailService.Get_ExpressionList(t => t.ReWorkOrderId == productReworkEntity.Id);//返工唛头明细

                #region 1、生成报工记录
                //var unit = _bsModelResourceExtendInfoBLL.Get_ExpressEntity(t => t.ResourceCode == processCode && t.FieldCode == "DW")?.FieldValue;

                MM_ProductReworkBGEntity productReworkBGEntity = new MM_ProductReworkBGEntity();
                productReworkBGEntity.Id = bgID;
                productReworkBGEntity.FactoryCode = productReworkEntity.FactoryCode;
                productReworkBGEntity.FactoryName = productReworkEntity.FactoryName;
                productReworkBGEntity.ReworkOrder = reworkOrder;
                productReworkBGEntity.ProcessCode = productReworkEntity.ReworkProcess;
                productReworkBGEntity.ProductOrder = productReworkEntity.ProductOrder;
                productReworkBGEntity.ContainerNO = productReworkEntity.ContainerNO;
                productReworkBGEntity.CustomerPO = productReworkEntity.CustomerPO;
                productReworkBGEntity.MaterialCode = productReworkEntity.MaterialCode;
                productReworkBGEntity.PieceQty = qty.ToDecimal();
                //cardBGRecordEntity.Unit = unit;//报工单位统一从工序属性里取值
                productReworkBGEntity.BadQty = badItemDetailList.Sum(t => t.BadQty);
                productReworkBGEntity.Creator = userCode;
                productReworkBGEntity.CreateTime = DateTime.Now;
                #endregion

                #region 2、报工不良信息
                badItemDetailList.ForEach(t =>
                {
                    t.Id = Guid.NewGuid().ToString();
                    t.FactoryCode = productReworkEntity.FactoryCode;
                    t.FactoryName = productReworkEntity.FactoryName;
                    t.Creator = userCode;
                    t.CreateTime = DateTime.Now;
                });
                #endregion

                #region 3、报工生产小组

                var pTeamUserList = _teamPersonItemBLL.Get_ExpressionList(t => t.PTeamCode == pTeamCode);
                var processPostList = _baseKeyParameterItemBLL.Get_ExpressionList(t =>
                    t.IsEnabled == true && t.EnCode == "ProcessPost" && t.ItemCode == pTeamEntity.ProcessCode).ToList();
                var transferBGPersonRecordList = pTeamUserList.Select(t =>
                     new PM_TransferBGPersonRecordEntity()
                     {
                         Id = Guid.NewGuid().ToString(),
                         BGID = bgID,
                         FactoryCode = productReworkEntity.FactoryCode,
                         FactoryName = productReworkEntity.FactoryName,
                         PTeamCode = t.PTeamCode,
                         PTeamName = pTeamEntity.PTeamName,
                         PostCode = t.PostCode,
                         PostName = processPostList.Find(m => m.Col1 == t.PostCode)?.Col2,
                         UserCode = t.UserCode,
                         UserName = t.UserName
                     }).ToList();
                #endregion


                if (productReworkEntity.ReWorkStatus == "1")//未开始
                {
                    #region 4、唛头报废
                    var data1 = _packingPrintMarkBLL.GetList(t => true);
                    var query1 = from a in data1
                                 join b in productReworkDetail on a.PackTransferCode equals b.MarkCode
                                 select a;
                    markList = query1.ToList();
                    markList.ForEach(item =>
                    {
                        item.Status = "3";//已作废
                        item.ModifyBy = userCode;
                        item.ModifyTime = DateTime.Now;
                    });
                    #endregion

                    #region 5、成品库存删除
                    var data2 = _productStockBLL.GetList(t => true);
                    var query2 = from a in data2
                                 join b in productReworkDetail on a.MarkCode equals b.MarkCode
                                 select a;
                    productStockList = query2.ToList();
                    #endregion

                    #region 6、更新返工单状态
                    productReworkEntity.ReWorkStatus = "2";//正在返工
                    productReworkEntity.ModifyBy = userCode;
                    productReworkEntity.ModifyTime = DateTime.Now;
                    #endregion
                }

                #region 7、流转履历
                var workOrder = productReworkEntity.WorkOrder;//工单号
                var processCode = productReworkEntity.ReworkProcess;
                //工艺路线
                var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var plOperationEntity = _plProcessOfOperationsBLL.Get_ExpressionEntity(t => t.ProcessId == plProcessEntity.Id && t.OperationCode == processCode);
                var plAttrList = _plProcessOfOperationsAttrBLL.Get_ExpressionList(t => t.OperationsId == plOperationEntity.Id).ToList();

                var locationCode = plAttrList.Find(t => t.AttrCode == "BGKW")?.AttrValue;//库位
                var whsCode = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == locationCode)?.ParentResource;//仓库

                PM_TransferCardResumeEntity cardResumeEntity = new PM_TransferCardResumeEntity();
                cardResumeEntity.Id = Guid.NewGuid().ToString();
                cardResumeEntity.FactoryCode = productReworkEntity.FactoryCode;
                cardResumeEntity.FactoryName = productReworkEntity.FactoryName;
                cardResumeEntity.CardCode = reworkOrder;//返工单号
                cardResumeEntity.ProcessCode = processCode;
                cardResumeEntity.BusinessType = "5";//成品返工
                cardResumeEntity.OperationId = bgID;
                cardResumeEntity.Flag = "1";
                cardResumeEntity.WhsCode = whsCode;
                cardResumeEntity.LocationCode = locationCode;
                //cardResumeEntity.IsInWHs = "1";
                cardResumeEntity.Creator = userCode;
                cardResumeEntity.CreateTime = DateTime.Now;
                cardResumeEntity.IsEnabled = true;
                #endregion


                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _productReworkBGService.SaveEntity("", productReworkBGEntity, out msg);//成品返工报工记录
                    _bgBadRecordBLL.SaveEntity_List(false, "", badItemDetailList, out msg);//不良记录
                    _bgPersonRecordBLL.SaveEntity_List(false, "", transferBGPersonRecordList, out msg);//生产小组
                    _resumeBLL.SaveEntity("", cardResumeEntity, out msg);//流转履历
                    if (productReworkEntity.ReWorkStatus == "1")
                    {
                        _packingPrintMarkBLL.SaveEntity_List(true, userName, markList, out msg);//唛头作废
                        _productStockBLL.DeleteList(productStockList);//成品库存删除
                        _productReworkService.SaveEntity(productReworkEntity.Id, productReworkEntity, out msg);//更新返工单状态
                    }

                    ts.Complete();
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 成品返工报工-查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductReworkBGQuery")]
        public HttpResponseMessage ProductReworkBGQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");//查询条件
                var data = _productReworkService.GetList(queryJson);
                result.resultData = data;
                result.success = true;
                result.returnMsg = "Common.SearchSuccess";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 原材料调拨
        /// <summary>
        /// 原材料调拨-选择仓库
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RawMaterialTransferStockQuery")]
        public HttpResponseMessage RawMaterialTransferStockQuery(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                dynamic scanResult = new ExpandoObject();

                var factoryCode = getValue(jo, "factoryCode");//工厂编码
                var locationCode = getValue(jo, "locationCode");//库位编码
                var batchNo = getValue(jo, "batchNo");//批次

                if (string.IsNullOrEmpty(factoryCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.RawMaterialAllocation.Tips_1");//请选择工厂
                }
                if (string.IsNullOrEmpty(locationCode))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.RawMaterialAllocation.Tips_2");//请选择库位
                }
                if (string.IsNullOrEmpty(batchNo))
                {
                    return AjaxResult(false, "MaterialManage.MaterialController.RawMaterialAllocation.Tips_3");//请选择批次
                }

                scanResult = _rawMaterialStockService.Get_ExpressionList(t => t.FactoryCode == factoryCode && t.LocationCode == locationCode
                    && t.BatchNo == batchNo).ToList();
                result.resultData = scanResult;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "Common.Error";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 原材料调拨-保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RawMaterialTransferSave")]
        public HttpResponseMessage RawMaterialTransferSave(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                if (jo == null)
                {
                    return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                }
                //业务服务层
                var keyValue = getValue(jo, "KeyValue");
                var entity = JsonConvert.DeserializeObject<dynamic>(getValue(jo, "Entity"));
                var associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");//移库关联

                #region 1、更新原库位库存
                var sourceEntity = _rawMaterialStockService.GetEntity(keyValue);
                sourceEntity.Qty -= Convert.ToDecimal(entity.TargetQty);
                sourceEntity.ModifyBy = CurrentAccount.UserCode;
                sourceEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 2.更新目标库位库存
                MM_RawMaterialStockEntity newEntity = new MM_RawMaterialStockEntity();
                string targetWhsCode = entity.TargetWhsCode;
                string targetLocationCode = entity.TargetLocationCode;
                var targetEntity = _rawMaterialStockService.Get_ExpressionEntity(t => t.WhsCode == targetWhsCode
                    && t.LocationCode == targetLocationCode && t.MaterialCode == sourceEntity.MaterialCode
                    && t.BatchNo == sourceEntity.BatchNo);
                if (targetEntity == null) //不存在新增
                {
                    newEntity = PubFunction.DeepCopyByReflection(sourceEntity);
                    newEntity.Id = Guid.NewGuid().ToString();
                    newEntity.WhsCode = targetWhsCode;
                    newEntity.LocationCode = targetLocationCode;
                    newEntity.Qty = Convert.ToDecimal(entity.TargetQty);
                    newEntity.Creator = CurrentAccount.UserCode;
                    newEntity.CreateTime = DateTime.Now;
                }
                else
                {
                    targetEntity.Qty += Convert.ToDecimal(entity.TargetQty);
                    targetEntity.ModifyBy = CurrentAccount.UserCode;
                    targetEntity.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 3.新增出库记录
                MM_RawMaterialOutEntity outEntity = new MM_RawMaterialOutEntity();
                outEntity.FactoryCode = sourceEntity.FactoryCode;
                outEntity.FactoryName = sourceEntity.FactoryName;
                outEntity.Id = Guid.NewGuid().ToString();
                outEntity.WhsCode = sourceEntity.WhsCode;
                outEntity.LocationCode = sourceEntity.LocationCode;
                outEntity.MaterialCode = sourceEntity.MaterialCode;
                outEntity.MaterialName = sourceEntity.MaterialName;
                outEntity.Spec = sourceEntity.Spec;
                outEntity.SmallClass = sourceEntity.SmallClass;
                outEntity.BatchNo = sourceEntity.BatchNo;
                outEntity.SupplierCode = sourceEntity.SupplierCode;
                outEntity.OutType = "3";//调拨
                outEntity.Qty = entity.TargetQty;
                outEntity.Unit = sourceEntity.Unit;
                outEntity.Creator = CurrentAccount.UserCode;
                outEntity.CreateTime = DateTime.Now;
                outEntity.AssociateNo = associateNo;//关联
                #endregion

                #region 4.新增入库记录
                MM_RawMaterialInEntity inEntity = new MM_RawMaterialInEntity();
                inEntity.Id = Guid.NewGuid().ToString();
                inEntity.FactoryCode = sourceEntity.FactoryCode;
                inEntity.FactoryName = sourceEntity.FactoryName;
                inEntity.MaterialCode = sourceEntity.MaterialCode;
                inEntity.MaterialName = sourceEntity.MaterialName;
                inEntity.Spec = sourceEntity.Spec;
                inEntity.SmallClass = sourceEntity.SmallClass;
                inEntity.Unit = sourceEntity.Unit;
                inEntity.BatchNo = sourceEntity.BatchNo;
                inEntity.SupplierCode = sourceEntity.SupplierCode;
                inEntity.Qty = entity.TargetQty;
                inEntity.InType = "3";//调拨
                inEntity.WhsCode = entity.TargetWhsCode;
                inEntity.LocationCode = entity.TargetLocationCode;
                inEntity.Creator = CurrentAccount.UserCode;
                inEntity.CreateTime = DateTime.Now;
                inEntity.AssociateNo = associateNo;//关联
                #endregion

                #region 同步SAP
                var factoryCode = sourceEntity.FactoryCode;

                var SAPSyncSwitch = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    var postDate = DateTime.Now;
                    //获取物料信息
                    var materialEntity = _materialService.Get_ExpressionEntity(t => t.MaterialCode == outEntity.MaterialCode);

                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = postDate.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    IF102_ITEM IF102_ITEM = new IF102_ITEM();
                    IF102_ITEM.BWART = "311";
                    IF102_ITEM.MATNR = materialEntity.SAPMaterialCode;
                    IF102_ITEM.WERKS = outEntity.FactoryCode ?? "";
                    IF102_ITEM.LGORT = outEntity.LocationCode ?? "";
                    IF102_ITEM.CHARG = outEntity.BatchNo ?? "";
                    IF102_ITEM.ERFMG = outEntity.Qty.ToString() ?? "";
                    IF102_ITEM.ERFME = outEntity.Unit ?? "";
                    IF102_ITEM.UMWRK = inEntity?.FactoryCode ?? "";
                    IF102_ITEM.UMLGO = inEntity?.LocationCode ?? "";
                    IF102_ITEM.UMCHA = inEntity?.BatchNo ?? "";
                    IT_ITEM.Add(IF102_ITEM);
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    outEntity.IsPosted = sapResult.Flag ? "1" : ""; ;
                    outEntity.PostedMsg = sapResult.Msg;
                    outEntity.PostedTime = DateTime.Now;
                    outEntity.PostedUser = "SAP";
                    outEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");

                    inEntity.IsPosted = sapResult.Flag ? "1" : ""; ;
                    inEntity.PostedMsg = sapResult.Msg;
                    inEntity.PostedTime = DateTime.Now;
                    inEntity.PostedUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                    inEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                }
                #endregion

                string msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _rawMaterialStockService.SaveEntity(sourceEntity.Id, sourceEntity, out msg);

                    if (targetEntity == null) _rawMaterialStockService.SaveEntity("", newEntity, out msg);
                    else _rawMaterialStockService.SaveEntity(targetEntity.Id, targetEntity, out msg);

                    _rawMaterialOutService.SaveEntity("", outEntity, out msg);
                    _rawMaterialInService.SaveEntity("", inEntity, out msg);

                    ts.Complete();
                }

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
