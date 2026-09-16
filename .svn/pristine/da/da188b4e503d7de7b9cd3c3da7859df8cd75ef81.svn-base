using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Cache.Factory;
using ALP.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailBLL
    {
        private IDataItemDetailService service = new DataItemDetailService();
        /// <summary>
        /// 缓存key
        /// </summary>
        public string cacheKey = "dataItemCache";

        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetList(string itemId)
        {
            return service.GetList(itemId);
        }
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            return service.GetDataItemList();
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList(string EnCode, string Name = "")
        {
            return service.GetDataItemList(EnCode, Name);
        }
        /// <summary>
        /// 数据字典列表
        /// </summary>
        /// <param name="EnCode">分类代码</param>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList_UA(string EnCode, string Name = "")
        {
            return service.GetDataItemList_UA(EnCode, Name);
        }
        /// <summary>
        ///获取字典列表子明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_DataItemByFather_PDA(string fatherCode, string remark1 = "")
        {
            return service.GetList_DataItemByFather_PDA(fatherCode, remark1);
        }
        /// <summary>
        /// 获取数据库中表中的信息
        /// </summary>
        /// <param name="tableName">表名</param> 
        /// <param name="codeName">字段名称</param> 
        /// <param name="codeValue">字段值</param>
        /// <returns>返回列表Json</returns>

        public DataTable GetDataTableList(string tableName, string codeName, string codeValue)
        {
            return service.GetDataTableList(tableName, codeName, codeValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="itemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemValue(string itemValue, string keyValue, string itemId)
        {
            return service.ExistItemValue(itemValue, keyValue, itemId);
        }
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue, string itemId)
        {
            return service.ExistItemName(itemName, keyValue, itemId);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity)
        {
            int result = 0;
            try
            {
                //dataItemDetailEntity.SimpleSpelling = Str.PinYin(dataItemDetailEntity.ItemName);
                result = service.SaveForm(keyValue, dataItemDetailEntity);
                CacheFactory.Cache().RemoveCache(cacheKey);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        #endregion
    }
}
