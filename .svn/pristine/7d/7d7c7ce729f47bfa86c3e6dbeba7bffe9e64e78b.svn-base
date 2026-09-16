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
    public interface ISys_DataSegregateGroupService
    {
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<Sys_DataSegregateGroup> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        int RemoveForm(string keyValue);
        /// <summary>
        /// 保存数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        int SaveForm(string keyValue, Sys_DataSegregateGroup entity);
    }
}
