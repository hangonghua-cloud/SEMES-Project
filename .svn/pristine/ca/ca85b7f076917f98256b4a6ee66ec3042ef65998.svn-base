using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.EquipmentManage;
using ALP.Application.Service.EquipmentManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;

namespace ALP.Application.WebApi.Controllers.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainTaskController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 设备保养项目详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("EP_EquipmentMaintainTask")]
    public class EP_EquipmentMaintainTaskController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentMaintainTaskPageList")]
        public HttpResponseMessage EP_EquipmentMaintainTaskPageList(JObject jo)
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
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentMaintainTaskPageDataTableList")]
        public HttpResponseMessage EP_EquipmentMaintainTaskPageDataTableList(JObject jo)
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
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEP_EquipmentMaintainTaskList")]
        public HttpResponseMessage GetEP_EquipmentMaintainTaskList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        /// 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaintenceResultPageList")]
        public HttpResponseMessage GetMaintenceResultPageList(JObject jo)//Pagination pagination, string queryJson
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
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.GetMaintenceResult(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetMaintenceResult")]
        public HttpResponseMessage GetMaintenceResult(JObject jo)//Pagination pagination, string queryJson
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Pagination pagination = null;// new Pagination();
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.GetMaintenceResult(pagination, queryJson);
                

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEP_EquipmentMaintainTask")]
        public HttpResponseMessage SaveEP_EquipmentMaintainTask(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_7");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_8");//缺少Entity参数！
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
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                //参数转实体
                EP_EquipmentMaintainTaskEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainTaskEntity>(getValue(jo, "Entity"));
                //保养工单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.EquipmentId))
                {
                    //设备编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_9");//设备编号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentMaintainTaskId))
                {
                    //保养任务编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_10");//保养任务编号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_11");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    //entity.UpdateDateTime = DateTimeOffset.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    //entity.CreatedDateTime = DateTime.Parse(entity.CreatedDateTime.ToString());
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_12");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: EP_EquipmentMaintainTask	EquipmentIdentifyCode流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("EP_EquipmentMaintainTask", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.EquipmentIdentifyCode = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.CreatedDateTime = DateTimeOffset.Now;
                    //计划保养日期
                    //entity.PlanDate = DateTime.Now;
                    //实际保养日期
                    //entity.ActiveDate = DateTime.Now;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.IsDeleted = false;
                }
                
                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg);
                /*result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;*///操作成功
                switch (isok)
                {
                    case 1:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_14");//添加成功
                        break;
                    case 2:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_15");//修改成功
                        break;
                    case 3:
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_16");//添加失败，该保养设备已经添加了选择的保养任务，不能重复添加，请重新选择
                        break;
                }
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchEP_EquipmentMaintainTask")]
        public HttpResponseMessage SaveBatchEP_EquipmentMaintainTask(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_18");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_19");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_8");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<EP_EquipmentMaintainTaskEntity> Insert_entity_list = new List<EP_EquipmentMaintainTaskEntity>();
                //更新数组
                List<EP_EquipmentMaintainTaskEntity> Update_entity_list = new List<EP_EquipmentMaintainTaskEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        EP_EquipmentMaintainTaskEntity entity = new EP_EquipmentMaintainTaskEntity();
                        //保养工单号
                        entity.EquipmentIdentifyCode =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_20")];//保养工单号
                        //工单状态
                        entity.EquipmentIdentifyStatus =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_21")];//工单状态
                        //设备编号
                        entity.EquipmentId =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_22")];//设备编号
                        //保养任务编号
                        entity.EquipmentMaintainTaskId =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_23")];//保养任务编号
                        //计划保养日期
                        entity.PlanDate = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_24")];//计划保养日期
                        //保养人员
                        entity.MaintainPerson =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_25")];//保养人员
                        //实际保养日期
                        entity.ActiveDate = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_26")];//实际保养日期
                        //保养详情
                        entity.Remark =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_27")];//保养详情
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_28")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_29")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_30")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_31")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        //是否删除
                        //entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        EP_EquipmentMaintainTaskEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteEP_EquipmentMaintainTask")]
        public HttpResponseMessage DeleteEP_EquipmentMaintainTask(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                EP_EquipmentMaintainTaskEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainTaskEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentMaintainTaskEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_35");//删除操作成功
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveEP_EquipmentMaintainTask")]
        public HttpResponseMessage RemoveEP_EquipmentMaintainTask(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                EP_EquipmentMaintainTaskEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainTaskEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentMaintainTaskEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_35");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_37");//删除操作失败
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentMaintainTaskEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_38");//获取详情数据成功
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentMaintainTaskEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_38");//获取详情数据成功
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="keyValue">条件值</param>
        /// <param name="keyValue2">条件值</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEntityByLinq")]
        public HttpResponseMessage GetEntityByLinq(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:44
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        /*[HttpPost]
        [Route("EP_EquipmentMaintainTask_export")]
        public HttpResponseMessage EP_EquipmentMaintainTask_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainTaskController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
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
        }*/
        
        
    }
}
