using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.MaterialManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using System.Transactions;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Service.ProduceManage;
using ALP.Application.UtilExtend.Util;
using System.Data;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-10-13
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductDispatchBillController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 成品发货单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_ProductDispatchBill")]
    public class MM_ProductDispatchBillController : ApiBaseController
    {
        MM_ProductDispatchItem_Service _dispatchItemService = new MM_ProductDispatchItem_Service();//发货单明细
        MM_ProductDispatchDetail_Service _dispatchDetailService = new MM_ProductDispatchDetail_Service();//发货单详情
        PL_WorkOrderBLL _workOrderBLL = new PL_WorkOrderBLL();//工单
        PL_ProductionOrderBLL _productionOrderBLL = new PL_ProductionOrderBLL();//订单
        MM_ProductStock_Service _productStockService = new MM_ProductStock_Service();//成品库存
        PM_PackingPrintMark_Service _markService = new PM_PackingPrintMark_Service();//唛头信息

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductDispatchBillPageList")]
        public HttpResponseMessage MM_ProductDispatchBillPageList(JObject jo)
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
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductDispatchBillPageDataTableList")]
        public HttpResponseMessage MM_ProductDispatchBillPageDataTableList(JObject jo)
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
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_ProductDispatchBillList")]
        public HttpResponseMessage GetMM_ProductDispatchBillList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_ProductDispatchBill")]
        public HttpResponseMessage SaveMM_ProductDispatchBill(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //业务服务类
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                //参数转实体
                MM_ProductDispatchBillEntity entity = JsonConvert.DeserializeObject<MM_ProductDispatchBillEntity>(getValue(jo, "Entity"));
                //发货单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.DocNum))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_7");//发货单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Customer))
                {
                    //客户 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_8");//客户不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = CurrentAccount.UserCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.Creator = CurrentAccount.UserCode;
                    entity.CreateTime = DateTime.Now;
                }

                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_ProductDispatchBill")]
        public HttpResponseMessage SaveBatchMM_ProductDispatchBill(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_11");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_12");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<MM_ProductDispatchBillEntity> Insert_entity_list = new List<MM_ProductDispatchBillEntity>();
                //更新数组
                List<MM_ProductDispatchBillEntity> Update_entity_list = new List<MM_ProductDispatchBillEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        MM_ProductDispatchBillEntity entity = new MM_ProductDispatchBillEntity();
                        //发货单号
                        entity.DocNum = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_13")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_13")];//发货单号
                        //客户
                        entity.Customer = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_14")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_14")];//客户
                        //发货单状态
                        entity.Status = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_15")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_15")];//发货单状态
                        //创建人
                        entity.Creator = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_16")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_16")];//创建人
                        //创建时间
                        entity.CreateTime = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_17")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_17")];//创建时间
                        //最后修改人
                        entity.ModifyBy = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_18")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_18")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_19")] == null ? "" : item[Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_19")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;


                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        MM_ProductDispatchBillEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_ProductDispatchBill")]
        public HttpResponseMessage DeleteMM_ProductDispatchBill(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                MM_ProductDispatchBillEntity entity = JsonConvert.DeserializeObject<MM_ProductDispatchBillEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductDispatchBillEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_22");//删除操作成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_ProductDispatchBill")]
        public HttpResponseMessage RemoveMM_ProductDispatchBill(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                MM_ProductDispatchBillEntity entity = JsonConvert.DeserializeObject<MM_ProductDispatchBillEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductDispatchBillEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
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
                    //删除
                    _Service.RemoveForm(Id, userName);
                    _dispatchItemService.RemoveForm(t => t.DocNum == model.DocNum);
                    _dispatchDetailService.RemoveForm(t => t.DocNum == model.DocNum);

                    ts.Complete();
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
        /// 功能描述: 获取实体
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductDispatchBillEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_24");//获取详情数据成功
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
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductDispatchBillEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_24");//获取详情数据成功
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
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
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
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                string OutMes = "";
                var list = _Service.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                string OutMes = "";
                var list = _Service.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 成品发货单
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_ProductDispatchBill_export")]
        public HttpResponseMessage MM_ProductDispatchBill_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                string msg = Language.GetText("Common.SearchSuccess");//查询成功
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
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:00:19
        /// 任务编号: 创建发货单信息
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDispatchBill")]
        public HttpResponseMessage SaveDispatchBill(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var msg = "";
                string customer = "";//客户
                List<MM_ProductDispatchDetailEntity> lstDispatchDetail = new List<MM_ProductDispatchDetailEntity>();

                //业务服务类
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                var deliveryType = getValue(jo, "DeliveryType");//发货类型
                var deliveryDate = getValue(jo, "DeliveryDate");//发货日期
                var remark = getValue(jo, "Remark");//备注
                //参数转实体
                var lstDispatchItem = JsonConvert.DeserializeObject<List<MM_ProductDispatchItemEntity>>(getValue(jo, "Entity"));

                //流水号
                var returnNum = "";
                _Service.GetSerialNO("DispatchBill", 1, out returnNum, out msg);
                var docNum = "FHD" + DateTime.Now.ToString("yyMMdd") + returnNum;

                #region 发货单明细(柜)
                foreach (var item in lstDispatchItem)
                {
                    var workOrderEntity = _workOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == item.WorkOrder);//工单实体
                    var productOrderEntity = _productionOrderBLL.Get_ExpressionEntity(t => t.ProductOrder == workOrderEntity.ProductOrder);//订单实体
                    if (string.IsNullOrEmpty(customer))
                    {
                        customer = productOrderEntity.Customer;
                    }
                    else if (customer != productOrderEntity.Customer)
                    {
                        result.success = false;
                        result.returnMsg = $"不同的客户不能放在一个发货单上";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    var productStockList = _productStockService.Get_ExpressionList(t => t.WorkOrder == item.WorkOrder);//成品库存
                    var markList = _markService.Get_ExpressionList(t => t.WorkOrder == item.WorkOrder).ToList();//唛头信息

                    foreach (var stock in productStockList)
                    {
                        var mark = markList.Find(t => t.PackTransferCode == stock.MarkCode);
                        if (mark.Status != "2")
                        {
                            result.success = false;
                            result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_27", mark.Mark);//唛头[{mark.Mark}]处于非入库状态，无法创建发货单
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        //成品发货详情
                        lstDispatchDetail.Add(new MM_ProductDispatchDetailEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            DocNum = docNum,
                            WorkOrder = stock.WorkOrder,
                            ContainerNO = stock.ContainerNO,
                            CustomerPO = stock.CustomerPO,
                            MarkCode = stock.MarkCode,
                            MaterialCode = stock.MaterialCode,
                            BoxQty = stock.BoxQty,
                            PieceQty = stock.PieceQty,
                            WhsCode = stock.WhsCode,
                            LocationCode = stock.LocationCode,
                            Creator = userCode,
                            CreateTime = DateTime.Now
                        });
                    }

                    item.Id = Guid.NewGuid().ToString();
                    item.DocNum = docNum;
                    item.PalletQty = productStockList.Count();
                    item.BoxQty = productStockList.Sum(t => t.BoxQty);
                    item.Status = "1";//未发货
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                }
                #endregion

                #region 发货单主表
                MM_ProductDispatchBillEntity dispatchBillEntity = new MM_ProductDispatchBillEntity();
                dispatchBillEntity.Id = Guid.NewGuid().ToString();
                dispatchBillEntity.DocNum = docNum;
                dispatchBillEntity.Customer = customer;
                dispatchBillEntity.Status = "1";//未发货
                dispatchBillEntity.Creator = userCode;
                dispatchBillEntity.CreateTime = DateTime.Now;
                dispatchBillEntity.DeliveryType = deliveryType;
                dispatchBillEntity.Remark = remark;
                dispatchBillEntity.DeliveryDate = deliveryDate.ToDate();
                dispatchBillEntity.Operator = userName;
                #endregion


                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _Service.SaveEntity("", dispatchBillEntity, out msg);//发货单
                    _dispatchItemService.SaveEntity_List(false, userName, lstDispatchItem, out msg);//发货明细（柜）
                    _dispatchDetailService.SaveEntity_List(false, userName, lstDispatchDetail, out msg);//发货详情（托）

                    ts.Complete();
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
        /// 生产发货单
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDispatchBillItem")]
        public HttpResponseMessage SaveDispatchBillItem(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ProductDispatchBillController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var msg = "";
                //流水号
                var returnNum = "";
                MM_ProductDispatchBill_Service _Service = new MM_ProductDispatchBill_Service();
                List<MM_ProductDispatchDetailEntity> dispatchDetailList = new List<MM_ProductDispatchDetailEntity>();

                var data = JsonConvert.DeserializeObject<List<MM_ProductDispatchItemEntity>>(getValue(jo, "data"));

                _Service.GetSerialNO("DispatchBill", data.Count, out returnNum, out msg);
                var index = int.Parse(returnNum);
                string docNum = "FHD" + DateTime.Now.ToString("yyMMdd") + (index++).ToString().PadLeft(3, '0'); //发货单号

                List<MM_ProductDispatchItemEntity> disaptchList = new List<MM_ProductDispatchItemEntity>();
                foreach (var item in data.GroupBy(t => new { t.ProductOrder, t.ContainerNO, t.CustomerPO }))
                {
                    #region 发货单主表
                    var main = new MM_ProductDispatchItemEntity();
                    main.Id = Guid.NewGuid().ToString();
                    main.DocNum = docNum;
                    main.FactoryCode = item.First().FactoryCode;
                    main.FactoryName = item.First().FactoryName;
                    main.ProductOrder = item.Key.ProductOrder;
                    main.ContainerNO = item.Key.ContainerNO;
                    main.CustomerPO = item.Key.CustomerPO;
                    main.DeliveryType = item.First().DeliveryType;
                    main.DeliveryDate = item.First().DeliveryDate;
                    main.PalletQty = item.Sum(t => t.PalletQty);
                    main.Qty = item.Sum(t => t.Qty);
                    main.BoxNum = item.Sum(t => t.BoxNum);
                    main.GrossWeight = item.Sum(t => t.GrossWeight);
                    main.Volume = item.Sum(t => t.Volume);
                    main.LoadingBill = item.First().LoadingBill;
                    main.InvoiceNO = item.First().InvoiceNO;
                    main.Remark = item.First().Remark;
                    main.Status = "1";
                    main.CreateTime = time;
                    main.Creator = userCode;
                    main.Operator = userName;
                    disaptchList.Add(main);
                    #endregion

                    #region 发货单明细
                    var arrWorkOrder = item.Select(t => t.WorkOrder).ToArray();
                    var workOrderList = _workOrderBLL.Get_ExpressionList(t => arrWorkOrder.Contains(t.WorkOrder)).ToList();
                    int i = 1;
                    foreach (var detail in item)
                    {
                        var workOrderEntity = workOrderList.Find(t => t.WorkOrder == detail.WorkOrder);
                        if (workOrderEntity == null)
                            return AjaxResult(false, $"工单【{detail.WorkOrder}】不存在");

                        MM_ProductDispatchDetailEntity detailEntity = new MM_ProductDispatchDetailEntity();
                        detailEntity.Id = Guid.NewGuid().ToString();
                        detailEntity.DispatchItemId = main.Id;
                        detailEntity.FactoryCode = workOrderEntity.FactoryCode;
                        detailEntity.FactoryName = workOrderEntity.FactoryName;
                        detailEntity.DocNum = docNum;
                        detailEntity.LineNum = i.ToString();
                        detailEntity.ProductOrder = workOrderEntity.ProductOrder;
                        detailEntity.ContainerNO = workOrderEntity.ContainerNO;
                        detailEntity.CustomerPO = workOrderEntity.CustomerPO;
                        detailEntity.MaterialCode = workOrderEntity.MaterialCode;
                        detailEntity.PalletQty = detail.PalletQty;
                        detailEntity.PieceQty = detail.Qty;
                        detailEntity.Creator = userCode;
                        detailEntity.CreateTime = DateTime.Now;
                        detailEntity.WorkOrder = workOrderEntity.WorkOrder;
                        detailEntity.ProductLine = workOrderEntity.Orderline;
                        detailEntity.DetailGrossWeight = detail.GrossWeight;
                        detailEntity.DetailVolume = detail.Volume;
                        dispatchDetailList.Add(detailEntity);

                        i += 1;
                    }
                    #endregion
                }
                //DataTable dt = Tools.ToDataTable(lstDispatchItem);
                //_ItemService.InsertDataTable(dt);

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _dispatchItemService.SaveEntity_List(false, userName, disaptchList, out msg);
                    _dispatchDetailService.SaveEntity_List(false, userName, dispatchDetailList, out msg);

                    ts.Complete();
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

                var data = _workOrderBLL.GetWorkOrderDataTable(pagination, queryJson);
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
    }
}
