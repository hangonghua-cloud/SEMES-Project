using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ALP.Util.WebControl;
using ALP.Application.Entity.QualityManage;

namespace ALP.Application.IService.QualityManage
{
    public interface QC_IQCQualityCheck_IService
    {
        IEnumerable<QC_IQCQualityCheckEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_IQCQualityCheckEntity> GetList(string checkType, out string msg);

        IEnumerable<QC_IQCQualityCheckEntity> GetList1(string checkType, out string msg);
        int SaveList(List<QC_IQCQualityCheckEntity> list);
        int SaveEntity(string keyValue, QC_IQCQualityCheckEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IQCQualityCheckEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_IQCQualityCheckEntity GetEntity(string keyValue);
        QC_IQCQualityCheckEntity GetEntityByQuery(string QueryField);
        QC_IQCQualityCheckEntity Get_ExpressionEntity(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition);
        IEnumerable<QC_IQCQualityCheckEntity> Get_ExpressionList(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition);
        IEnumerable<QC_IQCQualityCheckEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode,int index, out string returnNum, out string messageCode);
        bool GetTestMethod(out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
