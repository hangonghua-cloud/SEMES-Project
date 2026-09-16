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

namespace ALP.Application.Service.ProduceManage
{

    /// </summary>
    public interface PM_ReworkRecord_IService
    {

        IEnumerable<PM_ReworkRecordEntity> GetPageList(Pagination pagination, string queryJson);

        DataTable GetPageDataTableList(Pagination pagination, string queryJson);

        IEnumerable<PM_ReworkRecordEntity> GetList(string checkType, out string msg);
        int SaveEntity(string keyValue, PM_ReworkRecordEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_ReworkRecordEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");

        int RemoveForm(string keyValue, string UpdateByName = "");
        int RemoveForm(Expression<Func<PM_ReworkRecordEntity, bool>> condition);
        int Delete_SQL(string keyValue, out string msg);
        PM_ReworkRecordEntity GetEntity(string keyValue);

        PM_ReworkRecordEntity GetEntityByQuery(string QueryField);

        PM_ReworkRecordEntity Get_ExpressionEntity(Expression<Func<PM_ReworkRecordEntity, bool>> condition);

        IEnumerable<PM_ReworkRecordEntity> Get_ExpressionList(Expression<Func<PM_ReworkRecordEntity, bool>> condition);

        IEnumerable<PM_ReworkRecordEntity> GetList_TestOtherEntity(string checkType, out string msg);

        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);


        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);

        string GetList_export(string checkType, out string msg);


        #region 
        /// <summary>
        /// 返工任务查询
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        DataTable GetReworkRecord(string processCode);
        #endregion
    }
}
