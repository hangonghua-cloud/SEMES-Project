using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Application.Service.SystemManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;

namespace ALP.Application.Busines.SystemManage
{
    public class Sys_DepartmentBLL
    {
        private Sys_DepartmentService service = new Sys_DepartmentService();
        /// <summary>
        /// 缓存key
        /// </summary>
        private string cacheKey = "departmentCache";

        #region 获取数据
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {

            var data = service.Get_PageData(pagination, queryJson); 
            return data;
        }
        public DataTable Get_PageData_Control(string name="")
        {
            var data = service.Get_PageData_Control(name);
            return data;
        } 
        /// <summary>
        /// 部门实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sys_DepartmentEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        public DataTable GetInFactoryCode()
        {
            var data = service.GetInFactoryCode();
            return data;
        }
        public void DeleteForm(string keyvalue)
        {
            service.DeleteForm(keyvalue);
        }
        public void SaveForm(Sys_DepartmentEntity entity)
        {
            service.SaveForm(entity);
        }
        public Sys_DepartmentEntity GetEntity(System.Linq.Expressions.Expression<Func<Sys_DepartmentEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        public DataTable GetInOrgCode()
        {
            var data = service.GetInOrgCode();
            return data;
        }
        /// <summary>
        /// 获取系统资源
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCompanyResourcePageList(Pagination pagination, string queryJson)
        {
            return service.GetCompanyResourcePageList(pagination, queryJson);
        }
        #endregion

    }
}
