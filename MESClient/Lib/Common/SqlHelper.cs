using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;//Configuration表面配置，组态，构造
using System.Reflection;
using Dapper;

namespace Lib
{
    public class SqlHelper
    {
        static IDbConnection connection = null;
        public SqlHelper()
        {
            connection = new SqlConnection(GetSqlConnectionString());
        }

        public static string GetSqlConnectionString()
        {
            string connectString = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
            return connectString;
        }
        //适合增删改操作，返回影响条数
        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = sql;
                    comm.Parameters.AddRange(parameters);
                    return comm.ExecuteNonQuery();
                }
            }
        }
        //查询操作，返回查询结果中的第一行第一列的值
        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = new SqlConnection(GetSqlConnectionString()))
            {
                using (SqlCommand comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = sql;
                    comm.Parameters.AddRange(parameters);
                    return comm.ExecuteScalar();
                }
            }
        }
        //Adapter调整，查询操作，返回DataTable
        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, GetSqlConnectionString()))
            {
                DataTable dt = new DataTable();
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                adapter.Fill(dt);
                return dt;
            }
        }
        /// <summary>
        /// DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTableWithPage(string sql, SqlParameter[] parameters, string sidx)
        {
            sql = string.Format(@"SELECT * FROM (SELECT ROW_NUMBER() OVER(ORDER BY {1}) RowNum,*,COUNT(1) OVER() TotalCount FROM ( {0}  ) a ) b WHERE b.RowNum BETWEEN  ((@CurrentPage - 1) * @PageSize + 1)  AND  (@CurrentPage * @PageSize) ", sql, sidx);
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, GetSqlConnectionString()))
            {
                DataTable dt = new DataTable();
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                adapter.Fill(dt);
                return dt;
            }
        }

        public static SqlDataReader ExecuteReader(string sqlText, params SqlParameter[] parameters)
        {
            //SqlDataReader要求，它读取数据的时候有，它独占它的SqlConnection对象，而且SqlConnection必须是Open状态
            SqlConnection conn = new SqlConnection(GetSqlConnectionString());//不要释放连接，因为后面还需要连接打开状态
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandText = sqlText;
            cmd.Parameters.AddRange(parameters);
            //CommandBehavior.CloseConnection当SqlDataReader释放的时候，顺便把SqlConnection对象也释放掉
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static List<T> GetModelFromDB<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// 将DataRow转换成实体对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        private static T GetItem<T>(DataRow dr)
        {
            try
            {
                Type temp = typeof(T);
                T obj = Activator.CreateInstance<T>();
                foreach (DataColumn column in dr.Table.Columns)
                {
                    foreach (PropertyInfo pro in temp.GetProperties())
                    {
                        if (pro.Name.ToLower() == column.ColumnName.ToLower())
                        {
                            if (dr[column.ColumnName] == DBNull.Value)
                            {
                                pro.SetValue(obj, " ", null);
                                break;
                            }
                            else
                            {
                                pro.SetValue(obj, dr[column.ColumnName], null);
                                break;
                            }
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static List<T> GetList<T>(string sql, params SqlParameter[] parameters)
        {
            DataTable dt = ExecuteDataTable(sql, parameters);
            return GetModelFromDB<T>(dt);
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        public void UpdateBulk(string sql, object data)
        {
            var result = connection.Execute(sql, data);
        }
    }
}