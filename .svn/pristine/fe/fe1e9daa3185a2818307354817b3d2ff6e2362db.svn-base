using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.SystemManage
{
	/// <summary>
	/// Sys_DataSegregateGroupToPerson:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary> 
	public partial class Sys_DataSegregateGroupToPerson
	{
		public Sys_DataSegregateGroupToPerson()
		{}
		#region Model
		private string _id;
		private string _groupcode;
		private string _personcode;
        private string _personname;
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
		/// 人员编号
		/// </summary>
		public string PersonCode
		{
			set{ _personcode=value;}
			get{return _personcode;}
		}
        /// <summary>
        /// 人员名称
        /// </summary>
        [NotMapped]
        public string PersonName
        {
            set { _personname = value; }
            get { return _personname; }
        }
        #endregion Model

    }
}

