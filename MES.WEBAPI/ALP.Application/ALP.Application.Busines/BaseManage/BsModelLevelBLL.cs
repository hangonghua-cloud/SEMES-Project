using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ALP.Application.Busines.BaseManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级
    /// </summary>
    public class BsModelLevelBLL
    {
        private IBsModelLevelService service = new BsModelLevelService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "LevelCache";
        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetParentList(string queryJson)
        {
            return service.GetParentList(queryJson);
        }
        /// <summary>
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetParentListByCode(string queryJson)
        {
            return service.GetParentListByCode(queryJson);
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllList(string keyValue)
        {
            return service.GetAllList(keyValue);
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelLevelEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 判断数据表里是否含有层级等于传入参数的记录
        /// </summary>
        /// <param name="levelNum"></param>
        /// <returns></returns>
        public bool HasLevelNum(int levelNum)
        {
            return service.HasLevelNum(levelNum);
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
        public int SaveForm(string keyValue, BsModelLevelEntity entity)
        {
            return service.SaveForm(keyValue, entity);
        }
        #endregion
    }
}
