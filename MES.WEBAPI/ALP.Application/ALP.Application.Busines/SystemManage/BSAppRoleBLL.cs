using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using System.Linq.Expressions;

namespace ALP.Application.Busines.SystemManage
{
    public class BSAppRoleBLL
    {
        private IBSAppRoleService service = new BSAppRoleService();
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<APPRoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetAuthorList(string roleCode)
        {
            return service.GetAuthorList(roleCode);
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<APPRoleFunctionEntity> GetRoleFunctionPageList(Pagination pagination, string queryJson)
        {
            return service.GetRoleFunctionPageList(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, string userId)
        {
            try
            {
                return service.RemoveForm(keyValue, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveRoleFunctionForm(string keyValue, string userId)
        {
            try
            {
                return service.RemoveRoleFunctionForm(keyValue, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, APPRoleEntity entity)
        {
            try
            {
                return service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveRoleFunctionForm(string keyValue, APPRoleFunctionEntity entity)
        {
            try
            {
                return service.SaveRoleFunctionForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveFunctions(string roleCode, string userId, List<APPRoleToFunctionEntity> list)
        {
            try
            {
                return service.SaveFunctions(roleCode, userId, list);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
