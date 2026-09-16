using ALP.Application.Entity.Calendar;
using ALP.Application.Service.Calendar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.Calendar
{
    public class BS_CalendarManage_BLL
    {
        BS_CalendarManageService service = new BS_CalendarManageService();
        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_Data(map, out msg);
            return dt;
        }
        /// <summary>
        /// 查询日历和班次信息
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data_Both(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_Data_Both(map, out msg);
            return dt;
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public int Save_Entity(string keyValue, BS_CalendarManage entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        public BS_CalendarManage GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取保存表单SQL
        /// </summary>
        /// <param name="entity"></param> 
        /// <param name="msg"></param> 
        public string GetSaveSql(BS_CalendarManage entity, out string msg)
        {
            return service.GetSaveSql(entity, out msg);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="WorkOrderNO"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Update_state(string id, string userName, string state, out string msg)
        { 
            BS_CalendarManage entity = GetEntity(id);
            entity.Modifier = userName;
            entity.ModifyDate = DateTime.Now;
            entity.IsHoliday = Convert.ToBoolean(state);
            if (entity.IsHoliday)
            {
                entity.Title = "休息日";
            }
            else
            {
                entity.Title = "工作日";
            }
            int n = Save_Entity(id, entity, out msg);
            if (n > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="WorkOrderNO"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Update_state(BS_CalendarManage entity, out string msg)
        { 
            if (entity.IsHoliday)
            {
                entity.Title = "休息日";
            }
            else
            {
                entity.Title = "工作日";
            }
            int n = service.UpdateState(entity, out msg);
            if (n > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="WorkOrderNO"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool Delete_Entity(string WorkOrderNO, string userName, out string msg)
        {

            BS_CalendarManage entity = GetEntity(WorkOrderNO);
            entity.Modifier = userName;
            entity.ModifyDate = DateTime.Now;
            //entity.IsEnable = false;
            int n = Save_Entity(WorkOrderNO, entity, out msg);
            if (n > 0)
                return true;
            else
                return false;
        }

    }
}
