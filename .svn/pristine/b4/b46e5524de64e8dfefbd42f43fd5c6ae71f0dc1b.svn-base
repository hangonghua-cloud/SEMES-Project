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
    public interface MM_ProductDispatchDetail_IService
    {
        IEnumerable<MM_ProductDispatchDetailEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_ProductDispatchDetailEntity> GetList(string checkType, out string msg);
        IEnumerable<MM_ProductDispatchDetailEntity> GetList(Expression<Func<MM_ProductDispatchDetailEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_ProductDispatchDetailEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductDispatchDetailEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        MM_ProductDispatchDetailEntity GetEntity(string keyValue);
        MM_ProductDispatchDetailEntity GetEntityByQuery(string QueryField);
        MM_ProductDispatchDetailEntity Get_ExpressionEntity(Expression<Func<MM_ProductDispatchDetailEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchDetailEntity> Get_ExpressionList(Expression<Func<MM_ProductDispatchDetailEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchDetailEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
       
    }
}
