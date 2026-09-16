using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.ProduceManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.WebControl;

using System.IO;

namespace ALP.Application.Service.ProduceManage
{
    /// <summary>
    /// 1.创建日期: 2021-08-10
    /// 2.创建作者: admin
    /// 3.功能描述: PM_ProcessBadService 业务服务类
    /// 4.任务编号: 报工不良项目配置
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public interface PM_TeamPerson_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TeamPersonEntity> GetPageList(Pagination pagination, string queryJson);


        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);


        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TeamPersonEntity> GetList(string checkType, out string msg);


        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity(string keyValue, PM_TeamPersonEntity entity, out string msg);


        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_TeamPersonEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PM_TeamPersonEntity> entity_list, out string msg);


        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");


        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int RemoveForm(string keyValue, string UpdateByName = "");


        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int Delete_SQL(string keyValue, out string msg);


        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PM_TeamPersonEntity</returns>
        PM_TeamPersonEntity GetEntity(string keyValue);

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PM_TeamPersonEntity</returns>
        PM_TeamPersonEntity GetEntityByQuery(string QueryField);


        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TeamPersonEntity 对象</returns>
        PM_TeamPersonEntity Get_ExpressionEntity(Expression<Func<PM_TeamPersonEntity, bool>> condition);

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PM_TeamPersonEntity 列表</returns>
        IEnumerable<PM_TeamPersonEntity> Get_ExpressionList(Expression<Func<PM_TeamPersonEntity, bool>> condition);

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<PM_TeamPersonEntity> GetList_TestOtherEntity(string checkType, out string msg);


        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: admin
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
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
        /// 创建日期: 2021-08-10 16:24:18
        /// 任务编号: 报工不良项目配置
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        string GetList_export(string checkType, out string msg);


    }
}
