using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ALP.Application.IService.ProduceManage
{
    public interface PM_PackingPrintMarkIService
    {
        IEnumerable<PM_PackingPrintMarkEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetList(string queryJson);
        IEnumerable<PM_PackingPrintMarkEntity> GetList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_PackingPrintMarkEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PackingPrintMarkEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_PackingPrintMarkEntity GetEntity(string keyValue);
        PM_PackingPrintMarkEntity GetEntityByQuery(string QueryField);
        PM_PackingPrintMarkEntity Get_ExpressionEntity(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition);
        IEnumerable<PM_PackingPrintMarkEntity> Get_ExpressionList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition);
        IEnumerable<PM_PackingPrintMarkEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode,int index, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        List<dynamic> GetOrderByTransferCode(string PackTransferCode);
        /// <summary>
        /// 获取唛头信息
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        List<dynamic> GetMarkListByWorkOrder(string workOrder);
    }
}
