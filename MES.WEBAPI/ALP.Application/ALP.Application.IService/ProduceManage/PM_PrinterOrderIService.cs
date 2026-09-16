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
    public interface PM_PrinterOrderIService
    {
        IEnumerable<PM_PrinterOrderEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_PrinterOrderEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_PrinterOrderEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PrinterOrderEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_PrinterOrderEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_PrinterOrderEntity GetEntity(string keyValue);
        PM_PrinterOrderEntity GetEntityByQuery(string QueryField);
        PM_PrinterOrderEntity Get_ExpressionEntity(Expression<Func<PM_PrinterOrderEntity, bool>> condition);
        IEnumerable<PM_PrinterOrderEntity> Get_ExpressionList(Expression<Func<PM_PrinterOrderEntity, bool>> condition);
        IEnumerable<PM_PrinterOrderEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
