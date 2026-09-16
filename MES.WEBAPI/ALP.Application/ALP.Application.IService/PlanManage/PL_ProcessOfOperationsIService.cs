using ALP.Application.Entity.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.PlanManage
{
    public interface PL_ProcessOfOperationsIService
    {
        IEnumerable<PL_ProcessOfOperationsEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_ProcessOfOperationsEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ProcessOfOperationsEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsEntity> entity_list, out string msg);
        int RemoveForm(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition);
        int Delete(List<PL_ProcessOfOperationsEntity> lstEntity);
        PL_ProcessOfOperationsEntity GetEntity(string keyValue);
        PL_ProcessOfOperationsEntity Get_ExpressionEntity(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition);
        IEnumerable<PL_ProcessOfOperationsEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition);
        void UpdateHealthTime(string workOrder);
    }
}
