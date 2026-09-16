using ALP.Application.Busines.BaseManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util.Extension;
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

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 报表权限
    /// </summary>
    [Auth]
    [RoutePrefix("BaseReportRole")]
    public class BaseReportRoleController : ApiBaseController
    {

        private Base_ReportRoleBLL _ReportRoleBLL = new Base_ReportRoleBLL();
        private Base_ReportRoleAuthorizeBLL _ReportRoleAuthorizeBLL = new Base_ReportRoleAuthorizeBLL();
        private Base_ReportRecordBLL _ReportRecordBLL = new Base_ReportRecordBLL();


        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetListWithPage(JObject jo)
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
                var queryJson = getValue(jo, "queryJson");
                var data = new
                {
                    rows = _ReportRoleBLL.GetListWithPage(pagination, queryJson),
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records
                };
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 获取Tree
        /// <summary>
        /// 获取tree
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetAllMenu([FromUri] string keyword = "")
        {
            var result = new ResponseResult();
            var data = _ReportRecordBLL.GetList("").ToList();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = data.TreeWhere(t => t.ReportName.Contains(keyword), "").ToList();
            }
            data = data.Where(t => t.EnabledMark == "0").OrderBy(t => t.SortCode).ToList();
            var treeList = new List<TreeEntity>();

            foreach (Base_ReportRecordEntity item in data)
            {
                TreeEntity tree = new TreeEntity();
                bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                tree.id = item.Id;
                tree.text = item.ReportName;
                //tree.value = item.ItemCode;
                tree.parentId = item.ParentId;
                tree.isexpand = false;
                tree.complete = true;
                tree.Attribute = "isTree";
                //tree.AttributeValue = item.IsTree.ToString();
                tree.hasChildren = hasChildren;
                treeList.Add(tree);
            }
            result.resultData = data;
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 获取菜单下拉框，带选中的
        /// <summary>
        /// 获取菜单下拉框，带选中的
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetAllMenuWithChecked")]
        public HttpResponseMessage GetAllMenuWithChecked(Base_ReportRoleEntity entity)
        {
            var result = new ResponseResult();
            try
            {
                var data = _ReportRecordBLL.GetList("").ToList();
                var roleAuthorizedata = _ReportRoleAuthorizeBLL.GetList(t => t.GroupId == entity.Id).ToList();
                if (!string.IsNullOrEmpty(""))
                {
                    data = data.TreeWhere(t => t.ReportName.Contains(""), "").ToList();
                }
                data = data.Where(t => t.EnabledMark == "0").OrderBy(t => t.SortCode).ToList();
                var treeList = new List<TreeEntity>();

                foreach (Base_ReportRecordEntity item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.ParentId) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.ReportName;
                    tree.checkstate = roleAuthorizedata.Find(t => t.ReportId == item.Id) != null && item.Superior == 1 ? 1 : 0;
                    tree.parentId = item.ParentId;
                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    //tree.AttributeValue = item.IsTree.ToString();
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }
                result.resultData = treeList;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 保存权限
        /// <summary>
        /// 保存权限
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchForm")]
        public HttpResponseMessage SaveBatchForm(JObject json)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            try
            {
                var time = DateTime.Now;
                var groupId = getValue(json, "KeyValue");
                var list = JsonConvert.DeserializeObject<List<Base_ReportRoleAuthorizeEntity>>(getValue(json, "data"));

                  foreach(var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.CreateTime = time;
                    item.Creator = userCode;
                }
 
                _ReportRoleAuthorizeBLL.DeleteForm(t => t.GroupId == groupId);
                _ReportRoleAuthorizeBLL.InsertListEntity(list);

                result.resultData = null;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 保存角色对应的用户
        /// <summary>
        /// 保存用户对应角色
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchRoleUser")]
        public HttpResponseMessage SaveBatchRoleUser(JObject json)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
             
            try
            {
                var time = DateTime.Now;
                var groupId = getValue(json, "KeyValue");
                var list = JsonConvert.DeserializeObject<List<Base_ReportRoleEntity>>(getValue(json, "data"));


                var query = from t in _ReportRoleBLL.GetList(t=>t.GroupId==groupId)
                            join l in list on t.UserCode equals l.UserCode
                            select l;

                var list1 = query.ToList();
                if (list1.Count > 0)
                {
                    result.resultData = null;
                    result.success = false;
                    result.returnMsg = string.Join(",", list1.Select(t => t.UserCode)) + ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportRoleController.Tips_4");//数据重复
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                foreach(var item in list)
                {
                    item.Id = Guid.NewGuid().ToString();
                    item.Creator = userCode;
                    item.GroupId = groupId;
                    item.CreateTime = time;
                    item.EnabledMark = true;
                }

                _ReportRoleBLL.InsertList(list);
               
                result.resultData = null;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 删除记录
        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage RemoveForm(Base_ReportRoleEntity entity)
        {
            var result = new ResponseResult();
            try
            {
                //1.删除Sys_RoleAuthorizeEntity
                //2.删除Sys_RoleEntity
                // _roleAuthorizeBll.DeleteForm(t => t.RoleGuid == entity.RoleGuid);
                _ReportRoleBLL.SaveForm(entity.Id, entity);
                //_roleBll.RemoveForm(entity.RoleGuid);
                result.resultData = null;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 获取角色下拉框
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage GetRoleSelect()
        {
            var result = new ResponseResult();
            try
            {
                var list = _ReportRoleBLL.GetList(t=>true).ToList();
                var data = from item in list
                           select new
                           {
                               ItemCode = item.Id,
                               //ItemName = item.RoleName
                           };
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 获取角色下的用户
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportRoleUserList")]
        public HttpResponseMessage GetReportRoleUserList(JObject jo)
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
                var queryJson = getValue(jo, "queryJson");
                var data = _ReportRoleBLL.GetReportRoleUserList(pagination, queryJson);
                var jsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                };
                result.resultData = jsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion

        #region 获取不在当前组的用户
        /// <summary>
        /// 获取下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserNotInGroup")]
        public HttpResponseMessage GetUserNotInGroup(JObject jo)
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
                var queryJson = getValue(jo, "queryJson");
                var data = _ReportRoleBLL.GetUserNotInGroup(pagination, queryJson);
                var jsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Rows.Count,
                    page = pagination != null ? pagination.page : 1,
                    records = pagination != null ? pagination.records : data.Rows.Count,
                };
                result.resultData = jsonData;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError") + ex.Message;//执行失败
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion
    }
}
