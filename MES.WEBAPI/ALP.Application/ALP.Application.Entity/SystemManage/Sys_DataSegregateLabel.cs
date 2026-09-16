using System;
namespace ALP.Application.Entity.SystemManage
{
	/// <summary>
	/// Sys_DataSegregateLabel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
 
	public partial class Sys_DataSegregateLabel
	{
		public Sys_DataSegregateLabel()
		{}
		#region Model
		private string _id;
		private string _labelname;
        private string _Remark;
        private string _labelcolor;
		private int? _labelvalue;
		private bool _enabledmark= false;
		private string _createuser;
		private DateTime _createdate= DateTime.Now;
		private string _modifyuser;
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
		/// 标签名称
		/// </summary>
		public string LabelName
		{
			set{ _labelname=value;}
			get{return _labelname;}
		}
		/// <summary>
		/// 标签颜色
		/// </summary>
		public string LabelColor
		{
			set{ _labelcolor=value;}
			get{return _labelcolor;}
		}
		/// <summary>
		/// 隔离标签值
		/// </summary>
		public int? LabelValue
		{
			set{ _labelvalue=value;}
			get{return _labelvalue;}
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
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
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

