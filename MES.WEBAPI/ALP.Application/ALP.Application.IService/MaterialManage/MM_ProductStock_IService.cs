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
    public interface MM_ProductStock_IService
    {
        IEnumerable<MM_ProductStockEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_ProductStockEntity> GetList(string checkType, out string msg);
        IEnumerable<MM_ProductStockEntity> GetList(Expression<Func<MM_ProductStockEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_ProductStockEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductStockEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int DeleteList(List<MM_ProductStockEntity> lstEntity);
        int Delete_SQL(string keyValue, out string msg);
        MM_ProductStockEntity GetEntity(string keyValue);
        MM_ProductStockEntity GetEntityByQuery(string QueryField);
        MM_ProductStockEntity Get_ExpressionEntity(Expression<Func<MM_ProductStockEntity, bool>> condition);
        IEnumerable<MM_ProductStockEntity> Get_ExpressionList(Expression<Func<MM_ProductStockEntity, bool>> condition);
        IEnumerable<MM_ProductStockEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
       
    }
}
