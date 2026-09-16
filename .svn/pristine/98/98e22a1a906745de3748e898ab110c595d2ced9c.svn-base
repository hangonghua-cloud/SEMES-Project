using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Types;
using Siemens.SimaticIT.MasterData.EQU_MS.MSModel.DataModel;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class GetFactoryModelCmdHandlerShell 
    {
        /// <summary>
        /// 获取工厂建模数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private GetFactoryModelCmd.Response GetFactoryModelCmdHandler(GetFactoryModelCmd command)
        {
            var result = new List<FactoryModelType>();
            var GraphLinkC = platform.Query<IEquipmentGraphLinkConfiguration>().Where(t => t.IsDeleted == 0);
            var GraphNodeC = platform.Query<IEquipmentGraphNodeConfiguration>().Where(t => t.IsDeleted == 0);
            var Configura = platform.Query<IEquipmentConfiguration>().Where(t => t.IsDeleted == 0);
            var LevelSie = platform.Query<IEquipmentLevel>().Where(t => t.IsDeleted == 0);
            //var EqAttr = platform.Query<IEquipmentConfigurationProperty>().Where(t => t.IsDeleted == 0);

            var aa = from a in GraphLinkC
                     join b in GraphNodeC
                     on a.Source_Id equals b.Id into t1
                     from t2 in t1.DefaultIfEmpty()
                     join c in GraphNodeC
                     on a.Destination_Id equals c.Id into t3
                     from t4 in t3.DefaultIfEmpty()
                     select new
                     {
                         Id = t2.Id,
                         Source_Id = a.Source_Id,
                         Destination_Id = a.Destination_Id,
                         NId = t4.EquipmentConfigurationNId,
                         FNId = t2.EquipmentConfigurationNId
                     };

            var res = (from a in GraphNodeC
                       join b in aa
                       on a.EquipmentConfigurationNId equals b.NId into t1
                       from t2 in t1.DefaultIfEmpty()
                       join c in Configura
                       on a.EquipmentConfigurationNId equals c.NId into t3
                       from t4 in t3.DefaultIfEmpty()
                       join d in LevelSie
                       on t4.LevelNId equals d.NId into t5
                       from t6 in t5.DefaultIfEmpty()
                           //join e in EqAttr
                           //on t4.Id equals e.EquipmentConfiguration_Id into t7
                           //from t8 in t7.DefaultIfEmpty()
                       select new FactoryModelType
                       {
                           Id = t4.Id,
                           ModelCode = t4.NId,
                           ModelName = t4.Name,
                           ModelDesc = t4.Description,
                           ModelLevelCode = t4.LevelNId,
                           ModelLevelName = t6.Description,
                           //ProcessPart = t8 == null ? "" : t8.PropertyValue,
                           FModelCode = t2.FNId
                       }).ToList();
            return new GetFactoryModelCmd.Response() { ReturnVal = res };
        }
    }
}
