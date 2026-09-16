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
using ALP.Application.Busines.PlanManage;
using ALP.Application.Service.Resources;
using System.Transactions;
using ALP.Application.Service.Material;
using ALP.Application.Entity.Material;
using ALP.Application.Entity.PlanManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_AbrasiveOrderController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 磨粉料工单管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_AbrasiveOrder")]
    public class PM_AbrasiveOrderController : ApiBaseController
    {
        private BS_BOM_Service _bsBOMService = new BS_BOM_Service();
        private Base_Material_Service _bsMaterialService = new Base_Material_Service();
        private Base_MaterialFactory_Service _baseMaterialFactoryService = new Base_MaterialFactory_Service();
        Base_MaterialFacet_Service _materialFacetService = new Base_MaterialFacet_Service();

        private PM_AbrasiveOrderBLL _AbrasiveOrderBLL = new PM_AbrasiveOrderBLL();

        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();
        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _PLProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();
        private PL_BOMBLL _plBOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _plBOMItemBLL = new PL_BOMItemsBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_AbrasiveOrderPageList")]
        public HttpResponseMessage PM_AbrasiveOrderPageList(JObject jo)
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

                var data = _AbrasiveOrderBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_AbrasiveOrderPageDataTableList")]
        public HttpResponseMessage PM_AbrasiveOrderPageDataTableList(JObject jo)
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

                var data = _AbrasiveOrderBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_AbrasiveOrderList")]
        public HttpResponseMessage GetPM_AbrasiveOrderList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _AbrasiveOrderBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_AbrasiveOrder")]
        public HttpResponseMessage SavePM_AbrasiveOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //参数转实体
                PM_AbrasiveOrderEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveOrderEntity>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");

                string returnNum = "";
                string msg = "";

                if (string.IsNullOrEmpty(keyValue))
                {
                    _AbrasiveOrderBLL.GetSerialNO("AbrasiveOrder", out returnNum, out msg);
                    var workOrder = "MF" + DateTime.Now.ToString("yyyyMMdd") + returnNum.PadLeft(3, '0');
                    entity.WorkOrder = workOrder;
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    entity.OrderStatus = "1";

                    var bsBomEntity = _bsBOMService.Get_ExpressionEntity(t => t.BOMCode == entity.BOMCode);

                    //using (TransactionScope ts = new TransactionScope())
                    //{
                    //工艺,bom,物料 都需要插入  自制半成品暂时按照内销
                    if (!string.IsNullOrEmpty(entity.BOMCode))
                    {
                        _plBOMBLL.InsertPLBom(entity.FactoryCode, entity.BOMCode, entity.ProcessRoute, bsBomEntity?.OrderType, workOrder);
                    }
                    _plMaterialBLL.InsertPLMaterial(entity.FactoryCode, entity.MaterialCode, entity.ProcessRoute, workOrder);
                    _PLProcessBLL.InsertPLProcess(entity.FactoryCode, entity.ProcessRoute, workOrder, entity.ProcessCode);
                    _AbrasiveOrderBLL.SaveEntity("", entity, out msg);

                    //    ts.Complete();
                    //}
                }
                else
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;

                    _AbrasiveOrderBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 功能描述: 工单完成
        /// 创　　建: Dragon
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("FinishPM_AbrasiveOrder")]
        public HttpResponseMessage FinishPM_AbrasiveOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                string keyValue = getValue(jo, "KeyValue");
                var entity = _AbrasiveOrderBLL.GetEntity(keyValue);
                entity.OrderStatus = "3";
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;

                string msg = "";
                int isok = _AbrasiveOrderBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_AbrasiveOrder")]
        public HttpResponseMessage SaveBatchPM_AbrasiveOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");


                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _AbrasiveOrderBLL.GetList("", out msg);
                //插入数组
                List<PM_AbrasiveOrderEntity> Insert_entity_list = new List<PM_AbrasiveOrderEntity>();
                //更新数组
                List<PM_AbrasiveOrderEntity> Update_entity_list = new List<PM_AbrasiveOrderEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_AbrasiveOrderEntity entity = new PM_AbrasiveOrderEntity();
                        //
                        entity.FactoryCode = item[""] == null ? "" : item[""];
                        //
                        entity.ProcessCode = item[""] == null ? "" : item[""];
                        //
                        entity.WorkOrder = item[""] == null ? "" : item[""];
                        //
                        entity.OrderStatus = item[""] == null ? "" : item[""];
                        //
                        entity.MaterialCode = item[""] == null ? "" : item[""];
                        //
                        entity.MaterialName = item[""] == null ? "" : item[""];
                        //
                        entity.SmallClass = item[""] == null ? "" : item[""];
                        //
                        entity.Spec = item[""] == null ? "" : item[""];
                        //
                        entity.PlanQty = item[""] == null ? "" : item[""];
                        //
                        entity.Remark = item[""] == null ? "" : item[""];
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_AbrasiveOrderEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _AbrasiveOrderBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _AbrasiveOrderBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_AbrasiveOrder")]
        public HttpResponseMessage DeletePM_AbrasiveOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_AbrasiveOrderEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveOrderEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_AbrasiveOrderEntity model = _AbrasiveOrderBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _AbrasiveOrderBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_12");//删除操作成功
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
        /// 功能描述: 删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_AbrasiveOrder")]
        public HttpResponseMessage RemovePM_AbrasiveOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_AbrasiveOrderEntity entity = JsonConvert.DeserializeObject<PM_AbrasiveOrderEntity>(getValue(jo, "Entity"));

                if (entity.OrderStatus != "1")
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_14");//已经生产的不能删除！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var workOrder = entity.WorkOrder;

                var materialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var materialId = materialEntity?.Id;
                var processEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var processId = processEntity?.Id;
                var plBOMEntity = _plBOMBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder);
                var plBOMId = plBOMEntity?.Id;

                using (TransactionScope ts = new TransactionScope())
                {
                    _plMaterialBLL.RemoveForm(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                    _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == materialId);

                    _PLProcessBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == processId);
                    _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == processId);

                    _plBOMBLL.RemoveForm(t => t.WorkOrder == workOrder);
                    _plBOMItemBLL.RemoveForm(t => t.BOMId == plBOMId);
                    //删除
                    _AbrasiveOrderBLL.RemoveForm(t => t.Id == entity.Id);

                    ts.Complete();
                }

                result.success = true;
                //if (isok > 0)
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_12");//删除操作成功
                //else
                //    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_15");//删除操作失败
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_AbrasiveOrderEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _AbrasiveOrderBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_16");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_AbrasiveOrderEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _AbrasiveOrderBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_16");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
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

                var data = _AbrasiveOrderBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
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
                var list = _AbrasiveOrderBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
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
                var list = _AbrasiveOrderBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:23:32
        /// 任务编号: 磨粉料工单管理
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_AbrasiveOrder_export")]
        public HttpResponseMessage PM_AbrasiveOrder_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_AbrasiveOrderController.Tips_7");//缺少Entity参数！
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
                var data = _AbrasiveOrderBLL.GetList_export(CreatedByCode, out msg);

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

        #region 获取物料最新属性
        [HttpPost]
        [Route("PM_GetMaterialAttr")]
        public HttpResponseMessage PM_GetMaterialAttr(JObject jo)
        {
            try
            {
                var entity = JsonConvert.DeserializeObject<PM_AbrasiveOrderEntity>(getValue(jo, "Entity"));
                var bs_MaterialEntity = _bsMaterialService.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode);
                var baseMaterialFactoryEntity = _baseMaterialFactoryService.Get_ExpressionEntity(t =>
                    t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                //获取物料主数据属性
                List<Base_MaterialFacetEntity> bs_MaterialFacetList = _materialFacetService.Get_ExpressionList(t => t.MaterialFactoryId == baseMaterialFactoryEntity.Id).ToList();

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
                pl_materialentity.Creator = CurrentAccount.UserCode;
                pl_materialentity.CreateTime = DateTime.Now;
                pl_materialentity.ModifyBy = CurrentAccount.UserCode;
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
                using (var ts = new TransactionScope())
                {
                    string msg = "";
                    _plMaterialBLL.SaveEntity(pl_materialentity.Id, pl_materialentity, out msg);//更新主表

                    _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == pl_materialentity.Id);//删除主表
                                                                                              //_plMaterialBLL.RemoveForm(t=>t.WorkOrder == entity.WorkOrder);
                    if (PL_MaterialFacetList.Count > 0)
                    {
                        int n = _plMaterialFacetBLL.SaveEntity_List(false, "", PL_MaterialFacetList, out msg);
                    }
                    ts.Complete();
                }

                return AjaxResult(true, Language.GetText("Common.ExecutionSuccess"));//执行成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

    }
}
