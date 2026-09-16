using ALP.Application.Entity.Calendar;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Calendar
{
    public class BS_BreakTimeManageService : Data.Repository.RepositoryFactory<BS_BreakTimeManage>
    {

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT b.[Id]
                            ,b.[ShiftCode] 
							,sf.ShiftName
                            ,b.[Describe]
                            ,b.[Duration]
                            ,b.IsEnable
                            ,b.Creator
                            ,pe1.Name as [CreatorName]
                            ,b.[CreatDate]
                            ,b.Modifier
                            ,pe2.Name as [ModifierName]
                            ,b.[ModifyDate]
                        FROM [dbo].[BS_BreakTimeManage]  b
						left join [BS_ShiftManage] sf on sf.ShifCode=b.[ShiftCode] COLLATE Chinese_PRC_CI_AS 
						LEFT JOIN BS_People pe1 ON pe1.Code = b.Creator COLLATE Chinese_PRC_CI_AS
						LEFT JOIN BS_People pe2 ON pe1.Code = b.Modifier COLLATE Chinese_PRC_CI_AS
						where b.IsEnable=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject(); 
                //查询条件  
                if (!queryParam["ShifCode"].IsEmpty())
                {
                    sql.Append($" AND b.ShifCode = '{queryParam["ShifCode"]}'");
                } 
                if (!queryParam["ShiftName"].IsEmpty())
                {
                    sql.Append($" AND sf.ShiftName like '%{queryParam["ShiftName"]}%'");
                }
                if (!queryParam["Describe"].IsEmpty())
                {
                    sql.Append($" AND b.Describe like '%{queryParam["Describe"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map, out string msg)
        {
            DataTable dt = null;
            msg = "";
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@" SELECT b.[Id]
                            ,b.[ShiftCode] 
							,sf.ShiftName
                            ,b.[Describe]
                            ,b.[Duration]
                            ,b.IsEnable
                            ,b.Creator
                            ,pe1.Name as [CreatorName]
                            ,b.[CreatDate]
                            ,b.Modifier
                            ,pe2.Name as [ModifierName]
                            ,b.[ModifyDate]
                        FROM [dbo].[BS_BreakTimeManage]  b
						left join [BS_ShiftManage] sf on sf.ShifCode=b.[ShiftCode] COLLATE Chinese_PRC_CI_AS 
						LEFT JOIN BS_People pe1 ON pe1.Code = b.Creator COLLATE Chinese_PRC_CI_AS
						LEFT JOIN BS_People pe2 ON pe1.Code = b.Modifier COLLATE Chinese_PRC_CI_AS
						where b.IsEnable=1
                        ");
                if (map != null)
                {
                    if (map.ContainsKey("ShiftCode"))
                    {
                        sql.Append($" AND b.[ShiftCode]='{map["ShiftCode"]}'");
                    }
                    if (map.ContainsKey("ShiftName"))
                    {
                        sql.Append($" AND sf.[ShiftName] like '%{map["ShiftName"]}%'");
                    }
                    if (map.ContainsKey("Describe"))
                    {
                        sql.Append($" AND b.[Describe]='{map["Describe"]}'");
                    }
                }
                sql.Append($" order by b.CreatDate asc ");

                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, BS_BreakTimeManage entity, out string msg)
        {
            msg = "";
            int n = 0;
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    //entity.Create();
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    map.Add("Describe", entity.Describe); 
                    var dt = Get_Data(map, out msg);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.Describe + " 重复";

                    }
                    map.Clear();
                    map.Add("ShiftCode", entity.ShiftCode);
                    dt = Get_Data(map, out msg);
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.ShiftCode + " 重复";
                    }
                    else
                    {
                        n = this.BaseRepository().Insert(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        /// <returns></returns>
        public BS_BreakTimeManage GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(Convert.ToInt32(keyValue));
        }
    }
}
