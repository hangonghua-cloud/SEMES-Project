using ALP.Application.UtilExtend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.Tool
{
    /// <summary>
    /// 物料条码助手类
    /// </summary>
    public class BarCodeHelper
    {
        /// <summary>
        /// 物料码收料场景解析扫描的二维码信息函数
        /// </summary>
        /// <param name="mrBarCode"></param>
        /// <returns></returns>
        public static (string mrCode, string batchNo, string srCode) AnalisysMRBarCode(string mrBarCode)
        {
            if (string.IsNullOrEmpty(mrBarCode))
                return ("", "", "");
            try
            {
                string[] codeArray = mrBarCode.Split('|');
                if (codeArray.Length == 2)
                {
                    string batchNo = codeArray[0].Substring(codeArray[0].Length - 8);
                    string mrCode = codeArray[0].Substring(0, codeArray[0].Length - 8);
                    string srCode = codeArray[1];
                    return (mrCode, batchNo, srCode);
                }
            }
            catch (Exception ex)
            {
                LogExtends.WriteLog($"AnalisysMRBarCode异常：{ex.ToStr()}");
            }
            return ("", "", "");
        }
    }
}
