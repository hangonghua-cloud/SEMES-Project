using ALP.Application.Code.Model;
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
    public interface PL_BOMIService
    {
        DataTable GetWorkOrderMaterial(Pagination pagination, string queryJson);
        PL_MaskUnitConsomeModel GetWorkOrderBomUnitConsome(string queryJson);
        DataTable GetWorkOrderBom(Pagination pagination, string queryJson);
        DataTable GetOwnProductOrderBom(Pagination pagination, string queryJson);
        DataTable GetWorkOrderBomItem(Pagination pagination, string queryJson);
        PL_BOMEntity Get_ExpressionEntity(Expression<Func<PL_BOMEntity, bool>> condition);
        IEnumerable<PL_BOMEntity> Get_ExpressionList(Expression<Func<PL_BOMEntity, bool>> condition);
        void SaveForm(string keyValue, PL_BOMEntity entity);
        int SaveEntity_List(bool isUpdate, List<PL_BOMEntity> list);
        void RemoveForm(Expression<Func<PL_BOMEntity, bool>> condition);
        int Delete(List<PL_BOMEntity> lstEntity);

        bool InsertPLBom(string factory, string bomCode, string processRoute,string orderType, string workOrder);
        void InsertDataTable(DataTable dt);
    }
}
