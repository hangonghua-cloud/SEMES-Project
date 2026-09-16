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
using ALP.Application.UtilExtend;
using System.Data;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Busines.Material;
using System.Transactions;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Service.Resources;
using ALP.Application.Service.ToSAP;

namespace ALP.Application.WebApi.Controllers.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProductionOrderController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 生产订单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_ProductionOrder")]
    public class PL_ProductionOrderController : ApiBaseController
    {
        private PL_ProductionOrderBLL _ProductionOrderBLL = new PL_ProductionOrderBLL();
        private PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();
        private PL_BOMBLL _PLBOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _PLBOMItemsBLL = new PL_BOMItemsBLL();
        private PL_PrdOrderReqMaterialsBLL _PrdOrderReqMaterialsBLL = new PL_PrdOrderReqMaterialsBLL();
        private PL_MaterialBLL _PLMaterialBLL = new PL_MaterialBLL();
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();
        private Base_KeyParameterItemBLL _keyParameterItemBLL = new Base_KeyParameterItemBLL();
        private BS_BOMBLL _BOMBLL = new BS_BOMBLL();
        private BS_BOMItemsBLL _BOMItemsBLL = new BS_BOMItemsBLL();
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();
        private Base_MaterialBLL _MaterialBLL = new Base_MaterialBLL();
        private PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _plOperationBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _plAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private PL_PlanStoreIssueBLL _planStoreIssueBLL = new PL_PlanStoreIssueBLL();

        private static string appLogPath = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_ProductionOrderPageList")]
        public HttpResponseMessage PL_ProductionOrderPageList(JObject jo)
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

                var data = _ProductionOrderBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_ProductionOrderPageDataTableList")]
        public HttpResponseMessage PL_ProductionOrderPageDataTableList(JObject jo)
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

                var data = _ProductionOrderBLL.GetPageDataTableList(pagination, queryJson, CurrentAccount.UserCode);
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_ProductionOrderList")]
        public HttpResponseMessage GetPL_ProductionOrderList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _ProductionOrderBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_ProductionOrder")]
        public HttpResponseMessage SavePL_ProductionOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_8");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_9");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {

                PL_ProductionOrderEntity entity = JsonConvert.DeserializeObject<PL_ProductionOrderEntity>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");

                string msg = "";
                int isok = _ProductionOrderBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_ProductionOrder")]
        public HttpResponseMessage SaveBatchPL_ProductionOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var Resultmsg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }


            if (jo.SelectToken("data1") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_12");//缺少生产订单参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var listOrder = JsonConvert.DeserializeObject<List<PL_ProductionOrderEntity>>(getValue(jo, "data1"));
                var group = listOrder.GroupBy(t => new { t.ProductOrder }).ToList();
                if (group.Count != listOrder.Count)
                {
                    foreach (var item in group)
                    {
                        if (item.Count() > 1)
                        {
                            Resultmsg += item.Key.ProductOrder + ",";
                        }
                    }
                    result.success = false;
                    result.returnMsg = Resultmsg + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_13");//订单重复！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                foreach (var item in listOrder)
                {
                    //判断客户是否有定义规则
                    var KeyParameterItem = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "client" && t.ItemValue == "special" && t.ItemCode == item.Customer);
                    if (KeyParameterItem != null)
                    {
                        if (!string.IsNullOrEmpty(KeyParameterItem.Col1) && !string.IsNullOrEmpty(KeyParameterItem.Col2))
                        {
                            if (KeyParameterItem.Col1 != item.ProductOrder.Substring(0, KeyParameterItem.Col1.Length))
                            {
                                return AjaxResult(false, item.ProductOrder + "订单号不符合规则！应该以" + KeyParameterItem.Col1 + "前缀" + "后应以" + KeyParameterItem.Col2 + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_14"));//位结尾
                            }
                            if (int.Parse(KeyParameterItem.Col2) != item.ProductOrder.Substring(KeyParameterItem.Col1.Length).Length)
                            {
                                return AjaxResult(false, item.ProductOrder + "订单号不符合规则！应该以" + KeyParameterItem.Col1 + "前缀" + "后应以" + KeyParameterItem.Col2 + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_14"));//位结尾
                            }
                            //判断这个订单号是否维护过

                        }
                        else
                        {
                            var arr = item.ProductOrder.Split('-');
                            if (arr.Length != 2)
                                return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_15"));//订单号不符合规则！
                            if (arr[0].Length != 4 || arr[1].Length != 5)
                                return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_15"));//订单号不符合规则！
                            if (arr[1].Substring(arr[1].Length - 1, 1) != "#")
                                return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_16"));//订单号以#号字符结束！
                        }
                    }
                }

                Resultmsg = _ProductionOrderBLL.ProductOrderImport(listOrder);

                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? Language.GetText("Common.Success") : Resultmsg;//操作成功
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_ProductionOrder")]
        public HttpResponseMessage DeletePL_ProductionOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PL_ProductionOrderEntity entity = JsonConvert.DeserializeObject<PL_ProductionOrderEntity>(getValue(jo, "Entity"));
                if (entity.OrderStatus == "1")
                {//创建
                    int isok = _ProductionOrderBLL.DeleteEntity(entity.Id, out msg, null);
                }
                else if (entity.OrderStatus == "2")//审核
                {
                    //工单
                    var WorkOrderList = _WorkOrderBLL.Get_ExpressionList(t => t.ProductOrder == entity.ProductOrder).ToList();
                    var arrWorkOrder = WorkOrderList.Select(t => t.WorkOrder).ToList();
                    //工单物料
                    var plMaterialList = _PLMaterialBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    var arrMaterialId = plMaterialList.Select(t => t.Id).ToList();
                    var plMaterialFacetList = _plMaterialFacetBLL.Get_ExpressionList(t => arrMaterialId.Contains(t.MaterialId)).ToList();
                    //工单工艺
                    var plProcessList = _plProcessBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    var arrProcessId = plProcessList.Select(t => t.Id).ToList();
                    var plOperationList = _plOperationBLL.Get_ExpressionList(t => arrProcessId.Contains(t.ProcessId)).ToList();
                    var plAttrList = _plAttrBLL.Get_ExpressionList(t => arrProcessId.Contains(t.ProcessId)).ToList();
                    //工单BOM
                    var plBomList = _PLBOMBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    var arrBomId = plBomList.Select(t => t.Id).ToList();
                    var plBomItemList = _PLBOMItemsBLL.Get_ExpressionList(t => arrBomId.Contains(t.BOMId)).ToList();
                    //物料需求
                    var materialReqList = _PrdOrderReqMaterialsBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder) && t.IsDeleted == false).ToList();
                    //工单发料汇总表
                    var planStoreIssueList = _planStoreIssueBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();

                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    // 设置事务超时时间为60秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 60);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _ProductionOrderBLL.Delete(entity);
                        _WorkOrderBLL.Delete(WorkOrderList);
                        _plProcessBLL.Delete(plProcessList);
                        _plOperationBLL.Delete(plOperationList);
                        _plAttrBLL.Delete(plAttrList);
                        _PLBOMBLL.Delete(plBomList);
                        _PLBOMItemsBLL.Delete(plBomItemList);
                        _PrdOrderReqMaterialsBLL.Delete(materialReqList);
                        _planStoreIssueBLL.Delete(planStoreIssueList);
                        _PLMaterialBLL.Delete(plMaterialList);
                        _plMaterialFacetBLL.Delete(plMaterialFacetList);

                        ts.Complete();
                    }
                }

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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_ProductionOrder")]
        public HttpResponseMessage RemovePL_ProductionOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PL_ProductionOrderEntity entity = JsonConvert.DeserializeObject<PL_ProductionOrderEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                var productOrder = entity.ProductOrder;
                //删除
                _WorkOrderBLL.RemoveForm(t => t.ProductOrder == productOrder);
                int isok = _ProductionOrderBLL.RemoveForm(Id, null);
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_ProductionOrderEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ProductionOrderBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_ProductionOrderEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ProductionOrderBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
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
                var list = _ProductionOrderBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
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
                var list = _ProductionOrderBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 功能描述: 审核下载
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_ProductionOrder_export")]
        public HttpResponseMessage PL_ProductionOrder_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();


                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string ProductOrder = "";
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    ProductOrder = queryParam["ProductOrder"].ToString();
                    var productOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == ProductOrder);
                    if (productOrderEntity != null)
                    {
                        //更新审核下载时间、人员
                        productOrderEntity.CDownloadUser = CurrentAccount.UserName;
                        productOrderEntity.CDownloadTime = DateTime.Now;
                        _ProductionOrderBLL.SaveEntity(productOrderEntity.Id, productOrderEntity, out msg);
                    }
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _ProductionOrderBLL.GetList_export(ProductOrder, out msg);


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
        /// 功能描述: 生产下载
        /// 创　　建: Dragon
        /// 创建日期: 2021-07-27 16:28:04
        /// 任务编号: 生产订单表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_ProductionOrder_export2")]
        public HttpResponseMessage PL_ProductionOrder_export2(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();


                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string ProductOrder = "";
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    ProductOrder = queryParam["ProductOrder"].ToString();
                    var productOrderEntity = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == ProductOrder);
                    if (productOrderEntity != null)
                    {
                        //更新审核下载时间、人员
                        productOrderEntity.PDownloadUser = CurrentAccount.UserName;
                        productOrderEntity.PDownloadTime = DateTime.Now;
                        _ProductionOrderBLL.SaveEntity(productOrderEntity.Id, productOrderEntity, out msg);
                    }
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _ProductionOrderBLL.GetList_export2(ProductOrder, out msg);

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

        #region Excel上传
        /// <summary>
        /// 漏板上传
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ImportExcelFile")]
        public HttpResponseMessage ImportExcelFile()
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var userName = CurrentAccount.UserName;
                //var fullName = CurrentAccount.FullName;
                var httpPostedFile = HttpContext.Current.Request.Files;
                if (httpPostedFile.Count < 1)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_21");//没有找到文件！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //只取第一个文件
                var fileUpload = httpPostedFile[0];

                string uploadPath = appLogPath + "ImportFile/Order";
                if (Directory.Exists(uploadPath) == false)
                {
                    Directory.CreateDirectory(uploadPath);
                }
                var filePath = Path.Combine(uploadPath, fileUpload.FileName);
                //string filePathNew = FileUploadHelper.GetNewPathForDupes(filePath);
                //将硬盘路径转化为服务器路径的文件流
                string fileName = Path.Combine(appLogPath, Path.GetFileName(fileUpload.FileName));
                //NPOI得到EXCEL的第一种方法              
                fileUpload.SaveAs(fileName);
                var dt1 = new ExcelHelper().ExcelToDataTable(fileName, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_22"), true);//生产订单
                var dt2 = new ExcelHelper().ExcelToDataTable(fileName, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_23"), true);//生产计划工单
                //得到EXCEL的第二种方法(第一个参数是文件流,第二个是excel标签名,第三个是第几行开始读0算第一行)
                //DataTable dtData2 = ExcelHelper.RenderDataTableFromExcel(fileName, "Sheet1", 0);

                //替换两列的值
                foreach (DataRow row in dt1.Rows)
                {
                    if (row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_24")].ToString() == "内销") row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_24")] = "1";//订单类型
                    else if (row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_25")].ToString() == "出口") row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_25")] = "2";//订单类型
                    else row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_26")] = "";//订单类型
                }
                foreach (DataRow row in dt2.Rows)
                {
                    if (row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_27")].ToString() == "否") row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_27")] = false;//是否免产
                    else if (row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_28")].ToString() == "是") row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_28")] = true;//是否免产
                    else row[Language.GetText("PlanManage.PL_ProductionOrderController.Tips_29")] = "";//是否免产
                }

                _ProductionOrderBLL.DataTableToSQLServer(dt1, dt2);

                System.IO.File.Delete(fileUpload.FileName);

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

        #region 生成物料需求
        [HttpPost]
        [Route("SaveMaterialRequirement")]
        public HttpResponseMessage SaveMaterialRequirement(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            result.resultData = null;
            var msg = "";
            var keyValue = getValue(jo, "KeyValue");
            var time = DateTime.Now;
            try
            {
                var entity = JsonConvert.DeserializeObject<PL_ProductionOrderEntity>(getValue(jo, "Entity"));

                var productOrder = entity.ProductOrder;
                var ordertype = entity.OrderType;

                //兼容后导入的工单
                //var ent = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == productOrder && t.OrderStatus == "1");
                var ent = _ProductionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == productOrder);
                if (ent != null)
                {
                    if (ent.OrderStatus == "1") //创建
                    {
                        entity.OrderStatus = "2";//审核
                    }
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = time;
                    entity.AuditBy = userCode;
                    entity.AuditName = userName;
                    entity.AuditTime = time;

                }

                //判断大小张转化jpf add 2023-1-5 
                var woLists = _WorkOrderBLL.Get_ExpressionList(t => t.ProductOrder == productOrder && t.OrderStatus == "1").ToList();
                if (woLists.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_35")); //没有要审核的工单

                foreach (var item in woLists)
                {
                    var WorkOrder = item.WorkOrder;
                    var PL_Material = new PL_Material_Service().Get_ExpressionEntity(t => t.WorkOrder == WorkOrder && t.IsDeleted == false);
                    if (PL_Material.IsEmpty())
                    {
                        result.success = false;
                        result.returnMsg = WorkOrder + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_30");//工单物料未生成，请去维护后重新导入
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    var PL_MaterialFacet = new PL_MaterialFacet_Service().Get_ExpressionEntity(t => t.MaterialId == PL_Material.Id && t.AttrCode == "DXZH");
                    if (string.IsNullOrEmpty(PL_MaterialFacet?.AttrValue))
                    {
                        result.success = false;
                        result.returnMsg = WorkOrder + Language.GetText("PlanManage.PL_ProductionOrderController.Tips_31");//工单无大小张转化属性，请先维护后重新导入
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                #region 正常工单
                var woList = woLists.Where(t => t.IsVC == false).ToList();
                if (woList.Count > 0)
                {
                    //1.生成bom
                    var bomQuery = from wo in woList
                                   join bom in _BOMBLL.Get_ExpressionList(t => t.OrderType == ordertype)
                                   on new { wo.FactoryCode, wo.MaterialCode, wo.Process } equals new { bom.FactoryCode, bom.MaterialCode, bom.Process }
                                   join material in _MaterialBLL.Get_ExpressionList(t => true)
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
                    //1.1 校验bom是否存在
                    if (woList.Count != bomList.Count)
                    {
                        foreach (var item in woList.GroupBy(t => t.MaterialCode))
                        {
                            if (bomList.Find(t => t.MaterialCode == item.Key) == null)
                            {
                                msg += item.Key + ";";
                            }
                        }
                        result.success = false;
                        result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_32") + msg;//物料BOM信息没有查到
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    foreach (var item in woList)
                    {
                        item.IsEnabled = true;
                        item.OrderStatus = "2";
                        item.ModifyBy = userCode;
                        item.ModifyTime = time;
                    }

                    // 2.生成bomItem
                    var bomItemQuery = from bom in bomList
                                       join bomItem in _BOMItemsBLL.Get_ExpressionList(t => true) on bom.BsBomId equals bomItem.BOMId
                                       join materail in _MaterialBLL.Get_ExpressionList(t => true) on bomItem.MaterialCode equals materail.MaterialCode
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
                                           bomItem.MaterialType,
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
                                       IsDeleted = false,
                                   };

                    var reqList = reqQuery.ToList();

                    if (reqList.Count < 1)
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("PlanManage.PL_ProductionOrderController.Tips_33");//物料BOM信息没有查到
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    //免产单可以审核但不生成物料需求
                    var arrWorkOrder = woList.Where(t => t.AvoidProduce == true).Select(t => t.WorkOrder).ToArray();
                    reqList = reqList.Where(t => !arrWorkOrder.Contains(t.WorkOrder)).ToList();

                    DataTable bomDt = Tools.ToDataTable(bomList);

                    bomDt.Columns.Remove("BsBomId");
                    //bomDt.Columns.Remove("FactoryCode");

                    DataTable bomItemDt = Tools.ToDataTable(bomItemList);
                    DataTable reqDt = Tools.ToDataTable(reqList);

                    #region 同步SAP
                    var workOrderList = _WorkOrderBLL.Get_ExpressionList(t => t.ProductOrder == productOrder && t.OrderStatus == "1").ToList();
                    var factoryCode = workOrderList.First().FactoryCode;

                    var SAPSyncSwitch = _keyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                         && t.Remark1 == factoryCode);
                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                        var plBOMList = bomDt.ToDataList<PL_BOMEntity>();
                        var sapResult = new ToSAPService().WorkOrderCreateToSAP(workOrderList, postUser, plBOMList);
                        if (!sapResult.Item1)
                            return AjaxResult(false, sapResult.Item2);
                    }
                    #endregion

                    using (TransactionScope ts = new TransactionScope())
                    {
                        if (ent != null) _ProductionOrderBLL.SaveEntity(keyValue, entity, out msg);
                        if (woList.Count > 0) _WorkOrderBLL.SaveEntity_List(true, null, woList, out msg);

                        if (bomDt.Rows.Count > 0) _PLBOMBLL.InsertDataTable(bomDt);
                        if (bomItemDt.Rows.Count > 0) _PLBOMItemsBLL.InsertDataTable(bomItemDt);
                        if (reqDt.Rows.Count > 0) _PrdOrderReqMaterialsBLL.InsertDataTable(reqDt);

                        ts.Complete();
                    }
                }
                #endregion

                #region VC工单
                //vc 工单生成bom和物料需求 jpf add
                var woVCList = woLists.Where(t => t.MMXH != null && t.Spec != null && t.UV != null && t.KCKX != null && t.IsVC == true).ToList();
                if (woVCList.Count > 0)
                {
                    msg = new PL_ProductionOrder_Service().SaveMaterialRequirement(entity, userCode);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        result.success = false;
                        result.returnMsg = msg;
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                #endregion

                if (woList.Count == 0 && woVCList.Count == 0)
                    return AjaxResult(false, Language.GetText("PlanManage.PL_ProductionOrderController.Tips_34"));//没有符合条件的工单

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
        #endregion

    }
}
