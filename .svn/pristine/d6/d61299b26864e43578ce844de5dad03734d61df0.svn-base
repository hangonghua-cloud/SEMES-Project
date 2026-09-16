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
    public interface PL_ProcessIService
    {
        IEnumerable<PL_ProcessEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_ProcessEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ProcessEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessEntity> entity_list, out string msg);
        int RemoveForm(Expression<Func<PL_ProcessEntity, bool>> condition);
        int Delete(List<PL_ProcessEntity> lstEntity);
        PL_ProcessEntity GetEntity(string keyValue);
        PL_ProcessEntity GetEntity(Expression<Func<PL_ProcessEntity, bool>> condition);
        IEnumerable<PL_ProcessEntity> Get_ExpressionList(Expression<Func<PL_ProcessEntity, bool>> condition);
        bool InsertPLProcess(string factory, string processRoute, string workOrder, string startOperation);
    }
}
