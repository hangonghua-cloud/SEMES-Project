using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Data.Repository;
using ALP.Util.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.AuthorizeManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2016.04.14 09:16
    /// 描 述：系统表单实例
    /// </summary>
    public class ModuleFormInstanceService : RepositoryFactory,IModuleFormInstanceService
    {
        #region 获取数据
#pragma warning disable CS1573 // 参数“objectId”在“ModuleFormInstanceService.GetEntityByObjectId(string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
#pragma warning disable CS1572 // XML 注释中有“keyValue”的 param 标记，但是没有该名称的参数
        /// <summary>
        /// 获取一个实体类
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ModuleFormInstanceEntity GetEntityByObjectId(string objectId)
#pragma warning restore CS1572 // XML 注释中有“keyValue”的 param 标记，但是没有该名称的参数
#pragma warning restore CS1573 // 参数“objectId”在“ModuleFormInstanceService.GetEntityByObjectId(string)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        {
            try
            {
                var expression = LinqExtensions.True<ModuleFormInstanceEntity>();
                expression = expression.And(t => t.ObjectId.Equals(objectId));
                return this.BaseRepository().FindEntity<ModuleFormInstanceEntity>(expression);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存一个实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, ModuleFormInstanceEntity entity)
        {
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    return this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    return this.BaseRepository().Update(entity);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
