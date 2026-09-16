using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.Material;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.Material;
using ALP.Application.UtilExtend.Offices;

namespace ALP.Application.Service.Material
{ 
    /// <summary>
    /// 1.创建日期: 2021-07-22
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialGroupService 业务服务类
    /// 4.任务编号: 物料属性模板维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialGroup_Service : RepositoryFactory<Base_MaterialGroupEntity>, Base_MaterialGroupIService
    { 
        
        public IEnumerable<Base_MaterialGroupEntity> GetList()
        {
            return this.BaseRepository().IQueryable();
        }
        public IEnumerable<Base_MaterialGroupEntity> GetList(Expression<Func<Base_MaterialGroupEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        public Base_MaterialGroupEntity GetEntity(Expression<Func<Base_MaterialGroupEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        public void RemoveForm(string keyvalue)
        {
            this.BaseRepository().Delete(keyvalue);
        }
        public int Insert(Base_MaterialGroupEntity entity)
        {
          return  this.BaseRepository().Insert(entity);
        }
        public int Update(Base_MaterialGroupEntity entity)
        {
            return this.BaseRepository().Update(entity);
        }


        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialGroupEntity> Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
select a.id, a.GroupCode,a.GroupName from Base_MaterialGroup a 
left join Base_MaterialGroup b 
on a.ParentId=b.Id where b.GroupCode='quality_group'");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
              
                if (!queryParam["GroupCode"].IsEmpty())
                {
                    sql.Append($"  and a.GroupCode='{queryParam["GroupCode"]}' ");
                }
                if (!queryParam["GroupName"].IsEmpty())
                {
                    sql.Append($" and a.GroupName='{queryParam["GroupName"]}' ");
                }  
                
            }
            try
            {
                if (pagination == null)
                {
                    return this.BaseRepository().FindList(sql.ToString());
                }
                else
                {
                    return this.BaseRepository().FindList(sql.ToString(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialGroupEntity> Get_PageData1(string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
select a.id, a.GroupCode,a.GroupName from Base_MaterialGroup a 
left join Base_MaterialGroup b 
on a.ParentId=b.Id where b.GroupCode='quality_group'");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 

                if (!queryParam["GroupCode"].IsEmpty())
                {
                    sql.Append($"  and a.GroupCode='{queryParam["GroupCode"]}' ");
                }
                if (!queryParam["GroupName"].IsEmpty())
                {
                    sql.Append($" and a.GroupName='{queryParam["GroupName"]}' ");
                }

            }
            try
            {              
                    return this.BaseRepository().FindList(sql.ToString());
             
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
