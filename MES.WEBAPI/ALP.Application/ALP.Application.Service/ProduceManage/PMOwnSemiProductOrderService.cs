using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using Newtonsoft.Json.Linq;
using ALP.Util.WebControl;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// [PM_OwnSemiProductOrder]表数据访问类
    /// 作者:Dragon
    /// 创建时间:2022-12-06 10:24:28
    /// </summary>
    public class PMOwnSemiProductOrderService : RepositoryFactory<PMOwnSemiProductOrderEntity>
    {
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="pagination">分页</param>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.[FactoryCode],
                               A.[FactoryName],
                               A.[ProcessCode],
                               A.[ProcessName],
                               A.[ProductOrder],
                               A.[MaterialCode],
                               A.[MaterialName],
                               A.[Spec],
                               A.[ProductQty],
                               A.[ContainerNO],
                               A.[PalletNum],
                               A.[VolumeNum],
                               A.[DeliveryQty],
                               A.[UnitName],
                               A.[Meters],
                               A.[OrderStatus],
                               v1.ItemName OrderStatusName,
                               A.[CustomerCode],
                               A.[CustomerName],
                               A.[IsDeleted],
                               A.[Remark],
                               A.[CreatorCode],
                               A.[CreatorName],
                               A.[CreateTime],
                               A.[ModifyCode],
                               A.[ModifyName],
                               A.[ModifyTime]
                        FROM [dbo].[PM_OwnSemiProductOrder] A
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'OwnSemiProductOrderStatus'
                                   AND A.OrderStatus = v1.ItemValue
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
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND A.ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProcessName"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessName like N'%{queryParam["ProcessName"]}%'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND A.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
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
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND A.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
                }
                if (!queryParam["OrderStatus"].IsEmpty())
                {
                    sql.Append($" AND A.OrderStatus = N'{queryParam["OrderStatus"]}'");
                }
                if (!queryParam["CustomerCode"].IsEmpty())
                {
                    sql.Append($" AND A.CustomerCode like N'%{queryParam["CustomerCode"]}%'");
                }
                if (!queryParam["CustomerName"].IsEmpty())
                {
                    sql.Append($" AND A.CustomerName like N'%{queryParam["CustomerName"]}%'");
                }
                if (!queryParam["Remark"].IsEmpty())
                {
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                if (!queryParam["CreatorCode"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorCode like N'%{queryParam["CreatorCode"]}%'");
                }
                if (!queryParam["CreatorName"].IsEmpty())
                {
                    sql.Append($" AND A.CreatorName like N'%{queryParam["CreatorName"]}%'");
                }
                if (!queryParam["ModifyCode"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyCode like N'%{queryParam["ModifyCode"]}%'");
                }
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    sql.Append($" AND A.ModifyName like N'%{queryParam["ModifyName"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),A.CreateTime,120) <= N'{queryParam["EndTime"]}'");
                }
                //自制半成品订单出库时使用
                if (!queryParam["queryCode"].IsEmpty() && queryParam["queryCode"].ToString()== "1,2")
                {
                    sql.Append($" AND A.OrderStatus IN('1','2') ");
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
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOwnSemiProductOrderEntity</returns>
        public PMOwnSemiProductOrderEntity GetEntity(Expression<Func<PMOwnSemiProductOrderEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        #region 查询列表方法
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOwnSemiProductOrderEntity列表</returns>
        public IEnumerable<PMOwnSemiProductOrderEntity> GetList(Expression<Func<PMOwnSemiProductOrderEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        #endregion

        #region 校验是否存在
        /// <summary>
        ///功能描述: 根据Expression查询实体类是否存在
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOwnSemiProductOrderEntity列表</returns>
        public bool Any(Expression<Func<PMOwnSemiProductOrderEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).Any();
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, PMOwnSemiProductOrderEntity entity)
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
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<PMOwnSemiProductOrderEntity> list)
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
                            sql_temp.Append("UPDATE [dbo].[PM_OwnSemiProductOrder] set ");
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
                    //   bulkCopy.DestinationTableName = "PM_OwnSemiProductOrder";
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
        ///创建日期: 2022-12-06 10:24:28
        ///任务编号: 自制半成品订单管理
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>PMOwnSemiProductOrderEntity</returns>
        public int RemoveForm(Expression<Func<PMOwnSemiProductOrderEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion
    }
}

