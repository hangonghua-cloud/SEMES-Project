using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using ALP.Application.IService.AuthorizeManage;
using ALP.Application.Service.AuthorizeManage;
namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.02 14:27
    /// 描 述：机构管理
    /// </summary>
    public class OrganizeService : RepositoryFactory<OrganizeEntity>, IOrganizeService
    {
        private IAuthorizeService<OrganizeEntity> iauthorizeservice = new AuthorizeService<OrganizeEntity>();
        #region 获取数据
        /// <summary>
        /// 机构列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<OrganizeEntity> GetList()
        {
            return this.BaseRepository().IQueryable(t=>t.EnabledMark==1).OrderByDescending(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 机构实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public OrganizeEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
#pragma warning disable CS1572 // XML 注释中有“organizeName”的 param 标记，但是没有该名称的参数
#pragma warning disable CS1573 // 参数“fullName”在“OrganizeService.ExistFullName(string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        /// <summary>
        /// 公司名称不能重复
        /// </summary>
        /// <param name="organizeName">公司名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
#pragma warning restore CS1573 // 参数“fullName”在“OrganizeService.ExistFullName(string, string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1572 // XML 注释中有“organizeName”的 param 标记，但是没有该名称的参数
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 外文名称不能重复
        /// </summary>
        /// <param name="enCode">外文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 中文名称不能重复
        /// </summary>
        /// <param name="shortName">中文名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistShortName(string shortName, string keyValue)
        {
            var expression = LinqExtensions.True<OrganizeEntity>();
            expression = expression.And(t => t.ShortName == shortName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.OrganizeId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除机构
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            int count = this.BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
                throw new Exception("当前所选数据有子节点数据！");
            this.BaseRepository().ExecuteBySql($"UPDATE Base_Organize SET EnabledMark=0,DeleteMark=1 WHERE OrganizeId='{keyValue}'");
        }
        /// <summary>
        /// 保存机构表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="organizeEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, OrganizeEntity organizeEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                organizeEntity.Modify(keyValue);
                this.BaseRepository().Update(organizeEntity);
            }
            else
            {
                organizeEntity.Create();
                organizeEntity.EnabledMark = 1;
                organizeEntity.DeleteMark = 0;
                this.BaseRepository().Insert(organizeEntity);
            }
        }
        #endregion
    }
}
