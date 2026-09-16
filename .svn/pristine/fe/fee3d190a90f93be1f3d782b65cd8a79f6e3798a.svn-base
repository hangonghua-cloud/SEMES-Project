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
using ALP.Application.WebApi.Common;
using System.Transactions;

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
    [RoutePrefix("BS_TraitDetails")]
    public class BS_TraitDetailsController : ApiBaseController
    {
        private DataItemBLL _dataItemBLL = new DataItemBLL();
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
            result.resultData = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_2");//测试成功
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
        [Route("BS_TraitDetailsPageList")]
        public HttpResponseMessage BS_TraitDetailsPageList(JObject jo)
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("BS_TraitDetailsPageDataTableList")]
        public HttpResponseMessage BS_TraitDetailsPageDataTableList(JObject jo)
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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

        #region 特征值选择
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetTraitValue")]
        public HttpResponseMessage GetTraitValue(JObject jo)
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                var data = _Service.GetTraitValue(pagination, queryJson);
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
        #endregion

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBS_TraitDetailsList")]
        public HttpResponseMessage GetBS_TraitDetailsList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
        [Route("SaveBS_TraitDetails")]
        public HttpResponseMessage SaveBS_TraitDetails(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_7");//缺少Entity参数！
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                //参数转实体
                BS_TraitDetailsEntity entity = JsonConvert.DeserializeObject<BS_TraitDetailsEntity>(getValue(jo, "Entity"));

                //主表关联ID 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.TraitValue))
                {
                    //特征值 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_8");//特征值不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                string TraitCode = getValue(jo, "TraitCode");
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
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_9");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.CreateName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_10");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: BS_TraitDetails	TraitManageID流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("BS_TraitDetails", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.TraitManageID = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;

                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    entity.CreateTime = DateTime.Now;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    entity.IsDeleted = false;
                }
                entity.Id = Guid.NewGuid().ToString();

                var data = JsonConvert.DeserializeObject<List<BS_TraitDetailsAttrEntity>>(getValue(jo, "data"));
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        item.Id = Guid.NewGuid().ToString();
                        item.TraitDetailId = entity.Id;
                    }
                }
                BS_TraitDetailsAttrEntity bS_TraitDetailsAttrEntity = new BS_TraitDetailsAttrEntity();
                if (TraitCode == "MM")
                {
                    bS_TraitDetailsAttrEntity.Id = Guid.NewGuid().ToString();
                    bS_TraitDetailsAttrEntity.TraitDetailId = entity.Id;
                    bS_TraitDetailsAttrEntity.AttrCode = "MMXH";
                    bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_11");//面膜型号
                    bS_TraitDetailsAttrEntity.AttrType = "2";
                    bS_TraitDetailsAttrEntity.AttrValue = entity.TraitValue;
                    bS_TraitDetailsAttrEntity.CreateTime = DateTime.Now;
                    bS_TraitDetailsAttrEntity.Creator = userCode;
                    bS_TraitDetailsAttrEntity.CreatorName = userName;
                    data.Add(bS_TraitDetailsAttrEntity);
                }
                else if (TraitCode == "UV")
                {
                    bS_TraitDetailsAttrEntity.Id = Guid.NewGuid().ToString();
                    bS_TraitDetailsAttrEntity.TraitDetailId = entity.Id;
                    bS_TraitDetailsAttrEntity.AttrCode = "UV";
                    bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_12");//UV亮度
                    bS_TraitDetailsAttrEntity.AttrType = "2";
                    bS_TraitDetailsAttrEntity.AttrValue = entity.TraitValue;
                    bS_TraitDetailsAttrEntity.CreateTime = DateTime.Now;
                    bS_TraitDetailsAttrEntity.Creator = userCode;
                    bS_TraitDetailsAttrEntity.CreatorName = userName;
                    data.Add(bS_TraitDetailsAttrEntity);
                }
                else if (TraitCode == "KX")
                {
                    bS_TraitDetailsAttrEntity.Id = Guid.NewGuid().ToString();
                    bS_TraitDetailsAttrEntity.TraitDetailId = entity.Id;
                    bS_TraitDetailsAttrEntity.AttrCode = "KCKX";
                    bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_13");//开槽扣型
                    bS_TraitDetailsAttrEntity.AttrType = "2";
                    bS_TraitDetailsAttrEntity.AttrValue = entity.TraitValue;
                    bS_TraitDetailsAttrEntity.CreateTime = DateTime.Now;
                    bS_TraitDetailsAttrEntity.Creator = userCode;
                    bS_TraitDetailsAttrEntity.CreatorName = userName;
                    data.Add(bS_TraitDetailsAttrEntity);
                }

                string msg = "";
                int isok = _Service.SaveEntity(keyValue, entity, out msg);
                if (isok == 1)
                {
                    if (string.IsNullOrEmpty(keyValue))
                    {
                        if (data.Count > 0) new BS_TraitDetailsAttr_Service().SaveEntity_List(false, userCode, data, out msg);
                    }
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (isok == 2)
                {

                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_15");//特征值重复
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
        [Route("SaveBatchBS_TraitDetails")]
        public HttpResponseMessage SaveBatchBS_TraitDetails(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_17");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_18");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<BS_TraitDetailsEntity> Insert_entity_list = new List<BS_TraitDetailsEntity>();
                //更新数组
                List<BS_TraitDetailsEntity> Update_entity_list = new List<BS_TraitDetailsEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        BS_TraitDetailsEntity entity = new BS_TraitDetailsEntity();
                        //主表关联ID
                        entity.TraitManageID = item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_19")];//主表关联ID
                        //特征值
                        entity.TraitValue = item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_20")];//特征值
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTime.Now;
                        //是否删除
                        entity.IsDeleted = false;
                        entity.Creator = CreatedByCode;
                        entity.CreateName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        BS_TraitDetailsEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        [Route("DeleteBS_TraitDetails")]
        public HttpResponseMessage DeleteBS_TraitDetails(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                BS_TraitDetailsEntity entity = JsonConvert.DeserializeObject<BS_TraitDetailsEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_TraitDetailsEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_24");//删除操作成功
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
        [Route("RemoveBS_TraitDetails")]
        public HttpResponseMessage RemoveBS_TraitDetails(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                BS_TraitDetailsEntity entity = JsonConvert.DeserializeObject<BS_TraitDetailsEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                BS_TraitDetailsEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = _Service.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_24");//删除操作成功
                else
                    result.returnMsg = msg;
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
        /// <returns>BS_TraitDetailsEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_26");//获取详情数据成功
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
        /// <returns>BS_TraitDetailsEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_26");//获取详情数据成功
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
        [Route("BS_TraitDetails_export")]
        public HttpResponseMessage BS_TraitDetails_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
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
            // var userCode = CurrentAccount.UserCode;
            var time = DateTime.Now;
            var msg = "";
            List<BS_TraitDetailsEntity> materialFacetList = new List<BS_TraitDetailsEntity>();
            try
            {

                var data1 = JsonConvert.DeserializeObject<List<BS_TraitDetailsEntity>>(getValue(jo, "data1"));
                var dataArrt = JsonConvert.DeserializeObject<List<BS_TraitDetailsAttrEntity>>(getValue(jo, "dataArrt"));

                var TraitManageID = getValue(jo, "TraitManageID");
                var CreateName = getValue(jo, "CreateName");
                var Creator = getValue(jo, "Creator");
                var TraitCode = getValue(jo, "TraitCode");
                //所有数据字典
                //MaterialType
                //MaterialSmall
                //Unit
                var vList = _dataItemBLL.GetVDataDictionaryModelList();

                var group = data1.GroupBy(t => new { t.TraitValue }).ToList();
                if (group.Count != data1.Count)
                {
                    foreach (var item in group)
                    {
                        if (item.Count() > 1)
                        {
                            msg += item.Key.TraitValue + ",";
                        }
                    }
                    result.success = false;
                    result.returnMsg = msg + ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_28");//特征值重复！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_TraitDetailsAttrEntity bS_TraitDetailsAttrEntity = new BS_TraitDetailsAttrEntity();

                foreach (var item in data1)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.TraitManageID = TraitManageID;
                    item.Creator = Creator;
                    item.CreateName = CreateName;
                    item.CreateTime = time;
                    item.IsDeleted = false;
                    item.ModifyBy = CurrentAccount.UserCode;
                    item.ModifyName = CurrentAccount.UserName;
                    item.ModifyTime = DateTime.Now;
                    if (TraitCode == "MM")
                    {
                        bS_TraitDetailsAttrEntity.AttrCode = "MMXH";
                        bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_11");//面膜型号
                        bS_TraitDetailsAttrEntity.AttrType = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_30");//字符串
                        bS_TraitDetailsAttrEntity.AttrValue = item.TraitValue;
                        bS_TraitDetailsAttrEntity.TraitValue = item.TraitValue;
                        dataArrt.Add(bS_TraitDetailsAttrEntity);
                    }
                    if (TraitCode == "UV")
                    {
                        bS_TraitDetailsAttrEntity.AttrCode = "UV";
                        bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_12");//UV亮度
                        bS_TraitDetailsAttrEntity.AttrType = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_30");//字符串
                        bS_TraitDetailsAttrEntity.AttrValue = item.TraitValue;
                        bS_TraitDetailsAttrEntity.TraitValue = item.TraitValue;
                        dataArrt.Add(bS_TraitDetailsAttrEntity);
                    }
                    if (TraitCode == "KX")
                    {
                        bS_TraitDetailsAttrEntity.AttrCode = "KCKX";
                        bS_TraitDetailsAttrEntity.AttrName = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_13");//开槽扣型
                        bS_TraitDetailsAttrEntity.AttrType = ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_30");//字符串
                        bS_TraitDetailsAttrEntity.AttrValue = item.TraitValue;
                        bS_TraitDetailsAttrEntity.TraitValue = item.TraitValue;
                        dataArrt.Add(bS_TraitDetailsAttrEntity);
                    }

                }

                foreach (var item in dataArrt)
                {
                    item.Id = Guid.NewGuid().ToString();
                    var detailEntity = data1.Find(t => t.TraitValue == item.TraitValue);
                    if (detailEntity == null)
                        return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_33",item.TraitValue));//特征值信息中没有[{item.TraitValue}]！

                    item.TraitDetailId = detailEntity.Id;
                    item.AttrType = vList.Find(t => t.EnCode == "AttrType" && t.ItemName == item.AttrType)?.ItemValue;
                    item.Creator = Creator;
                    item.CreatorName = CreateName;
                    item.CreateTime = time;
                    PubFunction.RemoveAttribute(item);
                }

                BS_TraitDetails_Service _Service = new BS_TraitDetails_Service();
                var query = from t in data1
                            join m in _Service.Get_ExpressionList(t => t.IsDeleted == false)
                             on new { t.TraitManageID, t.TraitValue } equals new { m.TraitManageID, m.TraitValue }

                            select t;
                if (query.ToList().Count > 0)
                {
                    result.success = false;
                    result.returnMsg = string.Join(",", query.ToList().Select(t => t.TraitValue)) + ALP.Application.Service.Resources.Language.GetText("SAP.BS_TraitDetailsController.Tips_34");//数据重复
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
                    if (data1.Count > 0) _Service.NewSaveEntity_List(false, Creator, data1, out msg);
                    if (dataArrt.Count > 0) new BS_TraitDetailsAttr_Service().SaveEntity_List(false, Creator, dataArrt, out msg);

                    ts.Complete();
                }
                // if (data1.Count > 0) _Service.NewSaveEntity_List(false, Creator, data1, out msg);

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

    }
}
