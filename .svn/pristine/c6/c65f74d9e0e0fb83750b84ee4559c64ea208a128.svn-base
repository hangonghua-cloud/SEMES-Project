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
    public interface Base_ProcessAttrItemIService
    {
        DataTable GetListWithPage(Pagination pagination, string queryJson);

        void Insert(Base_ProcessAttrItemEntity entity);

        void Update(Base_ProcessAttrItemEntity entity);

        void Remove(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition);

        Base_ProcessAttrItemEntity GetEntity(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition);

        IEnumerable<Base_ProcessAttrItemEntity> GetList(Expression<Func<Base_ProcessAttrItemEntity, bool>> condition);
    }
}
