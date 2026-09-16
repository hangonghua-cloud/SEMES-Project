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
    public partial class DelAppModelCmdHandlerShell 
    {
        /// <summary>
        /// 删除移动端功能
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DelAppModelCmd.Response DelAppModelCmdHandler(DelAppModelCmd command)
        {
            var result = string.Empty;
            try
            {
                var itemEntity = platform.Query<IAppModelEntity>().FirstOrDefault(m => m.Id == command.Id);
                if (itemEntity != null)
                {
                    platform.Delete(itemEntity);
                    result = $@"删除成功!";
                }
                else
                {
                    result = $@"删除失败，无法查询到该项!";
                }
            }
            catch (Exception ex)
            {
                result = $@"删除失败：" + ex.ToString() + "!";
            }
            return new DelAppModelCmd.Response() { Result = result };
        }
    }
}
