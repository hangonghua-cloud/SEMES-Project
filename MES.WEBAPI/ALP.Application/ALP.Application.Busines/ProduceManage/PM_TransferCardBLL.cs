using ALP.Application.Code.Model;
using ALP.Application.Entity.PlanManage;
using ALP.Application.Entity.ProduceManage;
using ALP.Application.IService.PlanManage;
using ALP.Application.IService.ProduceManage;
using ALP.Application.Service.PlanManage;
using ALP.Application.Service.ProduceManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ProduceManage
{
    public class PM_TransferCardBLL
    {
        private PM_TransferCardIService service = new PM_TransferCard_Service();

        public IEnumerable<PM_TransferCardEntity> GetList(Expression<Func<PM_TransferCardEntity, bool>> condition)
        {
            return service.GetList(condition);
        }
        public PM_TransferCardEntity Get_ExpressionEntity(Expression<Func<PM_TransferCardEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }
        public IEnumerable<PM_TransferCardEntity> Get_ExpressionList(Expression<Func<PM_TransferCardEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }
        public int InsertList(List<PM_TransferCardEntity> lstEntity)
        {
            return service.InsertList(lstEntity);
        }
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }

        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }
        public int SaveEntity(string keyValue, PM_TransferCardEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferCardEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }
        public int insertList(List<PM_TransferCardEntity> list)
        {
            return service.insertList(list);
        }

        #region PDA接口
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <returns></returns>
        public DataTable GetCardList_PDA(string serialNumber)
        {
            return service.GetCardList_PDA(serialNumber);
        }
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="exeWorkOrder">执行工单号</param>
        /// <returns></returns>
        public DataTable GetCardList_PDA2(string exeWorkOrder)
        {
            return service.GetCardList_PDA2(exeWorkOrder);
        }

        /// <summary>
        /// 流转卡报废-流转卡扫描
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic TransferCardScrapScan(string cardCode)
        {
            return service.TransferCardScrapScan(cardCode);
        }
        /// <summary>
        /// 创建返工任务-流转卡扫描
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<dynamic> ReworkCardScan(string serialNumber, string processCode)
        {
            return service.ReworkCardScan(serialNumber, processCode);
        }
        /// <summary>
        /// 生产管理-扫一扫
        /// </summary>
        /// <param name="exeWorkOrder">序列号</param>
        /// <returns></returns>
        public List<dynamic> CodeBarScan(string exeWorkOrder)
        {
            return service.CodeBarScan(exeWorkOrder);
        }
        /// <summary>
        /// 生产管理-BOM查询
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        public List<dynamic> BOMQuery(string workOrder)
        {
            return service.BOMQuery(workOrder);
        }
        #endregion

        /// <summary>
        /// 生产管理-获取开工个数
        /// </summary>
        /// <param name="exeWorkOrder">序列号</param>
        /// <returns></returns>
        public dynamic CodeBarScan1(string exeWorkOrder, string CardStatus, string BusinessType)
        {
            return service.CodeBarScan1(exeWorkOrder, CardStatus, BusinessType);
        }

        public dynamic getNoBGTS(string serialNuber, string processCode)
        {
            return service.getNoBGTS(serialNuber, processCode);
        }


        #region App质量检测
        /// <summary>
        /// App根据流转卡获取信息
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public List<dynamic> GetQCTransferCardEntity(string cardCode)
        {
            return service.GetQCTransferCardEntity(cardCode);
        }
        public List<dynamic> GetSemiQCTransferCardEntity(string cardCode)
        {
            return service.GetSemiQCTransferCardEntity(cardCode);
        }
        #endregion
    }
}
