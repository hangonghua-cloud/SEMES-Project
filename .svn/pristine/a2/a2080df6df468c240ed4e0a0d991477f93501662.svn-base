using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.BaseManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Util.WebControl;
namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// [Base_Sequence]表数据访问类
    /// 作者:liyonguo
    /// 创建时间:2021-12-29 14:48:28
    /// </summary>
    public class BaseSequenceService : RepositoryFactory<BaseSequenceEntity>
    {
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="pagination">分页</param>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT
                 sql.Append(                            [SeqCode], 
                            [Desc], 
                            [NowSeqValue], 
                            [CreateTime], 
                            [DateMax], 
                            [MaxVlaue], 
                            [CreateBy], 
                            [Length], 
                            [Status], 
                            [InitValue], 
                            [ResetType], 
                            [IsRunning] 

                            FROM [dbo].[Base_Sequence] where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    //sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["SeqCode"].IsEmpty())
                {
                    //sql.Append($" AND SeqCode like N'%{queryParam["SeqCode"]}%'");
                }
                if (!queryParam["Desc"].IsEmpty())
                {
                    //sql.Append($" AND Desc like N'%{queryParam["Desc"]}%'");
                }
                if (!queryParam["NowSeqValue"].IsEmpty())
                {
                    //sql.Append($" AND NowSeqValue like N'%{queryParam["NowSeqValue"]}%'");
                }
                if (!queryParam["DateMax"].IsEmpty())
                {
                    //sql.Append($" AND DateMax like N'%{queryParam["DateMax"]}%'");
                }
                if (!queryParam["MaxVlaue"].IsEmpty())
                {
                    //sql.Append($" AND MaxVlaue like N'%{queryParam["MaxVlaue"]}%'");
                }
                if (!queryParam["CreateBy"].IsEmpty())
                {
                    //sql.Append($" AND CreateBy like N'%{queryParam["CreateBy"]}%'");
                }
                if (!queryParam["Status"].IsEmpty())
                {
                    //sql.Append($" AND Status like N'%{queryParam["Status"]}%'");
                }
                if (!queryParam["InitValue"].IsEmpty())
                {
                    //sql.Append($" AND InitValue like N'%{queryParam["InitValue"]}%'");
                }
                if (!queryParam["ResetType"].IsEmpty())
                {
                    //sql.Append($" AND ResetType like N'%{queryParam["ResetType"]}%'");
                }
                if (!queryParam["IsRunning"].IsEmpty())
                {
                    //sql.Append($" AND IsRunning like N'%{queryParam["IsRunning"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CreateTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CreateTime <= N'{queryParam["EndTime"]}'");
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
        ///功能描述: 根据Expression查询实体类
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>BaseSequenceEntity</returns>
        public BaseSequenceEntity GetEntity(Expression<Func<BaseSequenceEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>BaseSequenceEntity列表</returns>
        public IEnumerable<BaseSequenceEntity> GetList(Expression<Func<BaseSequenceEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, BaseSequenceEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    return this.BaseRepository().Update(entity);
                }
                else
                {
                    //if (string.IsNullOrEmpty(entity.Id))
                    //{
                    //    entity.Create();
                    //}
                    return this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        ///功能描述: 批量 保存表单（新增、修改）
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<BaseSequenceEntity> list)
        {
            try
            {
                if (isUpdate)
                {
                    StringBuilder sql = new StringBuilder();
                    if (list.Count > 0)
                    {
                        foreach (var Save_obj in list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[Base_Sequence] set ");
                            string keyValue = "";
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "Id")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + " = " + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ", ");
                                        }
                                    }
                                    else
                                    {
                                        if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                        {
                                            sql_temp.Append(x.Name + " = N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "', ");
                                        }
                                    }
                                }
                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id = '{keyValue}'; ");
                        }
                    }
                    return this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().Insert(list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        /// <summary>
        ///功能描述: 根据Expression删除实体类
        ///创　　建: liyonguo
        ///创建日期: 2021-12-29 14:48:28
        ///任务编号: 流水号管理
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>BaseSequenceEntity</returns>
        public int RemoveForm(Expression<Func<BaseSequenceEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <param name="seqCode"></param>
        /// <returns></returns>
        public string GetSerialNO(string seqCode)
        {
            var returnNum = string.Empty;
            var messageCode = string.Empty;
            try
            {
                //调用存储过程
                SqlParameter[] parameters = {
                    new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                    new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                    new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
                };
                parameters[0].Value = seqCode;
                parameters[1].Direction = ParameterDirection.Output;
                parameters[2].Direction = ParameterDirection.Output;
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
                messageCode = parameters[2].Value.ToString();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            return returnNum;
        }
        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <param name="seqCode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetSerialNO(string seqCode,int index)
        {
            var returnNum = string.Empty;
            var messageCode = string.Empty;
            try
            {
                //调用存储过程
                SqlParameter[] parameters = {
                    new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                    new SqlParameter("@Index", SqlDbType.Int),
                    new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                    new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
                };
                parameters[0].Value = seqCode;
                parameters[1].Value = index;
                parameters[2].Direction = ParameterDirection.Output;
                parameters[3].Direction = ParameterDirection.Output;
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetMultipleSerialNO", parameters);
                //返回参数值
                returnNum = parameters[2].Value.ToString();
                messageCode = parameters[3].Value.ToString();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            return returnNum;
        }

        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <returns></returns>
        public string GetBatch(string fDate)
        {
            var returnNum = string.Empty;
            var messageCode = string.Empty;
            try
            {
                //调用存储过程
                SqlParameter[] parameters = {
                    new SqlParameter("@FDate", SqlDbType.VarChar,60),
                    new SqlParameter("@FBatch", SqlDbType.VarChar,40),
                };
                parameters[0].Value = fDate;
                parameters[1].Direction = ParameterDirection.Output;
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("Proc_GetBatch", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
            return returnNum;
        }
    }
}
