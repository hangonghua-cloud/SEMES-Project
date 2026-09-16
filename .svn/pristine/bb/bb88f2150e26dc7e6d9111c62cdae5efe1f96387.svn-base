using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ALP.Application.Busines.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ALP.WebApi.Controllers.SystemManage
{
    //[Auth]
    [RoutePrefix("SystemManage/DataItem")]
    public class DataItemController : ApiBaseController
    {
        private DataItemBLL dataItemBLL = new DataItemBLL();
        public DataItemDetailBLL dataItemDetailBLL = new DataItemDetailBLL();

        #region 获取数据
        /// <summary>
        /// 分类列表 
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [Route("GetTreeJson")]
        public HttpResponseMessage GetTreeJson(string keyword = "")
        {
            var result = new ResponseResult();
            result.resultData = null;

            var treeList = new List<TreeEntity>();
            try
            {
                var data = dataItemBLL.GetList().ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.ItemName.Contains(keyword), "");
                }
                foreach (DataItemEntity item in data)
                {
                    TreeExtendEntity tree = new TreeExtendEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.ItemId) == 0 ? false : true;
                    tree.id = item.ItemId;
                    tree.text = item.ItemName;
                    tree.value = item.ItemCode;
                    tree.parentId = item.ParentId;
                    
                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    tree.IsDefault= item.IsDefault;
                    tree.AttributeValue = item.IsTree.ToString();
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }

                result.resultData = treeList;
                result.success = treeList.Count > 0 ? true : false;
                result.returnMsg = treeList.Count > 0 ? "获取数据字典分类成功" : "数据字典分类中查无[keyword=" + keyword + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_1");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //WriteLog("ex.Message:   " + ex.Message);
                //return HandleException(ex, ex.Message.ToString());
                result.resultData = treeList;
                result.success = false;
                result.returnMsg = "获取数据字典分类中[keyword=" + keyword + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_2") + ex.Message;//]的数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形列表Json</returns>
        [HttpGet]
        [Route("GetTreeListJson")]
        public HttpResponseMessage GetTreeListJson(string keyword = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                var data = dataItemBLL.GetList().ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.ItemName.Contains(keyword), "");
                }
                var TreeList = new List<TreeGridEntity>();
                foreach (DataItemEntity item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.ItemId) == 0 ? false : true;
                    tree.id = item.ItemId;
                    tree.parentId = item.ParentId;
                    tree.expanded = true;
                    tree.hasChildren = hasChildren;
                    tree.entityJson = item.ToJson();
                    TreeList.Add(tree);
                }

                result.resultData = TreeList;
                result.success = TreeList.Count > 0 ? true : false;
                result.returnMsg = TreeList.Count > 0 ? "获取数据字典分类成功" : "数据字典分类中查无[keyword=" + keyword + ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_3");//]的数据
                //return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //WriteLog("ex.Message:   " + ex.Message);
                result.success = false;
                result.statusCode = HttpStatusCode.InternalServerError.ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }
        /// <summary>
        /// 分类实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue = "")
        {
            var result = new ResponseResult();
            result.resultData = null;

            try
            {
                var data = dataItemBLL.GetEntity(keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_4");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = HttpStatusCode.InternalServerError.ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPageListByParentJson")]
        public HttpResponseMessage GetPageListByParent(JObject jo)
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
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_5");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = dataItemBLL.GetPageListByParent(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };

                result.resultData = JsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message;//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        #endregion

        #region 验证数据
        ///// <summary>
        ///// 分类编号不能重复
        ///// </summary>
        ///// <param name="ItemCode">编号</param>
        ///// <param name="keyValue">主键</param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult ExistItemCode(string ItemCode, string keyValue)
        //{
        //    bool IsOk = dataItemBLL.ExistItemCode(ItemCode, keyValue);
        //    return Content(IsOk.ToString());
        //}
        ///// <summary>
        ///// 分类名称不能重复
        ///// </summary>
        ///// <param name="ItemName">名称</param>
        ///// <param name="keyValue">主键</param>
        ///// <returns></returns>
        //[HttpGet]
        //public ActionResult ExistItemName(string ItemName, string keyValue)
        //{
        //    bool IsOk = dataItemBLL.ExistItemName(ItemName, keyValue);
        //    return Content(IsOk.ToString());
        //}
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_8");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    dataItemBLL.RemoveForm(keyValue);

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
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            try
            {
                string keyValue = getValue(jo, "keyValue");
                int returnResult = 0;
                DataItemEntity dataItemEntity = JsonConvert.DeserializeObject<DataItemEntity>(getValue(jo, "DataItemEntity"));
                if (!string.IsNullOrEmpty(keyValue))
                {
                    dataItemEntity.ModifyUserName = userName;
                }
                else
                {
                    dataItemEntity.CreateUserName = userName;
                }
                returnResult = dataItemBLL.SaveForm(keyValue, dataItemEntity);
                switch (returnResult)
                {
                    case 1:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_11");//新建成功
                        break;
                    case 2:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_12");//修改成功
                        break;
                    case 3:
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("SystemManage.DataItemController.Tips_13");//字典编码重复
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
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
