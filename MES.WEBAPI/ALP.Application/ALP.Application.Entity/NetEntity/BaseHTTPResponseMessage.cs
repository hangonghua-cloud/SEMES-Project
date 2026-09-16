using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.NetEntity
{
    /// <summary>
    /// HTTP 响应结果基类
    /// </summary>
    public abstract class BaseHTTPResponseMessage
    {
        /// <summary>
        /// 请求业务是否执行成功
        /// </summary>
        public bool IsSuccess { get; set; } = true;

        /// <summary>
        /// 请求响应状态码
        /// </summary>
        public string StatusCode { get; set; } = "200";

        /// <summary>
        /// 请求响应消息
        /// </summary>
        public string Msg { get; set; }

    }
}
