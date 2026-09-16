using ALP.Application.Code.Model;
using ALP.Application.Entity.PlanManage;
using ALP.Application.IService.PlanManage;
using ALP.Application.Service.PlanManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.PlanManage
{
    public class PL_BOMItemsBLL
    {
        private PL_BOMItemsIService service = new PL_BOMItems_Service();
        

        public PL_BOMItemsEntity Get_ExpressionEntity(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PL_BOMItemsEntity> Get_ExpressionList(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public void RemoveForm(Expression<Func<PL_BOMItemsEntity, bool>> condition)
        {
            service.RemoveForm(condition);
        }
        public int Delete(List<PL_BOMItemsEntity> lstEntity)
        {
            return service.Delete(lstEntity);
        }
        public void Save_List(bool isUpdate, List<PL_BOMItemsEntity> list)
        {
            service.Save_List(isUpdate, list);
        }
        /// <summary>
        /// 流转卡报工使用（带库位）
        /// </summary>
        /// <param name="bomId"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public IEnumerable<PL_BOMItemsEntity> GetBomItemList(string bomId, string processCode)
        {
            return service.GetBomItemList(bomId, processCode);
        }
        public void InsertDataTable(DataTable dt)
        {
            service.InsertDataTable(dt);
        }
    }
}
