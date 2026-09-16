using ALP.Application.Entity.ProduceManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Dto
{
    public class TeamDto
    {
        /// <summary>
        /// 小组编码
        /// </summary>
        public string PTeamCode { get; set; }
        /// <summary>
        /// /生产小组名称
        /// </summary>
        public string PTeamName { get; set; }
        /// <summary>
        /// 工序编码
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 工序名称
        /// </summary>
        public string ProcessName { get; set; }
        /// <summary>
        /// 人员列表
        /// </summary>
        public List<PM_TeamPerson_ItemsEntity> ItemList { get; set; }
        /// <summary>
        /// 岗位列表
        /// </summary>
        public List<PostEntity> PostList { get; set; }
    }
}
