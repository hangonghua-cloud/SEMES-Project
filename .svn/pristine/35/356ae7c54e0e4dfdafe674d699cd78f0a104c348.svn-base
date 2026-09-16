
using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.Service.SystemManage;
using ALP.Util.WebControl;
using ALP.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 系统业务管理类
    /// </summary>
    public class SystemBLL
    {
        SystemService service = new SystemService();

        /// <summary>
        /// 获取生产车间字典集合函数
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> LoadWorkShopDictionary()
        {
            return service.LoadDataDictionary("WorkShopCode");
        }


        /// <summary>
        /// 获取年月字典集合函数
        /// </summary>
        /// <param name="keyCode"></param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> LoadYearMonthDictionary()
        {
            return service.LoadDataDictionary("YearMonth");
        }

        /// <summary>
        /// 获取供应商
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetSuppliersPageList(Pagination pagination, string queryJson, ref string msg)
        {
            return service.GetSuppliersPageList(pagination, queryJson, ref msg);
        }

        /// <summary>
        /// 获取供应商不分页数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetSuppliersList(string queryJson)
        {
            return service.GetSuppliersList(queryJson);
        }
        public DataTable Login(string userId,string pwd)
        {
            return service.Login(userId,pwd);
        }
        public DataTable GetRoleFunctions(string queryJson)
        {
            var cachekey = "roleFunctions_cacheKey";
            var RoleFunctionsCache = CacheFactory.Cache().GetCache<DataTable>(cachekey);
            if (RoleFunctionsCache == null)
            {
                var roleFunctionsDt = service.GetRoleFunctions(queryJson);
                CacheFactory.Cache().WriteCache(roleFunctionsDt, cachekey, DateTime.Now.AddMinutes(10));
            }
            return service.GetRoleFunctions(queryJson);
        }
        /// <summary>
        /// 查询所有角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public DataTable GetRoles_PDA()
        {
            return service.GetRoles_PDA();
        }
        /// <summary>
        /// 获取用户部门和数据隔离标签
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IEnumerable<UserInfoEntity> GetEntity(string code)
        {
            return service.GetEntity(code);
        }
    }
}
