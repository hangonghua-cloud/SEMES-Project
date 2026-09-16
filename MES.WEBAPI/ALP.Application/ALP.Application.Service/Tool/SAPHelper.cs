
using ALP.Application.Entity.HTTPEntity;
using ALP.Application.Service.BaseManage;
using ALP.Application.UtilExtend.Util;
using ALP.Util.Entitys;
using ALP.Util.Enums;
using ALP.Util.HttpInterface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ALP.Util.HttpInterface.HttpHelperSAP;

namespace ALP.Application.Service.Helper
{
    /// <summary>
    /// SAP同步帮助类
    /// </summary>
    public class SAPHelper
    {
        private HttpHelperSAP helper = new HttpHelperSAP();
        private Base_KeyParameterItem_Service common = new Base_KeyParameterItem_Service();
        public static SAPHelper Instance = new SAPHelper();

        /// <summary>
        /// SAP通用参数
        /// </summary>
        /// <returns></returns>
        public SAPSettings getSetting()
        {
            SAPSettings rtn = new SAPSettings();
            rtn.success = true;
            try
            {
                var keyItem = common.Get_ExpressionList(t => t.EnCode == "SAP").ToList();
                // 地址 
                rtn.Url = keyItem.FirstOrDefault().ItemValue;
                rtn.Username = keyItem.FirstOrDefault().Col1;
                rtn.Pwd = keyItem.FirstOrDefault().Col2;
            }
            catch (Exception)
            {
                rtn.success = false;
            }

            return rtn;
        }

        /// <summary>
        /// MES->SAP
        /// </summary>
        /// <param name="INIF_ID">接口标识</param>
        /// <param name="sapRequest">请求参数</param>
        /// <returns></returns>
        public (bool Flag, string Msg, string Data) PostToSAP(string INIF_ID, object sapRequest)
        {
            string msg = string.Empty;

            SAPSettings SapDro = getSetting();
            if (!SapDro.success)
            {
                msg = "SAP配置获取失败";
                return (false, msg, "");
            }

            try
            {
                var param_json = JsonConvert.SerializeObject(sapRequest);
                CommonLog.WriteLogWorkDate(INIF_ID, "MES->SAP接口调用时间: " + DateTime.Now.ToString("G") + ",请求参数：" + param_json);
                DataResultEntity retStr = helper.PostToSAP(sapRequest, SapDro);
                CommonLog.WriteLogWorkDate(INIF_ID, "MES->SAP接口返回时间: " + DateTime.Now.ToString("G") + ",返回参数：" + retStr.Result.ToString());
                var ret = JsonConvert.DeserializeObject<SAPResponseDto>(retStr.Result.ToString());
                if (ret.RSP_DATA.STATUS == "S")
                {
                    msg = "过账成功！";
                    return (true, msg, ret.RSP_DATA.OUT_DATA);
                }
                else
                {
                    msg = "过账失败：" + ret.RSP_DATA.MESSAGE;
                    return (false, msg, ret.RSP_DATA.OUT_DATA);
                }
            }
            catch (Exception ex)
            {
                msg = "过账异常：" + ex.Message;
                CommonLog.WriteLogWorkDate(INIF_ID, "MES->SAP接口调用时间: " + DateTime.Now.ToString("G") + ",返回异常：" + msg);
                return (false, msg, "");
            }
        }
    }
}
