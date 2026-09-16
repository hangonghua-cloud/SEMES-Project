using System;
namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// [PM_PostCoefficient]表数据实体类
    /// 描述:PM_岗位系数维护
    /// 作者:Dragon
    /// 创建时间:2022-11-07 13:33:43
    /// </summary>
    public class PMPostCoefficientEntity : BaseEntity
    {
        #region 表: PM_PostCoefficient 实体类: PMPostCoefficient

        /// <summary>
        /// Id
        /// <summary>
        public string Id { get; set; }

        /// <summary>
        /// 岗位Id
        /// <summary>
        public string PostId { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode { get; set; }

        /// <summary>
        /// 岗位编码
        /// <summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 班组人数
        /// <summary>
        public int? PeopleQty { get; set; }

        /// <summary>
        /// 岗位系数
        /// <summary>
        public decimal? Coefficient { get; set; }

        /// <summary>
        /// 删除标记
        /// <summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 备注
        /// <summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建人编码
        /// <summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建人名称
        /// <summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 创建时间
        /// <summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 修改人编码
        /// <summary>
        public string ModifyCode { get; set; }

        /// <summary>
        /// 修改人名称
        /// <summary>
        public string ModifyName { get; set; }

        /// <summary>
        /// 最后修改时间
        /// <summary>
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

