using System;
namespace ALP.Application.Entity.MaterialManage
{
    /// <summary>
    /// [Base_ProductOrderFileCon]表数据实体类
    /// 描述:Base_按订单归档
    /// 作者:Dragon
    /// 创建时间:2023-05-18 10:21:29
    /// </summary>
    public class BaseProductOrderFileConEntity : BaseEntity
    {
        #region 表: Base_ProductOrderFileCon 实体类: BaseProductOrderFileCon
        
        /// <summary>
        /// Id
        /// <summary>
        public string Id {get; set; }
        
        /// <summary>
        /// 订单号
        /// <summary>
        public string ProductOrder {get; set; }
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

