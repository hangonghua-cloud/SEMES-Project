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
    public partial class BaseBOMCopyHandlerShell 
    {
        /// <summary>
        /// This is the handler the MES engineer should write
        /// This is the ENTRY POINT for the user in VS IDE
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private BaseBOMCopy.Response BaseBOMCopyHandler(BaseBOMCopy command)
        {
            // Put your code here
            // return new BaseBOMCopy.Response() { ... };

            throw new NotImplementedException();
        }
    }
}
