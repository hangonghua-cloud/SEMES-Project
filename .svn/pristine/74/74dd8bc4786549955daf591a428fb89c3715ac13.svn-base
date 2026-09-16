using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_PrinterWorkOrderIService
    {
        IEnumerable<PM_PrinterWorkOrderEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_PrinterWorkOrderEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_PrinterWorkOrderEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PrinterWorkOrderEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_PrinterWorkOrderEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_PrinterWorkOrderEntity GetEntity(string keyValue);
        PM_PrinterWorkOrderEntity GetEntityByQuery(string QueryField);
        PM_PrinterWorkOrderEntity Get_ExpressionEntity(Expression<Func<PM_PrinterWorkOrderEntity, bool>> condition);
        IEnumerable<PM_PrinterWorkOrderEntity> Get_ExpressionList(Expression<Func<PM_PrinterWorkOrderEntity, bool>> condition);
        IEnumerable<PM_PrinterWorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        string WorkOrderImport(List<PM_PrinterWorkOrderEntity> listPrd);
    }
}
