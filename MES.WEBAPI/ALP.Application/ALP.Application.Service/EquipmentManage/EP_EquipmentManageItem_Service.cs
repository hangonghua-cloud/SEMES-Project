using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.EquipManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.EquipManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-12
    /// 2.创建作者: liyongguo
    /// 3.功能描述: EP_EquipmentManageItemService 业务服务类
    /// 4.任务编号: 设备台账
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class EP_EquipmentManageItem_Service : RepositoryFactory<EP_EquipmentManageItemEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentManageItemEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EMId]
                      ,[EquipCode]
                      ,[EquipName]
                      ,[EquipStatus]
                      ,[EquipSpec]
                      ,[Manufacturer]
                      ,[ProducedDate]
                      ,[UserDate]
                      ,[Remark]
                      ,[Attachment]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModfiyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentManageItem] where 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["EMId"].IsEmpty())
                {
                    sql.Append($" AND EMId = N'{queryParam["EMId"]}'");
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT E.[Id],
                               E.[EMId],
                               E.FactoryCode,
                               E.FactoryName,
                               E.[EquipCode],
                               E.[EquipName],
                               E.[EquipClass],
                               E.[EquipStatus],
                               E.[EquipSpec],
                               E.[Manufacturer],
                               E.[ProducedDate],
                               E.[UserDate],
                               E.[Remark],
                               E.[Attachment],
                               E.[Creator],
                               E.[CreateTime],
                               E.[ModfiyBy],
                               E.[ModifyTime],
                               V.ItemName EquipStatusName,
                               B.Name CreatorName
                        FROM [dbo].[EP_EquipmentManageItem] E
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'EquipmentStatus'
                                   AND V.ItemValue = E.EquipStatus
                            LEFT JOIN dbo.BS_People B
                                ON B.Code = E.Creator
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
                //设备台账Id 是否为空进行查询
                if (!queryParam["EMId"].IsEmpty())
                {
                    sql.Append($" AND EMId = N'{queryParam["EMId"]}'");
                }
                //配套设备编码 是否为空进行查询
                if (!queryParam["EquipCode"].IsEmpty())
                {
                    //sql.Append($" AND EquipCode = N'{queryParam["EquipCode"]}'");
                    sql.Append($" AND EquipCode like N'%{queryParam["EquipCode"]}%'");
                }
                //配套设备名称 是否为空进行查询
                if (!queryParam["EquipName"].IsEmpty())
                {
                    //sql.Append($" AND EquipName = N'{queryParam["EquipName"]}'");
                    sql.Append($" AND EquipName like N'%{queryParam["EquipName"]}%'");
                }
                //设备状态 是否为空进行查询
                if (!queryParam["EquipStatus"].IsEmpty())
                {
                    sql.Append($" AND EquipStatus = N'{queryParam["EquipStatus"]}'");
                }
                //规格型号 是否为空进行查询
                if (!queryParam["EquipSpec"].IsEmpty())
                {
                    //sql.Append($" AND EquipSpec = N'{queryParam["EquipSpec"]}'");
                    sql.Append($" AND EquipSpec like N'%{queryParam["EquipSpec"]}%'");
                }
                //生产厂家 是否为空进行查询
                if (!queryParam["Manufacturer"].IsEmpty())
                {
                    //sql.Append($" AND Manufacturer = N'{queryParam["Manufacturer"]}'");
                    sql.Append($" AND Manufacturer like N'%{queryParam["Manufacturer"]}%'");
                }
                //出厂日期 是否为空进行查询
                if (!queryParam["ProducedDate"].IsEmpty())
                {
                    //sql.Append($" AND ProducedDate = N'{queryParam["ProducedDate"]}'");
                    sql.Append($" AND ProducedDate like N'%{queryParam["ProducedDate"]}%'");
                }
                //使用日期 是否为空进行查询
                if (!queryParam["UserDate"].IsEmpty())
                {
                    //sql.Append($" AND UserDate = N'{queryParam["UserDate"]}'");
                    sql.Append($" AND UserDate like N'%{queryParam["UserDate"]}%'");
                }
                //备注 是否为空进行查询
                if (!queryParam["Remark"].IsEmpty())
                {
                    //sql.Append($" AND Remark = N'{queryParam["Remark"]}'");
                    sql.Append($" AND Remark like N'%{queryParam["Remark"]}%'");
                }
                //附件 是否为空进行查询
                if (!queryParam["Attachment"].IsEmpty())
                {
                    //sql.Append($" AND Attachment = N'{queryParam["Attachment"]}'");
                    sql.Append($" AND Attachment like N'%{queryParam["Attachment"]}%'");
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentManageItemEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[EMId]
                      ,[EquipCode]
                      ,[EquipName]
                      ,[EquipStatus]
                      ,[EquipSpec]
                      ,[Manufacturer]
                      ,[ProducedDate]
                      ,[UserDate]
                      ,[Remark]
                      ,[Attachment]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModfiyBy]
                      ,[ModifyTime]
                  FROM [dbo].[EP_EquipmentManageItem] where 1=1  ");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-19 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentManageItemEntity> GetListBytime(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"
                      SELECT top 1
                      [Id]
                      ,[EMId]
                      ,[EquipCode]
                      ,[EquipName]
                      ,[EquipStatus]
                      ,[EquipSpec]
                      ,[Manufacturer]
                      ,[ProducedDate]
                      ,[UserDate]
                      ,[Remark]
                      ,[Attachment]
                      ,[Creator]
                      ,[CreateTime]
                      ,[ModfiyBy]
                      ,[ModifyTime]  
                 FROM [dbo].[EP_EquipmentManageItem] where 1=1");
            if (!checkType.IsEmpty())
            {
                sql.Append($@" and EMId = N'{checkType}' ");
            }
            sql.Append($@"order by  CreateTime desc");
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, EP_EquipmentManageItemEntity entity, out string msg)
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
                    //entity.Create();
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<EP_EquipmentManageItemEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<EP_EquipmentManageItemEntity> entity_list, out string msg)
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
                            sql_temp.Append("UPDATE [dbo].[EP_EquipmentManageItem] set ");
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
                    sql.Append($@"INSERT INTO [dbo].[EP_EquipmentManageItem] (
                                            [Id]
                                            ,[EMId]
                                            ,[EquipCode]
                                            ,[EquipName]
                                            ,[EquipStatus]
                                            ,[EquipSpec]
                                            ,[Manufacturer]
                                            ,[ProducedDate]
                                            ,[UserDate]
                                            ,[Remark]
                                            ,[Attachment]
                                            ,[Creator]
                                            ,[CreateTime]
                                            ,[ModfiyBy]
                                            ,[ModifyTime]
                                    ) VALUES ");
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            sql.Append($@"(
                                N'{Save_obj.Id}'
                                ,N'{Save_obj.EMId}'
                                ,N'{Save_obj.EquipCode}'
                                ,N'{Save_obj.EquipName}'
                                ,N'{Save_obj.EquipStatus}'
                                ,N'{Save_obj.EquipSpec}'
                                ,N'{Save_obj.Manufacturer}'
                                ,N'{Save_obj.ProducedDate}'
                                ,N'{Save_obj.UserDate}'
                                ,N'{Save_obj.Remark}'
                                ,N'{Save_obj.Attachment}'
                                ,N'{Save_obj.Creator}'
                                ,'{(Save_obj.CreateTime == null? DateTime.Now:Save_obj.CreateTime)}'
                                ,N'{Save_obj.ModfiyBy}'
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            int result = 0;
            EP_EquipmentManageItemEntity entity = this.BaseRepository().FindEntity(keyValue);
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
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
                sql.Append($@"DELETE FROM [dbo].[EP_EquipmentManageItem] WHERE Id=N'{keyValue}'");
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回EP_EquipmentManageItemEntity</returns>
        public EP_EquipmentManageItemEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        
        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回EP_EquipmentManageItemEntity</returns>
        public EP_EquipmentManageItemEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentManageItemEntity 对象</returns>
        public EP_EquipmentManageItemEntity Get_ExpressionEntity(Expression<Func<EP_EquipmentManageItemEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }
        
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回EP_EquipmentManageItemEntity 列表</returns>
        public IEnumerable<EP_EquipmentManageItemEntity> Get_ExpressionList(Expression<Func<EP_EquipmentManageItemEntity, bool>> condition)
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
        //    RepositoryFactory<EP_EquipmentManageItemEntity> bomService = new RepositoryFactory<EP_EquipmentManageItemEntity>();
        
        //    EP_EquipmentManageItemEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    EP_EquipmentManageItemDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.EP_EquipmentManageItem_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentManageItemEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<EP_EquipmentManageItemEntity> EP_EquipmentManageItemEntity_list =  db2.FindList<EP_EquipmentManageItemEntity>(sql.ToString());
                return EP_EquipmentManageItemEntity_list;
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
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
                DataTable EP_EquipmentManageItemEntity_DataTable = db2.FindTable(sql.ToString());
                return EP_EquipmentManageItemEntity_DataTable;
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
        /// 创建日期: 2021-10-12 09:43:58
        /// 任务编号: 设备台账
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [EMId] as '设备台账Id'
                      ,[EquipCode] as '配套设备编码'
                      ,[EquipName] as '配套设备名称'
                      ,[EquipStatus] as '设备状态'
                      ,[EquipSpec] as '规格型号'
                      ,[Manufacturer] as '生产厂家'
                      ,[ProducedDate] as '出厂日期'
                      ,[UserDate] as '使用日期'
                      ,[Remark] as '备注'
                      ,[Attachment] as '附件'
                      ,[Creator] as '创建人'
                      ,[CreateTime] as '创建时间'
                      ,[ModfiyBy] as '修改人'
                      ,[ModifyTime] as '修改时间'
                  FROM [dbo].[EP_EquipmentManageItem] where 1=1  ");
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
                string saveFileName = "设备台账_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";
                
                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("设备台账", dt, true);
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
