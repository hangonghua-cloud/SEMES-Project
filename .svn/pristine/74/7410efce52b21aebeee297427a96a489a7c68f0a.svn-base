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
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.3.13 16:19
    /// 描 述：用户信息与登录
    /// </summary>
    public class BSAppLogUserInfoBLL
    {
        private IBSAppLogUserInfoService service = new BSAppLogUserInfoService();

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BSAppLogUserInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, string userId)
        {
            int result = 0;
            try
            {
                result = service.RemoveForm(keyValue, userId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 保存数据(新增/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BSAppLogUserInfoEntity entity)
        {
            int result = 0;
            try
            {
                result = service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        /// <summary>
        /// 保存功能
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveFunctions(string Role, List<APPRoleToFunctionEntity> SaveRows)
        {
            int result = 0;
            try
            {
                result = service.SaveFunctions(Role, SaveRows);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        
        #endregion
    }
}
