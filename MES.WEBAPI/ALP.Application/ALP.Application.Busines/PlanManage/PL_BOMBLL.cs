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
    public class PL_BOMBLL
    {
        private PL_BOMIService service = new PL_BOM_Service();
        public DataTable GetWorkOrderMaterial(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderMaterial(pagination, queryJson);
        }
        public PL_MaskUnitConsomeModel GetWorkOrderBomUnitConsome(string queryJson)
        {
            return service.GetWorkOrderBomUnitConsome(queryJson);
        }
        public DataTable GetWorkOrderBom(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderBom(pagination, queryJson);
        }

        public DataTable GetOwnProductOrderBom(Pagination pagination, string queryJson)
        {
            return service.GetOwnProductOrderBom(pagination, queryJson);
        }
        public DataTable GetWorkOrderBomItem(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderBomItem(pagination, queryJson);
        }

        public PL_BOMEntity Get_ExpressionEntity(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_BOMEntity> Get_ExpressionList(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public void SaveForm(string keyValue, PL_BOMEntity entity)
        {
            service.SaveForm(keyValue, entity);
        }
        public int SaveEntity_List(bool isUpdate, List<PL_BOMEntity> list)
        {
            return service.SaveEntity_List(isUpdate, list);
        }
        public void RemoveForm(Expression<Func<PL_BOMEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }
        public int Delete(List<PL_BOMEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public bool InsertPLBom(string factory, string bomCode, string processRoute,string orderType, string workOrder)
        {
            return service.InsertPLBom(factory, bomCode, processRoute, orderType, workOrder);
        }
        public void InsertDataTable(DataTable dt)
        {
            service.InsertDataTable(dt);
        }
    }
}
