using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.BaseManage
{
    public class Base_ReportGroupService : RepositoryFactory<Base_ReportGroupEntity>, Base_ReportGroupIService
    {
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT   [Id]
                              ,[GroupName]
                              ,[SortCode]
                              ,[Remark]
                              ,[EnabledMark]
                              ,[Creator]
                              ,[CreateTime]
                              ,[ModifyBy]
                              ,[ModifyTime]
                          FROM [FHMESDB].[dbo].[Base_ReportGroup] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //ID 是否为空进行查询
                if (!queryParam["GroupName"].IsEmpty())
                {
                     sql.Append($" AND GroupName like N'%{queryParam["GroupName"]}%'");
                }
    
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Base_ReportGroupEntity GetEntity(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<Base_ReportGroupEntity> GetList(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        public void SaveForm(string keyValue, Base_ReportGroupEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.Modify(keyValue);
                this.BaseRepository().Update(entity);
            }
        }
        public int RemoveForm(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return this.BaseRepository().Delete(condition);
        }
    }
}
