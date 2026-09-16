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
    public interface PM_StartUpRecord_IService
    {
        IEnumerable<PM_StartUpRecordEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_StartUpRecordEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_StartUpRecordEntity> GetList(Expression<Func<PM_StartUpRecordEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_StartUpRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_StartUpRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        PM_StartUpRecordEntity GetEntity(string keyValue);
        PM_StartUpRecordEntity GetEntityByQuery(string QueryField);
        PM_StartUpRecordEntity Get_ExpressionEntity(Expression<Func<PM_StartUpRecordEntity, bool>> condition);
        IEnumerable<PM_StartUpRecordEntity> Get_ExpressionList(Expression<Func<PM_StartUpRecordEntity, bool>> condition);
        IEnumerable<PM_StartUpRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
