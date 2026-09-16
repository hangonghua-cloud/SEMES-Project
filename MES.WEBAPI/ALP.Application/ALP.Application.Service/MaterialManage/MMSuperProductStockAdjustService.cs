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
namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// [MM_SuperProductStockAdjust]表数据访问类
    /// 作者:dragon
    /// 创建时间:2022-01-10 19:49:52
    /// </summary>
    public class MMSuperProductStockAdjustService:RepositoryFactory<MMSuperProductStockAdjustEntity>
    {
       #region 查询分页列表
       /// <summary>
       ///功能描述: 查询分页列表(DataTable)
       ///创　　建: dragon
       ///创建日期: 2022-01-10 19:49:52
       ///任务编号: 超产品库存校准
       ///</summary>
       ///<param name="pagination">分页</param>
       ///<param name="queryJson">查询参数</param>
       ///<returns>返回分页列表</returns>
       public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
       {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT
                                                     A.[Id], 
                            A.[ProcessCode], 
                            A.[MaterialCode], 
                            A.[MaterialName], 
                            A.[Spec], 
                            A.[MMXH], 
                            A.[BatchNo], 
                            A.[DXZH], 
                            A.[TakeType], 
                            A.[Qty], 
                            A.[Creator], 
                            A.[CreateTime], 
                            A.[ModifyBy], 
                            A.[ModifyTime], 
                            A.[Remark] 

                       FROM [dbo].[MM_SuperProductStockAdjust] A where 1 = 1 "); 
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND A.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND A.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND A.Spec like N'%{queryParam["Spec"]}%'");
                }
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND A.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    sql.Append($" AND A.BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                if (!queryParam["TakeType"].IsEmpty())
                {
                    sql.Append($" AND A.TakeType like N'%{queryParam["TakeType"]}%'");
                }
                if (!queryParam["Creator"].IsEmpty())
                {
                    sql.Append($" AND A.Creator like N'%{queryParam["Creator"]}%'");
                }
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                if (!queryParam["Remark"].IsEmpty())
                {
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND A.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND A.CreateTime <= N'{queryParam["EndTime"]}'");
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
         ///创建日期: 2022-01-10 19:49:52
         ///任务编号: 超产品库存校准
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>MMSuperProductStockAdjustEntity</returns>
         public MMSuperProductStockAdjustEntity GetEntity(Expression<Func<MMSuperProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().FindEntity(condition);
         }
         #endregion
         
         #region 查询列表方法
         /// <summary>
         ///功能描述: 根据Expression查询实体类
         ///创　　建: dragon
         ///创建日期: 2022-01-10 19:49:52
         ///任务编号: 超产品库存校准
         ///</summary>
         ///<param name="condition">查询条件</param>
         ///<returns>MMSuperProductStockAdjustEntity列表</returns>
         public IEnumerable<MMSuperProductStockAdjustEntity> GetList(Expression<Func<MMSuperProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().IQueryable(condition);
         }
         #endregion
         
         #region 保存方法
         /// <summary>
         ///功能描述:  保存表单（新增、修改）
         ///创　　建: dragon
         ///创建日期: 2022-01-10 19:49:52
         ///任务编号: 超产品库存校准
         ///</summary>
         ///<param name="keyValue">主键值</param>
         ///<param name="entity">实体类</param>
         ///<returns>返回插入条数</returns>
         public int SaveEntity(string keyValue, MMSuperProductStockAdjustEntity entity)
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
         ///创建日期: 2022-01-10 19:49:52
         ///任务编号: 超产品库存校准
         ///</summary>
         ///<param name="isUpdate">是否更新</param>
         ///<param name="list">实体对象数组</param>
         ///<returns>返回插入条数</returns>
         public int SaveEntity_List(bool isUpdate, List<MMSuperProductStockAdjustEntity> list)
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
                                  sql_temp.Append("UPDATE [dbo].[MM_SuperProductStockAdjust] set ");
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
                                                  sql_temp.Append(x.Name + " = N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "', ");
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
         ///创建日期: 2022-01-10 19:49:52
         ///任务编号: 超产品库存校准
         ///</summary>
         ///<param name="condition">删除条件</param>
         ///<returns>MMSuperProductStockAdjustEntity</returns>
         public int RemoveForm(Expression<Func<MMSuperProductStockAdjustEntity, bool>> condition)
         {
              return this.BaseRepository().Delete(condition);
         }
         #endregion
    }
}

