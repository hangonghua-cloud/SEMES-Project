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

namespace ALP.Application.WebApi.Controllers.SAP
{
    /// <summary>
    /// 1.创建日期: 2022-11-02
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitManageController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 特征维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("BS_TraitManage")]
    public class BS_TraitManageController : ApiBaseController
    {
        BS_TraitDetails_Service _traitDetailsService = new BS_TraitDetails_Service();
        BS_TraitManage_Service _traitManageService = new BS_TraitManage_Service();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_TraitManagePageList")]
        public HttpResponseMessage BS_TraitManagePageList(JObject jo)
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
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                var data = _Service.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Count(),
                    page = pagination != null ? pagination.total : data.Count(),
                    records = pagination != null ? pagination.records : data.Count(),
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
        /// 功能描述：特征值弹窗数据源
        /// 创建jpf
        /// 时间:2022-11-14 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("BS_TraitManagerDetailDataTableList")]
        public HttpResponseMessage BS_TraitManagerDetailDataTableList(JObject jo)
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
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                var data = _Service.GetTraitManageDetaiilPageList(pagination, queryJson);
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_TraitManagePageDataTableList")]
        public HttpResponseMessage BS_TraitManagePageDataTableList(JObject jo)
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
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBS_TraitManageList")]
        public HttpResponseMessage GetBS_TraitManageList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                string msg = "";
                var list = _Service.GetList(checkType, out msg);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBS_TraitManage")]
        public HttpResponseMessage SaveBS_TraitManage(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_7");//缺少Entity参数！
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
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                //参数转实体
                BS_TraitManageEntity entity = JsonConvert.DeserializeObject<BS_TraitManageEntity>(getValue(jo, "Entity"));
                //工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.FactoryCode))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_8");//工厂编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.FactoryName))
                {
                    //工厂名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_9");//工厂名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TraitName))
                {
                    //特征名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_10");//特征名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.TraitCode))
                {
                    //特征名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_11");//特征编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {


                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    entity.CreateTime = DateTime.Parse(entity.CreateTime.ToString());
                }
                else
                {

                    if (string.IsNullOrEmpty(entity.Creator))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_12");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.CreateName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_13");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: BS_TraitManage	FactoryCode流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("BS_TraitManage", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.FactoryCode = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;

                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    entity.CreateTime = DateTime.Now;
                    //是否启用
                    //entity.IsEnable = false;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    entity.IsDeleted = false;
                }

                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg);
                if (isok == 1)
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (isok == 2)
                {

                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_15");//特征重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    result.success = false;
                    result.returnMsg = msg;
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBS_TraitManage")]
        public HttpResponseMessage SaveBatchBS_TraitManage(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_17");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_18");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<BS_TraitManageEntity> Insert_entity_list = new List<BS_TraitManageEntity>();
                //更新数组
                List<BS_TraitManageEntity> Update_entity_list = new List<BS_TraitManageEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        BS_TraitManageEntity entity = new BS_TraitManageEntity();
                        //工厂编码
                        entity.FactoryCode = item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_19")];//工厂编码
                        //工厂名称
                        entity.FactoryName = item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_20")];//工厂名称
                        //特征名称
                        entity.TraitName = item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_21")];//特征名称
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTime.Now;
                        //是否删除
                        entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        entity.CreateName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        BS_TraitManageEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBS_TraitManage")]
        public HttpResponseMessage DeleteBS_TraitManage(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                BS_TraitManageEntity entity = JsonConvert.DeserializeObject<BS_TraitManageEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_TraitManageEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_25");//删除操作成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBS_TraitManage")]
        public HttpResponseMessage RemoveBS_TraitManage(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                BS_TraitManageEntity entity = JsonConvert.DeserializeObject<BS_TraitManageEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_TraitManageEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var lstTraitDetails = _traitDetailsService.Get_ExpressionList(t => t.TraitManageID == Id).ToList();
                if (lstTraitDetails.Count > 0)
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_27"));//请先删除子表数据！

                //删除
                var msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_25");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_28");//删除操作失败
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_TraitManageEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_29");//获取详情数据成功
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
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>BS_TraitManageEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_29");//获取详情数据成功
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
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
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.IsDeleted == false).OrderByDescending(t => t.Id).ToList();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("BS_TraitManage_export")]
        public HttpResponseMessage BS_TraitManage_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitManageController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                BS_TraitManage_Service _Service = new BS_TraitManage_Service();
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
