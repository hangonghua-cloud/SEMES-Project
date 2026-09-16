using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.SystemManage
{
    /// <summary>
    /// Sys_Departments:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>  
    public class Sys_DepartmentEntity
    {
        #region Model
        private string _id;
        private string _parentnodeid;
        private DateTime _updatetime;
        private bool _iseffective;
        private string _code;
        private string _name;
        private string _updateUser;
        /// <summary>
        /// 
        /// </summary>
        public string ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ParentNodeID
        {
            set { _parentnodeid = value; }
            get { return _parentnodeid; }
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
        public string UpdateUser
        {
            set { _updateUser = value; }
            get { return _updateUser; }
        }
        
        #endregion Model
    }
}
