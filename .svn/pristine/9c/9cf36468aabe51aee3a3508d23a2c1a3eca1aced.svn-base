using ALP.Application.Entity.BaseManage;
using ALP.Application.Service.ModelLevel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ModelLevel
{
    public class Level_BLL
    {
        LeverService service = new LeverService();
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
        /// 查询模型的字段及值
        /// </summary>
        /// <returns></returns>
        public DataTable Get_FieldData(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_FieldData(map, out msg);

            return dt;
        }
        /// <summary>
        /// 查询模型的字段及值
        /// </summary>
        /// <returns></returns>
        public DataTable Get_FieldData2(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_FieldData2(map, out msg);

            return dt;
        }
        /// <summary>
        /// 取指定产品类型的产线
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ModelResourceExtendInfo_ByLevelCode(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_ModelResourceExtendInfo_ByLevelCode(map, out msg);

            return dt;
        }
        public DataTable Get_ResourceExtendByLevelCode(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_ResourceExtendByLevelCode(map, out msg);

            return dt;
        }
        public dynamic GetDynamicModelWithResource(Dictionary<string, string> map, out string msg)
        {
            var dt = service.GetDynamicModelWithResource(map, out msg);

            return dt;
        }
        public DataTable GetDynamicModelWithResource1(Dictionary<string, string> map, out string msg)
        {
            var dt = service.GetDynamicModelWithResource1(map, out msg);

            return dt;
        }

        /// <summary>
        ///根据工厂取出工序
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessByFactory(Dictionary<string, string> map, out string msg)
        {
            var dt = service.GetProcessByFactory(map, out msg);

            return dt;
        }

        /// <summary>
        /// 根据工厂、属性取出工序
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessByFactoryExtendInfo(string factoryCode, string fieldCode, string fieldValue, string name = "")
        {
            return service.GetProcessByFactoryExtendInfo(factoryCode, fieldCode, fieldValue, name);
        }
        /// <summary>
        ///根据工厂找仓库
        /// </summary>
        /// <returns></returns>
        public DataTable GetWarehouseByFactory(string factoryCode, string name = "")
        {
            var dt = service.GetWarehouseByFactory(factoryCode,name);
            return dt;
        }
        /// <summary>
        /// 根据工厂、属性找仓库
        /// </summary>
        /// <returns></returns>
        public DataTable GetWarehouseByFactoryExtendInfo(string factoryCode, string fieldCode, string fieldValue, string name = "")
        {
            var dt = service.GetWarehouseByFactoryExtendInfo(factoryCode, fieldCode, fieldValue, name);
            return dt;
        }
        /// <summary>
        /// 获取工厂对应工序对应机台列表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetAllFactoryProcessMachine(out string msg)
        {
            var dt = service.GetAllFactoryProcessMachine(out msg);
            return dt;
        }
        /// <summary>
        ///根据ParentSource取出列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByParentResource(string parentResource, string resourceName, out string msg)
        {
            var dt = service.GetListByParentResource(parentResource, resourceName, out msg);

            return dt;
        }

        /// <summary>
        ///根据ParentSource取出列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByProductionMachine(string parentResource, string resourceName, out string msg)
        {
            var dt = service.GetListByProductionMachine(parentResource, resourceName, out msg);

            return dt;
        }


        /// <summary>
        /// 取指定层级下的所有资源
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Lines_ByProductType(Dictionary<string, string> map, out string msg)
        {
            var dt = service.Get_Lines_ByProductType(map, out msg);

            return dt;
        }
        /// <summary>
        /// 获取保存表单SQL
        /// </summary>
        /// <param name="entity"></param> 
        /// <param name="msg"></param> 
        public bool Save_FieldData(BsModelResourceExtendInfoEntity entity, out string msg)
        {
            return service.Save_FieldData(entity, out msg);
        }
        public BsModelWithResourceEntity GetModelResourceByChild(string ResourceCode)
        {
            return service.GetModelResourceByChild(ResourceCode);
        }

        public IEnumerable<BsModelWithResourceEntity> GetModelResourceByFactory(string ModelLeve, string PResourceName, string ResourceName)
        {
            return service.GetModelResourceByFactory(ModelLeve, PResourceName, ResourceName);
        }
        public IEnumerable<BsModelWithResourceEntity> GetListExprocess(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return service.GetListExprocess(condition);
        }
    }
}
