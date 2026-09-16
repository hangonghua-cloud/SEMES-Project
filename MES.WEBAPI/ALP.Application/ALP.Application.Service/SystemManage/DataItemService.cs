using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using ALP.Util;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using ALP.Util;
using ALP.Application.Code.Model;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典分类
    /// </summary>
    public class DataItemService : RepositoryFactory<DataItemEntity>, IDataItemService
    {
        #region 获取数据
        /// <summary>
        /// 分类列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetList()
        {
            return this.BaseRepository().IQueryable().OrderBy(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 分类实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 根据分类编号获取实体对象
        /// </summary>
        /// <param name="ItemCode">编号</param>
        /// <returns></returns>
        public DataItemEntity GetEntityByCode(string ItemCode)
        {
            var expression = LinqExtensions.True<DataItemEntity>();
            if (!string.IsNullOrEmpty(ItemCode))
            {
                expression = expression.And(t => t.ItemCode == ItemCode);
            }
            return this.BaseRepository().FindEntity(expression);
        }
        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetListByParent(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string itemName = queryParam[""] == null ? "" : queryParam[""].ToString();
            string itemValue = queryParam[""] == null ? "" : queryParam[""].ToString();
            sql.Append(@"select B.ItemName,B.ItemValue from Base_DataItem as A
                        left join Base_DataItemDetail as B on A.ItemId=B.ParentId and B.EnabledMark=1
                        where A.EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                sql.Append(" and A.ItemCode='" + itemName + "' ");
            }
            if (!string.IsNullOrWhiteSpace(itemValue))
            {
                sql.Append(" and CHARINDEX('" + itemName + "', B.ItemValue)>0 ");
            }
            return this.BaseRepository().FindList(sql.ToString());
        }
        /// <summary>
        /// 根据条件查询数据字典
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<DataItemEntity> GetPageListByParent(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string parentCode = queryParam["ParentCode"] == null ? "" : queryParam["ParentCode"].ToString();
            string itemName = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            string itemValue = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            sql.Append(@"select B.ItemName,B.ItemValue from Base_DataItem as A
                        left join Base_DataItemDetail as B on A.ItemId=B.ParentId and B.EnabledMark=1
                        where A.EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                sql.Append(" and A.ItemCode='" + parentCode + "' ");
            }
            if (!string.IsNullOrWhiteSpace(itemValue))
            {
                sql.Append(" and CHARINDEX('" + itemValue + "', B.ItemValue)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(itemName))
            {
                sql.Append(" and CHARINDEX('" + itemName + "', B.ItemName)>0 ");
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
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
            var expression = LinqExtensions.True<DataItemEntity>();
            expression = expression.And(t => t.ItemCode == itemCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 分类名称不能重复
        /// </summary>
        /// <param name="itemName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistItemName(string itemName, string keyValue)
        {
            var expression = LinqExtensions.True<DataItemEntity>();
            expression = expression.And(t => t.ItemName == itemName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存分类表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="dataItemEntity">分类实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, DataItemEntity dataItemEntity)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataItemEntity.Modify(keyValue);
                this.BaseRepository().Update(dataItemEntity);
                result = 2;
            }
            else
            {
                DataItemEntity _entity = this.BaseRepository().FindEntity(t => t.ItemCode == dataItemEntity.ItemCode && t.EnabledMark == 1);
                if (_entity == null)
                {
                    dataItemEntity.Create();
                    this.BaseRepository().Insert(dataItemEntity);
                    result = 1;
                }
                else
                {
                    result = 3;
                }
            }
            return result;
        }
        #endregion

        /// <summary>
        /// 查询数据字典
        /// </summary>
        /// <returns></returns>
        public List<VDataDictionaryModel> GetVDataDictionaryModelList()
        {
            var sql = @"SELECT EnCode,EnName,ItemValue,ItemName,SortCode FROM dbo.V_DataDictionary ";
            return new RepositoryFactory<VDataDictionaryModel>().BaseRepository().FindList(sql).ToList();
        }
 
    }
}
