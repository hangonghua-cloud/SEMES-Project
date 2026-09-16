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
using ALP.Application.Busines.QualityManage;
using ALP.Application.Entity.QualityManage;
using System.Globalization;
using ALP.Application.Busines.Material;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Service.Material;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-25
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_ReceiptNoticeController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 收料通知单表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_ReceiptNotice")]
    public class MM_ReceiptNoticeController : ApiBaseController
    {
        private MM_ReceiptNoticeBLL _ReceiptNoticeBLL = new MM_ReceiptNoticeBLL();
        private QC_IQCQualityCheck_BLL _IQCQualityCheckBLL = new QC_IQCQualityCheck_BLL();
        private QC_IQCQualityCheckItem_BLL _IQCQualityCheckitemBLL = new QC_IQCQualityCheckItem_BLL();
        private MM_RawMaterialStockBLL _rawMaterialStockBLL = new MM_RawMaterialStockBLL();
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();
        private Base_KeyParameterItemBLL _baseKeyParameterItemBLL = new Base_KeyParameterItemBLL();//关键参数列表
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ReceiptNoticePageList")]
        public HttpResponseMessage MM_ReceiptNoticePageList(JObject jo)
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

                var data = _ReceiptNoticeBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ReceiptNoticePageDataTableList")]
        public HttpResponseMessage MM_ReceiptNoticePageDataTableList(JObject jo)
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

                var data = _ReceiptNoticeBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_ReceiptNoticeList")]
        public HttpResponseMessage GetMM_ReceiptNoticeList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _ReceiptNoticeBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_ReceiptNotice")]
        public HttpResponseMessage SaveMM_ReceiptNotice(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {

                MM_ReceiptNoticeEntity entity = JsonConvert.DeserializeObject<MM_ReceiptNoticeEntity>(getValue(jo, "Entity"));


                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Creator = CurrentAccount.UserCode;
                    entity.CreateTime = DateTime.Now;
                    entity.IsDeleted = false;
                }

                string msg = "";
                int isok = _ReceiptNoticeBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_ReceiptNotice")]
        public HttpResponseMessage SaveBatchMM_ReceiptNotice(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult();
            result.resultData = null;
            var index = 1;
            var retunNum = "";
            var isok = 0;
            var msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");

            try
            {
                var list = JsonConvert.DeserializeObject<List<MM_ReceiptNoticeEntity>>(getValue(jo, "data"));
                var FactoryCode = list[0].FactoryCode;
                //判断到料系数开关是否开关
                //判断是否超到料
                var count = 0;
                var PurchaseOrder = "";
                Decimal Coefficient = 0;
                //查找维护的开关是否是开启状态
                var Base_KeyParameterItemEntity = new Base_KeyParameterItem_Service().Get_ExpressionList(t => t.EnCode == "Switch" && t.ItemCode == "Controlprocurement" && t.Remark1 == FactoryCode).ToList();
                if (Base_KeyParameterItemEntity.FirstOrDefault().ItemValue == "1")
                {
                    list.ForEach(t =>
                    {
                        //查找物料维护的系数
                        var CGDLXS = (from a in new Base_MaterialFactory_Service().Get_ExpressionList(a => a.MaterialCode == t.MaterialCode && a.FactoryCode == t.FactoryCode).ToList()
                                      join b in new Base_MaterialFacet_Service().Get_ExpressionList(b => b.AttrCode == "CGDLXS").ToList()
                                      on a.Id equals b.MaterialFactoryId
                                      select b
                                           ).FirstOrDefault();
                        if (CGDLXS != null)
                        {
                            if (CGDLXS.AttrValue.Length > 0)
                            {
                                Coefficient = CGDLXS.AttrValue.ToDecimal();
                            }
                            else
                            {
                                Coefficient = 1;
                            }
                        }
                        else
                        {
                            Coefficient = 1;

                        }

                        //查找之前是否有过收料
                        Decimal oldArrivalQty = 0;
                        var table = new MM_ReceiptNotice_Service().GET_ArrivalQtyCount(t.PurchaseId, out msg);
                        if (table.Rows.Count > 0)
                        {
                            oldArrivalQty = table.Rows[0][0].ToDecimal();
                        }
                        var newArrivalQty = oldArrivalQty + t.ArrivalQty;
                        var caiQty = t.PurchaseNum * Coefficient;
                        if (newArrivalQty > caiQty)
                        {
                            count = count + 1;
                            PurchaseOrder = PurchaseOrder + "——" + t.PurchaseOrder;

                        }


                    });
                    //甲方要求去掉 2023-11-04
                    //if (count > 0)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = PurchaseOrder + Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_10");//创建通知单数量大于允许采购入库的数量
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                }
                if (!_ReceiptNoticeBLL.GetSerialNO("ReceiptNotice", out retunNum, out msg))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_11");//流水号生成错误！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (string.IsNullOrEmpty(keyValue))
                {
                    var ReceiptCode = DateTime.Now.ToString("yyMMdd") + retunNum.PadLeft(3, '0');

                    //判断无需检验
                    var query = from t in list
                                join mf in _MaterialFactoryBLL.Get_ExpressionList(t => true)
                                on new { t.MaterialCode, t.FactoryCode } equals new { mf.MaterialCode, mf.FactoryCode }
                                select new MM_ReceiptNoticeEntity()
                                {
                                    FactoryCode = t.FactoryCode,
                                    PurchaseId = t.PurchaseId,
                                    MaterialCode = t.MaterialCode,
                                    MaterialName = t.MaterialCode,
                                    Spec = t.Spec,
                                    SmallClass = t.SmallClass,
                                    SupplierCode = t.SupplierCode,
                                    ArrivalQty = t.ArrivalQty,
                                    ArrivalTime = t.ArrivalTime,
                                    Remark = t.Remark,
                                    ReceiptStatus = t.ReceiptStatus,
                                    IsExemption = mf.IsExemption,
                                };
                    var factoryCode = list.FirstOrDefault()?.FactoryCode;
                    var keyParamItemEntity = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch"
                        && t.ItemCode == "IQCCheckIncoming" && t.Remark1 == factoryCode);

                    list.ForEach(t =>
                    {
                        t.Create();
                        t.ReceiptCode = ReceiptCode;
                        t.LineNum = (index++).ToString();
                        t.Creator = userCode;
                        t.CreateTime = DateTime.Now;
                        if (t.IsExemption == "1" || keyParamItemEntity.ItemValue == "0")//IsExemption:1 免检,ItemValue 0 开关
                        {
                            t.ReceiptStatus = "3";//待入库
                        }
                        t.IsDeleted = false;
                    });
                    //创建IQC检验单
                    _IQCQualityCheckBLL.CreateInspectNo(list);

                    isok = _ReceiptNoticeBLL.SaveEntity_List(false, userCode, list, out msg);
                }
                else
                {
                    isok = _ReceiptNoticeBLL.SaveEntity_List(true, userCode, list, out msg);
                }

                result.success = isok > 0 ? true : false;
                result.resultData = list;
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_ReceiptNotice")]
        public HttpResponseMessage DeleteMM_ReceiptNotice(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                MM_ReceiptNoticeEntity entity = JsonConvert.DeserializeObject<MM_ReceiptNoticeEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ReceiptNoticeEntity model = _ReceiptNoticeBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _ReceiptNoticeBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_14");//删除操作成功
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_ReceiptNotice")]
        public HttpResponseMessage RemoveMM_ReceiptNotice(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var msg = "";
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                MM_ReceiptNoticeEntity entity = JsonConvert.DeserializeObject<MM_ReceiptNoticeEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;

                var qcEntity = _IQCQualityCheckBLL.Get_ExpressionEntity(t => t.ReceiptId == Id);


                //if (qcEntity != null)
                //{
                //    result.success = false;
                //    result.returnMsg = "已经存在检验记录,请先删除检验记录";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}

                var keyParamItemEntity = _baseKeyParameterItemBLL.Get_ExpressionEntity(t => t.EnCode == "Switch" && t.ItemCode == "IQCCheckIncoming");

                if (entity.ReceiptStatus != "1" && keyParamItemEntity.ItemValue == "1")//ReceiptStatus: 1 创建，ItemValue 1 开关
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_16");//通知单状态为创建时才能删除
                    return Request.CreateResponse(HttpStatusCode.OK, result);

                }
                else if (entity.ReceiptStatus != "1" && keyParamItemEntity.ItemValue == "0")
                {
                    if (_IQCQualityCheckitemBLL.Get_ExpressionList(t => t.IQCId == qcEntity.Id).Count() > 0)
                    {
                        //甲方要求去掉

                        //result.success = false;
                        //result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_17");//已有检验记录，无法删除
                        //return Request.CreateResponse(HttpStatusCode.OK, result);

                    }

                }


                //删除
                int isok = _ReceiptNoticeBLL.RemoveForm(t => t.Id == Id);
                _IQCQualityCheckBLL.Delete_SQL(qcEntity.Id, out msg);

                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_14");//删除操作成功
                else
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_18");//删除操作失败
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ReceiptNoticeEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ReceiptNoticeBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ReceiptNoticeEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _ReceiptNoticeBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_19");//获取详情数据成功
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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

                var data = _ReceiptNoticeBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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
                var list = _ReceiptNoticeBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
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
                var list = _ReceiptNoticeBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-25 10:31:16
        /// 任务编号: 收料通知单表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_ReceiptNotice_export")]
        public HttpResponseMessage MM_ReceiptNotice_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_7");//缺少Entity参数！
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
                var data = _ReceiptNoticeBLL.GetList_export(CreatedByCode, out msg);

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
        /// 合并批次数据源
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaterialStockBatch")]
        public HttpResponseMessage GetMaterialStockBatch(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var materialCode = getValue(jo, "materialCode");//物料编码
                var supplierCode = getValue(jo, "supplierCode");//供应商编码
                var factoryCode = getValue(jo, "factoryCode");//工厂编码

                var rawMaterialStockList = _rawMaterialStockBLL.Get_ExpressionList(t => t.FactoryCode == factoryCode
                    && t.MaterialCode == materialCode && t.SupplierCode == supplierCode);
                var batchList = rawMaterialStockList?.Where(t => !string.IsNullOrEmpty(t.BatchNo)).Select(t => t.BatchNo).Distinct().ToList();
                if (batchList.Count > 0)
                    batchList = batchList.OrderByDescending(t => Convert.ToInt32(t.Substring(0, 6))).ToList();

                result.resultData = batchList;
                result.success = true;
                //result.returnMsg = msg;
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
        /// 获取新批次
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetNewBatch")]
        public HttpResponseMessage GetNewBatch(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string batchNo = "";
                var msg = "";

                var batchDate = getValue(jo, "batchDate");
                var batchStr = batchDate.Replace("-", "").Substring(2);
                if (string.IsNullOrEmpty(batchDate))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("MaterialManage.MM_ReceiptNoticeController.Tips_21");//批次日期不能为空
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                #region 旧逻辑
                //Dictionary<string, object> dic = new Dictionary<string, object>();
                //dic.Add("batchDate", batchDate);
                //var rawMaterialStockList = _rawMaterialStockBLL.GetList(dic, out msg);
                //if (rawMaterialStockList.Count() > 0)
                //{
                //    var batchList = rawMaterialStockList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
                //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4).ToInt())
                //        .Distinct()
                //        .ToList();
                //    //batchNo = batchStr + "-" + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                //    batchNo = batchStr + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                //}
                //else
                //{
                //    //batchNo = batchStr + "-0001";
                //    batchNo = batchStr + "0001";
                //}
                //var batchEntity = _rawMaterialStockBLL.Get_ExpressionEntity(t => t.BatchNo == batchNo);
                //if (batchEntity != null)
                //{
                //    var oldBatchDateNo = batchEntity.BatchNo.Substring(0, 6);
                //    var oldBatchList = _rawMaterialStockBLL.Get_ExpressionList(t => t.BatchNo.Substring(0, 6) == oldBatchDateNo);
                //    var batchList = oldBatchList?.Where(t => !string.IsNullOrEmpty(t.BatchNo))
                //        .Select(t => t.BatchNo.Substring(t.BatchNo.Length - 4).ToInt()).Distinct().ToList();
                //    //batchNo = batchStr + "-" + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                //    batchNo = batchStr + (batchList.Max() + 1).ToString().PadLeft(4, '0');
                //}
                #endregion
                batchNo = new BaseSequenceService().GetBatch(batchStr);

                result.resultData = batchNo;
                result.success = true;
                //result.returnMsg = msg;
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
    }
}
