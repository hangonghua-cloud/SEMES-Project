using ALP.Application.Service.ToSAP;
using ALP.Application.WebApi.Controllers.API;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("ToSAP")]
    public class ToSAPController : ApiBaseController
    {
        ToSAPService _toSAPService = new ToSAPService();

        [HttpPost]
        [Route("ToSAPTest")]
        public HttpResponseMessage ToSAPTest()
        {
            try
            {
                _toSAPService.ToSAPTest();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }

        }

        #region 自制半成品报工 物料消耗-SAP工单领退料
        [HttpPost]
        [Route("OwnProductBGMatConsumeToSAP")]
        public HttpResponseMessage OwnProductBGMatConsumeToSAP()
        {
            try
            {
                _toSAPService.OwnProductBGMatConsumeToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 包装报工/流转卡报工 物料消耗-SAP工单领退料
        [HttpPost]
        [Route("CardAndPackBGMatConsumeToSAP")]
        public HttpResponseMessage CardAndPackBGMatConsumeToSAP()
        {
            try
            {
                _toSAPService.CardAndPackBGMatConsumeToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 原材料发货-SAP销售交货单过账
        [HttpPost]
        [Route("RawMaterialDispatchToSAP")]
        public HttpResponseMessage RawMaterialDispatchToSAP()
        {
            try
            {
                _toSAPService.RawMaterialDispatchToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 原材料发货-SAP销售交货单冲销过账
        [HttpPost]
        [Route("RawMaterialDispatchOffSAP")]
        public HttpResponseMessage RawMaterialDispatchOffSAP()
        {
            try
            {
                _toSAPService.RawMaterialDispatchOffSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 成品发货-SAP销售交货单过账
        [HttpPost]
        [Route("ProductDispatchItemToSAP")]
        public HttpResponseMessage ProductDispatchItemToSAP()
        {
            try
            {
                _toSAPService.ProductDispatchItemToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 成品发货-SAP销售交货单冲销过账
        [HttpPost]
        [Route("ProductDispatchItemOffToSAP")]
        public HttpResponseMessage ProductDispatchItemOffToSAP()
        {
            try
            {
                _toSAPService.ProductDispatchItemOffToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 采购入库-SAP采购订单入库
        [HttpPost]
        [Route("PurchaseInWhsToSAP")]
        public HttpResponseMessage PurchaseInWhsToSAP()
        {
            try
            {
                _toSAPService.PurchaseInWhsToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 原材料出库-SAP领退料过账
        [HttpPost]
        [Route("RawMaterialOutWhsToSAP")]
        public HttpResponseMessage RawMaterialOutWhsToSAP()
        {
            try
            {
                _toSAPService.RawMaterialOutWhsToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 原材料调拨-SAP调拨过账
        [HttpPost]
        [Route("RawMaterialDBToSAP")]
        public HttpResponseMessage RawMaterialDBToSAP()
        {
            try
            {
                _toSAPService.RawMaterialDBToSAP();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion



        #region 流转卡报工-SAP IF152
        /// <summary>
        /// 流转卡报工
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TransferCardBG")]
        public HttpResponseMessage TransferCardBG()
        {
            try
            {
                _toSAPService.TransferCardBG();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 包装报工-SAP IF152
        /// <summary>
        /// 包装报工
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PackingBG")]
        public HttpResponseMessage PackingBG()
        {
            try
            {
                _toSAPService.PackingBG();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 自制半成品报工-SAP IF152
        /// <summary>
        /// 自制半成品报工
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OwnProductBG")]
        public HttpResponseMessage OwnProductBG()
        {
            try
            {
                _toSAPService.OwnProductBG();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 流转卡报工 物料消耗-SAP IF152
        /// <summary>
        /// 流转卡报工 物料消耗
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("TransferCardBG_MatConsume")]
        public HttpResponseMessage TransferCardBG_MatConsume()
        {
            try
            {
                _toSAPService.TransferCardBG_MatConsume();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 包装报工 物料消耗-SAP IF152
        /// <summary>
        /// 包装报工 物料消耗
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PackingBG_MatConsume")]
        public HttpResponseMessage PackingBG_MatConsume()
        {
            try
            {
                _toSAPService.PackingBG_MatConsume();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 自制半成品报工 物料消耗-SAP IF152
        /// <summary>
        /// 自制半成品报工 物料消耗
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OwnProductBG_MatConsume")]
        public HttpResponseMessage OwnProductBG_MatConsume()
        {
            try
            {
                _toSAPService.OwnProductBG_MatConsume();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 包装报工入库-SAP >0 IF152
        /// <summary>
        /// 包装报工入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PackingBG_InWhs")]
        public HttpResponseMessage PackingBG_InWhs()
        {
            try
            {
                _toSAPService.PackingBG_InWhs();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 包装报工入库-SAP <0 IF122
        /// <summary>
        /// 包装报工入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("PackingBG_InWhs2")]
        public HttpResponseMessage PackingBG_InWhs2()
        {
            try
            {
                _toSAPService.PackingBG_InWhs2();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 自制半成品报工入库-SAP >0 IF152
        /// <summary>
        /// 自制半成品报工入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OwnProductBG_InWhs")]
        public HttpResponseMessage OwnProductBG_InWhs()
        {
            try
            {
                _toSAPService.OwnProductBG_InWhs();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 自制半成品报工入库-SAP <0 IF122
        /// <summary>
        /// 自制半成品报工入库
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("OwnProductBG_InWhs2")]
        public HttpResponseMessage OwnProductBG_InWhs2()
        {
            try
            {
                _toSAPService.OwnProductBG_InWhs2();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 成品发货-SAP IF150、106
        /// <summary>
        /// 成品发货
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("ProductDispatch")]
        public HttpResponseMessage ProductDispatch()
        {
            try
            {
                _toSAPService.ProductDispatch();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

        #region 库存查询-SAP IF135
        /// <summary>
        /// 库存查询
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("StockQuery")]
        public HttpResponseMessage StockQuery()
        {
            try
            {
                _toSAPService.StockQuery();

                return AjaxResult(true, "操作成功！");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, ex.Message);
            }
        }
        #endregion

    }
}