using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_BGBadRecord_IService
    {
        IEnumerable<PM_BGBadRecordEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_BGBadRecordEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_BGBadRecordEntity> GetList(Expression<Func<PM_BGBadRecordEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_BGBadRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_BGBadRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_BGBadRecordEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_BGBadRecordEntity GetEntity(string keyValue);
        PM_BGBadRecordEntity GetEntityByQuery(string QueryField);
        PM_BGBadRecordEntity Get_ExpressionEntity(Expression<Func<PM_BGBadRecordEntity, bool>> condition);
        IEnumerable<PM_BGBadRecordEntity> Get_ExpressionList(Expression<Func<PM_BGBadRecordEntity, bool>> condition);
        IEnumerable<PM_BGBadRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
