using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;
using System.Data;
using System.Text;
using ALP.Data;
using System.Data.Common;
using System.Linq.Expressions;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-11 09:48
    /// 描 述：报表配置页面
    /// </summary>
    public class Base_ReportRecordService : RepositoryFactory<Base_ReportRecordEntity>, Base_ReportRecordIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ReportRecordEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        public IEnumerable<Base_ReportRecordEntity> GetList(Expression<Func<Base_ReportRecordEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns></returns>
        public Base_ReportRecordEntity GetEntity(Expression<Func<Base_ReportRecordEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ReportRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM [dbo].[Base_ReportRecord] WHERE 1=1 AND Superior=1 ");
            var parameter = new List<DbParameter>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["ParentId"].IsEmpty())
            {
                var t = queryParam["ParentId"].ToString();
                sql.Append(@" AND ParentId = @ParentId ");
                parameter.Add(DbParameters.CreateDbParameter("@ParentId", t));
            }
            if (pagination == null)
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            else
            {
                return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            }
           
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
        public void SaveForm(string keyValue, Base_ReportRecordEntity entity)
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
