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
    /// 虚拟槽服务层
    /// </summary>
    public class BaseVirtualSlotService : RepositoryFactory, IBaseVirtualSlotService
    {


        /// <summary>
        /// 获取线体对应的虚拟槽列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        public IEnumerable<BaseVirtualSlotEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<BaseVirtualSlotEntity>();
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
                        expression = LinqExtensions.True<BaseVirtualSlotEntity>();
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
        public DataTable GetBase_VirtualSlotByLine(string lineCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT Id, ISNULL(slot.FixtureNumber, 0) AS FixtureNumber, slot.LineCode, slot.SlotId,  ISNULL(REPLACE(m.ProFactorDescr, '箱模', ''), '未分配模具') AS ProFactorDescr, m.ProFactorCode, m.SyncTime, CONVERT(NVARCHAR(20), SlotId) AS SlotId1, ISNULL(MouldCode, '') AS MouldCodeCheck, ISNULL(REPLACE(m.ProFactorDescr, '箱模', ''), '未分配模具') AS MouldName
                FROM Base_VirtualSlot slot
                left join Base_ClipTool CLIP ON CLIP.LINECODE=SLOT.LINECODE AND CLIP.SLOTID=SLOT.SLOTID
                LEFT JOIN dbo.Base_MouldDic m ON m.ProFactorCode=CLIP.MouldCode WHERE LineCode=@lineCode");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
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
        public DataTable GetListEntity(string lineCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" SELECT   DISTINCT slot.LineCode, slot.SlotId, ISNULL(slot.FixtureNumber, 0) AS FixtureNumber, ISNULL(dic.ProFactorDescr, '空闲') AS ProFactorDescr,isnull(clip.mouldcode,'') as mouldcode FROM Base_VirtualSlot slot
					 LEFT JOIN dbo.Base_ClipTool clip ON clip.LineCode = slot.LineCode AND clip.SlotId=slot.SlotId
					 LEFT JOIN dbo.Base_MouldDic dic ON clip.MouldCode=dic.ProFactorCode
					  WHERE 1=1  ");
                List<DbParameter> parameter = new List<DbParameter>();
                if (!string.IsNullOrEmpty(lineCode))
                {
                    strSql.Append(@" AND slot.LineCode=@lineCode");
                    parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
                }
                return this.BaseRepository().FindTable(strSql.ToStr(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        ///  获取虚拟槽对应的模架数量
        /// </summary>
        /// <param name="lineCode"></param>
        /// <param name="slotId"></param>
        /// <returns></returns>
        public DataTable GetSlot(string lineCode,string slotId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM Base_VirtualSlot WHERE 1=1 ");
                List<DbParameter> parameter = new List<DbParameter>();
                if (!string.IsNullOrEmpty(lineCode))
                {
                    strSql.Append(@" AND LineCode=@lineCode");
                    parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
                }
                if (!string.IsNullOrEmpty(slotId))
                {
                    strSql.Append(@" and slotId=@slotId");
                    parameter.Add(DbParameters.CreateDbParameter("@slotId", slotId));
                }
                return this.BaseRepository().FindTable(strSql.ToStr(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 根据传入的Json查询条件 动态解析为Expression
        /// 
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<BaseVirtualSlotEntity, bool>> GetQueryLinqExtensionsByJsonStr(JObject JsonStr)
        {
            var expression = LinqExtensions.True<BaseVirtualSlotEntity>();
            if (JsonStr.Count == 0) return expression;
            var groupOP = JsonStr.First.Last.ToString();
            var isAnd = (groupOP == "AND" ? true : false);
            var rules = JsonStr.Last.Last;
            var sql = BaseService.BuildCommonSql(isAnd, rules);
            expression = BaseService.BuildCommonExpression<BaseVirtualSlotEntity>(isAnd, rules);
            return expression;
        }

        /// <summary>
        /// 获取模具对应的合法虚拟槽
        /// </summary>
        /// <param name="mouldCode"></param>
        /// <param name="lineCode"></param>
        /// <returns></returns>
        public DataTable GetSlotSelect(string mouldCode, string lineCode)
        {
            string sql = @"SELECT slot.*,MouldCode
                    INTO #Slot
                    FROM dbo.Base_VirtualSlot slot
                    left join Base_ClipTool CLIP ON CLIP.LINECODE=SLOT.LINECODE AND CLIP.SLOTID=SLOT.SLOTID
                    WHERE SLOT.LineCode=@lineCode 
                    SELECT * INTO #temp FROM #Slot WHERE MouldCode=@mouldCode
                    UNION
                    SELECT * FROM #Slot WHERE ISNULL(MouldCode,'')=''
                    SELECT t.*,'['+CONVERT(NVARCHAR(20),SlotId)+'] '+ISNULL(dic.ProFactorDescr,'') AS SlotName FROM #TEMP t
                    LEFT JOIN dbo.Base_MouldDic dic ON dic.ProFactorCode=t.MouldCode";

            List<DbParameter> parameter = new List<DbParameter>();
            parameter.Add(DbParameters.CreateDbParameter("@lineCode", lineCode));
            parameter.Add(DbParameters.CreateDbParameter("@mouldCode", mouldCode));

            return this.BaseRepository().FindTable(sql.ToStr(), parameter.ToArray());

        }
    }
}
