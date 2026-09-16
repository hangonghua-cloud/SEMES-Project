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
    public class TransferCardDal
    {

        /// <summary>
        /// 获取分页列表(流转卡-执行工单)
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(TransferCardDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,b.ProductOrder,
                               b.FactoryCode,
                               b.FactoryName,
                               a.OrderType ExeWorkOrderType,
                               v1.ItemName ExeWorkOrderTypeName,
                               a.ExeWorkOrder,
                               b.CustomerPO,
                               a.Status,
                               v2.ItemName StatusName,
                               d.MMXH,
                               d.MMCJ,
                               d.Spec,
                               d.BWXH,
                               a.PiecesQty,
                               b.ContainerNO,
                               d.UV,
                               d.KCKX,
                               d.SmallClass,
                               CEILING(a.SheetsQty / CONVERT(DECIMAL(18, 0), d.SCTPSL)) PalletQty,
                               e.PrintedPalletQty,
                               a.SheetsQty,
                               a.CreateTime,
                               a.FinishTime
                        FROM dbo.PL_ExeWorkOrder a
                            INNER JOIN dbo.PL_WorkOrder b
                                ON a.IsEnabled = 1
                                   AND a.WorkOrder = b.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'ExecuteWorkOrderType'
                                   AND a.OrderType = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'ExeWorkOrderStatus'
                                   AND a.Status = v2.ItemValue
                            LEFT JOIN dbo.fn_GetMaterialAttrs() d
                                ON d.WorkOrder = a.WorkOrder
                                   AND d.MaterialCode = b.MaterialCode
                            LEFT JOIN
                            (
                                SELECT e1.ExeWorkOrder,
                                       COUNT(1) PrintedPalletQty
                                FROM dbo.PM_TransferCard e1
                                WHERE e1.IsEnabled = 1
                                      AND e1.PrintStatus = '2'
                                GROUP BY e1.ExeWorkOrder
                            ) e
                                ON a.ExeWorkOrder = e.ExeWorkOrder
                                   WHERE a.IsEnabled = 1 ");

            //工厂
            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND b.FactoryCode=@FactoryCode ");
            }
            //订单号
            if (!string.IsNullOrEmpty(query.ProductOrder))
            {
                sql.Append(@" AND a.ExeWorkOrder LIKE @ProductOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.ProductOrder);
            }
            //PO号
            if (!string.IsNullOrEmpty(query.CustomerPO))
            {
                sql.Append(@" AND b.CustomerPO LIKE @CustomerPO ");
                query.CustomerPO = string.Format(@"%{0}%", query.CustomerPO);
            }
            //工单类型
            if (!string.IsNullOrEmpty(query.ExecuteWorkOrderType))
            {
                sql.Append(@" AND a.OrderType =@ExecuteWorkOrderType ");
            }
            //执行工单状态
            if (!string.IsNullOrEmpty(query.ExeWorkOrderStatus))
            {
                sql.Append(@" AND a.Status=@ExeWorkOrderStatus ");
            }
            //面膜型号
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND d.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //规格型号
            if (!string.IsNullOrEmpty(query.Spec))
            {
                sql.Append(@"AND d.Spec LIKE @Spec ");
                query.Spec = string.Format(@"%{0}%", query.Spec);
            }
            //柜号
            if (!string.IsNullOrEmpty(query.ContainerNO))
            {
                sql.Append(@"AND b.ContainerNO LIKE @ContainerNO ");
                query.ContainerNO = string.Format(@"%{0}%", query.ContainerNO);
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
        /// 获取分页列表(流转卡-执行工单)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableWithPage(TransferCardDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,b.ProductOrder,
                               b.FactoryCode,
                               b.FactoryName,
                               a.OrderType ExeWorkOrderType,
                               v1.ItemName ExeWorkOrderTypeName,
                               a.ExeWorkOrder,
                               b.CustomerPO,
                               a.Status,
                               v2.ItemName StatusName,
							   CASE
                                   WHEN ISNULL(b.IsVC,0) = 0 THEN
                                       d.MaterialName
                                   ELSE
                                       b.MMXH
                               END AS MaterialName,
							   CASE
                                   WHEN ISNULL(b.IsVC,0) = 0 THEN
                                       d.MaterialName
                                   ELSE
                                       b.MMXH
                               END AS MMXH,
                               d.MMCJ,
							   CASE
                                   WHEN ISNULL(b.IsVC,0) = 0 THEN
                                       d.Spec
                                   ELSE
                                       b.Spec
                               END AS Spec,
                               d.BWXH,
                               a.PiecesQty,
                               b.ContainerNO,
							    CASE
                                   WHEN ISNULL(b.IsVC,0) = 0 THEN
                                       d.UV
                                   ELSE
                                       b.UV
                               END AS UV,
                               d.KCKX,
                               d.SmallClass,
                               e.PrintedPalletQty,
                               a.SheetsQty,
							   a.PSheetsQty,
                               a.CreateTime,
                               a.FinishTime,
							   b.OrderStatus
                        FROM dbo.PL_ExeWorkOrder a
                            INNER JOIN dbo.PL_WorkOrder b
                                ON a.IsEnabled = 1
                                   AND a.WorkOrder = b.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'ExecuteWorkOrderType'
                                   AND a.OrderType = v1.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'ExeWorkOrderStatus'
                                   AND a.Status = v2.ItemValue
                            LEFT JOIN dbo.fn_GetMaterialAttrs() d
                                ON d.WorkOrder = a.WorkOrder
                                   AND d.MaterialCode = b.MaterialCode
                            and d.FactoryCode=b.FactoryCode
                            LEFT JOIN
                            (
                                SELECT e1.ExeWorkOrder,
                                       COUNT(1) PrintedPalletQty
                                FROM dbo.PM_TransferCard e1
                                WHERE e1.IsEnabled = 1
                                      AND e1.PrintStatus = '2'
                                GROUP BY e1.ExeWorkOrder
                            ) e
                                ON a.ExeWorkOrder = e.ExeWorkOrder
                                   WHERE a.IsEnabled = 1
								   AND b.OrderStatus<='6' ");

            //工厂
            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND b.FactoryCode=@FactoryCode ");
            }
            //订单号
            if (!string.IsNullOrEmpty(query.ProductOrder))
            {
                sql.Append(@" AND b.ProductOrder LIKE @ProductOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.ProductOrder);
            }
            
            //工单号
            if (!string.IsNullOrEmpty(query.WorkOrder))
            {
                sql.Append(@" AND a.WorkOrder LIKE @WorkOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.WorkOrder);
            }
            
            //PO号
            if (!string.IsNullOrEmpty(query.CustomerPO))
            {
                sql.Append(@" AND b.CustomerPO LIKE @CustomerPO ");
                query.CustomerPO = string.Format(@"%{0}%", query.CustomerPO);
            }
            //工单类型
            if (!string.IsNullOrEmpty(query.ExecuteWorkOrderType))
            {
                sql.Append(@" AND a.OrderType =@ExecuteWorkOrderType ");
            }
            //执行工单状态
            if (!string.IsNullOrEmpty(query.ExeWorkOrderStatus))
            {
                sql.Append(@" AND a.Status=@ExeWorkOrderStatus ");
            }
            //面膜型号
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND d.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //规格型号
            if (!string.IsNullOrEmpty(query.Spec))
            {
                sql.Append(@"AND d.Spec LIKE @Spec ");
                query.Spec = string.Format(@"%{0}%", query.Spec);
            }
            //柜号
            if (!string.IsNullOrEmpty(query.ContainerNO))
            {
                sql.Append(@"AND b.ContainerNO = @ContainerNO ");
                //query.ContainerNO = string.Format(@"%{0}%", query.ContainerNO);
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
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public List<dynamic> GetTransferCardList(string exeWorkOrder)
        {
            string sql = @"SELECT a.Id,
                               a.TransferBy,
                               a.SerialNumber,
                               a.CardType,
                               v1.ItemName CardTypeName,
                               a.CardCode,
                               a.CardName,
                               a.PalletQty SheetQty,
                               a.CardStatus,
                               v2.ItemName CardStatusName,
                               a.PrintStatus,
                               v3.ItemName PrintStatusName,
                               c.ProcessCode,
                               d.ResourceName ProcessName,
                               c.BusinessType,
                               v4.ItemName BusinessTypeName,
                               CONVERT(VARCHAR(40), c.SheetQty) + '/' + CONVERT(VARCHAR(40), c.PieceQty) StockQty
                        FROM dbo.PM_TransferCard a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND a.CardType = v1.ItemValue
                            LEFT JOIN dbo.fn_GetMaterialAttrs() b
                                ON a.WorkOrder = b.WorkOrder
                                   AND a.MaterialCode = b.MaterialCode
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'CirculationCardStatus'
                                   AND a.CardStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'CirculationCardPrintingStatus'
                                   AND a.PrintStatus = v3.ItemValue
                            LEFT JOIN dbo.PM_TransferCardResume c
                                ON c.Flag = '1'
                                   AND a.CardCode = c.CardCode
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON d.EnabledMark = 1
                                   AND c.ProcessCode = d.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v4
                                ON v4.EnCode = 'FlowIdentification'
                                   AND c.BusinessType = v4.ItemValue
                        WHERE a.ExeWorkOrder = @ExeWorkOrder
                              AND a.IsEnabled = 1
                        ORDER BY a.CardCode";
            return DapperHelper<dynamic>.Query(sql, new { ExeWorkOrder = exeWorkOrder });
        }

        /// <summary>
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardList(string exeWorkOrder)
        {
            string sql = @"SELECT * FROM dbo.PM_TransferCard WHERE ExeWorkOrder=@exeWorkOrder";
            return DapperHelper<TransferCardEntity>.Query(sql, new { ExeWorkOrder = exeWorkOrder });
        }

        /// <summary>
        /// 流转卡列表
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public DataTable GetTransferCardDataTable(string exeWorkOrder)
        {
            string sql = @"SELECT a.Id,
                               a.TransferBy,
                               a.SerialNumber,
                               a.CardType,
                               v1.ItemName CardTypeName,
                               a.CardCode,
                               a.CardName,
                               a.PalletQty SheetQty,
                               a.CardStatus,
                               v2.ItemName CardStatusName,
                               a.PrintStatus,
                               v3.ItemName PrintStatusName,
                               c.ProcessCode,
                               d.ResourceName ProcessName,
                               c.BusinessType,
                               v4.ItemName BusinessTypeName,
                               CONVERT(VARCHAR(40), c.SheetQty) + '/' + CONVERT(VARCHAR(40), c.PieceQty) StockQty
                        FROM dbo.PM_TransferCard a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'CirculationCardType'
                                   AND a.CardType = v1.ItemValue
                            LEFT JOIN dbo.PL_WorkOrder pw ON a.WorkOrder=pw.WorkOrder
                            LEFT JOIN dbo.V_DataDictionary v2
                                ON v2.EnCode = 'CirculationCardStatus'
                                   AND a.CardStatus = v2.ItemValue
                            LEFT JOIN dbo.V_DataDictionary v3
                                ON v3.EnCode = 'CirculationCardPrintingStatus'
                                   AND a.PrintStatus = v3.ItemValue
                            LEFT JOIN dbo.PM_TransferCardResume c
                                ON c.Flag = '1'
                                   AND a.CardCode = c.CardCode
                            LEFT JOIN dbo.BS_ModelWithResource d
                                ON d.EnabledMark = 1
                                   AND c.ProcessCode = d.ResourceCode
                            LEFT JOIN dbo.V_DataDictionary v4
                                ON v4.EnCode = 'FlowIdentification'
                                   AND c.BusinessType = v4.ItemValue
                        WHERE a.ExeWorkOrder = @ExeWorkOrder
                              AND a.IsEnabled = 1
                        ORDER BY a.CardCode";
            return DapperHelper<dynamic>.GetDataTable(sql, new { ExeWorkOrder = exeWorkOrder });
        }

        /// <summary>
        /// 新建流转卡信息
        /// </summary>
        /// <param name="exeWorkOrder"></param>
        /// <returns></returns>
        public TransferCardEntity GetNewCard(string exeWorkOrder)
        {
            string sql = @"SELECT b.ProductOrder,
                               a.WorkOrder,
                               b.WorkOrderType,
                               a.ExeWorkOrder,
                               b.ContainerNO,
                               c.BJGY,
                               b.Remark WorkOrderRemark,
                               b.MaterialCode,
                               CASE WHEN  b.IsVC=1 THEN b.Spec ELSE c.Spec END Spec,
							   CASE WHEN  b.IsVC=1 THEN b.MMXH ELSE c.MMXH END MMXH,
                               c.JCGG,
                               c.BWXH,
							   CASE WHEN  b.IsVC=1 THEN b.KCKX ELSE c.KCKX END KCKX,
							   CASE WHEN  b.IsVC=1 THEN b.UV ELSE c.UV END UV,
                               b.TotalSheets,
                               b.ActualSheets,
                               b.OrderPieces,
                               b.OrderPieces * b.Yield ProductPieces,
                               c.TPGG,
                               b.OrderPallet,
                               c.BZTPSL PerPallerBox,
                               c.BZDHSL,
                               c.SMS Description,
                               CONVERT(VARCHAR(40), b.OrderStartPallet) + '-' + CONVERT(VARCHAR(40), b.OrderPallet) PalletNum,
                               f.Spec PaperBoxModel,
                               REPLACE(CONVERT(VARCHAR(100), p.BoxDate, 5), '-', '') + 'A' BoxDate,
                               a.TransferBy,
							   c.DXZH
                        FROM dbo.PL_ExeWorkOrder a
                            INNER JOIN dbo.PL_WorkOrder b
                                ON b.IsEnabled = 1
                                   AND a.WorkOrder = b.WorkOrder
                            INNER JOIN dbo.PL_ProductionOrder p
                                ON b.ProductOrder = p.ProductOrder
                            LEFT JOIN dbo.fn_GetMaterialAttrs() c
                                ON b.WorkOrder = c.WorkOrder
                                   AND b.MaterialCode = c.MaterialCode
                            LEFT JOIN
                            (
                                SELECT f1.WorkOrder,
                                       f2.Spec
                                FROM dbo.PL_BOM f1
                                    INNER JOIN dbo.PL_BOMItems f2
                                        ON f1.Id = f2.BOMId
                                WHERE f2.SmallClass = 'BC' --包材
                            ) f
                                ON a.WorkOrder = f.WorkOrder
                        WHERE a.IsEnabled = 1
                              AND a.ExeWorkOrder = @ExeWorkOrder ";
            return DapperHelper<TransferCardEntity>.QueryFirstOrDefault(sql, new { ExeWorkOrder = exeWorkOrder });
        }
        /// <summary>
        /// 新增流转卡
        /// </summary>
        /// <param name="cardEntity">流转卡</param>
        /// <param name="resumeEntity">流转</param>
        /// <returns></returns>
        public int NewTransferCard(dynamic cardEntity, dynamic resumeEntity)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //新增流转卡
            string sql1 = @"INSERT INTO dbo.PM_TransferCard
                        (
                            Id,
                            ProductOrder,
                            WorkOrder,
                            WorkOrderType,
                            ExeWorkOrder,
                            CardCode,
                            CardName,
                            CardType,
                            ContainerNO,
                            BJGY,
                            WorkOrderRemark,
                            MaterialCode,
                            Spec,
                            MMXH,
                            JCGG,
                            BWXH,
                            SCTPSL,
                            SCTPSLP,
                            BZTPSL,
                            KCKX,
                            UV,
                            TotalSheets,
                            ActualSheets,
                            OrderPieces,
                            ProductPieces,
                            TPGG,
                            OrderPallet,
                            PerPallerBox,
                            BZDHSL,
                            Description,
                            PalletNum,
                            PaperBoxModel,
                            BoxDate,
                            CardStatus,
                            PrintStatus,
                            Creator,
                            CreateTime,
                            IsEnabled,
                            PalletQty,
                            NewType,
                            StartProcess,
							TransferBy,
							SerialNumber,
                            PieceQty
                        )
                        VALUES
                        (@Id, @ProductOrder, @WorkOrder, @WorkOrderType, @ExeWorkOrder, @CardCode, @CardName, @CardType, @ContainerNO, @BJGY,
                         @WorkOrderRemark, @MaterialCode, @Spec, @MMXH, @JCGG, @BWXH, @SCTPSL, @SCTPSLP, @BZTPSL, @KCKX, @UV, @TotalSheets,
                         @ActualSheets, @OrderPieces, @ProductPieces, @TPGG, @OrderPallet, @PerPallerBox, @BZDHSL, @Description, @PalletNum,
                         @PaperBoxModel, @BoxDate, '1', '1', @Creator, GETDATE(), 1, @PalletQty, @NewType, @StartProcess,@TransferBy,@SerialNumber,@PieceQty) ";
            dic.Add(sql1, cardEntity);
            if (resumeEntity != null)
            {
                //流转履历
                string sql2 = @"INSERT INTO dbo.PM_TransferCardResume
                            (
                                Id,FactoryCode,FactoryName,
                                ProcessCode,
                                CardCode,
                                BusinessType,
                                Flag,
                                WhsCode,
                                LocationCode,
                                SheetQty,
                                PieceQty,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            VALUES
                            (   @Id,           -- Id - varchar(40)
                                @FactoryCode,@FactoryName,
                                @ProcessCode,  -- ProcessCode - varchar(30)
                                @CardCode,     -- CardCode - varchar(40)
                                @BusinessType, -- BusinessType - varchar(40)
                                @Flag,         -- Flag - varchar(10)
                                @WhsCode,      -- WhsCode - varchar(40)
                                @LocationCode, -- LocationCode - varchar(40)
                                @SheetQty,     -- SheetQty - decimal(18, 0)
                                @PieceQty,     -- PieceQty - decimal(18, 0)
                                @Creator,      -- Creator - varchar(30)
                                GETDATE(),     -- CreateTime - datetime
                                1              -- IsEnabled - bit
                                )";
                dic.Add(sql2, resumeEntity);
            }
            return DapperHelper<int>.ExecuteTransaction(dic);
        }
        /// <summary>
        /// 批量新增流转卡
        /// </summary>
        /// <param name="cardList">流转卡</param>
        /// <param name="resumeList">流转</param>
        /// <returns></returns>
        public int SaveBatchTransferCard(List<dynamic> cardList, List<dynamic> resumeList)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //新增流转卡
            string sql1 = @"INSERT INTO dbo.PM_TransferCard
                        (
                            Id,
                            ProductOrder,
                            WorkOrder,
                            WorkOrderType,
                            ExeWorkOrder,
                            CardCode,
                            CardName,
                            CardType,
                            ContainerNO,
                            BJGY,
                            WorkOrderRemark,
                            MaterialCode,
							MaterialName,
                            Spec,
                            MMXH,
                            JCGG,
                            BWXH,
                            SCTPSL,
                            SCTPSLP,
                            BZTPSL,
                            KCKX,
                            UV,
                            TotalSheets,
                            ActualSheets,
                            OrderPieces,
                            ProductPieces,
                            TPGG,
                            OrderPallet,
                            PerPallerBox,
                            BZDHSL,
                            Description,
                            PalletNum,
                            PaperBoxModel,
                            BoxDate,
                            CardStatus,
                            PrintStatus,
                            Creator,
                            CreateTime,
                            IsEnabled,
                            PalletQty,
                            NewType,
                            StartProcess,
							TransferBy,
							SerialNumber,
                            PieceQty,
							DXZH,
							ShowPalletQty,
							StartOperationPalletCount,
							SplitProcess
                        )
                        VALUES
                        (@Id, @ProductOrder, @WorkOrder, @WorkOrderType, @ExeWorkOrder, @CardCode, @CardName, @CardType, @ContainerNO, @BJGY,
                         @WorkOrderRemark, @MaterialCode,@MaterialName, @Spec, @MMXH, @JCGG, @BWXH, @SCTPSL, @SCTPSLP, @BZTPSL, @KCKX, @UV, @TotalSheets,
                         @ActualSheets, @OrderPieces, @ProductPieces, @TPGG, @OrderPallet, @PerPallerBox, @BZDHSL, @Description, @PalletNum,
                         @PaperBoxModel, @BoxDate, '1', @PrintStatus, @Creator, GETDATE(), 1, @PalletQty, @NewType, @StartProcess,@TransferBy,@SerialNumber,@PieceQty,
						 @DXZH,@ShowPalletQty,@StartOperationPalletCount,@SplitProcess) ";
            dic.Add(sql1, cardList);
            if (resumeList.Count > 0)
            {
                //流转履历
                string sql2 = @"INSERT INTO dbo.PM_TransferCardResume
                            (
                                Id,FactoryCode,FactoryName,
                                ProcessCode,
                                CardCode,
                                BusinessType,
                                Flag,
                                WhsCode,
                                LocationCode,
                                SheetQty,
                                PieceQty,
                                Creator,
                                CreateTime,
                                IsEnabled
                            )
                            VALUES
                            (   @Id,           -- Id - varchar(40)
                                @FactoryCode,@FactoryName,
                                @ProcessCode,  -- ProcessCode - varchar(30)
                                @CardCode,     -- CardCode - varchar(40)
                                @BusinessType, -- BusinessType - varchar(40)
                                @Flag,         -- Flag - varchar(10)
                                @WhsCode,      -- WhsCode - varchar(40)
                                @LocationCode, -- LocationCode - varchar(40)
                                @SheetQty,     -- SheetQty - decimal(18, 0)
                                @PieceQty,     -- PieceQty - decimal(18, 0)
                                @Creator,      -- Creator - varchar(30)
                                GETDATE(),     -- CreateTime - datetime
                                1              -- IsEnabled - bit
                                )";
                dic.Add(sql2, resumeList);
            }
            return DapperHelper<int>.ExecuteTransaction(dic);
        }
        /// <summary>
        /// 获取流转卡实体
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public TransferCardEntity GetCardEntity(string cardCode)
        {
            string sql = @"SELECT * FROM dbo.PM_TransferCard WHERE CardCode=@CardCode";
            return DapperHelper<TransferCardEntity>.QueryFirstOrDefault(sql, new { CardCode = cardCode });
        }

        /// <summary>
        /// 判断流转卡是否存在报工记录
        /// </summary>
        /// <param name="cardCode">流转卡编码</param>
        /// <returns></returns>
        public bool IsExistBGRecord(string cardCode)
        {
            string sql = @"SELECT COUNT(1) FROM dbo.PM_TranferCardBGRecord WHERE CardCode=@CardCode";
            var i = DapperHelper<int>.ExecuteScalar(sql, new { CardCode = cardCode });
            return Convert.ToInt32(i) > 0;
        }

        /// <summary>
        /// 拆分流转卡
        /// </summary>
        /// <param name="lstCard"></param>
        /// <returns></returns>
        public int SplitTransferCard(List<dynamic> lstCard, dynamic cardEntity)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            //1、删除旧数据
            string sql1 = @"DELETE FROM dbo.PM_TransferCard WHERE Id=@Id";
            dic.Add(sql1, cardEntity);
            //2、新增
            string sql2 = @"INSERT INTO dbo.PM_TransferCard
                        (
                            Id,
                            ProductOrder,
                            WorkOrder,
                            WorkOrderType,
                            ExeWorkOrder,
                            CardCode,
                            CardName,
                            CardType,
                            ContainerNO,
                            BJGY,
                            WorkOrderRemark,
                            MaterialCode,
                            Spec,
                            MMXH,
                            JCGG,
                            BWXH,
                            SCTPSL,
                            SCTPSLP,
                            BZTPSL,
                            KCKX,
                            UV,
                            TotalSheets,
                            ActualSheets,
                            OrderPieces,
                            ProductPieces,
                            TPGG,
                            OrderPallet,
                            PerPallerBox,
                            BZDHSL,
                            Description,
                            PalletNum,
                            PaperBoxModel,
                            BoxDate,
                            CardStatus,
                            PrintStatus,
                            Creator,
                            CreateTime,
                            IsEnabled,
                            PalletQty,
                            NewType,
                            StartProcess,
							TransferBy,
							SerialNumber
                        )
                        VALUES
                        (@Id, @ProductOrder, @WorkOrder, @WorkOrderType, @ExeWorkOrder, @CardCode, @CardName, @CardType, @ContainerNO, @BJGY,
                         @WorkOrderRemark, @MaterialCode, @Spec, @MMXH, @JCGG, @BWXH, @SCTPSL, @SCTPSLP, @BZTPSL, @KCKX, @UV, @TotalSheets,
                         @ActualSheets, @OrderPieces, @ProductPieces, @TPGG, @OrderPallet, @PerPallerBox, @BZDHSL, @Description, @PalletNum,
                         @PaperBoxModel, @BoxDate, '1', '1', @Creator, GETDATE(), 1, @PalletQty, @NewType, @StartProcess,@TransferBy,@SerialNumber) ";
            dic.Add(sql2, lstCard);
            return DapperHelper<int>.ExecuteTransaction(dic);
        }

        /// <summary>
        /// 获取流转卡列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardEntityList(List<string> lstCardCode)
        {
            string cardCodes = string.Join(",", lstCardCode).Replace(",", "','");
            string sql = $@"SELECT * FROM dbo.PM_TransferCard WHERE CardCode IN('{cardCodes}')";
            return DapperHelper<TransferCardEntity>.Query(sql, null);
        }
        /// <summary>
        /// 获取流转卡列表
        /// </summary>
        /// <param name="serialNumber">xu</param>
        /// <returns></returns>
        public List<TransferCardEntity> GetCardListBySerialNumber(string serialNumber)
        {
            string sql = $@"SELECT * FROM dbo.PM_TransferCard WHERE SerialNumber='{serialNumber}'";
            return DapperHelper<TransferCardEntity>.Query(sql, null);
        }
        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="lstCard"></param>
        /// <returns></returns>
        public int UpdatePrintStatus(List<TransferCardEntity> lstCard)
        {
            string sql = @"UPDATE dbo.PM_TransferCard SET PrintStatus=@PrintStatus,ModifyBy=@ModifyBy,ModifyTime=GETDATE() WHERE Id=@Id";
            return DapperHelper<int>.Execute(sql, lstCard);
        }

        /// <summary>
        /// 获取流转卡履历
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic GetResumeEntity(string cardCode)
        {
            string sql = @"SELECT * FROM dbo.PM_TransferCardResume WHERE Flag='1' AND CardCode=@CardCode";
            return DapperHelper<dynamic>.QueryFirstOrDefault(sql, new { CardCode = cardCode });
        }

        /// <summary>
        /// 获取流转卡履历
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        public dynamic GetCardResumeByFlag(string cardCode)
        {
            string sql = @"SELECT TOP 1 * FROM dbo.PM_TransferCardResume WHERE Flag='0' AND BusinessType='8' AND CardCode=@CardCode ORDER BY CreateTime DESC";
            return DapperHelper<dynamic>.QueryFirstOrDefault(sql, new { CardCode = cardCode });
        }
    }
}
