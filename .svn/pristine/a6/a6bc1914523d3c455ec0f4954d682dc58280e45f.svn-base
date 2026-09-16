
using System;
using System.Collections.Generic;
using System.Linq;
using Siemens.SimaticIT.Unified.Common;
using Siemens.SimaticIT.Unified.Common.Information;
using Siemens.SimaticIT.Handler;
using Siemens.SimaticIT.Unified;
using Siemens.SimaticIT.BasicDataManageFBApp.BasicDataManageFBApp.BAPOMModel.DataModel.ReadingModel;

namespace Siemens.SimaticIT.BasicDataManageFBApp.BasicDataManageFBApp.BAPOMModel.ReadingFunctions
{
    /// <summary>
    /// Partial class init
    /// </summary>
    [Handler(HandlerCategory.BasicMethod)]
    public partial class GetAppRolesRFHandlerShell 
    {
        /// <summary>
        /// 通过角色编号获取角色移动端权限
        /// </summary>
        /// <param name="readingFunction"></param>
        /// <returns></returns>
        [HandlerEntryPoint]
        private FunctionResponse<GetAppRolesRF.FunctionResponse> GetAppRolesRFHandler(GetAppRolesRF readingFunction)
        {

            try
            {
                var entRoles = Platform.ProjectionQuery<AppRolesConfiEntity>().Where(t => t.RoleCode == readingFunction.RoleCode && t.IsDeleted == 0);
                var entModel = Platform.ProjectionQuery<AppModelEntity>().Where(t => t.IsDeleted == 0);
                var res = (from a in entModel
                           join b in entRoles
                           on a.ModelCode equals b.ModelCode into c
                           from d in c.DefaultIfEmpty()
                           select new GetAppRolesRF.FunctionResponse
                           {
                               ModelCode = a.ModelCode,
                               ModelName = a.ModelName,
                               ModelType = a.ModelType,
                               OwnedPage = a.OwnedPage,
                               IsEnabled = d == null ? false : d.IsEnabled,
                           }).ToList();
                return new FunctionResponse<GetAppRolesRF.FunctionResponse>() { Data = res.AsQueryable() };
            }
            catch (Exception ex)
            {

            }
            return new FunctionResponse<GetAppRolesRF.FunctionResponse>() { };

        }
    }
}
