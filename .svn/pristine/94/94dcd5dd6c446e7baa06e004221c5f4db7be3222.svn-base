using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.UtilExtend.JsonUtil
{
    /// <summary>
    /// JSON格式化标准日期类型转换器
    /// </summary>
    public class FormattedDateTimeConverter : DateTimeConverterBase
    {
        /// <summary>
        /// 
        /// </summary>
        private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd" };

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dtConverter.WriteJson(writer, value, serializer);
        }
    }
}
