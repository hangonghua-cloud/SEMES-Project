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
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public interface ISys_DataSegregateGroupToPersonService
    {
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        IEnumerable<Sys_DataSegregateGroupToPerson> GetPageList(Pagination pagination, string groupCode);
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="pagination"
        /// <param name="groupCode"></param>
        /// <returns></returns>
        IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonSelectList(Pagination pagination, string groupCode);
        /// <summary>
        /// 获取成员列表（未选择）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonAllList(Pagination pagination, string queryJson);
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        int RemoveForm(string keyValues);
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="personCodes"></param>
        /// <returns></returns>
        int SaveForm(string groupCode, string personCodes);
    }
}
