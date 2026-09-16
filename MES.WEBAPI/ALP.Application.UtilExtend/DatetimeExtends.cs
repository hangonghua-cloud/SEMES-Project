using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend
{
    /// <summary>
    /// 日期拓展
    /// </summary>
    public static class DatetimeExtends
    {
        public static string FormatWorkDate(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd hh:mm:ss");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string FormatWorkDate(this DateTimeOffset? dateTime)
        {
            if (dateTime.HasValue)
                return dateTime.Value.ToString("yyyy/MM/dd HH:mm:ss");
            return "";
        }
    }
}
