using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.EquipmentManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.Service.EquipmentManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.Equipment
{

    /// <summary>
    /// PDA设备模块
    /// </summary>
    [RoutePrefix("Equipment")]
    public class EquipmentController : ApiBaseController
    {
        #region 实例化
        //基础信息
        private BS_People_Service _bsPeopleService = new BS_People_Service();//人员管理

        private EP_EquipmentManage_Service _EquipmentManageService = new EP_EquipmentManage_Service();
        private V_EP_EquipmentManage_Service V_EquipmentManageService = new V_EP_EquipmentManage_Service();
        private EP_EquipmentCheckItemDetail_Service _EquipmentCheckItemDetailService = new EP_EquipmentCheckItemDetail_Service();
        private EP_EquipmentCheckItemMaintenance_Service _EquipmentCheckItemMaintenanceService = new EP_EquipmentCheckItemMaintenance_Service();
        private EP_EquipmentCheckItemMaintenanceType_Service _EquipmentCheckItemMaintenanceTypeService = new EP_EquipmentCheckItemMaintenanceType_Service();

        private EP_EquipmentCheckRecord_Service _EquipmentCheckRecordService = new EP_EquipmentCheckRecord_Service();
        private EP_EquipmentCheckResult_Service _EquipmentCheckResultService = new EP_EquipmentCheckResult_Service();

        private EP_EquipmentMalfunctionRepair_Service _EquipmentMalfunctionRepairService = new EP_EquipmentMalfunctionRepair_Service();
        private EP_EquipmentSpareParts_Service _EquipmentSparePartsService = new EP_EquipmentSpareParts_Service();

        private EP_EquipmentMaintainTask_Service _EquipmentMaintainTaskService = new EP_EquipmentMaintainTask_Service();
        private EP_EquipmentMaintain_Service _EquipmentMaintainService = new EP_EquipmentMaintain_Service();
        Base_Images_Service _ImagesService = new Base_Images_Service();//图片上传

        private const string SuccessMsg = "Common.ExecutionSuccess";
        private const string FaildMsg = "Common.ExecutionError";
        #endregion

        #region 设备点检

        #region 扫描设备编码 查找设备点检任务
        /// <summary>
        /// 扫描设备编码 查找设备点检任务
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEquipmentCheckByCode")]
        public HttpResponseMessage GetEquipmentCheckByCode(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var equipmentId = getValue(jo, "EquipmentId");
                if (string.IsNullOrEmpty(equipmentId)) return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentCheckByCode.Tips_1");//请扫描设备
                var list = V_EquipmentManageService.GetList(equipmentId);


                var query = from equip in list
                            join type in _EquipmentCheckItemMaintenanceTypeService.Get_ExpressionList(t => true) on equip.EquipmentType equals type.EquipmentType
                            join main in _EquipmentCheckItemMaintenanceService.Get_ExpressionList(m => m.EnabledMark == true) on type.EquipmentMaintenanceId equals main.Id
                            join detail in _EquipmentCheckItemDetailService.Get_ExpressionList(d => d.EnabledMark == true) on main.CheckTaskId equals detail.CheckTaskId
                            select new
                            {
                                equip.EquipmentId,
                                equip.EquipmentName,
                                type.EquipmentType,
                                main.CheckTaskId,
                                main.CheckTaskName,
                                detail.CheckItemId,
                                detail.CheckItemName,
                                detail.CheckItemStandard,
                                detail.DataType,
                                detail.DataTypeName,
                                CheckResult = "",
                            };

                var data = query.ToList();
                if (data.Count < 1) return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentCheckByCode.Tips_2");//没有查询到点检任务

                dynamic obj = new ExpandoObject();

                var taskList = new List<dynamic>();
                foreach (var item in data.GroupBy(t => new { t.CheckTaskId, t.CheckTaskName }).ToList())
                {
                    var itemList = new List<dynamic>();
                    foreach (var detail in item)
                    {
                        if (int.Parse(detail.DataType) > 3 && detail.DataTypeName.IndexOf("/") > 0)
                        {
                            var optionList = new List<dynamic>();
                            var array = detail.DataTypeName.Split("/");
                            for (var i = 0; i < array.Length; i++)
                            {
                                optionList.Add(new
                                {
                                    value = i,
                                    name = array[i]
                                });
                            }
                            itemList.Add(new
                            {
                                detail.CheckItemId,
                                detail.CheckItemName,
                                detail.CheckItemStandard,
                                detail.CheckResult,
                                detail.DataType,
                                detail.DataTypeName,
                                Options = optionList
                            });
                        }
                        else
                        {
                            itemList.Add(new
                            {
                                detail.CheckItemId,
                                detail.CheckItemName,
                                detail.CheckItemStandard,
                                detail.CheckResult,
                                detail.DataType,
                                detail.DataTypeName,
                            });
                        }
                    }
                    taskList.Add(new
                    {
                        value = item.Key.CheckTaskId,
                        label = item.Key.CheckTaskName,
                        GridList = itemList
                    });
                }

                obj.EquipmentId = data[0].EquipmentId;
                obj.EquipmentName = data[0].EquipmentName;
                obj.TaskList = taskList;


                result.resultData = obj;
                result.success = true;
                result.returnMsg = SuccessMsg;
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

        #endregion

        #region 设备点检任务保存
        /// <summary>
        /// 设备点检任务保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEquipmentTackResult")]
        public HttpResponseMessage SaveEquipmentTackResult(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            var msg = "";
            var id = "";
            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var entity = JsonConvert.DeserializeObject<EP_EquipmentCheckRecordEntity>(getValue(jo, "entity"));
                var list = JsonConvert.DeserializeObject<List<EP_EquipmentCheckResultEntity>>(getValue(jo, "data"));
                //var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(jo["fileUrl"].ToString());

                //var peopleEntity = _bsPeopleService.Get_ExpressionEntity(t => t.Code == CurrentAccount.UserCode);
                //if (peopleEntity == null || string.IsNullOrEmpty(peopleEntity.FactoryCode))
                //    return AjaxResult(false, "人员[" + CurrentAccount.UserCode + "]不存在或人员没有配置工厂");
                var equipEntity = V_EquipmentManageService.GetEntity(t => t.EquipmentId == entity.EquipmentId);
                if (equipEntity == null)
                    return AjaxResultWithParams(false, "Equipment.EquipmentController.SaveEquipmentTackResult.Tips_1", entity.EquipmentId);//设备[" + entity.EquipmentId + "]不存在！

                entity.FactoryCode = equipEntity.TestMethodCoadin;
                entity.FactoryName = equipEntity.TestMethodCoadinName;
                entity.TypeInTime = time;

                _EquipmentCheckRecordService.SaveEntity(null, entity, out id, out msg);
                foreach (var item in list)
                {
                    item.Create();
                    item.ParentId = id;
                    item.FactoryCode = entity.FactoryCode;
                    item.FactoryName = entity.FactoryName;
                    item.CheckTaskId = entity.CheckTaskId;
                    item.CreateTime = time;
                    item.Creator = entity.TypeInPerson;
                    if (item.CheckResult == ALP.Application.Service.Resources.Language.GetText("Common.Yes") || item.CheckResult == ALP.Application.Service.Resources.Language.GetText("Common.Qua"))
                    {
                        item.CheckResult = "1";
                    }
                    else if (item.CheckResult == ALP.Application.Service.Resources.Language.GetText("Common.No") || item.CheckResult == ALP.Application.Service.Resources.Language.GetText("Common.UnQua"))
                    {
                        item.CheckResult = "2";
                    }

                }
                _EquipmentCheckResultService.SaveEntity_List(false, entity.TypeInPerson, list, out msg);

                #region 上传图片
                //foreach (var item in imgList)
                //{
                //    item.Id = Guid.NewGuid().ToString();
                //    item.FactoryCode = peopleEntity.FactoryCode;
                //    item.FactoryName = peopleEntity.FactoryName;
                //    item.Module = "设备模块";
                //    item.TableName = "EP_EquipmentCheckRecord";
                //    item.ParentId = id;
                //    item.Creator = entity.ModifyBy;
                //    item.CreateTime = DateTime.Now;
                //}
                //if (imgList != null && imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);
                #endregion

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

        #endregion

        #region 设备点检查询
        /// <summary>
        /// 设备点检查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDataTableCheckResult")]
        public HttpResponseMessage GetDataTableCheckResult(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var queryJson = getValue(jo, "queryJson");
                if (string.IsNullOrEmpty(queryJson)) return AjaxResult(false, "Equipment.EquipmentController.GetDataTableCheckResult.Tips_1");//请选择查询条件


                var list = _EquipmentCheckRecordService.GetDataTableCheckResult(queryJson);

                if (list.Count < 1) return AjaxResult(false, "Equipment.EquipmentController.GetDataTableCheckResult.Tips_2");//没有查询到检验记录

                var data = new List<dynamic>();

                foreach (var item in list.GroupBy(t => new
                {
                    t.EquipmentName,
                    t.CheckTaskName,
                    t.CheckConclusionName,
                    t.TypeInTime
                }).ToList())
                {

                    data.Add(new
                    {
                        item.Key.EquipmentName,
                        item.Key.CheckTaskName,
                        item.Key.CheckConclusionName,
                        item.Key.TypeInTime,
                        CheckItemList = item
                    });
                }

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

        #endregion

        #endregion

        #region 设备故障报修

        #region 根据设备编码获取设备信息
        /// <summary>
        /// 设备保修-根据设备编码获取设备信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEquipmentManageByCode")]
        public HttpResponseMessage GetEquipmentManageByCode(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var equipmentId = getValue(jo, "EquipmentId");
                if (string.IsNullOrEmpty(equipmentId)) return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentManageByCode.Tips_1");//请扫描设备

                //var entity=_EquipmentManageService.GetEntity(t => t.EquipmentId == equipmentId);

                var list = V_EquipmentManageService.GetList(equipmentId);
                if (list.Count() < 1)
                {
                    return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentManageByCode.Tips_2");//没有查到当前设备
                }

                result.resultData = list;
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

        #endregion

        #region 设备故障报修- 设备故障报修保存
        /// <summary>
        /// 设备故障报修- 设备故障报修保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEquipmentMalfunctionRepair")]
        public HttpResponseMessage SaveEquipmentMalfunctionRepair(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var entity = JsonConvert.DeserializeObject<EP_EquipmentMalfunctionRepairEntity>(getValue(jo, "entity"));
                var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(jo["fileUrl"].ToString());

                var equipEntity = V_EquipmentManageService.GetEntity(t => t.EquipmentId == entity.EquipmentId);
                if (equipEntity == null)
                    return AjaxResultWithParams(false, "Equipment.EquipmentController.SaveEquipmentMalfunctionRepair.Tips_1", entity.EquipmentId);//AjaxResult(false, "设备【" + entity.EquipmentId + "】不存在");

                entity.Id = Guid.NewGuid().ToString();
                entity.FactoryCode = equipEntity.TestMethodCoadin;
                entity.FactoryName = equipEntity.TestMethodCoadinName;
                entity.CreateTime = DateTime.Now;
                entity.RepairingStatus = "1";
                entity.EnabledMark = true;

                _EquipmentMalfunctionRepairService.SaveEntity(null, entity, out msg);

                #region 上传图片
                foreach (var item in imgList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = equipEntity.TestMethodCoadin;
                    item.FactoryName = equipEntity.TestMethodCoadinName;
                    item.Module = ALP.Application.Service.Resources.Language.GetText("Equipment.EquipmentController.SaveEquipmentMalfunctionRepair.Tips_2"); //"设备模块";
                    item.TableName = "EP_EquipmentMalfunctionRepair";
                    item.ParentId = entity.Id;
                    item.Creator = entity.Creator;
                    item.CreateTime = DateTime.Now;
                }
                if (imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);
                #endregion

                result.resultData = null;
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

        #endregion

        #endregion

        #region 设备维修

        #region 获取设备报修记录 -未修理
        /// <summary>
        /// 设备故障报修- 获取设备报修记录
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEquipmentMalfunctionRepair")]
        public HttpResponseMessage GetEquipmentMalfunctionRepair(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var queryJson = getValue(jo, "queryJson");
                if (string.IsNullOrEmpty(queryJson)) return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentMalfunctionRepair.Tips_1");//请选择查询条件

                var data = _EquipmentMalfunctionRepairService.GetEquipmentMalfunctionRepair(queryJson);

                if (data.Count < 1) return AjaxResult(false, "Equipment.EquipmentController.GetEquipmentMalfunctionRepair.Tips_2");//没有查询到报修记录

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

        #endregion


        #region 设备维修保存
        /// <summary>
        /// 设备故障维修- 设备维修记录保存
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEquipmentRepairRecord")]
        public HttpResponseMessage SaveEquipmentRepairRecord(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var entity = JsonConvert.DeserializeObject<EP_EquipmentMalfunctionRepairEntity>(getValue(jo, "entity"));
                var data = JsonConvert.DeserializeObject<List<EP_EquipmentSparePartsEntity>>(getValue(jo, "data"));
                var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(jo["fileUrl"].ToString());

                entity.ModifyTime = DateTime.Now;
                entity.RepairingStatus = "2";
                //System.TimeSpan ts= entity.FinishTime.Value - entity.CreateTime.Value;
                //entity.TimeLength = ts.TotalMinutes.ToString();

                var oldEntity = _EquipmentMalfunctionRepairService.Get_ExpressionEntity(t => t.Id == entity.Id);

                foreach (var item in data)
                {
                    item.Create();
                    item.FactoryCode = oldEntity?.FactoryCode;
                    item.FactoryName = oldEntity?.FactoryName;
                    item.RepairId = entity.Id;
                    item.EnabledMark = true;
                    item.UseType = "1";
                }

                _EquipmentMalfunctionRepairService.SaveEntity(entity.Id, entity, out msg);

                if (data.Count > 0) _EquipmentSparePartsService.SaveEntity_List(false, null, data, out msg);

                #region 上传图片
                foreach (var item in imgList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = oldEntity?.FactoryCode;
                    item.FactoryName = oldEntity?.FactoryName;
                    item.Module = ALP.Application.Service.Resources.Language.GetText("Equipment.EquipmentController.SaveEquipmentRepairRecord.Tips_2"); //"设备模块";
                    item.TableName = "EP_EquipmentMalfunctionRepair";
                    item.ParentId = entity.Id;
                    item.Creator = entity.ModifyBy;
                    item.CreateTime = DateTime.Now;
                }
                if (imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);
                #endregion

                result.resultData = null;
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

        #endregion

        #endregion

        #region 设备保养


        #region 获取设备保养任务
        /// <summary>
        /// 设备保养-获取设备保养任务
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEquipmentMaintainTask")]
        public HttpResponseMessage GetEquipmentMaintainTask(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var queryJson = getValue(jo, "queryJson");

                var data = _EquipmentMaintainTaskService.GetEquipmentMaintainTask(queryJson);

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

        #endregion


        #endregion

    }
}
