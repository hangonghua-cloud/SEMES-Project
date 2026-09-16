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
    /// 3.功能描述: EP_EquipmentMaintainResultController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 设备保养结果
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("EP_EquipmentMaintainResult")]
    public class EP_EquipmentMaintainResultController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentMaintainResultPageList")]
        public HttpResponseMessage EP_EquipmentMaintainResultPageList(JObject jo)
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
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentMaintainResultPageDataTableList")]
        public HttpResponseMessage EP_EquipmentMaintainResultPageDataTableList(JObject jo)
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
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEP_EquipmentMaintainResultList")]
        public HttpResponseMessage GetEP_EquipmentMaintainResultList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEP_EquipmentMaintainResult")]
        public HttpResponseMessage SaveEP_EquipmentMaintainResult(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
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
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                //参数转实体
                EP_EquipmentMaintainResultEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainResultEntity>(getValue(jo, "Entity"));
                //保养工单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.EquipmentMaintainIdentifyCode))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_8");//保养工单号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentMaintainId))
                {
                    //保养项目编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_9");//保养项目编号不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentMaintainName))
                {
                    //保养项目名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_10");//保养项目名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentMaintainResult))
                {
                    //保养结果 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_11");//保养结果不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Creator))
                {
                    //创建人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_12");//创建人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ModifyBy))
                {
                    //最后修改人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_13");//最后修改人不能为空！
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
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_14");//编辑人编号不能为空！
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
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_15");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: EP_EquipmentMaintainResult	EquipmentMaintainIdentifyCode流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("EP_EquipmentMaintainResult", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.EquipmentMaintainIdentifyCode = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.CreatedDateTime = DateTimeOffset.Now;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.IsDeleted = false;
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEP_EquipmentMaintainRecords")]
        public HttpResponseMessage SaveEP_EquipmentMaintainRecords(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("List") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_18");//缺少List参数！
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
                EP_EquipmentMaintainResult_Service _resultService = new EP_EquipmentMaintainResult_Service();
                //参数转实体
                EP_EquipmentMaintainTaskEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainTaskEntity>(getValue(jo, "Entity"));
                List<EP_EquipmentMaintainResultEntity> list = JsonConvert.DeserializeObject<List<EP_EquipmentMaintainResultEntity>>(getValue(jo, "List"));
                //设备编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                #region
                /*if (string.IsNullOrEmpty(entity.EquipmentId))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_19");//设备编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentName))
                {
                    //设备名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_20");//设备名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CheckTaskId))
                {
                    //点检任务编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_21");//点检任务编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CheckTaskName))
                {
                    //点检任务名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_22");//点检任务名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CheckConclusion))
                {
                    //点检结论 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_23");//点检结论不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TypeInPerson))
                {
                    //录入人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_24");//录入人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }*/
                #endregion

                //string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(entity.Id))
                {

                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_14");//编辑人编号不能为空！
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
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: EP_EquipmentCheckRecord	EquipmentId流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("EP_EquipmentCheckRecord", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.EquipmentId = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;

                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.CreatedDateTime = DateTimeOffset.Now;
                    //录入时间
                    //entity.TypeInTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.IsDeleted = false;
                }

                string msg = "";
                string id = "";
                int isok = _Service.SaveEntity(entity.Id, entity, out msg);
                /*if (string.IsNullOrWhiteSpace(entity.Id))
                    entity.Id = id;*/
                isok = _resultService.SaveEntity(entity, list, ref msg);
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchEP_EquipmentMaintainResult")]
        public HttpResponseMessage SaveBatchEP_EquipmentMaintainResult(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_25");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_26");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<EP_EquipmentMaintainResultEntity> Insert_entity_list = new List<EP_EquipmentMaintainResultEntity>();
                //更新数组
                List<EP_EquipmentMaintainResultEntity> Update_entity_list = new List<EP_EquipmentMaintainResultEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        EP_EquipmentMaintainResultEntity entity = new EP_EquipmentMaintainResultEntity();
                        //保养工单号
                        entity.EquipmentMaintainIdentifyCode =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_27")];//保养工单号
                        //保养项目编号
                        entity.EquipmentMaintainId =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_28")];//保养项目编号
                        //保养项目名称
                        entity.EquipmentMaintainName =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_29")];//保养项目名称
                        //保养结果
                        entity.EquipmentMaintainResult =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_30")];//保养结果
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_31")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_32")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_32")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_33")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_33")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_34")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_34")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        //是否删除
                        //entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        EP_EquipmentMaintainResultEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteEP_EquipmentMaintainResult")]
        public HttpResponseMessage DeleteEP_EquipmentMaintainResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                EP_EquipmentMaintainResultEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainResultEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentMaintainResultEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_37");//删除操作成功
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveEP_EquipmentMaintainResult")]
        public HttpResponseMessage RemoveEP_EquipmentMaintainResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                EP_EquipmentMaintainResultEntity entity = JsonConvert.DeserializeObject<EP_EquipmentMaintainResultEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentMaintainResultEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_37");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_39");//删除操作失败
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

        [HttpPost]
        [Route("RemoveBatchEP_EquipmentMaintainResult")]
        public HttpResponseMessage RemoveBatchEP_EquipmentMaintainResult(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_6");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (jo.SelectToken("UpdateUser") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_41");//缺少UpdateUser参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                string keyValue = getValue(jo, "KeyValue");
                string modifyBy = getValue(jo, "UpdateUser");
                //删除
                int isok = _Service.RemoveBatchForm(keyValue, modifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_37");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_39");//删除操作失败
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentMaintainResultEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_42");//获取详情数据成功
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentMaintainResultEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_42");//获取详情数据成功
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
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
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
        /// 创建日期: 2021-08-05 14:49:09
        /// 任务编号: 设备保养结果
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        /*[HttpPost]
        [Route("EP_EquipmentMaintainResult_export")]
        public HttpResponseMessage EP_EquipmentMaintainResult_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentMaintainResultController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                EP_EquipmentMaintainResult_Service _Service = new EP_EquipmentMaintainResult_Service();
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
