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
using Siemens.SimaticIT.BasicDataManageFBLib.CommandHandler;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class DataItemUpdateCmdHandlerShell
    {
        /// <summary>
        /// 修改数据字典子表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemUpdateCmd.Response DataItemUpdateCmdHandler(DataItemUpdateCmd command)
        {
            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            var logEnt = new QM_LogManagementEntity()
            {
                ModuleName = "数据字典",
                LogCode = "Update",
                ModifyWay = "编辑",
                UpdateByName = platform.Principal.Identity.Name,
                UpdateDateTime = DateTimeOffset.Now
            };

            try
            {
                var itemEntity = platform.Query<IDataItemEntity>().FirstOrDefault(m => m.Id == command.Id);
                if (itemEntity != null)
                {
                    //itemEntity.CategoryCode = command.CategoryCode;
                    //itemEntity.ItemCode = command.ItemCode;
                    itemEntity.ItemName = command.ItemName;
                    itemEntity.ItemValue = command.ItemValue;

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
                    result = $@"修改成功!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemUpdateCmd Success!");
                    //return new DataItemUpdateCmd.Response() { ReturnVal = command.ItemCode.ToString() + " Update success!" };

                    logEnt.ModifyId = itemEntity.Id.ToString();
                    var strContent = $@"原字段:ItemName 内容: '{itemEntity.ItemName}' =>修改为: '{command.ItemName}',原字段:ItemValue 内容: '{itemEntity.ItemValue}' =>修改为: '{command.ItemValue}',原字段:IsValid 内容: '{itemEntity.IsValid}' =>修改为: '{command.IsValid}'";

                    logEnt.ModifyContent = strContent;

                    var postEnt = new PostEnt()
                    {
                        KeyValue = "",
                        Entity = logEnt
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(postEnt);

                    new LogPost().HttpPostData("http://10.13.131.6/sitSrvApi/QM_LogManagement/SaveQM_LogManagement", json);
                }
                else
                {
                    result = $@"查询不到数据!";
                    tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemUpdateCmd Failed, Can Not Find 'ItemCode'!");
                    //return new DataItemUpdateCmd.Response() { ReturnVal = command.ItemCode.ToString() + " Update Failed,Can Not Find 'ItemCode'!" };
                }
            }
            catch (Exception ex)
            {
                result = $@"添加失败：" + ex.ToString() + "!";
                tracer.Write("Siemens-SimaticIT-Trace-BusinessLogic", Category.Informational, " Command DataItemUpdateCmd Failed, Error(-1002): " + ex.Message);
                //return new DataItemUpdateCmd.Response() { ReturnVal = command.ItemCode.ToString() + " Update Failed,Error(-1002): " + ex.Message };
            }
            return new DataItemUpdateCmd.Response() { ReturnVal = result };
        }
    }
}
