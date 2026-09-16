using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System.Data;
using System;
using System.Linq.Expressions;

namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public class Sys_DataSegregateGroupToPersonBLL
    {
        private ISys_DataSegregateGroupToPersonService service = new Sys_DataSegregateGroupToPersonService();


        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPageList(Pagination pagination, string groupCode)
        {
            return service.GetPageList(pagination, groupCode);
        }
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="pagination"
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonSelectList(Pagination pagination, string groupCode)
        {
            return service.GetPersonSelectList(pagination, groupCode);
        }
        /// <summary>
        /// 获取成员列表（未选择）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonAllList(Pagination pagination, string queryJson)
        {
            return service.GetPersonAllList(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="keyValues">主键</param>
        public int RemoveForm(string keyValues)
        {
            int result = 0;
            try
            {
                result = service.RemoveForm(keyValues);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="personCodes"></param>
        /// <returns></returns>
        public int SaveForm(string groupCode, string personCodes)
        {
            int result = 0;
            try
            {
                result = service.SaveForm(groupCode, personCodes);
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
