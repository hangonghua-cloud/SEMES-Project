using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.BaseManage
{
    public class Base_ReportGroupBLL
    {
        private Base_ReportGroupIService _service = new Base_ReportGroupService();
       public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return _service.GetPageDataTableList(pagination, queryJson);
        }
        public Base_ReportGroupEntity GetEntity(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return _service.GetEntity(condition);
        }
        public IEnumerable<Base_ReportGroupEntity> GetList(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return _service.GetList(condition);
        }
        public void SaveForm(string keyValue, Base_ReportGroupEntity entity)
        {
            _service.SaveForm(keyValue,entity);
        }
        public int RemoveForm(Expression<Func<Base_ReportGroupEntity, bool>> condition)
        {
            return _service.RemoveForm(condition);
        }
    }
}
