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

namespace ALP.Application.WebApi.Controllers.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_PackingRecordController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 包装记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_PackingBGTransferCard")]
    public class PM_PackingBGTransferCardController : ApiBaseController
    {
        private PM_PackingBGTransferCardBLL _PackingBGTransferCardBLL = new PM_PackingBGTransferCardBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_PackingBGTransferCardPageList")]
        public HttpResponseMessage PM_PackingBGTransferCardPageList(JObject jo)
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
                
                var data = _PackingBGTransferCardBLL.GetPageList(pagination, queryJson);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_PackingBGTransferCardPageDataTableList")]
        public HttpResponseMessage PM_PackingBGTransferCardPageDataTableList(JObject jo)
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
                
                var data = _PackingBGTransferCardBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_PackingBGTransferCardList")]
        public HttpResponseMessage GetPM_PackingBGTransferCardList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                
                string msg = "";
                var list = _PackingBGTransferCardBLL.GetList(checkType, out msg);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_PackingBGTransferCard")]
        public HttpResponseMessage SavePM_PackingBGTransferCard(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");

            try
            {
 
                PM_PackingBGTransferCardEntity entity = JsonConvert.DeserializeObject<PM_PackingBGTransferCardEntity>(getValue(jo, "Entity"));
                  
                
                string msg = "";
                int isok = _PackingBGTransferCardBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_PackingBGTransferCard")]
        public HttpResponseMessage SaveBatchPM_PackingBGTransferCard(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_10");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_11");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _PackingBGTransferCardBLL.GetList("", out msg);
                //插入数组
                List<PM_PackingBGTransferCardEntity> Insert_entity_list = new List<PM_PackingBGTransferCardEntity>();
                //更新数组
                List<PM_PackingBGTransferCardEntity> Update_entity_list = new List<PM_PackingBGTransferCardEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_PackingBGTransferCardEntity entity = new PM_PackingBGTransferCardEntity();
                        //包装Id
                       
                        //订单号
                        entity.ProductOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_12")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_12")];//订单号
                        //工单号
                        entity.WorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_13")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_13")];//工单号
                        //柜号
                        entity.ContainerNO =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_14")];//柜号
                        //PO号
                        entity.CustomerPO =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_15")];//PO号
                        //客户型号
                        entity.MaterialCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_16")];//客户型号
                        //流转卡
                        entity.CardCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_17")];//流转卡
                        //成品返工单号
                        entity.ReWorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_18")];//成品返工单号
                        //报工数量
                        entity.Qty =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_19")];//报工数量
                        //报工工序编码
                        entity.ProcessCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_20")];//报工工序编码
                        //报工人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_21")];//报工人
                        //报工时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_22")];//报工时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_23")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_24")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        ////是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_PackingBGTransferCardEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = _PackingBGTransferCardBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _PackingBGTransferCardBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_PackingBGTransferCard")]
        public HttpResponseMessage DeletePM_PackingBGTransferCard(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_PackingBGTransferCardEntity entity = JsonConvert.DeserializeObject<PM_PackingBGTransferCardEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_PackingBGTransferCardEntity model = _PackingBGTransferCardBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _PackingBGTransferCardBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_27");//删除操作成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_PackingBGTransferCard")]
        public HttpResponseMessage RemovePM_PackingBGTransferCard(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_PackingBGTransferCardEntity entity = JsonConvert.DeserializeObject<PM_PackingBGTransferCardEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_PackingBGTransferCardEntity model = _PackingBGTransferCardBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _PackingBGTransferCardBLL.RemoveForm(Id, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_27");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_29");//删除操作失败
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_PackingBGTransferCardEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _PackingBGTransferCardBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_30");//获取详情数据成功
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
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_PackingBGTransferCardEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _PackingBGTransferCardBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_30");//获取详情数据成功
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
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
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
                
                var data = _PackingBGTransferCardBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 ).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
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
                var list = _PackingBGTransferCardBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
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
                var list = _PackingBGTransferCardBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-21 16:08:03
        /// 任务编号: 包装记录表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_PackingBGTransferCard_export")]
        public HttpResponseMessage PM_PackingBGTransferCard_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_PackingBGTransferCardController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                
                string msg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }
                
                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _PackingBGTransferCardBLL.GetList_export(CreatedByCode, out msg);
                
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
