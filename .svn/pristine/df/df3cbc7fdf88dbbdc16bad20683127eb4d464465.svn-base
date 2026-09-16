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
    public class PL_ProcessOfOperationsBLL
    {
        private PL_ProcessOfOperationsIService service = new PL_ProcessOfOperations_Service();
        public IEnumerable<PL_ProcessOfOperationsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_ProcessOfOperationsEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_ProcessOfOperationsEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int RemoveForm(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete(List<PL_ProcessOfOperationsEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public PL_ProcessOfOperationsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_ProcessOfOperationsEntity Get_ExpressionEntity(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_ProcessOfOperationsEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public void UpdateHealthTime(string workOrder)
        {
            service.UpdateHealthTime(workOrder);
        }
    }
}
