using ALP.Application.Entity.ProduceManage;
using ALP.Application.IService.ProduceManage;
using ALP.Application.Service.ProduceManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ProduceManage
{
    public class PM_MaterialBatchConsumeRecordBLL
    {
        private PM_MaterialBatchConsumeRecord_IService service = new PM_MaterialBatchConsumeRecord_Service();

        public IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public int SaveEntity(string keyValue, PM_MaterialBatchConsumeRecordEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_MaterialBatchConsumeRecordEntity> entity_list, out string msg)
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
        public int RemoveForm(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PM_MaterialBatchConsumeRecordEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PM_MaterialBatchConsumeRecordEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PM_MaterialBatchConsumeRecordEntity Get_ExpressionEntity(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PM_MaterialBatchConsumeRecordEntity> Get_ExpressionList(Expression<Func<PM_MaterialBatchConsumeRecordEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PM_MaterialBatchConsumeRecordEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
    }
}
