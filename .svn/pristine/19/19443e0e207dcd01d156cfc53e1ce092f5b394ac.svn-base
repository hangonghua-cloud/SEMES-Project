using ALP.Application.Code.Model;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Util;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;

namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典分类
    /// </summary>
    public class DataItemBLL
    {
        private IDataItemService service = new DataItemService();

        public string cacheKey = "DataItemCache";

        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetList()
        {
            return service.GetList();
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 根据分类编号获取实体对象
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <returns></returns>
        public DataItemEntity GetEntityByCode(string ItemCode)
        {
            return service.GetEntityByCode(ItemCode);
        }
        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetListByParent(string queryJson)
        {
            return service.GetListByParent(queryJson);
        }
        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetPageListByParent(Pagination pagination, string queryJson)
        {
            return service.GetPageListByParent(pagination, queryJson);
        }
        public List<VDataDictionaryModel> GetVDataDictionaryModelList()
        {
            return service.GetVDataDictionaryModelList();
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 分类编号不能重复
        /// </summary>
        /// <param name="itemCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistItemCode(string itemCode, string keyValue)
        {
            return service.ExistItemCode(itemCode, keyValue);
        }
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="itemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue)
        {
            return service.ExistItemName(itemName, keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
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
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, DataItemEntity dataItemEntity)
        {
            try
            {
                return service.SaveForm(keyValue, dataItemEntity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion


    }
}
