using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.MaterialManage;
using ALP.WebApi.Models;
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Filter;
using System.Web.Http;
using System.Transactions;
namespace ALP.Application.WebApi.Controllers.MaterialManage
{
    /// <summary>
    /// [MM_SemiProductOut]控制器
    /// 描述:半成品出库记录
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:23:43
    /// </summary>
    [Auth]
    [RoutePrefix("MMSemiProductOut")]
    public class MMSemiProductOutController : ApiBaseController
    {
        private MMSemiProductOutService _MMSemiProductOutService = new MMSemiProductOutService();
         
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///任务编号: 半成品出库记录
        ///</summary>
        ///<param name="jo">pagination 分页参数;queryJson 查询参数</param>
        /// <returns>返回分页列表</returns>
        [HttpPost]
        [Route("GetPageDataTableList")]
        public HttpResponseMessage GetPageDataTableList(JObject jo)
        {
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
                 }
                 string queryJson = getValue(jo, "queryJson");
                 var watch = CommonHelper.TimerStart();
                 
                 var data = _MMSemiProductOutService.GetPageDataTableList(pagination, queryJson);
                 var JsonData = new
                 {
                     rows = data,
                     total = pagination != null ? pagination.total : data.Rows.Count,
                     page = pagination != null ? pagination.page : 1,
                     records = pagination != null ? pagination.records : data.Rows.Count,
                     costtime = CommonHelper.TimerEnd(watch)
                 };
                 
                 return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"), JsonData);//操作成功
             }
             catch (Exception ex)
             {
                 return  AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
             }
        }
        #endregion
        
        #region 获取实体类
        /// <summary>
        ///功能描述:  获取实体类
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///任务编号: 半成品出库记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值</param>
        ///<returns></returns>
        [HttpPost]
        [Route("GetFormJson")]
        public HttpResponseMessage GetFormJson(JObject jo)
        {
             try
             {
                 var keyValue = getValue(jo, "KeyValue");
                 
                 var data = _MMSemiProductOutService.GetEntity(t=>t.Id==keyValue);
                 return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"), data);//操作成功
             }
             catch (Exception ex)
             {
                 return  AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
             }
        }
        #endregion
        
        #region 保存表单（新增、修改）
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///任务编号: 半成品出库记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveForm")]
        public HttpResponseMessage SaveForm(JObject jo)
        {
             try
             {
                 var keyValue = getValue(jo, "KeyValue");
                 var entityStr = getValue(jo, "Entity");
                 
                 if (string.IsNullOrEmpty(entityStr))
                     return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMSemiProductOutController.Tips_4"));//缺少Entity参数！
                 
                 var entity = JsonConvert.DeserializeObject<MMSemiProductOutEntity>(entityStr);
                 
                 if (!string.IsNullOrEmpty(keyValue))
                 {
                     entity.ModifyCode = CurrentAccount.UserCode;
                     entity.ModifyName = CurrentAccount.UserName;
                     entity.ModifyTime = DateTime.Now;
                 }
                 else
                 {
                     entity.CreatorCode = CurrentAccount.UserCode;
                     entity.CreatorName = CurrentAccount.UserName;
                     entity.IsDeleted = false;
                     entity.CreateTime = DateTime.Now;
                 }
                 
                 int isok = _MMSemiProductOutService.SaveEntity(keyValue, entity);
                 if (isok == 0)
                     return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMSemiProductOutController.Tips_5"));//操作失败
                 
                 return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
             }
             catch (Exception ex)
             {
                 return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
             }
        }
        #endregion
        
        #region 批量保存
        /// <summary>
        ///功能描述:  批量保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///任务编号: 半成品出库记录
        ///</summary>
        ///<param name="jo">json参数, 包含keyValue 主键值, list 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("SaveBatchForm")]
        public HttpResponseMessage SaveBatchForm(JObject jo)
        {
             var isUpdate = false;
             try
             {
                 var keyValue = getValue(jo, "KeyValue");
                 var entity = JsonConvert.DeserializeObject<MMSemiProductOutEntity>(getValue(jo, "Entity"));
                 var list = JsonConvert.DeserializeObject<List<MMSemiProductOutEntity>>(getValue(jo, "data"));
                 
                 if (!string.IsNullOrEmpty(keyValue))
                     isUpdate = true;
                 
                 foreach(var item in list)
                 {
                     item.Id=Guid.NewGuid().ToString();
                 }
                 int isok = _MMSemiProductOutService.SaveEntity_List(isUpdate, list);
                 if (isok == 0)
                     return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMSemiProductOutController.Tips_8"));//操作失败
                 
                 return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
             }
             catch (Exception ex)
             {
                 return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
             }
        }
        #endregion
        
        #region 删除表单
        /// <summary>
        ///功能描述:  删除表单
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///任务编号: 半成品出库记录
        ///</summary>
        ///<param name="jo">json参数, 包含entity 实体对象</param>
        ///<returns></returns>
        [HttpPost]
        [Route("RemoveForm")]
        public HttpResponseMessage RemoveForm(JObject jo)
        {
             try
             {
                 var id = getValue(jo, "id");
                 if (string.IsNullOrEmpty(id))
                     return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMSemiProductOutController.Tips_10"));//id参数不能为空
                 
                 int isok = _MMSemiProductOutService.RemoveForm(t => t.Id == id);
                 if (isok == 0)
                     return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("MaterialManage.MMSemiProductOutController.Tips_5"));//操作失败
                 
                 return AjaxResult(true, ALP.Application.Service.Resources.Language.GetText("Common.Success"));//操作成功
             }
             catch (Exception ex)
             {
                 return AjaxResult(false, ALP.Application.Service.Resources.Language.GetText("Common.ErrorWithOther2") + ex.Message);//操作失败：
             }
        }
        #endregion
    }
}

