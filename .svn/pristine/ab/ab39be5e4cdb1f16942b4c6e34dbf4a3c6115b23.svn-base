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
    public class PL_FirstInspectionConfirmBLL
    {
        private PL_FirstInspectionConfirmIService service = new PL_FirstInspectionConfirm_Service();
        public void InsertList(List<PL_FirstInspectionConfirmEntity> list)
        {
            service.InsertList(list);
        }
        public void RemoveForm(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_FirstInspectionConfirmEntity> GetList(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public PL_FirstInspectionConfirmEntity GetEntity(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public void SaveEntity(string keyvalue, PL_FirstInspectionConfirmEntity entity)
        {
             service.SaveEntity(keyvalue, entity);
        }
    }
}
