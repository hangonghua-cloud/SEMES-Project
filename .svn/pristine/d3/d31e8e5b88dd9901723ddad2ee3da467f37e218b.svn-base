using ALP.Application.Entity.Material;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.Material
{
    public interface Base_MaterialIService
    {
        IEnumerable<Base_MaterialEntity> GetPageList(Pagination pagination, string queryJson);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        /// <summary>
        /// 功能描述: 查询规格(DataTable)
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-24
        /// 任务编号: 查询规格
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetSpecPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<Base_MaterialEntity> GetList(string checkType, out string msg);
        IEnumerable<Base_MaterialEntity> GetList(Expression<Func<Base_MaterialEntity, bool>> condition);
        int SaveEntity(string keyValue, Base_MaterialEntity entity, out string msg);
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialEntity> entity_list, out string msg);
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");
        int RemoveForm(string keyValue, string UpdateByName = "");
        int Delete_SQL(string keyValue, out string msg);
        Base_MaterialEntity GetEntity(string keyValue);
        Base_MaterialEntity GetEntityByQuery(string QueryField);
        Base_MaterialEntity Get_ExpressionEntity(Expression<Func<Base_MaterialEntity, bool>> condition);
        IEnumerable<Base_MaterialEntity> Get_ExpressionList(Expression<Func<Base_MaterialEntity, bool>> condition);
        IEnumerable<Base_MaterialEntity> GetList_TestOtherEntity(string checkType, out string msg);
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);
        bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);
        string GetList_export(string checkType, out string msg);
        DataTable GetDimMaterialSpec(Base_MaterialEntity entity);
        List<dynamic> GetBaseMaterialList(string queryJson);
    }
}
