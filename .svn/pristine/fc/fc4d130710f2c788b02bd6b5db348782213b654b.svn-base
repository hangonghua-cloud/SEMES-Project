using ALP.Application.Code;
using ALP.Application.Entity.AuthorizeManage;
using ALP.Application.Entity.AuthorizeManage.ViewModel;
using ALP.Application.Entity.BaseManage;
using System.Collections.Generic;
using System.Data;

namespace ALP.Application.IService.AuthorizeManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.12.5 22:35
    /// 描 述：授权认证
    /// </summary>
    public interface IAuthorizeService
    {
        /// <summary>
        /// 获取授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetModuleList(string userId,string fullName);

        /// <summary>
        /// 获取菜单授权功能
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetIsMenuModuleList(string userId, string fullName);
        /// <summary>
        /// 根据用户名获取授权
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<ModuleEntity> GetModuleList(string userId);
        /// <summary>
        /// 获取授权功能按钮
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleButtonEntity> GetModuleButtonList(string userId);
        /// <summary>
        /// 获取授权功能视图
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<ModuleColumnEntity> GetModuleColumnList(string userId);
        /// <summary>
        /// 获取授权功能Url、操作Url
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<AuthorizeUrlModel> GetUrlList(string userId);
        /// <summary>
        /// 获取关联用户关系
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        IEnumerable<UserRelationEntity> GetUserRelationList(string userId);
        /// <summary>
        /// 获得权限范围用户ID
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthorUserId(Operator operators, bool isWrite = false);
        /// <summary>
        /// 获得可读数据权限范围SQL
        /// </summary>
        /// <param name="operators">当前登陆用户信息</param>
        /// <param name="isWrite">可写入</param>
        /// <returns></returns>
        string GetDataAuthor(Operator operators, bool isWrite = false);
        /// <summary>
        /// 获取模块信息
        /// </summary>
        /// <returns></returns>
        DataTable GetModuleNames();
    }
}
