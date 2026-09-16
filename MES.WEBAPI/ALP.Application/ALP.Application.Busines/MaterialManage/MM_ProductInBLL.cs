using ALP.Application.Entity.MaterialManage;
using ALP.Application.IService.MaterialManage;
using ALP.Application.Service.MaterialManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.MaterialManage
{
    public class MM_ProductInBLL
    {
        private MM_ProductIn_IService service = new MM_ProductIn_Service();

        public IEnumerable<MM_ProductInEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<MM_ProductInEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public int SaveEntity(string keyValue, MM_ProductInEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<MM_ProductInEntity> entity_list, out string msg)
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
       
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public MM_ProductInEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public MM_ProductInEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public MM_ProductInEntity Get_ExpressionEntity(Expression<Func<MM_ProductInEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<MM_ProductInEntity> Get_ExpressionList(Expression<Func<MM_ProductInEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<MM_ProductInEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
