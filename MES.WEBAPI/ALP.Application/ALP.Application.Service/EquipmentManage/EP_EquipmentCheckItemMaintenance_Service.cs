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
//using ALP.Application.Service.QualityManage;

namespace ALP.Application.Service.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentCheckItemMaintenanceService 业务服务类
    /// 4.任务编号: 设备点检维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentCheckItemMaintenance_Service : RepositoryFactory<EP_EquipmentCheckItemMaintenanceEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckItemMaintenanceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[Factory]
					  ,B.[ResourceName] as FactoryName
                      ,[EquipmentType]
                      ,[CheckTaskId]
                      ,[CheckTaskName]
                      ,[Remark]
					  ,dbo.get_dicName('EffectivState', [IsUsed]) as IsUsed
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckItemMaintenance] as A
				  left join BS_ModelWithResource as B on A.Factory=B.ResourceCode
				  where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工厂 是否为空进行查询
                if (!queryParam["Factory"].IsEmpty())
                {
                    //sql.Append($" AND Factory = N'{queryParam["Factory"]}'");
                    sql.Append($" AND Factory like N'%{queryParam["Factory"]}%'");
                }
                //设备类别 是否为空进行查询
                if (!queryParam["EqupmentType"].IsEmpty())
                {
                    //sql.Append($" AND EqupmentType = N'{queryParam["EqupmentType"]}'");
                    sql.Append($" AND EqupmentType like N'%{queryParam["EqupmentType"]}%'");
                }
                //点检任务编码 是否为空进行查询
                if (!queryParam["CheckTaskId"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskId = N'{queryParam["CheckTaskId"]}'");
                    sql.Append($" AND CheckTaskId like N'%{queryParam["CheckTaskId"]}%'");
                }
                //点检任务名称 是否为空进行查询
                if (!queryParam["CheckTaskName"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskName = N'{queryParam["CheckTaskName"]}'");
                    sql.Append($" AND CheckTaskName like N'%{queryParam["CheckTaskName"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //状态 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    //sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT [Id],
                               [EquipmentType],
                               [CheckTaskId],
                               [CheckTaskName],
                               [Remark],
                               [IsUsed],
                               dbo.get_dicName('EffectivState', [IsUsed]) AS IsUsedName,
                               [Creator],
                               [CreateTime],
                               [ModifyBy],
                               [ModifyTime]
                        FROM [dbo].[EP_EquipmentCheckItemMaintenance] AS A
                        WHERE 1 = 1
                              AND A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                ////工厂 是否为空进行查询
                //if (!queryParam["FactoryCode"].IsEmpty())
                //{
                //    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                //}
                //工厂 是否为空进行查询
                if (!queryParam["Factory"].IsEmpty())
                {
                    //sql.Append($" AND Factory = N'{queryParam["Factory"]}'");
                    sql.Append($" AND Factory like N'%{queryParam["Factory"]}%'");
                }
                //设备类别 是否为空进行查询
                if (!queryParam["EqupmentType"].IsEmpty())
                {
                    //sql.Append($" AND EqupmentType = N'{queryParam["EqupmentType"]}'");
                    sql.Append($" AND EqupmentType like N'%{queryParam["EqupmentType"]}%'");
                }
                //点检任务编码 是否为空进行查询
                if (!queryParam["CheckTaskId"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskId = N'{queryParam["CheckTaskId"]}'");
                    sql.Append($" AND CheckTaskId like N'%{queryParam["CheckTaskId"]}%'");
                }
                //点检任务名称 是否为空进行查询
                if (!queryParam["CheckTaskName"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskName = N'{queryParam["CheckTaskName"]}'");
                    sql.Append($" AND CheckTaskName like N'%{queryParam["CheckTaskName"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //状态 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    //sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckItemMaintenanceEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[Factory]
                      ,[EqupmentType]
                      ,[CheckTaskId]
                      ,[CheckTaskName]
                      ,[Remark]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckItemMaintenance] where IsDeleted = 0 ");
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
        /// 获取关联的设备类型
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetTypePageList(string id)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"select A.ItemValue as ItemDetailId ,A.ItemName as EquipmentTypeName,A.ItemValue as EquipmentType,C.Id,C.EquipmentMaintenanceId from V_DataDictionary as A
                        left join EP_EquipmentCheckItemMaintenanceType as C
						 on A.ItemValue=C.EquipmentType and C.EnabledMark=1 and c.EquipmentMaintenanceId ='{id}'
                        where a.encode='EquipmentTypes'  ");
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 获取所有设备类型
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetAllEquipmentType(Pagination pagination, string queryJson, ref string msg)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string id = queryParam["Id"] == null ? "" : queryParam["Id"].ToString();
            string equipmentType = "";
            try
            {
                sql.Append($@"select EquipmentType from EP_EquipmentCheckItemMaintenance where Id='{id}' and IsUsed=1 ");
                DataTable dtType = this.BaseRepository().FindTable(sql.ToString());
                if (dtType.Rows.Count > 0)
                {
                    foreach (DataRow row in dtType.Rows)
                    {
                        equipmentType += row["EquipmentType"].ToString() + "','";
                    }
                    equipmentType = equipmentType.Substring(0, equipmentType.Length - 3);

                    sql.Clear();
                    sql.Append($@"select A.ItemDetailId,A.ItemName,A.ItemValue from Base_DataItemDetail as A
                                left join Base_DataItem as B on B.ItemId=A.ParentId
                                where B.ItemCode='EquipmentTypes' and ItemDetailId not in ('{equipmentType}')");
                    dt = this.BaseRepository().FindTable(sql.ToString(), pagination);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentCheckItemMaintenanceEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.ModifyTime = DateTime.Now;
                    //entity.IsUsed = "1";
                    entity.EnabledMark = true;
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    EP_EquipmentCheckItemMaintenanceEntity _entity = this.BaseRepository().FindEntity(t => t.EnabledMark == true && t.CheckTaskId == entity.CheckTaskId);
                    if (_entity == null)
                    {
                        entity.Create();
                        entity.CreateTime = DateTime.Now;
                        this.BaseRepository().Insert(entity);
                        n = 1;
                    }
                    else
                    {
                        n = 3;
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
        /// 添加关联设备类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveChildForm(string keyValue, List<EP_EquipmentCheckItemMaintenanceTypeEntity> list)
        {
            RepositoryFactory<EP_EquipmentCheckItemMaintenanceTypeEntity> typeService = new RepositoryFactory<EP_EquipmentCheckItemMaintenanceTypeEntity>();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            int result = 0;
            typeService.BaseRepository().Delete(t => t.EquipmentMaintenanceId == keyValue);
            foreach (EP_EquipmentCheckItemMaintenanceTypeEntity entity in list)
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.EquipmentMaintenanceId = keyValue;
                //db.Insert(entity);
            }
            //db.Commit();
            typeService.BaseRepository().Insert(list);
            result = 1;
            return result;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentCheckItemMaintenanceEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentCheckItemMaintenanceEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentCheckItemMaintenance] set ");
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
                                    if (x.Name == "IsUsed")
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
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentCheckItemMaintenance] (
                                            [Id]
                                            ,[Factory]
                                            ,[EqupmentType]
                                            ,[CheckTaskId]
                                            ,[CheckTaskName]
                                            ,[Remark]
                                            ,[IsUsed]
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
                                ,N'{Save_obj.Factory}'
                                ,N'{Save_obj.EquipmentType}'
                                ,N'{Save_obj.CheckTaskId}'
                                ,N'{Save_obj.CheckTaskName}'
                                ,N'{Save_obj.Remark}'
                                ,'1'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
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
                //日志服务类
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            RepositoryFactory<EP_EquipmentCheckItemDetailEntity> detailService = new RepositoryFactory<EP_EquipmentCheckItemDetailEntity>();
            int result = 0;
            EP_EquipmentCheckItemMaintenanceEntity entity = this.BaseRepository().FindEntity(keyValue);
            EP_EquipmentCheckItemDetailEntity childEntity = detailService.BaseRepository().FindEntity(t => t.CheckTaskId == entity.CheckTaskId);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
                db.Update(entity);
                if (childEntity != null)
                {
                    childEntity.EnabledMark = false;
                    db.Update(childEntity);
                }
                db.Commit();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentCheckItemMaintenance] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentCheckItemMaintenanceEntity</returns>
        public EP_EquipmentCheckItemMaintenanceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentCheckItemMaintenanceEntity</returns>
        public EP_EquipmentCheckItemMaintenanceEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckItemMaintenanceEntity 对象</returns>
        public EP_EquipmentCheckItemMaintenanceEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentCheckItemMaintenanceEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckItemMaintenanceEntity 列表</returns>
        public IEnumerable<EP_EquipmentCheckItemMaintenanceEntity> Get_ExpressionList(Expression<Func<EP_EquipmentCheckItemMaintenanceEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentCheckItemMaintenanceEntity> bomService = new RepositoryFactory<EP_EquipmentCheckItemMaintenanceEntity>();
        
        //    EP_EquipmentCheckItemMaintenanceEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentCheckItemMaintenanceDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentCheckItemMaintenance_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckItemMaintenanceEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentCheckItemMaintenanceEntity> EP_EquipmentCheckItemMaintenanceEntity_list =  db2.FindList<EP_EquipmentCheckItemMaintenanceEntity>(sql.ToString());
                return EP_EquipmentCheckItemMaintenanceEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
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
                DataTable EP_EquipmentCheckItemMaintenanceEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentCheckItemMaintenanceEntity_DataTable;
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:41:27
        /// 任务编号: 设备点检维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        /*public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Factory] as '工厂'
                      ,[EqupmentType] as '设备类别'
                      ,[CheckTaskId] as '点检任务编码'
                      ,[CheckTaskName] as '点检任务名称'
                      ,[Remark] as '备注'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentCheckItemMaintenance] where IsDeleted = 0 ");
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
                string saveFileName = "设备点检维护_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备点检维护", dt, true);
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
        }*/
        
    }
}
