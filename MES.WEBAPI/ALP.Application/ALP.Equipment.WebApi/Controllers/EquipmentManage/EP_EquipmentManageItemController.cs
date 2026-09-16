using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.EquipManage;
using ALP.Application.Service.EquipManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Entity.FileManage;
using System.IO;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;

namespace ALP.Application.WebApi.Controllers.EquipManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-12
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentManageController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 设备台账
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("EP_EquipmentManageItem")]
    public class EP_EquipmentManageItemController : ApiBaseController
    { 
        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        
        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentManageItemPageList")]
        public HttpResponseMessage EP_EquipmentManageItemPageList(JObject jo)
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
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                var data = _Service.GetPageList(pagination, queryJson);
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("EP_EquipmentManageItemPageDataTableList")]
        public HttpResponseMessage EP_EquipmentManageItemPageDataTableList(JObject jo)
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
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEP_EquipmentManageItemList")]
        public HttpResponseMessage GetEP_EquipmentManageItemList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveEP_EquipmentManageItem")]
        public HttpResponseMessage SaveEP_EquipmentManageItem(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            int isok = 0;
            string msg = "";
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_7");//缺少Entity参数！
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
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                //参数转实体
                EP_EquipmentManageItemEntity entity = JsonConvert.DeserializeObject<EP_EquipmentManageItemEntity>(getValue(jo, "Entity"));
                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");
                string username = getValue(jo, "userName");
                var imgList = JsonConvert.DeserializeObject<List<Base_ImagesEntity>>(getValue(jo, "imgListEntity"));

               

                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyTime = DateTime.Now;
                    isok = _Service.SaveEntity(keyValue, entity, out msg);
                }
                else
                {
                    var en = _Service.GetListBytime(entity.EMId, out msg);
                    if (en.Count()>0 && en!=null)
                    {
                        foreach (var item in en)
                        {
                          string eq= item.EquipCode;

                            int index = eq.LastIndexOf('_');
                            //从下一个索引开始截取
                             string num= eq.Substring(index + 1);
                            int a = int.Parse(num);
                            string b = "_";
                            a = a+1;
                            string equipcode = entity.EquipCode;
                            entity.EquipCode = equipcode + b + a.ToString();

                        }
                    }
                    else
                    {
                        int a = 1;
                        string b = "_";
                        string equipcode = entity.EquipCode;
                        entity.EquipCode = equipcode + b + a.ToString();
                    }
                   
                     

                    var ent = _Service.Get_ExpressionEntity(t => t.EquipCode == entity.EquipCode);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_8");//配套设备编码重复
                        result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.Creator = userCode;
                    entity.CreateTime = DateTime.Now;
                    entity.Id = Guid.NewGuid().ToString();
                    if (imgList.Count > 0)
                    {
                       // entity.Attachment = imgList.Count+"";
                        foreach (var item in imgList)
                        {
                            item.Create();
                            item.ParentId = entity.Id;
                            item.Creator = userCode;
                        }
                    }

                    isok = _Service.SaveEntity(null, entity, out msg);
                    if (imgList.Count > 0) new Base_Images_Service().SaveEntity_List(false, userCode, imgList, out msg);

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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchEP_EquipmentManageItem")]
        public HttpResponseMessage SaveBatchEP_EquipmentManageItem(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_11");//缺少CreatedByCode参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
            result.success = false;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_12");//缺少CreatedByName参数！
            return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));
                
                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");
                
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = _Service.GetList("", out msg);
                //插入数组
                List<EP_EquipmentManageItemEntity> Insert_entity_list = new List<EP_EquipmentManageItemEntity>();
                //更新数组
                List<EP_EquipmentManageItemEntity> Update_entity_list = new List<EP_EquipmentManageItemEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        EP_EquipmentManageItemEntity entity = new EP_EquipmentManageItemEntity();
                        //设备台账Id
                        entity.EMId =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_13")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_13")];//设备台账Id
                        //配套设备编码
                        entity.EquipCode =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_14")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_14")];//配套设备编码
                        //配套设备名称
                        entity.EquipName =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_15")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_15")];//配套设备名称
                        //设备状态
                        entity.EquipStatus =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_16")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_16")];//设备状态
                        //规格型号
                        entity.EquipSpec =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_17")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_17")];//规格型号
                        //生产厂家
                        entity.Manufacturer =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_18")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_18")];//生产厂家
                        //出厂日期
                        entity.ProducedDate =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_19")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_19")];//出厂日期
                        //使用日期
                        entity.UserDate =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_20")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_20")];//使用日期
                        //备注
                        entity.Remark =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_21")];//备注
                        //附件
                        entity.Attachment =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_22")];//附件
                        //创建人
                        entity.Creator =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_23")];//创建人
                        //创建时间
                        entity.CreateTime = item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_24")];//创建时间
                        //修改人
                        entity.ModfiyBy =  item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_25")];//修改人
                       
                        
                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        EP_EquipmentManageItemEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteEP_EquipmentManageItem")]
        public HttpResponseMessage DeleteEP_EquipmentManageItem(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                EP_EquipmentManageItemEntity entity = JsonConvert.DeserializeObject<EP_EquipmentManageItemEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentManageItemEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                string msg = "";
                int isok = _Service.DeleteEntity(Id, out msg, userCode);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_28");//删除操作成功
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveEP_EquipmentManageItem")]
        public HttpResponseMessage RemoveEP_EquipmentManageItem(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            
            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;
                
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                EP_EquipmentManageItemEntity entity = JsonConvert.DeserializeObject<EP_EquipmentManageItemEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                EP_EquipmentManageItemEntity model = _Service.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                //删除
                int isok = _Service.RemoveForm(Id, userName);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_28");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_30");//删除操作失败
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentManageItemEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                var data = _Service.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_31");//获取详情数据成功
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>EP_EquipmentManageItemEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();
            
            try
            {
                //业务服务层
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                var data = _Service.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_31");//获取详情数据成功
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
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
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
                var data = _Service.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 ).OrderByDescending(t => t.Id).ToList();
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("EP_EquipmentManageItem_export")]
        public HttpResponseMessage EP_EquipmentManageItem_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipManage.EP_EquipmentManageItemController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                
                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();
                
                EP_EquipmentManageItem_Service _Service = new EP_EquipmentManageItem_Service();
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
