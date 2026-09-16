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
    public class Base_MaterialGroupBLL
    {
        private Base_MaterialGroupIService service = new Base_MaterialGroup_Service();
      
        public IEnumerable<Base_MaterialGroupEntity> GetList()
        {
            return service.GetList();
        }
        public IEnumerable<Base_MaterialGroupEntity> GetList(Expression<Func<Base_MaterialGroupEntity, bool>> condition)
        {
            return service.GetList(condition);
        }

        public IEnumerable<Base_MaterialGroupEntity> Get_PageData(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData(pagination, queryJson);
            return dt;
        }

        public IEnumerable<Base_MaterialGroupEntity> Get_PageData1(string queryJson)
        {
            var dt = service.Get_PageData1(queryJson);
            return dt;
        }



        public Base_MaterialGroupEntity GetEntity(Expression<Func<Base_MaterialGroupEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public void RemoveForm(string keyValue)
        {
            service.RemoveForm(keyValue);
        }

        public int SaveForm(string keyValue, Base_MaterialGroupEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {

                if (service.GetEntity(t => t.GroupCode == entity.GroupCode || t.GroupName == entity.GroupName) != null) return 2;

                entity.Create();
                service.Insert(entity);
            }
            else
            {
                if (service.GetEntity(t => t.Id != keyValue && t.GroupName == entity.GroupName) != null) return 2;
                entity.Id = keyValue;
                service.Update(entity);
            }
            return 1;
        }
    }
}
