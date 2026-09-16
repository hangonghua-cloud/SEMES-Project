using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.ProduceManage;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.ProduceManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-19
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_ProductionFirstInspectionDetailService 业务服务类
    /// 4.任务编号: 生产首检
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_ProductionFirstInspectionDetail_Service : RepositoryFactory<PM_ProductionFirstInspectionDetailEntity>, PM_ProductionFirstInspectionDetailIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FirstInspectionId],TestItemId
                      ,[TestItemCoading]
                      ,[TestItemName]
                      ,[DataType]
                      ,[DataTypeName]
                      ,[TestItemStandard]
                      ,[WorkShopResult]
                      ,[QualityResult]
                  FROM [dbo].[PM_ProductionFirstInspectionDetail] where 1=1 ");
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
                //订单号 是否为空进行查询
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    //sql.Append($" AND ProductOrder = N'{queryParam["ProductOrder"]}'");
                    sql.Append($" AND ProductOrder like N'%{queryParam["ProductOrder"]}%'");
                }
                //工单号 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND WorkOrder = N'{queryParam["WorkOrder"]}'");
                    sql.Append($" AND WorkOrder like N'%{queryParam["WorkOrder"]}%'");
                }
                //执行工单号 是否为空进行查询
                if (!queryParam["ExeWorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND ExeWorkOrder = N'{queryParam["ExeWorkOrder"]}'");
                    sql.Append($" AND ExeWorkOrder like N'%{queryParam["ExeWorkOrder"]}%'");
                }
                //首检工序编码 是否为空进行查询
                if (!queryParam["FirstProcessMachine"].IsEmpty())
                {
                    //sql.Append($" AND FirstProcessMachine = N'{queryParam["FirstProcessMachine"]}'");
                    sql.Append($" AND FirstProcessMachine like N'%{queryParam["FirstProcessMachine"]}%'");
                }
                //首检机台 是否为空进行查询
                if (!queryParam["FirstMachine"].IsEmpty())
                {
                    //sql.Append($" AND FirstMachine = N'{queryParam["FirstMachine"]}'");
                    sql.Append($" AND FirstMachine like N'%{queryParam["FirstMachine"]}%'");
                }
                //质量复检标记 是否为空进行查询
                if (!queryParam["SecondMark"].IsEmpty())
                {
                    //sql.Append($" AND SecondMark = N'{queryParam["SecondMark"]}'");
                    sql.Append($" AND SecondMark like N'%{queryParam["SecondMark"]}%'");
                }
                //车间首检结果 是否为空进行查询
                if (!queryParam["FirstResult"].IsEmpty())
                {
                    //sql.Append($" AND FirstResult = N'{queryParam["FirstResult"]}'");
                    sql.Append($" AND FirstResult like N'%{queryParam["FirstResult"]}%'");
                }
                //车间首检人 是否为空进行查询
                if (!queryParam["FirstUser"].IsEmpty())
                {
                    //sql.Append($" AND FirstUser = N'{queryParam["FirstUser"]}'");
                    sql.Append($" AND FirstUser like N'%{queryParam["FirstUser"]}%'");
                }
                //车间首检时间 是否为空进行查询
                if (!queryParam["FirstTime"].IsEmpty())
                {
                    //sql.Append($" AND FirstTime = N'{queryParam["FirstTime"]}'");
                    sql.Append($" AND FirstTime like N'%{queryParam["FirstTime"]}%'");
                }
                //质量复检结果 是否为空进行查询
                if (!queryParam["SecondResult"].IsEmpty())
                {
                    //sql.Append($" AND SecondResult = N'{queryParam["SecondResult"]}'");
                    sql.Append($" AND SecondResult like N'%{queryParam["SecondResult"]}%'");
                }
                //质量复检人 是否为空进行查询
                if (!queryParam["SecondUser"].IsEmpty())
                {
                    //sql.Append($" AND SecondUser = N'{queryParam["SecondUser"]}'");
                    sql.Append($" AND SecondUser like N'%{queryParam["SecondUser"]}%'");
                }
                //质量复检时间 是否为空进行查询
                if (!queryParam["SecondTime"].IsEmpty())
                {
                    //sql.Append($" AND SecondTime = N'{queryParam["SecondTime"]}'");
                    sql.Append($" AND SecondTime like N'%{queryParam["SecondTime"]}%'");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                               a.FirstInspectionId,
                               a.TestItemId,
                               a.TestItemCoading,
                               a.TestItemName,
                               a.DataType,
                               a.DataTypeName,
                               a.TestItemStandard,
                               a.WorkShopResult,
                               a.QualityResult,
                               a.InspectClass,
                               a.Creator,
                               b.Name CreatorName,
                               a.CreateTime,
                               a.TestDepartment,
                               CASE a.TestDepartment
                                   WHEN '1' THEN
                                       '实验室'
                                   WHEN '2' THEN
                                       '质量部'
                                   WHEN '3' THEN
                                       '生产部'
                                   ELSE
                                       ''
                               END TestDepartmentName,
                               a.TestItemResult,
                               a.EnabledMark
                        FROM dbo.PM_ProductionFirstInspectionDetail a
                            LEFT JOIN dbo.BS_People b
                                ON a.Creator = b.Code
                        WHERE 1 = 1 ");
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
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //首检ID 是否为空进行查询
                if (!queryParam["FirstInspectionId"].IsEmpty())
                {
                    //sql.Append($" AND FirstInspectionId = N'{queryParam["FirstInspectionId"]}'");
                    sql.Append($" AND FirstInspectionId like N'%{queryParam["FirstInspectionId"]}%'");
                }
                //首检项目编码 是否为空进行查询
                if (!queryParam["TestItemCoading"].IsEmpty())
                {
                    //sql.Append($" AND TestItemCoading = N'{queryParam["TestItemCoading"]}'");
                    sql.Append($" AND TestItemCoading like N'%{queryParam["TestItemCoading"]}%'");
                }
                //首检项目名称 是否为空进行查询
                if (!queryParam["TestItemName"].IsEmpty())
                {
                    //sql.Append($" AND TestItemName = N'{queryParam["TestItemName"]}'");
                    sql.Append($" AND TestItemName like N'%{queryParam["TestItemName"]}%'");
                }
                //检测类型 是否为空进行查询
                if (!queryParam["DataType"].IsEmpty())
                {
                    //sql.Append($" AND DataType = N'{queryParam["DataType"]}'");
                    sql.Append($" AND DataType like N'%{queryParam["DataType"]}%'");
                }
                //检测类型名称 是否为空进行查询
                if (!queryParam["DataTypeName"].IsEmpty())
                {
                    //sql.Append($" AND DataTypeName = N'{queryParam["DataTypeName"]}'");
                    sql.Append($" AND DataTypeName like N'%{queryParam["DataTypeName"]}%'");
                }
                //首检标准 是否为空进行查询
                if (!queryParam["TestItemStandard"].IsEmpty())
                {
                    //sql.Append($" AND TestItemStandard = N'{queryParam["TestItemStandard"]}'");
                    sql.Append($" AND TestItemStandard like N'%{queryParam["TestItemStandard"]}%'");
                }
                //车间结果 是否为空进行查询
                if (!queryParam["WorkShopResult"].IsEmpty())
                {
                    //sql.Append($" AND WorkShopResult = N'{queryParam["WorkShopResult"]}'");
                    sql.Append($" AND WorkShopResult like N'%{queryParam["WorkShopResult"]}%'");
                }
                //质量结果 是否为空进行查询
                if (!queryParam["QualityResult"].IsEmpty())
                {
                    //sql.Append($" AND QualityResult = N'{queryParam["QualityResult"]}'");
                    sql.Append($" AND QualityResult like N'%{queryParam["QualityResult"]}%'");
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

                if (!queryParam["TestDepartment"].IsEmpty())
                {
                    sql.Append($" AND TestDepartment like N'%{queryParam["TestDepartment"]}%'");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionDetailEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FirstInspectionId],TestItemId
                      ,[TestItemCoading]
                      ,[TestItemName]
                      ,[DataType]
                      ,[DataTypeName]
                      ,[TestItemStandard]
                      ,[WorkShopResult]
                      ,[QualityResult]
                  FROM [dbo].[PM_ProductionFirstInspectionDetail] where 1=1 ");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_ProductionFirstInspectionDetailEntity entity, out string msg)
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_ProductionFirstInspectionDetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ProductionFirstInspectionDetailEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_ProductionFirstInspectionDetail] set ");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                sql.Append($@"DELETE FROM [dbo].[PM_ProductionFirstInspectionDetail] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_ProductionFirstInspectionDetailEntity</returns>
        public PM_ProductionFirstInspectionDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_ProductionFirstInspectionDetailEntity</returns>
        public PM_ProductionFirstInspectionDetailEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ProductionFirstInspectionDetailEntity 对象</returns>
        public PM_ProductionFirstInspectionDetailEntity Get_ExpressionEntity(Expression<Func<PM_ProductionFirstInspectionDetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_ProductionFirstInspectionDetailEntity 列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionDetailEntity> Get_ExpressionList(Expression<Func<PM_ProductionFirstInspectionDetailEntity, bool>> condition)
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
        //    RepositoryFactory<PM_ProductionFirstInspectionDetailEntity> bomService = new RepositoryFactory<PM_ProductionFirstInspectionDetailEntity>();
        
        //    PM_ProductionFirstInspectionDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_ProductionFirstInspectionDetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_ProductionFirstInspectionDetail_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_ProductionFirstInspectionDetailEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_ProductionFirstInspectionDetailEntity> PM_ProductionFirstInspectionDetailEntity_list =  db2.FindList<PM_ProductionFirstInspectionDetailEntity>(sql.ToString());
                return PM_ProductionFirstInspectionDetailEntity_list;
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
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
                DataTable PM_ProductionFirstInspectionDetailEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_ProductionFirstInspectionDetailEntity_DataTable;
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
        /// 创建日期: 2021-08-19 15:57:26
        /// 任务编号: 生产首检
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FirstInspectionId] as '首检ID'
                      ,[TestItemCoading] as '首检项目编码'
                      ,[TestItemName] as '首检项目名称'
                      ,[DataType] as '检测类型'
                      ,[DataTypeName] as '检测类型名称'
                      ,[TestItemStandard] as '首检标准'
                      ,[WorkShopResult] as '车间结果'
                      ,[QualityResult] as '质量结果'
                  FROM [dbo].[PM_ProductionFirstInspectionDetail] where 1=1 ");
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
                string saveFileName = "生产首检_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("生产首检", dt, true);
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

        public int RemoveForm(Expression<Func<PM_ProductionFirstInspectionDetailEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        #region PDA 检验记录查询
        /// <summary>
        /// 车间首检、质量首检 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<dynamic> FirstInspectionQueryResult(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" 
SELECT 
		PF.Id,PF.ProductOrder,PF.WorkOrder,PF.ExeWorkOrder,PF.FirstProcessCode,PF.FirstMachine,PF.InspectClass,CONVERT(VARCHAR(10),PF.CreateTime,120)CreateTime,
		M.ResourceName ProcessName,M1.ResourceName MachineName, 
		 CASE ISNULL(PF.Determination, '0')
                                   WHEN '0' THEN
                                       '未完成'
                                   ELSE
                                       '已完成'
                               END Determination,                --质量状态名称
		PD.TestItemCoading,PD.TestItemName,PD.DataTypeName,PD.TestItemStandard,PD.WorkShopResult,PD.QualityResult,PD.TestDepartment,V.ItemName TestDepartmentName,
		PW.ContainerNO
	 FROM dbo.PM_ProductionFirstInspection PF
	 INNER JOIN dbo.PM_ProductionFirstInspectionDetail PD ON PF.Id=PD.FirstInspectionId
	 LEFT JOIN dbo.PL_WorkOrder PW ON PW.WorkOrder = PF.WorkOrder
	 LEFT JOIN dbo.BS_ModelWithResource M ON M.ResourceCode=PF.FirstProcessCode
	 LEFT JOIN dbo.BS_ModelWithResource M1 ON M1.ResourceCode=PF.FirstMachine
	 LEFT JOIN dbo.V_DataDictionary V ON V.EnCode='AssayDepartment' AND v.ItemValue=PD.TestDepartment
                         where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                 
                if (!queryParam["InspectClass"].IsEmpty())
                {
                    sql.Append($" AND PF.InspectClass = N'{queryParam["InspectClass"]}'");
                }
                if (!queryParam["FirstProcessCode"].IsEmpty())
                {
                    sql.Append($" AND PF.FirstProcessCode = N'{queryParam["FirstProcessCode"]}'");
                }
                if (!queryParam["StartDate"].IsEmpty())
                {
                    sql.Append($" AND PF.CreateTime >= N'{queryParam["StartDate"]}'");
                }
                if (!queryParam["EndDate"].IsEmpty())
                {
                    sql.Append($" AND PF.CreateTime <= N'{queryParam["EndDate"]}' 23:59:59");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PW.ContainerNO = N'{queryParam["ContainerNO"]}'");
                }
 
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PF.ProductOrder Like N'%{queryParam["ProductOrder"]}%'");
                }

                sql.Append(@" Order by PD.TestDepartment");

            }
            try
            {
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
