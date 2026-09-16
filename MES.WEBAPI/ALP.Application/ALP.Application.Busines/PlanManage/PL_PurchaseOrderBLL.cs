using ALP.Application.Entity.PlanManage;
using ALP.Application.IService.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.PlanManage
{
    public class PL_PurchaseOrderBLL
    {
        private PL_PurchaseOrderIService service = new PL_PurchaseOrder_Service();
        public IEnumerable<PL_PurchaseOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:16
        /// 任务编号: 新建收料通知单时使用
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListByReceiptNotice(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableListByReceiptNotice(pagination, queryJson);
        }
        public IEnumerable<PL_PurchaseOrderEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_PurchaseOrderEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PurchaseOrderEntity> entity_list, out string msg)
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
        public PL_PurchaseOrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_PurchaseOrderEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PL_PurchaseOrderEntity Get_ExpressionEntity(Expression<Func<PL_PurchaseOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_PurchaseOrderEntity> Get_ExpressionList(Expression<Func<PL_PurchaseOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PL_PurchaseOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
        public DataTable GetDataTableList_Export(string queryJson)
        {
            return service.GetDataTableList_Export(queryJson);
        }
        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:16
        /// 任务编号: 按照库存采购数据源
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableListBySotck(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableListBySotck(pagination, queryJson);
        }
    }
}
