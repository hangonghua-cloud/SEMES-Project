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
    /// 描 述：工厂模型资源
    /// </summary>
    public class BsModelWithResourceService : RepositoryFactory<BsModelWithResourceEntity>, IBsModelWithResourceService
    {
        private IAuthorizeService<BsModelWithResourceEntity> iauthorizeservice = new AuthorizeService<BsModelWithResourceEntity>();

        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<BsModelWithResourceEntity>();
            var queryParam = queryJson.ToJObject();
            string resourceCode = queryParam["ResourceCode"] == null ? "" : queryParam["ResourceCode"].ToString();
            string resourceName = queryParam["ResourceName"] == null ? "" : queryParam["ResourceName"].ToString();
            string modelLevel = queryParam["ModelLevel"] == null ? "" : queryParam["ModelLevel"].ToString();
            string parentCode = queryParam["ParentResource"] == null ? "" : queryParam["ParentResource"].ToString();
            if (!string.IsNullOrWhiteSpace(resourceCode))
            {
                expression = expression.And(t => t.ResourceCode == resourceCode);
            }
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                expression = expression.And(t => t.ResourceName.Contains(resourceName));
            }
            if (!string.IsNullOrWhiteSpace(modelLevel))
            {
                expression = expression.And(t => t.ModelLeve == modelLevel);
            }
            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                expression = expression.And(t => t.ParentResource == parentCode);
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();
        }
        public IEnumerable<BsModelWithResourceEntity> GetList(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
        /// <summary>
        /// 根据层级编号获取低级资源
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetListByLevel(string queryJson)
        {
            var expression = LinqExtensions.True<BsModelWithResourceEntity>();
            var queryParam = queryJson.ToJObject();
            string level = queryParam["Level"] == null ? "" : queryParam["Level"].ToString();
            /*if (!string.IsNullOrWhiteSpace(level))
            {
                string smallLevel = (int.Parse(level) - 1).ToString();
                expression = expression.And(t => t.ModelLeve == smallLevel);
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().IQueryable(expression).OrderByDescending(t => t.CreateDate).ToList();*/
            StringBuilder sql = new StringBuilder();
            sql.Append("select Level from BS_ModelLevel where LevelCode = '" + level + "' ");
            DataTable dt = this.BaseRepository().FindTable(sql.ToString());
            string smallLevel = dt.Rows[0][0].ToString();
            sql.Clear();
            //   sql.Append(@"select ResourceCode,ResourceName,ModelLeve,ParentResource,Describe,pe1.Name as CreateUser,CreateDate,pe1.Name as ModifyUser,ModifyDate 
            //               from BS_ModelWithResource as A
            //LEFT JOIN BS_People pe1 ON pe1.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
            //LEFT JOIN BS_People pe2 ON pe2.Code = A.ModifyUser COLLATE Chinese_PRC_CI_AS
            //               where ModelLeve in (select LevelCode from BS_ModelLevel where Level = '" + smallLevel+"' and EnabledMark = 1) and EnabledMark = 1 ");

            sql.Append(@"select ResourceCode,ResourceName,ModelLeve,ParentResource,Describe --,pe1.Name as CreateUser,CreateDate,pe1.Name as ModifyUser,ModifyDate 
                           from BS_ModelWithResource as A
           -- LEFT JOIN BS_People pe1 ON pe1.Code = A.CreateUser COLLATE Chinese_PRC_CI_AS
           -- LEFT JOIN BS_People pe2 ON pe2.Code = A.ModifyUser COLLATE Chinese_PRC_CI_AS
                           where ModelLeve in (select LevelCode from BS_ModelLevel where Level = '" + smallLevel + "' and EnabledMark = 1) and EnabledMark = 1 ");

            /*if (!string.IsNullOrWhiteSpace(smallLevel) && int.Parse(smallLevel) > 1)
            {
                smallLevel = Convert.ToString(int.Parse(smallLevel));
                sql.Append(" and ModelLeve <= '"  + smallLevel + "'");
            }
            else
            {
                sql.Append(" and ModelLeve = '0' ");
            }*/
            return this.BaseRepository().FindList(sql.ToString());
        }


        /// <summary>
        /// 根据层级编号获取所属某相应级资源
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetResourceByLevelJson(string queryJson)
        {
            var queryParam = queryJson.ToJObject();
            string resourceCode = queryParam["ResourceCode"] == null ? "" : queryParam["ResourceCode"].ToString();
            string getLevelStr = queryParam["GetLevelStr"] == null ? "" : queryParam["GetLevelStr"].ToString();

            StringBuilder sql = new StringBuilder();
            sql.Append(@"WITH TEST_CTE 
                            AS
                            (
                            SELECT ResourceCode, ResourceName, ModelLeve FROM [dbo].[BS_ModelWithResource] WHERE ResourceCode = '{0}' AND EnabledMark = 1
                            UNION ALL
                            SELECT R.ResourceCode, R.ResourceName, R.ModelLeve FROM[dbo].[BS_ModelWithResource] R INNER JOIN  TEST_CTE T ON R.ParentResource = T.ResourceCode
                            )
                            SELECT * FROM TEST_CTE WHERE ModelLeve in ({1})");
            var sqlstr = string.Format(sql.ToString(), resourceCode, getLevelStr);
            return  this.BaseRepository().FindList(sqlstr);
        }
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<BsModelWithResourceEntity>();
            var queryParam = queryJson.ToJObject();
            //string resourceCode = queryParam["ResourceCode"] == null ? "" : queryParam["ResourceCode"].ToString();
            //string resourceName = queryParam["ResourceName"] == null ? "" : queryParam["ResourceName"].ToString();
            string level = queryParam["Level"] == null ? "" : queryParam["Level"].ToString();
            /*if (!string.IsNullOrWhiteSpace(resourceCode))
            {
                expression = expression.And(t => t.ResourceCode == resourceCode);
            }
            if (!string.IsNullOrWhiteSpace(resourceName))
            {
                expression = expression.And(t => t.ResourceName.Contains(resourceName));
            }*/
            /*if (!string.IsNullOrWhiteSpace(level))
            {
                expression = expression.And(t => t.ModelLeve == level);
            }
            expression = expression.And(t => t.EnabledMark == true);
            return this.BaseRepository().FindList(expression, pagination);*/
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.* from BS_ModelWithResource as A
                        inner join BS_ModelLevel as B on A.ModelLeve = B.LevelCode
                        where B.LevelCode = '" + level + "' and A.EnabledMark=1");
            if (!queryParam["ResourceCode"].IsEmpty())
            {
                sql.Append($" AND A.ResourceCode LIKE N'%{queryParam["ResourceCode"]}%'");
            }

            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BsModelWithResourceEntity> GetAllList(string keyValue)
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
            return this.BaseRepository().IQueryable().OrderByDescending(t => t.CreateDate).ToList();
        }
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BsModelWithResourceEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        public BsModelWithResourceEntity GetEntity(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
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
                var codeExpression = LinqExtensions.True<BsModelWithResourceEntity>();
                codeExpression = codeExpression.And(t => t.ResourceCode == code);
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
            var codeExpression = LinqExtensions.True<BsModelWithResourceEntity>();
            if (!string.IsNullOrWhiteSpace(name))
            {
                codeExpression = codeExpression.And(t => t.ResourceName == name && t.EnabledMark == true);
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
            BsModelWithResourceEntity entity = this.BaseRepository().FindEntity(keyValue);
            /*if (entity != null)
            {
                sql.Append("update BS_ModelWithResource set EnabledMark='False' where ResourceCode='" + keyValue + "'");
                this.BaseRepository().ExecuteBySql(sql.ToString());
            }*/
            this.BaseRepository().Delete(entity);
        }
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsModelWithResourceEntity entity)
        {
            StringBuilder sql = new StringBuilder();
            int result = 0;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Modify(keyValue);
                /*sql.Append("update BS_ModelWithResource set ResourceName='"+entity.ResourceName+"',ModelLeve='"+entity.ModelLeve+"',ParentResource='"+entity.ParentResource+"',Describe='"+entity.Describe+"',EnabledMark='"+entity.EnabledMark+"',ModifyDate=GETDATE(),ModifyUser='"+entity.ModifyUser+"' where ResourceCode='"+entity.ResourceCode+"'");
                this.BaseRepository().ExecuteBySql(sql.ToString());*/
                entity.EnabledMark = true;
                this.BaseRepository().Update(entity);
                result = 2;
            }
            else
            {
                BsModelWithResourceEntity _entity = this.BaseRepository().IQueryable(t => t.ResourceCode == entity.ResourceCode || t.ResourceName == entity.ResourceName).FirstOrDefault();
                if (_entity != null)
                {
                    if (_entity.ResourceCode == entity.ResourceCode)
                    {
                        return 3;
                    }
                    /*if (_entity.ResourceName == entity.ResourceName)
                    {
                        return 4;
                    }*/
                }

                entity.Create();
                entity.EnabledMark = true;
                this.BaseRepository().Insert(entity);
                result = 1;
            }
            return result;
        }
        #endregion

        #region 根据工序找工厂
        /// <summary>
        /// 根据工序找工厂
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public BsModelWithResourceEntity GetFactoryByProcess(string processCode)
        {
            string sql = $@"SELECT *
                            FROM dbo.BS_ModelWithResource
                            WHERE ResourceCode =
                            (
                                SELECT TOP 1
                                       ParentResource
                                FROM dbo.BS_ModelWithResource
                                WHERE ResourceCode =
                                (
                                    SELECT TOP 1
                                           ParentResource
                                    FROM dbo.BS_ModelWithResource
                                    WHERE ModelLeve = 'Process'
                                          AND ResourceCode = '{processCode}'
                                )
                            )";
            return this.BaseRepository().FindList(sql).FirstOrDefault();
        }
        #endregion
    }
}
