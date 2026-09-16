using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.Models.Account
{
    public class AppSystemModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 返回App版本是否需要更新
        /// </summary>
        public bool IsUpdate { get; set; }

        /// <summary>
        /// 最新版本号
        /// </summary>
        public string NewVersion { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        private string msg { get; set; }
        /// <summary>
        /// 响应消息
        /// </summary>
        public string Msg
        {
            get { return msg; }
            set { this.msg = ALP.Application.Service.Resources.Language.GetText(value); }
        }
    }

    public class AppPayloaddata
    {
        public string token { get; set; }
        public string nickname { get; set; }
        public string profile { get; set; }
        public bool hasLogin { get; set; }
        public bool isGoogle { get; set; }
        public object result { get; set; }
        public string code { get; set; }
        public string ret { get; set; }
    }

    public class AppRetLogindata
    {
        public string code { get; set; }
        private string Ret { get; set; }
        /// <summary>
        /// 响应消息
        /// </summary>
        public string ret
        {
            get { return Ret; }
            set { this.Ret = ALP.Application.Service.Resources.Language.GetText(value); }
        }
        public object retobject { get; set; }
        public string UserCode { get; set; }
        public AppPayloaddata data { get; set; }
    }

    public class AppUserInfo
    {
        public string UserCode { get; set; }
        public string UserName { get; set; }
        public string PostName { get; set; }
        public string LineName { get; set; }
        public string LineCode { get; set; }
        public string JobCode { get; set; }
        public string JobName { get; set; }
        public string Department_Name { get; set; }
        public string FactoryCode { get; set; }
        public string FactoryName { get; set; }

        public string WorkShop { get; set; }
        public string WorkShopCode { get; set; }
        public object AppModelPageList { get; set; }
        public object AppModelBtnList { get; set; }
        public object AppModelList { get; set; }
        public object AppRoleList { get; set; }
        public List<string> RoleNameList { get; set; }

    }
}