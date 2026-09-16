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
    public interface Base_MaterialGroupBindMaterialIService
    {
        IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList();
        IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition);
        Base_MaterialGroupBindMaterialEntity GetEntity(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition);
        void RemoveForm(string keyvalue);
        void RemoveForm(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition);
        int Insert(Base_MaterialGroupBindMaterialEntity entity);
        int Update(Base_MaterialGroupBindMaterialEntity entity);
        DataTable GetDataTableWithPage(Pagination pagination, string queryJson);
    }
}
