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
    public interface PL_ProcessOfOperationsAttrIService
    {
        IEnumerable<PL_ProcessOfOperationsAttrEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_ProcessOfOperationsAttrEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ProcessOfOperationsAttrEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsAttrEntity> entity_list, out string msg);
        int RemoveForm(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition);
        int Delete(List<PL_ProcessOfOperationsAttrEntity> lstEntity);
        PL_ProcessOfOperationsAttrEntity GetEntity(string keyValue);
        IEnumerable<PL_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition);
 
    }
}
