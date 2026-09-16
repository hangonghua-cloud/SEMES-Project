using ALP.Application.Busines.AuthorizeManage;
using ALP.Application.Busines.BaseManage;
using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.Entity.BaseManage;
using ALP.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ALP.Application.Cache
{

    public class ModuleCache
    {
        private ModuleBLL busines = new ModuleBLL();

        /// <summary>
        /// 功能列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<IEnumerable<ModuleEntity>>(busines.cacheKey);
            if (cacheList == null)
            {
                var data = busines.GetList();
                CacheFactory.Cache().WriteCache(data, busines.cacheKey);
                return data;
            }
            else
            {
                return cacheList;
            }
        }
        /// <summary>
        ///功能
        /// </summary>
        /// <param name="organizeId">功能Id</param>
        /// <returns></returns>
        public ModuleEntity GetEntity(string moduleId)
        {
            var data = this.GetList();
            if (!string.IsNullOrEmpty(moduleId))
            {
                var d = data.Where(t => t.ModuleId == moduleId).ToList<ModuleEntity>();
                if (d.Count > 0)
                {
                    return d[0];
                }
            }
            return new ModuleEntity();
        }

        public void RemoveCache(string key)
        {
            CacheFactory.Cache().RemoveCache(busines.cacheKey);
        }
    }
}
