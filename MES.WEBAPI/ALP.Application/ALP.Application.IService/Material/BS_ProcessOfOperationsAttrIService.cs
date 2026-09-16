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
    public interface BS_ProcessOfOperationsAttrIService
    {
        DataTable GetListWithPage(Pagination pagination, string queryJson);

        void Insert(BS_ProcessOfOperationsAttrEntity entity);

        void Update(BS_ProcessOfOperationsAttrEntity entity);

        void Remove(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition);

        BS_ProcessOfOperationsAttrEntity GetEntity(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition);

        IEnumerable<BS_ProcessOfOperationsAttrEntity> GetList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition);
        IEnumerable<BS_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition);
    }
}
