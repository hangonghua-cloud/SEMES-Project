using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Service.BaseManage;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util;
using ALP.Util.Extension;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.2.22 16:19
    /// 描 述：
    /// </summary>
    public class Sys_DataSegregateGroupToPersonService : RepositoryFactory<Sys_DataSegregateGroupToPerson>, ISys_DataSegregateGroupToPersonService
    {
        #region 获取数据
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPageList(Pagination pagination, string groupCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.Id,A.GroupCode,A.PersonCode,B.Name as PersonName 
                        from Sys_DataSegregateGroupToPerson as A
                        left join BS_People as B on A.PersonCode=B.Code
                        where 1=1 and A.GroupCode='" + groupCode + "' ");
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 获取成员列表
        /// </summary>
        /// <param name="pagination"
        /// <param name="groupCode"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonSelectList(Pagination pagination, string groupCode)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.Id,A.PersonCode,B.Name as PersonName 
                        from Sys_DataSegregateGroupToPerson as A
                        left join BS_People as B on A.PersonCode=B.Code
                        where 1=1 and A.GroupCode='" + groupCode + "'");
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 获取成员列表（未选择）
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroupToPerson> GetPersonAllList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string groupCode = queryParam["GroupCode"] == null ? "" : queryParam["GroupCode"].ToString();
            string deptId = queryParam["DeptId"] == null ? "" : queryParam["DeptId"].ToString();
            string personCode = queryParam["PersonCode"] == null ? "" : queryParam["PersonCode"].ToString();
            sql.Append(@"select Code as PersonCode,Name as PersonName from BS_People where 1=1  ");
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                sql.Append(" and Code not in (select PersonCode from Sys_DataSegregateGroupToPerson where GroupCode='" + groupCode + "') ");
            }
            if (!string.IsNullOrWhiteSpace(deptId))
            {
                sql.Append(" and Department_ID='" + deptId + "' ");
            }
            if (!string.IsNullOrWhiteSpace(personCode))
            {
                sql.Append(" and Code='" + personCode + "' ");
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="keyValues">主键</param>
        public int RemoveForm(string keyValues)
        {
            int result = 0;
            if (!string.IsNullOrWhiteSpace(keyValues))
            {
                string[] keyValueArrr = keyValues.Split(',');
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                for(int iCount = 0; iCount < keyValueArrr.Length; iCount++)
                {
                    Sys_DataSegregateGroupToPerson entity = this.BaseRepository().FindEntity(keyValueArrr[iCount]);
                    if (entity != null)
                    {
                        result = db.Delete(entity);
                    }
                }
                db.Commit();
            }
            return result;
        }
        /// <summary>
        /// 批量保存数据
        /// </summary>
        /// <param name="groupCode"></param>
        /// <param name="personCodes"></param>
        /// <returns></returns>
        public int SaveForm(string groupCode, string personCodes)
        {
            RepositoryFactory<BS_PeopleEntity> peopleService = new RepositoryFactory<BS_PeopleEntity>();

            int result = 0;
            if (string.IsNullOrWhiteSpace(groupCode))
            {
                result = 2;
            }
            else if (string.IsNullOrWhiteSpace(personCodes))
            {
                result = 3;
            }
            else
            {
                string[] personCodeArr = personCodes.Split(',');
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                for(int iCount = 0; iCount < personCodeArr.Length; iCount++)
                {
                    Sys_DataSegregateGroupToPerson entity = new Sys_DataSegregateGroupToPerson();
                    entity.Id = Guid.NewGuid().ToString();
                    entity.GroupCode = groupCode;
                    entity.PersonCode = personCodeArr[iCount].ToString();
                    db.Insert(entity);

                    BS_PeopleEntity peopleEntity = peopleService.BaseRepository().IQueryable(t => t.Code == entity.PersonCode).ToList().FirstOrDefault();
                   // peopleEntity.GroupCode = groupCode;
                    db.Update(peopleEntity);
                }
                db.Commit();
                result = 1;
            }
            return result;
        }
        #endregion
    }
}
