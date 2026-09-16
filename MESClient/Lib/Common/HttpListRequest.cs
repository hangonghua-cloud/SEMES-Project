using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lib
{
    /// <summary>
    /// 列表数据请求参数
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpListRequest<T> where T:new()
    {
        public HttpListRequest()
        {
            pagination = new HttpPagination();
            pagination.page = 1;
            pagination.rows = 60;
            pagination.sord = " asc ";
            queryJson = new T();
        }
        public HttpPagination pagination { get; set; }
        public T queryJson { get; set; }
    }
}
