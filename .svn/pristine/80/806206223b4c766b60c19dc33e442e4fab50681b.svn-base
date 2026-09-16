using ALP.Application.Code.Model;
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
    public class PL_WorkOrderBLL
    {
        private PL_WorkOrderIService service = new PL_WorkOrder_Service();
        public IEnumerable<PL_WorkOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public DataTable GetProductOrderPageDataTableList(Pagination pagination, string queryJson, string userCode = "")
        {
            return service.GetProductOrderPageDataTableList(pagination, queryJson, userCode);
        }
        public IEnumerable<PL_WorkOrderEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_WorkOrderEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_WorkOrderEntity> entity_list, out string msg)
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
        public int RemoveForm(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete(List<PL_WorkOrderEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PL_WorkOrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_WorkOrderEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PL_WorkOrderEntity Get_ExpressionEntity(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_WorkOrderEntity> Get_ExpressionList(Expression<Func<PL_WorkOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PL_WorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
        public string VCWorkOrderImport(List<PL_WorkOrderEntity> listPrd, string isAdd)
        {
            return service.VCWorkOrderImport(listPrd, isAdd);
        }
        public string WorkOrderImport(List<PL_WorkOrderEntity> listPrd,string isAdd)
        {
            return service.WorkOrderImport(listPrd, isAdd);
        }
        public int UpdateWorkOrderStatus(string productOrder, string status, string userCode)
        {
            return service.UpdateWorkOrderStatus(productOrder,status,userCode);
        }
        public void UpdateOrderStatus(PL_WorkOrderModel model, out string msg)
        {
            var workOrder = model.WorkOrder;
            var ent = service.Get_ExpressionEntity(r => r.WorkOrder == workOrder);
            ent.OrderStatus = "4";//已发料
            ent.ModifyBy = model.ModifyBy;
            ent.ModifyTime = model.ModifyTime;
            service.SaveEntity(ent.Id, ent, out msg);
        }

        public void UpdateOrderStatus(List<PL_WorkOrderModel> lstModel)
        {
            List<PL_WorkOrderEntity> lstEntity = new List<PL_WorkOrderEntity>();

            lstModel.ForEach(t =>
            {
                var workOrder = t.WorkOrder;
                var ent = service.Get_ExpressionEntity(r => r.WorkOrder == workOrder);
                ent.OrderStatus = "4";//已发料
                ent.ModifyBy = t.ModifyBy;
                ent.ModifyTime = t.ModifyTime;
                lstEntity.Add(ent);
            });

            service.UpdateList(lstEntity);
        }

        public int UpdateList(List<PL_WorkOrderEntity> lstEntity)
        {
            return service.UpdateList(lstEntity);
        }
        public List<dynamic> GetWorkOrderYield(string ProductOrder)
        {
            return service.GetWorkOrderYield(ProductOrder);
        }
        public List<dynamic> GetSplitIsOrderOrProduct(string ProductOrder)
        {
            return service.GetSplitIsOrderOrProduct(ProductOrder);
        }
        public List<dynamic> GetWorkOrderMaterialPieceConsume(string productOrder)
        {
            return service.GetWorkOrderMaterialPieceConsume(productOrder);
        }

        public DataTable GetWorkOrderDataTable(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderDataTable(pagination, queryJson);
        }


        #region PDA
        /// <summary>
        /// 工单详情
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        public DataTable GetWorkOrderInfoToPDA(string workOrder)
        {
            return service.GetWorkOrderInfoToPDA(workOrder);
        }
        #endregion
    }
}
