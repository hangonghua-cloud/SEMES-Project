using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class BarCodeInfoEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LineCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ScheduleNO { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PrdOrderCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string MaterialName { get; set; }

        /// <summary>
        /// 条码抽检状态 0 未抽检 1 抽检20190829
        /// </summary>
        public int IsSpotCheck { get; set; }
        /// <summary>
        /// 条码状态 0 可用 1 禁用
        /// </summary>
        public int BarCodeStatus { get; set; }

        /// <summary>
        /// 抽检类型
        /// </summary>
        public string InspectionType { get; set; }
        /// <summary>
        ///  条码当前标识
        /// </summary>
        public string CurrentFlag { get; set; }

        /// <summary>
        /// 过站检验判定结果
        /// </summary>
        public string JudgeResult { get; set; }
        /// <summary>
        /// 过站检验记录主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 冻结解冻状态 0正常不做任何操作 1解冻 2冻结 条码是否参考点
        /// </summary>
        public bool IsFrozen { get; set; }

        /// <summary>
        /// 过站检验状态标识
        /// </summary>
        public string CrossingFlag { get; set; }

        /// <summary>
        /// 过站点位
        /// </summary>
        public string AcqPositionCode { get; set; }

        /// <summary>
        /// 是否借机状态 1.借 2.还(20190814)
        /// </summary>
        /// <returns></returns>
        public int? IsBorrowMachine { get; set; }

        /// <summary>
        /// 借机抽检结果状态 NG OK(20190814)
        /// </summary>
        /// <returns></returns>
        public string IsBorrowMachineFlag { get; set; }

        /// <summary>
        /// 是否静态标识0不良静态1抽检静态(20190929)
        /// </summary>
        /// <returns></returns>
        public int? IsStatic { get; set; }


        /// <summary>
        /// 20191113 是否站长升等处理 0 没做处理 1 已经站长升等
        /// </summary>
        public int? UpgradeLevel { get; set; }

        /// <summary>
        /// frozenOp 0仅删除记录，1解冻并删除记录，2解冻并插入记录。20190905
        /// </summary>
        public int IsFrozenStatus { get; set; }

        /// <summary>
        /// IsFrozen=2 设置参考点时所需要的间隔时间  20190905
        /// </summary>
        public int Interval { get; set; }


        
    }
}
