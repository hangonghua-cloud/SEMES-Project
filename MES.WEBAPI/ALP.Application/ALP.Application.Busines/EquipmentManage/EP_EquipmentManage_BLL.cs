using ALP.Application.Entity.EquipmentManage;
using ALP.Application.Service.EquipmentManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.EquipmentManage
{
    public class EP_EquipmentManagee_BLL
    {

        EP_EquipmentManage_Service service = new EP_EquipmentManage_Service();
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EP_EquipmentManage> Get_PageData(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData(pagination, queryJson);
            return dt;
        }

        public IEnumerable<EP_EquipmentManage> Get_PageData1(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData1(pagination, queryJson);
            return dt;
        }


        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        public EP_EquipmentManage GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        public EP_EquipmentManage GetEntity(Expression<Func<EP_EquipmentManage, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, out string msg)
        {
            return service.RemoveForm(keyValue, out msg);
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public int SaveForm(string keyValue, EP_EquipmentManage entity, out string msg)
        {
            return service.SaveForm(keyValue, entity, out msg);
        }
    }
}
