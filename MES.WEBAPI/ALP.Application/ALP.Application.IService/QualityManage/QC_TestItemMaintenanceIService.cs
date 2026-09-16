using ALP.Application.Entity.QualityManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.QualityManage
{
    public interface QC_TestItemMaintenanceIService
    {
        IEnumerable<QC_TestItemMaintenanceEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_TestItemMaintenanceEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, QC_TestItemMaintenanceEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_TestItemMaintenanceEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_TestItemMaintenanceEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_TestItemMaintenanceEntity GetEntity(string keyValue);
        QC_TestItemMaintenanceEntity GetEntityByQuery(string QueryField);
        QC_TestItemMaintenanceEntity Get_ExpressionEntity(Expression<Func<QC_TestItemMaintenanceEntity, bool>> condition);
        IEnumerable<QC_TestItemMaintenanceEntity> Get_ExpressionList(Expression<Func<QC_TestItemMaintenanceEntity, bool>> condition);
        IEnumerable<QC_TestItemMaintenanceEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
