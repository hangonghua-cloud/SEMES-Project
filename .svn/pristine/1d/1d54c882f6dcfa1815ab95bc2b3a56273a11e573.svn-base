using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.SAP;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;
using ALP.Data;
using ALP.Application.UtilExtend.Util;

namespace ALP.Application.Service.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2022-11-02
    /// 2.创建作者: jpf
    /// 3.功能描述: BS_TraitDetailsService 业务服务类
    /// 4.任务编号: 特征维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_TraitDetails_Service : RepositoryFactory<BS_TraitDetailsEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_TraitDetailsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[TraitManageID]
                      ,[TraitValue]
                      ,[IsDeleted]
                      ,[Creator]
                      ,[CreateName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[BS_TraitDetails] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //工厂名称 是否为空进行查询
                if (!queryParam["FactoryName"].IsEmpty())
                {
                    //sql.Append($" AND FactoryName = N'{queryParam["FactoryName"]}'");
                    sql.Append($" AND FactoryName like N'%{queryParam["FactoryName"]}%'");
                }
                //特征名称 是否为空进行查询
                if (!queryParam["TraitName"].IsEmpty())
                {
                    //sql.Append($" AND TraitName = N'{queryParam["TraitName"]}'");
                    sql.Append($" AND TraitName like N'%{queryParam["TraitName"]}%'");
                }
                //是否启用 是否为空进行查询
                if (!queryParam["IsEnable"].IsEmpty())
                {
                    //sql.Append($" AND IsEnable = N'{queryParam["IsEnable"]}'");
                    sql.Append($" AND IsEnable like N'%{queryParam["IsEnable"]}%'");
                }
                //默认为0删除为1 是否为空进行查询
                if (!queryParam["IsDeleted"].IsEmpty())
                {
                    //sql.Append($" AND IsDeleted = N'{queryParam["IsDeleted"]}'");
                    sql.Append($" AND IsDeleted like N'%{queryParam["IsDeleted"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND ModifyName like N'%{queryParam["ModifyName"]}%'");
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
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT A.[Id],
                               A.[TraitManageID],
                               A.[TraitValue],
                               A.AttrModleCode,
                               A.[IsDeleted],
                               A.[Creator],
                               A.[CreateName],
                               CONVERT(VARCHAR(20), A.CreateTime, 120) CreateTime,
                               A.[ModifyBy],
                               CONVERT(VARCHAR(20), A.ModifyTime, 120) ModifyTime,
                               A.[ModifyName],
                               b.TraitCode,
                               b.TraitName
                        FROM [dbo].[BS_TraitDetails] A
                            INNER JOIN dbo.BS_TraitManage b
                                ON A.TraitManageID = b.Id
                        WHERE A.IsDeleted = 0");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND A.Id like N'%{queryParam["Id"]}%'");
                }
                //主表关联ID 是否为空进行查询
                if (!queryParam["TraitManageID"].IsEmpty())
                {
                    //sql.Append($" AND TraitManageID = N'{queryParam["TraitManageID"]}'");
                    sql.Append($" AND A.TraitManageID like N'%{queryParam["TraitManageID"]}%'");
                }
                //特征值 是否为空进行查询
                if (!queryParam["TraitValue"].IsEmpty())
                {
                    //sql.Append($" AND TraitValue = N'{queryParam["TraitValue"]}'");
                    sql.Append($" AND A.TraitValue like N'%{queryParam["TraitValue"]}%'");
                }
                //删除标识默认为0,删除1 是否为空进行查询
                if (!queryParam["IsDeleted"].IsEmpty())
                {
                    //sql.Append($" AND IsDeleted = N'{queryParam["IsDeleted"]}'");
                    sql.Append($" AND A.IsDeleted like N'%{queryParam["IsDeleted"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND A.Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人名称 是否为空进行查询
                if (!queryParam["CreateName"].IsEmpty())
                {
                    //sql.Append($" AND CreateName = N'{queryParam["CreateName"]}'");
                    sql.Append($" AND A.CreateName like N'%{queryParam["CreateName"]}%'");
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
                //修改人名称 是否为空进行查询
                if (!queryParam["ModifyName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyName = N'{queryParam["ModifyName"]}'");
                    sql.Append($" AND A.ModifyName like N'%{queryParam["ModifyName"]}%'");
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
                //特征值 是否为空进行查询
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND A.TraitValue like N'%{queryParam["Name"]}%'");
                }
                //特征编码 是否为空进行查询
                if (!queryParam["TraitCode"].IsEmpty())
                {
                    sql.Append($" AND b.TraitCode='{queryParam["TraitCode"]}'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND b.FactoryCode='{queryParam["FactoryCode"]}'");
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

        #region 特征值列表
        /// <summary>
        /// 功能描述: 特征值列表(DataTable)
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetTraitValue(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT [TraitValue]
                            FROM [dbo].[BS_TraitDetails] a
                                INNER JOIN dbo.BS_TraitManage b
                                    ON a.TraitManageID = b.Id
                            WHERE a.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND b.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                
                //特征编码 是否为空进行查询
                if (!queryParam["TraitCode"].IsEmpty())
                {
                    sql.Append($" AND b.TraitCode = N'{queryParam["TraitCode"]}'");
                }
                //特征值 是否为空进行查询
                if (!queryParam["TraitValue"].IsEmpty())
                {
                    sql.Append($" AND a.TraitValue like N'%{queryParam["TraitValue"]}%'");
                }
                //关键字 是否为空进行查询
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND a.TraitValue like N'%{queryParam["Name"]}%'");
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
        #endregion

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_TraitDetailsEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[TraitManageID]
                      ,[TraitValue]
                      ,[IsDeleted]
                      ,[Creator]
                      ,[CreateName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[ModifyName]
                  FROM [dbo].[BS_TraitDetails] where IsDeleted = 0 ");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, BS_TraitDetailsEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    BS_TraitDetailsEntity bS_TraitDetailsEntity = new BS_TraitDetailsEntity();

                    bS_TraitDetailsEntity = this.BaseRepository().IQueryable(t => (t.TraitManageID == entity.TraitManageID) && (t.TraitValue == entity.TraitValue) && t.IsDeleted == false).ToList().FirstOrDefault();

                    if(bS_TraitDetailsEntity != null) { 
                    if (bS_TraitDetailsEntity.Id == entity.Id)
                    {
                        entity.Modify(keyValue);
                        n = this.BaseRepository().Update(entity);

                    }
                    else
                    {
                        n = 2;
                    }
                    }
                    else
                    {

                    
                    entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                    }
                }
                else
                {
                    BS_TraitDetailsEntity bS_TraitDetailsEntity = new BS_TraitDetailsEntity();
                    bS_TraitDetailsEntity = this.BaseRepository().IQueryable(t => (t.TraitManageID == entity.TraitManageID) && (t.TraitValue == entity.TraitValue) && t.IsDeleted == false).ToList().FirstOrDefault();
                    if (bS_TraitDetailsEntity == null)
                    {
                        //entity.Create();
                        n = this.BaseRepository().Insert(entity);
                    }
                    else
                    {
                        n = 2;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<BS_TraitDetailsEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_TraitDetailsEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[BS_TraitDetails] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[BS_TraitDetails] (
                                            [Id]
                                            ,[TraitManageID]
                                            ,[TraitValue]
                                            ,[IsDeleted]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                     
                                            ,[ModifyName]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.TraitManageID}'
                                ,N'{Save_obj.TraitValue}'
                                ,{Save_obj.IsDeleted}
                                ,N'{Save_obj.Creator}'
                                ,N'{Save_obj.CreateName}'
                            
                                ,N'{Save_obj.ModifyBy}'
                               
                                ,N'{Save_obj.ModifyName}'
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
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
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: JPF
        /// 创建日期: 2022年11月9日17:25:53
        /// 任务编号: 特征数据导入
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<Base_MaterialEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int NewSaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_TraitDetailsEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[Base_Material] set ");
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
                    //using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                    //{
                    //    bulkCopy.DestinationTableName = "BS_TraitDetails";
                    //    //foreach(var item in new MMWhsBindMaterialDetailsEntity().GetType().GetProperties())
                    //    //{
                    //    //   bulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    //    //}
                    //    bulkCopy.WriteToServer(Tools.ToDataTable(entity_list));//将数据源数据写入到数据库中
                    //}
                    //return 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "",string ModifyBy="")
        {
            int result = 0;
            BS_TraitDetailsEntity entity = this.BaseRepository().FindEntity(keyValue);
            entity.ModifyTime = DateTime.Now;
            entity.ModifyName = UpdateByName;
            entity.ModifyBy = ModifyBy;
            if (entity != null)
            {
                //删除禁用标记
                entity.IsDeleted = true;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
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
                sql.Append($@"DELETE FROM [dbo].[BS_TraitDetails] WHERE Id=N'{keyValue}'");
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回BS_TraitDetailsEntity</returns>
        public BS_TraitDetailsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回BS_TraitDetailsEntity</returns>
        public BS_TraitDetailsEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_TraitDetailsEntity 对象</returns>
        public BS_TraitDetailsEntity Get_ExpressionEntity(Expression<Func<BS_TraitDetailsEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_TraitDetailsEntity 列表</returns>
        public IEnumerable<BS_TraitDetailsEntity> Get_ExpressionList(Expression<Func<BS_TraitDetailsEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
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
        //    RepositoryFactory<BS_TraitDetailsEntity> bomService = new RepositoryFactory<BS_TraitDetailsEntity>();
        
        //    BS_TraitDetailsEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    BS_TraitDetailsDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.BS_TraitDetails_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_TraitDetailsEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<BS_TraitDetailsEntity> BS_TraitDetailsEntity_list =  db2.FindList<BS_TraitDetailsEntity>(sql.ToString());
                return BS_TraitDetailsEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
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
                DataTable BS_TraitDetailsEntity_DataTable = db2.FindTable(sql.ToString());
                return BS_TraitDetailsEntity_DataTable;
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
        /// 创　　建: jpf
        /// 创建日期: 2022-11-02 16:13:18
        /// 任务编号: 特征维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [TraitManageID] as '主表关联ID'
                      ,[TraitValue] as '特征值'
                      ,[IsDeleted] as '删除标识默认为0,删除1'
                      ,[Creator] as '创建人'
                      ,[CreateName] as '创建人名称'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[ModifyName] as '修改人名称'
                  FROM [dbo].[BS_TraitDetails] where IsDeleted = 0 ");
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
                string saveFileName = "特征维护_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("特征维护", dt, true);
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
        
    }
}
