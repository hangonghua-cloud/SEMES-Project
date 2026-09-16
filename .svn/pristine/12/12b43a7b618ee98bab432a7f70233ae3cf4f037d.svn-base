using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Data;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 创 建：liyongguo
    /// 日 期：2020-11-11 09:48
    /// 描 述：报表配置页面
    /// </summary>
    public class Base_ReportRecordBLL
    {
        private Base_ReportRecordIService service = new Base_ReportRecordService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Base_ReportRecordEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<Base_ReportRecordEntity> GetList(Expression<Func<Base_ReportRecordEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Base_ReportRecordEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">condition</param>
        /// <returns></returns>
        public Base_ReportRecordEntity GetEntity(Expression<Func<Base_ReportRecordEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            return service.GetListWithPage(pagination, queryJson);
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
        public void SaveForm(string keyValue, Base_ReportRecordEntity entity)
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
        #endregion
    }
}
