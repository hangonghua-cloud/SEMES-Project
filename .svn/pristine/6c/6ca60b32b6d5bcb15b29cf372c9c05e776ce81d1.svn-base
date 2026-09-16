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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级扩展
    /// </summary>
    public class BsModelLevelExtendFieldsService : RepositoryFactory<BsModelLevelExtendFieldsEntity>, IBsModelLevelExtendFieldsService
    {
        private IAuthorizeService<BsModelLevelExtendFieldsEntity> iauthorizeservice = new AuthorizeService<BsModelLevelExtendFieldsEntity>();

        #region 获取数据
        /// <summary>
        /// 层级扩展字段列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetList(string queryJson)
        {
            //var expression = LinqExtensions.True<BsModelLevelExtendFieldsEntity>();
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string levelCode = queryParam["LevelCode"].ToString();
            string fieldCode = queryParam["FieldCode"].ToString();
            string fieldName = queryParam["FieldName"].ToString();
            /*if (!string.IsNullOrWhiteSpace(levelCode))
            {
                expression = expression.And(t => t.LevelCode == levelCode);
            }
            if (!string.IsNullOrWhiteSpace(fieldCode))
            {
                expression = expression.And(t => t.FieldCode == fieldCode);
            }
            if (!string.IsNullOrWhiteSpace(fieldName))
            {
                expression = expression.And(t => t.FieldName.Contains(fieldName));

            }
            expression = expression.And(t => t.EnabledMark == true);*/
            sql.Append("select * from BS_ModelLevelExtendFields where EnabledMark=1 and LevelCode='" + levelCode + "'");
            return this.BaseRepository().FindList(sql.ToString()).OrderByDescending(t => t.CreateDate).ToList();


        }
        /// <summary>
        /// 层级扩展字段列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string levelCode = queryParam["LevelCode"].ToString();
           
            sql.Append("select * from BS_ModelLevelExtendFields where EnabledMark=1 ");// order by CreateDate
            if (!string.IsNullOrWhiteSpace(levelCode))
            {
                sql.Append(" and LevelCode='" + levelCode + "' ");
            }
            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 层级扩展字段列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelExtendFieldsEntity> GetAllList()
        {
            var expression = LinqExtensions.True<BsModelLevelExtendFieldsEntity>();
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 层级扩展字段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelLevelExtendFieldsEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        #endregion

        #region 验证数据
        /// <summary>
        /// 层级扩展字段编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public bool ExistCode(string code)
        {
            bool existCode = false;
            if (!string.IsNullOrWhiteSpace(code))
            {
                var codeExpression = LinqExtensions.True<BsModelLevelExtendFieldsEntity>();
                codeExpression = codeExpression.And(t => t.FieldCode == code);
                existCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
            }
            return existCode;
        }
        /// <summary>
        /// 层级扩展字段名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string name)
        {
            bool existName = false;
            var codeExpression = LinqExtensions.True<BsModelLevelExtendFieldsEntity>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                codeExpression = codeExpression.And(t => t.FieldName == name && t.EnabledMark == true);
                var list = this.BaseRepository().IQueryable(codeExpression).Count();
                existName = list == 0 ? true : false;
            }
            return existName;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级扩展字段
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            StringBuilder sql = new StringBuilder();
            var expression = LinqExtensions.True<BsModelLevelExtendFieldsEntity>();
            expression = expression.And(t => t.id == keyValue);
            BsModelLevelExtendFieldsEntity entity = this.BaseRepository().IQueryable(expression).FirstOrDefault();
            if (entity != null)
            {
                //entity.EnabledMark = false;
                //this.BaseRepository().Update(entity);
                sql.Append("update BS_ModelLevelExtendFields set EnabledMark='False' where id='" + keyValue + "'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
            }
        }
        /// <summary>
        /// 保存层级扩展字段表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级扩展字段实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelLevelExtendFieldsEntity entity)
        {
            int result = 0;
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify(keyValue);
                sql.Append("update BS_ModelLevelExtendFields set FieldName='"+entity.FieldName+"',FieldType='"+entity.FieldType+"',IsReserve='"+entity.IsReserve+"',EnabledMark='"+entity.EnabledMark+"',ModifyDate=GETDATE(),ModifyUser='"+entity.ModifyUser+"'  where id='"+keyValue+"'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
                result = 1;
            }
            else
            {
                BsModelLevelExtendFieldsEntity _entity = new BsModelLevelExtendFieldsEntity();
                _entity = this.BaseRepository().IQueryable(t => (t.FieldCode == entity.FieldCode) || (t.FieldName == entity.FieldName && t.EnabledMark == true)).ToList().FirstOrDefault();
                if (_entity == null)
                {
                    entity.Create();
                    entity.EnabledMark = true;
                    this.BaseRepository().Insert(entity);
                    result = 1;
                }
                else if (_entity.FieldCode == entity.FieldCode)
                {
                    result = 3; //输入的字段编码已存在
                }
                else if (_entity.FieldName == entity.FieldName)
                {
                    result = 4; //输入的字段名称已经存在
                }
            }
            return result;
        }
        #endregion
    }
}
