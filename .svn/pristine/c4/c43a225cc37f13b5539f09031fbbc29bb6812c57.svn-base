using System.Configuration;
using ALP.Util.Ioc;
using System;
using Microsoft.Practices.Unity;

namespace ALP.Data.Repository
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.10.10
    /// 描 述：数据库建立工厂
    /// </summary>
    public class DbFactory
    {
        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="connString">连接字符串</param>
        /// <param name="DbType">数据库类型</param>
        /// <returns></returns>
        public static IDatabase Base(string connString, DatabaseType DbType)
        {
            DbHelper.DbType = DbType;
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride(
              "connString", connString), new ParameterOverride(
              "DbType", DbType.ToString()));
        }
        /// <summary>
        /// 连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase Base()
        {
            try
            {
                DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType),
                    UnityIocHelper.GetmapToByName("DBcontainer", "IDbContext"));
                return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride("connString", "BaseDb"), new ParameterOverride("DbType", ""));
            }
            catch (Exception ex) {
                throw ex;
            }
        }


        public static IDatabase ReportBase()
        {
            DbHelper.DbType = (DatabaseType)Enum.Parse (typeof (DatabaseType), UnityIocHelper.GetmapToByName ("DBcontainer", "IDbContext"));
            return UnityIocHelper.DBInstance.GetService<IDatabase> (new ParameterOverride (
             "connString", "ReportDb"), new ParameterOverride (
              "DbType", ""));
        }
        /// <summary>
        /// UA连接基础库
        /// </summary>
        /// <returns></returns>
        public static IDatabase UABase()
        {
            DbHelper.DbType = (DatabaseType)Enum.Parse(typeof(DatabaseType), UnityIocHelper.GetmapToByName("DBcontainer", "IDbContext"));
            return UnityIocHelper.DBInstance.GetService<IDatabase>(new ParameterOverride(
             "connString", "BaseDb"), new ParameterOverride(
              "DbType", ""));
        }
    }
}
