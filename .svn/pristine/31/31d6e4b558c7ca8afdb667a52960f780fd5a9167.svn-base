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
    public interface QC_IQCQualityCheckItem_IService
    {
        IEnumerable<QC_IQCQualityCheckItemEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_IQCQualityCheckItemEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, QC_IQCQualityCheckItemEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IQCQualityCheckItemEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_IQCQualityCheckItemEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_IQCQualityCheckItemEntity GetEntity(string keyValue);
        QC_IQCQualityCheckItemEntity GetEntityByQuery(string QueryField);
        QC_IQCQualityCheckItemEntity Get_ExpressionEntity(Expression<Func<QC_IQCQualityCheckItemEntity, bool>> condition);
        IEnumerable<QC_IQCQualityCheckItemEntity> Get_ExpressionList(Expression<Func<QC_IQCQualityCheckItemEntity, bool>> condition);
        IEnumerable<QC_IQCQualityCheckItemEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
