
using ALP.Application.Entity.ProduceManage;
using Lib;
using Lib.Model;
using Lib.Model.Common;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.ProduceManage
{
    public class PM_MaterialBatchUpRecordBLL
    {
        /// <summary>
        /// 原料批次上机-机台扫描
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<List<PM_MaterialBatchUpRecordEntity>> RawMBatchUpMachineScan(string machineCode)
        {
            return Http.Post<List<PM_MaterialBatchUpRecordEntity>>("/Produce/RawMBatchUpMachineScan", new { MachineCode = machineCode });
        }
        /// <summary>
        /// 原料批次上机-关键件条码扫描
        /// </summary>
        /// <param name="codeBar"></param>
        /// <returns></returns>
        public HttpResult<RawMaterialStockEntity> RawMBatchUpCodeBarScan(string codeBar)
        {
            return Http.Post<RawMaterialStockEntity>("/Produce/RawMBatchUpCodeBarScan", new { codeBar = codeBar });
        }
        /// <summary>
        /// 原料批次上机-保存
        /// </summary>
        /// <param name="codeBar"></param>
        /// <returns></returns>
        public HttpResult<string> RawMBatchUpCodeBarSave(string userCode, string userName, PM_MaterialBatchUpRecordEntity entity)
        {
            return Http.Post<string>("/Produce/RawMBatchUpCodeBarSave", new { userCode = userCode, userName = userName, entity = entity });
        }
        /// <summary>
        /// 原料批次上机-取消绑定
        /// </summary>
        /// <param name="codeBar"></param>
        /// <returns></returns>
        public HttpResult<string> RawMBatchUpMachineRemove(string userCode, string userName, List<PM_MaterialBatchUpRecordEntity> lstEntity)
        {
            return Http.Post<string>("/Produce/RawMBatchUpMachineRemove", new { userCode = userCode, userName = userName, data = lstEntity });
        }

        //自制半成品报工
        /// <summary>
        /// 自制半成品报工-生产小组扫描
        /// </summary>
        /// <param name="pTeamCode"></param>
        /// <returns></returns>
        public HttpResult<List<PM_TeamPerson_ItemsEntity>> TransferCardBGPTeamScan(string pTeamCode)
        {
            return Http.Post<List<PM_TeamPerson_ItemsEntity>>("/Produce/TransferCardBGPTeamScan", new { pTeamCode = pTeamCode });
        }

        /// <summary>
        /// 自制半成品报工-流转卡扫描
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<PM_OwnProductBGEntity> OwnProductBGCardScan(string cardCode)
        {
            return Http.Post<PM_OwnProductBGEntity>("/Produce/OwnProductBGCardScan", new { cardCode = cardCode });
        }
        /// <summary>
        /// 自制半成品报工-获取单卷条码报工记录
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<List<PM_OwnProductBGEntity>> GetOwnProductBGList(string cardCode)
        {
            return Http.Post<List<PM_OwnProductBGEntity>>("/Produce/GetOwnProductBGList", new { cardCode = cardCode });
        }

        /// <summary>
        /// 自制半成品报工-保存
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<string> OwnProductBGSave(string userCode, string userName, string userNames, string badQty, PM_OwnProductBGEntity entity, List<PM_BGBadRecordEntity> bgBadItemList)
        {
            var data = new
            {
                userCode = userCode,
                userName = userName,
                userNames = userNames,
                badQty = badQty,
                entity = entity,
                badItemDetailList = bgBadItemList
            };
            return Http.Post<string>("/Produce/OwnProductBGSave", data);
        }

        /// <summary>
        /// 自制半成品报工-单卷条码修改数量
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<string> OwnProductBGEditRoallQty(PM_OwnProductBGEntity entity)
        {
            return Http.Post<string>("/Produce/OwnProductBGEditRoallQty", new { entity = entity });
        }
        /// <summary>
        /// 生产小组人员绑定-小组编码扫描
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<TeamDto> PTeamCodeScan(string pTeamCode)
        {
            return Http.Post<TeamDto>("/Produce/PTeamCodeScan", new { pTeamCode = pTeamCode });
        }

        /// <summary>
        /// 生产小组人员绑定-保存
        /// </summary>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public HttpResult<string> PTeamSave(string pTeamCode, string userCode, string userName, List<PM_TeamPerson_ItemsEntity> personList)
        {
            return Http.Post<string>("/Produce/PTeamSave", new { pTeamCode = pTeamCode, userCode = userCode, userName = userName, personList = personList });
        }
        /// <summary>
        /// 生产小组人员绑定-山拿出
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public HttpResult<string> PTeamPersonDelete(string pTeamCode, string teamUserCode)
        {
            return Http.Post<string>("/Produce/PTeamPersonDelete", new { pTeamCode = pTeamCode, teamUserCode = teamUserCode });
        }
    }
}
