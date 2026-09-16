using ALP.WebApi.Controllers.MessageManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChat_WarningDTO;
using WHC.Framework.Business.Shared;
namespace ALP.WebApi.Common
{
    /// <summary>
    /// 微信接口
    /// </summary>
    public class WeChatInterface
    {
        /// <summary>
        /// 获取微信应用access_token
        /// </summary>
        /// <param name="Accesstoken"></param>
        /// <returns></returns>
        public static HttpWeChatResult GetToken(AccessToken Accesstoken)
        {
            return Http.Get<HttpWeChatResult>(Accesstoken);
        }
        /// <summary>
        /// 发送微信应用消息
        /// </summary>
        /// <param name="Accesstoken"></param>
        /// <returns></returns>
        public static HttpWeChatSendResult SendNew(string Accesstoken,object param)
        {
            return Http.Post<HttpWeChatSendResult>("",param,Accesstoken);
        }
     

    }
}
