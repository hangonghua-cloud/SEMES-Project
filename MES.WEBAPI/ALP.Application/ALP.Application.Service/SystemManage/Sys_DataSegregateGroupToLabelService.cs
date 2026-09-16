using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util;
using ALP.Util.Extension;
using System.Data.Common;
using ALP.Application.Service.Common;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public class Sys_DataSegregateGroupToLabelService : RepositoryFactory<Sys_DataSegregateGroupToLabel>, ISys_DataSegregateGroupToLabelService
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToLabel> GetPageList(Pagination pagination, string groupCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select Id,GroupCode,LabelCode,LabelColor,LabelValue from Sys_DataSegregateGroupToLabel where 1=1 and GroupCode='" + groupCode + "' ");
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT  gl.[Id]  
                          ,gl.[GroupCode] 
                          ,la.LabelName LabelName
                          ,gl.[LabelCode]
                          ,gl.[LabelColor]
                          ,gl.[LabelValue]
                      FROM  [dbo].[Sys_DataSegregateGroupToLabel] gl
                      left join Sys_DataSegregateLabel la on gl.[LabelCode]=la.Id where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //查询条件   
                //组code
                if (!queryParam["GroupCode"].IsEmpty())
                {
                    sql.Append($" AND gl.GroupCode = '{queryParam["GroupCode"]}'");
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
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量保存表单SQL
        /// </summary>
        /// <param name="listEnity"></param>
        /// <param name="GroupCode"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool GetSaveSql_DataSegregateGroupToLabel(List<Sys_DataSegregateLabel> listEnity, string GroupCode, out string msg)
        {
            bool bResult = false;
            int sum = 0;
            List<string> list_sql = new List<string>();
            msg = "";
            try
            {
                if (listEnity == null)
                {
                    msg = "对象不能为空";
                }
                StringBuilder SBSQL = new StringBuilder();
                foreach (var item in listEnity)
                {
                    
                    string ID = Guid.NewGuid().ToString("N").ToUpper();
                    SBSQL.Append($@" INSERT INTO [dbo].[Sys_DataSegregateGroupToLabel]
                                       ([Id]
                                       ,[GroupCode]
                                       ,[LabelCode]
                                       ,[LabelColor]
                                       ,[LabelValue])
                                 VALUES
                                       ('{ID}'
                                       ,'{GroupCode}'
                                       ,'{item.Id}'
                                       ,'{item.LabelColor}'
                                       ,{item.LabelValue})");

                    list_sql.Add(SBSQL.ToString());
                }
                CommService commService = new CommService();
                //批量执行事务  
                bResult = commService.ExecuteBySql_Trans(list_sql, out msg);                            
                SBSQL.Append($@"UPDATE  [dbo].[Sys_DataSegregateGroup] SET GroupLabelValue=(SELECT SUM(D.LabelValue) FROM [dbo].[Sys_DataSegregateGroupToLabel] D WHERE D.GroupCode='{GroupCode}' )
                                 FROM[dbo].[Sys_DataSegregateGroup]
                                WHERE GroupCode = '{GroupCode}' ");
                int result = this.BaseRepository().ExecuteBySql(SBSQL.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return bResult;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int RemoveForm(string ids)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"delete from [dbo].[Sys_DataSegregateGroupToLabel] where Id in({ids}) ");
            try
            {
                return this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 更新组标签值
        /// </summary>
        /// <param name="GroupCode"></param>
        /// <returns></returns>
        public int Update_GroupLabelValue(string GroupCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"update  [dbo].[Sys_DataSegregateGroup] 
                          set [GroupLabelValue]=(select sum([LabelValue]) from [dbo].[Sys_DataSegregateGroupToLabel] where [GroupCode]='{GroupCode}' )
                          where [GroupCode]='{GroupCode}' ");
            try
            {
                return this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex ;
            }
        }
        #endregion
    }
}
