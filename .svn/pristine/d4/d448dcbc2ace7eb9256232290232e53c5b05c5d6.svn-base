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
using ALP.Data;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Service.SystemManage;

namespace ALP.Application.Service.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: BS_BOMService 业务服务类
    /// 4.任务编号: 供应商管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_BOM_Service : RepositoryFactory<BS_BOMEntity>, BS_BOMIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_BOMEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode]
                      ,[MaterialClass]
                      ,[BOMCode]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[UnitNum]
                      ,[Process]
                      ,[Creator],OrderType
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[BS_BOM] where 1=1  ");
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
                    sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //物料分类 是否为空进行查询
                if (!queryParam["MaterialClass"].IsEmpty())
                {
                    //sql.Append($" AND MaterialClass = N'{queryParam["MaterialClass"]}'");
                    sql.Append($" AND MaterialClass like N'%{queryParam["MaterialClass"]}%'");
                }
                //BOM编码 是否为空进行查询
                if (!queryParam["BOMCode"].IsEmpty())
                {
                    //sql.Append($" AND BOMCode = N'{queryParam["BOMCode"]}'");
                    sql.Append($" AND BOMCode like N'%{queryParam["BOMCode"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料编码 工单获取最新BOM使用
                if (!queryParam["MaterialCode1"].IsEmpty())
                {
                    sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode1"]}'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //单位数量 是否为空进行查询
                if (!queryParam["OrderType"].IsEmpty())
                {
                    //sql.Append($" AND UnitNum = N'{queryParam["UnitNum"]}'");
                    sql.Append($" AND OrderType = N'{queryParam["OrderType"]}'");
                }
                //工艺 是否为空进行查询
                if (!queryParam["Process"].IsEmpty())
                {
                    //sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    sql.Append($" AND Process = N'{queryParam["Process"]}'");
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT B.Id,
                               B.BOMCode,
                               B.FactoryCode,
							   B.FactoryName,
                               B.OrderType,
                               B.MaterialCode,
                               B.MaterialName,
							   M.Spec,
                               B.MaterialClass,
                               B.UnitNum,
                               B.Process,
							   b.Creator,
							   b.CreateTime,
                               M.MaterialCode AS MaterialCode_BS,
                               M.MaterialName AS MaterialName_BS,
                               BP.ProcessName ProcessName,
                               V1.ItemName MaterialClassName,
                               M.UnitName,
                               B.UnitName as TraitUnitName,
                               B.isDefault,
							   b.Remark
                        FROM [dbo].[BS_BOM] B
                            LEFT JOIN dbo.Base_Material M
                                ON M.MaterialCode = B.MaterialCode
                            LEFT JOIN dbo.V_DataDictionary V1
                                ON V1.EnCode = 'MaterialType'
                                   AND V1.ItemValue = B.MaterialClass
                            LEFT JOIN dbo.BS_Process BP
                                ON BP.ProcessCode = B.Process AND b.FactoryCode=bp.FactoryCode
                        WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["OrderType"].IsEmpty())
                {
                    sql.Append($" AND B.OrderType = N'{queryParam["OrderType"]}'");
                }
                //工厂编码 是否为空进行查询
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    //sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    sql.Append($" AND B.FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                //物料分类 是否为空进行查询
                if (!queryParam["MaterialClass"].IsEmpty())
                {
                    //sql.Append($" AND MaterialClass = N'{queryParam["MaterialClass"]}'");
                    sql.Append($" AND B.MaterialClass like N'%{queryParam["MaterialClass"]}%'");
                }
                //BOM编码 是否为空进行查询
                if (!queryParam["BOMCode"].IsEmpty())
                {
                    //sql.Append($" AND BOMCode = N'{queryParam["BOMCode"]}'");
                    sql.Append($" AND BOMCode like N'%{queryParam["BOMCode"]}%'");
                }
                //物料编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND MaterialCode = N'{queryParam["MaterialCode"]}'");
                    sql.Append($" AND B.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //物料编码 工单获取最新BOM使用
                if (!queryParam["MaterialCode1"].IsEmpty())
                {
                    sql.Append($" AND B.MaterialCode = N'{queryParam["MaterialCode1"]}'");
                }
                //物料名称 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND MaterialName = N'{queryParam["MaterialName"]}'");
                    sql.Append($" AND B.MaterialName like N'%{queryParam["MaterialName"]}%'");
                }
                //规格 是否为空进行查询
                if (!queryParam["Spec"].IsEmpty())
                {
                    sql.Append($" AND M.Spec like N'%{queryParam["Spec"]}%'");
                }
                //单位数量 是否为空进行查询
                if (!queryParam["UnitNum"].IsEmpty())
                {
                    //sql.Append($" AND UnitNum = N'{queryParam["UnitNum"]}'");
                    sql.Append($" AND B.UnitNum like N'%{queryParam["UnitNum"]}%'");
                }
                //工艺 是否为空进行查询
                if (!queryParam["Process"].IsEmpty())
                {
                    //sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    sql.Append($" AND B.Process like N'%{queryParam["Process"]}%'");
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
                if (!queryParam["BOMType"].IsEmpty())
                {
                    sql.Append($" AND BOMType = N'{queryParam["BOMType"]}'");
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="factoryCode"></param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_BOMEntity> GetList(string checkType, string factoryCode, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[FactoryCode],OrderType
                      ,[MaterialClass]
                      ,[BOMCode]
                      ,[MaterialCode]
                      ,[MaterialName]
                      ,[UnitNum]
                      ,[Process]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModifyBy]
                      ,[ModifyTime]
                  FROM [dbo].[BS_BOM] where 1=1  ");
            if (!checkType.IsEmpty())
            {
                sql.Append($@" and MaterialCode = N'{checkType}' ");
            }
            if (!string.IsNullOrEmpty(factoryCode))
            {
                sql.Append($@" and FactoryCode = N'{factoryCode}' ");
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, BS_BOMEntity entity, out string msg)
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<BS_BOMEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_BOMEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[BS_BOM] set ");
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
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
                sql.Append($@"DELETE FROM [dbo].[BS_BOM] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回BS_BOMEntity</returns>
        public BS_BOMEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回BS_BOMEntity</returns>
        public BS_BOMEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_BOMEntity 对象</returns>
        public BS_BOMEntity Get_ExpressionEntity(Expression<Func<BS_BOMEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回BS_BOMEntity 列表</returns>
        public IEnumerable<BS_BOMEntity> Get_ExpressionList(Expression<Func<BS_BOMEntity, bool>> condition)
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
        //    RepositoryFactory<BS_BOMEntity> bomService = new RepositoryFactory<BS_BOMEntity>();

        //    BS_BOMEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    BS_BOMDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.BS_BOM_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BS_BOMEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<BS_BOMEntity> BS_BOMEntity_list = db2.FindList<BS_BOMEntity>(sql.ToString());
                return BS_BOMEntity_list;
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
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
                DataTable BS_BOMEntity_DataTable = db2.FindTable(sql.ToString());
                return BS_BOMEntity_DataTable;
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
        public bool GetSerialNO(string SeqCode, int index, out string returnNum, out string messageCode)
        {
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@Index", SqlDbType.Int),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Value = index;
            parameters[2].Direction = ParameterDirection.Output;
            parameters[3].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetMultipleSerialNO", parameters);
                //返回参数值
                returnNum = parameters[2].Value.ToString();
                messageCode = parameters[3].Value.ToString();
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
        /// 创建日期: 2021-07-26 18:04:37
        /// 任务编号: 供应商管理
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [FactoryCode] as '工厂编码'
                      ,[MaterialClass] as '物料分类'
                      ,[BOMCode] as 'BOM编码'
                      ,[MaterialCode] as '物料编码'
                      ,[MaterialName] as '物料名称'
                      ,[UnitNum] as '单位数量'
                      ,[Process] as '工艺'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModifyBy] as '最后修改人'
                      ,[ModifyTime] as '最后修改时间'
                  FROM [dbo].[BS_BOM] where 1=1  ");
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
                string saveFileName = "供应商管理_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("供应商管理", dt, true);
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


        public DataTable GetFormulaBOM(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT B.Id,
                               B.FactoryCode,
	                           B.FactoryName,
                               B.BOMCode,
                               B.MaterialCode,
                               B.MaterialName,
                               B.UnitNum,
                               B.CreateTime,
                               M.MaterialClass,
                               M.SmallClass,
                               V.ItemName SmallClassName,
                               B.OrderType
                        FROM dbo.BS_BOM B
                            INNER JOIN dbo.Base_Material M
                                ON B.MaterialCode = M.MaterialCode
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'MaterialSmall'
                                   AND V.ItemValue = M.SmallClass
                        WHERE M.SmallClass IN ( 'WLPF', 'XLPF' ) ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND B.FactoryCode='{queryParam["FactoryCode"]}' ");
                }
                //配方类型
                if (!queryParam["SmallClass"].IsEmpty())
                {
                    sql.Append($" AND M.SmallClass LIKE '%{queryParam["SmallClass"]}%' ");
                }
                //Bom编码
                if (!queryParam["BOMCode"].IsEmpty())
                {
                    sql.Append($" AND B.BOMCode LIKE '%{queryParam["BOMCode"]}%' ");
                }
                //配方编码
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND B.MaterialCode LIKE '%{queryParam["MaterialCode"]}%' ");
                }
                //配方名称
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    sql.Append($" AND B.MaterialName LIKE '%{queryParam["MaterialName"]}%' ");
                }
                //开始时间
                if (!queryParam["StartTime"].IsEmpty())
                {
                    sql.Append($" AND B.CreateTime>='{queryParam["StartTime"]}' ");
                }
                //结束时间
                if (!queryParam["EndTime"].IsEmpty())
                {
                    sql.Append($" AND B.CreateTime<='{queryParam["EndTime"]}' ");
                }
            }
            if (pagination == null)
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            else
            {
                return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            }

        }
        # region SAPBOM接收
        /// <summary>
        /// 创建：jpf
        /// 创建时间：2024-3-6 08:31:30
        /// 描述： SAPBOM数据接收
        /// </summary>
        public void SaveSAPBS_BOM(List<SAPBS_BOMEntity> SAPBS_BOMS)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            try
            {
                foreach (var item in SAPBS_BOMS)
                {
                    var sapBOMEntity = item.BS_BOM;
                    var sapBOMItems = item.BS_BOMItems;
                    if (string.IsNullOrEmpty(sapBOMEntity.BOMCode))
                    {
                        throw new Exception("BOM编码必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.FactoryCode))
                    {
                        throw new Exception("工厂编码必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.FactoryName))
                    {
                        throw new Exception("工厂名称必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.OrderType))
                    {
                        throw new Exception("订单类型必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.MaterialCode))
                    {
                        throw new Exception("物料编码必须传！");
                    }
                    //将SAP物料编码转化为mes的物料编码
                    var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", sapBOMEntity.MaterialCode);
                    var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                    if (dtMaterial.Rows.Count == 0)
                    {
                        throw new Exception(sapBOMEntity.MaterialCode + "物料编码未同步");
                    }
                    sapBOMEntity.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                    if (string.IsNullOrEmpty(sapBOMEntity.MaterialName))
                    {
                        throw new Exception("物料名称必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.MaterialClass))
                    {
                        throw new Exception("物料分类必须传！");
                    }
                    if (sapBOMEntity.UnitNum == null)
                    {
                        throw new Exception("单位数量必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.UnitName))
                    {
                        throw new Exception("单位必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.Process))
                    {
                        throw new Exception("工艺路线必须传！");
                    }
                    if (string.IsNullOrEmpty(sapBOMEntity.BOMType))
                    {
                        throw new Exception("BOM类型必须传！");
                    }
                    //判断一下BOM主表数据是否存在
                    var bomEntity = Get_ExpressionList(t => t.FactoryCode == sapBOMEntity.FactoryCode && t.BOMCode == sapBOMEntity.BOMCode).FirstOrDefault();
                    string bomId = "";
                    if (sapBOMEntity.IsDeleted == false)
                    {
                        if (bomEntity == null)
                        {
                            sapBOMEntity.Create();
                            sapBOMEntity.CreateTime = DateTime.Now;
                            sapBOMEntity.Creator = "SAP";
                            db.Insert(sapBOMEntity);
                            bomId = sapBOMEntity.Id;
                        }
                        else
                        {
                            bomEntity.UnitNum = sapBOMEntity.UnitNum;
                            bomEntity.UnitName = sapBOMEntity.UnitName;
                            bomEntity.Process = sapBOMEntity.Process;
                            bomEntity.BOMType = sapBOMEntity.BOMType;
                            bomEntity.ModifyTime = DateTime.Now;
                            db.Update(bomEntity);
                            bomId = bomEntity.Id;
                        }
                        var arrMaterialCode = sapBOMItems.Select(t => t.MaterialCode).Distinct().ToArray();
                        var materialList = new Base_Material_Service().Get_ExpressionList(t => arrMaterialCode.Contains(t.SAPMaterialCode)).ToList();
                        //var bomItemList = new BS_BOMItems_Service().Get_ExpressionList(t => t.BOMId == bomId).ToList();
                       
                        foreach (var Bitem in sapBOMItems)
                        {
                            #region 数据校验
                            if (string.IsNullOrEmpty(Bitem.MaterialName))
                            {
                                throw new Exception("BOM明细物料名称不允许为空");
                            }
                            if (string.IsNullOrEmpty(Bitem.MaterialCode))
                            {
                                throw new Exception("BOM明细物料编码不允许为空");
                            }
                            if (Bitem.Num == null)
                            {
                                throw new Exception("BOM明细数量不允许为空");
                            }
                            if (string.IsNullOrEmpty(Bitem.Unit))
                            {
                                throw new Exception("BOM明细单位编码不允许为空");
                            }
                            if (string.IsNullOrEmpty(Bitem.UnitName))
                            {
                                throw new Exception("BOM明细单位名称不允许为空");
                            }
                            if (string.IsNullOrEmpty(Bitem.Warehouse))
                            {
                                throw new Exception("BOM明细库存地点不允许为空");
                            }
                            if (string.IsNullOrEmpty(Bitem.ConsumeProcess))
                            {
                                throw new Exception("BOM明细工序不允许为空");
                            }

                            //if (string.IsNullOrEmpty(Bitem.MaterialType))
                            //{
                            //    throw new Exception("BOM明细物料类别不允许为空");
                            //}
                            #endregion

                            Bitem.MaterialCode = materialList.Find(t => t.SAPMaterialCode == Bitem.MaterialCode)?.MaterialCode;
                            Bitem.Id = Guid.NewGuid().ToString();
                            Bitem.CreateTime = DateTime.Now;
                            Bitem.BOMId = bomId;
                            Bitem.FactoryCode = sapBOMEntity.FactoryCode;
                            Bitem.FactoryName = sapBOMEntity.FactoryName;
                            Bitem.BOMCode = sapBOMEntity.BOMCode;

                            ////判断BOM明细是否已经维护
                            //var bomItem = bomItemList.Find(t => t.MaterialCode == Bitem.MaterialCode);
                            //if (bomItem == null)
                            //{
                            //    Bitem.Id = Guid.NewGuid().ToString();
                            //    Bitem.CreateTime = DateTime.Now;
                            //    Bitem.BOMId = bomId;
                            //    Bitem.FactoryCode = sapBOMEntity.FactoryCode;
                            //    Bitem.FactoryName = sapBOMEntity.FactoryName;
                            //    Bitem.BOMCode = sapBOMEntity.BOMCode;
                            //    db.Insert(Bitem);
                            //}
                            //else
                            //{
                            //    bomItem.Unit = Bitem.Unit;
                            //    bomItem.UnitName = Bitem.UnitName;
                            //    bomItem.Warehouse = Bitem.Warehouse;
                            //    bomItem.ConsumeProcess = Bitem.ConsumeProcess;
                            //    bomItem.MaterialType = Bitem.MaterialType;
                            //    bomItem.ModifyTime = DateTime.Now;
                            //    bomItem.FactoryCode = sapBOMEntity.FactoryCode;
                            //    bomItem.FactoryName = sapBOMEntity.FactoryName;
                            //    bomItem.BOMCode = sapBOMEntity.BOMCode;
                            //    db.Update(bomItem);
                            //}
                        }
                        string msg = "";
                        new BS_BOMItems_Service().RemoveForm(t => t.BOMId == bomId);
                        new BS_BOMItems_Service().SaveEntity_List(false, "", sapBOMItems, out msg);
                    }
                    else
                    {
                        if (bomEntity == null)
                        {
                            throw new Exception(sapBOMEntity.BOMCode + "BOM不存在，不允许删除");
                        }

                        new BS_BOMItems_Service().RemoveForm(t => t.BOMId == bomEntity.Id);
                        RemoveForm(bomEntity.Id, null);
                        // db.Delete(bomEntity);
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
