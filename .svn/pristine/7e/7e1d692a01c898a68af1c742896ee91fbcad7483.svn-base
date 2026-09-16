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
    /// 3.功能描述: EP_EquipmentToolService 业务服务类
    /// 4.任务编号: 设备刀具更换记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentTool_Service : RepositoryFactory<EP_EquipmentToolEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentToolEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[ToolId]
                      ,[ToolsName]
                      ,[SpecificationsModels]
                      ,[Factory]
                      ,[Remark]
                      ,[IsUsed]
                      ,[DateOfReplace]
                      ,[PersonOfReplace]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentTool] where EnabledMark = 0 ");
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
                //刀具编码 是否为空进行查询
                if (!queryParam["ToolId"].IsEmpty())
                {
                    //sql.Append($" AND ToolId = N'{queryParam["ToolId"]}'");
                    sql.Append($" AND ToolId like N'%{queryParam["ToolId"]}%'");
                }
                //刀具名称 是否为空进行查询
                if (!queryParam["ToolsName"].IsEmpty())
                {
                    //sql.Append($" AND ToolsName = N'{queryParam["ToolsName"]}'");
                    sql.Append($" AND ToolsName like N'%{queryParam["ToolsName"]}%'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["SpecificationsModels"].IsEmpty())
                {
                    //sql.Append($" AND SpecificationsModels = N'{queryParam["SpecificationsModels"]}'");
                    sql.Append($" AND SpecificationsModels like N'%{queryParam["SpecificationsModels"]}%'");
                }
                //生产厂家 是否为空进行查询
                if (!queryParam["Factory"].IsEmpty())
                {
                    //sql.Append($" AND Factory = N'{queryParam["Factory"]}'");
                    sql.Append($" AND Factory like N'%{queryParam["Factory"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //使用状态 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    //sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    sql.Append($" AND IsUsed like N'%{queryParam["IsUsed"]}%'");
                }
                //更换日期 是否为空进行查询
                if (!queryParam["DateOfReplace"].IsEmpty())
                {
                    //sql.Append($" AND DateOfReplace = N'{queryParam["DateOfReplace"]}'");
                    sql.Append($" AND DateOfReplace like N'%{queryParam["DateOfReplace"]}%'");
                }
                //更换人 是否为空进行查询
                if (!queryParam["PersonOfReplace"].IsEmpty())
                {
                    //sql.Append($" AND PersonOfReplace = N'{queryParam["PersonOfReplace"]}'");
                    sql.Append($" AND PersonOfReplace like N'%{queryParam["PersonOfReplace"]}%'");
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.EnabledMark,
                               A.Id,
                               A.FactoryCode,
                               A.FactoryName,
                               A.EquipmentId,
                               A.EquipmentName,
                               A.ToolId,
                               A.ToolsName,
                               A.SpecificationsModels,
                               A.Manufacturer,
                               A.Remark,
                               A.IsUsed,
                               A.DateOfReplace,
                               A.PersonOfReplace,
                               C.Name AS PersonOfReplaceName,
                               dbo.get_dicName('EffectiveState', A.IsUsed) AS StatusName,
                               A.LineCode,
                               A.LineName
                        FROM EP_EquipmentTool AS A
                            LEFT JOIN BS_People AS C
                                ON A.PersonOfReplace = C.Code
                                   AND C.IsEnabled = 1
                        WHERE A.EnabledMark = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //主键 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
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
                //刀具编码 是否为空进行查询
                if (!queryParam["ToolId"].IsEmpty())
                {
                    //sql.Append($" AND ToolId = N'{queryParam["ToolId"]}'");
                    sql.Append($" AND ToolId like N'%{queryParam["ToolId"]}%'");
                }
                //刀具名称 是否为空进行查询
                if (!queryParam["ToolsName"].IsEmpty())
                {
                    //sql.Append($" AND ToolsName = N'{queryParam["ToolsName"]}'");
                    sql.Append($" AND ToolsName like N'%{queryParam["ToolsName"]}%'");
                }
                //刀具编码 是否为空进行查询
                if (!queryParam["LineCode"].IsEmpty())
                {
                    //sql.Append($" AND ToolId = N'{queryParam["ToolId"]}'");
                    sql.Append($"  and CHARINDEX('{queryParam["LineCode"]}', A.LineCode)>0 ");
                }
                //刀具名称 是否为空进行查询
                if (!queryParam["LineName"].IsEmpty())
                {
                    //sql.Append($" AND ToolsName = N'{queryParam["ToolsName"]}'");
                    sql.Append($"  and CHARINDEX('{queryParam["LineName"]}', A.LineName)>0 ");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["SpecificationsModels"].IsEmpty())
                {
                    //sql.Append($" AND SpecificationsModels = N'{queryParam["SpecificationsModels"]}'");
                    sql.Append($" AND SpecificationsModels like N'%{queryParam["SpecificationsModels"]}%'");
                }
                //生产厂家 是否为空进行查询
                if (!queryParam["Manufacturer"].IsEmpty())
                {
                    //sql.Append($" AND SpecificationsModels = N'{queryParam["SpecificationsModels"]}'");
                    sql.Append($" AND Manufacturer like N'%{queryParam["Manufacturer"]}%'");
                }
                //生产厂家 是否为空进行查询
                if (!queryParam["Factory"].IsEmpty())
                {
                    //sql.Append($" AND Factory = N'{queryParam["Factory"]}'");
                    sql.Append($" AND Factory like N'%{queryParam["Factory"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //使用状态 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    //sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    sql.Append($" AND IsUsed like N'%{queryParam["IsUsed"]}%'");
                }
                //更换日期 是否为空进行查询
                if (!queryParam["DateOfReplace"].IsEmpty())
                {
                    //sql.Append($" AND DateOfReplace = N'{queryParam["DateOfReplace"]}'");
                    sql.Append($" AND DateOfReplace like N'%{queryParam["DateOfReplace"]}%'");
                }
                if (!queryParam["StartTime"].IsEmpty())
                {
                    //sql.Append($" AND DateOfReplace = N'{queryParam["DateOfReplace"]}'");
                    sql.Append($" AND DateOfReplace >= '{Tools.CovertToDateStr(queryParam["StartTime"])}'");
                }
                if (!queryParam["EndTime"].IsEmpty())
                {
                    //sql.Append($" AND DateOfReplace = N'{queryParam["DateOfReplace"]}'");
                    sql.Append($" AND DateOfReplace < '{Tools.CovertToNextDateStr(queryParam["EndTime"])}'");
                }
                //更换人 是否为空进行查询
                if (!queryParam["PersonOfReplace"].IsEmpty())
                {
                    //sql.Append($" AND PersonOfReplace = N'{queryParam["PersonOfReplace"]}'");
                    sql.Append($" AND PersonOfReplace like N'%{queryParam["PersonOfReplace"]}%'");
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentToolEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[ToolId]
                      ,[ToolsName]
                      ,[SpecificationsModels]
                      ,[Factory]
                      ,[Remark]
                      ,[IsUsed]
                      ,[DateOfReplace]
                      ,[PersonOfReplace]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentTool] where EnabledMark = 0 ");
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
        
        public DataTable GetLinePageList(Pagination pagination, string queryJson)
        {
            DataTable dt = new DataTable();
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string factory = queryParam["Factory"] == null ? "" : queryParam["Factory"].ToString();
            string parent = queryParam["ParentCode"] == null ? "" : queryParam["ParentCode"].ToString();
            string queryCode = queryParam["queryCode"] == null ? "" : queryParam["queryCode"].ToString();
            string queryName = queryParam["queryName"] == null ? "" : queryParam["queryName"].ToString();
            sql.Append($@"select A.ResourceCode as LineCode,A.ResourceName as LineName,B.ResourceCode as ProcessCode,B.ResourceName as ProcessName,
                        C.ResourceCode as WorkshopCode,C.ResourceName as WorkshopName,D.ResourceCode as FactoryCode,D.ResourceName as FactoryName 
                        from BS_ModelWithResource as A
                        left join BS_ModelWithResource as B on A.ParentResource=B.ResourceCode and B.ModelLeve='Process' and B.EnabledMark=1
                        left join BS_ModelWithResource as C on B.ParentResource=C.ResourceCode and C.ModelLeve='Workshop' and C.EnabledMark=1
                        left join BS_ModelWithResource as D on C.ParentResource=D.ResourceCode and D.ModelLeve='Factory' and D.EnabledMark=1
                        where A.EnabledMark=1 and A.ModelLeve='Machine'");
            if (!string.IsNullOrWhiteSpace(factory))
                sql.Append($@" and D.ResourceCode='{factory}' ");
            if (!string.IsNullOrWhiteSpace(parent))
                sql.Append($@" and A.ParentResource='{parent}' ");
            if (!string.IsNullOrWhiteSpace(queryCode))
                sql.Append($@" and CHARINDEX('{queryCode}', A.ResourceCode)>0 ");
            if (!string.IsNullOrWhiteSpace(queryName))
                sql.Append($@" and CHARINDEX('{queryName}', A.ResourceName)>0 ");
            if (pagination != null)
                dt = this.BaseRepository().FindTable(sql.ToString(), pagination);
            else
                dt = this.BaseRepository().FindTable(sql.ToString());
            return dt;
        }
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentToolEntity entity, out string msg)
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
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    entity.Id = Guid.NewGuid().ToString();
                    entity.ModifyTime = DateTime.Now;
                    entity.EnabledMark = true;
                    this.BaseRepository().Insert(entity);
                    n = 1;
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentToolEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentToolEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentTool] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentTool] (
                                            [Id]
                                            ,[EquipmentId]
                                            ,[EquipmentName]
                                            ,[ToolId]
                                            ,[ToolsName]
                                            ,[SpecificationsModels]
                                            ,[Factory]
                                            ,[Remark]
                                            ,[IsUsed]
                                            ,[DateOfReplace]
                                            ,[PersonOfReplace]
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
                                ,N'{Save_obj.ToolId}'
                                ,N'{Save_obj.ToolsName}'
                                ,N'{Save_obj.SpecificationsModels}'
                                ,N'{Save_obj.Factory}'
                                ,N'{Save_obj.Remark}'
                                ,'{(Save_obj.IsUsed == true ? 1:0)}'
                                ,'{(Save_obj.DateOfReplace == null? DateTime.Now:Save_obj.DateOfReplace)}'
                                ,N'{Save_obj.PersonOfReplace}'
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentToolEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentTool] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentToolEntity</returns>
        public EP_EquipmentToolEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentToolEntity</returns>
        public EP_EquipmentToolEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentToolEntity 对象</returns>
        public EP_EquipmentToolEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentToolEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentToolEntity 列表</returns>
        public IEnumerable<EP_EquipmentToolEntity> Get_ExpressionList(Expression<Func<EP_EquipmentToolEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentToolEntity> bomService = new RepositoryFactory<EP_EquipmentToolEntity>();
        
        //    EP_EquipmentToolEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentToolDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentTool_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentToolEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentToolEntity> EP_EquipmentToolEntity_list =  db2.FindList<EP_EquipmentToolEntity>(sql.ToString());
                return EP_EquipmentToolEntity_list;
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
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
                DataTable EP_EquipmentToolEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentToolEntity_DataTable;
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
        /// 创建日期: 2021-08-05 14:47:05
        /// 任务编号: 设备刀具更换记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        /*public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [EquipmentId] as '设备编码'
                      ,[EquipmentName] as '设备名称'
                      ,[ToolId] as '刀具编码'
                      ,[ToolsName] as '刀具名称'
                      ,[SpecificationsModels] as '规格型号'
                      ,[Factory] as '生产厂家'
                      ,[Remark] as '备注'
                      ,[IsUsed] as '使用状态'
                      ,[DateOfReplace] as '更换日期'
                      ,[PersonOfReplace] as '更换人'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentTool] where IsDeleted = 0 ");
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
                string saveFileName = "设备刀具更换记录_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备刀具更换记录", dt, true);
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
