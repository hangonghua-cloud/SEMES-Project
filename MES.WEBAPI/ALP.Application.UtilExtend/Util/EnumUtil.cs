using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALP.Application.UtilExtend
{
    public class EnumUtil
    {
        public static Dictionary<int, string> ToDict<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>()
                 .ToDictionary(d => Convert.ToInt32(d), d => d.ToString());            
        }
    }
}
