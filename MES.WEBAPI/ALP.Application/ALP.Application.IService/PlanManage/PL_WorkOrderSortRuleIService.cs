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
    public interface PL_WorkOrderSortRuleIService
    {
        IEnumerable<PL_WorkOrderSortRuleEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_WorkOrderSortRuleEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_WorkOrderSortRuleEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_WorkOrderSortRuleEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(Expression<Func<PL_WorkOrderSortRuleEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PL_WorkOrderSortRuleEntity GetEntity(string keyValue);
        PL_WorkOrderSortRuleEntity GetEntityByQuery(string QueryField);
        PL_WorkOrderSortRuleEntity Get_ExpressionEntity(Expression<Func<PL_WorkOrderSortRuleEntity, bool>> condition);
        IEnumerable<PL_WorkOrderSortRuleEntity> Get_ExpressionList(Expression<Func<PL_WorkOrderSortRuleEntity, bool>> condition);
        IEnumerable<PL_WorkOrderSortRuleEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        DataTable GetTableColumn();
    }
}
