using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-09
    /// 2.创建作者: liyongguo
    /// 3.功能描述: QC_OQCQualityCheck实体
    /// 4.任务编号: OQC检验记录
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_OQCQualityCheckEntity : BaseEntity
    { 
        #region 表: QC_OQCQualityCheck 实体类: QC_OQCQualityCheck 
 
        /// <summary>
        /// Id
        /// </summary>
        public string Id {get; set; } = "";
 
        /// <summary>
        /// 检测Id
        /// </summary>
        public string OQCCheckConfigId {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 检验单号
        /// </summary>
        public string InspectNo {get; set; } = "";

        /// <summary>
        /// 订单号
        /// </summary>
        public string ProductOrder {get; set; } = "";

        /// <summary>
        /// 唛头号
        /// </summary>
        public string PackTransferCode { get; set; } = "";

        /// <summary>
        /// 检验状态
        /// </summary>
        public string CheckStatus {get; set; } = "";
 
        /// <summary>
        /// 检验结论
        /// </summary>
        public string CheckResult {get; set; } = "";
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachment { get; set; } = "";
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; } = "";

        /// <summary>
        /// 检验单创建日期
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 检验单完成日期
        /// </summary>
        public DateTime? FinshTime {get; set; }
 
        /// <summary>
        /// 完成人
        /// </summary>
        public string FinshBy {get; set; } = "";
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; } = "";
 
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
