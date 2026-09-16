
using ALP.Application.WebApi.Controllers.API;
using ALP.WebApi.Models;
using System;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http;

using System.Collections.Generic;
using ALP.Application.Entity.SAPEntity;
using ALP.Application.Entity.Material;
using ALP.Application.Busines.Material;
using ALP.Application.Busines.SystemManage;
using ALP.Application.Service.Material;
using ALP.Data.Repository;
using ALP.Application.WebApi.Common;

namespace ALP.Application.WebApi.Controllers.SAPManage
{
    [RoutePrefix("Base_MaterialFactory")]
    public class Base_MaterialFactoryController : ApiBaseController
    {
        Base_MaterialFactory_Service _materialFactoryService = new Base_MaterialFactory_Service();
        Base_MaterialFacet_Service _materialFacetService = new Base_MaterialFacet_Service();

        /// <summary>
        /// 功能描述: SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-5 14:07:46
        /// 任务编号: 物料工厂属性
        /// </summary>
        /// <param name="jo">json参数, entity 实体对象数组</param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveSAPBase_MaterialFactory")]
        public HttpResponseMessage SaveSAPBase_MaterialFactory(JObject jo)
        {
            try
            {
                DtoHelper.WriteLogWorkDate("SAP物料工厂属性", "物料工厂属性SaveSAPBase_MaterialFactory接口调用时间: " + DateTime.Now.ToString("G") + ",参数" + jo);

                var data = JsonConvert.DeserializeObject<List<SAPBase_MaterialFactory>>(getValue(jo, "Entity"));

                var materialFactoryEntity = data[0].MaterialFactoryEntity;
                if (materialFactoryEntity == null)
                    return AjaxResult(false, "参数必填");

                _materialFactoryService.MaterialFactorySaveFromSAP(data);

                return AjaxResult(true, "操作成功");
            }
            catch (Exception ex)
            {
                return AjaxResult(false, "操作失败：" + ex.Message);
            }
        }
        /// <summary>
        /// 创建：jpf
        /// 创建日期：2024-3-5 16:32:21
        /// 任务名称：sap删除物料工厂属性接口
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteSAPBase_MaterialFacet")]
        public HttpResponseMessage DeleteSAPBase_MaterialFacet(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;

            if (jo == null)
            {
                result.success = false;
                result.returnMsg = "参数不能为空！";//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                //业务服务层
                var entity = JsonConvert.DeserializeObject<Base_MaterialFactoryEntity>(getValue(jo, "Entity"));
                var strSql = string.Format(@"select  * FROM Base_Material where SAPmaterialCode={0} and IsEnabled=1", entity.MaterialCode);
                var dtMaterial = new RepositoryFactory().BaseRepository().FindTable(strSql);
                if (dtMaterial.Rows.Count == 0)
                {
                    throw new Exception(entity.MaterialCode + "物料编码未同步");
                }
                entity.MaterialCode = dtMaterial.Rows[0]["MaterialCode"].ToString();
                var materialFactoryEntity = _materialFactoryService.Get_ExpressionEntity(t => t.FactoryCode == entity.FactoryCode && t.MaterialCode == entity.MaterialCode);
                if (materialFactoryEntity == null)
                {
                    result.success = false;
                    result.returnMsg = "物料编码" + entity.MaterialCode + "工厂" + entity.FactoryCode + "未找到对应得数据，无法删除";
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                //删除
                int isok = _materialFactoryService.RemoveForm(materialFactoryEntity.Id, "");
                _materialFacetService.RemoveForm(t => t.MaterialFactoryId == materialFactoryEntity.Id);
                result.success = isok > 0 ? true : false;
                if (isok > 0)
                    result.returnMsg = "删除成功";

                else
                    result.returnMsg = "删除失败";

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = "操作失败：" + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}
