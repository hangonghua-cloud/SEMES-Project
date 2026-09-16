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
    public interface BS_ProcessOfOperationsIService
    {
        IEnumerable<BS_ProcessOfOperationsEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<BS_ProcessOfOperationsEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, BS_ProcessOfOperationsEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_ProcessOfOperationsEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<BS_ProcessOfOperationsEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        BS_ProcessOfOperationsEntity GetEntity(string keyValue);
        BS_ProcessOfOperationsEntity GetEntityByQuery(string QueryField);
        BS_ProcessOfOperationsEntity Get_ExpressionEntity(Expression<Func<BS_ProcessOfOperationsEntity, bool>> condition);
        IEnumerable<BS_ProcessOfOperationsEntity> Get_ExpressionList(Expression<Func<BS_ProcessOfOperationsEntity, bool>> condition);
        IEnumerable<BS_ProcessOfOperationsEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
