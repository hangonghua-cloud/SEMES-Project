using ALP.Application.Entity.Calendar;
using ALP.Util.WebControl;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util.Extension;
using ALP.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ALP.Application.Service.Calendar
{
    public class BS_ShiftManageService : Data.Repository.RepositoryFactory<BS_ShiftManage>
    {

        /// <summary>
        /// 查询工单分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  [ShifCode]
                                      ,[ShiftName]
                                      ,[WorkshopCode]
                                      ,[ProductionLine]
                                      ,[StartDate]
                                      ,[EndDate]
                                      ,[RestTime]
                                      ,[IsSatrtBeforeOneDay]
                                      ,[IsEndBeforeOneDay]
                                      ,[IsEnable]
                                      ,A.Creator
                                      ,pe1.Name as [CreatorName]
                                      ,[CreatDate]
                                      ,A.Modifier
                                      ,pe2.Name as [ModifierName]
                                      ,[ModifyDate]
                                  FROM [SIT_UA_MES].[dbo].[BS_ShiftManage]  as A
								  LEFT JOIN BS_People pe1 ON pe1.Code = A.Creator COLLATE Chinese_PRC_CI_AS
								  LEFT JOIN BS_People pe2 ON pe1.Code = A.Modifier COLLATE Chinese_PRC_CI_AS
								  where [IsEnable]=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //查询条件 

                if (!queryParam["ShifCode"].IsEmpty())
                {
                    sql.Append($" AND ShifCode = '{queryParam["ShifCode"]}'");
                }
                //编码
                if (!queryParam["ShiftName"].IsEmpty())
                {
                    sql.Append($" AND ShiftName like '%{queryParam["ShiftName"]}%'");
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
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT  [ShifCode]
                                      ,[ShiftName]
                                      ,[WorkshopCode]
                                      ,[ProductionLine]
                                      ,[StartDate]
                                      ,[EndDate]
                                      ,[RestTime]
                                      ,[IsSatrtBeforeOneDay]
                                      ,[IsEndBeforeOneDay]
                                      ,[IsEnable]
                                      ,A.Creator
                                      ,pe1.Name as [CreatorName]
                                      ,[CreatDate]
                                      ,A.Modifier
                                      ,pe2.Name as [ModifierName]
                                      ,[ModifyDate]
                                  FROM [SIT_UA_MES].[dbo].[BS_ShiftManage]  as A
								  LEFT JOIN BS_People pe1 ON pe1.Code = A.Creator COLLATE Chinese_PRC_CI_AS
								  LEFT JOIN BS_People pe2 ON pe1.Code = A.Modifier COLLATE Chinese_PRC_CI_AS
								  where [IsEnable]=1 
                        ");
                if (map != null)
                {
                    if (map.ContainsKey("ShifCode"))
                    {
                        sql.Append($" AND [ShifCode]='{map["ShifCode"]}'");
                    }
                    if (map.ContainsKey("ShiftName"))
                    {
                        sql.Append($" AND [ShiftName] like'%{map["ShiftName"]}%'");
                    }
                }
                sql.Append($" order by CreatDate asc");
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
        public int SaveEntity(string keyValue, BS_ShiftManage entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
              
                    BS_ShiftManage _entity = this.BaseRepository().IQueryable(t => t.ShifCode == entity.ShifCode || t.ShiftName == entity.ShiftName).FirstOrDefault();
                    if (_entity != null)
                    {
                        if (_entity.ShifCode == entity.ShifCode)
                        {
                            n = -1;
                            return n;
                        }
                        if (_entity.ShiftName == entity.ShiftName)
                        {
                            n = -2;
                            return n;
                        }
                    }

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
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        /// <returns></returns>
        public BS_ShiftManage GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
    }
}
