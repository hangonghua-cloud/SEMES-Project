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
    public interface BS_BOMIService
    {
        IEnumerable<BS_BOMEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<BS_BOMEntity> GetList(string checkType,string factoryCode, out string msg);
        int SaveEntity(string keyValue, BS_BOMEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<BS_BOMEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        BS_BOMEntity GetEntity(string keyValue);
        BS_BOMEntity GetEntityByQuery(string QueryField);
        BS_BOMEntity Get_ExpressionEntity(Expression<Func<BS_BOMEntity, bool>> condition);
        IEnumerable<BS_BOMEntity> Get_ExpressionList(Expression<Func<BS_BOMEntity, bool>> condition);
        IEnumerable<BS_BOMEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, int index, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        DataTable GetFormulaBOM(Pagination pagination, string queryJson);
    }
}
