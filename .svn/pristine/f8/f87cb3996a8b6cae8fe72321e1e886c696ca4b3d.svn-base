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
using System.Transactions;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.Busines.PlanManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ReworkRecordController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: PM_生产返工记录主表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_ReworkRecord")]
    public class PM_ReworkRecordController : ApiBaseController
    {
        PM_ReworkRecord_Service _pmReworkRecordService = new PM_ReworkRecord_Service(); //返工任务主表
        PM_ReworkRecord_Detail_Service _detailService = new PM_ReworkRecord_Detail_Service();
        PM_TransferCardBLL _transferCardBLL = new PM_TransferCardBLL();//流转卡
        PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();//工单工艺路线
        PL_ProcessOfOperationsBLL _plProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();//工单工艺-工序
        PM_TransferCardResumeBLL _resumeBLL = new PM_TransferCardResumeBLL();//流转履历
        PM_OwnProductTransferBLL _OwnProductTransferBLL = new PM_OwnProductTransferBLL();//自制半成品
        PM_TranferCardBGRecord_Service _cardBGService = new PM_TranferCardBGRecord_Service();//流转卡报工记录

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ReworkRecordPageList")]
        public HttpResponseMessage PM_ReworkRecordPageList(JObject jo)
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
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                var data = _Service.GetPageList(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ReworkRecordPageDataTableList")]
        public HttpResponseMessage PM_ReworkRecordPageDataTableList(JObject jo)
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
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                var data = _Service.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_ReworkRecordList")]
        public HttpResponseMessage GetPM_ReworkRecordList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_ReworkRecord")]
        public HttpResponseMessage SavePM_ReworkRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                string msg = "";
                //业务服务类
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                //参数转实体
                PM_ReworkRecordEntity entity = JsonConvert.DeserializeObject<PM_ReworkRecordEntity>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;

                    #region 业务逻辑
                    var sourceReworkEntity = _pmReworkRecordService.Get_ExpressionEntity(t => t.Id == keyValue);
                    if (sourceReworkEntity.ReworkProcess != entity.ReworkProcess)
                    {
                        //工艺路线
                        var workOrder = sourceReworkEntity.WorkOrder;
                        var reworkProcess = entity.ReworkProcess;
                        var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                        var plOperationList = _plProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == plProcessEntity.Id).ToList();
                        var reworkOperationEntity = plOperationList.Find(t => t.OperationCode == reworkProcess);
                        if (reworkOperationEntity == null)
                            return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_8"));//返工工序不在工艺路线里

                        if (sourceReworkEntity.ReworkProductType == "1") //正常
                        {
                            var reworkDetailList = _detailService.Get_ExpressionList(t => t.ReworkId == keyValue && t.ReworkStatus=="1").ToList();
                            var arrCardCode = reworkDetailList.Select(t => t.CardCode);
                            var cardList = _transferCardBLL.Get_ExpressionList(t => arrCardCode.Contains(t.CardCode)).ToList();
                            foreach (var item in cardList)
                            {
                                if (item.CardStatus == "3")
                                {
                                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_9"));//流转卡处于返工中
                                }
                            }

                            var resumeList = _resumeBLL.Get_ExpressionList(t => t.Flag == "1" && arrCardCode.Contains(t.CardCode)).ToList();
                            if (resumeList.Count == 0)
                                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_10"));//找不到流转卡所在工序

                            int k = 0;
                            //流转卡报工记录
                            var cardBGList = _cardBGService.Get_ExpressionList(t => t.ProcessCode == reworkProcess && t.IsRework == "0" && arrCardCode.Contains(t.CardCode)).ToList();
                            foreach (var item in resumeList)
                            {
                                var itemOpeartion = plOperationList.Find(t => t.OperationCode == item.ProcessCode);
                                if (reworkOperationEntity.SN < itemOpeartion.SN)
                                    return AjaxResult(false, "流转卡已到工序[" + item.ProcessCode + ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_11"));//]，修改返工工序失败
                                else if (reworkOperationEntity.SN == itemOpeartion.SN)
                                {
                                    if (!cardBGList.Any(t => t.CardCode == item.CardCode))
                                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_12"));//返工任务中的流转卡未全部报工
                                    else
                                        k += 1;
                                }
                            }
                            if (k == resumeList.Count) //返工任务中所有流转卡均已经报工
                            {
                                foreach (var item in cardList)
                                {
                                    item.CardStatus = "3";//返工中
                                    item.ModifyBy = userCode;
                                    item.ModifyTime = DateTime.Now;
                                    item.SerialNumber = keyValue;
                                }
                                _transferCardBLL.SaveEntity_List(true, userName, cardList, out msg);
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.Creator = userCode;
                    entity.IsEnabled = true;
                }

                int isok = _Service.SaveEntity(keyValue, entity, out msg);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 质量确认
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("CheckRecord")]
        public HttpResponseMessage CheckRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //业务服务类
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();

                string keyValue = getValue(jo, "KeyValue");
                var taskEntity = _Service.Get_ExpressionEntity(t => t.Id == keyValue);
                if (taskEntity.Status != "3")
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_15");//返工任务未完成，不可以确认
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (taskEntity.ConfirmStatus == "1")
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_16");//质量已经确认,不允许二次确认
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                #region 返工任务

                taskEntity.ConfirmStatus = "1";//已确认
                taskEntity.QualityConfirmUser = userName;
                taskEntity.QualityConfirmTime = DateTime.Now;
                #endregion

                #region 更新流转卡状态
                var cardList = _transferCardBLL.Get_ExpressionList(t => t.SerialNumber == taskEntity.Id).ToList();
                if (cardList.Count > 0)
                {
                    foreach (var item in cardList)
                    {
                        item.CardStatus = "4";//已返工
                        item.ModifyBy = userCode;
                        item.ModifyTime = DateTime.Now;
                    }
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
                    _Service.SaveEntity(keyValue, taskEntity, out msg);
                    if (cardList.Count > 0)
                        _transferCardBLL.SaveEntity_List(true, userName, cardList, out msg);

                    ts.Complete();
                }
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_ReworkRecord")]
        public HttpResponseMessage SaveBatchPM_ReworkRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_18");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_19");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<PM_ReworkRecordEntity> Insert_entity_list = new List<PM_ReworkRecordEntity>();
                //更新数组
                List<PM_ReworkRecordEntity> Update_entity_list = new List<PM_ReworkRecordEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_ReworkRecordEntity entity = new PM_ReworkRecordEntity();
                        //返工单号
                        entity.ReworkOrder = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_20")];//返工单号
                        //订单号
                        entity.ProductOrder = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_21")];//订单号
                        //工单号
                        entity.WorkOrder = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_22")];//工单号
                        //执行工单号
                        entity.ExeWorkOrder = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_23")];//执行工单号
                        //当前工序
                        entity.CurrentProcess = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_24")];//当前工序
                        //责任工序
                        entity.DutyProcess = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_25")];//责任工序
                        //返工工序
                        entity.ReworkProcess = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_26")];//返工工序
                        //返工状态
                        entity.Status = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_27")];//返工状态
                        //确认状态
                        entity.ConfirmStatus = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_28")];//确认状态
                        //质量确认人
                        entity.QualityConfirmUser = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_29")];//质量确认人
                        //质量确认时间
                        entity.QualityConfirmTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_30")];//质量确认时间
                        //创建人编码
                        entity.Creator = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_31")];//创建人编码
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_32")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_32")];//创建时间
                        //修改人编码
                        entity.ModifyBy = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_33")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_33")];//修改人编码
                        //修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_34")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_34")];//修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];


                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_ReworkRecordEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
                        //判断旧列表中是否存在此编码
                        if (old_entity == null)
                        {
                            entity.Create();
                            //保存数组
                            Insert_entity_list.Add(entity);
                        }
                        else
                        {
                            entity.Id = old_entity.Id;
                            Update_entity_list.Add(entity);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (Insert_entity_list.Count > 0)
                {
                    //批量新增
                    isok = _Service.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _Service.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
                }

                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_ReworkRecord")]
        public HttpResponseMessage DeletePM_ReworkRecord(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                PM_ReworkRecordEntity entity = JsonConvert.DeserializeObject<PM_ReworkRecordEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ReworkRecordEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, userName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_37");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_ReworkRecord")]
        public HttpResponseMessage RemovePM_ReworkRecord(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg = "";
            int isok = 0;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                PM_ReworkRecordEntity entity = JsonConvert.DeserializeObject<PM_ReworkRecordEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ReworkRecordEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var query = from detail in _detailService.Get_ExpressionList(t => t.ReworkId == Id)
                            join trEntity in _transferCardBLL.Get_ExpressionList(t => true)
                            on detail.CardCode equals trEntity.CardCode
                            select trEntity;

                var list = query.ToList();
                list.ForEach(t =>
                {
                    t.CardStatus = "1";
                    t.ModifyBy = userCode;
                    t.ModifyTime = DateTime.Now;
                });

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    isok = _detailService.RemoveForm(t => t.ReworkId == Id);
                    if (list.Count > 0) _transferCardBLL.SaveEntity_List(true, null, list, out msg);
                    //删除
                    isok += _Service.RemoveForm(t => t.Id == Id);
                    ts.Complete();
                }

                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_37");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_39");//删除操作失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ReworkRecordEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_40");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ReworkRecordEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_40");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
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
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.IsEnabled == true).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                string OutMes = "";
                var list = _Service.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                string OutMes = "";
                var list = _Service.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-19 17:05:41
        /// 任务编号: PM_生产返工记录明细
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_ReworkRecord_export")]
        public HttpResponseMessage PM_ReworkRecord_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ReworkRecordController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                PM_ReworkRecord_Service _Service = new PM_ReworkRecord_Service();
                string msg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _Service.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


    }
}
