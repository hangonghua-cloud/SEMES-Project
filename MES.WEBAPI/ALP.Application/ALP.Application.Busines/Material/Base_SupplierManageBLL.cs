using ALP.Application.Entity.Material;
using ALP.Application.IService.Material;
using ALP.Application.Service.Material;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.Material
{
    public class Base_SupplierManageBLL
    {
        private Base_SupplierManageIService service = new Base_SupplierManage_Service();

        public IEnumerable<Base_SupplierManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }


        public IEnumerable<Base_SupplierManageEntity> GetList(string checkType, string Name = "")
        {
            return service.GetList(checkType, Name);
        }
        public int SaveEntity(string keyValue, Base_SupplierManageEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_SupplierManageEntity> entity_list, out string msg)
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
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public Base_SupplierManageEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public Base_SupplierManageEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public Base_SupplierManageEntity Get_ExpressionEntity(Expression<Func<Base_SupplierManageEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<Base_SupplierManageEntity> Get_ExpressionList(Expression<Func<Base_SupplierManageEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<Base_SupplierManageEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
