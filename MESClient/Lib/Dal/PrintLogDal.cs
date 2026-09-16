using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dal
{
    public class PrintLogDal
    {
        /// <summary>
        /// 打印日志列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetPrintLogDataTableWithPage(PrintLogDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.Id,a.FactoryCode,a.FactoryName,
                               a.BusinessType,
                               a.Code,
                               a.Creator,
                               a.CreateTime,
                               a.Operator
                        FROM dbo.PM_PrintLog a
                        WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND a.FactoryCode = @FactoryCode ");
            }
            //日志类型
            if (!string.IsNullOrEmpty(query.BusinessType))
            {
                sql.Append(@" AND a.BusinessType=@BusinessType ");
            }
            //打印开始时间
            if (query.StartTime != null)
            {
                sql.Append(@" AND a.CreateTime >=@StartTime ");
            }
            //打印开始时间
            if (query.EndTime != null)
            {
                sql.Append(@" AND a.CreateTime<=@EndTime ");
            }
            //操作人
            if (!string.IsNullOrEmpty(query.Operator))
            {
                sql.Append(@" AND a.Operator LIKE @Operator ");
                query.Operator = string.Format(@"%{0}%", query.Operator);
            }
            //操作人
            if (!string.IsNullOrEmpty(query.Code))
            {
                sql.Append(@" AND a.Code LIKE @Code ");
                query.Code = string.Format(@"%{0}%", query.Code);
            }

            var dt = DapperHelper<DataTable>.GetDataTableWithPage(sql.ToString(), query);
            record = 0;
            if (dt.Rows.Count > 0)
            {
                record = Convert.ToInt32(dt.Rows[0]["TotalCount"]);
            }
            return dt;
        }

        /// <summary>
        /// 保存打印日志
        /// </summary>
        /// <param name="logList"></param>
        /// <returns></returns>
        public int InsertPrintLog(dynamic logEntity)
        {
            string sql = @"INSERT INTO dbo.PM_PrintLog
                        (
                            Id,
							FactoryCode,
							FactoryName,
                            BusinessType,
                            Code,
                            Creator,
                            CreateTime,
                            Operator
                        )
                        VALUES
                        (   @Id,        -- Id - varchar(40)
						    @FactoryCode,
							@FactoryName,
                            @BusinessType,        -- BusinessType - varchar(40)
                            @Code,
                            @Creator,        -- Code - varchar(40)
                            GETDATE(), -- CreateTime - datetime
                            @Operator         -- Operator - varchar(40)
                            )";
            return DapperHelper<int>.Execute(sql, logEntity);
        }
    }
}
