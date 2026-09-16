using ALP.Application.Entity.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.SystemManage
{
    public class Sys_DataSegregateLabel_BLL
    {

        Sys_DataSegregateLabel_Service service = new Sys_DataSegregateLabel_Service();
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns> 
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData(pagination, queryJson);
            return dt;
        }
        /// <summary>
        /// 查询不在指定组标签的标签-分页查询
        /// </summary>
        /// <returns></returns> 
        public DataTable Get_PageData_notGroup(Pagination pagination, string queryJson)
        {
            var dt = service.Get_PageData_notGroup(pagination, queryJson);
            return dt;
        }
        
        public DataTable Sum_DataSegregateGroupToPerson(string PersonCode)
        {
            var dt = service.Sum_DataSegregateGroupToPerson(PersonCode);
            return dt;
        }
        public int GetDataSegregateLabel(string PersonCode)
        {
            object personlabel = CacheFactory.Cache().GetCache<string>(PersonCode);
            if (personlabel == null)
            {
                CacheFactory.Cache().WriteCache(personlabel, PersonCode, DateTime.Now.AddMinutes(5));
                return service.GetDataSegregateLabel(PersonCode);
            }
            return Int32.Parse(personlabel.ToString());
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public int Save_Entity(string keyValue, Sys_DataSegregateLabel entity, out string msg)
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
        public Sys_DataSegregateLabel GetEntity(string keyValue)
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
            Sys_DataSegregateLabel entity = GetEntity(WorkOrderNO);
            entity.ModifyUser = userName;
            entity.ModifyDate = DateTime.Now;
            entity.EnabledMark = false;
            int n = Save_Entity(WorkOrderNO, entity, out msg);
            if (n > 0)
                return true;
            else
                return false;
        }
    }
}
