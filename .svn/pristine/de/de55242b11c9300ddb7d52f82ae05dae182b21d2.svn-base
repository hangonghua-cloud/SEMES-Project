using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;
using Siemens.SimaticIT.SDK.Diagnostics.Common;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class DataItemDelCmdHandlerShell 
    {
        /// <summary>
        /// 删除数据字典子表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemDelCmd.Response DataItemDelCmdHandler(DataItemDelCmd command)
        {
            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            try
            {
                var itemEntity = platform.Query<IDataItemEntity>().FirstOrDefault(m => m.Id == command.Id);
                if (itemEntity != null)
                {
                    platform.Delete(itemEntity);
                    result = $@"删除成功!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemDelCmd Success!");
                    //return new DataItemDelCmd.Response() { ReturnVal = itemEntity.ItemCode.ToString() + " Delete success!" };
                }
                else
                {
                    result = $@"删除失败，无法查询到该项!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemDelCmd Failed, Can not find 'ItemCode'");
                    //return new DataItemDelCmd.Response() { ReturnVal = itemEntity.ItemCode.ToString() + " Delete Failed,Can not find 'ItemCode'!" };
                }
            }
            catch (Exception ex)
            {
                result = $@"删除失败：" + ex.ToString() + "!";
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemDelCmd Failed, Error(-1002): " + ex.Message);
                //return new DataItemDelCmd.Response() { ReturnVal = command.Id.ToString() + " Delete Failed,Error(-1002): " + ex.Message };
            }
            return new DataItemDelCmd.Response() { ReturnVal = result };
        }
    }
}
