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
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_OwnProductBGController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 自制半成品报工
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_OwnProductBG")]
    public class PM_OwnProductBGController : ApiBaseController
    {
        private PM_OwnProductBGBLL _OwnProductBGBLL = new PM_OwnProductBGBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_OwnProductBGPageList")]
        public HttpResponseMessage PM_OwnProductBGPageList(JObject jo)
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
                
                var data = _OwnProductBGBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_OwnProductBGPageDataTableList")]
        public HttpResponseMessage PM_OwnProductBGPageDataTableList(JObject jo)
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
                
                var data = _OwnProductBGBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_OwnProductBGList")]
        public HttpResponseMessage GetPM_OwnProductBGList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                
                string msg = "";
                var list = _OwnProductBGBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_OwnProductBG")]
        public HttpResponseMessage SavePM_OwnProductBG(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_7");//缺少Entity参数！
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
                
                //参数转实体
                PM_OwnProductBGEntity entity = JsonConvert.DeserializeObject<PM_OwnProductBGEntity>(getValue(jo, "Entity"));
                
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
              
                
                string msg = "";
                int isok = _OwnProductBGBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_OwnProductBG")]
        public HttpResponseMessage SaveBatchPM_OwnProductBG(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_10");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_11");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _OwnProductBGBLL.GetList("", out msg);
                //插入数组
                List<PM_OwnProductBGEntity> Insert_entity_list = new List<PM_OwnProductBGEntity>();
                //更新数组
                List<PM_OwnProductBGEntity> Update_entity_list = new List<PM_OwnProductBGEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_OwnProductBGEntity entity = new PM_OwnProductBGEntity();
                        //报工ID
                        entity.OwnProductId =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_12")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_12")];//报工ID
                        //工单号
                        entity.WorkOrder =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_13")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_13")];//工单号
                        //物料类别
                        entity.MaterialClass =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_14")];//物料类别
                        //报工数量
                        entity.MaterialCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_15")];//报工数量
                        //规格型号
                        entity.Spec =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_16")];//规格型号
                        //物料名称
                        entity.MaterialName =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_17")];//物料名称
                        //流转卡编码
                        entity.TransferCode =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_18")];//流转卡编码
                        //流转卡名称
                        entity.TransferName =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_19")];//流转卡名称
                        //报工工序
                        entity.BGProcess =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_20")];//报工工序
                        //生产机台
                        entity.BGMachine =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_21")];//生产机台
                        //报工数量
                        entity.BGQty =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_22")];//报工数量
                        //报工班次
                        entity.BGShift =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_23")];//报工班次
                        //报工小组
                        entity.UserGroup =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_24")];//报工小组
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_25")];//创建人
                        //报工日期
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_26")];//报工日期
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_27")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_28")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                   
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_OwnProductBGEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = _OwnProductBGBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _OwnProductBGBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_OwnProductBG")]
        public HttpResponseMessage DeletePM_OwnProductBG(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_OwnProductBGEntity entity = JsonConvert.DeserializeObject<PM_OwnProductBGEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_OwnProductBGEntity model = _OwnProductBGBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _OwnProductBGBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_31");//删除操作成功
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_OwnProductBG")]
        public HttpResponseMessage RemovePM_OwnProductBG(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_OwnProductBGEntity entity = JsonConvert.DeserializeObject<PM_OwnProductBGEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_OwnProductBGEntity model = _OwnProductBGBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _OwnProductBGBLL.RemoveForm(Id, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_31");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_33");//删除操作失败
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_OwnProductBGEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _OwnProductBGBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_34");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_OwnProductBGEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _OwnProductBGBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_34");//获取详情数据成功
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
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
                
                var data = _OwnProductBGBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 ).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
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
                var list = _OwnProductBGBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
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
                var list = _OwnProductBGBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_OwnProductBG_export")]
        public HttpResponseMessage PM_OwnProductBG_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_OwnProductBGController.Tips_7");//缺少Entity参数！
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
                var data = _OwnProductBGBLL.GetList_export(CreatedByCode, out msg);
                
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
