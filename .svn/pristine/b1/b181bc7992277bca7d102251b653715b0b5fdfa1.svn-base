using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend
{
    public static class StringExtends
    {
        /// <summary>
        /// 格式化为可执行SQL语句字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FormatSQLQuery(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return "";
            return str.Replace("'", "''");
        }

        /// <summary>
        /// 字符串转long类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            long result = 0;
            long.TryParse(str, out result);
            return result;
        }

        /// <summary>
        /// 根据分隔符分割为字符串数组【支持的分隔符包括： ,，;；|、】
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitStr(this string str)
        {
            List<string> array = new List<string>();
            if (string.IsNullOrWhiteSpace(str))
                return array.ToArray();
            array.AddRange(str.Split(new char[] { ',', '，', ';', '；', '|', '、' }));
            return array.ToArray();
        }
    }
}
