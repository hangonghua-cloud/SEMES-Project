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
    public interface Base_MaterialGroupIService
    {
        IEnumerable<Base_MaterialGroupEntity> GetList();
        IEnumerable<Base_MaterialGroupEntity> GetList(Expression<Func<Base_MaterialGroupEntity, bool>> condition);
        Base_MaterialGroupEntity GetEntity(Expression<Func<Base_MaterialGroupEntity, bool>> condition);
        void RemoveForm(string keyvalue);
        int Insert(Base_MaterialGroupEntity entity);
        int Update(Base_MaterialGroupEntity entity);

        IEnumerable<Base_MaterialGroupEntity> Get_PageData(Pagination pagination, string queryJson);

        IEnumerable<Base_MaterialGroupEntity> Get_PageData1(string queryJson);

    }
}
