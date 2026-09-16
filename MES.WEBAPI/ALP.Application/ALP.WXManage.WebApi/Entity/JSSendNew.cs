using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat_WarningDTO;

namespace ALP.WebApi.Controllers.MessageManage
{
    /// <summary>
    /// 微信发送信息参数
    /// </summary>
    public class SendNew
    {
        /// <summary>
        /// 成员ID列表（消息接收者，多个接收者用‘|’分隔，最多支持1000个）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送
        /// </summary>
        public string touser { get; set; }
        /// <summary>
        /// 部门ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string toparty { get; set; }
        /// <summary>
        /// 标签ID列表，多个接收者用‘|’分隔，最多支持100个。当touser为@all时忽略本参数
        /// </summary>
        public string totag { get; set; }
        /// <summary>
        /// 消息类型，此时固定为：textcard
        /// </summary>
        public string msgtype { get; set; } = "textcard";
        /// <summary>
        /// 企业应用的id，整型
        /// </summary>
        public string agentid { get; set; }

        /// <summary>
        /// 传送文本内容
        /// </summary>
        public Contentclass text { get; set; }
        /// <summary>
        /// 消息信息
        /// </summary>
        public Textcard textcard { get; set; }
        /// <summary>
        /// 表示是否开启id转译，0表示否，1表示是，默认0
        /// </summary>
        public int enable_id_trans { get; set; } = 0;
        /// <summary>
        /// 表示是否开启重复消息检查，0表示否，1表示是，默认0
        /// </summary>
        public int enable_duplicate_check { get; set; } = 0;
        /// <summary>
        /// 表示是否重复消息检查的时间间隔，默认1800s，最大不超过4小时
        /// </summary>
        public int duplicate_check_interval { get; set; } = 1800;
    }

    public class Contentclass
    { /// <summary>
      /// 标题，不超过128个字节，超过会自动截断（支持id转译）
      /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 描述，不超过512个字节，超过会自动截断（支持id转译）
        /// </summary>
        public string content { get; set; }

    }
}
