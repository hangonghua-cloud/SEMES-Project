using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data;
using ALP.Application.IService.AppManage;
using ALP.Application.Service.AppManage;
using ALP.Application.Entity.AppManage;
using ALP.Application.Code;
using System.Linq;

namespace ALP.Application.Busines.AppManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-05-24 16:22
    /// 描 述：线体
    /// </summary>
    public class AppVersionBLL
    {
        private AppVersionService service = new AppVersionService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<AppVersionEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AppVersionEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<AppVersionEntity> GetList()
        {
            return service.GetList();
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
        public void SaveForm(string keyValue, AppVersionEntity entity)
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
        /// 版本更新功能  直接增加版本 将该版本可用设为1  别的版本设为0
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Insert(AppVersionEntity entity)
        {
            var getentityByversion = GetList().Where(x => x.Version == entity.Version && x.AppType == entity.AppType).FirstOrDefault();
            if (getentityByversion != null)
            {
                return false;
            }
            else
            {
                return service.InsertApp(entity);
            }
        }

        /// <summary>
        /// 版本回滚功能
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool Modify(AppVersionEntity entity, string keyValue)
        {
            entity.Id = keyValue;
            return service.ModifyEntity(entity,keyValue);
        }
        #endregion
    }
}
