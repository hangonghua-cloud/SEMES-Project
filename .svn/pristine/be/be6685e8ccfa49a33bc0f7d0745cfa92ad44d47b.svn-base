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
    public class PL_ProcessBLL
    {
        private PL_ProcessIService service = new PL_Process_Service();
        public IEnumerable<PL_ProcessEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_ProcessEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_ProcessEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProcessEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int RemoveForm(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete(List<PL_ProcessEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public PL_ProcessEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_ProcessEntity GetEntity(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public IEnumerable<PL_ProcessEntity> Get_ExpressionList(Expression<Func<PL_ProcessEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public bool InsertPLProcess(string factory, string processRoute, string workOrder, string startOperation)
        {
            return service.InsertPLProcess(factory, processRoute, workOrder,startOperation);
        }
    }
}
