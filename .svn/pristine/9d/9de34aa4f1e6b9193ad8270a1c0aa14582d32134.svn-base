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
    public interface PL_MaterialIService
    {
        DataTable GetWorkOrderMaterial(Pagination pagination, string queryJson);
        PL_MaterialEntity Get_ExpressionEntity(Expression<Func<PL_MaterialEntity, bool>> condition);

    
        List<dynamic> Get_ExpressionEntity1(string WorkOrder);
        IEnumerable<PL_MaterialEntity> Get_ExpressionList(Expression<Func<PL_MaterialEntity, bool>> condition);
        void RemoveForm(Expression<Func<PL_MaterialEntity, bool>> condition);
        bool InsertPLMaterial(string factory, string materialCode, string processRoute, string workOrder);
        int SaveEntity(string keyValue, PL_MaterialEntity entity, out string msg);
        int Delete(List<PL_MaterialEntity> lstEntity);
    }
}
