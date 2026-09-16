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
    public interface Base_KeyParameterIService
    {
        IEnumerable<Base_KeyParameterEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<Base_KeyParameterEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, Base_KeyParameterEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_KeyParameterEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        Base_KeyParameterEntity GetEntity(string keyValue);
        Base_KeyParameterEntity GetEntityByQuery(string QueryField);
        Base_KeyParameterEntity Get_ExpressionEntity(Expression<Func<Base_KeyParameterEntity, bool>> condition);
        IEnumerable<Base_KeyParameterEntity> Get_ExpressionList(Expression<Func<Base_KeyParameterEntity, bool>> condition);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
