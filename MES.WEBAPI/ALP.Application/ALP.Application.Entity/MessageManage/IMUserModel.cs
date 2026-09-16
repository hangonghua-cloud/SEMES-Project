
namespace ALP.Application.Entity.MessageManage
{
#pragma warning disable CS1591 // 缺少对公共可见类型或成员“IMUserModel”的 XML 注释
    public class IMUserModel
#pragma warning restore CS1591 // 缺少对公共可见类型或成员“IMUserModel”的 XML 注释
    {
        #region 实体成员
        /// <summary>
        /// 用户主键
        /// </summary>		
        public string UserId { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>		
        public string RealName { get; set; }
        /// <summary>
        /// 机构主键
        /// </summary>
        public string OrganizeId { get; set; }
        /// <summary>
        /// 部门主键
        /// </summary>		
        public string DepartmentId { get; set; }
        /// <summary>
        /// 性别
        /// </summary>		
        public int Gender { get; set; } 
        /// <summary>
        /// 头像
        /// </summary>		
        public string HeadIcon { get; set; }
        /// <summary>
        /// 在线状态
        /// </summary>		
        public int UserOnLine { get; set; }
        #endregion
    }
}
