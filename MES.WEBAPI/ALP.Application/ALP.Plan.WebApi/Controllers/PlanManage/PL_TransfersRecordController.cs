using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.SAP;
using ALP.Application.Service.SAP;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.SystemManage;
using ALP.Application.Service.PlanManage;
using ALP.Application.Entity.PlanManage;
using System.Transactions;
using ALP.Application.Service.BaseManage;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Service.ProduceManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-16
    /// 2.创建作者: jpf
    /// 3.功能描述: PL_TransfersRecordController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 跨工厂调拨
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PL_TransfersRecord")]
    public class PL_TransfersRecordController : ApiBaseController
    {

        private DataItemBLL _dataItemBLL = new DataItemBLL();
        PL_Process_Service _plProcessService = new PL_Process_Service();
        PL_ProcessOfOperations_Service _plProcessOfOperationsService = new PL_ProcessOfOperations_Service();
        PL_ProcessOfOperationsAttr_Service _plProcessOfOperationsAttrService = new PL_ProcessOfOperationsAttr_Service();
        PL_BOM_Service _plBOMService = new PL_BOM_Service();
        PL_BOMItems_Service _plBOMItemsService = new PL_BOMItems_Service();
        PL_Material_Service _plMaterialService = new PL_Material_Service();
        PL_MaterialFacet_Service _plMaterialFacet = new PL_MaterialFacet_Service();
        PL_WorkOrder_Service _plWorkOrderService = new PL_WorkOrder_Service();
        PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("SAP.PL_TransfersRecordController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_TransfersRecordPageList")]
        public HttpResponseMessage PL_TransfersRecordPageList(JObject jo)
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
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PL_TransfersRecordPageDataTableList")]
        public HttpResponseMessage PL_TransfersRecordPageDataTableList(JObject jo)
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
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
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
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPL_TransfersRecordList")]
        public HttpResponseMessage GetPL_TransfersRecordList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
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
        /// 工单调拨接收操作
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ReceivePL_TransfersRecord")]
        public HttpResponseMessage ReceivePL_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {//业务服务类
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();

                //发起工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "Entity"));

                //获取登录人员的工厂权限
                var PeoFactoryCode = new BS_People_Service().Get_ExpressionEntity(t => t.Code == userCode).FactoryCode;
                foreach (var Item in entity_list)
                {
                    if (!PeoFactoryCode.Contains(Item.SendFactoryCode))
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_7");//账号权限工厂与发起工厂不一致，不允许接收
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                //根据工单号查询工单表并将VC和正常工单进行区分
                var Plworklist =( from a in entity_list
                                 join b in _plWorkOrderService.Get_ExpressionList(t => t.IsVC == false).ToList()
                                 on a.WorkOrder equals b.WorkOrder
                                 select b ).ToList();
                  
                var isVCPlworklist= (from a in entity_list
                                     join b in _plWorkOrderService.Get_ExpressionList(t => t.IsVC == true).ToList()
                                     on a.WorkOrder equals b.WorkOrder
                                     select b).ToList();
                //using (var ts = new TransactionScope())
                //{
                    
                    string msg = "";
                    var Resultmsg = "";
                    //正常工单调用正常生成工艺路线、BOM的方法
                    if (Plworklist.Count > 0)
                    {
                        foreach (var Item in Plworklist)
                        {
                            Item.FactoryCode = entity_list[0].AcceptFactoryCode;
                            Item.FactoryName = entity_list[0].AcceptFactoryName;
                        }
                        Resultmsg = new PL_WorkOrder_Service().Save_TransferWorkProcessBom(userCode, Plworklist);
                    }
                    if (isVCPlworklist.Count > 0)
                    {
                        foreach (var Item in isVCPlworklist)
                        {
                            Item.FactoryCode = entity_list[0].AcceptFactoryCode;
                            Item.FactoryName = entity_list[0].AcceptFactoryName;
                        }
                        Resultmsg = new PL_WorkOrder_Service().Save_TransferWorkProcessBom(userCode, isVCPlworklist);
                    }

                if (string.IsNullOrEmpty(Resultmsg))
                {
                    int isok = _Service.RebackTransferRecode(userCode, userName, "3", Language.GetText("SAP.PL_TransfersRecordController.Tips_8"), entity_list, out msg);//已接收
                }
                else
                {
                    result.resultData = null;
                    result.success = false;
                    result.returnMsg = Resultmsg;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }   

                    //if (isok > 0 && string.IsNullOrEmpty(Resultmsg))
                    //{
                    //    ts.Complete();
                    //}
                    //else
                    //{
                    //    ts.Dispose();
                    //    result.resultData = null;
                    //    result.success = false;
                    //    result.returnMsg = "执行失败" + msg;
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                //}
                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch(Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

        }
        /// <summary>
        /// 判断一起确认的工单工艺路线是否相同
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("IsidenticalProcess")]
        public HttpResponseMessage IsidenticalProcess(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            var msg = "";
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
               
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "Entity"));
                //查找第一个工单的工艺路线
                var WorkOrder = entity_list[0].WorkOrder;
                var FactoryCode = entity_list[0].AcceptFactoryCode;
                var pl_Process = _plProcessService.Get_ExpressionList(t=>t.WorkOrder== WorkOrder && t.FactoryCode== FactoryCode).FirstOrDefault();
                if (pl_Process.IsEmpty())
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_11");//未生成工艺路线
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //查找工艺路线下的工序
                var pl_Processop = _plProcessOfOperationsService.Get_ExpressionList(t=>t.ProcessId== pl_Process.Id).OrderBy(t=>t.SN) .ToList();

                var num = 0;
                var worklist = "";
                foreach (var item in entity_list)
                {
                    //查找第一个工单的工艺路线
                    var pl_ProcessList = _plProcessService.Get_ExpressionList(t => t.WorkOrder == item.WorkOrder && t.FactoryCode == item.AcceptFactoryCode).FirstOrDefault();
                    //查找工艺路线下的工序
                    var pl_ProcessopList = _plProcessOfOperationsService.Get_ExpressionList(t => t.ProcessId == pl_ProcessList.Id).OrderBy(t => t.SN).ToList();
                    if(pl_ProcessopList.Count!= pl_Processop.Count)
                    {
                        num = num + 1;
                        worklist = worklist + ',' + item.WorkOrder;
                    }
                    else
                    {
                        for(int i=0;i< pl_ProcessopList.Count;i++)
                       
                        {
                          if(pl_ProcessopList[i].SN != pl_Processop[i].SN || pl_ProcessopList[i].OperationCode != pl_Processop[i].OperationCode)
                            {
                                num = num + 1;
                                worklist = worklist + ',' + item.WorkOrder;
                            }
                        }
                    }
                }
                result.resultData = null;
                result.success = num>0? false : true;
                result.returnMsg = num > 0 ? worklist + "工艺路线与选择第一条工单的工艺路线不同":Language.GetText("Common.ExecutionSuccess") ;//执行成功
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
        /// 调拨工单确认部分工序调拨
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Otherconfirm_TransfersRecord")]
        public HttpResponseMessage Otherconfirm_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            var msg = "";
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_13");//缺少Entity参数！请选择一条开始生产的工序
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                PL_ProcessOfOperationsEntity processEntities= JsonConvert.DeserializeObject<PL_ProcessOfOperationsEntity>(getValue(jo, "Entity"));
                //List<PL_ProcessEntity> processEntities= JsonConvert.DeserializeObject<List<PL_ProcessEntity>>(getValue(jo, "Entity"));
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "ListData"));
                #region 判断工单在原工厂是否完工
                //var isnum = 0;
                //var WorkOrder = "";
                //判断调拨工单是否在原工厂生产完
                foreach (var item in entity_list)
                {
                    //判断流转卡是否有返工或者待返工的
                    var PM_TransferCardList = new PM_TransferCard_Service().Get_ExpressionList(t=>t.WorkOrder==item.WorkOrder && (t.CardStatus == "2" || t.CardStatus == "3")).ToList();
                    if (PM_TransferCardList.Any())
                    {
                        //isnum = isnum + 1;
                        //WorkOrder = WorkOrder + "," + item.WorkOrder;
                        result.success = false;
                        result.returnMsg = "操作失败：" + item.WorkOrder + Language.GetText("SAP.PL_TransfersRecordController.Tips_14");//在原调拨工序未生产完
                        result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else
                    {
                        //当无待返工的或者返工中，判断正常流程转卡是否报工完成
                        var TransferCardList = new PM_TransferCard_Service().Get_ExpressionList(t => t.WorkOrder == item.WorkOrder && (t.CardStatus == "1")).ToList();
                        foreach(var i in TransferCardList)
                        {//查找流转履历的数量
                            var TransferCardResume = new PM_TransferCardResume_Service().Get_ExpressionList(t => t.CardCode==i.CardCode && t.ProcessCode== item.TransProcessCode && t.BusinessType=="2" ).ToList();
                            if (!TransferCardResume.Any())
                            {
                                result.success = false;
                                result.returnMsg = "操作失败：" + item.WorkOrder + Language.GetText("SAP.PL_TransfersRecordController.Tips_14");//在原调拨工序未生产完
                                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                                return Request.CreateResponse(HttpStatusCode.OK, result);

                            }
                        }
                        
                    }
                }
                //if (isnum > 0)
                //{

                //}
                #endregion
                #region 注释掉处理确认操作
                //var vList = _dataItemBLL.GetVDataDictionaryModelList();
                ////将原有的工单的工艺路线、BOM、物料属性删除
                //var pl_Process = (from t in entity_list
                //                  join p in pL_Process_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                  on new { t.SendFactoryCode, t.WorkOrder } equals new { SendFactoryCode = p.FactoryCode, p.WorkOrder }
                //                  select p
                //                ).ToList();
                ////查找工艺路线
                //var pl_processOperations = (from t in pl_Process
                //                            join o in pL_ProcessOfOperations_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                            on t.Id equals o.ProcessId
                //                            select o
                //                          ).ToList();
                ////查找工艺路线属性
                //var pl_processAttr = (from t in pl_Process
                //                      join a in pL_ProcessOfOperationsAttr_Service.Get_ExpressionList(t => t.IsEnabled == true)
                //                      on t.Id equals a.ProcessId
                //                      select a
                //                    ).ToList();
                ////查找BOM
                //var pl_bom = (from t in entity_list
                //              join b in pL_BOM_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //              on new { t.SendFactoryCode, t.WorkOrder } equals new { SendFactoryCode = b.FactoryCode, b.WorkOrder }
                //              select b
                //            ).ToList();
                ////查找BOM明细
                //var pl_bomitem = (from t in pl_bom
                //                  join m in pL_BOMItems_Service.Get_ExpressionList(t => t.Id != null)
                //                  on t.Id equals m.BOMId
                //                  select m
                //                  ).ToList();
                ////查找工单物料
                //var pl_material = (from t in entity_list
                //                   join m in pL_Material_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                   on new { t.SendFactoryCode, t.WorkOrder } equals new { SendFactoryCode = m.FactoryCode, m.WorkOrder }
                //                   select m
                //                   ).ToList();
                //var pl_materialFact = (from m in pl_material
                //                       join f in pL_MaterialFacet.Get_ExpressionList(t => t.Id != null)
                //                       on m.Id equals f.MaterialId
                //                       select f
                //                       ).ToList();
                ////将调拨记录状态进行修改
                //foreach (var item in entity_list)
                //{

                //    item.TransStateCode = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemValue == "1")?.ItemValue;
                //    item.TransStateName = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemValue == "1")?.ItemName;
                //    item.ModifyTime = DateTime.Now;
                //    item.ModifyBy = userCode;
                //    item.ModifyName = userName;

                //}
                ////将工单的工厂换为接收工厂
                //var workList = (from t in entity_list
                //                join w in pL_WorkOrder_Service.Get_ExpressionList(t => t.IsEnabled == true)
                //                on new { t.SendFactoryCode, t.WorkOrder } equals new { SendFactoryCode = w.FactoryCode, w.WorkOrder }
                //                select w
                //                ).ToList();
                //foreach (var item in workList)
                //{
                //    item.FactoryCode = entity_list[0].AcceptFactoryCode;
                //    item.FactoryName = entity_list[0].AcceptFactoryName;
                //}
                //var sn = processEntities.SN;
                ////将现有工艺路线前段工艺路线删除
                //var pl_ProcessNew = (from t in entity_list
                //                  join p in pL_Process_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                  on new { t.AcceptFactoryCode, t.WorkOrder } equals new { AcceptFactoryCode = p.FactoryCode, p.WorkOrder }
                //                  select p
                //               ).ToList();
                ////查找工艺路线
                //var pl_processOperationsNew = (from t in pl_ProcessNew
                //                               join o in pL_ProcessOfOperations_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                            on t.Id equals o.ProcessId
                //                            where o.SN< sn
                //                               select o
                //                          ).ToList();
                ////查找工艺路线属性
                //var pl_processAttrNew = (from t in pl_ProcessNew
                //                         join a in pL_ProcessOfOperationsAttr_Service.Get_ExpressionList(t => t.IsEnabled == true)
                //                      on t.Id equals a.ProcessId
                //                      select a
                //                    ).ToList();

                //using (var ts = new TransactionScope())
                //{
                //    //删除工艺路线
                //    if (pl_Process.Any())
                //    {
                //        var delprocess = pL_Process_Service.Delete(pl_Process);
                //    }

                //    //删除工艺路线明细
                //    if (pl_processOperations.Any())
                //    {
                //        var delprocessOp = pL_ProcessOfOperations_Service.Delete(pl_processOperations);
                //    }

                //    //删除工艺路线属性
                //    if (pl_processAttr.Any())
                //    {
                //        var delprocessAttr = pL_ProcessOfOperationsAttr_Service.Delete(pl_processAttr);
                //    }

                //    //删除工单BOM
                //    if (pl_bom.Any())
                //    {
                //        var delBom = pL_BOM_Service.Delete(pl_bom);
                //    }

                //    //删除工单BOM明细
                //    if (pl_bomitem.Any())
                //    {
                //        var delBOMitem = pL_BOMItems_Service.Delete(pl_bomitem);
                //    }
                //    //删除工单物料
                //    var delma = pL_Material_Service.Delete(pl_material);
                //    //删除工单物料属性
                //    var delMafc = pL_MaterialFacet.Delete(pl_materialFact);
                //    //更新工单工厂
                //    var updatework = pL_WorkOrder_Service.SaveEntity_List(true, userName, workList, out msg);
                //    //更新调拨记录
                //   var updateTraReocd = _Service.ConTransferRecode(entity_list, out msg);

                //    //删除接收工厂工艺路线明细
                //    var delprocessOpNew = pL_ProcessOfOperations_Service.Delete(pl_processOperationsNew);
                //    //删除工艺路线属性
                //    var delprocessAttrNew = pL_ProcessOfOperationsAttr_Service.Delete(pl_processAttrNew);
                //    if (updateTraReocd > 0)
                //    {
                //        ts.Complete();
                //    }
                //    else
                //    {
                //        ts.Dispose();
                //        result.resultData = null;
                //        result.success = false;
                //        result.returnMsg = "执行失败" + msg;
                //        return Request.CreateResponse(HttpStatusCode.OK, result);
                //    }
                //}
                #endregion
                var Resultmsg = "";
                Resultmsg = _Service.PL_ConfirmTransfersRecord(userCode, entity_list, processEntities.SN, processEntities.OperationCode);

                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? Language.GetText("SAP.PL_TransfersRecordController.Tips_16")  : Resultmsg;//确认操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
                //result.resultData = null;
                //result.success = true;
                //result.returnMsg = "执行成功";
                //return Request.CreateResponse(HttpStatusCode.OK, result);
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
        /// 调拨工单确认全工序调拨
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Allconfirm_TransfersRecord")]
        public HttpResponseMessage Allconfirm_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            var msg = "";
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "Entity"));
                #region 注释掉
                ////获取数据字典
                //var vList = _dataItemBLL.GetVDataDictionaryModelList();
                ////将原有的工单的工艺路线、BOM、物料属性删除
                //var pl_Process = (from t in entity_list
                //                  join p in  pL_Process_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                  on new { t.SendFactoryCode ,t.WorkOrder } equals new { SendFactoryCode=p.FactoryCode,p.WorkOrder}
                //                  select p
                //                ).ToList();
                ////查找工艺路线
                //var pl_processOperations = (from t in pl_Process
                //                            join o in pL_ProcessOfOperations_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //                            on t.Id equals o.ProcessId
                //                            select o
                //                          ).ToList();
                ////查找工艺路线属性
                //var pl_processAttr = (from t in pl_Process
                //                      join a in pL_ProcessOfOperationsAttr_Service.Get_ExpressionList(t => t.IsEnabled == true)
                //                      on t.Id equals a.ProcessId
                //                      select a
                //                    ).ToList();
                ////查找BOM
                //var pl_bom = (from t in entity_list
                //              join b in pL_BOM_Service.Get_ExpressionList(t => t.IsDeleted == false)
                //              on new { t.SendFactoryCode, t.WorkOrder } equals new { SendFactoryCode = b.FactoryCode, b.WorkOrder }
                //              select b
                //            ).ToList();
                ////查找BOM明细
                //var pl_bomitem = (from t in pl_bom
                //                  join m in pL_BOMItems_Service.Get_ExpressionList(t=>t.Id !=null)
                //                  on t.Id equals m.BOMId
                //                  select m
                //                  ).ToList();
                ////查找工单物料
                //var pl_material = (from t in entity_list
                //                   join m in pL_Material_Service.Get_ExpressionList(t=>t.IsDeleted==false)
                //                   on new { t.SendFactoryCode,t.WorkOrder} equals new { SendFactoryCode=m.FactoryCode,m.WorkOrder }
                //                   select m
                //                   ).ToList();
                //var pl_materialFact = (from m in pl_material
                //                       join f in pL_MaterialFacet.Get_ExpressionList(t=>t.Id !=null)
                //                       on m.Id equals f.MaterialId
                //                       select f
                //                       ).ToList();
                ////将调拨记录状态进行修改
                //foreach (var item in entity_list)
                //{

                //    item.TransStateCode = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemValue == "1")?.ItemValue;
                //    item.TransStateName = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemValue == "1")?.ItemName;
                //    item.ModifyTime = DateTime.Now;
                //    item.ModifyBy = userCode;
                //    item.ModifyName = userName;

                //}
                ////将工单的工厂换为接收工厂
                //var workList = (from t in entity_list
                //                join w in pL_WorkOrder_Service.Get_ExpressionList(t=>t.IsEnabled==true).ToList()
                //                on new { t.SendFactoryCode,t.WorkOrder} equals new { SendFactoryCode=w.FactoryCode,w.WorkOrder }
                //                select w
                //                ).ToList(); 
                //foreach(var item in workList)
                //{
                //    item.FactoryCode = entity_list[0].AcceptFactoryCode;
                //    item.FactoryName = entity_list[0].AcceptFactoryName;
                //}
                //using (var ts = new TransactionScope())
                //{
                //    //删除工艺路线
                //    if (pl_Process.Any())
                //    {
                //        var delprocess = pL_Process_Service.Delete(pl_Process);
                //    }

                //    //删除工艺路线明细
                //    if (pl_processOperations.Any())
                //    {
                //        var delprocessOp = pL_ProcessOfOperations_Service.Delete(pl_processOperations);
                //    }

                //    //删除工艺路线属性
                //    if (pl_processAttr.Any())
                //    {
                //        var delprocessAttr = pL_ProcessOfOperationsAttr_Service.Delete(pl_processAttr);
                //    }

                //    //删除工单BOM
                //    if (pl_bom.Any())
                //    {
                //        var delBom = pL_BOM_Service.Delete(pl_bom);
                //    }

                //    //删除工单BOM明细
                //    if (pl_bomitem.Any())
                //    {
                //        var delBOMitem = pL_BOMItems_Service.Delete(pl_bomitem);
                //    }

                //    //删除工单物料
                //    var delma = pL_Material_Service.Delete(pl_material);
                //    //删除工单物料属性
                //    var delMafc = pL_MaterialFacet.Delete(pl_materialFact);
                //    //更新工单工厂
                //    var updatework = pL_WorkOrder_Service.SaveEntity_List(true,userName, workList,out msg);
                //    //更新调拨记录
                //    var updateTraReocd= _Service.ConTransferRecode(entity_list, out msg);
                //    if (updateTraReocd > 0)
                //    {
                //        ts.Complete();
                //    }
                //    else
                //    {
                //        ts.Dispose();
                //        result.resultData = null;
                //        result.success = false;
                //        result.returnMsg = "执行失败" + msg;
                //        return Request.CreateResponse(HttpStatusCode.OK, result);
                //    }
                //}
                #endregion

                var Resultmsg = "";
                Resultmsg = _Service.PL_AllConfirmTransfersRecord(userCode, entity_list);

                result.success = string.IsNullOrEmpty(Resultmsg) ? true : false;
                result.returnMsg = string.IsNullOrEmpty(Resultmsg) ? Language.GetText("SAP.PL_TransfersRecordController.Tips_17") : Resultmsg;//确认操作成功
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
        /// 工单调拨回退操作
        /// jpf 2022-12-2 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("FallbackPL_TransfersRecord")]
        public HttpResponseMessage FallbackPL_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                //业务服务类
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();

                //发起工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "Entity"));
                //获取登录人员的工厂权限
                var PeoFactoryCode = new BS_People_Service().Get_ExpressionEntity(t => t.Code == userCode).FactoryCode;
                foreach (var Item in entity_list)
                {
                    if (!PeoFactoryCode.Contains(Item.SendFactoryCode))
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_18");//账号权限工厂与发起工厂不一致，不允许回退
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                //查找工单下的工艺路线、工序属性
                var plprocess = (from t in entity_list
                                 join p in _plProcessService.Get_ExpressionList(t => t.IsDeleted == false).ToList() on t.WorkOrder equals p.WorkOrder
                                 join m in  _plProcessOfOperationsService.Get_ExpressionList(t => t.IsDeleted == true).ToList()
                                 on p.Id equals m.ProcessId
                                 select m).ToList();

                var plprocessArrt = (from t in plprocess
                                     join a in _plProcessOfOperationsAttrService.Get_ExpressionList(t => t.IsEnabled == true).ToList()
                                     on t.Id equals a.OperationsId
                                     select a
                                  ).ToList();
                using (var ts = new TransactionScope())
                {

                    string msg = "";
                    //将工艺路线删除
                    var delOpera = new PL_ProcessOfOperations_Service().newRemoveForm(false, userCode,plprocess, "");
                    var delArrt = new PL_ProcessOfOperationsAttr_Service().newRemoveForm(false, userCode,plprocessArrt, "");

                    int isok = _Service.RebackTransferRecode( userCode,userName,"2",Language.GetText("SAP.PL_TransfersRecordController.Tips_19"), entity_list, out msg);//已回退

                    if (isok > 0)
                    {
                        ts.Complete();
                    }
                    else
                    {
                        ts.Dispose();
                        result.resultData = null;
                        result.success = false;
                        result.returnMsg = Language.GetText("Common.ExecutionError") + msg;//执行失败
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePL_TransfersRecord")]
        public HttpResponseMessage SavePL_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var msg="";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }           
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            
            
            try
            {
                //业务服务类
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();

                //发起工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                List<PL_TransfersRecordEntity> entity_list = JsonConvert.DeserializeObject<List<PL_TransfersRecordEntity>>(getValue(jo, "Entity"));

                var TransTypeCode = entity_list[0].TransTypeCode;

                //获取数据字典
                var vList = _dataItemBLL.GetVDataDictionaryModelList();
                var AllPL_TransfersRecord = _Service.GetList_TransfersRecord(entity_list,out msg);
                if (AllPL_TransfersRecord.Any())
                {
                    
                        result.success = false;
                        result.returnMsg = string.Join(",", AllPL_TransfersRecord.ToList().Select(t => t.WorkOrder)) + Language.GetText("SAP.PL_TransfersRecordController.Tips_21");//数据重复已经调拨过不允许重复
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    
                }
               
                foreach (var item in entity_list)
                {
                    var WorkOrder = item.WorkOrder;
                    var plwork = _plWorkOrderService.Get_ExpressionEntity(t => (t.BatchStatus != 0 && t.BatchStatus != null) && t.WorkOrder == WorkOrder);
                    if (!plwork.IsEmpty())
                    {
                        result.resultData = null;
                        result.success = false;
                        result.returnMsg = "执行失败" + WorkOrder + Language.GetText("SAP.PL_TransfersRecordController.Tips_22");//工单已合批不允许调拨
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                    foreach (var item in entity_list)
                {
                    
                    item.Id = Guid.NewGuid().ToString();
                    item.TransStateCode = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemName == Language.GetText("SAP.PL_TransfersRecordController.Tips_23"))?.ItemValue;//待接收
                    item.TransStateName = vList.Find(t => t.EnCode == "TransferStatus" && t.ItemName == Language.GetText("SAP.PL_TransfersRecordController.Tips_24"))?.ItemName;//待接收
                    item.CreateTime= DateTime.Now;
                    item.IsDelete = false;
                }
                var plprocess = new List<PL_ProcessOfOperationsEntity>();
                var plprocessArrt = new List<PL_ProcessOfOperationsAttrEntity>();
              //查询工单工艺路线
              if (TransTypeCode== "AllProcessTransfer")
                {
                     plprocess = (from t in entity_list
                                    join p in new PL_Process_Service().Get_ExpressionList(t => t.IsDeleted == false) on t.WorkOrder equals p.WorkOrder
                                    join m in new PL_ProcessOfOperations_Service().Get_ExpressionList(t => t.IsDeleted == false)
                                    on p.Id equals m.ProcessId
                                    select m ).ToList();

                    plprocessArrt = (from t in plprocess
                                     join a in new PL_ProcessOfOperationsAttr_Service().Get_ExpressionList(t => t.IsEnabled == true)
                                     on t.Id equals a.OperationsId
                                     select a
                                   ).ToList();
                }
                else
                {


                    plprocess = (from t in entity_list
                                 join s in new PL_Process_Service().Get_ExpressionList(t => t.IsDeleted == false) on t.WorkOrder equals s.WorkOrder
                                 join o in new PL_ProcessOfOperations_Service().Get_ExpressionList(t => t.IsDeleted == false)
                                 on s.Id equals o.ProcessId
                                 where o.OperationCode == t.TransProcessCode
                                 join m in new PL_ProcessOfOperations_Service().Get_ExpressionList(t => t.IsDeleted == false)
                                 on s.Id equals m.ProcessId
                                 where m.SN > o.SN
                                 select m).ToList();
                    plprocessArrt = (from t in plprocess
                                     join a in new PL_ProcessOfOperationsAttr_Service().Get_ExpressionList(t => t.IsEnabled == true)
                                     on t.Id equals a.OperationsId
                                     select a
                                   ).ToList();
                }
                using (var ts = new TransactionScope())
                {

                    //string msg = "";
                    //将工艺路线删除
                   var delOpera = new PL_ProcessOfOperations_Service().newRemoveForm(true, userCode,plprocess, "");
                    var delArrt = new PL_ProcessOfOperationsAttr_Service().newRemoveForm(true, userCode,plprocessArrt, "");

                    int isok = _Service.SaveEntity_List(false,"", entity_list,  out msg);

                    if (isok>0)
                    {
                        ts.Complete();
                    }
                    else
                    {
                        ts.Dispose();
                        result.resultData = null;
                        result.success = false;
                        result.returnMsg = Language.GetText("Common.ExecutionError")+msg;//执行失败
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                result.resultData = null;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPL_TransfersRecord")]
        public HttpResponseMessage SaveBatchPL_TransfersRecord(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_26");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_27");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<PL_TransfersRecordEntity> Insert_entity_list = new List<PL_TransfersRecordEntity>();
                //更新数组
                List<PL_TransfersRecordEntity> Update_entity_list = new List<PL_TransfersRecordEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PL_TransfersRecordEntity entity = new PL_TransfersRecordEntity();
                        //发起工厂编码
                        entity.SendFactoryCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_28")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_28")];//发起工厂编码
                        //发起工厂名称
                        entity.SendFactoryName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_29")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_29")];//发起工厂名称
                        //接收工厂编码
                        entity.AcceptFactoryCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_30")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_30")];//接收工厂编码
                        //接收工厂名称
                        entity.AcceptFactoryName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_31")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_31")];//接收工厂名称
                        //订单编码
                        entity.ProductOrder =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_32")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_32")];//订单编码
                        //工单号
                        entity.WorkOrder =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_33")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_33")];//工单号
                        //物料编码
                        entity.MaterialCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_34")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_34")];//物料编码
                        //调拨类型:全工序调拨、部分工序调拨数据字典取值
                        entity.TransTypeCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_35")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_35")];//调拨类型:全工序调拨、部分工序调拨数据字典取值
                        //调拨类型:全工序调拨、部分工序调拨数据字典取值
                        entity.TransTypeName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_36")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_36")];//调拨类型:全工序调拨、部分工序调拨数据字典取值
                        //调拨工序编码
                        entity.TransProcessCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_37")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_37")];//调拨工序编码
                        //调拨工序名称
                        entity.TransProcessName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_38")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_38")];//调拨工序名称
                        //调拨状态：0待确认、1已确认、2已回退
                        entity.TransStateCode =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_39")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_39")];//调拨状态：0待确认、1已确认、2已回退
                        //调拨状态：0待确认、1已确认、2已回退
                        entity.TransStateName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_40")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_40")];//调拨状态：0待确认、1已确认、2已回退
                        //备注
                        entity.Remark =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_41")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_41")];//备注
                        //删除标识1代表已删除0代表未删除
                        entity.IsDelete =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_42")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_42")];//删除标识1代表已删除0代表未删除
                        //创建人
                        entity.Creator =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_43")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_43")];//创建人
                        //创建时间
                        entity.CreateTime = item[Language.GetText("SAP.PL_TransfersRecordController.Tips_44")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_44")];//创建时间
                        //创建人名称
                        entity.CreateName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_45")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_45")];//创建人名称
                        //最后修改人
                        entity.ModifyBy =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_46")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_46")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[Language.GetText("SAP.PL_TransfersRecordController.Tips_47")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_47")];//最后修改时间
                        //修改人名称
                        entity.ModifyName =  item[Language.GetText("SAP.PL_TransfersRecordController.Tips_48")] == null ? "" : item[Language.GetText("SAP.PL_TransfersRecordController.Tips_48")];//修改人名称
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        ////是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PL_TransfersRecordEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePL_TransfersRecord")]
        public HttpResponseMessage DeletePL_TransfersRecord(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                PL_TransfersRecordEntity entity = JsonConvert.DeserializeObject<PL_TransfersRecordEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_TransfersRecordEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_52");//删除操作成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePL_TransfersRecord")]
        public HttpResponseMessage RemovePL_TransfersRecord(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                PL_TransfersRecordEntity entity = JsonConvert.DeserializeObject<PL_TransfersRecordEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PL_TransfersRecordEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, entity.ModifyName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_52");//删除操作成功
                else
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_54");//删除操作失败
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_TransfersRecordEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_55");//获取详情数据成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PL_TransfersRecordEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_55");//获取详情数据成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                string OutMes = "";
                var list = _Service.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                string OutMes = "";
                var list = _Service.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-16 20:17:50
        /// 任务编号: 跨工厂调拨
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PL_TransfersRecord_export")]
        public HttpResponseMessage PL_TransfersRecord_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("SAP.PL_TransfersRecordController.Tips_6");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                PL_TransfersRecord_Service _Service = new PL_TransfersRecord_Service();
                string msg = Language.GetText("Common.SearchSuccess");//查询成功
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
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        
    }
}
