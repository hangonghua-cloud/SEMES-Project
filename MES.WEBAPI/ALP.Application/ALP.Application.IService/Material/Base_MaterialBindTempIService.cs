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
    public interface Base_MaterialBindTempIService 
    {
        IEnumerable<Base_MaterialBindTempEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<Base_MaterialBindTempEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, Base_MaterialBindTempEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialBindTempEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        Base_MaterialBindTempEntity GetEntity(string keyValue);
        Base_MaterialBindTempEntity GetEntityByQuery(string QueryField);
        Base_MaterialBindTempEntity Get_ExpressionEntity(Expression<Func<Base_MaterialBindTempEntity, bool>> condition);
        IEnumerable<Base_MaterialBindTempEntity> Get_ExpressionList(Expression<Func<Base_MaterialBindTempEntity, bool>> condition);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
