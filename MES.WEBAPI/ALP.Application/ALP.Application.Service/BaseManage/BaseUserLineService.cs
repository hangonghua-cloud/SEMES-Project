using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data;
using ALP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“BaseUserLineService”的 XML 注释
    public class BaseUserLineService : RepositoryFactory, IBaseUserLineService
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“BaseUserLineService”的 XML 注释
    {
        /// <summary>
        /// 插入用户线体对应关系
        /// </summary>
        /// <param name="paramalarmList"></param>
        public void InsertBaseUserLine(List<string> paramalarmList)
        {
            try
            {
                string insertSql = string.Empty;
                for (int i = 0; i < paramalarmList.Count; i++)
                {
                    insertSql += paramalarmList[i];
                }
                this.BaseRepository().ExecuteBySql(insertSql);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 获取当前用户所属的线体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>DataTable</returns>
        public DataTable GetListTable(string userId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT r.*,ISNULL(s.SlotCounts,'0') AS SlotCounts
                                        FROM dbo.Base_UserLineRelation R
                                             LEFT JOIN(SELECT COUNT(*) AS SlotCounts, LineCode
                                                       FROM Base_VirtualSlot
                                                       GROUP BY LineCode)s ON R.LineCode=s.LineCode where UserId=@userId Order by s.lineCode");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
                return this.BaseRepository().FindTable(strSql.ToString(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 获取当前用户所属的线体
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>List</returns>
        public IEnumerable<BaseUserLineEntity> GetListEntity(string userId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM dbo.Base_UserLineRelation R  where UserId=@userId");
                var parameter = new List<DbParameter>();
                parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
                return this.BaseRepository().FindList<BaseUserLineEntity>(strSql.ToString(), parameter.ToArray());
            }
            catch
            {
                throw;
            }
        }
    }
}
