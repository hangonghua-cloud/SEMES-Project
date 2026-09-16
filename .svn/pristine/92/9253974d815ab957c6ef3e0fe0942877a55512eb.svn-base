using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Busines.BaseManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;

namespace ALP.Application.WebApi.Controllers.LevelManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.22 16:19
    /// 描 述：工厂模型资源扩展
    /// </summary>
    //[Auth]
    [RoutePrefix("LevelManage/BsModelResourceExtendInfo")]
    public class BsModelResourceExtendInfoController : ApiBaseController
    {
        private BsModelResourceExtendInfoBLL resourceExtendInfoBLL = new BsModelResourceExtendInfoBLL();

        #region 获取数据
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent(ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")//测试成功  0001
            };
            return result1;
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetListJson")]
        public HttpResponseMessage GetListJson(string queryJson = "{}")
        {
            var result = new ResponseResult();
            try
            {
                var list = resourceExtendInfoBLL.GetList(queryJson);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_2");//检索成功
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
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPageListJson")]
        public HttpResponseMessage GetPageListJson(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                //var jo = jo.ToJObject();
                Pagination pagination = new Pagination();
                if (!jo["pagination"].IsEmpty())
                {
                    pagination = JsonConvert.DeserializeObject<Pagination>(getValue(jo, "pagination"));
                }
                else
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_3");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = resourceExtendInfoBLL.GetPageList(pagination, queryJson);
                var JsonDate = new
                {
                    rows = data,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                    costtime = CommonHelper.TimerEnd(watch)
                };
                result.resultData = JsonDate;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionSuccess");//执行成功
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError2") + ex.Message.ToString();//执行失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        //public IEnumerable<BsModelLevelEntity> GetAllList()
        //{
        //    return service.GetAllList();
        //}
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                var data = resourceExtendInfoBLL.GetEntity(keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_6");//获取详情数据成功
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            //return ToJson(result);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        //bool ExistCode(string code);
        /// <summary>
        /// 层级名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        //bool ExistFullName(string name);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="resource"></param>
        [HttpGet]
        [Route("DeleteForm")]
        public HttpResponseMessage Delete(string code, string value, string resource)
        {
            var result = new ResponseResult<object>();
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_7");//资源编号不能为空！
                }
                else
                {
                    resourceExtendInfoBLL.RemoveForm(code, resource);

                    result.success = true;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        [HttpPost]
        [Route("Save")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_10");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_11");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                BsModelResourceExtendInfoEntity entity = JsonConvert.DeserializeObject<BsModelResourceExtendInfoEntity>(getValue(jo, "Entity"));
                resourceExtendInfoBLL.SaveForm(keyValue, entity);
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
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
        #endregion

        #region 获取管理方式
        /// <summary>
        /// 获取管理方式
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetManageMode")]
        public HttpResponseMessage GetManageMode(JObject jo)
        {

            var result = new ResponseResult();
            result.resultData = null;
            try
            {


                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_10");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string keyValue = getValue(jo, "KeyValue");
                string FieldCode = "GLFS";//管理方式

                var BsModelResourceExtendInfo = resourceExtendInfoBLL.Get_ExpressEntity(t => t.ResourceCode == keyValue && t.FieldCode == FieldCode);
                result.resultData = BsModelResourceExtendInfo;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_6");//获取详情数据成功
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 获取某个属性实体
        /// <summary>
        /// 获取某个属性实体
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetResourceExtendEntity")]
        public HttpResponseMessage GetResourceExtendEntity(JObject jo)
        {
            try
            {
                string resourceCode = getValue(jo, "resourceCode");
                string fieldCode = getValue(jo, "fieldCode");
                if (string.IsNullOrEmpty(resourceCode))
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_13"));//resourceCode参数不能为空！

                if (string.IsNullOrEmpty(fieldCode))
                    return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelResourceExtendInfoController.Tips_14"));//fieldCode参数不能为空

                var entity = resourceExtendInfoBLL.Get_ExpressEntity(t => t.ResourceCode == resourceCode && t.FieldCode == fieldCode);

                return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"), entity);//操作成功
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
            }
        }
        #endregion

    }
}