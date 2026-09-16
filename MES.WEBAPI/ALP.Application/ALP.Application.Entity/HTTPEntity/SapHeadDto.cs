using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.HTTPEntity
{
    /// <summary>
    /// SAP参数头类
    /// </summary>
    public class SapHeadDto
    {
        /// <summary>
        /// 接口ID 唯一标识
        /// </summary>
        public string INIF_ID { get; set; }
        /// <summary>
        /// 消息ID
        /// </summary>
        public string MSG_ID { get; set; } = "123";
        /// <summary>
        /// 源系统
        /// </summary>
        public string SRC_SYSTEM { get; set; } = "MES";
        /// <summary>
        /// 目的系统
        /// </summary>
        public string DEST_SYSTEMP { get; set; } = "SAP";
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SEND_TIME { get; set; } = DateTime.Now.ToString("yyyyMMdd");
        /// <summary>
        /// SAP消息ID
        /// </summary>
        public string SAP_MSGID { get; set; } = "123";
    }
}
