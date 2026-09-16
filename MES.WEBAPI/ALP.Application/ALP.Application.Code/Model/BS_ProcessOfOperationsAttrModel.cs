using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Code.Model
{
    public class BS_ProcessOfOperationsAttrModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 外键
        /// </summary>
        public string OperationsId { get; set; }

        /// <summary>
        /// 属性编码
        /// </summary>
        public string AttrCode { get; set; }

        /// <summary>
        /// 属性名称
        /// </summary>
        public string AttrName { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrType { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public string AttrTypeName { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        public int? SortCode { get; set; } = 100;


        /// <summary>
        /// 录入值
        /// </summary>
        public string AttrValue { get; set; }


        /// <summary>
        /// 是否有效
        /// </summary>
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
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
        /// 工艺编码
        /// </summary>
        [NotMapped]
        public string ProcessCode { get; set; } = "";

        /// <summary>
        /// 工序编码
        /// </summary>
        [NotMapped]
        public string OperationCode { get; set; }
    }
}
