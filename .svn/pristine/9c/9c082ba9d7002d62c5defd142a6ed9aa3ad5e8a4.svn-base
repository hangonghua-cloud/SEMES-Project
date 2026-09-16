using ALP.Application.Busines.EquipmentManage;
using ALP.Application.Entity.EquipmentManage;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Filter;
using ALP.WebApi.Models;
using ALP.WebApi.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.EquipmentManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.8.3 16:19
    /// 描 述：设备台账
    /// </summary>
    [RoutePrefix("EquipmentManage")]
    public class EquipmentController : ApiBaseController
    {
        EP_EquipmentManagee_BLL bll = new EP_EquipmentManagee_BLL();

        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent(ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")//测试成功  0001
            };
            return result1;
        }
        #region 获取数据
        /// <summary>
        /// 查询分页列表 -设备台账
        /// </summary>
        /// <param name="jo"></param> 
        /// <returns>返回分页列表Json</returns>
        [HttpPost]
        [Route("GetPage_Equipment")]
        public HttpResponseMessage GetPage_Equipment(JObject jo)
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
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_2");//分页参数Pagination不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string queryJson = getValue(jo, "queryJson");
                var watch = CommonHelper.TimerStart();
                var data = bll.Get_PageData(pagination, queryJson);
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
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
        #region 提交数据
        [HttpGet]
        [Route("DeleteForm")]
        public HttpResponseMessage RemoveForm(string keyValue)
        {
            var result = new ResponseResult<object>();
            try
            {
                string msg = "";
                int returnValue = bll.RemoveForm(keyValue, out msg);

                if (returnValue == 1)
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.DeleteSuccess");//删除成功
                    result.success = true;
                }
                else
                {
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_6");//删除失败，没有找到记录
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
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("KeyValue") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_8");//缺少KeyValue参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_9");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string keyValue = getValue(jo, "KeyValue");

                EP_EquipmentManage entity = JsonConvert.DeserializeObject<EP_EquipmentManage>(getValue(jo, "Entity"));
                string msg = "";

                if (string.IsNullOrEmpty(keyValue))
                {
                    var ent = bll.GetEntity(t => t.EquipmentId == entity.EquipmentId);
                    if (ent != null)
                    {
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("EquipmentManage.EquipmentController.Tips_10");//设备编码重复
                        result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    entity.CreateTime = DateTime.Now;
                }
                else
                {
                    entity.ModifyTime = DateTime.Now;
                }



                int returnValue = bll.SaveForm(keyValue, entity, out msg);

                result.success = returnValue > 0 ? true : false;
                result.returnMsg = returnValue > 0 ? "执行成功" : ALP.Application.Service.Resources.Language.GetText("Common.ExecutionError")+ msg;//执行失败
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：// "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}