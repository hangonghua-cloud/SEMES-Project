using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;
namespace ALP.Application.Entity.SystemManage
{
	/// <summary>
	/// BS_APPRole:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	public partial class APPRoleEntity : BaseEntity
    {
		public APPRoleEntity()
		{}
		#region Model
		private int _id;
		private string _rolecode;
		private string _rolename;
		private bool _enabledmark= false;
		private string _createuser;
		private string _createusername;
		private DateTime _createdate= DateTime.Now;
		private string _modifyuser;
		private DateTime? _modifydate;
		/// <summary>
		/// 
		/// </summary>
		public int Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleCode
		{
			set{ _rolecode=value;}
			get{return _rolecode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RoleName
		{
			set{ _rolename=value;}
			get{return _rolename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool EnabledMark
		{
			set{ _enabledmark=value;}
			get{return _enabledmark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CreateUser
		{
			set{ _createuser=value;}
			get{return _createuser;}
		}
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string CreateUserName
		{
			set{ _createusername=value;}
			get{return _createusername;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CreateDate
		{
			set{ _createdate=value;}
			get{return _createdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ModifyUser
		{
			set{ _modifyuser=value;}
			get{return _modifyuser;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ModifyDate
		{
			set{ _modifydate=value;}
			get{return _modifydate;}
		}
		#endregion Model

	}
}

