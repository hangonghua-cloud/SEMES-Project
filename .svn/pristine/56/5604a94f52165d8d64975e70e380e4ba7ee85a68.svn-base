using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using ALP.Application.Entity.QualityManage;
using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.QualityManage
{
    public interface QC_IPQCDetail_IService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<QC_IPQCDetailEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        DataTable GetPageDataTableList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取检验方法下拉列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        DataTable GetCalibrationMethodPageList(Pagination pagination, string queryJson);

        /// <summary>
        /// 获取检验方法关联工序列表
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        DataTable GetProcessList(string method);

        /// <summary>
        /// 查询工序列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<QC_TestMaintenanceEntity> GetMainenanceList(Pagination pagination, string queryJson);

        /// <summary>
        /// 查询工序列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        IEnumerable<BsModelWithResourceEntity> GetProcessList(Pagination pagination, string queryJson);
        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<QC_IPQCDetailEntity> GetList(string checkType, out string msg);

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity(string keyValue, QC_IPQCDetailEntity entity, out string msg);

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IPQCDetailEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IPQCDetailEntity> entity_list, out string msg);

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int DeleteEntity(string keyValue, out string msg, string UpdateByName = "");

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int RemoveForm(string keyValue, string UpdateByName = "");

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        int Delete_SQL(string keyValue, out string msg);

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IPQCDetailEntity</returns>
        QC_IPQCDetailEntity GetEntity(string keyValue);

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IPQCDetailEntity</returns>
        QC_IPQCDetailEntity GetEntityByQuery(string QueryField);

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailEntity 对象</returns>
        QC_IPQCDetailEntity Get_ExpressionEntity(Expression<Func<QC_IPQCDetailEntity, bool>> condition);

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailEntity 列表</returns>
        IEnumerable<QC_IPQCDetailEntity> Get_ExpressionList(Expression<Func<QC_IPQCDetailEntity, bool>> condition);

        ///// <summary>
        ///// 删除主表数据并同步删除子表数据, 假删除更新删除标记
        ///// </summary>
        ///// <param name="keyValue"></param>
        ///// <returns></returns>
        //public int RemoveForm(string keyValue)
        //{
        //    int result = 0;
        //    StringBuilder sql = new StringBuilder();
        //    //子表服务类
        //    RepositoryFactory<QC_IPQCDetailEntity> bomService = new RepositoryFactory<QC_IPQCDetailEntity>();

        //    QC_IPQCDetailEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IPQCDetailDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IPQCDetail_Id == entity.Id).FirstOrDefault();
        //    if (entity != null)
        //    {
        //        //主表删除标记
        //        entity.IsEnabled = false;
        //        this.BaseRepository().Update(entity);
        //        if (bomEntity != null)
        //        {
        //            //子表删除标记
        //            bomEntity.IsEnabled = false;
        //            bomService.BaseRepository().Update(bomEntity);
        //        }
        //        result = 1;
        //    }

        //    return result;
        //}

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用另一个实体进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        IEnumerable<QC_IPQCDetailEntity> GetList_TestOtherEntity(string checkType, out string msg);

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
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
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 10:37:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        string GetList_export(string checkType, out string msg);
    }
}
