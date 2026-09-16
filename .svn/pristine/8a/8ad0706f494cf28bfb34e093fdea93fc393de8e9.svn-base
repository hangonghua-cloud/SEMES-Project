using ALP.Application.Entity.Material;
using ALP.Application.IService.Material;
using ALP.Application.UtilExtend.Util;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Material
{
    public class BS_ProcessOfOperationsAttr_Service : RepositoryFactory<BS_ProcessOfOperationsAttrEntity>, BS_ProcessOfOperationsAttrIService
    {
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@" select * from  BS_ProcessOfOperationsAttr WHERE 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["OperationsId"].IsEmpty())
                {
                    sql.Append($" AND OperationsId = N'{queryParam["OperationsId"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
            }
            if (pagination == null)
            {
                sql.Append(@" ORDER BY SortCode ");
                return this.BaseRepository().FindTable(sql.ToString());
            }
            else
            {
                return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            }
        }
        public void Insert(BS_ProcessOfOperationsAttrEntity entity)
        {
            this.BaseRepository().Insert(entity);
        }
        public void Update(BS_ProcessOfOperationsAttrEntity entity)
        {
            this.BaseRepository().Update(entity);
        }
        public void Remove(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public BS_ProcessOfOperationsAttrEntity GetEntity(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<BS_ProcessOfOperationsAttrEntity> GetList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        public IEnumerable<BS_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition).ToList();
        }

        //public int CopyEntitySql_List(List<BS_ProcessOfOperationsAttrEntity> entity_list, out string msg)
        //{
        //    int n = 0;
        //    msg = "";
        //    try
        //    {
        //        //n = this.BaseRepository().Insert(entity_list);
        //        StringBuilder sql = new StringBuilder();
        //    sql.Append($@"INSERT INTO [dbo].[BS_ProcessOfOperationsAttr] (
        //                                    [Id]
        //                                    ,[OperationsId]
        //                                    ,[AttrCode]
        //                                    ,[AttrName]
        //                                    ,[AttrType]
        //                                    ,[AttrTypeName]
        //                                    ,[SortCode]
        //                                    ,[AttrValue]
        //                                    ,[Creator]
        //                                    ,[CreateTime]
        //                                    ,[ModifyBy]
        //                                    ,[ModifyTime]
        //                            ) VALUES ");
        //    if (entity_list.Count > 0)
        //    {
        //        foreach (var Save_obj in entity_list)
        //        {
        //            sql.Append($@"(
        //                        N'{Save_obj.Id}'
        //                        ,N'{Save_obj.OperationsId}'
        //                        ,N'{Save_obj.AttrCode}',N'{Save_obj.AttrName}'
        //                        ,{Save_obj.AttrType}
        //                        ,N'{Save_obj.AttrTypeName}'
        //                        ,{Save_obj.SortCode}
        //                        ,N'{Save_obj.Creator}'
        //                        ,'{(Save_obj.CreateTime == null ? DateTimeOffset.Now : Save_obj.CreateTime)}'
        //                        ,N'{Save_obj.ModifyBy}'
        //                        ,'{(Save_obj.ModifyTime == null ? DateTimeOffset.Now : Save_obj.ModifyTime)}'
        //                    ),");
        //        }
        //    }
        //    //批量执行更新语句
        //    n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));

        //    }
        //    catch (Exception ex)
        //    {
        //        msg = ex.Message;
        //    }
        //    return n;
        //}

        /// <summary>
        /// 功能描述：复制工艺路线下的工序
        /// 创建：jpf 2022-11-15
        /// </summary>
        /// <returns></returns>
        public int CopyEntity_List(List<BS_ProcessOfOperationsAttrEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulkCopy.DestinationTableName = "BS_ProcessOfOperationsAttr";
                    //foreach (var item in new BS_ProcessOfOperationsAttrEntity().GetType().GetProperties())
                    //{
                    //    bulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    //}
                    var datatableList = Tools.ToDataTable(entity_list);
                    bulkCopy.WriteToServer(datatableList);//将数据源数据写入到数据库中
                }
                return 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }

        #region 批量保存方法
        /// <summary>
        ///功能描述: 批量 保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-06 10:23:43
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<BS_ProcessOfOperationsAttrEntity> list)
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
                            sql_temp.Append("UPDATE [dbo].[BS_ProcessOfOperationsAttr] set ");
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
                    //   bulkCopy.DestinationTableName = "MM_SemiProductOut";
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
    }
}
