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
    public interface BS_ProcessIService
    {
        IEnumerable<BS_ProcessEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<BS_ProcessEntity> GetList(string checkType,string name, out string msg);
        int SaveEntity(string keyValue, BS_ProcessEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_ProcessEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        BS_ProcessEntity GetEntity(string keyValue);
        BS_ProcessEntity GetEntityByQuery(string QueryField);
        BS_ProcessEntity Get_ExpressionEntity(Expression<Func<BS_ProcessEntity, bool>> condition);
        IEnumerable<BS_ProcessEntity> Get_ExpressionList(Expression<Func<BS_ProcessEntity, bool>> condition);
        IEnumerable<BS_ProcessEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
