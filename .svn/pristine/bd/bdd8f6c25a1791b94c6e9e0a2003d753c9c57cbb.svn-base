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
    public interface MM_RawMaterialStockIService
    {
        IEnumerable<MM_RawMaterialStockEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetRawMaterialStock(string queryJson);
        IEnumerable<MM_RawMaterialStockEntity> GetList(Dictionary<string, object> dic, out string msg);
        IEnumerable<MM_RawMaterialStockEntity> GetList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_RawMaterialStockEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialStockEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<MM_RawMaterialStockEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        MM_RawMaterialStockEntity GetEntity(string keyValue);
        MM_RawMaterialStockEntity GetEntityByQuery(string QueryField);
        MM_RawMaterialStockEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialStockEntity, bool>> condition);
        IEnumerable<MM_RawMaterialStockEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition);
        IEnumerable<MM_RawMaterialStockEntity> GetList_TestOtherEntity(string checkType, out string msg);
        List<dynamic> GetDynamic_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        /// <summary>
        /// 库存汇总查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetPageDataTableMList(Pagination pagination, string queryJson);
        /// <summary>
        /// 库存详情查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetPageDataTableDList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableCheck(Pagination pagination, string queryJson);
        int UpdateisFrozen(string isFrozen, string batchNo,string WhsCode, string userCode);
        DataTable GetMaterialStockTable(string queryJson);
        DataTable GetMaterialOrderStockTable(string queryJson);
        void StockBackup();
    }
}
