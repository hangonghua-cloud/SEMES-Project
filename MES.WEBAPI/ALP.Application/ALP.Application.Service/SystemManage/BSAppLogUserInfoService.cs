using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
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
    /// 日 期：2021.3.13 16:19
    /// 描 述：用户信息与登录
    /// </summary>
    public class BSAppLogUserInfoService : RepositoryFactory<BSAppLogUserInfoEntity>, IBSAppLogUserInfoService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BSAppLogUserInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string userId = queryParam["UserId"] == null ? "" : queryParam["UserId"].ToString();
            sql.Append(@"select U.Id,UserId,APPRole,RoleName,U.CreateDate,U.CreateUser,pe.Name as CreateUserName,U.ModifyDate,U.ModifyUser from BS_APPLogUserInfo U
                        Left join [dbo].[BS_APPRole] R on U.APPRole=R.[RoleCode] and R.EnabledMark=1 
			            LEFT JOIN BS_People pe ON pe.Code = U.CreateUser COLLATE Chinese_PRC_CI_AS
                        WHERE U.EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(userId))
            {
                sql.Append(" and CHARINDEX('" + userId + "',UserId)>0 ");
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, string userId)
        {
            int result = 0;
            try
            {
                BSAppLogUserInfoEntity entity = this.BaseRepository().FindEntity(int.Parse(keyValue));
                if (entity != null)
                {
                    entity.EnabledMark = false;
                    entity.ModifyUser = userId;
                    entity.ModifyDate = DateTime.Now;
                    result = this.BaseRepository().Update(entity);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 保存数据(新增/修改)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BSAppLogUserInfoEntity entity)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                if (entity.PWD.Length < 32)
                {
                    entity.PWD = Md5Helper.MD5(entity.PWD, 32);
                }
                entity.EnabledMark = true;
                entity.ModifyDate = DateTime.Now;
                this.BaseRepository().Update(entity);
                result = 2;
            }
            else
            {
                var list = this.BaseRepository().FindList("select Id from BS_APPLogUserInfo where EnabledMark=1 and UserId='" + entity.UserId + "'");
                if (list.Count() == 0)
                {
                    entity.EnabledMark = true;
                    entity.CreateDate = DateTime.Now;
                    entity.PWD = Md5Helper.MD5(entity.PWD, 32);// DESEncrypt.Encrypt(entity.PWD);
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


        public int SaveFunctions(string Role, List<APPRoleToFunctionEntity> SaveRows)
        {
            int result = 0;
            return result;
        }
        #endregion
    }
}
