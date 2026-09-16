using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-04 13:32
    /// 描 述：微应用预警配置表
    /// </summary>
    public class WebChatApplyConfigService : RepositoryFactory<WebChatApplyConfigEntity>, IWebChatApplyConfigIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public System.Data.DataTable GetPageList(Pagination pagination, string queryJson)
        {
            queryJson = (queryJson == "" ? null : queryJson);
            var queryParam = queryJson.ToJObject();

            System.Text.StringBuilder sqlSerach = new System.Text.StringBuilder();
            sqlSerach.Append(@"SELECT  dt.ItemId,dt.ApplyName,bl.LineCode, bl.LineName, wcac.ApplyConfigId,wcac.TargetValue,wcac.IfSendTime,wcac.TimingSendTime, 
                                wcac.EnabledMark,wcac.Description,wcac.CreateDate
                                from (SELECT dtS.ItemId, dtS.ItemName as ApplyName,dtS.ItemCode from Base_DataItem dtF LEFT JOIN Base_DataItem dtS on dtf.ItemId =dts.ParentId where dtf.ItemCode='WebChatApplyType') dt
                                inner JOIN  Base_WebChatApplyConfig wcac on  dt.ItemId=wcac.ApplyType
                                INNER JOIN Base_Lines bl on  wcac.LineCode= bl.Id where   1=1");

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ApplyType":              //预警所属分类
                        sqlSerach.Append(@" and dt.ApplyName like '%" + keyword + "%'");
                        break;
                    case "LineCode":              //所属线体
                        sqlSerach.Append(@" and bl.LineName like '%" + keyword + "%'");
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindTable(sqlSerach.ToString(),pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WebChatApplyConfigEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<WebChatApplyConfigEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ApplyType":              //预警所属分类
                        expression = expression.And(t => t.ApplyType.ToString().Contains(keyword));
                        break;
                    case "LineCode":              //所属线体
                        expression = expression.And(t => t.LineCode.ToString().Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["condition"].IsEmpty() && (!queryParam["begin"].IsEmpty() || !queryParam["end"].IsEmpty()))
            {
                string condition = queryParam["condition"].ToString();
                string begin = queryParam["begin"].ToString();
                string end = queryParam["end"].ToString();
                switch (condition)
                {
                    default:
                        break;
                }
            }
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WebChatApplyConfigEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, WebChatApplyConfigEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
