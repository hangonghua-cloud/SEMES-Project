using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Application.UtilExtend;

namespace ALP.Application.Service.BaseManage
{
    /// <summary>
    /// 创 建：gzq
    /// 日 期：2020-05-14 13:53
    /// 描 述：班次
    /// </summary>
    public class BaseShiftService : RepositoryFactory<BaseShiftEntity>, IBaseShiftService
    {
        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BaseShiftEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder("select * from [Mes_Base_Shift] where 1=1 ");
            if (!string.IsNullOrEmpty(queryJson))
            {
                var queryParam = queryJson.ToJObject();
                //查询条件
                if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
                {
                    string condition = queryParam["condition"].ToString();
                    string keyword = queryParam["keyword"].ToString();
                    if (keyword != "undefined")
                    {
                        sql.Append($"AND {condition} LIKE '%{keyword}%' ");
                    }
                }
                else if (!queryParam["filters"].IsEmpty())
                {
                    sql.Append(" and  ");
                    var Advancesql = BaseService.AutoGroupAdvanaceQuerySql(queryParam);
                    sql.Append(Advancesql);
                }

                else
                {
                    //if (!queryParam["Code"].IsEmpty())
                    //{
                    //    string EnCode = queryParam["Code"].ToString();
                    //    sql.Append($"AND Code LIKE '%{EnCode}%' ");
                    //}
                    //if (!queryParam["Name"].IsEmpty())
                    //{
                    //    string FullName = queryParam["Name"].ToString();
                    //    sql.Append($"AND Name LIKE '%{FullName}%' ");
                    //}
                    //string begin = queryParam["StartTime"].ToString();
                    //string end = queryParam["EndTime"].ToString();
                    //if (!string.IsNullOrEmpty(begin))
                    //    sql.Append($"AND DATEDIFF(dd,CONVERT(varchar(100),'{begin}', 23), createDate)>=0 ");
                    //if (!string.IsNullOrEmpty(end))
                    //    sql.Append($"AND DATEDIFF(dd, createDate,CONVERT(varchar(100),'{end}', 23))>=0 ");
                }
            }

            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<BaseShiftEntity> GetPageList2(Pagination pagination, string queryJson)
        {
            return this.BaseRepository().FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<BaseShiftEntity> GetList(string queryJson)
        {
            return this.BaseRepository().IQueryable().ToList();
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public BaseShiftEntity GetEntity(Guid keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 分页获取班次集合函数
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="workShopCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public IEnumerable<BaseShiftEntity> GetShiftList(Pagination pagination, string workShopCode, string userCode)
        {
            try
            {
                var expression = LinqExtensions.True<BaseShiftEntity>();
                if (!string.IsNullOrEmpty(workShopCode))
                    expression = expression.And(d => d.WorkShopCode == workShopCode);
                return new RepositoryFactory().BaseRepository().FindList<BaseShiftEntity>(expression, pagination);
            }
            catch (Exception ex)
            {
                LogExtends.WriteLog($"班次管理模块 - 分页获取班次集合函数异常：{ex}");
                return null;
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public (bool result, string msg) RemoveForm(string keyValue)
        {
            try
            {
                //【TODO：后续根据业务需求添加删除逻辑】
                this.BaseRepository().Delete(keyValue);
                return (true, "成功");
            }
            catch (Exception ex)
            {
                LogExtends.WriteLog($"删除班次操作异常：{ex}");
                return (false, "操作失败，服务器异常");
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, BaseShiftEntity entity)
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
