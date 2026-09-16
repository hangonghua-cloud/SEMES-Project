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
    public class BaseDataDal
    {

        #region  Select
        /// <summary>
        /// 工厂下拉列表
        /// </summary>
        /// <param name="userCode">登陆人</param>
        /// <returns></returns>
        public List<Select> GetFactorySelect(string userCode)
        {
            string sql = @"SELECT ResourceCode ItemCode,
                               ResourceName ItemName
                        FROM dbo.BS_ModelWithResource
                        WHERE ModelLeve = 'Factory'
                              AND EnabledMark = 1
                              AND ResourceCode IN
                                  (
                                      SELECT Code
                                      FROM dbo.fn_Split(
                                           (
                                               SELECT FactoryCode FROM BS_People WHERE Code = @UserCode
                                           ),
                                           ','
                                                       )
                                  )
                        ORDER BY SortCode";
            return DapperHelper<Select>.Query(sql, new { UserCode = userCode });
        }
        /// <summary>
        /// 工序下拉列表
        /// </summary>
        /// <param name="factoryCode">工厂编码</param>
        /// <returns></returns>
        public List<Select> GetProcessSelectByFactory(string factoryCode)
        {
            string sql = @"SELECT ResourceCode ItemCode,
                                   ResourceName ItemName
                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Process'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelWithResource
                                              WHERE ModelLeve = 'Factory'
                                                    AND EnabledMark = 1
                                                    AND ResourceCode = @FactoryCode
                                          )
                                                AND EnabledMark = 1 ) ORDER BY ResourceName";
            return DapperHelper<Select>.Query(sql, new { FactoryCode = factoryCode });
        }
        /// <summary>
        /// 机台下拉列表
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetMachineSelectByProcess(string processCode)
        {
            string sql = @"SELECT ResourceCode ItemCode,ResourceName ItemName FROM dbo.BS_ModelWithResource WHERE ParentResource=@ProcessCode AND EnabledMark=1 ORDER BY SortCode";
            return DapperHelper<Select>.Query(sql, new { ProcessCode = processCode });
        }
        /// <summary>
        /// 根据父级找子级
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetListSelectByParentResource(string parentResource)
        {
            string sql = @"SELECT ResourceCode ItemCode,ResourceName ItemName FROM dbo.BS_ModelWithResource WHERE ParentResource=@ParentResource AND EnabledMark=1 ORDER BY SortCode";
            return DapperHelper<Select>.Query(sql, new { ParentResource = parentResource });
        }
        /// <summary>
        /// 根据工厂找仓库
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public List<Select> GetWarehouseSelectByFactory(string factoryCode)
        {
            string sql = @"SELECT ResourceCode ItemCode,
                                   ResourceName ItemName
                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Warehouse'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = @FactoryCode
                                                    AND EnabledMark = 1
                                          )
                                                AND EnabledMark = 1
                                      )
                            ORDER BY SortCode";
            return DapperHelper<Select>.Query(sql, new { FactoryCode = factoryCode });
        }
        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<Select> GetDictionarySelect(string enCode)
        {
            string sql = @"SELECT ItemValue ItemCode,ItemName FROM dbo.V_DataDictionary WHERE EnCode=@EnCode ORDER BY SortCode";
            return DapperHelper<Select>.Query(sql, new { EnCode = enCode });
        }

        /// <summary>
        /// 不良项目
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetBadItemSelect(string processCode)
        {
            string sql = @"SELECT a.BadItemCode ItemCode,v1.ItemName FROM dbo.PM_ProcessBadItem a
                            LEFT JOIN dbo.V_DataDictionary v1 ON v1.EnCode='PoorWorkReport' AND a.BadItemCode=v1.ItemValue
                            WHERE a.IsEnabled=1 AND a.ProcessCode=@ProcessCode ORDER BY a.BadItemCode";
            return DapperHelper<Select>.Query(sql, new { ProcessCode = processCode });
        }
        /// <summary>
        /// 生产小组
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetPTeamSelect(string processCode)
        {
            string sql = @"SELECT PTeamCode ItemCode,PTeamName ItemName FROM dbo.PM_TeamPerson WHERE ProcessCode=@ProcessCode";
            return DapperHelper<Select>.Query(sql, new { ProcessCode = processCode });
        }
        /// <summary>
        /// 流转卡
        /// </summary>
        /// <param name="exeWorkOrder"></param>
        /// <returns></returns>
        public List<Select> GetCardSelect(string exeWorkOrder)
        {
            string sql = @"SELECT CardCode ItemCode,
                                   CardName ItemName
                            FROM dbo.PM_TransferCard
                            WHERE IsEnabled = 1
                                  AND CardStatus IN ( '1', '2', '4', '6' )
                                  AND ExeWorkOrder = @ExeWorkOrder
                            ORDER BY CardCode";
            return DapperHelper<Select>.Query(sql, new { ExeWorkOrder = exeWorkOrder });
        }
        #endregion

        #region 生产小组
        /// <summary>
        /// 生产小组列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetPTeamListWithPage(PTeamDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.Id,
                               a.FactoryCode,
                               b.ResourceName FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.PTeamCode,
                               a.PTeamName,
                               a.Creator,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime
                        FROM dbo.PM_TeamPerson a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.FactoryCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.ProcessCode = c.ResourceCode
                        WHERE 1 = 1 ");

            //生产小组编码
            if (!string.IsNullOrEmpty(query.PTeamCode))
            {
                sql.Append(@" AND a.PTeamCode LIKE @PTeamCode ");
                query.PTeamCode = string.Format(@"%{0}%", query.PTeamCode);
            }
            //生产小组名称
            if (!string.IsNullOrEmpty(query.PTeamName))
            {
                sql.Append(@" AND a.PTeamName LIKE @PTeamName ");
                query.PTeamName = string.Format(@"%{0}%", query.PTeamName);
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
        /// 生产小组列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetPTeamDataTableWithPage(PTeamDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.Id,
                               a.FactoryCode,
                               b.ResourceName FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.PTeamCode,
                               a.PTeamName,
                               a.Creator,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime
                        FROM dbo.PM_TeamPerson a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.FactoryCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.ProcessCode = c.ResourceCode
                        WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND a.FactoryCode = @FactoryCode ");
            }
            //生产小组编码
            if (!string.IsNullOrEmpty(query.PTeamCode))
            {
                sql.Append(@" AND a.PTeamCode LIKE @PTeamCode ");
                query.PTeamCode = string.Format(@"%{0}%", query.PTeamCode);
            }
            //生产小组名称
            if (!string.IsNullOrEmpty(query.PTeamName))
            {
                sql.Append(@" AND a.PTeamName LIKE @PTeamName ");
                query.PTeamName = string.Format(@"%{0}%", query.PTeamName);
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
        /// 获取生产小组列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<TeamPersonEntity> GetPTeamEntityList(List<string> lstPTeamCode)
        {
            string teamCodes = string.Join(",", lstPTeamCode).Replace(",", "','");
            string sql = $@"SELECT a.Id,
                               a.FactoryCode,
                               b.ResourceName FactoryName,
                               a.ProcessCode,
                               c.ResourceName ProcessName,
                               a.PTeamCode,
                               a.PTeamName,
                               a.Creator,
                               a.CreateTime,
                               a.ModifyBy,
                               a.ModifyTime
                        FROM dbo.PM_TeamPerson a
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.FactoryCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.ProcessCode = c.ResourceCode
                        WHERE 1 = 1 and a.PTeamCode IN('{teamCodes}')";
            return DapperHelper<TeamPersonEntity>.Query(sql, null);
        }
        #endregion

        #region 人员
        /// <summary>
        /// 人员列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetPeopleListWithPage(PeopleDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.Code UserCode,
                               a.Name UserName,
                               b.Name Dept
                        FROM dbo.BS_People a
                            LEFT JOIN dbo.BS_Departments b
                                ON a.Department_ID = b.Code
                        WHERE 1 = 1 ");

            //人员编码
            if (!string.IsNullOrEmpty(query.UserCode))
            {
                sql.Append(@" AND a.Code LIKE @UserCode ");
                query.UserCode = string.Format(@"%{0}%", query.UserCode);
            }
            //人员小组名称
            if (!string.IsNullOrEmpty(query.UserName))
            {
                sql.Append(@" AND a.Name LIKE @UserName ");
                query.UserName = string.Format(@"%{0}%", query.UserName);
            }
            //部门名称
            if (!string.IsNullOrEmpty(query.Dept))
            {
                sql.Append(@" AND b.Name LIKE @Dept ");
                query.Dept = string.Format(@"%{0}%", query.Dept);
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
        /// 人员列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetPeopleDataTableWithPage(PeopleDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.Code UserCode,
                               a.Name UserName,
                               b.Name Dept
                        FROM dbo.BS_People a
                            LEFT JOIN dbo.BS_Departments b
                                ON a.Department_ID = b.Code
                        WHERE 1 = 1 ");

            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND a.FactoryCode = @FactoryCode ");
            }
            //人员编码
            if (!string.IsNullOrEmpty(query.UserCode))
            {
                sql.Append(@" AND a.Code LIKE @UserCode ");
                query.UserCode = string.Format(@"%{0}%", query.UserCode);
            }
            //人员小组名称
            if (!string.IsNullOrEmpty(query.UserName))
            {
                sql.Append(@" AND a.Name LIKE @UserName ");
                query.UserName = string.Format(@"%{0}%", query.UserName);
            }
            //部门名称
            if (!string.IsNullOrEmpty(query.Dept))
            {
                sql.Append(@" AND b.Name LIKE @Dept ");
                query.Dept = string.Format(@"%{0}%", query.Dept);
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
        /// 获取人员列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<PeopleEntity> GetPeopleEntityList(List<string> lstUserCode)
        {
            string userCodes = string.Join(",", lstUserCode).Replace(",", "','");
            string sql = $@"SELECT Code UserCode,Name UserName FROM dbo.BS_People WHERE Code IN('{userCodes}')";
            return DapperHelper<PeopleEntity>.Query(sql, null);
        }
        #endregion

        #region 物料批次
        /// <summary>
        /// 物料批次列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetMaterialBatchListWithPage(MaterialBatchDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.*,
                               v1.ItemName SmallClassName,
                               b.ResourceName WhsName,
                               c.ResourceName LocationName
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND a.SmallClass = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.LocationCode = c.ResourceCode
                        WHERE a.Qty>0 ");

            //物料编码
            if (!string.IsNullOrEmpty(query.MaterialCode))
            {
                sql.Append(@" AND a.MaterialCode LIKE @MaterialCode ");
                query.MaterialCode = string.Format(@"%{0}%", query.MaterialCode);
            }
            //物料名称
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND a.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //仓库名称
            if (!string.IsNullOrEmpty(query.WhsName))
            {
                sql.Append(@" AND b.ResourceName LIKE @WhsName ");
                query.WhsName = string.Format(@"%{0}%", query.WhsName);
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
        /// 物料批次列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetMaterialBatchDataTableWithPage(MaterialBatchDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.*,
                               v1.ItemName SmallClassName,
                               b.ResourceName WhsName,
                               c.ResourceName LocationName
							   --d.ProductOrder
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND a.SmallClass = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.LocationCode = c.ResourceCode
							--LEFT JOIN 
							--(
							--    SELECT DISTINCT d1.BatchNo,d3.ProductOrder FROM dbo.MM_RawMaterialIn d1
							--	INNER JOIN dbo.MM_ReceiptNotice d2 ON d1.BusinessId=d2.Id
							--	INNER JOIN dbo.PL_PurchaseOrder d3 ON d2.PurchaseId=d3.Id
							--) d ON a.BatchNo=d.BatchNo
                        WHERE ISNULL(a.BatchNo,'') <>'' AND a.Qty>0 ");

            if (!string.IsNullOrEmpty(query.FactoryCode))
            {
                sql.Append(@" AND a.FactoryCode = @FactoryCode ");
            }
            //物料编码
            if (!string.IsNullOrEmpty(query.MaterialCode))
            {
                sql.Append(@" AND a.MaterialCode LIKE @MaterialCode ");
                query.MaterialCode = string.Format(@"%{0}%", query.MaterialCode);
            }
            //物料名称
            if (!string.IsNullOrEmpty(query.MaterialName))
            {
                sql.Append(@" AND a.MaterialName LIKE @MaterialName ");
                query.MaterialName = string.Format(@"%{0}%", query.MaterialName);
            }
            //仓库名称
            if (!string.IsNullOrEmpty(query.WhsName))
            {
                sql.Append(@" AND b.ResourceName LIKE @WhsName ");
                query.WhsName = string.Format(@"%{0}%", query.WhsName);
            }
            //订单号
            if (!string.IsNullOrEmpty(query.ProductOrder))
            {
                sql.Append(@" AND d.ProductOrder LIKE @ProductOrder ");
                query.ProductOrder = string.Format(@"%{0}%", query.ProductOrder);
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
        /// 获取物料批次列表
        /// </summary>
        /// <param name="lstId"></param>
        /// <returns></returns>
        public List<RawMaterialStockEntity> GetMaterialBatchEntityList(List<string> lstId)
        {
            string ids = string.Join(",", lstId).Replace(",", "','");
            string sql = $@"SELECT a.*,
                               v1.ItemName SmallClassName,
                               b.ResourceName WhsName,
                               c.ResourceName LocationName
                        FROM dbo.MM_RawMaterialStock a
                            LEFT JOIN dbo.V_DataDictionary v1
                                ON v1.EnCode = 'MaterialSmall'
                                   AND a.SmallClass = v1.ItemValue
                            LEFT JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.WhsCode = b.ResourceCode
                            LEFT JOIN dbo.BS_ModelWithResource c
                                ON c.EnabledMark = 1
                                   AND a.LocationCode = c.ResourceCode
                        WHERE 1 = 1 and a.Id IN ( '{ids}' )";
            return DapperHelper<RawMaterialStockEntity>.Query(sql, null);
        }
        #endregion

        #region 机台
        /// <summary>
        /// 机台列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetMachineListWithPage(MachineDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT a.ResourceCode MachineCode,a.ResourceName MachineName FROM dbo.BS_ModelWithResource a WHERE a.ModelLeve='Machine' ");

            //机台编码
            if (!string.IsNullOrEmpty(query.MachineCode))
            {
                sql.Append(@" AND a.ResourceCode LIKE @MachineCode ");
                query.MachineCode = string.Format(@"%{0}%", query.MachineCode);
            }
            //机台名称
            if (!string.IsNullOrEmpty(query.MachineName))
            {
                sql.Append(@" AND a.ResourceName LIKE @MachineName ");
                query.MachineName = string.Format(@"%{0}%", query.MachineName);
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
        /// 机台列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetMachineDataTabletWithPage(MachineDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.ResourceCode OperationCode,
                               a.ResourceName OperationName,
                               b.ResourceCode MachineCode,
                               b.ResourceName MachineName,
                               b.SortCode
                        FROM dbo.BS_ModelWithResource a
                            INNER JOIN dbo.BS_ModelWithResource b
                                ON a.ResourceCode = b.ParentResource
                        WHERE a.ModelLeve = 'Process'
                              AND a.EnabledMark = 1
                              AND a.ParentResource IN
                                  (
                                      SELECT ResourceCode
                                      FROM dbo.BS_ModelWithResource
                                      WHERE ParentResource =
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ModelLeve = 'Factory'
                                                AND EnabledMark = 1
                                                AND ResourceCode = @FactoryCode
                                      )
                                            AND EnabledMark = 1
                                  ) ");

            //工序编码
            if (!string.IsNullOrEmpty(query.OperationCode))
            {
                sql.Append(@" AND a.ResourceCode = @OperationCode ");
            }
            //机台编码
            if (!string.IsNullOrEmpty(query.MachineCode))
            {
                sql.Append(@" AND b.ResourceCode = @MachineCode ");
            }
            //机台名称
            if (!string.IsNullOrEmpty(query.MachineName))
            {
                sql.Append(@" AND b.ResourceName LIKE @MachineName ");
                query.MachineName = string.Format(@"%{0}%", query.MachineName);
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
        /// 获取机台列表
        /// </summary>
        /// <param name="lstId"></param>
        /// <returns></returns>
        public List<MachineEntity> GetMachineEntityList(List<string> lstMachineCode)
        {
            string machineCodes = string.Join(",", lstMachineCode).Replace(",", "','");
            string sql = $@"SELECT ResourceCode MachineCode,ResourceName MachineName FROM dbo.BS_ModelWithResource WHERE ResourceCode IN('{machineCodes}')";
            return DapperHelper<MachineEntity>.Query(sql, null);
        }
        #endregion

        #region 库位
        /// <summary>
        /// 库位列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetLocationListWithPage(LocationDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"  SELECT a.ResourceCode LocationCode,
                               a.ResourceName LocationName,
                               a.ParentResource WhsCode,
                               b.ResourceName WhsName,
							   a.SortCode
                        FROM dbo.BS_ModelWithResource a
                            INNER JOIN dbo.BS_ModelWithResource b
                                ON b.EnabledMark = 1
                                   AND a.ParentResource = b.ResourceCode
                        WHERE a.ModelLeve = 'StorageLocation'
                              AND a.EnabledMark = 1 ");

            //库位
            if (!string.IsNullOrEmpty(query.Location))
            {
                sql.Append(@" AND (a.ResourceCode LIKE @Location OR a.ResourceName LIKE @Location) ");
                query.Location = string.Format(@"%{0}%", query.Location);
            }
            //仓库
            if (!string.IsNullOrEmpty(query.Warehouse))
            {
                sql.Append(@" AND (b.ResourceCode LIKE @Warehouse OR b.ResourceName LIKE @Warehouse) ");
                query.Warehouse = string.Format(@"%{0}%", query.Warehouse);
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
        /// 库位列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetLocationDataTableWithPage(LocationDto query, out int record)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.ResourceCode WhsCode,
                               a.ResourceName WhsName,
                               b.ResourceCode LocationCode,
                               b.ResourceName LocationName,
                               b.SortCode
                        FROM dbo.BS_ModelWithResource a
                            INNER JOIN dbo.BS_ModelWithResource b
                                ON a.ResourceCode = b.ParentResource
                        WHERE a.ModelLeve = 'Warehouse'
                              AND a.EnabledMark = 1
                              AND a.ParentResource IN
                                  (
                                      SELECT ResourceCode
                                      FROM dbo.BS_ModelWithResource
                                      WHERE ParentResource =
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelResourceExtendInfo
                                          WHERE FieldCode = 'GLGC'
                                                AND FieldValue = @FactoryCode
                                                AND EnabledMark = 1
                                      )
                                            AND EnabledMark = 1
                                  )");

            //库位
            if (!string.IsNullOrEmpty(query.Location))
            {
                sql.Append(@" AND (b.ResourceCode LIKE @Location OR b.ResourceName LIKE @Location) ");
                query.Location = string.Format(@"%{0}%", query.Location);
            }
            //仓库
            if (!string.IsNullOrEmpty(query.Warehouse))
            {
                sql.Append(@" AND (a.ResourceCode LIKE @Warehouse OR a.ResourceName LIKE @Warehouse) ");
                query.Warehouse = string.Format(@"%{0}%", query.Warehouse);
            }

            var dt = DapperHelper<DataTable>.GetDataTableWithPage(sql.ToString(), query);
            record = 0;
            if (dt.Rows.Count > 0)
            {
                record = Convert.ToInt32(dt.Rows[0]["TotalCount"]);
            }
            return dt;
        }
        #endregion
    }
}
