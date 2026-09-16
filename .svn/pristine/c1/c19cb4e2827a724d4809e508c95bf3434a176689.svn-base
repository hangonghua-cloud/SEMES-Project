using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// 1.创建日期: 2021-09-16
    /// 2.创建作者: admin
    /// 3.功能描述: MM_RawMaterialCodeBar实体
    /// 4.任务编号: 原材料条码表
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class MM_RawMaterialCodeBarEntity : BaseEntity
    {
        #region 表: MM_RawMaterialCodeBar 实体类: MM_RawMaterialCodeBar 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 条码
        /// </summary>
        public string CodeBar { get; set; }

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 供应商编码
        /// </summary>
        public string SupplierCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime? InWhsDate { get; set; }

        /// <summary>
        /// 冻结标识
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }

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
