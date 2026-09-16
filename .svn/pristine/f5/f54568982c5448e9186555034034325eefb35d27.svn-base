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
    public class BS_ShiftManage_BLL
    {
        BS_ShiftManageService service = new BS_ShiftManageService();
        /// <summary>
        /// 分布查询
        /// </summary>
        /// <returns></returns> 
        public DataTable Get_PageData(Pagination pagination, string queryJson) 
        { 
            var dt = service.Get_PageData(pagination, queryJson); 
            return dt;
        }
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_Data(map, out msg);
            return dt;
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public int Save_Entity(string keyValue, BS_ShiftManage entity, out string msg)
        {
            int result = 0;
            try
            {
                result = service.SaveEntity(keyValue, entity, out msg);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        public BS_ShiftManage GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="WorkOrderNO"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Delete_Entity(string WorkOrderNO, string userName, out string msg)
        {
            BS_ShiftManage entity = GetEntity(WorkOrderNO);
            entity.Modifier = userName;
            entity.ModifyDate = DateTime.Now;
            entity.IsEnable = false;
            int n = Save_Entity(WorkOrderNO, entity, out msg);
            if (n > 0)
                return true;
            else
                return false;
        }
    }
}
