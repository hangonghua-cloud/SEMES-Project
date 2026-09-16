using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级扩展  
    /// </summary>
    public class BsModelLevelExtendFieldsBLL
    {
        private IBsModelLevelExtendFieldsService service = new BsModelLevelExtendFieldsService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "LevelCache";
        #region 获取数据
        /// <summary>
        /// 层级扩展字段列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 层级扩展字段列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 层级扩展字段列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetAllList()
        {
            return service.GetAllList();
        }
        /// <summary>
        /// 层级扩展字段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelLevelExtendFieldsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级扩展字段编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        //bool ExistCode(string code);
        /// <summary>
        /// 层级扩展字段名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        //bool ExistFullName(string name);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级扩展字段
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            service.RemoveForm(keyValue);
        }
        /// <summary>
        /// 保存层级扩展字段表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级扩展字段实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelLevelExtendFieldsEntity entity)
        { 
            return service.SaveForm(keyValue, entity);
        }
        #endregion
    }
}
