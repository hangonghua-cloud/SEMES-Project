using ALP.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend.Util
{
    public class Tools
    {
        public static string CovertToDateStr(object ob)
        {
            DateTime ret = DateTime.Now;
            if (ob == null) return "";
            if (DateTime.TryParse(ob.ToString(), out ret))
            {
                return ret.ToString("yyyy-MM-dd");
            }
            return "";

        }
        public static string CovertToNextDateStr(object ob)
        {
            DateTime ret = DateTime.Now;
            if (ob == null) return "";
            if (DateTime.TryParse(ob.ToString(), out ret))
            {
                return ret.AddDays(1).ToString("yyyy-MM-dd");
            }
            return "";

        }
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }
        public static List<T> Clone<T>(object List)
        {
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, List);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as List<T>;
            }
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTableNoMap<T>(List<T> items)
        {
            var flag = true;
            var tb = new DataTable(typeof(T).Name);

            List<PropertyInfo> props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
            var propsNew = Clone(props);
            foreach (PropertyInfo prop in props)
            {
                flag = true;
                foreach (var custom in prop.CustomAttributes)
                {
                    if (custom.AttributeType.Name == typeof(NotMappedAttribute).Name)
                    {
                        propsNew.Remove(prop);
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    Type t = GetCoreType(prop.PropertyType);
                    tb.Columns.Add(prop.Name, t);
                }
            }

            foreach (T item in items)
            {
                //var entity= RemoveAttribute(item);
                var values = new object[propsNew.Count];

                for (int i = 0; i < propsNew.Count; i++)
                {
                    values[i] = propsNew[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }
        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
        public static TOut TransReflection<TIn, TOut>(TIn tIn)
        {
            TOut tOut = Activator.CreateInstance<TOut>();
            var tInType = tIn.GetType();
            foreach (var itemOut in tOut.GetType().GetProperties())
            {
                var itemIn = tInType.GetProperty(itemOut.Name); ;
                if (itemIn != null)
                {
                    itemOut.SetValue(tOut, itemIn.GetValue(tIn));
                }
            }
            return tOut;
        }

        /// <summary>
        /// DateTime转换为10位时间戳（单位：秒）
        /// </summary>
        /// <param name="dateTime"> DateTime</param>
        /// <returns>10位时间戳（单位：秒）</returns>
        public static string DateTimeToTimeStamp(DateTime dateTime)
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="startTime">yyyy-MM-dd HH:mm:ss</param>
        /// <param name="endTime">yyyy-MM-dd HH:mm:ss</param>
        /// <param name="type">Days,Hours,Minutes</param>
        /// <returns></returns>
        public static int DateTimeInterval(string startTime, string endTime, string type)
        {
            TimeSpan ts = Convert.ToDateTime(endTime) - Convert.ToDateTime(startTime);
            var interval = 0;
            switch (type)
            {
                case "Days":
                    interval = ts.Days;
                    break;
                case "Hours":
                    interval = ts.Hours;
                    break;
                case "Minutes":
                    interval = ts.Minutes;
                    break;
                default:
                    break;
            }
            return interval;
        }

        /// <summary>
        /// 利用反射查出类中所有的NotMapped注解属性，并remove生成新的类的对象实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static T RemoveAttribute<T>(T entity)
        {
            JObject jo = JObject.FromObject(entity); //将对象转为JObject对象进行操作

            Type type = typeof(T); //获取实例具体引用的数据类型
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);//获取所有Public的属性
            foreach (var item in properties)
            {
                //循环是否带有NotMapped注解
                foreach (var item2 in item.CustomAttributes)
                {
                    if (item2.AttributeType.Name == typeof(NotMappedAttribute).Name)
                    {
                        jo.Remove(item.Name);
                        break;
                    }
                }
            }
            entity = JsonConvert.DeserializeObject<T>(jo.ToJson());
            return entity;
        }

        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于初始化新实体
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="s">数据源实体</param>
        /// <returns>返回的新实体</returns>
        public static D Mapper<D, S>(S s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
        public static D Mapper<D>(dynamic s)
        {
            D d = Activator.CreateInstance<D>(); //构造新实例
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                        {
                            dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }
    }
}
