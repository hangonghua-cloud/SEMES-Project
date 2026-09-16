using ALP.Application.Code;
using ALP.Application.Entity.SystemManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service
{
    /// <summary>
    /// commomn function define in this place
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// analyzing the rules to expression
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="isAnd"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static Expression<Func<TSource, bool>> BuildCommonExpression<TSource>(bool isAnd, JToken rules)
        {
            if (rules != null)
            {
                try
                {
                    Expression body = null;
                    var p = Expression.Parameter(typeof(TSource), "p");
                    int i = 0;
                    foreach (var item in rules)
                    {
                        var filed = item.First.Last.ToString();
                        var op = item.First.Next.Last.ToString();
                        var val = item.Last.Last.ToString();
                        var propertyName = Expression.Property(p, filed);
                        if (string.IsNullOrEmpty(val)) continue;
                        //
                        var mapOp = getMapExpressionOp(op);
                        PropertyInfo property = typeof(TSource).GetProperty(filed);          //query filed
                        Expression left = Expression.Property(p, property);
                        Expression right = Expression.Constant(val);
                        //
                        if (i == 0)
                        {
                            body = GetCorrespondingExpression(body, left, right, mapOp, propertyName, val);
                        }
                        else
                        {
                            var orAndBody = GetCorrespondingExpression(body, left, right, mapOp, propertyName, val);
                            if (isAnd)
                            {
                                body = Expression.And(body, orAndBody);
                            }
                            else
                            {
                                body = Expression.Or(body, orAndBody);
                            }
                        }
                        i++;
                    }
                    Expression<Func<TSource, bool>> orExp = Expression.Lambda<Func<TSource, bool>>(body, p);
                    return orExp;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
        /// <summary>
        /// analyzing the rules to sql
        /// </summary>
        /// <param name="isAnd"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static string BuildCommonSql(bool isAnd, JToken rules)
        {
            StringBuilder sqlSb = new StringBuilder();
            string groupOp = (isAnd ? " AND " : " OR ");
            if (rules != null)
            {
                for (int i = 0; i < rules.Count(); i++)
                {
                    var item = rules[i];
                    var filed = item.First.Last.ToString();
                    var op = item.First.Next.Last.ToString();
                    var val = item.Last.Last.ToString();
                    if (string.IsNullOrEmpty(val)) continue;
                    var mapOp = getMapOperatorOp(op);
                    if (mapOp == "in")
                    {
                        mapOp = " like ";
                        val = "%" + val + "%";
                    }
                    if (getQueryOperationType(op) == QueryOperationEnum.NumberOp)
                    {
                        sqlSb.Append(string.Format(" {0}{1}{2} ", filed, mapOp, val));
                    }
                    else
                    {
                        sqlSb.Append(string.Format(" {0}{1}'{2}' ", filed, mapOp, val));
                    }
                    if (i != rules.Count() - 1) sqlSb.Append(groupOp);
                }
                return $"({sqlSb.ToString()})";
            }
            return "(1=1)";
        }

        /// <summary>
        /// analyzing the rules to sql
        /// </summary>
        /// <param name="isAnd"></param>
        /// <param name="rules"></param>
        /// <returns></returns>
        public static string BuildMultiSql(bool isAnd, JToken rules)
        {
            StringBuilder sqlSb = new StringBuilder();
            string groupOp = (isAnd ? " AND " : " OR ");
            if (rules != null)
            {
                for (int i = 0; i < rules.Count(); i++)
                {
                    var item = rules[i];
                    var filed = item.First.Last.ToString();
                    var op = item.First.Next.Last.ToString();
                    var val = item.Last.Last.ToString();
                    if (string.IsNullOrEmpty(val)) continue;
                    var mapOp = getMapOperatorOp(op);
                    if (mapOp == "in")
                    {
                        mapOp = " like ";
                        val = "%" + val + "%";
                    }
                    if (getQueryOperationType(op) == QueryOperationEnum.NumberOp)
                    {
                        sqlSb.Append(string.Format(" {0}{1}{2} ", $"c.{filed}", mapOp, val));
                    }
                    else
                    {
                        sqlSb.Append(string.Format(" {0}{1}'{2}' ", $"c.{filed}", mapOp, val));
                    }
                    if (i != rules.Count() - 1) sqlSb.Append(groupOp);
                }
                return $"({sqlSb.ToString()})";
            }
            return "(1=1)";
        }

        private static Expression GetCorrespondingExpression(Expression body, Expression left, Expression right, string mapOp, MemberExpression propertyName, string val)
        {
            int? intVal = 0;
            DateTime? dateVal = DateTime.Now;
            switch (mapOp)
            {
                case "StringEquals":
                    MethodInfo stringEqualsMethod = typeof(string).GetMethod("Equals", new[] { typeof(string) });   //for  string equals
                    body = Expression.Equal(Expression.Call(propertyName, stringEqualsMethod, Expression.Constant(val)), Expression.Constant(true));
                    break;
                case "Equals":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.Equal(left, right);
                    break;
                case "DateEquals":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime?));
                    body = Expression.Equal(left, right);
                    break;
                case "DateGreaterThan":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime));
                    body = Expression.GreaterThan(left, right);
                    break;
                case "DateLessThan":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime));
                    body = Expression.LessThan(left, right);
                    break;
                case "DateGreaterThanOrEqual":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime?));
                    body = Expression.GreaterThanOrEqual(left, right);
                    break;
                case "DateDateLessThanOrEqual":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime?));
                    body = Expression.LessThanOrEqual(left, right);
                    break;
                case "DateNotEquals":
                    dateVal = DateTime.Parse(val);
                    right = Expression.Constant(dateVal, typeof(DateTime?));
                    body = Expression.NotEqual(left, right);
                    break;
                case "NotEquals":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.NotEqual(left, right);
                    break;
                case "StringNotEquals":
                    body = Expression.NotEqual(left, right);
                    break;
                case "Contains":
                    MethodInfo containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                    body = Expression.Call(propertyName, containsMethod, Expression.Constant(val));
                    break;


                case "GreaterThan":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.GreaterThan(left, right);
                    break;
                case "LessThan":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.LessThan(left, right);
                    break;
                case "GreaterThanOrEqual":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.GreaterThanOrEqual(left, right);
                    break;
                case "LessThanOrEqual":
                    intVal = Int32.Parse(val);
                    right = Expression.Constant(intVal, typeof(int?));
                    body = Expression.LessThanOrEqual(left, right);
                    break;
            }

            return body;
        }

        /// <summary>
        /// 根据条件数据动态生成或连接条件
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        private static string getMapExpressionOp(string op)
        {
            Dictionary<string, string> ops = new Dictionary<string, string>();
            //string
            ops.Add("seq", "StringEquals");
            ops.Add("in", "Contains");
            //datetime
            ops.Add("deq", "DateEquals");
            ops.Add("dgt", "DateGreaterThan");
            ops.Add("dlt", "DateLessThan");
            ops.Add("dge", "DateGreaterThanOrEqual");
            ops.Add("dle", "DateLessThanOrEqual");
            //int
            ops.Add("eq", "Equals");
            ops.Add("ni", "NotEquals");
            ops.Add("sni", "StringNotEquals");
            ops.Add("dni", "DateNotEquals");
            ops.Add("gt", "GreaterThan");
            ops.Add("lt", "LessThan");
            ops.Add("ge", "GreaterThanOrEqual");
            ops.Add("le", "LessThanOrEqual");
            return ops[op];
        }

        /// <summary>
        /// 根据条件数据动态生成或连接条件
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        private static string getMapOperatorOp(string op)
        {
            Dictionary<string, string> ops = new Dictionary<string, string>();
            //string
            ops.Add("seq", "=");
            ops.Add("in", "in");
            //datetime
            ops.Add("deq", "=");
            ops.Add("dgt", " >");
            ops.Add("dlt", "<");
            ops.Add("dge", ">=");
            ops.Add("dle", "<=");
            //int
            ops.Add("eq", "=");
            ops.Add("ni", "!=");
            ops.Add("sni", "<>");
            ops.Add("dni", "!=");
            ops.Add("gt", ">");
            ops.Add("lt", "<");
            ops.Add("ge", ">=");
            ops.Add("le", "<=");
            return ops[op];
        }

        private static QueryOperationEnum getQueryOperationType(string op)
        {
            QueryOperationEnum queryOperationEnum = QueryOperationEnum.NumberOp;
            switch (op)
            {
                case "seq":
                case "in":
                case "sni":
                    queryOperationEnum = QueryOperationEnum.StringOp;
                    break;
                case "deq":
                case "dgt":
                case "dlt":
                case "dge":
                case "dle":
                    queryOperationEnum = QueryOperationEnum.DateOp;
                    break;
            }
            return queryOperationEnum;
        }

        #region 动态sql生成(初始化、单项查询、高级查询)
        /// <summary>
        /// 自动解析前台查询字符串并拼接sql语句
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public static string autoProduceSql(string tableName, string queryJson)
        {
            //0.基础sql语句
            StringBuilder baseSql = new StringBuilder($"SELECT * FROM {tableName} WHERE 1=1 ");

            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //1 单项查询 queryJson:"{\"condition\":\"DisFrequency\",\"keyword\":\"1.00\",\"begin\":\"\",\"end\":\"\"}"
                if (queryParam.Property("condition") != null)
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();

                    if (!string.IsNullOrEmpty(keyword))
                    {
                        baseSql.Append($"AND {condition} LIKE '%{keyword}%' ");
                    }
                    else
                    {
                        string begin = queryParam["begin"].ToString();
                        string end = queryParam["end"].ToString();
                        if (!string.IsNullOrEmpty(begin))
                            baseSql.Append($"AND DATEDIFF(dd,CONVERT(varchar(100),{begin}, 23), {condition})>=0 ");
                        if (!string.IsNullOrEmpty(end))
                            baseSql.Append($"AND DATEDIFF(dd, {condition},CONVERT(varchar(100),{end}, 23))>=0");
                    }
                }
                else
                {
                    //2 高级查询 queryJson:"{\"groupOp\":\"AND\",\"rules\":[{\"field\":\"DisFrequency\",\"op\":\"seq\",\"data\":\"1.00\"},{\"field\":\"RequireInterval\",\"op\":\"in\",\"data\":\"333\"}]}"
                    var rules = queryParam.Last.Last;
                    if (rules.Count() != 0)
                    {
                        baseSql.Append(" AND ( ");
                        var groupOP = queryParam.First.Last.ToString();

                        for (int i = 0; i < rules.Count(); i++)
                        {
                            var item = rules[i];
                            var filed = item.First.Last.ToString();
                            var op = item.First.Next.Last.ToString();
                            var val = item.Last.Last.ToString();

                            if (string.IsNullOrEmpty(val)) continue;
                            var mapOp = getMapOperatorOp(op);
                            if (mapOp == "in")
                            {
                                mapOp = " like ";
                                val = "%" + val + "%";
                            }

                            if (getQueryOperationType(op) == QueryOperationEnum.NumberOp)
                            {
                                baseSql.Append(string.Format(" {0}{1}{2} ", filed, mapOp, val));
                            }
                            else
                            {
                                baseSql.Append(string.Format(" {0}{1}'{2}' ", filed, mapOp, val));
                            }
                            if (i != rules.Count() - 1) baseSql.Append(groupOP);
                        }
                        baseSql.Append(" )");
                    }
                }
            }
            return baseSql.ToString();
        }
        #endregion
        /// <summary>
        /// 高级查询组合sql查询条件
        /// </summary>
        /// <param name="queryParam"></param>
        /// <returns></returns>
        public static string AutoGroupAdvanaceQuerySql(JObject queryParam)
        {
            var groupOP = queryParam.First.Last.ToString();
            var isAnd = (groupOP == "AND" ? true : false);
            var rules = queryParam.Last.Last;
            var Advancesql = BuildCommonSql(isAnd, rules);
            Advancesql.Replace("(", "");
            Advancesql.Replace(")", "");
            return Advancesql;
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
        /// 执行SQL语句
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
    }
}