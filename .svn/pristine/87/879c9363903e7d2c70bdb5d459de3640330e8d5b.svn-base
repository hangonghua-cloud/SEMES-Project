using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class GetUAUsersCmdHandlerShell 
    {
        /// <summary>
        /// 移动端同步UA用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private GetUAUsersCmd.Response GetUAUsersCmdHandler(GetUAUsersCmd command)
        {
            var result = string.Empty;

            CookieContainer cc = new CookieContainer();
            string url1 = "";//Login
            string url2 = "";//Get All Users
            string url3 = "";//Logout

            //本地
            url1 = @"http://127.0.0.1/UMC/slwapi/login?user=MesApp&password=MesApp";
            url2 = @"http://127.0.0.1/UMC/slwapi/users";
            url3 = @"http://127.0.0.1/UMC/slwapi/logout";

            url1 = @"http://172.168.11.122/UMC/slwapi/login?user=MesApp&password=MesApp";
            url2 = @"http://172.168.11.122/UMC/slwapi/users";
            url3 = @"http://172.168.11.122/UMC/slwapi/logout";

            try
            {
                var entApp = platform.Query<IAppUsersEntity>().ToList();

                //用UA账号登录获取cookies
                SendDataByGET(url1, "", ref cc);

                //获取UA用户
                var usersStr = SendDataByPost(url2, "", ref cc);

                var serializer = new JavaScriptSerializer();
                serializer.MaxJsonLength = Int32.MaxValue;
                var str = serializer.Serialize(usersStr);
                var obj =serializer.DeserializeObject(usersStr);

                JObject js = JObject.FromObject(obj);

                //var js = JObject.Parse(str);
                var users = js["users"];
                var entList = new List<IAppUsersEntity>();

                foreach (var item in users)
                {
                    List<string> groupIds = new List<string>();
                    //获取UA用户详情
                    var _detailStr = SendDataByPost($"{url2}/{item["id"]}", "", ref cc);
                    if (!string.IsNullOrWhiteSpace(_detailStr))
                    {
                        var json_detail = JObject.Parse(_detailStr);
                        if (json_detail != null)
                        {
                            foreach (var group in json_detail["users"]["groups"])
                            {
                                groupIds.Add(group["id"].ToString());
                            }
                        }
                    }

                    var ent = entApp.FirstOrDefault(t => t.UserId == item["id"].ToString());
                    if (ent == null)
                    {
                        var entName = entApp.FirstOrDefault(t => t.UserCode == item["name"].ToString());
                        if (entName != null)
                        {
                            string userCode = item["name"]?.ToString();
                            var entDel = platform.Query<IAppUsersEntity>().FirstOrDefault(t => t.UserCode == userCode && t.IsDeleted == 0);
                            if (entDel != null)
                            {
                                entDel.IsDeleted = 1;
                                platform.Submit(entDel);
                            }
                        }
                        PassWordMD5 md5 = new PassWordMD5();
                        var entNew = platform.Create<IAppUsersEntity>();
                        entNew.UserId = item["id"].ToString();
                        entNew.UserCode = item["name"].ToString();
                        entNew.UserDesc = item["fullname"].ToString();
                        entNew.UserPWD = md5.MD5Encrypt64("123");
                        entNew.IsEnabled = true;
                        entNew.GroupIds= string.Join(",", groupIds);//用户组Id，逗号分隔
                        //platform.Submit(entNew);
                        entList.Add(entNew);
                    }
                    else
                    {
                        ent.GroupIds = string.Join(",", groupIds);//用户组Id，逗号分隔
                        entList.Add(ent);
                    }
                }
                if (entList.Count > 0)
                {
                    //platform.BulkInsert(entList, new BulkOptions { Lock = LockType.RowLock });//批量插入数据
                    platform.BulkImport(entList, new BulkImportOptions());//批量插入数据
                }

                result = @"操作成功！";

            }
            catch (Exception ex)
            {
                result = @"操作失败，" + ex.ToString();
            }
            //退出登录
            SendDataByGET(url3, "", ref cc);

            return new GetUAUsersCmd.Response() { Result = result };
        }



        /// <summary>
        /// get方法UA登录和退出登录
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string SendDataByGET(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            var js = JObject.Parse(retString);

            var re = js["result"].ToString();
            return retString;
        }

        /// <summary>
        /// post方法获取UA用户列表
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public static string SendDataByPost(string Url, string postDataStr, ref CookieContainer cookie)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            if (cookie.Count == 0)
            {
                request.CookieContainer = new CookieContainer();
                cookie = request.CookieContainer;
            }
            else
            {
                request.CookieContainer = cookie;
            }

            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = postDataStr.Length;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
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
            //string pwd = "";
            MD5 md5 = MD5.Create(); //实例化一个md5对像
                                    // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
            return Convert.ToBase64String(s);
        }
    }
}
