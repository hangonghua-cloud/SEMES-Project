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
using ALP.Application.Busines.SystemManage;
using ALP.Application.Busines.ModelLevel;
using System.Transactions;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_BOMController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("BS_BOM")]
    public class BS_BOMController : ApiBaseController
    {
        private BS_BOMBLL _BOMBLL = new BS_BOMBLL();
        private BS_BOMItemsBLL _BOMItemsBLL = new BS_BOMItemsBLL();
        private DataItemBLL _dataItemBLL = new DataItemBLL();
        private BS_ProcessBLL _ProcessBLL = new BS_ProcessBLL();
        private Level_BLL _leveBll = new Level_BLL();
        private Base_MaterialBLL _MaterialBLL = new Base_MaterialBLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("Material.BS_BOMController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_BOMPageList")]
        public HttpResponseMessage BS_BOMPageList(JObject jo)
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

                var data = _BOMBLL.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_BOMPageDataTableList")]
        public HttpResponseMessage BS_BOMPageDataTableList(JObject jo)
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

                var data = _BOMBLL.GetPageDataTableList(pagination, queryJson);
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBS_BOMList")]
        public HttpResponseMessage GetBS_BOMList(string checkType, string factoryCode)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var list = _BOMBLL.GetList(checkType, factoryCode, out msg);
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
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBS_BOM")]
        public HttpResponseMessage SaveBS_BOM(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            var returnNum = "0";
            string msg = "";

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_8");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_9");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            string keyValue = getValue(jo, "KeyValue");
            string queryJson = getValue(jo, "Entity");

            try
            {

                var entity = JsonConvert.DeserializeObject<BS_BOMEntity>(queryJson);

                if (!string.IsNullOrEmpty(keyValue))
                {
                    //var ent1 = _BOMBLL.Get_ExpressionEntity(t => t.Id != keyValue && t.MaterialCode == entity.MaterialCode);
                    //if (ent1 != null)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = "存在相同名称";
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                    entity.ModifyBy = userCode;
                    entity.ModifyTime = DateTime.Now;
                }
                else
                {
                    //var materialcode = entity.MaterialCode;
                    //var ent = _BOMBLL.Get_ExpressionEntity(t => t.MaterialCode == materialcode);
                    //if (ent != null)
                    //{
                    //    result.success = false;
                    //    result.returnMsg = "存在相同编码或者名称";
                    //    return Request.CreateResponse(HttpStatusCode.OK, result);
                    //}
                    var flag = _BOMBLL.GetSerialNO("BomCode", 1, out returnNum, out msg);
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    entity.BOMCode = "BOM" + DateTime.Now.ToString("yyMMdd") + returnNum;
                }
                if (entity.BOMType == "2" && entity.isDefault == true)
                {
                    var BOMEntityList = _BOMBLL.Get_ExpressionList(t => t.MaterialCode == entity.MaterialCode && t.FactoryCode == entity.FactoryCode && t.OrderType == entity.OrderType && t.isDefault == true).ToList();
                    if (BOMEntityList.Count > 0)
                    {
                        result.success = false;
                        result.returnMsg = entity.MaterialCode + Language.GetText("Material.BS_BOMController.Tips_10");//特征下存在默认BOM不允许有多个默认
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }

                int isok = _BOMBLL.SaveEntity(keyValue, entity, out msg);
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBS_BOM")]
        public HttpResponseMessage SaveBatchBS_BOM(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_13");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_14");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_9");//缺少Entity参数！
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
                var old_entity_list = _BOMBLL.GetList("", "", out msg);
                //插入数组
                List<BS_BOMEntity> Insert_entity_list = new List<BS_BOMEntity>();
                //更新数组
                List<BS_BOMEntity> Update_entity_list = new List<BS_BOMEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        BS_BOMEntity entity = new BS_BOMEntity();
                        //工厂编码
                        entity.FactoryCode = item[Language.GetText("Material.BS_BOMController.Tips_15")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_15")];//工厂编码
                        //物料分类
                        entity.MaterialClass = item[Language.GetText("Material.BS_BOMController.Tips_16")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_16")];//物料分类
                        //BOM编码
                        entity.BOMCode = item[Language.GetText("Material.BS_BOMController.Tips_17")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_17")];//BOM编码
                        //物料编码
                        entity.MaterialCode = item[Language.GetText("Material.BS_BOMController.Tips_18")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_18")];//物料编码
                        //物料名称
                        entity.MaterialName = item[Language.GetText("Material.BS_BOMController.Tips_19")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_19")];//物料名称
                        //单位数量
                        entity.UnitNum = item[Language.GetText("Material.BS_BOMController.Tips_20")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_20")];//单位数量
                        //工艺
                        entity.Process = item[Language.GetText("Material.BS_BOMController.Tips_21")] == null ? "" : item[Language.GetText("Material.BS_BOMController.Tips_21")];//工艺
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        ////是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        BS_BOMEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _BOMBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _BOMBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBS_BOM")]
        public HttpResponseMessage DeleteBS_BOM(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                BS_BOMEntity entity = JsonConvert.DeserializeObject<BS_BOMEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_BOMEntity model = _BOMBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _BOMBLL.DeleteEntity(Id, out msg, null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_24");//删除操作成功
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBS_BOM")]
        public HttpResponseMessage RemoveBS_BOM(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_7");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                BS_BOMEntity entity = JsonConvert.DeserializeObject<BS_BOMEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                _BOMItemsBLL.RemoveForm(t => t.BOMId == Id);
                int isok = _BOMBLL.RemoveForm(Id, null);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_24");//删除操作成功
                else
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_26");//删除操作失败
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_BOMEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _BOMBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_27");//获取详情数据成功
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_BOMEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _BOMBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_27");//获取详情数据成功
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
        /// 创建日期: 2021-07-26 18:04:37
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

                var data = _BOMBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-07-26 18:04:37
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
                var list = _BOMBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-26 18:04:37
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
                var list = _BOMBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("BS_BOM_export")]
        public HttpResponseMessage BS_BOM_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.BS_BOMController.Tips_9");//缺少Entity参数！
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
                var data = _BOMBLL.GetList_export(CreatedByCode, out msg);

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
        /// <summary>
        /// 获取小料 尾料bom信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFormulaBOM")]
        public HttpResponseMessage GetFormulaBOM(JObject jo)
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

                var data = _BOMBLL.GetFormulaBOM(pagination, queryJson);
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
            var msg = string.Empty;
            var returnNum = string.Empty;
            try
            {

                var data1 = JsonConvert.DeserializeObject<List<BS_BOMEntity>>(getValue(jo, "data1"));
                var data2 = JsonConvert.DeserializeObject<List<BS_BOMItemsEntity>>(getValue(jo, "data2"));

                data1.ForEach(item => { item.MaterialCode = item.MaterialCode.Trim().ToUpper(); });
                foreach (var item in data2)
                {
                    item.ProductCode = item.ProductCode.Trim().ToUpper();
                    item.MaterialCode = item.MaterialCode.Trim().ToUpper();
                }

                var query = from d in data1
                            join t in _BOMBLL.Get_ExpressionList(t => true)
                            on new { d.MaterialCode, d.OrderType, d.FactoryCode, d.Process } equals new { t.MaterialCode, t.OrderType, t.FactoryCode, t.Process }
                            select d;

                if (query.ToList().Count > 0)
                {
                    result.success = false;
                    result.returnMsg = string.Join(",", query.ToList().Select(t => t.MaterialCode)) + Language.GetText("Material.BS_BOMController.Tips_29");//数据重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //bom物料主数据
                var mquery = from d in data1
                             join t in _MaterialBLL.Get_ExpressionList(t => true)
                             on d.MaterialCode equals t.MaterialCode
                             select t;
                var mList = mquery.ToList();

                // bomitem物料数据
                var mquery2 = from d in data2
                              join t in _MaterialBLL.Get_ExpressionList(t => true)
                              on d.MaterialCode equals t.MaterialCode
                              select t;
                var mList2 = mquery2.ToList();

                _BOMBLL.GetSerialNO("BomCode", data1.Count, out returnNum, out msg);
                var index = returnNum.ToInt();
                //所有数据字典
                //MaterialType 
                //MaterialSmall
                //Unit

                var vList = _dataItemBLL.GetVDataDictionaryModelList();

                //获取工厂建模
                var levelListt = _leveBll.GetListExprocess(t => true).ToList();


                //获取工艺路线
                var processList = _ProcessBLL.Get_ExpressionList(t => true).ToList();

                foreach (var item in data1)
                {
                    if (!processList.Any(t => t.ProcessCode == item.Process))
                        return AjaxResult(false, Language.GetText("Material.BS_BOMController.Tips_30", item.Process));//工艺编码【{0}】不存在！

                    item.Id = Guid.NewGuid().ToString();
                    item.BOMCode = "BOM" + time.ToString("yyMMdd") + (index++).ToString().PadLeft(3, '0');
                    item.MaterialClass = mList.Find(t => t.MaterialCode == item.MaterialCode)?.MaterialClass;
                    //item.Process = processList.Find(t => t.ProcessName == item.Process)?.ProcessCode;
                    //item.BOMType = vList.Find(t => t.EnCode == "BOMType" && t.ItemName == item.BOMType)?.ItemValue;
                    item.Creator = userCode;
                    item.CreateTime = time;
                }

                foreach (var item in data2)
                {
                    if (!levelListt.Any(t => t.ResourceCode == item.ConsumeProcess))
                        return AjaxResult(false, Language.GetText("Material.BS_BOMController.Tips_31", item.ConsumeProcess));

                    if (!levelListt.Any(t => t.ResourceCode == item.Warehouse))
                        return AjaxResult(false, Language.GetText("Material.BS_BOMController.Tips_32", item.Warehouse));

                    item.Id = Guid.NewGuid().ToString();
                    var bomEntity = data1.Find(t => t.MaterialCode == item.ProductCode && t.OrderType == item.OrderType);
                    if (bomEntity == null)
                        return AjaxResult(false, $"没有找到bom主表数据,产品编码【{item.ProductCode}】、订单类型【{item.OrderType}】");

                    item.BOMId = bomEntity.Id;
                    item.FactoryName = data1.Find(t => t.MaterialCode == item.ProductCode && t.OrderType == item.OrderType).FactoryName;
                    item.FactoryCode = data1.Find(t => t.MaterialCode == item.ProductCode && t.OrderType == item.OrderType).FactoryCode;
                    //item.Warehouse = levelListt.Find(t => t.ResourceName == item.Warehouse)?.ResourceCode;

                    //item.ConsumeProcess = _leveBll.GetModelResourceByFactory("Process", item.FactoryName, item.ConsumeProcess).FirstOrDefault()?.ResourceCode;
                    //item.ConsumeProcess = levelListt.Find(t => t.ModelLeve == "Process" && t.ResourceName == item.ConsumeProcess )?.ResourceCode;
                    item.Unit = mList2.Find(t => t.MaterialCode == item.MaterialCode)?.Unit;
                    item.UnitName = mList2.Find(t => t.MaterialCode == item.MaterialCode)?.UnitName;
                    item.Creator = userCode;
                    item.CreateTime = time;
                }

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    if (data1.Count > 0) _BOMBLL.SaveEntity_List(false, userCode, data1, out msg);
                    if (data2.Count > 0) _BOMItemsBLL.SaveEntity_List(false, userCode, data2, out msg);
                    ts.Complete();
                }
                result.success = true;
                result.resultData = null;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
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
