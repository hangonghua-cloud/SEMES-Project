using ALP.Application.Entity.AppManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using ALP.Data;
using System.Data.Common;
//using ALP.Application.IService.AppManage;
using ALP.Application.Code;

namespace ALP.Application.Service.AppManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-05-24 16:22
    /// 描 述：线体
    /// </summary>
    public class AppVersionService : RepositoryFactory<AppVersionEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<AppVersionEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<AppVersionEntity>();

            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    switch (condition)
                    {
                        default:
                            break;
                    }
                }
                else if (!queryParam["condition"].IsEmpty() && (!queryParam["begin"].IsEmpty() || !queryParam["end"].IsEmpty()))
                {
                    string condition = queryParam["condition"].ToString();
                    string begin = queryParam["begin"].ToString();
                    string end = queryParam["end"].ToString();
                    switch (condition)
                    {
                        default:
                            break;
                    }
                }
                else
                {
                    expression = GetQueryLinqExtensionsByJsonStr(queryParam);
                }
            }

            return this.BaseRepository().FindList(expression, pagination);
        }

        /// <summary>
        /// 根据传入的Json查询条件 动态解析为Expression
        /// 
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<AppVersionEntity, bool>> GetQueryLinqExtensionsByJsonStr(JObject JsonStr)
        {
            var expression = LinqExtensions.True<AppVersionEntity>();
            if (JsonStr.Count == 0) return expression;
            var groupOP = JsonStr.First.Last.ToString();
            var isAnd = (groupOP == "AND" ? true : false);
            var rules = JsonStr.Last.Last;
            var sql = BaseService.BuildCommonSql(isAnd, rules);
            expression = BaseService.BuildCommonExpression<AppVersionEntity>(isAnd, rules);
            return expression;

        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public AppVersionEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>返回列表</returns>
        public List<AppVersionEntity> GetList()
        {
            return this.BaseRepository().IQueryable().ToList();
        }
        #endregion

        #region 验证数据

        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, AppVersionEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }

        /// <summary>
        /// 版本回滚功能
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public bool ModifyEntity(AppVersionEntity entity,string keyValue)
        {
            IDatabase db = DbFactory.Base().BeginTrans();
            try
            {
                string sql = $"update App_AppVersion set EnableFlag=1 where Id='{keyValue}'";
                db.ExecuteBySql(sql);
                versionFailure(db, entity.Id, entity.AppType);
                db.Commit();
                return true;
            }
            catch (System.Exception)
            {
                db.Rollback();
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 版本新增功能
        /// </summary>
        /// <param name="entity">版本管理记录</param>
        /// <returns></returns>
        public bool InsertApp(AppVersionEntity entity)
        {
            IDatabase db = DbFactory.Base().BeginTrans();
            try
            {
                entity.Create();
                db.Insert(entity);
                versionFailure(db, entity.Id, entity.AppType);
                db.Commit();
                return true;
            }
            catch (System.Exception)
            {
                db.Rollback();
                return false;
            }
            finally
            {
                db.Close();
            }
        }

        /// <summary>
        /// 版本失效
        /// </summary>
        public void versionFailure(IDatabase db, string Id, int? AppType)
        {
            var sql = $"update App_AppVersion set EnableFlag=0 where Id!='{Id}' and appType='{AppType}'";
            db.ExecuteBySql(sql);
        }
        #endregion
    }
}
