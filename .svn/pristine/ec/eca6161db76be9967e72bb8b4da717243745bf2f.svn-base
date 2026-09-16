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
    public interface PM_FormulaRecordDetailIService
    {
        IEnumerable<PM_FormulaRecordDetailEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PM_FormulaRecordDetailEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_FormulaRecordDetailEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_FormulaRecordDetailEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_FormulaRecordDetailEntity GetEntity(string keyValue);
        PM_FormulaRecordDetailEntity GetEntityByQuery(string QueryField);
        PM_FormulaRecordDetailEntity Get_ExpressionEntity(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition);
        IEnumerable<PM_FormulaRecordDetailEntity> Get_ExpressionList(Expression<Func<PM_FormulaRecordDetailEntity, bool>> condition);
        IEnumerable<PM_FormulaRecordDetailEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
