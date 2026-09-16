using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    public interface Base_ReportGroupIService
    {
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        Base_ReportGroupEntity GetEntity(Expression<Func<Base_ReportGroupEntity, bool>> condition);
        IEnumerable<Base_ReportGroupEntity> GetList(Expression<Func<Base_ReportGroupEntity, bool>> condition);
        void SaveForm(string keyValue, Base_ReportGroupEntity entity);
        int RemoveForm(Expression<Func<Base_ReportGroupEntity, bool>> condition);
    }
}
