using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace ALP.Application.Entity.BaseManage
{ 
    /// <summary>
    /// 1.创建日期: 2021-10-24
    /// 2.创建作者: admin
    /// 3.功能描述: BS_People实体
    /// 4.任务编号: 人员信息
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class BS_PeopleEntity
    { 
 
        /// <summary>
        /// ID
        /// </summary>
        public string ID {get; set; }
        /// <summary>
        /// 工厂编码
        /// </summary>
        public string FactoryCode { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string FactoryName { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code {get; set; }
 
        /// <summary>
        /// 名称
        /// </summary>
        public string Name {get; set; }
 
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex {get; set; }
 
        /// <summary>
        /// 身份证
        /// </summary>
        public string CertificateCode {get; set; }
 
        /// <summary>
        /// 电话
        /// </summary>
        public string MobilePhone {get; set; }
 
        /// <summary>
        /// 部门ID
        /// </summary>
        public string Department_ID {get; set; }
 
        /// <summary>
        /// 岗位
        /// </summary>
        public string Position_ID {get; set; }
 
        /// <summary>
        /// 超级管理员=1
        /// </summary>
        public string Job_ID {get; set; }
 
        /// <summary>
        /// 是否生效1生效
        /// </summary>
        public bool? IsEnabled {get; set; }
 
        /// <summary>
        /// 创建人
        /// </summary>
        public string Creator {get; set; }
 
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime {get; set; }
 
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string ModifyBy {get; set; }
 
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? ModifyTime {get; set; }
 
        /// <summary>
        /// 机台编码
        /// </summary>
        public string MachineCode {get; set; }
 
        /// <summary>
        /// 生产小组编码
        /// </summary>
        public string PTeamCode {get; set; }
        /// <summary>
        /// 开工工序
        /// </summary>
        public string ProcessCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
 
    }
}
