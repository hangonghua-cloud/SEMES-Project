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

namespace ALP.Application.WebApi.Controllers.MaterialManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-19
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductReworkController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 成品返工单明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_ProductRework")]
    public class MM_ProductReworkController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductReworkPageList")]
        public HttpResponseMessage MM_ProductReworkPageList(JObject jo)
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
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                var data = _Service.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total: data.Count(),
                    page = pagination != null ? pagination.total: data.Count(),
                    records = pagination != null ? pagination.records: data.Count(),
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_ProductReworkPageDataTableList")]
        public HttpResponseMessage MM_ProductReworkPageDataTableList(JObject jo)
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
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_ProductReworkList")]
        public HttpResponseMessage GetMM_ProductReworkList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_ProductRework")]
        public HttpResponseMessage SaveMM_ProductRework(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_7");//缺少Entity参数！
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
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                //参数转实体
                MM_ProductReworkEntity entity = JsonConvert.DeserializeObject<MM_ProductReworkEntity>(getValue(jo, "Entity"));
                //成品返工单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.ReWorkOrder))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_8");//成品返工单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ProductOrder))
                {
                    //订单号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_9");//订单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ContainerNO))
                {
                    //柜号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_10");//柜号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CustomerPO))
                {
                    //客户PO号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_11");//客户PO号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialCode))
                {
                    //客户型号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_12");//客户型号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                else if (string.IsNullOrEmpty(entity.DutyProcess))
                {
                    //返工责任工序 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_13");//返工责任工序不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ReWorkStatus))
                {
                    //成品返工单状态 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_14");//成品返工单状态不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Creator))
                {
                    //创建人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_15");//创建人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ModifyBy))
                {
                    //最后修改人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_16");//最后修改人不能为空！
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_ProductRework")]
        public HttpResponseMessage SaveBatchMM_ProductRework(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_19");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_20");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<MM_ProductReworkEntity> Insert_entity_list = new List<MM_ProductReworkEntity>();
                //更新数组
                List<MM_ProductReworkEntity> Update_entity_list = new List<MM_ProductReworkEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        MM_ProductReworkEntity entity = new MM_ProductReworkEntity();
                        //成品返工单号
                        entity.ReWorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_21")];//成品返工单号
                        //订单号
                        entity.ProductOrder =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_22")];//订单号
                        //柜号
                        entity.ContainerNO =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_23")];//柜号
                        //客户PO号
                        entity.CustomerPO =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_24")];//客户PO号
                        //客户型号
                        entity.MaterialCode =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_25")];//客户型号
                        
                        //返工责任工序
                        entity.DutyProcess =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_26")];//返工责任工序
                        //成品返工单状态
                        entity.ReWorkStatus =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_27")];//成品返工单状态
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_28")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_29")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_30")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_31")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        MM_ProductReworkEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_ProductRework")]
        public HttpResponseMessage DeleteMM_ProductRework(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                MM_ProductReworkEntity entity = JsonConvert.DeserializeObject<MM_ProductReworkEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductReworkEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_34");//删除操作成功
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_ProductRework")]
        public HttpResponseMessage RemoveMM_ProductRework(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                MM_ProductReworkEntity entity = JsonConvert.DeserializeObject<MM_ProductReworkEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_ProductReworkEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_34");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_36");//删除操作失败
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductReworkEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_37");//获取详情数据成功
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_ProductReworkEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_37");//获取详情数据成功
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
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
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
        /// 创建日期: 2021-10-19 21:03:26
        /// 任务编号: 成品返工单明细
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_ProductRework_export")]
        public HttpResponseMessage MM_ProductRework_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("MaterialManage.MM_ProductReworkController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                MM_ProductRework_Service _Service = new MM_ProductRework_Service();
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
