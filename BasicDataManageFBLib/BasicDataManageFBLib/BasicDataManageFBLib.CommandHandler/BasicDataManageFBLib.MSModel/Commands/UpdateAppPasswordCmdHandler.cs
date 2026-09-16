using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class UpdateAppPasswordCmdHandlerShell 
    {
        /// <summary>
        /// 修改移动端登录密码
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private UpdateAppPasswordCmd.Response UpdateAppPasswordCmdHandler(UpdateAppPasswordCmd command)
        {
            var result = string.Empty;
            try
            {
                var ent = platform.Query<IAppUsersEntity>().FirstOrDefault(p => p.Id == command.Id);
                if (ent != null)
                {
                    PassWordMD5 md5 = new PassWordMD5();
                    var oldPWD = md5.MD5Encrypt64(command.OldPassword);
                    var entPWD = ent.UserPWD;
                    if (oldPWD == entPWD && entPWD != "密码格式不正确" && oldPWD != "密码格式不正确")
                    {
                        ent.UserPWD = md5.MD5Encrypt64(command.NewPassword);
                        platform.Submit(ent);
                        result = @"1";
                    }
                    else
                    {
                        result = @"原密码错误，请重新录入！";
                    }
                }
                else
                {
                    result = @"操作失败，没有查询到用户信息！";
                }
            }
            catch (Exception ex)
            {
                result = @"操作失败，" + ex.ToString();
            }

            return new UpdateAppPasswordCmd.Response() { Result = result };
        }
    }
}