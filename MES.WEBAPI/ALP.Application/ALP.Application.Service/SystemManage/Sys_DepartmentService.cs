using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.SystemManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;

namespace ALP.Application.IService.SystemManage
{
    /// <summary>
    /// 部门管理接口
    /// </summary>
    public class Sys_DepartmentService : RepositoryFactory<Sys_DepartmentEntity>
    {
        #region 获取数据


        /// <summary>
        /// 查询工单分页列表
        /// </summary>

        /// <returns>返回分页列表</returns>
        public DataTable Get_PageData_Control(string name)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT   [ID],[Code] ItemValue ,[Name] ItemName
                      FROM  [dbo].[BS_Departments]
                      where [IsEffective]=1 ");

            if (!string.IsNullOrEmpty(name))
            {
                sql.Append($" and Name like '%{name}%' ");
            }

            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInFactoryCode()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT ResourceName,ResourceCode FROM BS_ModelWithResource WHERE ModelLeve = 'factory' ");
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable GetInOrgCode()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT ResourceName,ResourceCode FROM BS_ModelWithResource WHERE ModelLeve = 'company' ");
            try
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 查询工单分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT a.ID,
                               a.ParentNodeID,
                               a.FactoryCode,
                               a.FactoryName,
                               a.Code,
                               a.Name,
                               a.IsEffective,
                               a.UpdateTime,
                               a.UpdateUser
                        FROM dbo.BS_Departments a
                        WHERE a.IsEffective = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                Newtonsoft.Json.Linq.JObject queryParam = queryJson.ToJObject();

                //查询条件 
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND a.FactoryCode = '{queryParam["FactoryCode"]}'");
                }
                //部门名称
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND Name like '%{queryParam["Name"]}%'");
                }
                //编码
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND Code = '{queryParam["Code"]}'");
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
        /// 保存修改数据库
        /// </summary>
        /// <param name="entity"></param>
        public void SaveForm(Sys_DepartmentEntity entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
            {
                entity.ID = Guid.NewGuid().ToString();
                this.BaseRepository().Insert(entity);
            }
            else this.BaseRepository().Update(entity);
        }

        public void DeleteForm(string keyvalue)
        {
            this.BaseRepository().Delete(keyvalue);
        }
        public Sys_DepartmentEntity GetEntity(System.Linq.Expressions.Expression<Func<Sys_DepartmentEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sys_DepartmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 获取系统资源
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCompanyResourcePageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string level = queryParam["Level"] == null ? "" : queryParam["Level"].ToString();
            sql.Append($@"select ResourceCode,ResourceName,ParentResource,Describe,CreateUser,CreateDate from BS_ModelWithResource where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(level))
            {
                sql.Append($" and ModelLeve='{level}' ");//workshop
            }
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }
        #endregion
    }
}
