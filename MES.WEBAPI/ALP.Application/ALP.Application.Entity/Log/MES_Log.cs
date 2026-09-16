using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.Log
{
    /// <summary>
    /// 日志实体类  孙公聚 2021/7/15
    /// </summary>
    public class MES_Log : BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? ID { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        public string FunctionName { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string OperationBy { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperationOn { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string UTF1 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string UTF2 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string UTF3 { get; set; }
        /// <summary>
        /// 备用字段
        /// </summary>
        public string UTF4 { get; set; }
        /// <summary>
        /// 更改数据的 ID
        /// </summary>
        public string Table_ID { get; set; }
        /// <summary>
        /// 更改的表名
        /// </summary>
        public string TableName { get; set; }


    }
}
