using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
	/// PersonEntity:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    public class Sys_PersonEntity
    {
        #region Model
        private string _id;
        private string _sex;
        private string _certificatecode;
        private string _mobilephone;
        private string _department_id;
        private string _position_id;
        private string _job_id;
        private string _code;
        private string _name;
        private DateTime _updatetime;
        private bool _iseffective;
        private string _positionname;
        private string _jobname;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Sex
        {
            set { _sex = value; }
            get { return _sex; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CertificateCode
        {
            set { _certificatecode = value; }
            get { return _certificatecode; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MobilePhone
        {
            set { _mobilephone = value; }
            get { return _mobilephone; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Department_ID
        {
            set { _department_id = value; }
            get { return _department_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Position_ID
        {
            set { _position_id = value; }
            get { return _position_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Job_ID
        {
            set { _job_id = value; }
            get { return _job_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsEffective
        {
            set { _iseffective = value; }
            get { return _iseffective; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PositionName
        {
            set { _positionname = value; }
            get { return _positionname; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string JobName
        {
            set { _jobname = value; }
            get { return _jobname; }
        }
        #endregion Model


    }
}
