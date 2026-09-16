using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.SAP;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-12-05
    /// 2.创建作者: jpf
    /// 3.功能描述: MM_WarehouseSafetyStockService 业务服务类
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_WarehouseSafetyStock_Service : RepositoryFactory<MM_WarehouseSafetyStockEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_WarehouseSafetyStockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[FactoryName]
                      ,[WhsCode]
                      ,[WhsName]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[MaterialSpc]
                        ,UnitName
                      ,[Unit]
                      ,[SafetyQty]
                      ,[IsDelete]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[MM_WarehouseSafetyStock] where 1 = 1 ");
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
                //工厂名称 是否为空进行查询
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    //sql.Append($" AND FactoryName = N'{queryParam["FactoryName"]}'");
                    sql.Append($" AND FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //仓库名称 是否为空进行查询
                if (!queryParam["WhsName"].IsEmpty())
                {
                    //sql.Append($" AND WhsName = N'{queryParam["WhsName"]}'");
                    sql.Append($" AND WhsName like N'%{queryParam["WhsName"]}%'");
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
                //物料规格 是否为空进行查询
                if (!queryParam["MaterialSpc"].IsEmpty())
                {
                    //sql.Append($" AND MaterialSpc = N'{queryParam["MaterialSpc"]}'");
                    sql.Append($" AND MaterialSpc like N'%{queryParam["MaterialSpc"]}%'");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND Unit like N'%{queryParam["Unit"]}%'");
                }
                //安全库存数 是否为空进行查询
                if (!queryParam["SafetyQty"].IsEmpty())
                {
                    //sql.Append($" AND SafetyQty = N'{queryParam["SafetyQty"]}'");
                    sql.Append($" AND SafetyQty like N'%{queryParam["SafetyQty"]}%'");
                }
                //删除标识1代表已删除0代表未删除 是否为空进行查询
                if (!queryParam["IsDelete"].IsEmpty())
                {
                    //sql.Append($" AND IsDelete = N'{queryParam["IsDelete"]}'");
                    sql.Append($" AND IsDelete like N'%{queryParam["IsDelete"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND ModifyName like N'%{queryParam["ModifyName"]}%'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[FactoryName]
                      ,[WhsCode]
                      ,[WhsName]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[MaterialSpc]
                        ,UnitName
                      ,[Unit]
                      ,[SafetyQty]
                      ,[IsDelete]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[MM_WarehouseSafetyStock] where 1 = 1 ");
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
                //工厂名称 是否为空进行查询
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    //sql.Append($" AND FactoryName = N'{queryParam["FactoryName"]}'");
                    sql.Append($" AND FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //仓库名称 是否为空进行查询
                if (!queryParam["WhsName"].IsEmpty())
                {
                    //sql.Append($" AND WhsName = N'{queryParam["WhsName"]}'");
                    sql.Append($" AND WhsName like N'%{queryParam["WhsName"]}%'");
                }
               
                if(!queryParam["SafetyQtybig"].IsEmpty() && !queryParam["SafetyQty"].IsEmpty())
                {
                    sql.Append($"and  SafetyQty BETWEEN N'{queryParam["SafetyQty"]}' and N'{queryParam["SafetyQtybig"]}' ");
                }
                else
                {
                    if (!queryParam["SafetyQty"].IsEmpty())
                    {
                        //sql.Append($" AND WhsName = N'{queryParam["WhsName"]}'");
                        sql.Append($" AND SafetyQty = N'{queryParam["SafetyQty"]}'");
                    }
                    if (!queryParam["SafetyQtybig"].IsEmpty())
                    {
                        //sql.Append($" AND WhsName = N'{queryParam["WhsName"]}'");
                        sql.Append($" AND SafetyQty = N'{queryParam["SafetyQtybig"]}'");
                    }
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
                //物料规格 是否为空进行查询
                if (!queryParam["MaterialSpc"].IsEmpty())
                {
                    //sql.Append($" AND MaterialSpc = N'{queryParam["MaterialSpc"]}'");
                    sql.Append($" AND MaterialSpc like N'%{queryParam["MaterialSpc"]}%'");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND Unit like N'%{queryParam["Unit"]}%'");
                }
               
                //删除标识1代表已删除0代表未删除 是否为空进行查询
                if (!queryParam["IsDelete"].IsEmpty())
                {
                    //sql.Append($" AND IsDelete = N'{queryParam["IsDelete"]}'");
                    sql.Append($" AND IsDelete like N'%{queryParam["IsDelete"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND ModifyName like N'%{queryParam["ModifyName"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = N'{queryParam["queryName"]}'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_WarehouseSafetyStockEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[FactoryName]
                      ,[WhsCode]
                      ,[WhsName]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[MaterialSpc]
                       ,UnitName
                      ,[Unit]
                      ,[SafetyQty]
                      ,[IsDelete]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[MM_WarehouseSafetyStock] where 1 = 1 ");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_WarehouseSafetyStockEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
               

                if (!string.IsNullOrEmpty(keyValue))
                {
                    var WarehouseSafetyStockEntity = this.BaseRepository().IQueryable(t => (t.MaterialCode == entity.MaterialCode) && (t.FactoryName == entity.FactoryName) && (t.WhsCode == entity.WhsCode) && t.IsDelete == false).ToList().FirstOrDefault();

                    if (WarehouseSafetyStockEntity != null)
                    {
                        if (WarehouseSafetyStockEntity.Id == entity.Id)
                        {
                            entity.Modify(keyValue);
                            n = this.BaseRepository().Update(entity);

                        }
                        else
                        {
                            n = 2;
                        }
                    }
                    else
                    {
                        entity.Modify(keyValue);
                        n = this.BaseRepository().Update(entity);
                    }
                    
                }
                else
                {
                    var WarehouseSafetyStockEntity = this.BaseRepository().IQueryable(t => (t.MaterialCode == entity.MaterialCode) && (t.FactoryName == entity.FactoryName) && (t.WhsCode == entity.WhsCode) && t.IsDelete == false).ToList().FirstOrDefault();
                    if (WarehouseSafetyStockEntity == null)
                    {
                        entity.Create();
                        n = this.BaseRepository().Insert(entity);
                    }
                    else
                    {
                        n = 2;
                    }
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_WarehouseSafetyStockEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_WarehouseSafetyStockEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_WarehouseSafetyStock] set ");
                            string keyValue = "";
                            //循环实体
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "ID")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1: 0))+ ",");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[MM_WarehouseSafetyStock] (
                                            [Id]
                                            ,[FactoryCode]
                                            ,[FactoryName]
                                            ,[WhsCode]
                                            ,[WhsName]
                                            ,[MaterialCode]
                                            ,[MaterialName]
                                            ,[MaterialSpc]
                                            ,[Unit]
                                            ,[SafetyQty]
                                            ,[IsDelete]
                                            ,[Remark]
                                            ,[Creator]
                                            ,[CreateName]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                            ,[ModifyName]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.FactoryCode}'
                                ,N'{Save_obj.FactoryName}'
                                ,N'{Save_obj.WhsCode}'
                                ,N'{Save_obj.WhsName}'
                                ,N'{Save_obj.MaterialCode}'
                                ,N'{Save_obj.MaterialName}'
                                ,N'{Save_obj.MaterialSpc}'
                                ,N'{Save_obj.Unit}'
                                ,{Save_obj.SafetyQty}
                                ,{Save_obj.IsDelete}
                                ,N'{Save_obj.Remark}'
                                ,N'{Save_obj.Creator}'
                                ,N'{Save_obj.CreateName}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
                                ,N'{Save_obj.ModifyName}'
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_WarehouseSafetyStockEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.IsDelete = true;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
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
                sql.Append($@"DELETE FROM [dbo].[MM_WarehouseSafetyStock] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_WarehouseSafetyStockEntity</returns>
        public MM_WarehouseSafetyStockEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_WarehouseSafetyStockEntity</returns>
        public MM_WarehouseSafetyStockEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_WarehouseSafetyStockEntity 对象</returns>
        public MM_WarehouseSafetyStockEntity Get_ExpressionEntity(Expression<Func<MM_WarehouseSafetyStockEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_WarehouseSafetyStockEntity 列表</returns>
        public IEnumerable<MM_WarehouseSafetyStockEntity> Get_ExpressionList(Expression<Func<MM_WarehouseSafetyStockEntity, bool>> condition)
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
        //    RepositoryFactory<MM_WarehouseSafetyStockEntity> bomService = new RepositoryFactory<MM_WarehouseSafetyStockEntity>();
        
        //    MM_WarehouseSafetyStockEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_WarehouseSafetyStockDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_WarehouseSafetyStock_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_WarehouseSafetyStockEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<MM_WarehouseSafetyStockEntity> MM_WarehouseSafetyStockEntity_list =  db2.FindList<MM_WarehouseSafetyStockEntity>(sql.ToString());
                return MM_WarehouseSafetyStockEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
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
                DataTable MM_WarehouseSafetyStockEntity_DataTable = db2.FindTable(sql.ToString());
                return MM_WarehouseSafetyStockEntity_DataTable;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-12-05 16:40:59
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FactoryCode] as '工厂编码'
                      ,[FactoryName] as '工厂名称'
                      ,[WhsCode] as '仓库编码'
                      ,[WhsName] as '仓库名称'
                      ,[MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[MaterialSpc] as '物料规格'
                      ,[Unit] as '单位'
                      ,[SafetyQty] as '安全库存数'
                      ,[IsDelete] as '删除标识1代表已删除0代表未删除'
                      ,[Remark] as '备注'
                      ,[Creator] as '创建人'
                      ,[CreateName] as '创建人名称'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[ModifyName] as '修改人名称'
                  FROM [dbo].[MM_WarehouseSafetyStock] where IsDeleted = 0 ");
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
                string saveFileName = "仓库安全库存_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("仓库安全库存", dt, true);
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
        
    }
}
