using ALP.Application.Entity.Calendar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Calendar
{
    public class BS_CalendarManageService : Data.Repository.RepositoryFactory<BS_CalendarManage>
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
                                  ,[ProductionLine]
                                  ,[EveryDay]
                                  ,[IsHoliday]
                                  ,Creator
								  ,pe1.Name as [CreatorName]
                                  ,[CreatDate]
                                  ,Modifier
                                  ,pe2.Name as [ModifierName]
                                  ,[ModifyDate]
                                  ,[Title]
                              FROM [SIT_UA_MES].[dbo].[BS_CalendarManage] as A
						LEFT JOIN BS_People pe1 ON pe1.Code = A.Creator COLLATE Chinese_PRC_CI_AS
						LEFT JOIN BS_People pe2 ON pe1.Code = A.Modifier COLLATE Chinese_PRC_CI_AS
                       where 1=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("Id"))
                    {
                        sql.Append($" AND [Id]='{map["Id"]}'");
                    }
                    if (map.ContainsKey("ProductionLine"))
                    {
                        sql.Append($" AND [ProductionLine]='{map["ProductionLine"]}'");
                    }
                    if (map.ContainsKey("EveryDay"))
                    {
                        sql.Append($" AND [EveryDay]='{map["EveryDay"]}'");
                    }
                    if (map.ContainsKey("StartDay") && map.ContainsKey("EndDay"))
                    {
                        sql.Append($" AND [EveryDay] between '{map["StartDay"]}' and '{map["EndDay"]}'");
                    }
                }
                sql.Append($" order by EveryDay asc");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询日历和班次信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data_Both(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT   c.[Id] 
                              ,c.[EveryDay]
                              ,c.[IsHoliday]  
                              ,c.[Title]	 
	                          ,shof.Id shiftId
	                          ,shof.ShiftCode
	                          ,shof.StartDate
	                          ,shof.EndDate
	                          ,sh.ShiftName
                          FROM [SIT_UA_MES].[dbo].[BS_CalendarManage] c
                          left join [BS_ShiftOfCalendar] shof on c.Id=shof.CalendarId   
                          left join [dbo].[BS_ShiftManage]  sh on shof.ShiftCode=sh.ShifCode  COLLATE Chinese_PRC_CS_AS 
                          where 1=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("ProductionLine"))
                    {
                        sql.Append($" AND  c.[ProductionLine]='{map["ProductionLine"]}'");
                    }
                    if (map.ContainsKey("StartDay") && map.ContainsKey("EndDay"))
                    {
                        sql.Append($" AND c.[EveryDay] between '{map["StartDay"]}' and '{map["EndDay"]}'");
                    }
                }
                sql.Append($" order by c.[EveryDay] asc");
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
        public int SaveEntity(string keyValue, BS_CalendarManage entity, out string msg)
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
        /// 修改
        /// </summary> 
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int UpdateState(BS_CalendarManage entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (entity == null)
                {
                    msg = "对象不能为空";
                    return n;
                }
                string strSQL = $@"UPDATE [dbo].[BS_CalendarManage]
                                   SET
                                      [IsHoliday] = '{entity.IsHoliday}'
                                      ,[Modifier] = '{entity.Modifier}'
                                      ,[ModifyDate] = getdate()
                                      ,[Title] ='{entity.Title}'
                                 WHERE [ProductionLine]='{entity.ProductionLine}' and [EveryDay]='{entity.EveryDay}'";  
                n = this.BaseRepository().ExecuteBySql(strSQL);

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
        /// <param name="entity">对象实体</param>
        /// <param name="msg">异常消息</param>
        /// <returns></returns>
        public string GetSaveSql(BS_CalendarManage entity, out string msg)
        {
            StringBuilder SBSQL = new StringBuilder();
            msg = "";
            try
            {
                if (entity == null)
                {
                    msg = "对象不能为空";
                    return SBSQL.ToString();
                }
                SBSQL.Append($@"INSERT INTO [dbo].[BS_CalendarManage]
                                           ([Id]
                                           ,[ProductionLine]
                                           ,[EveryDay]
                                           ,[IsHoliday]
                                           ,[Creator]
                                           ,[CreatDate]
                                           ,[Modifier]
                                           ,[ModifyDate]
                                           ,[Title])
                                     VALUES
                                           ('{entity.Id}'
                                           ,'{entity.ProductionLine}'
                                           ,'{entity.EveryDay}'
                                           ,'{entity.IsHoliday}'
                                           ,'{entity.Creator}'
                                           ,'{entity.CreatDate}'
                                           ,'{entity.Modifier}'
                                           ,'{entity.ModifyDate}'
                                           ,N'{entity.Title}');
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
        public BS_CalendarManage GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
    }
}
