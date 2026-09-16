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
    public interface Base_ProcessAttrIService
    {
        DataTable GetListWithPage(Pagination pagination, string queryJson);

        void Insert(Base_ProcessAttrEntity entity);

        void Update(Base_ProcessAttrEntity entity);

        void Remove(Expression<Func<Base_ProcessAttrEntity, bool>> condition);

        Base_ProcessAttrEntity GetEntity(Expression<Func<Base_ProcessAttrEntity, bool>> condition);

        IEnumerable<Base_ProcessAttrEntity> GetList(Expression<Func<Base_ProcessAttrEntity, bool>> condition);
  
    }
}
