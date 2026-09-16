using ALP.Data.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-27
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_PrdOrderReqMaterials实体
    /// 4.任务编号: 订单物料需求表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_PrdOrderReqMaterialsEntity : BaseEntity
    { 
        #region 表: PL_PrdOrderReqMaterials 实体类: PL_PrdOrderReqMaterials 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 工单编码
        /// </summary>
        public string WorkOrder { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } 
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; }
        /// <summary>
        /// 物料规格
        /// </summary>
        public string Spec { get; set; }
        /// <summary>
        /// 物料小类
        /// </summary>
        public string SmallClass { get; set; }

        /// <summary>
        /// 物料需求量
        /// </summary>
        [DecimalPrecision(18,3)]
        public decimal? Amount {get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 采购类型
        /// </summary>
        public string PurchaseType { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        public bool? IsDeleted { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } 
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTimeOffset? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTimeOffset? ModifyTime {get; set; }
 
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
