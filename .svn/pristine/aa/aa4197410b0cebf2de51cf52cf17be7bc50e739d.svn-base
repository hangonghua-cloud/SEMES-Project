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
    public partial class AddAppModelCmdHandlerShell 
    {
        /// <summary>
        /// 添加移动端功能
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private AddAppModelCmd.Response AddAppModelCmdHandler(AddAppModelCmd command)
        {
            var res = string.Empty;
            try
            {
                var ent = platform.Query<IAppModelEntity>().FirstOrDefault(t => t.ModelCode == command.ModelCode && t.IsDeleted == 0);
                if (ent == null)
                {
                    var entNew = platform.Create<IAppModelEntity>();
                    entNew.ModelCode = command.ModelCode;
                    entNew.ModelName = command.ModelName;
                    entNew.ModelType = command.ModelType;
                    entNew.OwnedPage = command.OwnedPage;
                    entNew.OwnedPageName = command.OwnedPageName;
                    entNew.OrderByNum = command.OrderByNum;
                    entNew.ModelIco = command.ModelIco;
                    entNew.ModelIcoName = command.ModelIcoName;
                    entNew.ModelIcoClass = command.ModelIcoClass;
                    entNew.ModelColor = command.ModelColor;
                    entNew.CreatedByCode = command.CreatedByCode;
                    entNew.CreatedByName = command.CreatedByName;
                    entNew.CreatedOn = DateTime.Now;
                    entNew.CreatedTime = DateTime.Now;
                    platform.Submit(entNew);
                    res = @"操作成功！";
                }
                else
                {
                    res = @"操作失败，" + command.ModelCode + @"已存在！";
                }
            }
            catch (Exception ex)
            {
                res = @"操作失败，" + ex.ToString();
            }

            return new AddAppModelCmd.Response() { Result = res };
        }
    }
}