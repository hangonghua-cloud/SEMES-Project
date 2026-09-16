using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.Material;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.Material;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-21
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialBindTempFacetService 业务服务类
    /// 4.任务编号: 任务名称或编号
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialBindTempFacet_Service : RepositoryFactory<Base_MaterialBindTempFacetEntity>, Base_MaterialBindTempFacetIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialBindTempFacetEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[MateriaBindTempId]
                      ,[TempName]
                      ,[AttrCode]
                      ,[AttrName]
                      ,[AttrType]
                      ,[UpperLimit]
                      ,[LowerLimit]
                      ,[Length]
                      ,[DefaultValue]
                      ,[IsEnabled]
                      ,[Sort]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[Base_MaterialBindTempFacet] where IsEnabled=1  ");
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
                //模板编码 是否为空进行查询
                if (!queryParam["TempCode"].IsEmpty())
                {
                    //sql.Append($" AND TempCode = N'{queryParam["TempCode"]}'");
                    sql.Append($" AND TempCode like N'%{queryParam["TempCode"]}%'");
                }
                //模板名称 是否为空进行查询
                if (!queryParam["TempName"].IsEmpty())
                {
                    //sql.Append($" AND TempName = N'{queryParam["TempName"]}'");
                    sql.Append($" AND TempName like N'%{queryParam["TempName"]}%'");
                }
                //排序 是否为空进行查询
                if (!queryParam["Sort"].IsEmpty())
                {
                    //sql.Append($" AND Sort = N'{queryParam["Sort"]}'");
                    sql.Append($" AND Sort like N'%{queryParam["Sort"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //是否生效1是0否 是否为空进行查询
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      B.[Id]
                      ,[MateriaBindTempId]
                      ,[TempName]
                      ,[AttrCode]
                      ,[AttrName]
                      ,[AttrType]
                      ,[UpperLimit]
                      ,[LowerLimit]
                      ,[Length]
                      ,[DefaultValue]
                      ,[IsEnabled]
                      ,[Sort]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime],
					  DD.ItemName AttrTypeName
                  FROM [dbo].[Base_MaterialBindTempFacet] B
				  INNER JOIN  [dbo].[Base_DataItem] D ON D.ItemCode='AttrType'
				  INNER JOIN [dbo].[Base_DataItemDetail] DD ON D.ItemId=DD.ItemId AND  CAST(B.AttrType AS VARCHAR(50))=DD.ItemValue
				  WHERE B.IsEnabled=1   ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND B.Id like N'%{queryParam["Id"]}%'");
                }
                //模板分类 是否为空进行查询
                if (!queryParam["MateriaBindTempId"].IsEmpty())
                {
                    //sql.Append($" AND MateriaBindTempId = N'{queryParam["MateriaBindTempId"]}'");
                    sql.Append($" AND B.MateriaBindTempId like N'%{queryParam["MateriaBindTempId"]}%'");
                }
                //模板名称 是否为空进行查询
                if (!queryParam["TempName"].IsEmpty())
                {
                    //sql.Append($" AND TempName = N'{queryParam["TempName"]}'");
                    sql.Append($" AND B.TempName like N'%{queryParam["TempName"]}%'");
                }
                //属性编码 是否为空进行查询
                if (!queryParam["AttrCode"].IsEmpty())
                {
                    //sql.Append($" AND AttrCode = N'{queryParam["AttrCode"]}'");
                    sql.Append($" AND B.AttrCode like N'%{queryParam["AttrCode"]}%'");
                }
                //属性名称 是否为空进行查询
                if (!queryParam["AttrName"].IsEmpty())
                {
                    //sql.Append($" AND AttrName = N'{queryParam["AttrName"]}'");
                    sql.Append($" AND B.AttrName like N'%{queryParam["AttrName"]}%'");
                }
                //属性类型 是否为空进行查询
                if (!queryParam["AttrType"].IsEmpty())
                {
                    //sql.Append($" AND AttrType = N'{queryParam["AttrType"]}'");
                    sql.Append($" AND B.AttrType like N'%{queryParam["AttrType"]}%'");
                }
                //上限 是否为空进行查询
                if (!queryParam["UpperLimit"].IsEmpty())
                {
                    //sql.Append($" AND UpperLimit = N'{queryParam["UpperLimit"]}'");
                    sql.Append($" AND B.UpperLimit like N'%{queryParam["UpperLimit"]}%'");
                }
                //下限 是否为空进行查询
                if (!queryParam["LowerLimit"].IsEmpty())
                {
                    //sql.Append($" AND LowerLimit = N'{queryParam["LowerLimit"]}'");
                    sql.Append($" AND B.LowerLimit like N'%{queryParam["LowerLimit"]}%'");
                }
                //长度 是否为空进行查询
                if (!queryParam["Length"].IsEmpty())
                {
                    //sql.Append($" AND Length = N'{queryParam["Length"]}'");
                    sql.Append($" AND B.Length like N'%{queryParam["Length"]}%'");
                }
                //默认值 是否为空进行查询
                if (!queryParam["DefaultValue"].IsEmpty())
                {
                    //sql.Append($" AND DefaultValue = N'{queryParam["DefaultValue"]}'");
                    sql.Append($" AND B.DefaultValue like N'%{queryParam["DefaultValue"]}%'");
                }
                //是否可用 是否为空进行查询
                if (!queryParam["IsEnabled"].IsEmpty())
                {
                    //sql.Append($" AND IsEnabled = N'{queryParam["IsEnabled"]}'");
                    sql.Append($" AND B.IsEnabled like N'%{queryParam["IsEnabled"]}%'");
                }
                //排序 是否为空进行查询
                if (!queryParam["Sort"].IsEmpty())
                {
                    //sql.Append($" AND Sort = N'{queryParam["Sort"]}'");
                    sql.Append($" AND B.Sort like N'%{queryParam["Sort"]}%'");
                }
                //创建人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND B.Creator like N'%{queryParam["Creator"]}%'");
                }
                //创建时间 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    //sql.Append($" AND CreateTime = N'{queryParam["CreateTime"]}'");
                    sql.Append($" AND B.CreateTime like N'%{queryParam["CreateTime"]}%'");
                }
                //最后修改人 是否为空进行查询
                if (!queryParam["ModifyBy"].IsEmpty())
                {
                    //sql.Append($" AND ModifyBy = N'{queryParam["ModifyBy"]}'");
                    sql.Append($" AND B.ModifyBy like N'%{queryParam["ModifyBy"]}%'");
                }
                //最后修改时间 是否为空进行查询
                if (!queryParam["ModifyTime"].IsEmpty())
                {
                    //sql.Append($" AND ModifyTime = N'{queryParam["ModifyTime"]}'");
                    sql.Append($" AND B.ModifyTime like N'%{queryParam["ModifyTime"]}%'");
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
        /// 功能描述: 通过编码进行获取
        /// 创　　建: jpf
        /// 创建日期: add 2022-11-24 
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetListBycode(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT B.[Id],
                               B.[MateriaBindTempId],
                               B.[TempName],
                               B.[AttrCode],
                               B.[AttrName],
                               B.[AttrType],
                               B.[UpperLimit],
                               B.[LowerLimit],
                               B.[Length],
                               B.[DefaultValue],
                               B.[Sort],
                               DD.ItemName AttrTypeName
                        FROM 
						Base_MaterialBindTemp a LEFT JOIN 
						[dbo].[Base_MaterialBindTempFacet] B ON a.Id=b.MateriaBindTempId
                            INNER JOIN [dbo].[Base_DataItem] D
                                ON D.ItemCode = 'AttrType'
                            INNER JOIN [dbo].[Base_DataItemDetail] DD
                                ON D.ItemId = DD.ItemId
                                   AND CAST(B.AttrType AS VARCHAR(50)) = DD.ItemValue
                        WHERE B.IsEnabled = 1");
            if (!checkType.IsEmpty())
            {
                sql.Append($@" and a.TempCode = N'{checkType}' ");
            }
            sql.Append(" ORDER By B.Sort ");
            msg = "";
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT B.[Id],
                               B.[MateriaBindTempId],
                               A.TempCode,
							   A.TempName,
                               B.[AttrCode],
                               B.[AttrName],
                               B.[AttrType],
                               B.[UpperLimit],
                               B.[LowerLimit],
                               B.[Length],
                               B.[DefaultValue],
                               B.[Sort],
                               DD.ItemName AttrTypeName
                        FROM [dbo].[Base_MaterialBindTempFacet] B
						INNER JOIN dbo.Base_MaterialBindTemp A ON B.MateriaBindTempId=A.Id
						INNER JOIN [dbo].[Base_DataItem] D
                                ON D.ItemCode = 'AttrType'
                            INNER JOIN [dbo].[Base_DataItemDetail] DD
                                ON D.ItemId = DD.ItemId
                                   AND CAST(B.AttrType AS VARCHAR(50)) = DD.ItemValue
                        WHERE B.IsEnabled = 1 ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" AND A.TempCode='{checkType}' ");
                sql.Append($@" AND (a.Id='{checkType}' OR a.TempCode='{checkType}') ");
            }
            sql.Append(" ORDER By B.Sort ");
            msg = "";
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, Base_MaterialBindTempFacetEntity entity, out string msg)
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<Base_MaterialBindTempFacetEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialBindTempFacetEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[Base_MaterialBindTempFacet] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[Base_MaterialBindTempFacet] (
                                            [Id]
                                            ,[MateriaBindTempId]
                                            ,[TempName]
                                            ,[AttrCode]
                                            ,[AttrName]
                                            ,[AttrType]
                                            ,[UpperLimit]
                                            ,[LowerLimit]
                                            ,[Length]
                                            ,[DefaultValue]
                                            ,[IsEnabled]
                                            ,[Sort]
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
                                ,N'{Save_obj.MateriaBindTempId}'
                                ,N'{Save_obj.TempName}'
                                ,N'{Save_obj.AttrCode}'
                                ,N'{Save_obj.AttrName}'
                                ,{Save_obj.AttrType}
                                ,{Save_obj.UpperLimit}
                                ,{Save_obj.LowerLimit}
                                ,{Save_obj.Length}
                                ,N'{Save_obj.DefaultValue}'
                                ,'{(Save_obj.IsEnabled == true ? 1:0)}'
                                ,{Save_obj.Sort}
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
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            var entity = GetEntity(keyValue);
            entity.IsEnabled = false;
            this.BaseRepository().Update(entity);
            return 1;
        }
        public int RemoveForm(Expression<Func<Base_MaterialBindTempFacetEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
                sql.Append($@"DELETE FROM [dbo].[Base_MaterialBindTempFacet] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回Base_MaterialBindTempFacetEntity</returns>
        public Base_MaterialBindTempFacetEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回Base_MaterialBindTempFacetEntity</returns>
        public Base_MaterialBindTempFacetEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_MaterialBindTempFacetEntity 对象</returns>
        public Base_MaterialBindTempFacetEntity Get_ExpressionEntity(Expression<Func<Base_MaterialBindTempFacetEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_MaterialBindTempFacetEntity 列表</returns>
        public IEnumerable<Base_MaterialBindTempFacetEntity> Get_ExpressionList(Expression<Func<Base_MaterialBindTempFacetEntity, bool>> condition)
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
        //    RepositoryFactory<Base_MaterialBindTempFacetEntity> bomService = new RepositoryFactory<Base_MaterialBindTempFacetEntity>();
        
        //    Base_MaterialBindTempFacetEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    Base_MaterialBindTempFacetDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.Base_MaterialBindTempFacet_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialBindTempFacetEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<Base_MaterialBindTempFacetEntity> Base_MaterialBindTempFacetEntity_list =  db2.FindList<Base_MaterialBindTempFacetEntity>(sql.ToString());
                return Base_MaterialBindTempFacetEntity_list;
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
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
                DataTable Base_MaterialBindTempFacetEntity_DataTable = db2.FindTable(sql.ToString());
                return Base_MaterialBindTempFacetEntity_DataTable;
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
        /// 创建日期: 2021-07-21 15:46:27
        /// 任务编号: 任务名称或编号
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [MateriaBindTempId] as '模板分类'
                      ,[TempName] as '模板名称'
                      ,[AttrCode] as '属性编码'
                      ,[AttrName] as '属性名称'
                      ,[AttrType] as '属性类型'
                      ,[UpperLimit] as '上限'
                      ,[LowerLimit] as '下限'
                      ,[Length] as '长度'
                      ,[DefaultValue] as '默认值'
                      ,[IsEnabled] as '是否可用'
                      ,[Sort] as '排序'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[Base_MaterialBindTempFacet] where IsEnabled=1  ");
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
                string saveFileName = "任务名称或编号_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("任务名称或编号", dt, true);
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
