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
    public interface PL_WorkOrderIService
    {
        IEnumerable<PL_WorkOrderEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetProductOrderPageDataTableList(Pagination pagination, string queryJson, string userCode = "");
        IEnumerable<PL_WorkOrderEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_WorkOrderEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_WorkOrderEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PL_WorkOrderEntity, bool>> condition);
        int Delete(List<PL_WorkOrderEntity> lstEntity);
        int Delete_SQL(string keyValue, out string msg);
        PL_WorkOrderEntity GetEntity(string keyValue);
        PL_WorkOrderEntity GetEntityByQuery(string QueryField);
        PL_WorkOrderEntity Get_ExpressionEntity(Expression<Func<PL_WorkOrderEntity, bool>> condition);
        IEnumerable<PL_WorkOrderEntity> Get_ExpressionList(Expression<Func<PL_WorkOrderEntity, bool>> condition);
        IEnumerable<PL_WorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        int UpdateWorkOrderStatus(string productOrder, string status, string userCode);
        string WorkOrderImport(List<PL_WorkOrderEntity> listPrd, string isAdd);
        string VCWorkOrderImport(List<PL_WorkOrderEntity> listPrd,string isAdd);
        int UpdateList(List<PL_WorkOrderEntity> lstEntity);
        List<dynamic> GetWorkOrderYield(string ProductOrder);
        List<dynamic> GetSplitIsOrderOrProduct(string ProductOrder);
        List<dynamic> GetWorkOrderMaterialPieceConsume(string productOrder);
        DataTable GetWorkOrderDataTable(Pagination pagination, string queryJson);

        #region PDA
        /// <summary>
        /// 工单详情
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        DataTable GetWorkOrderInfoToPDA(string workOrder);
        #endregion
    }
}
