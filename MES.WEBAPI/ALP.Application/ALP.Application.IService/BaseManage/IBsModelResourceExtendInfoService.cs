using ALP.Application.Entity.BaseManage;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.IService.BaseManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 
    /// 创建人：ALP
    /// 日 期：2020.12.17 16:19
    /// 描 述：工厂模型资源扩展
    /// </summary>
    public interface IBsModelResourceExtendInfoService
    {
        #region 获取数据
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<BsModelResourceExtendInfoEntity> GetList(string queryJson);
        IEnumerable<BsModelResourceExtendInfoEntity> GetList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition);
        /// <summary>
        /// 层级列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        DataTable GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 层级列表all
        /// </summary>
        /// <returns></returns>
        IEnumerable<BsModelResourceExtendInfoEntity> GetAllList(string keyValue);
        /// <summary>
        /// 层级实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        BsModelResourceExtendInfoEntity GetEntity(string keyValue);
        BsModelResourceExtendInfoEntity Get_ExpressEntity(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition);
        IEnumerable<BsModelResourceExtendInfoEntity> Get_ExpressionList(Expression<Func<BsModelResourceExtendInfoEntity, bool>> condition);
        #endregion

        #region 验证数据
        /// <summary>
        /// 层级编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <returns></returns>
        //bool ExistCode(string code);
        /// <summary>
        /// 层级名称不能重复
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="code">主键</param>
        /// <returns></returns>
        //bool ExistFullName(string name);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除层级
        /// </summary>
        /// <param name="code"></param>
        /// <param name="value"></param>
        /// <param name="resource"></param>
        void RemoveForm(string code, string resource);
        /// <summary>
        /// 保存层级表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">层级实体</param>
        /// <returns></returns>
        int SaveForm(string keyValue, BsModelResourceExtendInfoEntity entity);
        #endregion
    }
}
