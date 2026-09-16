using ALP.Application.Busines.Calendar;
using ALP.Application.Busines.Comm;
using ALP.Application.Entity.Calendar;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.WebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;


namespace ALP.Application.WebApi.Controllers.Calendar
{
    public class CalendarController : ApiBaseController
    {
        [HttpGet]
        [Route("calendar/test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent("测试成功  0001" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")
            };
            return result1;
        }

        #region 休息时间管理
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/GetPage_BS_BreakTimeManage")]
        public HttpResponseMessage GetPage_BS_BreakTimeManage(JObject jo)
        {
            BS_BreakTimeManage_BLL bll = new BS_BreakTimeManage_BLL();
            var result = new ResponseResult();
            result.resultData = null;
            string ShifCode = getValue(jo, "ShifCode");
            string ShiftName = getValue(jo, "ShiftName");
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
                var data = bll.GetPage_BS_BreakTimeManage(pagination, queryJson);
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
                result.returnMsg = "执行成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "执行失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询-休息时间管理
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Get_BS_BreakTimeManage")]

        public HttpResponseMessage Get_BS_BreakTimeManage(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string ShifCode = getValue(jo, "ShifCode");
            string ShiftName = getValue(jo, "ShiftName");
            try
            {
                BS_BreakTimeManage_BLL orderBLL = new BS_BreakTimeManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ShifCode))
                    map.Add("ShifCode", ShifCode);
                if (!string.IsNullOrEmpty(ShiftName))
                    map.Add("ShiftName", ShiftName);
                string msg = "";
                DataTable dt = orderBLL.Get_Data(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 新增休息时间管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Insert_BS_BreakTimeManage")]
        public HttpResponseMessage Insert_BS_BreakTimeManage(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userName = getValue(jo, "userName");
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少Entity参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string keyValue = getValue(jo, "WorkOrderNO");
                if (string.IsNullOrEmpty(userName))
                {
                    result.success = false;
                    result.returnMsg = "用户名不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var now = DateTime.Now;
                BS_BreakTimeManage entity = JsonConvert.DeserializeObject<BS_BreakTimeManage>(getValue(jo, "Entity"));
                if (string.IsNullOrEmpty(entity.ShiftCode))
                {
                    result.success = false;
                    result.returnMsg = "参数 ShiftCode 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (entity.Duration == 0)
                {
                    result.success = false;
                    result.returnMsg = "参数 Duration 不能为0！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                entity.IsEnable = true;
                entity.Creator = userName;
                entity.CreatDate = now;

                BS_BreakTimeManage_BLL bll = new BS_BreakTimeManage_BLL();
                string msg = "";

                //保存数据
                int n = bll.Save_Entity("", entity, out msg);
                if (n > 0)
                {
                    result.success = true;
                    result.returnMsg = "操作成功";
                }
                else
                {
                    result.success = false;
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 修改-休息时间管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/update_BS_BreakTimeManage")]
        public HttpResponseMessage update_BS_BreakTimeManage(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");
            try
            {

                if (jo.SelectToken("Id") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 Id 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 Entity 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string Id = getValue(jo, "Id");

                if (string.IsNullOrEmpty(userName))
                {
                    result.success = false;
                    result.returnMsg = "userName 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_BreakTimeManage_BLL orderBLL = new BS_BreakTimeManage_BLL();

                var now = DateTime.Now;
                BS_BreakTimeManage model = orderBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_BreakTimeManage entity = JsonConvert.DeserializeObject<BS_BreakTimeManage>(getValue(jo, "Entity"));
                //entity.Modifier = userCode;
                entity.Modifier = userName;
                entity.ModifyDate = now;
                entity.IsEnable = true;
                string msg = "";
                int n = orderBLL.Save_Entity(Id, entity, out msg);
                if (n > 0)
                {
                    result.success = true;
                    result.returnMsg = "操作成功";
                }
                else
                {
                    result.success = false;
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        ///删除-休息时间管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calendar/Delete_BS_BreakTimeManage")]
        public HttpResponseMessage Delete_BS_BreakTimeManage(string Id)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (string.IsNullOrEmpty(Id))
            {
                result.success = false;
                result.returnMsg = "Id不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = "";
            try
            {
                BS_BreakTimeManage_BLL bll = new BS_BreakTimeManage_BLL();

                var now = DateTime.Now;
                BS_BreakTimeManage model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string msg = "";
                bool bresut = bll.Delete_Entity(Id, userName, out msg);
                result.success = bresut;
                if (bresut)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

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
        #region 班次管理
        /// <summary>
        /// 查询-班次
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/GetPage_BS_ShiftManage")]
        public HttpResponseMessage GetPage_BS_ShiftManage(JObject jo)
        {
            BS_ShiftManage_BLL bll = new BS_ShiftManage_BLL();
            var result = new ResponseResult();
            result.resultData = null;
            string ShifCode = getValue(jo, "ShifCode");
            string ShiftName = getValue(jo, "ShiftName");
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
                result.returnMsg = "执行成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "执行失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 查询-班次
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Get_BS_ShiftManage")]
        public HttpResponseMessage Get_BS_ShiftManage(JObject jo)
        {
            var result = new ResponseResult();

            string ShifCode = getValue(jo, "ShifCode");
            string ShiftName = getValue(jo, "ShiftName");
            try
            {
                BS_ShiftManage_BLL orderBLL = new BS_ShiftManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ShifCode))
                    map.Add("ShifCode", ShifCode);
                if (!string.IsNullOrEmpty(ShiftName))
                    map.Add("ShiftName", ShiftName);
                string msg = "";
                DataTable dt = orderBLL.Get_Data(map, out msg);
                result.resultData = dt;
                result.returnMsg = msg;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 新增-班次管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Insert_BS_ShiftManage")]
        public HttpResponseMessage Insert_BS_ShiftManage(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少Entity参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }



                var now = DateTime.Now;
                BS_ShiftManage entity = JsonConvert.DeserializeObject<BS_ShiftManage>(getValue(jo, "Entity"));
                if (string.IsNullOrEmpty(entity.ShifCode))
                {
                    result.success = false;
                    result.returnMsg = "参数 ShiftCode 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.ShiftName))
                {
                    result.success = false;
                    result.returnMsg = "参数 ShiftName 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                /*else if (string.IsNullOrEmpty(entity.ProductionLine))
                {
                    result.success = false;
                    result.returnMsg = "参数 ProductionLine 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }*/

                entity.IsEnable = true;
                entity.Creator = userName;
                entity.CreatDate = now;

                BS_ShiftManage_BLL bll = new BS_ShiftManage_BLL();
                string msg = "";
                int returnResult = bll.Save_Entity("", entity, out msg);
                if (returnResult == 1)
                {
                    result.success = true;
                    result.returnMsg = "操作成功";
                }
                else if (returnResult == -1)
                {
                    result.success = false;
                    result.returnMsg = "操作失败，班次代码不能重复";
                }
                else if (returnResult == -2)
                {
                    result.success = false;
                    result.returnMsg = "操作失败，班次名称不能重复";
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 修改-班次管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/update_BS_ShiftManage")]
        public HttpResponseMessage update_BS_ShiftManage(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");
            try
            {

                if (jo.SelectToken("ShifCode") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 ShifCode 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 Entity 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string Id = getValue(jo, "ShifCode");

                if (string.IsNullOrEmpty(userName))
                {
                    result.success = false;
                    result.returnMsg = "userName 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_ShiftManage_BLL orderBLL = new BS_ShiftManage_BLL();

                var now = DateTime.Now;
                BS_ShiftManage model = orderBLL.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_ShiftManage entity = JsonConvert.DeserializeObject<BS_ShiftManage>(getValue(jo, "Entity"));
                //entity.Modifier = userCode;
                entity.Modifier = userName;
                entity.ModifyDate = now;
                entity.IsEnable = true;
                string msg = "";
                int n = orderBLL.Save_Entity(Id, entity, out msg);
                result.success = n > 0 ? true : false;
                if (n == 1)
                {
                    result.success = true;
                    result.returnMsg = "操作成功";
                }
                //else if (n == -1)
                //{
                //    result.success = false;
                //    result.returnMsg = "操作失败，编号不能重复";
                //}
                //else if (n == -2)
                //{
                //    result.success = false;
                //    result.returnMsg = "操作失败，名称不能重复";
                //}
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        ///删除-班次管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calendar/Delete_BS_ShiftManage")]
        public HttpResponseMessage Delete_BS_ShiftManage(string Id)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (string.IsNullOrEmpty(Id))
            {
                result.success = false;
                result.returnMsg = "Id不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = "";
            try
            {
                BS_ShiftManage_BLL bll = new BS_ShiftManage_BLL();

                var now = DateTime.Now;
                BS_ShiftManage model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string msg = "";

                bool b = bll.Delete_Entity(Id, userName, out msg);
                result.success = b;
                if (b)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

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
        #region 假日管理
        /// <summary>
        /// 查询-休息时间管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Get_BS_HolidayManage")]

        public HttpResponseMessage Get_BS_HolidayManage(JObject jo)
        {
            var result = new ResponseResult();

            string TheYear = getValue(jo, "TheYear");
            string HolidayName = getValue(jo, "HolidayName");
            try
            {
                BS_HolidayManage_BLL orderBLL = new BS_HolidayManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(TheYear))
                    map.Add("TheYear", TheYear);
                if (!string.IsNullOrEmpty(HolidayName))
                    map.Add("HolidayName", HolidayName);
                string msg = "";
                DataTable dt = orderBLL.Get_Data(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 新增-假日管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Insert_BS_HolidayManage")]
        public HttpResponseMessage Insert_BS_HolidayManage(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少Entity参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var now = DateTime.Now;
                BS_HolidayManage entity = JsonConvert.DeserializeObject<BS_HolidayManage>(getValue(jo, "Entity"));
                if (string.IsNullOrEmpty(entity.HolidayName))
                {
                    result.success = false;
                    result.returnMsg = "参数 HolidayName 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.HolidayStart.ToStr()))
                {
                    result.success = false;
                    result.returnMsg = "参数 HolidayStart 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                else if (string.IsNullOrEmpty(entity.HolidayEnd.ToStr()))
                {
                    result.success = false;
                    result.returnMsg = "参数 HolidayEnd 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                int year1 = Convert.ToDateTime(entity.HolidayStart).Year;
                int year2 = Convert.ToDateTime(entity.HolidayEnd).Year;
                if (year1 != year2)
                {
                    result.success = false;
                    result.returnMsg = "假日不能跨年，请按年拆分两条假日！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                entity.HolidayStart = Convert.ToDateTime(entity.HolidayStart.ToString("yyyy-MM-dd"));
                entity.HolidayEnd = Convert.ToDateTime(entity.HolidayEnd.ToString("yyyy-MM-dd"));
                entity.TheYear = year1;
                entity.IsEnable = true;
                entity.Creator = userName;
                entity.CreatDate = now;

                BS_HolidayManage_BLL bll = new BS_HolidayManage_BLL();
                string msg = "";
                int n = bll.Save_Entity("", entity, out msg);
                result.success = n > 0 ? true : false;
                if (n > 0)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 修改-假日管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/update_BS_HolidayManage")]
        public HttpResponseMessage update_BS_HolidayManage(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");
            try
            {

                if (jo.SelectToken("Id") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 Id 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = "缺少 Entity 参数！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string Id = getValue(jo, "Id");

                if (string.IsNullOrEmpty(userName))
                {
                    result.success = false;
                    result.returnMsg = "userName 不能为空！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                BS_HolidayManage_BLL orderBLL = new BS_HolidayManage_BLL();

                var now = DateTime.Now;

                BS_HolidayManage entity = JsonConvert.DeserializeObject<BS_HolidayManage>(getValue(jo, "Entity"));
                //entity.Modifier = userCode;
                entity.HolidayStart = Convert.ToDateTime(entity.HolidayStart.ToString("yyyy-MM-dd"));
                entity.HolidayEnd = Convert.ToDateTime(entity.HolidayEnd.ToString("yyyy-MM-dd"));
                entity.Modifier = userName;
                entity.ModifyDate = now;
                entity.IsEnable = true;
                string msg = "";
                int n = orderBLL.Save_Entity(Id, entity, out msg);
                result.success = n > 0 ? true : false;
                result.returnMsg = "操作成功";
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        ///删除-假日管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calendar/Delete_BS_HolidayManage")]
        public HttpResponseMessage Delete_BS_HolidayManage(string Id)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (string.IsNullOrEmpty(Id))
            {
                result.success = false;
                result.returnMsg = "Id不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = "";
            try
            {
                BS_HolidayManage_BLL bll = new BS_HolidayManage_BLL();
                var now = DateTime.Now;
                BS_HolidayManage model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string msg = "";
                bool b = bll.Delete_Entity(Id, userName, out msg);
                result.success = b;
                if (b)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

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
        #region 工厂日历
        /// <summary>
        /// 查询-休息时间管理
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Get_Calendar")]
        public HttpResponseMessage Get_Calendar(JObject jo)
        {
            var result = new ResponseResult();
            JArray list_obj = new JArray();
            string TheYearMonth = getValue(jo, "DataTime");
            if (string.IsNullOrEmpty(TheYearMonth))
            {
                result.success = false;
                result.returnMsg = "DataTime参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string ProductionLine = getValue(jo, "ProductionLine");
            try
            {
                DateTime StartDay = GetMonth(TheYearMonth);
                //DateTime EndDay = StartDay.AddMonths(2).AddDays(-1);
                DateTime EndDay = StartDay.AddMonths(1).AddDays(-1);
                BS_CalendarManage_BLL cbll = new BS_CalendarManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                map.Add("StartDay", StartDay.ToString("yyyy-MM-dd"));
                //map.Add("StartDay", StartDay.AddMonths(-1).ToString("yyyy-MM-dd"));
                map.Add("EndDay", EndDay.ToString("yyyy-MM-dd"));
                map.Add("ProductionLine", ProductionLine);
                string msg = "";
                //查询日历和班次信息
                DataTable dt = cbll.Get_Data_Both(map, out msg);

                DataView dv = new DataView(dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    var dtCalendar = dv.ToTable(true, new string[] { "Id", "EveryDay" });  //第二个参数：去重字段

                    foreach (DataRow dr in dtCalendar.Rows)
                    {
                        var dtItem = dt.Select($"Id='{dr["Id"]}'");
                        foreach (DataRow item in dtItem)
                        {
                            JObject jobj = new JObject();
                            jobj.Add("id", item["Id"].ToString());
                            string title = "";
                            if (Convert.ToBoolean(item["IsHoliday"]))
                            {
                                title = Convert.ToDateTime(item["EveryDay"]).ToString("yyyy-MM-dd") + item["Title"].ToString();
                                //字体颜色
                                //jobj.Add("textColor", "#d71345");
                                //背景和边框颜色。
                                jobj.Add("color", "#fab27b");
                            }
                            else
                            {
                                //Convert.ToDateTime(item["EveryDay"]).ToString("yyyy-MM-dd") +
                                title = item["ShiftName"].ToString() + "：" + item["StartDate"] + "-" + item["EndDate"];
                            }
                            jobj.Add("title", title);
                            jobj.Add("start", Convert.ToDateTime(item["EveryDay"]).ToString("yyyy-MM-dd 00:00:00"));
                            jobj.Add("allDay", true);
                            jobj.Add("IsHoliday", Convert.ToInt32(item["IsHoliday"]));
                            jobj.Add("YMD", Convert.ToDateTime(item["Everyday"]).ToString("yyyy-MM-d"));
                            //jobj.Add("y", Convert.ToDateTime(item["EveryDay"]).Year);
                            //jobj.Add("m", Convert.ToDateTime(item["EveryDay"]).Month);
                            //jobj.Add("d", Convert.ToDateTime(item["EveryDay"]).Day);
                            list_obj.Add(jobj);
                            if (Convert.ToBoolean(item["IsHoliday"]))
                            {
                                break;
                            }
                        }
                    }
                }

                result.returnMsg = msg;
                result.resultData = list_obj;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 查询指定日期信息
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/Get_DateInfo")]
        public HttpResponseMessage Get_Date(JObject jo)
        {
            var result = new ResponseResult();
            string EveryDay = getValue(jo, "EveryDay");
            string ProductionLine = getValue(jo, "ProductionLine");
            string msg = "";
            if (string.IsNullOrEmpty(EveryDay))
            {
                result.success = false;
                result.returnMsg = "DataTime参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //查询指定年的假日
                BS_CalendarManage_BLL orderBLL = new BS_CalendarManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();

                map.Add("EveryDay", EveryDay.ToString());
                map.Add("ProductionLine", ProductionLine);

                DataTable dt_holiday = orderBLL.Get_Data(map, out msg);

                result.returnMsg = msg;
                result.resultData = dt_holiday;
                result.success = true;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = "操作失败，服务器异常";
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #region 生成-工厂日历
        /// <summary>
        ///生成-工厂日历
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/create_BS_CalendarManage")]
        public HttpResponseMessage create_BS_CalendarManage(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            var userCode = getValue(jo, "userCode");
            var userName = getValue(jo, "userName");

            //产线
            string ProductionLine = getValue(jo, "ProductionLine");
            if (string.IsNullOrEmpty(ProductionLine))
            {
                result.success = false;
                result.returnMsg = "ProductionLine 参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            //生成月份
            if (string.IsNullOrEmpty(getValue(jo, "DateTime")))
            {
                result.success = false;
                result.returnMsg = "DateTime 参数不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //DateTime dt2 = Convert.ToDateTime(getValue(jo, "year"));
            JArray checkValues = JArray.Parse(getValue(jo, "checkValues"));

            //指定的生成年月份
            DateTime EveryDay = Convert.ToDateTime(getValue(jo, "DateTime"));
            try
            {
                #region 判断此产线的日历是否已经生成
                DateTime StartDay = EveryDay;
                DateTime EndDay = StartDay.AddMonths(1).AddDays(-1);

                BS_CalendarManage_BLL cbll = new BS_CalendarManage_BLL();
                Dictionary<string, string> map2 = new Dictionary<string, string>();
                map2.Add("StartDay", StartDay.ToString("yyyy-MM-dd"));
                map2.Add("EndDay", EndDay.ToString("yyyy-MM-dd"));
                map2.Add("ProductionLine", ProductionLine);
                string msg = "";
                //查询日历和班次信息
                DataTable dtCalendar = cbll.Get_Data_Both(map2, out msg);
                if (dtCalendar != null && dtCalendar.Rows.Count > 0)
                {
                    result.success = false;
                    result.returnMsg = $"产线【{ProductionLine}】的【{EveryDay.ToString("yyyy-MM")}】日历已经存在，不能重复生成！";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                #endregion
                //年
                int TheYear = EveryDay.Year;
                //查询指定年的假日
                BS_HolidayManage_BLL orderBLL = new BS_HolidayManage_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();

                map.Add("TheYear", TheYear.ToString());

                DataTable dt_holiday = orderBLL.Get_Data(map, out msg);
                //这个月最小值
                DateTime minDt = DateTime.Parse(EveryDay.ToString("yyyy-MM") + "-01");
                //这个月最大值
                DateTime maxDt = minDt.AddMonths(1).AddDays(-1);
                //List<string> list_sql = new List<string>();
                StringBuilder sb_sql = new StringBuilder();
                DateTime dtNow = DateTime.Now;
                //循环这个月日期
                while (minDt <= maxDt)
                {
                    BS_CalendarManage model_calendar = new BS_CalendarManage();
                    model_calendar.Id = Guid.NewGuid();
                    model_calendar.EveryDay = minDt;
                    model_calendar.ProductionLine = ProductionLine;
                    model_calendar.IsHoliday = false;
                    model_calendar.CreatDate = dtNow;
                    model_calendar.Creator = userName;
                    model_calendar.Title = "工作日";
                    #region 判断星期是否休息
                    bool restDay = GetWeekDay_State(minDt, checkValues);
                    if (restDay)
                    {
                        model_calendar.Title = "休息日";
                        model_calendar.IsHoliday = true;
                    }
                    #endregion
                    #region 判断是否在假日
                    if (dt_holiday != null && dt_holiday.Rows.Count > 0)
                    {
                        var holidayRows = dt_holiday.Select($"'{minDt.ToString("yyyy-MM-dd")}'>=HolidayStart and '{minDt.ToString("yyyy-MM-dd")}'<=HolidayEnd");
                        if (holidayRows != null && holidayRows.Length > 0)
                        {
                            model_calendar.Title = "假日";
                            model_calendar.IsHoliday = true;
                        }
                    }
                    #endregion
                    BS_CalendarManage_BLL calendarBLL = new BS_CalendarManage_BLL();
                    string calendarSQL = calendarBLL.GetSaveSql(model_calendar, out msg);
                    sb_sql.Append(calendarSQL);

                    //查询产线的车间下所有班次，循环班次生成
                    #region 查询产线的车间下所有班次，循环班次生成
                    BS_ShiftManage_BLL shitBLL = new BS_ShiftManage_BLL();
                    DataTable dtShift = shitBLL.Get_Data(null, out msg);
                    if (dtShift != null && dtShift.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtShift.Rows)
                        {
                            BS_ShiftOfCalendar model_shiftOfCalendar = new BS_ShiftOfCalendar();
                            model_shiftOfCalendar.Id = Guid.NewGuid();
                            model_shiftOfCalendar.CalendarId = model_calendar.Id;
                            model_shiftOfCalendar.ShiftCode = item["ShifCode"].ToString();
                            model_shiftOfCalendar.StartDate = item["StartDate"].ToString();
                            model_shiftOfCalendar.EndDate = item["EndDate"].ToString();
                            model_shiftOfCalendar.CreatDate = dtNow;
                            model_shiftOfCalendar.Creator = userName;

                            BS_ShiftOfCalendar_BLL shiftOfBLL = new BS_ShiftOfCalendar_BLL();
                            string shiftOfSQL = shiftOfBLL.GetSaveSql(model_shiftOfCalendar, out msg);

                            sb_sql.Append(shiftOfSQL);
                        }
                    }
                    else
                    {
                        result.success = false;
                        result.returnMsg = $"请先“配置班次”！";
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    #endregion
                    //日期加1
                    minDt = minDt.AddDays(1);
                }
                var now = DateTime.Now;
                BS_CalendarManage entity = JsonConvert.DeserializeObject<BS_CalendarManage>(getValue(jo, "Entity"));

                CommonBLL commbll = new CommonBLL();
                //批量执行事务
                bool bResult = commbll.ExecuteBySql(sb_sql.ToStr(), out msg);
                result.success = bResult;
                if (bResult)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 判断日期是否为休息日
        /// </summary>
        /// <param name="dtNow"></param>
        /// <param name="checkValues"></param>
        /// <returns></returns>
        bool GetWeekDay_State(DateTime dtNow, JArray checkValues)
        {
            int weekId = (int)dtNow.DayOfWeek;
            var jt = checkValues.FirstOrDefault(x => x.Value<string>("id") == weekId.ToString());
            string PLAN_START_DATE = jt.Value<string>("checked");
            bool restDay = Convert.ToBoolean(jt.Value<string>("checked"));
            return restDay;
        }
        /// <summary>
        /// 将指定格式转化为年月日
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        DateTime GetMonth(string datetime)
        {
            string[] array_date = datetime.Split(' ');

            string year = array_date[0];
            string strMonth = array_date[1];
            List<string> array_month = new List<string> { "一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月" };
            int monthIndex = array_month.IndexOf(strMonth) + 1;
            return Convert.ToDateTime(year + "-" + monthIndex + "-01");
        }
        #endregion
        /// <summary>
        ///修改状态-工厂日历
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("calendar/UpdateState_Calendar")]
        public HttpResponseMessage UpdateState_Calendar(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string Id = getValue(jo, "Id");
            if (string.IsNullOrEmpty(Id))
            {
                result.success = false;
                result.returnMsg = "Id不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = getValue(jo, "userName");
            if (string.IsNullOrEmpty(userName))
            {
                result.success = false;
                result.returnMsg = "userName不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string state = getValue(jo, "state");
            if (string.IsNullOrEmpty(state))
            {
                result.success = false;
                result.returnMsg = "state 不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                BS_CalendarManage_BLL bll = new BS_CalendarManage_BLL();
                var now = DateTime.Now;
                BS_CalendarManage model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                string msg = "";
                bool b = bll.Update_state(Id, userName, state, out msg);
                result.success = b;
                if (b)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        ///修改状态-工厂日历
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("calendar/UpdateState_Calendar2")]
        public HttpResponseMessage UpdateState_Calendar2(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            string ProductionLine = getValue(jo, "ProductionLine");
            if (string.IsNullOrEmpty(ProductionLine))
            {
                result.success = false;
                result.returnMsg = "ProductionLine 不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string cData = getValue(jo, "cData");
            if (string.IsNullOrEmpty(cData))
            {
                result.success = false;
                result.returnMsg = "cData 不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = getValue(jo, "userName");
            if (string.IsNullOrEmpty(userName))
            {
                result.success = false;
                result.returnMsg = "userName 不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string state = getValue(jo, "state");
            if (string.IsNullOrEmpty(state))
            {
                result.success = false;
                result.returnMsg = "state 不能为空！";
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                BS_CalendarManage_BLL bll = new BS_CalendarManage_BLL();
                var now = DateTime.Now;
                BS_CalendarManage model = new BS_CalendarManage();
                model.IsHoliday = Convert.ToBoolean(state);
                model.Modifier = userName;
                model.ProductionLine = ProductionLine;
                model.EveryDay = Convert.ToDateTime(cData);
                string msg = "";
                bool b = bll.Update_state(model, out msg);
                result.success = b;
                if (b)
                    result.returnMsg = "操作成功";
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

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