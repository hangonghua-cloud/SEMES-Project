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

namespace ALP.Application.WebApi.Controllers.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialBindTempController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 任务名称或编号
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("Base_MaterialBindTempFacet")]
    public class Base_MaterialBindTempFacetController : ApiBaseController
    {
        private Base_MaterialBindTempFacetBLL _MateriaBindTempBLL = new Base_MaterialBindTempFacetBLL();

        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败


        #region 其他接口
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region 分页获取数据 实体

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialBindTempFacetPageList")]
        public HttpResponseMessage Base_MaterialBindTempFacetPageList(JObject jo)
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
                var data = _MateriaBindTempBLL.GetPageList(pagination, queryJson);
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
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = FaildMsg + ex.Message; 
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }

        #endregion

        #region 分页获取查询 DataTable
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialBindTempFacetPageDataTableList")]
        public HttpResponseMessage Base_MaterialBindTempFacetPageDataTableList(JObject jo)
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
 
                var data = _MateriaBindTempBLL.GetPageDataTableList(pagination, queryJson);
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
                result.returnMsg = SuccessMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = FaildMsg + ex.Message; 
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }

        #endregion

        #region 下拉列表
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBase_MaterialBindTempFacetList")]
        public HttpResponseMessage GetBase_MaterialBindTempFacetList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                
                string msg = "";
                var list = _MateriaBindTempBLL.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = SuccessMsg;
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
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBase_CodeMaterialBindTempFacetList")]
        public HttpResponseMessage GetBase_CodeMaterialBindTempFacetList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = new Base_MaterialBindTempFacet_Service().GetListBycode(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = SuccessMsg;
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
        #endregion

        #region  保存表单 新增、修改 
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBase_MaterialBindTempFacet")]
        public HttpResponseMessage SaveBase_MaterialBindTempFacet(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string keyValue = getValue(jo, "KeyValue");
 
            try
            {
                //参数转实体
                Base_MaterialBindTempFacetEntity entity = JsonConvert.DeserializeObject<Base_MaterialBindTempFacetEntity>(getValue(jo, "Entity"));

                //1.查询是否存在相同的编码和名称
                var ent = _MateriaBindTempBLL.Get_ExpressionEntity(t => t.MateriaBindTempId==entity.MateriaBindTempId && (t.AttrCode == entity.AttrCode || t.AttrName == entity.AttrName));
                if (ent != null && string.IsNullOrEmpty(keyValue))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_8");//存在相同编码或者名称
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var ent1 = _MateriaBindTempBLL.Get_ExpressionEntity(t => t.MateriaBindTempId == entity.MateriaBindTempId && t.Id != keyValue && t.AttrName == entity.AttrName);
                    if (ent1 != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_9");//存在相同名称
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                }


               
                //修改
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;
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
                int isok = _MateriaBindTempBLL.SaveEntity(keyValue, entity, out msg);
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? SuccessMsg: msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region 导入 保存表单 新增、修改
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBase_MaterialBindTempFacet")]
        public HttpResponseMessage SaveBatchBase_MaterialBindTempFacet(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_10");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_11");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_7");//缺少Entity参数！
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
                var old_entity_list = _MateriaBindTempBLL.Get_ExpressionList(t=>t.IsEnabled==true).ToList();
                //插入数组
                List<Base_MaterialBindTempFacetEntity> Insert_entity_list = new List<Base_MaterialBindTempFacetEntity>();
                //更新数组
                List<Base_MaterialBindTempFacetEntity> Update_entity_list = new List<Base_MaterialBindTempFacetEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        Base_MaterialBindTempFacetEntity entity = new Base_MaterialBindTempFacetEntity();
                        //模板分类
                        entity.MateriaBindTempId =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_12")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_12")];//模板分类
                        //模板名称
                        entity.TempName =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_13")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_13")];//模板名称
                        //属性编码
                        entity.AttrCode =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_14")];//属性编码
                        //属性名称
                        entity.AttrName =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_15")];//属性名称
                        //属性类型
                        entity.AttrType =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_16")];//属性类型
                        //上限
                        entity.UpperLimit =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_17")];//上限
                        //下限
                        entity.LowerLimit =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_18")];//下限
                        //长度
                        entity.Length =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_19")];//长度
                        //默认值
                        entity.DefaultValue =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_20")];//默认值
                        //是否可用
                        entity.IsEnabled = false;
                        //排序
                        entity.Sort =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_21")];//排序
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_22")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_23")];//创建时间
                        //最后修改人
                        entity.ModifyBy =  item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_24")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_25")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                     //   entity.CreatedDateTime = DateTimeOffset.Now;
                        //是否删除
                      //  entity.IsDeleted = false;
                      //  entity.CreatedByCode = CreatedByCode;
                     //   entity.CreatedByName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        Base_MaterialBindTempFacetEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                        isok = _MateriaBindTempBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                    }
                    if (Update_entity_list.Count > 0)
                    {
                        //批量修改
                        isok = _MateriaBindTempBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
                    }
                    
                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? SuccessMsg: msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region  删除 
        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBase_MaterialBindTempFacet")]
        public HttpResponseMessage DeleteBase_MaterialBindTempFacet(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                Base_MaterialBindTempFacetEntity entity = JsonConvert.DeserializeObject<Base_MaterialBindTempFacetEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                Base_MaterialBindTempFacetEntity model = _MateriaBindTempBLL.Get_ExpressionEntity(t=>t.Id==Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _MateriaBindTempBLL.DeleteEntity(Id, out msg, entity.AttrName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_28");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBase_MaterialBindTempFacet")]
        public HttpResponseMessage RemoveBase_MaterialBindTempFacet(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                
                Base_MaterialBindTempFacetEntity entity = JsonConvert.DeserializeObject<Base_MaterialBindTempFacetEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                Base_MaterialBindTempFacetEntity model = _MateriaBindTempBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _MateriaBindTempBLL.RemoveForm(Id, entity.AttrName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_28");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_30");//删除操作失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region  获取实体
        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialBindTempFacetEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _MateriaBindTempBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_31");//获取详情数据成功
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialBindTempFacetEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                
                var data = _MateriaBindTempBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_31");//获取详情数据成功
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
                
                var data = _MateriaBindTempBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 ).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = SuccessMsg;
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

        #endregion 

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
                var list = _MateriaBindTempBLL.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = SuccessMsg;
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
                var list = _MateriaBindTempBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = SuccessMsg;
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("Base_MaterialBindTempFacet_export")]
        public HttpResponseMessage Base_MaterialBindTempFacet_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialBindTempFacetController.Tips_7");//缺少Entity参数！
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
                var data = _MateriaBindTempBLL.GetList_export(CreatedByCode, out msg);
                
                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false; 
                result.returnMsg = FaildMsg + ex.Message; 
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        
    }
}
