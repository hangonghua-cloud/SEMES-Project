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

namespace ALP.Application.Service.SAP
{ 
    /// <summary>
    /// 1.创建日期: 2023-02-28
    /// 2.创建作者: jpf
    /// 3.功能描述: Base_DataFileConService 业务服务类
    /// 4.任务编号: 仓库安全库存
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_DataFileCon_Service : RepositoryFactory<Base_DataFileConEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_DataFileConEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[module]
                      ,[tableName]
                      ,[TableEntity]
                      ,[isFile]
                      ,[Creator]
                      ,[CreatorName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyByName]
                      ,[ModifyTime]
                  FROM [dbo].[Base_DataFileCon] where IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //模块 是否为空进行查询
                if (!queryParam["module"].IsEmpty())
                {
                    //sql.Append($" AND module = N'{queryParam["module"]}'");
                    sql.Append($" AND module like N'%{queryParam["module"]}%'");
                }
                //表名 是否为空进行查询
                if (!queryParam["tableName"].IsEmpty())
                {
                    //sql.Append($" AND tableName = N'{queryParam["tableName"]}'");
                    sql.Append($" AND tableName like N'%{queryParam["tableName"]}%'");
                }
                //表实体 是否为空进行查询
                if (!queryParam["TableEntity"].IsEmpty())
                {
                    //sql.Append($" AND TableEntity = N'{queryParam["TableEntity"]}'");
                    sql.Append($" AND TableEntity like N'%{queryParam["TableEntity"]}%'");
                }
                //是否归档 是否为空进行查询
                if (!queryParam["isFile"].IsEmpty())
                {
                    //sql.Append($" AND isFile = N'{queryParam["isFile"]}'");
                    sql.Append($" AND isFile like N'%{queryParam["isFile"]}%'");
                }
                //创建人编码 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["CreatorName"].IsEmpty())
                {
                    //sql.Append($" AND CreatorName = N'{queryParam["CreatorName"]}'");
                    sql.Append($" AND CreatorName like N'%{queryParam["CreatorName"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人编码 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyByName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyByName = N'{queryParam["ModifyByName"]}'");
                    sql.Append($" AND ModifyByName like N'%{queryParam["ModifyByName"]}%'");
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
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[module]
                      ,[tableName]
                      ,[TableEntity]
                      ,[isFile]
                      ,[Creator]
                      ,[CreatorName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyByName]
                      ,[ModifyTime]
                  FROM [dbo].[Base_DataFileCon] where 0 = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND Id like N'%{queryParam["Id"]}%'");
                }
                //模块 是否为空进行查询
                if (!queryParam["module"].IsEmpty())
                {
                    //sql.Append($" AND module = N'{queryParam["module"]}'");
                    sql.Append($" AND module like N'%{queryParam["module"]}%'");
                }
                //表名 是否为空进行查询
                if (!queryParam["tableName"].IsEmpty())
                {
                    //sql.Append($" AND tableName = N'{queryParam["tableName"]}'");
                    sql.Append($" AND tableName like N'%{queryParam["tableName"]}%'");
                }
                //表实体 是否为空进行查询
                if (!queryParam["TableEntity"].IsEmpty())
                {
                    //sql.Append($" AND TableEntity = N'{queryParam["TableEntity"]}'");
                    sql.Append($" AND TableEntity like N'%{queryParam["TableEntity"]}%'");
                }
                //是否归档 是否为空进行查询
                if (!queryParam["isFile"].IsEmpty())
                {
                    //sql.Append($" AND isFile = N'{queryParam["isFile"]}'");
                    sql.Append($" AND isFile like N'%{queryParam["isFile"]}%'");
                }
                //创建人编码 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["CreatorName"].IsEmpty())
                {
                    //sql.Append($" AND CreatorName = N'{queryParam["CreatorName"]}'");
                    sql.Append($" AND CreatorName like N'%{queryParam["CreatorName"]}%'");
                }

                //交货日期 是否为空进行查询
                if (!queryParam["StartPrepay"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryDate = N'{queryParam["DeliveryDate"]}'");
                    sql.Append($" AND CreateTime >= N'{queryParam["StartPrepay"]}'");
                }
                if (!queryParam["EndPrepay"].IsEmpty())
                {
                    //sql.Append($" AND DeliveryDate = N'{queryParam["DeliveryDate"]}'");
                    sql.Append($" AND CreateTime <= N'{queryParam["EndPrepay"]}'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人编码 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyByName"].IsEmpty())
                {
                    //sql.Append($" AND ModifyByName = N'{queryParam["ModifyByName"]}'");
                    sql.Append($" AND ModifyByName like N'%{queryParam["ModifyByName"]}%'");
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
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_DataFileConEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[module]
                      ,[tableName]
                      ,[TableEntity]
                      ,[isFile]
                      ,[Creator]
                      ,[CreatorName]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyByName]
                      ,[ModifyTime]
                  FROM [dbo].[Base_DataFileCon] where IsDeleted = 0 ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" and  = N'{checkType}' ");
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
        /// 按照时间进行归档
        /// </summary>
        /// <returns></returns>
        public string Save_DateTimeDataFileCon(string StartTime,string EndTime,string TableEntity)
        {
            var msg = "";

        
            DataTable TableEntitydt = TFTableEntityToDataTable(TableEntity);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@TableEntitys", TableEntitydt),
                    new SqlParameter("@StartTime",SqlDbType.VarChar,50),
                    new SqlParameter("@EndTime",SqlDbType.VarChar,50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[1].Value = StartTime;
            parameters[2].Value = EndTime;
            parameters[3].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("Base_DateTimeDataFileCon", parameters);
                var Resultmsg = parameters[3].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工单调拨接收生成失败");
                }
            }
            return msg;


        }

        /// <summary>
        /// 按照工单进行归档
        /// </summary>
        /// <param name="workorderLists"></param>
        /// <param name="TableEntity"></param>
        /// <returns></returns>
        public string Save_WorkDataFileCon(string workorderLists, string TableEntity)
        {
            var msg = "";
         
            DataTable workdt = TFWorkOrderListToDataTable(workorderLists);
            DataTable TableEntitydt= TFTableEntityToDataTable(TableEntity);
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@TableEntitys", TableEntitydt),
                    new SqlParameter("@WorkOrders",workdt),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };

            parameters[2].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("Base_WorkOrderDataFileCon", parameters);
                var Resultmsg = parameters[2].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("工单调拨接收生成失败");
                }
            }
            return msg;
        }

        /// <summary>
        /// 按照订单进行归档
        /// </summary>
        /// <returns></returns>
        public string Save_ProductOrderFileCon(string productOrders)
        {
            var msg = "";

            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@productOrders", productOrders),
                    new SqlParameter("@returnMsg",SqlDbType.VarChar, 8000)
            };

            parameters[1].Direction = ParameterDirection.Output;
            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("Base_ProductOrderFileCon", parameters); //定时执行 Pro_ProductOrderFileCon
                var Resultmsg = parameters[1].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            return msg;
        }

        private DataTable TFTableEntityToDataTable(string list)
        {
            if (list == null) return null;
            var TableEntitylist = list.Split(",");
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("TableEntity", System.Type.GetType("System.String"));
            foreach (var item in TableEntitylist)
            {
                dt.Rows.Add(item);
            }
            return dt;
        }
        private DataTable TFWorkOrderListToDataTable(string list )
        {
            if (list == null ) return null;
            var worklist = list.Split(",");
            DataTable dt = new DataTable("tableName");
            //2.创建带列名和类型名的列(两种方式任选其一)
            dt.Columns.Add("WorkOrder", System.Type.GetType("System.String"));
            foreach (var item in worklist)
            {
                dt.Rows.Add(item);
            }
            return dt;
        }
            /// <summary>
            /// 功能描述: 保存表单（新增、修改）
            /// 创　　建: jpf
            /// 创建日期: 2023-02-28 08:49:31
            /// 任务编号: 仓库安全库存
            /// </summary>
            /// <param name="keyValue">主键值</param>
            /// <param name="entity">实体对象</param>
            /// <param name="msg">输出错误内容</param>
            /// <returns>返回int 成功1, 失败0 </returns>
            public int SaveEntity(string keyValue, Base_DataFileConEntity entity, out string msg)
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
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<Base_DataFileConEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_DataFileConEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[Base_DataFileCon] set ");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE ='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[Base_DataFileCon] (
                                            [Id]
                                            ,[module]
                                            ,[tableName]
                                            ,[TableEntity]
                                            ,[isFile]
                                            ,[Creator]
                                            ,[CreatorName]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyByName]
                                            ,[ModifyTime]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.module}'
                                ,N'{Save_obj.tableName}'
                                ,N'{Save_obj.TableEntity}'
                                ,'{(Save_obj.isFile == true ? 1:0)}'
                                ,N'{Save_obj.Creator}'
                                ,N'{Save_obj.CreatorName}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,N'{Save_obj.ModifyByName}'
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
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
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
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            Base_DataFileConEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
               // entity.IsDeleted = true;
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
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
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
                sql.Append($@"DELETE FROM [dbo].[Base_DataFileCon] WHERE =N'{keyValue}'");
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
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回Base_DataFileConEntity</returns>
        public Base_DataFileConEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回Base_DataFileConEntity</returns>
        public Base_DataFileConEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t. Id== QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_DataFileConEntity 对象</returns>
        public Base_DataFileConEntity Get_ExpressionEntity(Expression<Func<Base_DataFileConEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: jpf
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_DataFileConEntity 列表</returns>
        public IEnumerable<Base_DataFileConEntity> Get_ExpressionList(Expression<Func<Base_DataFileConEntity, bool>> condition)
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
        //    RepositoryFactory<Base_DataFileConEntity> bomService = new RepositoryFactory<Base_DataFileConEntity>();
        
        //    Base_DataFileConEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    Base_DataFileConDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.Base_DataFileCon_ == entity.).FirstOrDefault();
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
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_DataFileConEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<Base_DataFileConEntity> Base_DataFileConEntity_list =  db2.FindList<Base_DataFileConEntity>(sql.ToString());
                return Base_DataFileConEntity_list;
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
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
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
                DataTable Base_DataFileConEntity_DataTable = db2.FindTable(sql.ToString());
                return Base_DataFileConEntity_DataTable;
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
        /// 创建日期: 2023-02-28 08:49:31
        /// 任务编号: 仓库安全库存
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id] as ''
                      ,[module] as '模块'
                      ,[tableName] as '表名'
                      ,[TableEntity] as '表实体'
                      ,[isFile] as '是否归档'
                      ,[Creator] as '创建人编码'
                      ,[CreatorName] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人编码'
                      ,[ModifyByName] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[Base_DataFileCon] where IsDeleted = 0 ");
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
                string saveFileName = "仓库安全库存_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("仓库安全库存", dt, true);
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
