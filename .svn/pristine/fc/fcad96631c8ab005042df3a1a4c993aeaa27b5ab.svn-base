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
using ALP.Application.UtilExtend.Offices;
using ALP.Data;

namespace ALP.Application.Service.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-24
    /// 2.创建作者: admin
    /// 3.功能描述: BS_PeopleService 业务服务类
    /// 4.任务编号: 人员信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_People_Service : RepositoryFactory<BS_PeopleEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_PeopleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ID]
                      ,[Code]
                      ,[Name]
                      ,[Sex]
                      ,[CertificateCode]
                      ,[MobilePhone]
                      ,[Department_ID]
                      ,[Position_ID]
                      ,[Job_ID]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[MachineCode]
                      ,[PTeamCode]
                  FROM [dbo].[BS_People] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["ID"].IsEmpty())
                {
                    //sql.Append($" AND ID = N'{queryParam["ID"]}'");
                    sql.Append($" AND ID like N'%{queryParam["ID"]}%'");
                }
                //编码 是否为空进行查询
                if (!queryParam["Code"].IsEmpty())
                {
                    //sql.Append($" AND Code = N'{queryParam["Code"]}'");
                    sql.Append($" AND Code like N'%{queryParam["Code"]}%'");
                }
                //名称 是否为空进行查询
                if (!queryParam["Name"].IsEmpty())
                {
                    //sql.Append($" AND Name = N'{queryParam["Name"]}'");
                    sql.Append($" AND Name like N'%{queryParam["Name"]}%'");
                }
                //性别 是否为空进行查询
                if (!queryParam["Sex"].IsEmpty())
                {
                    //sql.Append($" AND Sex = N'{queryParam["Sex"]}'");
                    sql.Append($" AND Sex like N'%{queryParam["Sex"]}%'");
                }
                //身份证 是否为空进行查询
                if (!queryParam["CertificateCode"].IsEmpty())
                {
                    //sql.Append($" AND CertificateCode = N'{queryParam["CertificateCode"]}'");
                    sql.Append($" AND CertificateCode like N'%{queryParam["CertificateCode"]}%'");
                }
                //电话 是否为空进行查询
                if (!queryParam["MobilePhone"].IsEmpty())
                {
                    //sql.Append($" AND MobilePhone = N'{queryParam["MobilePhone"]}'");
                    sql.Append($" AND MobilePhone like N'%{queryParam["MobilePhone"]}%'");
                }
                //部门ID 是否为空进行查询
                if (!queryParam["Department_ID"].IsEmpty())
                {
                    //sql.Append($" AND Department_ID = N'{queryParam["Department_ID"]}'");
                    sql.Append($" AND Department_ID like N'%{queryParam["Department_ID"]}%'");
                }
                //岗位 是否为空进行查询
                if (!queryParam["Position_ID"].IsEmpty())
                {
                    //sql.Append($" AND Position_ID = N'{queryParam["Position_ID"]}'");
                    sql.Append($" AND Position_ID like N'%{queryParam["Position_ID"]}%'");
                }
                //职务 是否为空进行查询
                if (!queryParam["Job_ID"].IsEmpty())
                {
                    //sql.Append($" AND Job_ID = N'{queryParam["Job_ID"]}'");
                    sql.Append($" AND Job_ID like N'%{queryParam["Job_ID"]}%'");
                }
                //是否生效1生效 是否为空进行查询
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
                
                //机台编码 是否为空进行查询
                if (!queryParam["MachineCode"].IsEmpty())
                {
                    //sql.Append($" AND MachineCode = N'{queryParam["MachineCode"]}'");
                    sql.Append($" AND MachineCode like N'%{queryParam["MachineCode"]}%'");
                }
                //生产小组编码 是否为空进行查询
                if (!queryParam["PTeamCode"].IsEmpty())
                {
                    //sql.Append($" AND PTeamCode = N'{queryParam["PTeamCode"]}'");
                    sql.Append($" AND PTeamCode like N'%{queryParam["PTeamCode"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ID]
                      ,[Code]
                      ,[Name]
                      ,[Sex]
                      ,[CertificateCode]
                      ,[MobilePhone]
                      ,[Department_ID]
                      ,[Position_ID]
                      ,[Job_ID]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[MachineCode]
                      ,[PTeamCode]
                  FROM [dbo].[BS_People] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["ID"].IsEmpty())
                {
                    //sql.Append($" AND ID = N'{queryParam["ID"]}'");
                    sql.Append($" AND ID like N'%{queryParam["ID"]}%'");
                }
                //编码 是否为空进行查询
                if (!queryParam["Code"].IsEmpty())
                {
                    //sql.Append($" AND Code = N'{queryParam["Code"]}'");
                    sql.Append($" AND Code like N'%{queryParam["Code"]}%'");
                }
                //名称 是否为空进行查询
                if (!queryParam["Name"].IsEmpty())
                {
                    //sql.Append($" AND Name = N'{queryParam["Name"]}'");
                    sql.Append($" AND Name like N'%{queryParam["Name"]}%'");
                }
                //性别 是否为空进行查询
                if (!queryParam["Sex"].IsEmpty())
                {
                    //sql.Append($" AND Sex = N'{queryParam["Sex"]}'");
                    sql.Append($" AND Sex like N'%{queryParam["Sex"]}%'");
                }
                //身份证 是否为空进行查询
                if (!queryParam["CertificateCode"].IsEmpty())
                {
                    //sql.Append($" AND CertificateCode = N'{queryParam["CertificateCode"]}'");
                    sql.Append($" AND CertificateCode like N'%{queryParam["CertificateCode"]}%'");
                }
                //电话 是否为空进行查询
                if (!queryParam["MobilePhone"].IsEmpty())
                {
                    //sql.Append($" AND MobilePhone = N'{queryParam["MobilePhone"]}'");
                    sql.Append($" AND MobilePhone like N'%{queryParam["MobilePhone"]}%'");
                }
                //部门ID 是否为空进行查询
                if (!queryParam["Department_ID"].IsEmpty())
                {
                    //sql.Append($" AND Department_ID = N'{queryParam["Department_ID"]}'");
                    sql.Append($" AND Department_ID like N'%{queryParam["Department_ID"]}%'");
                }
                //岗位 是否为空进行查询
                if (!queryParam["Position_ID"].IsEmpty())
                {
                    //sql.Append($" AND Position_ID = N'{queryParam["Position_ID"]}'");
                    sql.Append($" AND Position_ID like N'%{queryParam["Position_ID"]}%'");
                }
                //职务 是否为空进行查询
                if (!queryParam["Job_ID"].IsEmpty())
                {
                    //sql.Append($" AND Job_ID = N'{queryParam["Job_ID"]}'");
                    sql.Append($" AND Job_ID like N'%{queryParam["Job_ID"]}%'");
                }
                //是否生效1生效 是否为空进行查询
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
                
                //机台编码 是否为空进行查询
                if (!queryParam["MachineCode"].IsEmpty())
                {
                    //sql.Append($" AND MachineCode = N'{queryParam["MachineCode"]}'");
                    sql.Append($" AND MachineCode like N'%{queryParam["MachineCode"]}%'");
                }
                //生产小组编码 是否为空进行查询
                if (!queryParam["PTeamCode"].IsEmpty())
                {
                    //sql.Append($" AND PTeamCode = N'{queryParam["PTeamCode"]}'");
                    sql.Append($" AND PTeamCode like N'%{queryParam["PTeamCode"]}%'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_PeopleEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [ID]
                      ,[Code]
                      ,[Name]
                      ,[Sex]
                      ,[CertificateCode]
                      ,[MobilePhone]
                      ,[Department_ID]
                      ,[Position_ID]
                      ,[Job_ID]
                      ,[IsEnabled]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                      ,[MachineCode]
                      ,[PTeamCode]
                  FROM [dbo].[BS_People] where 1=1 ");
            if (!checkType.IsEmpty())
            {
                //sql.Append($@" and ID = N'{checkType}' ");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, BS_PeopleEntity entity, out string msg)
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
                    if (string.IsNullOrEmpty(entity.ID))
                    {
                        entity.Create();
                    }
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }
        
        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<BS_PeopleEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_PeopleEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[BS_People] set ");
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
                            sql.Append(sql_temp.ToString().TrimEnd(',')  + $" WHERE ID='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    //n = this.BaseRepository().Insert(entity_list);
                    StringBuilder sql = new StringBuilder();
                    sql.Append($@"INSERT INTO [dbo].[BS_People] (
                                            [ID]
                                            ,[Code]
                                            ,[Name]
                                            ,[Sex]
                                            ,[CertificateCode]
                                            ,[MobilePhone]
                                            ,[Department_ID]
                                            ,[Position_ID]
                                            ,[Job_ID]
                                            ,[IsEnabled]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModifyBy]
                                            ,[ModifyTime]
                                            ,[MachineCode]
                                            ,[PTeamCode]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                '{Save_obj.ID}'
                                ,N'{Save_obj.Code}'
                                ,N'{Save_obj.Name}'
                                ,N'{Save_obj.Sex}'
                                ,N'{Save_obj.CertificateCode}'
                                ,N'{Save_obj.MobilePhone}'
                                ,N'{Save_obj.Department_ID}'
                                ,N'{Save_obj.Position_ID}'
                                ,N'{Save_obj.Job_ID}'
                                ,'{(Save_obj.IsEnabled == true ? 1:0)}'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModifyBy}'
                                ,'{(Save_obj.ModifyTime == null? DateTime.Now:Save_obj.ModifyTime)}'
                                ,N'{Save_obj.MachineCode}'
                                ,N'{Save_obj.PTeamCode}'
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            BS_PeopleEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                //删除禁用标记
                //entity.IsDeleted = true;
                this.BaseRepository().Delete(entity);
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
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
                sql.Append($@"DELETE FROM [dbo].[BS_People] WHERE ID=N'{keyValue}'");
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回BS_PeopleEntity</returns>
        public BS_PeopleEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回BS_PeopleEntity</returns>
        public BS_PeopleEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.ID == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_PeopleEntity 对象</returns>
        public BS_PeopleEntity Get_ExpressionEntity(Expression<Func<BS_PeopleEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_PeopleEntity 列表</returns>
        public IEnumerable<BS_PeopleEntity> Get_ExpressionList(Expression<Func<BS_PeopleEntity, bool>> condition)
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
        //    RepositoryFactory<BS_PeopleEntity> bomService = new RepositoryFactory<BS_PeopleEntity>();
        
        //    BS_PeopleEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    BS_PeopleDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.BS_People_ID == entity.ID).FirstOrDefault();
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_PeopleEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<BS_PeopleEntity> BS_PeopleEntity_list =  db2.FindList<BS_PeopleEntity>(sql.ToString());
                return BS_PeopleEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
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
                DataTable BS_PeopleEntity_DataTable = db2.FindTable(sql.ToString());
                return BS_PeopleEntity_DataTable;
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
        /// 创　　建: admin
        /// 创建日期: 2021-10-24 16:19:42
        /// 任务编号: 人员信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Code] as '编码'
                      ,[Name] as '名称'
                      ,[Sex] as '性别'
                      ,[CertificateCode] as '身份证'
                      ,[MobilePhone] as '电话'
                      ,[Department_ID] as '部门ID'
                      ,[Position_ID] as '岗位'
                      ,[Job_ID] as '职务'
                      ,[IsEnabled] as '是否生效1生效'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                      ,[MachineCode] as '机台编码'
                      ,[PTeamCode] as '生产小组编码'
                  FROM [dbo].[BS_People] where 1=1 ");
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
                string saveFileName = "人员信息_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("人员信息", dt, true);
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
        #region 同步OA接口
        public void SaveOABS_People(List<BS_PeopleEntity> entity)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    if (string.IsNullOrEmpty(item.FactoryCode))
                    {
                        throw new Exception("工厂编码必填");
                    }
                    if (string.IsNullOrEmpty(item.FactoryName))
                    {
                        throw new Exception("工厂编码必填");
                    }
                    if (string.IsNullOrEmpty(item.Code))
                    {
                        throw new Exception("员工编码必填");
                    }
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        throw new Exception("姓名编码必填");
                    }
                    if (string.IsNullOrEmpty(item.Sex))
                    {
                        throw new Exception("性别必填");
                    }
                    if (string.IsNullOrEmpty(item.Sex))
                    {
                        throw new Exception("性别必填");
                    }
                    //判断人员是否存在
                    var people = Get_ExpressionEntity(t=>t.Code==item.Code);
                    if (people == null)
                    {
                        if (item.IsEnabled == true)
                        {
                            item.Create();
                            item.CreateTime = DateTime.Now;
                            db.Insert(item);
                        }

                    }
                    else
                    {

                   
                    if (item.IsEnabled==true)
                    {

                            people.FactoryCode = item.FactoryCode;
                            people.FactoryName = item.FactoryName;
                            people.Code = item.Code;
                            people.Sex = item.Sex;
                            people.Name = item.Name;
                            people.MobilePhone = item.MobilePhone;
                            people.Department_ID = item.Department_ID;
                            people.Position_ID = item.Position_ID;
                            people.IsEnabled = item.IsEnabled;
                            people.ModifyTime = DateTime.Now;
                            db.Update(people);
                        }
                    else
                    {
                            people.FactoryName = " ";
                            people.FactoryCode = " ";
                            people.IsEnabled = false;
                            db.Update(people);

                    }
                    }


                }

                    db.Commit();
            }
            catch (Exception ex)
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
