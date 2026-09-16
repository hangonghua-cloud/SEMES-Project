using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.WebApi.Models
{
    /// <summary>
    /// 接口响应结果通用类
    /// </summary>
    public class ResponseResult<T> : BaseResponseResult<T>
    {
        /// <summary>
        /// 请求命令
        /// </summary>
        public  string commandString { get; set; }

    }

    /// <summary>
    /// 接口响应结果通用类
    /// </summary>
    public class ResponseResult : BaseResponseResult
    {
    }
}
