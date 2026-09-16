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
    public class Sys_DataSegregateGroupBLL
    {
        private ISys_DataSegregateGroupService service = new Sys_DataSegregateGroupService();

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroup> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public int RemoveForm(string keyValue)
        {
            int result = 0;
            try
            {
                result = service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 保存数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, Sys_DataSegregateGroup entity)
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
        #endregion
    }
}
