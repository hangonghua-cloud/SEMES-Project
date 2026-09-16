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
    public class PL_ProcessOfOperationsAttrBLL
    {
        private PL_ProcessOfOperationsAttrIService service = new PL_ProcessOfOperationsAttr_Service();
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType,out msg);
        }
        public int SaveEntity(string keyValue, PL_ProcessOfOperationsAttrEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessOfOperationsAttrEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int RemoveForm(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete(List<PL_ProcessOfOperationsAttrEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public PL_ProcessOfOperationsAttrEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public IEnumerable<PL_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<PL_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
 
    }
}
