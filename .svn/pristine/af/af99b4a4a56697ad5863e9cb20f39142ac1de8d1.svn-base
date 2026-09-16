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
    public partial class DataItemCategoryDelCmdHandlerShell 
    {
        /// <summary>
        /// 删除数据字典主表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemCategoryDelCmd.Response DataItemCategoryDelCmdHandler(DataItemCategoryDelCmd command)
        {
            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            try
            {
                var categoryEntity = platform.Query<IDataItemCategoryEntity>().FirstOrDefault(m => m.Id == command.Id);
                if (categoryEntity != null)//判断是否有该Id的数据
                {
                    if (categoryEntity.ParentId.ToString() == "00000000-0000-0000-0000-000000000000")//判断删除的是否为2级菜单
                    {
                        var categoryChEntity = platform.Query<IDataItemCategoryEntity>().FirstOrDefault(m => m.ParentId == categoryEntity.Id);
                        if (categoryChEntity == null)//判断是否有子菜单
                        {
                            platform.Delete(categoryEntity);
                            result = $@"删除成功!";
                            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Success!");
                            //return new DataItemCategoryDelCmd.Response() { ReturnVal = categoryEntity.CategoryCode.ToString() + " Delete success!" };
                        }
                        else
                        {
                            result = $@"删除失败，该项有子项!";
                            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Failed, Data in sub table");
                            //return new DataItemCategoryDelCmd.Response() { ReturnVal = categoryEntity.CategoryCode.ToString() + " Delete Failed,Data in sub table" };
                        }
                    }
                    else
                    {
                        var itemEntity = platform.Query<IDataItemEntity>().FirstOrDefault(m => m.CategoryCode == categoryEntity.CategoryCode);
                        if (itemEntity != null)//判断是否有子项
                        {
                            result = $@"删除失败，该项有子项!";
                            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Failed, Data in sub table");
                            //return new DataItemCategoryDelCmd.Response() { ReturnVal = categoryEntity.CategoryCode.ToString() + " Delete Failed,Data in sub table" };
                        }
                        else
                        {
                            platform.Delete(categoryEntity);
                            result = $@"删除成功!";
                            tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Success!");
                            //return new DataItemCategoryDelCmd.Response() { ReturnVal = categoryEntity.CategoryCode.ToString() + " Delete success!" };
                        }
                    }
                }
                else
                {
                    result = $@"删除失败，无法查询到该项!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Failed, Can not find 'CategoryCode'");
                    //return new DataItemCategoryDelCmd.Response() { ReturnVal = "Delete Failed,Can not find 'CategoryCode'!" };
                }
            }
            catch (Exception ex)
            {
                result = $@"删除失败：" + ex.ToString() + "!";
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemCategoryDelCmd Failed, Error(-1002): " + ex.Message);
                //return new DataItemCategoryDelCmd.Response() { ReturnVal = command.Id.ToString() + " Delete Failed,Error(-1002): " + ex.Message };
            }
            return new DataItemCategoryDelCmd.Response() { ReturnVal = result };
        }
    }
}