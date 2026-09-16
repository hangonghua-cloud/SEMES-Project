using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.AuthorizeManage.ViewModel
{
#pragma warning disable CS1570 // XML 注释出现 XML 格式错误 --“此位置无法使用字符“”。”
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2015.11.27
    /// 描 述：授权功能Url、操作Url
    /// </summary
    public class AuthorizeUrlModel
#pragma warning restore CS1570 // XML 注释出现 XML 格式错误 --“此位置无法使用字符“”。”
    {
        /// <summary>
        /// 授权主键
        /// </summary>		
        public string AuthorizeId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId { set; get; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string UrlAddress { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
    }
}
