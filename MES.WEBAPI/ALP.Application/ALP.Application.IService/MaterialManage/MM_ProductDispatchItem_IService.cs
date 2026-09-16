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
    public interface MM_ProductDispatchItem_IService
    {
        IEnumerable<MM_ProductDispatchItemEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_ProductDispatchItemEntity> GetList(string checkType, out string msg);
        IEnumerable<MM_ProductDispatchItemEntity> GetList(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_ProductDispatchItemEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductDispatchItemEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        MM_ProductDispatchItemEntity GetEntity(string keyValue);
        MM_ProductDispatchItemEntity GetEntityByQuery(string QueryField);
        MM_ProductDispatchItemEntity Get_ExpressionEntity(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchItemEntity> Get_ExpressionList(Expression<Func<MM_ProductDispatchItemEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchItemEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
       
    }
}
