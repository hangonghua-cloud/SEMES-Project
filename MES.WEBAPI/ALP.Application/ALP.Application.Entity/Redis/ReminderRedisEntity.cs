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
    public class ReminderRedisEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string PositionCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string LineCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ScheduleNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Content { get; set; }
    }
}
