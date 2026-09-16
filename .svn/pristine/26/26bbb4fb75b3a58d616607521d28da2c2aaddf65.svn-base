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
    public class Base_SupplierBindMaterialGroupBLL
    {
        private Base_SupplierBindMaterialGroupIService service = new Base_SupplierBindMaterialGroup_Service();

        public IEnumerable<Base_SupplierBindMaterialGroupEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetSupplierBindMaterialSelect(string materialCode)
        {
            return service.GetSupplierBindMaterialSelect(materialCode);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<Base_SupplierBindMaterialGroupEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, Base_SupplierBindMaterialGroupEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_SupplierBindMaterialGroupEntity> entity_list, out string msg)
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
        public int RemoveForm(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public Base_SupplierBindMaterialGroupEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public Base_SupplierBindMaterialGroupEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public Base_SupplierBindMaterialGroupEntity Get_ExpressionEntity(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<Base_SupplierBindMaterialGroupEntity> Get_ExpressionList(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<Base_SupplierBindMaterialGroupEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public DataTable GetDataTable_TestOtherEntity(string materialCode, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(materialCode, out msg);
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
