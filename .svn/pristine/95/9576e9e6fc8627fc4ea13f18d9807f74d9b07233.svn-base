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
    public class Base_ProcessAttrItemBLL
    {
        private Base_ProcessAttrItemIService service = new Base_ProcessAttrItem_Service();

        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            return service.GetListWithPage(pagination, queryJson);
        }

        public void SaveForm(string keyValue, Base_ProcessAttrItemEntity entity)
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

        public void Remove(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            service.Remove(condition);
        }

        public Base_ProcessAttrItemEntity GetEntity(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }

        public IEnumerable<Base_ProcessAttrItemEntity> GetList(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
    }
}
