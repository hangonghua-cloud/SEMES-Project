using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    [Serializable]
    public class TeamPersonEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        [NotMapped]
        public string FactoryName { get; set; }

        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        [NotMapped]
        public string ProcessName { get; set; }

        /// <summary>
        /// 生产小组编码
        /// </summary>
        public string PTeamCode { get; set; }

        /// <summary>
        /// 生产小组名称
        /// </summary>
        public string PTeamName { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime { get; set; }
    }
}
