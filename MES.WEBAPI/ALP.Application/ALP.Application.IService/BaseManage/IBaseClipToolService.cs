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
    public interface IBaseClipToolService
    {
        IEnumerable<BaseClipToolEntity> GetPageList(Pagination pagination, string queryJson);
        IEnumerable<BaseClipToolEntity> GetListEntity(string lineCode);
        DataTable GetClips(string lineCode, string slotId);
    }
}
