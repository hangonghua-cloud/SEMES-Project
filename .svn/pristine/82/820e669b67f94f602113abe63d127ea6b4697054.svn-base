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
    public partial class DataItemCategoryUpdateCmdHandlerShell 
    {
        /// <summary>
        /// 修改数据字典主表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemCategoryUpdateCmd.Response DataItemCategoryUpdateCmdHandler(DataItemCategoryUpdateCmd command)
        {
            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            try
            {
                var itemEntity = platform.Query<IDataItemCategoryEntity>().FirstOrDefault(m => m.Id == command.Id);
                if (itemEntity != null)
                {
                    //itemEntity.CategoryCode = command.CategoryCode;
                    itemEntity.CategoryName = command.CategoryName;
                    if (command.SortNum == null)
                    {
                        itemEntity.SortNum = 0;
                    }
                    else
                    {
                        itemEntity.SortNum = command.SortNum;
                    }
                    itemEntity.IsValid = command.IsValid;
                    platform.Submit(itemEntity);
                    result = $@"{command.Id} 修改成功!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryUpdateCmd Success!");
                    //return new DataItemCategoryUpdateCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Update success!" };
                }
                else
                {
                    result = $@"{command.Id} 查询不到数据!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryUpdateCmd Failed, Can Not Find 'CategoryCode'!");
                    //return new DataItemCategoryUpdateCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Update Failed,Can Not Find 'CategoryCode'!" };
                }
            }
            catch (Exception ex)
            {
                result = $@"{command.Id} 添加失败：" + ex.ToString() + "!";
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryUpdateCmd Failed, Error(-1002): " + ex.Message);
                //return new DataItemCategoryUpdateCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Create Failed,Error(-1002): " + ex.Message };
            }
            return new DataItemCategoryUpdateCmd.Response() { ReturnVal = result };
        }
    }
}
