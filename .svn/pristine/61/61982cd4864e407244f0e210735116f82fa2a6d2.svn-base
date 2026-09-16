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
    public interface PL_MaterialFacetIService
    {
        PL_MaterialFacetEntity Get_ExpressionEntity(Expression<Func<PL_MaterialFacetEntity, bool>> condition);
        IEnumerable<PL_MaterialFacetEntity> Get_ExpressionList(Expression<Func<PL_MaterialFacetEntity, bool>> condition);
        void RemoveForm(Expression<Func<PL_MaterialFacetEntity, bool>> condition);

        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_MaterialFacetEntity> entity_list, out string msg);
        int Delete(List<PL_MaterialFacetEntity> lstEntity);
    }
}
