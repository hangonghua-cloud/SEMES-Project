using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System.Collections.Generic;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-04 13:32
    /// 描 述：微应用预警配置表
    /// </summary>
    public interface IWebChatApplyConfigIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        System.Data.DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<WebChatApplyConfigEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        WebChatApplyConfigEntity GetEntity(string keyValue);
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
        void SaveForm(string keyValue, WebChatApplyConfigEntity entity);
        #endregion
    }
}
