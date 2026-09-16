using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using System.Text;

using Newtonsoft.Json.Linq;
using ALP.Application.Service.SystemManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util;
using ALP.Util.Extension;

namespace ALP.Application.Service.SystemManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：丁零
    /// 日 期：2021.4.9 16:19
    /// 描 述：打印服务
    /// </summary>
    public class BsPrintServerService : RepositoryFactory<BsPrintServerEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsPrintServerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string server = queryParam["Server"] == null ? "" : queryParam["Server"].ToString();
            string ip = queryParam["IP"] == null ? "" : queryParam["IP"].ToString();

            sql.Append("select Id,PrintServer,IP,Note from BS_PrintServer where 1=1 ");
            if (!string.IsNullOrWhiteSpace(server))
            {
                sql.Append(" and PrintServer like '%" + server + "%' ");
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                sql.Append(" and IP like '%" + ip + "%' ");
            }

            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsPrintServerEntity> GetList(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string server = queryParam["Server"] == null ? "" : queryParam["Server"].ToString();
            string ip = queryParam["IP"] == null ? "" : queryParam["IP"].ToString();

            sql.Append("select Id,PrintServer,IP,PrintServer+'('+IP+')' as Server,Note from BS_PrintServer where 1=1 ");
            if (!string.IsNullOrWhiteSpace(server))
            {
                sql.Append(" and PrintServer like '%" + server + "%' ");
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                sql.Append(" and IP like '%" + ip + "%' ");
            }

            return this.BaseRepository().FindList(sql.ToString());
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public int RemoveForm(string keyValue)
        {
            int result = 0;
            try
            {
                BsPrintServerEntity entity = this.BaseRepository().FindEntity(int.Parse(keyValue));
                if (entity != null)
                {
                    this.BaseRepository().Delete(entity);
                    result = 1;
                }
                else
                {
                    result = 2;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(string keyValue, BsPrintServerEntity entity)
        {
            int result = 0;
            try
            {
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    this.BaseRepository().Update(entity);
                    result = 2;
                }
                else
                {
                    BsPrintServerEntity _entity = this.BaseRepository().FindEntity(t => t.IP == entity.IP);
                    if (_entity == null)
                    {
                        this.BaseRepository().Insert(entity);
                        result = 1;
                    }
                    else
                    {
                        result = 3;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        #endregion
    }
}
