using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ALP.Util.WebControl;
using ALP.Application.Entity.QualityManage;

namespace ALP.Application.IService.QualityManage
{
    public interface QC_TestMethodMaintenance_IService
    {
        IEnumerable<QC_TestMethodMaintenanceEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_TestMethodMaintenanceEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, QC_TestMethodMaintenanceEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_TestMethodMaintenanceEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_TestMethodMaintenanceEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_TestMethodMaintenanceEntity GetEntity(string keyValue);
        QC_TestMethodMaintenanceEntity GetEntityByQuery(string QueryField);
        QC_TestMethodMaintenanceEntity Get_ExpressionEntity(Expression<Func<QC_TestMethodMaintenanceEntity, bool>> condition);
        IEnumerable<QC_TestMethodMaintenanceEntity> Get_ExpressionList(Expression<Func<QC_TestMethodMaintenanceEntity, bool>> condition);
        IEnumerable<QC_TestMethodMaintenanceEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
