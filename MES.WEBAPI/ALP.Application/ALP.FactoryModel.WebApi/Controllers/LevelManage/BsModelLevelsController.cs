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
    /// 描 述：工厂模型层级
    /// </summary>
    //[Auth]
    [RoutePrefix("LevelManage/BsModelLevel")]
    public class BsModelLevelsController : ApiBaseController
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
                Content = new StringContent(ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")//测试成功  0001
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_2");//检索成功
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
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetParentListJson")]
        public HttpResponseMessage GetParentListJson(string queryJson)
        {
            var result = new ResponseResult();
            try
            {
                var list = bsModelLevelBLL.GetParentList(queryJson);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_2");//检索成功
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
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetParentListByCodeJson")]
        public HttpResponseMessage GetParentListByCodeJson(string queryJson)
        {
            var result = new ResponseResult();
            try
            {
                var list = bsModelLevelBLL.GetParentListByCode(queryJson);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_2");//检索成功
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_3");//分页参数Pagination不能为空！
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
                var data = bsModelLevelBLL.GetEntity(keyValue);

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_6");//获取详情数据成功
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
        /// <summary>
        /// 判断数据表里是否含有层级等于传入参数的记录
        /// </summary>
        /// <param name="levelNum"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("HasLevelNum")]
        public HttpResponseMessage HasLevelNum()
        {
            var result = new ResponseResult();
            try
            {
                string levelNum = "1";
                var data = bsModelLevelBLL.HasLevelNum(int.Parse(levelNum));
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_7");//获取数据成功
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="keyValue">主键</param>
        [HttpGet]
        [Route("DeleteForm")]
        public HttpResponseMessage Delete(string code)
        {
            var result = new ResponseResult<object>();
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_8");//用户编号不能为空！
                }
                else
                {
                    bsModelLevelBLL.RemoveForm(code);

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
            /*var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;*/
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_11");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_12");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "KeyValue");
                BsModelLevelEntity entity = JsonConvert.DeserializeObject<BsModelLevelEntity>(getValue(jo, "Entity"));
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.ModifyUser = entity.CreateUser;
                    entity.ModifyDate = DateTime.Now;
                }
                else
                {
                    entity.CreateUser = entity.CreateUser;
                    entity.CreateDate = DateTime.Now;
                }
                int returnResult = bsModelLevelBLL.SaveForm(keyValue, entity);
                if (returnResult == 1)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_13");//添加成功
                    result.success = true;
                }
                else if (returnResult == 2)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_14");//修改成功
                    result.success = true;
                }
                else if (returnResult == 3)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_15");//主键重复，请重新输入层级编码
                    result.success = false;
                }
                else if (returnResult == 4)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_16");//输入的层级名称已经存在
                    result.success = false;
                }
                else if (returnResult == 5)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("LevelManage.BsModelLevelsController.Tips_17");//输入的层级已经存在
                    result.success = false;
                }
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
    }
}