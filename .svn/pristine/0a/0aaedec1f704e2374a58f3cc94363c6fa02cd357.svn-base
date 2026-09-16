using ALP.Application.Code.Model;
using ALP.Application.Entity.ProduceManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.ProduceManage
{
    public interface PM_TransferCardIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TransferCardEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);


        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TransferCardEntity> GetList(string checkType, out string msg);
        IEnumerable<PM_TransferCardEntity> GetList(Expression<Func<PM_TransferCardEntity, bool>> condition);

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity(string keyValue, PM_TransferCardEntity entity, out string msg);


        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_TransferCardEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TransferCardEntity> entity_list, out string msg);
        int insertList(List<PM_TransferCardEntity> list);

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");


        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int RemoveForm(string keyValue, string UpdateByName = "");


        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int Delete_SQL(string keyValue, out string msg);


        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_TransferCardEntity</returns>
        PM_TransferCardEntity GetEntity(string keyValue);


        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_TransferCardEntity</returns>
        PM_TransferCardEntity GetEntityByQuery(string QueryField);

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardEntity 对象</returns>
        PM_TransferCardEntity Get_ExpressionEntity(Expression<Func<PM_TransferCardEntity, bool>> condition);
   

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TransferCardEntity 列表</returns>
        IEnumerable<PM_TransferCardEntity> Get_ExpressionList(Expression<Func<PM_TransferCardEntity, bool>> condition);


        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TransferCardEntity> GetList_TestOtherEntity(string checkType, out string msg);


        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        DataTable GetDataTable_TestOtherEntity(string checkType, out string msg);


        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
         bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode);


        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: admin
        /// 创建日期: 2021-08-11 20:45:31
        /// 任务编号: 流转卡信息
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        string GetList_export(string checkType, out string msg);


        int InsertList(List<PM_TransferCardEntity> lstEntity);

        #region PDA接口
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="serialNumber">序列号</param>
        /// <returns></returns>
        DataTable GetCardList_PDA(string serialNumber);
        /// <summary>
        /// 生产开工-流转卡扫描
        /// </summary>
        /// <param name="exeWorkOrder">执行工单号</param>
        /// <returns></returns>
        DataTable GetCardList_PDA2(string exeWorkOrder);
        /// <summary>
        /// 流转卡报废-流转卡扫描
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        dynamic TransferCardScrapScan(string cardCode);

        /// <summary>
        /// 创建返工任务-流转卡扫描
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        List<dynamic> ReworkCardScan(string serialNumber, string processCode);

        /// <summary>
        /// 生产管理-扫一扫
        /// </summary>
        /// <param name="exeWorkOrder">序列号</param>
        /// <returns></returns>
        List<dynamic> CodeBarScan(string exeWorkOrder);
        /// <summary>
        /// 生产管理-BOM查询
        /// </summary>
        /// <param name="workOrder">工单号</param>
        /// <returns></returns>
        List<dynamic> BOMQuery(string workOrder);
        #endregion

        dynamic CodeBarScan1(string exeWorkOrder, string CardStatus, string BusinessType);

        dynamic getNoBGTS(string serialNuber, string processCode);

        #region App质量检测
        /// <summary>
        /// App过程检验-根据流转卡获取信息
        /// </summary>
        /// <param name="cardCode"></param>
        /// <returns></returns>
        List<dynamic> GetQCTransferCardEntity(string cardCode);
        List<dynamic> GetSemiQCTransferCardEntity(string cardCode);
        #endregion
    }
}
