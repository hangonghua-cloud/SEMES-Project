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
    public class PL_PrdOrderReqMaterialsBLL
    {
        private PL_PrdOrderReqMaterialsIService service = new PL_PrdOrderReqMaterials_Service();
        public DataTable GetWorkOrderReqMaterials(Pagination pagination, string queryJson)
        {
            return service.GetWorkOrderReqMaterials(pagination, queryJson);
        }
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, PL_PrdOrderReqMaterialsEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PrdOrderReqMaterialsEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public void InsertDataTable(DataTable dt)
        {
             service.InsertDataTable(dt);
        }
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }
        public int Delete(List<PL_PrdOrderReqMaterialsEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PL_PrdOrderReqMaterialsEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PL_PrdOrderReqMaterialsEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PL_PrdOrderReqMaterialsEntity Get_ExpressionEntity(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> Get_ExpressionList(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
    }
}
