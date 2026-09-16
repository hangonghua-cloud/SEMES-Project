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
    /// 描 述：工厂模型资源扩展
    /// </summary>
    public class BsModelResourceExtendInfoBLL
    {
        private IBsModelResourceExtendInfoService service = new BsModelResourceExtendInfoService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "ResourceExtendInfoCache";
        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelResourceExtendInfoEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<BsModelResourceExtendInfoEntity> GetList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelResourceExtendInfoEntity> GetAllList(string keyValue)
        {
            return service.GetAllList(keyValue);
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelResourceExtendInfoEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public BsModelResourceExtendInfoEntity Get_ExpressEntity(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            return service.Get_ExpressEntity(condition);
        }
        #endregion
        public IEnumerable<BsModelResourceExtendInfoEntity> Get_ExpressionList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
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
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="resource"></param>
        public void RemoveForm(string code, string resource)
        {
            service.RemoveForm(code, resource);
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelResourceExtendInfoEntity entity)
        { 
            return service.SaveForm(keyValue, entity);
        }
        #endregion
    }
}
