using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-26
    /// 2.创建作者: liyongguo
    /// 3.功能描述: PM_AbrasiveBG实体
    /// 4.任务编号: 磨粉料工单管理
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PM_AbrasiveBGEntity : BaseEntity
    {
        #region 表: PM_AbrasiveBG 实体类: PM_AbrasiveBG 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 磨粉料工单Id
        /// </summary>
        public string AbrasiveId { get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 生成批次
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 工序
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// 生产机台
        /// </summary>
        public string Machine { get; set; }

        /// <summary>
        /// 班次
        /// </summary>
        public string Shift { get; set; }

        /// <summary>
        /// 报工数量
        /// </summary>
        public decimal? BGQty { get; set; }

        /// <summary>
        /// 包装方式
        /// </summary>
        public string PackingType { get; set; }
        /// <summary>
        /// 人员组别
        /// </summary>
        public string UserGroup { get; set; }
        /// <summary>
        /// 人员
        /// </summary>
        public string UserNames { get; set; }
        
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 报工时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 报工日期
        /// </summary>
        public DateTime? BGDate { get; set; }
        /// <summary>
        /// 是否过账 1：已过账
        /// </summary>
        public string IsPosted { get; set; }
        /// <summary>
        /// 过账消息
        /// </summary>
        public string PostedMsg { get; set; }
        /// <summary>
        /// 过账时间
        /// </summary>
        public DateTime? PostedTime { get; set; }
        /// <summary>
        /// 过账人员
        /// </summary>
        public string PostedUser { get; set; }
        /// <summary>
        /// 工单号
        /// </summary>
        public string WorkOrder { get; set; }
        /// <summary>
        /// SAP过账凭证
        /// </summary>
        public string SAP_MBLNR { get; set; }

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
