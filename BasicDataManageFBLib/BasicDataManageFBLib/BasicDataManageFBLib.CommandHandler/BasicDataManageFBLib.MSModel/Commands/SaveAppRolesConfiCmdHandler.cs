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
    public partial class SaveAppRolesConfiCmdHandlerShell 
    {
        /// <summary>
        /// 保存移动端角色权限
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private SaveAppRolesConfiCmd.Response SaveAppRolesConfiCmdHandler(SaveAppRolesConfiCmd command)
        {
            var res = string.Empty;
            try
            {
                if (command.ModelList.Count > 0)
                {
                    var entList = new List<IAppRolesConfiEntity>();
                    foreach (var item in command.ModelList)
                    {
                        var entRo = platform.Query<IAppRolesConfiEntity>().FirstOrDefault(t => t.RoleCode == command.RoleCode && t.ModelCode == item.ModelCode);
                        if (entRo == null)
                        {
                            var entNew = platform.Create<IAppRolesConfiEntity>();
                            entNew.RoleCode = command.RoleCode;
                            entNew.ModelCode = item.ModelCode;
                            entNew.IsEnabled = item.IsEnabled;
                            entNew.CreatedByCode = command.UserCode;
                            entNew.CreatedByName = command.UserName;
                            entNew.CreatedTime = DateTime.Now;
                            entList.Add(entNew);
                            //platform.Submit(entNew);
                        }
                        else
                        {
                            entRo.IsEnabled = item.IsEnabled;
                            entRo.UpdateByCode = command.UserCode;
                            entRo.UpdateByName = command.UserName;
                            entRo.UpdateTime = DateTime.Now;
                            platform.Submit(entRo);
                        }
                    }
                    if (entList.Count > 0)
                        platform.BulkInsert(entList, new BulkOptions { Lock = LockType.RowLock });//批量插入数据
                }
                res = @"操作成功！";
            }
            catch (Exception ex)
            {
                res = @"操作失败，" + ex.ToString();
            }
            return new SaveAppRolesConfiCmd.Response() { Result = res };
        }
    }
}