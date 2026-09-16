using ALP.Application.Busines.ModelLevel;
using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.Service.ModelLevel;
using ALP.Application.Service.Resources;
using ALP.Application.WebApi.Controllers.API;
using ALP.Util.WebControl;
using ALP.WebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace ALP.Application.WebApi.Controllers.LevelManage
{
    public class LevelController : ApiBaseController
    {
        // GET: Level
        [HttpGet]
        [Route("level/test")]
        public HttpResponseMessage test()
        {
            HttpResponseMessage result1 = new HttpResponseMessage
            {
                //Content = new StringContent(BuildReturn(result, detailInfo, PUUID), Encoding.GetEncoding("UTF-8"), "application/json")
                Content = new StringContent(Language.GetText("LevelManage.LevelController.Tips_1") + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Encoding.GetEncoding("UTF-8"), "application/json")//测试成功  0001
            };
            return result1;
        }
        /// <summary>
        /// 分类列表 
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [Route("level/GetTreeJson")]
        public HttpResponseMessage GetTreeJson(string keyword = "")
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {
                Level_BLL leveBll = new Level_BLL();
                string msg = "";
                List<TreeEntity> treeList = new List<TreeEntity>();
                Dictionary<string, string> map = new Dictionary<string, string>();
                map.Add("ResourceName", keyword);
                DataTable data = leveBll.Get_Data(map, out msg);
                if (data != null && data.Rows.Count > 0)
                {
                    GetTreeList("0", data, treeList, out treeList);
                }
                result.resultData = treeList;
                result.success = treeList.Count > 0 ? true : false;
                result.returnMsg = treeList.Count > 0 ? "查询成功" : "查无[keyword=" + keyword + Language.GetText("LevelManage.LevelController.Tips_2");//]的数据
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                //WriteLog("ex.Message:   " + ex.Message);
                //return HandleException(ex, ex.Message.ToString());
                result.resultData = null;
                result.success = false;
                result.returnMsg = "获取[keyword=" + keyword + Language.GetText("LevelManage.LevelController.Tips_3") + ex.Message;//]的数据失败：
                return Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
        }
        /// <summary>
        /// jpf
        /// 通过工厂查找仓库层级
        /// 2022-12-15 
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("level/GetWhsNameByFactory")]
        public HttpResponseMessage GetWhsNameByFactory(JObject jo)
        {
            var result = new ResponseResult();
            result.resultData = null;
            try
            {

                string factoryCode = getValue(jo, "factoryCode");


                var data = new LeverService().GetWhsNameByFactory(factoryCode);



                result.resultData = data;
                result.success = true;
                result.returnMsg = Language.GetText("Common.SearchSuccess");//查询成功
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.SearchError2") + ex.Message;//查询失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
        /// <summary>
        /// 递归查询树结构
        /// </summary>
        /// <param name="fatherId"></param>
        /// <param name="DT"></param>
        /// <param name="listTree"></param>
        /// <param name="SourceList"></param>
        private void GetTreeList(string fatherId, DataTable DT, List<TreeEntity> listTree, out List<TreeEntity> SourceList)
        {
            SourceList = listTree;
            var treeDataRow = DT.Select($"ParentResource='{fatherId}'");
            if (treeDataRow != null && treeDataRow.Length > 0)
            {
                foreach (DataRow item in treeDataRow)
                {
                    string fid = item["ResourceCode"].ToString();
                    TreeEntity tree = new TreeEntity();
                    tree.id = fid;// item.ItemId;
                    tree.text = item["ResourceName"].ToString();// item.ItemName;
                    tree.value = fid;// item.ItemCode;
                    tree.parentId = item["ParentResource"].ToString();// item.ParentId;
                    tree.isexpand = false;
                    tree.complete = true;
                    tree.Attribute = "isTree";
                    tree.AttributeValue = item["ModelLeve"].ToString();
                    var sonDR = DT.Select($"ParentResource='{fid}'");
                    bool hasChildren = sonDR.Length > 0 ? true : false;
                    tree.hasChildren = hasChildren;
                    SourceList.Add(tree);
                    if (hasChildren)
                    {
                        GetTreeList(fid, DT, SourceList, out SourceList);
                    }
                }
            }
        }

        /// <summary>
        /// 分类列表 
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/Get_FieldData")]
        public HttpResponseMessage Get_FieldData(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "LevelCode");
            string ResourceCode = getValue(jo, "ResourceCode");
            try
            {
                if (string.IsNullOrEmpty(ResourceCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_7");//ResourceCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (string.IsNullOrEmpty(LevelCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_8");//LevelCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ResourceCode))
                    map.Add("ResourceCode", ResourceCode);
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("LevelCode", LevelCode);
                string msg = "";
                DataTable dt = leveBll.Get_FieldData(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 分类列表 
        /// </summary>
        /// <param name="keyword">关键字查询</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/Get_FieldData2")]
        public HttpResponseMessage Get_FieldData2(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "LevelCode");
            string ResourceCode = getValue(jo, "ResourceCode");
            try
            {
                if (string.IsNullOrEmpty(ResourceCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_7");//ResourceCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (string.IsNullOrEmpty(LevelCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_8");//LevelCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ResourceCode))
                    map.Add("ResourceCode", ResourceCode);
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("LevelCode", LevelCode);
                string msg = "";
                DataTable dt = leveBll.Get_FieldData2(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 取指定层级下的所有资源 
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/Get_ModelResourceExtendInfo_ByLevelCode")]
        public HttpResponseMessage Get_ModelResourceExtendInfo_ByLevelCode(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "LevelCode");
            string name = getValue(jo, "Name");
            string role = getValue(jo, "role");
            try
            {

                if (string.IsNullOrEmpty(LevelCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_8");//LevelCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (string.IsNullOrEmpty(CurrentAccount.UserCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_10");//登录人不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("LevelCode", LevelCode);
                if (!string.IsNullOrEmpty(name))
                    map.Add("Name", name);
                if (!string.IsNullOrEmpty(role))
                    map.Add("role", role);
                //登录人
                map.Add("userCode", CurrentAccount.UserCode);

                string msg = "";
                DataTable dt = leveBll.Get_ModelResourceExtendInfo_ByLevelCode(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取指定厂库
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        /// 
        [HttpPost]
        [Route("level/Get_TargetWhsName")]
        public HttpResponseMessage Get_TargetWhsName(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "FieldCode");
            string name = getValue(jo, "FieldValue");
            try
            {
                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("FieldCode", LevelCode);
                if (!string.IsNullOrEmpty(name))
                    map.Add("FieldValue", name);


                string msg = "";
                DataTable dt = leveBll.GetDynamicModelWithResource1(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;




            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }
        /// <summary>
        /// 通过工厂获取虚拟仓库库位
        /// jpf add 2022-12-6 
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetWhoseLocationByFac")]
        public HttpResponseMessage GetWhoseLocationByFac(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string factoryCode = getValue(jo, "FactoryCode");

            //string ResourceCode = getValue(jo, "ResourceCode");
            try
            {
                //if (string.IsNullOrEmpty(ResourceCode))
                //{
                //    result.success = false;
                //    result.returnMsg = "ResourceCode 参数不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}

                if (string.IsNullOrEmpty(factoryCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_11");//工厂 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }


                string msg = "";
                DataTable dt = new LeverService().GetWhoseLocationByFac(factoryCode, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 取指定层级下的所有资源 
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/Get_ResourceExtendByLevelCode")]
        public HttpResponseMessage Get_ResourceExtendByLevelCode(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "LevelCode");
            string FieldCode = getValue(jo, "FieldCode");
            string FieldValue = getValue(jo, "FieldValue");
            string Describe = getValue(jo, "Describe"); //描述
            //string ResourceCode = getValue(jo, "ResourceCode");
            try
            {
                //if (string.IsNullOrEmpty(ResourceCode))
                //{
                //    result.success = false;
                //    result.returnMsg = "ResourceCode 参数不能为空！";
                //    return Request.CreateResponse(HttpStatusCode.OK, result);
                //}

                if (string.IsNullOrEmpty(LevelCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_8");//LevelCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                //if (!string.IsNullOrEmpty(ResourceCode))
                //    map.Add("ResourceCode", ResourceCode);
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("LevelCode", LevelCode);
                if (!string.IsNullOrEmpty(FieldCode))
                    map.Add("FieldCode", FieldCode);
                if (!string.IsNullOrEmpty(FieldValue))
                    map.Add("FieldValue", FieldValue);
                if (!string.IsNullOrEmpty(Describe))
                    map.Add("Describe", Describe);
                string msg = "";
                DataTable dt = leveBll.Get_ResourceExtendByLevelCode(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 根据工厂取出工序
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetProcessByFactory")]
        public HttpResponseMessage GetProcessByFactory(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string LevelCode = getValue(jo, "LevelCode");
            //string ResourceCode = getValue(jo, "ResourceCode");
            try
            {

                if (string.IsNullOrEmpty(LevelCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_8");//LevelCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                //if (!string.IsNullOrEmpty(ResourceCode))
                //    map.Add("ResourceCode", ResourceCode);
                if (!string.IsNullOrEmpty(LevelCode))
                    map.Add("LevelCode", LevelCode);
                string msg = "";
                DataTable dt = leveBll.GetProcessByFactory(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据工厂、属性取出工序
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetProcessByFactoryExtendInfo")]
        public HttpResponseMessage GetProcessByFactoryExtendInfo(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string factoryCode = getValue(jo, "factoryCode");//工厂编码
            string fieldCode = getValue(jo, "fieldCode");//属性编码
            string fieldValue = getValue(jo, "fieldValue");//属性值
            string name = getValue(jo, "Name");
            try
            {
                if (string.IsNullOrEmpty(factoryCode) || string.IsNullOrEmpty(fieldCode) || string.IsNullOrEmpty(fieldValue))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_14");//factoryCode、fieldCode、fieldValue 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                DataTable dt = leveBll.GetProcessByFactoryExtendInfo(factoryCode, fieldCode, fieldValue, name);
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_13") + ex.Message;//操作失败:
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据工厂找仓库
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetWarehouseByFactory")]
        public HttpResponseMessage GetWarehouseByFactory(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string factoryCode = getValue(jo, "factoryCode");
            string name = getValue(jo, "Name");
            try
            {
                if (string.IsNullOrEmpty(factoryCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_12");//factoryCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                DataTable dt = leveBll.GetWarehouseByFactory(factoryCode, name);
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_13") + ex.Message;//操作失败:
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        /// 根据工厂、属性找仓库
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetWarehouseByFactoryExtendInfo")]
        public HttpResponseMessage GetWarehouseByFactoryExtendInfo(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string factoryCode = getValue(jo, "factoryCode");
            string fieldCode = getValue(jo, "fieldCode");
            string fieldValue = getValue(jo, "fieldValue");
            string name = getValue(jo, "Name");
            try
            {
                if (string.IsNullOrEmpty(factoryCode) || string.IsNullOrEmpty(fieldCode) || string.IsNullOrEmpty(fieldValue))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_14");//factoryCode、fieldCode、fieldValue 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                DataTable dt = leveBll.GetWarehouseByFactoryExtendInfo(factoryCode, fieldCode, fieldValue, name);
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_13") + ex.Message;//操作失败:
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 取出所有工厂工序机台列表
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpGet]
        [Route("level/GetAllFactoryProcessMachine")]
        public HttpResponseMessage GetAllFactoryProcessMachine()
        {
            var result = new ResponseResult();
            try
            {
                Level_BLL leveBll = new Level_BLL();
                string msg = "";
                DataTable dt = leveBll.GetAllFactoryProcessMachine(out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据ParentResource取出列表
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/GetListByParentResource")]
        public HttpResponseMessage GetListByParentResource(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string parentResource = getValue(jo, "ParentResource");
            var resourceName = getValue(jo, "Name");
            try
            {

                if (string.IsNullOrEmpty(parentResource))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_15");//parentResource 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                Level_BLL leveBll = new Level_BLL();
                string msg = "";
                DataTable dt = leveBll.GetListByParentResource(parentResource, resourceName, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 取指定产品类型的产线 
        /// </summary>
        /// <param name="jo">对象</param>
        /// <returns>返回树形Json</returns>
        [HttpPost]
        [Route("level/Get_Lines_ByProductType")]
        public HttpResponseMessage Get_Lines_ByProductType(JObject jo)
        {
            var result = new ResponseResult();
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string ModelLeve = getValue(jo, "ModelLeve");
            string FieldCode = getValue(jo, "FieldCode");
            string FieldValue = getValue(jo, "FieldValue");
            try
            {
                if (string.IsNullOrEmpty(ModelLeve))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_16");//ModelLeve 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                if (string.IsNullOrEmpty(FieldCode))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_17");//FieldCode 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                if (string.IsNullOrEmpty(FieldValue))
                {
                    result.success = false;
                    result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_18");//FieldValue 参数不能为空！
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                Level_BLL leveBll = new Level_BLL();
                Dictionary<string, string> map = new Dictionary<string, string>();
                if (!string.IsNullOrEmpty(ModelLeve))
                    map.Add("ModelLeve", ModelLeve);
                if (!string.IsNullOrEmpty(FieldCode))
                    map.Add("FieldCode", FieldCode);
                if (!string.IsNullOrEmpty(FieldValue))
                    map.Add("FieldValue", FieldValue);
                string msg = "";
                DataTable dt = leveBll.Get_Lines_ByProductType(map, out msg);
                result.returnMsg = msg;
                result.resultData = dt;
            }
            catch (Exception ex)
            {
                result.success = false;
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                result.returnMsg = Language.GetText("Common.Error");//操作失败，服务器异常
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        /// <summary>
        ///保存字段数据
        /// </summary>
        /// <param name="jo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("level/Save_FieldData")]
        public HttpResponseMessage Save_FieldData(JObject jo)
        {
            var result = new ResponseResult<object>();
            result.resultData = null;
            if (jo == null)
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_6");//参数不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string ResourceCode = getValue(jo, "ResourceCode");
            if (string.IsNullOrEmpty(ResourceCode))
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_19");//ResourceCode 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string FieldCode = getValue(jo, "FieldCode");
            if (string.IsNullOrEmpty(FieldCode))
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_20");//FieldCode 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string FieldValue = getValue(jo, "FieldValue");
            if (string.IsNullOrEmpty(FieldValue))
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_21");//FieldValue 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            string userName = getValue(jo, "userName");
            if (string.IsNullOrEmpty(userName))
            {
                result.success = false;
                result.returnMsg = Language.GetText("LevelManage.LevelController.Tips_22");//userName 不能为空！
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            try
            {
                Level_BLL bll = new Level_BLL();
                var now = DateTime.Now;
                BsModelResourceExtendInfoEntity model = new BsModelResourceExtendInfoEntity();
                model.Id = Guid.NewGuid().ToString("N").ToUpper();
                model.ResourceCode = ResourceCode;
                model.FieldCode = FieldCode;
                model.FieldValue = FieldValue;
                model.CreateUser = userName;
                model.ModifyUser = userName;
                string msg = "";
                bool b = bll.Save_FieldData(model, out msg);
                result.success = b;
                if (b)
                    result.returnMsg = Language.GetText("Common.Success");//操作成功
                else
                    result.returnMsg = msg;
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception ex)
            {
                result.success = false;
                result.returnMsg = Language.GetText("Common.ErrorWithOther2") + ex.Message;//操作失败：
                result.statusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
        }
    }
}