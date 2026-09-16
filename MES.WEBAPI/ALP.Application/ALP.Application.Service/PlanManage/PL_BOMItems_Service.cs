using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.PlanManage;
using ALP.Application.Code.Model;
using ALP.Data;

namespace ALP.Application.Service.PlanManage
{

    public class PL_BOMItems_Service : RepositoryFactory<PL_BOMItemsEntity>, PL_BOMItemsIService
    {
        /// <summary>
        /// 流转卡报工使用（带库位）
        /// </summary>
        /// <param name="bomId"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public IEnumerable<PL_BOMItemsEntity> GetBomItemList(string bomId, string processCode)
        {
            string sql = $@"SELECT c.LocationCode,
                                   a.*
                            FROM dbo.PL_BOMItems a
                                LEFT JOIN
                                (
                                    SELECT c1.ResourceCode LocationCode,
                                           c2.ResourceName LocationName,
                                           c2.ParentResource WhsCode
                                    FROM dbo.BS_ModelResourceExtendInfo c1
                                        INNER JOIN dbo.BS_ModelWithResource c2
                                            ON c1.ResourceCode = c2.ResourceCode
                                    WHERE c1.FieldCode = 'GLFS'
                                          AND c1.FieldValue = '0'
                                ) c
                                    ON a.Warehouse = c.WhsCode
                            WHERE ISNULL(a.Warehouse, '') <> ''
                                  AND a.BOMId = '{bomId}'
                                  AND a.ConsumeProcess = '{processCode}'";
            return this.BaseRepository().FindList(sql);
        }

        public PL_BOMItemsEntity Get_ExpressionEntity(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<PL_BOMItemsEntity> Get_ExpressionList(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
        }

        public void RemoveForm(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_BOMItemsEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }
        public void Save_List(bool isUpdate, List<PL_BOMItemsEntity> entity_list)
        {

            int n = 0;
            try
            {
                if (isUpdate)
                {
                    //n = this.BaseRepository().Update(entity_list);
                    StringBuilder sql = new StringBuilder();
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[PL_BOMItems] set ");
                            string keyValue = "";
                            //循环实体
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
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ",");
                                        }
                                    }
                                    else
                                    {
                                        if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                        {
                                            sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                        }
                                    }
                                }

                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    n = this.BaseRepository().Insert(entity_list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertDataTable(DataTable dt)
        {

            //System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            //st.Start();
            //string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["BaseDb"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(DbConstSettings.BaseDbString))
            {
                conn.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    try
                    {
                        //插入到数据库的目标表 TbA：表名  
                        bulkCopy.DestinationTableName = "[dbo].[PL_BOMItems]";
                        //内存表的字段 对应数据库表的字段   
                        foreach (DataColumn col in dt.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }

                        bulkCopy.WriteToServer(dt);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            //st.Stop();
            //Console.WriteLine("成功!测试时间为：" + st.ElapsedMilliseconds);
            //Console.ReadKey();
        }
    }
}
