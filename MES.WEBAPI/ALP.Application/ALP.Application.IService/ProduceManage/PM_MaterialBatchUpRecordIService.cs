using ALP.Application.Entity.MaterialManage;
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
   public  interface PM_MaterialBatchUpRecordIService
    {
        IEnumerable<PM_MaterialBatchUpRecordEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_MaterialBatchUpRecordEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_MaterialBatchUpRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_MaterialBatchUpRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_MaterialBatchUpRecordEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_MaterialBatchUpRecordEntity GetEntity(string keyValue);
        PM_MaterialBatchUpRecordEntity GetEntityByQuery(string QueryField);
        PM_MaterialBatchUpRecordEntity Get_ExpressionEntity(Expression<Func<PM_MaterialBatchUpRecordEntity, bool>> condition);
        IEnumerable<PM_MaterialBatchUpRecordEntity> Get_ExpressionList(Expression<Func<PM_MaterialBatchUpRecordEntity, bool>> condition);
        IEnumerable<PM_MaterialBatchUpRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
