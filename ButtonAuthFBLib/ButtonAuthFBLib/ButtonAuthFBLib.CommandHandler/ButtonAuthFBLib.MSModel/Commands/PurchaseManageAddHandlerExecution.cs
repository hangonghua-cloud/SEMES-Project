using System;

using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified;

namespace Siemens.SimaticIT.MasterData.ButtonAuthFBLib.MSModel.Commands
{
    /// <summary>
    /// Class initialize
    /// </summary>
    public partial class PurchaseManageAddHandlerShell : ICommandHandler
    {
        private IUnifiedSdk platform;
        
        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="unifiedSdk"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        public Response Execute(IUnifiedSdk unifiedSdk, ICommand command)
        {
            platform = unifiedSdk;

            return PurchaseManageAddHandler((PurchaseManageAdd)command);
        }

        /// <summary>
        /// Retrieve the type of the command
        /// </summary>
        public global::System.Type GetCommandType()
        {
            return typeof(PurchaseManageAdd);
        }
    }
}
