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
using ALP.Application.IService.MaterialManage;
using ALP.Application.UtilExtend.Offices;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-02
    /// 2.创建作者: liyongguo
    /// 3.功能描述: MM_RawMaterialOutService 业务服务类
    /// 4.任务编号: 原材料出库表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_RawMaterialOut_Service : RepositoryFactory<MM_RawMaterialOutEntity>, MM_RawMaterialOut_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialOutEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[BusinessId]
                      ,[BGType]
                      ,[BGBatchNo]
                      ,[CardCode]
                      ,[ProductOrder]
                      ,[CustomerPO]
                      ,[ContainerNO]
                      ,[ExeWorkOrder]
                      ,[ProcessCode]
                      ,[Spec]
                      ,[CustomerModelName]
                      ,[BGQty]
                      ,[ProductUnit]
                      ,[CustomerModel]
                      ,[DocNum]
                      ,[WhsCode]
                      ,[LocationCode]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[SupplierCode]
                      ,[BatchNo]
                      ,[OutType]
                      ,[Qty]
                      ,[Unit]
                      ,[Remark]
                      ,[AssociateNo]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_RawMaterialOut] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND MR.ProcessCode = N'{queryParam["ProcessCode"]}'");
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
        /// 功能描述:  线边原材料消耗记录-汇总
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableGroupList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT MR.FactoryCode,
                               MR.FactoryName,
                               MR.[ProcessCode],
                               M.ResourceName ProcessName,
                               MR.[MaterialCode],
                               MR.[MaterialName],
                               SUM(MR.[Qty]) [Qty],
                               MR.[Unit],
                               CONVERT(VARCHAR(10), MR.[CreateTime], 23) [CreateTime]
                        FROM [dbo].[MM_RawMaterialOut] MR
                            LEFT JOIN dbo.BS_ModelWithResource M
                                ON M.ResourceCode = MR.ProcessCode
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND MR.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND MR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }

                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND MR.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND MR.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }

                //创建时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND MR.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND MR.CreateTime <= N'{queryParam["EndTime"]}'");
                }


                sql.Append(@" GROUP BY MR.FactoryCode,
                                     MR.FactoryName,
                                     MR.[ProcessCode],
                                     MR.[MaterialCode],
                                     MR.[MaterialName],
                                     MR.[Unit],
                                     M.ResourceName,
                                     CONVERT(VARCHAR(10), MR.[CreateTime], 23) ");
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
        /// 功能描述:  线边原材料消耗记录
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@"SELECT A.Id,
                               A.BusinessId,
                               A.FactoryCode,
                               A.FactoryName,
                               A.BGType,
                               A.BGBatchNo,
                               A.CardCode,
                               A.ProductOrder,
                               A.WorkOrder,
                               A.CustomerPO,
                               A.ContainerNO,
                               A.ExeWorkOrder,
                               A.ProcessCode,
                               A.Spec,
                               A.CustomerModelName,
                               A.BGQty,
                               A.ProductUnit,
                               A.CustomerModel,
                               A.DocNum,
                               A.WhsCode,
                               b.ResourceName WhsName,
                               A.LocationCode,
                               c.ResourceName LocationName,
                               A.MaterialCode,
                               A.MaterialName,
                               A.SmallClass,
							   v2.ItemName SmallClassName,
                               A.SupplierCode,
                               A.BatchNo,
                               A.OutType,
                               A.Qty,
                               A.Unit,
                               A.Remark,
                               A.AssociateNo,
                               A.Creator,
							   A.CreatorName,
                               A.CreateTime,
                               A.ModifyBy,
                               A.ModifyTime,
							   A.WorkShopCode,
							   A.WorkShopName,
							   A.MoveType,
							   v1.ItemName MoveTypeName
                        FROM dbo.MM_RawMaterialOut A
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.ModelLeve = 'Warehouse'
                                   AND A.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'StorageLocation'
                                   AND A.LocationCode = c.ResourceCode
							LEFT JOIN dbo.V_DataDictionary v1 ON v1.EnCode='OutMode'
							       AND A.MoveType=v1.ItemValue
							LEFT JOIN dbo.V_DataDictionary v2 ON v2.EnCode='MaterialSmall'
							       AND A.SmallClass=v2.ItemValue
                        WHERE 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["BusinessId"].IsEmpty())
                {
                    sql.Append($" AND A.BusinessId = N'{queryParam["BusinessId"]}'");
                }
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND A.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }

                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND A.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND A.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (A.MaterialCode like N'%{queryParam["Material"]}%' OR A.MaterialName like N'%{queryParam["Material"]}%') ");
                }
                //规格 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND A.Spec like N'%{queryParam["Spec"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND A.SmallClass='{queryParam["SmallClass"]}'");
                }
                //出库方式 是否为空进行查询
                if (!queryParam["MoveType"].IsEmpty())
                {
                    sql.Append($" AND A.MoveType='{queryParam["MoveType"]}' ");
                }
                //车间编码 是否为空进行查询
                if (!queryParam["WorkShopCode"].IsEmpty())
                {
                    sql.Append($" AND A.WorkShopCode='{queryParam["WorkShopCode"]}' ");
                }
                //内部订单号 是否为空进行查询
                if (!queryParam["WorkShopCode2"].IsEmpty())
                {
                    sql.Append($" AND A.WorkShopCode='{queryParam["WorkShopCode2"]}' ");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND A.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND A.CreateTime <= N'{queryParam["EndTime"]}'");
                }
                //出库类型 是否为空进行查询
                if (!queryParam["OutType"].IsEmpty())
                {
                    sql.Append($" AND A.OutType='{queryParam["OutType"]}' ");
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
                throw ex;
            }
        }


        /// <summary>
        /// 功能描述:  线边原材料消耗记录-明细
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListItem(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append(@" SELECT  MR.[Id]
                              ,MR.[BusinessId]
                              ,MR.[BGType]
                              ,MR.[BGBatchNo]
                              ,MR.[CardCode]
                              ,MR.[ProductOrder]
                              ,MR.[CustomerPO]
                              ,MR.[ContainerNO]
                              ,MR.[ExeWorkOrder]
                              ,MR.[ProcessCode]
                              ,MR.[Spec]
                              ,MR.[CustomerModelName]
                              ,MR.[BGQty]
                              ,MR.[ProductUnit]
                              ,MR.[CustomerModel]
                              ,MR.[DocNum]
                              ,MR.[WhsCode]
                              ,MR.[LocationCode]
                              ,MR.[MaterialCode]
                              ,MR.[MaterialName]
                              ,MR.[SupplierCode],S.Abbr [SupplierName]
                              ,MR.[BatchNo]                          
							  ,v1.ItemName OutType
                              ,MR.[Qty]
                              ,MR.[Unit]
                              ,MR.[Remark]
                              ,MR.[AssociateNo]
                              ,MR.[Creator]
                              ,MR.[CreateTime]
	                          ,M.ResourceName ProcessName,M1.ResourceName WhsName
                          FROM [FHMESDB].[dbo].[MM_RawMaterialOut] MR
                          LEFT JOIN dbo.BS_ModelWithResource M ON  MR.ProcessCode=M.ResourceCode
                          LEFT JOIN dbo.BS_ModelWithResource M1 ON MR.WhsCode=M.ResourceCode
                          LEFT JOIN dbo.Base_SupplierManage S ON S.SupplierCode=MR.SupplierCode
						  left join V_DataDictionary v1 on v1.EnCode='OutboundType' and v1.ItemValue=mr.OutType
				          WHERE 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();

                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND MR.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }

                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND MR.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
 
                //创建时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND MR.CreateTime BETWEEN  N'{queryParam["StartTime"]}' AND N'{Convert.ToDateTime(queryParam["StartTime"]).AddDays(1).ToString("yyyy-MM-dd")}'");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialOutEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[BusinessId]
                      ,[BGType]
                      ,[BGBatchNo]
                      ,[CardCode]
                      ,[ProductOrder]
                      ,[CustomerPO]
                      ,[ContainerNO]
                      ,[ExeWorkOrder]
                      ,[ProcessCode]
                      ,[Spec]
                      ,[CustomerModelName]
                      ,[BGQty]
                      ,[ProductUnit]
                      ,[CustomerModel]
                      ,[DocNum]
                      ,[WhsCode]
                      ,[LocationCode]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[SupplierCode]
                      ,[BatchNo]
                      ,[OutType]
                      ,[Qty]
                      ,[Unit]
                      ,[Remark]
                      ,[AssociateNo]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_RawMaterialOut] where 1=1 ");
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
                 throw ex;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_RawMaterialOutEntity entity, out string msg)
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
                 throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_RawMaterialOutEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialOutEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_RawMaterialOut] set ");
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
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
                 throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_RawMaterialOutEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {

                this.BaseRepository().Delete(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
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
                sql.Append($@"DELETE FROM [dbo].[MM_RawMaterialOut] WHERE Id=N'{keyValue}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                 throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_RawMaterialOutEntity</returns>
        public MM_RawMaterialOutEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_RawMaterialOutEntity</returns>
        public MM_RawMaterialOutEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_RawMaterialOutEntity 对象</returns>
        public MM_RawMaterialOutEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialOutEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_RawMaterialOutEntity 列表</returns>
        public IEnumerable<MM_RawMaterialOutEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialOutEntity, bool>> condition)
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
        //    RepositoryFactory<MM_RawMaterialOutEntity> bomService = new RepositoryFactory<MM_RawMaterialOutEntity>();

        //    MM_RawMaterialOutEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_RawMaterialOutDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_RawMaterialOut_Id == entity.Id).FirstOrDefault();
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
        /// 功能描述: 根据物料编码查询30天内出库的物料批次
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="materialCode">查询条件</param>
        /// <param name="factoryCode"></param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialOutEntity> GetList_TestOtherEntity(string materialCode,string factoryCode, out string msg)
        {
            var time = DateTime.Now;
            var start = time.AddDays(-30);
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT MAX(MO.Id) Id,
                               MO.MaterialCode,
                               MO.MaterialName,
                               MO.BatchNo,
                               MO.SupplierCode,
                               MAX(MO.CreateTime) CreateTime,
                               M.SmallClass
                        FROM dbo.MM_RawMaterialOut MO
                            LEFT JOIN dbo.Base_Material M
                                ON M.MaterialCode = MO.MaterialCode
                        WHERE MO.OutType = '6'
                              AND MO.MaterialCode = '{materialCode}'
                              AND MO.FactoryCode = '{factoryCode}' ");
            sql.Append($@" AND MO.CreateTime BETWEEN '{start.ToString("yyyy-MM-dd")}' AND '{time.ToString("yyyy-MM-dd")} 23:59:59'");
            sql.Append(@" GROUP BY MO.BatchNo,MO.MaterialCode,MO.MaterialName,MO.BatchNo,MO.SupplierCode,M.SmallClass
						    ORDER BY CreateTime desc");
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<MM_RawMaterialOutEntity> MM_RawMaterialOutEntity_list = db2.FindList<MM_RawMaterialOutEntity>(sql.ToString());
                return MM_RawMaterialOutEntity_list;
            }
            catch (Exception ex)
            {
                 throw ex;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  ");
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
                DataTable MM_RawMaterialOutEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_RawMaterialOutEntity_DataTable;
            }
            catch (Exception ex)
            {
                 throw ex;
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
        /// 创建日期: 2021-09-02 13:16:27
        /// 任务编号: 原材料出库表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [BusinessId] as '业务表ID'
                      ,[BGType] as '报工类型'
                      ,[BGBatchNo] as '报工批次'
                      ,[CardCode] as '流转卡号'
                      ,[ProductOrder] as '订单单号'
                      ,[CustomerPO] as '客户PO号'
                      ,[ContainerNO] as '柜号'
                      ,[ExeWorkOrder] as '执行工单号'
                      ,[ProcessCode] as '工序'
                      ,[Spec] as '规格'
                      ,[CustomerModelName] as '客户型号名称'
                      ,[BGQty] as '报工数量'
                      ,[ProductUnit] as '成品单位'
                      ,[CustomerModel] as '客户型号'
                      ,[DocNum] as '出库单号'
                      ,[WhsCode] as '仓库编码'
                      ,[LocationCode] as '库位编码'
                      ,[MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[SupplierCode] as '供应商编码'
                      ,[BatchNo] as '批次号'
                      ,[OutType] as '出库类型'
                      ,[Qty] as '出库数量'
                      ,[Unit] as '单位'
                      ,[Remark] as '备注'
                      ,[AssociateNo] as '关联号'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[MM_RawMaterialOut] where 1=1 ");
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
                string saveFileName = "原材料出库表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("原材料出库表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                 throw ex;
                return null;
            }
        }

        public int RemoveForm(Expression<Func<MM_RawMaterialOutEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
