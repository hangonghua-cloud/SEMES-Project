using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    public interface Base_KeyParameterItemIService
    {
        IEnumerable<Base_KeyParameterItemEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetKeyParameterItemList(string enCode,string itemCode,out string msg);
        IEnumerable<Base_KeyParameterItemEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, Base_KeyParameterItemEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_KeyParameterItemEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<Base_KeyParameterItemEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        Base_KeyParameterItemEntity GetEntity(string keyValue);
        Base_KeyParameterItemEntity GetEntityByQuery(string QueryField);
        Base_KeyParameterItemEntity Get_ExpressionEntity(Expression<Func<Base_KeyParameterItemEntity, bool>> condition);
        IEnumerable<Base_KeyParameterItemEntity> Get_ExpressionList(Expression<Func<Base_KeyParameterItemEntity, bool>> condition);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
