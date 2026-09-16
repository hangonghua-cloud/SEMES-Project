using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.QualityManage;
using ALP.Application.IService.QualityManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;

namespace ALP.Application.Service.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_IPQCDetailResultService 业务服务类
    /// 4.任务编号: 过程检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_IPQCDetailResult_Service : RepositoryFactory<QC_IPQCDetailResultEntity>, QC_IPQCDetailResult_IService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardIdr]
                      ,[TestItemCoading]
                      ,[TestItemResult]
                      ,[Remark]
                      ,[EnabledMark]
                      ,[Creator]
                      ,[CreatTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IPQCDetailResult] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //检验Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //流转卡编码 是否为空进行查询
                if (!queryParam["FlowCardIdr"].IsEmpty())
                {
                    //sql.Append($" AND FlowCardIdr = N'{queryParam["FlowCardIdr"]}'");
                    sql.Append($" AND FlowCardIdr like N'%{queryParam["FlowCardIdr"]}%'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["TestItemCoading"].IsEmpty())
                {
                    //sql.Append($" AND TestItemCoading = N'{queryParam["TestItemCoading"]}'");
                    sql.Append($" AND TestItemCoading like N'%{queryParam["TestItemCoading"]}%'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["TestItemResult"].IsEmpty())
                {
                    //sql.Append($" AND TestItemResult = N'{queryParam["TestItemResult"]}'");
                    sql.Append($" AND TestItemResult like N'%{queryParam["TestItemResult"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    //sql.Append($" AND EnabledMark = N'{queryParam["EnabledMark"]}'");
                    sql.Append($" AND EnabledMark like N'%{queryParam["EnabledMark"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["CreatTime"].IsEmpty())
                {
                    //sql.Append($" AND CreatTime = N'{queryParam["CreatTime"]}'");
                    sql.Append($" AND CreatTime like N'%{queryParam["CreatTime"]}%'");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardId]
                      ,[TestItemCoading]
                      ,[TestItemResult]
                      ,[Remark]
                      ,[EnabledMark]
                      ,[Creator]
                      ,[CreatTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IPQCDetailResult] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
     
                //流转卡编码 是否为空进行查询
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    sql.Append($" AND FlowCardId = N'{queryParam["FlowCardId"]}'");
                }
                //检验工序 是否为空进行查询
                if (!queryParam["TestItemCoading"].IsEmpty())
                {
                    //sql.Append($" AND TestItemCoading = N'{queryParam["TestItemCoading"]}'");
                    sql.Append($" AND TestItemCoading like N'%{queryParam["TestItemCoading"]}%'");
                }
                //检验机台 是否为空进行查询
                if (!queryParam["TestItemResult"].IsEmpty())
                {
                    //sql.Append($" AND TestItemResult = N'{queryParam["TestItemResult"]}'");
                    sql.Append($" AND TestItemResult like N'%{queryParam["TestItemResult"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["EnabledMark"].IsEmpty())
                {
                    //sql.Append($" AND EnabledMark = N'{queryParam["EnabledMark"]}'");
                    sql.Append($" AND EnabledMark like N'%{queryParam["EnabledMark"]}%'");
                }
                //检验员 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //检验时间 是否为空进行查询
                if (!queryParam["CreatTime"].IsEmpty())
                {
                    //sql.Append($" AND CreatTime = N'{queryParam["CreatTime"]}'");
                    sql.Append($" AND CreatTime like N'%{queryParam["CreatTime"]}%'");
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
        /// 获取检验数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCheckPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            DataTable dt = new DataTable();
            sql.Append(@"SELECT Q.[Id],
                               Q.FactoryCode,
                               Q.FactoryName,
                               Q.[FlowCardId],
                               Q.[TestItemCoading],
                               Q.[TestItemName],
                               Q.[TestItemStandard],
                               Q.[TestItemResult],
                               Q.[Remark],
                               Q.[DataType],
                               Q.[DataTypeName],
                               Q.[EnabledMark],
                               Q.[Creator],
                               Q.[CreatTime],
                               Q.[ModifyBy],
                               Q.[ModifyTime],
                               P.Name CreatorName
                        FROM [dbo].[QC_IPQCDetailResult] Q
                            LEFT JOIN dbo.BS_People P
                                ON Q.Creator = P.Code
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND Q.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["FlowCardId"].IsEmpty())
                {
                    sql.Append($" AND Q.FlowCardId = N'{queryParam["FlowCardId"]}'");
                }
                if (!queryParam["ParentId"].IsEmpty())//PDA查询用这个字段
                {
                    sql.Append($" AND Q.FlowCardId = N'{queryParam["ParentId"]}'");
                }
            }
 
            if (pagination != null)
            {
                dt = this.BaseRepository().FindTable(sql.ToString(), pagination);
            }
            else
            {
                sql.Append(@" ORDER BY TestItemCoading");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            return dt;
        }
        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FlowCardIdr]
                      ,[TestItemCoading]
                      ,[TestItemResult]
                      ,[Remark]
                      ,[EnabledMark]
                      ,[Creator]
                      ,[CreatTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[QC_IPQCDetailResult] where IsDeleted = 0 ");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_IPQCDetailResultEntity entity, out string msg)
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

        public int SaveEntity(QC_IPQCDetailEntity mainEntity, List<QC_IPQCDetailResultEntity> list, ref string msg)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            int n = 0;
            msg = "";
            try
            {
                mainEntity.EnabledMark = true;
                db.Update(mainEntity);
                foreach (QC_IPQCDetailResultEntity entity in list)
                {
                    if (!string.IsNullOrWhiteSpace(entity.Id))
                    {
                        entity.Modify(entity.Id);
                        db.Update(entity);
                    }
                    else
                    {
                        entity.Create();
                        db.Insert(entity);
                    }
                }
                db.Commit();
                n = 1;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IPQCDetailResultEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IPQCDetailResultEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[QC_IPQCDetailResult] set ");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            QC_IPQCDetailResultEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 删除记录
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int RemoveForm(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }


        public int RemoveBatchForm(string keyValue, string UpdateByName = "")
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            int result = 0;
            List<QC_IPQCDetailResultEntity> list = this.BaseRepository().IQueryable(t => t.FlowCardId == keyValue && t.EnabledMark == true).ToList();
            if (list.Count > 0)
            {
                foreach (QC_IPQCDetailResultEntity entity in list)
                {
                    //entity.EnabledMark = false;
                    entity.TestItemResult = "";
                    entity.ModifyBy = UpdateByName;
                    entity.ModifyTime = DateTime.Now;
                    db.Update(entity);
                }
                db.Commit();
                result = 1;
            }
            return result;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
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
                sql.Append($@"DELETE FROM [dbo].[QC_IPQCDetailResult] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IPQCDetailResultEntity</returns>
        public QC_IPQCDetailResultEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IPQCDetailResultEntity</returns>
        public QC_IPQCDetailResultEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailResultEntity 对象</returns>
        public QC_IPQCDetailResultEntity Get_ExpressionEntity(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailResultEntity 列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> Get_ExpressionList(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
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
        //    RepositoryFactory<QC_IPQCDetailResultEntity> bomService = new RepositoryFactory<QC_IPQCDetailResultEntity>();
        
        //    QC_IPQCDetailResultEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IPQCDetailResultDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IPQCDetailResult_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<QC_IPQCDetailResultEntity> QC_IPQCDetailResultEntity_list =  db2.FindList<QC_IPQCDetailResultEntity>(sql.ToString());
                return QC_IPQCDetailResultEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
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
                DataTable QC_IPQCDetailResultEntity_DataTable = db2.FindTable(sql.ToString());
                return QC_IPQCDetailResultEntity_DataTable;
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FlowCardIdr] as '流转卡编码'
                      ,[TestItemCoading] as '检验工序'
                      ,[TestItemResult] as '检验机台'
                      ,[Remark] as '备注'
                      ,[EnabledMark] as '附件'
                      ,[Creator] as '检验员'
                      ,[CreatTime] as '检验时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[QC_IPQCDetailResult] where IsDeleted = 0 ");
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
                string saveFileName = "过程检验记录表_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                /*ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("过程检验记录表", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();*/
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
