using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class DataItemCategoryAddCmdHandlerShell 
    {
        /// <summary>
        /// 新增数据字典主表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemCategoryAddCmd.Response DataItemCategoryAddCmdHandler(DataItemCategoryAddCmd command)
        {
            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            try
            {
                var newSortNum = command.SortNum;
                if (newSortNum == null)
                {
                    var maxSortNum = platform.Query<IDataItemCategoryEntity>().Where(m => m.IsDeleted == 0 && m.CategoryCode == command.CategoryCode).Max(t => t.SortNum);
                    if (maxSortNum == null)
                    {
                        newSortNum = 0;
                    }
                    else
                    {
                        newSortNum = Convert.ToInt32(maxSortNum) + 1;
                    }
                }
                var codeEntity = platform.Query<IDataItemCategoryEntity>().FirstOrDefault(m => m.CategoryCode == command.CategoryCode && m.IsDeleted == 0);
                if (codeEntity == null)
                {
                    var itemEntity = platform.Create<IDataItemCategoryEntity>();
                    itemEntity.CategoryCode = command.CategoryCode;
                    itemEntity.CategoryName = command.CategoryName;
                    itemEntity.ParentId = command.ParentId;
                    itemEntity.SortNum = newSortNum;
                    itemEntity.IsValid = command.IsValid;
                    platform.Submit(itemEntity);
                    result = $@"添加成功!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryAddCmd Success!");
                    //return new DataItemCategoryAddCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Create Success!" };
                }
                else
                {
                    result = $@"重复添加!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryAddCmd Failed, 'CategoryCode' repeat");
                    //return new DataItemCategoryAddCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Create Failed,'CategoryCode' repeat!" };
                }
            }
            catch (Exception ex)
            {
                result = $@"添加失败：" + ex.ToString() + "!";
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryAddCmd Failed, Error(-1002): " + ex.Message);
                //return new DataItemCategoryAddCmd.Response() { ReturnVal = command.CategoryCode.ToString() + " Create Failed,Error(-1002): " + ex.Message };
            }
            return new DataItemCategoryAddCmd.Response() { ReturnVal = result };
        }
    }
}
