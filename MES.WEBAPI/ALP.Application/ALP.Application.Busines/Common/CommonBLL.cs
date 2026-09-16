using ALP.Application.Service.BaseManage;
using ALP.Application.Service.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Busines.Comm
{
    public class CommonBLL
    {
        CommService commService = new CommService();
        /// <summary>
        /// 执行批量SQL--事务处理
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ExecuteBySql(List<string> listSql, out string msg)
        {
            return commService.ExecuteBySql_Trans(listSql, out msg);
        }
        /// <summary>
        /// 执行批量SQL--事务处理
        /// </summary>
        /// <param name="listSql"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool ExecuteBySql(string strSql, out string msg)
        {
            return commService.ExecuteBySql_Trans(strSql, out msg);
        }

        /// <summary>
        /// 根据单据类型获取流水号
        /// </summary>
        /// <param name="seqCode">表名称--规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string seqCode, out string returnNum, out string messageCode)
        {
            SerialNOService serialService = new SerialNOService();
            return serialService.GetSerialNO(seqCode, out returnNum, out messageCode);
        }
        /// <summary>
        /// 查询按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="BtnNames"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public DataTable FucntionAuthority(string userId, string BtnNames, out string messageCode)
        {
            SerialNOService serialService = new SerialNOService();
            return serialService.FucntionAuthority(userId, BtnNames,  out messageCode);
        }
        
    }
}
