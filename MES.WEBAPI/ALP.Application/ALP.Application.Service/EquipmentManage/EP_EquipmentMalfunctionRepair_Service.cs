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
using ALP.Application.Entity.Material;

namespace ALP.Application.Service.EquipmentManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentMalfunctionRepairService 业务服务类
    /// 4.任务编号: 设备报修
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMalfunctionRepair_Service : RepositoryFactory<EP_EquipmentMalfunctionRepairEntity>
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
        public IEnumerable<EP_EquipmentMalfunctionRepairEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[RepairingType]
                      ,[RepairingStatus]
                      ,[MalfunctionDescription]
                      ,[TimeLength]
                      ,[RepairingContent]
                      ,[RepairingPerson]
                      ,[FinishTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentMalfunctionRepair] where EnabledMark=1  ");
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
            sql.Append(@"SELECT ER.[Id],
                               ER.FactoryCode,
                               ER.FactoryName,
                               ER.[EquipmentId],
                               ER.[EquipmentName],
                               ER.[RepairingType],
                               ER.[RepairingStatus],
                               ER.[MalfunctionDescription],
                               ER.[TimeLength],
                               ER.[RepairingContent],
                               ER.[RepairingPerson],
                               ER.[FinishTime],
                               ER.[Creator],
                               ER.[CreateTime],
                               ER.[ModifyBy],
                               ER.[ModifyTime],
                               EM.TestMethodCoadinName,
                               EM.ProcessBelong,
                               EM.InstallationSiteName,
                               M2.ResourceName ProcessName,
                               M3.ResourceName WorkshopName,
                               P1.Name CreatorName,
                               P2.Name RepairingPersonName
                        FROM [dbo].[EP_EquipmentMalfunctionRepair] ER
                            LEFT JOIN [dbo].[V_EP_EquipmentManage] EM
                                ON ER.EquipmentId = EM.EquipmentId
                            LEFT JOIN dbo.BS_ModelWithResource M2
                                ON EM.ProcessBelong = M2.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource M3
                                ON EM.InstallationSite = M3.ResourceCode
                            LEFT JOIN dbo.BS_People P1
                                ON P1.Code = ER.Creator
                            LEFT JOIN dbo.BS_People P2
                                ON P2.Code = ER.RepairingPerson
                        WHERE ER.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND ER.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["InstallationSite"].IsEmpty())
                {
                    sql.Append($" AND EM.InstallationSite = N'{queryParam["InstallationSite"]}'");
                }
                //设备编码 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentId = N'{queryParam["EquipmentId"]}'");
                    sql.Append($" AND ER.EquipmentId like N'%{queryParam["EquipmentId"]}%'");
                }
                //设备名称 是否为空进行查询
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentName = N'{queryParam["EquipmentName"]}'");
                    sql.Append($" AND ER.EquipmentName like N'%{queryParam["EquipmentName"]}%'");
                }
                //报修类别 是否为空进行查询
                if (!queryParam["RepairingType"].IsEmpty())
                {
                    sql.Append($" AND ER.RepairingType = N'{queryParam["RepairingType"]}'");
                }
                //报修状态 是否为空进行查询
                if (!queryParam["RepairingStatus"].IsEmpty())
                {
                    sql.Append($" AND ER.RepairingStatus = N'{queryParam["RepairingStatus"]}'");
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


                //报修时间 是否为空进行查询
                if (!queryParam["StartDate"].IsEmpty())
                {
                    sql.Append($" AND ER.CreateTime >= N'{queryParam["StartDate"]}'");
                }
                if (!queryParam["EndDate"].IsEmpty())
                {
                    sql.Append($" AND ER.CreateTime <= N'{queryParam["EndDate"]}'");
                }

                if (!queryParam["FinishStartTime"].IsEmpty())
                {
                    sql.Append($" AND ER.FinishTime >= N'{queryParam["FinishStartTime"]}'");
                }
                if (!queryParam["FinishEndTime"].IsEmpty())
                {
                    sql.Append($" AND ER.FinishTime <= N'{queryParam["FinishEndTime"]}'");
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
        public IEnumerable<EP_EquipmentMalfunctionRepairEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[RepairingType]
                      ,[RepairingStatus]
                      ,[MalfunctionDescription]
                      ,[TimeLength]
                      ,[RepairingContent]
                      ,[RepairingPerson]
                      ,[FinishTime]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentMalfunctionRepair] where EnabledMark=1  ");
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

        public IEnumerable<Base_MaterialEntity> GetMaterialPageListWithClass(Pagination pagination, string queryJson)
        {
            RepositoryFactory<Base_MaterialEntity> materialService = new RepositoryFactory<Base_MaterialEntity>();
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string materialClass = queryParam["MaterialClass"] == null ? "" : queryParam["MaterialClass"].ToString();
            string materialCode = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string materialName = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            sql.Append($@"select Id,MaterialCode,MaterialName,Spec,Unit,dbo.get_dicName('Unit',Unit) as UnitName 
from Base_Material 
where IsEnabled=1 ");
            if (!string.IsNullOrWhiteSpace(materialClass))
            {
                sql.Append($"  and MaterialClass in ('{materialClass}') ");
            }
            if (!string.IsNullOrWhiteSpace(materialCode))
            {
                sql.Append($" and CHARINDEX('{materialCode}',MaterialCode)>0 ");
            }
            if (!string.IsNullOrWhiteSpace(materialName))
            {
                sql.Append($" and CHARINDEX('{materialName}',MaterialName)>0 ");
            }
            return materialService.BaseRepository().FindList(sql.ToString(), pagination);
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
        public int SaveEntity(string keyValue, EP_EquipmentMalfunctionRepairEntity entity, out string msg)
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
        /// 创建日期: 2021-09-24 15:51:22
        /// 任务编号: 设备报修
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentMalfunctionRepairEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentMalfunctionRepairEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentMalfunctionRepair] set ");
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
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentMalfunctionRepair] (
                                            [Id]
                                            ,[EquipmentId]
                                            ,[EquipmentName]
                                            ,[RepairingType]
                                            ,[RepairingStatus]
                                            ,[MalfunctionDescription]
                                            ,[TimeLength]
                                            ,[RepairingContent]
                                            ,[RepairingPerson]
                                            ,[FinishTime]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.EquipmentId}'
                                ,N'{Save_obj.EquipmentName}'
                                ,N'{Save_obj.RepairingType}'
                                ,N'{Save_obj.RepairingStatus}'
                                ,N'{Save_obj.MalfunctionDescription}'
                                ,N'{Save_obj.TimeLength}'
                                ,N'{Save_obj.RepairingContent}'
                                ,N'{Save_obj.RepairingPerson}'
                                ,'{(Save_obj.FinishTime == null ? DateTime.Now : Save_obj.FinishTime)}'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null ? DateTime.Now : Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null ? DateTime.Now : Save_obj.ModifyTime)}'
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
            EP_EquipmentMalfunctionRepairEntity entity = this.BaseRepository().FindEntity(keyValue);
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentMalfunctionRepair] WHERE Id=N'{keyValue}'");
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
        /// <returns>返回EP_EquipmentMalfunctionRepairEntity</returns>
        public EP_EquipmentMalfunctionRepairEntity GetEntity(string keyValue)
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
        /// <returns>返回EP_EquipmentMalfunctionRepairEntity</returns>
        public EP_EquipmentMalfunctionRepairEntity GetEntityByQuery(string QueryField)
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
        /// <returns>返回EP_EquipmentMalfunctionRepairEntity 对象</returns>
        public EP_EquipmentMalfunctionRepairEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentMalfunctionRepairEntity, bool>> condition)
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
        /// <returns>返回EP_EquipmentMalfunctionRepairEntity 列表</returns>
        public IEnumerable<EP_EquipmentMalfunctionRepairEntity> Get_ExpressionList(Expression<Func<EP_EquipmentMalfunctionRepairEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentMalfunctionRepairEntity> bomService = new RepositoryFactory<EP_EquipmentMalfunctionRepairEntity>();

        //    EP_EquipmentMalfunctionRepairEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentMalfunctionRepairDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentMalfunctionRepair_Id == entity.Id).FirstOrDefault();
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
        public IEnumerable<EP_EquipmentMalfunctionRepairEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentMalfunctionRepairEntity> EP_EquipmentMalfunctionRepairEntity_list = db2.FindList<EP_EquipmentMalfunctionRepairEntity>(sql.ToString());
                return EP_EquipmentMalfunctionRepairEntity_list;
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
                DataTable EP_EquipmentMalfunctionRepairEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentMalfunctionRepairEntity_DataTable;
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
                      [EquipmentId] as '设备编码'
                      ,[EquipmentName] as '设备名称'
                      ,[RepairingType] as '报修类别'
                      ,[RepairingStatus] as '报修状态'
                      ,[MalfunctionDescription] as '故障描述'
                      ,[TimeLength] as '维修时长'
                      ,[RepairingContent] as '维修内容'
                      ,[RepairingPerson] as '维修人'
                      ,[FinishTime] as '维修时间'
                      ,[Creator] as '报修人'
                      ,[CreateTime] as '报修时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentMalfunctionRepair] where EnabledMark=1  ");
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

        public void RemoveForm(Expression<Func<EP_EquipmentMalfunctionRepairEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }

        #region App获取报修记录
        /// <summary>
        /// APP设备故障报修- 获取设备报修记录
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<dynamic> GetEquipmentMalfunctionRepair(string queryJson)
        {
            var sql = new StringBuilder();

            sql.Append(@"SELECT EMR.Id,
                               EM.EquipmentId,
                               EM.EquipmentName,
                               EMR.MalfunctionDescription,
                               V.ItemName RepairingTypeName,
                               CONVERT(VARCHAR(16), EMR.CreateTime, 120) CreateTime,
                               BP.Name CreatorName
                        FROM [dbo].[EP_EquipmentMalfunctionRepair] EMR
                            INNER JOIN [dbo].[V_EP_EquipmentManage] EM
                                ON EM.EquipmentId = EMR.EquipmentId
                            LEFT JOIN dbo.BS_People BP
                                ON EMR.Creator = BP.Code
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'RepairsCategory'
                                   AND V.ItemValue = EMR.RepairingType
                        WHERE EMR.RepairingStatus = '1' ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["InstallationSite"].IsEmpty())
                {
                    sql.Append($" AND EM.InstallationSite = N'{queryParam["InstallationSite"]}'");
                }
                if (!queryParam["RepairingType"].IsEmpty())
                {
                    sql.Append($" AND EMR.RepairingType = N'{queryParam["RepairingType"]}'");
                }
                if (!queryParam["WorkShop"].IsEmpty())
                {
                    sql.Append($" AND EM.InstallationSite = N'{queryParam["WorkShop"]}'");
                }

                if (!queryParam["StartDate"].IsEmpty() && !queryParam["EndDate"].IsEmpty())
                {
                    sql.Append($"  AND EMR.CreateTime BETWEEN N'{queryParam["StartDate"]}' AND  N'{queryParam["EndDate"]} 23:59:59'");
                }
            }
            return this.BaseRepository().Query(sql.ToString());
        }
        #endregion

    }
}
