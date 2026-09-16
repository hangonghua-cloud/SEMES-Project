using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.BaseManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using Newtonsoft.Json.Linq;

namespace ALP.Application.Service.BaseManage
{
    public class F_FilesService : RepositoryFactory<F_FilesEntity>
    {
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(string queryJson, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT   [ID]
                                  ,[FID]
                                  ,[FType]
                                  ,[FName]
                                  ,[FPath]
                                  ,[CreateTime]
                                  ,[CreateUser]
                                  ,[FExtension]
                                  ,[Ext1]
                                  ,[Ext2]
                              FROM  [dbo].[F_Files] where 1=1 ");
                if (!string.IsNullOrEmpty(queryJson))
                {
                    JObject queryParam = queryJson.ToJObject();
                    //查询条件  
                    if (!queryParam["FID"].IsEmpty())
                    {
                        sql.Append($" AND [FID]='{queryParam["FID"]}'");
                    }
                    if (!queryParam["FType"].IsEmpty())
                    {
                        sql.Append($" AND [FType]=N'{queryParam["FType"]}'");
                    }
                    if (!queryParam["FName"].IsEmpty())
                    {
                        sql.Append($" AND [FName]=N'{queryParam["FName"]}'");
                    }
                }

                sql.Append($"  order by [CreateTime] asc");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 保存表单（新增）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string SaveEntity(F_FilesEntity entity, out string msg)
        {
            string id = "";
            int n = 0;
            msg = "";
            try
            {
                id = entity.ID = Guid.NewGuid().ToString("N").ToUpper();              
                n = this.BaseRepository().Insert(entity); 
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            if (n == 0)
                id = "";
            return id;
        }
        /// <summary>
        /// 删除（新增）
        /// </summary>
        /// <param name="id">实体对象</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int DeleteEntity(string id, out string msg)
        { 
            int n = 0;
            msg = "";
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    msg = "id不能为空";
                    return n;
                }
                string sql = $"delete [F_Files] where ID='{id}'";
                n = this.BaseRepository().ExecuteBySql( sql);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
           
            return n;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="fid">主键值</param>
        /// <param name="NotInIds">不删除的图片id</param>
        /// <param name="msg"></param>
        public int DeleteEntity(string fid, string NotInIds, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (string.IsNullOrEmpty(fid))
                {
                    msg = "id不能为空";
                    return n;
                }
                string sql = $@"DELETE  FROM [dbo].[F_Files]  
                            WHERE FID = '{fid}' AND ID not in ({NotInIds}); ";
                n = this.BaseRepository().ExecuteBySql(sql);
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
        public F_FilesEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
    }
}
