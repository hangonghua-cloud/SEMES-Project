using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;

using ALP.Util;

using ALP.Util.Extension;

 using Newtonsoft.Json.Linq;

 using System.Linq.Expressions;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2019-07-18 16:58
    /// 描 述：文件上传管理
    /// </summary>
    public class UploadManageService : RepositoryFactory<UploadManageEntity>, UploadManageIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<UploadManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<UploadManageEntity>();
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
                    case "FId":              //FunctionType
                        expression = expression.And(t => t.FId.ToString().Contains(keyword));
                        break;
                    case "FileName":              //FileName
                        expression = expression.And(t => t.FileName.ToString().Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
          else  if (!queryParam["condition"].IsEmpty() && (!queryParam["begin"].IsEmpty() || !queryParam["end"].IsEmpty()))
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
           if (expression == null) 
            { 
              expression = LinqExtensions.True<UploadManageEntity>(); 
            } 
           }
        return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<UploadManageEntity> GetList(string queryJson)
        {
            var expression = LinqExtensions.True<UploadManageEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "FId":              //FunctionType
                        expression = expression.And(t => t.FId.ToString().Contains(keyword));
                        break;
                    case "FileName":              //FileName
                        expression = expression.And(t => t.FileName.ToString().Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            if (!queryParam["condition"].IsEmpty() && (!queryParam["begin"].IsEmpty() || !queryParam["end"].IsEmpty()))
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
            return this.BaseRepository().IQueryable(expression).ToList();
        }
        /// <summary>
        /// 根据传入的Json查询条件 动态解析为
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<UploadManageEntity, bool>> GetQueryLinqExtensionsByJsonStr(JObject JsonStr) 
        {
            var expression = LinqExtensions.True<UploadManageEntity>();
            if (JsonStr.Count == 0) return expression;
            var groupOP = JsonStr.First.Last.ToString();
            var isAnd = (groupOP == "AND" ? true : false);
            var rules = JsonStr.Last.Last;
             var sql = BaseService.BuildCommonSql(isAnd, rules);
            expression = BaseService.BuildCommonExpression<UploadManageEntity>(isAnd, rules);
            return expression;
              }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public UploadManageEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
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
        public void SaveForm(string keyValue, UploadManageEntity entity)
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
        #endregion
    }
}
