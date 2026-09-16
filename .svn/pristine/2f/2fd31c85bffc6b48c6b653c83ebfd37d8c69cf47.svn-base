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
    public class Base_MaterialGroupBindMaterialBLL
    {
        private Base_MaterialGroupBindMaterialIService service = new Base_MaterialGroupBindMaterial_Service();

 
        public IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList()
        {
            return service.GetList();
        }
        public IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            return service.GetList(condition);
        }

        public Base_MaterialGroupBindMaterialEntity GetEntity(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public void RemoveForm(string keyValue)
        {
            service.RemoveForm(keyValue);
        }
        public void RemoveForm(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }
        public int SaveForm(string keyValue, Base_MaterialGroupBindMaterialEntity entity)
        {

            return service.Insert(entity);
            //if (string.IsNullOrEmpty(keyValue))
            //{

            //    if (service.GetEntity(t => t.GroupCode == entity.GroupCode && t.MaterialCode == entity.MaterialCode) != null) return 2;
            //    entity.Create();
            //    service.Insert(entity);
            //}
            //else
            //{
            //    if (service.GetEntity(t => t.Id != keyValue && t.GroupCode == entity.GroupCode && t.MaterialCode == entity.MaterialCode) != null) return 2;
            //    entity.Id = keyValue;
            //    service.Update(entity);
            //}
            //return 1;
        }
        public DataTable GetDataTableWithPage(Pagination pagination, string queryJson)
        {
            return service.GetDataTableWithPage(pagination, queryJson);
        }

    }
}
