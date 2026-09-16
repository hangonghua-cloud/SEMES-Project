using ALP.Application.Entity.Material;
using ALP.Application.IService.Material;
using ALP.Data;
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

namespace ALP.Application.Service.Material
{

    public class Base_ProcessAttr_Service : RepositoryFactory<Base_ProcessAttrEntity>, Base_ProcessAttrIService
    {
        public DataTable GetListWithPage(Pagination pagination,string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@" SELECT 
	                        Id,ItemCode,ItemName,FactoryCode,FactoryName,Process,ProcessName,IsEnabled,Creator,
	                        CONVERT(VARCHAR(50),CreateTime,120) CreateTime,ModifyBy,CONVERT(VARCHAR(5),ModifyTime,120) ModifyTime
                         FROM dbo.Base_ProcessAttr WHERE 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["ItemCode"].IsEmpty())
                {
                    sql.Append($" AND ItemCode = N'{queryParam["ItemCode"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
                if (!queryParam["ItemName"].IsEmpty())
                {
                    sql.Append($" AND ItemName Like N'%{queryParam["ItemName"]}%'");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                }
                if (!queryParam["Process"].IsEmpty())
                {
                    sql.Append($" AND Process = N'{queryParam["Process"]}'");
                }
            }
            if(pagination == null)
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            else
            {
                return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            }
        }
        public void Insert(Base_ProcessAttrEntity entity)
        {
            this.BaseRepository().Insert(entity);
        }
        public void Update(Base_ProcessAttrEntity entity)
        {
            this.BaseRepository().Update(entity);
        }
        public void Remove(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public Base_ProcessAttrEntity GetEntity(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<Base_ProcessAttrEntity> GetList(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
    }
}
