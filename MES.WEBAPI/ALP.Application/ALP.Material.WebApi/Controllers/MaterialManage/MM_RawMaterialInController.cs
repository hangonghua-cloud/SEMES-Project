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
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Busines.Material;
using System.Transactions;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Busines.QualityManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.Material;
using ALP.Application.Service.Resources;
using ALP.Application.Entity.SAPEntity.ToSAP;
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Entity.Enum;
using ALP.Application.Service.Helper;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-25
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_RawMaterialInController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 原材料入库单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_RawMaterialIn")]
    public class MM_RawMaterialInController : ApiBaseController
    {
        private MM_RawMaterialInBLL _RawMaterialInBLL = new MM_RawMaterialInBLL();
        private Base_MaterialFactoryBLL _baseMaterialFactoryBLL = new Base_MaterialFactoryBLL();//物料工厂属性
        private MM_RawMaterialStockBLL _rawMaterialStockBLL = new MM_RawMaterialStockBLL();//原材料库存
        private PL_PurchaseOrderBLL _PurchaseOrderBLL = new PL_PurchaseOrderBLL();
        private Base_KeyParameterItemBLL _baseKeyParameterItemBLL = new Base_KeyParameterItemBLL();//关键参数列表
        private QC_IQCQualityCheck_BLL _IQCQualityCheckBLL = new QC_IQCQualityCheck_BLL();
        private MM_ReceiptNoticeBLL _ReceiptNoticeBLL = new MM_ReceiptNoticeBLL();
        private BaseSequenceService _baseSequenceService = new BaseSequenceService();
        private Base_Material_Service _baseMaterialService = new Base_Material_Service();//物料主数据

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_RawMaterialInPageList")]
        public HttpResponseMessage MM_RawMaterialInPageList(JObject jo)
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

                var data = _RawMaterialInBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_RawMaterialInPageDataTableList")]
        public HttpResponseMessage MM_RawMaterialInPageDataTableList(JObject jo)
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

                var data = _RawMaterialInBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_RawMaterialInList")]
        public HttpResponseMessage GetMM_RawMaterialInList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _RawMaterialInBLL.GetList(checkType, out msg);
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_RawMaterialIn")]
        public HttpResponseMessage SaveBatchMM_RawMaterialIn(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_25");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_26");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _RawMaterialInBLL.GetList("", out msg);
                //插入数组
                List<MM_RawMaterialInEntity> Insert_entity_list = new List<MM_RawMaterialInEntity>();
                //更新数组
                List<MM_RawMaterialInEntity> Update_entity_list = new List<MM_RawMaterialInEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        MM_RawMaterialInEntity entity = new MM_RawMaterialInEntity();
                        //业务表Id
                        entity.BusinessId = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_27")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_27")];//业务表Id
                                                                                                                                                                                                                   //计划订单
                        entity.ProductOrder = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_28")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_28")];//计划订单
                                                                                                                                                                                                                     //原材料入库单号
                        entity.DocNum = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_29")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_29")];//原材料入库单号
                                                                                                                                                                                                               //物料编码
                        entity.MaterialCode = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_30")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_30")];//物料编码
                                                                                                                                                                                                                     //物料名称
                        entity.MaterialName = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_31")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_31")];//物料名称
                                                                                                                                                                                                                     //单位
                        entity.Unit = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_32")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_32")];//单位
                                                                                                                                                                                                             //批次号
                        entity.BatchNo = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_33")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_33")];//批次号
                                                                                                                                                                                                                //供应商
                        entity.SupplierCode = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_34")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_34")];//供应商
                                                                                                                                                                                                                     //质检状态
                        entity.QualityStatus = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_35")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_35")];//质检状态
                                                                                                                                                                                                                      //数量
                        entity.Qty = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_36")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_36")];//数量
                                                                                                                                                                                                            //入库类型
                        entity.InType = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_37")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_37")];//入库类型
                                                                                                                                                                                                               //入库仓库
                        entity.WhsCode = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_38")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_38")];//入库仓库
                                                                                                                                                                                                                //入库库位
                        entity.LocationCode = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_39")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_39")];//入库库位
                                                                                                                                                                                                                     //备注
                        entity.Remark = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_40")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_40")];//备注
                                                                                                                                                                                                               //创建人
                        entity.Creator = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_41")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_41")];//创建人
                                                                                                                                                                                                                //创建时间
                        entity.CreateTime = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_42")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_42")];//创建时间
                                                                                                                                                                                                                   //最后修改人
                        entity.ModifyBy = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_43")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_43")];//最后修改人
                                                                                                                                                                                                                 //最后修改时间
                        entity.ModifyTime = item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_44")] == null ? "" : item[Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_44")];//最后修改时间
                                                                                                                                                                                                                   //状态(启用/停用)
                                                                                                                                                                                                                   //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                                                                                                                                                                                                                   //创建日期// DateTime.Now;
                                                                                                                                                                                                                   //entity.CreatedDateTime = DateTimeOffset.Now;
                                                                                                                                                                                                                   ////是否删除
                                                                                                                                                                                                                   //entity.IsDeleted = false;
                                                                                                                                                                                                                   //entity.CreatedByCode = CreatedByCode;
                                                                                                                                                                                                                   //entity.CreatedByName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        MM_RawMaterialInEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _RawMaterialInBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _RawMaterialInBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_RawMaterialIn")]
        public HttpResponseMessage DeleteMM_RawMaterialIn(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                MM_RawMaterialInEntity entity = JsonConvert.DeserializeObject<MM_RawMaterialInEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_RawMaterialInEntity model = _RawMaterialInBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _RawMaterialInBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_48");//删除操作成功
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
        /// 功能描述: 获取实体
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_RawMaterialInEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _RawMaterialInBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_52");//获取详情数据成功
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_RawMaterialInEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _RawMaterialInBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_52");//获取详情数据成功
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
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

                var data = _RawMaterialInBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
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
                var list = _RawMaterialInBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
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
                var list = _RawMaterialInBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_RawMaterialIn_export")]
        public HttpResponseMessage MM_RawMaterialIn_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_7");//缺少Entity参数！
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
                var data = _RawMaterialInBLL.GetList_export(CreatedByCode, out msg);

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

        #region 公共方法
        /// <summary>
        /// 获取新批次
        /// </summary>
        /// <param name="batchDate"></param>
        /// <param name="batchCount"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNewBatch")]
        public List<string> GetNewBatchs(string batchDate, int batchCount)
        {
            List<string> lstBatchNo = new List<string>();
            string batchNo = "";
            string msg = "";
            int serialNo;//序列号

            var batchStr = batchDate.Replace("-", "").Substring(2);
            #region 旧逻辑
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("batchDate", batchDate);
            //var rawMaterialStockList = _rawMaterialStockBLL.GetList(dic, out msg);
            //if (rawMaterialStockList.Count() > 0)
            //{
            //    var batchList = rawMaterialStockList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
            //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4).ToInt()).Distinct().ToList();

            //    serialNo = batchList.Max() + 1;
            //}
            //else
            //{
            //    serialNo = 1;
            //}
            ////batchNo = batchStr + "-" + serialNo.ToString().PadLeft(4, '0');
            //batchNo = batchStr + serialNo.ToString().PadLeft(4, '0');
            //var batchEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == batchNo);
            //if (batchEntity != null)
            //{
            //    var oldBatchDateNo = batchEntity.BatchNo.Substring(0, 6);
            //    var oldBatchList = _rawMaterialStockBLL.Get_ExpressionList(t => t.BatchNo.Substring(0, 6) == oldBatchDateNo);
            //    var batchList = oldBatchList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
            //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4).ToInt()).Distinct().ToList();

            //    serialNo = batchList.Max() + 1;
            //}
            #endregion

            for (int i = 0; i < batchCount; i++)
            {
                //batchNo = batchStr + "-" + (serialNo + i).ToString().PadLeft(4, '0');
                //batchNo = batchStr + (serialNo + i).ToString().PadLeft(4, '0');
                batchNo = new BaseSequenceService().GetBatch(batchStr);
                lstBatchNo.Add(batchNo);
            }

            return lstBatchNo;
        }
        #endregion

        #region 收料通知单入库 、采购订单入库
        /// <summary>
        /// 功能描述: 收料通知单入库 、采购订单入库
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_RawMaterialIn")]
        public HttpResponseMessage SaveMM_RawMaterialIn(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var rawMaterialStockEntity = new MM_RawMaterialStockEntity();
            List<MM_RawMaterialInEntity> lstRawMaterialIn = new List<MM_RawMaterialInEntity>();//入库
            List<MM_RawMaterialStockEntity> lstRawMaterialStock = new List<MM_RawMaterialStockEntity>();//库存
            MM_ReceiptNoticeEntity receiptNoticeEntity = null;
            string inType = "";//入库类型  1：收料入库  2：采购入库
            List<MM_ReceiptNoticeEntity> lstReceiptNotice = new List<MM_ReceiptNoticeEntity>(); //采购入库使用
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                var purchaseId = getValue(jo, "PurchaseId");
                var arrivalQty = Convert.ToDecimal(getValue(jo, "ArrivalQty"));
                var batchCount = getValue(jo, "batchCount");//批次数量
                var iBatchCount = batchCount.ToInt();
                var isHePi = getValue(jo, "isHePi");//是否合批
                //参数转实体
                MM_RawMaterialInEntity entity = JsonConvert.DeserializeObject<MM_RawMaterialInEntity>(getValue(jo, "Entity"));
                if (string.IsNullOrEmpty(entity.BusinessTable))
                {
                    inType = "1";//收料入库
                    receiptNoticeEntity = _ReceiptNoticeBLL.Get_ExpressionEntity(t => t.Id == entity.BusinessId);
                    receiptNoticeEntity.BatchCount = receiptNoticeEntity.BatchCount == null ? 0 : receiptNoticeEntity.BatchCount;
                    receiptNoticeEntity.BatchCount += iBatchCount;
                    receiptNoticeEntity.ModifyBy = userCode;
                    receiptNoticeEntity.ModifyTime = DateTime.Now;
                    receiptNoticeEntity.ReceiptStatus = "4";//已完成
                    receiptNoticeEntity.InWhsTime = DateTime.Now;//入库时间
                    if (receiptNoticeEntity.InQty == null)
                        receiptNoticeEntity.InQty = 0;

                    receiptNoticeEntity.InQty += entity.Qty;
                }
                else
                {
                    inType = "2";//采购入库
                }

                //物料工厂数据
                var materialFactoryEntity = _baseMaterialFactoryBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                if (materialFactoryEntity == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_10");//物料工厂数据不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //校验是否IQC检 1开，0关
                var keyParamItemEntity = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch"
                    && t.ItemCode == "IQCCheckIncoming" && t.Remark1 == entity.FactoryCode);
                if (keyParamItemEntity == null)
                    return AjaxResult(false, Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_53"));//IQC检验开关没有维护！

                if (keyParamItemEntity.ItemValue == "1")
                {
                    if (inType == "1")//收料入库
                    {
                        var iqcEntity = _IQCQualityCheckBLL.Get_ExpressionEntity(test => test.ReceiptId == entity.BusinessId);
                        if (string.IsNullOrEmpty(iqcEntity?.TestResult))
                        {
                            result.success = false;
                            result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_12");//IQC未检验,不能入库
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                        else if (iqcEntity?.TestResult != "1")
                        {
                            result.success = false;
                            result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_13");//IQC检验不合格,不能入库
                            return Request.CreateResponse(HttpStatusCode.OK, result);
                        }
                    }
                }

                //判断物料是否超出单据（采购订单、收料通知单）数量
                if (inType == "1")//收料入库
                {
                    var rawMaterialInList = _RawMaterialInBLL.Get_ExpressionList(t => t.BusinessId == entity.BusinessId && t.MaterialCode == entity.MaterialCode);
                    var qty = entity.Qty;
                    if (rawMaterialInList.Count() > 0)
                    {
                        qty += rawMaterialInList.Sum(t => t.Qty);
                    }
                    //if (qty > arrivalQty)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_15");//入库数量不能超出单据数量
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                }

                #region 采购订单、收料通知单
                //采购订单
                var purchaseEntity = _PurchaseOrderBLL.GetEntity(purchaseId);
                if (purchaseEntity != null)
                {
                    purchaseEntity.ModifyBy = userCode;
                    purchaseEntity.ModifyTime = DateTime.Now;
                    purchaseEntity.ArrivalStatus = "2";//部分到货
                    if (purchaseEntity.InQty == null)
                        purchaseEntity.InQty = 0;

                    purchaseEntity.InQty += entity.Qty;
                }

                MM_ReceiptNoticeEntity receiptEntity = null; //采购入库用

                //同步SAP开关
                var factoryCode = entity.FactoryCode;
                var SAPSyncSwitch = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);

                if (inType == "2")//采购入库
                {
                    //开关 采购系数
                    var switchEntity = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.Remark1 == entity.FactoryCode && t.ItemCode == "Controlprocurement");
                    if (switchEntity?.ItemValue == "1")
                    {
                        var coefficient = string.IsNullOrEmpty(purchaseEntity.Coefficient) ? "1" : purchaseEntity.Coefficient;
                        var dCoefficient = coefficient.ToDecimal();
                        var allowQty = purchaseEntity.PurchaseNum * dCoefficient - arrivalQty;
                        if (entity.Qty > allowQty)
                            return AjaxResult(false, Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_17", allowQty.ToString()));//可入库数量为：{allowQty.ToString()}
                    }

                    if (SAPSyncSwitch?.ItemValue != "1")
                    {
                        var serialNo = _baseSequenceService.GetSerialNO("ReceiptNotice");
                        var receiptCode = DateTime.Now.ToString("yyMMdd") + serialNo;

                        #region 新增收料通知单
                        //var manufacturerCode = getValue(jo, "manufacturerCode");//厂家编码
                        //var manufacturerName = getValue(jo, "manufacturerName");//厂家名称
                        var supplierCode2 = getValue(jo, "SupplierCode2");
                        var supplierName2 = getValue(jo, "SupplierName2");
                        var supplierCode3 = getValue(jo, "SupplierCode3");
                        var supplierName3 = getValue(jo, "SupplierName3");
                        var supplierCode4 = getValue(jo, "SupplierCode4");
                        var supplierName4 = getValue(jo, "SupplierName4");
                        var supplierCode5 = getValue(jo, "SupplierCode5");
                        var supplierName5 = getValue(jo, "SupplierName5");
                        var supplierCode6 = getValue(jo, "SupplierCode6");
                        var supplierName6 = getValue(jo, "SupplierName6");

                        receiptEntity = new MM_ReceiptNoticeEntity();
                        receiptEntity.Id = Guid.NewGuid().ToString();
                        receiptEntity.FactoryCode = entity.FactoryCode;
                        receiptEntity.FactoryName = entity.FactoryName;
                        receiptEntity.PurchaseId = purchaseId;
                        receiptEntity.ReceiptCode = receiptCode;
                        receiptEntity.LineNum = "1";
                        receiptEntity.ArrivalQty = entity.Qty;
                        receiptEntity.ArrivalTime = DateTime.Now;
                        receiptEntity.ReceiptStatus = "4";
                        receiptEntity.Remark = "采购入库";//采购入库
                                                      //receiptEntity.ManufacturerCode = manufacturerCode;
                                                      //receiptEntity.ManufacturerName = manufacturerName;
                        receiptEntity.SupplierCode2 = supplierCode2;
                        receiptEntity.SupplierName2 = supplierName2;
                        receiptEntity.SupplierCode3 = supplierCode3;
                        receiptEntity.SupplierName3 = supplierName3;
                        receiptEntity.SupplierCode4 = supplierCode4;
                        receiptEntity.SupplierName4 = supplierName4;
                        receiptEntity.SupplierCode5 = supplierCode5;
                        receiptEntity.SupplierName5 = supplierName5;
                        receiptEntity.SupplierCode6 = supplierCode6;
                        receiptEntity.SupplierName6 = supplierName6;
                        receiptEntity.BatchCount = batchCount.ToIntOrNull();
                        receiptEntity.Creator = CurrentAccount.UserCode;
                        receiptEntity.CreateTime = DateTime.Now;
                        receiptEntity.MaterialCode = entity.MaterialCode;
                        receiptEntity.MaterialName = entity.MaterialName;
                        receiptEntity.Spec = entity.Spec;
                        receiptEntity.SmallClass = entity.SmallClass;
                        receiptEntity.SupplierCode = entity.SupplierCode;
                        receiptEntity.IsExemption = materialFactoryEntity.IsExemption;
                        receiptEntity.InWhsTime = DateTime.Now; //入库时间
                        receiptEntity.MaterialCode = purchaseEntity?.MaterialCode;
                        receiptEntity.MaterialName = purchaseEntity?.MaterialName;
                        receiptEntity.Spec = purchaseEntity?.Spec;
                        receiptEntity.SmallClass = purchaseEntity?.SmallClass;
                        receiptEntity.SupplierCode = purchaseEntity?.Supplier;
                        receiptEntity.InQty = entity.Qty;
                        #endregion

                        lstReceiptNotice.Add(receiptEntity);
                    }
                }
                else  //收料入库 校验数量  采购系数
                {
                    var rawMaterialInList = _RawMaterialInBLL.Get_ExpressionList(t => t.BusinessId == entity.BusinessId && t.MaterialCode == entity.MaterialCode);
                    var qty = rawMaterialInList.Sum(t => t.Qty); //已入库数量
                    //开关
                    var switchEntity = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.Remark1 == entity.FactoryCode && t.ItemCode == "Controlprocurement");
                    if (switchEntity?.ItemValue == "1")
                    {
                        if (purchaseEntity == null)
                            return AjaxResult(false, "采购订单不能为空");

                        var coefficient = string.IsNullOrEmpty(purchaseEntity.Coefficient) ? "1" : purchaseEntity.Coefficient;
                        var dCoefficient = coefficient.ToDecimal();
                        var oldReceiptEntity = _ReceiptNoticeBLL.Get_ExpressionEntity(t => t.Id == entity.BusinessId);
                        var allowQty = oldReceiptEntity?.ArrivalQty * dCoefficient - qty;
                        if (entity.Qty > allowQty)
                            return AjaxResult(false, Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_17", allowQty.ToString()));//可入库数量为：{allowQty.ToString()}
                    }
                }

                #endregion
                //入库记录
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.ProductOrder = purchaseEntity?.ProductOrder;
                    entity.DocNum = DateTime.Now.ToString("yyyyMMddHHmmss");
                    entity.Id = entity.GenGuid().ToString();
                    entity.QualityStatus = "1";//待检验
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                    entity.IsDeleted = false;
                    entity.BatchNo = entity.BatchNo ?? "";
                    entity.SupplierCode = entity.SupplierCode ?? "";

                    if (inType == "2")//采购入库
                    {
                        if (SAPSyncSwitch?.ItemValue != "1")
                        {
                            entity.InType = "1";//收料
                            entity.BusinessTable = "MM_ReceiptNotice";//收料通知单
                            entity.BusinessId = receiptEntity.Id;
                        }
                        else
                        {
                            entity.InType = "10"; //采购入库
                            entity.BusinessTable = "PL_PurchaseOrder";//采购订单
                            entity.BusinessId = purchaseEntity?.Id;
                            entity.PurchaseOrder = purchaseEntity?.PurchaseOrder;
                            entity.LineNum = purchaseEntity?.LineNum;
                        }
                    }
                    else
                    {
                        //收料通知单入库
                        entity.InType = "1";//收料
                        entity.BusinessTable = "MM_ReceiptNotice";//收料通知单
                        entity.BusinessId = receiptNoticeEntity.Id;
                        entity.PurchaseOrder = receiptNoticeEntity?.PurchaseOrder;
                        entity.LineNum = receiptNoticeEntity?.PurchaseOrderLineNum;
                        entity.ReceiptCode = receiptNoticeEntity.ReceiptCode;
                        entity.ReceiptLineNum = receiptNoticeEntity?.LineNum;
                    }

                    //if (isHePi == "0")//不合批  //Dragon 业务弃用 2024-04-02
                    //{
                    //    string batchDate = entity.BatchDate.Value.ToString("yyyy-MM-dd");
                    //    var lstBactchNo = GetNewBatchs(batchDate, batchCount.ToInt());
                    //    var rawInQty = entity.Qty;

                    //    var perBatchQty = Math.Floor(rawInQty.Value / iBatchCount);
                    //    if (perBatchQty < 1)
                    //        return AjaxResult(false, $"每批数量不能小于1");

                    //    int j = 0;
                    //    foreach (var item in lstBactchNo)
                    //    {
                    //        j += 1;
                    //        var rawInEntity = Tools.Clone(entity);
                    //        rawInEntity.Id = Guid.NewGuid().ToString();
                    //        rawInEntity.BatchNo = item;
                    //        rawInEntity.Qty = perBatchQty;
                    //        if (j == iBatchCount)
                    //            rawInEntity.Qty = rawInQty.Value - perBatchQty * (j - 1);
                    //        lstRawMaterialIn.Add(rawInEntity);
                    //    }
                    //}
                    //else
                    lstRawMaterialIn.Add(entity);
                }
                else
                {
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }

                #region 库存记录
                if (materialFactoryEntity.IsUsed.Value)  //如果启用批次管理，判断批次是否为空
                {
                    if (string.IsNullOrEmpty(entity.BatchNo))
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_21");//该物料启用了批次管理，批次不能为空
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //判断是否合批
                    var mbStockEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode
                         && t.SupplierCode == entity.SupplierCode && t.BatchNo == entity.BatchNo);
                    if (mbStockEntity != null)//合批后批次/纸盒日期也相同
                    {
                        entity.BatchDate = mbStockEntity.BatchDate;
                    }
                    //库存记录
                    rawMaterialStockEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode
                        && t.WhsCode == entity.WhsCode && t.LocationCode == entity.LocationCode && t.BatchNo == entity.BatchNo);
                    if (rawMaterialStockEntity != null)
                    {
                        rawMaterialStockEntity.Qty += entity.Qty;
                        rawMaterialStockEntity.ModifyBy = userCode;
                        rawMaterialStockEntity.ModifyTime = DateTime.Now;
                    }
                    else
                    {
                        rawMaterialStockEntity = new MM_RawMaterialStockEntity();
                        rawMaterialStockEntity.FactoryCode = entity.FactoryCode;
                        rawMaterialStockEntity.FactoryName = entity.FactoryName;
                        rawMaterialStockEntity.MaterialCode = entity.MaterialCode;
                        rawMaterialStockEntity.MaterialName = entity.MaterialName;
                        rawMaterialStockEntity.Spec = entity.Spec;
                        rawMaterialStockEntity.SmallClass = entity.SmallClass;
                        rawMaterialStockEntity.BatchNo = entity.BatchNo ?? "";
                        rawMaterialStockEntity.Qty = entity.Qty;
                        rawMaterialStockEntity.Unit = entity.Unit;
                        rawMaterialStockEntity.SupplierCode = entity.SupplierCode ?? "";
                        rawMaterialStockEntity.WhsCode = entity.WhsCode;
                        rawMaterialStockEntity.LocationCode = entity.LocationCode;
                        rawMaterialStockEntity.IsFrozen = "0";//未冻结
                        rawMaterialStockEntity.Creator = userCode;
                        rawMaterialStockEntity.CreateTime = DateTime.Now;
                        rawMaterialStockEntity.BatchDate = entity.BatchDate;
                    }

                    if (isHePi == "0")
                    {
                        foreach (var item in lstRawMaterialIn)
                        {
                            var rawStockEntity = Tools.Clone(rawMaterialStockEntity);
                            rawStockEntity.Id = Guid.NewGuid().ToString();
                            rawStockEntity.BatchNo = item.BatchNo ?? "";
                            rawStockEntity.Qty = item.Qty;
                            rawStockEntity.SupplierCode = rawStockEntity.SupplierCode ?? "";
                            lstRawMaterialStock.Add(rawStockEntity);
                        }
                    }
                }
                else
                {
                    //库存记录
                    rawMaterialStockEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode
                        && t.WhsCode == entity.WhsCode && t.LocationCode == entity.LocationCode);
                    if (rawMaterialStockEntity != null)
                    {
                        rawMaterialStockEntity.Qty += entity.Qty;
                        rawMaterialStockEntity.ModifyBy = userCode;
                        rawMaterialStockEntity.ModifyTime = DateTime.Now;
                        rawMaterialStockEntity.BatchNo = rawMaterialStockEntity.BatchNo ?? "";
                        rawMaterialStockEntity.SupplierCode = rawMaterialStockEntity.SupplierCode ?? "";
                    }
                    else
                    {
                        rawMaterialStockEntity = new MM_RawMaterialStockEntity();
                        rawMaterialStockEntity.FactoryCode = entity.FactoryCode;
                        rawMaterialStockEntity.FactoryName = entity.FactoryName;
                        rawMaterialStockEntity.MaterialCode = entity.MaterialCode;
                        rawMaterialStockEntity.MaterialName = entity.MaterialName;
                        rawMaterialStockEntity.Spec = entity.Spec;
                        rawMaterialStockEntity.SmallClass = entity.SmallClass;
                        rawMaterialStockEntity.Qty = entity.Qty;
                        rawMaterialStockEntity.Unit = entity.Unit;
                        //rawMaterialStockEntity.SupplierCode = entity.SupplierCode;
                        rawMaterialStockEntity.WhsCode = entity.WhsCode;
                        rawMaterialStockEntity.LocationCode = entity.LocationCode;
                        rawMaterialStockEntity.IsFrozen = "0";//未冻结
                        rawMaterialStockEntity.Creator = userCode;
                        rawMaterialStockEntity.CreateTime = DateTime.Now;
                        rawMaterialStockEntity.BatchNo = rawMaterialStockEntity.BatchNo ?? "";
                        rawMaterialStockEntity.SupplierCode = rawMaterialStockEntity.SupplierCode ?? "";
                    }
                }
                #endregion

                #region 同步SAP

                if (SAPSyncSwitch?.ItemValue == "1")
                {
                    IF108 sapRequest = new IF108();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF108.ToString();

                    sapRequest.RSQ_DATA = new IF108_RSQ_DATA();
                    IF108_HEAD IF108_HEAD = new IF108_HEAD();
                    IF108_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IF108_HEAD.BUDAT = entity.PostDate.Value.ToString("yyyyMMdd");
                    sapRequest.RSQ_DATA.IS_HEAD = IF108_HEAD;

                    List<IF108_ITEM> IT_ITEM = new List<IF108_ITEM>();
                    var arrMaterialCode = lstRawMaterialIn.Select(t => t.MaterialCode).Distinct().ToArray();
                    var materialList = _baseMaterialService.Get_ExpressionList(t => arrMaterialCode.Contains(t.MaterialCode)).ToList();

                    foreach (var item in lstRawMaterialIn)
                    {
                        IF108_ITEM IF108_ITEM = new IF108_ITEM();
                        IF108_ITEM.BWART = "101";
                        IF108_ITEM.MATNR = materialList.Find(t => t.MaterialCode == item.MaterialCode)?.SAPMaterialCode ?? "";
                        IF108_ITEM.WERKS = entity.FactoryCode ?? "";
                        IF108_ITEM.LGORT = entity.LocationCode ?? "";
                        IF108_ITEM.CHARG = entity.BatchNo ?? "";
                        IF108_ITEM.ERFMG = entity.Qty.ToString() ?? "";
                        IF108_ITEM.ERFME = entity.Unit ?? "";
                        IF108_ITEM.LIFNR = entity.SupplierCode ?? "";
                        IF108_ITEM.EBELN = entity.PurchaseOrder ?? "";
                        IF108_ITEM.EBELP = entity.LineNum ?? "";
                        IF108_ITEM.VBELN_IM = entity.ReceiptCode ?? "";
                        IF108_ITEM.VBELP_IM = entity.ReceiptLineNum ?? "";
                        IT_ITEM.Add(IF108_ITEM);
                    }
                    sapRequest.RSQ_DATA.IT_ITEM = IT_ITEM;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    entity.IsPosted = sapResult.Flag ? "1" : "";
                    entity.PostedMsg = sapResult.Msg;
                    entity.PostedTime = DateTime.Now;
                    entity.PostedUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                    entity.SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    entity.SAP_MJAHR = getValue(JObject.Parse(sapResult.Data), "EV_MJAHR");
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
                    if (receiptNoticeEntity != null)
                        _ReceiptNoticeBLL.SaveEntity(receiptNoticeEntity.Id, receiptNoticeEntity, out msg);

                    if (inType == "2" && lstReceiptNotice.Count > 0)//采购入库
                    {
                        _ReceiptNoticeBLL.SaveEntity("", receiptEntity, out msg);
                    }

                    if (isHePi == "0")
                        _rawMaterialStockBLL.SaveEntity_List(false, userName, lstRawMaterialStock, out msg);
                    else
                        _rawMaterialStockBLL.SaveEntity(rawMaterialStockEntity.Id, rawMaterialStockEntity, out msg);

                    _RawMaterialInBLL.SaveEntity_List(false, userName, lstRawMaterialIn, out msg);

                    _PurchaseOrderBLL.SaveEntity(purchaseId, purchaseEntity, out msg);

                    ts.Complete();
                }

                if (inType == "2" && lstReceiptNotice.Count > 0)//采购入库
                    _IQCQualityCheckBLL.CreateInspectNo(lstReceiptNotice);//创建IQC检验单

                result.returnMsg = Language.GetText("Common.Success");//操作成功
                var data = inType == "1" ? receiptNoticeEntity : receiptEntity;//收料入库
                result.resultData = new List<dynamic> { data };
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

        #region 采购订单、收料通知单 删除入库
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 采购订单、收料通知单 删除入库（逻辑删除）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:32:39
        /// 任务编号: 原材料入库单
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_RawMaterialIn")]
        public HttpResponseMessage RemoveMM_RawMaterialIn(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                MM_RawMaterialInEntity entity = JsonConvert.DeserializeObject<MM_RawMaterialInEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;

                var materialFactoryEntity = _baseMaterialFactoryBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                if (materialFactoryEntity == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_10");//物料工厂数据不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                MM_RawMaterialStockEntity rawMaterialStockEntity = null;
                if (materialFactoryEntity.IsUsed.Value)
                {
                    //库存记录
                    rawMaterialStockEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode
                        && t.LocationCode == entity.LocationCode && t.BatchNo == entity.BatchNo);
                }
                else
                {
                    //库存记录
                    rawMaterialStockEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode
                        && t.LocationCode == entity.LocationCode);
                }
                if (rawMaterialStockEntity == null)
                    return AjaxResult(false, Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_50"));//库存不存在！

                if (rawMaterialStockEntity.Qty < entity.Qty)
                    return AjaxResult(false, "库存不足");

                rawMaterialStockEntity.Qty -= entity.Qty;
                rawMaterialStockEntity.ModifyBy = userCode;
                rawMaterialStockEntity.ModifyTime = DateTime.Now;

                #region 更新采购订单 Or 收料通知单数量
                MM_ReceiptNoticeEntity receiptNoticeEntity = null;
                var purchaseOrderEntity = _PurchaseOrderBLL.Get_ExpressionEntity(t => t.Id == entity.BusinessId);
                if (purchaseOrderEntity != null)
                {
                    purchaseOrderEntity.InQty -= entity.Qty;
                    purchaseOrderEntity.ModifyBy = CurrentAccount.UserCode;
                    purchaseOrderEntity.ModifyTime = DateTime.Now;
                    if (purchaseOrderEntity.InQty == 0)
                    {
                        purchaseOrderEntity.ArrivalStatus = "1";
                    }
                }
                else
                {
                    receiptNoticeEntity = _ReceiptNoticeBLL.Get_ExpressionEntity(t => t.Id == entity.BusinessId);
                    if (receiptNoticeEntity == null)
                        return AjaxResult(false, Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_54"));//未找到原单据

                    receiptNoticeEntity.InQty -= entity.Qty;
                    receiptNoticeEntity.ModifyBy = CurrentAccount.UserCode;
                    receiptNoticeEntity.ModifyTime = DateTime.Now;
                    if (receiptNoticeEntity.InQty == 0)
                    {
                        receiptNoticeEntity.ReceiptStatus = "3";
                    }

                    purchaseOrderEntity = _PurchaseOrderBLL.Get_ExpressionEntity(t => t.Id == receiptNoticeEntity.PurchaseId);
                    if (purchaseOrderEntity != null)
                    {
                        purchaseOrderEntity.InQty -= entity.Qty;
                        purchaseOrderEntity.ModifyBy = CurrentAccount.UserCode;
                        purchaseOrderEntity.ModifyTime = DateTime.Now;
                        if (purchaseOrderEntity.InQty == 0)
                        {
                            purchaseOrderEntity.ArrivalStatus = "1";
                        }
                    }
                }
                #endregion

                #region 同步SAP
                var factoryCode = entity.FactoryCode;

                var SAPSyncSwitch = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "SAPSync"
                    && t.Remark1 == factoryCode);
                if (SAPSyncSwitch?.ItemValue == "1") // 1：开
                {
                    var rawInEntity = _RawMaterialInBLL.Get_ExpressionEntity(t => t.Id == Id);
                    var materialEntity = _baseMaterialService.Get_ExpressionEntity(t => t.MaterialCode == rawInEntity.MaterialCode);

                    IF109 sapRequest = new IF109();
                    sapRequest.HEAD = new SapHeadDto();
                    sapRequest.HEAD.INIF_ID = SAPInterface.IF109.ToString();

                    sapRequest.RSQ_DATA = new IF109_RSQ_DATA();
                    IF109_HEAD IS_HEAD = new IF109_HEAD();
                    IS_HEAD.MBLNR = rawInEntity.SAP_MBLNR ?? "";
                    IS_HEAD.MJAHR = DateTime.Now.Year.ToString();
                    IS_HEAD.BLDAT = DateTime.Now.ToString("yyyyMMdd");
                    IS_HEAD.BUDAT = DateTime.Now.ToString("yyyyMMdd");
                    IS_HEAD.MATNR = materialEntity?.SAPMaterialCode ?? "";
                    IS_HEAD.EBELN = rawInEntity.PurchaseOrder ?? "";
                    IS_HEAD.EBELP = rawInEntity.LineNum ?? "";
                    if (rawInEntity.InType == "1")
                    {
                        IS_HEAD.VBELN_IM = rawInEntity.ReceiptCode ?? "";
                        IS_HEAD.VBELP_IM = rawInEntity.ReceiptLineNum ?? "";
                    }
                    sapRequest.RSQ_DATA.IS_HEAD = IS_HEAD;

                    var sapResult = SAPHelper.Instance.PostToSAP(sapRequest.HEAD.INIF_ID, sapRequest);
                    if (!sapResult.Flag)
                        return AjaxResult(false, sapResult.Msg);

                    entity.Off_IsPosted = sapResult.Flag ? "1" : "";
                    entity.Off_PostedMsg = sapResult.Msg;
                    entity.Off_PostedTime = DateTime.Now;
                    entity.Off_PostedUser = CurrentAccount.UserCode + "-" + CurrentAccount.UserName;
                    entity.Off_SAP_MBLNR = getValue(JObject.Parse(sapResult.Data), "EV_MBLNR");
                    entity.Off_SAP_MJAHR = getValue(JObject.Parse(sapResult.Data), "EV_MJAHR");
                    entity.IsDeleted = true;
                }
                #endregion

                string msg = "";
                using (TransactionScope ts = new TransactionScope())
                {
                    _rawMaterialStockBLL.SaveEntity(rawMaterialStockEntity.Id, rawMaterialStockEntity, out msg);
                    if (SAPSyncSwitch?.ItemValue == "1")
                        _RawMaterialInBLL.SaveEntity(entity.Id, entity, out msg);
                    else
                        _RawMaterialInBLL.RemoveForm(t => t.Id == Id);

                    if (purchaseOrderEntity != null)
                        _PurchaseOrderBLL.SaveEntity(purchaseOrderEntity.Id, purchaseOrderEntity, out msg);

                    if (receiptNoticeEntity != null)
                        _ReceiptNoticeBLL.SaveEntity(receiptNoticeEntity.Id, receiptNoticeEntity, out msg);

                    ts.Complete();
                }

                result.success = true;
                //if (isok > 0)
                result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_48");//删除操作成功
                //else
                //    result.returnMsg = Language.GetText("MaterialManage.MM_RawMaterialInController.Tips_51");//删除操作失败
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
