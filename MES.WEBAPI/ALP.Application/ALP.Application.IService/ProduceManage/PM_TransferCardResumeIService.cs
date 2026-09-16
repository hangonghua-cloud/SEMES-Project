using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_TransferCardResumeIService
    {
        IEnumerable<PM_TransferCardResumeEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_TransferCardResumeEntity> GetList(Dictionary<string, object> dic);
        IEnumerable<PM_TransferCardResumeEntity> GetList(Expression<Func<PM_TransferCardResumeEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_TransferCardResumeEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferCardResumeEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_TransferCardResumeEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_TransferCardResumeEntity GetEntity(string keyValue);
        PM_TransferCardResumeEntity GetEntityByQuery(string QueryField);
        PM_TransferCardResumeEntity Get_ExpressionEntity(Expression<Func<PM_TransferCardResumeEntity, bool>> condition);
        IEnumerable<PM_TransferCardResumeEntity> Get_ExpressionList(Expression<Func<PM_TransferCardResumeEntity, bool>> condition);
        IEnumerable<PM_TransferCardResumeEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        DataTable GetPageDataTableListReceive(Pagination pagination, string queryJson);
        DataTable GetPageDataTableListReceiveItem(Pagination pagination, string queryJson);

        DataTable GetPageDataTableListBeforeSlotting(Pagination pagination, string queryJson);
        DataTable GetPageDataTableListBeforeSlottingItem(Pagination pagination, string queryJson);
        
        DataTable GetPageDataTableListCompete(Pagination pagination, string queryJson);
        DataTable GetPageDataTableListCompeteItem(Pagination pagination, string queryJson);
    }
}
