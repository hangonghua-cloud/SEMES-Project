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
    public interface PL_PrdOrderReqMaterialsIService
    {
        DataTable GetWorkOrderReqMaterials(Pagination pagination, string queryJson);
        IEnumerable<PL_PrdOrderReqMaterialsEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PL_PrdOrderReqMaterialsEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_PrdOrderReqMaterialsEntity> entity_list, out string msg);
        void InsertDataTable(DataTable dt);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete(List<PL_PrdOrderReqMaterialsEntity> lstEntity);
        int Delete_SQL(string keyValue, out string msg);
        PL_PrdOrderReqMaterialsEntity GetEntity(string keyValue);
        PL_PrdOrderReqMaterialsEntity GetEntityByQuery(string QueryField);
        PL_PrdOrderReqMaterialsEntity Get_ExpressionEntity(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition);
        IEnumerable<PL_PrdOrderReqMaterialsEntity> Get_ExpressionList(Expression<Func<PL_PrdOrderReqMaterialsEntity, bool>> condition);
        IEnumerable<PL_PrdOrderReqMaterialsEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
    }
}
