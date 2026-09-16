using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.Material;
using ALP.Application.Service.Material;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.Material;
using System.Transactions;
using ALP.Application.Service.Resources;
using ALP.Application.Code.Model;
using ALP.Application.UtilExtend.Util;

namespace ALP.Application.WebApi.Controllers.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_ProcessController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("BS_Process")]
    public class BS_ProcessController : ApiBaseController
    {
        private BS_ProcessOfOperationsBLL _ProcessOfOperationsBLL = new BS_ProcessOfOperationsBLL();
        private BS_Process_Service _bsProcessService = new BS_Process_Service();//基础数据工艺路线service层
        private BS_ProcessOfOperationsAttr_Service _attrService = new BS_ProcessOfOperationsAttr_Service();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("Material.BS_ProcessController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_ProcessPageList")]
        public HttpResponseMessage BS_ProcessPageList(JObject jo)
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

                var data = _bsProcessService.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Count(),
                    page = pagination != null ? pagination.total : data.Count(),
                    records = pagination != null ? pagination.total : data.Count(),
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
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 工艺路线管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_ProcessPageDataTableList")]
        public HttpResponseMessage BS_ProcessPageDataTableList(JObject jo)
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

                var data = _bsProcessService.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }



        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBS_ProcessList")]
        public HttpResponseMessage GetBS_ProcessList(string checkType, string Name = "")
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _bsProcessService.GetList(checkType, Name, out msg);
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
        /// 功能描述：复制工艺路线工序属性
        /// 创建：jpf2022-11-15 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>'
        [HttpPost]
        [Route("CopyBs_Process")]
        public HttpResponseMessage CopyBs_Process(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var msg = "";
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_8");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string ProcessCode = getValue(jo, "KeyValue");

            try
            {
                //校验当前选择的工艺是否有工序等数据
                var ProcessOfOperationsEntityList = new BS_ProcessOfOperations_Service().GetProcessOfOperationsEntityList(ProcessCode);
                if (ProcessOfOperationsEntityList.Count > 0)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_10");//已存在工序数据,无法粘贴
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //获取工艺路线工厂信息
                var ProcessEntiy = _bsProcessService.Get_ExpressionEntity(t => t.ProcessCode == ProcessCode);
                List<BS_ProcessOfOperationsEntity> BS_ProcessOfOperationsEntityList = JsonConvert.DeserializeObject<List<BS_ProcessOfOperationsEntity>>(getValue(jo, "Entity"));
                List<BS_ProcessOfOperationsAttrEntity> BS_ProcessOfOperationsAttrEntityList = new List<BS_ProcessOfOperationsAttrEntity>();
                foreach (var item in BS_ProcessOfOperationsEntityList)
                {
                    //获取工序下的属性
                    var ProcessAttrList = new BS_ProcessOfOperationsAttr_Service().Get_ExpressionList(t => t.OperationsId == item.Id);

                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = ProcessEntiy.FactoryCode;
                    item.FactoryName = ProcessEntiy.FactoryName;
                    item.ProcessCode = ProcessCode;
                    item.CreateTime = DateTime.Now;
                    item.Creator = userCode;

                    //var enTityList=  ProcessAttrList.Select(a=> { a.OperationsId = item.Id ; return a; }).ToList();
                    var enTityList = ProcessAttrList.Select(a => { a.OperationsId = item.Id; a.Id = Guid.NewGuid().ToString(); a.SortCode = 100; return a; }).ToList();
                    BS_ProcessOfOperationsAttrEntityList.AddRange(enTityList);
                }
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 1, 100);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    if (BS_ProcessOfOperationsEntityList.Count > 0)
                    {
                        new BS_ProcessOfOperations_Service().CopyEntity_List(BS_ProcessOfOperationsEntityList, out msg);
                    }
                    if (BS_ProcessOfOperationsAttrEntityList.Count > 0)
                    {
                        new BS_ProcessOfOperationsAttr_Service().CopyEntity_List(BS_ProcessOfOperationsAttrEntityList, out msg);
                    }
                    ts.Complete();
                }
                //if (BS_ProcessOfOperationsEntityList.Count > 0)
                //{
                //    new BS_ProcessOfOperations_Service().CopyEntity_List(BS_ProcessOfOperationsEntityList, out msg);
                //}
                //if (BS_ProcessOfOperationsAttrEntityList.Count > 0)
                //{
                //    new BS_ProcessOfOperationsAttr_Service().CopyEntity_List(BS_ProcessOfOperationsAttrEntityList, out msg);
                //}

                result.success = true;
                result.resultData = null;
                result.returnMsg = "操作成功";
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBS_Process")]
        public HttpResponseMessage SaveBS_Process(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_8");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            string queryJson = getValue(jo, "Entity");
            try
            {
                //业务服务类

                //参数转实体
                BS_ProcessEntity entity = JsonConvert.DeserializeObject<BS_ProcessEntity>(getValue(jo, "Entity"));

                //1.查询是否存在相同的编码和名称
                var ent = _bsProcessService.Get_ExpressionEntity(t => t.ProcessCode == entity.ProcessCode || t.ProcessName == entity.ProcessName);
                if (ent != null && string.IsNullOrEmpty(keyValue))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_12");//存在相同编码或者名称
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var ent1 = _bsProcessService.Get_ExpressionEntity(t => t.Id != keyValue && t.ProcessName == entity.ProcessName);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_13");//存在相同名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }
                if (entity.ProcessType == "2" && entity.IsDefault == true)
                {
                    var ProssEntityList = _bsProcessService.Get_ExpressionList(t => t.SmallClass == entity.SmallClass
                        && t.FactoryCode == entity.FactoryCode && t.IsDefault == true && t.Id != keyValue).ToList();
                    if (ProssEntityList.Count > 0)
                    {
                        result.success = false;
                        result.returnMsg = entity.SmallClass + Language.GetText("Material.BS_ProcessController.Tips_14");//特征下存在默认工艺路线不允许有多个默认
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;

                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    entity.CreateTime = DateTime.Parse(entity.CreateTime.ToString());
                }
                else
                {
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }



                string msg = "";
                int isok = _bsProcessService.SaveEntity(keyValue, entity, out msg);
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBS_Process")]
        public HttpResponseMessage SaveBatchBS_Process(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_16");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_17");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
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
                var old_entity_list = _bsProcessService.GetList("", "", out msg);
                //插入数组
                List<BS_ProcessEntity> Insert_entity_list = new List<BS_ProcessEntity>();
                //更新数组
                List<BS_ProcessEntity> Update_entity_list = new List<BS_ProcessEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        BS_ProcessEntity entity = new BS_ProcessEntity();
                        //工厂编码
                        entity.FactoryCode = item[Language.GetText("Material.BS_ProcessController.Tips_18")] == null ? "" : item[Language.GetText("Material.BS_ProcessController.Tips_18")];//工厂编码
                        //工艺编码
                        entity.ProcessCode = item[Language.GetText("Material.BS_ProcessController.Tips_19")] == null ? "" : item[Language.GetText("Material.BS_ProcessController.Tips_19")];//工艺编码
                        //工艺名称
                        entity.ProcessName = item[Language.GetText("Material.BS_ProcessController.Tips_20")] == null ? "" : item[Language.GetText("Material.BS_ProcessController.Tips_20")];//工艺名称
                        //物料分类
                        entity.MaterialClass = item[Language.GetText("Material.BS_ProcessController.Tips_21")] == null ? "" : item[Language.GetText("Material.BS_ProcessController.Tips_21")];//物料分类
                        //
                        entity.SmallClass = item[""] == null ? "" : item[""];
                        //备注
                        entity.Remark = item[Language.GetText("Material.BS_ProcessController.Tips_22")] == null ? "" : item[Language.GetText("Material.BS_ProcessController.Tips_22")];//备注
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTimeOffset.Now;
                        //是否删除
                        //entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        // entity.CreatedByName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        BS_ProcessEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _bsProcessService.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _bsProcessService.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBS_Process")]
        public HttpResponseMessage DeleteBS_Process(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                BS_ProcessEntity entity = JsonConvert.DeserializeObject<BS_ProcessEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_ProcessEntity model = _bsProcessService.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _bsProcessService.DeleteEntity(Id, out msg, null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_25");//删除操作成功
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBS_Process")]
        public HttpResponseMessage RemoveBS_Process(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                var entity = JsonConvert.DeserializeObject<BS_ProcessEntity>(getValue(jo, "Entity"));

                _ProcessOfOperationsBLL.RemoveForm(t => t.ProcessCode == entity.ProcessCode);
                //删除
                int isok = _bsProcessService.RemoveForm(entity.Id, null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_25");//删除操作成功
                else
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_27");//删除操作失败
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_ProcessEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _bsProcessService.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_28");//获取详情数据成功
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
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_ProcessEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _bsProcessService.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_28");//获取详情数据成功
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
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
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

                var data = _bsProcessService.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
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
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
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
                var list = _bsProcessService.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
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
                var list = _bsProcessService.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 14:57:01
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("BS_Process_export")]
        public HttpResponseMessage BS_Process_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_ProcessController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();


                string msg = Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _bsProcessService.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #region 同步工单工艺路线
        [HttpPost]
        [Route("WorkOrderProcessSync")]
        public HttpResponseMessage WorkOrderProcessSync(JObject jo)
        {
            try
            {
                var processCode = getValue(jo, "processCode");
                if (string.IsNullOrEmpty(processCode))
                    return AjaxResult(false, Language.GetText("Material.BS_ProcessController.Tips_30"));//参数processCode不能为空！

                _bsProcessService.WorkOrderProcessSync(processCode);

                return AjaxResult(true, Language.GetText("Material.BS_ProcessController.Tips_31"));//操作成功！
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 特征工艺路线导入
        [HttpPost]
        [Route("TraitProcessImport")]
        public HttpResponseMessage TraitProcessImport(JObject jo)
        {
            try
            {
                List<BS_ProcessEntity> main = new List<BS_ProcessEntity>();
                List<BS_ProcessOfOperationsEntity> sub = new List<BS_ProcessOfOperationsEntity>();
                List<BS_ProcessOfOperationsAttrEntity> subDetail = new List<BS_ProcessOfOperationsAttrEntity>();

                var data1 = JsonConvert.DeserializeObject<List<BS_ProcessEntity>>(getValue(jo, "data1"));
                var data2 = JsonConvert.DeserializeObject<List<BS_ProcessOfOperationsEntity>>(getValue(jo, "data2"));
                var data3 = JsonConvert.DeserializeObject<List<BS_ProcessOfOperationsAttrModel>>(getValue(jo, "data3"));

                if (data1 == null)
                {
                    return AjaxResult(false, "导入的数据不完整");
                }
                if (data1.Count == 0)
                {
                    return AjaxResult(false, "导入的数据不完整");
                }

                //工艺路线
                foreach (var item in data1)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.ProcessType = "2";
                    item.Creator = CurrentAccount.UserCode;
                    item.CreateTime = DateTime.Now;
                    main.Add(item);

                    if (item.MaterialClass == "规格" || item.MaterialClass == "扣型")
                    {
                        if (data2 == null)
                        {
                            return AjaxResult(false, "请维护工序");
                        }
                        //工序
                        var subData2 = data2.FindAll(t => t.ProcessCode == item.ProcessCode);
                        if (subData2 == null)
                        {
                            return AjaxResult(false, "请维护工序");
                        }
                        foreach (var item2 in subData2)
                        {
                            item2.Id = Guid.NewGuid().ToString();
                            item2.FactoryCode = item.FactoryCode;
                            item2.FactoryName = item.FactoryName;
                            item2.FactoryName = item.FactoryName;
                            item2.Creator = CurrentAccount.UserCode;
                            item2.CreateTime = DateTime.Now;
                            sub.Add(item2);

                            if (item.MaterialClass == "扣型")
                            {
                                if (data3 == null)
                                {
                                    return AjaxResult(false, "请维护属性");
                                }
                                //属性
                                var subData3 = data3.FindAll(t => t.ProcessCode == item2.ProcessCode && t.OperationCode == item2.OperationCode);
                                if (subData3 == null)
                                {
                                    return AjaxResult(false, "请维护属性");
                                }
                                foreach (var item3 in subData3)
                                {
                                    item3.Id = Guid.NewGuid().ToString();
                                    item3.OperationsId = item2.Id;
                                    item3.SortCode = 100;
                                    item3.IsEnabled = true;
                                    item3.Creator = CurrentAccount.UserCode;
                                    item3.CreateTime = DateTime.Now;
                                    var item3_Ent = Tools.Mapper<BS_ProcessOfOperationsAttrEntity>(item3);
                                    subDetail.Add(item3_Ent);
                                }
                            }
                        }
                    }               
                }

                var msg = "";
                //执行事务
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    _bsProcessService.SaveEntity_List(false, "", main, out msg);
                    if (sub.Count > 0)
                    {
                        _ProcessOfOperationsBLL.SaveEntity_List(false, "", sub, out msg);
                    }
                    if (subDetail.Count > 0)
                    {
                        _attrService.SaveEntity_List(false, subDetail);
                    }

                    ts.Complete();
                }

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "异常：" + ex.Message);
            }
        }
        #endregion
    }
}
