using Lib.Model;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Dal
{
    public class PackingBGDal
    {
        /// <summary>
        /// 获取分页列表(包装报工分页)
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(PackingBGDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PLW.ProductOrder,
                               PLW.WorkOrder,
                               PLW.ContainerNO,
                               PLW.CustomerPO,
                               PLW.MaterialCode,
                               PLW.FactoryCode,
                               e.ResourceName FactoryName,
                               PLW.DeliveryPieces,
                               PLW.DeliveryBox,
                               PLW.DeliveryPallet,
                               PLW.DeliveryWholePallet,
                               PMA.CPBZTPSL PerPalletPieceQty,
							   PMA.BZTPSL PerPalletBoxQty,
                               PLW.DeliveryStartPallet,
                               PMA.MaterialName,
                               PMA.Spec,
                               PMA.SmallClass,
                               PMA.MaterialClass,
                               PMA.MMXH,
                               PMA.MMCJ,
                               PLS.LoadingDate,
                               bitem.Spec PaperBox,
                               CASE
                                   WHEN ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) = 0 THEN
                                       '未包装'
                                   WHEN ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) < PLW.DeliveryPieces THEN
                                       '正在包装'
                                   WHEN ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) = PLW.DeliveryPieces THEN
                                       '包装完成'
                                   ELSE
                                       ''
                               END PackingStatus,
                               [dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder) WorkQty,
                               dbo.Fun_GetPrintMarkNum(PLW.WorkOrder) PrintNum
                        FROM dbo.PL_WorkOrder PLW
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PMA.WorkOrder = PLW.WorkOrder
                            LEFT JOIN dbo.PL_LoadingSchedule PLS
                                ON PLS.WorkOrder = PLW.WorkOrder
                            LEFT JOIN dbo.BS_ModelWithResource e
                                ON e.ModelLeve = 'Factory'
                                   AND PLW.FactoryCode = e.ResourceCode
                            LEFT JOIN dbo.PL_BOM bom
                                ON PLW.WorkOrder = bom.WorkOrder
                            LEFT JOIN dbo.PL_BOMItems bitem
                                ON bom.Id = bitem.BOMId
                                   AND bitem.SmallClass = 'BC'
                        WHERE 1 = 1
                              AND ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) > 0 ");

            //工厂
            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND PLW.FactoryCode=@FactoryCode ");
            }
            //订单号
            if (!string.IsNullOrEmpty(query.ProductOrder))
            {
                sql.Append(@" AND PLW.ProductOrder LIKE @ProductOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.ProductOrder);
            }
            //PO号
            if (!string.IsNullOrEmpty(query.CustomerPO))
            {
                sql.Append(@" AND PLW.CustomerPO LIKE @CustomerPO ");
                query.CustomerPO = string.Format(@"%{0}%", query.CustomerPO);
            }
            //包装状态
            if (!string.IsNullOrEmpty(query.PackingStatus))
            {
                if (query.PackingStatus == "0")//未包装
                {
                    sql.Append(@" AND ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) = 0 ");
                }
                else if (query.PackingStatus == "1")//正在包装
                {
                    sql.Append(@" AND ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) < PLW.DeliveryPieces ");
                }
                else if (query.PackingStatus == "2")//包装完成
                {
                    sql.Append(@" AND ISNULL([dbo].[Fun_GetTransferCardSumQty](PLW.WorkOrder), 0) = PLW.DeliveryPieces ");
                }
            }
            //客户型号
            if (!string.IsNullOrEmpty(query.MaterialCode))
            {
                sql.Append(@" AND PLW.MaterialCode LIKE @MaterialCode ");
                query.MaterialCode = string.Format(@"%{0}%", query.MaterialCode);
            }
            //面膜型号
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND PMA.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //纸盒型号
            if (!string.IsNullOrEmpty(query.PaperBox))
            {
                sql.Append(@" AND bitem.Spec LIKE @PaperBox ");
                query.PaperBox = string.Format(@"%{0}%", query.PaperBox);
            }
            //规格型号
            if (!string.IsNullOrEmpty(query.Spec))
            {
                sql.Append(@"AND PMA.Spec LIKE @Spec ");
                query.Spec = string.Format(@"%{0}%", query.Spec);
            }
            //柜号
            if (!string.IsNullOrEmpty(query.ContainerNO))
            {
                sql.Append(@"AND PLW.ContainerNO = @ContainerNO ");
                //query.ContainerNO = string.Format(@"%{0}%", query.ContainerNO);
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
        /// 获取分页列表(包装报工分页)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableWithPage(PackingBGDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PLW.ProductOrder,
                               PLW.WorkOrder,
                               PLW.ContainerNO,
                               PLW.CustomerPO,
                               PLW.MaterialCode,
                               PLW.FactoryCode,
                               PLW.FactoryName,
                               PLW.DeliveryPieces,
                               PLW.DeliveryBox,
                               PLW.DeliveryPallet,
                               PLW.DeliveryWholePallet,
                               PMA.CPBZTPSL PerPalletPieceQty,
                               PMA.BZTPSL PerPalletBoxQty,
                               PLW.DeliveryStartPallet,
                               CASE PLW.AvoidProduce
                                   WHEN 1 THEN
                                       '是'
                                   ELSE
                                       '否'
                               END AvoidProduce,
                               PMA.MaterialName,
                               PMA.Spec,
                               PMA.SmallClass,
                               PMA.MaterialClass,
                               PMA.MMXH,
                               PMA.MMCJ,
                               bitem.Spec PaperBox,
                               v1.ItemName PackingStatus,
                               a.WorkQty,
                               b.PrintNum
                        FROM dbo.PL_WorkOrder PLW
                            LEFT JOIN dbo.fn_GetMaterialAttrs() PMA
                                ON PMA.WorkOrder = PLW.WorkOrder
                            LEFT JOIN dbo.PL_BOM bom
                                ON PLW.WorkOrder = bom.WorkOrder
                            LEFT JOIN dbo.PL_BOMItems bitem
                                ON bom.Id = bitem.BOMId
                                   AND bitem.SmallClass = 'BC'
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'PackingStatus'
                                   AND v1.ItemValue = PLW.PackingStatus
                            LEFT JOIN
                            (
                                SELECT WorkOrder,
                                       SUM(Qty) WorkQty
                                FROM dbo.PM_PackingBGTransferCard
                                GROUP BY WorkOrder
                            ) a
                                ON PLW.WorkOrder = a.WorkOrder
                            LEFT JOIN
                            (
                                SELECT WorkOrder,
                                       COUNT(1) PrintNum
                                FROM dbo.PM_PackingPrintMark
                                WHERE PrintStatus = '3'
                                GROUP BY WorkOrder
                            ) b
                                ON PLW.WorkOrder = b.WorkOrder
                        WHERE 1 = 1
                              AND EXISTS(SELECT 1 FROM dbo.PM_PackingPrintMark z WHERE plw.WorkOrder=z.WorkOrder) ");

            //工厂
            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND PLW.FactoryCode=@FactoryCode ");
            }
            //订单号
            if (!string.IsNullOrEmpty(query.ProductOrder))
            {
                sql.Append(@" AND PLW.ProductOrder LIKE @ProductOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.ProductOrder);
            }
            //PO号
            if (!string.IsNullOrEmpty(query.CustomerPO))
            {
                sql.Append(@" AND PLW.CustomerPO LIKE @CustomerPO ");
                query.CustomerPO = string.Format(@"%{0}%", query.CustomerPO);
            }
            //包装状态
            if (!string.IsNullOrEmpty(query.PackingStatus))
            {
                sql.Append(@" and ISNULL(plw.PackingStatus,'1')=@PackingStatus ");
            }
            //客户型号
            if (!string.IsNullOrEmpty(query.MaterialCode))
            {
                sql.Append(@" AND PLW.MaterialCode LIKE @MaterialCode ");
                query.MaterialCode = string.Format(@"%{0}%", query.MaterialCode);
            }
            //面膜型号
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND PMA.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //纸盒型号
            if (!string.IsNullOrEmpty(query.PaperBox))
            {
                sql.Append(@" AND bitem.Spec LIKE @PaperBox ");
                query.PaperBox = string.Format(@"%{0}%", query.PaperBox);
            }
            //规格型号
            if (!string.IsNullOrEmpty(query.Spec))
            {
                sql.Append(@"AND PMA.Spec LIKE @Spec ");
                query.Spec = string.Format(@"%{0}%", query.Spec);
            }
            //柜号
            if (!string.IsNullOrEmpty(query.ContainerNO))
            {
                sql.Append(@" AND PLW.ContainerNO LIKE @ContainerNO ");
                query.ContainerNO = string.Format(@"%{0}%", query.ContainerNO);
            }
            //报工开始时间
            if (query.StartTime != null)
            {
                sql.Append(@" AND EXISTS (SELECT 1 FROM dbo.PM_PackingBGTransferCard WHERE WorkOrder=PLW.WorkOrder AND CONVERT(VARCHAR(10),CreateTime,120) >= CONVERT(VARCHAR(10),@StartTime,120)) ");
            }
            //报工结束时间
            if (query.EndTime != null)
            {
                sql.Append(@" AND EXISTS (SELECT 1 FROM dbo.PM_PackingBGTransferCard WHERE WorkOrder=PLW.WorkOrder AND CONVERT(VARCHAR(10),CreateTime,120) <= CONVERT(VARCHAR(10),@EndTime,120)) ");
            }
            //报工机台
            if (!string.IsNullOrEmpty(query.MachineCode))
            {
                sql.Append(@" AND EXISTS(SELECT 1 FROM dbo.PM_PackingBGTransferCard WHERE WorkOrder=PLW.WorkOrder AND MachineCode LIKE @MachineCode ) ");
                query.MachineCode = string.Format(@"%{0}%", query.MachineCode);
            }
            if (!string.IsNullOrWhiteSpace(query.PrintStatus))
            {
                sql.Append(@" AND EXISTS(SELECT 1 FROM dbo.PM_PackingPrintMark WHERE WorkOrder=PLW.WorkOrder AND PrintStatus=@PrintStatus) ");
            }
            if (!string.IsNullOrWhiteSpace(query.CardCode))
            {
                sql.Append(" AND EXISTS(SELECT 1 FROM dbo.PM_TransferCard WHERE WorkOrder=PLW.WorkOrder AND CardCode=@CardCode) ");
            }

            var dt = DapperHelper<DataTable>.GetDataTableWithPage(sql.ToString(), query);
            record = 0;
            if (dt.Rows.Count > 0)
            {
                record = Convert.ToInt32(dt.Rows[0]["TotalCount"]);
            }
            return dt;
        }

        /// <summary>
        /// 包装报工记录列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetPackingBGList(string workOrder)
        {
            string sql = @"SELECT a.Id,
                               a.Id,
                               a.ProductOrder,
                               a.WorkOrder,
                               a.ContainerNO,
                               a.CustomerPO,
                               a.MaterialCode,
                               a.CardCode,
                               b.CardName,
                               b.CardType,
                               v1.ItemName CardTypeName,
                               a.ReWorkOrder,
                               a.Qty,
                               a.ProcessCode,
                               a.Creator,
                               c.Name CreatorName,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime
                        FROM [dbo].[PM_PackingBGTransferCard] a
                            INNER JOIN dbo.PM_TransferCard b
                                ON a.CardCode = b.CardCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND b.CardType = v1.ItemValue
                            LEFT JOIN dbo.BS_People c
                                ON a.Creator = c.Code
                        WHERE 1 = 1
                              AND a.WorkOrder = @WorkOrder";
            return DapperHelper<dynamic>.Query(sql, new { WorkOrder = workOrder });
        }
        /// <summary>
        /// 包装报工记录列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public DataTable GetPackingBGDataTable(string workOrder)
        {
            string sql = @"SELECT a.Id,
                               a.Id,
                               a.ProductOrder,
                               a.WorkOrder,
                               a.ContainerNO,
                               a.CustomerPO,
                               a.MaterialCode,
                               a.CardCode,
                               b.CardName,
                               b.CardType,
                               v1.ItemName CardTypeName,
                               a.ReWorkOrder,
                               a.Qty,
                               a.ProcessCode,
                               a.Creator,
                               c.Name CreatorName,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime,
							   a.MachineCode
                        FROM [dbo].[PM_PackingBGTransferCard] a
                            INNER JOIN dbo.PM_TransferCard b
                                ON a.CardCode = b.CardCode
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND b.CardType = v1.ItemValue
                            LEFT JOIN dbo.BS_People c
                                ON a.Creator = c.Code
                        WHERE 1 = 1
                              AND a.WorkOrder = @WorkOrder
							  ORDER BY a.CreateTime";
            return DapperHelper<DataTable>.GetDataTable(sql, new { WorkOrder = workOrder });
        }
        /// <summary>
        /// 唛头列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetPrintMarkList(string workOrder)
        {
            string sql = @"SELECT a.Id,
                               a.PackingRecordId,
                               a.PackTransferCode,
                               a.ProductOrder,
                               a.WorkOrder,
							   a.Customer,
							   a.MaterialCode,
                               a.ContainerNO,
                               a.CustomerPO,
							   a.Spec,
							   a.Quantity,
                               a.Mark,
                               a.PrintStatus,
                               v1.ItemName PrintStatusName
                        FROM [dbo].[PM_PackingPrintMark] a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'ShippingMarkPrinting'
                                   AND a.PrintStatus = v1.ItemValue
                        WHERE 1 = 1
                              AND a.WorkOrder = @WorkOrder";
            return DapperHelper<dynamic>.Query(sql, new { WorkOrder = workOrder });
        }

        /// <summary>
        /// 唛头列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public DataTable GetPrintMarkDataTable(string workOrder)
        {
            string sql = @"SELECT a.Id,a.WorkOrder,
                               a.PackingRecordId,
                               a.PackTransferCode,
                               a.ProductOrder,
                               a.WorkOrder,
                               a.Customer,
                               a.MaterialCode,
                               a.ContainerNO,
                               a.CustomerPO,
                               c.AttrValue Spec,
                               a.Quantity,
                               a.Mark,
                               a.PrintStatus,
                               v1.ItemName PrintStatusName,
							   a.Status,
							   v2.ItemName StatusName
                        FROM [dbo].[PM_PackingPrintMark] a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'ShippingMarkPrinting'
                                   AND a.PrintStatus = v1.ItemValue
                            LEFT JOIN dbo.fn_GetMaterialAttrs() b
                                ON a.WorkOrder = b.WorkOrder
                            LEFT JOIN dbo.PL_MaterialFacet c
                                ON b.Id = c.MaterialId
                                   AND c.AttrCode = 'Spec'
						    LEFT JOIN dbo.V_DataDictionary v2 ON v2.EnCode='MarkStatus'
							AND a.Status=v2.ItemValue
                        WHERE 1 = 1
                              AND a.WorkOrder = @WorkOrder
                        ORDER BY CONVERT(INT, SUBSTRING(a.Mark, CHARINDEX('-', a.Mark) + 1, 2))";
            return DapperHelper<DataTable>.GetDataTable(sql, new { WorkOrder = workOrder });
        }

        /// <summary>
        /// 获取要打印的唛头列表
        /// </summary>
        /// <param name="lstMarkCode"></param>
        /// <returns></returns>
        public List<MarkEntity> GetMarkList(List<string> lstMarkCode)
        {
            string markCodes = string.Join(",", lstMarkCode).Replace(",", "','");
            string sql = $@"SELECT * FROM dbo.PM_PackingPrintMark WHERE PackTransferCode IN('{markCodes}')";
            return DapperHelper<MarkEntity>.Query(sql, null);
        }
        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="lstMark"></param>
        /// <returns></returns>
        public int UpdatePrintStatus(List<MarkEntity> lstMark)
        {
            string sql = @"UPDATE dbo.PM_PackingPrintMark SET PrintStatus=@PrintStatus,ModifyBy=@ModifyBy,ModifyTime=GETDATE() WHERE Id=@Id";
            return DapperHelper<int>.Execute(sql, lstMark);
        }

        /// <summary>
        /// 获取唛头名称
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetMarkTemplate(string workOrder, string factoryCode)
        {
            string sql = $@"SELECT c.MarkCode AS '唛头编码',
                               c.MarkName AS '唛头名称'
                        FROM dbo.PL_Material a
                            LEFT JOIN dbo.PL_MaterialFacet b
                                ON a.Id = b.MaterialId
                            LEFT JOIN dbo.PL_MarkUpload c
                                ON b.AttrValue = c.MarkCode
                                   AND c.IsEnabled = 1
                        WHERE a.IsDeleted = 0
                              AND a.WorkOrder = '{workOrder}'
                              AND b.AttrCode = 'MTBM'
                              AND a.FactoryCode = '{factoryCode}'";
            return DapperHelper<DataTable>.GetDataTable(sql, null);
        }

        /// <summary>
        /// 获取唛头名称
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetMarkCodeTemplate(string workOrder, string factoryCode)
        {
            string sql = $@"IF OBJECT_ID('tempdb..#MTMBM') IS NOT NULL
                                DROP TABLE #MTMBM;

                            DECLARE @attrValue NVARCHAR(255) = N'';

                            SELECT TOP 1
                                   @attrValue = b.AttrValue
                            FROM dbo.PL_Material a
                                LEFT JOIN dbo.PL_MaterialFacet b
                                    ON a.Id = b.MaterialId
                                       AND b.AttrCode = 'ZYULIU1'
                            WHERE a.IsDeleted = 0
                                  AND a.WorkOrder = '{workOrder}'
                                  AND a.FactoryCode = '{factoryCode}';

                            SELECT Code MarkCode
                            INTO #MTMBM
                            FROM dbo.fn_Split(@attrValue, ',');

                            SELECT a.MarkCode,
                                   a.MarkName
                            FROM dbo.PL_MarkUpload a
                                INNER JOIN #MTMBM b
                                    ON a.MarkCode = b.MarkCode; ";
            return DapperHelper<DataTable>.GetDataTable(sql, null);
        }
    }
}
