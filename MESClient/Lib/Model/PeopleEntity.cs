using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    [Serializable]
    public class PeopleEntity
    {

        /// <summary>
        /// 编码
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }

    }
}
