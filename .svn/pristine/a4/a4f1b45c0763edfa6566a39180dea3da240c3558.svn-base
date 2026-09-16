using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Types;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;
using Siemens.SimaticIT.UAMES.ThirdPartyDBFBLib.UMModel.DataModel.ProjectionModel;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class GetAppUserRolesCmdHandlerShell 
    {
        /// <summary>
        /// 通过用户获移动端功能页面和按钮权限
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private GetAppUserRolesCmd.Response GetAppUserRolesCmdHandler(GetAppUserRolesCmd command)
        {
            var pageList = new List<UserPageListType>();
            var btnList = new List<UserBtnListType>();
            var result = string.Empty;
            try
            {
                var entUser = platform.Query<IAppUsersEntity>().FirstOrDefault(p => p.UserCode == command.UserCode);
                if (entUser == null)
                {
                    result = @"没有获取到用户信息，如果已添加用户，请先同步!";
                    return new GetAppUserRolesCmd.Response() { UserPageList = pageList, UserBtnList = btnList, Result = result };
                }

                var entRoleRe = platform.ProjectionQuery<IV_RoleAssociation>().Where(p => p.UserId == entUser.UserId && p.IsDeleted == 0).ToList();
                var entRole = platform.ProjectionQuery<IV_UARoles>().Where(p => p.IsDeleted == 0).ToList();
                var entRoleModel = platform.Query<IAppRolesConfiEntity>().Where(p => p.IsEnabled == true);
                var entAppModel = platform.Query<IAppModelEntity>();
                if (entRoleRe.Count == 0)
                {
                    result = @"该用户没有权限，请先设置权限!";
                    return new GetAppUserRolesCmd.Response() { UserPageList = pageList, UserBtnList = btnList, Result = result };
                }
                //获取用户的所有角色
                var entRoleList = (from a in entRole
                                   join b in entRoleRe
                                   on a.Id equals b.RoleId
                                   select new
                                   {
                                       RoleName = a.Name
                                   }).ToList();
                if (entRoleList.Count == 0)
                {
                    result = @"该用户没有权限，请先设置权限!";
                    return new GetAppUserRolesCmd.Response() { UserPageList = pageList, UserBtnList = btnList, Result = result };
                }

                var roleList = new List<string>();
                foreach (var item in entRoleList)
                {
                    roleList.Add(item.RoleName);
                }
                //通过角色获取用户的所有页面按钮权限
                //var modelList = entRoleModel.Where(p => roleList.Contains(p.RoleCode) && p.IsEnabled == true).ToList();
                var modelList = (from a in entRoleModel
                                 join b in entAppModel
                                 on a.ModelCode equals b.ModelCode into c
                                 from d in c.DefaultIfEmpty()
                                 where roleList.Contains(a.RoleCode)
                                 select new
                                 {
                                     ModelCode = a == null ? "" : a.ModelCode,
                                     ModelName = d == null ? "" : d.ModelName,
                                     ModelType = d == null ? "" : d.ModelType,
                                     ModelIco = d == null ? "" : d.ModelIco,
                                     ModelColor = d == null ? "" : d.ModelColor,
                                     OwnedPage = d == null ? "" : d.OwnedPage,
                                     OrderByNum = d == null ? 0 : d.OrderByNum,
                                 }).Distinct().ToList();
                modelList = modelList.OrderBy(x => x.OrderByNum).ToList();
                foreach (var item1 in modelList)
                {
                    if (item1.ModelType == "页面")
                    {
                        var page = new UserPageListType();
                        page.ModelCode = item1.ModelCode;
                        page.ModelName = item1.ModelName;
                        page.ModelIco = item1.ModelIco;
                        page.ModelColor = item1.ModelColor;
                        page.OwnedPage = item1.OwnedPage;
                        pageList.Add(page);
                    }
                    else if (item1.ModelType == "按钮")
                    {
                        var btn = new UserBtnListType();
                        btn.ModelCode = item1.ModelCode;
                        btn.ModelName = item1.ModelName;
                        btn.ModelIco = item1.ModelIco;
                        btn.ModelColor = item1.ModelColor;
                        btn.OwnedPage = item1.OwnedPage;
                        btnList.Add(btn);
                    }
                }
                result = @"1";
            }
            catch (Exception ex)
            {
                result = @"获取用户权限信息失败，" + ex.ToString();
            }
            return new GetAppUserRolesCmd.Response() { UserPageList = pageList, UserBtnList = btnList, Result = result };
        }
    }
}