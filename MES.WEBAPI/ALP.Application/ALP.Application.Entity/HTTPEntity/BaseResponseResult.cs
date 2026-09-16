using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.HTTPEntity
{
    /// <summary>
    /// HTTP请求响应结果基类
    /// </summary>
    public abstract class BaseResponseResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;

        /// <summary>
        /// 响应消息
        /// </summary>
        public string returnMsg { get; set; } = "请求成功";

        /// <summary>
        /// 业务状态码
        /// </summary>
        public string statusCode { get; set; } = "200";
        /// <summary>
        /// 业务结果集合
        /// </summary>
        public List<T> resultData { get; set; } = new List<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    public abstract class BaseResponseResult
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; } = true;

        /// <summary>
        /// 响应消息
        /// </summary>
        public string returnMsg { get; set; } = "请求成功";

        /// <summary>
        /// 业务状态码
        /// </summary>
        public string statusCode { get; set; } = "200";
        /// <summary>
        /// 业务结果集合
        /// </summary>
        public object resultData { get; set; }
    }
}
