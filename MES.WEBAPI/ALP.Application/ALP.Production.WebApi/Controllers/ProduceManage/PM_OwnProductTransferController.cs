using System;
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
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.UtilExtend.Util;
using System.Web;
using System.IO;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Service.BaseManage;
using Language = ALP.Application.Service.Resources.Language;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_OwnProductOrderController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 自制半成品工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_OwnProductTransfer")]
    public class PM_OwnProductTransferController : ApiBaseController
    {
        private PM_OwnProductTransferBLL _OwnProductTransferBLL = new PM_OwnProductTransferBLL();
        private PM_OwnProductOrderBLL _OwnProductOrderBLL = new PM_OwnProductOrderBLL();
        private MM_RawMaterialStockBLL _rawMaterialStockBLL = new MM_RawMaterialStockBLL();
        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();//工单物料
        private PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();//工单工艺路线
        private PL_ProcessOfOperationsBLL _plProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();//工单工艺-工序
        private PL_ProcessOfOperationsAttrBLL _plProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();//工单工艺-工序-属性
        private BsModelWithResourceBLL _bsModelWithResourceBLL = new BsModelWithResourceBLL(); //工厂建模
        BaseSequenceService _baseSequenceService = new BaseSequenceService();//序列号

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_OwnProductTransferPageList")]
        public HttpResponseMessage PM_OwnProductTransferPageList(JObject jo)
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

                var data = _OwnProductTransferBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_OwnProductTransferPageDataTableList")]
        public HttpResponseMessage PM_OwnProductTransferPageDataTableList(JObject jo)
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

                var data = _OwnProductTransferBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_OwnProductTransferList")]
        public HttpResponseMessage GetPM_OwnProductTransferList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _OwnProductTransferBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_OwnProductTransfer")]
        public HttpResponseMessage SavePM_OwnProductTransfer(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var time = DateTime.Now;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            var printNum = int.Parse(getValue(jo, "PrintNum"));


            try
            {
                //业务服务类

                //参数转实体
                PM_OwnProductTransferEntity entity = JsonConvert.DeserializeObject<PM_OwnProductTransferEntity>(getValue(jo, "Entity"));

                string msg = "";
                int isok = _OwnProductTransferBLL.SaveEntity(keyValue, entity, out msg);
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

        #region 流转卡打印
        /// <summary>
        /// 功能描述: 流转卡打印
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_OwnProductTransfer")]
        public HttpResponseMessage SaveBatchPM_OwnProductTransfer(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult();

            var time = DateTime.Now;
            var timeStr = time.ToString("yyMMdd");
            var timeStamp = Convert.ToDecimal(Tools.DateTimeToTimeStamp(time));
            var timedate = time.ToString("yy-MM-dd");
            var timedate1 = time.ToString("yyyy-MM-dd");

            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            var printNum = int.Parse(getValue(jo, "PrintNum"));
            var returnNum = "";
            try
            {
                _OwnProductTransferBLL.GetSerialNO("OwnProductTransfer", printNum, out returnNum, out msg);
                var index = int.Parse(returnNum);
                var entity = JsonConvert.DeserializeObject<PM_OwnProductTransferEntity>(getValue(jo, "Entity"));

                var list = new List<PM_OwnProductTransferEntity>();
                if (string.IsNullOrEmpty(keyValue))
                {
                    string batchNo = "";
                    //var batchStr = timedate.Replace("-", "").Substring(0);

                    //Dictionary<string, object> dic1 = new Dictionary<string, object>();
                    //dic1.Add("batchDate", timedate1);
                    //var rawMaterialStockList = _rawMaterialStockBLL.GetList(dic1, out msg);
                    //if (rawMaterialStockList.Count() > 0)
                    //{
                    //    var batchList = rawMaterialStockList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
                    //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4)
                    //    .ToInt()).Distinct().ToList();
                    //    batchNo = batchStr + "-" + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                    //}
                    //else
                    //{
                    //    batchNo = batchStr + "-0001";
                    //}

                    //var batchEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == batchNo);
                    //if (batchEntity != null)
                    //{
                    //    var oldBatchDateNo = batchEntity.BatchNo.Substring(0, 6);
                    //    var oldBatchList = _rawMaterialStockBLL.Get_ExpressionList(t => t.BatchNo.Substring(0, 6) == oldBatchDateNo);
                    //    var batchList = oldBatchList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
                    //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4).ToInt()).Distinct().ToList();
                    //    batchNo = batchStr + "-" + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                    //}

                    //批次新逻辑
                    string serialNo = _baseSequenceService.GetSerialNO("OwnProductBatch");
                    //batchNo = "B" + timeStr + "-" + serialNo;
                    //batchNo = "B" + timeStr + serialNo;
                    batchNo = new BaseSequenceService().GetBatch("B" + timeStr);

                    //var OwnProductOrder = _OwnProductOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder);
                    //var plMaterialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted == false);
                    //if (plMaterialEntity == null)
                    //    return AjaxResult(false, "工单物料不存在");

                    //var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == entity.WorkOrder && t.IsDeleted ==false);
                    //if (plProcessEntity == null)
                    //    return AjaxResult(false, "工单工艺路线不存在");

                    //var ProcessId = plProcessEntity == null ? "" : plProcessEntity.Id;
                    //var plOperationList = _plProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == ProcessId).OrderBy(t => t.SN).ToList();
                    //if (plOperationList.Count == 0)
                    //    return AjaxResult(false, "工单工艺路线-工序不存在");

                    //var ProcessOfOperationsAttrlist = new List<PL_ProcessOfOperationsAttrEntity>();

                    //var query = from a in _plProcessOfOperationsAttrBLL.Get_ExpressionList(t => true)
                    //            join b in plOperationList on a.ProcessId equals b.ProcessId
                    //            select a;
                    //ProcessOfOperationsAttrlist = query.ToList();
                    //if (ProcessOfOperationsAttrlist.Count == 0)
                    //    return AjaxResult(false, "工单工艺路线-属性不存在");

                    //var attrvalue = ProcessOfOperationsAttrlist.Find(t => t.AttrCode == "BGKW");
                    //if (attrvalue == null)
                    //    return AjaxResult(false, "工艺路线属性【BGKW】不存在");

                    //if (string.IsNullOrEmpty(attrvalue.AttrValue))
                    //    return AjaxResult(false, "报工库位没维护");

                    //var bsModel = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == attrvalue.AttrValue);
                    //MM_RawMaterialStockEntity MM_RawMaterialStock = new MM_RawMaterialStockEntity();
                    //MM_RawMaterialStock.Id = Guid.NewGuid().ToString();
                    //MM_RawMaterialStock.FactoryCode = entity.FactoryCode;
                    //MM_RawMaterialStock.FactoryName = entity.FactoryName;
                    //MM_RawMaterialStock.MaterialCode = OwnProductOrder.MaterialCode;
                    //MM_RawMaterialStock.MaterialName = OwnProductOrder.MaterialName;
                    //MM_RawMaterialStock.BatchNo = batchNo;
                    //MM_RawMaterialStock.Qty = 0;
                    //MM_RawMaterialStock.Unit = plMaterialEntity.Unit;
                    //MM_RawMaterialStock.SupplierCode = "";
                    //MM_RawMaterialStock.WhsCode = bsModel.ParentResource;
                    //MM_RawMaterialStock.LocationCode = bsModel.ResourceCode;
                    //MM_RawMaterialStock.IsFrozen = "0";
                    //MM_RawMaterialStock.Creator = userCode;
                    //MM_RawMaterialStock.CreateTime = DateTime.Now;
                    //MM_RawMaterialStock.BatchDate = timedate1.ToDate();
                    //_rawMaterialStockBLL.SaveEntity("", MM_RawMaterialStock, out msg);
                    for (var i = 1; i <= printNum; i++)
                    {
                        var transferName = timeStr + index.ToString().PadLeft(4, '0');
                        var transferCode = "Z" + timeStr + index.ToString().PadLeft(4, '0');
                        list.Add(new PM_OwnProductTransferEntity
                        {
                            Id = Guid.NewGuid().ToString(),
                            FactoryCode = entity.FactoryCode,
                            FactoryName = entity.FactoryName,
                            WorkOrder = entity.WorkOrder,
                            TransferBatch = timeStamp,
                            BatchNumber = batchNo,
                            TransferCode = transferCode,
                            TransferName = transferName,
                            TransferStatus = "1",
                            UserNames = entity.UserNames,
                            Creator = userCode,
                            CreateTime = time,
                            CardStatus = "1"
                        });
                        index++;
                    }
                }

                var isok = _OwnProductTransferBLL.SaveEntity_List(false, userCode, list, out msg);

                #region 打印
                //string fileName = HttpContext.Current.Server.MapPath("~/") + @Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_10");//Template/自制半成品.frx
                string fileName = HttpContext.Current.Server.MapPath("~/") + "Template/自制半成品.frx";
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var cardCodes = string.Join(",", list.Select(t => t.TransferCode));
                dic.Add("CardCode", cardCodes);
                var resultData = PrintToPDF(fileName, dic);
                #endregion
                result.resultData = resultData;
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
        #endregion

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_OwnProductTransfer")]
        public HttpResponseMessage DeletePM_OwnProductTransfer(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_OwnProductTransferEntity entity = JsonConvert.DeserializeObject<PM_OwnProductTransferEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_OwnProductTransferEntity model = _OwnProductTransferBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _OwnProductTransferBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_13");//删除操作成功
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_OwnProductTransfer")]
        public HttpResponseMessage RemovePM_OwnProductTransfer(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_OwnProductTransferEntity entity = JsonConvert.DeserializeObject<PM_OwnProductTransferEntity>(getValue(jo, "Entity"));

                //删除
                int isok = _OwnProductTransferBLL.RemoveForm(t => t.Id == entity.Id);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_13");//删除操作成功
                else
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_15");//删除操作失败
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_OwnProductTransferEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _OwnProductTransferBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_16");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_OwnProductTransferEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _OwnProductTransferBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_16");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
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

                var data = _OwnProductTransferBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
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
                var list = _OwnProductTransferBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
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
                var list = _OwnProductTransferBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_OwnProductTransfer_export")]
        public HttpResponseMessage PM_OwnProductTransfer_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductTransferController.Tips_7");//缺少Entity参数！
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
                var data = _OwnProductTransferBLL.GetList_export(CreatedByCode, out msg);

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

        //
        /// <summary>
        /// 补打打印流转卡
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("PrintPM_OwnProductTransfer")]
        public HttpResponseMessage PrintPM_OwnProductTransfer(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var list = JsonConvert.DeserializeObject<List<PM_OwnProductTransferEntity>>(getValue(jo, "data"));

                string fileName = HttpContext.Current.Server.MapPath("~/") + @"Template/自制半成品.frx";//Template/自制半成品.frx
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var cardCodes = string.Join(",", list.Select(t => t.TransferCode));
                dic.Add("CardCode", cardCodes);
                var resultData = PrintToPDF(fileName, dic);
                result.resultData = resultData;
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
    }
}
