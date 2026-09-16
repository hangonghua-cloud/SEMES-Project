using ALP.Application.IService.BaseManage;
using ALP.Data;
using ALP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
    public class CommonService
    {
        /// <summary>
        /// 获取业务表中最新行号
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static int GetNewItemNo(string tableName, string fieldName, string orderNo)
        {
            if (string.IsNullOrEmpty(tableName))
                return 0;
            RepositoryFactory repositoryFactory = new RepositoryFactory();
            try
            {
                string sql = $@"SELECT TOP 1 itemNo+1 AS ItemNo FROM {tableName} WHERE {fieldName}='{orderNo}' ORDER BY itemNo DESC;";

                var entity = repositoryFactory.BaseRepository().FindList<ItemNoEntity>(sql).FirstOrDefault();
                if (entity != null)
                    return entity.ItemNo;
                return 1;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 事物批量执行SQL语句集合函数
        /// </summary>
        /// <param name="sqlList"></param>
        public bool ExecuteTransSQL(List<string> sqlList)
        {
            if (sqlList == null)
                return false;

            IDatabase _db = DbFactory.Base().BeginTrans();
            try
            {
                sqlList.ForEach(d =>
                {
                    _db.ExecuteBySql(d);
                });
                _db.Commit();
            }
            catch (Exception ex)
            {
                _db.Rollback();
                throw ex;
            }
            finally
            {
                _db.Close();
            }
            return true;
        }

        /// <summary>
        /// 执行SQL语句 带参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dbParameter"></param>
        /// <returns></returns>
        public bool ExecuteTransSQL(string sql, params DbParameter[] dbParameter)
        {
            if (string.IsNullOrEmpty(sql))
                return false;

            IDatabase _db = DbFactory.Base().BeginTrans();
            try
            {

                _db.ExecuteBySql(sql, dbParameter);
                _db.Commit();
            }
            catch (Exception ex)
            {
                _db.Rollback();
                throw ex;
            }
            finally
            {
                _db.Close();
            }
            return true;
        }

        /// <summary>
        /// 执行SQL语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public bool ExecuteSQL(string sql)
        {
            if (string.IsNullOrEmpty(sql))
                return false;

            IDatabase _db = DbFactory.Base();
            try
            {
                _db.ExecuteBySql(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _db.Close();
            }
            return true;
        }
         
        public IEnumerable<T> FindList<T>(string sql) where  T :class, new()
        {
            if (string.IsNullOrEmpty(sql))
                return null;

            IDatabase _db = DbFactory.Base();
            try
            {
                return _db.FindList<T>(sql);
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
            finally
            {
                _db.Close();
            }    
        }

      
    }

    public class ItemNoEntity
    {
        public int ItemNo { get; set; }
    }
}
