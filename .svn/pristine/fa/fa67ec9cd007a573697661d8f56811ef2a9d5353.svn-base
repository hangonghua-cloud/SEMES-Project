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
    /// 1.创建日期: 2022-12-05
    /// 2.创建作者: jpf
    /// 3.功能描述: MM_WarehouseSafetyStockController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("MM_WarehouseSafetyStock")]
    public class MM_WarehouseSafetyStockController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_WarehouseSafetyStockPageList")]
        public HttpResponseMessage MM_WarehouseSafetyStockPageList(JObject jo)
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
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                var data = _Service.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total: data.Count(),
                    page = pagination != null ? pagination.records: data.Count(),
                    records = pagination != null ? pagination.records: data.Count(),
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("MM_WarehouseSafetyStockPageDataTableList")]
        public HttpResponseMessage MM_WarehouseSafetyStockPageDataTableList(JObject jo)
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
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetMM_WarehouseSafetyStockList")]
        public HttpResponseMessage GetMM_WarehouseSafetyStockList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveMM_WarehouseSafetyStock")]
        public HttpResponseMessage SaveMM_WarehouseSafetyStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_7");//缺少Entity参数！
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
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                //参数转实体
                MM_WarehouseSafetyStockEntity entity = JsonConvert.DeserializeObject<MM_WarehouseSafetyStockEntity>(getValue(jo, "Entity"));
                //工厂编码 是否为空进行判断. 友情提示, 如果第一个是系统内定义编号, 请屏蔽此并参考下边创建的流水号用法
                if (string.IsNullOrEmpty(entity.FactoryCode))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_8");//工厂编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.FactoryName))
                {
                    //工厂名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_9");//工厂名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WhsCode))
                {
                    //仓库编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_10");//仓库编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.WhsName))
                {
                    //仓库名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_11");//仓库名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialCode))
                {
                    //物料编码 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_12");//物料编码不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialName))
                {
                    //物料名称 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_13");//物料名称不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.MaterialSpc))
                {
                    //物料规格 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_14");//物料规格不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.Unit))
                {
                    //单位 是否为空进行判断
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_15");//单位不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                
                if (!string.IsNullOrEmpty(keyValue))
                {
                    
                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_16");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.ModifyName))
                    {
                        //编辑人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_17");//编辑人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
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
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_18");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.CreateName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_19");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: MM_WarehouseSafetyStock	FactoryCode流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("MM_WarehouseSafetyStock", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.FactoryCode = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;
                    
                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    entity.CreateTime = DateTime.Now;
                    //创建时间
                    //entity.CreateTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    entity.IsDelete = false;
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_21");//同一物料在同仓库维护重复
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchMM_WarehouseSafetyStock")]
        public HttpResponseMessage SaveBatchMM_WarehouseSafetyStock(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_23");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_24");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<MM_WarehouseSafetyStockEntity> Insert_entity_list = new List<MM_WarehouseSafetyStockEntity>();
                //更新数组
                List<MM_WarehouseSafetyStockEntity> Update_entity_list = new List<MM_WarehouseSafetyStockEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        MM_WarehouseSafetyStockEntity entity = new MM_WarehouseSafetyStockEntity();
                        //工厂编码
                        entity.FactoryCode =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_25")];//工厂编码
                        //工厂名称
                        entity.FactoryName =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_26")];//工厂名称
                        //仓库编码
                        entity.WhsCode =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_27")];//仓库编码
                        //仓库名称
                        entity.WhsName =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_28")];//仓库名称
                        //物料编码
                        entity.MaterialCode =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_29")];//物料编码
                        //物料名称
                        entity.MaterialName =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_30")];//物料名称
                        //物料规格
                        entity.MaterialSpc =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_31")];//物料规格
                        //单位
                        entity.Unit =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_32")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_32")];//单位
                        //安全库存数
                        entity.SafetyQty =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_33")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_33")];//安全库存数
                        //备注
                        entity.Remark =  item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_34")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_34")];//备注
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        entity.CreateTime = DateTime.Now;
                        //是否删除
                        entity.IsDelete = false;
                        entity.Creator = CreatedByCode;
                        entity.CreateName = CreatedByName;
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        MM_WarehouseSafetyStockEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteMM_WarehouseSafetyStock")]
        public HttpResponseMessage DeleteMM_WarehouseSafetyStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                MM_WarehouseSafetyStockEntity entity = JsonConvert.DeserializeObject<MM_WarehouseSafetyStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_WarehouseSafetyStockEntity model = _Service.GetEntity(Id);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_38");//删除操作成功
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveMM_WarehouseSafetyStock")]
        public HttpResponseMessage RemoveMM_WarehouseSafetyStock(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                MM_WarehouseSafetyStockEntity entity = JsonConvert.DeserializeObject<MM_WarehouseSafetyStockEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                MM_WarehouseSafetyStockEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, entity.ModifyName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_38");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_40");//删除操作失败
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_WarehouseSafetyStockEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_41");//获取详情数据成功
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>MM_WarehouseSafetyStockEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_41");//获取详情数据成功
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
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
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.IsDelete == false).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
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
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("MM_WarehouseSafetyStock_export")]
        public HttpResponseMessage MM_WarehouseSafetyStock_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SAP.MM_WarehouseSafetyStockController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                MM_WarehouseSafetyStock_Service _Service = new MM_WarehouseSafetyStock_Service();
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message; //查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString(); 
                return Request.CreateResponse(HttpStatusCode.OK, result); 
            }
        }
        
        
    }
}
