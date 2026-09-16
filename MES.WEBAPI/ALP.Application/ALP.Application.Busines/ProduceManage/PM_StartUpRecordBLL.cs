using ALP.Application.Entity.ProduceManage;
using ALP.Application.IService.ProduceManage;
using ALP.Application.Service.ProduceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ProduceManage
{
    public class PM_StartUpRecordBLL
    {
        PM_StartUpRecord_IService service = new PM_StartUpRecord_Service();

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_StartUpRecordEntity 对象</returns>
        public PM_StartUpRecordEntity Get_ExpressionEntity(Expression<Func<PM_StartUpRecordEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }


        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_StartUpRecordEntity 列表</returns>
        public IEnumerable<PM_StartUpRecordEntity> Get_ExpressionList(Expression<Func<PM_StartUpRecordEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }

        public IEnumerable<PM_StartUpRecordEntity> GetList(Expression<Func<PM_StartUpRecordEntity, bool>> condition)
        {
            return service.GetList(condition);
        }

        public int SaveEntity(string keyValue, PM_StartUpRecordEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_StartUpRecordEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
    }
}
