using ALP.Application.Entity.MaterialManage;
using ALP.Application.IService.MaterialManage;
using ALP.Application.Service.MaterialManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.MaterialManage
{
    public class MM_RawMaterialStockBLL
    {
        private MM_RawMaterialStockIService service = new MM_RawMaterialStock_Service();

        public IEnumerable<MM_RawMaterialStockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public DataTable GetRawMaterialStock(string queryJson)
        {
            return service.GetRawMaterialStock(queryJson);
        }
        public IEnumerable<MM_RawMaterialStockEntity> GetList(Dictionary<string,object> dic, out string msg)
        {
            return service.GetList(dic, out msg);
        }
        public IEnumerable<MM_RawMaterialStockEntity> GetList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public int SaveEntity(string keyValue, MM_RawMaterialStockEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialStockEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }
        public int RemoveForm(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public MM_RawMaterialStockEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public MM_RawMaterialStockEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public MM_RawMaterialStockEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<MM_RawMaterialStockEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialStockEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<MM_RawMaterialStockEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public List<dynamic> GetDynamic_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDynamic_TestOtherEntity(checkType, out msg);
        }
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
        /// <summary>
        /// 库存汇总查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableMList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableMList(pagination, queryJson);
        }
        /// <summary>
        /// 库存详情查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableDList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableDList(pagination, queryJson);
        }
        public DataTable GetPageDataTableCheck(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableCheck(pagination, queryJson);
        }
        public int UpdateisFrozen(string isFrozen, string batchNo,string WhsCode, string userCode)
        {
            return service.UpdateisFrozen(isFrozen, batchNo, WhsCode, userCode);
        }
        public DataTable GetMaterialStockTable(string queryJson)
        {
            return service.GetMaterialStockTable(queryJson);
        }
        public DataTable GetMaterialOrderStockTable(string queryJson)
        {
            return service.GetMaterialOrderStockTable(queryJson);
        }
        public void StockBackup()
        {
            service.StockBackup();
        }
    }
}
