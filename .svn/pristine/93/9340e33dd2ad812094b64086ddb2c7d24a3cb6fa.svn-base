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
using ALP.Application.Busines.SystemManage;
using ALP.Application.Busines.ModelLevel;
using ALP.Application.WebApi.Common;

namespace ALP.Application.WebApi.Controllers.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialFactoryController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 工厂物料数据维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("Base_MaterialFactory")]
    public class Base_MaterialFactoryController : ApiBaseController
    {
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();
        private DataItemBLL _dataItemBLL = new DataItemBLL();
        private BS_ProcessBLL _ProcessBLL = new BS_ProcessBLL();
        private Level_BLL _leveBll = new Level_BLL();
        private Base_MaterialFacet_Service _baseMaterialFacetService = new Base_MaterialFacet_Service();
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialFactoryPageList")]
        public HttpResponseMessage Base_MaterialFactoryPageList(JObject jo)
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

                var data = _MaterialFactoryBLL.GetPageList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialFactoryPageDataTableList")]
        public HttpResponseMessage Base_MaterialFactoryPageDataTableList(JObject jo)
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

                var data = _MaterialFactoryBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBase_MaterialFactoryList")]
        public HttpResponseMessage GetBase_MaterialFactoryList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var list = _MaterialFactoryBLL.GetList(checkType, out msg);
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
        /// 根据工厂 物料获取工艺路线下拉
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBase_MaterialFactorySelect")]
        public HttpResponseMessage GetBase_MaterialFactorySelect(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                string queryJson = getValue(jo, "queryJson");
                var dt = _MaterialFactoryBLL.GetListSelect(queryJson);
                result.resultData = dt;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBase_MaterialFactory")]
        public HttpResponseMessage SaveBase_MaterialFactory(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_7");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_8");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            string queryJson = getValue(jo, "Entity");

            try
            {
                //业务服务类

                //参数转实体
                Base_MaterialFactoryEntity entity = JsonConvert.DeserializeObject<Base_MaterialFactoryEntity>(getValue(jo, "Entity"));
                var data = JsonConvert.DeserializeObject<List<Base_MaterialFacetEntity>>(getValue(jo, "data"));

                entity.MaterialCode = entity.MaterialCode.Trim().ToUpper();
                if (string.IsNullOrEmpty(keyValue))
                {
                    var ent = _MaterialFactoryBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_9");//工厂物料编码已经存在
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.Id = Guid.NewGuid().ToString();
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    foreach (var item in data)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.MaterialFactoryId = entity.Id;
                    }
                }
                else
                {
                    var ent1 = _MaterialFactoryBLL.Get_ExpressionEntity(t => t.Id != keyValue && t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_9");//工厂物料编码已经存在
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }

                string msg = "";
                int isok = _MaterialFactoryBLL.SaveEntity(keyValue, entity, out msg);
                if (string.IsNullOrEmpty(keyValue))
                    _baseMaterialFacetService.SaveEntity_List(false, userName, data, out msg);

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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBase_MaterialFactory")]
        public HttpResponseMessage SaveBatchBase_MaterialFactory(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_12");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_13");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_8");//缺少Entity参数！
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
                var old_entity_list = _MaterialFactoryBLL.GetList("", out msg);
                //插入数组
                List<Base_MaterialFactoryEntity> Insert_entity_list = new List<Base_MaterialFactoryEntity>();
                //更新数组
                List<Base_MaterialFactoryEntity> Update_entity_list = new List<Base_MaterialFactoryEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        Base_MaterialFactoryEntity entity = new Base_MaterialFactoryEntity();
                        //物料编码
                        entity.MaterialCode = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_14")];//物料编码
                        //物料名称
                        entity.FactoryCode = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_15")];//物料名称
                        //库存地点
                        entity.Warehouse = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_16")];//库存地点
                        //采购类型
                        entity.ProcureType = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_17")];//采购类型
                        //生产工艺路线
                        entity.ProcessRoute = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_18")];//生产工艺路线
                        //是否启用批次管理
                        entity.IsUsed = false;
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        ////是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        Base_MaterialFactoryEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _MaterialFactoryBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _MaterialFactoryBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBase_MaterialFactory")]
        public HttpResponseMessage DeleteBase_MaterialFactory(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                Base_MaterialFactoryEntity entity = JsonConvert.DeserializeObject<Base_MaterialFactoryEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                Base_MaterialFactoryEntity model = _MaterialFactoryBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _MaterialFactoryBLL.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_21");//删除操作成功
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBase_MaterialFactory")]
        public HttpResponseMessage RemoveBase_MaterialFactory(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_8");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                Base_MaterialFactoryEntity entity = JsonConvert.DeserializeObject<Base_MaterialFactoryEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;

                //删除
                int isok = _MaterialFactoryBLL.RemoveForm(Id, userCode);
                _baseMaterialFacetService.RemoveForm(t => t.MaterialFactoryId == Id);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_21");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_23");//删除操作失败
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialFactoryEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _MaterialFactoryBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_24");//获取详情数据成功
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialFactoryEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _MaterialFactoryBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_24");//获取详情数据成功
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
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

                var data = _MaterialFactoryBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
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
                var list = _MaterialFactoryBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
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
                var list = _MaterialFactoryBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-30 19:46:38
        /// 任务编号: 工厂物料数据维护
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("Base_MaterialFactory_export")]
        public HttpResponseMessage Base_MaterialFactory_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_8");//缺少Entity参数！
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
                var data = _MaterialFactoryBLL.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
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

        #region 导入Excel
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveImportExcel")]
        public HttpResponseMessage SaveImportExcel(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            try
            {

                var data = JsonConvert.DeserializeObject<List<Base_MaterialFactoryEntity>>(getValue(jo, "data"));
                var data1 = JsonConvert.DeserializeObject<List<Base_MaterialFacetEntity>>(getValue(jo, "data1"));

                //所有数据字典
                //MaterialType
                //MaterialSmall
                //Unit
                var vList = _dataItemBLL.GetVDataDictionaryModelList();

                //获取工厂建模
                var levelListt = _leveBll.GetListExprocess(t => true).ToList();
                //获取工艺路线
                var processList = _ProcessBLL.Get_ExpressionList(t => true).ToList();

                foreach (var item in data)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.FactoryCode = levelListt.Find(t => t.ResourceName == item.FactoryCode)?.ResourceCode;
                    item.Warehouse = levelListt.Find(t => t.ResourceName == item.Warehouse)?.ResourceCode;
                    item.ProcureType = vList.Find(t => t.EnCode == "ProcureType" && t.ItemName == item.ProcureType)?.ItemValue;
                    item.ProcessRoute = processList.Find(t => t.ProcessName == item.ProcessRoute)?.ProcessCode;
                    item.Creator = userCode;
                    item.CreateTime = time;
                    item.MaterialCode = item.MaterialCode.Trim().ToUpper();
                }
                foreach (var item in data1)
                {
                    item.MaterialCode = item.MaterialCode.Trim().ToUpper();
                    item.Id = Guid.NewGuid().ToString();
                    item.MaterialFactoryId = data.Find(t => t.MaterialCode == item.MaterialCode).Id;
                    item.AttrType = vList.Find(t => t.EnCode == "AttrType" && t.ItemName == item.AttrType)?.ItemValue;
                    PubFunction.RemoveAttribute(item);
                }
                var query = from d in data
                            join t in _MaterialFactoryBLL.Get_ExpressionList(t => true)
                            on new { d.MaterialCode, d.FactoryCode } equals new { t.MaterialCode, t.FactoryCode }
                            select d;

                if (query.ToList().Count > 0)
                {
                    result.success = false;
                    result.returnMsg = string.Join(",", query.ToList().Select(t => t.MaterialCode)) + ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialFactoryController.Tips_26");//数据重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    if (data.Count > 0) _MaterialFactoryBLL.SaveEntity_List(false, userCode, data, out msg);
                    if (data1.Count > 0) _baseMaterialFacetService.SaveEntity_List(false, userCode, data1, out msg);
                    ts.Complete();
                }

                //  _MaterialFactoryBLL.InsertList(data);
                //if (data.Count > 0) _MaterialFactoryBLL.SaveEntity_List(false, userCode, data, out msg);

                result.success = true;
                result.resultData = null;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
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

        #endregion
    }
}
