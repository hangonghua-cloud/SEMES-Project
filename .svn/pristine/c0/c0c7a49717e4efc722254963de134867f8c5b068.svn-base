using System;
using System.Collections.Generic;
using System.Data;
using ALP.Application.Entity.SystemManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public interface ISys_DataSegregateGroupToLabelService
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        IEnumerable<Sys_DataSegregateGroupToLabel> GetPageList(Pagination pagination, string groupCode);
        DataTable Get_PageData(Pagination pagination, string queryJson);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        int RemoveForm(string keyValue);
        int Update_GroupLabelValue(string GroupCode);
        bool GetSaveSql_DataSegregateGroupToLabel(List<Sys_DataSegregateLabel> listEnity, string GroupCode, out string msg);
    }
}
