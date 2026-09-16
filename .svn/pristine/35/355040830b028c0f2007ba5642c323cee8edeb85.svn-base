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
    public interface PL_PlanStoreIssueIService
    {
        DataTable GetListWithPage(Pagination pagination, string queryJson);
        DataTable GetListWithPageExeWorkOrder(Pagination pagination, string queryJson);
        DataTable GetStoreIssueWorkOrderOld(string index, string queryJson);
        DataTable GetStoreIssueWorkOrder(List<dynamic> list);
        int UpdateForm(List<PL_PlanStoreIssueEntity> list);
        int SaveForm(string keyvalue, PL_PlanStoreIssueEntity entity);
        int RemoveForm(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition);
        PL_PlanStoreIssueEntity GetEntity(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PlanStoreIssueEntity> entity_list, out string msg);
        IEnumerable<PL_PlanStoreIssueEntity> GetList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        int InsertList(List<PL_PlanStoreIssueEntity> lstEntity);
        int UpdateList(List<PL_PlanStoreIssueEntity> lstEntity);
        int Delete(List<PL_PlanStoreIssueEntity> lstEntity);
        IEnumerable<PL_PlanStoreIssueEntity> Get_ExpressionList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition);

    }
}
