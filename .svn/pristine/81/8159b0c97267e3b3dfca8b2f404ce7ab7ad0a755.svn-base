using ALP.Application.Entity.QualityManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.QualityManage
{
    public interface QC_TestMethodMaterialIService
    {
        IEnumerable<QC_TestMethodMaterialEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<QC_TestMethodMaterialEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, QC_TestMethodMaterialEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_TestMethodMaterialEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<QC_TestMethodMaterialEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        QC_TestMethodMaterialEntity GetEntity(string keyValue);
        QC_TestMethodMaterialEntity GetEntityByQuery(string QueryField);
        QC_TestMethodMaterialEntity Get_ExpressionEntity(Expression<Func<QC_TestMethodMaterialEntity, bool>> condition);
        IEnumerable<QC_TestMethodMaterialEntity> Get_ExpressionList(Expression<Func<QC_TestMethodMaterialEntity, bool>> condition);
        IEnumerable<QC_TestMethodMaterialEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
