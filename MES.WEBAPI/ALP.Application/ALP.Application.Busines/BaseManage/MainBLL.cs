using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创 建：超级管理员
    /// 日 期：2019-05-10 15:01
    /// 描 述：Main
    /// </summary>
    public class MainBLL
    {
        private MainIService service = new MainService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MainEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MainEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<SubEntity> GetDetails(string keyValue)
        {
            return service.GetDetails(keyValue);
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
        public void SaveForm(string keyValue, MainEntity entity,List<SubEntity> entryList)
        {
            try
            {
                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
