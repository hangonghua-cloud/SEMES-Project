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
    public interface MM_SuperProductStock_IService
    {
        IEnumerable<MM_SuperProductStockEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_SuperProductStockEntity> GetList(string checkType, out string msg);
        IEnumerable<MM_SuperProductStockEntity> GetList(Expression<Func<MM_SuperProductStockEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_SuperProductStockEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_SuperProductStockEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        MM_SuperProductStockEntity GetEntity(string keyValue);
        MM_SuperProductStockEntity GetEntityByQuery(string QueryField);
        MM_SuperProductStockEntity Get_ExpressionEntity(Expression<Func<MM_SuperProductStockEntity, bool>> condition);
        IEnumerable<MM_SuperProductStockEntity> Get_ExpressionList(Expression<Func<MM_SuperProductStockEntity, bool>> condition);
        IEnumerable<MM_SuperProductStockEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
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
        /// <summary>
        /// 待转超产品明细
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetDZCCPPageDataTableMList(Pagination pagination, string queryJson);
        /// <summary>
        ///  获取执行工单信息
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        DataTable GetExeWorkOrderList(Dictionary<string, object> dic);
    }
}
