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
    public interface PM_OwnProductTransferIService
    {
        IEnumerable<PM_OwnProductTransferEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_OwnProductTransferEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_OwnProductTransferEntity> GetList(Expression<Func<PM_OwnProductTransferEntity, bool>> condition);
        int SaveEntity(string keyValue, PM_OwnProductTransferEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_OwnProductTransferEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_OwnProductTransferEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_OwnProductTransferEntity GetEntity(string keyValue);
        PM_OwnProductTransferEntity GetEntityByQuery(string QueryField);
        /// <summary>
        /// 生产管理-扫一扫
        /// </summary>
        /// <param name="exeWorkOrder">序列号</param>
        /// <returns></returns>
        List<dynamic> CodeBarScan(string exeWorkOrder, string batchNo);
        PM_OwnProductTransferEntity Get_ExpressionEntity(Expression<Func<PM_OwnProductTransferEntity, bool>> condition);
        IEnumerable<PM_OwnProductTransferEntity> Get_ExpressionList(Expression<Func<PM_OwnProductTransferEntity, bool>> condition);
        IEnumerable<PM_OwnProductTransferEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, int index, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
