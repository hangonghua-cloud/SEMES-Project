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
    /// 3.功能描述: Base_MaterialGroupBindMaterialService 业务服务类
    /// 4.任务编号: 物料属性模板维护
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialGroupBindMaterial_Service : RepositoryFactory<Base_MaterialGroupBindMaterialEntity>, Base_MaterialGroupBindMaterialIService
    {

        public IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList()
        {
            return this.BaseRepository().IQueryable();
        }
        public IEnumerable<Base_MaterialGroupBindMaterialEntity> GetList(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

        public Base_MaterialGroupBindMaterialEntity GetEntity(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }

        public void RemoveForm(string keyvalue)
        {
            this.BaseRepository().Delete(keyvalue);
        }
        public void RemoveForm(Expression<Func<Base_MaterialGroupBindMaterialEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        public int Insert(Base_MaterialGroupBindMaterialEntity entity)
        {
            return this.BaseRepository().Insert(entity);
        }
        public int Update(Base_MaterialGroupBindMaterialEntity entity)
        {
            return this.BaseRepository().Update(entity);
        }
        public DataTable GetDataTableWithPage(Pagination pagination,string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      M.Id,M.MaterialCode,M.SortCode
                       ,GroupCode
                      ,M.Creator
                      ,M.CreateTime
                      ,M.ModifyBy
                      ,M.ModifyTime,
					  B.MaterialName,B.Spec,B.MaterialClass,B.SmallClass,B.Unit,
					  V.ItemName MaterialClassName,V1.ItemName SmallClassName,V2.ItemName UnitName
                  FROM dbo.Base_MaterialGroupBindMaterial M 
				  LEFT JOIN dbo.Base_Material B ON M.MaterialCode=B.MaterialCode
				  LEFT JOIN dbo.V_DataDictionary V ON V.EnCode='MaterialType' AND V.ItemValue=B.MaterialClass
				  LEFT JOIN dbo.V_DataDictionary V1 ON V1.EnCode='MaterialSmall' AND V1.ItemValue=B.SmallClass
				  LEFT JOIN dbo.V_DataDictionary V2 ON V2.EnCode='Unit' AND V2.ItemValue=B.Unit
				  WHERE 1=1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["GroupCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND GroupCode = N'{queryParam["GroupCode"]}'");
                }
                //模板编码 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND TempCode = N'{queryParam["TempCode"]}'");
                    sql.Append($" AND M.MaterialCode like N'%{queryParam["MaterialCode"]}%'");
                }
                //模板编码 是否为空进行查询
                if (!queryParam["MaterialName"].IsEmpty())
                {
                    //sql.Append($" AND TempCode = N'{queryParam["TempCode"]}'");
                    sql.Append($" AND B.MaterialName like N'%{queryParam["MaterialName"]}%'");
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
    }
}
