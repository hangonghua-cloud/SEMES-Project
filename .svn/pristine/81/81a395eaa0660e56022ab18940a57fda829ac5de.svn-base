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

namespace ALP.Application.WebApi.Controllers.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-11
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TransferCardController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 流转卡信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_TransferCard")]
    public class PM_TransferCardController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_TransferCardPageList")]
        public HttpResponseMessage PM_TransferCardPageList(JObject jo)
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
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                var data = _Service.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total: data.Count(),
                    page = pagination != null ? pagination.total: data.Count(),
                    records = pagination != null ? pagination.total: data.Count(),
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_TransferCardPageDataTableList")]
        public HttpResponseMessage PM_TransferCardPageDataTableList(JObject jo)
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
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_TransferCardList")]
        public HttpResponseMessage GetPM_TransferCardList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_TransferCard")]
        public HttpResponseMessage SavePM_TransferCard(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //if (string.IsNullOrEmpty(userCode))
            //{
                //result.success = false;
                //result.returnMsg = "用户名不能为空！";
                //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}
            
            try
            {
                //业务服务类
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                //参数转实体
                PM_TransferCardEntity entity = JsonConvert.DeserializeObject<PM_TransferCardEntity>(getValue(jo, "Entity"));
                //订单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.ProductOrder))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_8");//订单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WorkOrder))
                {
                    //工单号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_9");//工单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WorkOrderType))
                {
                    //工单类型 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_10");//工单类型不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ExeWorkOrder))
                {
                    //执行工单号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_11");//执行工单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CardCode))
                {
                    //流转卡编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_12");//流转卡编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CardName))
                {
                    //流转卡名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_13");//流转卡名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CardType))
                {
                    //流转卡类型 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_14");//流转卡类型不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ContainerNO))
                {
                    //柜号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_15");//柜号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.BJGY))
                {
                    //背胶工艺 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_16");//背胶工艺不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WorkOrderRemark))
                {
                    //工单备注 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_17");//工单备注不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialCode))
                {
                    //物料编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_18");//物料编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Spec))
                {
                    //规格型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_19");//规格型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MMXH))
                {
                    //面膜型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_20");//面膜型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.JCGG))
                {
                    //挤出规格 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_21");//挤出规格不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.BWXH))
                {
                    //板纹型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_22");//板纹型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.KCKX))
                {
                    //开槽扣型 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_23");//开槽扣型不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.UV))
                {
                    //UV 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_24");//UV不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TPGG))
                {
                    //托盘规格 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_25");//托盘规格不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Description))
                {
                    //说明书 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_26");//说明书不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.PalletNum))
                {
                    //托盘编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_27");//托盘编号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.PaperBoxModel))
                {
                    //纸盒型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_28");//纸盒型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.PrintStatus))
                {
                    //打印状态 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_29");//打印状态不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Creator))
                {
                    //创建人编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_30");//创建人编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ModifyBy))
                {
                    //修改人编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_31");//修改人编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    entity.ModifyBy = CurrentAccount.UserCode;
                }
                else
                {

                    entity.Creator = CurrentAccount.UserCode;
                    entity.CreateTime = DateTime.Now;
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_TransferCard")]
        public HttpResponseMessage SaveBatchPM_TransferCard(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_34");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_35");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<PM_TransferCardEntity> Insert_entity_list = new List<PM_TransferCardEntity>();
                //更新数组
                List<PM_TransferCardEntity> Update_entity_list = new List<PM_TransferCardEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_TransferCardEntity entity = new PM_TransferCardEntity();
                        //订单号
                        entity.ProductOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_36")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_36")];//订单号
                        //工单号
                        entity.WorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_37")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_37")];//工单号
                        //工单类型
                        entity.WorkOrderType =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_38")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_38")];//工单类型
                        //执行工单号
                        entity.ExeWorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_39")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_39")];//执行工单号
                        //流转卡编码
                        entity.CardCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_40")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_40")];//流转卡编码
                        //流转卡名称
                        entity.CardName =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_41")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_41")];//流转卡名称
                        //流转卡类型
                        entity.CardType =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_42")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_42")];//流转卡类型
                        //单拖张数
                        //entity.PerPalletQty =  item["单拖张数"] == null ? "" : item["单拖张数"];
                        //柜号
                        entity.ContainerNO =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_43")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_43")];//柜号
                        //背胶工艺
                        entity.BJGY =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_44")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_44")];//背胶工艺
                        //工单备注
                        entity.WorkOrderRemark =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_45")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_45")];//工单备注
                        //物料编码
                        entity.MaterialCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_46")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_46")];//物料编码
                        //规格型号
                        entity.Spec =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_47")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_47")];//规格型号
                        //面膜型号
                        entity.MMXH =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_48")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_48")];//面膜型号
                        //挤出规格
                        entity.JCGG =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_49")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_49")];//挤出规格
                        //板纹型号
                        entity.BWXH =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_50")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_50")];//板纹型号
                        //生产托盘数量（张）
                        entity.SCTPSL =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_51")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_51")];//生产托盘数量（张）
                        //生产托盘数量（片）
                        entity.SCTPSLP =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_52")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_52")];//生产托盘数量（片）
                        //包装托盘数量
                        entity.BZTPSL =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_53")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_53")];//包装托盘数量
                        //开槽扣型
                        entity.KCKX =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_54")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_54")];//开槽扣型
                        //UV
                        entity.UV =  item["UV"] == null ? "" : item["UV"];
                        //订单张数
                        entity.TotalSheets =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_55")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_55")];//订单张数
                        //生产张数
                        entity.ActualSheets =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_56")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_56")];//生产张数
                        //订单片数
                        entity.OrderPieces =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_57")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_57")];//订单片数
                        //生产片数
                        entity.ProductPieces =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_58")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_58")];//生产片数
                        //托盘规格
                        entity.TPGG =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_59")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_59")];//托盘规格
                        //单柜拖数
                        entity.OrderPallet =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_60")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_60")];//单柜拖数
                        //单柜盒数
                        entity.PerPallerBox =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_61")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_61")];//单柜盒数
                        //单盒片数
                        entity.BZDHSL =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_62")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_62")];//单盒片数
                        //说明书
                        entity.Description =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_63")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_63")];//说明书
                        //托盘编号
                        entity.PalletNum =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_64")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_64")];//托盘编号
                        //纸盒型号
                        entity.PaperBoxModel =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_65")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_65")];//纸盒型号
                        //纸盒日期
                        entity.BoxDate = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_66")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_66")];//纸盒日期
                        //冻结标记
                        //entity.FrozenMark = false;
                        ////返工标记
                        //entity.ReworkMark = false;
                        ////报废标记
                        //entity.ScrapMark = false;
                        //打印状态
                        entity.PrintStatus =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_67")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_67")];//打印状态
                        //创建人编码
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_68")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_68")];//创建人编码
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_69")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_69")];//创建时间
                        //修改人编码
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_70")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_70")];//修改人编码
                        //修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_71")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_71")];//修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                      
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_TransferCardEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_TransferCard")]
        public HttpResponseMessage DeletePM_TransferCard(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                PM_TransferCardEntity entity = JsonConvert.DeserializeObject<PM_TransferCardEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_TransferCardEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, CurrentAccount.UserName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_74");//删除操作成功
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_TransferCard")]
        public HttpResponseMessage RemovePM_TransferCard(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                PM_TransferCardEntity entity = JsonConvert.DeserializeObject<PM_TransferCardEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_TransferCardEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, CurrentAccount.UserName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_74");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_76");//删除操作失败
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_TransferCardEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_77");//获取详情数据成功
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_TransferCardEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_77");//获取详情数据成功
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
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
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
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
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_TransferCard_export")]
        public HttpResponseMessage PM_TransferCard_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                PM_TransferCard_Service _Service = new PM_TransferCard_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        
    }
}
