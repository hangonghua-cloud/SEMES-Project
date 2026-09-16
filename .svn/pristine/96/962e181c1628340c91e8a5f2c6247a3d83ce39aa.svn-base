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
    /// [MM_RawMaterialDispatchSub]表数据访问类
    /// 作者:Dragon
    /// 创建时间:2024-03-13 09:27:07
    /// </summary>
    public class MMRawMaterialDispatchSubService : RepositoryFactory<MMRawMaterialDispatchSubEntity>
    {
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="pagination">分页</param>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.Id,
                               A.DispatchId,
                               A.DeliveryNo,
                               A.MaterialCode,
                               A.MaterialName,
                               A.Qty,
                               A.UnitName,
                               A.IsDeleted,
                               A.Remark,
                               A.CreateByCode,
                               A.CreateByName,
                               A.CreateTime,
                               A.ModifyByCode,
                               A.ModifyByName,
                               A.ModifyTime,
                               A.DetailGrossWeight,
                               A.DetailVolume,
                               A.LineNum,
                               A.SubStatus,
                               A.Spec,
                               A.ProductOrder,
                               A.ProductLine,
                               A.Unit
                        FROM [dbo].[MM_RawMaterialDispatchSub] A
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["DispatchId"].IsEmpty())
                {
                    sql.Append($" AND A.DispatchId = N'{queryParam["DispatchId"]}'");
                }
                if (!queryParam["DeliveryNo"].IsEmpty())
                {
                    sql.Append($" AND A.DeliveryNo like N'%{queryParam["DeliveryNo"]}%'");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
                }
                if (!queryParam["Remark"].IsEmpty())
                {
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                if (!queryParam["CreateByCode"].IsEmpty())
                {
                    sql.Append($" AND A.CreateByCode like N'%{queryParam["CreateByCode"]}%'");
                }
                if (!queryParam["CreateByName"].IsEmpty())
                {
                    sql.Append($" AND A.CreateByName like N'%{queryParam["CreateByName"]}%'");
                }
                if (!queryParam["ModifyByCode"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyByCode like N'%{queryParam["ModifyByCode"]}%'");
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
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>MMRawMaterialDispatchSubEntity</returns>
        public MMRawMaterialDispatchSubEntity GetEntity(Expression<Func<MMRawMaterialDispatchSubEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        #region 查询列表方法
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>MMRawMaterialDispatchSubEntity列表</returns>
        public IEnumerable<MMRawMaterialDispatchSubEntity> GetList(Expression<Func<MMRawMaterialDispatchSubEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, MMRawMaterialDispatchSubEntity entity)
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
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<MMRawMaterialDispatchSubEntity> list)
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
                            sql_temp.Append("UPDATE [dbo].[MM_RawMaterialDispatchSub] set ");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id = '{keyValue}'; ");
                        }
                    }
                    return this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().Insert(list);
                    //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                    //{
                    //   bulkCopy.DestinationTableName = MM_RawMaterialDispatchSub;
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
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>MMRawMaterialDispatchSubEntity</returns>
        public int RemoveForm(Expression<Func<MMRawMaterialDispatchSubEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion

        /// <summary>
        ///功能描述: 发货详情
        ///创　　建: Dragon
        ///创建日期: 2024-03-13 09:27:07
        ///任务编号: MM_原材料半成品发货单子表
        ///</summary>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetDeliveryDetailList(string queryJson)
         {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.Id,
                               A.DispatchId,
                               A.DeliveryNo,
                               A.MaterialCode,
                               A.MaterialName,
							   A.Spec,
                               A.Qty,
                               A.UnitName,
                               A.IsDeleted,
                               A.Remark,
                               A.CreateByCode,
                               A.CreateByName,
                               A.CreateTime,
                               A.ModifyByCode,
                               A.ModifyByName,
                               A.ModifyTime,
                               A.DetailGrossWeight,
                               A.DetailVolume,
                               A.LineNum,
                               A.SubStatus
                        FROM [dbo].[MM_RawMaterialDispatchSub] A
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                }
                if (!queryParam["DispatchId"].IsEmpty())
                {
                    sql.Append($" AND A.DispatchId = N'{queryParam["DispatchId"]}'");
                }
                if (!queryParam["DeliveryNo"].IsEmpty())
                {
                    sql.Append($" AND A.DeliveryNo like N'%{queryParam["DeliveryNo"]}%'");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
 

            }

            return this.BaseRepository().FindTable(sql.ToString());

        }
    }
}

