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
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ALP.Application.Busines.BaseManage
{
    public class BaseVirtualSlotBLL
    {
        private IBaseVirtualSlotService service = new BaseVirtualSlotService();

        public IEnumerable<BaseVirtualSlotEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        public DataTable GetBase_VirtualSlotByLine(string lineCode)
        {
            return service.GetBase_VirtualSlotByLine(lineCode);
        }
        public DataTable GetSlot(string lineCode, string slotId)
        {
            return service.GetSlot(lineCode, slotId);
        }

        public DataTable GetListEntity(string lineCode)
        {
            DataTable dt = service.GetListEntity(lineCode);
            foreach(DataRow dr in dt.Rows)
            {
                string _s = Regex.Replace(dr["profactordescr"].ToStr(), @"[\u4e00-\u9fa5]", ""); //去除汉字
                dr["profactordescr"] = _s;
            }
            return dt;
        }

        public DataTable GetSlotSelect(string mouldCode, string lineCode)
        {
            DataTable dt = service.GetSlotSelect(mouldCode, lineCode);
            foreach (DataRow dr in dt.Rows)
            {
                string _s = Regex.Replace(dr["slotname"].ToStr(), @"[\u4e00-\u9fa5]", ""); //去除汉字
                dr["slotname"] = _s;
            }
            return dt;
        }
    }
}
