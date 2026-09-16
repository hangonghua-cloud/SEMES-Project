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
    /// [PM_OperationPalletNum]表数据访问类
    /// 作者:Dragon
    /// 创建时间:2022-12-02 15:15:55
    /// </summary>
    public class PMOperationPalletNumService : RepositoryFactory<PMOperationPalletNumEntity>
    {
        #region 查询分页列表
        /// <summary>
        ///功能描述: 查询分页列表(DataTable)
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
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
                               A.DocType,
                               v1.ItemName DocTypeName,
                               A.MaterialCode,
                               A.MaterialName,
                               A.[Spec],
                               A.[PalletNum],
                               A.[UnitName],
                               A.[IsDeleted],
                               A.[Remark],
                               A.[CreatorCode],
                               A.[CreatorName],
                               A.[CreateTime],
                               A.[ModifyCode],
                               A.[ModifyName],
                               A.[ModifyTime],
							   A.HourCoefficient
                        FROM [dbo].[PM_OperationPalletNum] A
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'PriceType'
                                   AND A.DocType = v1.ItemValue
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
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProcessName"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessName like N'%{queryParam["ProcessName"]}%'");
                }
                if (!queryParam["DocType"].IsEmpty())
                {
                    sql.Append($" AND A.DocType = N'{queryParam["DocType"]}'");
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
                if (!queryParam["PalletNum"].IsEmpty())
                {
                    sql.Append($" AND A.PalletNum like N'%{queryParam["PalletNum"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOperationPalletNumEntity</returns>
        public PMOperationPalletNumEntity GetEntity(Expression<Func<PMOperationPalletNumEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        #region 查询列表方法
        /// <summary>
        ///功能描述: 根据Expression查询实体类
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOperationPalletNumEntity列表</returns>
        public IEnumerable<PMOperationPalletNumEntity> GetList(Expression<Func<PMOperationPalletNumEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        #endregion

        #region 校验是否存在
        /// <summary>
        ///功能描述: 根据Expression查询实体类是否存在
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="condition">查询条件</param>
        ///<returns>PMOperationPalletNumEntity列表</returns>
        public bool Any(Expression<Func<PMOperationPalletNumEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).Any();
        }
        #endregion

        #region 保存方法
        /// <summary>
        ///功能描述:  保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="keyValue">主键值</param>
        ///<param name="entity">实体类</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity(string keyValue, PMOperationPalletNumEntity entity)
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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<PMOperationPalletNumEntity> list)
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
                            sql_temp.Append("UPDATE [dbo].[PM_OperationPalletNum] set ");
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
                    //   bulkCopy.DestinationTableName = "PM_OperationPalletNum";
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
        ///创建日期: 2022-12-02 15:15:55
        ///任务编号: 工序物料托盘数量维护
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>PMOperationPalletNumEntity</returns>
        public int RemoveForm(Expression<Func<PMOperationPalletNumEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion

        #region 导出
        /// <summary>
        ///功能描述: 导出
        ///创　　建: Dragon
        ///创建日期: 2022-11-16 14:40:01
        ///任务编号: 绩效管理
        ///</summary>
        ///<param name="queryJson">查询参数</param>
        ///<returns>返回分页列表</returns>
        public DataTable GetDataTableList_Export(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                               A.[FactoryCode] 工厂编码,
                               A.[FactoryName] 工厂名称,
                               A.[ProcessCode] 工序编码,
                               A.[ProcessName] 工序名称,
                               v1.ItemName 单据类型,
                               A.MaterialCode 物料编码,
                               A.MaterialName 物料名称,
                               A.[Spec] 规格,
                               A.[PalletNum] 托盘数量,
                               A.[UnitName] 单位,
                               A.[Remark] 备注,
							   A.HourCoefficient 工时系数
                        FROM [dbo].[PM_OperationPalletNum] A
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'PriceType'
                                   AND A.DocType = v1.ItemValue
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
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                if (!queryParam["ProcessName"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessName like N'%{queryParam["ProcessName"]}%'");
                }
                if (!queryParam["DocType"].IsEmpty())
                {
                    sql.Append($" AND A.DocType = N'{queryParam["DocType"]}'");
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
                if (!queryParam["PalletNum"].IsEmpty())
                {
                    sql.Append($" AND A.PalletNum like N'%{queryParam["PalletNum"]}%'");
                }
                if (!queryParam["UnitName"].IsEmpty())
                {
                    sql.Append($" AND A.UnitName like N'%{queryParam["UnitName"]}%'");
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
            }
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}

