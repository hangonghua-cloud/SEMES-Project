using System;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.BaseManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Busines.SystemManage;
using System.Web.Http;
using System.Net.Http;

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 报表配置
    /// </summary>
    [Auth]
    [RoutePrefix("BaseReport")]
    public class BaseReportController : ApiBaseController
    {


        #region 实例化
        private Base_ReportRecordBLL _ReportRecordBLL = new Base_ReportRecordBLL();
        private Sys_PersonsBLL _UserBLL = new Sys_PersonsBLL();
        private Base_ReportRoleAuthorizeBLL _ReportRoleAuthorizeBLL = new Base_ReportRoleAuthorizeBLL();
        private Base_ReportRoleBLL _ReportRoleBLL = new Base_ReportRoleBLL();
        #endregion

        #region 获取报表树结构 --权限
        /// <summary>
        /// 获取报表树结构
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportTree")]
        public HttpResponseMessage GetReportTree(JObject json)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            try
            {
                var treeList = new List<TreeEntity>();
 
                //2.获取用户
                var entity = _UserBLL.GetEntity(t => t.Code == userCode);
               if (entity == null)
                {
                    result.resultData = treeList;
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportController.Tips_1");//用户不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //1.所有显示的菜单
                var data = _ReportRecordBLL.GetList(t=>t.EnabledMark=="0").OrderBy(t => t.SortCode).ToList();

                //1.1 管理员 直接随便看 Job_ID=1 超级管理员
                var authdata1 = new List<Base_ReportRoleAuthorizeEntity>();
                if (entity.Job_ID != "1")
                {
                    // 2.获取用户的分配的角色
                    var authRole = _ReportRoleBLL.GetList(t => t.UserCode == userCode).ToList();
                    //3.获取用户的权限
                    var authQuery = from t in _ReportRoleAuthorizeBLL.GetList(t => true)
                                    join r in authRole.Select(t => t.GroupId).ToList() on t.GroupId equals r
                                    select t;
                    var authData = authQuery.ToList();
                    if (authData.Count < 1)
                    {
                        result.resultData = treeList;
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportController.Tips_2");//用户权限不存在
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    authdata1.AddRange(authData);
                    //4.处理没有父级元素的权限 递归添加
                    foreach (var item in authData)
                    {
                        if (data.Find(t => t.Id == item.ReportId) != null)
                        {
                            Recursion(authdata1, data, item.ReportId);
                        }
                    }
                }

                foreach (var item in data)
                {
                    if (entity.Job_ID !="1" && authdata1.Find(t => t.ReportId == item.Id) == null ) continue;
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.ReportName;
                    tree.value = item.Address;
                    tree.parentId = item.ParentId;
                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    tree.AttributeValue = "";
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

        //递归添加父级菜单
        private void Recursion(List<Base_ReportRoleAuthorizeEntity> authData, List<Base_ReportRecordEntity> data, string parentId)
        {
            if (parentId == "0") return;
            else
            {
                if (authData.Find(t => t.ReportId == parentId) == null)
                {
                    authData.Add(new Base_ReportRoleAuthorizeEntity() { ReportId = parentId });
                }
                parentId = data.Find(t => t.Id == parentId).ParentId;
                Recursion(authData, data, parentId);
            }
        }


        #endregion

        #region 获取所有节点下拉框菜单
        /// <summary>
        /// 获取所有节点下拉框
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportSelect")]
        public HttpResponseMessage GetReportSelect()
        {
            var result = new ResponseResult();
            var data = _ReportRecordBLL.GetList("").OrderBy(t => t.SortCode).ToList();
            //var list = new List<VBaseDictionary>();
            //foreach (var item in data.Where(t => t.Superior == 0 && t.EnabledMark == 0).ToList())
            //{
            //    var dic = new VBaseDictionary();
            //    dic.ItemCode = item.Id;
            //    dic.ItemName = item.ReportName;
            //    list.Add(dic);
            //}
            result.resultData = data;
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }
        #endregion

        #region 获取当前节点信息
        /// <summary>
        /// 获取当前节点信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(Base_ReportRecordEntity entity)
        {
            var result = new ResponseResult();
           
            try
            { 
                result.resultData = _ReportRecordBLL.GetEntity(entity.Id);
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

        #region 创建修改节点
        /// <summary>
        /// 创建修改节点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var keyValue = getValue(jo, "KeyValue");
                var entity = JsonConvert.DeserializeObject<Base_ReportRecordEntity>(getValue(jo, "Entity"));
                var time = DateTime.Now;
                if (string.IsNullOrEmpty(keyValue))
                {
                    //校验名称是否相同
                    var ent = _ReportRecordBLL.GetEntity(t => t.ReportName == entity.ReportName);
                    if (ent != null)
                    { 
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportController.Tips_6");//存在相同名称菜单或者报表
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }

                    entity.Creator = CurrentAccount.UserCode;
                    entity.CreateTime = time;
                   
                }
                else
                {
                    var ent = _ReportRecordBLL.GetEntity(t => t.ReportName == entity.ReportName && t.Id != entity.Id);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("BaseManage.BaseReportController.Tips_6");//存在相同名称菜单或者报表
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.ModifyBy = CurrentAccount.UserCode;
                    entity.ModifyTime = time; 
                }
                _ReportRecordBLL.SaveForm(keyValue, entity);
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

        #region 删除节点
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(Base_ReportRecordEntity entity)
        {
            var result = new ResponseResult();
            try
            {
                _ReportRecordBLL.RemoveForm(entity.Id); 
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

        #region 分页获取数据
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetListWithPage")]
        public HttpResponseMessage GetListWithPage(JObject json)
        {
            var result = new ResponseResult();
            Pagination pagination = new Pagination();
            if (!json["pagination"].IsEmpty())
            {
                pagination = JsonConvert.DeserializeObject<Pagination>(getValue(json, "pagination"));
            }
            else
            {
                pagination = null;
                //result.success = false;
                //result.returnMsg = "分页参数Pagination不能为空！";
                //return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            var queryJson = getValue(json, "queryJson");
            try
            {
                var data = new
                {
                    rows = _ReportRecordBLL.GetListWithPage(pagination, queryJson),
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

        #region 获取树形菜单
        /// <summary>
        /// GetReportMeun
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetReportMeun")]
        public HttpResponseMessage GetReportMeun(JObject jo)
        {
            var result = new ResponseResult();
            try
            { 
                var data = _ReportRecordBLL.GetList("").OrderBy(t => t.SortCode).ToList();
                data = data.Where(t => t.EnabledMark == "0" && t.Superior == 0).ToList();
                var treeList = new List<TreeEntity>();
                foreach (var item in data)
                {
                    TreeEntity tree = new TreeEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.ReportName;
                    tree.value = item.Address;
                    tree.parentId = item.ParentId;
                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    tree.AttributeValue = "";
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


    }
}
