using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.BaseManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.BaseManage;
using ALP.Application.UtilExtend.Offices;
using ALP.Data;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_KeyParameterItemService 业务服务类
    /// 4.任务编号: 关键参数维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_KeyParameterItem_Service : RepositoryFactory<Base_KeyParameterItemEntity>, Base_KeyParameterItemIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_KeyParameterItemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EnCode]
                      ,[ItemCode]
                      ,[ItemName]
                      ,[ItemValue]
                      ,[Col1]
                      ,[Col2]
                      ,[Col3]
                      ,[Remark1]
                      ,[Remark2]
                      ,[SortCode]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[Base_KeyParameterItem] where 1=1 ");
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
                //父级 是否为空进行查询
                if (!queryParam["EnCode"].IsEmpty())
                {
                    //sql.Append($" AND ParentId = N'{queryParam["ParentId"]}'");
                    sql.Append($" AND EnCode = N'{queryParam["EnCode"]}'");
                }
                //编码 是否为空进行查询
                if (!queryParam["ItemCode"].IsEmpty())
                {
                    //sql.Append($" AND ItemCode = N'{queryParam["ItemCode"]}'");
                    sql.Append($" AND ItemCode like N'%{queryParam["ItemCode"]}%'");
                }
                //名称 是否为空进行查询
                if (!queryParam["ItemName"].IsEmpty())
                {
                    //sql.Append($" AND ItemName = N'{queryParam["ItemName"]}'");
                    sql.Append($" AND ItemName like N'%{queryParam["ItemName"]}%'");
                }
                //是否有效 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //排序 是否为空进行查询
                if (!queryParam["SortCode"].IsEmpty())
                {
                    //sql.Append($" AND SortCode = N'{queryParam["SortCode"]}'");
                    sql.Append($" AND SortCode like N'%{queryParam["SortCode"]}%'");
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
                    sql.Append(" ORDER BY SortCode");
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EnCode]
                      ,[ItemCode]
                      ,[ItemName]
                      ,[ItemValue]
                      ,[Col1]
                      ,[Col2]
                      ,[Col3]
                      ,[Remark1]
                      ,[Remark2]
                      ,[SortCode]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[Base_KeyParameterItem] where 1=1 ");
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
                //EnCode 是否为空进行查询
                if (!queryParam["EnCode"].IsEmpty())
                {
                    //sql.Append($" AND EnCode = N'{queryParam["EnCode"]}'");
                    sql.Append($" AND EnCode like N'%{queryParam["EnCode"]}%'");
                }
                //编码 是否为空进行查询
                if (!queryParam["ItemCode"].IsEmpty())
                {
                    //sql.Append($" AND ItemCode = N'{queryParam["ItemCode"]}'");
                    sql.Append($" AND ItemCode like N'%{queryParam["ItemCode"]}%'");
                }
                //名称 是否为空进行查询
                if (!queryParam["ItemName"].IsEmpty())
                {
                    //sql.Append($" AND ItemName = N'{queryParam["ItemName"]}'");
                    sql.Append($" AND ItemName like N'%{queryParam["ItemName"]}%'");
                }
                //值 是否为空进行查询
                if (!queryParam["ItemValue"].IsEmpty())
                {
                    //sql.Append($" AND ItemValue = N'{queryParam["ItemValue"]}'");
                    sql.Append($" AND ItemValue like N'%{queryParam["ItemValue"]}%'");
                }
                //字段1 是否为空进行查询
                if (!queryParam["Col1"].IsEmpty())
                {
                    //sql.Append($" AND Col1 = N'{queryParam["Col1"]}'");
                    sql.Append($" AND Col1 like N'%{queryParam["Col1"]}%'");
                }
                //字段2 是否为空进行查询
                if (!queryParam["Col2"].IsEmpty())
                {
                    //sql.Append($" AND Col2 = N'{queryParam["Col2"]}'");
                    sql.Append($" AND Col2 like N'%{queryParam["Col2"]}%'");
                }
                //字段3 是否为空进行查询
                if (!queryParam["Col3"].IsEmpty())
                {
                    //sql.Append($" AND Col3 = N'{queryParam["Col3"]}'");
                    sql.Append($" AND Col3 like N'%{queryParam["Col3"]}%'");
                }
                //备注1 是否为空进行查询
                if (!queryParam["Remark1"].IsEmpty())
                {
                    //sql.Append($" AND Remark1 = N'{queryParam["Remark1"]}'");
                    sql.Append($" AND Remark1 like N'%{queryParam["Remark1"]}%'");
                }
                //备注2 是否为空进行查询
                if (!queryParam["Remark2"].IsEmpty())
                {
                    //sql.Append($" AND Remark2 = N'{queryParam["Remark2"]}'");
                    sql.Append($" AND Remark2 like N'%{queryParam["Remark2"]}%'");
                }
                //排序 是否为空进行查询
                if (!queryParam["SortCode"].IsEmpty())
                {
                    //sql.Append($" AND SortCode = N'{queryParam["SortCode"]}'");
                    sql.Append($" AND SortCode like N'%{queryParam["SortCode"]}%'");
                }
                //是否有效 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND IsEnabled like N'%{queryParam["IsEnabled"]}%'");
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

        public DataTable GetKeyParameterItemList(string enCode,string itemCode, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT *
                                FROM dbo.Base_KeyParameterItem
                                WHERE   IsEnabled = 1 ");

                if (!string.IsNullOrEmpty(enCode))
                {
                    sql.Append($@" AND EnCode = '{enCode}'");
                }
                if (!string.IsNullOrEmpty(itemCode))
                {
                    sql.Append($@" AND ItemCode = '{itemCode}'");
                }
                sql.Append(@"   ORDER BY SortCode");
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_KeyParameterItemEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EnCode]
                      ,[ItemCode]
                      ,[ItemName]
                      ,[ItemValue]
                      ,[Col1]
                      ,[Col2]
                      ,[Col3]
                      ,[Remark1]
                      ,[Remark2]
                      ,[SortCode]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[Base_KeyParameterItem] where 1=1 ");
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, Base_KeyParameterItemEntity entity, out string msg)
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<Base_KeyParameterItemEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_KeyParameterItemEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[Base_KeyParameterItem] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[Base_KeyParameterItem] (
                                            [Id]
                                            ,[EnCode]
                                            ,[ItemCode]
                                            ,[ItemName]
                                            ,[ItemValue]
                                            ,[Col1]
                                            ,[Col2]
                                            ,[Col3]
                                            ,[Remark1]
                                            ,[Remark2]
                                            ,[SortCode]
                                            ,[IsEnabled]
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
                                ,N'{Save_obj.EnCode}'
                                ,N'{Save_obj.ItemCode}'
                                ,N'{Save_obj.ItemName}'
                                ,N'{Save_obj.ItemValue}'
                                ,N'{Save_obj.Col1}'
                                ,N'{Save_obj.Col2}'
                                ,N'{Save_obj.Col3}'
                                ,N'{Save_obj.Remark1}'
                                ,N'{Save_obj.Remark2}'
                                ,{Save_obj.SortCode}
                                ,'{(Save_obj.IsEnabled == true ? 1 : 0)}'
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }
        public int RemoveForm(Expression<Func<Base_KeyParameterItemEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
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
                sql.Append($@"DELETE FROM [dbo].[Base_KeyParameterItem] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回Base_KeyParameterItemEntity</returns>
        public Base_KeyParameterItemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回Base_KeyParameterItemEntity</returns>
        public Base_KeyParameterItemEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_KeyParameterItemEntity 对象</returns>
        public Base_KeyParameterItemEntity Get_ExpressionEntity(Expression<Func<Base_KeyParameterItemEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_KeyParameterItemEntity 列表</returns>
        public IEnumerable<Base_KeyParameterItemEntity> Get_ExpressionList(Expression<Func<Base_KeyParameterItemEntity, bool>> condition)
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
        //    RepositoryFactory<Base_KeyParameterItemEntity> bomService = new RepositoryFactory<Base_KeyParameterItemEntity>();

        //    Base_KeyParameterItemEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    Base_KeyParameterItemDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.Base_KeyParameterItem_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_KeyParameterItemEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<Base_KeyParameterItemEntity> Base_KeyParameterItemEntity_list = db2.FindList<Base_KeyParameterItemEntity>(sql.ToString());
                return Base_KeyParameterItemEntity_list;
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
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
                DataTable Base_KeyParameterItemEntity_DataTable = db2.FindTable(sql.ToString());
                return Base_KeyParameterItemEntity_DataTable;
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
        /// 创建日期: 2021-07-30 09:08:10
        /// 任务编号: 关键参数维护
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [EnCode] as 'EnCode'
                      ,[ItemCode] as '编码'
                      ,[ItemName] as '名称'
                      ,[ItemValue] as '值'
                      ,[Col1] as '字段1'
                      ,[Col2] as '字段2'
                      ,[Col3] as '字段3'
                      ,[Remark1] as '备注1'
                      ,[Remark2] as '备注2'
                      ,[SortCode] as '排序'
                      ,[IsEnabled] as '是否有效'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[Base_KeyParameterItem] where 1=1 ");
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
                string saveFileName = "关键参数维护_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("关键参数维护", dt, true);
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
        #region SAP客户主数据
        /// <summary>
        /// 描述：SAP客户主数据接收
        /// 创建：jpf
        /// 时间：2024-3-26 15:10:54
        /// </summary>
        /// <param name="entities"></param>
        public  void SaveBase_SAPKeyParameter(List<Base_KeyParameterItemEntity> entities)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                foreach (var item in entities)
                {
                    if (string.IsNullOrEmpty(item.ItemCode))
                    {
                        throw new Exception("客户编码必须传!");
                    }
                    if (string.IsNullOrEmpty(item.ItemName))
                    {
                        throw new Exception("客户名称必须传!");
                    }

                    //判断是否存在存在就更新
                    var KeyParameterItemEntity = Get_ExpressionEntity(t=>t.EnCode== "client" && t.ItemCode==item.ItemCode);
                    if (KeyParameterItemEntity == null)
                    {
                        item.EnCode = "client";
                        item.Create();
                        item.CreateTime = DateTime.Now;
                        db.Insert(item);
                    }
                    else
                    {
                        KeyParameterItemEntity.ItemCode = item.ItemCode;
                        KeyParameterItemEntity.ItemName = item.ItemName;
                        KeyParameterItemEntity.ItemValue = item.ItemValue;
                        KeyParameterItemEntity.Col1 = item.Col1;
                        KeyParameterItemEntity.Col2 = item.Col2;
                        KeyParameterItemEntity.Col3 = item.Col3;
                        KeyParameterItemEntity.Remark1 = item.Remark1;
                        KeyParameterItemEntity.Remark2 = item.Remark2;
                        KeyParameterItemEntity.IsEnabled = item.IsEnabled;
                        KeyParameterItemEntity.ModifyTime = DateTime.Now;
                        db.Update(KeyParameterItemEntity);
                    }
                }

                    db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            finally
            {
                db.Close();
            }
        }

        #endregion
    }
}
