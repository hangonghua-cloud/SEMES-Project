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
    public interface MM_RawMaterialInIService
    {
        IEnumerable<MM_RawMaterialInEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_RawMaterialInEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, MM_RawMaterialInEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_RawMaterialInEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<MM_RawMaterialInEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        MM_RawMaterialInEntity GetEntity(string keyValue);
        MM_RawMaterialInEntity GetEntityByQuery(string QueryField);
        MM_RawMaterialInEntity Get_ExpressionEntity(Expression<Func<MM_RawMaterialInEntity, bool>> condition);
        IEnumerable<MM_RawMaterialInEntity> Get_ExpressionList(Expression<Func<MM_RawMaterialInEntity, bool>> condition);
        IEnumerable<MM_RawMaterialInEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
