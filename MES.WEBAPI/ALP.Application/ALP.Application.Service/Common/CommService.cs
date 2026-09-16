using ALP.Application.Entity.Calendar;
using ALP.Data;
using ALP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Common
{
    public class CommService
    {
        /// <summary>
        ///执行批量SQL--事务处理
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ExecuteBySql_Trans(List<string> listSql, out string msg)
        {
            msg = "";
            //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            IDatabase db = DbFactory.Base().BeginTrans();
            bool b = false;
            int index = 0;
            try
            {
                if (listSql != null)
                {
                    foreach (var itemSQL in listSql)
                    {
                        index = listSql.IndexOf(itemSQL);
                        db.ExecuteBySql(itemSQL);
                    }
                    db.Commit();
                    b = true;
                }
                else
                {
                    msg = "listSql 参数不能为空";
                }
            }
            catch (Exception ex)
            {
                b = false;
                db.Rollback();
                msg = ex.Message;
            }
            return b;
            //this.BaseRepository().InsertSqlList(paramalarmList, msg);
        }
        /// <summary>
        /// 执行批量SQL--事务处理
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ExecuteBySql_Trans(string strSql, out string msg)
        {
            msg = "";
            //IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            IDatabase db = DbFactory.Base().BeginTrans();
            bool b = false;
            try
            {
                if (!string.IsNullOrEmpty(strSql))
                {
                    db.ExecuteBySql(strSql);
                    db.Commit();
                    b = true;
                }
                else {
                    msg = "strSql 参数不能为空";
                }
            }
            catch (Exception ex)
            {
                db.Rollback();
                msg = ex.Message;
                b = false;
            }
            return b;
            //this.BaseRepository().InsertSqlList(paramalarmList, msg);
        }
    }
}
