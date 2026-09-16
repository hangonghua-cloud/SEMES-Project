using ALP.Application.Code;
using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2016.03.29 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeService<T> : RepositoryFactory<T>, IAuthorizeService<T> where T : class,new()
    {
        private IRepository db = new RepositoryFactory().BaseRepository();
        private AuthorizeService authorizeService = new AuthorizeService();
        #region 带权限的数据源查询
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.IQueryable()”的 XML 注释
        public IQueryable<T> IQueryable()
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.IQueryable()”的 XML 注释
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().IQueryable();
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().IQueryable(lambda);
            }
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.IQueryable(Expression<Func<T, bool>>)”的 XML 注释
        public IQueryable<T> IQueryable(Expression<Func<T, bool>> condition)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.IQueryable(Expression<Func<T, bool>>)”的 XML 注释
        {
            if (GetReadUserId() != "")
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                condition = condition.And(lambda);
            }
            return db.IQueryable<T>(condition);
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(Pagination)”的 XML 注释
        public IEnumerable<T> FindList(Pagination pagination)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(Pagination)”的 XML 注释
        {
            if (GetReadUserId() == "")
            {
                return this.BaseRepository().FindList(pagination);
            }
            else
            {
                var parameter = Expression.Parameter(typeof(T), "t");
                var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
                var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
                return this.BaseRepository().FindList(lambda, pagination);
            }
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(Expression<Func<T, bool>>, Pagination)”的 XML 注释
        public IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(Expression<Func<T, bool>>, Pagination)”的 XML 注释
        {
            //if (GetReadUserId() != "")
            //{
            //    var parameter = Expression.Parameter(typeof(T), "t");
            //    var authorConditon = Expression.Constant(GetReadUserId()).Call("Contains", parameter.Property("CreateUserId"));
            //    var lambda = authorConditon.ToLambda<Func<T, bool>>(parameter);
            //    condition = condition.And(lambda);
            //}
            return this.BaseRepository().FindList(condition, pagination);
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string)”的 XML 注释
        public IEnumerable<T> FindList(string strSql)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string)”的 XML 注释
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql);
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, DbParameter[])”的 XML 注释
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, DbParameter[])”的 XML 注释
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter);
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, Pagination)”的 XML 注释
        public IEnumerable<T> FindList(string strSql, Pagination pagination)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, Pagination)”的 XML 注释
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, pagination);
        }
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, DbParameter[], Pagination)”的 XML 注释
        public IEnumerable<T> FindList(string strSql, DbParameter[] dbParameter, Pagination pagination)
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“AuthorizeService<T>.FindList(string, DbParameter[], Pagination)”的 XML 注释
        {
            strSql = strSql + (GetReadSql() == "" ? "" : string.Format("and CreateUserId in({0})", GetReadSql()));
            return this.BaseRepository().FindList(strSql, dbParameter, pagination);
        }
        #endregion

        #region 取数据权限用户
        private string GetReadUserId()
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return "";
            }
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorizeUserId;
        }
        private string GetReadSql()
        {
            return OperatorProvider.Provider.Current().DataAuthorize.ReadAutorize;
        }
        #endregion
    }
}
