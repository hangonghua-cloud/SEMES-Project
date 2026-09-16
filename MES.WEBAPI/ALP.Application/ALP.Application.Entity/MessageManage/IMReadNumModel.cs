
namespace ALP.Application.Entity.MessageManage
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IMReadNumModel”的 XML 注释
    public class IMReadNumModel
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IMReadNumModel”的 XML 注释
    {
        /// <summary>
        /// 消息发送者
        /// </summary>
        public string SendId { set; get; }
        /// <summary>
        /// 消息接收者
        /// </summary>
        public string UserId { set; get; }
        /// <summary>
        /// 消息数量
        /// </summary>
        public int unReadNum { set; get; }
        /// <summary>
        /// 联系人Id
        /// </summary>
        public string OtherId { set; get; }
    }
}
