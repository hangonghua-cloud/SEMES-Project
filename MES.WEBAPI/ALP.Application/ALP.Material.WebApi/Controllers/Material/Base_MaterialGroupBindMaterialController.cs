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
    /// 1.创建日期: 2021-07-22
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialGroupBindMaterialController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 物料属性模板维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("Base_MaterialGroupBindMaterial")]
    public class Base_MaterialGroupBindMaterialController : ApiBaseController
    {

        private Base_MaterialGroupBindMaterialBLL _MaterialGroupBindMaterialBLL = new Base_MaterialGroupBindMaterialBLL();
        private string SuccessMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
        private string FaildMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError");//执行失败


        #region 获取数据

        [HttpPost]
        [Route("GetDataTableWithPage")]
        public HttpResponseMessage GetDataTableWithPage(JObject jo)
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

                var data = _MaterialGroupBindMaterialBLL.GetDataTableWithPage(pagination, queryJson);
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupBindMaterialController.Tips_3");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    _MaterialGroupBindMaterialBLL.RemoveForm(keyValue);

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
        /// <param name="keyValue"></param>
        /// <param name="dataItemEntity"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveFormList")]
        public HttpResponseMessage SaveFormList(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            result.resultData = null;
            if (jo.SelectToken("GroupCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupBindMaterialController.Tips_6");//缺少组参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            if (jo.SelectToken("data") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupBindMaterialController.Tips_7");//缺少data参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                string keyValue = getValue(jo, "keyValue");
                string username = getValue(jo, "username");
                if (string.IsNullOrEmpty(username))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Material.Base_MaterialGroupBindMaterialController.Tips_3");//用户名不能为空！请联系系统管理员
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    var groupCode = getValue(jo, "GroupCode");
                    var list = JsonConvert.DeserializeObject<List<Base_MaterialGroupBindMaterialEntity>>(getValue(jo, "data"));
                    var time = DateTime.Now;
                    //_MaterialGroupBindMaterialBLL.RemoveForm(t => t.GroupCode == groupCode);
                    list.ForEach(t =>
                    {
                        t.Id = Guid.NewGuid().ToString();
                        t.CreateTime = time;
                        t.Creator = userCode;
                        _MaterialGroupBindMaterialBLL.SaveForm(null, t);
                    });

                    result.success = true;
                    result.returnMsg = SuccessMsg;
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
