using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model.Role
{
    public class RoleEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public List<RolePermissionEntity> PermissionList { get; set; }
    }
    public class RolePermissionEntity
    {
        /// <summary>
        /// 权限Code 一般代表控件或Form的Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限名称 一般代表控件或Form的Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 展示顺序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 是否按钮权限
        /// </summary>
        public List<string> ButtonList { get; set; }
        /// <summary>
        /// 是否含有下级权限
        /// </summary>
        public List<RolePermissionEntity> Children { get; set; }
    }
}
