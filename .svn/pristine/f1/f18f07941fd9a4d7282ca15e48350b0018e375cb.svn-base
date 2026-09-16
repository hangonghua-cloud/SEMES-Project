using ALP.Application.Entity.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.PlanManage
{
    public interface PL_ExeWorkOrderWearingLayerIService
    {
        IEnumerable<PL_ExeWorkOrderWearingLayerEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        DataTable GetOrderListWithPage(Pagination pagination, string queryJson);
        IEnumerable<PL_ExeWorkOrderWearingLayerEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_ExeWorkOrderWearingLayerEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_ExeWorkOrderWearingLayerEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        PL_ExeWorkOrderWearingLayerEntity GetEntity(string keyValue);
        PL_ExeWorkOrderWearingLayerEntity GetEntityByQuery(string QueryField);
        PL_ExeWorkOrderWearingLayerEntity Get_ExpressionEntity(Expression<Func<PL_ExeWorkOrderWearingLayerEntity, bool>> condition);
        IEnumerable<PL_ExeWorkOrderWearingLayerEntity> Get_ExpressionList(Expression<Func<PL_ExeWorkOrderWearingLayerEntity, bool>> condition);
        IEnumerable<PL_ExeWorkOrderWearingLayerEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
