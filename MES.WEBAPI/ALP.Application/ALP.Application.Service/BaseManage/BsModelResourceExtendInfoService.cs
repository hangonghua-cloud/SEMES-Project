using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.IService.AuthorizeManage;
using ALP.Application.Service.AuthorizeManage;
using ALP.Application.UtilExtend;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源扩展
    /// </summary>
    public class BsModelResourceExtendInfoService : RepositoryFactory<BsModelResourceExtendInfoEntity>, IBsModelResourceExtendInfoService
    {
        private IAuthorizeService<BsModelResourceExtendInfoEntity> iauthorizeservice = new AuthorizeService<BsModelResourceExtendInfoEntity>();

        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelResourceExtendInfoEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<BsModelResourceExtendInfoEntity>();
            var queryParam = queryJson.ToJObject();
            string resourceCode = queryParam["ResourceCode"] == null ? "" : queryParam["ResourceCode"].ToString();
            if (!string.IsNullOrWhiteSpace(resourceCode))
            {
                expression = expression.And(t => t.ResourceCode == resourceCode);
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        public IEnumerable<BsModelResourceExtendInfoEntity> GetList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<BsModelResourceExtendInfoEntity>();
            var queryParam = queryJson.ToJObject();
            string resourceCode = queryParam["ResourceCode"] == null ? "" : queryParam["ResourceCode"].ToString();
            string levelCode = queryParam["LevelCode"] == null ? "" : queryParam["LevelCode"].ToString();
            /*if (!string.IsNullOrWhiteSpace(resourceCode))
            {
                expression = expression.And(t => t.ResourceCode == resourceCode);
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().FindList(expression, pagination);*/
            StringBuilder sql = new StringBuilder();
            /*sql.Append(@"select b.FieldType,b.FieldCode,b.FieldName,m.FieldValue from [dbo].[BS_ModelLevelExtendFields] b
                        left join BS_ModelResourceExtendInfo m on b.FieldCode=m.FieldCode and m.ResourceCode='" + resourceCode+"' where b.LevelCode='"+levelCode+"' ");*/
            sql.Append(@"select b.FieldType,b.FieldCode,b.FieldName,m.FieldValue from [dbo].[BS_ModelLevelExtendFields] b
                        left join BS_ModelResourceExtendInfo m on b.FieldCode=m.FieldCode  
                        left join BS_ModelLevel l on b.LevelCode=l.LevelCode
                        where l.Level='"+levelCode+ @"' and (m.ResourceCode='" + resourceCode + @"' or m.ResourceCode is null)");
            return this.BaseRepository().FindTable(sql.ToString(), pagination);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelResourceExtendInfoEntity> GetAllList(string keyValue)
        {
            /*StringBuilder sql = new StringBuilder();
            sql.Append("select * from Bs_ModelLevel where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql.Append(" and charindex(" + keyValue + ", LevelCode)>0 or charindex(" + keyValue + ", LevelName)>0) ");
            }
            sql.Append(" order by CreateDate desc ");
            return this.BaseRepository().FindTable(sql.ToString());*/
            var expression = LinqExtensions.True<BsModelWithResourceEntity>();
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable().OrderByDescending(t=>t.CreateDate).ToList();
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelResourceExtendInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        public BsModelResourceExtendInfoEntity Get_ExpressEntity(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-31 14:48:53
        /// 任务编号: 原材料库存明细表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回MM_RawMaterialStockEntity 列表</returns>
        public IEnumerable<BsModelResourceExtendInfoEntity> Get_ExpressionList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }
        #region 验证数据
        /// <summary>
        /// 层级编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        /*public bool ExistCode(string code)
        {
            bool existCode = false;
            if (!string.IsNullOrWhiteSpace(code))
            {
                var codeExpression = LinqExtensions.True<BsModelWithResourceEntity>();
                codeExpression = codeExpression.And(t => t.ResourceCode == code);
                existCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
            }
            return existCode;
        }*/
        /// <summary>
        /// 层级名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        /*public bool ExistFullName(string name)
        {
            bool existName = false;
            var codeExpression = LinqExtensions.True<BsModelWithResourceEntity>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                codeExpression = codeExpression.And(t => t.ResourceName == name && t.EnabledMark == true);
                var list = this.BaseRepository().IQueryable(codeExpression).Count();
                existName = list == 0 ? true : false;
            }
            return existName;
        }*/
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="resource"></param>
        public void RemoveForm(string code, string resource)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"update BS_ModelResourceExtendInfo set EnabledMark=0 where FieldCode='" + code+@"' and ResourceCode='"+resource+@"'");
            this.BaseRepository().ExecuteBySql(sql.ToString());
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelResourceExtendInfoEntity entity)
        {
            StringBuilder sql = new StringBuilder();
            int result = 0;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify(keyValue);
                sql.Append("update BS_ModelResourceExtendInfo set FieldValue='"+entity.FieldValue+"',ModifyDate=GETDATE(),ModifyUser='"+entity.ModifyUser+"' where Id='"+keyValue+"'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
                result = 1;
            }
            else
            {
                entity.Create();
                entity.EnabledMark = true;
                this.BaseRepository().Insert(entity);
                result = 1;
            }
            return result;
        }
        #endregion
    }
}
