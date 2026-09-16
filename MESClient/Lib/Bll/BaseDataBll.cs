using ALP.Application.Entity.BaseManage;
using Lib.Dal;
using Lib.Model;
using Lib.Model.Common;
using Lib.Model.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Bll
{
    public class BaseDataBll
    {
        BaseDataDal dal = new BaseDataDal();

        #region Select
        /// <summary>
        /// 工厂下拉列表
        /// </summary>
        /// <returns></returns>
        public List<Select> GetFactorySelect(string userCode)
        {
            return dal.GetFactorySelect(userCode);
        }
        /// <summary>
        /// 工序下拉列表
        /// </summary>
        /// <param name="factoryCode">工厂编码</param>
        /// <returns></returns>
        public List<Select> GetProcessSelectByFactory(string factoryCode)
        {
            return dal.GetProcessSelectByFactory(factoryCode);
        }
        /// <summary>
        /// 机台下拉列表
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetMachineSelectByProcess(string processCode)
        {
            return dal.GetMachineSelectByProcess(processCode);
        }
        /// <summary>
        /// 根据父级找子级
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetListSelectByParentResource(string parentResource)
        {
            return dal.GetListSelectByParentResource(parentResource);
        }
        /// <summary>
        /// 根据工厂找仓库
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public List<Select> GetWarehouseSelectByFactory(string factoryCode)
        {
            return dal.GetWarehouseSelectByFactory(factoryCode);
        }
        /// <summary>
        /// 数据字典
        /// </summary>
        /// <param name="enCode"></param>
        /// <returns></returns>
        public List<Select> GetDictionarySelect(string enCode)
        {
            return dal.GetDictionarySelect(enCode);
        }
        /// <summary>
        /// 不良项目
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetBadItemSelect(string processCode)
        {
            return dal.GetBadItemSelect(processCode);
        }
        /// <summary>
        /// 生产小组
        /// </summary>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public List<Select> GetPTeamSelect(string processCode)
        {
            return dal.GetPTeamSelect(processCode);
        }
        /// <summary>
        /// 流转卡
        /// </summary>
        /// <param name="exeWorkOrder">执行工单</param>
        /// <returns></returns>
        public List<Select> GetCardSelect(string exeWorkOrder)
        {
            return dal.GetCardSelect(exeWorkOrder);
        }
        #endregion

        #region 生产小组
        /// <summary>
        /// 生产小组列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetPTeamListWithPage(PTeamDto query, out int record)
        {
            return dal.GetPTeamListWithPage(query, out record);
        }
        /// <summary>
        /// 生产小组列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetPTeamDataTableWithPage(PTeamDto query, out int record)
        {
            return dal.GetPTeamDataTableWithPage(query, out record);
        }
        /// <summary>
        /// 获取生产小组列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<TeamPersonEntity> GetPTeamEntityList(List<string> lstPTeamCode)
        {
            return dal.GetPTeamEntityList(lstPTeamCode);
        }
        #endregion

        #region 人员
        /// <summary>
        /// 人员列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetPeopleListWithPage(PeopleDto query, out int record)
        {
            return dal.GetPeopleListWithPage(query, out record);
        }
        /// <summary>
        /// 人员列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetPeopleDataTableWithPage(PeopleDto query, out int record)
        {
            return dal.GetPeopleDataTableWithPage(query, out record);
        }
        /// <summary>
        /// 获取人员列表
        /// </summary>
        /// <param name="lstCardCode"></param>
        /// <returns></returns>
        public List<PeopleEntity> GetPeopleEntityList(List<string> lstUserCode)
        {
            return dal.GetPeopleEntityList(lstUserCode);
        }
        #endregion

        #region 物料批次
        /// <summary>
        /// 物料批次列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetMaterialBatchListWithPage(MaterialBatchDto query, out int record)
        {
            return dal.GetMaterialBatchListWithPage(query, out record);
        }
        /// <summary>
        /// 物料批次列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetMaterialBatchDataTableWithPage(MaterialBatchDto query, out int record)
        {
            return dal.GetMaterialBatchDataTableWithPage(query, out record);
        }
        /// <summary>
        /// 获取物料批次列表
        /// </summary>
        /// <param name="lstId"></param>
        /// <returns></returns>
        public List<RawMaterialStockEntity> GetMaterialBatchEntityList(List<string> lstId)
        {
            return dal.GetMaterialBatchEntityList(lstId);
        }
        #endregion

        #region 机台
        /// <summary>
        /// 机台列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetMachineListWithPage(MachineDto query, out int record)
        {
            return dal.GetMachineListWithPage(query, out record);
        }
        /// <summary>
        /// 机台列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetMachineDataTabletWithPage(MachineDto query, out int record)
        {
            return dal.GetMachineDataTabletWithPage(query, out record);
        }
        /// <summary>
        /// 获取机台列表
        /// </summary>
        /// <param name="lstMachineCode"></param>
        /// <returns></returns>
        public List<MachineEntity> GetMachineEntityList(List<string> lstMachineCode)
        {
            return dal.GetMachineEntityList(lstMachineCode);
        }
        #endregion

        #region 库位
        /// <summary>
        /// 库位列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public List<dynamic> GetLocationListWithPage(LocationDto query, out int record)
        {
            return dal.GetLocationListWithPage(query, out record);
        }
        /// <summary>
        /// 库位列表（分页）
        /// </summary>
        /// <param name="query"></param>
        /// <param name="record"></param>
        /// <returns></returns>
        public DataTable GetLocationDataTableWithPage(LocationDto query, out int record)
        {
            return dal.GetLocationDataTableWithPage(query, out record);
        }
        #endregion

        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="userCode">登录用户编码</param>
        /// <returns></returns>
        public HttpResult<BS_PeopleEntity> GetLoginInfo(string userCode)
        {
            return Http.Post<BS_PeopleEntity>("/Base/GetLoginInfo", new { userCode = userCode });
        }
        /// <summary>
        /// 根据子级获取上级实体
        /// </summary>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public HttpResult<BsModelWithResourceEntity> GetModelResourceByChild(string resourceCode)
        {
            return Http.Post<BsModelWithResourceEntity>("/BaseManage/BsModelLevel/GetModelResourceByChild", new { ResourceCode = resourceCode });
        }

        /// <summary>
        /// 工序报工不良项目配置
        /// </summary>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public HttpResult<List<Select2>> GetPMProcessBadItem(string processCode)
        {
            return Http.Post<List<Select2>>("/Produce/GetPMProcessBadItem", new { processCode = processCode });
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public HttpResult<string> GetSerialNO(string seqCode)
        {
            return Http.Post<string>("/Base/GetSerialNo", new { seqCode = seqCode });
        }

        /// <summary>
        /// 序列号
        /// </summary>
        /// <param name="resourceCode"></param>
        /// <returns></returns>
        public HttpResult<List<PersonDto>> GetUserList(string loginUserCode, string userCode, string NoLike)
        {
            return Http.Post<List<PersonDto>>("/Base/GetUserList", new { loginUserCode = loginUserCode, UserCode = userCode, NoLike = NoLike });
        }

    }
}
