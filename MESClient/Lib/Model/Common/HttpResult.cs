using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib.Model.Common
{
    /// <summary>
    /// api返回结果对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpResult<T>
    {
        public T Result { get; set; }
        public T resultData { get; set; }

        public string TargetUrl { get; set; }
        public bool Success { get; set; }

        public string returnMsg { get; set; }
        public HttpError Error { get; set; }
        public string UnAuthorizedRequest { get; set; }
    }
}
