using System;
using System.Data;
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
using ALP.Application.Busines.MaterialManage;
using System.Transactions;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.Busines.ProduceManage;
using ALP.Application.WebApi.Common;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.Resources;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;
using ALP.Application.Service.Material;
using ALP.Application.Service.PlanManage;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-08
    /// 2.创建作者: admin
    /// 3.功能描述: MM_SuperProductStockController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 超产品库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_SuperProductStock")]
    public class MM_SuperProductStockController : ApiBaseController
    {
        Base_KeyParameterItem_Service _keyParameterItemService = new Base_KeyParameterItem_Service();

        MM_SuperProductStock_Service _superProductStockService = new MM_SuperProductStock_Service();
        MM_SupProductStockTransferBLL _supProductStockTransferBLL = new MM_SupProductStockTransferBLL();
        PL_WorkOrderBLL _WorkOrderBLL = new PL_WorkOrderBLL();
        PL_ExeWorkOrderBLL _exeWorkOrderBLL = new PL_ExeWorkOrderBLL();
        PM_TransferCardBLL _transferCardBLL = new PM_TransferCardBLL();
        PM_TransferCardResumeBLL _resumeBLL = new PM_TransferCardResumeBLL();
        PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();//工单工艺路线
        PL_ProcessOfOperationsBLL _plProcessOfOperationsBLL = new PL_ProcessOfOperationsBLL();//工单工艺-工序
        PL_ProcessOfOperationsAttrBLL _plProcessOfOperationsAttrBLL = new PL_ProcessOfOperationsAttrBLL();//工单工艺-工序-属性
        BsModelWithResourceBLL _bsModelWithResourceBLL = new BsModelWithResourceBLL(); //工厂建模
        MMSuperProductStockAdjustService _MMSuperProductStockAdjustService = new MMSuperProductStockAdjustService();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_SuperProductStockPageList")]
        public HttpResponseMessage MM_SuperProductStockPageList(JObject jo)
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

                var data = _superProductStockService.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_SuperProductStockPageDataTableList")]
        public HttpResponseMessage MM_SuperProductStockPageDataTableList(JObject jo)
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
                var data = _superProductStockService.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_SuperProductStockList")]
        public HttpResponseMessage GetMM_SuperProductStockList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_SuperProductStock")]
        public HttpResponseMessage SaveMM_SuperProductStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //业务服务类
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                //参数转实体
                MM_SuperProductStockEntity entity = JsonConvert.DeserializeObject<MM_SuperProductStockEntity>(getValue(jo, "Entity"));
                //工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.FactoryCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_8");//工厂编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ProcessCode))
                {
                    //工序编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_9");//工序编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialCode))
                {
                    //客户型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_10");//客户型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Spec))
                {
                    //规格型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_11");//规格型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MMXH))
                {
                    //面膜型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_12");//面膜型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.BatchNo))
                {
                    //批次号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_13");//批次号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WhsCode))
                {
                    //仓库编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_14");//仓库编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    entity.Creator = userCode;
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_SuperProductStock")]
        public HttpResponseMessage SaveBatchMM_SuperProductStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_17");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_18");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<MM_SuperProductStockEntity> Insert_entity_list = new List<MM_SuperProductStockEntity>();
                //更新数组
                List<MM_SuperProductStockEntity> Update_entity_list = new List<MM_SuperProductStockEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        MM_SuperProductStockEntity entity = new MM_SuperProductStockEntity();
                        //工厂编码
                        entity.FactoryCode = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_19")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_19")];//工厂编码
                        //工序编码
                        entity.ProcessCode = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_20")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_20")];//工序编码
                        //客户型号
                        entity.MaterialCode = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_21")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_21")];//客户型号
                        //规格型号
                        entity.Spec = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_22")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_22")];//规格型号
                        //面膜型号
                        entity.MMXH = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_23")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_23")];//面膜型号
                        //批次号
                        entity.BatchNo = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_24")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_24")];//批次号
                        //仓库编码
                        entity.WhsCode = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_25")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_25")];//仓库编码
                        //库存数量(片)
                        entity.StockQty = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_26")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_26")];//库存数量(片)
                        //锁定数量/片
                        entity.LockedQty = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_27")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_27")];//锁定数量/片
                        //创建人
                        entity.Creator = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_28")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_28")];//创建人
                        //创建时间
                        entity.CreateTime = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_29")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_29")];//创建时间
                        //修改人
                        entity.ModifyBy = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_30")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_30")];//修改人
                        //修改时间
                        entity.ModifyTime = item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_31")] == null ? "" : item[Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_31")];//修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        MM_SuperProductStockEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_SuperProductStock")]
        public HttpResponseMessage DeleteMM_SuperProductStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                MM_SuperProductStockEntity entity = JsonConvert.DeserializeObject<MM_SuperProductStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_SuperProductStockEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_34");//删除操作成功
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_SuperProductStock")]
        public HttpResponseMessage RemoveMM_SuperProductStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                MM_SuperProductStockEntity entity = JsonConvert.DeserializeObject<MM_SuperProductStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_SuperProductStockEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _Service.RemoveForm(Id, userName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_34");//删除操作成功
                else
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_36");//删除操作失败
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_SuperProductStockEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_37");//获取详情数据成功
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_SuperProductStockEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_37");//获取详情数据成功
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
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
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_SuperProductStock_export")]
        public HttpResponseMessage MM_SuperProductStock_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                MM_SuperProductStock_Service _Service = new MM_SuperProductStock_Service();
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

        #region 超产品库存汇总-主信息
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-20 15:42:02
        /// 任务编号: 超产品库存汇总查询
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableMList")]
        public HttpResponseMessage GetPageDataTableMList(JObject jo)
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

                var data = _superProductStockService.GetPageDataTableMList(pagination, queryJson);
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
        #endregion

        #region 超产品库存汇总-明细
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-20 15:42:02
        /// 任务编号: 超产品库存详情查询
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableDList")]
        public HttpResponseMessage GetPageDataTableDList(JObject jo)
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

                var data = _superProductStockService.GetPageDataTableDList(pagination, queryJson);
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
        #endregion


        /// <summary>
        /// 获取订单号
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetExeWorkOrderList")]
        public HttpResponseMessage GetExeWorkOrderList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string materialCode = getValue(jo, "materialCode"); //客户型号
                string productOrder = getValue(jo, "Name");  //订单号
                string batchNo = getValue(jo, "batchNo");//超产品批次
                string exeWorkOrderType = getValue(jo, "exeWorkOrderType");//执行工单类型
                string processCode = getValue(jo, "processCode");//工序编码

                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (!string.IsNullOrEmpty(materialCode))
                    dic.Add("MaterialCode", materialCode);
                if (!string.IsNullOrEmpty(productOrder))
                    dic.Add("ProductOrder", productOrder);
                if (!string.IsNullOrEmpty(batchNo))
                    dic.Add("BatchNo", batchNo);
                if (!string.IsNullOrEmpty(exeWorkOrderType))
                    dic.Add("ExeWorkOrderType", exeWorkOrderType);
                if (!string.IsNullOrEmpty(processCode))
                    dic.Add("ProcessCode", processCode);
                DataTable data = _superProductStockService.GetExeWorkOrderList(dic);

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

        #region 超产品库存管理-转出
        /// <summary>
        /// 功能描述: 
        /// 创　　建: admin
        /// 创建日期: 2021-09-07 15:42:02
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("SuperProductStockOut")]
        public HttpResponseMessage SuperProductStockOut(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                var outEntity = JsonConvert.DeserializeObject<MM_SupProductStockTransferEntity>(getValue(jo, "Entity"));

                #region 1、新增转出记录
                outEntity.Id = Guid.NewGuid().ToString();
                outEntity.Creator = userCode;
                outEntity.CreateTime = DateTime.Now;
                #endregion

                #region 2、扣减库存
                var stockEntity = _superProductStockService.Get_ExpressionEntity(t => t.ProcessCode == outEntity.ProcessCode
                    && t.MaterialCode == outEntity.MaterialCode && t.BatchNo == outEntity.BatchNo);

                PL_ExeWorkOrderEntity exeWorkOrderEntity = null;
                if (outEntity.OutType == "1")
                {
                    stockEntity.StockQty -= outEntity.Qty;
                    exeWorkOrderEntity = _exeWorkOrderBLL.Get_ExpressionEntity(t => t.ExeWorkOrder == outEntity.ExeWorkOrder);
                    stockEntity.LockedQty -= exeWorkOrderEntity.PiecesQty;//解锁超产品执行工单的片数
                    if (stockEntity.LockedQty < 0)
                        stockEntity.LockedQty = 0;
                }
                else
                {
                    stockEntity.StockQty -= outEntity.Qty;
                }
                stockEntity.ModifyBy = userCode;
                stockEntity.ModifyTime = DateTime.Now;
                #endregion

                #region 3、生成流转卡
                PM_TransferCardEntity cardEntity = null;
                if (outEntity.OutType == "0")
                {
                    var cardList = _transferCardBLL.Get_ExpressionList(t => t.ExeWorkOrder == outEntity.ExeWorkOrder).ToList();
                    var oldCardEntity = cardList.FirstOrDefault();
                    if (oldCardEntity == null)
                    {
                        return AjaxResult(false, "转出的执行工单没有流转卡");
                    }
                    //var oldCardEntity = cardList.Where(t => t.CardStatus == "1").FirstOrDefault();
                    //if (oldCardEntity == null)
                    //{
                    //    oldCardEntity = cardList.Where(t => t.CardStatus == "4").FirstOrDefault();
                    //    if (oldCardEntity == null)
                    //    {
                    //        oldCardEntity = cardList.Where(t => t.CardStatus == "6").FirstOrDefault();
                    //        if (oldCardEntity == null)
                    //        {
                    //            result.success = false;
                    //            result.returnMsg = "没有可继承的流转卡";
                    //            return Request.CreateResponse(HttpStatusCode.OK, result);
                    //        }
                    //    }
                    //}
                    //var resumeEntity = _resumeBLL.Get_ExpressionEntity(t => t.CardCode == oldCardEntity.CardCode && t.Flag == "1");
                    //if (resumeEntity == null)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = "找不到流转卡所在工序";
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                    //if (resumeEntity.ProcessCode != outEntity.ProcessCode)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = "未到超产品所在工序";
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                    var maxNo = cardList.Select(t => t.CardName).Max(t => Convert.ToInt32(t.Substring(t.IndexOf('-') + 1)));
                    var cardName = oldCardEntity.CardName.Substring(0, oldCardEntity.CardName.IndexOf('-') + 1) + (maxNo + 1).ToString().PadLeft(2, '0');

                    cardEntity = PubFunction.DeepCopyByReflection(oldCardEntity);
                    cardEntity.Id = Guid.NewGuid().ToString();
                    cardEntity.CardCode = oldCardEntity.ExeWorkOrder + "-" + oldCardEntity.ContainerNO + "C" + cardName;
                    cardEntity.CardName = cardName;
                    cardEntity.CardType = "5";//超产品
                    cardEntity.PalletQty = Math.Ceiling(outEntity.Qty.Value / outEntity.DXZH.Value);
                    cardEntity.PieceQty = outEntity.Qty;
                    cardEntity.NewType = "2";
                    cardEntity.StartProcess = outEntity.ProcessCode;
                    cardEntity.CardStatus = "1";
                    cardEntity.PrintStatus = "1";
                    cardEntity.IsEnabled = true;
                    cardEntity.SerialNumber = Guid.NewGuid().ToString();
                    cardEntity.Creator = userCode;
                    cardEntity.CreateTime = DateTime.Now;
                    cardEntity.ModifyBy = null;
                    cardEntity.ModifyTime = null;
                }

                #endregion

                #region 4、流转履历
                PM_TransferCardResumeEntity cardResumeEntity = null;
                if (outEntity.OutType == "0")
                {
                    var processEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == cardEntity.WorkOrder && t.IsDeleted == false);
                    var pOperationList = _plProcessOfOperationsBLL.Get_ExpressionList(t => t.ProcessId == processEntity.Id).OrderBy(t => t.SN).ToList();
                    var pOerationEntity = pOperationList.Find(t => t.OperationCode == outEntity.ProcessCode);
                    var pAttrList = _plProcessOfOperationsAttrBLL.Get_ExpressionList(t => t.ProcessId == processEntity.Id && t.OperationsId == pOerationEntity.Id).ToList();
                    var locationCode = pAttrList.FirstOrDefault(t => t.AttrCode == "KGKW")?.AttrValue;//库位
                    var whsCode = _bsModelWithResourceBLL.GetEntity(t => t.ResourceCode == locationCode)?.ParentResource;//仓

                    cardResumeEntity = new PM_TransferCardResumeEntity();
                    cardResumeEntity.Id = Guid.NewGuid().ToString();
                    cardResumeEntity.FactoryCode = outEntity.FactoryCode;
                    cardResumeEntity.FactoryName = outEntity.FactoryName;
                    cardResumeEntity.ProcessCode = outEntity.ProcessCode;
                    cardResumeEntity.CardCode = cardEntity.CardCode;
                    cardResumeEntity.BusinessType = "8";
                    cardResumeEntity.Flag = "1";
                    cardResumeEntity.WhsCode = whsCode;
                    cardResumeEntity.LocationCode = locationCode;
                    cardResumeEntity.SheetQty = cardEntity.PalletQty;
                    cardResumeEntity.PieceQty = cardEntity.PieceQty;
                    cardResumeEntity.Creator = userCode;
                    cardResumeEntity.CreateTime = DateTime.Now;
                    cardResumeEntity.IsEnabled = true;
                }
                #endregion

                #region 5、执行工单超产品批次解锁

                if (outEntity.OutType == "1")
                {
                    exeWorkOrderEntity.BatchNo = "";
                    exeWorkOrderEntity.SupId = "";
                    exeWorkOrderEntity.ModifyBy = userCode;
                    exeWorkOrderEntity.ModifyTime = DateTime.Now;
                }
                #endregion

                #region 同步SAP
                var factoryCode = outEntity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    IF152 sapRequest = new IF152();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF152.ToString();
                    sapRequest.RSQ_DATA = new IF152_RSQ_DATA();

                    IF152_HEAD IF152_HEAD = new IF152_HEAD();
                    IF152_HEAD.ZBTYPE = SAPBusinessType.超产品领退料.GetHashCode().ToString();
                    IF152_HEAD.ZTYPE = SAPBusinessType.超产品领退料.ToString();
                    sapRequest.RSQ_DATA.IS_HEAD = IF152_HEAD;

                    //明细
                    var materialEntity = new Base_Material_Service().Get_ExpressionEntity(t => t.MaterialCode == outEntity.MaterialCode);
                    var plWorkOrderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == outEntity.WorkOrder);
                    var plProcessEntity = new PL_Process_Service().GetEntity(t => t.WorkOrder == outEntity.WorkOrder);
                    var plOperationEntity = new PL_ProcessOfOperations_Service().Get_ExpressionEntity(t =>
                        t.ProcessId == plProcessEntity.Id && t.OperationCode == outEntity.ProcessCode);
                    if (plOperationEntity == null)
                        return AjaxResult(false, $"工艺路线中没有工序[{outEntity.ProcessCode}]");
                    var plAttrEntity = new PL_ProcessOfOperationsAttr_Service().Get_ExpressionEntity(t => t.OperationsId == plOperationEntity.Id
                          && t.AttrCode == "HSLLX");

                    List<IF152_ITEM2> IT_ITEM2 = new List<IF152_ITEM2>();
                    IF152_ITEM2 IF152_ITEM2 = new IF152_ITEM2();
                    IF152_ITEM2.MESID = outEntity.Id ?? "";
                    IF152_ITEM2.ZTIME = outEntity.CreateTime.Value.ToString("yyyyMMddHHmmssfff") ?? "";
                    IF152_ITEM2.WERKS = outEntity.FactoryCode ?? "";
                    IF152_ITEM2.AUFNR = plWorkOrderEntity.SAP_AUFNR ?? "";
                    IF152_ITEM2.BUDAT = outEntity.CreateTime.Value.ToString("yyyyMMdd") ?? "";
                    IF152_ITEM2.BKTXT = "";
                    IF152_ITEM2.MTSNR = "";
                    IF152_ITEM2.MATNR = materialEntity?.SAPMaterialCode ?? "";
                    IF152_ITEM2.CHARG = "";
                    IF152_ITEM2.ERFMG = outEntity.Qty.ToString() ?? "";
                    IF152_ITEM2.ISHS = plAttrEntity?.AttrValue ?? "";
                    IF152_ITEM2.LGORT = outEntity.LocationCode ?? "";
                    IF152_ITEM2.SGTXT = outEntity.Creator ?? "";
                    IT_ITEM2.Add(IF152_ITEM2);
                    sapRequest.RSQ_DATA.IT_ITEM2 = IT_ITEM2;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    outEntity.IsPosted = sapResult.Flag ? "0" : "";
                    outEntity.PostedMsg = sapResult.Msg;
                    outEntity.PostedTime = DateTime.Now;
                    outEntity.PostedUser = "SAP";
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
                    _supProductStockTransferBLL.SaveEntity("", outEntity, out msg);
                    _superProductStockService.SaveEntity(stockEntity.Id, stockEntity, out msg);
                    if (cardEntity != null)
                        _transferCardBLL.SaveEntity("", cardEntity, out msg);
                    if (cardResumeEntity != null)
                        _resumeBLL.SaveEntity("", cardResumeEntity, out msg);
                    if (exeWorkOrderEntity != null)
                    {
                        _exeWorkOrderBLL.SaveEntity(exeWorkOrderEntity.Id, exeWorkOrderEntity, out msg);
                    }

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_39");//转出成功
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

        #region 待转超产品明细
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-20 15:42:02
        /// 任务编号: 待转超产品明细查询
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetDZCCPPageDataTableMList")]
        public HttpResponseMessage GetDZCCPPageDataTableMList(JObject jo)
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

                var data = _superProductStockService.GetDZCCPPageDataTableMList(pagination, queryJson);
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
        #endregion

        #region 待转超产品明细新 四期修改
        [HttpPost]
        [Route("GetDZCCPList")]
        public HttpResponseMessage GetDZCCPList(JObject jo)
        {
            try
            {
                string queryJson = getValue(jo, "queryJson");

                var data = _superProductStockService.GetDZCCPList(queryJson);

                return AjaxResult(true, Language.GetText("Common.SearchSuccess"), data);//查询成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, Language.GetText("Common.SearchError2") + ex.Message);//查询失败：
            }
        }
        #endregion

        #region 超产品库存管理-转入
        /// <summary>
        /// 功能描述: 
        /// 创　　建: admin
        /// 创建日期: 2021-09-07 15:42:02
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("SuperProductStockIn")]
        public HttpResponseMessage SuperProductStockIn(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                var inEntity = JsonConvert.DeserializeObject<MM_SupProductStockTransferEntity>(getValue(jo, "Entity"));

                if (inEntity.DXZH == null)
                    return AjaxResult(false, Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_42"));//大小张转换属性不能为空

                #region 1、新增转入记录
                inEntity.Id = Guid.NewGuid().ToString();
                inEntity.Creator = userCode;
                inEntity.CreateTime = DateTime.Now;
                #endregion

                #region 2、增加库存
                var stockEntity = _superProductStockService.Get_ExpressionEntity(t => t.ProcessCode == inEntity.ProcessCode
                    && t.MaterialCode == inEntity.MaterialCode && t.BatchNo == inEntity.BatchNo && t.LocationCode == inEntity.LocationCode);
                if (stockEntity != null)
                {
                    stockEntity.StockQty += inEntity.Qty;
                    stockEntity.ModifyBy = userCode;
                    stockEntity.ModifyTime = DateTime.Now;
                }
                else
                {
                    stockEntity = new MM_SuperProductStockEntity();
                    stockEntity.FactoryCode = inEntity.FactoryCode;
                    stockEntity.FactoryName = inEntity.FactoryName;
                    stockEntity.ProcessCode = inEntity.ProcessCode;
                    stockEntity.MaterialCode = inEntity.MaterialCode;
                    stockEntity.MaterialName = inEntity.MaterialName;
                    stockEntity.Spec = inEntity.Spec;
                    stockEntity.MMXH = inEntity.MMXH;
                    stockEntity.DXZH = inEntity.DXZH;
                    stockEntity.BatchNo = inEntity.BatchNo;
                    stockEntity.StockQty = inEntity.Qty;
                    stockEntity.Creator = userCode;
                    stockEntity.CreateTime = DateTime.Now;
                    stockEntity.WhsCode = inEntity.WhsCode;
                    stockEntity.WhsName = inEntity.WhsName;
                    stockEntity.LocationCode = inEntity.LocationCode;
                    stockEntity.LocationName = inEntity.LocationName;
                }
                #endregion

                #region 同步SAP
                var factoryCode = inEntity.FactoryCode;

                var SAPSyncSwitch = _keyParameterItemService.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                     && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    var workOrderEntity = _WorkOrderBLL.Get_ExpressionEntity(t => t.WorkOrder == inEntity.WorkOrder);

                    IF152 sapRequest = new IF152();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF152.ToString();
                    sapRequest.RSQ_DATA = new IF152_RSQ_DATA();

                    IF152_HEAD IF152_HEAD = new IF152_HEAD();
                    IF152_HEAD.ZBTYPE = SAPBusinessType.超产品入库.GetHashCode().ToString();
                    IF152_HEAD.ZTYPE = SAPBusinessType.超产品入库.ToString();
                    sapRequest.RSQ_DATA.IS_HEAD = IF152_HEAD;

                    List<IF152_ITEM3> IT_ITEM3 = new List<IF152_ITEM3>();
                    IF152_ITEM3 IF152_ITEM3 = new IF152_ITEM3();
                    IF152_ITEM3.MESID = inEntity.Id ?? "";
                    IF152_ITEM3.ZTIME = inEntity.CreateTime.Value.ToString("yyyyMMddHHmmssfff") ?? "";
                    IF152_ITEM3.WERKS = inEntity.FactoryCode.ToString() ?? "";
                    IF152_ITEM3.ZTYPE = "3";
                    IF152_ITEM3.BUDAT = inEntity.CreateTime.Value.ToString("yyyyMMdd") ?? "";
                    IF152_ITEM3.BKTXT = inEntity.Creator ?? ""; ;
                    IF152_ITEM3.LFSNR = "";
                    IF152_ITEM3.AUFNR = workOrderEntity.SAP_AUFNR ?? "";
                    IF152_ITEM3.ERFMG = inEntity.Qty.ToString() ?? "";
                    IF152_ITEM3.LGOBE = inEntity.LocationCode ?? "";
                    IF152_ITEM3.CHARG = "";
                    IF152_ITEM3.UMMAT_KDAUF = "";
                    IF152_ITEM3.UMMAT_KDPOS = "";
                    IT_ITEM3.Add(IF152_ITEM3);
                    sapRequest.RSQ_DATA.IT_ITEM3 = IT_ITEM3;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    inEntity.IsPosted = sapResult.Flag ? "0" : "";
                    inEntity.PostedMsg = sapResult.Msg;
                    inEntity.PostedTime = DateTime.Now;
                    inEntity.PostedUser = "SAP";
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
                    _supProductStockTransferBLL.SaveEntity("", inEntity, out msg);
                    _superProductStockService.SaveEntity(stockEntity.Id, stockEntity, out msg);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_40");//转入成功
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

        #region 超产品库存管理-校准
        /// <summary>
        /// 功能描述: 
        /// 创　　建: admin
        /// 创建日期: 2021-09-07 15:42:02
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("SuperProductStockAdjust")]
        public HttpResponseMessage SuperProductStockAdjust(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                var keyValue = getValue(jo, "KeyValue"); //库存Id
                var adjustEntity = JsonConvert.DeserializeObject<MMSuperProductStockAdjustEntity>(getValue(jo, "Entity"));

                var stockEntity = _superProductStockService.Get_ExpressionEntity(t => t.Id == keyValue);
                var offsetQty = adjustEntity.AdjustQty - stockEntity.StockQty;

                #region 1、新增校准记录
                adjustEntity.Id = Guid.NewGuid().ToString();
                adjustEntity.Creator = userCode;
                adjustEntity.CreateTime = DateTime.Now;
                if (offsetQty > 0)
                {
                    adjustEntity.TakeType = "1";//盘盈
                }
                else
                {
                    adjustEntity.TakeType = "2";//盘亏
                }
                adjustEntity.Qty = Math.Abs(offsetQty.Value);
                #endregion

                #region 2、修改库存
                stockEntity.StockQty = adjustEntity.AdjustQty;
                stockEntity.ModifyBy = userCode;
                stockEntity.ModifyTime = DateTime.Now;
                #endregion

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _MMSuperProductStockAdjustService.SaveEntity("", adjustEntity);
                    _superProductStockService.SaveEntity(stockEntity.Id, stockEntity, out msg);

                    ts.Complete();
                }

                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_SuperProductStockController.Tips_41");//校准成功
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

    }
}
