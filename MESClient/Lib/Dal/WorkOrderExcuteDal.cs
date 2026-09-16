using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lib.Dal
{
    public class WorkOrderExcuteDal
    {

        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(WorkOrderExeSWDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.ExeWorkOrder,
                               c.FactoryCode,
                               d.ResourceName FactoryName,
                               a.ProcessCode,
                               v.ResourceName ProcessName,
                               a.EquipCode,
                               e.ResourceName EquipName,
                               a.CreateTime,
                               a.PlanProductTime,
                               a.WorkOrder,
                               c.WorkOrderType,
                               v1.ItemName WorkOrderTypeName,
                               a.SWStatus,
                               v2.ItemName SWStatusName,
                               a.ProductOrder,
                               c.ContainerNO,
                               f.MMXH,
                               f.MMCJ,
                               f.SmallClass,
                               f.Spec,
                               f.BWXH,
                               f.UV,
                               f.KCKX,
                               b.SheetsQty,
                               b.PiecesQty,
                               CEILING(b.SheetsQty / ISNULL(CONVERT(DECIMAL(18, 0), f.SCTPSL), 1)) PalletQty,
                               g.BGQty,
                               a.SWSeq
                        FROM dbo.PM_ExeWorkOrderSW a
                            INNER JOIN dbo.PL_ExeWorkOrder b
                                ON a.ExeWorkOrder = b.ExeWorkOrder
                            INNER JOIN dbo.PL_WorkOrder c
                                ON a.WorkOrder = c.WorkOrder
                                   AND b.WorkOrder = c.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'WorkOrderType'
                                   AND c.WorkOrderType = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON d.ModelLeve = 'Factory'
                                   AND c.FactoryCode = d.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource v
                                ON v.ModelLeve = 'Process'
                                   AND a.ProcessCode = v.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource e
                                ON e.ModelLeve = 'Machine'
                                   AND e.ParentResource IN ( 'FH_JC', 'FH_KC' )
                                   AND a.EquipCode = e.ResourceCode
                            LEFT JOIN dbo.fn_GetMaterialAttrs() f
                                ON c.MaterialCode = f.MaterialCode
                                   AND c.WorkOrder = f.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'SWStatus'
                                   AND a.SWStatus = v2.ItemValue
                            LEFT JOIN
                            (
                                SELECT g2.ExeWorkOrder,
                                       g1.ProcessCode,
                                       g1.MachineCode,
                                       ISNULL(SUM(g1.Qty), 0) BGQty
                                FROM dbo.PM_TranferCardBGRecord g1
                                    INNER JOIN dbo.PM_TransferCard g2
                                        ON g1.CardCode = g2.CardCode
                                WHERE g1.IsEnabled = 1
                                GROUP BY g2.ExeWorkOrder,
                                         g1.ProcessCode,
                                         g1.MachineCode
                            ) g
                                ON a.ExeWorkOrder = g.ExeWorkOrder
                                   AND a.EquipCode = g.MachineCode
                        WHERE 1 = 1
                              AND a.SWStatus IN ( '1', '2' )");

            //工厂
            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND c.FactoryCode=@FactoryCode ");
            }
            //工序
            if (!string.IsNullOrEmpty(query.ProcessCode))
            {
                sql.Append(@" AND a.ProcessCode=@ProcessCode ");
            }
            //机台
            if (!string.IsNullOrEmpty(query.MachineCode))
            {
                sql.Append(@" AND a.EquipCode=@MachineCode ");
            }
            if (!string.IsNullOrEmpty(query.SWStatus))
            {
                sql.Append(@" AND a.SWStatus=@SWStatus ");
            }
            var result = DapperHelper<dynamic>.QueryPage(sql.ToString(), query);
            record = 0;
            if (result.Count > 0)
            {
                record = result[0].TotalCount;
            }
            return result;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetList(WorkOrderExeDto query)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT * FROM dbo.PL_ExeWorkOrder WHERE 1=1");

            //工单号
            if (!string.IsNullOrEmpty(query.WorkOrder))
            {
                sql.Append(@" AND WorkOrder=@WorkOrder ");
            }
            if (!string.IsNullOrEmpty(query.ExeWorkOrderType))
            {
                sql.Append(@" AND OrderType=@ExeWorkOrderType ");
            }
            var result = DapperHelper<dynamic>.Query(sql.ToString(), query);
            return result;
        }

        /// <summary>
        /// 挤出开工
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="userCode">用户编码</param>
        /// <returns></returns>
        public int Start(string id, string exeWorkOrder, string userCode)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //1、更新派工表生产状态
            string sql1 = @"UPDATE dbo.PM_ExeWorkOrderSW SET SWStatus='2',ModifyBy=@UserCode,ModifyTime=GETDATE() WHERE Id=@Id ";
            dic.Add(sql1, new { Id = id, UserCode = userCode });
            //2、更新执行工单状态
            string sql2 = @"UPDATE dbo.PL_ExeWorkOrder SET Status='2',ModifyBy=@UserCode,ModifyTime=GETDATE() WHERE ExeWorkOrder=@ExeWorkOrder";
            dic.Add(sql2, new { ExeWorkOrder = exeWorkOrder, UserCode = userCode });
            //3.生成开工记录（执行工单下所有托）
            string sql3 = @"INSERT INTO dbo.PM_StartUpRecord
                            (
                                Id,
                                ProductOrder,
                                WorkOrder,
                                ExeWorkOrder,
                                ProcessCode,
                                CardCode,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            SELECT NEWID(),ProductOrder,WorkOrder,ExeWorkOrder,'FH_JC',CardCode,@UserCode,GETDATE(),1 FROM dbo.PM_TransferCard WHERE ExeWorkOrder=@ExeWorkOrder";
            dic.Add(sql3, new { ExeWorkOrder = exeWorkOrder, UserCode = userCode });
            return DapperHelper<int>.ExecuteTransaction(dic);
        }
        /// <summary>
        /// 生产小组人员
        /// </summary>
        /// <param name="processCode"></param>
        /// <param name="pTeamCode"></param>
        /// <returns></returns>
        public List<dynamic> GetPTeamPersonList(string processCode, string pTeamCode)
        {
            string sql = @"SELECT a.Id,
                               a.PTeamCode,
                               b.PTeamName,
                               a.PostCode,
                               c.Col2 PostName,
                               a.UserCode,
                               d.Name UserName
                        FROM dbo.PM_TeamPerson_Items a
                            INNER JOIN dbo.PM_TeamPerson b
                                ON a.PTeamCode = b.PTeamCode
                            LEFT JOIN dbo.Base_KeyParameterItem c
                                ON c.IsEnabled = 1
                                   AND c.ItemCode = @ProcessCode
                                   AND a.PostCode = c.Col1
                            LEFT JOIN dbo.BS_People d
                                ON a.UserCode = d.Code
                        WHERE a.PTeamCode = @PTeamCode";
            return DapperHelper<dynamic>.Query(sql, new { ProcessCode = processCode, PTeamCode = pTeamCode });
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
            string sql = @"SELECT c.MaterialCode,
                               c.MaterialName,
                               c.Spec,
                               d.GroupCode,
                               e.BatachNo,
                               c.SheetQty RecoilQty
                        FROM dbo.PM_TransferCard a
                            INNER JOIN dbo.PL_BOM b
                                ON a.WorkOrder = b.WorkOrder
                            INNER JOIN dbo.PL_BOMItems c
                                ON b.Id = c.BOMId
                            LEFT JOIN dbo.Base_MaterialGroupBindMaterial d
                                ON c.MaterialCode = d.MaterialCode
                            LEFT JOIN dbo.PM_MaterialBatchUpRecord e
                                ON e.IsEnabled = 1
                                   AND a.ExeWorkOrder = e.ExeWorkOrder
                        WHERE a.CardCode = @CardCode
                              AND c.ConsumeProcess = @ProcessCode
                              AND e.MachineCode = @MachineCode  ";
            return DapperHelper<dynamic>.Query(sql, new { CardCode = cardCode, ProcessCode = processCode, MachineCode = machineCode });
        }
        /// <summary>
        /// 报工记录提交
        /// </summary>
        /// <param name="cardBGRecord"></param>
        /// <param name="lstCardBGBadRecord"></param>
        /// <param name="lstCardPerson"></param>
        /// <param name="lstMB"></param>
        /// <returns></returns>
        public int SaveBGRecord(dynamic cardBGRecord, List<dynamic> lstCardBGBadRecord, List<dynamic> lstCardBGPerson, List<dynamic> lstCardBGMB)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //1.流转卡报工记录
            string sql1 = @"INSERT INTO dbo.PM_TranferCardBGRecord
                            (
                                Id,
                                CardCode,
                                ProcessCode,
                                MachineCode,
                                Qty,
                                BadQty,
                                Creator,
                                CreateTime,
                                IsEnabled,
                                BGUser
                            )
                            VALUES(@Id,@CardCode,@ProcessCode,@MachineCode,@Qty,@BadQty,@Creator,GETDATE(),1,@BGUser)";
            dic.Add(sql1, cardBGRecord);

            //2.流转卡报工不良记录
            if (lstCardBGBadRecord.Count > 0)
            {
                string sql2 = @"INSERT INTO dbo.PM_BGBadRecord
                            (
                                Id,
                                BGID,
                                BadItemCode,
                                BadItemName,
                                BadQty,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            VALUES(NEWID(),@BGID,@BadItemCode,@BadItemName,@BadQty,@Creator,GETDATE(),1)";
                dic.Add(sql2, lstCardBGBadRecord);
            }
            //3.流转卡报工生产人员
            if (lstCardBGPerson.Count > 0)
            {
                string sql3 = @"INSERT INTO dbo.PM_TransferBGPersonRecord
                            (
                                Id,
                                BGID,
                                PTeamCode,
                                PTeamName,
                                PostCode,
                                PostName,
                                UserCode,
                                UserName,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            VALUES(NEWID(),@BGID,@PTeamCode,@PTeamName,@PostCode,@PostName,@UserCode,@UserName,@Creator,GETDATE(),1)";
                dic.Add(sql3, lstCardBGPerson);
            }
            //4.物料批次绑定记录
            if (lstCardBGMB.Count > 0)
            {
                string sql4 = @"INSERT INTO dbo.PM_MaterialBatchBindRecord
                            (
                                Id,
                                BGID,
                                MaterialCode,
                                MaterialName,
                                Spec,
                                GroupCode,
                                BatchNo,
                                RecoilQty,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            VALUES(NEWID(),@BGID,@MaterialCode,@MaterialName,@Spec,@MaterialGroup,@BatchNo,@RecoilQty,@Creator,GETDATE(),1)";
                dic.Add(sql4, lstCardBGMB);
            }
            return DapperHelper<int>.ExecuteTransaction(dic);
        }
    }
}
