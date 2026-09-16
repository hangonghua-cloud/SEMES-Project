using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util.Extension;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ALP.Application.IService.AuthorizeManage;
using ALP.Application.Service.AuthorizeManage;
using ALP.Util.Attributes;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.02 14:27
    /// 描 述：部门管理
    /// </summary>
    public class DepartmentService : RepositoryFactory<DepartmentEntity>, IDepartmentService
    {
        private IAuthorizeService<DepartmentEntity> iauthorizeservice = new AuthorizeService<DepartmentEntity>();
        #region 获取数据
        /// <summary>
        /// 部门列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DepartmentEntity> GetList()
        {
            string name = EntityAttribute.GetEntityTable<DepartmentEntity>();
            string key = EntityAttribute.GetEntityKey<DepartmentEntity>();
            return this.BaseRepository().IQueryable(t=>t.EnabledMark==1).ToList();
        }
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public DepartmentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 部门编号不能重复
        /// </summary>
        /// <param name="enCode">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string enCode, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.EnCode == enCode);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        /// <summary>
        /// 部门名称不能重复
        /// </summary>
        /// <param name="fullName">名称</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string fullName, string keyValue)
        {
            var expression = LinqExtensions.True<DepartmentEntity>();
            expression = expression.And(t => t.FullName == fullName);
            if (!string.IsNullOrEmpty(keyValue))
            {
                expression = expression.And(t => t.DepartmentId != keyValue);
            }
            return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            int count = this.BaseRepository().IQueryable(t => t.ParentId == keyValue).Count();
            if (count > 0)
                throw new Exception("当前所选数据有子节点数据！");
            this.BaseRepository().ExecuteBySql($"UPDATE Base_Department SET EnabledMark=0,DeleteMark=1 WHERE DepartmentId='{keyValue}'");
        }
        /// <summary>
        /// 保存部门表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="departmentEntity">机构实体</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, DepartmentEntity departmentEntity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                departmentEntity.Modify(keyValue);
                this.BaseRepository().Update(departmentEntity);
            }
            else
            {
                //20190918因微应用共同调用因此改在此插入操作人、姓名
                if (string.IsNullOrEmpty(departmentEntity.CreateUserId))
                departmentEntity.CreateUserId = Code.OperatorProvider.Provider.Current().UserId;
                if (string.IsNullOrEmpty(departmentEntity.CreateUserName))
                    departmentEntity.CreateUserName = Code.OperatorProvider.Provider.Current().UserName;
                departmentEntity.Create();
                departmentEntity.EnabledMark = 1;
                departmentEntity.DeleteMark = 0;
                this.BaseRepository().Insert(departmentEntity);
            }
        }
        #endregion
    }
}
