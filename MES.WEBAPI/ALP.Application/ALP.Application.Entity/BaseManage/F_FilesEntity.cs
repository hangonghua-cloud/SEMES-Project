using System;
namespace ALP.Application.Entity.BaseManage
{
	/// <summary>
	/// F_Files:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary> 
	public partial class F_FilesEntity
	{
		public F_FilesEntity()
		{}
		#region Model
		private string _id;
		private string _fid;
		private string _ftype;
		private string _fname;
		private string _fpath;
		private DateTime? _createtime= DateTime.Now;
		private string _createuser;
		private string _fextension;
		private string _ext1;
		private string _ext2;
		/// <summary>
		/// 
		/// </summary>
		public string ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 父ID
		/// </summary>
		public string FID
		{
			set{ _fid=value;}
			get{return _fid;}
		}
		/// <summary>
		/// 文件分类
		/// </summary>
		public string FType
		{
			set{ _ftype=value;}
			get{return _ftype;}
		}
		/// <summary>
		/// 文件名称
		/// </summary>
		public string FName
		{
			set{ _fname=value;}
			get{return _fname;}
		}
		/// <summary>
		/// 文件路径
		/// </summary>
		public string FPath
		{
			set{ _fpath=value;}
			get{return _fpath;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CreateTime
		{
			set{ _createtime=value;}
			get{return _createtime;}
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
		/// 扩展名称
		/// </summary>
		public string FExtension
		{
			set{ _fextension=value;}
			get{return _fextension;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ext1
		{
			set{ _ext1=value;}
			get{return _ext1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Ext2
		{
			set{ _ext2=value;}
			get{return _ext2;}
		}
		#endregion Model

	}
}

