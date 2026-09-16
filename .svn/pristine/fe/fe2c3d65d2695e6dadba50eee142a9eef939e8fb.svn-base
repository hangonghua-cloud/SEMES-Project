using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.PlanManage;

namespace ALP.Application.Service.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-18
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_ProcessOfOperationsAttrService 业务服务类
    /// 4.任务编号: 订单采购
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_ProcessOfOperationsAttr_Service : RepositoryFactory<PL_ProcessOfOperationsAttrEntity>, PL_ProcessOfOperationsAttrIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[OperationsId]
                      ,[AttrCode]
                      ,[AttrName]
                      ,[AttrType]
                      ,[AttrTypeName]
                      ,[SortCode]
                      ,[AttrValue]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperationsAttr] where 1 = 1 ");
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
                //外键 是否为空进行查询
                if (!queryParam["OperationsId"].IsEmpty())
                {
                    //sql.Append($" AND OperationsId = N'{queryParam["OperationsId"]}'");
                    sql.Append($" AND OperationsId like N'%{queryParam["OperationsId"]}%'");
                }
                //属性编码 是否为空进行查询
                if (!queryParam["AttrCode"].IsEmpty())
                {
                    //sql.Append($" AND AttrCode = N'{queryParam["AttrCode"]}'");
                    sql.Append($" AND AttrCode like N'%{queryParam["AttrCode"]}%'");
                }
                //属性名称 是否为空进行查询
                if (!queryParam["AttrName"].IsEmpty())
                {
                    //sql.Append($" AND AttrName = N'{queryParam["AttrName"]}'");
                    sql.Append($" AND AttrName like N'%{queryParam["AttrName"]}%'");
                }
                //属性类型 是否为空进行查询
                if (!queryParam["AttrType"].IsEmpty())
                {
                    //sql.Append($" AND AttrType = N'{queryParam["AttrType"]}'");
                    sql.Append($" AND AttrType like N'%{queryParam["AttrType"]}%'");
                }
                //属性类型名称 是否为空进行查询
                if (!queryParam["AttrTypeName"].IsEmpty())
                {
                    //sql.Append($" AND AttrTypeName = N'{queryParam["AttrTypeName"]}'");
                    sql.Append($" AND AttrTypeName like N'%{queryParam["AttrTypeName"]}%'");
                }
                //顺序号 是否为空进行查询
                if (!queryParam["SortCode"].IsEmpty())
                {
                    //sql.Append($" AND SortCode = N'{queryParam["SortCode"]}'");
                    sql.Append($" AND SortCode like N'%{queryParam["SortCode"]}%'");
                }
                //录入值 是否为空进行查询
                if (!queryParam["AttrValue"].IsEmpty())
                {
                    //sql.Append($" AND AttrValue = N'{queryParam["AttrValue"]}'");
                    sql.Append($" AND AttrValue like N'%{queryParam["AttrValue"]}%'");
                }
                //是否可用 是否为空进行查询
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
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[OperationsId]
                      ,[AttrCode]
                      ,[AttrName]
                      ,[AttrType]
                      ,[AttrTypeName]
                      ,[SortCode]
                      ,[AttrValue]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperationsAttr] where 1 = 1 ");
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
                //外键 是否为空进行查询
                if (!queryParam["OperationsId"].IsEmpty())
                {
                    //sql.Append($" AND OperationsId = N'{queryParam["OperationsId"]}'");
                    sql.Append($" AND OperationsId like N'%{queryParam["OperationsId"]}%'");
                }
                //属性编码 是否为空进行查询
                if (!queryParam["AttrCode"].IsEmpty())
                {
                    //sql.Append($" AND AttrCode = N'{queryParam["AttrCode"]}'");
                    sql.Append($" AND AttrCode like N'%{queryParam["AttrCode"]}%'");
                }
                //属性名称 是否为空进行查询
                if (!queryParam["AttrName"].IsEmpty())
                {
                    //sql.Append($" AND AttrName = N'{queryParam["AttrName"]}'");
                    sql.Append($" AND AttrName like N'%{queryParam["AttrName"]}%'");
                }
                //属性类型 是否为空进行查询
                if (!queryParam["AttrType"].IsEmpty())
                {
                    //sql.Append($" AND AttrType = N'{queryParam["AttrType"]}'");
                    sql.Append($" AND AttrType like N'%{queryParam["AttrType"]}%'");
                }
                //属性类型名称 是否为空进行查询
                if (!queryParam["AttrTypeName"].IsEmpty())
                {
                    //sql.Append($" AND AttrTypeName = N'{queryParam["AttrTypeName"]}'");
                    sql.Append($" AND AttrTypeName like N'%{queryParam["AttrTypeName"]}%'");
                }
                //顺序号 是否为空进行查询
                if (!queryParam["SortCode"].IsEmpty())
                {
                    //sql.Append($" AND SortCode = N'{queryParam["SortCode"]}'");
                    sql.Append($" AND SortCode like N'%{queryParam["SortCode"]}%'");
                }
                //录入值 是否为空进行查询
                if (!queryParam["AttrValue"].IsEmpty())
                {
                    //sql.Append($" AND AttrValue = N'{queryParam["AttrValue"]}'");
                    sql.Append($" AND AttrValue like N'%{queryParam["AttrValue"]}%'");
                }
                //是否可用 是否为空进行查询
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
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[OperationsId]
                      ,[AttrCode]
                      ,[AttrName]
                      ,[AttrType]
                      ,[AttrTypeName]
                      ,[SortCode]
                      ,[AttrValue]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[PL_ProcessOfOperationsAttr] where 1 = 1 ");
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
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_ProcessOfOperationsAttrEntity entity, out string msg)
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
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PL_ProcessOfOperationsAttrEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsAttrEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PL_ProcessOfOperationsAttr] set ");
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
                                    if (x.Name == "IsEnabled")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 1 : (x.GetValue(Save_obj, null).ToString().ToLower() == "true" ? 1: 0))+ ",");
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
                throw new Exception(ex.Message);
            }
            return n;
        }
        
 
        
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition)
        {
             return this.BaseRepository().Delete(condition);
        }
        public int Delete(List<PL_ProcessOfOperationsAttrEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }
        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: jpf
        /// 创建日期: 2022-11-30 20:17:50
        /// 任务编号: 工艺路线批量更新
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int newRemoveForm(bool isbool,string usercode, List<PL_ProcessOfOperationsAttrEntity> lstEntity, string UpdateByName = "")
        {
            int result = 0;

            if (lstEntity.Count > 0)
            {
                foreach (var item in lstEntity)
                {
                    item.IsEnabled = isbool;
                    item.ModifyBy = usercode;
                    item.ModifyTime = DateTime.Now;
                    //删除禁用标记
                    this.BaseRepository().Update(item);
                }
               
                result = 1;
            }
            else
            {
                result = 0;//没有找到记录
            }

            return result;
        }
        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_ProcessOfOperationsAttrEntity</returns>
        public PL_ProcessOfOperationsAttrEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
 
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-18 09:14:15
        /// 任务编号: 订单采购
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_ProcessOfOperationsAttrEntity 列表</returns>
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

        public PL_ProcessOfOperationsAttrEntity Get_ExpressionEntity(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
    }
}
