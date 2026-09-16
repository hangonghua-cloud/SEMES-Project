using ALP.Application.Entity.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.PlanManage
{
    public interface PL_FirstInspectionConfirmIService
    {
        void InsertList(List<PL_FirstInspectionConfirmEntity> list);
        void RemoveForm(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition);
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);
        IEnumerable<PL_FirstInspectionConfirmEntity> GetList(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition);
        PL_FirstInspectionConfirmEntity GetEntity(Expression<Func<PL_FirstInspectionConfirmEntity, bool>> condition);
        void SaveEntity(string keyvalue, PL_FirstInspectionConfirmEntity entity);
    }
}
