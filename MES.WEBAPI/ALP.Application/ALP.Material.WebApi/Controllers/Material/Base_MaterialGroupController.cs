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
using ALP.Application.Entity.SystemManage;

namespace ALP.Application.WebApi.Controllers.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-22
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialGroupController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 物料属性模板维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("Base_MaterialGroup")]
    public class Base_MaterialGroupController : ApiBaseController
    {
        private Base_MaterialGroupBLL _MaterialGroupBLL = new Base_MaterialGroupBLL();
        private Base_MaterialGroupBindMaterialBLL _MaterialGroupBindMaterialBLL = new Base_MaterialGroupBindMaterialBLL();
        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败

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
                var data = _MaterialGroupBLL.GetList().ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.GroupName.Contains(keyword), "");
                }
                foreach (Base_MaterialGroupEntity item in data)
                {
                    TreeExtendEntity tree = new TreeExtendEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.text = item.GroupName;
                    tree.value = item.GroupCode;
                    tree.parentId = item.ParentId;

                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    tree.IsDefault = 0;
                    tree.AttributeValue = "";
                    tree.hasChildren = hasChildren;
                    treeList.Add(tree);
                }

                result.resultData = treeList;
                result.success = treeList.Count > 0 ? true : false;
                result.returnMsg = treeList.Count > 0 ? SuccessMsg:FaildMsg;
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //WriteLog("ex.Message:   " + ex.Message);
                //return HandleException(ex, ex.Message.ToString());
                result.resultData = treeList;
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
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
                var data = _MaterialGroupBLL.GetList().ToList();
                if (!string.IsNullOrEmpty(keyword))
                {
                    data = data.TreeWhere(t => t.GroupName.Contains(keyword), "");
                }
                var TreeList = new List<TreeGridEntity>();
                foreach (Base_MaterialGroupEntity item in data)
                {
                    TreeGridEntity tree = new TreeGridEntity();
                    bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
                    tree.id = item.Id;
                    tree.parentId = item.ParentId;
                    tree.expanded = true;
                    tree.hasChildren = hasChildren;
                    tree.entityJson = item.ToJson();
                    TreeList.Add(tree);
                }

                result.resultData = TreeList;
                result.success = TreeList.Count > 0 ? true : false;
                result.returnMsg = TreeList.Count > 0 ? SuccessMsg : FaildMsg;
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
                var data = _MaterialGroupBLL.GetEntity(t=>t.Id==keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupController.Tips_3");//获取详情数据成功
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
                string groupCode = getValue(jo, "GroupCode");
                if (string.IsNullOrEmpty(username))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupController.Tips_4");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    _MaterialGroupBLL.RemoveForm(keyValue);
                    _MaterialGroupBindMaterialBLL.RemoveForm(t => t.GroupCode == groupCode);

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
        /// <param name="Base_MaterialGroupEntity">分类实体</param>
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
                Base_MaterialGroupEntity Base_MaterialGroupEntity = JsonConvert.DeserializeObject<Base_MaterialGroupEntity>(getValue(jo, "Entity"));
                if (!string.IsNullOrEmpty(keyValue))
                {
                    Base_MaterialGroupEntity.ModifyBy = userCode;
                }
                else
                {
                    Base_MaterialGroupEntity.Creator = userCode;
                }
                returnResult = _MaterialGroupBLL.SaveForm(keyValue, Base_MaterialGroupEntity);
                switch (returnResult)
                {
                    case 1:
                        result.success = true;
                        result.returnMsg = SuccessMsg;
                        break;
                    case 2:
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupController.Tips_7");//编码或者名称重复
                        break;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = FaildMsg + ex.Message;
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        #endregion
    }
}
