namespace ALP.Application.Code
{
    public static class StockHelper
    {
        /// <summary>
        /// 根据物料编码获取ERP库存位置
        /// </summary>
        /// <param name="qcResult">质检结果</param>
        /// <param name="mrCode">物料编码</param>
        /// <returns></returns>
        public static (string erpWHCode, string erpWHName,string erpWhnbr) GetErpWhCodeByMrCode(string qcResult,string mrCode)
        {
            if (string.IsNullOrEmpty(mrCode)||mrCode.Length<2)
                return ("", "","");
      
            //2020.08.19 NIU 因仓库名称说代码对应关系错误，故重新梳理调整
            switch (qcResult)
            {    
                //case "NG":
                //    return ("148556", "不合格品库");
                //case "JJ":
                //    return ("07", "旧件库");
                //case "DZ":
                //    return ("14", "呆滞物料库");
                //case "WX":
                //    return ("124411", "维修件库");
                //default:
                //    switch (mrCode.Substring(0, 2))
                //    {
                //        case "01":
                //            return ("124403", "原材料库");
                //        case "02":
                //            return ("124413", "共耗超市库");
                //        case "03":
                //            return ("124405", "低值易耗库");
                //        case "04":
                //            return ("124407", "半成品库");
                //        case "05":
                //            return ("124408", "成品库");
                //        default:
                //            return ("", "");
                //    }

                case "NG":
                    return ("148556", "不良品库","12");
                case "JJ":
                    return ("07", "旧件库","13");
                case "DZ":
                    return ("14", "呆滞物料库","14");
                case "WX":
                    return ("124411", "维修件库","06");
                default:
                    switch (mrCode.Substring(0, 2))
                    {
                        case "01":
                            return ("124403", "原材料库","01.01");
                        case "02":
                            return ("124413", "共耗超市库","02");
                        case "03":
                            return ("124405", "低值易耗库","03");
                        case "04":
                            return ("124407", "半成品库","04");
                        case "05":
                            return ("124408", "成品库","05");
                        case "06":
                            return ("124411", "维修件库","06");
                        case "08":
                            return ("148555", "借件库","08");
                        case "10":
                            return ("124415", "大柳塔库","10");
                        case "12":
                            return ("148556", "不良品库","12");
                        case "13":
                            return ("07", "旧件库","13");
                        case "14":
                            return ("14", "呆滞物料库","14");
                        default:
                            return ("", "","");
                    }
            }
        }
    }
}