using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Data.Repository;
using ALP.Util.Extension;
using System.Collections.Generic;
using System.Linq;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.10.29 15:13
    /// 描 述：系统视图
    /// </summary>
    public class ModuleColumnService : RepositoryFactory<ModuleColumnEntity>, IModuleColumnService
    {
        #region 获取数据
        /// <summary>
        /// 视图列表
        /// </summary>
        /// <returns></returns>
        public List<ModuleColumnEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 视图列表
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <returns></returns>
        public List<ModuleColumnEntity> GetList(string moduleId)
        {
            var expression = LinqExtensions.True<ModuleColumnEntity>();
            expression = expression.And(t => t.ModuleId.Equals(moduleId));
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 视图实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ModuleColumnEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
#pragma warning disable CS1572 // XML 注释中有“moduleButtonEntity”的 param 标记，但是没有该名称的参数
#pragma warning disable CS1573 // 参数“moduleColumnEntity”在“ModuleColumnService.AddEntity(ModuleColumnEntity)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        /// <summary>
        /// 添加视图
        /// </summary>
        /// <param name="moduleButtonEntity">视图实体</param>
        public void AddEntity(ModuleColumnEntity moduleColumnEntity)
#pragma warning restore CS1573 // 参数“moduleColumnEntity”在“ModuleColumnService.AddEntity(ModuleColumnEntity)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning restore CS1572 // XML 注释中有“moduleButtonEntity”的 param 标记，但是没有该名称的参数
        {
            moduleColumnEntity.Create();
            this.BaseRepository().Insert(moduleColumnEntity);
        }
        #endregion
    }
}
