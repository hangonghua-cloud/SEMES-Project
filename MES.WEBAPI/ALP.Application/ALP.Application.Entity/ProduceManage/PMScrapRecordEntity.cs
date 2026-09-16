using System;
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// [PM_ScrapRecord]表数据实体类
    /// 描述:PM_生产报废记录
    /// 作者:Dragon
    /// 创建时间:2022-12-21 09:11:16
    /// </summary>
    public class PMScrapRecordEntity : BaseEntity
    {
        #region 表: PM_ScrapRecord 实体类: PMScrapRecord
        
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
        /// 工序编码
        /// <summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// <summary>
        public string ProcessName { get; set; }

        /// <summary>
        /// 报废编码
        /// <summary>
        public string ScrapCode {get; set; }
        /// <summary>
        /// 物料小类编码
        /// </summary>
        public string SmallClassCode { get; set; }
        /// <summary>
        /// 物料小类名称
        /// </summary>
        public string SmallClassName { get; set; }

        /// <summary>
        /// 报废数量
        /// <summary>
        public decimal? ScrapQty {get; set; }
        
        /// <summary>
        /// 报废原因编码
        /// <summary>
        public string BadItemCode {get; set; }
        
        /// <summary>
        /// 报废原因名称
        /// <summary>
        public string BadItemName {get; set; }
        
        /// <summary>
        /// 分摊开始时间
        /// <summary>
        public DateTime? StartTime {get; set; }
        
        /// <summary>
        /// 分摊结束时间
        /// <summary>
        public DateTime? EndTime {get; set; }
        
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
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
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

