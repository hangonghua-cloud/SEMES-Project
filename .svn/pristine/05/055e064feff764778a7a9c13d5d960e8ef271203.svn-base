using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.DataModel;
using Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Types;

namespace Siemens.SimaticIT.MasterData.BasicDataManageFBLib.MSModel.Commands
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class GetTreeDataCmdHandlerShell 
    {
        /// <summary>
        /// 获取数据字典树形数据
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private GetTreeDataCmd.Response GetTreeDataCmdHandler(GetTreeDataCmd command)
        {
            var treeData = new List<GetTreeList>();
            var allItemFirEntity = platform.Query<IDataItemCategoryEntity>().ToList();
            var itemFirEntity = allItemFirEntity.Where(m => m.ParentId == new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(m => m.SortNum).ToList();
            var itemSenEntity = allItemFirEntity.Where(m => m.ParentId != new Guid("00000000-0000-0000-0000-000000000000")).OrderBy(m => m.SortNum).ToList();
            foreach (IDataItemCategoryEntity itemFir in itemFirEntity)
            {
                var _treeFirData = new GetTreeList();
                _treeFirData.id = itemFir.Id;
                _treeFirData.label = itemFir.CategoryName;
                _treeFirData.ParentId = itemFir.ParentId.ToString();
                _treeFirData.CategoryCode = itemFir.CategoryCode;
                _treeFirData.CategoryName = itemFir.CategoryName;
                _treeFirData.SortNum = itemFir.SortNum;
                _treeFirData.icon = "Diagram";
                _treeFirData.expanded = false;
                _treeFirData.children = new List<GetTreeList>();
                foreach (IDataItemCategoryEntity itemSec in itemSenEntity)
                {
                    if (itemSec.ParentId == itemFir.Id)
                    {
                        var _treeSenData = new GetTreeList();
                        _treeSenData.id = itemSec.Id;
                        _treeSenData.label = itemSec.CategoryName;
                        _treeSenData.ParentId = itemSec.ParentId.ToString();
                        _treeSenData.CategoryCode = itemSec.CategoryCode;
                        _treeSenData.CategoryName = itemSec.CategoryName;
                        _treeSenData.SortNum = itemSec.SortNum;
                        _treeSenData.icon = "Certificate";
                        _treeSenData.expanded = false;
                        _treeSenData.children = new List<GetTreeList>();

                        _treeFirData.children.Add(_treeSenData);
                    }
                }
                treeData.Add(_treeFirData);
            }
            return new GetTreeDataCmd.Response() { TreeData = treeData };
        }
    }
}
