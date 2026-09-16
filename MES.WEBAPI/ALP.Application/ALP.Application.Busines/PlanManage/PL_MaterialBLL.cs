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
    public class PL_MaterialBLL
    {
        private PL_MaterialIService service = new PL_Material_Service();
        public DataTable GetWorkOrderMaterial(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderMaterial(pagination, queryJson);
        }

        public List<dynamic> Get_ExpressionEntity1(string WorkOrder)
        {
            return service.Get_ExpressionEntity1(WorkOrder);
        }


        public PL_MaterialEntity Get_ExpressionEntity(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_MaterialEntity> Get_ExpressionList(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public void RemoveForm(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }
        public bool InsertPLMaterial(string factory, string materialCode, string processRoute, string workOrder)
        {
            return service.InsertPLMaterial(factory, materialCode, processRoute, workOrder);
        }

        public int SaveEntity(string keyValue, PL_MaterialEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }

        public int Delete(List<PL_MaterialEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
    }
}
