using ALP.Application.Entity.PlanManage;
using ALP.Application.IService.PlanManage;
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

namespace ALP.Application.Service.PlanManage
{
    public class PL_FirstInspectionConfirm_Service : RepositoryFactory<PL_FirstInspectionConfirmEntity>, PL_FirstInspectionConfirmIService
    {
        public void InsertList(List<PL_FirstInspectionConfirmEntity> list)
        {
            this.BaseRepository().Insert(list);
        }
        public void RemoveForm(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }

        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT  PF.[Id]
                          , PF.[ProductOrder]
                          , PF.[WorkOrder]
                          , PF.[ContainerNO]
                          , PF.[MaterialCode]
                          , PF.[Process]
                          , PF.[Creator],PF.FirstStatus,PF.MMXH
                          , PF.[CreateTime],M.ResourceName ProcessName
                      FROM [FHMESDB].[dbo].[PL_FirstInspectionConfirm] PF
                      LEFT JOIN dbo.BS_ModelWithResource M ON M.ResourceCode=PF.Process where 1=1");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                if (!queryParam["Process"].IsEmpty())
                {
                    sql.Append($" AND PF.Process = N'{queryParam["Process"]}'");
                }
                if (!queryParam["FirstStatus"].IsEmpty())
                {
                    sql.Append($" AND PF.FirstStatus = N'{queryParam["FirstStatus"]}'");
                }
                if (!queryParam["ProductOrder"].IsEmpty())
                {
                    sql.Append($" AND PF.ProductOrder Like N'%{queryParam["ProductOrder"]}%'");
                }
                if (!queryParam["ContainerNO"].IsEmpty())
                {
                    sql.Append($" AND PF.ContainerNO = N'{queryParam["ContainerNO"]}'");
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
        public IEnumerable<PL_FirstInspectionConfirmEntity> GetList(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
           return this.BaseRepository().IQueryable(condition);
        }
        public PL_FirstInspectionConfirmEntity GetEntity(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public void SaveEntity(string keyvalue, PL_FirstInspectionConfirmEntity entity)
        {
            if (string.IsNullOrEmpty(keyvalue))
            {
                if (string.IsNullOrEmpty(entity.Id)) entity.Create();
                this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.Id = keyvalue;
                this.BaseRepository().Update(entity);
            }
        }
    }
}
