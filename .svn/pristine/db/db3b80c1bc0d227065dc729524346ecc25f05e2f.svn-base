using Lib.Dal;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class WorkOrderExcuteBll
    {
        WorkOrderExcuteDal dal = new WorkOrderExcuteDal();

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(WorkOrderExeSWDto query, out int record)
        {
            return dal.GetListWithPage(query, out record);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetList(WorkOrderExeDto query)
        {
            return dal.GetList(query);
        }
        /// <summary>
        /// 开工
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public int Start(string id, string exeWorkOrder, string userCode)
        {
            return dal.Start(id, exeWorkOrder, userCode);
        }
        /// <summary>
        /// 生产小组人员
        /// </summary>
        /// <param name="processCode"></param>
        /// <param name="pTeamCode"></param>
        /// <returns></returns>
        public List<dynamic> GetPTeamPersonList(string processCode, string pTeamCode)
        {
            return dal.GetPTeamPersonList(processCode, pTeamCode);
        }
        /// <summary>
        /// 获取物料批次绑定信息（用于生成）
        /// </summary>
        /// <param name="cardCode">流转卡编码</param>
        /// <param name="processCode">工序编码</param>
        /// <param name="machineCode">机台</param>
        /// <returns></returns>
        public List<dynamic> GetMaterialBatchBindRecordBy(string cardCode, string processCode, string machineCode)
        {
            return dal.GetMaterialBatchBindRecordBy(cardCode, processCode, machineCode);
        }
        /// <summary>
        /// 报工记录提交
        /// </summary>
        /// <param name="cardBGRecord"></param>
        /// <param name="lstCardBGBadRecord"></param>
        /// <param name="lstCardPerson"></param>
        /// <param name="lstMB"></param>
        /// <returns></returns>
        public int SaveBGRecord(dynamic cardBGRecord, List<dynamic> lstCardBGBadRecord, List<dynamic> lstCardPerson, List<dynamic> lstMB)
        {
            return dal.SaveBGRecord(cardBGRecord, lstCardBGBadRecord, lstCardPerson, lstMB);
        }
    }
}
