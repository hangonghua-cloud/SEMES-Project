using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Service.BaseManage;
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
    public class Sys_DataSegregateGroupService : RepositoryFactory<Sys_DataSegregateGroup>, ISys_DataSegregateGroupService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<Sys_DataSegregateGroup> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string groupCode = queryParam["GroupCode"] == null ? "" : queryParam["GroupCode"].ToString();
            string groupName = queryParam["GroupName"] == null ? "" : queryParam["GroupName"].ToString();
            sql.Append(@"select Id,A.GroupCode,GroupName,GroupLabelValue,Remarks,A.CreateUser,pe1.Name as CreateUserName,CreateDate,A.ModifyUser,pe2.Name as ModifyUserName,ModifyDate 
                        from Sys_DataSegregateGroup  as A
						LEFT JOIN BS_People pe1 ON pe1.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
						LEFT JOIN BS_People pe2 ON pe2.Code = A.ModifyUser COLLATE Chinese_PRC_CI_AS
                        where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                sql.Append(" and GroupCode='" + groupCode + "' ");
            }
            if (!string.IsNullOrWhiteSpace(groupName))
            {
                sql.Append(" and GroupName like '%" + groupName + "%' ");
            }

            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public int RemoveForm(string keyValue)
        {
            int result = 0;
            Sys_DataSegregateGroup entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                entity.EnabledMark = false;
                result = this.BaseRepository().Update(entity);
            }
            return result;
        }
        /// <summary>
        /// 保存数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, Sys_DataSegregateGroup entity)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                entity.EnabledMark = true;
                this.BaseRepository().Update(entity);
                result = 2;
            }
            else
            {
                List<Sys_DataSegregateGroup> groupList = this.BaseRepository().IQueryable(t => t.GroupCode == entity.GroupCode && t.EnabledMark == true).ToList();
                if (groupList.Count == 0)
                {
                    entity.Create();
                    entity.EnabledMark = true;
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
        #endregion
    }
}
