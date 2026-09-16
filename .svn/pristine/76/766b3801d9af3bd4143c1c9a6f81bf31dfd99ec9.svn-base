using ALP.Application.Entity.QualityManage;
using ALP.Application.IService.QualityManage;
using ALP.Application.Service.QualityManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.QualityManage
{
    public class QC_OQCQualityCheckBLL
    {
        private QC_OQCQualityCheckIService service = new QC_OQCQualityCheck_Service();

        public IEnumerable<QC_OQCQualityCheckEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<QC_OQCQualityCheckEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, QC_OQCQualityCheckEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_OQCQualityCheckEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }
        public int RemoveForm(Expression<Func<QC_OQCQualityCheckEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public QC_OQCQualityCheckEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public QC_OQCQualityCheckEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public QC_OQCQualityCheckEntity Get_ExpressionEntity(Expression<Func<QC_OQCQualityCheckEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<QC_OQCQualityCheckEntity> Get_ExpressionList(Expression<Func<QC_OQCQualityCheckEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<QC_OQCQualityCheckEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
        }
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
        public int SaveOQCQualityChecInspectNo(string inspectNo, string userCode, string idStr)
        {
            return service.SaveOQCQualityChecInspectNo(inspectNo, userCode, idStr);
        }
    }
}
