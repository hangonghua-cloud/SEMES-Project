using ALP.Application.Code;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseClipToolService : RepositoryFactory, IBaseClipToolService
    {
        /// <summary>
        /// 获取线体对应的虚拟槽列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        public IEnumerable<BaseClipToolEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<BaseClipToolEntity>();
                if (!string.IsNullOrEmpty(queryJson))
                {
                    var queryParam = queryJson.ToJObject();
                    //查询条件
                    if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                    {
                        string condition = queryParam["condition"].ToString();
                        string keyword = queryParam["keyword"].ToString();
                        switch (condition)
                        {
                            case "LineCode":              //线体
                                expression = expression.And(t => t.LineCode.ToString().Contains(keyword));
                                break;
                        }
                    }
                    else if (!queryParam["condition"].IsEmpty() && (!queryParam["LineCode"].IsEmpty()))
                    {
                        string condition = queryParam["condition"].ToString();
                        string LineCode = queryParam["LineCode"].ToString();
                        switch (condition)
                        {
                            case "LineCode":              //线体
                                expression = expression.And(t => t.LineCode.ToString().Contains(LineCode));
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        expression = GetQueryLinqExtensionsByJsonStr(queryParam);
                    }

                    if (expression == null)
                    {
                        expression = LinqExtensions.True<BaseClipToolEntity>();
                    }
                }
                else
                {
                    BaseUserLineService bs = new BaseUserLineService();
                    DataTable dt = bs.GetListTable(OperatorProvider.Provider.Current().UserId);
                    string defaultLineCode = string.Empty;
                    foreach (DataRow item in dt.Rows)
                    {
                        defaultLineCode += item["LineCode"].ToStr();
                    }
                    if (!string.IsNullOrEmpty(defaultLineCode))
                    {
                        expression = expression.And(t => defaultLineCode.Contains(t.LineCode.ToString()));
                    }
                }
                return this.BaseRepository().FindList(expression, pagination);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据线体获得虚拟槽
        /// </summary>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public IEnumerable<BaseClipToolEntity> GetListEntity(string lineCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM Base_ClipTool WHERE LineCode=@lineCode");
                List<DbParameter> parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
                return this.BaseRepository().FindList<BaseClipToolEntity>(strSql.ToStr(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获得虚拟槽对应的模架
        /// </summary>
        /// <param name="lineCode"></param>
        /// <param name="slotId"></param>
        /// <returns></returns>
        public DataTable GetClips(string lineCode, string slotId)
        {
            string sql = @"SELECT * FROM dbo.Base_ClipTool WHERE LineCode=@lineCode AND SlotId=@slotId";
            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
            parameter.Add(DbParameters.CreateDbParameter("@slotId", slotId));
            try
            {
                return this.BaseRepository().FindTable(sql.ToStr(), parameter.ToArray());
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 根据传入的Json查询条件 动态解析为Expression
        /// 
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<BaseClipToolEntity, bool>> GetQueryLinqExtensionsByJsonStr(JObject JsonStr)
        {
            var expression = LinqExtensions.True<BaseClipToolEntity>();
            if (JsonStr.Count == 0) return expression;
            var groupOP = JsonStr.First.Last.ToString();
            var isAnd = (groupOP == "AND" ? true : false);
            var rules = JsonStr.Last.Last;
            var sql = BaseService.BuildCommonSql(isAnd, rules);
            expression = BaseService.BuildCommonExpression<BaseClipToolEntity>(isAnd, rules);
            return expression;
        }
    }
}
