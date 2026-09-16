using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.EquipmentManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentSparePartsService 业务服务类
    /// 4.任务编号: 设备报修
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentSpareParts_Service : RepositoryFactory<EP_EquipmentSparePartsEntity>
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentSparePartsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[RepairId]
                      ,[SparePartsId]
                      ,[SparePartsName]
                      ,[SmallClass]
                      ,[SpecificationsModels]
                      ,[Num]
                      ,[Unit]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentSpareParts] where EnabledMark=1  ");
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
                //设备编码 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentId = N'{queryParam["EquipmentId"]}'");
                    sql.Append($" AND EquipmentId like N'%{queryParam["EquipmentId"]}%'");
                }
                //设备名称 是否为空进行查询
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentName = N'{queryParam["EquipmentName"]}'");
                    sql.Append($" AND EquipmentName like N'%{queryParam["EquipmentName"]}%'");
                }
                //报修类别 是否为空进行查询
                if (!queryParam["RepairingType"].IsEmpty())
                {
                    //sql.Append($" AND RepairingType = N'{queryParam["RepairingType"]}'");
                    sql.Append($" AND RepairingType like N'%{queryParam["RepairingType"]}%'");
                }
                //报修状态 是否为空进行查询
                if (!queryParam["RepairingStatus"].IsEmpty())
                {
                    //sql.Append($" AND RepairingStatus = N'{queryParam["RepairingStatus"]}'");
                    sql.Append($" AND RepairingStatus like N'%{queryParam["RepairingStatus"]}%'");
                }
                //故障描述 是否为空进行查询
                if (!queryParam["MalfunctionDescription"].IsEmpty())
                {
                    //sql.Append($" AND MalfunctionDescription = N'{queryParam["MalfunctionDescription"]}'");
                    sql.Append($" AND MalfunctionDescription like N'%{queryParam["MalfunctionDescription"]}%'");
                }
                //维修时长 是否为空进行查询
                if (!queryParam["TimeLength"].IsEmpty())
                {
                    //sql.Append($" AND TimeLength = N'{queryParam["TimeLength"]}'");
                    sql.Append($" AND TimeLength like N'%{queryParam["TimeLength"]}%'");
                }
                //维修内容 是否为空进行查询
                if (!queryParam["RepairingContent"].IsEmpty())
                {
                    //sql.Append($" AND RepairingContent = N'{queryParam["RepairingContent"]}'");
                    sql.Append($" AND RepairingContent like N'%{queryParam["RepairingContent"]}%'");
                }
                //维修人 是否为空进行查询
                if (!queryParam["RepairingPerson"].IsEmpty())
                {
                    //sql.Append($" AND RepairingPerson = N'{queryParam["RepairingPerson"]}'");
                    sql.Append($" AND RepairingPerson like N'%{queryParam["RepairingPerson"]}%'");
                }
                //维修时间 是否为空进行查询
                if (!queryParam["FinishTime"].IsEmpty())
                {
                    //sql.Append($" AND FinishTime = N'{queryParam["FinishTime"]}'");
                    sql.Append($" AND FinishTime like N'%{queryParam["FinishTime"]}%'");
                }
                //报修人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //报修时间 是否为空进行查询
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
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 备品备件记录查询
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var sql1 = new StringBuilder();
            sql.Append(@"SELECT *
                            FROM
                            (
                                SELECT EP.FactoryCode,
                                       EP.FactoryName,
                                       EM.EquipmentId,
                                       EM.EquipmentType,
                                       EM.EquipmentName,
                                       EM.TestMethodCoadin,
                                       EP.Id,
                                       EP.UseType,
                                       EP.SparePartsId,
                                       EP.SparePartsName,
                                       EP.Creator,
                                       EP.CreateTime,
                                       EP.SmallClass,
                                       BM.Spec,
                                       V.ItemName MaterialClassName,
                                       P.Name CreatorName
                                FROM [dbo].[EP_EquipmentMalfunctionRepair] ER
                                    INNER JOIN [dbo].[V_EP_EquipmentManage] EM
                                        ON ER.EquipmentId = EM.EquipmentId
                                    INNER JOIN [dbo].[EP_EquipmentSpareParts] EP
                                        ON ER.Id = EP.RepairId
                                    LEFT JOIN dbo.Base_Material BM
                                        ON BM.MaterialCode = EP.SparePartsId
                                    LEFT JOIN dbo.V_DataDictionary V
                                        ON V.EnCode = 'MaterialType'
                                           AND V.ItemValue = BM.MaterialClass
                                    LEFT JOIN dbo.BS_People P
                                        ON P.Code = EP.Creator
                                UNION ALL
                                SELECT EP.FactoryCode,
                                       EP.FactoryName,
                                       EM.EquipmentId,
                                       EM.EquipmentType,
                                       EM.EquipmentName,
                                       EM.TestMethodCoadin,
                                       EP.Id,
                                       EP.UseType,
                                       EP.SparePartsId,
                                       EP.SparePartsName,
                                       EP.Creator,
                                       EP.CreateTime,
                                       EP.SmallClass,
                                       BM.Spec,
                                       V.ItemName MaterialClassName,
                                       P.Name CreatorName
                                FROM [dbo].[EP_EquipmentMaintainTask] ET
                                    INNER JOIN dbo.EP_EquipmentManage EM
                                        ON ET.EquipmentId = EM.EquipmentId
                                    INNER JOIN [dbo].[EP_EquipmentSpareParts] EP
                                        ON ET.Id = EP.RepairId
                                    LEFT JOIN dbo.Base_Material BM
                                        ON BM.MaterialCode = EP.SparePartsId
                                    LEFT JOIN dbo.V_DataDictionary V
                                        ON V.EnCode = 'MaterialType'
                                           AND V.ItemValue = BM.MaterialClass
                                    LEFT JOIN dbo.BS_People P
                                        ON P.Code = EP.Creator
                            ) a
                            WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    sql.Append($" AND a.EquipmentId Like N'%{queryParam["EquipmentId"]}%'");
                }
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($" AND a.EquipmentName LIKE N'%{queryParam["EquipmentName"]}%'");
                }
                if (!queryParam["SparePartsId"].IsEmpty())
                {
                    sql.Append($" AND a.SparePartsId Like N'%{queryParam["SparePartsId"]}%'");
                }
                if (!queryParam["SparePartsName"].IsEmpty())
                {
                    sql.Append($" AND a.SparePartsName LIKE N'%{queryParam["SparePartsName"]}%'");
                }
                if (!queryParam["SpecificationsModels"].IsEmpty())
                {
                    sql.Append($" AND a.Spec LIKE N'%{queryParam["SpecificationsModels"]}%'");
                }
                if (!queryParam["ReplaceType"].IsEmpty())
                {
                    sql.Append($" AND a.UseType = N'{queryParam["ReplaceType"]}'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime >= N'{queryParam["StartTime"]}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND a.CreateTime <= N'{queryParam["EndTime"]}' ");
                }
            }
            //sql.Append(sql1);
            //sql.Append(@"   UNION ALL
            //                SELECT EP.FactoryCode,
            //                   EP.FactoryName,
            //                   EM.EquipmentId,
            //                   EM.EquipmentType,
            //                   EM.EquipmentName,
            //                   EM.TestMethodCoadin,
            //                   EP.Id,
            //                   EP.UseType,
            //                   EP.SparePartsId,
            //                   EP.SparePartsName,
            //                   EP.Creator,
            //                   EP.CreateTime,
            //                   EP.SmallClass,
            //                   BM.Spec,
            //                   V.ItemName MaterialClassName,
            //                   P.Name CreatorName
            //            FROM [dbo].[EP_EquipmentMaintainTask] ET
            //                INNER JOIN dbo.EP_EquipmentManage EM
            //                    ON ET.EquipmentId = EM.EquipmentId
            //                INNER JOIN [dbo].[EP_EquipmentSpareParts] EP
            //                    ON ET.Id = EP.RepairId
            //                LEFT JOIN dbo.Base_Material BM
            //                    ON BM.MaterialCode = EP.SparePartsId
            //                LEFT JOIN dbo.V_DataDictionary V
            //                    ON V.EnCode = 'MaterialType'
            //                       AND V.ItemValue = BM.MaterialClass
            //                LEFT JOIN dbo.BS_People P
            //                    ON P.Code = EP.Creator
            //            WHERE 1 = 1 ");

            //sql.Append(sql1);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT [Id],
                               [RepairId],
                               a.FactoryCode,
                               a.FactoryName,
                               UseType,
                               [SparePartsId],
                               [SparePartsName],
                               [SmallClass],
                               [SpecificationsModels],
                               b.ItemName AS UnitName,
                               [EnabledMark],
                               [Num],
                               [Creator],
                               [CreateTime],
                               [ModifyBy],
                               [ModifyTime]
                        FROM [dbo].[EP_EquipmentSpareParts] a
                            LEFT JOIN [dbo].[V_DataDictionary] b
                                ON a.Unit = b.ItemValue
                                   AND b.EnCode = 'Unit'
                        WHERE a.EnabledMark = 1 ");
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
                //工厂
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                    //sql.Append($" AND RepairId like N'%{queryParam["RepairId"]}%'");
                }
                //报修Id 是否为空进行查询
                if (!queryParam["RepairId"].IsEmpty())
                {
                    sql.Append($" AND RepairId = N'{queryParam["RepairId"]}'");
                    //sql.Append($" AND RepairId like N'%{queryParam["RepairId"]}%'");
                }
                //设备编码 是否为空进行查询
                if (!queryParam["SparePartsId"].IsEmpty())
                {
                    //sql.Append($" AND SparePartsId = N'{queryParam["SparePartsId"]}'");
                    sql.Append($" AND SparePartsId like N'%{queryParam["SparePartsId"]}%'");
                }
                //设备名称 是否为空进行查询
                if (!queryParam["SparePartsName"].IsEmpty())
                {
                    //sql.Append($" AND SparePartsName = N'{queryParam["SparePartsName"]}'");
                    sql.Append($" AND SparePartsName like N'%{queryParam["SparePartsName"]}%'");
                }
                //类型 是否为空进行查询
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    //sql.Append($" AND SmallClass = N'{queryParam["SmallClass"]}'");
                    sql.Append($" AND SmallClass like N'%{queryParam["SmallClass"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["SpecificationsModels"].IsEmpty())
                {
                    //sql.Append($" AND SpecificationsModels = N'{queryParam["SpecificationsModels"]}'");
                    sql.Append($" AND SpecificationsModels like N'%{queryParam["SpecificationsModels"]}%'");
                }
                //数量 是否为空进行查询
                if (!queryParam["Num"].IsEmpty())
                {
                    //sql.Append($" AND Num = N'{queryParam["Num"]}'");
                    sql.Append($" AND Num like N'%{queryParam["Num"]}%'");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND UnitName like N'%{queryParam["Unit"]}%'");
                }
                //报修人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //报修时间 是否为空进行查询
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentSparePartsEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[RepairId]
                      ,[SparePartsId]
                      ,[SparePartsName]
                      ,[SmallClass]
                      ,[SpecificationsModels]
                      ,[Num]
                      ,[Unit]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentSpareParts] where EnabledMark=1  ");
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentSparePartsEntity entity, out string msg)
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
                    entity.Create();
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentSparePartsEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentSparePartsEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentSpareParts] set ");
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

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentSparePartsEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentSpareParts] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentSparePartsEntity</returns>
        public EP_EquipmentSparePartsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentSparePartsEntity</returns>
        public EP_EquipmentSparePartsEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentSparePartsEntity 对象</returns>
        public EP_EquipmentSparePartsEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentSparePartsEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentSparePartsEntity 列表</returns>
        public IEnumerable<EP_EquipmentSparePartsEntity> Get_ExpressionList(Expression<Func<EP_EquipmentSparePartsEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentSparePartsEntity> bomService = new RepositoryFactory<EP_EquipmentSparePartsEntity>();

        //    EP_EquipmentSparePartsEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentSparePartsDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentSpareParts_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentSparePartsEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentSparePartsEntity> EP_EquipmentSparePartsEntity_list = db2.FindList<EP_EquipmentSparePartsEntity>(sql.ToString());
                return EP_EquipmentSparePartsEntity_list;
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
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
                DataTable EP_EquipmentSparePartsEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentSparePartsEntity_DataTable;
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [RepairId] as '报修Id'
                      ,[SparePartsId] as '设备编码'
                      ,[SparePartsName] as '设备名称'
                      ,[SmallClass] as '类型'
                      ,[SpecificationsModels] as '规格型号'
                      ,[Num] as '数量'
                      ,[Unit] as '单位'
                      ,[Creator] as '报修人'
                      ,[CreateTime] as '报修时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentSpareParts] where EnabledMark=1  ");
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
                string saveFileName = "设备报修_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备报修", dt, true);
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
        public void RemoveForm(Expression<Func<EP_EquipmentSparePartsEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
    }
}
