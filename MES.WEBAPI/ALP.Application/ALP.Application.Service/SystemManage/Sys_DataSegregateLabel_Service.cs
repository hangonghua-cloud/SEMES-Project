using ALP.Application.Entity.SystemManage;
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

namespace ALP.Application.Service.SystemManage
{
    public class Sys_DataSegregateLabel_Service : Data.Repository.RepositoryFactory<Sys_DataSegregateLabel>
    {
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  [Id]
                                  ,[LabelName]
                                  ,[LabelColor]
                                  ,[LabelValue]
                                  ,[EnabledMark]
                                  ,CreateUser
                                  ,pe.Name as [CreateUserName]
                                  ,[CreateDate]
                                  ,[ModifyUser]
                                  ,[ModifyDate]
                                  ,[Remark]
                              FROM [SIT_UA_MES].[dbo].[Sys_DataSegregateLabel] as A
							  LEFT JOIN BS_People pe ON pe.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
                              where EnabledMark=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //查询条件  
                //标签名称
                if (!queryParam["LabelName"].IsEmpty())
                {
                    sql.Append($" AND LabelName like '%{queryParam["LabelName"]}%'");
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
        /// 查询不在指定组标签的标签
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable Get_PageData_notGroup(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  la.[Id]
                                  ,la.[LabelName]
                                  ,la.[LabelColor]
                                  ,la.[LabelValue]
                                  ,la.[EnabledMark]
                                  ,CreateUser
                                  ,pe.Name as [CreateUserName]
                                  ,la.[CreateDate]
                                  ,la.[ModifyUser]
                                  ,la.[ModifyDate]
                                  ,la.[Remark]
                              FROM [SIT_UA_MES].[dbo].[Sys_DataSegregateLabel] la 
							  LEFT JOIN BS_People pe ON pe.Code = la.CreateUser COLLATE Chinese_PRC_CI_AS
                              where EnabledMark=1 and not  EXISTS (
							select [LabelCode] from  [dbo].[Sys_DataSegregateGroupToLabel] gl where gl.[LabelCode]=la.Id  
							  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //查询条件  
                //标签名称
                if (!queryParam["GroupCode"].IsEmpty())
                {
                    sql.Append($" AND gl.[GroupCode]= '{queryParam["GroupCode"]}'");
                }

            }
            sql.Append(" ) ");
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
        /// 
        /// </summary>
        /// <param name="PersonCode"></param>
        /// <returns></returns>
        public DataTable Sum_DataSegregateGroupToPerson(string PersonCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT isnull(SUM(isnull(LabelValue,0)),0) AS LabelValue FROM [dbo].[Sys_DataSegregateGroupToPerson] P
                            LEFT JOIN [dbo].[Sys_DataSegregateGroup] G ON P.GroupCode=G.GroupCode
                            LEFT JOIN [dbo].[Sys_DataSegregateGroupToLabel] L ON G.GroupCode=L.GroupCode
                            WHERE PersonCode='{PersonCode}' AND G.EnabledMark=1");
            return this.BaseRepository().FindTable(sql.ToString());

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PersonCode"></param>
        /// <returns></returns>
        public int GetDataSegregateLabel(string PersonCode)
        {
            int sumlabelvalue = 0;
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT isnull(SUM(isnull(LabelValue,0)),0) AS LabelValue FROM [dbo].[Sys_DataSegregateGroupToPerson] P
                            LEFT JOIN [dbo].[Sys_DataSegregateGroup] G ON P.GroupCode=G.GroupCode
                            LEFT JOIN [dbo].[Sys_DataSegregateGroupToLabel] L ON G.GroupCode=L.GroupCode
                            WHERE PersonCode='{PersonCode}' AND G.EnabledMark=1");
            var findobject = this.BaseRepository().FindObject(sql.ToString());
            if (Int32.TryParse(findobject.ToString(), out sumlabelvalue))
            {
                return sumlabelvalue;
            }
            return sumlabelvalue;
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int DeleteEntity(string keyValue, Sys_DataSegregateLabel entity, out string msg)
        {
            msg = "";
            int n = 0;
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    n = this.BaseRepository().Update(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, Sys_DataSegregateLabel entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
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
        public Sys_DataSegregateLabel GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
    }
}
