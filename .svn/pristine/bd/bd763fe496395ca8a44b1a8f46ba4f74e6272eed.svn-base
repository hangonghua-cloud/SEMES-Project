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
    public interface MM_ProductDispatchBill_IService
    {
        IEnumerable<MM_ProductDispatchBillEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_ProductDispatchBillEntity> GetList(string checkType, out string msg);
        IEnumerable<MM_ProductDispatchBillEntity> GetList(Expression<Func<MM_ProductDispatchBillEntity, bool>> condition);
        int SaveEntity(string keyValue, MM_ProductDispatchBillEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductDispatchBillEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        MM_ProductDispatchBillEntity GetEntity(string keyValue);
        MM_ProductDispatchBillEntity GetEntityByQuery(string QueryField);
        MM_ProductDispatchBillEntity Get_ExpressionEntity(Expression<Func<MM_ProductDispatchBillEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchBillEntity> Get_ExpressionList(Expression<Func<MM_ProductDispatchBillEntity, bool>> condition);
        IEnumerable<MM_ProductDispatchBillEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode,int index, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
       
    }
}
