using ALP.Application.Entity.MaterialManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ALP.Application.IService.MaterialManage
{
    public interface MM_RawMaterialOut_IService
    {
        IEnumerable<MM_RawMaterialOutEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableGroupList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableListItem(Pagination pagination, string queryJson);
        IEnumerable<MM_RawMaterialOutEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, MM_RawMaterialOutEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialOutEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<MM_RawMaterialOutEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        MM_RawMaterialOutEntity GetEntity(string keyValue);
        MM_RawMaterialOutEntity GetEntityByQuery(string QueryField);
        MM_RawMaterialOutEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialOutEntity, bool>> condition);
        IEnumerable<MM_RawMaterialOutEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialOutEntity, bool>> condition);
        IEnumerable<MM_RawMaterialOutEntity> GetList_TestOtherEntity(string materialCode,string factoryCode, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
