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

namespace ALP.Application.WebApi.Controllers.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级
    /// </summary>
    //[Auth]
    [RoutePrefix("BaseManage/BsModelLevel")]
    public class BsModelLevelController : ApiBaseController
    {
        private BsModelLevelBLL bsModelLevelBLL = new BsModelLevelBLL();

        #region 获取数据
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent("测试成功  0001" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")
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
                var list = bsModelLevelBLL.GetList(queryJson);
                result.resultData = list;
                result.success = true;
                result.returnMsg = "查阅成功";
            }
            catch(Exception ex)
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
        [Route("GetPageList")]
        public HttpResponseMessage GetPageListJsoon(JObject jo)
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
                    result.returnMsg = "分页参数Pagination不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bsModelLevelBLL.GetPageList(pagination, queryJson);
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
                result.returnMsg = "执行成功";
            }
            catch(Exception ex)
            {
                result.success = false;
                result.returnMsg = "执行失败：" + ex.Message.ToString();
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
                var data = bsModelLevelBLL.GetEntity(keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = "获取详情数据成功";
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
        /// <param name="keyValue">主键</param>
        [HttpPost]
        [Route("Delete")]
        public HttpResponseMessage Delete(JObject jo)
        {
            var result = new ResponseResult<object>();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            try
            {
                string keyValue = getValue(jo, "KeyValue");
                if (string.IsNullOrEmpty(userCode))
                {
                    result.success = false;
                    result.returnMsg = "用户名不能为空！";
                }
                else
                {
                    bsModelLevelBLL.RemoveForm(keyValue);

                    result.success = true;
                    result.returnMsg = "删除成功";
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
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
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少KeyValue参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少Entity参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                if (string.IsNullOrEmpty(userCode))
                {
                    result.success = false;
                    result.returnMsg = "用户名不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else
                {
                    BsModelLevelEntity entity = JsonConvert.DeserializeObject<BsModelLevelEntity>(getValue(jo, "Entity"));
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        entity.ModifyUser = userCode;
                        entity.ModifyDate = DateTime.Now;
                    }
                    else
                    {
                        entity.CreateUser = userCode;
                        entity.CreateDate = DateTime.Now;
                    }
                    bsModelLevelBLL.SaveForm(keyValue, entity);
                    result.success = true;
                    result.returnMsg = "操作成功";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}