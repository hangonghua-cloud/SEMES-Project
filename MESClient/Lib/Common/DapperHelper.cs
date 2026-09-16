using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Dapper;
using Lib.Common;

namespace Lib
{
    public class DapperHelper<T>
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        private static string Type = Lib.Common.Language.GetLocation();

        public static string connectionString
        {
            get
            {
                var conns = ConfigurationManager.ConnectionStrings["BaseDb" + "_" + (string.IsNullOrEmpty(Type) ? "CN" : Type)]?.ToString();
                return conns;
            }
        }
        //private static readonly string connectionString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="sql">查询的sql</param>
        /// <param name="param">替换参数</param>
        /// <returns></returns>
        public static List<T> Query(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Query<T>(sql, param).ToList();
            }
        }
        public static List<T> QueryPage(string sql, object param)
        {
            sql = string.Format(@"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY @Sidx) RowNum,*,COUNT(1) OVER() TotalCount FROM ( {0}  ) a ) b WHERE b.RowNum BETWEEN  ((@CurrentPage - 1) * @PageSize + 1)  AND  (@CurrentPage * @PageSize) ", sql);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var result = con.Query<T>(sql, param).ToList();
                return result;
            }
        }

        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTableWithPage(string sql, object param)
        {
            var sidx = PubFunction.GetPropertyValue(param, "Sidx");
            sql = string.Format(@"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {1}) RowNum,*,COUNT(1) OVER() TotalCount FROM ( {0}  ) a ) b WHERE b.RowNum BETWEEN  ((@CurrentPage - 1) * @PageSize + 1)  AND  (@CurrentPage * @PageSize) ", sql, sidx);
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var reader = con.ExecuteReader(sql, param);
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            }
        }
        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                var reader = con.ExecuteReader(sql, param);
                DataTable table = new DataTable();
                table.Load(reader);
                return table;
            }
        }

        /// <summary>
        /// 查询第一个数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirst(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QueryFirst<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询第一个数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QueryFirstOrDefault(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QueryFirstOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询单条数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingle(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QuerySingle<T>(sql, param);
            }
        }

        /// <summary>
        /// 查询单条数据没有返回默认值
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T QuerySingleOrDefault(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.QuerySingleOrDefault<T>(sql, param);
            }
        }

        /// <summary>
        /// 增删改
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static int Execute(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.Execute(sql, param);
            }
        }

        /// <summary>
        /// Reader获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static IDataReader ExecuteReader(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteReader(sql, param);
            }
        }

        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteScalar(sql, param);
            }
        }

        /// <summary>
        /// Scalar获取数据
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static T ExecuteScalarForT(string sql, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                return con.ExecuteScalar<T>(sql, param);
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static List<T> ExecutePro(string proc, object param)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                List<T> list = con.Query<T>(proc,
                    param,
                    null,
                    true,
                    null,
                    CommandType.StoredProcedure).ToList();
                return list;
            }
        }


        /// <summary>
        /// 事务1 - 全SQL
        /// </summary>
        /// <param name="sqlarr">多条SQL</param>
        /// <param name="param">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(string[] sqlarr)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var sql in sqlarr)
                        {
                            result += con.Execute(sql, null, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 事务2 - 声明参数
        ///demo:
        ///dic.Add("Insert into Users values (@UserName, @Email, @Address)",
        ///        new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });
        /// </summary>
        /// <param name="Key">多条SQL</param>
        /// <param name="Value">param</param>
        /// <returns></returns>
        public static int ExecuteTransaction(Dictionary<string, object> dic)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed) con.Open();
                using (var transaction = con.BeginTransaction())
                {
                    try
                    {
                        int result = 0;
                        foreach (var sql in dic)
                        {
                            result += con.Execute(sql.Key, sql.Value, transaction);
                        }

                        transaction.Commit();
                        return result;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
        }
    }
}