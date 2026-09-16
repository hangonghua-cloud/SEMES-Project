using ALP.Application.Entity.AppManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace ALP.Application.IService.AppManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-05-24 16:22
    /// 描 述：线体
    /// </summary>
    public interface AppVersionIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<AppVersionEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        AppVersionEntity GetEntity(string keyValue);


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        List<AppVersionEntity> GetList();
        #endregion

        #region 验证数据

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
        void SaveForm(string keyValue, AppVersionEntity entity);

        /// <summary>
        /// 版本回滚功能
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        bool ModifyEntity(AppVersionEntity entity,string keyValue);

        bool InsertApp(AppVersionEntity app);
        #endregion
    }
}
