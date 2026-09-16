using ALP.Application.Entity.ProduceManage;
using ALP.Application.IService.ProduceManage;
using ALP.Application.Service.ProduceManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ProduceManage
{
    public class PM_TranferCardBGRecordBLL
    {
        PM_TranferCardBGRecord_IService service = new PM_TranferCardBGRecord_Service();


        public IEnumerable<PM_TranferCardBGRecordEntity> GetList(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TranferCardBGRecordEntity 对象</returns>
        public PM_TranferCardBGRecordEntity Get_ExpressionEntity(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
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
        /// <returns>返回PM_TranferCardBGRecordEntity 列表</returns>
        public IEnumerable<PM_TranferCardBGRecordEntity> Get_ExpressionList(Expression<Func<PM_TranferCardBGRecordEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }

        public int SaveEntity(string keyValue, PM_TranferCardBGRecordEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }

        public int Insert(List<PM_TranferCardBGRecordEntity> lstEntity)
        {
            return service.Insert(lstEntity);
        }

        #region PDA接口
        /// <summary>
        /// 养生时间
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public string GetHealthTime(string serialNumber, string processCode)
        {
            return service.GetHealthTime(serialNumber, processCode);
        }
        /// <summary>
        /// 养生时间
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public string GetHealthTime2(string exeWorkOrder, string processCode)
        {
            return service.GetHealthTime2(exeWorkOrder, processCode);
        }
        /// <summary>
        /// 获取用户报工记录
        /// </summary>
        /// <param name="userCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable GetUserBGRecord(string userCode, string startTime, string endTime)
        {
            return service.GetUserBGRecord(userCode, startTime, endTime);
        }
        #endregion
    }
}
