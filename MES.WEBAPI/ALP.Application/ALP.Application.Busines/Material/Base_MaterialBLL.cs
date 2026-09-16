using ALP.Application.Entity.Material;
using ALP.Application.IService.Material;
using ALP.Application.Service.Material;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.Material
{
    public class Base_MaterialBLL
    {
        private Base_MaterialIService service = new Base_Material_Service();


        public IEnumerable<Base_MaterialEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }
        /// <summary>
        /// 功能描述: 查询规格(DataTable)
        /// 创　　建: Dragon
        /// 创建日期: 2022-12-24
        /// 任务编号: 查询规格
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetSpecPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetSpecPageDataTableList(pagination, queryJson);
        }
        public IEnumerable<Base_MaterialEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }
        public IEnumerable<Base_MaterialEntity> GetList(Expression<Func<Base_MaterialEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public int SaveEntity(string keyValue, Base_MaterialEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialEntity> entity_list, out string msg)
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
        public Base_MaterialEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public Base_MaterialEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        public Base_MaterialEntity Get_ExpressionEntity(Expression<Func<Base_MaterialEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<Base_MaterialEntity> Get_ExpressionList(Expression<Func<Base_MaterialEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public IEnumerable<Base_MaterialEntity> GetList_TestOtherEntity(string checkType, out string msg)
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
        public DataTable GetDimMaterialSpec(Base_MaterialEntity entity)
        {
            return service.GetDimMaterialSpec(entity);
        }
        public List<dynamic> GetBaseMaterialList(string queryJson)
        {
            return service.GetBaseMaterialList(queryJson);
        }
    }
}
