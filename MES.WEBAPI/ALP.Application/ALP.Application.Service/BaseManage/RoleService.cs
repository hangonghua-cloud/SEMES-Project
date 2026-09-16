using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ALP.Data;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.AuthorizeManage;
using ALP.Application.IService.AuthorizeManage;
using System;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.4 14:31
    /// 描 述：角色管理
    /// </summary>
    public class RoleService : RepositoryFactory<RoleEntity>, IRoleService
    {
        private IAuthorizeService<RoleEntity> iauthorizeservice = new AuthorizeService<RoleEntity>();

        #region 获取数据
        /// <summary>
        /// 角色列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 1).And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        ///// <summary>
        ///// 角色列表
        ///// </summary>
        ///// <param name="pagination">分页</param>
        ///// <param name="queryJson">查询参数</param>
        ///// <returns></returns>
        //public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        //{
        //    var expression = LinqExtensions.True<RoleEntity>();
        //    var queryParam = queryJson.ToJObject();
        //    //查询条件
        //    if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
        //    {
        //        string condition = queryParam["condition"].ToString();
        //        string keyword = queryParam["keyword"].ToString();
        //        switch (condition)
        //        {
        //            case "EnCode":            //角色编号
        //                expression = expression.And(t => t.EnCode.Contains(keyword));
        //                break;
        //            case "FullName":          //角色名称
        //                expression = expression.And(t => t.FullName.Contains(keyword));
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //    expression = expression.And(t => t.Category == 1);
        //    expression = expression.And(t => t.EnabledMark == 1);
        //    return this.BaseRepository().FindList(expression, pagination);
        //}

        /// <summary>
        /// 分页查询
        /// </summary>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {

            StringBuilder sql = new StringBuilder("select * from [Base_Role] where 1=1 and DeleteMark=0 and EnabledMark=1 ");

            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    if (keyword != "undefined")
                    {
                        sql.Append($"AND {condition} LIKE '%{keyword}%' ");
                    }
                }
                else if (!queryParam["EnCode"].IsEmpty()|| !queryParam["FullName"].IsEmpty())
                {
                    if (!queryParam["EnCode"].IsEmpty())
                    {
                        string EnCode = queryParam["EnCode"].ToString();
                        sql.Append($"AND EnCode LIKE '%{EnCode}%' ");
                    }
                    if (!queryParam["FullName"].IsEmpty())
                    {
                        string FullName = queryParam["FullName"].ToString();
                        sql.Append($"AND FullName LIKE '%{FullName}%' ");
                    }
                    //string begin = queryParam["StartTime"].ToString();
                    //string end = queryParam["EndTime"].ToString();
                    //if (!string.IsNullOrEmpty(begin))
                    //    sql.Append($"AND DATEDIFF(dd,CONVERT(varchar(100),'{begin}', 23), createDate)>=0 ");
                    //if (!string.IsNullOrEmpty(end))
                    //    sql.Append($"AND DATEDIFF(dd, createDate,CONVERT(varchar(100),'{end}', 23))>=0 ");
                }
                else if(!queryParam["filters"].IsEmpty())
                {
                    sql.Append(" and  ");
                    var Advancesql = BaseService.AutoGroupAdvanaceQuerySql(queryParam);
                    sql.Append(Advancesql);
                }
            }
            try
            {
                this.BaseRepository().FindList(sql.ToString(), pagination);
            }
            catch (Exception ex)
            {
                throw;
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }

        /// <summary>
        /// 角色列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetAllList()
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  r.RoleId ,
				                    o.FullName AS OrganizeId ,
				                    r.Category ,
				                    r.EnCode ,
				                    r.FullName ,
				                    r.SortCode ,
				                    r.EnabledMark ,
				                    r.Description ,
				                    r.CreateDate
                    FROM    Base_Role r
				                    LEFT JOIN Base_Organize o ON o.OrganizeId = r.OrganizeId
                    WHERE   o.FullName is not null and r.Category = 1 and r.EnabledMark =1
                    ORDER BY o.FullName, r.SortCode");
            return this.BaseRepository().FindList(strSql.ToString());
        }
        /// <summary>
        /// 角色实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public RoleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 角色编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.EnCode == enCode).And(t => t.Category == 1);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 角色名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.FullName == fullName).And(t => t.Category == 1);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().ExecuteBySql($"UPDATE Base_Role SET EnabledMark=0,DeleteMark=1 WHERE RoleId ='{keyValue}'");
        }
        /// <summary>
        /// 保存角色表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="roleEntity">角色实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoleEntity roleEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //验证编码唯一
                var codeExpression = LinqExtensions.True<RoleEntity>();
                codeExpression = codeExpression.And(t => t.EnCode == roleEntity.EnCode).And(t => t.Category == 1);
                codeExpression = codeExpression.And(t => t.RoleId != keyValue);
                var existEnCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
                if (!existEnCode)
                    throw new Exception("编码已存在！");
                //验证名称唯一
                var nameExpression = LinqExtensions.True<RoleEntity>();
                nameExpression = nameExpression.And(t => t.FullName == roleEntity.FullName).And(t => t.Category == 1).And(t => t.OrganizeId == roleEntity.OrganizeId);
                nameExpression = nameExpression.And(t => t.RoleId != keyValue);
                var existFullName = this.BaseRepository().IQueryable(nameExpression).Count() == 0 ? true : false;
                if (!existFullName)
                    throw new Exception("名称已存在！");

                roleEntity.Modify(keyValue);
                this.BaseRepository().Update(roleEntity);
            }
            else
            {
                //验证编码唯一
                var codeExpression = LinqExtensions.True<RoleEntity>();
                codeExpression = codeExpression.And(t => t.EnCode == roleEntity.EnCode).And(t => t.Category == 1);
                var existEnCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
                if (!existEnCode)
                    throw new Exception("编码已存在！");
                //验证名称唯一
                var nameExpression = LinqExtensions.True<RoleEntity>();
                nameExpression = nameExpression.And(t => t.FullName == roleEntity.FullName).And(t => t.Category == 1).And(t => t.OrganizeId == roleEntity.OrganizeId);
                var existFullName = this.BaseRepository().IQueryable(nameExpression).Count() == 0 ? true : false;
                if (!existFullName)
                    throw new Exception("名称已存在！");

                roleEntity.Create();
                roleEntity.Category = 1;
                roleEntity.EnabledMark = 1;
                roleEntity.DeleteMark = 0;
                this.BaseRepository().Insert(roleEntity);
            }
        }
        #endregion
    }
}
