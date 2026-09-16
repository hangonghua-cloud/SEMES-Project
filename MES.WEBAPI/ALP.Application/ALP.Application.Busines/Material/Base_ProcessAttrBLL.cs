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
    public class Base_ProcessAttrBLL
    {
        private Base_ProcessAttrIService service = new Base_ProcessAttr_Service();

        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            return service.GetListWithPage(pagination, queryJson);
        }

        public void SaveForm(string keyValue, Base_ProcessAttrEntity entity)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                service.Insert(entity);
            }
            else
            {
                service.Update(entity);
            }
        }

        public void Remove(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            service.Remove(condition);
        }

        public Base_ProcessAttrEntity GetEntity(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }

        public IEnumerable<Base_ProcessAttrEntity> GetList(Expression<Func<Base_ProcessAttrEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
    }
}
