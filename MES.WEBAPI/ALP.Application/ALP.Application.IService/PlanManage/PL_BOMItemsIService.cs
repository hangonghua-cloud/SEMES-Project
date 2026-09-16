using ALP.Application.Code.Model;
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
    public interface PL_BOMItemsIService
    {

        PL_BOMItemsEntity Get_ExpressionEntity(Expression<Func<PL_BOMItemsEntity, bool>> condition);
        IEnumerable<PL_BOMItemsEntity> Get_ExpressionList(Expression<Func<PL_BOMItemsEntity, bool>> condition);
        void RemoveForm(Expression<Func<PL_BOMItemsEntity, bool>> condition);
        int Delete(List<PL_BOMItemsEntity> lstEntity);
        void Save_List(bool isUpdate, List<PL_BOMItemsEntity> list);
        /// <summary>
        /// 流转卡报工使用（带库位）
        /// </summary>
        /// <param name="bomId"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        IEnumerable<PL_BOMItemsEntity> GetBomItemList(string bomId, string processCode);
        void InsertDataTable(DataTable dt);
    }
}
