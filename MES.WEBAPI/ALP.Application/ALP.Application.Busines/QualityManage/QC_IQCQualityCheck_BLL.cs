using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ALP.Util.WebControl;
using ALP.Application.Entity.QualityManage;
using ALP.Application.IService.QualityManage;
using ALP.Application.Service.QualityManage;
using ALP.Application.Entity.MaterialManage;
using ALP.Application.Service.Material;
using ALP.Application.Entity.Material;

namespace ALP.Application.Busines.QualityManage
{
    public class QC_IQCQualityCheck_BLL
    {
        private QC_IQCQualityCheck_IService service = new QC_IQCQualityCheck_Service();
        private QC_TestMethodMaterialIService _TestMethodMaterialService = new QC_TestMethodMaterial_Service();
        private QC_TestMethodMaintenance_IService _TestMethodMaintenanceService = new QC_TestMethodMaintenance_Service();
        private QC_TestMethodItemMaintenance_IService _TestMethodItemMaintenanceService = new QC_TestMethodItemMaintenance_Service();
        private Base_MaterialGroupBindMaterial_Service _MaterialGroupBindMaterialService = new Base_MaterialGroupBindMaterial_Service();
        private Base_MaterialFactory_Service _MaterialFactoryService = new Base_MaterialFactory_Service();
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }


        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList1(string checkType, out string msg)
        {
            return service.GetList1(checkType, out msg);
        }


        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_IQCQualityCheckEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }


        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IQCQualityCheckListEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IQCQualityCheckEntity> entity_list, out string msg)
        {
            return SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IQCQualityCheckListEntity</returns>
        public QC_IQCQualityCheckEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IQCQualityCheckListEntity</returns>
        public QC_IQCQualityCheckEntity GetEntityByQuery(string QueryField)
        {
            return service.GetEntityByQuery(QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IQCQualityCheckListEntity 对象</returns>
        public QC_IQCQualityCheckEntity Get_ExpressionEntity(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition)
        {
            return service.Get_ExpressionEntity(condition);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IQCQualityCheckListEntity 列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> Get_ExpressionList(Expression<Func<QC_IQCQualityCheckEntity, bool>> condition)
        {
            return service.Get_ExpressionList(condition);
        }

        ///// <summary>
        ///// 删除主表数据并同步删除子表数据, 假删除更新删除标记
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <returns></returns>
        //int RemoveForm(string keyValue)
        //{
        //    int result = 0;
        //    StringBuilder sql = new StringBuilder();
        //    //子表服务类
        //    RepositoryFactory<QC_IQCQualityCheckListEntity> bomService = new RepositoryFactory<QC_IQCQualityCheckListEntity>();

        //    QC_IQCQualityCheckListEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IQCQualityCheckListDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IQCQualityCheckList_Id == entity.Id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        //主表删除标记
        //        entity.IsEnabled = false;
        //        this.BaseRepository().Update(entity);
        //        if (bomEntity != null)
        //        {
        //            //子表删除标记
        //            bomEntity.IsEnabled = false;
        //            bomService.BaseRepository().Update(bomEntity);
        //        }
        //        result = 1;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IQCQualityCheckEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
        }

        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, 1, out returnNum, out messageCode);
        }

        /// <summary>
        ///  存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetTestMethod(out string messageCode)
        {
            return service.GetTestMethod(out messageCode);
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-27 10:46:19
        /// 任务编号: IQC品质检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
        /// <summary>
        /// 生产IQC检验任务
        /// </summary>
        /// <param name="list"></param>
        public void CreateInspectNo(List<MM_ReceiptNoticeEntity> list)
        {
            var returnNum = "";
            var msg = "";
            var time = DateTime.Now;
            var sn = time.ToString("yyMMdd");
            var iqcList = new List<QC_IQCQualityCheckEntity>();
            try
            {
                var list1 = new List<Base_MaterialGroupBindMaterialEntity>();
                for (int i = 0; i < list.Count; i++)
                {
                    string a1 = list[i].MaterialCode;

                    list1.AddRange(_MaterialGroupBindMaterialService.GetList(t => t.MaterialCode == a1).ToList());
                }

                //流水号
                service.GetSerialNO("IQCQualityCheck", list.Count, out returnNum, out msg);
                var index = int.Parse(returnNum);
                //IQC检验 实验室检测
                var query = from main in _TestMethodMaintenanceService.Get_ExpressionList(t => t.TestType == "1")
                            join item in _TestMethodItemMaintenanceService.Get_ExpressionList(t => true) on main.Id equals item.TestMethodId
                            join material in _TestMethodMaterialService.Get_ExpressionList(t => true) on main.Id equals material.TestMethodId
                            join gro in list1 on material.SmallClass equals gro.GroupCode
                            join iqc in list on gro.MaterialCode equals iqc.MaterialCode
                            select new
                            {
                                main.Id,
                                item.TestDepartment,
                                iqc.MaterialCode,
                                iqc.MaterialName,
                                gro.GroupCode,
                                iqc.IsExemption
                            };

                var mList = query.ToList();

                //   if (mList.Count < 1) return;

                /**
                 * 实验室状态 1：不需要；2：待检验；3：检验完成
                 * 质检状态 1：待检验，2：检验完成
                 * */

                foreach (var item in list)
                {
                    var ent = mList.Find(t => t.MaterialCode == item.MaterialCode);
                    // if (ent == null) continue;
                    if (ent == null)
                    {
                        iqcList.Add(new QC_IQCQualityCheckEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ReceiptId = item.Id,
                            TestMethodIdId = "",
                            FactoryCode = item.FactoryCode,
                            FactoryName = item.FactoryName,
                            InspectNo = "IQC" + sn + (index++).ToString().PadLeft(3, '0'),
                            MaterialCode = item.MaterialCode,
                            MaterialName = item.MaterialName,
                            Spec = item.Spec,
                            SmallClass = item.SmallClass,
                            SupplierCode = item.SupplierCode,
                            LabStatus = "",
                            QualityStatus = "",
                            IsEnabled = true,
                            CreateTime = time,
                            Creator = item.Creator,
                        });
                    }
                    else
                    {
                        var labStatus = mList.Find(t => t.MaterialCode == item.MaterialCode && t.TestDepartment == "1") == null ? "1" : "2";
                        var QualityStatus = "1";
                        if (ent.IsExemption == "1")//免检
                        {
                            labStatus = "1";//无需检验
                            QualityStatus = "2";//检验完成
                        }
                        iqcList.Add(new QC_IQCQualityCheckEntity()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ReceiptId = item.Id,
                            TestMethodIdId = ent.Id,
                            FactoryCode = item.FactoryCode,
                            FactoryName = item.FactoryName,
                            InspectNo = "IQC" + sn + (index++).ToString().PadLeft(3, '0'),
                            MaterialCode = item.MaterialCode,
                            MaterialName = item.MaterialName,
                            Spec = item.Spec,
                            SmallClass = item.SmallClass,
                            SupplierCode = item.SupplierCode,
                            LabStatus = labStatus,
                            QualityStatus = QualityStatus,
                            TestResult = QualityStatus == "2" ? "1" : null,// TestResult=1 合格
                            IsEnabled = true,
                            CreateTime = time,
                            Creator = item.Creator,
                        });
                    }

                }

                var n = service.SaveList(iqcList);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
