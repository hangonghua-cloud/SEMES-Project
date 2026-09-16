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
using ALP.Application.UtilExtend.Util;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
//using ALP.Application.Service.QualityManage;

namespace ALP.Application.Service.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: 王坤
    /// 3.功能描述: EP_EquipmentMaintainService 业务服务类
    /// 4.任务编号: 设备保养项目维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentMaintain_Service : RepositoryFactory<EP_EquipmentMaintainEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.Id,A.Factory,C.ResourceName as FactoryName,A.EquipmentType,dbo.get_dicName('EquipmentTypes', A.EquipmentType) as EquipmentTypeName,A.EquipmentTaskId,A.EquipmentTaskName,A.Period,A.Remark,A.Creator,A.CreateTime,
B.EquipmentIdentifyCode,B.EquipmentId,D.EquipmentName,B.EquipmentIdentifyStatus,dbo.get_dicName('MaintainenceWorkOrderStatus',B.EquipmentIdentifyStatus) as EquipmentIdentifyStatusName,B.PlanDate,B.MaintainPerson,B.ActiveDate,B.Remark as Remark2
from EP_EquipmentMaintain as A
left join EP_EquipmentMaintainTask as B on A.EquipmentTaskId=B.EquipmentMaintainTaskId
left join EP_EquipmentManage as D on B.EquipmentId=D.EquipmentId
left join BS_ModelWithResource as C on A.Factory=C.ResourceCode and C.ModelLeve='Factory'
where 1=1 and A.IsUsed=1  ");
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
                //设备类别 是否为空进行查询
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentType = N'{queryParam["EquipmentType"]}'");
                    sql.Append($" AND EquipmentType like N'%{queryParam["EquipmentType"]}%'");
                }
                //保养任务编码 是否为空进行查询
                if (!queryParam["EquipmentTaskId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentTaskId = N'{queryParam["EquipmentTaskId"]}'");
                    sql.Append($" AND EquipmentTaskId like N'%{queryParam["EquipmentTaskId"]}%'");
                }
                //保养任务名称 是否为空进行查询
                if (!queryParam["EquipmentTaskName"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentTaskName = N'{queryParam["EquipmentTaskName"]}'");
                    sql.Append($" AND EquipmentTaskName like N'%{queryParam["EquipmentTaskName"]}%'");
                }
                //保养周期 是否为空进行查询
                if (!queryParam["Period"].IsEmpty())
                {
                    //sql.Append($" AND Period = N'{queryParam["Period"]}'");
                    sql.Append($" AND Period like N'%{queryParam["Period"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //是否可用 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    //sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    sql.Append($" AND IsUsed like N'%{queryParam["IsUsed"]}%'");
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            /*sql.Append(@"select A.Id,A.Factory,C.ResourceName as FactoryName,A.EquipmentType,dbo.get_dicName('EquipmentTypes', A.EquipmentType) as EquipmentTypeName,A.EquipmentTaskId,A.EquipmentTaskName,A.Period,A.Remark,A.Creator,A.CreateTime,A.IsUsed,
                        B.EquipmentIdentifyCode,B.EquipmentId,D.EquipmentName,B.EquipmentIdentifyStatus,dbo.get_dicName('MaintainenceWorkOrderStatus',B.EquipmentIdentifyStatus) as EquipmentIdentifyStatusName,B.PlanDate,B.MaintainPerson,B.ActiveDate,B.Remark as Remark2
                        from EP_EquipmentMaintain as A
                        left join EP_EquipmentMaintainTask as B on A.EquipmentTaskId=B.EquipmentMaintainTaskId
                        left join EP_EquipmentManage as D on B.EquipmentId=D.EquipmentId
                        left join BS_ModelWithResource as C on A.Factory=C.ResourceCode and C.ModelLeve='Factory'
                        where 1=1 and A.EnabledMark=1  ");*/
            sql.Append($@"SELECT A.Id,
                               A.EquipmentType,
                               dbo.get_dicName('EquipmentTypes', A.EquipmentType) AS EquipmentTypeName,
                               A.EquipmentTaskId,
                               A.EquipmentTaskName,
                               A.Period,
                               A.Remark,
                               A.Creator,
                               A.CreateTime,
                               A.IsUsed
                        FROM EP_EquipmentMaintain AS A
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
                    sql.Append($" AND A.Id = N'{queryParam["Id"]}'");
                }
                //if (!queryParam["FactoryCode"].IsEmpty())
                //{
                //    sql.Append($" AND A.FactoryCode = N'{queryParam["FactoryCode"]}'");
                //}
                //设备类别 是否为空进行查询
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentType = N'{queryParam["EquipmentType"]}'");
                    sql.Append($" AND A.EquipmentType like N'%{queryParam["EquipmentType"]}%'");
                }
                //保养任务编码 是否为空进行查询
                if (!queryParam["EquipmentTaskId"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentTaskId = N'{queryParam["EquipmentTaskId"]}'");
                    sql.Append($" AND A.EquipmentTaskId like N'%{queryParam["EquipmentTaskId"]}%'");
                }
                //保养任务名称 是否为空进行查询
                if (!queryParam["EquipmentTaskName"].IsEmpty())
                {
                    //sql.Append($" AND EquipmentTaskName = N'{queryParam["EquipmentTaskName"]}'");
                    sql.Append($" AND A.EquipmentTaskName like N'%{queryParam["EquipmentTaskName"]}%'");
                }
                //保养周期 是否为空进行查询
                if (!queryParam["Period"].IsEmpty())
                {
                    //sql.Append($" AND Period = N'{queryParam["Period"]}'");
                    sql.Append($" AND Period like N'%{queryParam["Period"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND A.Remark like N'%{queryParam["Remark"]}%'");
                }
                //是否可用 是否为空进行查询
                if (!queryParam["IsUsed"].IsEmpty())
                {
                    if (queryParam["IsUsed"].ToString() == "2")
                        queryParam["IsUsed"] = "0";
                    //sql.Append($" AND IsUsed = N'{queryParam["IsUsed"]}'");
                    sql.Append($" AND A.IsUsed like N'%{queryParam["IsUsed"]}%'");
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
                if (!queryParam["EquipmentIdentifyCode"].IsEmpty())
                {
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentIdentifyCode"]}', B.EquipmentIdentifyCode)>0 ");
                }
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentId"]}', B.EquipmentId)>0 ");
                }
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($" and CHARINDEX('{queryParam["EquipmentName"]}', B.EquipmentName)>0 ");
                }
                if (!queryParam["PlanStartDate"].IsEmpty())
                {
                    sql.Append($" and B.PlanDate>={Tools.CovertToDateStr(queryParam["PlanStartDate"])} ");
                }
                if (!queryParam["PlanEndDate"].IsEmpty())
                {
                    sql.Append($" and B.PlanDate<={Tools.CovertToNextDateStr(queryParam["PlanEndDate"])}  ");
                }
                 if (!queryParam["ActiveStartDate"].IsEmpty())
                {
                    sql.Append($" and B.ActiveDate>={Tools.CovertToDateStr(queryParam["ActiveStartDate"])} ");
                }
                if (!queryParam["ActiveEndDate"].IsEmpty())
                {
                    sql.Append($" and B.ActiveDate<={Tools.CovertToNextDateStr(queryParam["ActiveEndDate"])} ");
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.Id,A.Factory,C.ResourceName as FactoryName,A.EquipmentType,dbo.get_dicName('EquipmentTypes', A.EquipmentType) as EquipmentTypeName,A.EquipmentTaskId,A.EquipmentTaskName,A.Period,A.Remark,A.Creator,A.CreateTime,
                        B.EquipmentIdentifyCode,B.EquipmentId,D.EquipmentName,B.EquipmentIdentifyStatus,dbo.get_dicName('MaintainenceWorkOrderStatus',B.EquipmentIdentifyStatus) as EquipmentIdentifyStatusName,B.PlanDate,B.MaintainPerson,B.ActiveDate,B.Remark as Remark2
                        from EP_EquipmentMaintain as A
                        left join EP_EquipmentMaintainTask as B on A.EquipmentTaskId=B.EquipmentMaintainTaskId
                        left join EP_EquipmentManage as D on B.EquipmentId=D.EquipmentId
                        left join BS_ModelWithResource as C on A.Factory=C.ResourceCode and C.ModelLeve='Factory'
                        where 1=1 and A.EnabledMark=1  ");
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
        /// 根据设备加载任务列表
        /// </summary>
        /// <param name="equipmentId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetMaintainTaskListByEquipment(string equipmentId, ref string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"select A.EquipmentTaskId,A.EquipmentTaskName,C.EquipmentId from EP_EquipmentMaintain as A
                        left join EP_EquipmentMaintainType as B on A.Id = B.EquipmentMaintainId and B.EnabledMark = 1
                        left join(select B.ItemDetailId, B.ItemName, B.ItemValue from Base_DataItemDetail as B
                                    inner join Base_DataItem as C on B.ParentId = C.ItemId and C.ItemCode = 'EquipmentTypes') as B1 on B.EquipmentType = B1.ItemDetailId
                        left join (SELECT a.Id,
C.ResourceCode as EquipmentId,
C.ResourceName as EquipmentName,
b.ItemValue as EquipmentType
FROM 
[BS_ModelWithResource] F LEFT JOIN 
[BS_ModelWithResource] E  ON F.ResourceCode=E.ParentResource LEFT JOIN 
[BS_ModelWithResource] D ON E.ResourceCode=D.ParentResource LEFT JOIN 
[BS_ModelWithResource] C ON D.ResourceCode=C.ParentResource LEFT JOIN 
[FHMESDB].[dbo].[BS_ModelResourceExtendInfo] a ON C.ResourceCode=A.ResourceCode left join
[dbo].[V_DataDictionary] b on a.FieldValue=b.ItemValue where FieldCode='CXZL' AND B.EnCode='EquipmentTypes' AND  C.ModelLeve='Machine') as C on B1.ItemValue = C.EquipmentType 
                        where A.EnabledMark=1 and C.EquipmentId = '{equipmentId}' ");
            return this.BaseRepository().FindTable(sql.ToString());
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
            sql.Append($@"select A.ItemDetailId,A.ItemName as EquipmentTypeName,A.ItemValue as EquipmentType,C.Id,C.EquipmentMaintainId from Base_DataItemDetail as A
                        left join Base_DataItem as B on B.ItemId=A.ParentId
                        left join EP_EquipmentMaintainType as C on A.ItemDetailId=C.EquipmentType and C.EnabledMark=1 and C.EquipmentMaintainId='{id}'
                        where B.ItemCode='EquipmentTypes' ");
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentMaintainEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    entity.ModifyTime = DateTime.Now;
                    //entity.EnabledMark = true;
                    this.BaseRepository().Update(entity);
                    n = 2;
                }
                else
                {
                    EP_EquipmentMaintainEntity _entity = this.BaseRepository().FindEntity(t => t.EnabledMark == true && t.EquipmentTaskId == entity.EquipmentTaskId);
                    if (_entity == null)
                    {
                        entity.Create();
                        entity.CreateTime = DateTime.Now;
                        //entity.EquipmentType = "2";
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentMaintainEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentMaintainEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentMaintain] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentMaintain] (
                                            [Id]
                                            ,[EquipmentType]
                                            ,[EquipmentTaskId]
                                            ,[EquipmentTaskName]
                                            ,[Period]
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
                                ,N'{Save_obj.EquipmentType}'
                                ,N'{Save_obj.EquipmentTaskId}'
                                ,N'{Save_obj.EquipmentTaskName}'
                                ,N'{Save_obj.Period}'
                                ,N'{Save_obj.Remark}'
                                ,'{(Save_obj.IsUsed == true ? 1:0)}'
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
        /// 添加关联设备类型
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveChildForm(string keyValue, List<EP_EquipmentMaintainTypeEntity> list)
        {
            RepositoryFactory<EP_EquipmentMaintainTypeEntity> typeService = new RepositoryFactory<EP_EquipmentMaintainTypeEntity>();
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            int result = 0;
            typeService.BaseRepository().Delete(t => t.EquipmentMaintainId == keyValue);
            foreach (EP_EquipmentMaintainTypeEntity entity in list)
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.EquipmentMaintainId = keyValue;
                //db.Insert(entity);
            }
            //db.Commit();
            typeService.BaseRepository().Insert(list);
            result = 1;
            return result;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <param name="msg"></param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(EP_EquipmentMaintainEntity entity, ref string msg, string UpdateByName = "")
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            RepositoryFactory<EP_EquipmentMaintainDetailEntity> detailService = new RepositoryFactory<EP_EquipmentMaintainDetailEntity>();
            RepositoryFactory<EP_EquipmentSparePartsEntity> spareService = new RepositoryFactory<EP_EquipmentSparePartsEntity>();
            int result = 0;
            try
            {
                if (entity != null)
                {
                    //删除禁用标记
                    //entity.EnabledMark = false;
                    entity.ModifyBy = UpdateByName;
                    entity.ModifyTime = DateTime.Now;
                    entity.EnabledMark = false;
                    db.Update(entity);

                    List<EP_EquipmentMaintainDetailEntity> detailEntities = detailService.BaseRepository().IQueryable(t => t.EnabledMark == true && t.EquipmentTaskId == entity.EquipmentTaskId).ToList();
                    if (detailEntities.Count > 0)
                    {
                        foreach (EP_EquipmentMaintainDetailEntity _entity in detailEntities)
                        {
                            _entity.EnabledMark = false;
                            _entity.ModifyBy = UpdateByName;
                            _entity.ModifyTime = DateTime.Now;
                        }
                        db.Update(detailEntities);
                    }

                    List<EP_EquipmentSparePartsEntity> spareEntities = spareService.BaseRepository().IQueryable(t => t.EnabledMark == true && t.RepairId == entity.Id).ToList();
                    if (spareEntities.Count > 0)
                    {
                        foreach (EP_EquipmentSparePartsEntity _entity in spareEntities)
                        {
                            _entity.EnabledMark = false;
                            _entity.ModifyBy = UpdateByName;
                            _entity.ModifyTime = DateTime.Now;
                        }
                        db.Update(spareEntities);
                    }

                    db.Commit();

                    result = 1;
                }
                else
                {
                    result = 0;//没有找到记录
                }
            }
            catch(Exception ex)
            {
                msg = ex.Message;
            }
            
            return result;
        }
        
        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentMaintain] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentMaintainEntity</returns>
        public EP_EquipmentMaintainEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentMaintainEntity</returns>
        public EP_EquipmentMaintainEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentMaintainEntity 对象</returns>
        public EP_EquipmentMaintainEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentMaintainEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 王坤
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentMaintainEntity 列表</returns>
        public IEnumerable<EP_EquipmentMaintainEntity> Get_ExpressionList(Expression<Func<EP_EquipmentMaintainEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentMaintainEntity> bomService = new RepositoryFactory<EP_EquipmentMaintainEntity>();
        
        //    EP_EquipmentMaintainEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentMaintainDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentMaintain_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentMaintainEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentMaintainEntity> EP_EquipmentMaintainEntity_list =  db2.FindList<EP_EquipmentMaintainEntity>(sql.ToString());
                return EP_EquipmentMaintainEntity_list;
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
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
                DataTable EP_EquipmentMaintainEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentMaintainEntity_DataTable;
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
        /// 创建日期: 2021-08-05 14:48:01
        /// 任务编号: 设备保养项目维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        /*public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [EquipmentType] as '设备类别'
                      ,[EquipmentTaskId] as '保养任务编码'
                      ,[EquipmentTaskName] as '保养任务名称'
                      ,[Period] as '保养周期'
                      ,[Remark] as '备注'
                      ,[IsUsed] as '是否可用'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[EP_EquipmentMaintain] where IsDeleted = 0 ");
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
                string saveFileName = "设备保养项目维护_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备保养项目维护", dt, true);
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
