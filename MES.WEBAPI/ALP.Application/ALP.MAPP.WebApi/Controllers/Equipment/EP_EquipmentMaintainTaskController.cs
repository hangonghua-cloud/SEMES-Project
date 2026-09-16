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
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-9-25
    /// 2.创建作者: 刘万军
    /// 3.功能描述: EP_EquipmentMaintainTaskController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 设备保养项目详情
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [RoutePrefix("EP_EquipmentMaintainTask")]
    public class EP_EquipmentMaintainTaskController : ApiBaseController
    {
        private const string SuccessMsg = "Common.ExecutionSuccess";
        private const string FaildMsg = "Common.ExecutionError";


        /// <summary>
        /// 功能描述: 根据时间范围和设备类别 获取未保养的任务
        /// 创　　建: 刘万军
        /// 创建日期: 2021-9-25 10:09:17
        /// 任务编号: 设备保养任务
        /// </summary>
        /// <param name="jo"></param>
        /// <returns>返回列表</returns>
        [HttpPost]
        [Route("GetEquipmentMaintainTaskByDateAndType")]
        public HttpResponseMessage GetEquipmentMaintainTaskByDateAndType(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var queryJson = getValue(jo, "queryJson");
                if (string.IsNullOrEmpty(queryJson)) return AjaxResult(false, "Equipment.EP_EquipmentMaintainTaskController.GetEquipmentMaintainTaskByDateAndType.Tips_1");//请选择查询条件

                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                var data = _Service.GetEquipmentMaintainTaskByDateAndType(queryJson);

                if (data.Count < 1) return AjaxResult(false, "Equipment.EP_EquipmentMaintainTaskController.GetEquipmentMaintainTaskByDateAndType.Tips_2");//没有查询到保养任务记录

                result.resultData = data;
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", msg + ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（修改）, 保养项目数组\ 备件更换数组
        /// 创　　建: 刘万军
        /// 创建日期: 2021-9-27 08:16:09
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
                result.returnMsg = "Common.ParamsNotNull";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            /*
             * Id: this.form2.Id,//保养任务执行ID
                    EquipmentId: this.form2.EquipmentId,//保养设备编码
                    EquipmentName: this.form2.EquipmentName,//保养设备名称
                    RepairingType: this.form2.RepairingType,//保养设备类别
                    EquipmentMaintainTaskName: this.form2.EquipmentMaintainTaskName,//保养任务名称
                    EquipmentMaintainTaskId: this.form2.EquipmentMaintainTaskId,//保养任务ID
                    RepairingPerson: this.form2.RepairingPerson,//保养人编码
                    RepairingPersonName: this.form2.RepairingPersonName,//保养人姓名
                    EP_EquipmentMaintainDetailList:this.EP_EquipmentMaintainDetailList,//保养项目
                    SparePartsItemDetailList: this.SparePartsItemDetailList,//备件更换
                    ModifyBy:this.loginInfo.result.UserCode//当前登录人编码
             */
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("Id") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_1", "Id");//缺少Id参数
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养设备编码
            if (jo.SelectToken("EquipmentId") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_2";//缺少保养设备编码
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养设备名称
            if (jo.SelectToken("EquipmentName") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_3";//缺少保养设备名称
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养人编码
            if (jo.SelectToken("RepairingPerson") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_4";//缺少保养人编码
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养人姓名
            if (jo.SelectToken("RepairingPersonName") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_5";//缺少保养人姓名
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养任务ID
            if (jo.SelectToken("EquipmentMaintainTaskId") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_6";//缺少保养任务ID
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养任务名称
            if (jo.SelectToken("EquipmentMaintainTaskName") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_7";//缺少保养任务名称
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //保养项目
            if (jo.SelectToken("EP_EquipmentMaintainDetailList") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_8"; //缺少保养项目
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //备件更换
            if (jo.SelectToken("SparePartsItemDetailList") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_9"; //缺少备件更换
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //当前登录人编码
            if (jo.SelectToken("ModifyBy") == null)
            {
                result.success = false;
                result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_10";//缺少当前登录人编码
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //业务服务类
                EP_EquipmentMaintainTask_Service _Service = new EP_EquipmentMaintainTask_Service();
                //保养项目业务服务类
                EP_EquipmentMaintainResult_Service _resultService = new EP_EquipmentMaintainResult_Service();
                //备件业务服务类
                EP_EquipmentSpareParts_Service _EP_EquipmentSpareParts_Service = new EP_EquipmentSpareParts_Service();
                BS_People_Service _bsPeopleService = new BS_People_Service();
                //参数转实体
                EP_EquipmentMaintainTaskEntity entity = _Service.GetEntity(getValue(jo, "Id"));
                //JsonConvert.DeserializeObject<EP_EquipmentMaintainTaskEntity>(getValue(jo, "Entity"));
                //保养工单号 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.EquipmentId))
                {
                    //设备编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_11";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.EquipmentMaintainTaskId))
                {
                    //保养任务编号 是否为空进行判断
                    result.success = false;
                    result.returnMsg = "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_12";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "Id");
                //string queryJson = getValue(jo, "Entity");
                //保养设备编码
                entity.EquipmentId = getValue(jo, "EquipmentId");
                //保养设备名称
                entity.EquipmentName = getValue(jo, "EquipmentName");
                //保养任务名称
                entity.EquipmentMaintainTaskName = getValue(jo, "EquipmentMaintainTaskName");
                //保养任务ID
                entity.EquipmentMaintainTaskId = getValue(jo, "EquipmentMaintainTaskId");
                //保养人编码
                entity.MaintainPerson = getValue(jo, "RepairingPerson");
                //保养人编码
                entity.MaintainPersonName = getValue(jo, "RepairingPersonName");
                //实际保养时间
                entity.ActiveDate = DateTime.Now;
                //保养状态 1 已保养
                entity.MaintenanceStatus = "1";
                //保养状态 1 已保养
                entity.MaintenanceStatusName = ALP.Application.Service.Resources.Language.GetText("Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Data_1");//"已保养";
                //修改人
                entity.ModifyBy = getValue(jo, "ModifyBy");
                //最后修改时间
                entity.ModifyTime = DateTime.Now;

                string msg = "";

                //var peopleEntity = _bsPeopleService.Get_ExpressionEntity(t => t.Code == CurrentAccount.UserCode);
                //entity.FactoryCode = peopleEntity?.FactoryCode;
                //entity.FactoryName = peopleEntity?.FactoryName;

                //保养项目 数组
                List<EP_EquipmentMaintainResultEntity> list = JsonConvert.DeserializeObject<List<EP_EquipmentMaintainResultEntity>>(getValue(jo, "EP_EquipmentMaintainDetailList"));
                foreach (var item in list)
                {
                    item.FactoryCode = entity?.FactoryCode;
                    item.FactoryName = entity?.FactoryName;
                }

                //保养任务更新, 创建保养项目, 再创建下一周期的保养项目
                int isok = _resultService.SaveEntity(entity, list, ref msg);
                //保养项目 数组
                List<EP_EquipmentSparePartsEntity> SparePartsItemDetailList = JsonConvert.DeserializeObject<List<EP_EquipmentSparePartsEntity>>(getValue(jo, "SparePartsItemDetailList"));
                foreach (var item in SparePartsItemDetailList)
                {
                    item.FactoryCode = entity?.FactoryCode;
                    item.FactoryName = entity?.FactoryName;
                }

                // 批量新增
                int batchInsert = _EP_EquipmentSpareParts_Service.SaveEntity_List(false, "", SparePartsItemDetailList, out msg);

                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? "Equipment.EP_EquipmentMaintainTaskController.SaveEP_EquipmentMaintainTask.Tips_13" : msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


        /// <summary>
        /// 功能描述: 根据保养任务编码查询保养项目
        /// 创　　建: 刘万军
        /// 创建日期: 2021-9-25 15:41:31
        /// 任务编号: 设备保养项目详情
        /// </summary>
        /// <param name="jo"></param>
        /// <returns>返回列表</returns>
        [HttpPost]
        [Route("GetEP_EquipmentMaintainDetailList")]
        public HttpResponseMessage GetEP_EquipmentMaintainDetailList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var queryJson = getValue(jo, "queryJson");
                if (string.IsNullOrEmpty(queryJson)) return AjaxResult(false, "Equipment.EP_EquipmentMaintainTaskController.GetEP_EquipmentMaintainDetailList.Tips_1");//请选择查询条件

                EP_EquipmentMaintainDetail_Service _Service = new EP_EquipmentMaintainDetail_Service();
                var data = _Service.GetEP_EquipmentMaintainDetailList(queryJson);

                if (data.Count < 1) return AjaxResult(false, "Equipment.EP_EquipmentMaintainTaskController.GetEP_EquipmentMaintainDetailList.Tips_2");//没有查询到保养任务项目记录

                result.resultData = data;
                result.success = true;
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", msg + ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

    }
}
