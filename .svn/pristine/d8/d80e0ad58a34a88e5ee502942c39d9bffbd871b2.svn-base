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
    public class MM_SuperProductStockBLL
    {
        private MM_SuperProductStock_IService service = new MM_SuperProductStock_Service();

        public IEnumerable<MM_SuperProductStockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<MM_SuperProductStockEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public IEnumerable<MM_SuperProductStockEntity> GetList(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public int SaveEntity(string keyValue, MM_SuperProductStockEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_SuperProductStockEntity> entity_list, out string msg)
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
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public MM_SuperProductStockEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public MM_SuperProductStockEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public MM_SuperProductStockEntity Get_ExpressionEntity(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<MM_SuperProductStockEntity> Get_ExpressionList(Expression<Func<MM_SuperProductStockEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<MM_SuperProductStockEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
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
        /// <summary>
        /// 待转超产品明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetDZCCPPageDataTableMList(Pagination pagination, string queryJson)
        {
            return service.GetDZCCPPageDataTableMList(pagination, queryJson);
        }
        /// <summary>
        ///  获取执行工单信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        public DataTable GetExeWorkOrderList(Dictionary<string, object> dic)
        {
            return service.GetExeWorkOrderList(dic);
        }


    }
}
