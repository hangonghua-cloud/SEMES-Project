using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-27 15:19
    /// 描 述：报表权限角色
    /// </summary>
    public interface Base_ReportRoleIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Base_ReportRoleEntity> GetList(Expression<Func<Base_ReportRoleEntity, bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Base_ReportRoleEntity GetEntity(string keyValue);
        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetListWithPage(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        Base_ReportRoleEntity GetEntity(Expression<Func<Base_ReportRoleEntity, bool>> condition);

        DataTable GetReportRoleUserList(Pagination pagination, string queryJson);
        DataTable GetUserNotInGroup(Pagination pagination, string queryJson);
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
        void SaveForm(string keyValue, Base_ReportRoleEntity entity);
        void InsertList(List<Base_ReportRoleEntity> list);
        #endregion
    }
}
