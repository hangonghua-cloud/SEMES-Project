//=====================================================================================
// All Rights Reserved , Copyright © Learun 2013
//=====================================================================================

using System;
using System.Security.Cryptography;
using System.Text;

namespace ALP.Application.UtilExtend
{
    /// <summary>
    /// MD5加密帮助类
    /// 版本：2.0
    /// <author>
    ///		<name>shecixiong</name>
    ///		<date>2013.09.27</date>
    /// </author>
    /// </summary>
    public class Md5Helper
    {
        #region "MD5加密"
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <param name="code">加密位数16/32</param>
        /// <returns></returns>
        public static string MD5(string str, int code)
        {
            string strEncrypt = string.Empty;
            if (code == 16)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").Substring(8, 16);
            }

            if (code == 32)
            {
                strEncrypt = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
            }

            return strEncrypt;
        }
        #endregion


    }
    public class PassWordMD5
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public string MD5Encrypt64(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 3)
            {
                return "密码格式不正确";
            }
            string cl = password + "alp";
            //实例化一个md5对像
            //string pwd = "";
            MD5 md5 = MD5.Create();
            // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}