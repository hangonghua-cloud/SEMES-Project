using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.ProduceManage
{
    /// <summary>
    /// 报工不良分摊
    /// </summary>
    public class PM_BGBadShareEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 分摊数量
        /// </summary>
        public decimal? ShareQty { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }
    }
}
