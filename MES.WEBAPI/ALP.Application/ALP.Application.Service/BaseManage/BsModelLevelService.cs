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

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级
    /// </summary>
    public class BsModelLevelService : RepositoryFactory<BsModelLevelEntity>, IBsModelLevelService
    {
        private IAuthorizeService<BsModelLevelEntity> iauthorizeservice = new AuthorizeService<BsModelLevelEntity>();

        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<BsModelLevelEntity>();
            var queryParam = queryJson.ToJObject();
            string levelCode = queryParam["LevelCode"] == null ? "" : queryParam["LevelCode"].ToString();
            string levelName = queryParam["LevelName"] == null ? "" : queryParam["LevelName"].ToString();
            if (!string.IsNullOrWhiteSpace(levelCode))
            {
                expression = expression.And(t => t.LevelCode == levelCode);
            }
            if (!string.IsNullOrWhiteSpace(levelName))
            {
                expression = expression.And(t => t.LevelName.Contains(levelName));
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable(expression).OrderBy(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetParentList(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string levelCode = queryParam["Level"] == null ? "" : queryParam["Level"].ToString();
            /*if (!string.IsNullOrWhiteSpace(level))
            var expression = LinqExtensions.True<BsModelLevelEntity>();
            {
                expression = expression.And(t => t.Level < int.Parse(level));
            }
            expression = expression.And(t => t.EnabledMark == true);*/
            StringBuilder sql = new StringBuilder();
            sql.Append("select Level from BS_ModelLevel where EnabledMark=1 and LevelCode='"+levelCode+"' ");
            DataTable dt = this.BaseRepository().FindTable(sql.ToString());
            string level = dt.Rows[0][0].ToString();
            sql.Clear();
            sql.Append(@"select * from BS_ModelLevel where  EnabledMark=1  ");
            if (!string.IsNullOrWhiteSpace(level) && int.Parse(level) > 0)
            {
                string smallLevel = Convert.ToString(int.Parse(level) - 1);
                sql.Append(" and Level = '" + smallLevel + "' ");
            }
            else
            {
                sql.Append(" and Level = '0' ");
            }
            return this.BaseRepository().FindList(sql.ToString());
        }
        /// <summary>
        /// 获取上级层级
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetParentListByCode(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string level = queryParam["Level"] == null ? "" : queryParam["Level"].ToString();
            /*if (!string.IsNullOrWhiteSpace(level))
            var expression = LinqExtensions.True<BsModelLevelEntity>();
            {
                expression = expression.And(t => t.Level < int.Parse(level));
            }
            expression = expression.And(t => t.EnabledMark == true);*/
            StringBuilder sql = new StringBuilder();
            sql.Append("select Level from BS_ModelLevel where LevelCode = '" + level + "' ");
            DataTable dt = this.BaseRepository().FindTable(sql.ToString());
            string smallLevel = dt.Rows[0][0].ToString();
            sql.Clear();
            sql.Append(@"select * from BS_ModelLevel where  EnabledMark=1  ");
            if (!string.IsNullOrWhiteSpace(smallLevel) && int.Parse(smallLevel) > 0)
            {
                smallLevel = Convert.ToString(int.Parse(smallLevel) - 1);
                sql.Append(" and Level = '" + smallLevel + "' ");
            }
            else
            {
                sql.Append(" and Level = '0' ");
            }
            return this.BaseRepository().FindList(sql.ToString());
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelLevelEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<BsModelLevelEntity>();
            var queryParam = queryJson.ToJObject();
            string levelCode = queryParam["LevelCode"].ToString();
            string levelName = queryParam["LevelName"].ToString();
            if (!string.IsNullOrWhiteSpace(levelCode))
            {
                expression = expression.And(t => t.LevelCode == levelCode);
            }
            if (!string.IsNullOrWhiteSpace(levelName))
            {
                expression = expression.And(t => t.LevelName.Contains(levelName));
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public DataTable GetAllList(string keyValue)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from BS_ModelLevel where EnabledMark=1 ");
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                sql.Append(" and charindex(" + keyValue + ", LevelCode)>0 or charindex(" + keyValue + ", LevelName)>0) ");
            }
            sql.Append(" order by Level asc ");
            return this.BaseRepository().FindTable(sql.ToString());
            /*var expression = LinqExtensions.True<BsModelLevelEntity>();
            expression = expression.And(t => t.EnabledMark == true);*/
           // return this.BaseRepository().IQueryable().OrderByDescending(t=>t.CreateDate).ToList();
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelLevelEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 判断数据表里是否含有层级等于传入参数的记录
        /// </summary>
        /// <param name="levelNum"></param>
        /// <returns></returns>
        public bool HasLevelNum(int levelNum)
        {
            var expression = LinqExtensions.True<BsModelLevelEntity>();
            expression = expression.And(t => t.Level == levelNum);
            return this.BaseRepository().IQueryable(expression).ToList().Count == 0 ? true : false;
        }
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        public bool ExistCode(string code)
        {
            bool existCode = false;
            if (!string.IsNullOrWhiteSpace(code))
            {
                var codeExpression = LinqExtensions.True<BsModelLevelEntity>();
                codeExpression = codeExpression.And(t => t.LevelCode == code && t.EnabledMark == true);
                existCode = this.BaseRepository().IQueryable(codeExpression).Count() == 0 ? true : false;
            }
            return existCode;
        }
        /// <summary>
        /// 层级名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        public bool ExistFullName(string name)
        {
            bool existName = false;
            var codeExpression = LinqExtensions.True<BsModelLevelEntity>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                codeExpression = codeExpression.And(t => t.LevelName == name && t.EnabledMark == true);
                var list = this.BaseRepository().IQueryable(codeExpression).Count();
                existName = list == 0 ? true : false;
            }
            return existName;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            StringBuilder sql = new StringBuilder();
            BsModelLevelEntity entity = this.BaseRepository().FindEntity(keyValue);
            if (entity != null)
            {
                sql.Append("update BS_ModelLevel set EnabledMark='False' where LevelCode='" + keyValue + "'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
            }
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelLevelEntity entity)
        {
            StringBuilder sql = new StringBuilder();
            int result = 0;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify(keyValue);
                sql.Append("update BS_ModelLevel set LevelName='"+entity.LevelName+"',Describe='"+entity.Describe+"',EnabledMark='"+entity.EnabledMark+"',ModifyDate=GETDATE(),ModifyUser='"+entity.ModifyUser+"' where LevelCode='"+entity.LevelCode+"'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
                result = 2;
            }
            else
            {
                /*bool codeExist = ExistCode(entity.LevelCode);
                if (!codeExist)
                    throw new Exception("层级编码已存在");*/
                ///语句有问题
                //BsModelLevelEntity _entity = this.BaseRepository().IQueryable(t => t.LevelCode == entity.LevelCode && t.LevelName == entity.LevelName && t.Level == entity.Level && t.EnabledMark == true).ToList().FirstOrDefault();
                BsModelLevelEntity _entity = new BsModelLevelEntity();
                _entity = this.BaseRepository().IQueryable(t => (t.LevelCode == entity.LevelCode) || (t.LevelName == entity.LevelName && t.EnabledMark == true) || (t.Level == entity.Level && t.EnabledMark == true)).ToList().FirstOrDefault();
                if (_entity == null)
                {
                    entity.Create();
                    entity.EnabledMark = true;
                    this.BaseRepository().Insert(entity);
                    result = 1;
                }
                //else if(_entity.LevelCode == entity.LevelCode)
                //{
                //    result = 3; //主键重复，请重新输入层级编码
                //}
                else if(_entity.LevelName == entity.LevelName)
                {
                    result = 4; //输入的层级名称已经存在
                }
                else if(_entity.Level == entity.Level)
                {
                    result = 5; //输入的层级已经存在
                }
            }
            return result;
        }
        #endregion
    }
}
