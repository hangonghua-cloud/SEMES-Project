using System;
using System.Collections.Generic;
using System.Data;

using ALP.Application.Entity.SystemManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.SystemManage
{
    public interface IBSAppRoleService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<APPRoleEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<APPRoleFunctionEntity> GetRoleFunctionPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        DataTable GetAuthorList(string roleCode);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int RemoveForm(string keyValue, string userId);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int RemoveRoleFunctionForm(string keyValue, string userId);
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SaveForm(string keyValue, APPRoleEntity entity);
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        int SaveRoleFunctionForm(string keyValue, APPRoleFunctionEntity entity);
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        int SaveFunctions(string roleCode, string userId, List<APPRoleToFunctionEntity> list);
    }
}
