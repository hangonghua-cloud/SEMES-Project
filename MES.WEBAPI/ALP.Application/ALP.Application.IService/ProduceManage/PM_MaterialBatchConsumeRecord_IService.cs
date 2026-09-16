using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_MaterialBatchConsumeRecord_IService
    {
        IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_MaterialBatchConsumeRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_MaterialBatchConsumeRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_MaterialBatchConsumeRecordEntity GetEntity(string keyValue);
        PM_MaterialBatchConsumeRecordEntity GetEntityByQuery(string QueryField);
        PM_MaterialBatchConsumeRecordEntity Get_ExpressionEntity(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition);
        IEnumerable<PM_MaterialBatchConsumeRecordEntity> Get_ExpressionList(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition);
        IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
