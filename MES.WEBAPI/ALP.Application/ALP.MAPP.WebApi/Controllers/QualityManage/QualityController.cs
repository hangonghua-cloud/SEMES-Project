using ALP.Application.Busines.ProduceManage;
using ALP.Application.Busines.QualityManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Service.BaseManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util.Extension;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;
using ALP.Application.Service.ProduceManage;

namespace ALP.Application.WebApi.Controllers.QualityManage
{
    /// <summary>
    /// 质量模块
    /// </summary>
    [RoutePrefix("Quality")]
    public class QualityController : ApiBaseController
    {
        #region 实例化
        private BS_People_Service _bsPeopleService = new BS_People_Service();//人员
        private BsModelWithResourceService _bsModelWithResourceService = new BsModelWithResourceService();//工厂建模


        private PM_TransferCardBLL _TransferCardBLL = new PM_TransferCardBLL();
        private PM_OwnProductOrder_Service _ownProductOrderService = new PM_OwnProductOrder_Service();//自制半成品工单
        PM_OwnProductTransferBLL _OwnProductTransferBLL = new PM_OwnProductTransferBLL();//自制半成品
        PM_OwnProductBGBLL _ownProductBGBLL = new PM_OwnProductBGBLL();//自治半成品报工记录
        private PM_TransferCardResumeBLL _TransferCardResumeBLL = new PM_TransferCardResumeBLL();
        private QC_TestMaintenanceBLL _TestMaintenanceBLL = new QC_TestMaintenanceBLL();
        private QC_TestItemMaintenanceBLL _TestItemMaintenanceBLL = new QC_TestItemMaintenanceBLL();
        private QC_TestProcessMaintenanceBLL _TestProcessMaintenanceBLL = new QC_TestProcessMaintenanceBLL();

        private QC_IPQCDetail_BLL _IPQCDetailBLL = new QC_IPQCDetail_BLL();
        private QC_IPQCDetailResult_BLL _IPQCDetailResultBLL = new QC_IPQCDetailResult_BLL();

        private QC_PollingDetail_BLL _PollingDetailBLL = new QC_PollingDetail_BLL();
        private QC_PollingDetailResult_BLL _PollingDetailResultBLL = new QC_PollingDetailResult_BLL();

        private PM_PackingPrintMarkBLL _PackingPrintMarkBLL = new PM_PackingPrintMarkBLL();
        private QC_OQCCheckConfigItemBLL _OQCCheckConfigItemBLL = new QC_OQCCheckConfigItemBLL();

        private QC_OQCQualityCheckBLL _OQCQualityCheckBLL = new QC_OQCQualityCheckBLL();
        private QC_OQCQualityCheckItemBLL _OQCQualityCheckItemBLL = new QC_OQCQualityCheckItemBLL();
        Base_Images_Service _ImagesService = new Base_Images_Service();//图片上传


        private const string SuccessMsg = "Common.ExecutionSuccess";
        private const string FaildMsg = "Common.ExecutionError";
        #endregion

        #region 巡检/过程检验

        #region 巡检/过程检验-流转卡扫描

        /// <summary>
        /// 扫描流转卡获取信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetQCTransferCardEntity")]
        public HttpResponseMessage GetQCTransferCardEntity(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            dynamic scanResult = new ExpandoObject();
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var cardCode = getValue(jo, "CardCode");
                if (cardCode == null) return AjaxResult(false, "QualityManage.QualityController.GetQCTransferCardEntity.Tips_1");//AjaxResult(false, "请扫描流转卡");
                var cardEntity = _TransferCardBLL.Get_ExpressionEntity(t => t.CardCode == cardCode);
                if (cardEntity != null)
                {
                    if (cardEntity.CardStatus == "5")
                    {
                        return AjaxResult(false, "QualityManage.QualityController.GetQCTransferCardEntity.Tips_2");//AjaxResult(false, "流转卡已报废！");
                    }
                    var entitylist = _TransferCardBLL.GetQCTransferCardEntity(cardCode);
                    if (entitylist == null || entitylist.Count < 1) return AjaxResultWithParams(false, "QualityManage.QualityController.GetQCTransferCardEntity.Tips_3", cardEntity.MaterialCode);//AjaxResult(false, $"物料[{cardEntity.MaterialCode}]没有物料组");

                    var data = new List<dynamic>();

                    foreach (var item in entitylist.GroupBy(t => new
                    {
                        t.FactoryCode,
                        t.ProductOrder,
                        t.WorkOrder,
                        t.ExeWorkOrder,
                        t.CardCode,
                        t.CardName,
                        t.ContainerNO,
                        t.Spec,
                        t.CardStatus
                    }).ToList())
                    {
                        data.Add(new
                        {
                            item.Key.FactoryCode,
                            item.Key.ProductOrder,
                            item.Key.WorkOrder,
                            item.Key.ExeWorkOrder,
                            item.Key.CardCode,
                            item.Key.CardName,
                            item.Key.ContainerNO,
                            item.Key.Spec,
                            item.Key.CardStatus,
                            MaterialGrouplist = item,
                            ProductType = "1"
                        });

                    }
                    return AjaxResult(true, "Common.Success", data);
                }
                var ownCardEntity = _OwnProductTransferBLL.Get_ExpressionEntity(t => t.TransferCode == cardCode);
                if (ownCardEntity != null)
                {
                    var ownProductOrderEntity = _ownProductOrderService.Get_ExpressionEntity(t => t.WorkOrder == ownCardEntity.WorkOrder);
                    var entitylist = _TransferCardBLL.GetSemiQCTransferCardEntity(cardCode);
                    if (entitylist == null || entitylist.Count < 1) return AjaxResultWithParams(false, "QualityManage.QualityController.GetQCTransferCardEntity.Tips_3", ownProductOrderEntity.MaterialCode);//AjaxResult(false, $"物料[{ownProductOrderEntity.MaterialCode}]没有物料组");

                    var data = new List<dynamic>();

                    foreach (var item in entitylist.GroupBy(t => new
                    {
                        t.FactoryCode,
                        t.ProductOrder,
                        t.WorkOrder,
                        t.ExeWorkOrder,
                        t.CardCode,
                        t.CardName,
                        t.Spec
                    }).ToList())
                    {
                        data.Add(new
                        {
                            item.Key.FactoryCode,
                            item.Key.ProductOrder,
                            item.Key.WorkOrder,
                            item.Key.ExeWorkOrder,
                            item.Key.CardCode,
                            item.Key.CardName,
                            item.Key.Spec,
                            MaterialGrouplist = item,
                            ProductType = "2" //半成品
                        });

                    }
                    return AjaxResult(true, "Common.Success", data);
                }
                return AjaxResult(false, "QualityManage.QualityController.GetQCTransferCardEntity.Tips_4");//AjaxResult(false, "当前流转卡不存在");
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", msg + ex.Message);//ALP.Application.Service.Resources.Language.GetText("Common.ExecutionErrorWithOther", msg + ex.Message);
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        /// <summary>
        /// 根据流转卡获取流转履历表数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetprocessList")]
        public HttpResponseMessage GetprocessList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";
            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var cardCode = getValue(jo, "CardCode");
                if (cardCode == null) return AjaxResult(false, "QualityManage.QualityController.GetprocessList.Tips_1");//AjaxResult(false, "请扫描流转卡");

                string processCode = "";
                var resumeEntity = _TransferCardResumeBLL.Get_ExpressionEntity(t => t.CardCode == cardCode && t.Flag == "1");
                if (resumeEntity == null)
                {
                    var ownBGEntity = _ownProductBGBLL.Get_ExpressionList(t => t.TransferCode == cardCode).OrderByDescending(t => t.CreateTime).FirstOrDefault();
                    if (ownBGEntity == null)
                        return AjaxResult(false, "QualityManage.QualityController.GetprocessList.Tips_2");//AjaxResult(false, "没有与该流转卡相关的工序！");

                    processCode = ownBGEntity.BGProcess;
                }
                else
                    processCode = resumeEntity.ProcessCode;

                result.resultData = processCode;
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

        #region 获取检验方法

        /// <summary>
        /// 获取检验方法
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetQCTestMethodList")]
        public HttpResponseMessage GetQCTestMethodList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var factoryCode = getValue(jo, "FactoryCode");//工厂
                var materialGrouplist = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "MaterialGrouplist"));//物料小类
                var processCode = getValue(jo, "ProcessCode");// 工序
                var testType = getValue(jo, "TestType");// 检验分类 1巡检；2过程检验；3首检

                if (string.IsNullOrEmpty(factoryCode) ||
                   string.IsNullOrEmpty(processCode) || string.IsNullOrEmpty(testType))
                {
                    return AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "查询参数不能为空");
                }
                var expression = LinqExtensions.True<QC_TestMaintenanceEntity>();

                expression = expression.And(t => t.FactoryCode == factoryCode);
                expression = expression.And(t => t.TestType == testType);
                expression = expression.And(t => t.IsEnabled == true);


                var query = from test in _TestMaintenanceBLL.Get_ExpressionList(expression)
                            join process in _TestProcessMaintenanceBLL.Get_ExpressionList(t => t.ProcessCode == processCode) on test.Id equals process.TestMaintenanceId
                            select new
                            {
                                test.TestType,
                                test.SmallClass,
                                test.TestMethodCoading,
                                test.TestMethodName,
                                test.TestMethodDescription,
                            };
                var list = query.ToList();


                var list1 = new List<dynamic>();
                var list2 = new List<dynamic>();
                for (int i = 0; i < materialGrouplist.Count; i++)
                {
                    string a = materialGrouplist[i].GroupName.ToString();
                    list1.AddRange(list.Where(t => t.SmallClass == a));

                }
                if (list1.Count < 1) return AjaxResult(false, "QualityManage.QualityController.GetQCTestMethodList.Tips_1");//AjaxResult(false, "检测方法为空,请维护检测方法");


                list2.Add(list1.First());


                result.resultData = list2;
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

        #region 获取检验项目
        /// <summary>
        /// 获取检验项目
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetQCTestItemList")]
        public HttpResponseMessage GetQCTestItemList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "查询参数不能为空");

                var factoryCode = getValue(jo, "FactoryCode");//工厂
                var materialGrouplist = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "MaterialGrouplist"));//物料小类
                var processCode = getValue(jo, "ProcessCode");// 工序
                var testMethodCoading = getValue(jo, "TestMethodCoading");// 检验方法
                var testType = getValue(jo, "TestType");// 检验分类 1巡检；2过程检验；3首检

                if (string.IsNullOrEmpty(factoryCode) ||
                   string.IsNullOrEmpty(processCode) || string.IsNullOrEmpty(testType))
                {
                    return AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "查询参数不能为空");
                }


                var expression = LinqExtensions.True<QC_TestMaintenanceEntity>();

                expression = expression.And(t => t.FactoryCode == factoryCode);
                expression = expression.And(t => t.TestType == testType);
                expression = expression.And(t => t.IsEnabled == true);
                expression = expression.And(t => t.TestMethodCoading == testMethodCoading);



                var query = from test in _TestMaintenanceBLL.Get_ExpressionList(expression)
                            join item in _TestItemMaintenanceBLL.Get_ExpressionList(t => t.IsEnabled == true) on test.Id equals item.TestMaintenanceId
                            join process in _TestProcessMaintenanceBLL.Get_ExpressionList(t => t.ProcessCode == processCode) on test.Id equals process.TestMaintenanceId
                            select new
                            {
                                test.TestType,
                                test.SmallClass,
                                test.TestMethodCoading,
                                item.TestItemCoading,
                                item.TestItemName,
                                item.TestItemStandard,
                                item.TestDepartment,
                                item.DataType,
                                item.DataTypeName
                            };
                var list = query.ToList();

                var list1 = new List<dynamic>();
                for (int i = 0; i < materialGrouplist.Count; i++)
                {
                    string a = materialGrouplist[i].GroupName.ToString();
                    list1.AddRange(list.Where(t => t.SmallClass == a));

                }


                //巡检只有质量
                if (testType == "1") list1 = list1.Where(t => t.TestDepartment == "2").ToList();

                if (list1.Count < 1) return AjaxResult(false, "QualityManage.QualityController.GetQCTestItemList.Tips_1");//AjaxResult(false, "检测项目为空,请维护检测项目");

                var taskList = new List<dynamic>();
                foreach (var detail in list1)
                {
                    if (int.Parse(detail.DataType) > 3 && detail.DataTypeName.IndexOf("/") > 0)
                    {
                        var optionList = new List<dynamic>();
                        var array = detail.DataTypeName.Split('/');
                        for (var i = 0; i < array.Length; i++)
                        {
                            optionList.Add(new
                            {
                                text = array[i]
                            });
                        }
                        taskList.Add(new
                        {
                            detail.TestMethodCoading,
                            detail.TestItemCoading,
                            detail.TestItemName,
                            detail.TestItemStandard,
                            detail.TestDepartment,
                            detail.DataType,
                            detail.DataTypeName,
                            TestItemResult = "",
                            Options = optionList
                        });
                    }
                    else
                    {
                        taskList.Add(new
                        {
                            detail.TestMethodCoading,
                            detail.TestItemCoading,
                            detail.TestItemName,
                            detail.TestItemStandard,
                            detail.TestDepartment,
                            detail.DataType,
                            detail.DataTypeName,
                            TestItemResult = "",
                            Options = ""
                        });
                    }
                }

                result.resultData = taskList.OrderBy(t=>t.TestItemCoading).ToList();
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

        #region 巡检/过程检验-保存
        /// <summary>
        /// 扫描流转卡获取信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQCTestItemForm")]
        public HttpResponseMessage SaveQCTestItemForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var time = DateTime.Now;
            //var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var testType = getValue(jo, "TestType");// 检验分类 1巡检；2过程检验；3首检

                //1.巡检 QC_PollingDetail
                if (testType == "1")
                {
                    var entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "entity"));
                    var list = JsonConvert.DeserializeObject<List<QC_PollingDetailResultEntity>>(getValue(jo, "data"));
                    var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "fileUrl"));
                    var userCode = getValue(jo, "userCode");//用户编码
                    var userName = getValue(jo, "userName");//用户名称
                    //查找是否需要实验室检验
                    var query = from test in _TestMaintenanceBLL.Get_ExpressionList(t => t.TestMethodCoading == entity.CalibrationMethod)
                                join item in _TestItemMaintenanceBLL.Get_ExpressionList(t => t.IsEnabled == true && t.TestDepartment == "1") on test.Id equals item.TestMaintenanceId
                                select new
                                {
                                    item.TestDepartment
                                };
                    var count = query.ToList().Count;
                    if (count >= 1) entity.LaboratoryTestStatus = "2";//待检验
                    else entity.LaboratoryTestStatus = "1";//无需检验

                    var testMainEntity = _TestMaintenanceBLL.Get_ExpressionEntity(t => t.TestMethodCoading == entity.CalibrationMethod);
                    if (testMainEntity == null)
                        return AjaxResult(false, "QualityManage.QualityController.SaveQCTestItemForm.Tips_1");//AjaxResult(false, "检验方法不存在");

                    var factoryEntity = _bsModelWithResourceService.GetEntity(t => t.ResourceCode == testMainEntity.FactoryCode);

                    entity.Id = Guid.NewGuid().ToString();
                    entity.FactoryCode = testMainEntity.FactoryCode;
                    entity.FactoryName = factoryEntity?.ResourceName;
                    entity.InspectionTime = time;
                    entity.CreateTime = time;
                    entity.Creator = entity.Inspector;//质量检验
                    entity.EnabledMark = true;


                    foreach (var item in imgList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.FactoryCode = testMainEntity.FactoryCode;
                        item.FactoryName = factoryEntity?.ResourceName;
                        item.Module = ALP.Application.Service.Resources.Language.GetText("QualityManage.QualityController.SaveQCTestItemForm.Data_1");//"质量模块";
                        item.TableName = "QC_PollingDetail";
                        item.ParentId = entity.Id;
                        item.Creator = userCode;
                        item.CreateTime = DateTime.Now;
                    }

                    foreach (var item in list)
                    {
                        item.Create();
                        item.FactoryCode = testMainEntity.FactoryCode;
                        item.FactoryName = factoryEntity?.ResourceName;
                        item.Creator = entity.Inspector;
                        item.PollingDetailId = entity.Id;
                        item.CreatTime = time;
                        item.EnabledMark = true;
                    }

                    var msg = "";
                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    // 设置事务超时时间为60秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 60);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _PollingDetailBLL.SaveEntity(null, entity, out msg);
                        _PollingDetailResultBLL.SaveEntity_List(false, null, list, out msg);
                        if (imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);

                        ts.Complete();
                    }

                }
                else if (testType == "2")
                {
                    //2.过程检验 QC_IPQCDetail
                    var entity = JsonConvert.DeserializeObject<QC_IPQCDetailEntity>(getValue(jo, "entity"));
                    string machineCode = entity.TestMachine;
                    var list = JsonConvert.DeserializeObject<List<QC_IPQCDetailResultEntity>>(getValue(jo, "data"));
                    var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "fileUrl"));
                    var userCode = getValue(jo, "userCode");//用户编码
                    var userName = getValue(jo, "userName");//用户名称

                    if (string.IsNullOrEmpty(machineCode))
                    {
                        return AjaxResult(false, "QualityManage.QualityController.SaveQCTestItemForm.Tips_2");//AjaxResult(false, "机台没有获取到值，请重新扫描");
                    }

                    var testMainEntity = _TestMaintenanceBLL.Get_ExpressionEntity(t => t.TestMethodCoading == entity.CalibrationMethod && t.IsEnabled == true);
                    if (testMainEntity == null)
                        return AjaxResult(false, "QualityManage.QualityController.SaveQCTestItemForm.Tips_1");//AjaxResult(false, "检验方法不存在");

                    var factoryEntity = _bsModelWithResourceService.GetEntity(t => t.ResourceCode == testMainEntity.FactoryCode);

                    entity.Id = Guid.NewGuid().ToString();
                    entity.FactoryCode = testMainEntity.FactoryCode;
                    entity.FactoryName = factoryEntity?.ResourceName;
                    entity.EnabledMark = true;
                    entity.CreateTime = time;
                    entity.InspectionTime = time;

                    foreach (var item in list)
                    {
                        item.Create();
                        item.FactoryCode = testMainEntity.FactoryCode;
                        item.FactoryName = factoryEntity?.ResourceName;
                        item.CreatTime = time;
                        item.Creator = entity.Inspector;
                        item.FlowCardId = entity.Id;
                        item.EnabledMark = true;
                    }

                    foreach (var item in imgList)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.FactoryCode = testMainEntity.FactoryCode;
                        item.FactoryName = factoryEntity?.ResourceName;
                        item.Module = ALP.Application.Service.Resources.Language.GetText("QualityManage.QualityController.SaveQCTestItemForm.Data_1");//"质量模块";
                        item.TableName = "QC_IPQCDetail";
                        item.ParentId = entity.Id;
                        item.Creator = CurrentAccount.UserCode;
                        item.CreateTime = DateTime.Now;
                    }

                    #region 记录登陆人绑定的机台、生产小组
                    var peopleEntity = _bsPeopleService.Get_ExpressionEntity(t => t.Code == userCode);
                    var isUpdate = false;
                    if (peopleEntity.MachineCode != machineCode)
                    {
                        isUpdate = true;
                        peopleEntity.MachineCode = machineCode;
                        peopleEntity.ModifyBy = userCode;
                        peopleEntity.ModifyTime = DateTime.Now;
                        //if (!string.IsNullOrEmpty(peopleEntity.Remark) && peopleEntity.Remark.Length > 1900)
                        //    peopleEntity.Remark = DateTime.Now.ToString() + userName + "修改机台为：" + machineCode + ";";
                        //else
                        //    peopleEntity.Remark += DateTime.Now.ToString() + userName + "修改机台为：" + machineCode + ";";
                    }
                    #endregion

                    var msg = "";
                    TransactionOptions transactionOption = new TransactionOptions();
                    //设置事务隔离级别
                    transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                    // 设置事务超时时间为60秒
                    transactionOption.Timeout = new TimeSpan(0, 0, 60);
                    using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                    //using (var ts = new TransactionScope())
                    {
                        _IPQCDetailBLL.SaveEntity(null, entity, out msg);
                        _IPQCDetailResultBLL.SaveEntity_List(false, null, list, out msg);
                        if (imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);
                        if (isUpdate)//更新绑定的机台、生产小组信息
                        {
                            _bsPeopleService.SaveEntity(peopleEntity.ID, peopleEntity, out msg);
                        }
                        ts.Complete();
                    }

                }
                result.resultData = null;
                result.success = true;
                result.returnMsg = SuccessMsg;

            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "Common.Error";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region 检测记录查询
        /// <summary>
        /// 检测记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetQCTestRecordList")]
        public HttpResponseMessage GetQCTestRecordList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var testType = getValue(jo, "TestType");// 检验分类 1巡检；2过程检验；3首检
                var queryJson = getValue(jo, "queryJson");

                if (testType == "1")
                {
                    result.resultData = _PollingDetailBLL.GetPageDataTableList(null, queryJson);
                }
                else
                {
                    result.resultData = _IPQCDetailBLL.GetPageDataTableList(null, queryJson);
                }

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

        #region  检测记录查询明细
        /// <summary>
        /// 检测记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetQCTestResultRecordList")]
        public HttpResponseMessage GetQCTestResultRecordList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var testType = getValue(jo, "TestType");// 检验分类 1巡检；2过程检验；3首检
                var queryJson = getValue(jo, "queryJson");

                if (testType == "1")
                {
                    result.resultData = _PollingDetailResultBLL.GetPageDataTableList(null, queryJson);
                }
                else
                {
                    result.resultData = _IPQCDetailResultBLL.GetCheckPageDataTableList(null, queryJson);
                }

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

        #region 巡检质量判定
        /// <summary>
        /// 巡检质量判定
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePollingDetailForm")]
        public HttpResponseMessage SavePollingDetailForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "entity"));

                var ent = _PollingDetailBLL.Get_ExpressionEntity(t => t.Id == entity.Id);
                if (ent == null) return AjaxResult(false, "QualityManage.QualityController.SavePollingDetailForm.Tips_1");//AjaxResult(false, "没有这个检测记录");

                ent.Remark = entity.Remark;
                ent.ModifyTime = DateTime.Now;
                ent.Determination = entity.Determination;

                _PollingDetailBLL.SaveEntity(ent.Id, ent, out msg);

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

        #region OQC成品检验

        #region  OQC根据唛头号查询订单信息
        /// <summary>
        /// OQC根据唛头号查询订单信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOrderByTransferCode")]
        public HttpResponseMessage GetOrderByTransferCode(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                //PackTransferCode:""

                var packTransferCode = getValue(jo, "PackTransferCode");
                if (string.IsNullOrEmpty(packTransferCode)) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var list = _PackingPrintMarkBLL.GetOrderByTransferCode(packTransferCode);
                if (list.Count < 1) return AjaxResult(false, "QualityManage.QualityController.GetOrderByTransferCode.Tips_1");//AjaxResult(false, "没有找到相关的唛头订单信息");

                dynamic obj = new ExpandoObject();
                foreach (var item in list.GroupBy(t => new
                {
                    t.PackTransferCode,
                    t.ProductOrder,
                    t.MaterialCode,
                    t.ContainerNO,
                    t.CustomerPO,
                    t.Spec
                }).ToList())
                {
                    var optionList = new List<dynamic>();
                    foreach (var detail in item)
                    {
                        if (detail.id.ToString() != "")
                        {
                            optionList.Add(new
                            {
                                TestMethodCoading = detail.id,
                                TestMethodName = detail.TestMethodName
                            });
                        }
                    }
                    var optionList1 = new List<dynamic>();

                    optionList1.Add(optionList.First());

                    obj.PackTransferCode = item.Key.PackTransferCode;
                    obj.ProductOrder = item.Key.ProductOrder;
                    obj.MaterialCode = item.Key.MaterialCode;
                    obj.ContainerNO = item.Key.ContainerNO;
                    obj.CustomerPO = item.Key.CustomerPO;
                    obj.Spec = item.Key.Spec;
                    obj.OpetionList = optionList1;
                }

                result.resultData = obj;
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

        #region OQC根据检验方法获取检验项目
        /// <summary>
        /// OQC根据检验方法获取检验项目
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOQCCheckConfigItem")]
        public HttpResponseMessage GetOQCCheckConfigItem(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var id = getValue(jo, "Id");
                if (string.IsNullOrEmpty(id)) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                //Id:"xxxxxxxxxx",TestDepartment:"2"
                //查询质量检验项目
                var list = _OQCCheckConfigItemBLL.Get_ExpressionList(t => t.OQCCheckConfigId == id && t.TestDepartment == "2").ToList();
                if (list.Count < 1) return AjaxResult(false, "QualityManage.QualityController.GetOQCCheckConfigItem.Tips_1");//AjaxResult(false, "没有获取到检验项目");

                var taskList = new List<dynamic>();
                foreach (var detail in list)
                {
                    if (int.Parse(detail.DataType) > 3 && detail.DataTypeName.IndexOf("/") > 0)
                    {
                        var optionList = new List<dynamic>();
                        var array = detail.DataTypeName.Split('/');
                        for (var i = 0; i < array.Length; i++)
                        {
                            optionList.Add(new
                            {
                                text = array[i]
                            });
                        }
                        taskList.Add(new
                        {
                            detail.TestItemCoading,
                            detail.TestItemName,
                            detail.TestItemStandard,
                            detail.TestDepartment,
                            detail.DataType,
                            detail.DataTypeName,
                            TestItemResult = "",
                            Distinguish = "",
                            Options = optionList
                        });
                    }
                    else
                    {
                        taskList.Add(new
                        {
                            detail.TestItemCoading,
                            detail.TestItemName,
                            detail.TestItemStandard,
                            detail.TestDepartment,
                            detail.DataType,
                            detail.DataTypeName,
                            TestItemResult = "",
                            Distinguish = "",
                            Options = ""
                        });
                    }
                }

                result.resultData = taskList;
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

        #region  OQC保存-检验记录
        /// <summary>
        /// OQC保存-检验记录
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveOQCCheckConfigForm")]
        public HttpResponseMessage SaveOQCCheckConfigForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";
            var returnNum = "";
            var time = DateTime.Now;
            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var entity = JsonConvert.DeserializeObject<QC_OQCQualityCheckEntity>(getValue(jo, "entity"));
                var list = JsonConvert.DeserializeObject<List<QC_OQCQualityCheckItemEntity>>(getValue(jo, "data"));
                var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "fileUrl"));
                var userCode = getValue(jo, "userCode");//用户编码
                var userName = getValue(jo, "userName");//用户名称

                var markEntity = _PackingPrintMarkBLL.Get_ExpressionEntity(t => t.PackTransferCode == entity.PackTransferCode);
                if (markEntity == null)
                    return AjaxResult(false, "QualityManage.QualityController.SaveOQCCheckConfigForm.Tips_1");//AjaxResult(false, "唛头不存在");

                entity.Id = Guid.NewGuid().ToString();
                entity.FactoryCode = markEntity.FactoryCode;
                entity.FactoryName = markEntity.FactoryName;
                entity.CreateTime = time;
                entity.CheckStatus = "2";//检验中

                _OQCQualityCheckBLL.GetSerialNO("OQCQualityCheck", out returnNum, out msg);
                entity.InspectNo = "OQC" + time.ToString("yyMMdd") + returnNum;

                _OQCQualityCheckBLL.SaveEntity(null, entity, out msg);

                foreach (var item in list)
                {
                    item.Create();
                    item.OQCQualityCheckId = entity.Id;
                    item.FactoryCode = markEntity.FactoryCode;
                    item.FactoryName = markEntity.FactoryName;
                    item.CreateTime = time;
                }

                _OQCQualityCheckItemBLL.SaveEntity_List(false, null, list, out msg);


                foreach (var item in imgList)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = markEntity.FactoryCode;
                    item.FactoryName = markEntity.FactoryName;
                    item.Module = ALP.Application.Service.Resources.Language.GetText("QualityManage.QualityController.SaveOQCCheckConfigForm.Data_1"); //"质量模块";
                    item.TableName = "QC_OQCQualityCheck";
                    item.ParentId = entity.Id;
                    item.Creator = userCode;
                    item.CreateTime = DateTime.Now;
                }

                if (imgList.Count > 0) _ImagesService.SaveEntity_List(false, null, imgList, out msg);

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

        #region OQC检验记录查询
        /// <summary>
        /// OQC检验记录查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOQCQualityCheckRecord")]
        public HttpResponseMessage GetOQCQualityCheckRecord(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var queryJson = getValue(jo, "queryJson");
                //PackTransferCode:"唛头号",ProductOrder:"订单号",ContainerNO:"柜号",StartDate:"2021-10-01",EndDate:"2021-10-10"

                if (string.IsNullOrEmpty(queryJson)) return AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "Common.SearchParamsNotNull");//AjaxResult(false, "查询参数不能为空");

                var data = _OQCQualityCheckBLL.GetPageDataTableList(null, queryJson);

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

        #region OQC成品检验单合批
        /// <summary>
        /// OQC成品检验单合批
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveOQCQualityChecInspectNo")]
        public HttpResponseMessage SaveOQCQualityChecInspectNo(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var list = JsonConvert.DeserializeObject<List<string>>(getValue(jo, "data"));
                var inspectNo = getValue(jo, "InspectNo");//合并后批号
                var userCode = getValue(jo, "UserCode");//操作人

                var idStr = "";
                foreach (var str in list)
                {
                    idStr = "'" + str + "',";
                }
                if (!string.IsNullOrEmpty(idStr)) idStr = idStr.Substring(0, idStr.Length - 1);
                else return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                _OQCQualityCheckBLL.SaveOQCQualityChecInspectNo(inspectNo, userCode, idStr);

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

        #region OQC成品检验项目记录
        /// <summary>
        /// OQC成品检验项目记录
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetOQCQualityCheckItemRecord")]
        public HttpResponseMessage GetOQCQualityCheckItemRecord(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空
                var querJson = getValue(jo, "queryJson");
                //OQCQualityCheckId:""

                var data = _OQCQualityCheckItemBLL.GetPageDataTableList(null, querJson);
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

        #region OQC成品检验质量判定
        /// <summary>
        /// OQC成品检验质量判定
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveOQCQualityCheckResult")]
        public HttpResponseMessage SaveOQCQualityCheckResult(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var msg = "";

            try
            {
                if (jo == null) return AjaxResult(false, "Common.ParamsNotNull");//参数不能为空

                var id = getValue(jo, "Id");
                var checkResult = getValue(jo, "CheckResult");
                var userCode = getValue(jo, "UserCode");

                var entity = _OQCQualityCheckBLL.Get_ExpressionEntity(t => t.Id == id);
                entity.CheckResult = checkResult;
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;

                _OQCQualityCheckBLL.SaveEntity(id, entity, out msg);

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
