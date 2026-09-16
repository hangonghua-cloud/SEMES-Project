using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-09-01
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_BOMItems实体
    /// 4.任务编号: 工单BOM明细
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_BOMItemsEntity : BaseEntity
    { 
        #region 表: PL_BOMItems 实体类: PL_BOMItems 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
 
        /// <summary>
        /// BOM主键
        /// </summary>
        public string BOMId {get; set; }
 
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; }
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; }
 
        /// <summary>
        /// 规格型号
        /// </summary>
        public string Spec {get; set; }
 
        /// <summary>
        /// 物料分类
        /// </summary>
        public string MaterialClass {get; set; }
 
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass {get; set; }
 
        /// <summary>
        /// 物料类型
        /// </summary>
        public string MaterialType {get; set; }
 
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? Num {get; set; }
 
        /// <summary>
        /// 单位编码
        /// </summary>
        public string Unit {get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public string Warehouse {get; set; }
        /// <summary>
        /// 库位编码
        /// </summary>
        [NotMapped]
        public string LocationCode { get; set; }
 
        /// <summary>
        /// 消耗工序
        /// </summary>
        public string ConsumeProcess {get; set; }
 
        /// <summary>
        /// 采购类型
        /// </summary>
        public string ProcureType {get; set; }
 
        /// <summary>
        /// 工艺路线
        /// </summary>
        public string ProcessRoute {get; set; }
 
        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public bool? IsUsed {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
        /// <summary>
        /// 外发标识 1：外发 2：自制
        /// </summary>
        public string WFMark { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
        }
        #endregion
 
        #endregion
    }
}
