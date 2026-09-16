using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    public interface IBaseVirtualSlotService
    {
        IEnumerable<BaseVirtualSlotEntity> GetPageList(Pagination pagination, string queryJson);

        DataTable GetBase_VirtualSlotByLine(string lineCode);
        DataTable GetListEntity(string lineCode);
        DataTable GetSlotSelect(string mouldCode, string lineCode);

        DataTable GetSlot(string lineCode, string slotId);
    }
}
