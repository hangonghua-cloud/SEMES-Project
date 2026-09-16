using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;
namespace ALP.Application.Entity.SystemManage
{
	/// <summary>
	/// Sys_DataSegregateGroup:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary> 
	public partial class Sys_DataSegregateGroup : BaseEntity
    {
		public Sys_DataSegregateGroup()
		{}
		#region Model
		private string _id;
		private string _groupcode;
		private string _groupname;
        private string _Remarks;
        private int? _grouplabelvalue;
		private bool _enabledmark= false;
		private string _createuser;
		private string _createusername;
		private DateTime _createdate= DateTime.Now;
		private string _modifyuser;
		private string _modifyusername;
		private DateTime? _modifydate;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 组编号
		/// </summary>
		public string GroupCode
		{
			set{ _groupcode=value;}
			get{return _groupcode;}
		}
		/// <summary>
		/// 组名称
		/// </summary>
		public string GroupName
		{
			set{ _groupname=value;}
			get{return _groupname;}
		}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _Remarks = value; }
            get { return _Remarks; }
        }
        /// <summary>
        /// 隔离标签值
        /// </summary>
        public int? GroupLabelValue
		{
			set{ _grouplabelvalue=value;}
			get{return _grouplabelvalue;}
		}
		/// <summary>
		/// 是否有效
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
        [NotMapped]
        public string ModifyUserName
		{
			set{ _modifyusername=value;}
			get{return _modifyusername;}
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


        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateDate = DateTime.Now;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.Id = keyValue;
            this.ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

