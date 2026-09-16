using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.Models.System
{
    /// <summary>
    /// 
    /// </summary>
    public class GetStationParms
    {
        /// <summary>
        /// 车间编码
        /// </summary>
        public string WorkShopCode { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode { get; set; }
    }
}