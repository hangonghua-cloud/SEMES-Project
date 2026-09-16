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
using ALP.Application.Entity.EquipmentManage;

namespace ALP.Application.WebApi.Controllers.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_ProductionFirstInspectionController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 生产首检
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_ProductionFirstInspectionDetail")]
    public class PM_ProductionFirstInspectionDetailController : ApiBaseController
    {
        private PM_ProductionFirstInspectionDetailBLL _ProductionFirstInspectionDetailBLL = new PM_ProductionFirstInspectionDetailBLL();
        private PM_ProductionFirstInspectionBLL _ProductionFirstInspectionBLL = new PM_ProductionFirstInspectionBLL();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ProductionFirstInspectionDetailPageList")]
        public HttpResponseMessage PM_ProductionFirstInspectionDetailPageList(JObject jo)
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
                
                var data = _ProductionFirstInspectionDetailBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_ProductionFirstInspectionDetailPageDataTableList")]
        public HttpResponseMessage PM_ProductionFirstInspectionDetailPageDataTableList(JObject jo)
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
                
                var data = _ProductionFirstInspectionDetailBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_ProductionFirstInspectionDetailList")]
        public HttpResponseMessage GetPM_ProductionFirstInspectionDetailList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                
                string msg = "";
                var list = _ProductionFirstInspectionDetailBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_ProductionFirstInspectionDetail")]
        public HttpResponseMessage SavePM_ProductionFirstInspectionDetail(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            try
            {
 
                PM_ProductionFirstInspectionDetailEntity entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionDetailEntity>(getValue(jo, "Entity"));
 
                string msg = "";

                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }
                int isok = _ProductionFirstInspectionDetailBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 功能描述: 保存表单（新增、修改）实验室保存
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_ProductionFirstInspectionDetail1")]
        public HttpResponseMessage SavePM_ProductionFirstInspectionDetail1(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");


            try
            {

                var entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PM_ProductionFirstInspectionDetailEntity>>(getValue(jo, "data"));
                //PM_ProductionFirstInspectionDetailEntity entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionDetailEntity>(getValue(jo, "Entity"));
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;
               
                entity.LaboratoryTestStatus = "3";

                foreach (var item in list)
                {
                    item.Create();
                    item.FirstInspectionId = keyValue;
                    item.Creator = userCode;
                    item.CreateTime = entity.ModifyTime;
                    item.EnabledMark = true;
                }

                string msg = "";
                _ProductionFirstInspectionDetailBLL.RemoveForm(t => t.FirstInspectionId == keyValue && t.TestDepartment == "1");
                _ProductionFirstInspectionBLL.SaveEntity(keyValue, entity, out msg);
                int isok = _ProductionFirstInspectionDetailBLL.SaveEntity_List(false, userCode, list, out msg);

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
        /// 保存-非实验室
        /// </summary>
        /// <param name="mainEntity"></param>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_ProductionFirstInspectionDetail2")]
        public HttpResponseMessage SaveQC_PollingDetailResults(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("List") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_10");//缺少List参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }



            try
            {
                var entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<PM_ProductionFirstInspectionDetailEntity>>(getValue(jo, "List"));
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;
                entity.Creator = userCode;
                entity.CreateTime = DateTime.Now;

                foreach (var item in list)
                {
                    item.Create();
                    item.FirstInspectionId = keyValue;
                    item.Creator = userCode;
                    item.CreateTime = entity.ModifyTime;
                }

                string msg = "";
                int isok = 0;

                _ProductionFirstInspectionDetailBLL.RemoveForm(t => t.FirstInspectionId == keyValue && t.TestDepartment == "2");
                _ProductionFirstInspectionBLL.SaveEntity(keyValue, entity, out msg);
                isok = _ProductionFirstInspectionDetailBLL.SaveEntity_List(false, userCode, list, out msg);

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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_ProductionFirstInspectionDetail")]
        public HttpResponseMessage SaveBatchPM_ProductionFirstInspectionDetail(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_11");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_12");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _ProductionFirstInspectionDetailBLL.GetList("", out msg);
                //插入数组
                List<PM_ProductionFirstInspectionDetailEntity> Insert_entity_list = new List<PM_ProductionFirstInspectionDetailEntity>();
                //更新数组
                List<PM_ProductionFirstInspectionDetailEntity> Update_entity_list = new List<PM_ProductionFirstInspectionDetailEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_ProductionFirstInspectionDetailEntity entity = new PM_ProductionFirstInspectionDetailEntity();
                        //首检ID
                        entity.FirstInspectionId =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_13")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_13")];//首检ID
                        //首检项目编码
                        entity.TestItemCoading =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_14")];//首检项目编码
                        //首检项目名称
                        entity.TestItemName =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_15")];//首检项目名称
                        //检测类型
                        entity.DataType =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_16")];//检测类型
                        //检测类型名称
                        entity.DataTypeName =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_17")];//检测类型名称
                        //首检标准
                        entity.TestItemStandard =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_18")];//首检标准
                        //车间结果
                        entity.WorkShopResult =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_19")];//车间结果
                        //质量结果
                        entity.QualityResult =  item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_20")];//质量结果
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                       // entity.CreatedDateTime = DateTimeOffset.Now;
                        //是否删除
                       // entity.IsDeleted = false;
                       // entity.CreatedByCode = CreatedByCode;
                       // entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_ProductionFirstInspectionDetailEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = _ProductionFirstInspectionDetailBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _ProductionFirstInspectionDetailBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_ProductionFirstInspectionDetail")]
        public HttpResponseMessage DeletePM_ProductionFirstInspectionDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_ProductionFirstInspectionDetailEntity entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionDetailEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ProductionFirstInspectionDetailEntity model = _ProductionFirstInspectionDetailBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _ProductionFirstInspectionDetailBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_23");//删除操作成功
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_ProductionFirstInspectionDetail")]
        public HttpResponseMessage RemovePM_ProductionFirstInspectionDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                PM_ProductionFirstInspectionDetailEntity entity = JsonConvert.DeserializeObject<PM_ProductionFirstInspectionDetailEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_ProductionFirstInspectionDetailEntity model = _ProductionFirstInspectionDetailBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _ProductionFirstInspectionDetailBLL.RemoveForm(Id, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_23");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_25");//删除操作失败
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ProductionFirstInspectionDetailEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _ProductionFirstInspectionDetailBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_ProductionFirstInspectionDetailEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _ProductionFirstInspectionDetailBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_26");//获取详情数据成功
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                
                var data = _ProductionFirstInspectionDetailBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 ).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                var list = _ProductionFirstInspectionDetailBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                var list = _ProductionFirstInspectionDetailBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_ProductionFirstInspectionDetail_export")]
        public HttpResponseMessage PM_ProductionFirstInspectionDetail_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_ProductionFirstInspectionDetailController.Tips_7");//缺少Entity参数！
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
                var data = _ProductionFirstInspectionDetailBLL.GetList_export(CreatedByCode, out msg);
                
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
