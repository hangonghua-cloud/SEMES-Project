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
using ALP.Application.Service.PlanManage;
using ALP.Application.Entity.PlanManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-03
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ExeWorkOrderSWController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 派工执行工单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_ExeWorkOrderSW")]
    public class PM_ExeWorkOrderSWController : ApiBaseController
    {

        PL_WorkOrder_Service _plWorkOrderService = new PL_WorkOrder_Service();//工单
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ExeWorkOrderSWPageList")]
        public HttpResponseMessage PM_ExeWorkOrderSWPageList(JObject jo)
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
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ExeWorkOrderSWPageDataTableList")]
        public HttpResponseMessage PM_ExeWorkOrderSWPageDataTableList(JObject jo)
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
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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
        /// 功能描述: 查询（挤出、开槽工序）未开工、正在生产、未派工的执行工单
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetExeWorkOrderDataTableList")]
        public HttpResponseMessage GetExeWorkOrderDataTableList(JObject jo)
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
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                var data = _Service.GetExeWorkOrderDataTableList(pagination, queryJson);
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
        /// 功能描述: 查询工单信息
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetExeWorkOrderInfo")]
        public HttpResponseMessage GetExeWorkOrderInfo(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                //string exeWorkOrder = getValue(jo, "exeWorkOrder");
                string workOrder = getValue(jo, "workOrder");
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                string msg = "";
                var data = _Service.GetExeWorkOrderInfo(workOrder, out msg);

                result.resultData = data;
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_ExeWorkOrderSWList")]
        public HttpResponseMessage GetPM_ExeWorkOrderSWList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_ExeWorkOrderSW")]
        public HttpResponseMessage SavePM_ExeWorkOrderSW(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //业务服务类
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                //参数转实体
                PM_ExeWorkOrderSWEntity entity = JsonConvert.DeserializeObject<PM_ExeWorkOrderSWEntity>(getValue(jo, "Entity"));
                //订单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                //if (string.IsNullOrEmpty(entity.ProductOrder))
                //{
                //    result.success = false;
                //    result.returnMsg = "订单号不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}
                //else if (string.IsNullOrEmpty(entity.WorkOrder))
                //{
                //    //工单号 是否为空进行判断
                //    result.success = false;
                //    result.returnMsg = "工单号不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}
                //else if (string.IsNullOrEmpty(entity.ExeWorkOrder))
                //{
                //    //执行工单号 是否为空进行判断
                //    result.success = false;
                //    result.returnMsg = "执行工单号不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}
                //else if (string.IsNullOrEmpty(entity.ProcessCode))
                //{
                //    //工序编码 是否为空进行判断
                //    result.success = false;
                //    result.returnMsg = "工序编码不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}
                //else 
                if (string.IsNullOrEmpty(entity.EquipCode))
                {
                    //机台编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_8");//机台编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (entity.PlanProductTime == null)
                {
                    //计划生产时间 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_9");//计划生产时间不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyTime = DateTime.Now;
                    entity.ModifyBy = CurrentAccount.UserCode;
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.Creator = CurrentAccount.UserCode;

                }

                string msg = "";
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
        /// 功能描述: 批量（新增）
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("InsertBatchPM_ExeWorkOrderSW")]
        public HttpResponseMessage InsertBatchPM_ExeWorkOrderSW(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var entity_list = JsonConvert.DeserializeObject<List<PM_ExeWorkOrderSWEntity>>(getValue(jo, "data"));
                var machineCode = getValue(jo, "MachineCode");
                var planProductTime = getValue(jo, "PlanProductTime");

                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                PL_ExeWorkOrder_Service _ExeWorkOrder_Service = new PL_ExeWorkOrder_Service();
                string msg = "";
                int isok = 1;

                if (entity_list.Count > 0)
                {
                    decimal i = 1;
                    entity_list.OrderByDescending(t => t.ProductOrder).ThenBy(t=>t.ExeWorkOrder).ToList().ForEach(t =>
                    {
                        t.Create();
                        t.SWStatus = "1";
                        t.EquipCode = machineCode;
                        t.PlanProductTime = Convert.ToDateTime(planProductTime + " 00:00");
                        t.Creator = CurrentAccount.UserCode;
                        t.CreateTime = DateTime.Now;
                        t.SWSeq = GetTimeStamp() + i;
                        i++;
                    });

                    var query = from workEntity in _plWorkOrderService.Get_ExpressionList(t => true)
                                join swEntity in entity_list on workEntity.WorkOrder equals swEntity.WorkOrder
                                select new PL_WorkOrderEntity()
                                {
                                    Id = workEntity.Id,
                                    AssignStatus = "2",
                                    ModifyBy = userCode,
                                    ModifyTime = DateTime.Now
                                };

                    var list = query.ToList();
                    //批量新增
                    isok = _Service.SaveEntity_List(false, entity_list, out msg);
                    _plWorkOrderService.SaveEntity_List(true, userCode, list, out msg);
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
        /// 功能描述: 批量（修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("UpdateBatchPM_ExeWorkOrderSW")]
        public HttpResponseMessage UpdateBatchPM_ExeWorkOrderSW(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<PM_ExeWorkOrderSWEntity> entity_list = JsonConvert.DeserializeObject<List<PM_ExeWorkOrderSWEntity>>(getValue(jo, "Entity"));

                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                string msg = "";
                int isok = 1;

                if (entity_list.Count > 0)
                {
                    entity_list.OrderBy(t => t.PlanProductTime).ToList().ForEach(t =>
                    {
                        t.ModifyBy = CurrentAccount.UserCode;
                        t.ModifyTime = DateTime.Now;
                    });
                    //批量修改
                    isok = _Service.SaveEntity_List(true, entity_list, out msg);
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_ExeWorkOrderSW")]
        public HttpResponseMessage DeletePM_ExeWorkOrderSW(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                PM_ExeWorkOrderSWEntity entity = JsonConvert.DeserializeObject<PM_ExeWorkOrderSWEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ExeWorkOrderSWEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_14");//删除操作成功
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_ExeWorkOrderSW")]
        public HttpResponseMessage RemovePM_ExeWorkOrderSW(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                PM_ExeWorkOrderSWEntity entity = JsonConvert.DeserializeObject<PM_ExeWorkOrderSWEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ExeWorkOrderSWEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _Service.RemoveForm(Id, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_14");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_16");//删除操作失败
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ExeWorkOrderSWEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_17");//获取详情数据成功
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ExeWorkOrderSWEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_17");//获取详情数据成功
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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
        /// 创建日期: 2021-08-03 15:02:36
        /// 任务编号: 派工执行工单
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_ExeWorkOrderSW_export")]
        public HttpResponseMessage PM_ExeWorkOrderSW_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ExeWorkOrderSWController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                PM_ExeWorkOrderSW_Service _Service = new PM_ExeWorkOrderSW_Service();
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

        public static decimal GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (decimal)ts.TotalSeconds;
        }
    }
}
