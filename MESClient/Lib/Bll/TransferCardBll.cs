using Lib.Dal;
using Lib.Model;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class TransferCardBll
    {
        TransferCardDal dal = new TransferCardDal();

        /// <summary>
        /// 获取分页列表(流转卡-执行工单)
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(TransferCardDto query, out int record)
        {
            return dal.GetListWithPage(query, out record);
        }
        /// <summary>
        /// 获取分页列表(流转卡-执行工单)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableWithPage(TransferCardDto query, out int record)
        {
            return dal.GetDataTableWithPage(query, out record);
        }
        /// <summary>
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public List<dynamic> GetTransferCardList(string exeWorkOrder)
        {
            return dal.GetTransferCardList(exeWorkOrder);
        }
        /// <summary>
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardList(string exeWorkOrder)
        {
            return dal.GetCardList(exeWorkOrder);
        }
        /// <summary>
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public DataTable GetTransferCardDataTable(string exeWorkOrder)
        {
            return dal.GetTransferCardDataTable(exeWorkOrder);
        }
        /// <summary>
        /// 新建流转卡信息
        /// </summary>
        /// <param name="exeWorkOrder"></param>
        /// <returns></returns>
        public TransferCardEntity GetNewCard(string exeWorkOrder)
        {
            return dal.GetNewCard(exeWorkOrder);
        }
        /// <summary>
        /// 新增流转卡(保存)
        /// </summary>
        /// <param name="cardEntity">流转卡</param>
        /// <param name="resumeEntity">流转绿鬣</param>
        /// <returns></returns>
        public int NewTransferCard(dynamic cardEntity, dynamic resumeEntity)
        {
            return dal.NewTransferCard(cardEntity, resumeEntity);
        }
        /// <summary>
        /// 批量新增流转卡
        /// </summary>
        /// <param name="cardList">流转卡</param>
        /// <param name="resumeList">流转</param>
        /// <returns></returns>
        public int SaveBatchTransferCard(List<dynamic> cardList, List<dynamic> resumeList)
        {
            return dal.SaveBatchTransferCard(cardList, resumeList);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public TransferCardEntity GetCardEntity(string cardCode)
        {
            return dal.GetCardEntity(cardCode);
        }
        /// <summary>
        /// 判断流转卡是否存在报工记录
        /// </summary>
        /// <param name="cardCode">流转卡编码</param>
        /// <returns></returns>
        public bool IsExistBGRecord(string cardCode)
        {
            return dal.IsExistBGRecord(cardCode);
        }
        /// <summary>
        /// 拆分流转卡
        /// </summary>
        /// <param name="lstCard"></param>
        /// <returns></returns>
        public int SplitTransferCard(List<dynamic> lstCard, dynamic cardEntity)
        {
            return dal.SplitTransferCard(lstCard, cardEntity);
        }
        /// <summary>
        /// 获取流转卡列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardEntityList(List<string> lstCardCode)
        {
            return dal.GetCardEntityList(lstCardCode);
        }
        /// <summary>
        /// 获取流转卡列表
        /// </summary>
        /// <param name="serialNumber">xu</param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardListBySerialNumber(string serialNumber)
        {
            return dal.GetCardListBySerialNumber(serialNumber);
        }
        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="lstCard"></param>
        /// <returns></returns>
        public int UpdatePrintStatus(List<TransferCardEntity> lstCard)
        {
            return dal.UpdatePrintStatus(lstCard);
        }
        /// <summary>
        /// 获取流转卡履历
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic GetResumeEntity(string cardCode)
        {
            return dal.GetResumeEntity(cardCode);
        }
        /// <summary>
        /// 获取流转卡履历
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic GetCardResumeByFlag(string cardCode)
        {
            return dal.GetCardResumeByFlag(cardCode);
        }
    }
}
