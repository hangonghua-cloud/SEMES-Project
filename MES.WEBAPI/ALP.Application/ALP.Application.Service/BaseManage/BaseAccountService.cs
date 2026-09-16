using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.UtilExtend;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
    public class BaseAccountService : RepositoryFactory<MESBaseAccountEntity>, IBaseAccountService
    {
        /// <summary>
        /// 分页获取操作工账号集合函数
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="userEnCode"></param>
        /// <param name="pagination"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IEnumerable<MESBaseAccountEntity> LoadAccountList(string firstName, string lastName, string userEnCode, Pagination pagination, string userCode)
        {
            try
            {
                var expression = LinqExtensions.True<MESBaseAccountEntity>();
                expression = expression.And(d => d.IsDeleted == false);
                if (!string.IsNullOrEmpty(firstName))
                    expression = expression.And(d => d.FirstName.Contains(firstName));
                if (!string.IsNullOrEmpty(lastName))
                    expression = expression.And(d => d.LastName.Contains(lastName));
                if (!string.IsNullOrEmpty(userEnCode))
                    expression = expression.And(d => d.UserEnCode.Contains(userEnCode));
                return new RepositoryFactory().BaseRepository().FindList<MESBaseAccountEntity>(expression, pagination);
            }
            catch (Exception ex)
            {
                LogExtends.WriteLog($"分页获取操作工账号集合函数异常：{ex}");
                return null;
            }
        }

        /// <summary>
        /// 保存账户信息函数
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public (bool result, string msg) SaveEntity(string keyValue, MESBaseAccountEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                Guid key = Guid.Parse(keyValue);
                var exists = new RepositoryFactory().BaseRepository().FindEntity<MESBaseAccountEntity>(d => d.UserEnCode == entity.UserEnCode && d.ID != key);
                if (exists != null)
                    return (false, "操作失败，该工牌码已存在，请勿重复创建。");
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                var exists = new RepositoryFactory().BaseRepository().FindEntity<MESBaseAccountEntity>(d => d.UserEnCode == entity.UserEnCode);
                if (exists != null)
                    return (false, "操作失败，该工牌码已存在，请勿重复创建。");
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
            return (true, "成功");
        }

        /// <summary>
        /// 删除账户信息函数
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="userCode"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public (bool result, string msg) DeleteEntity(string keyValue, string userCode, string userName)
        {
            try
            {
                DateTimeOffset now = DateTimeOffset.Now;
                new RepositoryFactory().BaseRepository().ExecuteBySql($@"UPDATE MES_Base_Account 
SET IsDeleted=1,ModifyDate='{now}',ModifyUserCode='{userCode.FormatSQLQuery()}',ModifyUserName='{userName.FormatSQLQuery()}'
WHERE ID='{keyValue}';");
                return (true, "成功");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
