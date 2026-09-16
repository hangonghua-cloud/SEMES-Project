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
    /// 日 期：2020-11-11 09:48
    /// 描 述：报表配置页面
    /// </summary>
    public interface Base_ReportRecordIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Base_ReportRecordEntity> GetList(string queryJson);
        IEnumerable<Base_ReportRecordEntity> GetList(Expression<Func<Base_ReportRecordEntity, bool>> condition);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Base_ReportRecordEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns></returns>
        Base_ReportRecordEntity GetEntity(Expression<Func<Base_ReportRecordEntity, bool>> condition);
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetListWithPage(Pagination pagination, string queryJson);
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
        void SaveForm(string keyValue, Base_ReportRecordEntity entity);
        #endregion
    }
}
