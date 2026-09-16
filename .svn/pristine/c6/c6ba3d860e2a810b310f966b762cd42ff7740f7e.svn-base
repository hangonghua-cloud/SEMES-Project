using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ALP.QuartzTask.Util
{
    public class SqlHelper
    {
        //连接字符串
        public static readonly string connStr = ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;

        //返回的是受影响的行数
        public static int ExecuteNonQuery(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    con.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    if (ps != null)
                    {
                        cmd.Parameters.AddRange(ps);
                    }
                    con.Open();//打开数据库
                    return cmd.ExecuteScalar();
                }
            }
        }
        public static SqlDataReader ExecuteReader(string sql, params SqlParameter[] ps)
        {
            SqlConnection con = new SqlConnection(connStr);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                if (ps != null)
                {
                    cmd.Parameters.AddRange(ps);
                }
                try
                {
                    con.Open();
                    return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//关闭关联的connection
                }
                catch (Exception ex)
                {
                    con.Close();
                    con.Dispose();
                    throw ex;
                }

            }

        }
        public static DataSet SqlDataSet(string sql, DataSet ds, string tableName, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {

                using (SqlDataAdapter da = new SqlDataAdapter(sql, con))
                {
                    if (ps != null)
                    {
                        da.SelectCommand.Parameters.AddRange(ps);
                    }
                    con.Open();
                    ds.Clear();
                    
                    da.Fill(ds,tableName);

                    return ds;
                }
            }
        }
        //SqlHelper  
        public static int SqlDataAdapter(string sql, DataSet ds, string tableName, params SqlParameter[] ps)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlDataAdapter sa = new SqlDataAdapter(sql, con))
                {
                    SqlCommandBuilder builder = new SqlCommandBuilder(sa);

                    return sa.Update(ds, tableName);
                }
            }
        }

        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] ps)
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connStr))
            {
                if (ps != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(ps);
                }
                adapter.Fill(dt);
            }
            return dt;
        }

        #region 调用存储过程  (自定义超时时间)
        /// <summary>
        /// 调用存储过程  (自定义超时时间)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static DataSet ExecutePro(string storedProcedureName, IDbDataParameter[] parameters)
        {
            DataSet responseDs = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(storedProcedureName, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 2000;//超时时间
                    if (parameters != null && parameters.Length > 0)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter != null)
                            {
                                if (parameter.Direction == ParameterDirection.InputOutput
                                    || parameter.Direction == ParameterDirection.Input
                                    || parameter.Value == null)
                                {
                                    parameter.Value = DBNull.Value;
                                }
                            }
                            sqlCmd.Parameters.Add(parameter);
                        }
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    sda.Fill(responseDs);
                }
            }
            return responseDs;
        }
        #endregion

        #region 调用存储过程MES库
        /// <summary>
        /// 调用存储过程MES库  (自定义超时时间)
        /// </summary>
        /// <param name="storedProcedureName">存储过程名称</param>
        /// <param name="parameters">存储过程参数</param>
        /// <returns></returns>
        public static DataSet ExecuteMESPro(string storedProcedureName, IDbDataParameter[] parameters)
        {
            DataSet responseDs = new DataSet();
            using (SqlConnection sqlConn = new SqlConnection(connStr))
            {
                sqlConn.Open();
                using (SqlCommand sqlCmd = new SqlCommand(storedProcedureName, sqlConn))
                {
                    sqlCmd.CommandType = CommandType.StoredProcedure;
                    sqlCmd.CommandTimeout = 2000;//超时时间
                    if (parameters != null && parameters.Length > 0)
                    {
                        foreach (var parameter in parameters)
                        {
                            if (parameter != null)
                            {
                                sqlCmd.Parameters.Add(parameter);
                            } 
                        }
                    }
                    SqlDataAdapter sda = new SqlDataAdapter(sqlCmd);
                    sda.Fill(responseDs);
                }
            }
            return responseDs;
        }
        #endregion

        #region 更新上次执行时间
        /// <summary>
        /// 更新上次执行时间
        /// </summary>
        /// <param name="time"></param>
        /// <param name="taskCode"></param>
        public static void UpdateJobLastExeTime(DateTime time, string taskCode)
        {
            string sql =
                $"update Base_TimedTask set LastExeTime='{time.ToString("yyyy-MM-dd HH:mm:ss.fff")}' where TaskCode='{taskCode}'"; 
            var sql1 = $@" UPDATE dbo.Base_CollectionConfigDetails SET LastCollectTime='{time.ToString("yyyy-MM-dd HH:mm:ss.fff")}' WHERE TimedTask='{taskCode}'";
            ExecuteNonQuery(sql);
            ExecuteNonQuery(sql1);
        }
        #endregion

        #region 批量插入
        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tableName"></param>
        public static void InsetBulkCopy(DataTable dt,string tableName)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connStr,
                      SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
            {
                bulkCopy.DestinationTableName = tableName; //目标表，即要将数据插入到哪张表中去
                bulkCopy.ColumnMappings.Add("FQN", "TagName"); //数据源中的列名与目标表的属性的映射关系
                bulkCopy.ColumnMappings.Add("Value", "TagValue");
                bulkCopy.ColumnMappings.Add("DateTime", "CollectTime");
                bulkCopy.WriteToServer(dt); //将数据源数据写入到数据库中
            }
        }
        #endregion
         

    }
}
      