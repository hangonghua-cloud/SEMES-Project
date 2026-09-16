using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ALP.WebApi.Controllers.SystemManage
{
    public class TreeGridEntity2
    {
        public string parentId { get; set; }
        public string id { get; set; }
        public string text { get; set; }
        public DataItemDetailEntity entityJson { get; set; }
        public bool expanded { get; set; }
        public bool hasChildren { get; set; }
    }
    //[Auth]
    [RoutePrefix("SystemManage/DataItemDetail")]
    public class DataItemDetailController : ApiBaseController
    {
        private DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();
        //private DataItemCache dataItemCache = new DataItemCache();
        private DataItemBLL dataItemBLL = new DataItemBLL();
        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [Route("GetTreeListJson")]
        public HttpResponseMessage GetTreeListJson(string itemId, string condition = "", string keyword = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemDetailBLL.GetList(itemId).ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    #region 多条件查询
                    switch (condition)
                    {
                        case "ItemName":        //项目名
                            data = data.TreeWhere(t => t.ItemName.Contains(keyword), "ItemDetailId");
                            break;
                        case "ItemValue":      //项目值
                            data = data.TreeWhere(t => t.ItemValue.Contains(keyword), "ItemDetailId");
                            break;
                        case "SimpleSpelling": //拼音
                            data = data.TreeWhere(t => t.SimpleSpelling.Contains(keyword), "ItemDetailId");
                            break;
                        default:
                            break;
                    }
                    #endregion
                }
                var TreeList = new List<TreeGridEntity2>();
                foreach (DataItemDetailEntity item in data)
                {
                    TreeGridEntity2 tree = new TreeGridEntity2();
                    bool hasChildren = data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
                    tree.id = item.ItemDetailId;
                    tree.parentId = item.ParentId;
                    tree.expanded = true;
                    tree.hasChildren = hasChildren;
                    tree.entityJson = item;
                    TreeList.Add(tree);
                }

                //string str ="{"+ TreeList.ToJson()+"}";

                //JObject respObj = (JObject)JsonConvert.DeserializeObject(str);
                result.resultData = TreeList;// (JObject)JsonConvert.DeserializeObject(str);
                result.success = TreeList.Count > 0 ? true : false;
                result.returnMsg = TreeList.Count > 0 ? "获取数据字典详情列表成功" : "数据字典详情中查无[keyword=" + keyword + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_1");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //WriteLog("ex.Message:   " + ex.Message);
                //return HandleException(ex, ex.Message.ToString());
                result.resultData = new TreeGridEntity();
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_2") + ex.Message;//获取数据字典详情数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        ///// <summary>
        ///// 明细实体 
        ///// </summary>
        ///// <param name="keyValue">主键值</param>
        ///// <returns>返回对象Json</returns>
        //[HttpGet]
        //[Route("GetTreeJson")]
        //public ActionResult GetFormJson(string keyValue)
        //{
        //    var data = dataItemDetailBLL.GetEntity(keyValue);
        //    return Content(data.ToJson());
        //}
        ///// <summary>
        ///// 获取数据字典列表（绑定控件）
        ///// </summary>
        ///// <param name="EnCode">代码</param>
        ///// <returns>返回列表树Json</returns>
        //[HttpGet]
        //[Route("GetTreeJson")]
        //public ActionResult GetDataItemTreeJson(string EnCode)
        //{
        //    var data = dataItemCache.GetDataItemList(EnCode);
        //    var treeList = new List<TreeEntity>();
        //    foreach (DataItemModel item in data)
        //    {
        //        TreeEntity tree = new TreeEntity();
        //        bool hasChildren = data.Count(t => t.ParentId == item.ItemDetailId) == 0 ? false : true;
        //        tree.id = item.ItemDetailId;
        //        tree.text = item.ItemName;
        //        tree.value = item.ItemValue;
        //        tree.parentId = item.ParentId;
        //        tree.isexpand = true;
        //        tree.complete = true;
        //        tree.hasChildren = hasChildren;
        //        treeList.Add(tree);
        //    }
        //    return Content(treeList.TreeToJson());
        //}
        /// <summary>
        /// 获取数据字典列表（绑定控件）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDataItemListJson")]
        public HttpResponseMessage GetDataItemListJson(string EnCode, string Name = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemDetailBLL.GetDataItemList(EnCode, Name).ToList();

                result.resultData = data;
                result.success = data.Count > 0 ? true : false;
                result.returnMsg = data.Count > 0 ? "获取数据字典详情列表成功" : "数据字典详情中查无[EnCode=" + EnCode + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_3");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.resultData = null;
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_2") + ex.Message;//获取数据字典详情数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        /// <summary>
        /// 获取数据字典列表（绑定控件专用）
        /// </summary>
        /// <param name="EnCode">代码</param>
        /// <param name="Name"></param>
        /// <param name="modal"></param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDataItemListJson_UA")]
        public HttpResponseMessage GetDataItemListJson_UA(string EnCode, string Name = "", string modal = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                List<DataItemModel> data = dataItemDetailBLL.GetDataItemList_UA(EnCode, Name).ToList();

                List<DataItemModel> list = new List<DataItemModel>();
                if (modal != "1")//弹框
                {
                    DataItemModel item = new DataItemModel();
                    item.ItemName = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_4");//--请选择--
                    item.ItemValue = "";
                    list.Add(item);
                }
                list.AddRange(data);

                result.resultData = list;
                result.success = data.Count > 0 ? true : false;
                result.returnMsg = data.Count > 0 ? "获取数据字典详情列表成功" : "数据字典详情中查无[EnCode=" + EnCode + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_3");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.resultData = null;
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_2") + ex.Message;//获取数据字典详情数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        /// <summary>
        /// 获取字典列表子明细-PDA专用
        /// </summary>
        /// <returns>返回列表Json</returns>
        [HttpPost]
        [Route("GetList_DataItemByFather_PDA")]
        public HttpResponseMessage GetList_DataItemByFather_PDA(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var encode = getValue(jo, "EnCode");
                var remark1 = getValue(jo, "Remark1");
                result.success = true;
                result.resultData = dataItemDetailBLL.GetList_DataItemByFather_PDA(encode, remark1);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 获取数据库中表中的信息
        /// </summary>
        /// <param name="tableName">表名</param> 
        /// <param name="codeName">字段名称</param> 
        /// <param name="codeValue">字段值</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        [Route("GetDataTableList")]
        public HttpResponseMessage GetDataTableList(string tableName, string codeName = "", string codeValue = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemDetailBLL.GetDataTableList(tableName, codeName, codeValue);

                if (data == null || data.Rows.Count < 1)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_5");//数据库中查不到相关的数据
                }
                else
                {
                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_6");//获取数据信息成功
                }

                result.resultData = data;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.resultData = null;
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_7") + ex.Message;//获取数据信息失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }


        /// <summary>
        /// 获取质量检验结果字典，通过备注来区分不同的检验类型，gx工序检验、cp产品检验、fh发货检验、th退货检验、sctl委外加工、ir检验结果 WWJG委托加工 wh 库存检验
        /// </summary>
        /// <param name="EnCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetQCDataItemListJson")]
        public HttpResponseMessage GetQCDataItemListJson(string EnCode)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemDetailBLL.GetDataItemList("InspectionResultCollect").ToList();
                List<DataItemModel> lst = new List<DataItemModel>();

                foreach (var item in data)
                {
                    if (EnCode == "ProcessJudgeResult" && item.Description.ToUpper().Contains("GX"))
                        lst.Add(item);
                    else if (EnCode == "ProductJudgeResult" && item.Description.ToUpper().Contains("CP"))
                        lst.Add(item);
                    else if (EnCode == "DeliveryJudgeResult" && item.Description.ToUpper().Contains("FH"))
                        lst.Add(item);
                    else if (EnCode == "ReturnJudgeResult" && item.Description.ToUpper().Contains("TH"))
                        lst.Add(item);
                    else if (EnCode == "ProductionReturnJudgeResult" && item.Description.ToUpper().Contains("SCTL"))
                        lst.Add(item);
                    else if (EnCode == "DeleteProcessJudgeResult" && item.Description.ToUpper().Contains("WWJG"))
                        lst.Add(item);
                    else if (EnCode == "WarehouseResult" && item.Description.ToUpper().Contains("WH"))
                        lst.Add(item);
                    else if (EnCode == "InspectionResult" && item.Description.ToUpper().Contains("IR"))
                        lst.Add(item);
                }

                result.resultData = lst;
                result.success = data.Count > 0 ? true : false;
                result.returnMsg = data.Count > 0 ? "获取数据字典详情列表成功" : "数据字典详情中查无[EnCode=" + EnCode + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_3");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.resultData = null;
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_2") + ex.Message;//获取数据字典详情数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }

        ///// <summary>
        ///// 获取数据字典子列表（绑定控件）
        ///// </summary>
        ///// <param name="EnCode">代码</param>
        ///// <param name="ItemValue">项目值</param>
        ///// <returns>返回列表Json</returns>
        //[Route("GetTreeJson")]
        //public ActionResult GetSubDataItemListJson(string EnCode, string ItemValue)
        //{
        //    var data = dataItemCache.GetSubDataItemList(EnCode, ItemValue);
        //    return Content(data.ToJson());
        //}

        ///// <summary>
        ///// 获取数据字典列表（绑定控件）
        ///// </summary>
        ///// <param name="EnCode">代码</param>
        ///// <returns>返回列表Json</returns>
        //[HttpGet]
        //[Route("GetDataItemListSelect")]
        //public ActionResult GetDataItemListSelect(string EnCode)
        //{
        //    StringBuilder searchsb = new StringBuilder();
        //    var data = dataItemCache.GetDataItemList(EnCode).ToList();
        //    searchsb.Append("<select><option value = '-1000'>请选择</option>");
        //    foreach (var item in data)
        //    {
        //        searchsb.Append(string.Format("<option value = '{0}'>{1}</option>", item.ItemValue, item.ItemName));
        //    }
        //    searchsb.Append("</select>");
        //    return Content(searchsb.ToString());
        //}
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string keyValue = getValue(jo, "keyValue");
                string username = getValue(jo, "username");
                if (string.IsNullOrEmpty(username))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_8");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    dataItemDetailBLL.RemoveForm(keyValue);

                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">字典项实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                string keyValue = getValue(jo, "keyValue");
                string username = getValue(jo, "username");
                if (string.IsNullOrEmpty(username))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_8");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    DataItemDetailEntity dataItemDetailEntity = JsonConvert.DeserializeObject<DataItemDetailEntity>(getValue(jo, "DataItemDetailEntity"));
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        dataItemDetailEntity.ModifyUserName = username;
                    }
                    else
                    {
                        dataItemDetailEntity.CreateUserName = username;
                    }
                    int returnResult = dataItemDetailBLL.SaveForm(keyValue, dataItemDetailEntity);
                    switch (returnResult)
                    {
                        case 1:
                            result.success = true;
                            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_11");//添加成功
                            break;
                        case 2:
                            result.success = true;
                            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_12");//修改成功
                            break;
                        case 3:
                            result.success = false;
                            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemDetailController.Tips_13");//添加失败，字典明细数据编码重复
                            break;
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion
    }
}
