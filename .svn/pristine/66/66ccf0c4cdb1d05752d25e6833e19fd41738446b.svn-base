using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq.Expressions;
using ALP.Cache.Factory;

namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.4.9 16:19
    /// 描 述：打印服务人员绑定
    /// </summary>
    public class BsPeopleByPrintServerBLL
    {
        private BsPeopleByPrintServerService service = new BsPeopleByPrintServerService();

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsPeopleByPrintServerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetList(string code)
        {
            return service.GetList(code);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public string GetPrintServerByPersonCode(string personCode)
        {
            string printServerCacheKey = "PrintServerCacheKey";
            string printServerIp = "";
            DataTable cachePrintServers = CacheFactory.Cache().GetCache<DataTable>(printServerCacheKey);
            if (cachePrintServers == null)
            {
                cachePrintServers = service.GetPrintServerByPersonCode();
                CacheFactory.Cache().WriteCache(cachePrintServers, printServerCacheKey, DateTime.Now.AddHours(10));
            }
          else
            {
                DataRow[] drs = cachePrintServers.Select(string.Format("PeopleCode = '{0}' ", personCode));
                if (drs != null && drs.Length > 0)
                {
                    printServerIp = drs[0]["IP"].ToString();
                }
            }
            return printServerIp;
        }


        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue)
        {
            try
            {
                return service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(BsPeopleByPrintServerEntity entity)
        {
            try
            {
                string printServerCacheKey = "PrintServerCacheKey";
                CacheFactory.Cache().RemoveCache(printServerCacheKey);
                return service.SaveForm(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int UpdateIsDeptLimit(string queryJson)
        {
            return service.UpdateIsDeptLimit(queryJson);
        }
        #endregion
    }
}
