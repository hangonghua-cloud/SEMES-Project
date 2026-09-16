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
    public interface Base_MaterialFacetIService
    {
        IEnumerable<Base_MaterialFacetEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<Base_MaterialFacetEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, Base_MaterialFacetEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialFacetEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<Base_MaterialFacetEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        Base_MaterialFacetEntity GetEntity(string keyValue);
        Base_MaterialFacetEntity GetEntityByQuery(string QueryField);
        Base_MaterialFacetEntity Get_ExpressionEntity(Expression<Func<Base_MaterialFacetEntity, bool>> condition);
        IEnumerable<Base_MaterialFacetEntity> Get_ExpressionList(Expression<Func<Base_MaterialFacetEntity, bool>> condition);
        IEnumerable<Base_MaterialFacetEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
