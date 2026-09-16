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

namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-08
    /// 2.创建作者: admin
    /// 3.功能描述: MM_SuperProductStockService 业务服务类
    /// 4.任务编号: 超产品库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_SuperProductStock_Service : RepositoryFactory<MM_SuperProductStockEntity>, MM_SuperProductStock_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_SuperProductStockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[MaterialCode]
                      ,[Spec]
                      ,[MMXH]
                      ,[BatchNo]
                      ,[WhsCode]
                      ,[StockQty]
                      ,[LockedQty]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_SuperProductStock] where IsDeleted = 0 ");
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
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    //sql.Append($" AND ProcessCode = N'{queryParam["ProcessCode"]}'");
                    sql.Append($" AND ProcessCode like N'%{queryParam["ProcessCode"]}%'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Spec like N'%{queryParam["Spec"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND MMXH = N'{queryParam["MMXH"]}'");
                    sql.Append($" AND MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //批次号 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //库存数量(片) 是否为空进行查询
                if (!queryParam["StockQty"].IsEmpty())
                {
                    //sql.Append($" AND StockQty = N'{queryParam["StockQty"]}'");
                    sql.Append($" AND StockQty like N'%{queryParam["StockQty"]}%'");
                }
                //锁定数量/片 是否为空进行查询
                if (!queryParam["LockedQty"].IsEmpty())
                {
                    //sql.Append($" AND LockedQty = N'{queryParam["LockedQty"]}'");
                    sql.Append($" AND LockedQty like N'%{queryParam["LockedQty"]}%'");
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.FactoryCode,
                               a.FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.MaterialCode,
                               a.MaterialName,
                               a.Spec,
                               a.DXZH,
                               a.MMXH,
                               a.BatchNo,
							   a.WhsCode,
							   a.WhsName,
							   a.LocationCode,
							   a.LocationName,
                               a.StockQty,
                               ISNULL(a.LockedQty, 0) LockedQty,
                               a.StockQty - ISNULL(a.LockedQty, 0) NoLockQty,
                               CEILING(a.StockQty / a.DXZH) SheetStockQty,
                               CEILING(ISNULL(a.LockedQty, 0) / a.DXZH) SheetLockedQty,
                               CEILING((a.StockQty - ISNULL(a.LockedQty, 0)) / a.DXZH) SheetNoLockQty
                        FROM dbo.MM_SuperProductStock a
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'Process'
                                   AND a.ProcessCode = c.ResourceCode
                        WHERE a.StockQty > 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND a.Id like N'%{queryParam["Id"]}%'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND a.ProcessCode = N'{queryParam["ProcessCode"]}'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    //sql.Append($" AND Spec = N'{queryParam["Spec"]}'");
                    sql.Append($" AND Spec like N'%{queryParam["Spec"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    //sql.Append($" AND MMXH = N'{queryParam["MMXH"]}'");
                    sql.Append($" AND MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //批次号 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //库存数量(片) 是否为空进行查询
                if (!queryParam["StockQty"].IsEmpty())
                {
                    //sql.Append($" AND StockQty = N'{queryParam["StockQty"]}'");
                    sql.Append($" AND StockQty like N'%{queryParam["StockQty"]}%'");
                }
                //锁定数量/片 是否为空进行查询
                if (!queryParam["LockedQty"].IsEmpty())
                {
                    //sql.Append($" AND LockedQty = N'{queryParam["LockedQty"]}'");
                    sql.Append($" AND LockedQty like N'%{queryParam["LockedQty"]}%'");
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_SuperProductStockEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[ProcessCode]
                      ,[MaterialCode]
                      ,[Spec]
                      ,[MMXH]
                      ,[BatchNo]
                      ,[WhsCode]
                      ,[StockQty]
                      ,[LockedQty]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[MM_SuperProductStock] where IsDeleted = 0 ");
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
        public IEnumerable<MM_SuperProductStockEntity> GetList(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_SuperProductStockEntity entity, out string msg)
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
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_SuperProductStockEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_SuperProductStockEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_SuperProductStock] set ");
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
                    //StringBuilder sql = new StringBuilder();
                    //sql.Append($@"INSERT INTO [dbo].[MM_SuperProductStock] (
                    //                        [Id]
                    //                        ,[FactoryCode]
                    //                        ,[ProcessCode]
                    //                        ,[MaterialCode]
                    //                        ,[Spec]
                    //                        ,[MMXH]
                    //                        ,[BatchNo]
                    //                        ,[WhsCode]
                    //                        ,[StockQty]
                    //                        ,[LockedQty]
                    //                        ,[Creator]
                    //                        ,[CreateTime]
                    //                        ,[ModifyBy]
                    //                        ,[ModifyTime]
                    //                ) VALUES ");
                    //if (entity_list.Count > 0)
                    //{
                    //    foreach (var Save_obj in entity_list)
                    //    {
                    //        sql.Append($@"(
                    //            N'{Save_obj.Id}'
                    //            ,N'{Save_obj.FactoryCode}'
                    //            ,N'{Save_obj.ProcessCode}'
                    //            ,N'{Save_obj.MaterialCode}'
                    //            ,N'{Save_obj.Spec}'
                    //            ,N'{Save_obj.MMXH}'
                    //            ,N'{Save_obj.BatchNo}'
                    //            ,N'{Save_obj.WhsCode}'
                    //            ,{Save_obj.StockQty}
                    //            ,{Save_obj.LockedQty}
                    //            ,N'{Save_obj.Creator}'
                    //            ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                    //            ,N'{Save_obj.ModifyBy}'
                    //            ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
                    //        ),");
                    //    }
                    //}
                    ////批量执行更新语句
                    //n = this.BaseRepository().ExecuteBySql(sql.ToString().TrimEnd(','));
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
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_SuperProductStockEntity entity = this.BaseRepository().FindEntity(keyValue);
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
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
                sql.Append($@"DELETE FROM [dbo].[MM_SuperProductStock] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_SuperProductStockEntity</returns>
        public MM_SuperProductStockEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_SuperProductStockEntity</returns>
        public MM_SuperProductStockEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_SuperProductStockEntity 对象</returns>
        public MM_SuperProductStockEntity Get_ExpressionEntity(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_SuperProductStockEntity 列表</returns>
        public IEnumerable<MM_SuperProductStockEntity> Get_ExpressionList(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
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
        //    RepositoryFactory<MM_SuperProductStockEntity> bomService = new RepositoryFactory<MM_SuperProductStockEntity>();

        //    MM_SuperProductStockEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_SuperProductStockDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_SuperProductStock_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_SuperProductStockEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<MM_SuperProductStockEntity> MM_SuperProductStockEntity_list = db2.FindList<MM_SuperProductStockEntity>(sql.ToString());
                return MM_SuperProductStockEntity_list;
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
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
                DataTable MM_SuperProductStockEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_SuperProductStockEntity_DataTable;
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
        /// 创建日期: 2021-09-08 14:51:03
        /// 任务编号: 超产品库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FactoryCode] as '工厂编码'
                      ,[ProcessCode] as '工序编码'
                      ,[MaterialCode] as '客户型号'
                      ,[Spec] as '规格型号'
                      ,[MMXH] as '面膜型号'
                      ,[BatchNo] as '批次号'
                      ,[WhsCode] as '仓库编码'
                      ,[StockQty] as '库存数量(片)'
                      ,[LockedQty] as '锁定数量/片'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人'
                      ,[ModifyTime] as '修改时间'
                  FROM [dbo].[MM_SuperProductStock] where IsDeleted = 0 ");
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
                string saveFileName = "超产品库存_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("超产品库存", dt, true);
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

        /// <summary>
        /// 功能描述: 库存汇总查询(DataTable)
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableMList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.FactoryCode,
                               a.FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.MaterialCode,
                               a.MaterialName,
                               a.Spec,
                               a.MMXH,
                               SUM(a.StockQty) StockQty,
                               ISNULL(SUM(a.LockedQty), 0) LockedQty,
                               SUM(a.StockQty) - ISNULL(SUM(a.LockedQty), 0) NoLockQty
                        FROM dbo.MM_SuperProductStock a
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'Process'
                                   AND a.ProcessCode = c.ResourceCode 
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = '{queryParam["FactoryCode"]}' ");
                }
                //工序编码 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND a.ProcessCode = '{queryParam["ProcessCode"]}' ");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode LIKE '%{queryParam["MaterialCode"]}%' ");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND a.MMXH LIKE '%{queryParam["MMXH"]}%' ");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND a.Spec LIKE '%{queryParam["Spec"]}%' ");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    sql.Append($" AND a.WhsCode LIKE '%{queryParam["WhsCode"]}%' ");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
            }
            sql.Append(@"GROUP BY a.FactoryCode,
                                 a.FactoryName,
                                 a.ProcessCode,
                                 c.ResourceName,
                                 a.MaterialCode,
                                 a.MaterialName,
                                 a.Spec,
                                 a.MMXH ");
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
        /// 库存详情查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableDList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,a.FactoryCode,
                               a.FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.MaterialCode,
                               a.MaterialName,
                               a.Spec,
                               a.MMXH,
                               a.BatchNo,
                               a.StockQty StockQty,
                               a.LockedQty LockedQty,
                               a.StockQty - ISNULL(a.LockedQty, 0) NoLockQty
                        FROM dbo.MM_SuperProductStock a
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'Process'
                                   AND a.ProcessCode = c.ResourceCode
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //仓库编码 是否为空进行查询
                //if (!queryParam["WhsCode"].IsEmpty())
                //{
                //    sql.Append($" AND a.WhsCode LIKE '%{queryParam["WhsCode"]}%' ");
                //}
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //批次 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    sql.Append($" AND a.BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //工序 是否为空进行查询
                if (!queryParam["ProcessCode"].IsEmpty())
                {
                    sql.Append($" AND a.ProcessCode = N'{queryParam["ProcessCode"]}'");
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
        ///  获取执行工单信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable GetExeWorkOrderList(Dictionary<string, object> dic)
        {
            DataTable dt = null;

            if (!dic.ContainsKey("MaterialCode"))
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT b.ProductOrder,
                                       b.ContainerNO,
                                       a.WorkOrder,
                                       a.ExeWorkOrder,
									   b.OrderPieces
                                FROM dbo.PL_ExeWorkOrder a
                                    INNER JOIN dbo.PL_WorkOrder b
                                        ON a.WorkOrder = b.WorkOrder
                                WHERE b.POStatus IN ( '1', '2' )  AND a.IsEnabled=1  ");
            sql.Append($@" AND b.MaterialCode='{dic["MaterialCode"]}' ");
            //订单号
            if (dic.ContainsKey("ProductOrder"))
            {
                sql.Append($" AND b.ProductOrder LIKE '%{dic["ProductOrder"]}%' ");
            }
            //批次号
            if (dic.ContainsKey("BatchNo"))
            {
                sql.Append($" AND a.BatchNo='{dic["BatchNo"]}' ");
            }
            //执行工单类型
            if (dic.ContainsKey("ExeWorkOrderType"))
            {
                sql.Append($" AND a.OrderType='{dic["ExeWorkOrderType"]}' ");
            }
            //起始工序
            if (dic.ContainsKey("ProcessCode"))
            {
                sql.Append($" AND a.StartOperation='{dic["ProcessCode"]}' ");
            }

            dt = this.BaseRepository().FindTable(sql.ToString());

            return dt;
        }

        /// <summary>
        /// 待转超产品明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetDZCCPPageDataTableMList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT b.FactoryCode,
                               d.ResourceName FactoryName,
                               a.MaterialCode,
                               a.MaterialName,
                               dbo.fn_GetMaterialAttr(a.MaterialCode,b.FactoryCode, 'MMXH') MMXH,
                               a.Spec,
                               c.AttrValue DXZH
                        FROM dbo.Base_Material a
                            INNER JOIN dbo.Base_MaterialFactory b
                                ON a.MaterialCode = b.MaterialCode
                            INNER JOIN dbo.Base_MaterialFacet c
                                ON b.Id = c.MaterialFactoryId
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON d.ModelLeve = 'Factory'
                                   AND b.FactoryCode = d.ResourceCode
                        WHERE c.AttrCode = 'DXZH'
                              AND a.MaterialClass = 'CHPN' ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //订单号 是否为空进行查询
                //if (!queryParam["ProductOrder"].IsEmpty())
                //{
                //    sql.Append($" AND b.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                //}
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND b.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialName like N'%{queryParam["MMXH"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND a.Spec like N'%{queryParam["Spec"]}%'");
                }
            }
            //    sql.Append(@"GROUP BY a.ProcessCode,
            //                     d.ResourceName,
            //                     b.ProductOrder,
            //                     b.ContainerNO,
            //                     b.MaterialCode,
            //                     b.MMXH,
            //                     b.Spec,
            //e.AttrValue ");
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
        /// 待转超产品明细
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetDZCCPList(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"IF OBJECT_ID('tempdb..#temp_table') IS NOT NULL
                            DROP TABLE #temp_table;

                        SELECT a.WorkOrder,
                               SUM(a.Qty) WorkOrderInQty
                        INTO #temp_table
                        FROM
                        (
                            SELECT FactoryCode,WorkOrder,
                                   Qty
                            FROM dbo.PM_PackingBGTransferCard
                            UNION ALL
                            SELECT FactoryCode,WorkOrder,
                                   Qty
                            FROM dbo.MM_SupProductStockTransfer
                            WHERE BusinessType = '1'
                        ) a
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 

                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" and FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                sql.Append(@" GROUP BY WorkOrder;

                        SELECT a.FactoryCode,
                               a.FactoryName,
                               a.ProductOrder,
                               a.ContainerNO,
                               a.WorkOrder,
                               a.MaterialCode,
							   b.MaterialName,
							   b.DXZH,
                               b.MMXH,
                               b.Spec,
                               a.ActualSheets * CONVERT(DECIMAL, b.DXZH) Qty,
                               c.WorkOrderInQty,
                               a.OrderStatus,
                               v1.ItemName OrderStatusName,
                               a.POStatus,
                               v2.ItemName POStatusName
                        FROM dbo.PL_WorkOrder a
                            INNER JOIN dbo.fn_GetMaterialAttrs() b
                                ON a.WorkOrder = b.WorkOrder
                            LEFT JOIN #temp_table c
                                ON a.WorkOrder = c.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'WorkOrderStatus'
                                   AND a.OrderStatus = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'PoStatus'
                                   AND a.POStatus = v2.ItemValue
                        WHERE a.OrderStatus IN ( '5', '6', '20', '30' )
                              AND a.FreezeFlag = '0' ");

                //工厂 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND a.ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //柜号 是否为空进行查询
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND a.ContainerNO like N'%{queryParam["ContainerNO"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    sql.Append($" AND a.WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //客户型号 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //面膜型号 是否为空进行查询
                if (!queryParam["MMXH"].IsEmpty())
                {
                    sql.Append($" AND b.MMXH like N'%{queryParam["MMXH"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND b.Spec like N'%{queryParam["Spec"]}%'");
                }
            }
            return this.BaseRepository().FindTable(sql.ToString());
        }
    }
}
