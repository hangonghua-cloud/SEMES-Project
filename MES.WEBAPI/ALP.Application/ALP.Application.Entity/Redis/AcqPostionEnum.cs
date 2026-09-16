using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Data.Entity
{
    /// <summary>
    /// 
    /// </summary>
    public enum AcqPostionEnum
    {
        /// <summary>
        /// 
        /// </summary>
        YZCJ = 10,
        /// <summary>
        /// 
        /// </summary>
        YJCJ = 50,
        /// <summary>
        /// 
        /// </summary>
        XXCJ = 230,
        /// <summary>
        /// 
        /// </summary>
        RKCJ = 240,
        /// <summary>
        /// 
        /// </summary>
        MTRK = 100
    }

    public enum AcqStatus
    {
        /// <summary>
        /// 未通过状态
        /// </summary>
        Miss = 0,
        /// <summary>
        /// OK状态
        /// </summary>
        Yes = 1,
        /// <summary>
        /// 非录入缺陷NG状态
        /// </summary>
        NG = 2,
        /// <summary>
        /// 返修后站点状态
        /// </summary>
        ReCheck = 3,
        /// <summary>
        /// QC检验员检验NG状态
        /// </summary>
        QCNG = 4
    }
    /// <summary>
    /// 
    /// </summary>
    public enum BarCodeStatus
    {
        /// <summary>
        /// 
        /// </summary>
        Vlaid = 0,
        /// <summary>
        /// 
        /// </summary>
        InVlalid = 1

    }

}
