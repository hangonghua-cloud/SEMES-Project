using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Cache.Factory;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.SystemManage
{
    public class Sys_PersonsBLL
    {
        private Sys_PersonService service = new Sys_PersonService();
        /// <summary>
        /// 缓存key
        /// </summary>
        private string cacheKey = "personCache";

        #region 获取数据
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {

            var data = service.Get_PageData(pagination, queryJson);

            return data;
        }

        #endregion

        #region 获取实体
        public BS_PeopleEntity GetEntity(Expression<Func<BS_PeopleEntity, bool>> condition)
        {
            return service.GetEntity(condition);
        }
        #endregion

        #region 新增修改
        public void SaveForm(BS_PeopleEntity entity)
        {
            service.SaveForm(entity);
        }
        #endregion

        #region 删除
        public void DeleteForm(string keyvalue)
        {
            service.DeleteForm(keyvalue);
        }
        #endregion

        /// <summary>
        /// 获取流水吗
        /// </summary>
        /// <param name="SeqCode"></param>
        /// <param name="returnNum"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }
        public DataTable GetListUser(string name, Pagination pagination, string loginUserCode, string factoryCode = "")
        {
            return service.GetListUser(name, pagination, loginUserCode, factoryCode);
        }
    }
}
