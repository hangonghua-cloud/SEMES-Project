using ALP.Application.Code;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService.BaseManage;
using ALP.Application.Service.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.BaseManage
{
    public class BaseClipToolBLL
    {
        private IBaseClipToolService service = new BaseClipToolService();

        public IEnumerable<BaseClipToolEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        public IEnumerable<BaseClipToolEntity> GetListEntity(string lineCode)
        {
            return service.GetListEntity(lineCode);
        }

        public DataTable GetClips(string lineCode, string slotId)
        {
            return service.GetClips(lineCode, slotId);
        }
    }
}
