using ALP.Application.Entity.SystemManage;
using ALP.Application.Entity.SystemManage.ViewModel;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.17 9:56
    /// 描 述：数据字典明细
    /// </summary>
    public class DataItemDetailService : RepositoryFactory<DataItemDetailEntity>, IDataItemDetailService
    {
        #region 获取数据
        /// <summary>
        /// 明细列表
        /// </summary>
        /// <param name="itemId">分类Id</param>
        /// <returns></returns>
        public IEnumerable<DataItemDetailEntity> GetList(string itemId)
        {
            return this.BaseRepository().IQueryable(t => t.ItemId == itemId).OrderBy(t => t.SortCode).ToList();
        }
        /// <summary>
        /// 明细实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataItemDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  i.ItemId ,
                                    i.ItemCode AS EnCode ,
                                    d.ItemDetailId ,
                                    d.ParentId ,
                                    d.ItemCode ,
                                    d.ItemName ,
                                    d.ItemValue ,
                                    d.QuickQuery ,
                                    d.SimpleSpelling ,
                                    d.IsDefault ,
                                    d.SortCode ,
                                    d.EnabledMark
                            FROM    Base_DataItemDetail d
                                    inner JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   d.EnabledMark = 1
                                    AND d.DeleteMark = 0
                            ORDER BY d.SortCode ASC");
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString());
        }





        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList(string EnCode, string Name = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT  i.ItemId ,
                                    i.ItemCode AS EnCode ,
                                    d.ItemDetailId ,
                                    d.ParentId ,
                                    d.ItemCode ,
                                    d.ItemName ,
                                    d.ItemValue ,
                                    d.QuickQuery ,
                                    d.SimpleSpelling ,
                                    d.IsDefault ,
                                    d.SortCode ,
                                    d.EnabledMark,
                                    d.[Description],
									d.Remark1
                            FROM    Base_DataItemDetail d
                                    inner JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   1 = 1
                                    AND i.ItemCode = @EnCode
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0");
            if (!string.IsNullOrEmpty(Name))
                strSql.Append($@" AND (d.ItemValue LIKE '%{Name}%' OR d.ItemName LIKE '%{Name}%') ");

            strSql.Append(@" ORDER BY d.SortCode ASC ");
            DbParameter[] parameter =
            {
                 new SqlParameter("@EnCode",EnCode)
            };
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString(), parameter);
        }
        /// <summary>
        /// 获取数据字典列表（给绑定下拉框提供的）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DataItemModel> GetDataItemList_UA(string EnCode, string Name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT 
                                    d.ItemName ,
                                    d.ItemValue,Remark1,d.Description                                   
                            FROM    Base_DataItemDetail d
                                    inner JOIN Base_DataItem i ON i.ItemId = d.ItemId
                            WHERE   i.ItemCode = @EnCode
                                    AND d.EnabledMark = 1
                                    AND d.DeleteMark = 0");
            if (!string.IsNullOrEmpty(Name))
                strSql.Append($@" AND (d.ItemValue LIKE '%{Name}%' OR d.ItemName LIKE '%{Name}%') ");

            strSql.Append(@"ORDER BY d.SortCode ASC");
            DbParameter[] parameter =
            {
                 new SqlParameter("@EnCode",EnCode)
            };
            return new RepositoryFactory().BaseRepository().FindList<DataItemModel>(strSql.ToString(), parameter);
        }

        /// <summary>
        ///获取字典列表子明细
        /// </summary>
        /// <returns></returns>
        public DataTable GetList_DataItemByFather_PDA(string fatherCode, string remark1 = "")
        {
            string sql = $@"select B.ItemName,B.ItemValue
						from Base_DataItem as A
                        left join Base_DataItemDetail as B on A.ItemId=B.ParentId and B.EnabledMark=1
                        where A.EnabledMark=1 and A.ItemCode='{fatherCode}'";
            if (!string.IsNullOrEmpty(remark1))
                sql += $@" AND b.Remark1 ='{remark1}' ";

            sql += @"order by B.SortCode";
            return this.BaseRepository().FindTable(sql);
        }

        /// <summary>
        /// 根据字典名称获取CODE
        /// </summary>
        /// <returns></returns>
        public string GetDataItemCode(string EnCode, string itemName)
        {
            string result = "";
            IEnumerable<DataItemModel> lst = GetDataItemList(EnCode);
            foreach (var item in lst)
            {
                if (item.ItemValue.ToUpper() == itemName.ToUpper())
                {
                    result = item.ItemName;
                    break;
                }
            }

            return result;
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
            try
            {
                string sql = $"select * from {tableName} where 1=1 ";

                if (!string.IsNullOrEmpty(codeName) && !string.IsNullOrEmpty(codeValue))
                {
                    sql += $" and {codeName}='{codeValue}'";
                }

                return this.BaseRepository().FindTable(sql);
            }
            catch (Exception e)
            {
                throw;
            }

            return null;
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
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.ItemValue == itemValue).And(t => t.ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
            var expression = LinqExtensions.True<DataItemDetailEntity>();
            expression = expression.And(t => t.ItemName == itemName).And(t => t.ItemId == itemId);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.ItemDetailId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
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
            if (!string.IsNullOrEmpty(keyValue))
            {
                dataItemDetailEntity.Modify(keyValue);
                this.BaseRepository().Update(dataItemDetailEntity);
                result = 2;
            }
            else
            {
                DataItemDetailEntity entity = this.BaseRepository().FindEntity(t => t.ItemId == dataItemDetailEntity.ItemId && t.ItemValue == dataItemDetailEntity.ItemValue && t.EnabledMark == 1);
                if (entity == null)
                {
                    dataItemDetailEntity.Create();
                    this.BaseRepository().Insert(dataItemDetailEntity);
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
    }
}
