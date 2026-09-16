using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_PrdOrderReqMaterialsController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 订单物料需求表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_PrdOrderReqMaterials")]
    public class PL_PrdOrderReqMaterialsController : ApiBaseController
    {
        private PL_PrdOrderReqMaterialsBLL _PrdOrderReqMaterialsBLL = new PL_PrdOrderReqMaterialsBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 获取物料需求
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetWorkOrderReqMaterials")]
        public HttpResponseMessage GetWorkOrderReqMaterials(JObject jo)
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

                var data = _PrdOrderReqMaterialsBLL.GetWorkOrderReqMaterials(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_PrdOrderReqMaterialsPageList")]
        public HttpResponseMessage PL_PrdOrderReqMaterialsPageList(JObject jo)
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
                
                var data = _PrdOrderReqMaterialsBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_PrdOrderReqMaterialsPageDataTableList")]
        public HttpResponseMessage PL_PrdOrderReqMaterialsPageDataTableList(JObject jo)
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
                
                var data = _PrdOrderReqMaterialsBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_PrdOrderReqMaterialsList")]
        public HttpResponseMessage GetPL_PrdOrderReqMaterialsList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                
                string msg = "";
                var list = _PrdOrderReqMaterialsBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_PrdOrderReqMaterials")]
        public HttpResponseMessage SavePL_PrdOrderReqMaterials(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_7");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_8");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            string queryJson = getValue(jo, "Entity");
            try
            {
                //业务服务类
                
                //参数转实体
                PL_PrdOrderReqMaterialsEntity entity = JsonConvert.DeserializeObject<PL_PrdOrderReqMaterialsEntity>(getValue(jo, "Entity"));
         
                
              
                
                
                
                string msg = "";
                int isok = _PrdOrderReqMaterialsBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_PrdOrderReqMaterials")]
        public HttpResponseMessage SaveBatchPL_PrdOrderReqMaterials(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_11");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_12");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_8");//缺少Entity参数！
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
                var old_entity_list = _PrdOrderReqMaterialsBLL.GetList("", out msg);
                //插入数组
                List<PL_PrdOrderReqMaterialsEntity> Insert_entity_list = new List<PL_PrdOrderReqMaterialsEntity>();
                //更新数组
                List<PL_PrdOrderReqMaterialsEntity> Update_entity_list = new List<PL_PrdOrderReqMaterialsEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PL_PrdOrderReqMaterialsEntity entity = new PL_PrdOrderReqMaterialsEntity();
                    
                        //工单编码
                        entity.WorkOrder =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_13")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_13")];//工单编码
                      
                        //物料编码
                        entity.MaterialCode =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_14")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_14")];//物料编码
                        //物料名称
                        entity.MaterialName =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_15")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_15")];//物料名称
                        //物料需求量
                        entity.Amount =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_16")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_16")];//物料需求量
                        //创建人
                        entity.Creator =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_17")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_17")];//创建人
                        //创建时间
                        entity.CreateTime =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_18")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_18")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_19")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_19")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime =  item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_20")] == null ? "" : item[Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_20")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        ////创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        ////是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PL_PrdOrderReqMaterialsEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = _PrdOrderReqMaterialsBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _PrdOrderReqMaterialsBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_PrdOrderReqMaterials")]
        public HttpResponseMessage DeletePL_PrdOrderReqMaterials(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PL_PrdOrderReqMaterialsEntity entity = JsonConvert.DeserializeObject<PL_PrdOrderReqMaterialsEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_PrdOrderReqMaterialsEntity model = _PrdOrderReqMaterialsBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _PrdOrderReqMaterialsBLL.DeleteEntity(Id, out msg,null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_23");//删除操作成功
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_PrdOrderReqMaterials")]
        public HttpResponseMessage RemovePL_PrdOrderReqMaterials(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PL_PrdOrderReqMaterialsEntity entity = JsonConvert.DeserializeObject<PL_PrdOrderReqMaterialsEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_PrdOrderReqMaterialsEntity model = _PrdOrderReqMaterialsBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _PrdOrderReqMaterialsBLL.RemoveForm(Id, null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_23");//删除操作成功
                else
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_25");//删除操作失败
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_PrdOrderReqMaterialsEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _PrdOrderReqMaterialsBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_PrdOrderReqMaterialsEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _PrdOrderReqMaterialsBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
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
                var list = _PrdOrderReqMaterialsBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
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
                var list = _PrdOrderReqMaterialsBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_PrdOrderReqMaterials_export")]
        public HttpResponseMessage PL_PrdOrderReqMaterials_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("PlanManage.PL_PrdOrderReqMaterialsController.Tips_8");//缺少Entity参数！
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
                var data = _PrdOrderReqMaterialsBLL.GetList_export(CreatedByCode, out msg);
                
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
        
        
    }
}
