using ALP.Application.Entity.EquipmentManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace ALP.Application.Service.EquipmentManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-26
    /// 2.创建作者: huxiao
    /// 3.功能描述: V_EP_EquipmentManageService 业务服务类
    /// 4.任务编号: 任务编号或模块名称
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class V_EP_EquipmentManage_Service : RepositoryFactory<V_EP_EquipmentManageEntity>
    { 
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<V_EP_EquipmentManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[TestMethodCoadinName]
                      ,[InstallationSiteName]
                      ,[ProcessBelongName]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[EquipmentTypeName]
                      ,[EquipmentType]
                      ,[CreateTime]
                  FROM [dbo].[V_EP_EquipmentManage] where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND Id = '{queryParam["Id"]}'");
                    //sql.Append($" AND Id like '%{queryParam["Id"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["TestMethodCoadinName"].IsEmpty())
                {
                    sql.Append($" AND TestMethodCoadinName = '{queryParam["TestMethodCoadinName"]}'");
                    //sql.Append($" AND TestMethodCoadinName like '%{queryParam["TestMethodCoadinName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["InstallationSiteName"].IsEmpty())
                {
                    sql.Append($" AND InstallationSiteName = '{queryParam["InstallationSiteName"]}'");
                    //sql.Append($" AND InstallationSiteName like '%{queryParam["InstallationSiteName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["ProcessBelongName"].IsEmpty())
                {
                    sql.Append($" AND ProcessBelongName = '{queryParam["ProcessBelongName"]}'");
                    //sql.Append($" AND ProcessBelongName like '%{queryParam["ProcessBelongName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    sql.Append($" AND EquipmentId = '{queryParam["EquipmentId"]}'");
                    //sql.Append($" AND EquipmentId like '%{queryParam["EquipmentId"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($" AND EquipmentName = '{queryParam["EquipmentName"]}'");
                    //sql.Append($" AND EquipmentName like '%{queryParam["EquipmentName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentTypeName"].IsEmpty())
                {
                    sql.Append($" AND EquipmentTypeName = '{queryParam["EquipmentTypeName"]}'");
                    //sql.Append($" AND EquipmentTypeName like '%{queryParam["EquipmentTypeName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    sql.Append($" AND EquipmentType = '{queryParam["EquipmentType"]}'");
                    //sql.Append($" AND EquipmentType like '%{queryParam["EquipmentType"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    sql.Append($" AND CreateTime = '{queryParam["CreateTime"]}'");
                    //sql.Append($" AND CreateTime like '%{queryParam["CreateTime"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = '{queryParam["queryName"]}'");
                    //sql.Append($" AND 关键名称 like '%{queryParam["queryName"]}%'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = '{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like '%{queryParam["queryCode"]}%'");
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
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[TestMethodCoadinName]
                      ,[InstallationSiteName]
                      ,[ProcessBelongName]
                      ,[EquipmentId]
                      ,[EquipmentName]
                      ,[EquipmentTypeName]
                      ,[EquipmentType]
                      ,[CreateTime]
                  FROM [dbo].[V_EP_EquipmentManage] where 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                // 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    sql.Append($" AND Id = '{queryParam["Id"]}'");
                    //sql.Append($" AND Id like '%{queryParam["Id"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["TestMethodCoadinName"].IsEmpty())
                {
                    sql.Append($" AND TestMethodCoadinName = '{queryParam["TestMethodCoadinName"]}'");
                    //sql.Append($" AND TestMethodCoadinName like '%{queryParam["TestMethodCoadinName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["InstallationSiteName"].IsEmpty())
                {
                    sql.Append($" AND InstallationSiteName = '{queryParam["InstallationSiteName"]}'");
                    //sql.Append($" AND InstallationSiteName like '%{queryParam["InstallationSiteName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["ProcessBelongName"].IsEmpty())
                {
                    sql.Append($" AND ProcessBelongName = '{queryParam["ProcessBelongName"]}'");
                    //sql.Append($" AND ProcessBelongName like '%{queryParam["ProcessBelongName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    sql.Append($" AND EquipmentId = '{queryParam["EquipmentId"]}'");
                    //sql.Append($" AND EquipmentId like '%{queryParam["EquipmentId"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($" AND EquipmentName = '{queryParam["EquipmentName"]}'");
                    //sql.Append($" AND EquipmentName like '%{queryParam["EquipmentName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentTypeName"].IsEmpty())
                {
                    sql.Append($" AND EquipmentTypeName = '{queryParam["EquipmentTypeName"]}'");
                    //sql.Append($" AND EquipmentTypeName like '%{queryParam["EquipmentTypeName"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    sql.Append($" AND EquipmentType = '{queryParam["EquipmentType"]}'");
                    //sql.Append($" AND EquipmentType like '%{queryParam["EquipmentType"]}%'");
                }
                // 是否为空进行查询
                if (!queryParam["CreateTime"].IsEmpty())
                {
                    sql.Append($" AND CreateTime = '{queryParam["CreateTime"]}'");
                    //sql.Append($" AND CreateTime like '%{queryParam["CreateTime"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["queryName"].IsEmpty())
                {
                    //sql.Append($" AND 关键名称 = '{queryParam["queryName"]}'");
                    //sql.Append($" AND 关键名称 like '%{queryParam["queryName"]}%'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = '{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like '%{queryParam["queryCode"]}%'");
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
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public List<dynamic> GetList(string checkType)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT [Id],
                               TestMethodCoadin,
                               [TestMethodCoadinName],
                               [InstallationSiteName],
                               [ProcessBelongName],
                               [EquipmentId],
                               [EquipmentName],
                               [EquipmentTypeName],
                               [EquipmentType],
                               [CreateTime]
                        FROM [dbo].[V_EP_EquipmentManage] where 1=1 ");
            if (!checkType.IsEmpty())
            {
                sql.Append($@" and EquipmentId = '{checkType}' ");
            }
            try
            {
                return this.BaseRepository().Query(sql.ToString());
            }
            catch (Exception ex)
            {
              
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, V_EP_EquipmentManageEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    ////日志服务类
                    //QM_LogManagement_Service _log_service = new QM_LogManagement_Service();
                    ////保存操作日志
                    //_log_service.SaveLog(entity, "编辑", "任务编号或模块名称", entity.UpdateByName, keyValue, this.BaseRepository().FindEntity(keyValue));
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    n = this.BaseRepository().Insert(entity);
                    ////日志服务类
                    //QM_LogManagement_Service _log_service = new QM_LogManagement_Service();
                    ////保存操作日志
                    //_log_service.SaveLog(entity, "新增", "任务编号或模块名称", entity.CreatedByName, entity.Id, null);
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
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
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
                ////日志服务类
                //QM_LogManagement_Service _log_service = new QM_LogManagement_Service();
                ////保存操作日志
                //_log_service.SaveLog(this.BaseRepository().FindEntity(keyValue), "删除", "任务编号或模块名称", UpdateByName, keyValue, null);
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
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
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
                sql.Append($@"DELETE FROM [dbo].[V_EP_EquipmentManage] WHERE Id='{keyValue}'");
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
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回V_EP_EquipmentManageEntity</returns>
        public V_EP_EquipmentManageEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        public V_EP_EquipmentManageEntity GetEntity(Expression<Func<V_EP_EquipmentManageEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回V_EP_EquipmentManageEntity</returns>
        public V_EP_EquipmentManageEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
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
        //    RepositoryFactory<V_EP_EquipmentManageEntity> bomService = new RepositoryFactory<V_EP_EquipmentManageEntity>();
        
        //    V_EP_EquipmentManageEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    V_EP_EquipmentManageDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.V_EP_EquipmentManage_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<V_EP_EquipmentManageEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
                IEnumerable<V_EP_EquipmentManageEntity> V_EP_EquipmentManageEntity_list =  db2.FindList<V_EP_EquipmentManageEntity>(sql.ToString());
                return V_EP_EquipmentManageEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }
        
        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: huxiao
        /// 创建日期: 2021-10-26 10:48:59
        /// 任务编号: 任务编号或模块名称
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
                DataTable V_EP_EquipmentManageEntity_DataTable = db2.FindTable(sql.ToString());
                return V_EP_EquipmentManageEntity_DataTable;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }


        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<V_EP_EquipmentManageEntity> GetList(Expression<Func<V_EP_EquipmentManageEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }


    }
}
