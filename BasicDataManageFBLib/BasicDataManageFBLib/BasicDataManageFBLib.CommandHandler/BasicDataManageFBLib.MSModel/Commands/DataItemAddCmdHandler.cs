using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;
using Siemens.SimaticIT.SDK.Diagnostics.Tracing;
using Siemens.SimaticIT.SDK.Diagnostics.Common;
using Siemens.SimaticIT.BasicDataManageFBLib.CommandHandler;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class DataItemAddCmdHandlerShell
    {
        /// <summary>
        /// 新增数据字典子表数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private DataItemAddCmd.Response DataItemAddCmdHandler(DataItemAddCmd command)
        {


            ITracer tracer = platform.Tracer;
            var result = string.Empty;
            var logEnt = new QM_LogManagementEntity()
            {
                ModuleName = "数据字典",
                LogCode = "Add",
                ModifyWay = "新增",
                UpdateByName = platform.Principal.Identity.Name,
                UpdateDateTime = DateTimeOffset.Now
            };

            try
            {
                var newSortNum = command.SortNum;
                if (newSortNum == null)
                {
                    var maxSortNum = platform.Query<IDataItemEntity>().Where(m => m.IsDeleted == 0 && m.CategoryCode == command.CategoryCode).Max(t => t.SortNum);
                    if (maxSortNum == null)
                    {
                        newSortNum = 0;
                    }
                    else
                    {
                        newSortNum = Convert.ToInt32(maxSortNum) + 1;
                    }
                }
                //如果编码没有手动录入则根据流水号自动生成
                if (string.IsNullOrEmpty(command.ItemCode))
                {
                    var codeEntity = platform.Query<IDataItemEntity>().FirstOrDefault(m => m.ItemName == command.ItemName &&
                        m.IsDeleted == 0 && m.CategoryCode == command.CategoryCode);
                    if (codeEntity != null)
                    {
                        result = $@"重复添加!";
                    }
                    else
                    {
                        var maxCode = platform.Query<IDataItemEntity>().Where(m => m.IsDeleted == 0 && m.CategoryCode == command.CategoryCode &&
                                                m.IsAuto == true).Max(t => t.ItemCode);
                        var newCode = "1001";
                        if (maxCode != null)
                        {
                            newCode = (Convert.ToInt32(maxCode) + 1).ToString();
                        }
                        var itemEntity = platform.Create<IDataItemEntity>();
                        itemEntity.CategoryCode = command.CategoryCode;
                        itemEntity.ItemCode = newCode;
                        itemEntity.ItemName = command.ItemName;
                        itemEntity.ItemValue = command.ItemValue;
                        itemEntity.SortNum = newSortNum;
                        itemEntity.IsValid = command.IsValid;
                        itemEntity.IsAuto = true;
                        platform.Submit(itemEntity);
                        result = $@"添加成功！";
                        logEnt.ModifyId = itemEntity.Id.ToString();
                        var strContent = $@"ID='{itemEntity.Id}',ItemCode='{itemEntity.ItemCode}',ItemName='{itemEntity.ItemName}',ItemValue='{itemEntity.ItemValue}'
                                            ,SortNum='{itemEntity.SortNum}',IsValid='{itemEntity.IsValid}',IsAuto='{itemEntity.IsAuto}'";
                        logEnt.ModifyContent = strContent;
                    }
                }
                else
                {
                    var codeEntity = platform.Query<IDataItemEntity>().FirstOrDefault(m => (m.ItemCode == command.ItemCode || m.ItemName == command.ItemName) &&
                        m.IsDeleted == 0 && m.CategoryCode == command.CategoryCode);
                    if (codeEntity == null)
                    {
                        var itemEntity = platform.Create<IDataItemEntity>();
                        itemEntity.CategoryCode = command.CategoryCode;
                        itemEntity.ItemCode = command.ItemCode;
                        itemEntity.ItemName = command.ItemName;
                        itemEntity.ItemValue = command.ItemValue;
                        itemEntity.SortNum = newSortNum;
                        itemEntity.IsValid = command.IsValid;
                        itemEntity.IsAuto = false;
                        platform.Submit(itemEntity);
                        result = $@"添加成功！";
                        logEnt.ModifyId = itemEntity.Id.ToString();
                        var strContent = $@"ID='{itemEntity.Id}',ItemCode='{itemEntity.ItemCode}',ItemName='{itemEntity.ItemName}',ItemValue='{itemEntity.ItemValue}'
                                            ,SortNum='{itemEntity.SortNum}',IsValid='{itemEntity.IsValid}',IsAuto='{itemEntity.IsAuto}'";
                        logEnt.ModifyContent = strContent;
                    }
                    else
                    {
                        result = $@"重复添加!";
                    }
                }

                var postEnt = new PostEnt()
                {
                    KeyValue = "",
                    Entity = logEnt
                };
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(postEnt);

                new LogPost().HttpPostData("http://10.13.131.6/sitSrvApi/QM_LogManagement/SaveQM_LogManagement", json);
            }
            catch (Exception ex)
            {
                result = $@"添加失败：" + ex.ToString() + "!";
            }
            return new DataItemAddCmd.Response() { ReturnVal = result };
        }
    }
}
