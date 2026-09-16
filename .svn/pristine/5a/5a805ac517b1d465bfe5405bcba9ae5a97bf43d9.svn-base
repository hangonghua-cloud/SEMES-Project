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
   public interface PM_TransferBGPersonRecord_IService
    {
        IEnumerable<PM_TransferBGPersonRecordEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_TransferBGPersonRecordEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_TransferBGPersonRecordEntity> GetList(Expression<Func<PM_TransferBGPersonRecordEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_TransferBGPersonRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferBGPersonRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_TransferBGPersonRecordEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_TransferBGPersonRecordEntity GetEntity(string keyValue);
        PM_TransferBGPersonRecordEntity GetEntityByQuery(string QueryField);
        PM_TransferBGPersonRecordEntity Get_ExpressionEntity(Expression<Func<PM_TransferBGPersonRecordEntity, bool>> condition);
        IEnumerable<PM_TransferBGPersonRecordEntity> Get_ExpressionList(Expression<Func<PM_TransferBGPersonRecordEntity, bool>> condition);
        IEnumerable<PM_TransferBGPersonRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
