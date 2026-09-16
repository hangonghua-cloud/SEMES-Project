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
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using ALP.Application.IService.ProduceManage;
using System.Dynamic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: admin
    /// 3.功能描述: PM_TranferCardBGRecordService 业务服务类
    /// 4.任务编号: 报工信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_TranferCardBGRecord_Service : RepositoryFactory<PM_TranferCardBGRecordEntity>, PM_TranferCardBGRecord_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TranferCardBGRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[CardCode]
                      ,[ProcessCode]
                      ,[MachineCode]
                      ,[Qty],Unit
                      ,[BadQty]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                      ,[BGUser]
					  ,ReworkDId
					  ,IsRework
                  FROM [dbo].[PM_TranferCardBGRecord] where IsEnabled = 1 ");
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

                //流转卡编码 是否为空进行查询
                if (!queryParam["CardCode"].IsEmpty())
                {
                    //sql.Append($" AND CardCode = N'{queryParam["CardCode"]}'");
                    sql.Append($" AND CardCode like N'%{queryParam["CardCode"]}%'");
                }
                //报工工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //报工机台编码 是否为空进行查询
                if (!queryParam["MachineCode"].IsEmpty())
                {
                    //sql.Append($" AND MachineCode = N'{queryParam["MachineCode"]}'");
                    sql.Append($" AND MachineCode like N'%{queryParam["MachineCode"]}%'");
                }
                //报工数量 是否为空进行查询
                if (!queryParam["Qty"].IsEmpty())
                {
                    //sql.Append($" AND Qty = N'{queryParam["Qty"]}'");
                    sql.Append($" AND Qty like N'%{queryParam["Qty"]}%'");
                }
                //不良数量 是否为空进行查询
                if (!queryParam["BadQty"].IsEmpty())
                {
                    //sql.Append($" AND BadQty = N'{queryParam["BadQty"]}'");
                    sql.Append($" AND BadQty like N'%{queryParam["BadQty"]}%'");
                }
                //创建人编码 是否为空进行查询
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
                //修改人编码 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND ModifyTime like N'%{queryParam["ModifyTime"]}%'");
                }
                //有效标记 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
                }
                //报工人名称 是否为空进行查询
                if (!queryParam["BGUser"].IsEmpty())
                {
                    //sql.Append($" AND BGUser = N'{queryParam["BGUser"]}'");
                    sql.Append($" AND BGUser like N'%{queryParam["BGUser"]}%'");
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            //      sql.Append(@"
            //SELECT a.Id,
            //                         c.FactoryCode,
            //                         d.ResourceName FactoryName,
            //                         b.ProductOrder,
            //                         b.ContainerNO,
            //                         b.MaterialCode,
            //                         b.WorkOrderType,
            //                         v1.ItemName WorkOrderTypeName,
            //                         mm.SmallClass,
            //                         b.MMXH,
            //                         mm.MMCJ,
            //                         b.Spec,
            //                         b.BWXH,
            //                         b.UV,
            //                         b.KCKX,
            //                         b.CardName,
            //                         a.CardCode,
            //                         b.CardType,
            //                         v2.ItemName CardTypeName,
            //                         a.ProcessCode,
            //                         e.ResourceName ProcessName,
            //                         a.MachineCode,e1.ResourceName MachineName,
            //                         a.Qty,a.Unit,
            //                         a.BadQty,
            //                         a.CreateTime,
            //                         a.BGUser,
            //                         a.ReworkDId,
            //                         a.IsRework
            //                  FROM dbo.PM_TranferCardBGRecord a
            //                      INNER JOIN dbo.PM_TransferCard b
            //                          ON a.CardCode = b.CardCode
            //                      INNER JOIN dbo.PL_WorkOrder c
            //                          ON b.WorkOrder = c.WorkOrder
            //                      LEFT JOIN dbo.BS_ModelWithResource d
            //                          ON d.ModelLeve = 'Factory'
            //                             AND c.FactoryCode = d.ResourceCode
            //                      LEFT JOIN dbo.V_DataDictionary v1
            //                          ON v1.EnCode = 'WorkOrderType'
            //                             AND b.WorkOrderType = v1.ItemValue
            //                      LEFT JOIN dbo.fn_GetMaterialAttrs() mm
            //                          ON b.MaterialCode = mm.MaterialCode
            //                             AND b.WorkOrder = mm.WorkOrder
            //                      LEFT JOIN dbo.V_DataDictionary v2
            //                          ON v2.EnCode = 'CirculationCardType'
            //                             AND b.CardType = v2.ItemValue
            //                      LEFT JOIN dbo.BS_ModelWithResource e
            //                          ON e.ModelLeve = 'Process'
            //                             AND a.ProcessCode = e.ResourceCode
            //		   LEFT JOIN dbo.BS_ModelWithResource e1
            //                          ON e1.ModelLeve = 'Machine'
            //                             AND a.MachineCode = e1.ResourceCode
            //                  WHERE a.IsEnabled = 1 ");

            sql.Append(@"SELECT
                                   c.FactoryCode,
                                   c.FactoryName,
                                   c.ProductOrder,c.WorkOrder,
                                   c.ContainerNO,
                                   c.MaterialCode,
                                   c.WorkOrderType,
                                   c.CreateTime,
                                   v1.ItemName WorkOrderTypeName,
                                   mm.SmallClass,
                                   mm.MMXH,
                                   mm.MMCJ,
                                   mm.Spec,
                                   mm.BWXH,
                                   mm.UV,
                                   mm.KCKX

                            FROM  dbo.PL_WorkOrder c
                                LEFT JOIN dbo.V_DataDictionary v1
                                    ON v1.EnCode = 'WorkOrderType'
                                       AND c.WorkOrderType = v1.ItemValue
                                LEFT JOIN dbo.fn_GetMaterialAttrs() mm
                                    ON c.MaterialCode = mm.MaterialCode
                                       AND c.WorkOrder = mm.WorkOrder
                            WHERE  EXISTS(
			                            SELECT 1 FROM dbo.PL_ExeWorkOrder pe
			                            INNER JOIN dbo.PM_TranferCardBGRecord b ON b.ExeWorkOrder = pe.ExeWorkOrder
			                            WHERE pe.WorkOrder=c.WorkOrder
		                               ) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND c.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }

                //工单类型 是否为空进行查询
                if (!queryParam["WorkOrderType"].IsEmpty())
                {
                    sql.Append($" AND c.WorkOrderType = N'{queryParam["WorkOrderType"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND c.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND c.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND mm.MMXH like N'%{queryParam["MMXH"]}%'");
                }

                //开始时间 是否为空进行查询
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                //结束时间 是否为空进行查询
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime <= N'{queryParam["EndTime"]}'");
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TranferCardBGRecordEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[CardCode]
                      ,[ProcessCode]
                      ,[MachineCode]
                      ,[Qty],Unit
                      ,[BadQty]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[IsEnabled]
                      ,[BGUser]
                      ,ReworkDId
					  ,IsRework
                  FROM [dbo].[PM_TranferCardBGRecord] where IsEnabled = 1 ");
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

        public IEnumerable<PM_TranferCardBGRecordEntity> GetList(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_TranferCardBGRecordEntity entity, out string msg)
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

        public int Insert(List<PM_TranferCardBGRecordEntity> lstEntity)
        {
            return this.BaseRepository().Insert(lstEntity);
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_TranferCardBGRecordEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TranferCardBGRecordEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_TranferCardBGRecord] set ");
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
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[PM_TranferCardBGRecord] (
                                            [Id]
                                            ,[CardCode]
                                            ,[ProcessCode]
                                            ,[MachineCode]
                                            ,[Qty],Unit
                                            ,[BadQty]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                            ,[IsEnabled]
                                            ,[BGUser]
					  ,ReworkDId
					  ,IsRework,DXZH
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.CardCode}'
                                ,N'{Save_obj.ProcessCode}'
                                ,N'{Save_obj.MachineCode}'
                                ,{Save_obj.Qty},{Save_obj.Unit}
                                ,{Save_obj.BadQty}
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null ? DateTime.Now : Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null ? DateTime.Now : Save_obj.ModifyTime)}'
                                ,'{(Save_obj.IsEnabled == true ? 1 : 0)}'
                                ,N'{Save_obj.BGUser}'
                                ,N'{Save_obj.ReworkDId}'
                                ,N'{Save_obj.IsRework}'
                                ,N'{Save_obj.DXZH}'
                            ),");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_TranferCardBGRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.IsEnabled = false;
                this.BaseRepository().Update(entity);
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
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
                sql.Append($@"DELETE FROM [dbo].[PM_TranferCardBGRecord] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_TranferCardBGRecordEntity</returns>
        public PM_TranferCardBGRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_TranferCardBGRecordEntity</returns>
        public PM_TranferCardBGRecordEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TranferCardBGRecordEntity 对象</returns>
        public PM_TranferCardBGRecordEntity Get_ExpressionEntity(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TranferCardBGRecordEntity 列表</returns>
        public IEnumerable<PM_TranferCardBGRecordEntity> Get_ExpressionList(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
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
        //    RepositoryFactory<PM_TranferCardBGRecordEntity> bomService = new RepositoryFactory<PM_TranferCardBGRecordEntity>();

        //    PM_TranferCardBGRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_TranferCardBGRecordDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_TranferCardBGRecord_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_TranferCardBGRecordEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_TranferCardBGRecordEntity> PM_TranferCardBGRecordEntity_list = db2.FindList<PM_TranferCardBGRecordEntity>(sql.ToString());
                return PM_TranferCardBGRecordEntity_list;
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
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
                DataTable PM_TranferCardBGRecordEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_TranferCardBGRecordEntity_DataTable;
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
        /// 创建日期: 2021-08-18 09:22:52
        /// 任务编号: 报工信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      ,[CardCode] as '流转卡编码'
                      ,[ProcessCode] as '报工工序编码'
                      ,[MachineCode] as '报工机台编码'
                      ,[Qty] as '报工数量'
                      ,[BadQty] as '不良数量'
                      ,[Creator] as '创建人编码'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人编码'
                      ,[ModifyTime] as '修改时间'
                      ,[IsEnabled] as '有效标记'
                      ,[BGUser] as '报工人名称'
                  FROM [dbo].[PM_TranferCardBGRecord] where IsDeleted = 0 ");
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
                string saveFileName = "报工信息_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("报工信息", dt, true);
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

        #region PDA接口
        /// <summary>
        /// 养生时间
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public string GetHealthTime(string serialNumber, string processCode)
        {
            string healthTime = "";

            string sql = $@"SELECT TOP 1
                           CONVERT(VARCHAR(100), a.HealthTime, 21) HealthTime
                    FROM dbo.PM_TranferCardBGRecord a
                    WHERE a.IsEnabled = 1
					      AND a.IsRework='0'
                          AND a.CardCode IN
                              (
                                  SELECT CardCode
                                  FROM dbo.PM_TransferCard
                                  WHERE SerialNumber = '{serialNumber}'
                              )
                          AND a.ProcessCode = '{processCode}'
                    ORDER BY a.CreateTime ";
            var dt = this.BaseRepository().FindTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                healthTime = dt.Rows[0]["HealthTime"].ToString();
            }
            return healthTime;
        }
        /// <summary>
        /// 养生时间
        /// </summary>
        /// <param name="exeWorkOrder"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public string GetHealthTime2(string exeWorkOrder, string processCode)
        {
            string healthTime = "";

            string sql = $@"SELECT TOP 1
                           CONVERT(VARCHAR(100), a.HealthTime, 21) HealthTime
                    FROM dbo.PM_TranferCardBGRecord a
                    WHERE a.IsEnabled = 1
					      AND a.IsRework='0'
                          AND a.CardCode IN
                              (
                                  SELECT CardCode
                                  FROM dbo.PM_TransferCard
                                  WHERE ExeWorkOrder = '{exeWorkOrder}'
                              )
                          AND a.ProcessCode = '{processCode}'
                    ORDER BY a.CreateTime ";
            var dt = this.BaseRepository().FindTable(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                healthTime = dt.Rows[0]["HealthTime"].ToString();
            }
            return healthTime;
        }

        /// <summary>
        /// 获取用户报工记录
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetUserBGRecord(string userCode, string startTime, string endTime)
        {

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@UserCode", SqlDbType.VarChar,50),
                new SqlParameter("@StartDate", SqlDbType.VarChar,50),
                new SqlParameter("@EndDate", SqlDbType.VarChar,50),
            };
            parameters[0].Value = userCode;
            parameters[1].Value = startTime;
            parameters[2].Value = endTime;
            //parameters[1].Direction = ParameterDirection.Output;
            //parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                var dt = db2.ExecuteProc_Table("P_GetUserBGRecord", parameters);
                //var modelList = new List<dynamic>();
                //foreach (DataRow row in dt.Rows)
                //{
                //    dynamic model = new ExpandoObject();
                //    var dict = (IDictionary<string, object>)model;
                //    foreach (DataColumn column in dt.Columns)
                //    {
                //        dict[column.ColumnName] = row[column];
                //    }
                //    modelList.Add(model);
                //}

                //return modelList;
                return dt;
            }
            catch (SqlException ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
        #endregion

    }
}
