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
    public partial class UpdateAppModelCmdHandlerShell 
    {
        /// <summary>
        /// 更新移动端功能
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private UpdateAppModelCmd.Response UpdateAppModelCmdHandler(UpdateAppModelCmd command)
        {
            var res = string.Empty;
            try
            {
                var ent = platform.Query<IAppModelEntity>().FirstOrDefault(t => t.Id == command.Id);
                if (ent != null)
                {
                    ent.ModelName = command.ModelName;
                    ent.ModelType = command.ModelType;
                    ent.OwnedPage = command.OwnedPage;
                    ent.OwnedPageName = command.OwnedPageName;
                    ent.OrderByNum = command.OrderByNum;
                    ent.ModelIco = command.ModelIco;
                    ent.ModelIcoName = command.ModelIcoName;
                    ent.ModelIcoClass = command.ModelIcoClass;
                    ent.ModelColor = command.ModelColor;
                    ent.UpdateByCode = command.UpdateByCode;
                    ent.UpdateByName = command.UpdateByName;
                    ent.UpdateTime = DateTime.Now;
                    platform.Submit(ent);
                    res = @"操作成功！";
                }
                else
                {
                    res = @"操作失败，无法查询到该项！";
                }
                
            }
            catch (Exception ex)
            {
                res = @"操作失败，" + ex.ToString();
            }

            return new UpdateAppModelCmd.Response() { Result = res };
        }
    }
}