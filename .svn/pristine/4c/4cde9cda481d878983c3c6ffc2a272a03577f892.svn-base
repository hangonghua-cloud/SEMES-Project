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
    public interface Base_MaterialFactoryIService
    {
        IEnumerable<Base_MaterialFactoryEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<Base_MaterialFactoryEntity> GetList(string checkType, out string msg);
        IEnumerable<Base_MaterialFactoryEntity> GetList(Expression<Func<Base_MaterialFactoryEntity, bool>> condition);
        DataTable GetListSelect(string queryJson);
        int SaveEntity(string keyValue, Base_MaterialFactoryEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialFactoryEntity> entity_list, out string msg);
        void InsertList(List<Base_MaterialFactoryEntity> list);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<Base_MaterialFactoryEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        Base_MaterialFactoryEntity GetEntity(string keyValue);
        Base_MaterialFactoryEntity GetEntityByQuery(string QueryField);
        Base_MaterialFactoryEntity Get_ExpressionEntity(Expression<Func<Base_MaterialFactoryEntity, bool>> condition);
        IEnumerable<Base_MaterialFactoryEntity> Get_ExpressionList(Expression<Func<Base_MaterialFactoryEntity, bool>> condition);
        IEnumerable<Base_MaterialFactoryEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
