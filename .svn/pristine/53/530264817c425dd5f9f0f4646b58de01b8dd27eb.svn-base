using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.QualityManage;
using ALP.Application.IService.QualityManage;
using ALP.Application.Service.QualityManage;
using ALP.Util.WebControl;

namespace ALP.Application.Busines.QualityManage
{
    public class QC_IPQCDetailResult_BLL
    {
        private QC_IPQCDetailResult_IService service = new QC_IPQCDetailResult_Service();

        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetPageDataTableList(pagination, queryJson);
        }

        /// <summary>
        /// 获取检验数据
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetCheckPageDataTableList(Pagination pagination, string queryJson)
        {
            return service.GetCheckPageDataTableList(pagination, queryJson);
        }
        /// <summary>
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetList(string checkType, out string msg)
        {
            return service.GetList(checkType, out msg);
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, QC_IPQCDetailResultEntity entity, out string msg)
        {
            return service.SaveEntity(keyValue, entity, out msg);
        }

        public int SaveEntity(QC_IPQCDetailEntity mainEntity, List<QC_IPQCDetailResultEntity> list, ref string msg)
        {
            return service.SaveEntity(mainEntity, list, ref msg);
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<QC_IPQCDetailResultEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<QC_IPQCDetailResultEntity> entity_list, out string msg)
        {
            return service.SaveEntity_List(IsUpdate, CreatedByName, entity_list, out msg);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            return service.DeleteEntity(keyValue, out msg, UpdateByName);
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveForm(keyValue, UpdateByName);
        }
        public int RemoveForm(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
        {
            return service.RemoveForm(condition);
        }

        public int RemoveBatchForm(string keyValue, string UpdateByName = "")
        {
            return service.RemoveBatchForm(keyValue, UpdateByName);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            return service.Delete_SQL(keyValue, out msg);
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回QC_IPQCDetailResultEntity</returns>
        public QC_IPQCDetailResultEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回QC_IPQCDetailResultEntity</returns>
        public QC_IPQCDetailResultEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return service.GetEntityByQuery(QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailResultEntity 对象</returns>
        public QC_IPQCDetailResultEntity Get_ExpressionEntity(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return service.Get_ExpressionEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述: 根据条件（linq）方法查询列表
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回QC_IPQCDetailResultEntity 列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> Get_ExpressionList(Expression<Func<QC_IPQCDetailResultEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return service.Get_ExpressionList(condition);
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

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
        //    RepositoryFactory<QC_IPQCDetailResultEntity> bomService = new RepositoryFactory<QC_IPQCDetailResultEntity>();

        //    QC_IPQCDetailResultEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    QC_IPQCDetailResultDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.QC_IPQCDetailResult_Id == entity.Id).FirstOrDefault();
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
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<QC_IPQCDetailResultEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetList_TestOtherEntity(checkType, out msg);
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            return service.GetDataTable_TestOtherEntity(checkType, out msg);
        }

        /// <summary>
        /// 根据单据类型获取流水号 存储过程调用示例
        /// </summary>
        /// <param name="SeqCode">规则代码</param>
        /// <param name="returnNum">返回的流水号</param>
        /// <param name="messageCode">异常消息等</param>
        /// <returns></returns>
        public bool GetSerialNO(string SeqCode, out string returnNum, out string messageCode)
        {
            return service.GetSerialNO(SeqCode, out returnNum, out messageCode);
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: 丁零
        /// 创建日期: 2021-08-23 17:47:38
        /// 任务编号: 过程检验记录表
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            return service.GetList_export(checkType, out msg);
        }
    }
}
