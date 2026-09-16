using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using ALP.Util;
using ALP.Util.Extension;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创 建：超级管理员
    /// 日 期：2019-05-10 15:01
    /// 描 述：Main
    /// </summary>
    public class MainService : RepositoryFactory, MainIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<MainEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<MainEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Id":              //主键
                        expression = expression.And(t => t.Id.Contains(keyword));
                        break;
                    case "Code":              //编码
                        expression = expression.And(t => t.Code.Contains(keyword));
                        break;
                    case "Description":              //描述
                        expression = expression.And(t => t.Description.Contains(keyword));
                        break;
                    case "CreateTime":              //创建时间
                        expression = expression.And(t => t.CreateTime.ToString().Contains(keyword));
                        break;
                    default:
                        break;
                }
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public MainEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<MainEntity>(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<SubEntity> GetDetails(string keyValue)
        {
            return this.BaseRepository().FindList<SubEntity>("select * from Sub where ParentCode='" + keyValue + "'");
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                db.Delete<MainEntity>(keyValue);
                db.Delete<SubEntity>(t => t.Id.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
#pragma warning disable CS1573 // 参数“entryList”在“MainService.SaveForm(string, MainEntity, List<SubEntity>)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, MainEntity entity, List<SubEntity> entryList)
#pragma warning restore CS1573 // 参数“entryList”在“MainService.SaveForm(string, MainEntity, List<SubEntity>)”的 XML 注释中没有匹配的 param 标记(但其他参数有)
        {
            IRepository db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<SubEntity>(t => t.ParentCode.Equals(keyValue));
                    foreach (SubEntity item in entryList)
                    {
                        item.Create();
                        item.ParentCode = entity.Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    //明细
                    foreach (SubEntity item in entryList)
                    {
                        item.Create();
                        item.ParentCode = entity.Id;
                        item.CreateTime = DateTime.Now;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        #endregion
    }
}
