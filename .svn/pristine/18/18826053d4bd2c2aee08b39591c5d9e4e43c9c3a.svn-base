using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-30
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialFactory实体
    /// 4.任务编号: 工厂物料数据维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialFactoryEntity : BaseEntity
    {
        #region 表: Base_MaterialFactory 实体类: Base_MaterialFactory 

        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; } = "";

        /// <summary>
        /// 物料编码
        /// </summary>
        public string MaterialCode { get; set; } = "";

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; } = "";

        /// <summary>
        /// 库存地点
        /// </summary>
        public string Warehouse { get; set; } = "";

        /// <summary>
        /// 采购类型
        /// </summary>
        public string ProcureType { get; set; }
        

        /// <summary>
        /// 工艺路线编码
        /// </summary>
        public string ProcessRoute { get; set; } = "";
        /// <summary>
        /// 工艺路线名称
        /// </summary>
        public string ProcessRouteName { get; set; }

        /// <summary>
        /// 是否启用批次管理
        /// </summary>
        public bool? IsUsed { get; set; }
        /// <summary>
        /// 是否免检 0否;1是
        /// </summary>
        public string IsExemption { get; set; } = "";

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; } = "";

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; } = "";

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
        /// <summary>
        /// 安全库存
        /// </summary>
        public decimal? SafeStock { get; set; }
        /// <summary>
        /// 物料属性模板
        /// </summary>
        public string TemplateCode { get; set; }
        /// <summary>
        /// 删除标记
        ///创建人：jpf
        ///时间：2024-3-6 13:15:54
        /// </summary>
        public bool? IsDeleted { get; set; }

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
