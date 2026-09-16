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
    /// 3.功能描述: EP_EquipmentCheckResultService 业务服务类
    /// 4.任务编号: 设备点检结果
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentCheckResult_Service : RepositoryFactory<EP_EquipmentCheckResultEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckResultEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      A.[Id]
                      ,A.[ParentId]
                      ,A.[CheckTaskId]
                      ,A.[CheckItemId]
					  ,B.[CheckItemName]
                      ,A.[CheckItemStandard]
					  ,A.[CheckResult]
                      ,case when B.DataType='string' or B.DataType='decimal' then A.[CheckResult] when B.DataType='bool' and A.CheckResult='1' then '合格' when B.DataType='bool' and A.CheckResult='2' then '不合格' end as CheckResultValue
                      ,A.[EnabledMark]
                      ,A.[Creator]
                      ,A.[CreateTime]
                      ,A.[ModifyBy]
                      ,A.[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckResult] as A 
				  left join EP_EquipmentCheckItemDetail as B on A.CheckItemId=B.CheckItemId and B.EnabledMark=1
				  where A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["ParentId"].IsEmpty())
                {
                    sql.Append($" AND ParentId = N'{queryParam["ParentId"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //点检任务编码 是否为空进行查询
                if (!queryParam["CheckTaskId"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskId = N'{queryParam["CheckTaskId"]}'");
                    sql.Append($" AND A.CheckTaskId like N'%{queryParam["CheckTaskId"]}%'");
                }
                //点检项目编码 是否为空进行查询
                if (!queryParam["CheckItemId"].IsEmpty())
                {
                    //sql.Append($" AND CheckItemId = N'{queryParam["CheckItemId"]}'");
                    sql.Append($" AND A.CheckItemId like N'%{queryParam["CheckItemId"]}%'");
                }
                //点检项目标准 是否为空进行查询
                if (!queryParam["CheckItemStandard"].IsEmpty())
                {
                    //sql.Append($" AND CheckItemStandard = N'{queryParam["CheckItemStandard"]}'");
                    sql.Append($" AND A.CheckItemStandard like N'%{queryParam["CheckItemStandard"]}%'");
                }
                //点检结果 是否为空进行查询
                if (!queryParam["CheckResult"].IsEmpty())
                {
                    //sql.Append($" AND CheckResult = N'{queryParam["CheckResult"]}'");
                    sql.Append($" AND A.CheckResult like N'%{queryParam["CheckResult"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND A.Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND A.CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND A.ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND A.ModifyTime like N'%{queryParam["ModifyTime"]}%'");
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.[ParentId],
							   a.FactoryCode,
							   a.FactoryName,
                               A.[CheckTaskId],
                               A.[CheckItemId],
                               B.[CheckItemName],
                               A.[CheckItemStandard],
                               A.[CheckResult],
                               CASE
                                   WHEN B.DataType = '1'
                                        OR B.DataType = '2'
                                        OR B.DataType = '3' THEN
                                       A.[CheckResult]
                                   WHEN B.DataType = '4'
                                        AND A.CheckResult = '1' THEN
                                       '合格'
                                   WHEN B.DataType = '4'
                                        AND A.CheckResult = '2' THEN
                                       '不合格'
                                   WHEN B.DataType = '5'
                                        AND A.CheckResult = '1' THEN
                                       '是'
                                   WHEN B.DataType = '5'
                                        AND A.CheckResult = '2' THEN
                                       '否'
                                   WHEN B.DataType = '6'
                                        AND A.CheckResult = '1' THEN
                                       '正常'
                                   WHEN B.DataType = '6'
                                        AND A.CheckResult = '2' THEN
                                       '否'
                               END AS CheckResultValue,
                               A.[EnabledMark],
                               A.[Creator],
                               A.[CreateTime],
                               A.[ModifyBy],
                               A.[ModifyTime],
	                           c.CheckTaskName
                        FROM [dbo].[EP_EquipmentCheckResult] AS A
                            LEFT JOIN dbo.EP_EquipmentCheckItemDetail AS B
                                ON A.CheckItemId = B.CheckItemId
                                   AND B.EnabledMark = 1
                                   AND A.CheckTaskId = B.CheckTaskId
                            LEFT JOIN dbo.EP_EquipmentCheckRecord c ON a.ParentId=c.Id
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                if (!queryParam["ParentId"].IsEmpty())
                {
                    sql.Append($" AND ParentId = N'{queryParam["ParentId"]}'");
                    //sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //点检任务编码 是否为空进行查询
                if (!queryParam["CheckTaskId"].IsEmpty())
                {
                    //sql.Append($" AND CheckTaskId = N'{queryParam["CheckTaskId"]}'");
                    sql.Append($" AND A.CheckTaskId like N'%{queryParam["CheckTaskId"]}%'");
                }
                //点检项目编码 是否为空进行查询
                if (!queryParam["CheckItemId"].IsEmpty())
                {
                    //sql.Append($" AND CheckItemId = N'{queryParam["CheckItemId"]}'");
                    sql.Append($" AND A.CheckItemId like N'%{queryParam["CheckItemId"]}%'");
                }
                //点检项目标准 是否为空进行查询
                if (!queryParam["CheckItemStandard"].IsEmpty())
                {
                    //sql.Append($" AND CheckItemStandard = N'{queryParam["CheckItemStandard"]}'");
                    sql.Append($" AND A.CheckItemStandard like N'%{queryParam["CheckItemStandard"]}%'");
                }
                //点检结果 是否为空进行查询
                if (!queryParam["CheckResult"].IsEmpty())
                {
                    //sql.Append($" AND CheckResult = N'{queryParam["CheckResult"]}'");
                    sql.Append($" AND A.CheckResult like N'%{queryParam["CheckResult"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND A.Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND A.CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND A.ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND A.ModifyTime like N'%{queryParam["ModifyTime"]}%'");
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckResultEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      A.[Id]
                      ,A.[ParentId]
                      ,A.[CheckTaskId]
                      ,A.[CheckItemId]
					  ,B.[CheckItemName]
                      ,A.[CheckItemStandard]
					  ,A.[CheckResult]
                      ,case when B.DataType='string' or B.DataType='decimal' then A.[CheckResult] when B.DataType='bool' and A.CheckResult='1' then '合格' when B.DataType='bool' and A.CheckResult='2' then '不合格' end as CheckResultValue
                      ,A.[EnabledMark]
                      ,A.[Creator]
                      ,A.[CreateTime]
                      ,A.[ModifyBy]
                      ,A.[ModifyTime]
                  FROM [dbo].[EP_EquipmentCheckResult] as A 
				  left join EP_EquipmentCheckItemDetail as B on A.CheckItemId=B.CheckItemId and B.EnabledMark=1
				  where A.EnabledMark = 1 ");
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
        
        public DataTable GetResultPageList(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string parentId = queryParam["ParentId"] == null ? "" : queryParam["ParentId"].ToString();
            string checkTaskId = queryParam["CheckTaskId"] == null ? "" : queryParam["CheckTaskId"].ToString();
            /*sql.Append($@"select C.Id,B.CheckTaskId,B.CheckTaskName,A.CheckItemId,A.CheckItemName,A.CheckItemStandard,A.DataType,C.CheckResult 
                        --,case when A.DataType='string' or A.DataType='decimal' then C.[CheckResult] when A.DataType='bool' and C.CheckResult='1' then '合格' when A.DataType='bool' and C.CheckResult='2' then '不合格' end as CheckResultValue  
                        from EP_EquipmentCheckItemDetail as A
                        left join EP_EquipmentCheckItemMaintenance as B on A.CheckTaskId=B.CheckTaskId
                        left join EP_EquipmentCheckResult as C on C.CheckItemId=A.CheckItemId and C.CheckTaskId='{checkTaskId}'  
						where A.CheckTaskId='{checkTaskId}'   ");*/// and C.ParentId='{}'
            sql.Append($@"select C.Id,B.CheckTaskId,B.CheckTaskName,A.CheckItemId,A.CheckItemName,A.CheckItemStandard,A.DataType,C.CheckResult 
                        --,case when A.DataType='string' or A.DataType='decimal' then C.[CheckResult] when A.DataType='bool' and C.CheckResult='1' then '合格' when A.DataType='bool' and C.CheckResult='2' then '不合格' end as CheckResultValue  
                        from EP_EquipmentCheckItemDetail as A
                        left join EP_EquipmentCheckItemMaintenance as B on A.CheckTaskId=B.CheckTaskId and B.EnabledMark=1
                        left join EP_EquipmentCheckResult as C on C.CheckItemId=A.CheckItemId and C.CheckTaskId='{checkTaskId}' and C.EnabledMark=1 ");
            if (string.IsNullOrWhiteSpace(parentId))
                sql.Append($@" and C.ParentId is null ");
            else
                sql.Append($@" and C.ParentId='{parentId}' ");
            sql.Append($@" where A.CheckTaskId='{checkTaskId}' and B.IsUsed=1 and A.EnabledMark=1 ");
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentCheckResultEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
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
        /// 批量保存表单（新增、修改）
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="list"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public int SaveEntity(EP_EquipmentCheckRecordEntity recordEntity, List<EP_EquipmentCheckResultEntity> list,out string msg)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            int n = 0;
            msg = "";
            try
            {
                foreach(EP_EquipmentCheckResultEntity entity in list)
                {
                    if (!string.IsNullOrWhiteSpace(entity.Id))
                    {
                        entity.ModifyTime = DateTime.Now;
                        entity.EnabledMark = true;
                        entity.ParentId = recordEntity.Id;
                        db.Update(entity);
                    }
                    else
                    {
                        entity.Id = Guid.NewGuid().ToString();
                        entity.ParentId = recordEntity.Id;
                        entity.CreateTime = DateTime.Now;
                        db.Insert(entity);
                    }
                }
                db.Commit();
                n = 1;
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentCheckResultEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentCheckResultEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentCheckResult] set ");
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
                                    if (x.Name == "EnabledMark")
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentCheckResultEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentCheckResult] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentCheckResultEntity</returns>
        public EP_EquipmentCheckResultEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentCheckResultEntity</returns>
        public EP_EquipmentCheckResultEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckResultEntity 对象</returns>
        public EP_EquipmentCheckResultEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentCheckResultEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentCheckResultEntity 列表</returns>
        public IEnumerable<EP_EquipmentCheckResultEntity> Get_ExpressionList(Expression<Func<EP_EquipmentCheckResultEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentCheckResultEntity> bomService = new RepositoryFactory<EP_EquipmentCheckResultEntity>();
        
        //    EP_EquipmentCheckResultEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentCheckResultDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentCheckResult_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentCheckResultEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentCheckResultEntity> EP_EquipmentCheckResultEntity_list =  db2.FindList<EP_EquipmentCheckResultEntity>(sql.ToString());
                return EP_EquipmentCheckResultEntity_list;
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
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
                DataTable EP_EquipmentCheckResultEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentCheckResultEntity_DataTable;
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
        /// 创建日期: 2021-08-05 14:43:55
        /// 任务编号: 设备点检结果
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        /*public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [CheckTaskId] as '点检任务编码'
                      ,[CheckItemId] as '点检项目编码'
                      ,[CheckItemStandard] as '点检项目标准'
                      ,[CheckResult] as '点检结果'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentCheckResult] where IsDeleted = 0 ");
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
                string saveFileName = "设备点检结果_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备点检结果", dt, true);
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
