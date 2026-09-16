using System;
namespace ALP.Application.Entity.Calendar
{
	/// <summary>
	/// 数据字典明细表
	/// </summary>
	
	public partial class BS_BreakTimeManage: BaseEntity
    {
		public BS_BreakTimeManage()
		{}
		#region Model
		private int _id;
		private string _shifcode;
		private string _shiftcode;
		private string _describe;
		private int _duration=0;
		private bool _isenable= false;
		private string _creator;
		private DateTime _creatdate;
		private string _modifier;
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
		public string ShiftCode
		{
			set{ _shifcode=value;}
			get{return _shifcode;}
		}	
		/// <summary>
		/// 
		/// </summary>
		public string Describe
		{
			set{ _describe=value;}
			get{return _describe;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int Duration
		{
			set{ _duration=value;}
			get{return _duration;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsEnable
		{
			set{ _isenable=value;}
			get{return _isenable;}
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
		public DateTime CreatDate
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
		#endregion Model

	}
}

