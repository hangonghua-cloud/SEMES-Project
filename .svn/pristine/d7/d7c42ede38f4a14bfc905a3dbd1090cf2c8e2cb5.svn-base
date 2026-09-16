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
    public interface QC_MaterialInventoryCheck_IService
    {
        IEnumerable<QC_MaterialInventoryCheckEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_MaterialInventoryCheckEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, QC_MaterialInventoryCheckEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_MaterialInventoryCheckEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_MaterialInventoryCheckEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_MaterialInventoryCheckEntity GetEntity(string keyValue);
        QC_MaterialInventoryCheckEntity GetEntityByQuery(string QueryField);
        QC_MaterialInventoryCheckEntity Get_ExpressionEntity(Expression<Func<QC_MaterialInventoryCheckEntity, bool>> condition);
        IEnumerable<QC_MaterialInventoryCheckEntity> Get_ExpressionList(Expression<Func<QC_MaterialInventoryCheckEntity, bool>> condition);
        IEnumerable<QC_MaterialInventoryCheckEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
