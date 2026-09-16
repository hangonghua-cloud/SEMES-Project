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
using System.Web;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using System.Data;
using System.Data.SqlClient;
using ALP.Application.UtilExtend;
using ALP.Application.Code.Model;
using System.Transactions;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Busines.Material;
using ALP.Application.Entity.Material;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.Material;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.ProduceManage;
using ALP.Application.Service.SAP;
using ALP.Application.Service.Resources;
using ALP.Application.Service.ToSAP;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_WorkOrderController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 生产工单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_WorkOrder")]
    public class PL_WorkOrderController : ApiBaseController
    {
        //基础数据
        private Base_MaterialBLL _bsMaterialBLL = new Base_MaterialBLL();
        private Base_MaterialFactory_Service _baseMaterialFactoryService = new Base_MaterialFactory_Service();
        private Base_MaterialFacetBLL _base_MaterialFacetBLL = new Base_MaterialFacetBLL();
        private BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模
        private BS_ProcessBLL _BSProcessBLL = new BS_ProcessBLL();
        private BS_ProcessOfOperationsBLL _BSProcessOfOperationsBLL = new BS_ProcessOfOperationsBLL();
        private BS_ProcessOfOperationsAttrBLL _BSProcessOfOperationsAttrBLL = new BS_ProcessOfOperationsAttrBLL();
        private BaseSequenceService _baseSequenceService = new BaseSequenceService();//序列号
        private PL_MarkUpload_Service _plMarkUploadService = new PL_MarkUpload_Service();//唛头配置
        //计划
        private PL_ProductionOrderBLL _ProductionOrderBLL = new PL_ProductionOrderBLL();
        private PL_FirstInspectionConfirmBLL _FirstInspectionConfirmBLL = new PL_FirstInspectionConfirmBLL();
        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();
        private PL_BOMBLL _plBomBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _plBomItemsBLL = new PL_BOMItemsBLL();
        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _PLProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private PL_WorkOrder_Service _plWorkOrderService = new PL_WorkOrder_Service();
        private PL_ExeWorkOrderBLL _ExeWorkOrderBLL = new PL_ExeWorkOrderBLL();
        private PL_PlanStoreIssue_Service _planStoreServcie = new PL_PlanStoreIssue_Service();//拆解发料
        //生产
        private PM_TransferCardBLL _TransferCardBLL = new PM_TransferCardBLL();

        private PM_PackingPrintMark_Service _markService = new PM_PackingPrintMark_Service();//唛头
        private PM_PackingBGTransferCard_Service _packingBGService = new PM_PackingBGTransferCard_Service();//包装报工
        private PMOperationPalletNumService _PMOperationPalletNumService = new PMOperationPalletNumService();

        private static string appLogPath = AppDomain.CurrentDomain.BaseDirectory;
        private object _BOMBLL;
        private PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();
        private PL_BOMBLL _PLBOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _PLBOMItemsBLL = new PL_BOMItemsBLL();
        private PL_PrdOrderReqMaterialsBLL _PrdOrderReqMaterialsBLL = new PL_PrdOrderReqMaterialsBLL();
        private BS_BOMBLL _newBOMBLL = new BS_BOMBLL();
        private BS_BOMItemsBLL _BOMItemsBLL = new BS_BOMItemsBLL();
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();

        private PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _plOperationBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _plAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private PL_PlanStoreIssueBLL _planStoreIssueBLL = new PL_PlanStoreIssueBLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();

            result.resultData = Language.GetText("PlanManage.PL_WorkOrderController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_WorkOrderPageList")]
        public HttpResponseMessage PL_WorkOrderPageList(JObject jo)
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

                var data = _plWorkOrderService.GetPageList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_WorkOrderPageDataTableList")]
        public HttpResponseMessage PL_WorkOrderPageDataTableList(JObject jo)
        {

            //CommonLog.WriteInputLog(jo.ToString(), "PL_WorkOrderPageDataTableList");
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


                var data = _plWorkOrderService.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_WorkOrderProcessTable")]
        public HttpResponseMessage PL_WorkOrderProcessTable(JObject jo)
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

                var data = new PL_WorkOrder_Service().PL_WorkOrderProcessTable(pagination, queryJson);
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
        /// 功能描述: 销售订单明细查询
        /// 创　　建: dragon
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetProductOrderPageDataTableList")]
        public HttpResponseMessage GetProductOrderPageDataTableList(JObject jo)
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

                var data = _plWorkOrderService.GetProductOrderPageDataTableList(pagination, queryJson, CurrentAccount.UserCode);
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
        /// 功能描述: 获取工单下的工艺路线
        /// 创　　建: jpf
        /// 创建日期: 2022-12-26 
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPl_ProcessList")]
        public HttpResponseMessage GetPl_ProcessList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {


                string FactoryCode = getValue(jo, "FactoryCode");
                string WorkOrder = getValue(jo, "WorkOrder");
                var data = new PL_Process_Service().GetPl_ProcessList(FactoryCode, WorkOrder);
                result.resultData = data;
                result.resultData = data;
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_WorkOrderList")]
        public HttpResponseMessage GetPL_WorkOrderList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _plWorkOrderService.GetList(checkType, out msg);
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
        /// 功能描述：修改工艺路线
        /// 创建：jpf
        /// 创建日期：2022-11-29
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save_VCworkProcess")]
        public HttpResponseMessage Save_VCworkProcess(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var Resultmsg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }


            if (jo.SelectToken("entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_8");//缺少特征工艺路线数据！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var workOrder = getValue(jo, "WorkOrder");
                var BSProcessList = JsonConvert.DeserializeObject<List<BS_ProcessEntity>>(getValue(jo, "entity"));
                Resultmsg = new PL_WorkOrder_Service().Save_VCworkProcess(userCode, workOrder, BSProcessList);
                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? "操作成功 " : Resultmsg;//操作成功
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
        /// 功能描述：导入VC工单
        /// 创建：jpf
        /// 创建日期：2022-11-17 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_VCWorkOrder")]
        public HttpResponseMessage SavePL_VCWorkOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var Resultmsg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }


            if (jo.SelectToken("data1") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_11");//缺少内销生产工单参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var isAdd = getValue(jo, "isAdd");
                var workOrderList = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "data1"));
                //匹配工厂编码
                var factoryList = _bsModelWithResourceService.GetList(t => t.ModelLeve == "Factory" && t.EnabledMark == true).ToList();
                foreach (var item in workOrderList)
                {
                    item.FactoryCode = factoryList.Find(t => t.ResourceName == item.FactoryName)?.ResourceCode;
                }
                if (workOrderList.Exists(t => string.IsNullOrEmpty(t.FactoryCode)))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_12"));//部分工厂编码没有匹配到

                workOrderList = workOrderList.OrderBy(t => t.ProductOrder).ThenBy(t => t.MMXH).ThenBy(t => t.CustomerPO).ThenBy(t => t.ContainerNO).ToList();
                var group = workOrderList.GroupBy(t => new
                {
                    t.FactoryCode,
                    t.ProductOrder,
                    t.CustomerPO,
                    t.ContainerNO,
                    t.MaterialCode,
                    t.AvoidProduce,
                    t.PackPalletNum,
                    t.MMXH,
                    t.Spec,
                    t.UV,
                    t.KCKX,
                    t.BW
                }).ToList();
                if (group.Count != workOrderList.Count)
                {
                    foreach (var item in group)
                    {
                        if (item.Count() > 1)
                        {
                            Resultmsg += item.Key.MaterialCode + ",";
                        }
                    }
                    result.success = false;
                    result.returnMsg = Resultmsg + Language.GetText("PlanManage.PL_WorkOrderController.Tips_13");//导入数据存在重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //拆分 PackPalletNum 改为按订单号、柜号分组
                //foreach (var item in listOrder.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.CustomerPO, t.ContainerNO }).ToList())
                foreach (var item in workOrderList.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.ContainerNO }).ToList())
                {
                    decimal wholePallet = 0;
                    foreach (var detail in item)
                    {
                        var array = detail.PackPalletNum.Split("-");
                        if (array.Length != 2)
                        {
                            result.success = false;
                            result.returnMsg = Resultmsg + Language.GetText("PlanManage.PL_WorkOrderController.Tips_14");//包装托盘码格式不正确
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }

                        detail.OrderStartPallet = decimal.Parse(array[0]);
                        detail.OrderPallet = decimal.Parse(array[1]);
                        //if (detail.OrderStartPallet != 0)
                        //{
                        //    wholePallet += detail.OrderPallet.Value - detail.OrderStartPallet.Value + 1M;
                        //}
                    }
                    //整柜总托数：下单结束托号的最大值
                    wholePallet = item.Max(t => t.OrderPallet.Value);
                    foreach (var detail in item)
                    {
                        detail.Creator = userCode;
                        detail.OrderWholePallet = wholePallet;
                    }
                }

                Resultmsg = _plWorkOrderService.VCWorkOrderImport(workOrderList, isAdd);

                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? "操作成功" + workOrderList.Count() + Language.GetText("PlanManage.PL_WorkOrderController.Tips_15") : Resultmsg;//条数据;
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
        /// 功能描述: 导入订单明细（工单）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_WorkOrder")]
        public HttpResponseMessage SavePL_WorkOrder(JObject jo)
        {

            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var Resultmsg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }


            if (jo.SelectToken("data1") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_16");//缺少生产工单参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var isAdd = getValue(jo, "isAdd");
                var workOrderList = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "data1"));
                //匹配工厂编码
                var factoryList = _bsModelWithResourceService.GetList(t => t.ModelLeve == "Factory" && t.EnabledMark == true).ToList();
                foreach (var item in workOrderList)
                {
                    item.FactoryCode = factoryList.Find(t => t.ResourceName == item.FactoryName)?.ResourceCode;
                    item.MaterialCode = item.MaterialCode.Trim();
                }
                if (workOrderList.Exists(t => string.IsNullOrEmpty(t.FactoryCode)))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_12"));//部分工厂编码没有匹配到

                workOrderList = workOrderList.OrderBy(t => t.ProductOrder).ThenBy(t => t.CustomerPO).ThenBy(t => t.ContainerNO).ToList();
                var group = workOrderList.GroupBy(t => new
                {
                    t.FactoryCode,
                    t.ProductOrder,
                    t.CustomerPO,
                    t.ContainerNO,
                    t.MaterialCode,
                    t.AvoidProduce,
                    t.PackPalletNum
                }).ToList();
                if (group.Count != workOrderList.Count)
                {
                    foreach (var item in group)
                    {
                        if (item.Count() > 1)
                        {
                            Resultmsg += item.Key.MaterialCode + ",";
                        }
                    }
                    result.success = false;
                    result.returnMsg = Resultmsg + Language.GetText("PlanManage.PL_WorkOrderController.Tips_13");//导入数据存在重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //拆分 PackPalletNum 改为按订单号、柜号分组
                //foreach (var item in listOrder.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.CustomerPO, t.ContainerNO }).ToList())
                foreach (var item in workOrderList.GroupBy(t => new { t.FactoryCode, t.ProductOrder, t.ContainerNO }).ToList())
                {
                    decimal wholePallet = 0;
                    foreach (var detail in item)
                    {
                        var array = detail.PackPalletNum.Split("-");
                        if (array.Length != 2)
                        {
                            result.success = false;
                            result.returnMsg = Resultmsg + Language.GetText("PlanManage.PL_WorkOrderController.Tips_17");//包装托盘码格式不正确！
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        if (decimal.Parse(array[1]) - decimal.Parse(array[0]) < 0)
                            return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_18", detail.MaterialCode, detail.PackPalletNum));//客户型号[{detail.MaterialCode}]托盘编码[{detail.PackPalletNum}]错误，不允许导入！

                        detail.OrderStartPallet = decimal.Parse(array[0]);
                        detail.newOrderPallet = decimal.Parse(array[1]);
                        detail.OrderPallet = decimal.Parse(array[1]);
                        if (detail.OrderPallet != 0)
                        {
                            detail.OrderPallet = decimal.Parse(array[1]) - decimal.Parse(array[0]) + 1;
                        }
                        //else
                        //{
                        //    detail.OrderPallet = decimal.Parse(array[1]);
                        //}
                        //if (detail.OrderStartPallet != 0)
                        //{
                        //    wholePallet += detail.OrderPallet.Value - detail.OrderStartPallet.Value + 1M;
                        //}
                    }
                    //整柜总托数：下单结束托号的最大值
                    wholePallet = item.Max(t => t.newOrderPallet.Value);
                    foreach (var detail in item)
                    {
                        detail.Creator = userCode;
                        detail.OrderWholePallet = wholePallet;
                    }
                }

                Resultmsg = _plWorkOrderService.WorkOrderImport(workOrderList, isAdd);

                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? "操作成功" + workOrderList.Count() + Language.GetText("PlanManage.PL_WorkOrderController.Tips_15") : Resultmsg;//条数据;
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


        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_WorkOrder")]
        public HttpResponseMessage SaveBatchPL_WorkOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("data") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_19");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string keyValue = getValue(jo, "KeyValue");
            string msg = "";
            int isok = 1;
            var time = DateTime.Now;
            try
            {
                var list = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "data"));
                list.ForEach(t =>
                {
                    t.ModifyBy = userCode;
                    t.ModifyTime = time;
                    //isok=_plWorkOrderService.SaveEntity(t.Id, t, out msg);
                });
                if (jo.SelectToken("inspctionData") != null)
                {
                    var inspctionList = JsonConvert.DeserializeObject<List<PL_FirstInspectionConfirmEntity>>(getValue(jo, "inspctionData"));
                    foreach (var item in inspctionList)
                    {
                        _FirstInspectionConfirmBLL.RemoveForm(t => t.ProductOrder == item.ProductOrder &&
                        t.WorkOrder == item.WorkOrder && t.MaterialCode == item.MaterialCode && t.ContainerNO == item.ContainerNO);
                        item.Id = Guid.NewGuid().ToString();
                        item.FirstStatus = "1";//待检验
                        item.Creator = userCode;
                        item.CreateTime = time;
                    }
                    _FirstInspectionConfirmBLL.InsertList(inspctionList);
                }
                isok = _plWorkOrderService.SaveEntity_List(true, userCode, list, out msg);


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
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_WorkOrder")]
        public HttpResponseMessage DeletePL_WorkOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_19");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PL_WorkOrderEntity entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_WorkOrderEntity model = _plWorkOrderService.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                //删除流转卡
                int a = _TransferCardBLL.Delete_SQL(entity.WorkOrder, out msg);
                //删除执行工单
                _ExeWorkOrderBLL.RemoveForm(t => t.WorkOrder == entity.WorkOrder);
                //删除拆解发料
                _planStoreServcie.RemoveForm(t => t.WorkOrder == entity.WorkOrder);
                //删除工单物料
                var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                var plMaterialId = plMaterialEntity?.Id;
                _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == plMaterialId);
                _plMaterialBLL.RemoveForm(t => t.Id == plMaterialId);
                //删除工单工艺路线
                var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == entity.WorkOrder);
                var plProcessId = plProcessEntity?.Id;
                _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == plProcessId);
                _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == plProcessId);
                _PLProcessBLL.RemoveForm(t => t.Id == plProcessId);
                //删除工单BOM
                var plBomEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                var plBomId = plBomEntity?.Id;
                _plBomItemsBLL.RemoveForm(t => t.BOMId == plBomId);
                _plBomBLL.RemoveForm(t => t.Id == plBomId);
                int isok = _plWorkOrderService.DeleteEntity(Id, out msg, null);

                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_23");//删除操作成功
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_WorkOrder")]
        public HttpResponseMessage RemovePL_WorkOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_19");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PL_WorkOrderEntity entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));

                var list = _plWorkOrderService.Get_ExpressionList(t => t.ProductOrder == entity.ProductOrder);

                var workOrder = entity.WorkOrder;
                var productOrder = entity.ProductOrder;
                var bomEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var BOMId = bomEntity?.Id;
                var materialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var materialId = materialEntity?.Id;
                var processEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var processId = processEntity?.Id;


                using (var ts = new TransactionScope())
                {
                    if (list.Count() <= 1)
                    {
                        _ProductionOrderBLL.RemoveForm(t => t.ProductOrder == productOrder);
                    }
                    _plWorkOrderService.RemoveForm(t => t.WorkOrder == workOrder);
                    _plBomBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _plBomItemsBLL.RemoveForm(t => t.BOMId == BOMId);

                    _plMaterialBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == materialId);

                    _PLProcessBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == processId);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == processId);

                    ts.Complete();
                }


                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 销售订单管理批量获取工单的物料属性
        /// 时间： 2023年2月13日
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkListMaterial")]
        public HttpResponseMessage GetWorkListMaterial(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                var workOrderList = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "Entity"));

                foreach (var entity in workOrderList)
                {
                    if (entity.IsVC == false)
                    {
                        #region 工单物料属性
                        var bs_MaterialEntity = _bsMaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode);
                        var baseMaterialFactoryEntity = _baseMaterialFactoryService.Get_ExpressionEntity(t =>
                            t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                        //获取物料主数据属性
                        List<Base_MaterialFacetEntity> bs_MaterialFacetList = _base_MaterialFacetBLL.Get_ExpressionList(t => t.MaterialFactoryId == baseMaterialFactoryEntity.Id).ToList();

                        List<PL_MaterialFacetEntity> PL_MaterialFacetList = new List<PL_MaterialFacetEntity>();

                        var pl_materialentity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                        var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == pl_materialentity.Id);

                        pl_materialentity.Id = Guid.NewGuid().ToString();
                        pl_materialentity.WorkOrder = entity.WorkOrder;
                        pl_materialentity.FactoryCode = entity.FactoryCode;
                        pl_materialentity.FactoryName = entity.FactoryName;
                        pl_materialentity.MaterialCode = bs_MaterialEntity.MaterialCode;
                        pl_materialentity.MaterialName = bs_MaterialEntity.MaterialName;
                        pl_materialentity.Spec = bs_MaterialEntity.Spec;
                        pl_materialentity.MaterialClass = bs_MaterialEntity.MaterialClass;
                        pl_materialentity.SmallClass = bs_MaterialEntity.SmallClass;
                        pl_materialentity.Unit = bs_MaterialEntity.UnitName;
                        pl_materialentity.Creator = userCode;
                        pl_materialentity.CreateTime = DateTime.Now;
                        pl_materialentity.ModifyBy = userCode;
                        pl_materialentity.ModifyTime = DateTime.Now;

                        foreach (var item in bs_MaterialFacetList)
                        {
                            PL_MaterialFacetEntity pL_MaterialFacet = new PL_MaterialFacetEntity();
                            pL_MaterialFacet.Id = Guid.NewGuid().ToString();
                            pL_MaterialFacet.MaterialId = pl_materialentity.Id;
                            pL_MaterialFacet.AttrCode = item.AttrCode;
                            pL_MaterialFacet.AttrType = item.AttrType;
                            pL_MaterialFacet.AttrValue = item.AttrValue;
                            PL_MaterialFacetList.Add(pL_MaterialFacet);
                        }
                        #endregion

                        #region 更新流转卡
                        //如果生成了流转卡一并更新
                        var workOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.IsEnabled == true);
                        var cardList = _TransferCardBLL.Get_ExpressionList(t => t.WorkOrder == pl_materialentity.WorkOrder).ToList();
                        if (cardList.Count > 0)
                        {
                            foreach (var item in cardList)
                            {
                                item.MaterialCode = pl_materialentity.MaterialCode;
                                item.MaterialName = pl_materialentity.MaterialName;
                                item.Spec = pl_materialentity.Spec;
                                item.BJGY = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BJGY")?.AttrValue;
                                item.WorkOrderRemark = workOrderEntity?.Remark;
                                item.MMXH = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "MMXH")?.AttrValue;
                                item.JCGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "JCGG")?.AttrValue;
                                item.BWXH = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BWXH")?.AttrValue;
                                item.DXZH = plMaterialFacetList.ToList().Find(t => t.AttrCode == "DXZH")?.AttrValue.ToDecimalOrNull();
                                item.BZTPSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZSCTPSL")?.AttrValue.ToDecimalOrNull();
                                item.KCKX = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "KCKX")?.AttrValue;
                                item.UV = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "UV")?.AttrValue;
                                item.DJ = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "DJ")?.AttrValue;
                                item.OrderPieces = workOrderEntity.OrderPieces;
                                item.ProductPieces = workOrderEntity.OrderPieces * workOrderEntity.Yield;
                                item.TPGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "TPGG")?.AttrValue;
                                item.OrderPallet = workOrderEntity.OrderPallet;
                                item.PerPallerBox = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZTPSL")?.AttrValue.ToDecimalOrNull();
                                item.BZDHSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimalOrNull();
                                item.Description = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "SMS")?.AttrValue;
                            }
                        }
                        #endregion

                        string msg = "";
                        //using (var ts = new TransactionScope())
                        //{
                        _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == pl_materialentity.Id);
                        _plMaterialBLL.RemoveForm(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                        _plMaterialBLL.SaveEntity("", pl_materialentity, out msg);//新增主表

                        if (PL_MaterialFacetList.Count > 0)//新增明细
                        {
                            int n = _plMaterialFacetBLL.SaveEntity_List(false, "", PL_MaterialFacetList, out msg);
                        }
                        //更新流转卡
                        if (cardList.Count > 0)
                            _TransferCardBLL.SaveEntity_List(true, userName, cardList, out msg);

                        //    ts.Complete();
                        //}
                    }
                    else
                    {
                        var resultmsg = new PL_MaterialFacet_Service().VCGetMaterialFact(entity.WorkOrder, userCode);
                        if (!string.IsNullOrEmpty(resultmsg))
                        {
                            result.success = false;
                            result.returnMsg = resultmsg;
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                    }
                }

                if (workOrderList.First().IsVC == false)
                {
                    var arrWorkOrder = workOrderList.Select(t => t.WorkOrder).Distinct();
                    //所有的工单工艺路线
                    var plProcessList = _PLProcessBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    //新工艺路线(基础数据)
                    var arrProcessCode = plProcessList.Select(t => t.ProcessCode).Distinct().ToArray();
                    var bsprocessList = _BSProcessBLL.Get_ExpressionList(t => arrProcessCode.Contains(t.ProcessCode)).ToList();
                    //获取工艺工序（基础数据）
                    var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => arrProcessCode.Contains(t.ProcessCode)).ToList();
                    //获取子表属性(基础数据)
                    var bsOperationIds = bsOperationList.Select(t => t.Id);
                    var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                    List<PL_ProcessOfOperationsEntity> plOperationList = new List<PL_ProcessOfOperationsEntity>();
                    List<PL_ProcessOfOperationsAttrEntity> plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                    foreach (var item in workOrderList)
                    {
                        #region 更新工单工艺路线
                        //修改工艺 todo
                        var plProcessEntity = plProcessList.Find(t => t.WorkOrder == item.WorkOrder && t.IsDeleted == false);
                        var bsProcessEntity = bsprocessList.Find(t => t.ProcessCode == plProcessEntity.ProcessCode);
                        plProcessEntity.ProcessCode = bsProcessEntity.ProcessCode;
                        plProcessEntity.ModifyBy = userCode;
                        plProcessEntity.ModifyTime = DateTime.Now;
                        plProcessEntity.ProcessName = bsProcessEntity.ProcessName;
                        plProcessEntity.MaterialClass = bsProcessEntity.MaterialClass;
                        plProcessEntity.SmallClass = bsProcessEntity.SmallClass;
                        //工单表
                        item.Process = plProcessEntity.ProcessCode;
                        item.ModifyBy = userCode;
                        item.ModifyTime = DateTime.Now;

                        var perBSOperationList = bsOperationList.FindAll(t => t.ProcessCode == bsProcessEntity.ProcessCode);
                        perBSOperationList.ForEach(item2 =>
                        {
                            //工单-工艺-工序
                            var plOperationEntity = new PL_ProcessOfOperationsEntity();
                            plOperationEntity.Id = Guid.NewGuid().ToString();
                            plOperationEntity.ProcessId = plProcessEntity.Id;
                            plOperationEntity.FactoryCode = plProcessEntity.FactoryCode;
                            plOperationEntity.FactoryName = plProcessEntity.FactoryName;
                            plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                            plOperationEntity.OperationCode = item2.OperationCode;
                            plOperationEntity.OperationName = item2.OperationName;
                            plOperationEntity.SN = item2.SN;
                            plOperationEntity.OutWarehouse = item2.OutWarehouse;
                            plOperationEntity.CuringCycle = item2.CuringCycle;
                            plOperationEntity.Creator = userCode;
                            plOperationEntity.CreateTime = DateTime.Now;
                            plOperationEntity.IsDeleted = false;
                            plOperationList.Add(plOperationEntity);

                            //工单-工艺-属性
                            var bsOperationAttrList = bsAttrList.FindAll(t => t.OperationsId == item2.Id);
                            bsOperationAttrList.ForEach(item3 =>
                            {
                                var plAttrEntity = new PL_ProcessOfOperationsAttrEntity();
                                plAttrEntity.Id = Guid.NewGuid().ToString();
                                plAttrEntity.ProcessId = plProcessEntity.Id;
                                plAttrEntity.OperationsId = plOperationEntity.Id;
                                plAttrEntity.AttrCode = item3.AttrCode;
                                plAttrEntity.AttrName = item3.AttrName;
                                plAttrEntity.AttrType = item3.AttrType;
                                plAttrEntity.AttrTypeName = item3.AttrTypeName;
                                plAttrEntity.SortCode = item3.SortCode;
                                plAttrEntity.AttrValue = item3.AttrValue;
                                plAttrEntity.IsEnabled = true;
                                plAttrEntity.Creator = userCode;
                                plAttrEntity.CreateTime = DateTime.Now;
                                plAttrList.Add(plAttrEntity);
                            });
                        });

                        #endregion
                    }

                    var msg = "";
                    //更新工单工艺路线
                    var arrplProcessId = plProcessList.Select(t => t.Id);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => arrplProcessId.Contains(t.ProcessId));
                    _PLProcessOfOperationsBLL.RemoveForm(t => arrplProcessId.Contains(t.ProcessId));

                    _PLProcessBLL.SaveEntity_List(true, userCode, plProcessList, out msg);
                    _PLProcessOfOperationsBLL.SaveEntity_List(false, userCode, plOperationList, out msg);
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                    //更新工单
                    _plWorkOrderService.SaveEntity_List(true, userCode, workOrderList, out msg);
                }

                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 获取最新物料属性
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaterial")]
        public HttpResponseMessage GetMaterial(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                PL_WorkOrderEntity entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));
                //判断工单是否正常工单与VC工单，正常工单运行之前的逻辑 jpf  add 2022-12-19 
                if (entity.IsVC == false)
                {
                    #region 更新工单物料
                    var bs_MaterialEntity = _bsMaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode);
                    var baseMaterialFactoryEntity = _baseMaterialFactoryService.Get_ExpressionEntity(t =>
                        t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                    //获取物料主数据属性
                    List<Base_MaterialFacetEntity> bs_MaterialFacetList = _base_MaterialFacetBLL.Get_ExpressionList(t => t.MaterialFactoryId == baseMaterialFactoryEntity.Id).ToList();

                    List<PL_MaterialFacetEntity> PL_MaterialFacetList = new List<PL_MaterialFacetEntity>();

                    var pl_materialentity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                    var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == pl_materialentity.Id);

                    pl_materialentity.WorkOrder = entity.WorkOrder;
                    pl_materialentity.FactoryCode = entity.FactoryCode;
                    pl_materialentity.FactoryName = entity.FactoryName;
                    pl_materialentity.MaterialCode = bs_MaterialEntity.MaterialCode;
                    pl_materialentity.MaterialName = bs_MaterialEntity.MaterialName;
                    pl_materialentity.Spec = bs_MaterialEntity.Spec;
                    pl_materialentity.MaterialClass = bs_MaterialEntity.MaterialClass;
                    pl_materialentity.SmallClass = bs_MaterialEntity.SmallClass;
                    pl_materialentity.Unit = bs_MaterialEntity.UnitName;
                    pl_materialentity.Creator = userCode;
                    pl_materialentity.CreateTime = DateTime.Now;
                    pl_materialentity.ModifyBy = userCode;
                    pl_materialentity.ModifyTime = DateTime.Now;

                    foreach (var item in bs_MaterialFacetList)
                    {
                        PL_MaterialFacetEntity pL_MaterialFacet = new PL_MaterialFacetEntity();
                        pL_MaterialFacet.Id = Guid.NewGuid().ToString();
                        pL_MaterialFacet.MaterialId = pl_materialentity.Id;
                        pL_MaterialFacet.AttrCode = item.AttrCode;
                        pL_MaterialFacet.AttrType = item.AttrType;
                        pL_MaterialFacet.AttrValue = item.AttrValue;

                        PL_MaterialFacetList.Add(pL_MaterialFacet);
                    }
                    #endregion

                    #region 更新流转卡
                    var workOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.IsEnabled == true);
                    //如果生成了流转卡一并更新
                    var cardList = _TransferCardBLL.Get_ExpressionList(t => t.WorkOrder == pl_materialentity.WorkOrder).ToList();
                    if (cardList.Count > 0)
                    {
                        foreach (var item in cardList)
                        {
                            item.MaterialCode = pl_materialentity.MaterialCode;
                            item.MaterialName = pl_materialentity.MaterialName;
                            item.Spec = pl_materialentity.Spec;
                            item.BJGY = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BJGY")?.AttrValue;
                            item.WorkOrderRemark = workOrderEntity?.Remark;
                            item.MMXH = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "MMXH")?.AttrValue;
                            item.JCGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "JCGG")?.AttrValue;
                            item.BWXH = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BWXH")?.AttrValue;
                            item.DXZH = plMaterialFacetList.ToList().Find(t => t.AttrCode == "DXZH")?.AttrValue.ToDecimalOrNull();
                            item.BZTPSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZSCTPSL")?.AttrValue.ToDecimalOrNull();
                            item.KCKX = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "KCKX")?.AttrValue;
                            item.UV = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "UV")?.AttrValue;
                            item.DJ = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "DJ")?.AttrValue;
                            item.OrderPieces = workOrderEntity.OrderPieces;
                            item.ProductPieces = workOrderEntity.OrderPieces * workOrderEntity.Yield;
                            item.TPGG = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "TPGG")?.AttrValue;
                            item.OrderPallet = workOrderEntity.OrderPallet;
                            item.PerPallerBox = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZTPSL")?.AttrValue.ToDecimalOrNull();
                            item.BZDHSL = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimalOrNull();
                            item.Description = plMaterialFacetList.FirstOrDefault(t => t.AttrCode == "SMS")?.AttrValue;
                        }
                    }
                    #endregion

                    #region 更新工单工艺路线
                    //工单工艺路线
                    var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);

                    var bsProcessEntity = _BSProcessBLL.Get_ExpressionEntity(t => t.ProcessCode == plProcessEntity.ProcessCode);
                    //获取工艺工序（基础数据）
                    var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessCode == plProcessEntity.ProcessCode).ToList();
                    //获取子表属性(基础数据)
                    var bsOperationIds = bsOperationList.Select(t => t.Id);
                    var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                    List<PL_ProcessOfOperationsEntity> plOperationList = new List<PL_ProcessOfOperationsEntity>();
                    List<PL_ProcessOfOperationsAttrEntity> plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();

                    //修改工艺 todo
                    plProcessEntity.ProcessCode = bsProcessEntity.ProcessCode;
                    plProcessEntity.ModifyBy = userCode;
                    plProcessEntity.ModifyTime = DateTime.Now;
                    plProcessEntity.ProcessName = bsProcessEntity.ProcessName;
                    plProcessEntity.MaterialClass = bsProcessEntity.MaterialClass;
                    plProcessEntity.SmallClass = bsProcessEntity.SmallClass;
                    //工单表
                    entity.Process = plProcessEntity.ProcessCode;
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;

                    bsOperationList.ForEach(item2 =>
                    {
                        //工单-工艺-工序
                        var plOperationEntity = new PL_ProcessOfOperationsEntity();
                        plOperationEntity.Id = Guid.NewGuid().ToString();
                        plOperationEntity.ProcessId = plProcessEntity.Id;
                        plOperationEntity.FactoryCode = plProcessEntity.FactoryCode;
                        plOperationEntity.FactoryName = plProcessEntity.FactoryName;
                        plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                        plOperationEntity.OperationCode = item2.OperationCode;
                        plOperationEntity.OperationName = item2.OperationName;
                        plOperationEntity.SN = item2.SN;
                        plOperationEntity.OutWarehouse = item2.OutWarehouse;
                        plOperationEntity.CuringCycle = item2.CuringCycle;
                        plOperationEntity.Creator = userCode;
                        plOperationEntity.CreateTime = DateTime.Now;
                        plOperationEntity.IsDeleted = false;
                        plOperationList.Add(plOperationEntity);

                        //工单-工艺-属性
                        var bsOperationAttrList = bsAttrList.FindAll(t => t.OperationsId == item2.Id);
                        bsOperationAttrList.ForEach(item3 =>
                        {
                            var plAttrEntity = new PL_ProcessOfOperationsAttrEntity();
                            plAttrEntity.Id = Guid.NewGuid().ToString();
                            plAttrEntity.ProcessId = plProcessEntity.Id;
                            plAttrEntity.OperationsId = plOperationEntity.Id;
                            plAttrEntity.AttrCode = item3.AttrCode;
                            plAttrEntity.AttrName = item3.AttrName;
                            plAttrEntity.AttrType = item3.AttrType;
                            plAttrEntity.AttrTypeName = item3.AttrTypeName;
                            plAttrEntity.SortCode = item3.SortCode;
                            plAttrEntity.AttrValue = item3.AttrValue;
                            plAttrEntity.IsEnabled = true;
                            plAttrEntity.Creator = userCode;
                            plAttrEntity.CreateTime = DateTime.Now;
                            plAttrList.Add(plAttrEntity);
                        });
                    });

                    #endregion

                    string msg = "";
                    using (var ts = new TransactionScope())
                    {
                        _plMaterialBLL.SaveEntity(pl_materialentity.Id, pl_materialentity, out msg);//更新主表

                        _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == pl_materialentity.Id);
                        if (PL_MaterialFacetList.Count > 0)
                        {
                            int n = _plMaterialFacetBLL.SaveEntity_List(false, "", PL_MaterialFacetList, out msg);
                        }
                        if (cardList.Count > 0)
                            _TransferCardBLL.SaveEntity_List(true, userName, cardList, out msg);

                        //更新工单工艺路线
                        _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);
                        _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);

                        _PLProcessBLL.SaveEntity(plProcessEntity.Id, plProcessEntity, out msg);
                        _PLProcessOfOperationsBLL.SaveEntity_List(false, userCode, plOperationList, out msg);
                        _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                        //更新工单
                        _plWorkOrderService.SaveEntity(entity.Id, entity, out msg);

                        ts.Complete();
                    }
                }
                else
                {
                    var resultmsg = new PL_MaterialFacet_Service().VCGetMaterialFact(entity.WorkOrder, userCode);
                    if (!string.IsNullOrEmpty(resultmsg))
                    {
                        result.success = false;
                        result.returnMsg = resultmsg;
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_WorkOrderEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _plWorkOrderService.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_WorkOrderEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _plWorkOrderService.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
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
                var list = _plWorkOrderService.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
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
                var list = _plWorkOrderService.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-27 16:28:27
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_WorkOrder_export")]
        public HttpResponseMessage PL_WorkOrder_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_19");//缺少Entity参数！
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
                var data = _plWorkOrderService.GetList_export(CreatedByCode, out msg);

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
        /// <summary>
        /// 编辑工单  
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_WorkOrderForm")]
        public HttpResponseMessage SavePL_WorkOrderForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var keyValue = getValue(jo, "KeyValue");
            try
            {
                var plOperationList = new List<PL_ProcessOfOperationsEntity>();
                var plAttrList = new List<PL_ProcessOfOperationsAttrEntity>();
                var processId = "";
                var plProcessEntity = new PL_ProcessEntity();
                var markList = new List<PM_PackingPrintMarkEntity>();//已生成唛头
                PL_PlanStoreIssueEntity planStoreEntity = null;
                List<PM_PackingPrintMarkEntity> newMarkList = new List<PM_PackingPrintMarkEntity>();//将要生成唛头

                var flag = false;
                var entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));

                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    entity.IsEnabled = true;
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }
                else
                {
                    var oldWorkOrder = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);

                    string[] arrPackingStatus = new string[] { "2", "3" };
                    if (arrPackingStatus.Contains(oldWorkOrder.PackingStatus))
                        return AjaxResult(false, "工单已包装，无法修改");

                    if (!jo["DXZH"].IsEmpty())
                    {
                        entity.TotalSheets = Math.Ceiling((entity.OrderPieces / decimal.Parse(getValue(jo, "DXZH"))).Value);
                        if (entity.Yield.HasValue && entity.Yield != 0)
                        {
                            entity.ActualSheets = Math.Ceiling(entity.TotalSheets.Value / entity.Yield.Value);
                            planStoreEntity = _planStoreServcie.GetEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                            if (planStoreEntity != null)
                            {
                                planStoreEntity.OrderNum = entity.TotalSheets;
                                planStoreEntity.ProductNum = entity.ActualSheets;
                                planStoreEntity.ModifyBy = userCode;
                                planStoreEntity.ModifyTime = DateTime.Now;
                            }
                        }
                    }
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                    entity.PackPalletNum = entity.OrderStartPallet.ToString() + '-' + (entity.OrderStartPallet + entity.OrderPallet - 1).ToString();
                    var oldEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                    if (oldEntity.WorkOrderType == "1" && entity.OrderPallet != null && entity.OrderPallet != 0)
                    {
                        entity.PerPalletPieceQty = Math.Round(entity.OrderPieces.Value / entity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                        entity.PerPalletBoxQty = Math.Round(entity.OrderBox.Value / entity.OrderPallet.Value, 2, MidpointRounding.AwayFromZero);
                    }
                    if (oldEntity.Process != entity.Process)
                    {
                        //修改工艺 todo
                        plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == entity.WorkOrder);
                        plProcessEntity.ProcessCode = entity.Process;
                        plProcessEntity.ModifyBy = userCode;
                        plProcessEntity.ModifyTime = DateTime.Now;
                        processId = plProcessEntity.Id;
                        var process = entity.Process;
                        var bsprocessEntity = _BSProcessBLL.Get_ExpressionEntity(t => t.ProcessCode == process);
                        plProcessEntity.ProcessName = bsprocessEntity.ProcessName;
                        plProcessEntity.MaterialClass = bsprocessEntity.MaterialClass;
                        plProcessEntity.SmallClass = bsprocessEntity.SmallClass;
                        //获取工艺子表
                        var bsOperationList = _BSProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessCode == process).ToList();
                        var bsOperationEntity = bsOperationList.Find(t => t.OperationCode == entity.StartOperation);
                        if (bsOperationEntity == null)
                        {
                            return AjaxResult(false, $"起始工序【{entity.StartOperation}】在工艺路线里不存在");
                        }
                        bsOperationList = bsOperationList.Where(t => t.SN >= bsOperationEntity.SN).ToList();//只保留起始工序及之后的工序
                                                                                                            //获取子表属性
                        var bsOperationIds = bsOperationList.Select(t => t.Id);
                        var bsAttrList = _BSProcessOfOperationsAttrBLL.Get_ExpressionList(t => bsOperationIds.Contains(t.OperationsId)).ToList();

                        bsOperationList.ForEach(item =>
                        {
                            //工单-工艺-工序
                            var plOperationEntity = new PL_ProcessOfOperationsEntity();
                            plOperationEntity.Id = Guid.NewGuid().ToString();
                            plOperationEntity.ProcessId = processId;
                            plOperationEntity.ProcessCode = plProcessEntity.ProcessCode;
                            plOperationEntity.OperationCode = item.OperationCode;
                            plOperationEntity.OperationName = item.OperationName;
                            plOperationEntity.SN = item.SN;
                            plOperationEntity.OutWarehouse = item.OutWarehouse;
                            plOperationEntity.CuringCycle = item.CuringCycle;
                            plOperationEntity.Creator = userCode;
                            plOperationEntity.CreateTime = DateTime.Now;
                            plOperationList.Add(plOperationEntity);

                            //工单-工艺-属性
                            var bsOperationAttrList = bsAttrList.FindAll(t => t.OperationsId == item.Id);
                            bsOperationAttrList.ForEach(item2 =>
                            {
                                var plAttrEntity = new PL_ProcessOfOperationsAttrEntity();
                                plAttrEntity.Id = Guid.NewGuid().ToString();
                                plAttrEntity.ProcessId = processId;
                                plAttrEntity.OperationsId = plOperationEntity.Id;
                                plAttrEntity.AttrCode = item2.AttrCode;
                                plAttrEntity.AttrName = item2.AttrName;
                                plAttrEntity.AttrType = item2.AttrType;
                                plAttrEntity.AttrTypeName = item2.AttrTypeName;
                                plAttrEntity.SortCode = item2.SortCode;
                                plAttrEntity.AttrValue = item2.AttrValue;
                                plAttrEntity.IsEnabled = true;
                                plAttrEntity.Creator = userCode;
                                plAttrEntity.CreateTime = DateTime.Now;
                                plAttrList.Add(plAttrEntity);
                            });
                        });
                    }
                    //重新生成唛头
                    markList = _markService.Get_ExpressionList(t => t.WorkOrder == oldEntity.WorkOrder).ToList();
                    if (markList.Count > 0 && (oldEntity.OrderWholePallet != entity.OrderWholePallet
                        || oldEntity.OrderStartPallet != entity.OrderStartPallet || oldEntity.OrderPieces != entity.OrderPieces))
                    {
                        flag = true;

                        #region 唛头
                        var time = DateTime.Now;
                        decimal? currentProcessBGQty = 0;//已报工数量
                        var productOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == entity.ProductOrder);
                        var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.FactoryCode == entity.FactoryCode && t.IsDeleted == false);
                        var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id).ToList();

                        //var perPalletPieceQty = plMaterialFacetList.Find(t => t.AttrCode == "CPBZTPSL")?.AttrValue.ToDecimal();//单托片数
                        var perPalletPieceQty = entity.PerPalletPieceQty;//单托片数
                        if (perPalletPieceQty == null || perPalletPieceQty == 0)
                        {
                            return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_28"));//产品包装托盘数量不能为空或者0
                        }
                        //var boxQty = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue;//单托盒数
                        var boxQty = entity.PerPalletBoxQty;//单托盒数
                        var packingBGList = _packingBGService.Get_ExpressionList(t => t.WorkOrder == entity.WorkOrder && t.FactoryCode == entity.FactoryCode);
                        if (packingBGList != null && packingBGList.Count() > 0)
                        {
                            currentProcessBGQty = packingBGList.Sum(t => t.Qty);//当前工序已报工数量
                        }
                        decimal? offsetQty = 0;
                        if (oldEntity.PackingStatus == "2")
                            offsetQty = currentProcessBGQty;
                        else
                            offsetQty = entity.OrderPieces;

                        if (offsetQty >= perPalletPieceQty)
                        {
                            int palletCount = (int)offsetQty / (int)perPalletPieceQty;
                            if (palletCount > 0)
                            {
                                string returnNum = string.Empty;
                                _markService.GetSerialNO("PackingPrintMark", palletCount, out returnNum, out msg);
                                var index = Int32.Parse(returnNum);
                                for (int i = 0; i < palletCount; i++)
                                {
                                    #region 8、唛头信息
                                    var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                    packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                    packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                    packingPrintMarkEntity.FactoryCode = entity.FactoryCode;
                                    packingPrintMarkEntity.FactoryName = entity.FactoryName;
                                    packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMdd") + (index++).ToString().PadLeft(4, '0');
                                    packingPrintMarkEntity.Mark = entity.OrderWholePallet + "-" + (entity.OrderStartPallet + i);
                                    packingPrintMarkEntity.ProductOrder = entity.ProductOrder;
                                    packingPrintMarkEntity.WorkOrder = entity.WorkOrder;
                                    packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                    packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                    packingPrintMarkEntity.ContainerNO = entity.ContainerNO;
                                    packingPrintMarkEntity.CustomerPO = entity.CustomerPO;
                                    //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                    //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";
                                    packingPrintMarkEntity.Quantity = entity.PerPalletBoxQty.ToString().TrimEnd('.', '0') + " ctns";
                                    packingPrintMarkEntity.PrintStatus = "1";//未打印
                                    packingPrintMarkEntity.Creator = userCode;
                                    packingPrintMarkEntity.CreateTime = time;
                                    packingPrintMarkEntity.WorkOrderType = entity.WorkOrderType;
                                    packingPrintMarkEntity.Status = "1";//待入库
                                    packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                    packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                    packingPrintMarkEntity.PieceQty = perPalletPieceQty;
                                    //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                    newMarkList.Add(packingPrintMarkEntity);
                                    #endregion
                                }
                            }
                        }

                        if (offsetQty == entity.OrderPieces)
                        {
                            if (perPalletPieceQty * newMarkList.Count < entity.OrderPieces)
                            {
                                var BZDHSL = plMaterialFacetList.Find(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimal();
                                if (BZDHSL == 0 || BZDHSL == null)
                                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_29"));//包装单盒片数没有值

                                string returnNum = string.Empty;
                                _markService.GetSerialNO("PackingPrintMark", 1, out returnNum, out msg);
                                var index = Int32.Parse(returnNum);
                                #region 8、唛头信息
                                var packingPrintMarkEntity = new PM_PackingPrintMarkEntity();
                                packingPrintMarkEntity.Id = Guid.NewGuid().ToString();
                                packingPrintMarkEntity.PackingRecordId = markList.First().PackingRecordId;
                                packingPrintMarkEntity.FactoryCode = entity.FactoryCode;
                                packingPrintMarkEntity.FactoryName = entity.FactoryName;
                                packingPrintMarkEntity.PackTransferCode = "M" + time.ToString("yyMMdd") + (index++).ToString().PadLeft(4, '0');
                                packingPrintMarkEntity.Mark = entity.OrderWholePallet + "-" + (entity.OrderStartPallet + newMarkList.Count);
                                packingPrintMarkEntity.ProductOrder = entity.ProductOrder;
                                packingPrintMarkEntity.WorkOrder = entity.WorkOrder;
                                packingPrintMarkEntity.Customer = productOrderEntity.Customer;
                                packingPrintMarkEntity.MaterialCode = plMaterialFacetList.Find(t => t.AttrCode == "MTXH")?.AttrValue;//唛头型号
                                packingPrintMarkEntity.ContainerNO = entity.ContainerNO;
                                packingPrintMarkEntity.CustomerPO = entity.CustomerPO;
                                //packingPrintMarkEntity.Spec = plMaterialFacetList.Find(t => t.AttrCode == "Spec")?.AttrValue;
                                //packingPrintMarkEntity.Quantity = plMaterialFacetList.Find(t => t.AttrCode == "BZTPSL")?.AttrValue + " ctns";

                                packingPrintMarkEntity.PrintStatus = "1";//未打印
                                packingPrintMarkEntity.Creator = userCode;
                                packingPrintMarkEntity.CreateTime = time;
                                packingPrintMarkEntity.WorkOrderType = entity.WorkOrderType;
                                packingPrintMarkEntity.Status = "1";//待入库
                                packingPrintMarkEntity.BoxDate = productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A";
                                packingPrintMarkEntity.MMXH = plMaterialFacetList.Find(t => t.AttrCode == "MMXH")?.AttrValue;
                                //packingPrintMarkEntity.MTBT = plMaterialFacetList.Find(t => t.AttrCode == "MTBT")?.AttrValue;//唛头标题
                                packingPrintMarkEntity.PieceQty = entity.OrderPieces - (perPalletPieceQty * newMarkList.Count);
                                packingPrintMarkEntity.Quantity = (Math.Ceiling(packingPrintMarkEntity.PieceQty.Value / BZDHSL.Value)).ToString().TrimEnd('.', '0') + " ctns";
                                newMarkList.Add(packingPrintMarkEntity);
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
                using (var ts = new TransactionScope())
                {
                    _plWorkOrderService.SaveEntity(keyValue, entity, out msg);
                    //删除属性
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == processId);
                    //删除工艺子表
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == processId);
                    _PLProcessBLL.SaveEntity(plProcessEntity.Id, plProcessEntity, out msg);
                    _PLProcessOfOperationsBLL.SaveEntity_List(false, userCode, plOperationList, out msg);
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, userCode, plAttrList, out msg);

                    if (flag)
                        _markService.RemoveForm(t => t.WorkOrder == entity.WorkOrder);

                    if (newMarkList.Count > 0)
                        _markService.SaveEntity_List(false, userName, newMarkList, out msg);

                    if (planStoreEntity != null)
                        _planStoreServcie.SaveForm(planStoreEntity.Id, planStoreEntity);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 工单冻结/解冻  
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("WorkOrderFreezeFlag")]
        public HttpResponseMessage WorkOrderFreezeFlag(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var keyValue = getValue(jo, "KeyValue");
            try
            {

                var entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;
                _plWorkOrderService.SaveEntity(keyValue, entity, out msg);

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        ///  导出补料单EXCel
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PL_BLWorkOrder_export")]
        public HttpResponseMessage PL_BLWorkOrder_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                List<PL_WorkOrderEntity> entity_list = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "Entity"));
                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                var data = _plWorkOrderService.WorkOrder_export(entity_list, out msg);
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


        #region 新增补料拣余单

        /// <summary>
        ///  新增补料拣余单
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_AddWorkOrderForm")]
        public HttpResponseMessage SavePL_AddWorkOrderForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var processRoute = getValue(jo, "ProcessRoute");//工艺路线
            var orderType = getValue(jo, "OrderType");//订单类型
            var oldWorkOrder = getValue(jo, "OldWorkOrder");//旧工单
            var keyValue = getValue(jo, "KeyValue");
            var remark = getValue(jo, "Remark");
            //var ContainerNO = getValue(jo, "ContainerNO");//柜号
            var DXZH = Convert.ToDecimal(getValue(jo, "DXZH"));//大小张转换
            try
            {
                var entity = JsonConvert.DeserializeObject<PL_WorkOrderEntity>(getValue(jo, "Entity"));
                var list = _plWorkOrderService.Get_ExpressionList(t => t.ProductOrder == entity.ProductOrder).Select(t => int.Parse(t.WorkOrder.Substring(t.WorkOrder.Length - 4, 4)));
                var index = list.Max();

                entity.Creator = userCode;
                entity.CreateTime = DateTime.Now;

                var oldWorkOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsEnabled == true);
                if (oldWorkOrderEntity == null)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_30", oldWorkOrder));//原工单{oldWorkOrder}不存在！

                if (oldWorkOrderEntity.OrderStatus == "9")
                {
                    return AjaxResult(false, "原工单已结案，无法创建");
                }

                entity.IsVC = oldWorkOrderEntity.IsVC; //继承原工单
                entity.MMXH = oldWorkOrderEntity.MMXH;
                entity.Spec = oldWorkOrderEntity.Spec;
                entity.UV = oldWorkOrderEntity.UV;
                entity.KCKX = oldWorkOrderEntity.KCKX;
                entity.Create();
                entity.IsEnabled = true;
                entity.Creator = userCode;
                entity.CreateTime = DateTime.Now;
                entity.Remark = remark;
                entity.TransferBy = "1";//默认按柜
                entity.SAP_AUFNR = oldWorkOrderEntity.SAP_AUFNR;

                if (entity.WorkOrderType == "2")//补料单
                {
                    msg = InsertOrder_B(entity, index);
                    if (!string.IsNullOrEmpty(msg))
                        return AjaxResult(false, msg);
                }
                else if (entity.WorkOrderType == "3")//拣余单
                {
                    msg = InsertOrder_J(entity, oldWorkOrder, index, DXZH, remark);
                    if (!string.IsNullOrEmpty(msg))
                        return AjaxResult(false, msg);
                }

                msg = InsertStoreIssueEntity(entity, oldWorkOrder, remark);
                if (!string.IsNullOrEmpty(msg))
                    return AjaxResult(false, msg);

                if (entity.IsVC != true)
                {
                    var plbomEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);
                    _plBomBLL.InsertPLBom(entity.FactoryCode, plbomEntity?.BOMCode, processRoute, orderType, entity.WorkOrder);
                    _plMaterialBLL.InsertPLMaterial(entity.FactoryCode, entity.MaterialCode, processRoute, entity.WorkOrder);
                    _PLProcessBLL.InsertPLProcess(entity.FactoryCode, processRoute, entity.WorkOrder, entity.StartOperation);
                    _plWorkOrderService.SaveEntity(keyValue, entity, out msg);
                }
                else //VC工单
                {
                    #region 新工单物料、属性
                    var oldPLMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);
                    var newPLMaterialEntity = Tools.Clone(oldPLMaterialEntity);
                    newPLMaterialEntity.Id = Guid.NewGuid().ToString();
                    newPLMaterialEntity.WorkOrder = entity.WorkOrder;
                    newPLMaterialEntity.Creator = CurrentAccount.UserCode;
                    newPLMaterialEntity.CreateTime = DateTime.Now;
                    //新工单物料属性
                    var oldPLMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == oldPLMaterialEntity.Id).ToList();
                    if (oldPLMaterialFacetList.Count == 0)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_42", oldWorkOrder));//工单【{workOrder}】物料属性不存在！

                    var newPLMaterialFacetList = Tools.Clone(oldPLMaterialFacetList);
                    foreach (var item in newPLMaterialFacetList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.MaterialId = newPLMaterialEntity.Id;
                    }
                    #endregion

                    #region 新工单工艺路线、工序、属性
                    var oldPLProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);
                    var newPLProcessEntity = Tools.Clone(oldPLProcessEntity);
                    newPLProcessEntity.Id = Guid.NewGuid().ToString();
                    newPLProcessEntity.WorkOrder = entity.WorkOrder;
                    newPLProcessEntity.Creator = CurrentAccount.UserCode;
                    newPLProcessEntity.CreateTime = DateTime.Now;
                    //新工单工艺-工序
                    var oldPLProcessOperationList = _PLProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == oldPLProcessEntity.Id && t.IsDeleted == false).ToList();
                    if (oldPLProcessOperationList.Count == 0)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_43", oldWorkOrder));//工单【{workOrder}】工艺路线-工序不存在

                    var newPLProcessOperationList = Tools.Clone(oldPLProcessOperationList);
                    foreach (var item in newPLProcessOperationList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.ProcessId = newPLProcessEntity.Id;
                        item.Creator = CurrentAccount.UserCode;
                        item.CreateTime = DateTime.Now;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }
                    //新工单工艺-属性
                    var oldPLProcessAttrList = _PLProcessOfOperationsAttrBLL.Get_ExpressionList(t => t.ProcessId == oldPLProcessEntity.Id && t.IsEnabled == true).ToList();
                    if (oldPLProcessAttrList.Count == 0)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_44", oldWorkOrder));//工单【{workOrder}】工艺路线-属性不存在！

                    var newPLProcessAttrList = Tools.Clone(oldPLProcessAttrList);
                    foreach (var item in oldPLProcessOperationList)
                    {
                        var newPLProcessOperationAttrList = newPLProcessAttrList.FindAll(t => t.OperationsId == item.Id);
                        var operatonId = newPLProcessOperationList.Find(t => t.OperationCode == item.OperationCode).Id;
                        foreach (var attr in newPLProcessOperationAttrList)
                        {
                            attr.Id = Guid.NewGuid().ToString();
                            attr.ProcessId = newPLProcessEntity.Id;
                            attr.OperationsId = operatonId;
                            attr.Creator = CurrentAccount.UserCode;
                            attr.CreateTime = DateTime.Now;
                            attr.ModifyBy = CurrentAccount.UserCode;
                            attr.ModifyTime = DateTime.Now;
                        }
                    }
                    #endregion

                    #region 新工单BOM、明细
                    var oldPLBOMEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);
                    var newPLBOMEntity = Tools.Clone(oldPLBOMEntity);
                    newPLBOMEntity.Id = Guid.NewGuid().ToString();
                    newPLBOMEntity.WorkOrder = entity.WorkOrder;
                    newPLBOMEntity.Creator = CurrentAccount.UserCode;
                    newPLBOMEntity.CreateTime = DateTime.Now;
                    //新工单BOM明细
                    var oldPLBOMItemList = _plBomItemsBLL.Get_ExpressionList(t => t.BOMId == oldPLBOMEntity.Id).ToList();
                    if (oldPLBOMItemList.Count == 0)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_45", oldWorkOrder));//工单【{workOrder}】BOM明细不存在！
                    var newPLBOMItemList = Tools.Clone(oldPLBOMItemList);
                    foreach (var item in newPLBOMItemList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.BOMId = newPLBOMEntity.Id;
                        item.Creator = CurrentAccount.UserCode;
                        item.CreateTime = DateTime.Now;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }
                    #endregion

                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    //设置事务超时时间为1000秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _plWorkOrderService.SaveEntity("", entity, out msg);//保存新工单
                        _plMaterialBLL.SaveEntity("", newPLMaterialEntity, out msg);//新工单物料
                        _plMaterialFacetBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLMaterialFacetList, out msg);
                        _PLProcessBLL.SaveEntity("", newPLProcessEntity, out msg);//工单工艺路线
                        _PLProcessOfOperationsBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLProcessOperationList, out msg);//工单工艺-工序
                        _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLProcessAttrList, out msg);//工单工艺-属性
                        _plBomBLL.SaveForm("", newPLBOMEntity);//工单BOM
                        _plBomItemsBLL.Save_List(false, newPLBOMItemList);//工单BOM明细

                        ts.Complete();
                    }
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #region 拣余单
        private string InsertOrder_J(PL_WorkOrderEntity entity, string oldWorkOrder, int index, decimal DXZH, string remark)
        {
            entity.OrderStatus = "4";//已发料
            entity.ContainerNO = entity.ContainerNO;
            entity.WorkOrder = entity.ProductOrder + "J" + (index + 1).ToString().PadLeft(4, '0');
            return InsertExeWorkOrder_J(entity, oldWorkOrder, DXZH, remark);
        }
        #endregion

        #region 补料单
        private string InsertOrder_B(PL_WorkOrderEntity entity, int index)
        {
            entity.OrderStatus = "3";//发布状态
            entity.WorkOrder = entity.ProductOrder + "B" + (index + 1).ToString().PadLeft(4, '0');
            return null;
        }
        #endregion

        #region 公共方法-新增
        private string InsertStoreIssueEntity(PL_WorkOrderEntity entity, string oldWorkOrder, string remark)
        {
            var planStoreIssueEntity = _planStoreServcie.GetEntity(t => t.WorkOrder == oldWorkOrder);
            if (planStoreIssueEntity == null)
                return Language.GetText("PlanManage.PL_WorkOrderController.Tips_31");//原工单拆解发料记录不存在

            // todo 其他字段需要填充进去
            _planStoreServcie.SaveForm(null, new PL_PlanStoreIssueEntity()
            {
                Id = Guid.NewGuid().ToString(),
                FactoryCode = entity.FactoryCode,
                FactoryName = entity.FactoryName,
                WorkOrder = entity.WorkOrder,
                OrderOrProduct = planStoreIssueEntity.OrderOrProduct,
                WearLayerConsume = planStoreIssueEntity.WearLayerConsume,
                MaskConsume = planStoreIssueEntity.MaskConsume,
                Yield = planStoreIssueEntity.Yield,
                DXZH = planStoreIssueEntity.DXZH,
                MaskStatus = "1",
                WearLayerStatus = "1",
                OrderNum = entity.TotalSheets,
                ProductNum = entity.TotalSheets,
                UnProductNum = entity.TotalSheets,
                Creator = entity.Creator,
                CreateTime = entity.CreateTime,
                Remark = remark,
                IsDeleted = false
            });

            return "";
        }

        #endregion

        #region 拣余单生成执行工单 流转卡
        /// <summary>
        /// 拣余单生成执行工单 流转卡
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="oldWorkOrder"></param>
        /// <param name="DXZH"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        private string InsertExeWorkOrder_J(PL_WorkOrderEntity entity, string oldWorkOrder, decimal DXZH, string remark)
        {
            var index = 1;
            var workOrderEntity = _plWorkOrderService.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder);
            var productOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == entity.ProductOrder);
            //工单物料主数据
            var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);

            //工单物料属性
            var plMaterialFacetEntityList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == plMaterialEntity.Id);

            //var SCTPSLEntity = plMaterialFacetEntityList.ToList().Find(t => t.AttrCode == "SCTPSL");//单拖张数转换值
            //var SCTPSL = SCTPSLEntity == null ? "1" : SCTPSLEntity.AttrValue;
            //工单BOM
            var plBomEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);//一工单一BOM

            var plBomItemsList = _plBomItemsBLL.Get_ExpressionList(t => t.BOMId == plBomEntity.Id);

            //工单工艺路线
            var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == oldWorkOrder && t.IsDeleted == false);
            var plOperationList = _PLProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == plProcessEntity.Id && t.IsDeleted == false)
                .OrderBy(t => t.SN).ToList();
            var startOperationEntity = plOperationList.Find(t => t.OperationCode == entity.StartOperation);
            if (startOperationEntity == null)
                return Language.GetText("PlanManage.PL_WorkOrderController.Tips_56", entity.StartOperation);//工艺路线里没有起始工序{0}

            plOperationList = plOperationList.Where(t => t.SN >= startOperationEntity.SN).OrderBy(t => t.SN).ToList();
            if (plOperationList.Count == 0)
                return Language.GetText("PlanManage.PL_WorkOrderController.Tips_32");//工艺路线-工序不能为空！

            //工序物料托盘数量
            List<PMOperationPalletNumEntity> pmOperationPalletNumList = new List<PMOperationPalletNumEntity>();
            if (workOrderEntity.IsVC == true)
            {
                //过滤掉包装工序
                var arrProcessCode = plOperationList.Take(plOperationList.Count - 1).Select(t => t.OperationCode);
                pmOperationPalletNumList = _PMOperationPalletNumService.GetList(t => arrProcessCode.Contains(t.ProcessCode)
                    && t.Spec == plMaterialEntity.Spec && t.DocType == "2").ToList();
                if (pmOperationPalletNumList.Count != arrProcessCode.Count())
                    return Language.GetText("PlanManage.PL_WorkOrderController.Tips_33");//部分工序物料托盘数量未维护！
            }
            else
            {
                var arrProcessCode = plOperationList.Take(plOperationList.Count - 1).Select(t => t.OperationCode);
                pmOperationPalletNumList = _PMOperationPalletNumService.GetList(t => arrProcessCode.Contains(t.ProcessCode)
                    && t.MaterialCode == plMaterialEntity.MaterialCode && t.DocType == "1").ToList();
                if (pmOperationPalletNumList.Count != arrProcessCode.Count())
                    return Language.GetText("PlanManage.PL_WorkOrderController.Tips_33");//部分工序物料托盘数量未维护！
            }

            var list = _ExeWorkOrderBLL.Get_ExpressionList(t => t.WorkOrder == entity.WorkOrder);
            if (list.Count() > 0)
            {
                index = list.Select(t => int.Parse(t.ExeWorkOrder.Substring(t.ExeWorkOrder.Length - 2, 2))).Max() + 1;
            }
            var msg = "";
            var newEntity = new PL_ExeWorkOrderEntity();
            newEntity.FactoryCode = workOrderEntity.FactoryCode;
            newEntity.FactoryName = workOrderEntity.FactoryName;
            newEntity.WorkOrder = entity.WorkOrder;
            newEntity.ExeWorkOrder = entity.WorkOrder + "-Z" + index.ToString().PadLeft(2, '0');
            newEntity.OrderType = "3";//捡余单
            newEntity.Status = "1";//未开始
            newEntity.AssignStatus = "1";
            newEntity.SheetsQty = entity.TotalSheets;
            newEntity.PSheetsQty = entity.TotalSheets;
            newEntity.PiecesQty = newEntity.PSheetsQty * DXZH;
            newEntity.Yield = workOrderEntity.Yield;
            newEntity.Process = entity.Process;
            newEntity.StartOperation = entity.StartOperation;
            newEntity.TransferBy = workOrderEntity.TransferBy;
            newEntity.Creator = entity.Creator;
            newEntity.CreateTime = entity.CreateTime;
            newEntity.IsEnabled = true;
            newEntity.Remark = remark;
            _ExeWorkOrderBLL.SaveEntity(null, newEntity, out msg);

            var lstCard = new List<PM_TransferCardEntity>();
            var serialNumber = Guid.NewGuid().ToString();
            decimal palletQty = 0;
            string unitName = "";
            string splitOperation = "";//拆托工序
            int palletCount = GetPalletCount(plOperationList, pmOperationPalletNumList, newEntity.PSheetsQty.Value, DXZH.ToInt(), out palletQty, out unitName, out splitOperation);
            //获取首工序托数
            var pmOperationPalletNumEntity = pmOperationPalletNumList.Find(t => t.ProcessCode == newEntity.StartOperation);
            if (pmOperationPalletNumEntity == null)
            {
                return Language.GetText("PlanManage.PL_WorkOrderController.Tips_64");//请维护起始工序物料托盘数量
            }
            int startOperationPalletCount = GetStartOperationPalletCount(newEntity.PSheetsQty.Value, pmOperationPalletNumEntity, DXZH.ToInt());
            for (int i = 1; i <= palletCount; i++)
            {
                PM_TransferCardEntity cardEntity = new PM_TransferCardEntity();
                cardEntity.Create();
                cardEntity.WorkOrder = entity.WorkOrder;
                //cardEntity.FactoryCode = entity.FactoryCode;
                cardEntity.CreateTime = DateTime.Now;
                cardEntity.Creator = CurrentAccount.UserCode;
                cardEntity.ProductOrder = entity.ProductOrder;
                cardEntity.WorkOrderType = "3";
                cardEntity.ExeWorkOrder = newEntity.ExeWorkOrder;
                cardEntity.TransferBy = workOrderEntity.TransferBy;
                cardEntity.CardCode = newEntity.ExeWorkOrder + "-00C" + palletCount.ToString() + "-" + i.ToString().PadLeft(2, '0');
                cardEntity.CardName = palletCount.ToString() + "-" + i.ToString().PadLeft(2, '0');
                cardEntity.CardType = "3";
                cardEntity.ContainerNO = entity.ContainerNO;
                cardEntity.BJGY = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "BJGY")?.AttrValue;
                cardEntity.WorkOrderRemark = remark;
                cardEntity.MaterialCode = workOrderEntity.MaterialCode;
                cardEntity.MaterialName = plMaterialEntity.MaterialName;
                cardEntity.Spec = plMaterialEntity.Spec;
                cardEntity.MMXH = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "MMXH")?.AttrValue;
                cardEntity.JCGG = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "JCGG")?.AttrValue;
                cardEntity.BWXH = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "BWXH")?.AttrValue;
                cardEntity.DXZH = DXZH.ToDecimalOrNull();
                cardEntity.SCTPSL = pmOperationPalletNumList.Find(t => t.ProcessCode == entity.StartOperation)?.PalletNum;
                cardEntity.SCTPSLP = cardEntity.SCTPSL * DXZH.ToDecimal();
                cardEntity.BZTPSL = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "BZSCTPSL")?.AttrValue.ToDecimalOrNull();
                cardEntity.KCKX = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "KCKX")?.AttrValue;
                cardEntity.UV = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "UV")?.AttrValue;
                cardEntity.DJ = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "DJ")?.AttrValue;
                cardEntity.TotalSheets = newEntity.SheetsQty;
                cardEntity.ActualSheets = newEntity.PSheetsQty;
                cardEntity.OrderPieces = entity.OrderPieces;
                cardEntity.ProductPieces = entity.OrderPieces * entity.Yield;//订单片数*良率
                cardEntity.TPGG = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "TPGG")?.AttrValue;
                cardEntity.OrderPallet = workOrderEntity.OrderPallet;
                cardEntity.PerPallerBox = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "BZTPSL")?.AttrValue.ToDecimalOrNull();
                cardEntity.BZDHSL = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "BZDHSL")?.AttrValue.ToDecimalOrNull();
                cardEntity.Description = plMaterialFacetEntityList.FirstOrDefault(t => t.AttrCode == "SMS")?.AttrValue;
                cardEntity.PaperBoxModel = plBomItemsList.FirstOrDefault(t => t.SmallClass == "BC")?.Spec;
                cardEntity.BoxDate = productOrderEntity.BoxDate != null ? productOrderEntity.BoxDate.Value.ToString("ddMMyy") + "A" : "";
                cardEntity.CardStatus = "1";
                cardEntity.IsEnabled = true;
                cardEntity.StartProcess = newEntity.StartOperation;
                cardEntity.PalletNum = i + "-" + palletCount.ToString().PadLeft(2, '0');
                cardEntity.StartOperationPalletCount = startOperationPalletCount;//首工序托数
                //拆托工序
                if (splitOperation != cardEntity.StartProcess)
                    cardEntity.SplitProcess = splitOperation;

                //托盘张数、片数  首工序托数最后一托
                if (i == startOperationPalletCount)
                {
                    //if (unitName == Language.GetText("PlanManage.PL_WorkOrderController.Tips_34"))//张
                    //{
                    cardEntity.PalletQty = newEntity.PSheetsQty.Value - ((startOperationPalletCount - 1) * pmOperationPalletNumEntity.PalletNum.Value);
                    cardEntity.PieceQty = cardEntity.PalletQty * DXZH.ToDecimal();
                    //}
                    //else
                    //{
                    //    cardEntity.PieceQty = newEntity.PSheetsQty.Value * DXZH.ToDecimal() - ((startOperationPalletCount - 1) * pmOperationPalletNumEntity.PalletNum.Value);
                    //    cardEntity.PalletQty = Math.Ceiling(cardEntity.PieceQty.Value / DXZH.ToDecimal());
                    //}
                }
                else
                {
                    cardEntity.PalletQty = cardEntity.SCTPSL;
                    cardEntity.PieceQty = cardEntity.SCTPSLP;
                }

                //流水号
                if (cardEntity.TransferBy == "1")
                {
                    if (i <= startOperationPalletCount)
                    {
                        cardEntity.SerialNumber = serialNumber;
                        cardEntity.PrintStatus = "3"; //可打印
                    }
                    else
                        cardEntity.PrintStatus = "1"; //未打印
                }
                else
                    cardEntity.SerialNumber = Guid.NewGuid().ToString();

                //拆托 标准托盘数量显示
                if (string.IsNullOrEmpty(cardEntity.SplitProcess))
                    cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + cardEntity.PieceQty.Value.ToString();
                else
                {
                    var splitOperationNumEntity = pmOperationPalletNumList.Find(t => t.ProcessCode == splitOperation);
                    if (i == startOperationPalletCount)
                    {
                        //if (splitOperationNumEntity.UnitName == Language.GetText("PlanManage.PL_WorkOrderController.Tips_35"))//张
                        //{
                        var remainQty = newEntity.PSheetsQty % splitOperationNumEntity.PalletNum.Value;
                        cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + remainQty.ToString() + "/" + (remainQty * cardEntity.DXZH).ToString();
                        //}
                        //else
                        //{
                        //    var remainQty = newEntity.PSheetsQty % Math.Ceiling(splitOperationNumEntity.PalletNum.Value / cardEntity.DXZH.Value);
                        //    cardEntity.ShowPalletQty = cardEntity.PalletQty.Value.ToString() + "/" + remainQty.ToString() + "/" + (remainQty * cardEntity.DXZH).ToString();
                        //}
                    }
                    else
                    {
                        //if (splitOperationNumEntity.UnitName == Language.GetText("PlanManage.PL_WorkOrderController.Tips_35"))//张
                        cardEntity.ShowPalletQty = pmOperationPalletNumEntity.PalletNum.Value.ToString() + "/" + splitOperationNumEntity.PalletNum.Value.ToString() + "/" + (splitOperationNumEntity.PalletNum.Value * cardEntity.DXZH).ToString();
                        //else
                        //    cardEntity.ShowPalletQty = pmOperationPalletNumEntity.PalletNum.Value.ToString() + "/" + (Math.Ceiling(splitOperationNumEntity.PalletNum.Value / cardEntity.DXZH.Value)).ToString() + "/" + splitOperationNumEntity.PalletNum.Value.ToString();
                    }
                }
                lstCard.Add(cardEntity);
            }
            _TransferCardBLL.insertList(lstCard);
            return msg;
        }



        #endregion

        #endregion

        #region 批量修改要料

        /// <summary>
        /// 批量修改要料
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_DemandMaterialForm")]
        public HttpResponseMessage SavePL_DemandMaterialForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //var keyValue = getValue(jo, "KeyValue");
            try
            {
                var list = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "data"));
                if (list.Count > 0)
                {
                    _plWorkOrderService.SaveEntity_List(true, userCode, list, out msg);
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region 发布工单
        /// <summary>
        /// 发布工单
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PublishBatchPL_WorkOrderForm")]
        public HttpResponseMessage PublishBatchPL_WorkOrderForm(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var time = DateTime.Now;
            var flag = false;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var modelList = JsonConvert.DeserializeObject<List<PL_WorkOrderModel>>(getValue(jo, "data"));
                var workOrderList = new List<PL_WorkOrderEntity>();
                var StoreIssueList = new List<PL_PlanStoreIssueEntity>();

                if (modelList.Count < 1)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var dylist = _plWorkOrderService.GetWorkOrderYield(modelList[0].ProductOrder);
                var orList = _plWorkOrderService.GetSplitIsOrderOrProduct(modelList[0].ProductOrder);
                var consumeList = _plWorkOrderService.GetWorkOrderMaterialPieceConsume(modelList[0].ProductOrder);

                if (dylist.Count < 1 || dylist.ToList().Any(t => t.Yield == null))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_37");//没有获取到良率！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //检查是否维护大小张
                var num = 0;
                foreach (var item in modelList)
                {
                    if (string.IsNullOrEmpty(item.DXZH.ToString()))
                    {
                        num = num + 1;
                    }

                }
                if (num > 0)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_38");//大小张转换没有维护
                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }

                foreach (var item in modelList)
                {
                    var totalSheets = Math.Ceiling((item.OrderPieces / item.DXZH).Value);
                    var yield = dylist.Find(t => t.WorkOrder == item.WorkOrder)?.Yield;
                    if (yield == null)
                    {
                        return AjaxResult(false, $"工单【{item.WorkOrder}】良率不能为空");
                    }
                    if (yield <= 0 || yield > 1)
                    {
                        flag = true;
                        break;
                    }
                    workOrderList.Add(new PL_WorkOrderEntity()
                    {
                        Id = item.Id,
                        OrderStatus = item.OrderStatus,
                        TotalSheets = totalSheets,
                        ActualSheets = Math.Ceiling(totalSheets / yield),
                        WorkOrderType = item.WorkOrderType,
                        Yield = yield,
                        ModifyBy = userCode,
                        ModifyTime = time,
                        ReleaseBy = userCode,
                        ReleaseName = userName,
                        ReleaseTime = time

                    });
                    var orderOrProduct = orList.Find(t => t.WorkOrder == item.WorkOrder)?.OrderOrProduct;
                    //单耗
                    var mastConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "Mark")?.DanHao;
                    var wearLayerConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "WearLayer")?.DanHao;

                    StoreIssueList.Add(new PL_PlanStoreIssueEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FactoryCode = item.FactoryCode,
                        FactoryName = item.FactoryName,
                        WorkOrder = item.WorkOrder,
                        MaskStatus = "1",
                        WearLayerStatus = "1",
                        OrderNum = totalSheets,
                        ProductNum = Math.Ceiling(totalSheets / yield),
                        UnProductNum = orderOrProduct == "1" ? Math.Ceiling(totalSheets / yield) : totalSheets,
                        Yield = yield,
                        DXZH = item.DXZH,
                        OrderOrProduct = orderOrProduct,
                        MaskConsume = Convert.IsDBNull(mastConsume) ? null : mastConsume,
                        WearLayerConsume = Convert.IsDBNull(wearLayerConsume) ? null : wearLayerConsume,
                        Creator = userCode,
                        CreateTime = time,
                        IsDeleted = false
                    });
                }
                if (flag)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_39");//良率计算错误！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                // TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                //transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为1000秒
                //transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                //using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))

                using (var ts = new TransactionScope())
                {

                    _plWorkOrderService.SaveEntity_List(true, userCode, workOrderList, out msg);
                    _planStoreServcie.InsertList(StoreIssueList);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region 创建发货单查询

        [HttpPost]
        [Route("GetWorkOrderDataTable")]
        public HttpResponseMessage GetWorkOrderDataTable(JObject jo)
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

                var data = _plWorkOrderService.GetWorkOrderDataTable(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.total : data.Rows.Count,
                    records = pagination != null ? pagination.total : data.Rows.Count,
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
        #endregion

        #region 删除工艺路线-工序、属性
        [HttpPost]
        [Route("RemovePLOperationAndAttr")]
        public HttpResponseMessage RemovePLOperationAndAttr(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_19");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                var entity = JsonConvert.DeserializeObject<PL_ProcessOfOperationsEntity>(getValue(jo, "Entity"));

                using (var ts = new TransactionScope())
                {
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.Id == entity.Id);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.OperationsId == entity.Id);

                    ts.Complete();
                }

                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.DeleteSuccess");//删除成功
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
        #endregion

        #region 工单合批管理
        #region 合批
        /// <summary>
        /// 功能描述: 合批
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-29
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("GroupBatch")]
        public HttpResponseMessage GroupBatch(JObject jo)
        {
            try
            {
                var arrWorkOrder = JsonConvert.DeserializeObject<List<string>>(getValue(jo, "arrWorkOrder"));
                if (arrWorkOrder == null || arrWorkOrder.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_41"));//没有要合批的数据行！

                var workOrder = arrWorkOrder[0];
                var workOrderList = _plWorkOrderService.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                #region 新工单
                var newWorkOrderEntity = Tools.Clone(workOrderList.First());
                var serialNo = _baseSequenceService.GetSerialNO("GroupBatch");
                newWorkOrderEntity.Id = Guid.NewGuid().ToString();
                newWorkOrderEntity.ProductOrder = "HPDD" + DateTime.Now.ToString("yyyyMMdd") + serialNo;
                newWorkOrderEntity.WorkOrder = "HPGD" + DateTime.Now.ToString("yyyyMMdd") + serialNo;
                newWorkOrderEntity.ContainerNO = serialNo.ToInt().ToString();
                newWorkOrderEntity.OrderPieces = workOrderList.Sum(t => t.OrderPieces);
                newWorkOrderEntity.OrderBox = workOrderList.Sum(t => t.OrderBox);
                newWorkOrderEntity.OrderPallet = workOrderList.Sum(t => t.OrderPallet);
                newWorkOrderEntity.OrderWholePallet = workOrderList.Sum(t => t.OrderWholePallet);
                newWorkOrderEntity.DeliveryPieces = workOrderList.Sum(t => t.DeliveryPieces);
                newWorkOrderEntity.DeliveryBox = workOrderList.Sum(t => t.DeliveryBox);
                newWorkOrderEntity.DeliveryPallet = workOrderList.Sum(t => t.DeliveryPallet);
                newWorkOrderEntity.DeliveryWholePallet = workOrderList.Sum(t => t.DeliveryWholePallet);
                newWorkOrderEntity.TotalSheets = workOrderList.Sum(t => t.TotalSheets);
                newWorkOrderEntity.ActualSheets = workOrderList.Sum(t => t.ActualSheets);
                newWorkOrderEntity.BatchStatus = 1;
                newWorkOrderEntity.IsEnabled = true;
                newWorkOrderEntity.Creator = CurrentAccount.UserCode;
                newWorkOrderEntity.CreateTime = DateTime.Now;
                newWorkOrderEntity.ModifyBy = CurrentAccount.UserCode;
                newWorkOrderEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 新订单 默认逻辑删除，系统看不到
                var productOrder = workOrderList.First().ProductOrder;
                var oldProductOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == productOrder);
                var newProductOrderEntity = Tools.Clone(oldProductOrderEntity);
                newProductOrderEntity.Id = Guid.NewGuid().ToString();
                newProductOrderEntity.ProductOrder = newWorkOrderEntity.ProductOrder;
                newProductOrderEntity.IsDeleted = true;
                newProductOrderEntity.CreateBy = CurrentAccount.UserCode;
                newProductOrderEntity.CreateTime = DateTime.Now;
                newProductOrderEntity.ModifyBy = CurrentAccount.UserCode;
                newProductOrderEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 新工单物料、属性
                var oldPLMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var newPLMaterialEntity = Tools.Clone(oldPLMaterialEntity);
                newPLMaterialEntity.Id = Guid.NewGuid().ToString();
                newPLMaterialEntity.WorkOrder = newWorkOrderEntity.WorkOrder;
                newPLMaterialEntity.Creator = CurrentAccount.UserCode;
                newPLMaterialEntity.CreateTime = DateTime.Now;
                //新工单物料属性
                var oldPLMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.MaterialId == oldPLMaterialEntity.Id).ToList();
                if (oldPLMaterialFacetList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_42", workOrder));//工单【{workOrder}】物料属性不存在！

                var newPLMaterialFacetList = Tools.Clone(oldPLMaterialFacetList);
                foreach (var item in newPLMaterialFacetList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.MaterialId = newPLMaterialEntity.Id;
                }
                #endregion

                #region 新工单工艺路线、工序、属性
                var oldPLProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var newPLProcessEntity = Tools.Clone(oldPLProcessEntity);
                newPLProcessEntity.Id = Guid.NewGuid().ToString();
                newPLProcessEntity.WorkOrder = newWorkOrderEntity.WorkOrder;
                newPLProcessEntity.Creator = CurrentAccount.UserCode;
                newPLProcessEntity.CreateTime = DateTime.Now;
                //新工单工艺-工序
                var oldPLProcessOperationList = _PLProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == oldPLProcessEntity.Id && t.IsDeleted == false).ToList();
                if (oldPLProcessOperationList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_43", workOrder));//工单【{workOrder}】工艺路线-工序不存在

                var newPLProcessOperationList = Tools.Clone(oldPLProcessOperationList);
                foreach (var item in newPLProcessOperationList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.ProcessId = newPLProcessEntity.Id;
                    item.Creator = CurrentAccount.UserCode;
                    item.CreateTime = DateTime.Now;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }
                //新工单工艺-属性
                var oldPLProcessAttrList = _PLProcessOfOperationsAttrBLL.Get_ExpressionList(t => t.ProcessId == oldPLProcessEntity.Id && t.IsEnabled == true).ToList();
                if (oldPLProcessAttrList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_44", workOrder));//工单【{workOrder}】工艺路线-属性不存在！

                var newPLProcessAttrList = Tools.Clone(oldPLProcessAttrList);
                foreach (var item in oldPLProcessOperationList)
                {
                    var newPLProcessOperationAttrList = newPLProcessAttrList.FindAll(t => t.OperationsId == item.Id);
                    var operatonId = newPLProcessOperationList.Find(t => t.OperationCode == item.OperationCode).Id;
                    foreach (var attr in newPLProcessOperationAttrList)
                    {
                        attr.Id = Guid.NewGuid().ToString();
                        attr.ProcessId = newPLProcessEntity.Id;
                        attr.OperationsId = operatonId;
                        attr.Creator = CurrentAccount.UserCode;
                        attr.CreateTime = DateTime.Now;
                        attr.ModifyBy = CurrentAccount.UserCode;
                        attr.ModifyTime = DateTime.Now;
                    }
                }
                #endregion

                #region 新工单BOM、明细
                var oldPLBOMEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var newPLBOMEntity = Tools.Clone(oldPLBOMEntity);
                newPLBOMEntity.Id = Guid.NewGuid().ToString();
                newPLBOMEntity.WorkOrder = newWorkOrderEntity.WorkOrder;
                newPLBOMEntity.Creator = CurrentAccount.UserCode;
                newPLBOMEntity.CreateTime = DateTime.Now;
                //新工单BOM明细
                var oldPLBOMItemList = _plBomItemsBLL.Get_ExpressionList(t => t.BOMId == oldPLBOMEntity.Id).ToList();
                if (oldPLBOMItemList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_45", workOrder));//工单【{workOrder}】BOM明细不存在！
                var newPLBOMItemList = Tools.Clone(oldPLBOMItemList);
                foreach (var item in newPLBOMItemList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.BOMId = newPLBOMEntity.Id;
                    item.Creator = CurrentAccount.UserCode;
                    item.CreateTime = DateTime.Now;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 工单拆解发料
                PL_PlanStoreIssueEntity newPlanStoreEntity = null;
                List<PL_PlanStoreIssueEntity> oldPlanStoreList = new List<PL_PlanStoreIssueEntity>();
                //判断是否存在发布的工单
                var flag = workOrderList.Any(t => t.OrderStatus == "3");
                if (flag)
                {
                    oldPlanStoreList = _planStoreServcie.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    newPlanStoreEntity = Tools.Clone(oldPlanStoreList.First());
                    newPlanStoreEntity.Id = Guid.NewGuid().ToString();
                    newPlanStoreEntity.WorkOrder = newWorkOrderEntity.WorkOrder;
                    newPlanStoreEntity.OrderNum = newWorkOrderEntity.TotalSheets;
                    newPlanStoreEntity.ProductNum = newWorkOrderEntity.ActualSheets;
                    newPlanStoreEntity.UnProductNum = newPlanStoreEntity.OrderOrProduct == "1" ? Math.Ceiling(newWorkOrderEntity.TotalSheets.Value / newPlanStoreEntity.Yield.Value) : newWorkOrderEntity.TotalSheets;
                    newPlanStoreEntity.IsDeleted = false;
                    newPlanStoreEntity.Creator = CurrentAccount.UserCode;
                    newPlanStoreEntity.CreateTime = DateTime.Now;

                    foreach (var item in oldPlanStoreList)
                    {
                        item.IsDeleted = true;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }

                    newWorkOrderEntity.OrderStatus = "3";
                }
                #endregion

                #region 更新原工单
                foreach (var item in workOrderList)
                {
                    item.BatchWorkOrder = newWorkOrderEntity.WorkOrder;
                    item.BatchStatus = 2;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                    if (newWorkOrderEntity.OrderStatus == "3")
                        item.OrderStatus = "3";
                }
                #endregion

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                //设置事务超时时间为1000秒
                transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _ProductionOrderBLL.SaveEntity("", newProductOrderEntity, out msg);//新订单
                    _plWorkOrderService.SaveEntity("", newWorkOrderEntity, out msg);//保存新工单
                    _plMaterialBLL.SaveEntity("", newPLMaterialEntity, out msg);//新工单物料
                    _plMaterialFacetBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLMaterialFacetList, out msg);
                    _PLProcessBLL.SaveEntity("", newPLProcessEntity, out msg);//工单工艺路线
                    _PLProcessOfOperationsBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLProcessOperationList, out msg);//工单工艺-工序
                    _PLProcessOfOperationsAttrBLL.SaveEntity_List(false, CurrentAccount.UserCode, newPLProcessAttrList, out msg);//工单工艺-属性
                    _plBomBLL.SaveForm("", newPLBOMEntity);//工单BOM
                    _plBomItemsBLL.Save_List(false, newPLBOMItemList);//工单BOM明细
                    if (flag) //发布生成拆解发料
                    {
                        _planStoreServcie.SaveForm("", newPlanStoreEntity);//拆解发料
                        _planStoreServcie.SaveEntity_List(true, CurrentAccount.UserCode, oldPlanStoreList, out msg);//拆解发料更新
                    }
                    _plWorkOrderService.SaveEntity_List(true, CurrentAccount.UserCode, workOrderList, out msg);//更新工单

                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 取消合批
        /// <summary>
        /// 功能描述: 取消合批
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-29
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("UnGroupBatch")]
        public HttpResponseMessage UnGroupBatch(JObject jo)
        {
            try
            {
                var workOrder = getValue(jo, "workOrder");
                if (string.IsNullOrEmpty(workOrder))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_48"));//合批工单不能为空！

                var oldWorkOrderList = _plWorkOrderService.Get_ExpressionList(t => t.BatchWorkOrder == workOrder).ToList();
                foreach (var item in oldWorkOrderList)
                {
                    item.BatchWorkOrder = " ";
                    item.BatchStatus = 0;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyTime = DateTime.Now;
                }
                var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);
                var plProcessEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder);
                var plBOMEntity = _plBomBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);

                List<PL_PlanStoreIssueEntity> oldPlanStoreList = new List<PL_PlanStoreIssueEntity>();
                var newPLStoreEntity = _planStoreServcie.GetEntity(t => t.WorkOrder == workOrder);
                if (newPLStoreEntity != null)
                {
                    var arrWorkOrder = oldWorkOrderList.Select(t => t.WorkOrder);
                    oldPlanStoreList = _planStoreServcie.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                    foreach (var item in oldPlanStoreList)
                    {
                        item.IsDeleted = false;
                        item.ModifyBy = CurrentAccount.UserCode;
                        item.ModifyTime = DateTime.Now;
                    }
                }

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                //设置事务超时时间为1000秒
                transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _plWorkOrderService.RemoveForm(t => t.WorkOrder == workOrder);
                    _plWorkOrderService.SaveEntity_List(true, CurrentAccount.UserCode, oldWorkOrderList, out msg);
                    _plMaterialBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == plMaterialEntity.Id);
                    _PLProcessBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == plProcessEntity.Id);
                    _plBomBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _plBomItemsBLL.RemoveForm(t => t.BOMId == plBOMEntity.Id);
                    if (newPLStoreEntity != null)
                    {
                        _planStoreServcie.RemoveForm(t => t.WorkOrder == workOrder);
                        _planStoreServcie.SaveEntity_List(true, CurrentAccount.UserCode, oldPlanStoreList, out msg);
                    }

                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region 发布
        /// <summary>
        /// 功能描述: 发布
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-29
        /// 任务编号: 生产工单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("HePiPublish")]
        public HttpResponseMessage HePiPublish(JObject jo)
        {
            try
            {
                var modelList = JsonConvert.DeserializeObject<List<PL_WorkOrderModel>>(getValue(jo, "data"));
                var workOrderList = new List<PL_WorkOrderEntity>();
                var StoreIssueList = new List<PL_PlanStoreIssueEntity>();
                var flag = false;

                if (modelList.Count < 1)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_49"));//参数不能为空！

                var dylist = _plWorkOrderService.GetWorkOrderYield(modelList[0].ProductOrder);
                var orList = _plWorkOrderService.GetSplitIsOrderOrProduct(modelList[0].ProductOrder);
                var consumeList = _plWorkOrderService.GetWorkOrderMaterialPieceConsume(modelList[0].ProductOrder);

                if (dylist.Count < 1 || dylist.ToList().Any(t => t.Yield == null))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_50"));//没有获取到良率！

                foreach (var item in modelList)
                {
                    var totalSheets = Math.Ceiling((item.OrderPieces / item.DXZH).Value);
                    var yield = (Decimal)dylist.Find(t => t.WorkOrder == item.WorkOrder)?.Yield;
                    if (yield <= 0 || yield > 1)
                    {
                        flag = true;
                        break;
                    }
                    workOrderList.Add(new PL_WorkOrderEntity()
                    {
                        Id = item.Id,
                        OrderStatus = "3",
                        TotalSheets = totalSheets,
                        ActualSheets = Math.Ceiling(totalSheets / yield),
                        WorkOrderType = item.WorkOrderType,
                        Yield = yield,
                        ModifyBy = CurrentAccount.UserCode,
                        ModifyTime = DateTime.Now
                    });
                    var oldWorkOrderList = _plWorkOrderService.Get_ExpressionList(t => t.BatchWorkOrder == item.WorkOrder).ToList();
                    foreach (var item2 in oldWorkOrderList)
                    {
                        item2.OrderStatus = "3";
                        item2.ModifyBy = CurrentAccount.UserCode;
                        item2.ModifyTime = DateTime.Now;
                    }
                    workOrderList.AddRange(oldWorkOrderList);
                    var orderOrProduct = orList.Find(t => t.WorkOrder == item.WorkOrder)?.OrderOrProduct;
                    //单耗
                    var mastConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "Mark")?.DanHao;
                    var wearLayerConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "WearLayer")?.DanHao;

                    StoreIssueList.Add(new PL_PlanStoreIssueEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FactoryCode = item.FactoryCode,
                        FactoryName = item.FactoryName,
                        WorkOrder = item.WorkOrder,
                        MaskStatus = "1",
                        WearLayerStatus = "1",
                        OrderNum = totalSheets,
                        ProductNum = Math.Ceiling(totalSheets / yield),
                        UnProductNum = orderOrProduct == "1" ? Math.Ceiling(totalSheets / yield) : totalSheets,
                        Yield = yield,
                        DXZH = item.DXZH,
                        OrderOrProduct = orderOrProduct,
                        MaskConsume = Convert.IsDBNull(mastConsume) ? null : mastConsume,
                        WearLayerConsume = Convert.IsDBNull(wearLayerConsume) ? null : wearLayerConsume,
                        Creator = CurrentAccount.UserCode,
                        CreateTime = DateTime.Now,
                        IsDeleted = false
                    });
                }
                if (flag)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_51"));//良率计算错误！

                var msg = "";
                // TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                //transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为1000秒
                //transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                //using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                using (var ts = new TransactionScope())
                {
                    _plWorkOrderService.SaveEntity_List(true, CurrentAccount.UserCode, workOrderList, out msg);
                    _planStoreServcie.InsertList(StoreIssueList);
                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #endregion

        #region 公共方法
        /// <summary>
        /// 获取执行工单生成托数
        /// </summary>
        /// <param name="pmOperationPalletNumList">//工序物料托盘数量集合/param>
        /// <param name="sheetQty">执行工单张数</param>
        /// <param name="spec">规格</param>
        /// <param name="DXZH">大小转换</param>
        /// <returns></returns>
        private int GetPalletCount(List<PL_ProcessOfOperationsEntity> plOperationList, List<PMOperationPalletNumEntity> pmOperationPalletNumList, decimal sheetQty, int DXZH, out decimal palletQty, out string unitName, out string operationCode)
        {
            //取工艺路线-工序的最大托数
            int num = 0;
            palletQty = 0;
            unitName = "";
            operationCode = "";
            var newPLOperationList = plOperationList.Take(plOperationList.Count - 1).OrderBy(t => t.SN).ToList();
            foreach (var operation in newPLOperationList)
            {
                var item = pmOperationPalletNumList.Find(t => t.ProcessCode == operation.OperationCode);
                var palletCount = 0;
                //if (item.UnitName == Language.GetText("PlanManage.PL_WorkOrderController.Tips_52"))//张
                palletCount = (int)Math.Ceiling(sheetQty / item.PalletNum.ToDecimal());
                //else
                //    palletCount = (int)Math.Ceiling(sheetQty / (item.PalletNum / DXZH).ToDecimal());

                if (palletCount > num)
                {
                    num = palletCount;
                    palletQty = item.PalletNum.Value;
                    unitName = item.UnitName;
                    operationCode = item.ProcessCode;
                }
            }

            return num;
        }

        /// <summary>
        /// 获取工艺路线首工序托数
        /// </summary>
        /// <param name="sheetQty">执行工单张数</param>
        /// <param name="pmOperationPalletNumEntity">实体</param>
        /// <param name="DXZH">大小转换</param>
        /// <returns></returns>
        private int GetStartOperationPalletCount(decimal sheetQty, PMOperationPalletNumEntity pmOperationPalletNumEntity, int DXZH)
        {
            int num = 0;

            var palletCount = 0;
            //if (pmOperationPalletNumEntity.UnitName == Language.GetText("PlanManage.PL_WorkOrderController.Tips_53"))//张
            palletCount = (int)Math.Ceiling(sheetQty / pmOperationPalletNumEntity.PalletNum.ToDecimal());
            //else
            //    palletCount = (int)Math.Ceiling(sheetQty / (pmOperationPalletNumEntity.PalletNum / DXZH).ToDecimal());

            num = palletCount;
            return num;
        }
        #endregion

        #region 工单唛头预览
        [HttpPost]
        [Route("WorkMarkPreview")]
        public HttpResponseMessage WorkMarkPreview(JObject jo)
        {
            List<string> data = new List<string>();
            try
            {
                var productOrder = getValue(jo, "productOrder");

                var materialCodes = "";
                List<string> arrWorkOrderType = new List<string>() { "1", "4" };
                var workOrderList = _plWorkOrderService.Get_ExpressionList(t => arrWorkOrderType.Contains(t.WorkOrderType) && t.ProductOrder == productOrder).ToList();
                var arrWorkOrder = workOrderList.Select(t => t.WorkOrder).ToArray();
                var plMaterialList = _plMaterialBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                var arrPLMaterialId = plMaterialList.Select(t => t.Id).ToArray();
                var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => t.AttrCode == "MTBM" && arrPLMaterialId.Contains(t.MaterialId)).ToList();
                foreach (var item in workOrderList)
                {
                    var query = from a in plMaterialList.Where(t => t.WorkOrder == item.WorkOrder)
                                join b in plMaterialFacetList on a.Id equals b.MaterialId
                                select b;

                    var templateName = query.ToList().FirstOrDefault()?.AttrValue;
                    if (string.IsNullOrEmpty(templateName))
                        materialCodes += item.MaterialCode + ",";

                    item.MTBM = templateName;
                }

                if (!string.IsNullOrEmpty(materialCodes))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_54", materialCodes.TrimEnd(',')));//客户型号【{materialCodes.TrimEnd(,)}】没有维护唛头编码属性

                //获取唛头列表
                var markConfigList = _plMarkUploadService.Get_ExpressionList(t => t.IsEnabled == true).ToList();
                foreach (var group in workOrderList.GroupBy(t => t.MTBM))
                {
                    var templateName = markConfigList.Find(t => t.MarkCode == group.Key)?.MarkName;
                    if (string.IsNullOrEmpty(templateName))
                        return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_55"));//唛头配置不存在！

                    //参数
                    List<string> lstMaterialCode = new List<string>();
                    List<string> lstWorkOrder = new List<string>();
                    foreach (var item in group)
                    {
                        if (!lstMaterialCode.Contains(item.MaterialCode))
                        {
                            lstMaterialCode.Add(item.MaterialCode);
                            lstWorkOrder.Add(item.WorkOrder);
                        }
                    }
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    var code = string.Join(",", lstWorkOrder);
                    dic.Add("Code", code);
                    //模板路径
                    var mapPath = HttpContext.Current.Server.MapPath("~/");
                    DirectoryInfo parentDir = Directory.GetParent(mapPath);//去掉最后一级目录
                    var myParentDir = parentDir.Parent.FullName;
                    string fileName = myParentDir + @"/Material/Upload/" + templateName; //  本地：ALP.Material.WebApi

                    var resultData = PrintToPDF(fileName, dic);
                    data.Add(resultData);
                }

                return AjaxResult(true, Language.GetText("Common.Success"), data);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

        #region SAP结案
        /// <summary>
        /// 创建：jpf
        /// 时间：2024-3-29 13:20:15
        /// 描述：SAP结案
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinishCase")]
        public HttpResponseMessage FinishCase(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var workOrderList = JsonConvert.DeserializeObject<List<PL_WorkOrderEntity>>(getValue(jo, "data"));
                if (workOrderList == null || workOrderList.Count == 0)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_7");//参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (workOrderList.Exists(t => t.WorkOrderType != "1" && t.WorkOrderType != "4"))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_63"));//只有正常工单、免产单可以执行此操作

                if (workOrderList.Exists(t => (t.POStatus).ToInt() < 3))
                {
                    return AjaxResult(false, "当前PO状态不允许结案");
                }

                var newworkOrderList = workOrderList.Select(t => string.IsNullOrEmpty(t.Update_IsPosted));
                if (newworkOrderList.Any())
                {
                    string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                    var isbool = new ToSAPService().PL_WorkOrderStatusTOSAP(workOrderList, postUser);
                    if (isbool)
                    {
                        result.success = true;
                        result.returnMsg = Language.GetText("Common.Success");
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_61"); //部分工单传SAP失败
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                else
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_WorkOrderController.Tips_62"); //无需要传SAP的工单
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region SAP发布工单（弃用待确认）
        [HttpPost]
        [Route("PublishSAPWorkOrder")]
        public HttpResponseMessage PublishSAPWorkOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;

            var time = DateTime.Now;
            var msg = "";
            try
            {
                var modelList = JsonConvert.DeserializeObject<List<PL_WorkOrderModel>>(getValue(jo, "data"));
                var workOrderList = new List<PL_WorkOrderEntity>();
                var StoreIssueList = new List<PL_PlanStoreIssueEntity>();

                if (modelList == null || modelList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_7"));//参数不能为空！

                if (modelList.Any(t => t.OrderStatus == "3"))
                    AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_59")); //存在已发布的工单

                var woLists = _WorkOrderBLL.Get_ExpressionList(t => t.ProductOrder == modelList[0].ProductOrder && t.OrderStatus == "1").ToList();
                //查找订单信息获取订单类型
                var ProductionOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == modelList[0].ProductOrder);
                var ordertype = ProductionOrderEntity.OrderType;
                foreach (var item in woLists)
                {
                    var WorkOrder = item.WorkOrder;
                    var PL_Material = new PL_Material_Service().Get_ExpressionEntity(t => t.WorkOrder == WorkOrder && t.IsDeleted == false);
                    if (PL_Material.IsEmpty())
                        return AjaxResult(false, WorkOrder + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_30"));//工单物料未生成，请去维护后重新导入

                    var PL_MaterialFacet = new PL_MaterialFacet_Service().Get_ExpressionEntity(t => t.MaterialId == PL_Material.Id && t.AttrCode == "DXZH");
                    if (string.IsNullOrEmpty(PL_MaterialFacet?.AttrValue))
                        return AjaxResult(false, WorkOrder + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_31"));//工单无大小张转化属性，请先维护后重新导入

                }
                var woList = woLists.Where(t => t.IsVC == false).ToList();
                if (woList.Count > 0)
                {
                    //1.生成bom
                    var bomQuery = from wo in woList
                                   join bom in _newBOMBLL.Get_ExpressionList(t => t.OrderType == ordertype)
                                   on new { wo.FactoryCode, wo.MaterialCode, wo.Process } equals new { bom.FactoryCode, bom.MaterialCode, bom.Process }
                                   join material in _bsMaterialBLL.Get_ExpressionList(t => true)
                                   on bom.MaterialCode equals material.MaterialCode
                                   select new
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       BsBomId = bom.Id,
                                       bom.FactoryCode,
                                       bom.FactoryName,
                                       wo.WorkOrder,
                                       bom.BOMCode,
                                       bom.MaterialClass,
                                       bom.MaterialCode,
                                       bom.MaterialName,
                                       bom.Process,
                                       bom.UnitNum,
                                       bom.OrderType,
                                       material.Spec,
                                       Creator = userCode,
                                       CreateTime = time,
                                       IsDeleted = false
                                   };

                    var bomList = bomQuery.ToList();
                    //判断BOM是否生成，生成就过滤掉
                    foreach (var item in bomList)
                    {
                        //判断BOM是否存在，存在就不生成
                        var bomEntity = _PLBOMBLL.Get_ExpressionEntity(t => t.FactoryCode == item.FactoryCode && t.WorkOrder == item.WorkOrder && t.BOMCode == item.BOMCode && t.MaterialCode == item.MaterialCode);
                        if (bomEntity != null)
                        {
                            bomList.Remove(item);
                        }

                    }

                    // 2.生成bomItem
                    var bomItemQuery = from bom in bomList
                                       join bomItem in _BOMItemsBLL.Get_ExpressionList(t => true) on bom.BsBomId equals bomItem.BOMId
                                       join materail in _bsMaterialBLL.Get_ExpressionList(t => true) on bomItem.MaterialCode equals materail.MaterialCode
                                       join mfactory in _MaterialFactoryBLL.Get_ExpressionList(t => true) on materail.MaterialCode equals mfactory.MaterialCode
                                       where bom.FactoryCode == mfactory.FactoryCode
                                       select new
                                       {
                                           Id = Guid.NewGuid().ToString(),
                                           BOMId = bom.Id,
                                           bom.BOMCode,
                                           bom.FactoryCode,
                                           bom.FactoryName,
                                           bomItem.MaterialCode,
                                           bomItem.MaterialName,
                                           bomItem.Num,
                                           bomItem.Warehouse,
                                           bomItem.ConsumeProcess,
                                           materail.Spec,
                                           materail.SmallClass,
                                           materail.MaterialClass,
                                           materail.Unit,
                                           materail.UnitName,
                                           mfactory.ProcureType,
                                           mfactory.ProcessRoute,
                                           mfactory.IsUsed,
                                           Creator = userCode,
                                           CreateTime = time
                                       };

                    var bomItemList = bomItemQuery.ToList();

                    //3.生成物料需求 dragon 2023-03-07
                    var reqQuery = from workOrder in woList.Where(t => t.WorkOrderType == "1")
                                       //var reqQuery = from workOrder in woList.Where(t => t.WorkOrderType == "1" && t.AvoidProduce == false)
                                   join bom in bomList on workOrder.WorkOrder equals bom.WorkOrder
                                   join bomItem in bomItemList on bom.Id equals bomItem.BOMId
                                   select new PL_PrdOrderReqMaterialsEntity
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       FactoryCode = bom.FactoryCode,
                                       FactoryName = bom.FactoryName,
                                       WorkOrder = workOrder.WorkOrder,
                                       MaterialCode = bomItem.MaterialCode,
                                       MaterialName = bomItem.MaterialName,
                                       Spec = bomItem.Spec,
                                       SmallClass = bomItem.SmallClass,
                                       UnitName = bomItem.UnitName,
                                       Amount = Math.Round((workOrder.OrderPieces / bom.UnitNum * bomItem.Num).Value, 3),
                                       PurchaseType = bomItem.ProcureType,
                                       Creator = userCode,
                                       CreateTime = time,
                                       IsDeleted = false
                                   };

                    var reqList = reqQuery.ToList();

                    if (reqList.Count < 1)
                        return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_33"));//物料BOM信息没有查到

                    //免产单可以审核但不生成物料需求
                    var arrWorkOrder = woList.Where(t => t.AvoidProduce == true).Select(t => t.WorkOrder).ToArray();
                    reqList = reqList.Where(t => !arrWorkOrder.Contains(t.WorkOrder)).ToList();

                    DataTable bomDt = Tools.ToDataTable(bomList);

                    bomDt.Columns.Remove("BsBomId");
                    //bomDt.Columns.Remove("FactoryCode");

                    DataTable bomItemDt = Tools.ToDataTable(bomItemList);
                    DataTable reqDt = Tools.ToDataTable(reqList);

                    if (woList.Count > 0) _WorkOrderBLL.SaveEntity_List(true, null, woList, out msg);

                    if (bomDt.Rows.Count > 0) _PLBOMBLL.InsertDataTable(bomDt);
                    if (bomItemDt.Rows.Count > 0) _PLBOMItemsBLL.InsertDataTable(bomItemDt);
                    if (reqDt.Rows.Count > 0) _PrdOrderReqMaterialsBLL.InsertDataTable(reqDt);
                }

                var dylist = _plWorkOrderService.GetWorkOrderYield(modelList[0].ProductOrder);
                var orList = _plWorkOrderService.GetSplitIsOrderOrProduct(modelList[0].ProductOrder);
                var consumeList = _plWorkOrderService.GetWorkOrderMaterialPieceConsume(modelList[0].ProductOrder);

                if (dylist.Count < 1 || dylist.ToList().Any(t => t.Yield == null))
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_37"));//没有获取到良率！

                //检查是否维护大小张
                var num = 0;
                foreach (var item in modelList)
                {
                    if (string.IsNullOrEmpty(item.DXZH.ToString()))
                    {
                        num = num + 1;
                    }

                }
                if (num > 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_38"));//大小张转换没有维护

                var flag = false;
                foreach (var item in modelList)
                {
                    var totalSheets = Math.Ceiling((item.OrderPieces / item.DXZH).Value);
                    var yield = (Decimal)dylist.Find(t => t.WorkOrder == item.WorkOrder)?.Yield;
                    if (yield <= 0 || yield > 1)
                    {
                        flag = true;
                        break;
                    }
                    workOrderList.Add(new PL_WorkOrderEntity()
                    {
                        Id = item.Id,
                        OrderStatus = item.OrderStatus,
                        TotalSheets = totalSheets,
                        ActualSheets = Math.Ceiling(totalSheets / yield),
                        WorkOrderType = item.WorkOrderType,
                        Yield = yield,
                        ModifyBy = userCode,
                        ModifyTime = time,
                        ReleaseBy = userCode,
                        ReleaseName = userName,
                        ReleaseTime = time

                    });
                    var orderOrProduct = orList.Find(t => t.WorkOrder == item.WorkOrder)?.OrderOrProduct;
                    //单耗
                    var mastConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "Mark")?.DanHao;
                    var wearLayerConsume = consumeList.Find(t => t.WorkOrder == item.WorkOrder && t.TypeName == "WearLayer")?.DanHao;

                    StoreIssueList.Add(new PL_PlanStoreIssueEntity()
                    {
                        Id = Guid.NewGuid().ToString(),
                        FactoryCode = item.FactoryCode,
                        FactoryName = item.FactoryName,
                        WorkOrder = item.WorkOrder,
                        MaskStatus = "1",
                        WearLayerStatus = "1",
                        OrderNum = totalSheets,
                        ProductNum = Math.Ceiling(totalSheets / yield),
                        UnProductNum = orderOrProduct == "1" ? Math.Ceiling(totalSheets / yield) : totalSheets,
                        Yield = yield,
                        DXZH = item.DXZH,
                        OrderOrProduct = orderOrProduct,
                        MaskConsume = Convert.IsDBNull(mastConsume) ? null : mastConsume,
                        WearLayerConsume = Convert.IsDBNull(wearLayerConsume) ? null : wearLayerConsume,
                        Creator = userCode,
                        CreateTime = time,
                        IsDeleted = false
                    });
                }
                if (flag)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_39"));//良率计算错误！

                // TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                //transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为1000秒
                //transactionOption.Timeout = new TimeSpan(0, 0, 1000);
                //using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))

                string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                var isbool = new ToSAPService().PL_WorkOrderTOSAP(workOrderList, postUser);

                if (isbool)
                {
                    using (var ts = new TransactionScope())
                    {
                        _plWorkOrderService.SaveEntity_List(true, userCode, workOrderList, out msg);
                        _planStoreServcie.InsertList(StoreIssueList);

                        ts.Complete();
                    }

                    return AjaxResult(true, Language.GetText("Common.Success"));
                }
                else
                    return AjaxResult(false, Language.GetText("PlanManage.PL_WorkOrderController.Tips_60"));
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);
            }
        }

        #endregion
    }
}
