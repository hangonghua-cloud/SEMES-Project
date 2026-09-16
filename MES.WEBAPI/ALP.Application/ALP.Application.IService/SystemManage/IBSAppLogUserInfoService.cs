using System;
using System.Collections.Generic;

using ALP.Application.Entity.SystemManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.3.13 16:19
    /// 描 述：用户信息与登录
    /// </summary>
    public interface IBSAppLogUserInfoService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<BSAppLogUserInfoEntity> GetPageList(Pagination pagination, string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int RemoveForm(string keyValue, string userId);
        /// <summary>
        /// 保存数据(新增/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SaveForm(string keyValue, BSAppLogUserInfoEntity entity);


        int SaveFunctions(string Role, List<APPRoleToFunctionEntity> SaveRows);
        #endregion
    }
}
