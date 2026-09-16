using System;
using System.Collections.Generic;
using ALP.Application.UtilExtend.Util;
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
    /// 3.功能描述: EP_EquipmentCheckRecordService 业务服务类
    /// 4.任务编号: 设备点检记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentCheckRecord_Service : RepositoryFactory<EP_EquipmentCheckRecordEntity>
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[CheckTaskId]
                      ,[CheckTaskName]
					  ,[CheckConclusion]
                      ,case when [CheckConclusion]='1' then '合格' when [CheckConclusion]='2' then '不合格' end as CheckConclusionName
                      ,[TypeInTeam]
                      ,[TypeInPerson]
                      ,[TypeInTime]
                      ,[EnabledMark]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckRecord] where EnabledMark = 1 ");
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
                //点检结论 是否为空进行查询
                if (!queryParam["CheckConclusion"].IsEmpty())
                {
                    //sql.Append($" AND CheckConclusion = N'{queryParam["CheckConclusion"]}'");
                    sql.Append($" AND CheckConclusion like N'%{queryParam["CheckConclusion"]}%'");
                }
                //录入班组 是否为空进行查询
                if (!queryParam["TypeInTeam"].IsEmpty())
                {
                    //sql.Append($" AND TypeInTeam = N'{queryParam["TypeInTeam"]}'");
                    sql.Append($" AND TypeInTeam like N'%{queryParam["TypeInTeam"]}%'");
                }
                //录入人 是否为空进行查询
                if (!queryParam["TypeInPerson"].IsEmpty())
                {
                    //sql.Append($" AND TypeInPerson = N'{queryParam["TypeInPerson"]}'");
                    sql.Append($" AND TypeInPerson like N'%{queryParam["TypeInPerson"]}%'");
                }
                //录入时间 是否为空进行查询
                if (!queryParam["TypeInTime"].IsEmpty())
                {
                    //sql.Append($" AND TypeInTime = N'{queryParam["TypeInTime"]}'");
                    sql.Append($" AND TypeInTime like N'%{queryParam["TypeInTime"]}%'");
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
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.FactoryCode,
                               A.FactoryName,
                               A.[EquipmentId],
                               A.[EquipmentName],
                               A.[CheckTaskId],
                               A.[CheckTaskName],
                               A.[CheckConclusion],
                               CASE
                                   WHEN [CheckConclusion] = '1' THEN
                                       '合格'
                                   WHEN [CheckConclusion] = '2' THEN
                                       '不合格'
                               END AS CheckConclusionName,
                               A.[TypeInTeam],
                               A.[TypeInPerson],
                               A.[TypeInTime],
                               A.[EnabledMark],
                               A.[ModifyBy],
                               A.[ModifyTime]
                        FROM [dbo].[EP_EquipmentCheckRecord] AS A
                            LEFT JOIN dbo.V_EP_EquipmentManage AS B
                                ON A.EquipmentId = B.EquipmentId
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //设备编码 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentId = N'{queryParam["EquipmentId"]}'");
                    sql.Append($" AND A.EquipmentId like N'%{queryParam["EquipmentId"]}%'");
                }
                //设备名称 是否为空进行查询
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentName = N'{queryParam["EquipmentName"]}'");
                    sql.Append($" AND A.EquipmentName like N'%{queryParam["EquipmentName"]}%'");
                }
                //点检任务编码 是否为空进行查询
                if (!queryParam["CheckTaskId"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskId = N'{queryParam["CheckTaskId"]}'");
                    sql.Append($" AND A.CheckTaskId like N'%{queryParam["CheckTaskId"]}%'");
                }
                //点检任务名称 是否为空进行查询
                if (!queryParam["CheckTaskName"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskName = N'{queryParam["CheckTaskName"]}'");
                    sql.Append($" AND A.CheckTaskName like N'%{queryParam["CheckTaskName"]}%'");
                }
                //点检结论 是否为空进行查询
                if (!queryParam["CheckConclusion"].IsEmpty())
                {
                    sql.Append($" AND A.CheckConclusion = N'{queryParam["CheckConclusion"]}'");
                    //sql.Append($" AND A.CheckConclusion like N'%{queryParam["CheckConclusion"]}%'");
                }
                //录入班组 是否为空进行查询
                if (!queryParam["TypeInTeam"].IsEmpty())
                {
                    //sql.Append($" AND TypeInTeam = N'{queryParam["TypeInTeam"]}'");
                    sql.Append($" AND TypeInTeam like N'%{queryParam["TypeInTeam"]}%'");
                }
                //录入人 是否为空进行查询
                if (!queryParam["TypeInPerson"].IsEmpty())
                {
                    //sql.Append($" AND TypeInPerson = N'{queryParam["TypeInPerson"]}'");
                    sql.Append($" AND TypeInPerson like N'%{queryParam["TypeInPerson"]}%'");
                }
                //录入时间 是否为空进行查询
                if (!queryParam["TypeInTime"].IsEmpty())
                {
                    //sql.Append($" AND TypeInTime = N'{queryParam["TypeInTime"]}'");
                    sql.Append($" AND TypeInTime like N'%{queryParam["TypeInTime"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND TypeInTime >= '{Tools.CovertToDateStr(queryParam["StartTime"])}'");
                    //sql.Append($" AND TypeInTime like N'%{queryParam["TypeInTime"]}%'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND TypeInTime <= '{Tools.CovertToNextDateStr(queryParam["EndTime"])}'");
                    //sql.Append($" AND TypeInTime like N'%{queryParam["EndTime"]}%'");
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
        /// 获取任务列表
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetCheckTaskByEquipmentList(string equipmentId, ref string msg)
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();
            msg = "";
            try
            {
                sql.Append($@"select distinct c.CheckTaskId,c.CheckTaskName,d.EquipmentId from EP_EquipmentCheckItemMaintenanceType as A
 left join EP_EquipmentCheckItemMaintenance as C on A.EquipmentMaintenanceId=C.Id 
                            left join (
SELECT a.Id,
C.ResourceCode as EquipmentId,
C.ResourceName as EquipmentName,
b.ItemValue as EquipmentType
FROM 
[BS_ModelWithResource] F LEFT JOIN 
[BS_ModelWithResource] E  ON F.ResourceCode=E.ParentResource LEFT JOIN 
[BS_ModelWithResource] D ON E.ResourceCode=D.ParentResource LEFT JOIN 
[BS_ModelWithResource] C ON D.ResourceCode=C.ParentResource LEFT JOIN 
[FHMESDB].[dbo].[BS_ModelResourceExtendInfo] a ON C.ResourceCode=A.ResourceCode left join
[dbo].[V_DataDictionary] b on a.FieldValue=b.ItemValue where FieldCode='CXZL' AND B.EnCode='EquipmentTypes' AND  C.ModelLeve='Machine' ) as D on A.EquipmentType=D.EquipmentType and D.ID is not null
where 1=1 and C.IsUsed=1 and C.EnabledMark=1");
                if (!equipmentId.IsEmpty())
                {
                    sql.Append($@" and D.EquipmentId = N'{equipmentId}' ");
                }


                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckRecordEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[CheckTaskId]
                      ,[CheckTaskName]
					  ,[CheckConclusion]
                      ,case when [CheckConclusion]='1' then '合格' when [CheckConclusion]='2' then '不合格' end as CheckConclusionName
                      ,[TypeInTeam]
                      ,[TypeInPerson]
                      ,[TypeInTime]
                      ,[EnabledMark]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckRecord] where EnabledMark = 1 ");
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="id"></param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentCheckRecordEntity entity, out string id, out string msg)
        {
            int n = 0;
            msg = "";
            id = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.ModifyTime = DateTime.Now;
                    entity.EnabledMark = true;
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    id = entity.Id;
                    entity.TypeInTime = DateTime.Now;
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentCheckRecordEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentCheckRecordEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentCheckRecord] set ");
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
                                    if (x.Name == "EnabledMark")
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
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentCheckRecord] (
                                            [Id]
                                            ,[EquipmentId]
                                            ,[EquipmentName]
                                            ,[CheckTaskId]
                                            ,[CheckTaskName]
                                            ,[CheckConclusion]
                                            ,[TypeInTeam]
                                            ,[TypeInPerson]
                                            ,[TypeInTime]
                                            ,[EnabledMark]
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
                                ,N'{Save_obj.CheckTaskId}'
                                ,N'{Save_obj.CheckTaskName}'
                                ,N'{Save_obj.CheckConclusion}'
                                ,N'{Save_obj.TypeInTeam}'
                                ,N'{Save_obj.TypeInPerson}'
                                ,'{(Save_obj.TypeInTime == null ? DateTime.Now : Save_obj.TypeInTime)}'
                                ,1
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentCheckRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                entity.EnabledMark = false;
                entity.ModifyTime = DateTime.Now;
                entity.ModifyBy = UpdateByName;
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentCheckRecord] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentCheckRecordEntity</returns>
        public EP_EquipmentCheckRecordEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentCheckRecordEntity</returns>
        public EP_EquipmentCheckRecordEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckRecordEntity 对象</returns>
        public EP_EquipmentCheckRecordEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentCheckRecordEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckRecordEntity 列表</returns>
        public IEnumerable<EP_EquipmentCheckRecordEntity> Get_ExpressionList(Expression<Func<EP_EquipmentCheckRecordEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentCheckRecordEntity> bomService = new RepositoryFactory<EP_EquipmentCheckRecordEntity>();

        //    EP_EquipmentCheckRecordEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentCheckRecordDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentCheckRecord_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckRecordEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentCheckRecordEntity> EP_EquipmentCheckRecordEntity_list = db2.FindList<EP_EquipmentCheckRecordEntity>(sql.ToString());
                return EP_EquipmentCheckRecordEntity_list;
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
        /// 创建日期: 2021-08-05 14:42:41
        /// 任务编号: 设备点检记录
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
                DataTable EP_EquipmentCheckRecordEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentCheckRecordEntity_DataTable;
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

    
        #region 设备点检查询PDA

        /// <summary>
        /// 设备点检查询PDA
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<dynamic> GetDataTableCheckResult(string queryJson)
        {
            try
            {
                var sql = new StringBuilder();

                sql.Append(@"SELECT 
	                            CR.EquipmentName,CR.CheckTaskName,
	                            CASE CR.CheckConclusion WHEN '1' THEN '合格' ELSE '不合格' END CheckConclusionName,
	                            CONVERT(VARCHAR(16),CR.TypeInTime,120) TypeInTime,
	                            ED.CheckItemName,
	                            ECR.CheckItemStandard,
	                            ECR.CheckResult
                               FROM  [dbo].[V_EP_EquipmentManage] EP
                               INNER JOIN [dbo].[EP_EquipmentCheckRecord] CR ON EP.EquipmentId=CR.EquipmentId
                               INNER JOIN [dbo].[EP_EquipmentCheckResult] ECR ON CR.Id=ECR.ParentId
                               INNER JOIN dbo.EP_EquipmentCheckItemDetail ED ON ED.CheckTaskId=CR.CheckTaskId AND ED.CheckItemId=ECR.CheckItemId
                               WHERE 1=1 ");

                //WHERE EP.ProcessBelong='FH_JC' AND EP.EquipmentType ='6' AND DATEDIFF(DAY,CR.TypeInTime,'2021-09-16')=0
                var parameter = new List<DbParameter>();
                if (!string.IsNullOrEmpty(queryJson))
                {
                    JObject queryParam = queryJson.ToJObject();
                    if (!queryParam["ProcessBelong"].IsEmpty())
                    {
                        sql.Append($" AND EP.ProcessBelong = N'{queryParam["ProcessBelong"]}'");
                    }
                    if (!queryParam["EquipmentType"].IsEmpty())
                    {
                        sql.Append($" AND EP.EquipmentType = N'{queryParam["EquipmentType"]}'");
                    }
                    if (!queryParam["StartTime"].IsEmpty())
                    {
                        sql.Append($" AND DATEDIFF(DAY,CR.TypeInTime,N'{queryParam["StartTime"]}')=0");
                    }
                }
                //return this.BaseRepository().Query(sql.ToString(), parameter.ToArray());
                return this.BaseRepository().Query(sql.ToString());
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion 

    }
}
