using System;
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// [PM_PerformanceManage]表数据实体类
    /// 描述:绩效管理
    /// 作者:Dragon
    /// 创建时间:2022-11-16 14:40:01
    /// </summary>
    public class PMPerformanceManageEntity : BaseEntity
    {
        #region 表: PM_PerformanceManage 实体类: PMPerformanceManage
        
        /// <summary>
        /// Id
        /// <summary>
        public string Id {get; set; }
        
        /// <summary>
        /// 工厂编码
        /// <summary>
        public string FactoryCode {get; set; }
        
        /// <summary>
        /// 工厂名称
        /// <summary>
        public string FactoryName {get; set; }
        
        /// <summary>
        /// 车间编码
        /// <summary>
        public string WorkshopCode {get; set; }
        
        /// <summary>
        /// 车间名称
        /// <summary>
        public string WorkshopName {get; set; }
        
        /// <summary>
        /// 工序编码
        /// <summary>
        public string ProcessCode {get; set; }
        
        /// <summary>
        /// 工序名称
        /// <summary>
        public string ProcessName {get; set; }
        
        /// <summary>
        /// 岗位编码
        /// <summary>
        public string PostCode {get; set; }
        
        /// <summary>
        /// 岗位名称
        /// <summary>
        public string PostName {get; set; }
        
        /// <summary>
        /// 岗位系数
        /// <summary>
        public decimal? Coefficient {get; set; }
        
        /// <summary>
        /// 总岗位系数
        /// <summary>
        public decimal? TotalCoefficient {get; set; }
        
        /// <summary>
        /// 报工Id
        /// <summary>
        public string BGId {get; set; }
        
        /// <summary>
        /// 流转卡号
        /// <summary>
        public string CardCode {get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec { get; set; }

        /// <summary>
        /// 报工小组编码
        /// <summary>
        public string PTeamCode {get; set; }
        
        /// <summary>
        /// 生产小组人数
        /// <summary>
        public int? PeopleQty {get; set; }
        
        /// <summary>
        /// 员工工号
        /// <summary>
        public string UserCode {get; set; }
        
        /// <summary>
        /// 员工名称
        /// <summary>
        public string UserName {get; set; }
        
        /// <summary>
        /// 计薪日期
        /// <summary>
        public string PayrollDate {get; set; }
        
        /// <summary>
        /// 报工数量
        /// <summary>
        public decimal? Qty {get; set; }
        
        /// <summary>
        /// 单位
        /// <summary>
        public string UnitName {get; set; }
        
        /// <summary>
        /// 产品单价
        /// <summary>
        public decimal? Price {get; set; }
        
        /// <summary>
        /// 设备系数
        /// <summary>
        public decimal? EquipCoefficient {get; set; }
        
        /// <summary>
        /// 工资
        /// <summary>
        public decimal? Salary {get; set; }
        
        /// <summary>
        /// 信息来源 1:MES 0:人工录入
        /// <summary>
        public int? InfoSource {get; set; }
        
        /// <summary>
        /// 事项说明
        /// <summary>
        public string Description {get; set; }
        
        /// <summary>
        /// 删除标记
        /// <summary>
        public bool? IsDeleted {get; set; }
        
        /// <summary>
        /// 备注
        /// <summary>
        public string Remark {get; set; }
        
        /// <summary>
        /// 创建人编码
        /// <summary>
        public string CreatorCode {get; set; }
        
        /// <summary>
        /// 创建人名称
        /// <summary>
        public string CreatorName {get; set; }
        
        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime {get; set; }
        
        /// <summary>
        /// 修改人编码
        /// <summary>
        public string ModifyCode {get; set; }
        
        /// <summary>
        /// 修改人名称
        /// <summary>
        public string ModifyName {get; set; }
        
        /// <summary>
        /// 最后修改时间
        /// <summary>
        public DateTime? ModifyTime {get; set; }
        #endregion
        
        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// <summary>
        public override void Create()
        {
             this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// <summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
             this.Id = keyValue;
        }
        #endregion
    }
}

