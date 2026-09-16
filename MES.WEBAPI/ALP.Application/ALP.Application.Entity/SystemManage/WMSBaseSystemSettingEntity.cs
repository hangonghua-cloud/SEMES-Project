using System;
using ALP.Application.Code;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-09-02 10:26
    /// 描 述：系统配置表  别名：SS
    /// </summary>
    public class WMSBaseSystemSettingEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        /// <returns></returns>
        public Guid id { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? createDate { get; set; }
        /// <summary>
        /// 创建人Code
        /// </summary>
        /// <returns></returns>
        public string createUserCode { get; set; }
        /// <summary>
        /// 创建人名称
        /// </summary>
        /// <returns></returns>
        public string createUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? modifyDate { get; set; }
        /// <summary>
        /// 修改人Code
        /// </summary>
        /// <returns></returns>
        public string modifyUserCode { get; set; }
        /// <summary>
        /// 修改人名称
        /// </summary>
        /// <returns></returns>
        public string modifyUserName { get; set; }
        /// <summary>
        /// 销售出库是否启用质检标识
        /// </summary>
        /// <returns></returns>
        public bool saleQCFlag { get; set; }
        /// <summary>
        /// 允许存放于该ERP仓库主键的物料进行报废作业
        /// </summary>
        /// <returns></returns>
        public string scrapERPWHKey { get; set; }
        /// <summary>
        /// 生产退料作业是否启用校验退料数量标识
        /// </summary>
        /// <returns></returns>
        public bool mrCancelCountLimitFlag { get; set; }
        /// <summary>
        /// 料废质检结果【不填写则表示不以料废质检结果影响用料清单的料废数量和用料数量】
        /// </summary>
        /// <returns></returns>
        public string mrWastQCResult { get; set; }
        /// <summary>
        /// 工废质检结果【不填写则表示不以工废质检结果影响用料清单的工废数量】
        /// </summary>
        /// <returns></returns>
        public string opWastQCResult { get; set; }
        /// <summary>
        /// 不受约束的库位类型
        /// </summary>
        /// <returns></returns>
        public string freeLocation { get; set; }
        /// <summary>
        /// 该配置项内容为销售出库作业允许出库的质检结果
        /// </summary>
        /// <returns></returns>
        public string saleQCResult { get; set; }
        /// <summary>
        /// 强制ERP库存位置仓库配置，该配置下的仓库下属的库位ERP库存位置禁止清除
        /// </summary>
        /// <returns></returns>
        public string forceERPWHCode { get; set; }
        /// <summary>
        /// 启用ERP库位的仓库码集合,该配置内的仓库启用了ERP库位的账务层级
        /// </summary>
        /// <returns></returns>
        public string SettingERPWHLCode { get; set; }
        /// <summary>
        /// 允许请检并销售出库的库存库位类型集合【逗号分隔】
        /// </summary>
        /// <returns></returns>
        public string AllowSaleOutWHLType { get; set; }
        /// <summary>
        /// 配置物料入库类作业入库时允许直接存储的仓库库位类型【该配置主要供入库类作业推荐库位功能使用】
        /// </summary>
        /// <returns></returns>
        public string AllowStockWHLType { get; set; }
        /// <summary>
        /// 该配置项用于配置退料到仓库类作业无需退料质检作业的单据类型
        /// </summary>
        /// <returns></returns>
        public string CancelMaterialNoQCOrderType { get; set; }

        /// <summary>
        /// 配置允许计划管理模块进行排产的生产工单状态
        /// </summary>
        public string AllowPlaningWorkOrderStatus { get; set; }

        /// <summary>
        /// 工序合法生产周期，【单位：分钟】
        /// </summary>
        public double ProcessProductionCycle { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.id = Guid.NewGuid();
            this.createDate = DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.createUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.id = Guid.Parse(keyValue);
            this.modifyDate = DateTime.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.modifyUserName = OperatorProvider.Provider.Current().UserName;
            }
        }
        #endregion
    }
}