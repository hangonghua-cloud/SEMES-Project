using System;
using System.ComponentModel.DataAnnotations.Schema;
using ALP.Application.Code;

namespace ALP.Application.Entity.BaseManage
{
    /// <summary>
    /// 创 建：gzq
    /// 日 期：2020-05-14 13:53
    /// 描 述：班次
    /// </summary>
    public class BaseShiftEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// Id
        /// </summary>
        /// <returns></returns>
        public Guid Id { get; set; }

        /// <summary>
        /// 车间编码
        /// </summary>
        public string WorkShopCode { get; set; }

        /// <summary>
        /// 车间名称
        /// </summary>
        public string WorkShopName { get; set; }

        /// <summary>
        /// 班次编码
        /// </summary>
        /// <returns></returns>
        public string Code { get; set; }
        /// <summary>
        /// 班次名称
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <returns></returns>
        public string BeginTime { get; set; }
        /// <summary>
        /// 开始时间小时
        /// </summary>
        [NotMapped]
        public int StartHour { get; set; }
        /// <summary>
        /// 开始时间分钟
        /// </summary>
        [NotMapped]
        public int StartMinute { get; set; }
        /// <summary>
        /// 结束时间小时
        /// </summary>
        [NotMapped]
        public int EndHour { get; set; }
        /// <summary>
        /// 结束时间分钟
        /// </summary>
        [NotMapped]
        public int EndMinute { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <returns></returns>
        public string EndTime { get; set; }
        /// <summary>
        /// 是否有效
        /// </summary>
        /// <returns></returns>
        public bool? IsEnabled { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? CreatedOn { get; set; }
        /// <summary>
        /// 创建人用户名
        /// </summary>
        /// <returns></returns>
        public string CreatedByCode { get; set; }
        /// <summary>
        /// 创建人姓名
        /// </summary>
        /// <returns></returns>
        public string CreatedByName { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        /// <returns></returns>
        public DateTimeOffset? UpdatedOn { get; set; }
        /// <summary>
        /// 修改人用户名
        /// </summary>
        /// <returns></returns>
        public string UpdatedByCode { get; set; }
        /// <summary>
        /// 修改人姓名
        /// </summary>
        /// <returns></returns>
        public string UpdatedByName { get; set; }
        #endregion

        /// <summary>
        /// 主键ID
        /// </summary>
        [NotMapped]
        public string PrimaryKey { get; set; }

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = this.CreatedOn ?? DateTimeOffset.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.CreatedByCode = OperatorProvider.Provider.Current().Account;
                this.CreatedByName = OperatorProvider.Provider.Current().UserName;
            }
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            
            this.Id = Guid.Parse(keyValue);
            this.UpdatedOn = this.UpdatedOn ?? DateTimeOffset.Now;
            if (OperatorProvider.AppUserId != null)
            {
                this.UpdatedByCode = OperatorProvider.Provider.Current().Account;
                this.UpdatedByName = OperatorProvider.Provider.Current().UserName;
            }
        }


        #endregion
    }
}