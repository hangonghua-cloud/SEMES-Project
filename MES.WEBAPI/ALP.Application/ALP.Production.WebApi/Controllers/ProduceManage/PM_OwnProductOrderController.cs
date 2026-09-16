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
using ALP.Application.Service.Material;
using ALP.Application.Busines.Material;
using ALP.Application.Entity.Material;
using ALP.Application.Entity.PlanManage;
using System.Transactions;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Service.ToSAP;
using ALP.Application.Service.Resources;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.SystemManage;
using ALP.Application.Service.Helper;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;

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
    [RoutePrefix("PM_OwnProductOrder")]
    public class PM_OwnProductOrderController : ApiBaseController
    {
        private PM_OwnProductOrderBLL _OwnProductOrderBLL = new PM_OwnProductOrderBLL();

        private BS_BOM_Service _bsBOMService = new BS_BOM_Service();
        private PL_BOMBLL _plBOMBLL = new PL_BOMBLL();
        private PL_MaterialBLL _plMaterialBLL = new PL_MaterialBLL();
        private PL_MaterialFacetBLL _plMaterialFacetBLL = new PL_MaterialFacetBLL();
        private PL_BOMItemsBLL _plBomItemsBLL = new PL_BOMItemsBLL();
        private PL_ProcessBLL _PLProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _PLProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();
        private PL_ProcessOfOperationsAttrBLL _PLProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();

        private Base_MaterialBLL _bsMaterialBLL = new Base_MaterialBLL();
        private Base_MaterialFactory_Service _baseMaterialFactoryService = new Base_MaterialFactory_Service();
        private Base_MaterialFacetBLL _base_MaterialFacetBLL = new Base_MaterialFacetBLL();

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
            result.resultData = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_2");//测试成功
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
        [Route("PM_OwnProductOrderPageList")]
        public HttpResponseMessage PM_OwnProductOrderPageList(JObject jo)
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

                var data = _OwnProductOrderBLL.GetPageList(pagination, queryJson);
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
        [Route("PM_OwnProductOrderPageDataTableList")]
        public HttpResponseMessage PM_OwnProductOrderPageDataTableList(JObject jo)
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

                var data = _OwnProductOrderBLL.GetPageDataTableList(pagination, queryJson);
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
        [Route("GetPM_OwnProductOrderList")]
        public HttpResponseMessage GetPM_OwnProductOrderList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _OwnProductOrderBLL.GetList(checkType, out msg);
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

        #region 新增、修改
        [HttpPost]
        [Route("SavePM_OwnProductOrder")]
        public HttpResponseMessage SavePM_OwnProductOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //参数转实体
                PM_OwnProductOrderEntity entity = JsonConvert.DeserializeObject<PM_OwnProductOrderEntity>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                if (string.IsNullOrEmpty(keyValue))
                {
                    //var ent = _OwnProductOrderBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode && t.BOMCode == entity.BOMCode);
                    //if (ent != null)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = "当前物料已经创建工单";
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}

                    var returnNum = "";
                    _OwnProductOrderBLL.GetSerialNO("OwnProductOrder", out returnNum, out msg);
                    var workOrder = "BCP-" + entity.SmallClass + DateTime.Now.ToString("yyMMdd") + returnNum.PadLeft(3, '0');
                    entity.WorkOrder = workOrder;
                    entity.LineNum = "10";
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    entity.OrderStatus = "1";//工单状态

                    if (string.IsNullOrEmpty(entity.ProcessRoute))
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_8");//缺少工艺路线！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    #region 同步SAP
                    var SAPSyncSwitch = new Base_KeyParameterItemBLL().Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                       && t.Remark1 == entity.FactoryCode);

                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                        var sapResult = new ToSAPService().PM_OwnProductOrderTOSAP(entity);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);

                        entity.IsPosted = sapResult.Flag ? "1" : "";
                        entity.SAP_AUFNR = getValue(sapResult.Data.ToJObject(), "EV_AUFNR");
                        entity.PostedMsg = sapResult.Msg;
                        entity.PostedUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                        entity.PostedTime = DateTime.Now;
                    }
                    #endregion

                    //工艺,bom,物料 都需要插入  自制半成品暂时按照内销
                    if (!string.IsNullOrEmpty(entity.BOMCode))
                    {
                        var bsBomEntity = _bsBOMService.Get_ExpressionEntity(t => t.BOMCode == entity.BOMCode);
                        _plBOMBLL.InsertPLBom(entity.FactoryCode, entity.BOMCode, entity.ProcessRoute, bsBomEntity?.OrderType, workOrder);
                    }
                    _plMaterialBLL.InsertPLMaterial(entity.FactoryCode, entity.MaterialCode, entity.ProcessRoute, workOrder);
                    _PLProcessBLL.InsertPLProcess(entity.FactoryCode, entity.ProcessRoute, workOrder, entity.ProcessCode);
                    _OwnProductOrderBLL.SaveEntity("", entity, out msg);

                }
                else //修改
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;

                    #region 同步SAP
                    var SAPSyncSwitch = new Base_KeyParameterItemBLL().Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == entity.FactoryCode);

                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        IF111 sapRequest = new IF111();
                        sapRequest.HEAD = new SapHeadDto();
                        sapRequest.HEAD.INIF_ID = SAPInterface.IF111.ToString();

                        sapRequest.RSQ_DATA = new IF111_RSQ_DATA();
                        sapRequest.RSQ_DATA.IV_IFLAG = "1";
                        IF111_DATA IS_DATA = new IF111_DATA();
                        IS_DATA.AUFNR = entity.SAP_AUFNR ?? "";
                        IS_DATA.GAMNG = entity.PlanQty.ToString() ?? "";
                        IS_DATA.GSTRP = entity.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.GLTRP = entity.CreateTime.Value.ToString("yyyyMMdd");
                        IS_DATA.TXT = entity.Remark ?? "";
                        sapRequest.RSQ_DATA.IS_DATA = IS_DATA;

                        var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);
                    }
                    #endregion

                    int isok = _OwnProductOrderBLL.SaveEntity(keyValue, entity, out msg);
                }
                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_OwnProductOrder")]
        public HttpResponseMessage SaveBatchPM_OwnProductOrder(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_11");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_12");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _OwnProductOrderBLL.GetList("", out msg);
                //插入数组
                List<PM_OwnProductOrderEntity> Insert_entity_list = new List<PM_OwnProductOrderEntity>();
                //更新数组
                List<PM_OwnProductOrderEntity> Update_entity_list = new List<PM_OwnProductOrderEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_OwnProductOrderEntity entity = new PM_OwnProductOrderEntity();
                        //工厂编码
                        entity.FactoryCode = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_13")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_13")];//工厂编码
                        //工序编码
                        entity.ProcessCode = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_14")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_14")];//工序编码
                        //工单状态
                        entity.OrderStatus = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_15")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_15")];//工单状态
                        //工单号
                        entity.WorkOrder = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_16")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_16")];//工单号
                        //物料小类
                        entity.SmallClass = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_17")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_17")];//物料小类
                        //物料编码
                        entity.MaterialCode = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_18")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_18")];//物料编码
                        //计划数量
                        entity.PlanQty = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_19")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_19")];//计划数量
                        //备注
                        entity.Remark = item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_20")] == null ? "" : item[Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_20")];//备注
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;


                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_OwnProductOrderEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _OwnProductOrderBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _OwnProductOrderBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_OwnProductOrder")]
        public HttpResponseMessage DeletePM_OwnProductOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_OwnProductOrderEntity entity = JsonConvert.DeserializeObject<PM_OwnProductOrderEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_OwnProductOrderEntity model = _OwnProductOrderBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _OwnProductOrderBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_23");//删除操作成功
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
        /// 功能描述: 获取半成品物料最新属性
        /// 创　　建: jpf
        /// 创建日期: 2023年2月1日15:11:19
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("PM_OwnGetMaterial")]
        public HttpResponseMessage PM_OwnGetMaterial(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                PM_OwnProductOrderEntity entity = JsonConvert.DeserializeObject<PM_OwnProductOrderEntity>(getValue(jo, "Entity"));
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
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:24:13
        /// 任务编号: 自制半成品工单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_OwnProductOrder")]
        public HttpResponseMessage RemovePM_OwnProductOrder(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                PM_OwnProductOrderEntity entity = JsonConvert.DeserializeObject<PM_OwnProductOrderEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                var workOrder = entity.WorkOrder;

                var bomEntity = _plBOMBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var BOMId = bomEntity?.Id;
                var materialEntity = _plMaterialBLL.Get_ExpressionEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var materialId = materialEntity?.Id;
                var processEntity = _PLProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var processId = processEntity?.Id;

                //删除
                int isok = _OwnProductOrderBLL.RemoveForm(t => t.Id == Id);

                _plBOMBLL.RemoveForm(t => t.WorkOrder == workOrder);
                _plBomItemsBLL.RemoveForm(t => t.BOMId == BOMId);

                _plMaterialBLL.RemoveForm(t => t.WorkOrder == workOrder);
                _plMaterialFacetBLL.RemoveForm(t => t.MaterialId == materialId);

                _PLProcessBLL.RemoveForm(t => t.WorkOrder == workOrder);
                _PLProcessOfOperationsBLL.RemoveForm(t => t.ProcessId == processId);
                _PLProcessOfOperationsAttrBLL.RemoveForm(t => t.ProcessId == processId);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_23");//删除操作成功
                else
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_26");//删除操作失败
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
        /// <returns>PM_OwnProductOrderEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _OwnProductOrderBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_27");//获取详情数据成功
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
        /// <returns>PM_OwnProductOrderEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _OwnProductOrderBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_27");//获取详情数据成功
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

                var data = _OwnProductOrderBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
                var list = _OwnProductOrderBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
                var list = _OwnProductOrderBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        [Route("PM_OwnProductOrder_export")]
        public HttpResponseMessage PM_OwnProductOrder_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_7");//缺少Entity参数！
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
                var data = _OwnProductOrderBLL.GetList_export(CreatedByCode, out msg);

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

        #region SAP结案
        [HttpPost]
        [Route("FinishCase")]
        public HttpResponseMessage FinishCase(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                var workOrderList = JsonConvert.DeserializeObject<List<PM_OwnProductOrderEntity>>(getValue(jo, "data"));
                if (workOrderList.Count < 1)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_5");//参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var newworkOrderList = workOrderList.Select(t => string.IsNullOrEmpty(t.Update_IsPosted));
                if (newworkOrderList.Any())
                {
                    string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                    var isbool = new ToSAPService().PM_OwnProductOrderStatusTOSAP(workOrderList, postUser);
                    if (isbool)
                    {
                        result.success = true;
                        result.returnMsg = Language.GetText("Common.Success");
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_28");//部分工单传SAP失败
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                else
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("ProduceManage.PM_OwnProductOrderController.Tips_29");//无需要传SAP的工单
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

        #region 工单导入
        [HttpPost]
        [Route("WorkOrderImport")]
        public HttpResponseMessage WorkOrderImport(JObject jo)
        {
            try
            {
                //参数转实体
                var data = JsonConvert.DeserializeObject<List<PM_OwnProductOrderEntity>>(getValue(jo, "data"));
                if (data == null || data.Count == 0)
                    return AjaxResult(false, "没有要导入的数据");

                var arrFactoryCode = data.Select(t => t.FactoryCode).Distinct().ToArray();
                var arrOperationCode = data.Select(t => t.ProcessCode).Distinct().ToArray();
                var arrBOMCode = data.Select(t => t.BOMCode).Distinct().ToArray();
                var arrProcessRoute = data.Select(t => t.ProcessRoute).Distinct().ToArray();
                var arrMaterialCode = data.Select(t => t.MaterialCode).Distinct().ToArray();

                var factoryList = new BsModelWithResourceService().GetList(t => arrFactoryCode.Contains(t.ResourceCode)).ToList();
                var operationList = new BsModelWithResourceService().GetList(t => arrOperationCode.Contains(t.ResourceCode)).ToList();
                var bomList = new BS_BOM_Service().Get_ExpressionList(t => arrBOMCode.Contains(t.BOMCode)).ToList();
                var processRouteList = new BS_Process_Service().Get_ExpressionList(t => arrProcessRoute.Contains(t.ProcessCode)).ToList();
                var materialList = new Base_Material_Service().Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();
                var smallList = new DataItemDetailService().GetDataItemList("MaterialSmall").ToList();

                foreach (var item in data)
                {
                    #region 数据校验
                    if (string.IsNullOrEmpty(item.FactoryCode))
                        return AjaxResult(false, "工厂编码不能为空");

                    if (!factoryList.Any(t => t.ResourceCode == item.FactoryCode))
                        return AjaxResult(false, "工厂编码不存在");

                    if (string.IsNullOrEmpty(item.ProcessCode))
                        return AjaxResult(false, "工序编码不能为空");

                    if (!operationList.Any(t => t.ResourceCode == item.ProcessCode))
                        return AjaxResult(false, "工序编码不存在");

                    if (string.IsNullOrEmpty(item.WorkOrder))
                        return AjaxResult(false, "工单号不能为空");

                    if (string.IsNullOrEmpty(item.LineNum))
                        return AjaxResult(false, "行号不能为空");

                    if (string.IsNullOrEmpty(item.BOMCode))
                        return AjaxResult(false, "BOM编码不能为空");

                    if (!bomList.Any(t => t.BOMCode == item.BOMCode))
                        return AjaxResult(false, "BOM编码不存在");

                    if (string.IsNullOrEmpty(item.ProcessRoute))
                        return AjaxResult(false, "工艺路线不能为空");

                    if (!processRouteList.Any(t => t.ProcessCode == item.ProcessRoute))
                        return AjaxResult(false, "工艺路线编码不存在");

                    if (string.IsNullOrEmpty(item.MaterialCode))
                        return AjaxResult(false, "物料编码不能为空");

                    if (!materialList.Any(t => t.MaterialCode == item.MaterialCode))
                        return AjaxResult(false, "物料编码不存在");

                    if (item.PlanQty == null)
                        return AjaxResult(false, "计划数量不能为空");
                    #endregion

                    var materialEntity = materialList.Find(t => t.MaterialCode == item.MaterialCode);
                    item.MaterialName = materialEntity.MaterialName;
                    item.SmallClass = materialEntity.SmallClass;
                    item.SmallClassName = smallList.Find(t => t.ItemValue == item.SmallClass)?.ItemName;
                    item.UnitName = item.UnitName ?? materialEntity.UnitName;
                    item.OrderStatus = "1";
                    item.Creator = CurrentAccount.UserCode;
                    item.CreateTime = DateTime.Now;
                }

                #region 同步SAP
                var factoryCode = data.First().FactoryCode;

                var SAPSyncSwitch = new Base_KeyParameterItemBLL().Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                   && t.Remark1 == factoryCode);

                foreach (var item in data)
                {
                    if (SAPSyncSwitch?.ItemValue == "1")
                    {
                        string postUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                        var sapResult = new ToSAPService().PM_OwnProductOrderTOSAP(item);
                        if (!sapResult.Flag)
                            return AjaxResult(false, sapResult.Msg);

                        item.IsPosted = sapResult.Flag ? "1" : "";
                        item.SAP_AUFNR = getValue(sapResult.Data.ToJObject(), "SAP_AUFNR");
                        item.PostedMsg = sapResult.Msg;
                        item.PostedUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                        item.PostedTime = DateTime.Now;
                    }
                }
                #endregion

                foreach (var item in data)
                {
                    string msg = "";
                    using (TransactionScope ts = new TransactionScope())
                    {
                        //工艺,bom,物料 都需要插入  自制半成品暂时按照内销
                        if (!string.IsNullOrEmpty(item.BOMCode))
                        {
                            var bsBomEntity = _bsBOMService.Get_ExpressionEntity(t => t.BOMCode == item.BOMCode);
                            _plBOMBLL.InsertPLBom(item.FactoryCode, item.BOMCode, item.ProcessRoute, bsBomEntity?.OrderType, item.WorkOrder);
                        }
                        _plMaterialBLL.InsertPLMaterial(item.FactoryCode, item.MaterialCode, item.ProcessRoute, item.WorkOrder);
                        _PLProcessBLL.InsertPLProcess(item.FactoryCode, item.ProcessRoute, item.WorkOrder, item.ProcessCode);
                        _OwnProductOrderBLL.SaveEntity("", item, out msg);

                        ts.Complete();
                    }
                }

                return AjaxResult(true, Language.GetText("Common.Success"));//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败:
            }
        }
        #endregion
    }
}
