using ALP.Application.Entity.Material;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.Material
{
    public interface Base_SupplierBindMaterialGroupIService
    {
        IEnumerable<Base_SupplierBindMaterialGroupEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetSupplierBindMaterialSelect(string materialCode);
        IEnumerable<Base_SupplierBindMaterialGroupEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, Base_SupplierBindMaterialGroupEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_SupplierBindMaterialGroupEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        Base_SupplierBindMaterialGroupEntity GetEntity(string keyValue);
        Base_SupplierBindMaterialGroupEntity GetEntityByQuery(string QueryField);
        Base_SupplierBindMaterialGroupEntity Get_ExpressionEntity(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition);
        IEnumerable<Base_SupplierBindMaterialGroupEntity> Get_ExpressionList(Expression<Func<Base_SupplierBindMaterialGroupEntity, bool>> condition);
        IEnumerable<Base_SupplierBindMaterialGroupEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string materialCode, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
