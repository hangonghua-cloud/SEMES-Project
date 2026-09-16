using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESClient.Util
{
    public class FormEnumHelper
    {
        /// <summary>
        /// 枚举转字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetEnumDesString<T>(T type)
        {
            foreach (var enumame in Enum.GetValues(typeof(T)))
            {
                var index = Convert.ToInt32(enumame);
                if (index == Convert.ToInt32(type))
                {
                    return enumame.ToString();
                }
            }
            return "";
        }
        /// <summary>
        /// 字符转枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="str"></param>
        /// <returns></returns>
        public static T? GetStringDesEnum<T>(string str) where T : struct
        {
            foreach (var enumame in Enum.GetValues(typeof(T)))
            {
                if (str == enumame.ToString())
                {
                    return (T)enumame;
                }
            }
            return null;
        }
        public static T GetStringDesEnumClass<T>(string str) where T : class
        {
            foreach (var enumame in Enum.GetValues(typeof(T)))
            {
                if (str == enumame.ToString())
                {
                    return (T)enumame;
                }
            }
            return null;
        }
    }
    /// <summary>
    /// 布局特殊处理
    /// </summary>
    public enum SpecialHandle
    {
        /// <summary>
        /// Label换行
        /// </summary>
        [Description("LabelChangeLine")]
        LabelChangeLine = 1
    }
}
