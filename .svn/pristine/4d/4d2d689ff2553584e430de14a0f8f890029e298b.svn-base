using ALP.Application.Entity.MaterialManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ALP.Application.IService.MaterialManage
{
    public interface MM_SupProductStockTransfer_IService
    {
        IEnumerable<MM_SupProductStockTransferEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_SupProductStockTransferEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, MM_SupProductStockTransferEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_SupProductStockTransferEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        MM_SupProductStockTransferEntity GetEntity(string keyValue);
        MM_SupProductStockTransferEntity GetEntityByQuery(string QueryField);
        MM_SupProductStockTransferEntity Get_ExpressionEntity(Expression<Func<MM_SupProductStockTransferEntity, bool>> condition);
        IEnumerable<MM_SupProductStockTransferEntity> Get_ExpressionList(Expression<Func<MM_SupProductStockTransferEntity, bool>> condition);
        IEnumerable<MM_SupProductStockTransferEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
