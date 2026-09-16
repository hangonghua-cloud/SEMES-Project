using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-27 15:19
    /// 描 述：报表角色权限
    /// </summary>
    public interface Base_ReportRoleAuthorizeIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Base_ReportRoleAuthorizeEntity> GetList(string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Base_ReportRoleAuthorizeEntity> GetList(Expression<Func<Base_ReportRoleAuthorizeEntity, bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Base_ReportRoleAuthorizeEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Base_ReportRoleAuthorizeEntity entity);
        /// <summary>
        /// 创建表单 List
        /// </summary>
        /// <param name="list"></param>
        void InsertListEntity(List<Base_ReportRoleAuthorizeEntity> list);
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="condition">条件</param>
        void DeleteForm(Expression<Func<Base_ReportRoleAuthorizeEntity, bool>> condition);
        #endregion
    }
}
