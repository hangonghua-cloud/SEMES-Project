using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-05
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PL_BOM实体
    /// 4.任务编号: 工单发料表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    [Serializable]
    public class PL_BOMEntity : BaseEntity
    { 
        #region 表: PL_BOM 实体类: PL_BOM 
 
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
        /// WorkOrder
        /// </summary>
        public string WorkOrder { get; set; }


        /// <summary>
        /// WorkOrder
        /// </summary>
        public string Spec { get; set; } 

        /// <summary>
        /// 物料分类
        /// </summary>
        public string MaterialClass {get; set; } 
 
        /// <summary>
        /// BOM编码
        /// </summary>
        public string BOMCode {get; set; } 
 
        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode {get; set; } 
 
        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName {get; set; } 
 
        /// <summary>
        /// 单位数量
        /// </summary>
        public decimal? UnitNum {get; set; }
 
        /// <summary>
        /// 工艺
        /// </summary>
        public string Process {get; set; } 
 
        /// <summary>
        /// OrderType
        /// </summary>
        public string OrderType {get; set; } 
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
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } 
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
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
