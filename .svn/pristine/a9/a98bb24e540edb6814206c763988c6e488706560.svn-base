using ALP.Application.Entity.PlanManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.PlanManage
{
    /// <summary>
    /// 1.创建日期: 2024-3-11 08:17:10
    /// 2.创建作者: jpf
    /// 3.功能描述:  PL_InternalOrderService 业务服务类
    /// 4.任务编号: 内部订单
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class PL_InternalOrder_Service : RepositoryFactory<PL_InternalOrderEntity>
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT a.Id,
                               a.OrderType,
                               a.OrderDescription,
                               a.Companycode,
                               a.InternalOrder,
                               a.Status
                        FROM dbo.PL_InternalOrder a
                        WHERE 1 = 1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //内部订单号
                if (!queryParam["InternalOrder"].IsEmpty())
                {
                    sql.Append($" AND a.InternalOrder like N'%{queryParam["InternalOrder"]}%'");
                }
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND a.OrderDescription like N'%{queryParam["Name"]}%'");
                }
                //未关闭的内部订单
                if (!queryParam["Status"].IsEmpty())
                {
                    sql.Append($" AND a.Status = N'{queryParam["Status"]}'");
                }
                
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindTable(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindTable(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: jpf
        /// 创建日期: 2024-3-11 08:17:00
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int DeleteEntity(string keyValue, out string msg, string UpdateByName = "")
        {
            int n = 0;
            msg = "";
            try
            {
                //删除
                n = this.BaseRepository().Delete(keyValue);
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 假删除, 通过主键删除, 删除标记设置为0
        /// 创　　建: jpf
        /// 创建日期: 2024-3-11 08:16:49
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            return this.BaseRepository().Delete(keyValue);
        }


        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: jpf
        /// 创建日期:2024-3-11 08:16:38
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回PL_PurchaseOrderEntity</returns>
        public PL_InternalOrderEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: jpf
        /// 创建日期: 2024-3-11 08:16:27
        /// 任务编号: 采购订单
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回PL_PurchaseOrderEntity</returns>
        public PL_InternalOrderEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }
        public PL_InternalOrderEntity GetEntity(Expression<Func<PL_InternalOrderEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: jpf
        /// 创建日期: 2024-3-11 08:16:14
        /// 任务编号: 采购订单
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回PL_PurchaseOrderEntity 列表</returns>
        public IEnumerable<PL_InternalOrderEntity> Get_ExpressionList(Expression<Func<PL_InternalOrderEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
            //调用示例 var data = _Service.Get_ExpressionList(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false).OrderByDescending(t => t.PlanProNo).ToList();
        }

        #region SAP接口
        /// <summary>
        /// 功能描述:  SAP接口
        /// 创　　建: jpf
        /// 创建日期: 2024-3-8 15:21:38
        /// 任务编号: 内部订单
        /// </summary>
        /// <param name="entity"></param>
        public void SaveSAPPL_InternalOrder(List<PL_InternalOrderEntity> entity)
        {
            //开启事务进行数据的插入
            IDatabase db = DbFactory.UABase().BeginTrans();
            //var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (var item in entity)
                {
                    if (string.IsNullOrEmpty(item.InternalOrder))
                    {
                        throw new Exception("内部订单号必须传");
                    }
                    if (string.IsNullOrEmpty(item.Status))
                    {
                        throw new Exception("状态必须传");
                    }
                    if (string.IsNullOrEmpty(item.OrderType))
                    {
                        throw new Exception("订单类型必须传");
                    }
                    if (string.IsNullOrEmpty(item.OrderDescription))
                    {
                        throw new Exception("订单描述必须传");
                    }
                    if (string.IsNullOrEmpty(item.Companycode))
                    {
                        throw new Exception("公司代码必须传");
                    }
                    //判断内部订单是否存在
                    var InternalOrder = GetEntity(t => t.InternalOrder == item.InternalOrder);
                    if (InternalOrder == null)
                    {
                        item.Create();
                        db.Insert(item);
                    }
                    else
                    {
                        InternalOrder.Status = item.Status;
                        db.Update(InternalOrder);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            finally
            {
                db.Close();
            }
        }


        #endregion
    }
}
