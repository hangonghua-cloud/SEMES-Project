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
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Data;
using ALP.Application.Entity.SAPEntity;

namespace ALP.Application.Service.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-31
    /// 2.创建作者: admin
    /// 3.功能描述: MM_RawMaterialStockService 业务服务类
    /// 4.任务编号: 原材料库存明细表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_RawMaterialStock_Service : RepositoryFactory<MM_RawMaterialStockEntity>, MM_RawMaterialStockIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialStockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[MaterialCode]
                      ,[MaterialName],Spec,SmallClass
                      ,[BatchNo]
                      ,[Qty]
                      ,[Unit]
                      ,[SupplierCode]
                      ,[WhsCode]
                      ,[LocationCode]
                      ,[IsFrozen]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],BatchDate
                  FROM [dbo].[MM_RawMaterialStock] where 1=1 ");
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
                //批次号 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //数量 是否为空进行查询
                if (!queryParam["Qty"].IsEmpty())
                {
                    //sql.Append($" AND Qty = N'{queryParam["Qty"]}'");
                    sql.Append($" AND Qty like N'%{queryParam["Qty"]}%'");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND Unit like N'%{queryParam["Unit"]}%'");
                }
                //供应商 是否为空进行查询
                if (!queryParam["SupplierCode"].IsEmpty())
                {
                    //sql.Append($" AND SupplierCode = N'{queryParam["SupplierCode"]}'");
                    sql.Append($" AND SupplierCode like N'%{queryParam["SupplierCode"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //库位编码 是否为空进行查询
                if (!queryParam["LocationCode"].IsEmpty())
                {
                    //sql.Append($" AND LocationCode = N'{queryParam["LocationCode"]}'");
                    sql.Append($" AND LocationCode like N'%{queryParam["LocationCode"]}%'");
                }
                //冻结标识 是否为空进行查询
                if (!queryParam["IsFrozen"].IsEmpty())
                {
                    //sql.Append($" AND IsFrozen = N'{queryParam["IsFrozen"]}'");
                    sql.Append($" AND IsFrozen like N'%{queryParam["IsFrozen"]}%'");
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
                //修改人 是否为空进行查询
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
        /// 创建日期: 2022-12-15
        /// 任务编号: 耐磨层发料数据源
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListNew(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.FactoryCode,
                               a.FactoryName,
                               a.MaterialCode,
                               a.MaterialName,
                               a.Spec,
                               a.SmallClass,
                               v1.ItemName SmallClassName,
                               a.BatchNo,
                               a.Qty,
                               a.Unit,
                               a.SupplierCode,
                               d.Abbr SupplierName,
                               a.WhsCode,
                               b.ResourceName WhsName,
                               a.LocationCode,
                               c.ResourceName LocationName,
                               CASE e.FieldValue
                                   WHEN '1' THEN
                                       '库位管理'
                                   ELSE
                                       '非库位管理'
                               END ManageMode,
                               a.IsFrozen
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.ModelLeve = 'Warehouse'
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'StorageLocation'
                                   AND a.LocationCode = c.ResourceCode
                            LEFT JOIN dbo.Base_SupplierManage d
                                ON a.SupplierCode = d.SupplierCode
                            LEFT JOIN dbo.BS_ModelResourceExtendInfo e
                                ON e.FieldCode = 'GLFS'
                                   AND a.LocationCode = e.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND a.SmallClass = v1.ItemValue
                        WHERE 1 = 1
                              AND a.Qty <> 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND a.Id = N'{queryParam["Id"]}'");
                }
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["FieldValue"].IsEmpty())
                {
                    sql.Append($" AND e.FieldValue = N'{queryParam["FieldValue"]}'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND a.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料编码或名称
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (a.MaterialCode LIKE '%{queryParam["Material"]}%' OR a.MaterialName LIKE '%{queryParam["Material"]}%') ");
                }
                //批次号 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND a.BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //数量 是否为空进行查询
                if (!queryParam["Qty"].IsEmpty())
                {
                    //sql.Append($" AND Qty = N'{queryParam["Qty"]}'");
                    sql.Append($" AND a.Qty like N'%{queryParam["Qty"]}%'");
                }
                if (!queryParam["QtyStr"].IsEmpty())
                {
                    //QtyStr=">0"
                    sql.Append($" AND a.Qty   {queryParam["QtyStr"]}");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND a.Unit like N'%{queryParam["Unit"]}%'");
                }
                //供应商 是否为空进行查询
                if (!queryParam["SupplierCode"].IsEmpty())
                {
                    //sql.Append($" AND SupplierCode = N'{queryParam["SupplierCode"]}'");
                    sql.Append($" AND a.SupplierCode like N'%{queryParam["SupplierCode"]}%'");
                }
                //供应商名称 是否为空进行查询
                if (!queryParam["SupplierName"].IsEmpty())
                {
                    sql.Append($" AND d.SupplierName like N'%{queryParam["SupplierName"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    //sql.Append($" AND a.WhsCode like N'%{queryParam["WhsCode"]}%'");
                    var WhsCodeArr = queryParam["WhsCode"].ToString().Substring(0, queryParam["WhsCode"].ToString().Length - 1).Split(",");
                    var WhsCode = "";
                    foreach (var item in WhsCodeArr)
                    {
                        WhsCode = WhsCode + "'" + item + "'" + ",";
                    }
                    WhsCode = WhsCode.Substring(0, WhsCode.Length - 1);
                    sql.Append($" AND a.WhsCode in (SELECT ResourceCode FROM BS_ModelWithResource WHERE ParentResource in ({WhsCode}))");
                }
                else
                {
                    sql.Append($@"and a.WhsCode in (SELECT ResourceCode


                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Warehouse'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = '{queryParam["FactoryCode"]}'
                                                    AND EnabledMark = 1
                                          )
                                                AND EnabledMark = 1
                                      )
                  )");
                }
                //仓库名称 是否为空进行查询
                if (!queryParam["WhsName"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND b.ResourceName like N'%{queryParam["WhsName"]}%'");
                }
                if (!queryParam["ResourceName"].IsEmpty())
                {
                    sql.Append($" AND c.ResourceName like N'%{queryParam["ResourceName"]}%'");
                }
                //库位编码 是否为空进行查询
                if (!queryParam["LocationCode"].IsEmpty())
                {
                    //sql.Append($" AND LocationCode = N'{queryParam["LocationCode"]}'");
                    sql.Append($" AND a.LocationCode like N'%{queryParam["LocationCode"]}%'");
                }
                //管理方式 是否为空进行查询
                if (!queryParam["ManageMode"].IsEmpty())
                {
                    sql.Append($" AND e.FieldValue like N'%{queryParam["ManageMode"]}%'");
                }
                //冻结标识 是否为空进行查询
                if (!queryParam["IsFrozen"].IsEmpty())
                {
                    //sql.Append($" AND IsFrozen = N'{queryParam["IsFrozen"]}'");
                    sql.Append($" AND a.IsFrozen like N'%{queryParam["IsFrozen"]}%'");
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
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
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
                               a.MaterialCode,
                               a.MaterialName,
                               a.Spec,
                               a.SmallClass,
                               v1.ItemName SmallClassName,
                               a.BatchNo,
                               a.Qty,
                               a.Unit,
                               a.SupplierCode,
                               d.Abbr SupplierName,
                               a.WhsCode,
                               b.ResourceName WhsName,
                               a.LocationCode,
                               c.ResourceName LocationName,
                               CASE e.FieldValue
                                   WHEN '1' THEN
                                       '库位管理'
                                   ELSE
                                       '非库位管理'
                               END ManageMode,
                               a.IsFrozen,
							   a.Remark
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.ModelLeve = 'Warehouse'
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'StorageLocation'
                                   AND a.LocationCode = c.ResourceCode
                            LEFT JOIN dbo.Base_SupplierManage d
                                ON a.SupplierCode = d.SupplierCode
                            LEFT JOIN dbo.BS_ModelResourceExtendInfo e
                                ON e.FieldCode = 'GLFS'
                                   AND a.LocationCode = e.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND a.SmallClass = v1.ItemValue
                        WHERE 1 = 1
                              AND a.Qty <> 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND a.Id = N'{queryParam["Id"]}'");
                }
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["FieldValue"].IsEmpty())
                {
                    sql.Append($" AND e.FieldValue = N'{queryParam["FieldValue"]}'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND a.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND a.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料小类 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND (a.SmallClass LIKE '%{queryParam["SmallClass"]}%' OR v1.ItemName LIKE '%{queryParam["SmallClass"]}%') ");
                }
                //物料编码或名称
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (a.MaterialCode LIKE '%{queryParam["Material"]}%' OR a.MaterialName LIKE '%{queryParam["Material"]}%') ");
                }
                //批次号 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND a.BatchNo like N'%{queryParam["BatchNo"]}%'");
                }
                //数量 是否为空进行查询
                if (!queryParam["Qty"].IsEmpty())
                {
                    //sql.Append($" AND Qty = N'{queryParam["Qty"]}'");
                    sql.Append($" AND a.Qty like N'%{queryParam["Qty"]}%'");
                }
                if (!queryParam["QtyStr"].IsEmpty())
                {
                    //QtyStr=">0"
                    sql.Append($" AND a.Qty   {queryParam["QtyStr"]}");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND a.Unit like N'%{queryParam["Unit"]}%'");
                }
                //供应商 是否为空进行查询
                if (!queryParam["SupplierCode"].IsEmpty())
                {
                    //sql.Append($" AND SupplierCode = N'{queryParam["SupplierCode"]}'");
                    sql.Append($" AND a.SupplierCode like N'%{queryParam["SupplierCode"]}%'");
                }
                //供应商名称 是否为空进行查询
                if (!queryParam["SupplierName"].IsEmpty())
                {
                    sql.Append($" AND d.SupplierName like N'%{queryParam["SupplierName"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    sql.Append($" AND a.WhsCode = N'{queryParam["WhsCode"]}'");
                    //sql.Append($" AND a.WhsCode like N'%{queryParam["WhsCode"]}%'");
                }
                //仓库名称 是否为空进行查询
                if (!queryParam["WhsName"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    sql.Append($" AND b.ResourceName like N'%{queryParam["WhsName"]}%'");
                }
                //库位编码 是否为空进行查询
                if (!queryParam["LocationCode"].IsEmpty())
                {
                    sql.Append($" AND a.LocationCode = N'{queryParam["LocationCode"]}'");
                    //sql.Append($" AND a.LocationCode like N'%{queryParam["LocationCode"]}%'");
                }
                //管理方式 是否为空进行查询
                if (!queryParam["ManageMode"].IsEmpty())
                {
                    sql.Append($" AND e.FieldValue like N'%{queryParam["ManageMode"]}%'");
                }
                //冻结标识 是否为空进行查询
                if (!queryParam["IsFrozen"].IsEmpty())
                {
                    //sql.Append($" AND IsFrozen = N'{queryParam["IsFrozen"]}'");
                    sql.Append($" AND a.IsFrozen like N'%{queryParam["IsFrozen"]}%'");
                }
                //原材料发货管理-库存查询
                if (!queryParam["RawDispatchMaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode='{queryParam["RawDispatchMaterialCode"]}' ");
                }
                //原材料发货管理 - 库存查询
                if (!queryParam["CKLX"].IsEmpty())
                {
                    sql.Append($"  AND EXISTS(SELECT 1 FROM dbo.BS_ModelResourceExtendInfo t1 WHERE t1.ResourceCode = a.WhsCode AND t1.FieldCode = 'CKLX' AND t1.FieldValue = '{queryParam["CKLX"]}') ");
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
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="dic">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialStockEntity> GetList(Dictionary<string, object> dic, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id],FactoryCode
                      ,[MaterialCode]
                      ,[MaterialName],Spec,SmallClass
                      ,[BatchNo]
                      ,[Qty]
                      ,[Unit]
                      ,[SupplierCode]
                      ,[WhsCode]
                      ,[LocationCode]
                      ,[IsFrozen]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],BatchDate
                  FROM [dbo].[MM_RawMaterialStock] where 1=1 ");
            if (dic.ContainsKey("batchDate"))
            {
                sql.Append($@" and BatchDate = N'{dic["batchDate"]}' ");
            }
            msg = "";
            try
            {
                return this.BaseRepository().FindList(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public IEnumerable<MM_RawMaterialStockEntity> GetList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, MM_RawMaterialStockEntity entity, out string msg)
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
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<MM_RawMaterialStockEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialStockEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[MM_RawMaterialStock] set ");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            MM_RawMaterialStockEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                //entity.IsDeleted = true;
                this.BaseRepository().Delete(entity);
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }

        public int Delete(List<MM_RawMaterialStockEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
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
                sql.Append($@"DELETE FROM [dbo].[MM_RawMaterialStock] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回MM_RawMaterialStockEntity</returns>
        public MM_RawMaterialStockEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回MM_RawMaterialStockEntity</returns>
        public MM_RawMaterialStockEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_RawMaterialStockEntity 对象</returns>
        public MM_RawMaterialStockEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_RawMaterialStockEntity 列表</returns>
        public IEnumerable<MM_RawMaterialStockEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
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
        //    RepositoryFactory<MM_RawMaterialStockEntity> bomService = new RepositoryFactory<MM_RawMaterialStockEntity>();

        //    MM_RawMaterialStockEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    MM_RawMaterialStockDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.MM_RawMaterialStock_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MM_RawMaterialStockEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<MM_RawMaterialStockEntity> MM_RawMaterialStockEntity_list = db2.FindList<MM_RawMaterialStockEntity>(sql.ToString());
                return MM_RawMaterialStockEntity_list;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        /// <summary>
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 查询批次
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public List<dynamic> GetDynamic_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT TOP 10
                               a.BatchNo,
                               a.BatchNo + '~' + CAST(SUM(a.Qty) AS VARCHAR(10)) + '~' + ISNULL(b.Abbr, '') BatchNoQty
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.Base_SupplierManage b
                                ON a.SupplierCode = b.SupplierCode
                        WHERE a.MaterialCode = '{checkType}'
                              AND ISNULL(a.Qty, 0) > 0
                        GROUP BY a.BatchNo,
                                 b.Abbr
                        ORDER BY a.BatchNo DESC ");

            msg = "";
            try
            {
                return this.BaseRepository().Query(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public DataTable GetWeiLiaoBatchNo(string materialCode, string whsCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT TOP 10
                                   a.BatchNo,
                                   a.WhsCode,
                                   a.LocationCode,
                                   a.BatchNo + '~' + CAST(SUM(a.Qty) AS NVARCHAR(50)) + '~' + ISNULL(b.Abbr, '') BatchNoQty
                            FROM dbo.MM_RawMaterialStock a
                                LEFT JOIN dbo.Base_SupplierManage b
                                    ON a.SupplierCode = b.SupplierCode
                                LEFT JOIN dbo.BS_ModelWithResource c
                                    ON a.WhsCode = c.ResourceCode
                                LEFT JOIN dbo.BS_ModelWithResource d
                                    ON c.ResourceCode = d.ParentResource
                                       AND a.LocationCode = d.ResourceCode
                                LEFT JOIN dbo.BS_ModelResourceExtendInfo e
                                    ON d.ResourceCode = e.ResourceCode
                                       AND e.FieldCode = 'GLFS'
                                       AND e.FieldValue = '0'
                                       AND a.LocationCode = e.ResourceCode
                            WHERE a.MaterialCode = '{materialCode}'
                                  AND a.WhsCode = '{whsCode}'
                                  AND ISNULL(a.Qty, 0) > 0
                            GROUP BY a.BatchNo,
                                     b.Abbr,
                                     a.WhsCode,
                                     a.LocationCode
                            ORDER BY a.BatchNo DESC ");

            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
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
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[BatchNo] as '批次号'
                      ,[Qty] as '数量'
                      ,[Unit] as '单位'
                      ,[SupplierCode] as '供应商'
                      ,[WhsCode] as '仓库编码'
                      ,[LocationCode] as '库位编码'
                      ,[IsFrozen] as '冻结标识'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '修改人'
                      ,[ModifyTime] as '修改时间'
                  FROM [dbo].[MM_RawMaterialStock] where 1=1 ");
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
                string saveFileName = "原材料库存明细表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("原材料库存明细表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public int RemoveForm(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
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
            sql.Append(@"SELECT m1.FactoryCode,m1.FactoryName,m1.WhsCode,
                               m1.WhsName,
                               m1.MaterialCode,
                               m1.MaterialName,
							   m1.Spec,
                               m1.Unit,
                               m1.ActualQty,
                               ISNULL(m2.KWQty, 0) KWQty,
                               m1.ActualQty - ISNULL(m2.KWQty, 0) FKWQty,
							   ISNULL(SafetyQty,0) AS SafetyQty,
							   CASE WHEN ActualQty<ISNULL(SafetyQty,0)  THEN 1 ELSE 2 END AS isLower
                        FROM
                        (
                            SELECT a.FactoryCode,
							       a.FactoryName,
								   a.WhsCode,
                                   b.ResourceName WhsName,
                                   a.MaterialCode,
                                   a.MaterialName,
								   a.Spec,
                                   a.Unit,
                                   SUM(a.Qty) ActualQty
                            FROM dbo.MM_RawMaterialStock a
                                LEFT JOIN dbo.BS_ModelWithResource b
                                    ON b.ModelLeve = 'Warehouse'
                                       AND a.WhsCode = b.ResourceCode
                            GROUP BY a.FactoryCode,
							         a.FactoryName,
							         a.WhsCode,
                                     b.ResourceName,
                                     a.MaterialCode,
                                     a.MaterialName,
									 a.Spec,
                                     a.Unit
                        ) m1
                            LEFT JOIN
                            (
                                SELECT a.FactoryCode,
							           a.FactoryName,
									   a.WhsCode,
                                       b.ResourceName WhsName,
                                       a.MaterialCode,
                                       a.MaterialName,
									   a.Spec,
                                       a.Unit,
                                       SUM(a.Qty) KWQty
                                FROM dbo.MM_RawMaterialStock a
                                    LEFT JOIN dbo.BS_ModelWithResource b
                                        ON b.ModelLeve = 'Warehouse'
                                           AND a.WhsCode = b.ResourceCode
                                WHERE EXISTS
                                (
                                    SELECT 1
                                    FROM dbo.BS_ModelWithResource c1
                                        INNER JOIN dbo.BS_ModelResourceExtendInfo c2
                                            ON c1.ResourceCode = c2.ResourceCode
                                    WHERE c1.ModelLeve = 'StorageLocation'
                                          AND c2.FieldCode = 'GLFS'
                                          AND c2.FieldValue = 1
                                          AND c1.ResourceCode = a.LocationCode
                                )
                                GROUP BY a.FactoryCode,
							             a.FactoryName,
									     a.WhsCode,
                                         b.ResourceName,
                                         a.MaterialCode,
                                         a.MaterialName,
										 a.Spec,
                                         a.Unit
                            ) m2
                                ON m1.FactoryCode=m2.FactoryCode
								   AND m1.WhsCode = m2.WhsCode
                                   AND m1.MaterialCode = m2.MaterialCode
								   LEFT JOIN MM_WarehouseSafetyStock a ON m1.MaterialCode=a.MaterialCode AND m1.FactoryCode=a.FactoryCode
								   AND m1.WhsCode=a.WhsCode
                        WHERE 1 = 1");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND m1.FactoryCode = '{queryParam["FactoryCode"]}' ");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    sql.Append($" AND m1.WhsCode LIKE '%{queryParam["WhsCode"]}%' ");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND m1.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND m1.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND m1.Spec like N'%{queryParam["Spec"]}%'");
                }
                if (!queryParam["SafetyQtybig"].IsEmpty() && !queryParam["SafetyQty"].IsEmpty())
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
                if (!queryParam["isLower"].IsEmpty())
                {
                    if (queryParam["isLower"].ToString() == "1")
                    {
                        sql.Append($" and  ISNULL(SafetyQty,0) > ActualQty");
                    }
                    else
                    {
                        sql.Append($" and  ISNULL(SafetyQty,0) < ActualQty");
                    }

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
        /// 库存详情查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableDList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT m1.WhsCode,
                               m1.WhsName,
                               m1.MaterialCode,
                               m1.MaterialName,
                               m1.Unit,
                               m1.SupplierCode,
                               m1.SupplierName,
                               m1.BatchNo,
                               m1.IsFrozen,
                               m1.ActualQty,
                               ISNULL(m2.KWQty, 0) KWQty,
                               m1.ActualQty - ISNULL(m2.KWQty, 0) FKWQty
                        FROM
                        (
                            SELECT a.WhsCode,
                                   b.ResourceName WhsName,
                                   a.MaterialCode,
                                   a.MaterialName,
                                   a.Unit,
                                   a.SupplierCode,
                                   c.Abbr SupplierName,
                                   a.BatchNo,
                                   CASE a.IsFrozen
                                       WHEN '1' THEN
                                           '是'
                                       ELSE
                                           '否'
                                   END IsFrozen,
                                   SUM(a.Qty) ActualQty
                            FROM dbo.MM_RawMaterialStock a
                                LEFT JOIN dbo.BS_ModelWithResource b
                                    ON b.ModelLeve = 'Warehouse'
                                       AND a.WhsCode = b.ResourceCode
                                LEFT JOIN dbo.Base_SupplierManage c
                                    ON c.IsEnabled = 1
                                       AND a.SupplierCode = c.SupplierCode
                            GROUP BY a.WhsCode,
                                     b.ResourceName,
                                     a.MaterialCode,
                                     a.MaterialName,
                                     a.Unit,
                                     a.SupplierCode,
                                     c.Abbr,
                                     a.BatchNo,
                                     a.IsFrozen
                        ) m1
                            LEFT JOIN
                            (
                                SELECT a.WhsCode,
                                       b.ResourceName WhsName,
                                       a.MaterialCode,
                                       a.MaterialName,
                                       a.Unit,
                                       a.SupplierCode,
                                       c.Abbr SupplierName,
                                       a.BatchNo,
                                       CASE a.IsFrozen
                                           WHEN '1' THEN
                                               '是'
                                           ELSE
                                               '否'
                                       END IsFrozen,
                                       SUM(a.Qty) KWQty
                                FROM dbo.MM_RawMaterialStock a
                                    LEFT JOIN dbo.BS_ModelWithResource b
                                        ON b.ModelLeve = 'Warehouse'
                                           AND a.WhsCode = b.ResourceCode
                                    LEFT JOIN dbo.Base_SupplierManage c
                                        ON c.IsEnabled = 1
                                           AND a.SupplierCode = c.SupplierCode
                                WHERE EXISTS
                                (
                                    SELECT 1
                                    FROM dbo.BS_ModelWithResource c1
                                        INNER JOIN dbo.BS_ModelResourceExtendInfo c2
                                            ON c1.ResourceCode = c2.ResourceCode
                                    WHERE c1.ModelLeve = 'StorageLocation'
                                          AND c2.FieldCode = 'GLFS'
                                          AND c2.FieldValue = 1
                                          AND c1.ResourceCode = a.LocationCode
                                )
                                GROUP BY a.WhsCode,
                                         b.ResourceName,
                                         a.MaterialCode,
                                         a.MaterialName,
                                         a.Unit,
                                         a.SupplierCode,
                                         c.Abbr,
                                         a.BatchNo,
                                         a.IsFrozen
                            ) m2
                                ON m1.WhsCode = m2.WhsCode
                                   AND m1.MaterialCode = m2.MaterialCode
                                   AND m1.SupplierCode = m2.SupplierCode
                                   AND m1.BatchNo = m2.BatchNo
                                   AND m1.IsFrozen = m2.IsFrozen
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    sql.Append($" AND m1.WhsCode LIKE '%{queryParam["WhsCode"]}%' ");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND m1.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND m1.MaterialName like N'%{queryParam["MaterialName"]}%'");
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
        /// 库存检验详情查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableCheck(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT M.FactoryCode,
                               M.FactoryName,
                               M.MaterialCode,
                               M.MaterialName,
                               M.WhsCode,
                               MW.ResourceName WhsName,
                               M.BatchNo,
                               M.SupplierCode,
                               BS.Abbr SupplierName,
                               M.IsFrozen,
                               M.SmallClass,
                               v1.ItemName SmallClassName
                        FROM dbo.MM_RawMaterialStock M
                            LEFT JOIN dbo.Base_SupplierManage BS
                                ON M.SupplierCode = BS.SupplierCode
                            LEFT JOIN dbo.BS_ModelWithResource MW
                                ON M.WhsCode = MW.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND M.SmallClass = v1.ItemValue
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND M.FactoryCode = '{queryParam["FactoryCode"]}' ");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    sql.Append($" AND M.WhsCode = '{queryParam["WhsCode"]}' ");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (M.MaterialCode like N'%{queryParam["Material"]}%' OR M.MaterialName like N'%{queryParam["Material"]}%' )");
                }
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    sql.Append($" AND M.BatchNo like N'%{queryParam["BatchNo"]}%'");
                }

            }
            sql.Append(@" GROUP BY M.FactoryCode,
                                 M.FactoryName,
                                 M.MaterialCode,
                                 M.MaterialName,
                                 M.BatchNo,
                                 M.SupplierCode,
                                 M.IsFrozen,
                                 BS.Abbr,
                                 M.WhsCode,
                                 MW.ResourceName,
                                 M.SmallClass,
                                 v1.ItemName ");
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
        ///根据批号冻结库存
        /// </summary>
        /// <param name="batchNo"></param>
        /// <param name="userCode"></param>
        /// <param name="WhsCode"></param>
        /// <returns></returns>
        public int UpdateisFrozen(string isFrozen, string batchNo, string WhsCode, string userCode)
        {
            var sql = $@"UPDATE dbo.MM_RawMaterialStock 
                                                        SET IsFrozen='{isFrozen}',
                                                        ModifyBy='{userCode}',
                                                        ModifyTime=GETDATE() 
                                                        WHERE BatchNo='{batchNo}' AND WhsCode='{WhsCode}'";
            return this.BaseRepository().ExecuteBySql(sql);
        }

        #region 面膜 耐磨层发料查询库存
        /// <summary>
        /// 面膜 耐磨层发料查询库存
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetRawMaterialStock(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,a.FactoryCode,a.FactoryName,
                               a.MaterialCode,
                               a.MaterialName,
                               a.BatchNo,a.SmallClass,a.Spec,
                               a.Qty,
                               a.Unit,
                               a.SupplierCode,
                               d.Abbr SupplierName,
                               a.WhsCode,
                               b.ResourceName WhsName,
                               a.LocationCode,
                               c.ResourceName LocationName,
                              e.FieldValue,
                               a.IsFrozen
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.ModelLeve = 'Warehouse'
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.ModelLeve = 'StorageLocation'
                                   AND a.LocationCode = c.ResourceCode
                            LEFT JOIN dbo.Base_SupplierManage d
                                ON a.SupplierCode = d.SupplierCode
                            LEFT JOIN dbo.BS_ModelResourceExtendInfo e
                                ON e.FieldCode = 'CKSX'
                                   AND a.WhsCode = e.ResourceCode
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["FieldValue"].IsEmpty())
                {
                    sql.Append($" AND e.FieldValue = N'{queryParam["FieldValue"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND a.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    sql.Append($" AND a.BatchNo Like N'%{queryParam["BatchNo"]}%'");
                }
                if (!queryParam["ResourceName"].IsEmpty())
                {
                    sql.Append($" AND c.ResourceName like N'%{queryParam["ResourceName"]}%'");
                }
                //仓库编码 是否为空进行查询
                if (!queryParam["WhsCode"].IsEmpty())
                {
                    //sql.Append($" AND WhsCode = N'{queryParam["WhsCode"]}'");
                    //sql.Append($" AND a.WhsCode like N'%{queryParam["WhsCode"]}%'");
                    var WhsCodeArr = queryParam["WhsCode"].ToString().Substring(0, queryParam["WhsCode"].ToString().Length - 1).Split(",");
                    var WhsCode = "";
                    foreach (var item in WhsCodeArr)
                    {
                        WhsCode = WhsCode + "'" + item + "'" + ",";
                    }
                    WhsCode = WhsCode.Substring(0, WhsCode.Length - 1);
                    sql.Append($" AND a.WhsCode in (SELECT ResourceCode FROM BS_ModelWithResource WHERE ParentResource in ({WhsCode}))");
                }
                else
                {
                    sql.Append($@"and a.WhsCode in (SELECT ResourceCode


                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Warehouse'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = '{queryParam["FactoryCode"]}'
                                                    AND EnabledMark = 1
                                          )
                                                AND EnabledMark = 1
                                      )
                  )");
                }
                if (!queryParam["QtyStr"].IsEmpty())
                {
                    //QtyStr=">0"
                    sql.Append($" AND a.Qty   {queryParam["QtyStr"]}");
                }

                if (!queryParam["IsFrozen"].IsEmpty())
                {
                    sql.Append($" AND IsFrozen = N'{queryParam["IsFrozen"]}'");
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

        #region 采购订单 -库存采购查询
        /// <summary>
        /// 采购订单 -库存采购查询
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMaterialStockTable(string queryJson)
        {
            var sql = new StringBuilder();
            #region Old
            //sql.Append(@"SELECT BMF.FactoryCode,
            //                   MM.FactoryName,
            //                   BMF.MaterialCode,
            //                   BM.MaterialName,
            //                   BM.UnitName,
            //                   MM.Qty,
            //                   BMF.SafeStock,
            //                   B.TransitQty,
            //                   BM.SmallClass,
            //                   V.ItemName SmallClassName,
            //                   BM.Spec,
            //                   CASE
            //                       WHEN ISNULL(MM.Qty, 0) < BMF.SafeStock THEN
            //                           '1'
            //                       ELSE
            //                           '0'
            //                   END IsSafe
            //            FROM dbo.Base_MaterialFactory BMF
            //                INNER JOIN dbo.Base_Material BM
            //                    ON BM.MaterialCode = BMF.MaterialCode
            //                       AND BM.IsEnabled = '1'
            //                LEFT JOIN
            //                (
            //                    SELECT FactoryCode,
            //                           FactoryName,
            //                           MaterialCode,
            //                           SUM(Qty) Qty
            //                    FROM dbo.MM_RawMaterialStock
            //                    GROUP BY FactoryCode,
            //                             FactoryName,
            //                             MaterialCode
            //                ) MM
            //                    ON MM.MaterialCode = BMF.MaterialCode
            //                       AND BMF.FactoryCode = MM.FactoryCode
            //                LEFT JOIN
            //                (
            //                    SELECT A.FactoryCode,
            //                           A.MaterialCode,
            //                           SUM(A.PurchaseNum) - SUM(ISNULL(A.Qty, 0)) TransitQty
            //                    FROM
            //                    (  SELECT
            //                               PO.FactoryCode,
            //                               PO.MaterialCode,
            //                               PO.PurchaseNum,
            //                               SUM(MRI.Qty) Qty
            //                        FROM dbo.PL_PurchaseOrder PO
            //                            LEFT JOIN dbo.MM_ReceiptNotice MRN
            //                                ON PO.Id = MRN.PurchaseId
            //                            LEFT JOIN dbo.MM_RawMaterialIn MRI
            //                                ON MRN.Id = MRI.BusinessId
            //                                   AND MRI.InType = 1
            //                        WHERE PO.ArrivalStatus IN ( '1', '2' )
            //                        GROUP BY PO.FactoryCode,
            //                                 PO.MaterialCode,
            //                                 PO.PurchaseNum
            //                    ) A
            //                    GROUP BY A.FactoryCode,
            //                             A.MaterialCode
            //                ) B
            //                    ON B.MaterialCode = BMF.MaterialCode
            //                       AND B.FactoryCode = BMF.FactoryCode
            //                LEFT JOIN dbo.V_DataDictionary V
            //                    ON V.EnCode = 'MaterialSmall'
            //                       AND V.ItemValue = BM.SmallClass
            //            WHERE 1 = 1 ");
            #endregion;

            sql.Append(@"IF OBJECT_ID('tempdb..#rawStock') IS NOT NULL DROP TABLE  #rawStock;

 SELECT FactoryCode,
                                       FactoryName,
                                       MaterialCode,
                                       SUM(Qty) Qty
									   INTO #rawStock
                                FROM dbo.MM_RawMaterialStock
                                GROUP BY FactoryCode,
                                         FactoryName,
                                         MaterialCode;

IF OBJECT_ID('tempdb..#temp1') IS NOT NULL DROP TABLE  #temp1;

SELECT
                                           PO.FactoryCode,
                                           PO.MaterialCode,
                                           PO.PurchaseNum -ISNULL(ro.Qty,0) TransitQty
										   INTO #temp1
                                    FROM dbo.PL_PurchaseOrder PO
									LEFT JOIN 
									(
									    SELECT MRN.PurchaseId,SUM(ISNULL(MRI.Qty,0)) Qty FROM dbo.MM_ReceiptNotice MRN
										LEFT JOIN dbo.MM_RawMaterialIn MRI ON MRN.Id = MRI.BusinessId AND MRI.InType = 1
										GROUP BY MRN.PurchaseId
									) ro ON po.Id=ro.PurchaseId
                                    WHERE PO.ArrivalStatus IN ( '1', '2' );

SELECT BMF.FactoryCode,
                               MM.FactoryName,
                               BMF.MaterialCode,
                               BM.MaterialName,
                               BM.UnitName,
                               MM.Qty,
                               BMF.SafeStock,
                               B.TransitQty,
                               BM.SmallClass,
                               V.ItemName SmallClassName,
                               BM.Spec,
                               CASE
                                   WHEN ISNULL(MM.Qty, 0) < BMF.SafeStock THEN
                                       '1'
                                   ELSE
                                       '0'
                               END IsSafe
                        FROM dbo.Base_MaterialFactory BMF
                            INNER JOIN dbo.Base_Material BM
                                ON BM.MaterialCode = BMF.MaterialCode
                                   AND BM.IsEnabled = '1'
                            LEFT JOIN #rawStock MM
                                ON MM.MaterialCode = BMF.MaterialCode
                                   AND BMF.FactoryCode = MM.FactoryCode
                            LEFT JOIN #temp1 B
                                ON B.MaterialCode = BMF.MaterialCode
                                   AND B.FactoryCode = BMF.FactoryCode
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = BM.SmallClass
                        WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND BMF.FactoryCode =  '{queryParam["FactoryCode"]}' ");
                }
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (BM.MaterialName like '%{queryParam["Material"]}%' OR BM.MaterialCode like '%{queryParam["Material"]}%')");
                }
                if (!queryParam["ProcureType"].IsEmpty())
                {
                    sql.Append($" AND BMF.ProcureType = '{queryParam["ProcureType"]}' ");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND BMF.MaterialCode = '{queryParam["MaterialCode"]}' ");
                }
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND BM.SmallClass = '{queryParam["SmallClass"]}' ");
                }
                if (!queryParam["IsSafe"].IsEmpty())
                {
                    sql.Append($" AND CASE WHEN ISNULL(MM.Qty,0)<BMF.SafeStock THEN '1' ELSE '0' END = '{queryParam["IsSafe"]}' ");
                }
            }

            return this.BaseRepository().FindTable(sql.ToString());
        }
        #endregion

        #region 采购订单-按照订单库存采购
        /// <summary>
        /// 采购订单-按照订单库存采购
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetMaterialOrderStockTable(string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@"IF OBJECT_ID('tempdb..#rawStock') IS NOT NULL DROP TABLE #rawStock;
SELECT MR.FactoryCode,
                                       MaterialCode,
                                       SUM(Qty) Qty  INTO #rawStock
                                FROM dbo.MM_RawMaterialStock MR WITH(NOLOCK)
                                WHERE 1 = 1
                                      AND
                                      (
                                          SmallClass IN ( 'MM', 'NMC' )
                                          AND NOT EXISTS
                                (
                                    SELECT 1
                                    FROM dbo.BS_ModelWithResource M WITH(NOLOCK)
                                        INNER JOIN dbo.BS_ModelResourceExtendInfo ME WITH(NOLOCK)
                                            ON ME.ResourceCode = M.ResourceCode
                                    WHERE ME.FieldCode = 'GLFS'
                                          AND ME.FieldValue = '0'
                                          AND M.ResourceCode = MR.LocationCode
                                )
                                      )
                                GROUP BY MR.FactoryCode,
                                         MaterialCode;

IF OBJECT_ID('tempdb..#purchaseOrder') IS NOT NULL DROP TABLE #purchaseOrder;
SELECT PO.FactoryCode,
                                       PO.MaterialCode,
                                       SUM(PO.PurchaseNum) - SUM(ISNULL(MRI.Qty, 0)) TransitQty INTO #purchaseOrder
                                FROM dbo.PL_PurchaseOrder PO WITH(NOLOCK)
                                    LEFT JOIN
                                    (
                                        SELECT MRN.PurchaseId,
                                               SUM(ISNULL(MR.Qty, 0)) Qty
                                        FROM dbo.MM_ReceiptNotice MRN WITH(NOLOCK)
                                            LEFT JOIN dbo.MM_RawMaterialIn MR WITH(NOLOCK)
                                                ON MRN.Id = MR.BusinessId
                                                   AND MR.InType = 1
                                        GROUP BY MRN.PurchaseId
                                    ) MRI
                                        ON PO.Id = MRI.PurchaseId
                                WHERE PO.ArrivalStatus IN ( '1', '2' )
                                GROUP BY PO.FactoryCode,
                                         PO.MaterialCode;

IF OBJECT_ID('tempdb..#workOrder') IS NOT NULL DROP TABLE #workOrder;
SELECT SUM(PP.Amount) OrderQty,
                                       PP.MaterialCode,
                                       PW.FactoryCode INTO #workOrder
                                FROM dbo.PL_WorkOrder PW WITH(NOLOCK)
                                    INNER JOIN dbo.PL_PrdOrderReqMaterials PP WITH(NOLOCK)
                                        ON PW.WorkOrder = PP.WorkOrder
                                WHERE 1 = 1
                                      AND NOT EXISTS
                                (
                                    SELECT 1 FROM dbo.PL_ExeWorkOrder PE WITH(NOLOCK) WHERE PE.WorkOrder = PP.WorkOrder
                                )
                                --WHERE BB.SmallClass IN('MM','NMC','MTP')
                                GROUP BY PP.MaterialCode,
                                         PW.FactoryCode

SELECT BMF.FactoryCode,
                               bs1.ResourceName FactoryName,
                               BMF.MaterialCode,
                               BM.MaterialName,
                               BM.UnitName,
                               MM.Qty,
                               B.TransitQty,
                               BM.SmallClass,
                               V.ItemName SmallClassName,
                               BM.Spec,
                               C.OrderQty,
                               C.OrderQty - ISNULL(B.TransitQty, 0) - ISNULL(MM.Qty, 0) BuyQty
                        FROM dbo.Base_MaterialFactory BMF WITH(NOLOCK)
                            INNER JOIN dbo.Base_Material BM WITH(NOLOCK)
                                ON BM.MaterialCode = BMF.MaterialCode
                                   AND BM.IsEnabled = '1'
                            LEFT JOIN #rawStock MM
                                ON MM.MaterialCode = BMF.MaterialCode
                                   AND MM.FactoryCode = BMF.FactoryCode
                            LEFT JOIN #purchaseOrder B
                                ON B.MaterialCode = BMF.MaterialCode
                                   AND B.FactoryCode = BMF.FactoryCode
                            LEFT JOIN #workOrder C
                                ON C.MaterialCode = BMF.MaterialCode
                                   AND C.FactoryCode = BMF.FactoryCode
                            LEFT JOIN dbo.V_DataDictionary V WITH(NOLOCK)
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = BM.SmallClass
                            LEFT JOIN dbo.BS_ModelWithResource bs1 WITH(NOLOCK)
                                ON bs1.ModelLeve = 'Factory'
                                   AND bs1.ResourceCode = BMF.FactoryCode
                        WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND BMF.FactoryCode = '{queryParam["FactoryCode"]}' ");
                }
                if (!queryParam["Material"].IsEmpty())
                {
                    sql.Append($" AND (BM.MaterialName like '%{queryParam["Material"]}%' OR BM.MaterialCode like '%{queryParam["Material"]}%')");
                }
                if (!queryParam["ProcureType"].IsEmpty())
                {
                    sql.Append($" AND BMF.ProcureType = '{queryParam["ProcureType"]}' ");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND BMF.MaterialCode = '{queryParam["MaterialCode"]}' ");
                }
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND BM.SmallClass = '{queryParam["SmallClass"]}' ");
                }
                if (!queryParam["UpperLimit"].IsEmpty())
                {
                    sql.Append($" AND C.OrderQty-ISNULL(B.TransitQty,0)-ISNULL(mm.Qty,0) <= '{queryParam["UpperLimit"]}' ");
                }
                if (!queryParam["LowerLimit"].IsEmpty())
                {
                    sql.Append($" AND C.OrderQty-ISNULL(B.TransitQty,0)-ISNULL(mm.Qty,0) >= '{queryParam["LowerLimit"]}' ");
                }
            }

            return this.BaseRepository().FindTable(sql.ToString());
        }
        #endregion

        #region 原材料库存备份
        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public void StockBackup()
        {
            try
            {
                //调用存储过程
                SqlParameter[] parameters = {
                };
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("Pro_RawMaterialStockBackup", parameters);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SAP原材料库存
        public void SaveSAPMM_RawMaterialStock(List<SAP_MaterialStock> entitys)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                foreach (var item in entitys)
                {
                    if (string.IsNullOrEmpty(item.FactoryCode))
                    {
                        throw new Exception("工厂编码必须传！");
                    }
                    if (string.IsNullOrEmpty(item.FactoryName))
                    {
                        throw new Exception("工厂名称必须传！");
                    }
                    if (string.IsNullOrEmpty(item.MaterialCode))
                    {
                        throw new Exception("物料编码必须传！");
                    }
                    if (string.IsNullOrEmpty(item.MaterialName))
                    {
                        throw new Exception("物料名称必须传！");
                    }
                    if (string.IsNullOrEmpty(item.Spec))
                    {
                        throw new Exception("物料规格必须传！");
                    }
                    if (string.IsNullOrEmpty(item.SmallClass))
                    {
                        throw new Exception("物料小类必须传！");
                    }

                    if (item.Qty.IsEmpty())
                    {
                        throw new Exception("数量必须传！");
                    }
                    if (string.IsNullOrEmpty(item.Unit))
                    {
                        throw new Exception("单位必须传！");
                    }
                    if (string.IsNullOrEmpty(item.SupplierCode))
                    {
                        throw new Exception("供应商编码必须传！");
                    }
                    if (string.IsNullOrEmpty(item.WhsCode))
                    {
                        throw new Exception("仓库编码必须传！");
                    }
                    if (string.IsNullOrEmpty(item.LocationCode))
                    {
                        throw new Exception("库位编码必须传！");
                    }
                    //将SAP物料编码转化为MES的物料编码
                    var MaterSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", item.MaterialCode);
                    var MaterData = new RepositoryFactory().BaseRepository().FindTable(MaterSql);
                    if (MaterData.Rows.Count == 0)
                    {
                        throw new Exception(item.MaterialCode + "物料编码未同步");
                    }
                    item.MaterialCode = MaterData.Rows[0]["MaterialCode"].ToString();
                    //通过仓库编码进行判断是原材料、成品库存
                    var StockSql = string.Format(@" select b.FieldType,b.FieldCode,b.FieldName,m.FieldValue  from [dbo].[BS_ModelLevelExtendFields] b
                               left join BS_ModelResourceExtendInfo m on b.FieldCode=m.FieldCode and m.ResourceCode='{0}'
                                where 1=1 and b.EnabledMark=1 AND  [LevelCode]='Warehouse'AND b.FieldCode='CKLX'", item.WhsCode);
                    var StockData = new RepositoryFactory().BaseRepository().FindTable(StockSql);
                    if (StockData.Rows.Count == 0)
                    {
                        throw new Exception(item.WhsCode + "仓库编码在MES系统中未找到对应的属性维护，无法区分原材料、成品库");
                    }
                    var FieldValue = StockData.Rows[0]["FieldValue"].ToString();
                    if (FieldValue == "1")
                    {
                        //判断原材料数据是否已经传了
                        var RawMaterialStockEntiy = Get_ExpressionEntity(t => t.FactoryCode == item.FactoryCode && t.MaterialCode == item.MaterialCode
                        && t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode && t.SmallClass == item.SmallClass
                        );
                        MM_RawMaterialStockEntity mM_RawMaterialStockEntity = new MM_RawMaterialStockEntity();

                        if (RawMaterialStockEntiy == null)
                        {
                            mM_RawMaterialStockEntity.FactoryCode = item.FactoryCode;
                            mM_RawMaterialStockEntity.FactoryName = item.FactoryName;
                            mM_RawMaterialStockEntity.MaterialCode = item.MaterialCode;
                            mM_RawMaterialStockEntity.MaterialName = item.MaterialName;
                            mM_RawMaterialStockEntity.Spec = item.Spec;
                            mM_RawMaterialStockEntity.Qty = item.Qty;
                            mM_RawMaterialStockEntity.SmallClass = item.SmallClass;
                            mM_RawMaterialStockEntity.Specialmark = item.Specialmark;
                            mM_RawMaterialStockEntity.SupplierCode = item.SupplierCode;
                            mM_RawMaterialStockEntity.WhsCode = item.WhsCode;
                            mM_RawMaterialStockEntity.LocationCode = item.LocationCode;
                            mM_RawMaterialStockEntity.BatchNo = item.BatchNo;
                            mM_RawMaterialStockEntity.Remark = item.Remark;
                            mM_RawMaterialStockEntity.Unit = item.Unit;
                            mM_RawMaterialStockEntity.IsFrozen = item.IsFrozen ? "1" : "0";
                            mM_RawMaterialStockEntity.CreateTime = DateTime.Now;
                            mM_RawMaterialStockEntity.Create();
                            db.Insert(item);
                        }
                        else
                        {
                            RawMaterialStockEntiy.FactoryCode = item.FactoryCode;
                            RawMaterialStockEntiy.FactoryName = item.FactoryName;
                            RawMaterialStockEntiy.MaterialCode = item.MaterialCode;
                            RawMaterialStockEntiy.MaterialName = item.MaterialName;
                            RawMaterialStockEntiy.Spec = item.Spec;
                            RawMaterialStockEntiy.SmallClass = item.SmallClass;
                            RawMaterialStockEntiy.Specialmark = item.Specialmark;
                            RawMaterialStockEntiy.SupplierCode = item.SupplierCode;
                            RawMaterialStockEntiy.WhsCode = item.WhsCode;
                            RawMaterialStockEntiy.LocationCode = item.LocationCode;
                            RawMaterialStockEntiy.BatchNo = item.BatchNo;
                            RawMaterialStockEntiy.Unit = item.Unit;
                            RawMaterialStockEntiy.IsFrozen = item.IsFrozen ? "1" : "0";
                            RawMaterialStockEntiy.ModifyTime = DateTime.Now;
                            db.Update(RawMaterialStockEntiy);
                        }
                    }
                    else
                    {
                        //成品处理
                        var ProductStockEntity = new MM_ProductStock_Service().Get_ExpressionEntity(t => t.FactoryCode == item.FactoryCode && t.MaterialCode == item.MaterialCode
                    && t.WhsCode == item.WhsCode && t.LocationCode == item.LocationCode && t.ProductOrder == item.ProductOrder
                    && t.ContainerNO == item.ContainerNO
                    );
                        MM_ProductStockEntity mM_ProductStockEntity = new MM_ProductStockEntity();
                        if (ProductStockEntity == null)
                        {
                            mM_ProductStockEntity.FactoryCode = item.FactoryCode;
                            mM_ProductStockEntity.FactoryName = item.FactoryName;
                            mM_ProductStockEntity.ProductOrder = item.ProductOrder;
                            mM_ProductStockEntity.ContainerNO = item.ContainerNO;
                            mM_ProductStockEntity.MaterialCode = item.MaterialCode;
                            mM_ProductStockEntity.LocationCode = item.LocationCode;
                            mM_ProductStockEntity.WhsCode = item.WhsCode;
                            mM_ProductStockEntity.PalletQty = item.PalletQty;
                            mM_ProductStockEntity.PieceQty = item.Qty;
                            mM_ProductStockEntity.Specialmark = item.Specialmark;
                            mM_ProductStockEntity.IsEnabled = item.IsFrozen;
                            mM_ProductStockEntity.Create();
                            mM_ProductStockEntity.CreateTime = DateTime.Now;
                            db.Insert(mM_ProductStockEntity);
                        }
                        else
                        {
                            ProductStockEntity.FactoryCode = item.FactoryCode;
                            ProductStockEntity.FactoryName = item.FactoryName;
                            ProductStockEntity.ProductOrder = item.ProductOrder;
                            ProductStockEntity.ContainerNO = item.ContainerNO;
                            ProductStockEntity.MaterialCode = item.MaterialCode;
                            ProductStockEntity.LocationCode = item.LocationCode;
                            ProductStockEntity.WhsCode = item.WhsCode;
                            ProductStockEntity.PalletQty = item.PalletQty;
                            ProductStockEntity.PieceQty = item.Qty;
                            ProductStockEntity.Specialmark = item.Specialmark;
                            ProductStockEntity.IsEnabled = item.IsFrozen;
                            ProductStockEntity.ModifyTime = DateTime.Now;
                            db.Update(ProductStockEntity);
                        }
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            finally
            {
                db.Close();
            }

        }

        #endregion
    }
}
