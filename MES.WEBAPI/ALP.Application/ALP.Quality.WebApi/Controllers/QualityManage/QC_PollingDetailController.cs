using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Busines.QualityManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Text;
using System.Collections.Generic;
using ALP.Application.Busines.PlanManage;
using ALP.Application.Busines.Material;
using System.Transactions;

namespace ALP.Application.WebApi.Controllers.QualityManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_PollingDetailController 控制器 友情提示: 如果是移动APP接口使用请把[Auth]注释掉
    /// 4.任务编号: 巡检检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Auth]
    [RoutePrefix("QC_PollingDetail")]
    public class QC_PollingDetailController : ApiBaseController
    {
        private QC_PollingDetail_BLL bll = new QC_PollingDetail_BLL();
        private QC_PollingDetailResult_BLL _qcPollingDetailResultBLL = new QC_PollingDetailResult_BLL();
        private PL_ProcessBLL _plProcessBLL = new PL_ProcessBLL();
        private PL_ProcessOfOperationsBLL _plOperationBLL = new PL_ProcessOfOperationsBLL();
        private QC_TestMaintenanceBLL _qcTestMaintenanceBLL = new QC_TestMaintenanceBLL();  //检测类型
        private QC_TestItemMaintenanceBLL _qcTestItemMaintenanceBLL = new QC_TestItemMaintenanceBLL(); //检测项目
        private QC_TestProcessMaintenanceBLL _qcTestProcessMaintenanceBLL = new QC_TestProcessMaintenanceBLL(); //关联工序
        private Base_MaterialGroupBindMaterialBLL _baseGroupBindMaterialBLL = new Base_MaterialGroupBindMaterialBLL();//物料组绑定物料

        /// <summary>
        /// 功能描述: 测试接口
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        [HttpGet]
        [Route("test")]
        public HttpResponseMessage test()
        {
            var result = new ResponseResult();
            result.resultData = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");//API接口测试正常！
            result.success = true;
            result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_2");//测试成功
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_PollingDetailPageList")]
        public HttpResponseMessage QC_PollingDetailPageList(JObject jo)
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
                var data = bll.GetPageList(pagination, queryJson);
                var JsonData = new
                {
                    rows = data,
                    total = pagination != null ? pagination.total : data.Count(),
                    page = pagination != null ? pagination.total : data.Count(),
                    records = pagination != null ? pagination.total : data.Count(),
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

        /// <summary>
        /// 功能描述: 获取列表(分页) 数据支持查询与分页
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("QC_PollingDetailPageDataTableList")]
        public HttpResponseMessage QC_PollingDetailPageDataTableList(JObject jo)
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
                var data = bll.GetPageDataTableList(pagination, queryJson);
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

        /// <summary>
        /// 用户实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserEntity")]
        public HttpResponseMessage GetUserEntity(string keyValue)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var data = bll.GetUserEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                if (msg != "")
                {
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 功能描述: 获取所有列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetQC_PollingDetailList")]
        public HttpResponseMessage GetQC_PollingDetailList(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string msg = "";
                var list = bll.GetList(checkType, out msg);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                if (msg != "")
                {
                    result.returnMsg = msg;
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">json参数, 包含keyValue 主键值, entity 实体对象 </param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveQC_PollingDetail")]
        public HttpResponseMessage SaveQC_PollingDetail(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //判断主键内容是否为空, 为空新增, 有值修改
            if (jo.SelectToken("KeyValue") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_6");//缺少KeyValue参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //if (string.IsNullOrEmpty(userCode))
            //{
            //result.success = false;
            //result.returnMsg = "用户名不能为空！";
            //return Request.CreateResponse(HttpStatusCode.OK, result);
            //}

            try
            {
                //业务服务类
                //参数转实体
                QC_PollingDetailEntity entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "Entity"));


                string keyValue = getValue(jo, "KeyValue");
                string queryJson = getValue(jo, "Entity");

                if (!string.IsNullOrEmpty(keyValue))
                {

                    if (string.IsNullOrEmpty(entity.ModifyBy))
                    {
                        //编辑人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_8");//编辑人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    /*else if (string.IsNullOrEmpty(entity.UpdateByName))
                    {
                        //编辑人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_9");//编辑人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }*/
                    //entity.UpdateByCode = entity.UpdateByCode;
                    //更新日期
                    //entity.UpdateDateTime = DateTimeOffset.Now;
                    //创建日期 把创建日期也进行重新保存一次, 保存日期时区丢失问题。
                    //entity.CreatedDateTime = DateTime.Parse(entity.CreatedDateTime.ToString());
                }
                else
                {

                    /*if (string.IsNullOrEmpty(entity.Inspector))
                    {
                        //创建人编号 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_10");//创建人编号不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    else if (string.IsNullOrEmpty(entity.CreatedByName))
                    {
                        //创建人姓名 是否为空进行判断
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_11");//创建人姓名不能为空！
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }*/
                    ////使用流水号, 表BASE_Sequence 表内定义格式参考: QC_PollingDetail	ListType流水编号	2         	2021-03-08 00:00:00.000	20210315	999	刘万军	3	1	1	2	1
                    //Service.BaseManage.SerialNOService serialService = new Service.BaseManage.SerialNOService();
                    //string returnNum = "";
                    //string errorMsg = "";
                    //bool proResult = serialService.GetSerialNO("QC_PollingDetail", out returnNum, out errorMsg);
                    ////表字段自定义编码
                    //entity.ListType = "自定义前辍" + DateTime.Now.ToString("yyyyMMddHHmmss") + returnNum;

                    //创建人
                    //entity.CreatedByCode = entity.CreatedByCode;
                    //创建日期
                    //entity.CreatedDateTime = DateTimeOffset.Now;
                    //有效标志
                    //entity.EnabledMark = false;
                    //检验时间
                    //entity.InspectionTime = DateTime.Now;
                    //最后修改时间
                    //entity.ModifyTime = DateTime.Now;
                    //是否删除 为真 删除不可见, 假 可见未删除
                    //entity.IsDeleted = false;
                }

                string msg = "";
                int isok = bll.SaveEntity(keyValue, entity, out msg);
                /*result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;*///操作成功
                switch (isok)
                {
                    case 1:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_13");//添加成功
                        break;
                    case 2:
                        result.success = true;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_14");//修改成功
                        break;
                    case 3:
                        result.success = false;
                        result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_15");//添加失败，输入的流转卡编号不存在，请重新输入
                        break;
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

        /// <summary>
        /// 更新判定状态
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="determinationValue"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveDetermination")]
        public HttpResponseMessage SaveDetermination(JObject jo)//string keyValue, string determinationValue, ref string msg
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                string keyValue = getValue(jo, "KeyValue");
                string determinationValue = getValue(jo, "DeterminationValue");
                string inspector = getValue(jo, "Inspector");
                string msg = "";

                //判定
                int isok = bll.SaveDetermination(keyValue, determinationValue, inspector, ref msg);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_17");//判定操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_18") + msg;//判定操作失败，
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

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveBatchQC_PollingDetail")]
        public HttpResponseMessage SaveBatchQC_PollingDetail(JObject jo)
        {
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人编码 为空判断
            if (jo.SelectToken("CreatedByCode") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_19");//缺少CreatedByCode参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            //创建人姓名 为空判断
            if (jo.SelectToken("CreatedByName") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_20");//缺少CreatedByName参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            if (jo.SelectToken("Entity") == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_7");//缺少Entity参数！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                List<dynamic> upload_entity_list = JsonConvert.DeserializeObject<List<dynamic>>(getValue(jo, "Entity"));

                string keyValue = getValue(jo, "KeyValue");
                string CreatedByName = getValue(jo, "CreatedByName");
                string CreatedByCode = getValue(jo, "CreatedByCode");

                string msg = "";
                int isok = 1;
                //取出旧所有数据
                var old_entity_list = bll.GetList("", out msg);
                //插入数组
                List<QC_PollingDetailEntity> Insert_entity_list = new List<QC_PollingDetailEntity>();
                //更新数组
                List<QC_PollingDetailEntity> Update_entity_list = new List<QC_PollingDetailEntity>();
                foreach (var item in upload_entity_list)
                {
                    try
                    {
                        QC_PollingDetailEntity entity = new QC_PollingDetailEntity();
                        //单据类型

                        //流转卡编码
                        entity.FlowCardId = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_21")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_21")];//流转卡编码
                        //检验工序
                        entity.TestProcess = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_22")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_22")];//检验工序
                        //检验机台
                        entity.ProductionMachine = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_23")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_23")];//检验机台
                        //实验室状态
                        entity.LaboratoryTestStatus = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_24")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_24")];//实验室状态
                        //判定结果
                        entity.Determination = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_25")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_25")];//判定结果
                        //备注
                        entity.Remark = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_26")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_26")];//备注
                        //附件
                        entity.Attachment = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_27")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_27")];//附件
                        //有效标志
                        entity.EnabledMark = false;
                        //检验员
                        entity.Inspector = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_28")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_28")];//检验员
                        //检验时间
                        entity.InspectionTime = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_29")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_29")];//检验时间
                        //最后修改人
                        entity.ModifyBy = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_30")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_30")];//最后修改人
                        //最后修改时间
                        entity.ModifyTime = item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_31")] == null ? "" : item[ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_31")];//最后修改时间
                        //状态(启用/停用)
                        //entity.Status = item["状态"] == null ? "启用" : item["状态"];
                        //创建日期// DateTime.Now;
                        //entity.CreatedDateTime = DateTimeOffset.Now;
                        //是否删除
                        //entity.IsDeleted = false;
                        //entity.CreatedByCode = CreatedByCode;
                        //entity.CreatedByName = CreatedByName;

                        //取出相似编码， 提示： 此处换上表中唯一编码（不是主键）
                        QC_PollingDetailEntity old_entity = old_entity_list.FirstOrDefault(x => x.Id == entity.Id);
                        //判断旧列表中是否存在此编码
                        if (old_entity == null)
                        {
                            entity.Create();
                            //保存数组
                            Insert_entity_list.Add(entity);
                        }
                        else
                        {
                            entity.Id = old_entity.Id;
                            Update_entity_list.Add(entity);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }

                if (Insert_entity_list.Count > 0)
                {
                    //批量新增
                    isok = bll.SaveEntity_List(false, CreatedByName, Insert_entity_list, out msg);
                }
                if (Update_entity_list.Count > 0)
                {
                    //批量修改
                    isok = bll.SaveEntity_List(true, CreatedByName, Update_entity_list, out msg);
                }

                result.success = isok > 0 ? true : false;
                result.returnMsg = isok > 0 ? ALP.Application.Service.Resources.Language.GetText("Common.Success") : msg;//操作成功
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("DeleteQC_PollingDetail")]
        public HttpResponseMessage DeleteQC_PollingDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_PollingDetailEntity entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_PollingDetailEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                string msg = "";
                int isok = bll.DeleteEntity(Id, out msg, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_35");//删除操作成功
                else
                    result.returnMsg = "删除操作失败: " + msg;//删除操作失败:
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

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">json参数</param>
        [HttpPost]
        [Route("RemoveQC_PollingDetail")]
        public HttpResponseMessage RemoveQC_PollingDetail(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_5");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            try
            {
                var userCode = CurrentAccount.UserCode;
                var userName = CurrentAccount.UserName;

                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //业务服务层
                QC_PollingDetailEntity entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "Entity"));
                string Id = entity.Id;
                QC_PollingDetailEntity model = bll.GetEntity(Id);
                if (model == null)
                {
                    result.success = false;
                    result.returnMsg = Id + " 对象不存在";//对象不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                //删除
                int isok = bll.RemoveForm(Id, entity.ModifyBy);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_35");//删除操作成功
                else
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_37");//删除操作失败
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

        /// <summary>
        /// 功能描述: 获取实体
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_PollingDetailEntity</returns>
        [HttpGet]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                var data = bll.GetEntity(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_38");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>QC_PollingDetailEntity</returns>
        [HttpGet]
        [Route("GetEntityByQuery")]
        public HttpResponseMessage GetEntityByQuery(string keyValue)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                var data = bll.GetEntityByQuery(keyValue);
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_38");//获取详情数据成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 通过N个字段拼写linq查询数组对象 参考
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="keyValue">条件值</param>
        /// <param name="keyValue2">条件值</param>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetEntityByLinq")]
        public HttpResponseMessage GetEntityByLinq(string keyValue, string keyValue2)
        {
            var result = new ResponseResult();

            try
            {
                //业务服务层
                var data = bll.Get_ExpressionList(t => t.Id == keyValue && t.Id == keyValue2 && t.EnabledMark == true).OrderByDescending(t => t.Id).ToList();
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
            }
            return ToJson(result);
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetList_TestOtherEntity")]
        public HttpResponseMessage GetList_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string OutMes = "";
                var list = bll.GetList_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个数据表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpGet]
        [Route("GetDataTable_TestOtherEntity")]
        public HttpResponseMessage GetDataTable_TestOtherEntity(string checkType)
        {
            var result = new ResponseResult();
            try
            {
                string OutMes = "";
                var list = bll.GetDataTable_TestOtherEntity(checkType, out OutMes);
                result.resultData = list;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <param name="jo">queryJson 查询参数</param>
        /// <returns>链接地址</returns>
        [HttpPost]
        [Route("QC_PollingDetail_export")]
        public HttpResponseMessage QC_PollingDetail_export(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                if (jo.SelectToken("Entity") == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_7");//缺少Entity参数！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                string queryJson = getValue(jo, "Entity");
                JObject queryParam = queryJson.ToJObject();

                string msg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                string CreatedByCode = "";
                if (!queryParam["CreatedByCode"].IsEmpty())
                {
                    CreatedByCode = queryParam["CreatedByCode"].ToString();
                }

                //查询条件 默认是当前登录用户ID, 可传空 导出全部
                var data = bll.GetList_export(CreatedByCode, out msg);

                result.resultData = data;
                result.success = true;
                result.returnMsg = msg;
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

        #region 获取工单-工艺路线-工序

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpPost]
        [Route("GetWorkOrderOperation")]
        public HttpResponseMessage GetWorkOrderOperation(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var workOrder = getValue(jo, "workOrder");
                var plProcessEntity = _plProcessBLL.GetEntity(t => t.WorkOrder == workOrder && t.IsDeleted == false);
                var plOperationList = _plOperationBLL.Get_ExpressionList(t => t.ProcessId == plProcessEntity.Id);
                var data = plOperationList.OrderBy(t => t.SN).Select(t => new { t.OperationCode, t.OperationName });

                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion

        #region 获取检测项目

        /// <summary>
        /// 功能描述: 获取检测方法
        /// 创　　建: dragon
        /// 创建日期: 2021-08-23 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpPost]
        [Route("GetTestItemMaintenance")]
        public HttpResponseMessage GetTestItemMaintenance(JObject jo)
        {
            var result = new ResponseResult();
            try
            {
                var processCode = getValue(jo, "processCode"); //工序
                var materialCode = getValue(jo, "materialCode"); //物料编码

                //获取物料组
                var materialGroupList = _baseGroupBindMaterialBLL.GetList(t => t.MaterialCode == materialCode).ToList();
                if (materialGroupList.Count == 0)
                {
                    result.success = false;
                    result.returnMsg = "物料[" + materialCode + ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_40");//]没有绑定物料组
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                var arrMainId = _qcTestProcessMaintenanceBLL.Get_ExpressionList(t => t.ProcessCode == processCode)
                    .Select(t => t.TestMaintenanceId).ToList();
                if (arrMainId.Count == 0)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_41");//检测项目不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var arrMaterialGroup = materialGroupList.Select(t => t.GroupCode);
                var testMainEntity = _qcTestMaintenanceBLL.Get_ExpressionEntity(t =>
                  arrMainId.Contains(t.Id) && arrMaterialGroup.Contains(t.SmallClass) && t.TestType == "1");
                if (testMainEntity == null)
                {
                    result.success = false;
                    result.returnMsg = ALP.Application.Service.Resources.Language.GetText("QualityManage.QC_PollingDetailController.Tips_41");//检测项目不存在
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                var detail = _qcTestItemMaintenanceBLL.Get_ExpressionList(t => t.TestMaintenanceId == testMainEntity.Id
                    && t.TestDepartment=="1").OrderBy(t => t.TestItemCoading).ToList();

                var data = new
                {
                    main = testMainEntity,
                    detail
                };
                result.resultData = data;
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion


        #region 保存新增的检验记录

        /// <summary>
        /// 功能描述: 保存新增的检验记录
        /// 创　　建: dragon
        /// 创建日期: 2022-03-21 10:42:59
        /// 任务编号: 巡检检验记录表
        /// </summary>
        /// <returns>返回列表</returns>
        [HttpPost]
        [Route("PollingDetailAndResultSave")]
        public HttpResponseMessage PollingDetailAndResultSave(JObject jo)
        {
            var result = new ResponseResult();
            var userCode = CurrentAccount.UserCode;
            var userName = CurrentAccount.UserName;
            try
            {
                var entity = JsonConvert.DeserializeObject<QC_PollingDetailEntity>(getValue(jo, "Entity"));
                var list = JsonConvert.DeserializeObject<List<QC_PollingDetailResultEntity>>(getValue(jo, "data"));

                entity.Id = Guid.NewGuid().ToString();
                entity.Creator = userCode;
                entity.CreateTime = DateTime.Now;
                entity.ModifyBy = userCode;
                entity.ModifyTime = DateTime.Now;
                entity.Inspector = userCode;
                entity.InspectionTime = DateTime.Now;
                if (list.Any(t => t.TestDepartment == "1"))
                {
                    entity.LabInspector = userCode;
                    entity.LabInspectionTime = DateTime.Now;
                    entity.LaboratoryTestStatus = "3";
                }
                else
                    entity.LaboratoryTestStatus = "1";

                foreach (var item in list)
                {
                    item.Create();
                    item.PollingDetailId = entity.Id;
                    item.Creator = userCode;
                    item.CreatTime = entity.ModifyTime;
                    item.EnabledMark = true;
                }

                var msg = "";
                TransactionOptions transactionOption = new TransactionOptions();
                //设置事务隔离级别
                transactionOption.IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted;
                // 设置事务超时时间为60秒
                transactionOption.Timeout = new TimeSpan(0, 0, 60);
                using (var ts = new TransactionScope(TransactionScopeOption.Required, transactionOption))
                //using (var ts = new TransactionScope())
                {
                    bll.Insert(entity);
                    _qcPollingDetailResultBLL.SaveEntity_List(false, userName, list, out msg);

                    ts.Complete();
                }
                result.success = true;
                result.returnMsg = ALP.Application.Service.Resources.Language.GetText("Common.Success");//操作成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = ex.Message.ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        #endregion
    }
}
