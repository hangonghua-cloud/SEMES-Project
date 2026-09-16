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
    public class PL_ExeWorkOrderBLL
    {
        private PL_ExeWorkOrderIService service = new PL_ExeWorkOrder_Service();
        public IEnumerable<PL_ExeWorkOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_ExeWorkOrderEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_ExeWorkOrderEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ExeWorkOrderEntity> entity_list, out string msg)
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
        public int RemoveForm(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PL_ExeWorkOrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public dynamic GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PL_ExeWorkOrderEntity Get_ExpressionEntity(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_ExeWorkOrderEntity> Get_ExpressionList(Expression<Func<PL_ExeWorkOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PL_ExeWorkOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
        public void SaveFormModel(PL_WorkOrderModel model, out string msg)
        {
            var index = 0;
            var workOrder = model.WorkOrder;
            var exelist = service.Get_ExpressionList(m => m.WorkOrder == workOrder);
            index = exelist.Count() + 1;
            service.SaveEntity(null, new PL_ExeWorkOrderEntity()
            {
                Id = Guid.NewGuid().ToString(),
                WorkOrder = workOrder,
                ExeWorkOrder = workOrder + "-" + (index.ToString().PadLeft(2, '0')),
                Status = "1",
                OrderType = model.WorkOrderType,
                SheetsQty = model.ShouldNum,
                PiecesQty = model.ActNum,
                ActualNum = model.ActualNum,
                SuperNum = model.SuperNum,
                ShouldNum = model.ShouldNum,
                ConsumeNum = model.ConsumeNum,
                Process = model.Process,
                StartOperation = model.StartOperation,
                IsEnabled = true,
                TransferBy = model.TransferBy,
                Creator = model.ModifyBy,
                CreateTime = model.ModifyTime
            }, out msg);
            //超产品单
            if (model.DeductionNum > 0)
            {
                index++;
                service.SaveEntity(null, new PL_ExeWorkOrderEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkOrder = workOrder,
                    ExeWorkOrder = workOrder + "-" + (index.ToString().PadLeft(2, '0')),
                    Status = "1",
                    //TotalSheets = null,
                    PiecesQty = model.DeductionNum,
                    OrderType = "5",//超产品单
                    Yield = model.Yield,
                    Process = model.Process,
                    StartOperation = model.SuperStartOperation,
                    TransferBy = model.TransferBy,
                    IsEnabled = true,
                    Creator = model.ModifyBy,
                    CreateTime = model.ModifyTime
                }, out msg);
            }

        }

        public int InsertList(List<PL_ExeWorkOrderEntity> lstEntity)
        {
            return service.InsertList(lstEntity);
        }
    }
}
