using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Service.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util;
using ALP.Util.Extension;

namespace ALP.Application.Service.SystemManage
{
    public class BSAppRoleService : RepositoryFactory<APPRoleEntity>, IBSAppRoleService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<APPRoleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string roleCode = queryParam["Code"] == null ? "" : queryParam["Code"].ToString();
            string roleName = queryParam["Name"] == null ? "" : queryParam["Name"].ToString();
            sql.Append(@"select Id,RoleCode,RoleName,A.CreateUser,pe.Name as CreateUserName,CreateDate,EnabledMark 
                        from BS_APPRole as A
						LEFT JOIN BS_People pe ON pe.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
                        where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(roleCode))
            {
                sql.Append(" and RoleCode like '%" + roleCode + "%' ");
            }
            if (!string.IsNullOrWhiteSpace(roleName))
            {
                sql.Append(" and RoleName like '%" + roleName + "%' ");
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public DataTable GetAuthorList(string roleCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT R.ModuleName,R.FunctionCode,R.FunctionName, Case WHEN isnull(RT.Id,'')=''THEN 'NotSelcted'  ELSE 'SELECTED'  END as ItemSelected
                        FROM  [dbo].[BS_APPRoleFunction] R 
                        LEFT JOIN [dbo].[BS_APPRoleToFunction] RT ON R.[FunctionCode]=RT.[FunctionCode] AND RT.[EnabledMark]=1  and RT.RoleCode='{roleCode}'
                        WHERE R.[EnabledMark]=1
                        ORDER By R.ModuleName,R.FunctionName ");
            return this.BaseRepository().FindTable(sql.ToString());
        }
        /// <summary>
        /// 获取权限
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<APPRoleFunctionEntity> GetRoleFunctionPageList(Pagination pagination,string queryJson)
        {
            RepositoryFactory<APPRoleFunctionEntity> functionService = new RepositoryFactory<APPRoleFunctionEntity>();
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string code = queryParam["Code"] == null ? "" : queryParam["Code"].ToString();
            string name = queryParam["Name"] == null ? "" : queryParam["Name"].ToString();
            sql.Append(@"select Id,ModuleCode,ModuleName,FunctionCode,FunctionName,FunctionColor,FunctionIco,Type,case when Type='1' then '页面' else '按钮' end as TypeName,Sort,EnabledMark,A.CreateUser,pe.Name as CreateUserName,CreateDate 
                        from BS_APPRoleFunction as A
						LEFT JOIN BS_People pe ON pe.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
                        where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(code))
            {
                sql.Append(" and FunctionCode like '%" + code + "%' ");
            }
            if (!string.IsNullOrWhiteSpace(name))
            {
                sql.Append(" and FunctionName like '%" + name + "%' ");
            }
            return functionService.BaseRepository().FindList(sql.ToString(), pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, string userId)
        {
            int result = 0;
            try
            {
                APPRoleEntity entity = this.BaseRepository().FindEntity(int.Parse(keyValue));
                if (entity != null)
                {
                    entity.EnabledMark = false;
                    entity.ModifyUser = userId;
                    entity.ModifyDate = DateTime.Now;
                    result = this.BaseRepository().Update(entity);
                }
                else
                {
                    result = 2;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveRoleFunctionForm(string keyValue, string userId)
        {
            RepositoryFactory<APPRoleFunctionEntity> functionService = new RepositoryFactory<APPRoleFunctionEntity>();
            int result = 0;
            try
            {
                APPRoleFunctionEntity entity = functionService.BaseRepository().FindEntity(int.Parse(keyValue));
                if (entity != null)
                {
                    entity.EnabledMark = false;
                    entity.ModifyUser = userId;
                    entity.ModifyDate = DateTime.Now;
                    result = functionService.BaseRepository().Update(entity);
                }
                else
                {
                    result = 2;
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, APPRoleEntity entity)
        {
            int result = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    entity.ModifyDate = DateTime.Now;
                    //entity.EnabledMark = true;
                    this.BaseRepository().Update(entity);
                    result = 2;
                }
                else
                {
                    APPRoleEntity _entity = this.BaseRepository().FindEntity(t => t.RoleCode == entity.RoleCode && t.EnabledMark == true);
                    if (_entity == null)
                    {
                        entity.CreateDate = DateTime.Now;
                        //entity.EnabledMark = true;
                        this.BaseRepository().Insert(entity);
                        result = 1;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存数据(新建/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveRoleFunctionForm(string keyValue, APPRoleFunctionEntity entity)
        {
            RepositoryFactory<APPRoleFunctionEntity> functionService = new RepositoryFactory<APPRoleFunctionEntity>();
            int result = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    entity.ModifyDate = DateTime.Now;
                    //entity.EnabledMark = true;
                    functionService.BaseRepository().Update(entity);
                    result = 2;
                }
                else
                {
                    APPRoleFunctionEntity _entity = functionService.BaseRepository().FindEntity(t => t.FunctionCode == entity.FunctionCode && t.EnabledMark == true);
                    if (_entity == null)
                    {
                        entity.Id = Guid.NewGuid().ToString();
                        entity.CreateDate = DateTime.Now;
                        //entity.EnabledMark = true;
                        functionService.BaseRepository().Insert(entity);
                        result = 1;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存实体
        /// </summary>
        /// <param name="roleCode"></param>
        /// <param name="userId"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int SaveFunctions(string roleCode, string userId,List<APPRoleToFunctionEntity> list)
        {
            int result = 0;
            RepositoryFactory<APPRoleToFunctionEntity> functionService = new RepositoryFactory<APPRoleToFunctionEntity>();
            StringBuilder sql = new StringBuilder();
            sql.Append("DELETE FROM [dbo].[BS_APPRoleToFunction] WHERE [RoleCode]='" + roleCode + "' ");
            this.BaseRepository().ExecuteBySql(sql.ToString());
            foreach(APPRoleToFunctionEntity entity in list)
            {
                entity.RoleCode = roleCode;
                //entity.FunctionCode = entity.FunctionCode;
                entity.EnabledMark = true;
                entity.CreateUser = userId;
                entity.CreateDate = DateTime.Now;
            }
            functionService.BaseRepository().Insert(list);
            result = 1;
            return result;
        }
        #endregion
    }
}
