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
    /// 3.功能描述: EP_EquipmentCheckItemMaintenanceController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 设备点检维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("EP_EquipmentCheckItemMaintenance")]
    public class EP_EquipmentCheckItemMaintenanceController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentCheckItemMaintenancePageList")]
        public HttpResponseMessage EP_EquipmentCheckItemMaintenancePageList(JObject jo)
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
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentCheckItemMaintenancePageDataTableList")]
        public HttpResponseMessage EP_EquipmentCheckItemMaintenancePageDataTableList(JObject jo)
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
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEP_EquipmentCheckItemMaintenanceList")]
        public HttpResponseMessage GetEP_EquipmentCheckItemMaintenanceList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 获取关联的设备类型
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTypePageList")]
        public HttpResponseMessage GetTypePageList(string id)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                /*Pagination pagination = new Pagination();
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
                var watch = CommonHelper.TimerStart();*/
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                var data = _Service.GetTypePageList(id);
                /*var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                    costtime = CommonHelper.TimerEnd(watch)
                };*/

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
        /// 添加关联设备类型
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveChildForm")]
        public HttpResponseMessage SaveChildForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("EquipmentMaintenanceId") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_6");//缺少BadProductId参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("SaveRows") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_7");//缺少SaveRows参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "EquipmentMaintenanceId");

                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                List<EP_EquipmentCheckItemMaintenanceTypeEntity> list = JsonConvert.DeserializeObject<List<EP_EquipmentCheckItemMaintenanceTypeEntity>>(getValue(jo, "SaveRows"));
                int returnValue = _Service.SaveChildForm(keyValue, list);
                switch (returnValue)
                {
                    case 1:
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_8");//添加成功
                        result.success = true;
                        break;
                    case 2:
                        result.returnMsg = "";
                        result.success = false;
                        break;
                    case 0:
                        result.returnMsg = "";
                        result.success = false;
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEP_EquipmentCheckItemMaintenance")]
        public HttpResponseMessage SaveEP_EquipmentCheckItemMaintenance(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_10");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_11");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_12");//缺少Entity参数！
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
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                //参数转实体
                EP_EquipmentCheckItemMaintenanceEntity entity = JsonConvert.DeserializeObject<EP_EquipmentCheckItemMaintenanceEntity>(getValue(jo, "Entity"));
                ////工厂 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                //if (string.IsNullOrEmpty(entity.FactoryCode))
                //{
                //    result.success = false;
                //    result.returnMsg = "工厂不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}
                /*else if (string.IsNullOrEmpty(entity.EquipmentType))
                {
                    //设备类别 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_13");//设备类别不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }*/
                if (string.IsNullOrEmpty(entity.CheckTaskId))
                {
                    //点检任务编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_14");//点检任务编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.CheckTaskName))
                {
                    //点检任务名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_15");//点检任务名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                /*else if (string.IsNullOrEmpty(entity.Creator))
                {
                    //创建人 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_16");//创建人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }*/
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_17");//编辑人编号不能为空！
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
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_18");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: EP_EquipmentCheckItemMaintenance	Factory流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("EP_EquipmentCheckItemMaintenance", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.Factory = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchEP_EquipmentCheckItemMaintenance")]
        public HttpResponseMessage SaveBatchEP_EquipmentCheckItemMaintenance(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_10");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_20");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_21");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_12");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<EP_EquipmentCheckItemMaintenanceEntity> Insert_entity_list = new List<EP_EquipmentCheckItemMaintenanceEntity>();
                //更新数组
                List<EP_EquipmentCheckItemMaintenanceEntity> Update_entity_list = new List<EP_EquipmentCheckItemMaintenanceEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        EP_EquipmentCheckItemMaintenanceEntity entity = new EP_EquipmentCheckItemMaintenanceEntity();
                        //工厂
                        entity.Factory =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_22")];//工厂
                        //设备类别
                        entity.EquipmentType =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_23")];//设备类别
                        //点检任务编码
                        entity.CheckTaskId =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_24")];//点检任务编码
                        //点检任务名称
                        entity.CheckTaskName =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_25")];//点检任务名称
                        //备注
                        entity.Remark =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_26")];//备注
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_27")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_28")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_29")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_30")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreateTime = DateTimeOffset.Now;
                        //是否删除
                        //entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        EP_EquipmentCheckItemMaintenanceEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteEP_EquipmentCheckItemMaintenance")]
        public HttpResponseMessage DeleteEP_EquipmentCheckItemMaintenance(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_10");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_12");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                EP_EquipmentCheckItemMaintenanceEntity entity = JsonConvert.DeserializeObject<EP_EquipmentCheckItemMaintenanceEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentCheckItemMaintenanceEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_33");//删除操作成功
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveEP_EquipmentCheckItemMaintenance")]
        public HttpResponseMessage RemoveEP_EquipmentCheckItemMaintenance(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_10");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_12");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                EP_EquipmentCheckItemMaintenanceEntity entity = JsonConvert.DeserializeObject<EP_EquipmentCheckItemMaintenanceEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentCheckItemMaintenanceEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_33");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_35");//删除操作失败
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentCheckItemMaintenanceEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_36");//获取详情数据成功
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentCheckItemMaintenanceEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_36");//获取详情数据成功
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
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
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        /*[HttpPost]
        [Route("EP_EquipmentCheckItemMaintenance_export")]
        public HttpResponseMessage EP_EquipmentCheckItemMaintenance_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EP_EquipmentCheckItemMaintenanceController.Tips_12");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                EP_EquipmentCheckItemMaintenance_Service _Service = new EP_EquipmentCheckItemMaintenance_Service();
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
