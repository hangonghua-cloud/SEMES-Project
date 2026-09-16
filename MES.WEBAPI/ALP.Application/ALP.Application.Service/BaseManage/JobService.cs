using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.AuthorizeManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.4 14:31
    /// 描 述：职位管理
    /// </summary>
    public class JobService : RepositoryFactory<RoleEntity>, IJobService
    {
        private IAuthorizeService<RoleEntity> iauthorizeservice = new AuthorizeService<RoleEntity>();

        #region 获取数据
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetList()
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.Category == 3).And(t => t.EnabledMark == 1).And(t => t.DeleteMark == 0);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 职位列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<RoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            var queryParam = queryJson.ToJObject();
            //机构主键
            if (!queryParam["organizeId"].IsEmpty())
            {
                string organizeId = queryParam["organizeId"].ToString();
                expression = expression.And(t => t.OrganizeId.Equals(organizeId));
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "EnCode":            //职位编号
                        expression = expression.And(t => t.EnCode.Contains(keyword));
                        break;
                    case "FullName":          //职位名称
                        expression = expression.And(t => t.FullName.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            expression = expression.And(t => t.Category == 3);
            expression = expression.And(t => t.EnabledMark == 1);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 职位实体
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
        /// 职位编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.EnCode == enCode).And(t => t.Category == 3);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 职位名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<RoleEntity>();
            expression = expression.And(t => t.FullName == fullName).And(t => t.Category == 3);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.RoleId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().ExecuteBySql($"UPDATE Base_Role SET EnabledMark=0,DeleteMark=1 WHERE RoleId ='{keyValue}'");
        }
        /// <summary>
        /// 保存职位表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="jobEntity">职位实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, RoleEntity jobEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                //验证编码唯一
                var codeExpression = LinqExtensions.True<RoleEntity>();
                codeExpression = codeExpression.And(t => t.EnCode == jobEntity.EnCode).And(t => t.Category == 3);
                codeExpression = codeExpression.And(t => t.RoleId != keyValue);
                var existEnCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
                if (!existEnCode)
                    throw new Exception("编码已存在！");
                //验证名称唯一
                var nameExpression = LinqExtensions.True<RoleEntity>();
                nameExpression = nameExpression.And(t => t.FullName == jobEntity.FullName).And(t => t.Category == 3).And(t => t.OrganizeId == jobEntity.OrganizeId);
                nameExpression = nameExpression.And(t => t.RoleId != keyValue);
                var existFullName = this.BaseRepository().IQueryable(nameExpression).Count() == 0 ? true : false;
                if (!existFullName)
                    throw new Exception("名称已存在！");

                jobEntity.Modify(keyValue);
                this.BaseRepository().Update(jobEntity);
            }
            else
            {
                //验证编码唯一
                var codeExpression = LinqExtensions.True<RoleEntity>();
                codeExpression = codeExpression.And(t => t.EnCode == jobEntity.EnCode).And(t => t.Category == 3);
                var existEnCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
                if (!existEnCode)
                    throw new Exception("编码已存在！");
                //验证名称唯一
                var nameExpression = LinqExtensions.True<RoleEntity>();
                nameExpression = nameExpression.And(t => t.FullName == jobEntity.FullName).And(t => t.Category == 3).And(t => t.OrganizeId == jobEntity.OrganizeId);
                var existFullName = this.BaseRepository().IQueryable(nameExpression).Count() == 0 ? true : false;
                if (!existFullName)
                    throw new Exception("名称已存在！");

                jobEntity.Create();
                jobEntity.Category = 3;
                jobEntity.EnabledMark = 1;
                jobEntity.DeleteMark = 0;
                this.BaseRepository().Insert(jobEntity);
            }
        }
        #endregion
    }
}
