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
    public interface MM_ReceiptNoticeIService
    {
        IEnumerable<MM_ReceiptNoticeEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<MM_ReceiptNoticeEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, MM_ReceiptNoticeEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ReceiptNoticeEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        MM_ReceiptNoticeEntity GetEntity(string keyValue);
        MM_ReceiptNoticeEntity GetEntityByQuery(string QueryField);
        MM_ReceiptNoticeEntity Get_ExpressionEntity(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition);
        IEnumerable<MM_ReceiptNoticeEntity> Get_ExpressionList(Expression<Func<MM_ReceiptNoticeEntity, bool>> condition);
        IEnumerable<MM_ReceiptNoticeEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
