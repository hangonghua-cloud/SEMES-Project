using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源
    /// </summary>
    public class BsModelWithResourceBLL
    {
        private IBsModelWithResourceService service = new BsModelWithResourceService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "WithResourceCache";
        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<BsModelWithResourceEntity> GetList(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 根据层级编号获取低级资源
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetListByLevel(string queryJson)
        {
            return service.GetListByLevel(queryJson);
        }
        /// <summary>
        /// 根据层级编号获取所属某相应级资源
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetResourceByLevelJson(string queryJson)
        {
            return service.GetResourceByLevelJson(queryJson);
        }
        
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetAllList(string keyValue)
        {
            return service.GetAllList(keyValue);
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelWithResourceEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public BsModelWithResourceEntity GetEntity(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        //bool ExistCode(string code);
        /// <summary>
        /// 层级名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        //bool ExistFullName(string name);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            service.RemoveForm(keyValue);
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelWithResourceEntity entity)
        {
            return service.SaveForm(keyValue, entity);
        }
        #endregion

        /// <summary>
        /// 根据工序找工厂
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public BsModelWithResourceEntity GetFactoryByProcess(string processCode)
        {
            return service.GetFactoryByProcess(processCode);
        }
    }
}
