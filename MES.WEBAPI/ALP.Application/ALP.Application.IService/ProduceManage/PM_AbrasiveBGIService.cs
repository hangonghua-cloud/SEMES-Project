using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.WebControl;

using System.IO;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_AbrasiveBGIService
    {
        IEnumerable<PM_AbrasiveBGEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_AbrasiveBGEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_AbrasiveBGEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_AbrasiveBGEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_AbrasiveBGEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_AbrasiveBGEntity GetEntity(string keyValue);
        PM_AbrasiveBGEntity GetEntityByQuery(string QueryField);
        PM_AbrasiveBGEntity Get_ExpressionEntity(Expression<Func<PM_AbrasiveBGEntity, bool>> condition);
        IEnumerable<PM_AbrasiveBGEntity> Get_ExpressionList(Expression<Func<PM_AbrasiveBGEntity, bool>> condition);
        IEnumerable<PM_AbrasiveBGEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
