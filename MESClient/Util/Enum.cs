using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MESClient.Util
{
    /// <summary>
    /// 打印模板名称
    /// </summary>
    public enum Template
    {
        [Description("流转卡")]
        TransferCard = 1,
        [Description("唛头码")]
        MarkCode = 2,
        [Description("唛头16K")]
        Mark16K = 3,
        [Description("唛头A4")]
        MarkA4 = 4,
        [Description("生产小组")]
        PTeam = 5,
        [Description("人员")]
        People = 6,
        [Description("物料批次")]
        MaterialBatch = 7,
        [Description("机台")]
        Machine = 8
    }

    /// <summary>
    /// 请求方法
    /// </summary>
    public enum WebApi
    {
        [Description("登录")]
        Login =1,
        [Description("修改密码")]
        UpdatePassword = 2,
        [Description("更新日志")]
        UpdateLog = 3,
    }
}
