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
    public interface PL_ProductRuleIService
    {
        IEnumerable<PL_ProductRuleEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_ProductRuleEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ProductRuleEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProductRuleEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        PL_ProductRuleEntity GetEntity(string keyValue);
        PL_ProductRuleEntity GetEntityByQuery(string QueryField);
        PL_ProductRuleEntity Get_ExpressionEntity(Expression<Func<PL_ProductRuleEntity, bool>> condition);
        IEnumerable<PL_ProductRuleEntity> Get_ExpressionList(Expression<Func<PL_ProductRuleEntity, bool>> condition);
        IEnumerable<PL_ProductRuleEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
