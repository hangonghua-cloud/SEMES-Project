using ALP.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ALP.Application.WebApi.Common
{
    /// <summary>
    /// 深拷贝
    /// </summary>
    public static class PubFunction
    {
        public static T DeepCopyByReflection<T>(T obj)
        {
            if (obj is string || obj.GetType().IsValueType)
                return obj;

            object retval = Activator.CreateInstance(obj.GetType());
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            foreach (var field in fields)
            {
                try
                {
                    field.SetValue(retval, DeepCopyByReflection(field.GetValue(obj)));
                }
                catch { }
            }

            return (T)retval;
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
    }
}