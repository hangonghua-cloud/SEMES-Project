using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Code.Model;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Entity.MaterialManage;
using System.Transactions;
using ALP.Application.Service.Resources;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;
using ALP.Application.Busines.Material;
using ALP.Application.Busines.BaseManage;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-01
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ExeWorkOrderWearingLayerController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 执行工单耐磨层发料
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_ExeWorkOrderWearingLayer")]
    public class PL_ExeWorkOrderWearingLayerController : ApiBaseController
    {
        private PL_ExeWorkOrderWearingLayerBLL _ExeWorkOrderWearingLayer = new PL_ExeWorkOrderWearingLayerBLL();
        private PL_PlanStoreIssueBLL _PlanStoreIssueBLL = new PL_PlanStoreIssueBLL();
        private MM_RawMaterialStockBLL _RawMaterialStockBLL = new MM_RawMaterialStockBLL();
        private MM_RawMaterialOutBLL _RawMaterialOutBLL = new MM_RawMaterialOutBLL();
        private MM_RawMaterialInBLL _RawMaterialInBLL = new MM_RawMaterialInBLL();
        private PL_ExeWorkOrderBLL _ExeWorkOrderBLL = new PL_ExeWorkOrderBLL();
        private Base_MaterialBLL _baseMaterialBLL = new Base_MaterialBLL();//物料主数据
        private Base_KeyParameterItemBLL _keyParameterItemBLL = new Base_KeyParameterItemBLL(); //关键参数

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_ExeWorkOrderWearingLayerPageList")]
        public HttpResponseMessage PL_ExeWorkOrderWearingLayerPageList(JObject jo)
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

                var data = _ExeWorkOrderWearingLayer.GetPageList(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述:  订单发料汇总查询
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetOrderListWithPage")]
        public HttpResponseMessage GetOrderListWithPage(JObject jo)
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

                var data = _ExeWorkOrderWearingLayer.GetOrderListWithPage(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_ExeWorkOrderWearingLayerPageDataTableList")]
        public HttpResponseMessage PL_ExeWorkOrderWearingLayerPageDataTableList(JObject jo)
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

                var data = _ExeWorkOrderWearingLayer.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_ExeWorkOrderWearingLayerList")]
        public HttpResponseMessage GetPL_ExeWorkOrderWearingLayerList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _ExeWorkOrderWearingLayer.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_ExeWorkOrderWearingLayer")]
        public HttpResponseMessage SavePL_ExeWorkOrderWearingLayer(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_8");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_9");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }



            try
            {

                PL_ExeWorkOrderWearingLayerEntity entity = JsonConvert.DeserializeObject<PL_ExeWorkOrderWearingLayerEntity>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {

                }

                string msg = "";
                int isok = _ExeWorkOrderWearingLayer.SaveEntity(keyValue, entity, out msg);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）耐磨层发料保存
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_ExeWorkOrderWearingLayer")]
        public HttpResponseMessage SaveBatchPL_ExeWorkOrderWearingLayer(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var time = DateTime.Now;
            var docNum = time.ToString("yyyyMMddHHmmss");
            string msg = "";
            int isok = 1;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //耐磨层发料
                var wllist = JsonConvert.DeserializeObject<List<PL_ExeWorkOrderWearingLayerEntity>>(getValue(jo, "data1"));
                //工单发料
                var silist = JsonConvert.DeserializeObject<List<PL_PlanStoreIssueEntity>>(getValue(jo, "data2"));
                //物料
                var mmlist = JsonConvert.DeserializeObject<List<RawMaterialStockModel>>(getValue(jo, "data3"));
                if (mmlist == null || mmlist.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_12"));//发料信息不能为空，请重新发料

                //扣库存
                var mrlist = new List<MM_RawMaterialStockEntity>();
                //出库记录
                var outList = new List<MM_RawMaterialOutEntity>();
                //入库
                var inList = new List<MM_RawMaterialInEntity>();
                //
                var taginsertList = new List<MM_RawMaterialStockEntity>();
                var tagupdateList = new List<MM_RawMaterialStockEntity>();

                foreach (var item in mmlist)
                {
                    var associateNo = Guid.NewGuid().ToString();//移库关联
                    mrlist.Add(new MM_RawMaterialStockEntity()
                    {
                        Id = item.Id,
                        Qty = item.Qty - item.ActNum,
                        ModifyBy = userCode,
                        ModifyTime = time
                    });
                    outList.Add(new MM_RawMaterialOutEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FactoryCode = item.FactoryCode,
                        FactoryName = item.FactoryName,
                        DocNum = docNum,
                        WhsCode = item.OldWhsCode,
                        Spec = item.Spec,

                        LocationCode = item.OldLocationCode,
                        MaterialCode = item.MaterialCode,
                        MaterialName = item.MaterialName,
                        SupplierCode = item.SupplierCode,
                        BatchNo = item.BatchNo,
                        OutType = "6",//发料
                        Qty = item.ActNum,
                        Unit = item.Unit,
                        Creator = userCode,
                        CreateTime = time,
                        AssociateNo = associateNo
                    });
                    //3.入库记录
                    MM_RawMaterialInEntity inEntity = new MM_RawMaterialInEntity();
                    inEntity.Id = Guid.NewGuid().ToString();
                    inEntity.FactoryCode = item.FactoryCode;
                    inEntity.FactoryName = item.FactoryName;
                    inEntity.MaterialCode = item.MaterialCode;
                    inEntity.MaterialName = item.MaterialName;
                    inEntity.Unit = item.Unit;
                    inEntity.BatchNo = item.BatchNo;
                    inEntity.Spec = item.Spec;
                    inEntity.SmallClass = item.SmallClass;
                    inEntity.SupplierCode = item.SupplierCode;
                    inEntity.Qty = item.ActNum;
                    inEntity.InType = "6";//发料
                    inEntity.WhsCode = item.WhsCode;
                    inEntity.LocationCode = item.LocationCode;//入库库位
                    inEntity.Creator = userCode;
                    inEntity.CreateTime = time;
                    inEntity.AssociateNo = associateNo;//移库关联
                    inList.Add(inEntity);

                    //4.目标库存
                    var targetEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                       && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo);
                    if (targetEntity == null) //不存在新增
                    {
                        if (taginsertList.Count > 0 && taginsertList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                            && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo) != null)//判断insertList是否存在
                        {
                            taginsertList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                                && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                        }
                        else
                        {
                            var newEntity = new MM_RawMaterialStockEntity();
                            newEntity.Id = Guid.NewGuid().ToString();
                            newEntity.FactoryCode = item.FactoryCode;
                            newEntity.FactoryName = item.FactoryName;
                            newEntity.MaterialCode = item.MaterialCode;
                            newEntity.MaterialName = item.MaterialName;
                            newEntity.Spec = item.Spec;
                            newEntity.SmallClass = item.SmallClass;
                            newEntity.BatchNo = item.BatchNo;
                            newEntity.Unit = item.Unit;
                            newEntity.WhsCode = item.WhsCode;
                            newEntity.LocationCode = item.LocationCode;
                            newEntity.SupplierCode = item.SupplierCode;
                            newEntity.Qty = item.ActNum;
                            newEntity.Creator = userCode;
                            newEntity.CreateTime = time;
                            taginsertList.Add(newEntity);
                        }

                    }
                    else
                    {
                        if (tagupdateList.Count > 0 && tagupdateList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                             && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo) != null)//判断updatetList是否存在
                        {
                            tagupdateList.Find(t => t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode
                                && t.MaterialCode == item.MaterialCode && t.BatchNo == item.BatchNo).Qty += item.ActNum;
                        }
                        else
                        {
                            targetEntity.Qty += item.ActNum;
                            targetEntity.ModifyBy = userCode;
                            targetEntity.ModifyTime = time;
                            tagupdateList.Add(targetEntity);
                        }
                    }
                }
                var list = _PlanStoreIssueBLL.GetList(t => true);
                var query = from t in list
                            join s in silist on t.WorkOrder equals s.WorkOrder
                            select new PL_PlanStoreIssueEntity
                            {
                                Id = t.Id,
                                WearLayerStatus = "2",
                                ModifyBy = userCode,
                                ModifyTime = time
                            };
                silist = query.ToList();

                foreach (var item in wllist)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.Creator = CurrentAccount.UserCode;
                    item.CreateTime = DateTime.Now;
                }

                #region 同步SAP
                var factoryCode = outList.First().FactoryCode;

                var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    //获取物料信息
                    var arrMaterialCode = outList.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _baseMaterialBLL.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    foreach (var item in outList)
                    {
                        var rawInEntity = inList.Find(t => t.AssociateNo == item.AssociateNo);

                        IF102_ITEM IF102_ITEM = new IF102_ITEM();
                        IF102_ITEM.BWART = "311";
                        IF102_ITEM.MATNR = materialList.Find(t => t.MaterialCode == item.MaterialCode)?.SAPMaterialCode ?? "";
                        IF102_ITEM.WERKS = item.FactoryCode ?? "";
                        IF102_ITEM.LGORT = item.LocationCode ?? "";
                        IF102_ITEM.CHARG = item.BatchNo ?? "";
                        IF102_ITEM.ERFMG = item.Qty.ToString() ?? "";
                        IF102_ITEM.ERFME = item.Unit ?? "";
                        IF102_ITEM.UMWRK = rawInEntity?.FactoryCode ?? "";
                        IF102_ITEM.UMLGO = rawInEntity?.LocationCode ?? "";
                        IF102_ITEM.UMCHA = rawInEntity?.BatchNo ?? "";
                        IT_ITEM.Add(IF102_ITEM);
                    }
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    foreach (var item in outList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }
                    foreach (var item in inList)
                    {
                        item.IsPosted = sapResult.Flag ? "1" : ""; ;
                        item.PostedMsg = sapResult.Msg;
                        item.PostedTime = DateTime.Now;
                        item.PostedUser = "SAP";
                        item.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    }
                }
                #endregion

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _PlanStoreIssueBLL.SaveEntity_List(true, userCode, silist, out msg);
                    isok = _ExeWorkOrderWearingLayer.SaveEntity_List(false, userCode, wllist, out msg);
                    _RawMaterialStockBLL.SaveEntity_List(true, userCode, mrlist, out msg);
                    _RawMaterialOutBLL.SaveEntity_List(false, userCode, outList, out msg);
                    _RawMaterialInBLL.SaveEntity_List(false, userCode, inList, out msg);
                    if (tagupdateList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(true, userCode, tagupdateList, out msg);
                    if (taginsertList.Count > 0) _RawMaterialStockBLL.SaveEntity_List(false, userCode, taginsertList, out msg);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_ExeWorkOrderWearingLayer")]
        public HttpResponseMessage DeletePL_ExeWorkOrderWearingLayer(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PL_ExeWorkOrderWearingLayerEntity entity = JsonConvert.DeserializeObject<PL_ExeWorkOrderWearingLayerEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_ExeWorkOrderWearingLayerEntity model = _ExeWorkOrderWearingLayer.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _ExeWorkOrderWearingLayer.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_16");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_ExeWorkOrderWearingLayer")]
        public HttpResponseMessage RemovePL_ExeWorkOrderWearingLayer(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PL_ExeWorkOrderWearingLayerEntity entity = JsonConvert.DeserializeObject<PL_ExeWorkOrderWearingLayerEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_ExeWorkOrderWearingLayerEntity model = _ExeWorkOrderWearingLayer.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _ExeWorkOrderWearingLayer.RemoveForm(Id, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_16");//删除操作成功
                else
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_18");//删除操作失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_ExeWorkOrderWearingLayerEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ExeWorkOrderWearingLayer.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_19");//获取详情数据成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_ExeWorkOrderWearingLayerEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ExeWorkOrderWearingLayer.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_19");//获取详情数据成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
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

                var data = _ExeWorkOrderWearingLayer.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
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
                var list = _ExeWorkOrderWearingLayer.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
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
                var list = _ExeWorkOrderWearingLayer.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-01 09:39:20
        /// 任务编号: 执行工单耐磨层发料
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_ExeWorkOrderWearingLayer_export")]
        public HttpResponseMessage PL_ExeWorkOrderWearingLayer_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ExeWorkOrderWearingLayerController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();


                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _ExeWorkOrderWearingLayer.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        #region 耐磨层退库
        [HttpPost]
        [Route("Save_CancellingStocks")]
        public HttpResponseMessage Save_CancellingStocks(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            var userCode = CurrentAccount.UserCode;
            var msg = "";
            try
            {
                var model = JsonConvert.DeserializeObject<RawMaterialStockModel>(getValue(jo, "Entity"));

                //1.发料数量
                var exeWorkOrder = model.ExeWorkOrder;//执行工单号

                var exeEntity = _ExeWorkOrderWearingLayer.Get_ExpressionEntity(t => t.Id == model.Id);

                if (exeEntity.CancellingNum == null) exeEntity.CancellingNum = 0;
                exeEntity.CancellingNum += model.Qty;//退库数量
                exeEntity.ConsumeNum = exeEntity.ActualNum - exeEntity.CancellingNum;
                exeEntity.SuperNum = exeEntity.ConsumeNum - exeEntity.ShouldNum;
                if (exeEntity.SuperNum <= 0 || exeEntity.SuperNum == null) exeEntity.SuperNum = 0;
                exeEntity.ModifyBy = userCode;
                exeEntity.ModifyTime = time;

                var materialEntity = _baseMaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == model.MaterialCode);
                string associateNo = DateTime.Now.ToString("yyyyMMddHHmmss");
                //2.0 出库记录
                var outEntity = new MM_RawMaterialOutEntity()
                {
                    FactoryCode = model.FactoryCode,
                    FactoryName = model.FactoryName,
                    WorkOrder = exeEntity.ExeWorkOrder,
                    DocNum = time.ToString("yyyyMMddHHmmss"),
                    MaterialCode = model.MaterialCode,
                    MaterialName = model.MaterialName,
                    Unit = model.Unit,
                    SupplierCode = model.SupplierCode,
                    SmallClass = materialEntity?.SmallClass,
                    OutType = "8",//退库
                    WhsCode = model.OldWhsCode,
                    BatchNo = model.BatchNo,
                    Qty = model.Qty,
                    LocationCode = model.OldLocationCode,
                    Creator = userCode,
                    CreateTime = time,
                    AssociateNo = associateNo,
                    BusinessId = exeEntity.Id,
                    BusinessTable = " PL_ExeWorkOrderWearingLayer"
                };

                //2.入库记录
                var inEntity = new MM_RawMaterialInEntity()
                {
                    DocNum = time.ToString("yyyyMMddHHmmss"),
                    FactoryCode = model.FactoryCode,
                    FactoryName = model.FactoryName,
                    MaterialCode = model.MaterialCode,
                    MaterialName = model.MaterialName,
                    Unit = model.Unit,
                    SupplierCode = model.SupplierCode,
                    InType = "5",//退库
                    WhsCode = model.WhsCode,
                    BatchNo = model.BatchNo,
                    Qty = model.Qty,
                    LocationCode = model.LocationCode,
                    Creator = userCode,
                    CreateTime = time,
                    BusinessId = exeEntity.Id,
                    BusinessTable = " PL_ExeWorkOrderWearingLayer"
                };

                //3.库存添加
                var mrEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == model.BatchNo && t.MaterialCode == model.MaterialCode
                    && t.IsFrozen == "0" && t.WhsCode == model.WhsCode && t.LocationCode == model.LocationCode);

                if (mrEntity != null && !string.IsNullOrEmpty(mrEntity.Id))
                {
                    mrEntity.Qty = mrEntity.Qty + model.Qty;
                    mrEntity.ModifyBy = userCode;
                    mrEntity.ModifyTime = time;
                }
                else
                {
                    mrEntity = new MM_RawMaterialStockEntity();
                    mrEntity.FactoryCode = model.FactoryCode;
                    mrEntity.FactoryName = model.FactoryName;
                    mrEntity.MaterialCode = model.MaterialCode;
                    mrEntity.MaterialName = model.MaterialName;
                    mrEntity.BatchNo = model.BatchNo;
                    mrEntity.Unit = model.Unit;
                    mrEntity.SupplierCode = model.SupplierCode;
                    mrEntity.WhsCode = model.WhsCode;
                    mrEntity.Qty = model.Qty;
                    mrEntity.LocationCode = model.LocationCode;
                    mrEntity.Creator = userCode;
                    mrEntity.CreateTime = time;
                    mrEntity.IsFrozen = "0";
                }
                //4.线边库库存扣减
                var stockOutEntity = _RawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == model.BatchNo && t.MaterialCode == model.MaterialCode
                    && t.IsFrozen == "0" && t.WhsCode == model.OldWhsCode && t.LocationCode == model.OldLocationCode
                    && t.SupplierCode == model.SupplierCode);
                if (stockOutEntity != null && !string.IsNullOrEmpty(stockOutEntity.Id))
                {
                    stockOutEntity.Qty -= model.Qty;
                    stockOutEntity.ModifyBy = userCode;
                    stockOutEntity.ModifyTime = time;
                }
                else
                    return AjaxResult(false, Language.GetText("PlanManage.PL_PlanStoreIssueController.Tips_11", model.MaterialCode, model.BatchNo, model.OldWhsCode, model.OldLocationCode));//没有库存，物料【{model.MaterialCode}】、批次【{model.BatchNo}】、仓库【{model.OldWhsCode}】、库位【{model.OldLocationCode}】

                PL_PlanStoreIssueEntity plEntity = null;
                if (exeEntity.ActualNum <= 0)
                {
                    plEntity = _PlanStoreIssueBLL.GetEntity(t => t.WorkOrder == model.WorkOrder);
                    plEntity.WearLayerStatus = "1";
                    plEntity.ModifyBy = userCode;
                    plEntity.ModifyTime = time;
                }

                #region 同步SAP
                var factoryCode = exeEntity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    IF102 sapRequest = new IF102();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF102.ToString();

                    sapRequest.RSQ_DATA = new IF102_RSQ_DATA();
                    IF102_HEAD IF102_HEAD = new IF102_HEAD();
                    IF102_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF102_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF102_HEAD;

                    List<IF102_ITEM> IT_ITEM = new List<IF102_ITEM>();
                    IF102_ITEM IF102_ITEM = new IF102_ITEM();
                    IF102_ITEM.BWART = "311";
                    IF102_ITEM.MATNR = materialEntity?.SAPMaterialCode ?? "";
                    IF102_ITEM.WERKS = outEntity.FactoryCode ?? "";
                    IF102_ITEM.LGORT = outEntity.LocationCode ?? "";
                    IF102_ITEM.CHARG = outEntity.BatchNo ?? "";
                    IF102_ITEM.ERFMG = outEntity.Qty.ToString() ?? "";
                    IF102_ITEM.ERFME = outEntity.Unit ?? "";
                    IF102_ITEM.UMWRK = inEntity.FactoryCode ?? "";
                    IF102_ITEM.UMLGO = inEntity.LocationCode ?? "";
                    IF102_ITEM.UMCHA = inEntity.BatchNo ?? "";
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
                    inEntity.PostedUser = "SAP";
                    inEntity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");

                }
                #endregion

                using (TransactionScope ts = new TransactionScope())
                {
                    if (exeEntity.ActualNum <= 0)
                    {
                        _PlanStoreIssueBLL.SaveForm(plEntity.Id, plEntity);
                        _ExeWorkOrderWearingLayer.RemoveForm(exeEntity.Id, userCode);
                    }
                    else
                    {
                        _ExeWorkOrderWearingLayer.SaveEntity(exeEntity.Id, exeEntity, out msg);
                    }
                    _RawMaterialInBLL.SaveEntity(null, inEntity, out msg);
                    _RawMaterialStockBLL.SaveEntity(mrEntity.Id, mrEntity, out msg);
                    _RawMaterialOutBLL.SaveEntity("", outEntity, out msg);
                    _RawMaterialStockBLL.SaveEntity(stockOutEntity.Id, stockOutEntity, out msg);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ExecutionError") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        #endregion

    }
}
