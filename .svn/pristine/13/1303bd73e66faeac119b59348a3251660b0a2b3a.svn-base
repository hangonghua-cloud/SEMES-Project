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
using System.Transactions;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Entity.PlanManage;
using ALP.Application.WebApi.Common;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Busines.MaterialManage;
using ALP.Application.Service.Resources;

namespace ALP.Application.WebApi.Controllers.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 物料主数据
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("Base_Material")]
    public class Base_MaterialController : ApiBaseController
    {
        private Base_MaterialBLL _MaterialBLL = new Base_MaterialBLL();
        private BS_BOMItemsBLL _BOMItemsBLL = new BS_BOMItemsBLL();
        private Base_MaterialFacetBLL _MaterialFacetBLL = new Base_MaterialFacetBLL();
        private DataItemBLL _dataItemBLL = new DataItemBLL();
        private BS_BOMBLL _BS_BOMBLL = new BS_BOMBLL();
        private PL_BOMBLL _PL_BOMBLL = new PL_BOMBLL();
        private PL_BOMItemsBLL _PL_BOMItemsBLL = new PL_BOMItemsBLL();
        private Base_MaterialFactoryBLL _MaterialFactoryBLL = new Base_MaterialFactoryBLL();
        private MM_RawMaterialStockBLL _RawMaterialStockBLL = new MM_RawMaterialStockBLL();

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = Language.GetText("Material.Base_MaterialController.Tips_3") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_4");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialPageList")]
        public HttpResponseMessage Base_MaterialPageList(JObject jo)
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

                var data = _MaterialBLL.GetPageList(pagination, queryJson);
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
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("Base_MaterialPageDataTableList")]
        public HttpResponseMessage Base_MaterialPageDataTableList(JObject jo)
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

                var data = _MaterialBLL.GetPageDataTableList(pagination, queryJson);
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

        #region 查询规格
        /// <summary>
        /// 功能描述: 查询规格(分页) 数据支持查询与分页
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-24
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetSpecPageDataTableList")]
        public HttpResponseMessage GetSpecPageDataTableList(JObject jo)
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

                var data = _MaterialBLL.GetSpecPageDataTableList(pagination, queryJson);
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
        #endregion

        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetBase_MaterialList")]
        public HttpResponseMessage GetBase_MaterialList(string checkType)
        {
            var result = new ResponseResult();
            try
            {

                string msg = "";
                var list = _MaterialBLL.GetList(checkType, out msg);
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBase_Material")]
        public HttpResponseMessage SaveBase_Material(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_8");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_9");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_10");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //if (jo.SelectToken("Data") == null)
            //{
            //    result.success = false;
            //    result.returnMsg = "缺少Data参数！";
            //    return Request.CreateResponse(HttpStatusCode.OK, result);
            //}

            string keyValue = getValue(jo, "KeyValue");

            //string data = getValue(jo, "Data");
            try
            {
                //参数转实体
                var entity = JsonConvert.DeserializeObject<Base_MaterialEntity>(getValue(jo, "Entity"));
                //var list = JsonConvert.DeserializeObject<List<Base_MaterialFacetEntity>>(data);

                //1.查询是否存在相同的编码和名称
                entity.MaterialCode = entity.MaterialCode.ToUpper();//转大写
                var ent = _MaterialBLL.Get_ExpressionEntity(t => t.MaterialCode == entity.MaterialCode);
                if (ent != null && string.IsNullOrEmpty(keyValue))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_11");//存在相同编码
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string msg = "";
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyBy = userCode;

                    //更新日期
                    entity.ModifyTime = DateTime.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    entity.CreateTime = DateTime.Parse(entity.CreateTime.ToString());
                    
                    //保存到BS_BOMItems表
                    List<BS_BOMItemsEntity> bsbomentlist = new List<BS_BOMItemsEntity>();
                    var query = _BOMItemsBLL.Get_ExpressionList(t => t.MaterialCode == entity.MaterialCode);
                    bsbomentlist = query.ToList();
                    if (bsbomentlist.Count > 0)
                    {
                        foreach (var item in bsbomentlist)
                        {
                            item.MaterialName = entity.MaterialName;
                        }
                        _BOMItemsBLL.SaveEntity_List(true, userCode, bsbomentlist, out msg);
                    }

                    //保存到原材料库存表MM_RawMaterialStock
                    List<MM_RawMaterialStockEntity> RawMaterialStocklist = new List<MM_RawMaterialStockEntity>();
                    RawMaterialStocklist = _RawMaterialStockBLL.Get_ExpressionList(t => t.MaterialCode == entity.MaterialCode).ToList();
                    if (RawMaterialStocklist.Count > 0)
                    {
                        foreach (var item in RawMaterialStocklist)
                        {
                            item.MaterialName = entity.MaterialName;
                        }
                        _RawMaterialStockBLL.SaveEntity_List(true, userCode, RawMaterialStocklist, out msg);
                    }
                }
                else
                {
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                }

                int isok = _MaterialBLL.SaveEntity(keyValue, entity, out msg);

                //_MaterialFacetBLL.RemoveForm(t => t.MaterialId == entity.Id);
                //list.ForEach(t =>
                //{
                //    t.MaterialId = entity.Id;
                //    _MaterialFacetBLL.SaveEntity(null, t, out msg);
                //});
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");//操作成功
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchBase_Material")]
        public HttpResponseMessage SaveBatchBase_Material(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_8");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_14");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_15");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_10");//缺少Entity参数！
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
                var old_entity_list = _MaterialBLL.GetList("", out msg);
                //插入数组
                List<Base_MaterialEntity> Insert_entity_list = new List<Base_MaterialEntity>();
                //更新数组
                List<Base_MaterialEntity> Update_entity_list = new List<Base_MaterialEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        Base_MaterialEntity entity = new Base_MaterialEntity();
                        //物料编码
                        entity.MaterialCode = item[Language.GetText("Material.Base_MaterialController.Tips_16")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_16")];//物料编码
                        entity.MaterialCode = entity.MaterialCode.ToUpper();
                        //物料名称
                        entity.MaterialName = item[Language.GetText("Material.Base_MaterialController.Tips_17")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_17")];//物料名称
                        //规格型号
                        entity.Spec = item[Language.GetText("Material.Base_MaterialController.Tips_18")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_18")];//规格型号
                        //物料分类
                        entity.MaterialClass = item[Language.GetText("Material.Base_MaterialController.Tips_19")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_19")];//物料分类
                        //物料小类
                        entity.SmallClass = item[Language.GetText("Material.Base_MaterialController.Tips_20")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_20")];//物料小类
                        //单位
                        entity.Unit = item[Language.GetText("Material.Base_MaterialController.Tips_21")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_21")];//单位
                                                                                                                                                                                           //库存地点

                        entity.Creator = item[Language.GetText("Material.Base_MaterialController.Tips_22")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_22")];//创建人
                        //创建时间
                        entity.CreateTime = item[Language.GetText("Material.Base_MaterialController.Tips_23")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_23")];//创建时间
                        //最后修改人
                        entity.ModifyBy = item[Language.GetText("Material.Base_MaterialController.Tips_24")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_24")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[Language.GetText("Material.Base_MaterialController.Tips_25")] == null ? "" : item[Language.GetText("Material.Base_MaterialController.Tips_25")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTimeOffset.Now;
                        //是否删除

                        entity.Creator = CreatedByCode;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        Base_MaterialEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
                    isok = _MaterialBLL.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = _MaterialBLL.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteBase_Material")]
        public HttpResponseMessage DeleteBase_Material(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_8");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_10");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                Base_MaterialEntity entity = JsonConvert.DeserializeObject<Base_MaterialEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                Base_MaterialEntity model = _MaterialBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = _MaterialBLL.DeleteEntity(Id, out msg, entity.Creator);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_28");//删除操作成功
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveBase_Material")]
        public HttpResponseMessage RemoveBase_Material(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_8");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_10");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层

                Base_MaterialEntity entity = JsonConvert.DeserializeObject<Base_MaterialEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                string MaterialCode = entity.MaterialCode;
                Base_MaterialEntity model = _MaterialBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                BS_BOMEntity bomentity = _BS_BOMBLL.Get_ExpressionEntity(t => t.MaterialCode == MaterialCode);
                if (bomentity != null)
                {
                    result.success = false;
                    result.returnMsg = MaterialCode + " 在BOM中存在关联数据，无法删除";//在BOM中存在关联数据，无法删除
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                List<BS_BOMItemsEntity> bomitementitylist = _BOMItemsBLL.Get_ExpressionList(t => t.MaterialCode == MaterialCode).ToList();
                if (bomitementitylist.Count() > 0)
                {
                    result.success = false;
                    result.returnMsg = MaterialCode + " 在BOM详情中存在关联数据，无法删除";//在BOM详情中存在关联数据，无法删除
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                PL_BOMEntity pl_bomentity = _PL_BOMBLL.Get_ExpressionEntity(t => t.MaterialCode == MaterialCode);
                if (pl_bomentity != null)
                {
                    result.success = false;
                    result.returnMsg = MaterialCode + " 在工单BOM中存在关联数据，无法删除";//在工单BOM中存在关联数据，无法删除
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                List<PL_BOMItemsEntity> pl_bomitemslist = _PL_BOMItemsBLL.Get_ExpressionList(t => t.MaterialCode == MaterialCode).ToList();

                if (pl_bomitemslist.Count() > 0)
                {
                    result.success = false;
                    result.returnMsg = MaterialCode + " 在工单BOM详情中存在关联数据，无法删除";//在工单BOM详情中存在关联数据，无法删除
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //删除
                //_MaterialFacetBLL.RemoveForm(t => t.MaterialId == Id);
                _MaterialFactoryBLL.RemoveForm(t => t.MaterialCode == Id);
                int isok = _MaterialBLL.RemoveForm(Id, entity.Creator);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_28");//删除操作成功
                else
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_34");//删除操作失败
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _MaterialBLL.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_35");//获取详情数据成功
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>Base_MaterialEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层

                var data = _MaterialBLL.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_35");//获取详情数据成功
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
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

                var data = _MaterialBLL.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
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
                var list = _MaterialBLL.GetList_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
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
                var list = _MaterialBLL.GetDataTable_TestOtherEntity(checkType, out OutMes);
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
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("Base_Material_export")]
        public HttpResponseMessage Base_Material_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("Material.Base_MaterialController.Tips_10");//缺少Entity参数！
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
                var data = _MaterialBLL.GetList_export(CreatedByCode, out msg);

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
        /// <summary>
        /// 模糊查询物料规格
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDimMaterialSpec")]
        public HttpResponseMessage GetDimMaterialSpec(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var entity = JsonConvert.DeserializeObject<Base_MaterialEntity>(getValue(jo, "Entity"));

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = _MaterialBLL.GetDimMaterialSpec(entity);

                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 模糊查询物料规格
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetBaseMaterialList")]
        public HttpResponseMessage GetBaseMaterialList(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var queryJson = getValue(jo, "queryJson");
                var data = _MaterialBLL.GetBaseMaterialList(queryJson);

                result.resultData = new
                {
                    rows = data,
                    records = data.Count
                };
                result.success = true;
                result.returnMsg = Language.GetText("Common.Success");
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;
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
            List<Base_MaterialFacetEntity> materialFacetList = new List<Base_MaterialFacetEntity>();
            try
            {

                var data1 = JsonConvert.DeserializeObject<List<Base_MaterialEntity>>(getValue(jo, "data1"));
                //var data2 = JsonConvert.DeserializeObject<List<Base_MaterialFacetEntity>>(getValue(jo, "data2"));
                //var data3 = JsonConvert.DeserializeObject<List<Base_MaterialFactoryEntity>>(getValue(jo, "data3"));

                //所有数据字典
                //MaterialType
                //MaterialSmall
                //Unit
                var vList = _dataItemBLL.GetVDataDictionaryModelList();

                foreach (var item in data1)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.MaterialClass = vList.Find(t => t.EnCode == "MaterialType" && t.ItemName == item.MaterialClass)?.ItemValue;
                    item.SmallClass = vList.Find(t => t.EnCode == "MaterialSmall" && t.ItemName == item.SmallClass)?.ItemValue;
                    item.Unit = vList.Find(t => t.EnCode == "Unit" && t.ItemName == item.UnitName)?.ItemValue;
                    item.UnitName = vList.Find(t => t.EnCode == "Unit" && t.ItemName == item.UnitName)?.ItemName;
                    item.IsEnabled = true;
                    item.Creator = userCode;
                    item.CreateTime = time;
                    item.IsDeleted = false;
                    item.MaterialCode = item.MaterialCode.Trim().ToUpper();
                }

                var group = data1.GroupBy(t => new { t.MaterialCode }).ToList();
                if (group.Count != data1.Count)
                {
                    foreach (var item in group)
                    {
                        if (item.Count() > 1)
                        {
                            msg += item.Key.MaterialCode + ",";
                        }
                    }
                    result.success = false;
                    result.returnMsg = msg + Language.GetText("Material.Base_MaterialController.Tips_39");//物料重复！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var query = from t in data1
                            join m in _MaterialBLL.Get_ExpressionList(t => true) on t.MaterialCode equals m.MaterialCode
                            select t;
                if (query.ToList().Count > 0)
                {
                    result.success = false;
                    result.returnMsg = string.Join(",", query.ToList().Select(t => t.MaterialCode)) + Language.GetText("Material.Base_MaterialController.Tips_40");//数据重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }



                //foreach (var item in data2)
                //{
                //    item.Id = Guid.NewGuid().ToString();
                //    item.MaterialId = data1.Find(t => t.MaterialCode == item.MaterialCode).Id;
                //    item.AttrType = vList.Find(t => t.EnCode == "AttrType" && t.ItemName == item.AttrType)?.ItemValue;
                //    PubFunction.RemoveAttribute(item);
                //}

                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    if (data1.Count > 0) _MaterialBLL.SaveEntity_List(false, userCode, data1, out msg);
                    //if (data2.Count > 0) _MaterialFacetBLL.SaveEntity_List(false, userCode, data2, out msg);
                    ts.Complete();
                }

                result.success = true;
                result.resultData = null;
                result.returnMsg = Language.GetText("Common.Success");
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
