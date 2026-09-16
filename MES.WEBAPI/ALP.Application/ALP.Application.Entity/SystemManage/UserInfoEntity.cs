using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;
namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// 获取用户部门和数据隔离标签
    /// </summary>
    public partial class UserInfoEntity
    {
		public UserInfoEntity()
		{}

        /// <summary>
        /// 用户代码
        /// </summary>
        public string UserCode { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        public string DepartmentID { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string GroupLabelValue { get; set; }

        /// <summary>
        /// 是否部门授权约束
        /// </summary>
        public bool IsDeptLimit { get; set; }

    }
}

