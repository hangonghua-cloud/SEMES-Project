using ALP.Application.Entity.Material;
using ALP.Application.IService.Material;
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
    public class Base_ProcessAttrItem_Service: RepositoryFactory<Base_ProcessAttrItemEntity>, Base_ProcessAttrItemIService
    {
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            var sql = new StringBuilder();
            sql.Append(@"  SELECT 
	                        PA.ItemCode,PA.FactoryCode,PA.Process,PAI.*
                          FROM  dbo.Base_ProcessAttr PA
                          INNER JOIN  dbo.Base_ProcessAttrItem PAI ON PA.Id=PAI.ProcessAttrId
                          WHERE 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                if (!queryParam["ProcessAttrId"].IsEmpty())
                {
                    sql.Append($" AND ProcessAttrId = N'{queryParam["ProcessAttrId"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
                if (!queryParam["ItemCode"].IsEmpty())
                {
                    sql.Append($" AND ItemCode = N'{queryParam["ItemCode"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND FactoryCode = N'{queryParam["FactoryCode"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
                if (!queryParam["Process"].IsEmpty())
                {
                    sql.Append($" AND Process = N'{queryParam["Process"]}'");
                    //parameter.Add(DbParameters.CreateDbParameter("Id", queryParam["Id"]));
                }
            }
            if (pagination == null)
            {
                return this.BaseRepository().FindTable(sql.ToString());
            }
            else
            {
                return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
            }
        }
        public void Insert(Base_ProcessAttrItemEntity entity)
        {
            this.BaseRepository().Insert(entity);
        }
        public void Update(Base_ProcessAttrItemEntity entity)
        {
            this.BaseRepository().Update(entity);
        }
        public void Remove(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public Base_ProcessAttrItemEntity GetEntity(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<Base_ProcessAttrItemEntity> GetList(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
    }
}
