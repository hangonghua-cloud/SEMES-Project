using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.MaterialManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using Newtonsoft.Json.Linq;
using ALP.Util.WebControl;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
using System.ComponentModel.DataAnnotations.Schema;
               
namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// [Base_ProductOrderFileCon]表数据访问类
    /// 作者:Dragon
    /// 创建时间:2023-05-18 10:21:29
    /// </summary>
    public class BaseProductOrderFileConService:RepositoryFactory<BaseProductOrderFileConEntity>
    {
         #region 查询分页列表
         /// <summary>
         ///功能描述: 查询分页列表(DataTable)
         ///创　　建: Dragon
         ///创建日期: 2023-05-18 10:21:29
         ///任务编号: Base_按订单归档
         ///</summary>
         ///<param name="pagination">分页</param>
         ///<param name="queryJson">查询参数</param>
         ///<returns>返回分页列表</returns>
         public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
         {
              StringBuilder sql = new StringBuilder();
              sql.Append(@"SELECT
                                                       A.[Id], 
A.[ProductOrder] 
                         FROM [dbo].[Base_ProductOrderFileCon] A where 1 = 1 "); 
              var parameter = new List<DbParameter>();
              if (!string.IsNullOrEmpty(queryJson))
              {
                  JObject queryParam = queryJson.ToJObject();
                  if (!queryParam["Id"].IsEmpty())
                  {
                      sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                      //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                  }
                  if (!queryParam["ProductOrder"].IsEmpty())
                  {
                      sql.Append($" AND A.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                  }
                  if (!queryParam["StartTime"].IsEmpty())
                  {
                      sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) >= N'{queryParam["StartTime"]}'");
                  }
                  if (!queryParam["EndTime"].IsEmpty())
                  {
                      sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) <= N'{queryParam["EndTime"]}'");
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
         
         #region 查询实体方法
         /// <summary>
         ///功能描述: 根据Expression查询实体类
         ///创　　建: Dragon
         ///创建日期: 2023-05-18 10:21:29
         ///任务编号: Base_按订单归档
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>BaseProductOrderFileConEntity</returns>
         public BaseProductOrderFileConEntity GetEntity(Expression<Func<BaseProductOrderFileConEntity, bool>> condition)
         {
              return this.BaseRepository().FindEntity(condition);
         }
         #endregion
         
         #region 查询列表方法
         /// <summary>
         ///功能描述: 根据Expression查询实体类
         ///创　　建: Dragon
         ///创建日期: 2023-05-18 10:21:29
         ///任务编号: Base_按订单归档
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>BaseProductOrderFileConEntity列表</returns>
         public IEnumerable<BaseProductOrderFileConEntity> GetList(Expression<Func<BaseProductOrderFileConEntity, bool>> condition)
         {
              return this.BaseRepository().IQueryable(condition);
         }
         #endregion
        
        #region 校验是否存在
        /// <summary>
        ///功能描述: 根据Expression查询实体类是否存在
        ///创　　建: Dragon
        ///创建日期: 2023-05-18 10:21:29
        ///任务编号: Base_按订单归档
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>BaseProductOrderFileConEntity列表</returns>
        public bool Any(Expression<Func<BaseProductOrderFileConEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).Any();
        }
        #endregion
        
        #region 保存方法
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2023-05-18 10:21:29
        ///任务编号: Base_按订单归档
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, BaseProductOrderFileConEntity entity)
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
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    return this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region 批量保存方法
        /// <summary>
        ///功能描述: 批量 保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2023-05-18 10:21:29
        ///任务编号: Base_按订单归档
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<BaseProductOrderFileConEntity> list)
        {
            try
            {
                if(isUpdate)
                {
                    StringBuilder sql = new StringBuilder();
                        if (list.Count > 0)
                        {
                            foreach(var Save_obj in list)
                            {
                                StringBuilder sql_temp = new StringBuilder();
                                sql_temp.Append("UPDATE [dbo].[Base_ProductOrderFileCon] set ");
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
                                                sql_temp.Append(x.Name + " = " + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1: 0))+ ", ");
                                            }
                                        }
                                        else
                                        {
                                            var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                            if (!hasNotMapped)
                                            {
                                                if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                                {
                                                    sql_temp.Append(x.Name + " = N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                                }
                                            }
                                        }
                                    }
                                });
                                sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE Id = '{keyValue}'; ");
                            }
                        }
                        return this.BaseRepository().ExecuteBySql(sql.ToString());
                    }
                else
                {
                    return this.BaseRepository().Insert(list);
                    //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                    //{
                    //   bulkCopy.DestinationTableName = "Base_ProductOrderFileCon";
                    //   //foreach(var item in new MMWhsBindMaterialDetailsEntity().GetType().GetProperties())
                    //   //{
                    //   //   bulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    //   //}
                    //   bulkCopy.WriteToServer(Tools.ToDataTable(list));//将数据源数据写入到数据库中
                    //}
                    //return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        
        #region 删除方法
        /// <summary>
        ///功能描述: 根据Expression删除实体类
        ///创　　建: Dragon
        ///创建日期: 2023-05-18 10:21:29
        ///任务编号: Base_按订单归档
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>BaseProductOrderFileConEntity</returns>
        public int RemoveForm(Expression<Func<BaseProductOrderFileConEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion
    }
}

