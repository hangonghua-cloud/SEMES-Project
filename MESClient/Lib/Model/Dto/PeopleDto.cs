using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Dto
{
    public class PeopleDto
    {
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 人员编码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public string Sidx { get; set; } = "(SELECT 0)";
    }
}
