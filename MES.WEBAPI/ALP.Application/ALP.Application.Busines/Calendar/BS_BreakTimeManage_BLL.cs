using ALP.Application.Entity.Calendar;
using ALP.Application.Service.Calendar;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.Calendar
{
    public class BS_BreakTimeManage_BLL
    {
        BS_BreakTimeManageService service = new BS_BreakTimeManageService(); 
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable GetPage_BS_BreakTimeManage(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData(pagination, queryJson);
            return dt;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map,out string msg)
        {
            var dt = service.Get_Data(map,out msg);
            return dt;
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public int Save_Entity(string keyValue, BS_BreakTimeManage entity,out string msg)
        {
            return service.SaveEntity(keyValue, entity,out msg);
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        public BS_BreakTimeManage GetEntity(string keyValue)
        {
            return service.GetEntity( keyValue);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="WorkOrderNO"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Delete_Entity(string Id, string userName,out string msg)
        {
            BS_BreakTimeManage entity = GetEntity(Id);
            entity.Modifier = userName;
            entity.ModifyDate = DateTime.Now;
            entity.IsEnable = false;
            int n = Save_Entity(Id, entity,out msg);
            if (n > 0)
                return true;
            else
                return false;
        }
    }
}
