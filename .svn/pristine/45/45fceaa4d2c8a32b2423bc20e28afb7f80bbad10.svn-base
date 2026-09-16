using ALP.Application.Entity.HTTPEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SAPEntity.ToSAP
{
    /// <summary>
    /// 工单状态维护接口
    /// </summary>
    public class IF128 : SAPRequestDto
    {
        public IF128_RSQ_DATA RSQ_DATA { get; set; }
    }
    public class IF128_RSQ_DATA
    {
        public List<IF128_HEAD> IT_HEAD { get; set; }
    }
    public class IF128_HEAD
    {
        /// <summary>
        /// SAP工单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// 工单状态
        /// </summary>
        public string APRIO { get; set; }
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    public class IF128_RSP_DATA
    {
        /// <summary>
        /// 工单号
        /// </summary>
        public string AUFNR { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string APRIO { get; set; }
        /// <summary>
        /// 消息状态
        /// </summary>
        public string STATUS { get; set; }
        /// <summary>
        /// 消息文本
        /// </summary>
        public string MESSAGE { get; set; }
    }
}
