using System;
namespace ALP.Application.Entity.SystemManage
{
	/// <summary>
	/// Sys_DataSegregateGroupToLabel:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
 
	public partial class Sys_DataSegregateGroupToLabel
	{
		public Sys_DataSegregateGroupToLabel()
		{}
		#region Model
		private string _id;
		private string _groupcode;
		private string _labelcode;
		private string _labelcolor;
        private string _labelvalue;
		/// <summary>
		/// 
		/// </summary>
		public string Id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 数据隔离组
		/// </summary>
		public string GroupCode
		{
			set{ _groupcode=value;}
			get{return _groupcode;}
		}
		/// <summary>
		/// 标签编号
		/// </summary>
		public string LabelCode
		{
			set{ _labelcode=value;}
			get{return _labelcode;}
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
		/// 标签值
		/// </summary>
		public string LabelValue
		{
			set{ _labelvalue=value;}
			get{return _labelvalue;}
		}
		#endregion Model

	}
}

