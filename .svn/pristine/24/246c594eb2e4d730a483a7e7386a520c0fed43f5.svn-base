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
    /// 描 述：打印服务人员绑定
    /// </summary>
    public class BsPeopleByPrintServerService : RepositoryFactory<BsPeopleByPrintServerEntity>
    {
        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public IEnumerable<BsPeopleByPrintServerEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            var queryParam = queryJson.ToJObject();
            string server = queryParam["Server"] == null ? "" : queryParam["Server"].ToString();
            string ip = queryParam["IP"] == null ? "" : queryParam["IP"].ToString();

            /*sql.Append("select Id,PrintServer,IP,Note from BS_PrintServer where 1=1 ");
            if (!string.IsNullOrWhiteSpace(server))
            {
                sql.Append(" and PrintServer like '%" + server + "%' ");
            }
            if (!string.IsNullOrWhiteSpace(ip))
            {
                sql.Append(" and IP like '%" + ip + "%' ");
            }*/

            return this.BaseRepository().FindList(sql.ToString(), pagination);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public DataTable GetList(string code)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"select A.Id,B.PrintServer,B.IP,A.PeopleCode from BS_PeopleByPrintServer as A
                        left join BS_PrintServer as B on A.PrintServerId=B.Id
                        where A.IsEnable=1 and A.PeopleCode='" + code + "' ");
            return this.BaseRepository().FindTable(sql.ToString());
        }

        /// <summary>
        /// 当前打印服务器配置
        /// </summary>
        /// <returns></returns>
        public DataTable GetPrintServerByPersonCode()
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT [PeopleCode],IP  FROM [SIT_UA_MES].[dbo].[BS_PeopleByPrintServer] P
                                                 INNER JOIN  [dbo].[BS_PrintServer]  S ON P.[PrintServerId]=S.Id");
            return this.BaseRepository().FindTable(sql.ToString());
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
                BsPeopleByPrintServerEntity entity = this.BaseRepository().FindEntity(int.Parse(keyValue));
                if (entity != null)
                {
                    entity.IsEnable = false;
                    this.BaseRepository().Update(entity);
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
        /// 更新权限
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public int UpdateIsDeptLimit(string queryJson)
        {
            int n = 0;
            var queryParam = queryJson.ToJObject();
            StringBuilder sql = new StringBuilder();
            sql.Append($@"UPDATE BS_People SET IsDeptLimit = '{queryParam["Islimit"]}' WHERE ID = '{queryParam["ID"]}'");
            n = this.BaseRepository().ExecuteBySql(sql.ToString());
            return n;
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int SaveForm(BsPeopleByPrintServerEntity entity)
        {
            int result = 0;
            try
            {
                BsPeopleByPrintServerEntity _entity = this.BaseRepository().FindEntity(t => t.PeopleCode==entity.PeopleCode && t.IsEnable == true);
                if (_entity!=null)
                {
                    if (_entity.PrintServerId != entity.PrintServerId)
                    {
                        _entity.PrintServerId = entity.PrintServerId;
                        this.BaseRepository().Update(_entity);
                        result = 2;
                    }
                    else
                    {
                        result = 3;
                    }
                }
                else
                {
                    entity.IsEnable = true;
                    entity.CreatedTime = DateTime.Now;
                    this.BaseRepository().Insert(entity);
                    result = 1;

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
