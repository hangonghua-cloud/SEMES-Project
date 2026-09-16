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
    /// 日 期：2019-11-03 11:53
    /// 描 述：微应用人员管理
    /// </summary>
    public class WebChatApplyUserService : RepositoryFactory<WebChatApplyUserEntity>, WebChatApplyUserIService
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
            sqlSerach.Append(@"SELECT  dt.ApplyName,dt.ItemId,bl.LineCode, bl.LineName, wcapu.ApplyUserId,wcapu.ParentId,wcapu.EnCode,wcapu.RealName,
                                wcapu.OuterPhone ,wcapu.IfManager,wcapu.EnabledMark,wcapu.Description,wcapu.CreateDate
                                from (SELECT dtS.ItemId, dtS.ItemName as ApplyName,dtS.ItemCode from Base_DataItem dtF LEFT JOIN Base_DataItem dtS on dtf.ItemId =dts.ParentId where dtf.ItemCode='WebChatApplyType') dt
                                inner JOIN Base_WebChatApplyUser wcapu on  dt.ItemId=wcapu.ParentId
                                left JOIN Base_Lines bl on  wcapu.LineCode= bl.Id where   1=1");

            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "ApplyName":              //微应用预警名称
                        sqlSerach.Append(@" and dt.ApplyName like '%" + keyword + "%'");
                        break;
                    case "LineCode":              //所属线体
                        sqlSerach.Append(@" and bl.LineName like '%" + keyword + "%'");
                        break;
                    case "EnCode":              //员工编号
                        sqlSerach.Append(@" and wcapu.EnCode like '%" + keyword + "%'");
                        break;
                    case "RealName":              //员工姓名
                        sqlSerach.Append(@" and wcapu.RealName like '%" + keyword + "%'");
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindTable(sqlSerach.ToString(), pagination);
        }
       
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WebChatApplyUserEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<WebChatApplyUserEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "RealName":              //机构分类
                        expression = expression.And(t => t.RealName.ToString().Contains(keyword));
                        break;
                    case "EnCode":              //员工代码
                        expression = expression.And(t => t.EnCode.ToString().Contains(keyword));
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
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<WebChatApplyUserEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.EnCode).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public WebChatApplyUserEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 员工编号不能重复
        /// </summary>
        /// <param name="enCode">员工编号</param>
        /// <param name="applyType">预警类型主键</param>
        /// <param name="lineCode">所属产线</param>
        /// <param name="keyValue">是否新增、编辑主键ID</param>
        /// <returns></returns>
        public string ExistEnCode(string enCode, string applyType, string lineCode,string keyValue)
         {
            var expression = LinqExtensions.True<WebChatApplyUserEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            expression = expression.And(t => t.ParentId == applyType);
            var getIfExists = this.BaseRepository().IQueryable(expression).ToList();
            var vTureStatus = string.Empty;
            if (getIfExists.Count > 0)
            {
                //已添加了其它产线，再添加所属线体为空时判断提示
                if (string.IsNullOrEmpty(lineCode))
                {
                    getIfExists = getIfExists.Where(t => t.LineCode == lineCode).ToList();
                    if (getIfExists.Count > 0)
                        vTureStatus = "AllLineExists";//已经添加所有产线不可以添加
                    else
                        vTureStatus = "OkOtherLineExists";//已经添加其他产线不可以添加
                }
                else//产线不为空则继续判断
                {
                    //先添加了所属线体为空，再添加其它产线，判断并提示
                    var getIfAllExists = getIfExists.Where(t => string.IsNullOrEmpty(t.LineCode) == true).ToList();
                    if (getIfAllExists.Count > 0 && string.IsNullOrEmpty(keyValue))
                    {
                        vTureStatus = "AllLineExists";//已经添加所有产线不可以添加
                    }
                    else
                    {
                        //添加了在其他产线。在添加其他产线 判断并提示
                        getIfExists = getIfExists.Where(t => t.LineCode == lineCode).ToList();
                        if (getIfExists.Count > 0)
                            vTureStatus = "OkLineExists";//已有产线存在了不可以添加
                        else
                            vTureStatus = "NoLineExists";//不存在可以添加
                    }
                }

            }
            else
            {
                vTureStatus = "NoLineExists";//不存在可以添加
            }
            return vTureStatus;
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
        public void SaveForm(string keyValue, WebChatApplyUserEntity entity)
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
