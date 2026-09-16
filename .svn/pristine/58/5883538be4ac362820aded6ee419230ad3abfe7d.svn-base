using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend.JsonUtil
{
    public class JsonHelper
    {
        /// <summary>
        /// 将对象序列化为JSON格式
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>json字符串</returns>
        public static string SerializeObject(object o)
        {
            string json = JsonConvert.SerializeObject(o);
            return json;
        }

        /// <summary>
        /// 解析JSON字符串生成对象实体
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json字符串(eg.{"ID":"112","Name":"石子儿"})</param>
        /// <returns>对象实体</returns>
        public static T DeserializeJsonToObject<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(T));
            T t = o as T;
            return t;
        }

        /// <summary>
        /// 解析JSON数组生成对象实体集合
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">json数组字符串(eg.[{"ID":"112","Name":"石子儿"}])</param>
        /// <returns>对象实体集合</returns>
        public static List<T> DeserializeJsonToList<T>(string json) where T : class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader sr = new StringReader(json);
            object o = serializer.Deserialize(new JsonTextReader(sr), typeof(List<T>));
            List<T> list = o as List<T>;
            return list;
        }

        /// <summary>
        /// 反序列化JSON到给定的匿名对象.
        /// </summary>
        /// <typeparam name="T">匿名对象类型</typeparam>
        /// <param name="json">json字符串</param>
        /// <param name="anonymousTypeObject">匿名对象</param>
        /// <returns>匿名对象</returns>
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject)
        {
            T t = JsonConvert.DeserializeAnonymousType(json, anonymousTypeObject);
            return t;
        }

        private static JsonHelper _jsonHelper = new JsonHelper();

        public static JsonHelper Instance { get { return _jsonHelper; } }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
        }

        public string SerializeByConverter(object obj, params JsonConverter[] converters)
        {
            return JsonConvert.SerializeObject(obj, converters);
        }

        public T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input);
        }

        public T DeserializeByConverter<T>(string input, params JsonConverter[] converter)
        {
            return JsonConvert.DeserializeObject<T>(input, converter);
        }

        public T DeserializeBySetting<T>(string input, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(input, settings);
        }

        /// <summary>
        /// 将json转化为对象 Add 20230918 XiangLi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T ConvertToObj<T>(string json, T t)
        {
            var obj = JsonConvert.DeserializeObject(json, t.GetType());
            return (T)obj;
        }

        /// <summary>
        /// 将json转化为List Add 20230918 XiangLi
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static IList<T> ConvertJsonToList<T>(string json, IList<T> t)
        {
            var obj = JsonConvert.DeserializeObject(json, t.GetType(), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return (IList<T>)obj;
        }

        /// <summary>
        /// DataTable对象转换为List对象 Add 20230918 XiangLi
        /// </summary>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(DataTable table)
        {
            bool flag = table == null;
            IList<T> result;
            if (flag)
            {
                result = null;
            }
            else
            {
                List<DataRow> list = new List<DataRow>();
                foreach (DataRow item in table.Rows)
                {
                    list.Add(item);
                }
                result = ConvertTo<T>(list);
            }
            return result;
        }

        /// <summary>
        /// DataRow对象转换为List对象  Add 20230918 XiangLi
        /// </summary>
        /// <returns></returns>
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;
            bool flag = rows != null;
            if (flag)
            {
                list = new List<T>();
                foreach (DataRow current in rows)
                {
                    T item = CreateItem<T>(current);
                    list.Add(item);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据DataRow对象转换为类对象  Add 20230918 XiangLi
        /// </summary>
        /// <returns></returns>
        public static T CreateItem<T>(DataRow row)
        {
            T t = default(T);
            bool flag = row != null;
            if (flag)
            {
                t = Activator.CreateInstance<T>();
                foreach (DataColumn dataColumn in row.Table.Columns)
                {
                    string text = dataColumn.ColumnName;
                    PropertyInfo property = t.GetType().GetProperty(text);
                    bool flag2 = property == null;
                    if (flag2)
                    {
                        text = dataColumn.ColumnName.ToLower();
                        property = t.GetType().GetProperty(dataColumn.ColumnName.ToLower());
                    }
                    bool flag3 = property != null;
                    if (flag3)
                    {
                        try
                        {
                            object value = row[text];
                            bool flag4 = row[text] == DBNull.Value;
                            if (flag4)
                            {
                                property.SetValue(t, null, null);
                            }
                            else
                            {
                                Type type = property.PropertyType;
                                bool flag5 = type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                                if (flag5)
                                {
                                    NullableConverter nullableConverter = new NullableConverter(type);
                                    type = nullableConverter.UnderlyingType;
                                }
                                value = Convert.ChangeType(value, type);
                                property.SetValue(t, value, null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                    text = dataColumn.ColumnName;
                    FieldInfo field = t.GetType().GetField(dataColumn.ColumnName);
                    bool flag6 = field == null;
                    if (flag6)
                    {
                        text = dataColumn.ColumnName.ToLower();
                        field = t.GetType().GetField(text);
                    }
                    bool flag7 = field != null;
                    if (flag7)
                    {
                        try
                        {
                            object value2 = row[text];
                            bool flag8 = row[text] == DBNull.Value;
                            if (flag8)
                            {
                                field.SetValue(t, null);
                            }
                            else
                            {
                                Type type2 = field.FieldType;
                                bool flag9 = type2.IsGenericType && type2.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
                                if (flag9)
                                {
                                    NullableConverter nullableConverter2 = new NullableConverter(type2);
                                    type2 = nullableConverter2.UnderlyingType;
                                }
                                value2 = Convert.ChangeType(value2, type2);
                                field.SetValue(t, value2);
                            }
                        }
                        catch (Exception ex2)
                        {
                            throw ex2;
                        }
                    }
                }
            }
            return t;
        }
    }
}
