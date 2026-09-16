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
    public class Sys_DataSegregateGroupToLabelBLL
    {
        private ISys_DataSegregateGroupToLabelService service = new Sys_DataSegregateGroupToLabelService();


        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToLabel> GetPageList(Pagination pagination, string groupCode)
        {
            return service.GetPageList(pagination, groupCode);
        }
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            return service.Get_PageData(pagination, queryJson);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量保存表单SQL
        /// </summary>
        /// <param name="listEnity"></param>
        /// <param name="GroupCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool GetSaveSql_DataSegregateGroupToLabel(List<Sys_DataSegregateLabel> listEnity, string GroupCode, out string msg)
        {
            var result = service.GetSaveSql_DataSegregateGroupToLabel(listEnity, GroupCode, out msg);
            return result;
        }
        /// <summary>
        /// 更新组标签值
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public int Update_GroupLabelValue(string GroupCode)
        {
            var result = service.Update_GroupLabelValue(GroupCode);
            return result;
        }
        /// <summary>
        /// 删除数据
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
        #endregion
    }
}
