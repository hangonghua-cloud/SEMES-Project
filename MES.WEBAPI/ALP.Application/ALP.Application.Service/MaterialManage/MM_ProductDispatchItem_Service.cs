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
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using ALP.Application.IService.MaterialManage;
using ALP.Data;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.UtilExtend.Util;
using ALP.Application.Service.Material;
using ALP.Application.Service.SystemManage;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Service.PlanManage;

namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-10-13
    /// 2.创建作者: admin
    /// 3.功能描述: MM_ProductDispatchItemService 业务服务类
    /// 4.任务编号: 成品发货明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_ProductDispatchItem_Service : RepositoryFactory<MM_ProductDispatchItemEntity>, MM_ProductDispatchItem_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ProductDispatchItemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[DocNum],ProductOrder,WorkOrder
                      ,[ContainerNO]
                      ,[CustomerPO]
                      ,[PalletQty]
                      ,[BoxQty]
                      ,[ContainerID]
                      ,[CarNumber]
                      ,[ForkliftWorker]
                      ,[WoodWorker]
                      ,[Remark]
                      ,[AttachId]
                      ,[Operator]
                      ,[Status]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_ProductDispatchItem] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //发货单号 是否为空进行查询
                if (!queryParam["DocNum"].IsEmpty())
                {
                    //sql.Append($" AND DocNum = N'{queryParam["DocNum"]}'");
                    sql.Append($" AND DocNum like N'%{queryParam["DocNum"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    //sql.Append($" AND ContainerNO = N'{queryParam["ContainerNO"]}'");
                    sql.Append($" AND ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //客户PO号 是否为空进行查询
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    //sql.Append($" AND CustomerPO = N'{queryParam["CustomerPO"]}'");
                    sql.Append($" AND CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                //计划发货数量/托 是否为空进行查询
                if (!queryParam["PalletQty"].IsEmpty())
                {
                    //sql.Append($" AND PalletQty = N'{queryParam["PalletQty"]}'");
                    sql.Append($" AND PalletQty like N'%{queryParam["PalletQty"]}%'");
                }
                //计划发货数量/盒 是否为空进行查询
                if (!queryParam["BoxQty"].IsEmpty())
                {
                    //sql.Append($" AND BoxQty = N'{queryParam["BoxQty"]}'");
                    sql.Append($" AND BoxQty like N'%{queryParam["BoxQty"]}%'");
                }
                //集装箱ID 是否为空进行查询
                if (!queryParam["ContainerID"].IsEmpty())
                {
                    //sql.Append($" AND ContainerID = N'{queryParam["ContainerID"]}'");
                    sql.Append($" AND ContainerID like N'%{queryParam["ContainerID"]}%'");
                }
                //车牌号 是否为空进行查询
                if (!queryParam["CarNumber"].IsEmpty())
                {
                    //sql.Append($" AND CarNumber = N'{queryParam["CarNumber"]}'");
                    sql.Append($" AND CarNumber like N'%{queryParam["CarNumber"]}%'");
                }
                //叉车工 是否为空进行查询
                if (!queryParam["ForkliftWorker"].IsEmpty())
                {
                    //sql.Append($" AND ForkliftWorker = N'{queryParam["ForkliftWorker"]}'");
                    sql.Append($" AND ForkliftWorker like N'%{queryParam["ForkliftWorker"]}%'");
                }
                //木工 是否为空进行查询
                if (!queryParam["WoodWorker"].IsEmpty())
                {
                    //sql.Append($" AND WoodWorker = N'{queryParam["WoodWorker"]}'");
                    sql.Append($" AND WoodWorker like N'%{queryParam["WoodWorker"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["AttachId"].IsEmpty())
                {
                    //sql.Append($" AND AttachId = N'{queryParam["AttachId"]}'");
                    sql.Append($" AND AttachId like N'%{queryParam["AttachId"]}%'");
                }
                //发货人 是否为空进行查询
                if (!queryParam["Operator"].IsEmpty())
                {
                    //sql.Append($" AND Operator = N'{queryParam["Operator"]}'");
                    sql.Append($" AND Operator like N'%{queryParam["Operator"]}%'");
                }
                //发货状态 是否为空进行查询
                if (!queryParam["Status"].IsEmpty())
                {
                    //sql.Append($" AND Status = N'{queryParam["Status"]}'");
                    sql.Append($" AND Status like N'%{queryParam["Status"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND ModifyTime like N'%{queryParam["ModifyTime"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT P.[Id],
                               P.FactoryCode,
                               P.FactoryName,
                               P.[DocNum],
                               P.[ProductOrder],
                               P.[WorkOrder],
                               P.[ContainerNO],
                               P.[CustomerPO],
                               P.[DeliveryType],
                               V1.ItemName DeliveryTypeName,
                               P.[Status],
                               V2.ItemName StatusName,
                               P.[PalletQty],
                               P.[BoxQty],
                               P.[InvoiceNO],
                               P.[LoadingBill],
                               P.[DeliveryDate],
                               P.[BoxNum],
                               P.[GrossWeight],
                               P.[Volume],
                               P.[ContainerID],
                               P.[ForkliftWorker],
                               P.[CarNumber],
                               P.[WoodWorker],
                               P.[AttachId],
                               P.[Remark],
                               P.[Operator],
                               P.[Creator],
							   a.Name CreatorName,
                               P.[CreateTime],
                               P.[ModifyBy],
                               P.[ModifyTime],P.SendTime,
							   p.PostDate,
							   p.PostMark,
							   CASE p.PostMark WHEN 'G' THEN '过账' WHEN 'R' THEN '冲销' ELSE '' END PostMarkName,
							   p.IsPosted,
							   CASE p.IsPosted WHEN '1' THEN '已同步' ELSE '' END SAPSync,
							   p.PostedMsg,
							   p.PostedUser,
							   p.PostedTime,
							   p.SAP_VBELN,
							   p.SAP_MBLNR,
							   p.Off_IsPosted,
							   CASE p.Off_IsPosted WHEN '1' THEN '已冲销' ELSE '' END Off_SAPSync,
							   p.Off_PostedMsg,
							   p.Off_PostedUser,
							   p.Off_PostedTime,
							   p.Off_SAP_VBELN,
							   p.Off_SAP_MBLNR,
                               p.Off_PostDate,
							   p.Qty,p.SealingNo
                        FROM [FHMESDB].[dbo].[MM_ProductDispatchItem] P
                            LEFT JOIN dbo.V_DataDictionary V1
                                ON V1.EnCode = 'DeliveryType'
                                   AND P.DeliveryType = V1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary V2
                                ON V2.EnCode = 'DeliveryStatus'
                                   AND P.Status = V2.ItemValue
							LEFT JOIN dbo.BS_People a ON p.Creator=a.Code
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND P.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //发货单号 是否为空进行查询
                if (!queryParam["DocNum"].IsEmpty())
                {
                    sql.Append($" AND P.DocNum like N'%{queryParam["DocNum"]}%'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND P.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND P.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                //客户PO号 是否为空进行查询
                if (!queryParam["CustomerPO"].IsEmpty())
                {
                    sql.Append($" AND P.CustomerPO like N'%{queryParam["CustomerPO"]}%'");
                }
                if (!queryParam["DeliveryType"].IsEmpty())
                {
                    sql.Append($" AND P.DeliveryType = N'{queryParam["DeliveryType"]}'");
                }
                if (!queryParam["Status"].IsEmpty())
                {
                    sql.Append($" AND P.Status = N'{queryParam["Status"]}'");
                }
                //发货日期 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),P.DeliveryDate) >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND CONVERT(VARCHAR(10),P.DeliveryDate) <= N'{queryParam["EndTime"]}'");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ProductDispatchItemEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[DocNum],ProductOrder,WorkOrder
                      ,[ContainerNO]
                      ,[CustomerPO]
                      ,[PalletQty]
                      ,[BoxQty]
                      ,[ContainerID]
                      ,[CarNumber]
                      ,[ForkliftWorker]
                      ,[WoodWorker]
                      ,[Remark]
                      ,[AttachId]
                      ,[Operator]
                      ,[Status]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_ProductDispatchItem] where 1=1 ");
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
        public IEnumerable<MM_ProductDispatchItemEntity> GetList(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_ProductDispatchItemEntity entity, out string msg)
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_ProductDispatchItemEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductDispatchItemEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_ProductDispatchItem] set ");
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
                                        var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                        if (!hasNotMapped)
                                        {
                                            if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                            {
                                                sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                            }
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
                        bulkCopy.DestinationTableName = "[dbo].[MM_ProductDispatchItem]";
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_ProductDispatchItemEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记

                this.BaseRepository().Delete(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }

        public int RemoveForm(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
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
                sql.Append($@"DELETE FROM [dbo].[MM_ProductDispatchItem] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_ProductDispatchItemEntity</returns>
        public MM_ProductDispatchItemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_ProductDispatchItemEntity</returns>
        public MM_ProductDispatchItemEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_ProductDispatchItemEntity 对象</returns>
        public MM_ProductDispatchItemEntity Get_ExpressionEntity(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_ProductDispatchItemEntity 列表</returns>
        public IEnumerable<MM_ProductDispatchItemEntity> Get_ExpressionList(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
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
        //    RepositoryFactory<MM_ProductDispatchItemEntity> bomService = new RepositoryFactory<MM_ProductDispatchItemEntity>();

        //    MM_ProductDispatchItemEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_ProductDispatchItemDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_ProductDispatchItem_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_ProductDispatchItemEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<MM_ProductDispatchItemEntity> MM_ProductDispatchItemEntity_list = db2.FindList<MM_ProductDispatchItemEntity>(sql.ToString());
                return MM_ProductDispatchItemEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
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
                DataTable MM_ProductDispatchItemEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_ProductDispatchItemEntity_DataTable;
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-13 14:01:18
        /// 任务编号: 成品发货明细
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [DocNum] as '发货单号'
                      ,[ContainerNO] as '柜号'
                      ,[CustomerPO] as '客户PO号'
                      ,[PalletQty] as '计划发货数量/托'
                      ,[BoxQty] as '计划发货数量/盒'
                      ,[ContainerID] as '集装箱ID'
                      ,[CarNumber] as '车牌号'
                      ,[ForkliftWorker] as '叉车工'
                      ,[WoodWorker] as '木工'
                      ,[Remark] as '备注'
                      ,[AttachId] as '附件'
                      ,[Operator] as '发货人'
                      ,[Status] as '发货状态'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[MM_ProductDispatchItem] where 1=1 ");
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
                string saveFileName = "成品发货明细_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("成品发货明细", dt, true);
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

        #region SAP发货单信息接口
        /// <summary>
        /// 接收SAP发货单信息
        /// 创建：jpf
        /// 时间：2024-3-11 09:24:21
        /// </summary>
        /// <param name="lstEntity"></param>
        public void SaveSAPMM_ProductDispatchItem(List<SAPMM_ProductDispatchItemEntity> lstEntity)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                //SAP发货单状态转为MES状态
                var deliveryStatusList = new DataItemDetailService().GetDataItemList("DeliveryStatus").ToList();
                var unitList = new DataItemDetailService().GetDataItemList("Unit").ToList();//单位列表

                foreach (var item in lstEntity)
                {
                    var sapMain = item.ProductDispatchItem;
                    var sapDetail = item.DispatchDetails;

                    sapMain.Status = deliveryStatusList.Find(t => t.Description == sapMain.Status)?.ItemValue;
                    //删除时，sap无法传发货单类型，mes通过单号判断是否成品
                    var flag = new MM_ProductDispatchItem_Service().GetList(t => t.DocNum == sapMain.DocNum).Any();
                    if (flag)
                        sapMain.DocType = "10";
                    //成品
                    if (sapMain.DocType == "10")
                    {
                        #region 成品发货单

                        if (string.IsNullOrEmpty(sapMain.DocNum))
                        {
                            throw new Exception("发货单号必须传");
                        }

                        //判断是否有删除标识
                        if (sapMain.IsDeleted == true) //删除
                        {
                            var productDispatchItemEntity = Get_ExpressionEntity(t => t.DocNum == sapMain.DocNum);
                            if (productDispatchItemEntity == null)
                            {
                                throw new Exception(sapMain.DocNum + "发货单不存在不允许删除");
                            }
                            new MM_ProductDispatchDetail_Service().RemoveForm(t => t.DispatchItemId == productDispatchItemEntity.Id);
                            new MM_ProductDispatchItem_Service().RemoveForm(t => t.Id == productDispatchItemEntity.Id);
                        }
                        else //新增、修改
                        {
                            #region 数据校验
                            if (string.IsNullOrEmpty(sapMain.FactoryCode))
                            {
                                throw new Exception("工厂编码必须传");
                            }
                            if (string.IsNullOrEmpty(sapMain.FactoryName))
                            {
                                throw new Exception("工厂名称必须传");
                            }
                            if (string.IsNullOrEmpty(sapMain.DocType))
                            {
                                throw new Exception("发货单类型必须传");
                            }
                            if (string.IsNullOrEmpty(sapMain.ProductOrder))
                            {
                                throw new Exception("订单号必填");
                            }
                            if (string.IsNullOrEmpty(sapMain.ContainerNO))
                            {
                                throw new Exception("柜号必填");
                            }
                            #endregion

                            var productDispatchItemEntity = Get_ExpressionEntity(t => t.FactoryCode == sapMain.FactoryCode && t.DocNum == sapMain.DocNum
                                && t.ProductOrder == sapMain.ProductOrder && t.ContainerNO == sapMain.ContainerNO && t.CustomerPO == sapMain.CustomerPO);
                            if (productDispatchItemEntity == null) //新增
                            {
                                productDispatchItemEntity = Tools.Mapper<MM_ProductDispatchItemEntity>(sapMain);
                                productDispatchItemEntity.Id = Guid.NewGuid().ToString();
                                productDispatchItemEntity.PalletQty = sapDetail.Sum(t => t.PalletQty);
                                productDispatchItemEntity.Qty = sapDetail.Sum(t => t.MaterialQty);

                                db.Insert(productDispatchItemEntity);
                            }
                            else
                            {
                                productDispatchItemEntity.FactoryCode = sapMain.FactoryCode;
                                productDispatchItemEntity.FactoryName = sapMain.FactoryName;
                                productDispatchItemEntity.DocNum = sapMain.DocNum;
                                productDispatchItemEntity.ProductOrder = sapMain.ProductOrder;
                                productDispatchItemEntity.ContainerNO = sapMain.ContainerNO;
                                productDispatchItemEntity.CustomerPO = sapMain.CustomerPO;
                                productDispatchItemEntity.DeliveryType = sapMain.DeliveryType;
                                productDispatchItemEntity.Status = sapMain.Status;
                                productDispatchItemEntity.LoadingBill = sapMain.LoadingBill;
                                productDispatchItemEntity.InvoiceNO = sapMain.InvoiceNO;
                                productDispatchItemEntity.BoxNum = sapMain.BoxNum;
                                productDispatchItemEntity.GrossWeight = sapMain.GrossWeight;
                                productDispatchItemEntity.Volume = sapMain.Volume;
                                productDispatchItemEntity.ModifyTime = DateTime.Now;
                                productDispatchItemEntity.CusdeclarationDate = sapMain.CusdeclarationDate;
                                productDispatchItemEntity.CusdeclarationNum = sapMain.CusdeclarationNum;
                                productDispatchItemEntity.Creator = sapMain.Creator;
                                productDispatchItemEntity.Harbor = sapMain.Harbor;
                                productDispatchItemEntity.PalletQty = sapDetail.Sum(t => t.PalletQty);
                                productDispatchItemEntity.Qty = sapDetail.Sum(t => t.MaterialQty);

                                db.Update(productDispatchItemEntity);
                            }

                            //处理明细数据
                            var arrMaterialCode = sapDetail.Select(t => t.MaterialCode).Distinct().ToArray();
                            var materialList = new Base_Material_Service().Get_ExpressionList(t => arrMaterialCode.Contains(t.SAPMaterialCode)).ToList();

                            var detailList = new List<MM_ProductDispatchDetailEntity>();
                            foreach (var detail in sapDetail)
                            {
                                if (string.IsNullOrEmpty(detail.MaterialCode))
                                {
                                    throw new Exception("客户型号必须传");
                                }
                                if (string.IsNullOrEmpty(detail.ProductLine))
                                {
                                    throw new Exception("订单行号不能为空");
                                }

                                var matItem = materialList.Find(t => t.SAPMaterialCode == detail.MaterialCode);
                                if (matItem == null)
                                    throw new Exception($"SAP物料[{detail.MaterialCode}]不存在");

                                detail.MaterialCode = matItem.MaterialCode;

                                var productDispatchDetailEntity = Tools.Mapper<MM_ProductDispatchDetailEntity>(detail);
                                productDispatchDetailEntity.Create();
                                productDispatchDetailEntity.DispatchItemId = productDispatchItemEntity.Id;
                                productDispatchDetailEntity.FactoryCode = sapMain.FactoryCode;
                                productDispatchDetailEntity.FactoryName = sapMain.FactoryName;
                                productDispatchDetailEntity.DocNum = sapMain.DocNum;
                                productDispatchDetailEntity.ContainerNO = sapMain.ContainerNO;
                                productDispatchDetailEntity.CustomerPO = sapMain.CustomerPO;
                                productDispatchDetailEntity.ProductOrder = sapMain.ProductOrder;
                                productDispatchDetailEntity.CreateTime = DateTime.Now;
                                productDispatchDetailEntity.PalletQty = detail.PalletQty;
                                productDispatchDetailEntity.PieceQty = detail.MaterialQty;

                                var workOrderEntity = new PL_WorkOrder_Service().Get_ExpressionEntity(t => t.ProductOrder == sapMain.ProductOrder
                                      && t.Orderline == detail.ProductLine);
                                if (workOrderEntity == null)
                                    throw new Exception($"订单号[{sapMain.ProductOrder}]行号[{detail.ProductLine}]对应的工单不存在");

                                productDispatchDetailEntity.WorkOrder = workOrderEntity.WorkOrder;

                                detailList.Add(productDispatchDetailEntity);
                            }
                            string msg = "";
                            new MM_ProductDispatchDetail_Service().RemoveForm(t => t.DispatchItemId == productDispatchItemEntity.Id);
                            new MM_ProductDispatchDetail_Service().SaveEntity_List(false, "", detailList, out msg);
                        }

                        #endregion
                    }
                    //原材料、半成品发货
                    else
                    {
                        #region 原材料、半成品
                        if (string.IsNullOrEmpty(sapMain.DocNum))
                        {
                            throw new Exception("发货单号必须传");
                        }

                        if (sapMain.IsDeleted == true) //删除
                        {
                            //判断一下发货单是否存在
                            var rawMaterialDispatchEntity = new MMRawMaterialDispatchService().GetEntity(t => t.DeliveryNo == sapMain.DocNum);
                            if (rawMaterialDispatchEntity == null)
                            {
                                throw new Exception("发货单不存在，不允许删除");
                            }
                            //删除
                            new MMRawMaterialDispatchSubService().RemoveForm(t => t.DispatchId == rawMaterialDispatchEntity.Id);
                            new MMRawMaterialDispatchService().RemoveForm(t => t.Id == rawMaterialDispatchEntity.Id);
                        }
                        else //新增、修改
                        {
                            var rawMaterialDispatchEntity = new MMRawMaterialDispatchService().GetEntity(t => t.FactoryCode == sapMain.FactoryCode && t.DeliveryNo == sapMain.DocNum);
                            if (rawMaterialDispatchEntity == null)
                            {
                                rawMaterialDispatchEntity = new MMRawMaterialDispatchEntity();
                                rawMaterialDispatchEntity.FactoryCode = sapMain.FactoryCode;
                                rawMaterialDispatchEntity.FactoryName = sapMain.FactoryName;
                                rawMaterialDispatchEntity.DeliveryNo = sapMain.DocNum;
                                rawMaterialDispatchEntity.GrossWeight = sapMain.GrossWeight;
                                rawMaterialDispatchEntity.Volume = sapMain.Volume;
                                rawMaterialDispatchEntity.InvoiceNO = sapMain.InvoiceNO;
                                rawMaterialDispatchEntity.LoadingBill = sapMain.LoadingBill;
                                rawMaterialDispatchEntity.Status = sapMain.Status;
                                rawMaterialDispatchEntity.DeliveryDate = sapMain.DeliveryDate;
                                rawMaterialDispatchEntity.CusdeclarationDate = sapMain.CusdeclarationDate;
                                rawMaterialDispatchEntity.CusdeclarationNum = sapMain.CusdeclarationNum;
                                rawMaterialDispatchEntity.CreateByCode = sapMain.Creator;
                                rawMaterialDispatchEntity.Harbor = sapMain.Harbor;
                                rawMaterialDispatchEntity.Create();
                                rawMaterialDispatchEntity.CreateTime = DateTime.Now;
                                rawMaterialDispatchEntity.IsDeleted = false;
                                db.Insert(rawMaterialDispatchEntity);
                            }
                            else
                            {
                                //韩总要求去掉限制  dragon  2025-05-08
                                //if (rawMaterialDispatchEntity.Status == "2" || rawMaterialDispatchEntity.Status == "3")
                                //{
                                //    throw new Exception("已发货，不可以再同步");
                                //}

                                rawMaterialDispatchEntity.FactoryCode = sapMain.FactoryCode;
                                rawMaterialDispatchEntity.FactoryName = sapMain.FactoryName;
                                rawMaterialDispatchEntity.DeliveryNo = sapMain.DocNum;
                                rawMaterialDispatchEntity.GrossWeight = sapMain.GrossWeight;
                                rawMaterialDispatchEntity.Volume = sapMain.Volume;
                                rawMaterialDispatchEntity.InvoiceNO = sapMain.InvoiceNO;
                                rawMaterialDispatchEntity.LoadingBill = sapMain.LoadingBill;
                                rawMaterialDispatchEntity.Status = sapMain.Status;
                                rawMaterialDispatchEntity.DeliveryDate = sapMain.DeliveryDate;
                                rawMaterialDispatchEntity.ModifyTime = DateTime.Now;
                                rawMaterialDispatchEntity.CusdeclarationDate = sapMain.CusdeclarationDate;
                                rawMaterialDispatchEntity.CusdeclarationNum = sapMain.CusdeclarationNum;
                                rawMaterialDispatchEntity.CreateByCode = sapMain.Creator;
                                rawMaterialDispatchEntity.Harbor = sapMain.Harbor;

                                db.Update(rawMaterialDispatchEntity);
                            }

                            //处理明细数据
                            var arrMaterialCode = sapDetail.Select(t => t.MaterialCode).Distinct().ToArray();
                            var materialList = new Base_Material_Service().Get_ExpressionList(t => arrMaterialCode.Contains(t.SAPMaterialCode)).ToList();

                            var detailList = new List<MMRawMaterialDispatchSubEntity>();
                            foreach (var detail in sapDetail)
                            {
                                if (string.IsNullOrEmpty(detail.MaterialCode))
                                {
                                    throw new Exception("物料号必须传");
                                }
                                if (string.IsNullOrEmpty(detail.LineNum))
                                {
                                    throw new Exception("行号必须传");
                                }
                                //将sap物料编码转化为MES物料编码
                                var matItem = materialList.Find(t => t.SAPMaterialCode == detail.MaterialCode);
                                if (matItem == null)
                                    throw new Exception($"SAP物料[{detail.MaterialCode}]不存在");

                                detail.MaterialCode = matItem.MaterialCode;

                                //判断明细数据是否存在
                                var rawMaterialDispatchSubEntity = new MMRawMaterialDispatchSubEntity();
                                rawMaterialDispatchSubEntity.Create();
                                rawMaterialDispatchSubEntity.DispatchId = rawMaterialDispatchEntity.Id;
                                rawMaterialDispatchSubEntity.MaterialCode = detail.MaterialCode;
                                rawMaterialDispatchSubEntity.MaterialName = matItem.MaterialName;
                                rawMaterialDispatchSubEntity.Qty = detail.MaterialQty;
                                rawMaterialDispatchSubEntity.Unit = detail.Unit;
                                rawMaterialDispatchSubEntity.UnitName = unitList.Find(t => t.ItemValue == detail.Unit)?.ItemName;
                                rawMaterialDispatchSubEntity.DeliveryNo = sapMain.DocNum;
                                rawMaterialDispatchSubEntity.DetailGrossWeight = detail.DetailGrossWeight;
                                rawMaterialDispatchSubEntity.DetailVolume = detail.DetailVolume;
                                rawMaterialDispatchSubEntity.Spec = matItem.Spec;
                                rawMaterialDispatchSubEntity.CreateTime = DateTime.Now;
                                rawMaterialDispatchSubEntity.LineNum = detail.LineNum;
                                rawMaterialDispatchSubEntity.IsDeleted = false;
                                rawMaterialDispatchSubEntity.CreateByCode = sapMain.Creator;
                                rawMaterialDispatchSubEntity.ProductOrder = string.IsNullOrEmpty(detail.ProductOrder) ? sapMain.ProductOrder : detail.ProductOrder;
                                rawMaterialDispatchSubEntity.ProductLine = detail.ProductLine;
                                detailList.Add(rawMaterialDispatchSubEntity);
                            }
                            new MMRawMaterialDispatchSubService().RemoveForm(t => t.DispatchId == rawMaterialDispatchEntity.Id);
                            new MMRawMaterialDispatchSubService().SaveEntity_List(false, detailList);
                        }

                        #endregion
                    }
                }

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                throw ex;
            }
            finally
            {
                db.Close();
            }
        }

        #endregion
    }
}
