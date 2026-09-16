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
    public class PL_PlanStoreIssueBLL
    {
        private PL_PlanStoreIssueIService service = new PL_PlanStoreIssue_Service();
        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            return service.GetListWithPage(pagination, queryJson);
        }
        public DataTable GetListWithPageExeWorkOrder(Pagination pagination, string queryJson)
        {
            return service.GetListWithPageExeWorkOrder(pagination, queryJson);
        }
        public DataTable GetStoreIssueWorkOrderOld(string index, string queryJson)
        {
            return service.GetStoreIssueWorkOrderOld(index, queryJson);
        }
        public DataTable GetStoreIssueWorkOrder(List<dynamic> list)
        {
            return service.GetStoreIssueWorkOrder(list);
        }
        public int UpdateForm(List<PL_PlanStoreIssueEntity> list)
        {
            return service.UpdateForm(list);
        }
        public int SaveForm(string keyvalue, PL_PlanStoreIssueEntity entity)
        {
            return service.SaveForm(keyvalue, entity);
        }
        public int RemoveForm(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public PL_PlanStoreIssueEntity GetEntity(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PlanStoreIssueEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public IEnumerable<PL_PlanStoreIssueEntity> GetList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }

        public void SaveFormModel(PL_WorkOrderModel model)
        {
            service.SaveForm(model.Id, new PL_PlanStoreIssueEntity()
            {
                //MaskStatus="2",
                Id = model.Id,
                WearLayerStatus = model.WearLayerStatus,
                UnProductNum = model.UnProductNum,
                ModifyBy = model.ModifyBy,
                ModifyTime = model.ModifyTime
                //ShouldNum=model.ShouldNum,
                //ActualNum= model.ActualNum,
                //DeductionNum=model.DeductionNum,
                //DeductionAfterNum=model.DeductionAfterNum,
                //SuperNum=model.SuperNum,
                //ConsumeNum=model.ConsumeNum,
            });
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="lstEntity"></param>
        /// <returns></returns>
        public int UpdateList(List<PL_PlanStoreIssueEntity> lstEntity)
        {
            return service.UpdateList(lstEntity);
        }
        public int InsertList(List<PL_PlanStoreIssueEntity> lstEntity)
        {
            return service.InsertList(lstEntity);
        }
        public int Delete(List<PL_PlanStoreIssueEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public IEnumerable<PL_PlanStoreIssueEntity> Get_ExpressionList(Expression<Func<PL_PlanStoreIssueEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }

    }
}
