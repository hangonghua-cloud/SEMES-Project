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
    public class PL_ProductionOrderBLL
    {
        private PL_ProductionOrderIService service = new PL_ProductionOrder_Service();
        public IEnumerable<PL_ProductionOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson, string userCode = "")
        {
            return service.GetPageDataTableList(pagination, queryJson, userCode);
        }
        public IEnumerable<PL_ProductionOrderEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_ProductionOrderEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ProductionOrderEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }
        public int RemoveForm(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete(List<PL_ProductionOrderEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public int Delete(PL_ProductionOrderEntity entity)
        {
            return service.Delete(entity);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PL_ProductionOrderEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_ProductionOrderEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PL_ProductionOrderEntity Get_ExpressionEntity(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_ProductionOrderEntity> Get_ExpressionList(Expression<Func<PL_ProductionOrderEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PL_ProductionOrderEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
        }
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
        public string GetList_export2(string checkType, out string msg)
        {
            return service.GetList_export2(checkType, out msg);
        }
        public void DataTableToSQLServer(DataTable dt1, DataTable dt2)
        {
            service.DataTableToSQLServer(dt1, dt2);
        }
        public string ProductOrderImport(List<PL_ProductionOrderEntity> listPrd)
        {
            return service.ProductOrderImport(listPrd);
        }
    }
}
