using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.BaseManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.BaseManage
{
    public class F_FilesBLL
    {

        F_FilesService service = new F_FilesService();
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(string queryJson, out string msg)
        {
            var dt = service.Get_Data(queryJson, out msg);
            return dt;
        }
        /// <summary>
        /// 保存（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        public string Save_Entity(F_FilesEntity entity, out string msg)
        {
            return service.SaveEntity(entity, out msg);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg"></param>
        public int Delete_Entity(string id, out string msg)
        {
            return service.DeleteEntity(id, out msg);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fid">父id</param>
        /// <param name="NotInIds">不删除的图片id</param>
        /// <param name="msg"></param>
        public int Delete_Entity(string fid,string NotInIds, out string msg)
        {
            return service.DeleteEntity(fid, NotInIds, out msg);
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        public F_FilesEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
    }
}
