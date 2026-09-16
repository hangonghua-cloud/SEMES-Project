using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Busines.QualityManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.IService.QualityManage;
using ALP.Application.Service.QualityManage;
using ALP.Application.Service.Material;
using ALP.Application.Entity.Material;
using ALP.Application.Busines.Material;

namespace ALP.Application.WebApi.Controllers.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-27
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_IQCQualityCheckListController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: IQC品质检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("QC_IQCQualityCheck")]
    public class QC_IQCQualityCheckController : ApiBaseController
    {
        private QC_IQCQualityCheck_IService service = new QC_IQCQualityCheck_Service();
        private QC_TestMethodMaterialIService _TestMethodMaterialService = new QC_TestMethodMaterial_Service();
        private QC_TestMethodMaintenance_IService _TestMethodMaintenanceService = new QC_TestMethodMaintenance_Service();
        private QC_TestMethodItemMaintenance_IService _TestMethodItemMaintenanceService = new QC_TestMethodItemMaintenance_Service();
        private Base_MaterialGroupBindMaterial_Service _MaterialGroupBindMaterialService = new Base_MaterialGroupBindMaterial_Service();
        private QC_IQCQualityCheck_BLL bll = new QC_IQCQualityCheck_BLL();
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_IQCQualityCheckListPageList")]
        public HttpResponseMessage QC_IQCQualityCheckListPageList(JObject jo)
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
                
                var data = bll.GetPageList(pagination, queryJson);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_IQCQualityCheckListPageDataTableList")]
        public HttpResponseMessage QC_IQCQualityCheckListPageDataTableList(JObject jo)
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
                string messageCode = "";
                string checkType = "";
                string msg;
                // var a = bll.GetTestMethod(out messageCode);

               // var list =new List<QC_IQCQualityCheckEntity>();

                //var list = bll.GetList1(checkType, out msg);
                //if (list.Count() > 0)
                //{

                //    var list1 = new List<Base_MaterialGroupBindMaterialEntity>();

                //    foreach (var item in list)
                //    {
                //        string a1 = item.MaterialCode;

                //        list1.AddRange(_MaterialGroupBindMaterialService.GetList(t => t.MaterialCode == a1).ToList());
                //    }


                //    //流水号
                //    //service.GetSerialNO("IQCQualityCheck", list.Count(), out returnNum, out msg);
                //    //var index = int.Parse(returnNum);
                //    //IQC检验 实验室检测
                //    var query = from main in _TestMethodMaintenanceService.Get_ExpressionList(t => t.TestType == "1")
                //                join item in _TestMethodItemMaintenanceService.Get_ExpressionList(t => true) on main.Id equals item.TestMethodId
                //                join material in _TestMethodMaterialService.Get_ExpressionList(t => true) on main.Id equals material.TestMethodId
                //                join gro in list1 on material.SmallClass equals gro.GroupCode
                //                join iqc in list on gro.MaterialCode equals iqc.MaterialCode
                //                join mf in _MaterialFactoryBLL.Get_ExpressionList(t=>true) on iqc.MaterialCode equals mf.MaterialCode
                //                select new
                //                {
                //                    main.Id,
                //                    item.TestDepartment,
                //                    iqc.MaterialCode,
                //                    iqc.MaterialName,
                //                    gro.GroupCode,
                //                    mf.IsExemption
                //                };

                //    var mList = query.ToList();

                //    //   if (mList.Count < 1) return;

                //    /**
                //     * 实验室状态 1：不需要；2：待检验；3：检验完成
                //     * 质检状态 1：待检验，2：检验完成
                //     * */


                //    foreach (var item in list)
                //    {
                //        string msg1 = "";

                //        var ent = mList.Find(t => t.MaterialCode == item.MaterialCode);
                //        // if (ent == null) continue;
                //        if (ent == null)
                //        {
                //            QC_IQCQualityCheckEntity entity = service.GetEntity(item.Id);
                //            entity.TestMethodIdId = "";
                //            entity.LabStatus = "";
                //            entity.QualityStatus = "1";
                //            service.SaveEntity(entity.Id, entity, out msg1);
                //        }
                //        else
                //        {
                //            var labStatus = mList.Find(t => t.MaterialCode == item.MaterialCode && t.TestDepartment == "1") == null ? "1" : "2";
                //            var QualityStatus = "1";
                //            if (ent.IsExemption == "1")//免检
                //            {
                //                labStatus = "1";//无需检验
                //                QualityStatus = "2";//检验完成
                //            }
                //            QC_IQCQualityCheckEntity entity = service.GetEntity(item.Id);
                //            entity.TestMethodIdId = ent.Id;
                //            entity.LabStatus = labStatus;
                //            entity.QualityStatus = QualityStatus;
                //            entity.TestResult = QualityStatus == "2" ? "1" : null;// TestResult=1 合格
                //            service.SaveEntity(entity.Id, entity, out msg1);
                //        }

                //    }
                //}
                
                var data = bll.GetPageDataTableList(pagination, queryJson);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetQC_IQCQualityCheckListList")]
        public HttpResponseMessage GetQC_IQCQualityCheckListList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var list = bll.GetList(checkType, out msg);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQC_IQCQualityCheckList")]
        public HttpResponseMessage SaveQC_IQCQualityCheckList(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_7");//缺少Entity参数！
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
                QC_IQCQualityCheckEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckEntity>(getValue(jo, "Entity"));
                
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_8");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_9");//编辑人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    // = DateTime.Parse(entity.CreatedDateTime.ToString());
                }
                else
                {
                    
                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_10");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: QC_IQCQualityCheckList	ReceivingNotificationLineNumber流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("QC_IQCQualityCheckList", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.ReceivingNotificationLineNumber = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.InspectionTime = DateTime.Now;
                    //有效标志
                    //entity.EnabledMark = false;
                    //检验时间
                    //entity.InspectionTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.EnabledMark = false;
                }
                
                string msg = "";
                int isok = bll.SaveEntity(keyValue, entity, out msg);
                /*result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;*///操作成功
                switch (isok)
                {
                    case 1:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_12");//添加成功
                        break;
                    case 2:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_13");//修改成功
                        break;
                    case 3:
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_14");//添加失败，检验单号重复
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchQC_IQCQualityCheckList")]
        public HttpResponseMessage SaveBatchQC_IQCQualityCheckList(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_16");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_17");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = bll.GetList("", out msg);
                //插入数组
                List<QC_IQCQualityCheckEntity> Insert_entity_list = new List<QC_IQCQualityCheckEntity>();
                //更新数组
                List<QC_IQCQualityCheckEntity> Update_entity_list = new List<QC_IQCQualityCheckEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        QC_IQCQualityCheckEntity entity = new QC_IQCQualityCheckEntity();
                        
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_18")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_19")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        QC_IQCQualityCheckEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = bll.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = bll.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteQC_IQCQualityCheckList")]
        public HttpResponseMessage DeleteQC_IQCQualityCheckList(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IQCQualityCheckEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IQCQualityCheckEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = bll.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_23");//删除操作成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveQC_IQCQualityCheckList")]
        public HttpResponseMessage RemoveQC_IQCQualityCheckList(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_IQCQualityCheckEntity entity = JsonConvert.DeserializeObject<QC_IQCQualityCheckEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_IQCQualityCheckEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = bll.RemoveForm(Id, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_23");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_25");//删除操作失败
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_IQCQualityCheckListEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                var data = bll.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_26");//获取详情数据成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_IQCQualityCheckListEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                var data = bll.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_26");//获取详情数据成功
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
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
                var data = bll.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.IsEnabled == true).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
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
                var list = bll.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
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
                var list = bll.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("QC_IQCQualityCheckList_export")]
        public HttpResponseMessage QC_IQCQualityCheckList_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_IQCQualityCheckController.Tips_7");//缺少Entity参数！
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
                var data = bll.GetList_export(CreatedByCode, out msg);
                
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
