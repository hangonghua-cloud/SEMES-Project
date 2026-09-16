using Lib.Dal;
using Lib.Model;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class PackingBGBll
    {
        PackingBGDal dal = new PackingBGDal();

        /// <summary>
        /// 获取分页列表(包装报工分页)
        /// </summary>
        /// <returns></returns>
        public List<dynamic> GetListWithPage(PackingBGDto query, out int record)
        {
            return dal.GetListWithPage(query, out record);
        }
        /// <summary>
        /// 获取分页列表(包装报工分页)
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTableWithPage(PackingBGDto query, out int record)
        {
            return dal.GetDataTableWithPage(query, out record);
        }
        /// <summary>
        /// 报工记录列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetPackingBGList(string workOrder)
        {
            return dal.GetPackingBGList(workOrder);
        }
        /// <summary>
        /// 包装报工记录列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public DataTable GetPackingBGDataTable(string workOrder)
        {
            return dal.GetPackingBGDataTable(workOrder);
        }
        /// <summary>
        /// 唛头列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public List<dynamic> GetPrintMarkList(string workOrder)
        {
            return dal.GetPrintMarkList(workOrder);
        }
        /// <summary>
        /// 唛头列表
        /// </summary>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public DataTable GetPrintMarkDataTable(string workOrder)
        {
            return dal.GetPrintMarkDataTable(workOrder);
        }
        /// <summary>
        /// 获取要打印的唛头列表
        /// </summary>
        /// <param name="lstMarkCode"></param>
        /// <returns></returns>
        public List<MarkEntity> GetMarkList(List<string> lstMarkCode)
        {
            return dal.GetMarkList(lstMarkCode);
        }
        /// <summary>
        /// 更新打印状态
        /// </summary>
        /// <param name="lstMark"></param>
        /// <returns></returns>
        public int UpdatePrintStatus(List<MarkEntity> lstMark)
        {
            return dal.UpdatePrintStatus(lstMark);
        }
        /// <summary>
        /// 获取唛头名称
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetMarkTemplate(string workOrder, string factoryCode)
        {
            return dal.GetMarkTemplate(workOrder, factoryCode);
        }

        /// <summary>
        /// 获取唛头名称
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetMarkCodeTemplate(string workOrder, string factoryCode)
        {
            return dal.GetMarkCodeTemplate(workOrder, factoryCode);
        }
    }
}
