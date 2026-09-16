using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-27 15:19
    /// 描 述：报表角色权限
    /// </summary>
    public class Base_ReportRoleAuthorizeBLL
    {
        private Base_ReportRoleAuthorizeIService service = new Base_ReportRoleAuthorizeService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ReportRoleAuthorizeEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ReportRoleAuthorizeEntity> GetList(Expression<Func<Base_ReportRoleAuthorizeEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ReportRoleAuthorizeEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Base_ReportRoleAuthorizeEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 创建表单 List
        /// </summary>
        /// <param name="list"></param>
        public void InsertListEntity(List<Base_ReportRoleAuthorizeEntity> list)
        {
            service.InsertListEntity(list);
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="condition">条件</param>
        public void DeleteForm(Expression<Func<Base_ReportRoleAuthorizeEntity, bool>> condition)
        {
            service.DeleteForm(condition);
        }
        #endregion
    }
}
