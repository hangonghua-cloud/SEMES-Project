using ALP.Application.Entity.Calendar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Calendar
{
    public class BS_ShiftOfCalendarService : Data.Repository.RepositoryFactory<BS_ShiftOfCalendar>
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT [Id]
                                  ,[CalendarId]
                                  ,[ShiftCode]
                                  ,[StartDate]
                                  ,[EndDate]
                                  ,A.Creator
                                  ,pe1.Name as [CreatorName]
                                  ,[CreatDate]
                                  ,A.Modifier
                                  ,pe2.Name as [ModifierName]
                                  ,[ModifyDate]
                              FROM [SIT_UA_MES].[dbo].[BS_ShiftOfCalendar] as A
							  LEFT JOIN BS_People pe1 ON pe1.Code = A.Creator COLLATE Chinese_PRC_CI_AS
							  LEFT JOIN BS_People pe2 ON pe1.Code = A.Modifier COLLATE Chinese_PRC_CI_AS
                        ");
                if (map != null)
                {
                    if (map.ContainsKey("CalendarId"))
                    {
                        sql.Append($" AND [CalendarId]='{map["CalendarId"]}'");
                    }
                    if (map.ContainsKey("ShiftCode"))
                    {
                        sql.Append($" AND [ShiftCode]='{map["ShiftCode"]}'");
                    }
                }
                sql.Append($" order by [ShiftCode] asc");
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
        public int SaveEntity(string keyValue, BS_ShiftOfCalendar entity, out string msg)
        {
            int n = 0;
            msg = "";
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
                    entity.Id = new Guid("N");
                    n = this.BaseRepository().Insert(entity);

                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        /// <summary>
        /// 获取保存表单SQL
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public string GetSaveSql(BS_ShiftOfCalendar entity, out string msg)
        { 
            msg = "";
            StringBuilder SBSQL = new StringBuilder();
            try
            {
                if (entity == null)
                {
                    msg = "对象不能为空";
                    return "";
                }
               
                SBSQL.Append($@"INSERT INTO [dbo].[BS_ShiftOfCalendar]
                                           ([Id]
                                           ,[CalendarId]
                                           ,[ShiftCode]
                                           ,[StartDate]
                                           ,[EndDate]
                                           ,[Creator]
                                           ,[CreatDate]
                                           ,[Modifier]
                                           ,[ModifyDate])
                                     VALUES
                                           ('{entity.Id}'
                                           ,'{entity.CalendarId}'
                                           ,'{entity.ShiftCode}'
                                           ,'{entity.StartDate}'
                                           ,'{entity.EndDate}'
                                           ,'{entity.Creator}'
                                           ,'{entity.CreatDate}'
                                           ,'{entity.Modifier}'
                                           ,'{entity.ModifyDate}');
                                ");
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return SBSQL.ToString();
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        /// <returns></returns>
        public BS_ShiftOfCalendar GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
    }
}
