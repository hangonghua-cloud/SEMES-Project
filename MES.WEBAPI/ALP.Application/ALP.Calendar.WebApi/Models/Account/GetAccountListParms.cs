using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.Models.Account
{
    public class GetAccountListParms
    {
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 姓
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// 工牌
        /// </summary>
        public string UserEnCode { get; set; }
        /// <summary>
        /// 分页对象
        /// </summary>
        public Pagination PagingParms { get; set; }
    }
}