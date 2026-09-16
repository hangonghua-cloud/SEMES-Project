using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
namespace ALP.Application.Busines.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创 建：超级管理员
    /// 日 期：2017-04-28 16:58
    /// 描 述：数据源表
    /// </summary>
    public class DataSourceBLL
    {
        private DataSourceIService service = new DataSourceService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DataSourceEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataSourceEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public DataTable GetTableDataList(string dataBaseLinkId, string tableName)
        {
            return service.GetTableDataList(dataBaseLinkId,  tableName);
        }

        /// <summary>
        /// 获取数据库中相应表的DataTable
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public DataTable GetTableData(string tableName)
        {
            return service.GetTableData(tableName);
        }

        /// <summary>
        /// 根据特定字段查询数据库中相应表的DataTable
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="feildName">数据库表中的字段名</param>
        /// <param name="value">数据库表中的字段值</param>
        /// <returns></returns>
        public DataTable GetTableDataByFeidld(string tableName, string feildName, string value)
        {
            return service.GetTableDataByFeidld(tableName,feildName,value);

        }

        /// <summary>
        /// 根据特定字段查询数据库中相应表的DataTable
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="feildName">数据库表中的字段名</param>
        /// <param name="value">数据库表中的字段值</param>
        /// <param name="feildName2">数据库表中的字段名</param>
        /// <param name="value2">数据库表中的字段值</param>
        /// <returns></returns>
        public DataTable GetTableDataByDoubleFeidld(string tableName, string feildName, string value, string feildName2, string value2)
        {
            return service.GetTableDataByDoubleFeidld(tableName, feildName, value, feildName2, value2);

        }
        #endregion

        #region jqgrid下拉框数据源
        /// <summary>
        /// jqgrid高级/单项查询下拉框数据源
        /// </summary>
        /// <param name="TableName">数据表名</param>
        /// <param name="EnCode">下拉框value</param>
        /// <param name="EnName">下拉框display</param>
        /// <returns>返回列表Json</returns>
        public DataTable GetDistinctTableData(string TableName, string EnCode,string EnName)
        {
            return service.GetDistinctTableData(TableName, EnCode,EnName);
        }
        #endregion

        #region 动态查询
        /// <summary>
        /// 动态获取Json字符串 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="EnCode">查询字段</param>
        /// <param name="Display">展示字段</param>
        /// <param name="keyword">查询字段值</param>
        /// <returns>返回对象Json</returns>
        public DataTable AutoGetTableDataList(string tableName, string EnCode,string Display, string keyword)
        {
            return service.AutoGetTableDataList(tableName, EnCode, Display, keyword);
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
        public string TestData(string dbtype, string connection,string sql)
        {
            return service.TestData(dbtype,  connection, sql);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataSourceEntity entity)
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
        #endregion

        #region UADM数据获取
        /// <summary>
        /// 获取Json字符串 
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="conditions">条件</param>
        /// <returns>返回对象Json</returns>
        public DataTable GetUATableData(string tableName, string conditions)
        {
            return service.GetUATableData(tableName,conditions);
        }
        #endregion
    }
}
