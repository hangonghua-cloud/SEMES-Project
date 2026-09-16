using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.AppManage;
using ALP.Application.UtilExtend;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;

namespace ALP.Application.Service.AppManage
{
    /// <summary>
    /// 1.创建日期: 2021-04-10
    /// 2.创建作者: why
    /// 3.功能描述: AppModelEntity_SieService 业务服务类
    /// 4.任务编号: 
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class AppModelService : RepositoryFactory<AppRolesConfEntity>
    {
        /// <summary>
        /// 功能描述: 用户登录()
        /// 创　　建: why
        /// 创建日期: 2021-04-10
        /// 任务编号: 
        /// </summary>
        /// <returns>返回登录信息</returns>
        public AppPassWordEntity GetLogin(string UserCode, string UserPWD)
        {
            try
            {
                AppPassWordEntity re = new AppPassWordEntity();
                PassWordMD5 md = new PassWordMD5();
                var pwd = md.MD5Encrypt64(UserPWD);
                string sql = String.Format(@"select * from [FHMESDB].[dbo].[V_Sy_AppUsers] where UserCode='{0}' and UserPWD='{1}'", UserCode, pwd);
                //StringBuilder sql = new StringBuilder();
                //sql.Append(@"select a.OwnedPage,a.OwnedPageName,a.ModelCode,a.ModelName,a.ModelColor,a.ModelIco from V_Sy_AppModel a where ModelType = N'页面' ORDER BY OrderByNum asc ");
                DataTable dt = this.BaseRepository().FindTable(sql);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["IsEnabled"].ToString() == "True")
                    {
                        string sql1 = String.Format(@"SELECT * from [dbo].[BS_People] where Code='{0}'", UserCode);
                        var dt1 = this.BaseRepository().FindTable(sql1);
                        if (dt1.Rows.Count > 0)
                        {
                            //re.UserPostName = dt1.Rows[0]["Post_Name"].ToString();

                            //re.LineName = dt1.Rows[0]["Line_Name"].ToString();
                            //re.LineCode = dt1.Rows[0]["Line_Code"].ToString();
                            //re.WorkShop = dt1.Rows[0]["ShopName"].ToString();
                            //re.WorkShopCode = dt1.Rows[0]["ShopCode"].ToString();
                            re.UserName = dt1.Rows[0]["Name"].ToString();
                            //re.LineName = dt1.Rows[0]["Line_Name"].ToString();
                            //re.LineCode = dt1.Rows[0]["Line_Code"].ToString();
                            re.JobCode = dt1.Rows[0]["Job_ID"].ToString();
                            //re.JobName=dt1.Rows[0]["JobName"].ToString();
                            re.Department_Name = dt1.Rows[0]["Department_ID"].ToString();
                            re.FactoryCode = dt1.Rows[0]["FactoryCode"].ToString();
                            re.FactoryName = dt1.Rows[0]["FactoryName"].ToString();
                        }
                        re.LoginCode = true;
                        re.UserName = re.UserName;
                        re.LoginMsg = "登录成功！";
                    }
                    else
                    {
                        re.LoginCode = false;
                        re.LoginMsg = "该账号未启用，请联系管理员！";
                    }
                }
                else
                {
                    re.LoginCode = false;
                    re.LoginMsg = "账号或密码错误！";
                }
                return re;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询APP角色权限(DataTable)
        /// 创　　建: why
        /// 创建日期: 2021-04-10
        /// 任务编号: 
        /// </summary>
        /// <returns>返回角色功能列表</returns>
        public AppRoleModelsEntity GetPageDataTableList(string UserCode)
        {

            string sql = String.Format(@"select a.* from [FHMESDB].[dbo].[V_Sy_AppRolesConf] a 
                                        where UserCode='{0}' ORDER BY SortNum,OrderByNum asc;", UserCode);
            try
            {
                AppRoleModelsEntity ar = new AppRoleModelsEntity();
                var list = this.BaseRepository().FindList(sql);
                ar.AppModelPageList = from a in list
                                      where a.ModelType == "页面"
                                      group a by new { a.OwnedPage, a.OwnedPageName, a.ModelCode, a.ModelName, a.ModelColor, a.ModelIco, } into g
                                      select new
                                      {
                                          OwnedPage = g.Key.OwnedPage,
                                          OwnedPageName = g.Key.OwnedPageName,
                                          ModelCode = g.Key.ModelCode,
                                          ModelName = g.Key.ModelName,
                                          ModelColor = g.Key.ModelColor,
                                          ModelIco = g.Key.ModelIco,
                                      };
                ar.AppRoleList = from a in list
                                 where a.ModelType == "页面"
                                 group a by new { a.RoleCode, a.RoleName } into g
                                 select new
                                 {
                                     RoleCode = g.Key.RoleCode,
                                     RoleName = g.Key.RoleName,
                                 };

                ar.AppModelBtnList = from a in list
                                     where a.ModelType == "按钮"
                                     select new
                                     {
                                         OwnedPage = a.OwnedPage,
                                         OwnedPageName = a.OwnedPageName,
                                         ModelCode = a.ModelCode,
                                         ModelName = a.ModelName,
                                     };
                ar.AppModelList = from a in list
                                  where a.ModelType == "页面"
                                  group a by new { a.OwnedPage, a.OwnedPageName } into g
                                  select new
                                  {
                                      OwnedPage = g.Key.OwnedPage,
                                      OwnedPageName = g.Key.OwnedPageName
                                  };
                return ar;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述:登录用户修改密码
        /// 创　　建: 周子鑫
        /// 创建日期: 2021-08-10 13:27:37
        /// 任务编号: 修改密码
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="newPwd">新密码</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int UpdatePwd(string userCode, string newPwd, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                PassWordMD5 md = new PassWordMD5();
                var pwd = md.MD5Encrypt64(newPwd);
                StringBuilder sql = new StringBuilder();
                sql.Append($@"update MES.[dbo].[AppUsersEntity_Sie_1204211063]  set UserPWD=N'{pwd}' where UserCode=N'{userCode}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
    }
}
