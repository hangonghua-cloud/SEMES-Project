using System;
namespace ALP.Application.Entity.Calendar
{
	/// <summary>
	/// BS_HolidayManage:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	
	public partial class BS_HolidayManage : BaseEntity
    {
		public BS_HolidayManage()
		{}
		#region Model
		private int _id;
		private int _theyear;
		private string _holidayname;
		private DateTime _holidaystart;
		private DateTime _holidayend;
		private string _creator;
		private DateTime? _creatdate= DateTime.Now;
		private string _modifier;
		private DateTime? _modifydate;
		private bool _isenable;
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
		public int TheYear
		{
			set{ _theyear=value;}
			get{return _theyear;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HolidayName
		{
			set{ _holidayname=value;}
			get{return _holidayname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime HolidayStart
		{
			set{ _holidaystart=value;}
			get{return _holidaystart;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime HolidayEnd
		{
			set{ _holidayend=value;}
			get{return _holidayend;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Creator
		{
			set{ _creator=value;}
			get{return _creator;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreatDate
		{
			set{ _creatdate=value;}
			get{return _creatdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Modifier
		{
			set{ _modifier=value;}
			get{return _modifier;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ModifyDate
		{
			set{ _modifydate=value;}
			get{return _modifydate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsEnable
		{
			set{ _isenable=value;}
			get{return _isenable;}
		}
		#endregion Model

	}
}

