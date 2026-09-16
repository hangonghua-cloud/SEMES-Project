using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;

namespace Siemens.SimaticIT.MasterData.ButtonAuthFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class RawMaterialStockManageAdjustHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private RawMaterialStockManageAdjust.Response RawMaterialStockManageAdjustHandler(RawMaterialStockManageAdjust command)
        {
            // Put your code here
            // return new RawMaterialStockManageAdjust.Response() { ... };

            throw new NotImplementedException();
        }
    }
}
