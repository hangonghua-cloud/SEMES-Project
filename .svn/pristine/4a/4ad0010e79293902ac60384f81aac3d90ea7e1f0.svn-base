using ALP.Application.Code;
using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.Entity.AuthorizeManage.ViewModel;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Application.Service.AuthorizeManage;
using ALP.Application.Service.BaseManage;
using ALP.Cache.Factory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public class AuthorizeBLL
    {
        private IAuthorizeService service = new AuthorizeService();
        private ModuleBLL moduleBLL = new ModuleBLL();
        private ModuleButtonBLL moduleButtonBLL = new ModuleButtonBLL();
        private ModuleColumnBLL moduleColumnBLL = new ModuleColumnBLL();
        private UserService userService = new UserService();

        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleBLL.GetList().FindAll(t => t.EnabledMark.Equals(1));
            }
            else
            {
                return service.GetModuleList(userId, "");
            }
        }
        /// <summary>
        /// 获取授权的菜单功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetIsMenuModuleList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleBLL.GetIsMenuList().FindAll(t => t.EnabledMark.Equals(1));
            }
            else
            {
                return service.GetIsMenuModuleList(userId, "");
            }
        }
        /// <summary>
        /// 根据登录账号获取对应的权限
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetModuleListByAccount(string account)
        {
            string userId = string.Empty;

            UserEntity entity = userService.GetEntityByAccount(account);
            if (entity != null)
                userId = entity.UserId;
            else
                return null;

            return service.GetModuleList(userId);

        }
        public IEnumerable<ModuleEntity> GetModuleListByName(string userId, string fullName = "")
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                //查询出所有的
                List<ModuleEntity> list = moduleBLL.GetList().FindAll(t => t.EnabledMark.Equals(1));
                //当前复合条件 所有的
                List<ModuleEntity> listSearch = list.FindAll(t => t.FullName.Contains(fullName));
                if (string.IsNullOrEmpty(fullName))
                {
                    return moduleBLL.GetList().FindAll(t => t.EnabledMark.Equals(1));
                }
                listFinal = new List<ModuleEntity>();
                for (int i = 0; i < listSearch.Count; i++)
                {
                    InitFinal(listSearch[i]);
                    GetParentList(list, listSearch[i]);
                    GetChildList(list, listSearch[i]);
                }
                return listFinal;
            }
            else
            {
                return service.GetModuleList(userId, fullName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public IEnumerable<ModuleEntity> GetAPPModuleList(string userId, string fullName)
        {
            return service.GetModuleList(userId, fullName);
        }

        //最终需要返回的
        List<ModuleEntity> listFinal = new List<ModuleEntity>();
        private void GetParentList(List<ModuleEntity> list, ModuleEntity module)
        {
            ModuleEntity parentItem = list.Find(t => t.ModuleId == module.ParentId);
            if (parentItem != null)
            {
                InitFinal(parentItem);
                GetParentList(list, parentItem);
            }
        }
        private void GetChildList(List<ModuleEntity> list, ModuleEntity module)
        {
            List<ModuleEntity> childItemList = list.FindAll(t => t.ParentId == module.ModuleId);
            if (childItemList.Count > 0)
            {
                for (int i = 0; i < childItemList.Count; i++)
                {
                    InitFinal(childItemList[i]);
                    GetChildList(list, childItemList[i]);
                }

            }
        }
        private void InitFinal(ModuleEntity item)
        {
            ModuleEntity me = listFinal.Find(t => t.FullName == item.FullName);
            if (me == null)
            {
                listFinal.Add(item);
            }
        }
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleButtonBLL.GetList();
            }
            else
            {
                return service.GetModuleButtonList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId)
        {
            if (OperatorProvider.Provider.Current().IsSystem)
            {
                return moduleColumnBLL.GetList();
            }
            else
            {
                return service.GetModuleColumnList(userId);
            }
        }
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<AuthorizeUrlModel> GetUrlList(string userId)
        {
            return service.GetUrlList(userId);
        }
        /// <summary>
        /// Action执行权限认证
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="moduleId">模块Id</param>
        /// <param name="action">请求地址</param>
        /// <returns></returns>
        public bool ActionAuthorize(string userId, string moduleId, string action)
        {
            List<AuthorizeUrlModel> authorizeUrlList = new List<AuthorizeUrlModel>();
            var cacheList = CacheFactory.Cache().GetCache<List<AuthorizeUrlModel>>("AuthorizeUrl_" + userId);
            if (cacheList == null)
            {
                authorizeUrlList = this.GetUrlList(userId).ToList();
                CacheFactory.Cache().WriteCache(authorizeUrlList, "AuthorizeUrl_" + userId, DateTime.Now.AddMinutes(1));
            }
            else
            {
                authorizeUrlList = cacheList;
            }
            authorizeUrlList = authorizeUrlList.FindAll(t => t.ModuleId.Equals(moduleId));
            foreach (AuthorizeUrlModel item in authorizeUrlList)
            {
                if (!string.IsNullOrEmpty(item.UrlAddress))
                {
                    string[] url = item.UrlAddress.Split('?');
                    if (item.ModuleId == moduleId && url[0] == action)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthorUserId(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthorUserId(operators, isWrite);
        }
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        public string GetDataAuthor(Operator operators, bool isWrite = false)
        {
            return service.GetDataAuthor(operators, isWrite);
        }
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetModuleNames()
        {
            DataTable dt = new DataTable();
            try
            {
                dt = service.GetModuleNames();
            }
            catch (Exception ex)
            {

            }
            return dt;
        }

        ///// <summary>
        ///// 根据用户主键ID获取用户授权功能集合
        ///// </summary>
        ///// <param name="userCode"></param>
        ///// <returns></returns>
        //public List<ModuleResponseResult> GetAppModules(string userCode)
        //{
        //    UserService userService = new UserService();
        //    var entity = userService.GetEntityByAccount(userCode);
        //    if (entity == null)
        //        return null;
        //    var list = service.GetIsMenuModuleList(entity.UserId, "").ToList();
        //    if (list == null || list.Count <= 0)
        //        return null;
        //    var data = CreateModuleTree(list, "0", "");
        //    return data;
        //}

        ///// <summary>
        ///// 生成功能模块树
        ///// </summary>
        ///// <param name="moduleList"></param>
        ///// <param name="parentKey"></param>
        ///// <param name="parentName"></param>
        ///// <returns></returns>
        //private List<ModuleResponseResult> CreateModuleTree(List<ModuleEntity> moduleList, string parentKey, string parentName)
        //{
        //    List<ModuleResponseResult> dataList = new List<ModuleResponseResult>();
        //    foreach (var item in moduleList)
        //    {
        //        if (item.ParentId == parentKey)
        //        {
        //            dataList.Add(new ModuleResponseResult
        //            {
        //                Name = item.FullName,
        //                Url = item.UrlAddress,
        //                ClassName = parentName,
        //                MenuList = CreateModuleTree(moduleList, item.ModuleId, item.FullName)
        //            });
        //        }
        //    }
        //    return dataList;
        //}
    }
}
