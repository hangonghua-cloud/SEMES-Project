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
    public class PM_PackingPrintMarkBLL
    {
        private PM_PackingPrintMarkIService service = new PM_PackingPrintMark_Service();

        public IEnumerable<PM_PackingPrintMarkEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        public DataTable GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        public IEnumerable<PM_PackingPrintMarkEntity> GetList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public int SaveEntity(string keyValue, PM_PackingPrintMarkEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_PackingPrintMarkEntity> entity_list, out string msg)
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
        public int RemoveForm(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public PM_PackingPrintMarkEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public PM_PackingPrintMarkEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public PM_PackingPrintMarkEntity Get_ExpressionEntity(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PM_PackingPrintMarkEntity> Get_ExpressionList(Expression<Func<PM_PackingPrintMarkEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<PM_PackingPrintMarkEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
        }
        public bool GetSerialNO(string SeqCode,int index, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode,index, out returnNum, out messageCode);
        }
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
        public List<dynamic> GetOrderByTransferCode(string PackTransferCode)
        {
            return service.GetOrderByTransferCode(PackTransferCode);
        }
        /// <summary>
        /// 获取唛头信息
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetMarkListByWorkOrder(string workOrder)
        {
            return service.GetMarkListByWorkOrder(workOrder);
        }
    }
}
