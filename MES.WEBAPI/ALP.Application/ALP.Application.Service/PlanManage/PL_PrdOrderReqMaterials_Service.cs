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
using ALP.Application.UtilExtend.Offices;
using ALP.Application.Entity.SystemManage;
using ALP.Data;

namespace ALP.Application.Service.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_PrdOrderReqMaterialsService 业务服务类
    /// 4.任务编号: 订单物料需求表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_PrdOrderReqMaterials_Service : RepositoryFactory<PL_PrdOrderReqMaterialsEntity>, PL_PrdOrderReqMaterialsIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 获取物料需求
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetWorkOrderReqMaterials(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PW.FactoryCode,
                               PW.FactoryName,
                               PW.ProductOrder,
                               PM.MaterialCode,
                               PM.MaterialName,
                               PM.UnitName,
                               PM.Spec,
                               PM.SmallClass,
                               V.ItemName SmallClassName,
                               SUM(PM.Amount) Amount,
                               PP.DeliveryDate
                        FROM dbo.PL_ProductionOrder PP
                            INNER JOIN dbo.PL_WorkOrder PW
                                ON PW.ProductOrder = PP.ProductOrder
                            INNER JOIN dbo.PL_PrdOrderReqMaterials PM
                                ON PM.WorkOrder = PW.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = PM.SmallClass
                        WHERE PM.IsDeleted=0 AND NOT EXISTS
                        (
                            SELECT 1
                            FROM dbo.PL_PurchaseOrder pu
                            WHERE pu.ProductOrder = PW.ProductOrder
                                  AND pu.MaterialCode = PM.MaterialCode
                        ) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND PW.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["ProcureType"].IsEmpty())
                {
                    sql.Append($" AND PM.PurchaseType = N'{queryParam["ProcureType"]}'");
                }
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND PM.SmallClass = N'{queryParam["SmallClass"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND PW.ProductOrder Like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["Material"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND (PM.MaterialCode Like N'%{queryParam["Material"]}%' OR PM.MaterialName Like N'%{queryParam["Material"]}%')");
                }
                if (!queryParam["WorkOrderStatusStr"].IsEmpty())
                {
                    sql.Append($" AND PW.OrderStatus < N'{queryParam["WorkOrderStatusStr"]}'");
                }

                if (!queryParam["StartPrepay"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND PP.DeliveryDate >= N'{queryParam["StartPrepay"]}'");
                }
                if (!queryParam["EndPrepay"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND PP.DeliveryDate <= N'{queryParam["EndPrepay"]}'");
                }
                sql.Append(@" GROUP BY PW.FactoryCode,
                                 PW.FactoryName,
                                 PW.ProductOrder,
                                 PM.MaterialCode,
                                 PM.MaterialName,
                                 PM.Spec,
                                 PM.SmallClass,
                                 V.ItemName,
                                 PM.UnitName,
                                 PP.DeliveryDate ");
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
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[WorkOrder]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[Amount],UnitName
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_PrdOrderReqMaterials] where IsDeleted=0  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //生产订单 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工单编码 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["柜号"].IsEmpty())
                {
                    //sql.Append($" AND 柜号 = N'{queryParam["柜号"]}'");
                    sql.Append($" AND 柜号 like N'%{queryParam["柜号"]}%'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["FinalMaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND FinalMaterialCode = N'{queryParam["FinalMaterialCode"]}'");
                    sql.Append($" AND FinalMaterialCode like N'%{queryParam["FinalMaterialCode"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料需求量 是否为空进行查询
                if (!queryParam["Amount"].IsEmpty())
                {
                    //sql.Append($" AND Amount = N'{queryParam["Amount"]}'");
                    sql.Append($" AND Amount like N'%{queryParam["Amount"]}%'");
                }

                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = '{queryParam["queryName"]}'");
                    //sql.Append($" AND 关键名称 like N'%{queryParam["queryName"]}%'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = N'{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like N'%{queryParam["queryCode"]}%'");
                }
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindList(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PW.ProductOrder,
                               PM.MaterialCode,
                               PM.MaterialName,
                               PM.UnitName,
                               SUM(PM.Amount) Amount,
                               PP.DeliveryDate
                        FROM dbo.PL_ProductionOrder PP
                            INNER JOIN dbo.PL_WorkOrder PW
                                ON PW.ProductOrder = PP.ProductOrder
                            INNER JOIN dbo.PL_PrdOrderReqMaterials PM
                                ON PM.WorkOrder = PW.WorkOrder
                        WHERE PM.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //生产订单 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND PW.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }

                //工单编码 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND PW.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }

                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND PM.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND PM.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }

                sql.Append(@" GROUP BY PW.ProductOrder,PM.MaterialCode,PM.MaterialName,PM.UnitName,PP.DeliveryDate");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                       [Id]
                      ,[WorkOrder]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[Amount],UnitName
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_PrdOrderReqMaterials] where IsDeleted=0 ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" and Id = N'{checkType}' ");
            }
            msg = "";
            try
            {
                return this.BaseRepository().FindList(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_PrdOrderReqMaterialsEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_PrdOrderReqMaterialsEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PrdOrderReqMaterialsEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (IsUpdate)
                {
                    //n = this.BaseRepository().Update(entity_list);
                    StringBuilder sql = new StringBuilder();
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[PL_PrdOrderReqMaterials] set ");
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
                msg = ex.Message;
            }
            return n;
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
                        bulkCopy.DestinationTableName = "[dbo].[PL_PrdOrderReqMaterials]";
                        //内存表的字段 对应数据库表的字段   
                        bulkCopy.ColumnMappings.Add("Id", "Id");
                        bulkCopy.ColumnMappings.Add("FactoryCode", "FactoryCode");
                        bulkCopy.ColumnMappings.Add("FactoryName", "FactoryName");
                        bulkCopy.ColumnMappings.Add("WorkOrder", "WorkOrder");
                        bulkCopy.ColumnMappings.Add("MaterialCode", "MaterialCode");
                        bulkCopy.ColumnMappings.Add("MaterialName", "MaterialName");
                        bulkCopy.ColumnMappings.Add("Amount", "Amount");
                        bulkCopy.ColumnMappings.Add("UnitName", "UnitName");
                        bulkCopy.ColumnMappings.Add("Spec", "Spec");
                        bulkCopy.ColumnMappings.Add("SmallClass", "SmallClass");
                        bulkCopy.ColumnMappings.Add("Creator", "Creator");
                        bulkCopy.ColumnMappings.Add("CreateTime", "CreateTime");
                        bulkCopy.ColumnMappings.Add("PurchaseType", "PurchaseType");
                        bulkCopy.ColumnMappings.Add("IsDeleted", "IsDeleted");
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            int n = 0;
            msg = "";
            try
            {
                //删除
                n = this.BaseRepository().Delete(keyValue);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PL_PrdOrderReqMaterialsEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                //entity.IsDeleted = true;
                this.BaseRepository().Update(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }


        #region 删除方法
        /// <summary>
        ///功能描述: 根据Expression删除实体类
        ///创　　建: Dragon
        ///创建日期: 2022-11-08 14:54:29
        ///任务编号: 产品工价维护
        ///</summary>
        ///<param name="condition">删除条件</param>
        ///<returns>PMProductPriceEntity</returns>
        public int RemoveForm(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
        #endregion
        public int Delete(List<PL_PrdOrderReqMaterialsEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"DELETE FROM [dbo].[PL_PrdOrderReqMaterials] WHERE Id=N'{keyValue}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_PrdOrderReqMaterialsEntity</returns>
        public PL_PrdOrderReqMaterialsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_PrdOrderReqMaterialsEntity</returns>
        public PL_PrdOrderReqMaterialsEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_PrdOrderReqMaterialsEntity 列表</returns>
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> Get_ExpressionList(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

        ///// <summary>
        ///// 删除主表数据并同步删除子表数据, 假删除更新删除标记
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <returns></returns>
        //public int RemoveForm(string keyValue)
        //{
        //    int result = 0;
        //    StringBuilder sql = new StringBuilder();
        //    //子表服务类
        //    RepositoryFactory<PL_PrdOrderReqMaterialsEntity> bomService = new RepositoryFactory<PL_PrdOrderReqMaterialsEntity>();

        //    PL_PrdOrderReqMaterialsEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PL_PrdOrderReqMaterialsDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PL_PrdOrderReqMaterials_Id == entity.Id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        //主表删除标记
        //        entity.IsEnabled = false;
        //        this.BaseRepository().Update(entity);
        //        if (bomEntity != null)
        //        {
        //            //子表删除标记
        //            bomEntity.IsEnabled = false;
        //            bomService.BaseRepository().Update(bomEntity);
        //        }
        //        result = 1;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<PL_PrdOrderReqMaterialsEntity> PL_PrdOrderReqMaterialsEntity_list = db2.FindList<PL_PrdOrderReqMaterialsEntity>(sql.ToString());
                return PL_PrdOrderReqMaterialsEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                DataTable PL_PrdOrderReqMaterialsEntity_DataTable = db2.FindTable(sql.ToString());
                return PL_PrdOrderReqMaterialsEntity_DataTable;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
                messageCode = parameters[2].Value.ToString();
                b = true;
            }
            catch (Exception ex)
            {
                messageCode = ex.Message;
            }
            return b;
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:27:57
        /// 任务编号: 订单物料需求表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT    
                      [WorkOrder] as '工单编码'
                      ,[MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[Amount] as '物料需求量'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[PL_PrdOrderReqMaterials] where 1=1  ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and CreatedByCode = '{checkType}' ");
            }
            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());
                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);

                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = "订单物料需求表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("订单物料需求表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        public PL_PrdOrderReqMaterialsEntity Get_ExpressionEntity(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
    }
}
