using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System.Collections.Generic;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-11-03 11:53
    /// 描 述：微应用人员管理
    /// </summary>
    public interface WebChatApplyUserIService
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
        IEnumerable<WebChatApplyUserEntity> GetList(string queryJson);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<WebChatApplyUserEntity> GetList();
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        WebChatApplyUserEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 员工编号不能重复
        /// </summary>
        /// <param name="enCode">员工编号</param>
        /// <param name="applyType">预警类型主键</param>
        /// <param name="lineCode">所属产线</param>
        /// <param name="keyValue">是否新增、编辑主键ID</param>
        /// <returns></returns>
        string ExistEnCode(string enCode, string applyType, string lineCode, string keyValue);
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
        void SaveForm(string keyValue, WebChatApplyUserEntity entity);
        #endregion
    }
}
