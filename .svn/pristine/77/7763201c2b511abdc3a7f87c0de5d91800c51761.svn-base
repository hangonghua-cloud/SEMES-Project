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
    public partial class UpdateAppUserCmdHandlerShell 
    {
        /// <summary>
        /// 修改移动端角色信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private UpdateAppUserCmd.Response UpdateAppUserCmdHandler(UpdateAppUserCmd command)
        {
            var result = string.Empty;
            try
            {
                var ent = platform.Query<IAppUsersEntity>().FirstOrDefault(p => p.Id == command.Id);
                if (ent != null)
                {
                    ent.IsEnabled = command.IsEnabled;
                    platform.Submit(ent);
                    result = @"操作成功！";
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

            return new UpdateAppUserCmd.Response() { Result = result };
        }
    }
}
