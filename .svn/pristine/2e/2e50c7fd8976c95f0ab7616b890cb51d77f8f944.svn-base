using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;
using System.Linq.Expressions;
using System.Data.Common;
using System.Data;
using System.Text;
using ALP.Data;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-27 15:19
    /// 描 述：报表权限角色
    /// </summary>
    public class Base_ReportRoleService : RepositoryFactory<Base_ReportRoleEntity>, Base_ReportRoleIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="condition">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ReportRoleEntity> GetList(Expression<Func<Base_ReportRoleEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ReportRoleEntity GetEntity(string keyValue)
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
            sql.Append(@"SELECT * FROM [dbo].[Base_ReportRole] WHERE 1=1");
            var parameter = new List<DbParameter>();
            //var queryParam = queryJson.ToJObject();
            //if (!queryParam["StartTime"].IsEmpty())
            //{
            //    var t = Convert.ToDateTime(queryParam["StartTime"]).ToLocalTime().ToString("yyyy-MM-dd 08:00");
            //    sql.Append(@" AND OperatedTime >= @StartTime ");
            //    parameter.Add(DbParameters.CreateDbParameter("@StartTime", t));
            //}
            var table = new RepositoryFactory().BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            return table;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        public Base_ReportRoleEntity GetEntity(Expression<Func<Base_ReportRoleEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
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
        public void SaveForm(string keyValue, Base_ReportRoleEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                //entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        public void InsertList(List<Base_ReportRoleEntity> list)
        {
            this.BaseRepository().Insert(list);
        }
        #endregion

        #region 获取角色下的用户
        /// <summary>
        /// 获取角色下的用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetReportRoleUserList(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@"
                          SELECT 
	                        R.GroupId,R.Remarks,R.CreateTime,P.Code,P.Name
                          FROM dbo.Base_ReportRole R
                          INNER JOIN dbo.BS_People P ON P.Code=R.UserCode
                          WHERE 1=1 ") ;
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                if (!queryParam["GroupId"].IsEmpty())
                {
                    sql.Append($" AND R.GroupId= '{queryParam["GroupId"].ToString()}'");
                }
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
        /// <summary>
        /// 获取不在当前组下的用户
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetUserNotInGroup(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@"                   
                          SELECT 
	                        P.ID,P.Code UserCode,P.Name,P.Sex,depart.[Name] DepartName ,V.ItemName PositionName
                          FROM dbo.BS_People P
                          left join [dbo].[BS_Departments] depart on depart.Code= P.[Department_ID]
                          LEFT JOIN dbo.V_DataDictionary V ON V.EnCode='Position' AND P.Position_ID=V.ItemValue
                          WHERE  NOT EXISTS (SELECT 1 FROM dbo.Base_ReportRole R WHERE P.Code=R.UserCode AND R.GroupId =@GroupId) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                if (!queryParam["GroupId"].IsEmpty())
                {
                    parameter.Add(DbParameters.CreateDbParameter("@GroupId", queryParam["GroupId"].ToString()));
                }
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND P.Code like '%{queryParam["Code"].ToString()}%'");
                }
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND P.Name like '%{queryParam["Name"].ToString()}%'");
                }
                if (!queryParam["DeptName"].IsEmpty())
                {
                    sql.Append($" AND depart.[Name] like '%{queryParam["DeptName"].ToString()}%'");
                }
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
    }
}
