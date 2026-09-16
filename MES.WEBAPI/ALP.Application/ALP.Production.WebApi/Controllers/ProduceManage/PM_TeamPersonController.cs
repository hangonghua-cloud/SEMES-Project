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

namespace ALP.Application.WebApi.Controllers.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TeamPersonController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 生产小组人员管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("PM_TeamPerson")]
    public class PM_TeamPersonController : ApiBaseController
    {
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_TeamPersonPageList")]
        public HttpResponseMessage PM_TeamPersonPageList(JObject jo)
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
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                var data = _Service.GetPageList(pagination, queryJson);
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("PM_TeamPersonPageDataTableList")]
        public HttpResponseMessage PM_TeamPersonPageDataTableList(JObject jo)
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
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetPM_TeamPersonList")]
        public HttpResponseMessage GetPM_TeamPersonList(string ProcessCode="")
        {
            var result = new ResponseResult();
            try
            {
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                string msg = "";
                var list = _Service.GetList(ProcessCode, out msg);
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SavePM_TeamPerson")]
        public HttpResponseMessage SavePM_TeamPerson(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //业务服务类
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                string keyValue = getValue(jo, "KeyValue");
                //参数转实体
                PM_TeamPersonEntity entity = JsonConvert.DeserializeObject<PM_TeamPersonEntity>(getValue(jo, "Entity"));

                if (string.IsNullOrEmpty(entity.FactoryCode))
                {
                    //工厂编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_8");//工厂编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ProcessCode))
                {
                    //工序编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_9");//工序编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.PTeamCode))
                {
                    //生产小组编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_10");//生产小组编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                else if (string.IsNullOrEmpty(entity.PTeamName))
                {
                    //生产小组编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_11");//生产小组名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var isExist = _Service.Get_ExpressionEntity(t => t.PTeamCode == entity.PTeamCode && t.Id != keyValue);
                if (isExist != null)
                {
                    result.success = false;
                    result.returnMsg = "生产小组编码[" + entity.PTeamCode + ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_12");//]已存在！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }



                if (!string.IsNullOrEmpty(keyValue))
                {
                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    entity.ModifyBy = CurrentAccount.UserCode;
                }
                else
                {
                    entity.CreateTime = DateTime.Now;
                    entity.Creator = CurrentAccount.UserCode;
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchPM_TeamPerson")]
        public HttpResponseMessage SaveBatchPM_TeamPerson(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            ////创建人编码 为空判断
            //if (jo.SelectToken("CreatedByCode") == null)
            //{
            //result.success = false;
            //result.returnMsg = "缺少CreatedByCode参数！";
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}

            ////创建人姓名 为空判断
            //if (jo.SelectToken("CreatedByName") == null)
            //{
            //result.success = false;
            //result.returnMsg = "缺少CreatedByName参数！";
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<PM_TeamPersonEntity> Insert_entity_list = new List<PM_TeamPersonEntity>();
                //更新数组
                List<PM_TeamPersonEntity> Update_entity_list = new List<PM_TeamPersonEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        PM_TeamPersonEntity entity = new PM_TeamPersonEntity();
                        //报工ID

                        //工厂编码
                        entity.FactoryCode = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_15")];//工厂编码
                        //工厂名称

                        //工序编码
                        entity.ProcessCode = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_16")];//工序编码
                        //工序名称

                        //生产小组编码
                        entity.PTeamCode = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_17")];//生产小组编码
                        //生产小组名称
                        entity.PTeamName = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_18")];//生产小组名称
                        //生产小组人员

                        //创建人
                        entity.Creator = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_19")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_20")];//创建时间
                        //最后修改人
                        entity.ModifyBy = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_21")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_22")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTime.Now;


                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        PM_TeamPersonEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeletePM_TeamPerson")]
        public HttpResponseMessage DeletePM_TeamPerson(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                PM_TeamPersonEntity entity = JsonConvert.DeserializeObject<PM_TeamPersonEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_TeamPersonEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, "");
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_25");//删除操作成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemovePM_TeamPerson")]
        public HttpResponseMessage RemovePM_TeamPerson(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                PM_TeamPersonEntity entity = JsonConvert.DeserializeObject<PM_TeamPersonEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                PM_TeamPersonEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _Service.RemoveForm(Id, "");
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_25");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_27");//删除操作失败
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_TeamPersonEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_28");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>PM_TeamPersonEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_28");//获取详情数据成功
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
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
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-05 09:15:33
        /// 任务编号: 生产小组人员管理
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("PM_TeamPerson_export")]
        public HttpResponseMessage PM_TeamPerson_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("ProduceManage.PM_TeamPersonController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                PM_TeamPerson_Service _Service = new PM_TeamPerson_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }


    }
}
