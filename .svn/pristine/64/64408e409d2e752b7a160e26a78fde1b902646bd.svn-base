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
    /// 1.创建日期: 2021-08-24
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_FormulaRecordDetailService 业务服务类
    /// 4.任务编号: 小料喂料记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_FormulaRecordDetail_Service : RepositoryFactory<PM_FormulaRecordDetailEntity>, PM_FormulaRecordDetailIService
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_FormulaRecordDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FormulaId]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[Unit]
                      ,[ActQty]
                      ,[TheoryQty]
                      ,[BatchNo]
                  FROM [dbo].[PM_FormulaRecordDetail] where 1=1  ");
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
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND FactoryCode like N'%{queryParam["FactoryCode"]}%'");
                }
                //配方类型 是否为空进行查询
                if (!queryParam["FormulaType"].IsEmpty())
                {
                    //sql.Append($" AND FormulaType = N'{queryParam["FormulaType"]}'");
                    sql.Append($" AND FormulaType like N'%{queryParam["FormulaType"]}%'");
                }
                //配方编码 是否为空进行查询
                if (!queryParam["FormulaCode"].IsEmpty())
                {
                    //sql.Append($" AND FormulaCode = N'{queryParam["FormulaCode"]}'");
                    sql.Append($" AND FormulaCode like N'%{queryParam["FormulaCode"]}%'");
                }
                //配方名称 是否为空进行查询
                if (!queryParam["FormulaName"].IsEmpty())
                {
                    //sql.Append($" AND FormulaName = N'{queryParam["FormulaName"]}'");
                    sql.Append($" AND FormulaName like N'%{queryParam["FormulaName"]}%'");
                }
                //配方生效时间 是否为空进行查询
                if (!queryParam["FormulaTime"].IsEmpty())
                {
                    //sql.Append($" AND FormulaTime = N'{queryParam["FormulaTime"]}'");
                    sql.Append($" AND FormulaTime like N'%{queryParam["FormulaTime"]}%'");
                }
                //配料机台 是否为空进行查询
                if (!queryParam["FormulaMachine"].IsEmpty())
                {
                    //sql.Append($" AND FormulaMachine = N'{queryParam["FormulaMachine"]}'");
                    sql.Append($" AND FormulaMachine like N'%{queryParam["FormulaMachine"]}%'");
                }
                //配料付数 是否为空进行查询
                if (!queryParam["FormulaNum"].IsEmpty())
                {
                    //sql.Append($" AND FormulaNum = N'{queryParam["FormulaNum"]}'");
                    sql.Append($" AND FormulaNum like N'%{queryParam["FormulaNum"]}%'");
                }
                //报工班组 是否为空进行查询
                if (!queryParam["UserGroup"].IsEmpty())
                {
                    //sql.Append($" AND UserGroup = N'{queryParam["UserGroup"]}'");
                    sql.Append($" AND UserGroup like N'%{queryParam["UserGroup"]}%'");
                }
                //报工人 是否为空进行查询
                if (!queryParam["Creator"].IsEmpty())
                {
                    //sql.Append($" AND Creator = N'{queryParam["Creator"]}'");
                    sql.Append($" AND Creator like N'%{queryParam["Creator"]}%'");
                }
                //报工时间 是否为空进行查询
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT 
                      F.[Id]
                      , F.[FormulaId]
                      , F.[MaterialCode]
                      , F.[MaterialName]
                      , F.[Unit]
                      , F.[ActQty]
                      , F.[TheoryQty],F.UnitConsome
                      , F.[BatchNo],V.ItemName UnitName
                  FROM [dbo].[PM_FormulaRecordDetail] F
				  LEFT JOIN dbo.V_DataDictionary V ON V.EnCode='Unit' AND V.ItemValue=F.Unit
				  WHERE 1=1  ");
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
                //报工ID 是否为空进行查询
                if (!queryParam["FormulaId"].IsEmpty())
                {
                    //sql.Append($" AND FormulaId = N'{queryParam["FormulaId"]}'");
                    sql.Append($" AND FormulaId like N'%{queryParam["FormulaId"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //单位 是否为空进行查询
                if (!queryParam["Unit"].IsEmpty())
                {
                    //sql.Append($" AND Unit = N'{queryParam["Unit"]}'");
                    sql.Append($" AND Unit like N'%{queryParam["Unit"]}%'");
                }
                //实际消耗 是否为空进行查询
                if (!queryParam["ActQty"].IsEmpty())
                {
                    //sql.Append($" AND ActQty = N'{queryParam["ActQty"]}'");
                    sql.Append($" AND ActQty like N'%{queryParam["ActQty"]}%'");
                }
                //理论消耗 是否为空进行查询
                if (!queryParam["TheoryQty"].IsEmpty())
                {
                    //sql.Append($" AND TheoryQty = N'{queryParam["TheoryQty"]}'");
                    sql.Append($" AND TheoryQty like N'%{queryParam["TheoryQty"]}%'");
                }
                //消耗批次 是否为空进行查询
                if (!queryParam["BatchNo"].IsEmpty())
                {
                    //sql.Append($" AND BatchNo = N'{queryParam["BatchNo"]}'");
                    sql.Append($" AND BatchNo like N'%{queryParam["BatchNo"]}%'");
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_FormulaRecordDetailEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FormulaId]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[Unit]
                      ,[ActQty]
                      ,[TheoryQty]
                      ,[BatchNo]
                  FROM [dbo].[PM_FormulaRecordDetail] where 1=1  ");
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PM_FormulaRecordDetailEntity entity, out string msg)
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_FormulaRecordDetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_FormulaRecordDetailEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[PM_FormulaRecordDetail] set ");
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
                throw ex;
            }
            return n;
        }
        
        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            PM_FormulaRecordDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
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
                sql.Append($@"DELETE FROM [dbo].[PM_FormulaRecordDetail] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_FormulaRecordDetailEntity</returns>
        public PM_FormulaRecordDetailEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_FormulaRecordDetailEntity</returns>
        public PM_FormulaRecordDetailEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_FormulaRecordDetailEntity 对象</returns>
        public PM_FormulaRecordDetailEntity Get_ExpressionEntity(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_FormulaRecordDetailEntity 列表</returns>
        public IEnumerable<PM_FormulaRecordDetailEntity> Get_ExpressionList(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition)
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
        //    RepositoryFactory<PM_FormulaRecordDetailEntity> bomService = new RepositoryFactory<PM_FormulaRecordDetailEntity>();
        
        //    PM_FormulaRecordDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    PM_FormulaRecordDetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.PM_FormulaRecordDetail_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<PM_FormulaRecordDetailEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<PM_FormulaRecordDetailEntity> PM_FormulaRecordDetailEntity_list =  db2.FindList<PM_FormulaRecordDetailEntity>(sql.ToString());
                return PM_FormulaRecordDetailEntity_list;
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
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
                DataTable PM_FormulaRecordDetailEntity_DataTable = db2.FindTable(sql.ToString());
                return PM_FormulaRecordDetailEntity_DataTable;
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
        /// 创建日期: 2021-08-24 09:50:54
        /// 任务编号: 小料喂料记录
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FormulaId] as '报工ID'
                      ,[MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[Unit] as '单位'
                      ,[ActQty] as '实际消耗'
                      ,[TheoryQty] as '理论消耗'
                      ,[BatchNo] as '消耗批次'
                  FROM [dbo].[PM_FormulaRecordDetail] where 1=1  ");
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
                string saveFileName = "小料喂料记录_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("小料喂料记录", dt, true);
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

        public int RemoveForm(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
