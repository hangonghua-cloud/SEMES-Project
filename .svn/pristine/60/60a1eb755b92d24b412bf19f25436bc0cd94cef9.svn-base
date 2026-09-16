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
    public class BS_ProcessOfOperationsAttrBLL
    {
        private BS_ProcessOfOperationsAttrIService service = new BS_ProcessOfOperationsAttr_Service();

        public DataTable GetListWithPage(Pagination pagination, string queryJson)
        {
            return service.GetListWithPage(pagination, queryJson);
        }

        public void SaveForm(string keyValue, BS_ProcessOfOperationsAttrEntity entity)
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
        public void SaveList(string keyValue,string userCode,List<BS_ProcessOfOperationsAttrEntity> list)
        {
            var time = DateTime.Now;
            service.Remove(t => t.OperationsId == keyValue);
            list.ForEach(t=>{
                t.Create();
                t.Creator = userCode;
                t.CreateTime = time;
                service.Insert(t);
            });
        }

        public void Remove(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            service.Remove(condition);
        }

        public BS_ProcessOfOperationsAttrEntity GetEntity(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }

        public IEnumerable<BS_ProcessOfOperationsAttrEntity> GetList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public IEnumerable<BS_ProcessOfOperationsAttrEntity> Get_ExpressionList(Expression<Func<BS_ProcessOfOperationsAttrEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
    }
}
