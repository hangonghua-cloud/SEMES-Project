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
    public interface PL_ProductionOrderIService
    {
        IEnumerable<PL_ProductionOrderEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson,string userCode = "");
        IEnumerable<PL_ProductionOrderEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ProductionOrderEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProductionOrderEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PL_ProductionOrderEntity, bool>> condition);
        int Delete(List<PL_ProductionOrderEntity> lstEntity);
        int Delete(PL_ProductionOrderEntity entity);
        int Delete_SQL(string keyValue, out string msg);
        PL_ProductionOrderEntity GetEntity(string keyValue);
        PL_ProductionOrderEntity GetEntityByQuery(string QueryField);
        PL_ProductionOrderEntity Get_ExpressionEntity(Expression<Func<PL_ProductionOrderEntity, bool>> condition);
        IEnumerable<PL_ProductionOrderEntity> Get_ExpressionList(Expression<Func<PL_ProductionOrderEntity, bool>> condition);
        IEnumerable<PL_ProductionOrderEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        string GetList_export2(string checkType, out string msg);
        void DataTableToSQLServer(DataTable dt1, DataTable dt2);

        string ProductOrderImport(List<PL_ProductionOrderEntity> listPrd);
    }
}
