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
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.PlanManage
{ 
  
    public class PL_BOM_Service : RepositoryFactory<PL_BOMEntity>, PL_BOMIService
    {
        public DataTable GetWorkOrderMaterial(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PB.WorkOrder,
                               PB.BOMCode,
                               PB.MaterialCode,
                               PB.MaterialName,
                               PB.Spec,
                               PB.MaterialClass,
                               PB.UnitNum,
                               PB.Process,
                               PB.OrderType,
                               PBM.MaterialCode ChildCode,
                               PBM.MaterialName ChildName,
                               PBM.BOMCode ChildBomCode,
                               PBM.Num ChildNum,
                               PBM.Unit,
                               PBM.Warehouse,
                               PBM.ConsumeProcess
                        FROM dbo.PL_BOM PB
                            LEFT JOIN dbo.PL_BOMItems PBM
                                ON PB.Id = PBM.BOMId
                        WHERE PB.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PB.WorkOrder = '{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PB.MaterialCode = '{queryParam["MaterialCode"]}'");
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
        /// 获取修改工艺路线后工单BOM
        /// jpf add 2022-11-29 
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public DataTable GetVCWorkOrderTraitMaterial(string workOrder)
        {
            var table= new DataTable();
            //调用存储过程
            SqlParameter[] parameters = {
                    
                    new SqlParameter("@WorkOrder",SqlDbType.VarChar, 50),
          
            };
            parameters[0].Value = workOrder;
    
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                table =  db.ExecuteProc_Table("PL_GetTraitBOM", parameters);
   
                
                return table;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工艺路线生成失败");
                }
            }
            return table;
        }

        /// <summary>
        /// 工单BOM修改中保存调用方法
        /// jpf add 2022-11-29
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="entity_list"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int SaveEntity_List(string workOrder,string BOMid, List<PL_BOMItemsEntity> entity_list, out string msg)
        {
            int n = 0;
             msg = "";
            try
            {
                //查询原有的bom明细数据将其删除
                var BOMitemList = new PL_BOMItems_Service().Get_ExpressionList(t=>t.BOMId==BOMid).ToList();

                //将现有的BOM明细进行保存
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }

            return n;
        }
        /// <summary>
        /// 获取VC工单BOM数据
        /// jpf add 2022-11-28 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetVCWorkOrderMaterial(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT B.[Id],
                               B.FactoryCode,
                               B.FactoryName,
                               B.[BOMId],
                               B.BOMCode,
                               M.[MaterialCode],
                               M.[MaterialName],
                               B.[Num],
                               B.[Unit],
                               B.[Warehouse],
                               B.[ConsumeProcess] ProcessCode,
                               B.[ConsumeProcess],
                       
                               BR.ResourceName ProcessName,
                               B.[Creator],
                               B.[CreateTime],
                               B.[ModifyBy],
                               B.[ModifyTime],
                               V2.ItemName UnitName,
                               V3.ResourceName WarehouseName,
                               V4.ItemName MaterialClassName,
                               V5.ItemName SmallClassName,
                               M.SmallClass,
                               M.MaterialClass,
                               M.Spec,
                               MF.IsUsed
                        FROM dbo.PL_BOM PB
                            LEFT JOIN dbo.PL_BOMItems B
                                ON PB.Id = B.BOMId
								 LEFT JOIN dbo.V_DataDictionary V2
                                ON V2.EnCode = 'Unit'
                                   AND V2.ItemValue = B.Unit
                            LEFT JOIN dbo.BS_ModelWithResource V3
                                ON V3.ResourceCode = B.Warehouse
                            LEFT JOIN dbo.BS_ModelWithResource BR
                                ON BR.ModelLeve = 'Process'
                                   AND BR.ResourceCode = B.ConsumeProcess
                            LEFT JOIN dbo.Base_Material M
                                ON M.MaterialCode = B.MaterialCode
                            LEFT JOIN dbo.V_DataDictionary V4
                                ON V4.EnCode = 'MaterialType'
                                   AND V4.ItemValue = M.MaterialClass
                            LEFT JOIN dbo.V_DataDictionary V5
                                ON V5.EnCode = 'MaterialSmall'
                                   AND V5.ItemValue = M.SmallClass
                            LEFT JOIN dbo.Base_MaterialFactory MF
                                ON B.MaterialCode = MF.MaterialCode
								AND B.FactoryCode=MF.FactoryCode
                        WHERE PB.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PB.WorkOrder = '{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PB.MaterialCode = '{queryParam["MaterialCode"]}'");
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

        public PL_MaskUnitConsomeModel GetWorkOrderBomUnitConsome(string queryJson)
        {
            var sql =new StringBuilder();
            sql.Append(@"SELECT PB.WorkOrder,
                               CAST(PBM.Num * 1.0 / PB.UnitNum AS DECIMAL(10, 5)) DanHao
                        FROM dbo.PL_BOM PB
                            INNER JOIN dbo.PL_BOMItems PBM
                                ON PB.Id = PBM.BOMId
                            INNER JOIN dbo.PL_Material PM
                                ON PBM.MaterialCode = PM.MaterialCode
                        WHERE PM.MaterialClass IN ( 'WGMO', 'ZZMO' )
                              AND PB.IsDeleted = 0 ");
            JObject queryParam = queryJson.ToJObject();
            if (!queryParam["WorkOrder"].IsEmpty())
            {
                //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                sql.Append($" AND PB.WorkOrder = '{queryParam["WorkOrder"]}'");
            }
            if (!queryParam["MaterialCode"].IsEmpty())
            {
                //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                sql.Append($" AND PB.MaterialCode = '{queryParam["MaterialCode"]}'");
            }
            return  new RepositoryFactory().BaseRepository().FindList<PL_MaskUnitConsomeModel>(sql.ToString()).FirstOrDefault();
        }

        public DataTable GetWorkOrderBom(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PW.Id,
                               PP.OrderType,
                               v1.ItemName OrderTypeName,
                               PW.FactoryCode,
                               PW.FactoryName,
                               PW.ProductOrder,
                               PW.WorkOrder,
                               PW.CustomerPO,
                               PW.ContainerNO,
                               PW.OrderStatus,
                               v2.ItemName OrderStatusName,
                               PW.POStatus,
                               PW.WorkOrderType,
                               PW.MaterialCode,
                               PM.MaterialName,
                               PM.Spec,
                               PW.OrderPieces,
                               PW.OrderBox,
                               PW.OrderPallet,
                               PW.OrderStartPallet,
                               PW.TotalSheets,
                               PW.Yield,
                               PW.ActualSheets,
                               PW.Process,
                               PS.ProcessName,
                               PW.StartOperation,
                               M.ResourceName StartOperationName,
                               PW.TransferBy,
                               PW.FirstInspectionConfirm,
                               PW.FirstInspectionOperation,
                               PW.AvoidProduce,
                               PW.FreezeFlag,
                               PW.IsEnabled,
                               PW.DemandMaterial,
                               PW.Remark,
                               PB.BOMCode,
                               PB.UnitNum,
                               PB.Id PLBOMId
                        FROM dbo.PL_ProductionOrder PP
                            INNER JOIN dbo.PL_WorkOrder PW
                                ON PW.ProductOrder = PP.ProductOrder
                            LEFT JOIN dbo.PL_Material PM
                                ON PM.WorkOrder = PW.WorkOrder
                               and PM.FactoryCode=pw.FactoryCode
                            LEFT JOIN dbo.PL_BOM PB
                                ON PB.IsDeleted = 0
                                   AND PB.WorkOrder = PW.WorkOrder
                                    and pb.FactoryCode=PW.FactoryCode
                            LEFT JOIN dbo.PL_Process PS
                                ON PS.IsDeleted = 0
                                   AND PS.WorkOrder = PW.WorkOrder
                                and PS.FactoryCode=PW.FactoryCode
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON M.ResourceCode = PW.StartOperation
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'OrderType'
                                   AND PP.OrderType = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'WorkOrderStatus'
                                   AND PW.OrderStatus = v2.ItemValue
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PW.WorkOrder = '{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PW.MaterialCode = '{queryParam["MaterialCode"]}'");
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
        
        public DataTable GetOwnProductOrderBom(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT P.[Id],
                               P.[FactoryCode],
                               P.[ProcessCode],
                               P.[OrderType],
                               P.[WorkOrder],
                               P.[SmallClass],
                               P.[MaterialCode],
                               P.[PlanQty],
                               P.[Remark],
                               P.[Creator],
                               P.[CreateTime],
                               P.[ModifyBy],
                               P.[ModifyTime],
                               MW.ResourceName ProcessName,
                               V.ItemName SmallClassName,
                               M.MaterialName,
                               M.Spec,
                               P.BOMCode,
                               BP.Name CreatorName,
                               P.ProcessRoute,
                               PS.ProcessName ProcessRouteName
                        FROM [dbo].[PM_OwnProductOrder] P
                            LEFT JOIN dbo.BS_ModelWithResource MW
                                ON MW.ResourceCode = P.ProcessCode
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = P.SmallClass
                            LEFT JOIN dbo.PL_Material M
                                ON M.IsDeleted = 0
                                   AND P.WorkOrder = M.WorkOrder
                            LEFT JOIN dbo.PL_Process PS
                                ON PS.IsDeleted = 0
                                   AND PS.WorkOrder = P.WorkOrder
                            LEFT JOIN dbo.BS_People BP
                                ON BP.Code = P.Creator
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND P.WorkOrder = '{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND P.MaterialCode = '{queryParam["MaterialCode"]}'");
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

        public DataTable GetWorkOrderBomItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PB.WorkOrder,
                               PB.Id PLBOMId,
                               PBM.MaterialCode,
                               PBM.MaterialName,
                               PBM.BOMCode,
                               PBM.Num,
                               PBM.Unit,
                               PBM.UnitName,
                               PBM.Warehouse,
                               PBM.ConsumeProcess ProcessCode,
                               M.ResourceName ProcessName,
                               M1.ResourceName WarehouseName,
                               PBM.MaterialClass,
                               V1.ItemName MaterialClassName,
                               PBM.SmallClass,
                               V2.ItemName SmallClassName,
                               PBM.Spec,
							   PBM.IsUsed,
							   PBM.WFMark,
							   CASE PBM.WFMark WHEN '1' THEN '外发' ELSE '自制' END WFMarkName
                        FROM dbo.PL_BOM PB
                            LEFT JOIN dbo.PL_BOMItems PBM
                                ON PB.Id = PBM.BOMId
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON PBM.ConsumeProcess = M.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource M1
                                ON PBM.Warehouse = M1.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary V1
                                ON V1.EnCode = 'MaterialType'
                                   AND V1.ItemValue = PBM.MaterialClass
                            LEFT JOIN dbo.V_DataDictionary V2
                                ON V2.EnCode = 'MaterialSmall'
                                   AND V2.ItemValue = PBM.SmallClass
                        WHERE PB.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PB.WorkOrder = '{queryParam["WorkOrder"]}'");
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

        public PL_BOMEntity Get_ExpressionEntity(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<PL_BOMEntity> Get_ExpressionList(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
        }
        public void SaveForm(string keyValue,PL_BOMEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                this.BaseRepository().Insert(entity);
            }
            else
            {
                this.BaseRepository().Update(entity);
            }
        }

        #region 批量保存方法
        /// <summary>
        ///功能描述: 批量 保存表单（新增、修改）
        ///创　　建: Dragon
        ///创建日期: 2022-12-20 17:31:27
        ///任务编号: 工单BOM
        ///</summary>
        ///<param name="isUpdate">是否更新</param>
        ///<param name="list">实体对象数组</param>
        ///<returns>返回插入条数</returns>
        public int SaveEntity_List(bool isUpdate, List<PL_BOMEntity> list)
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
                            sql_temp.Append("UPDATE [dbo].[PL_BOM] set ");
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
                    //   bulkCopy.DestinationTableName = "PL_BOM";
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
        public void RemoveForm(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_BOMEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }


       /// <summary>
       /// 工单插入BOM
       /// </summary>
       /// <param name="factory"></param>
       /// <param name="bomCode"></param>
       /// <param name="processRoute"></param>
       /// <param name="workOrder"></param>
       /// <returns></returns>
        public bool InsertPLBom(string factory,string bomCode,string processRoute,string orderType, string workOrder)
        {
 
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@FactoryCode", SqlDbType.VarChar,30),
                new SqlParameter("@BOMCode", SqlDbType.VarChar,30),
                new SqlParameter("@ProcessRoute", SqlDbType.VarChar,30),
                new SqlParameter("@OrderType", SqlDbType.VarChar,30),
                new SqlParameter("@WorkOrder", SqlDbType.VarChar,30),
                new SqlParameter("@Resultmsg", SqlDbType.VarChar,100)
            };
            parameters[0].Value = factory;
            parameters[1].Value = bomCode;
            parameters[2].Value = processRoute;
            parameters[3].Value = orderType;
            parameters[4].Value = workOrder;
            parameters[5].Direction = ParameterDirection.Output;
  
            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("PL_InsertPLBOM", parameters);

                return true;
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
                        bulkCopy.DestinationTableName = "[dbo].[PL_BOM]";
                        //内存表的字段 对应数据库表的字段   
                        //bulkCopy.ColumnMappings.Add("Id", "Id");
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
