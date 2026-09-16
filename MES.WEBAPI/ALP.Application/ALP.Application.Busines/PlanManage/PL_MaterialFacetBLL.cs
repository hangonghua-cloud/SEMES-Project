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
    public class PL_MaterialFacetBLL
    {
        private PL_MaterialFacetIService service = new PL_MaterialFacet_Service();

        public PL_MaterialFacetEntity Get_ExpressionEntity(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_MaterialFacetEntity> Get_ExpressionList(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public void RemoveForm(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }

        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_MaterialFacetEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int Delete(List<PL_MaterialFacetEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
    }
}
