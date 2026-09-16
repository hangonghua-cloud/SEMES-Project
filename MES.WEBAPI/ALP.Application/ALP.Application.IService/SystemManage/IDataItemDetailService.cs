using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using System.Collections.Generic;
using System.Data;

namespace ALP.Application.IService.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public interface IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        IEnumerable<DataItemDetailEntity> GetList(string itemId);
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        DataItemDetailEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataItemModel> GetDataItemList();
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataItemModel> GetDataItemList(string EnCode, string Name = "");
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        IEnumerable<DataItemModel> GetDataItemList_UA(string EnCode, string Name = "");
        /// <summary>
        ///获取字典列表子明细
        /// </summary>
        /// <returns></returns>
        DataTable GetList_DataItemByFather_PDA(string fatherCode, string remark1 = "");
        /// <summary>
        /// 获取数据库中表中的信息
        /// </summary>
        /// <param name="tableName">表名</param> 
        /// <param name="codeName">字段名称</param> 
        /// <param name="codeValue">字段值</param>
        /// <returns>返回列表Json</returns>
        DataTable GetDataTableList(string tableName, string codeName, string codeValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 项目值不能重复
        /// </summary>
        /// <param name="itemValue">项目值</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        bool ExistItemValue(string itemValue, string keyValue, string itemId);
        /// <summary>
        /// 项目名不能重复
        /// </summary>
        /// <param name="itemName">项目名</param>
        /// <param name="keyValue">主键</param>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        bool ExistItemName(string itemName, string keyValue, string itemId);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存明细表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemDetailEntity">明细实体</param>
        /// <returns></returns>
        int SaveForm(string keyValue, DataItemDetailEntity dataItemDetailEntity);
        #endregion
    }
}
