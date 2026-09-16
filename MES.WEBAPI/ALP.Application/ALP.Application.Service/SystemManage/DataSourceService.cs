using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System;
using System.Text;
using System.Configuration;
using ALP.Data;
using System.Diagnostics.Contracts;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创 建：超级管理员
    /// 日 期：2017-04-28 16:58
    /// 描 述：数据源表
    /// </summary>
    public class DataSourceService : RepositoryFactory<DataSourceEntity>, DataSourceIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<DataSourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<DataSourceEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体GetTableDataList
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DataSourceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取实体GetTableDataList
        /// </summary>
        /// <param name="dataBaseLinkId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetTableDataList(string dataBaseLinkId, string tableName)
        {
            DataBaseLinkService link = new DataBaseLinkService();
            DataBaseLinkEntity dataBaseLinkEntity = link.GetEntity(dataBaseLinkId);
            if (dataBaseLinkEntity != null)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("SELECT * FROM " + tableName + "");
                return this.BaseRepository(dataBaseLinkEntity.DbConnection).FindTable(strSql.ToString());
            }
            return null;
        }

        /// <summary>
        /// 获取数据库中相应表的DataTable
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <returns></returns>
        public DataTable GetTableData(string tableName)
        {
            string sql = $"SELECT* FROM {tableName} ";
            return this.BaseRepository().FindTable(sql);
        }

        /// <summary>
        /// 根据特定字段查询数据库中相应表的DataTable
        /// </summary>
        /// <param name="tableName">数据库表名</param>
        /// <param name="feildName">数据库表中的字段名</param>
        /// <param name="value">数据库表中的字段值</param>
        /// <returns></returns>
        public DataTable GetTableDataByFeidld(string tableName,string feildName,string value)
        {
            string sql = $"SELECT* FROM {tableName} where {feildName}='{value}'";
            return this.BaseRepository().FindTable(sql);
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
            string sql = $"SELECT* FROM {tableName} where {feildName}='{value}' and {feildName2}='{value2}'";
            return this.BaseRepository().FindTable(sql);
        }

        /// <summary>
        /// </summary>
        /// <param name="dbtype"></param>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns>返回对象Json</returns>
        public string TestData(string dbtype, string connection, string sql)
        {
            try
            {
                #region 测试连接
                SqlConnection dbConnection = null;
                string ServerAddress = "";
                switch (dbtype)
                {
                    case "SqlServer":
                        dbConnection = new SqlConnection(connection);
                        ServerAddress = dbConnection.DataSource;

                        break;
                    default:
                        break;
                }
                SqlDataAdapter da = null;
                DataTable dt = new DataTable();
                try
                {
                    dbConnection.Open();
                    //return this.BaseRepository(dataBaseLinkEntity.DbConnection).FindTable(strSql.ToString());
                    da = new SqlDataAdapter(sql, dbConnection);
                    da.Fill(dt);
                    dbConnection.Close();
                    return dt.ToJson();
                }
                catch
                {

                    return "[{\"失败\":\"请检查数据连接或sql语句\"}]";
                }



                #endregion

            }
            catch (Exception)
            {
                return "[{\"失败\":\"请检查数据连接或sql语句\"}]";
            }
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
        public DataTable GetDistinctTableData(string TableName, string EnCode, string EnName)
        {
            string sql;
            if (EnCode.Equals(EnName))
            {
                sql = $"SELECT DISTINCT {EnCode} FROM {TableName} ORDER BY {EnCode} ASC";
            }else{
                sql = $"SELECT DISTINCT {EnCode},{EnName} FROM {TableName} ORDER BY {EnCode} ASC";
            }
           
            return this.BaseRepository().FindTable(sql);
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
        public DataTable AutoGetTableDataList(string tableName, string EnCode, string Display, string keyword)
        {
            if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(EnCode) || string.IsNullOrEmpty(Display)){
                return null;
            }else if (!string.IsNullOrEmpty(keyword)){
                string strSql = $"SELECT DISTINCT TOP 10 {EnCode} value,{EnCode} text, {Display} form FROM { tableName}";
                if (!string.IsNullOrEmpty(keyword)) strSql += $" where { EnCode} like '%{ keyword}%'";
                return this.BaseRepository().FindTable(strSql.ToString());
            }
            return null;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DataSourceEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
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
        //public DataTable GetUATableData(string tableName,string conditions)
        //{
        //    string sql = $"select* from {tableName} where 1=1 ";

        //    if (!string.IsNullOrEmpty(conditions))
        //    {
        //        JArray jArray = JArray.Parse(conditions);
        //        foreach (var jjson in jArray){
        //            JObject jdata = (JObject)jjson;
        //            sql += $"and {jdata["key"].ToString()}='{jdata["value"].ToString()}'";
        //        }
        //    }

        //    return this.UABaseRepository().FindTable(sql);
        //}

        public DataTable GetUATableData(string tableName, string conditions)
        {
            string sql = $"select* from {tableName} where 1=1 ";

            if (!string.IsNullOrEmpty(conditions))
            {

                JArray jArray = JArray.Parse(conditions);
                foreach (var jjson in jArray)
                {
                    JObject jdata = (JObject)jjson;
                    sql += $"and {jdata["key"].ToString()}='{jdata["value"].ToString()}'";
                }
            }

            return this.UABaseRepository().FindTable(sql);
        }
        #endregion
    }
}
