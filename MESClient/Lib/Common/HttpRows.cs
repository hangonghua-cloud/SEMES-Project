using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Lib
{
    /// <summary>
    /// api 返回行对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpRows<T>
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public List<T> rows { get; set; }

        public List<string> ProductCode { get; set; }
    }

    public class HttpRows1<T>
    {
        public int total { get; set; }
        public int page { get; set; }
        public int records { get; set; }
        public T rows { get; set; }
    }
}
