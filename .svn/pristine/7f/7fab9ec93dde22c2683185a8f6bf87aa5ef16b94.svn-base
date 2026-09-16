using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型层级扩展
    /// </summary>
    public interface IBsModelLevelExtendFieldsService
    {
        #region 获取数据
        /// <summary>
        /// 层级扩展字段列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<BsModelLevelExtendFieldsEntity> GetList(string queryJson);
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<BsModelLevelExtendFieldsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 层级扩展字段列表all
        /// </summary>
        /// <returns></returns>
        IEnumerable<BsModelLevelExtendFieldsEntity> GetAllList();
        /// <summary>
        /// 层级扩展字段实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        BsModelLevelExtendFieldsEntity GetEntity(string keyValue);
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级扩展字段编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        bool ExistCode(string code);
        /// <summary>
        /// 层级扩展字段名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        bool ExistFullName(string name);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级扩展字段
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存层级扩展字段表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级扩展字段实体</param>
        /// <returns></returns>
        int SaveForm(string keyValue, BsModelLevelExtendFieldsEntity entity);
        #endregion
    }
}
