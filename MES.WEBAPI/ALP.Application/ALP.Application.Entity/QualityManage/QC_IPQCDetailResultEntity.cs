using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.QualityManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-08-23
    /// 2.创建作者: 丁零
    /// 3.功能描述: QC_IPQCDetailResult实体
    /// 4.任务编号: 过程检验记录表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class QC_IPQCDetailResultEntity : BaseEntity
    { 
        #region 表: QC_IPQCDetailResult 实体类: QC_IPQCDetailResult 
 
        /// <summary>
        /// 检验Id
        /// </summary>
        public string Id {get; set; } = "";
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 流转卡编码
        /// </summary>
        public string FlowCardId {get; set; } = "";
 
        /// <summary>
        /// 检验工序
        /// </summary>
        public string TestItemCoading {get; set; } = "";
 
        /// <summary>
        /// 检验机台
        /// </summary>
        public string TestItemResult {get; set; } = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string TestItemName { get; set; } = "";

        /// <summary>
        /// 标准
        /// </summary>
        public string TestItemStandard { get; set; } = "";

        /// <summary>
        /// 数据类型
        /// </summary>
        public string DataType { get; set; } = "";

        /// <summary>
        /// 数据类型名称
        /// </summary>
        public string DataTypeName { get; set; } = "";

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark {get; set; } = "";
 
        /// <summary>
        /// 附件
        /// </summary>
        public bool EnabledMark {get; set; }
 
        /// <summary>
        /// 检验员
        /// </summary>
        public string Creator {get; set; } = "";
 
        /// <summary>
        /// 检验时间
        /// </summary>
        public DateTime? CreatTime {get; set; }
 
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
            this.CreatTime = DateTime.Now;
            this.EnabledMark = true;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyTime = DateTime.Now;
            this.EnabledMark = true;
        }
        #endregion
 
        #endregion
    }
}
