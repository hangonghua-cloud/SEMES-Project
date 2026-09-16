using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Data.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public class ScheduleOrderRedis
    {
        /// <summary>
        /// 
        /// </summary>
        public string ScheduleCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LineCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TechnicalPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ProFactorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Qty { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ProduceTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? MESStatus { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool? IsBegun { get; set; }
        /// <summary>
        /// 排程订单状态
        /// </summary>
        public enum MESStatuss
        {
            /// <summary>
            /// 
            /// </summary>
            完工 = -1,
            /// <summary>
            /// 
            /// </summary>
            删除 = -2,
            /// <summary>
            /// 
            /// </summary>
            取消 = -3,
            /// <summary>
            /// 
            /// </summary>
            关闭 = -5,
            /// <summary>
            /// 
            /// </summary>
            正常 = 0,
            /// <summary>
            /// 
            /// </summary>
            扫尾计划 = 1,
            /// <summary>
            /// 
            /// </summary>
            暂停 = 101,//暂停
            /// <summary>
            /// 
            /// </summary>
            生成物料配送单 = 102
        }
    }

}
