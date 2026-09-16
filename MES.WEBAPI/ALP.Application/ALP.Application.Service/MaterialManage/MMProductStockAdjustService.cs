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
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Util.WebControl;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// [MM_ProductStockAdjust]表数据访问类
    /// 作者:dragon
    /// 创建时间:2022-05-14 17:54:49
    /// </summary>
    public class MMProductStockAdjustService:RepositoryFactory<MMProductStockAdjustEntity>
    {
         #region 查询分页列表
         /// <summary>
         ///功能描述: 查询分页列表(DataTable)
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="pagination">分页</param>
         ///<param name="queryJson">查询参数</param>
         ///<returns>返回分页列表</returns>
         public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
         {
              StringBuilder sql = new StringBuilder();
              sql.Append(@"SELECT
                                                       A.[Id], 
                            A.[FactoryCode], 
                            A.[ProductOrder], 
                            A.[WorkOrder], 
                            A.[ContainerNO], 
                            A.[MaterialCode], 
                            A.[CustomerPO], 
                            A.[WhsCode], 
                            A.[LocationCode], 
                            A.[PalletQty], 
                            A.[BoxQty], 
                            A.[PerPalletBoxQty], 
                            A.[AdjustPalletQty], 
                            A.[AdjustBoxQty], 
                            A.[Creator], 
                            A.[CreatorName], 
                            A.[CreateTime], 
                            A.[ModifyBy], 
                            A.[ModifyByName], 
                            A.[ModifyTime] 

                         FROM [dbo].[MM_ProductStockAdjust] A where 1 = 1 "); 
              var parameter = new List<DbParameter>();
              if (!string.IsNullOrEmpty(queryJson))
              {
                  JObject queryParam = queryJson.ToJObject();
                  if (!queryParam["Id"].IsEmpty())
                  {
                      sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                      //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                  }
                  if (!queryParam["FactoryCode"].IsEmpty())
                  {
                      sql.Append($" AND A.FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                  }
                  if (!queryParam["ProductOrder"].IsEmpty())
                  {
                      sql.Append($" AND A.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                  }
                  if (!queryParam["WorkOrder"].IsEmpty())
                  {
                      sql.Append($" AND A.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                  }
                  if (!queryParam["ContainerNO"].IsEmpty())
                  {
                      sql.Append($" AND A.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                  }
                  if (!queryParam["MaterialCode"].IsEmpty())
                  {
                      sql.Append($" AND A.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                  }
                  if (!queryParam["CustomerPO"].IsEmpty())
                  {
                      sql.Append($" AND A.CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                  }
                  if (!queryParam["WhsCode"].IsEmpty())
                  {
                      sql.Append($" AND A.WhsCode like N'%{queryParam["WhsCode"]}%'");
                  }
                  if (!queryParam["LocationCode"].IsEmpty())
                  {
                      sql.Append($" AND A.LocationCode like N'%{queryParam["LocationCode"]}%'");
                  }
                  if (!queryParam["Creator"].IsEmpty())
                  {
                      sql.Append($" AND A.Creator like N'%{queryParam["Creator"]}%'");
                  }
                  if (!queryParam["CreatorName"].IsEmpty())
                  {
                      sql.Append($" AND A.CreatorName like N'%{queryParam["CreatorName"]}%'");
                  }
                  if (!queryParam["ModifyBy"].IsEmpty())
                  {
                      sql.Append($" AND A.ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                  }
                  if (!queryParam["ModifyByName"].IsEmpty())
                  {
                      sql.Append($" AND A.ModifyByName like N'%{queryParam["ModifyByName"]}%'");
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
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>MMProductStockAdjustEntity</returns>
         public MMProductStockAdjustEntity GetEntity(Expression<Func<MMProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().FindEntity(condition);
         }
         #endregion
         
         #region 查询列表方法
         /// <summary>
         ///功能描述: 根据Expression查询实体类
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>MMProductStockAdjustEntity列表</returns>
         public IEnumerable<MMProductStockAdjustEntity> GetList(Expression<Func<MMProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().IQueryable(condition);
         }
         #endregion
         
         #region 保存方法
         /// <summary>
         ///功能描述:  保存表单（新增、修改）
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="keyValue">主键值</param>
         ///<param name="entity">实体类</param>
         ///<returns>返回插入条数</returns>
         public int SaveEntity(string keyValue, MMProductStockAdjustEntity entity)
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
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="isUpdate">是否更新</param>
         ///<param name="list">实体对象数组</param>
         ///<returns>返回插入条数</returns>
         public int SaveEntity_List(bool isUpdate, List<MMProductStockAdjustEntity> list)
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
                                  sql_temp.Append("UPDATE [dbo].[MM_ProductStockAdjust] set ");
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
                                              if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                              {
                                                  sql_temp.Append(x.Name + " = N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
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
                      //   bulkCopy.DestinationTableName = MM_ProductStockAdjust;
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
         ///创　　建: dragon
         ///创建日期: 2022-05-14 17:54:49
         ///任务编号: MM_成品库存校准记录
         ///</summary>
         ///<param name="condition">删除条件</param>
         ///<returns>MMProductStockAdjustEntity</returns>
         public int RemoveForm(Expression<Func<MMProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().Delete(condition);
         }
         #endregion
    }
}

