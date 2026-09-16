using ALP.Application.Entity.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.PlanManage
{
    public interface PL_PurchaseOrderIService
    {
        IEnumerable<PL_PurchaseOrderEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_PurchaseOrderEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_PurchaseOrderEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PurchaseOrderEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        PL_PurchaseOrderEntity GetEntity(string keyValue);
        PL_PurchaseOrderEntity GetEntityByQuery(string QueryField);
        PL_PurchaseOrderEntity Get_ExpressionEntity(Expression<Func<PL_PurchaseOrderEntity, bool>> condition);
        IEnumerable<PL_PurchaseOrderEntity> Get_ExpressionList(Expression<Func<PL_PurchaseOrderEntity, bool>> condition);
        IEnumerable<PL_PurchaseOrderEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        DataTable GetDataTableList_Export(string queryJson);
        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:16
        /// 任务编号: 按照库存采购数据源
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageDataTableListBySotck(Pagination pagination, string queryJson);
        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-27 16:28:16
        /// 任务编号: 新建收料通知单时使用
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageDataTableListByReceiptNotice(Pagination pagination, string queryJson);
    }
}
