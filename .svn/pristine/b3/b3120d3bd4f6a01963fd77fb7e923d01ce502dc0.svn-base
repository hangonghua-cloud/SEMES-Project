using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.WebControl;

using System.IO;
using ALP.Application.Code.Model;

namespace ALP.Application.Service.ProduceManage
{

    /// </summary>
    public interface PM_ReworkRecord_Detail_IService
    {

        IEnumerable<PM_ReworkRecord_DetailEntity> GetPageList(Pagination pagination, string queryJson);

        DataTable GetPageDataTableList(Pagination pagination, string queryJson,string reworkProductType);

        IEnumerable<PM_ReworkRecord_DetailEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_ReworkRecord_DetailEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ReworkRecord_DetailEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");

        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_ReworkRecord_DetailEntity GetEntity(string keyValue);

        PM_ReworkRecord_DetailEntity GetEntityByQuery(string QueryField);

        PM_ReworkRecord_DetailEntity Get_ExpressionEntity(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition);

        IEnumerable<PM_ReworkRecord_DetailEntity> Get_ExpressionList(Expression<Func<PM_ReworkRecord_DetailEntity, bool>> condition);

        IEnumerable<PM_ReworkRecord_DetailEntity> GetList_TestOtherEntity(string checkType, out string msg);

        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);


        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);

        string GetList_export(string checkType, out string msg);
        List<PM_ReworkRecordModel> GetReworkRecordDetail(string reworkId, string reworkProductType);

    }
}
