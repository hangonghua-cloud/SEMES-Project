using ALP.Application.Entity.EquipmentManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util.WebControl;
using ALP.Util.Extension;
using ALP.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace ALP.Application.Service.EquipmentManage
{
    public class EP_EquipmentManage_Service : Data.Repository.RepositoryFactory<EP_EquipmentManage>
    {

        /// <summary>
        /// 查询分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<EP_EquipmentManage> Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT a.Id,
                                F.ResourceCode TestMethodCoadin,
                                       F.ResourceName AS TestMethodCoadinName,
                                       E.ResourceName AS InstallationSiteName,
                                       D.ResourceName AS ProcessBelongName,
                                       C.ResourceCode AS EquipmentId,
                                       C.ResourceName AS EquipmentName,
                                       b.ItemName AS EquipmentTypeName,
                                       a.CreateDate AS CreateTime
                                FROM [BS_ModelWithResource] F
                                    LEFT JOIN [BS_ModelWithResource] E
                                        ON F.ResourceCode = E.ParentResource
                                    LEFT JOIN [BS_ModelWithResource] D
                                        ON E.ResourceCode = D.ParentResource
                                    LEFT JOIN [BS_ModelWithResource] C
                                        ON D.ResourceCode = C.ParentResource
                                    LEFT JOIN [FHMESDB].[dbo].[BS_ModelResourceExtendInfo] a
                                        ON C.ResourceCode = a.ResourceCode
                                    LEFT JOIN [dbo].[V_DataDictionary] b
                                        ON a.FieldValue = b.ItemValue
                                WHERE FieldCode = 'CXZL'
                                      AND b.EnCode = 'EquipmentTypes'
                                      AND C.ModelLeve = 'Machine' ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($"  and EquipmentId='{queryParam["EquipmentId"]}' ");
                    sql.Append($"  and CHARINDEX('{queryParam["EquipmentId"]}',C.ResourceCode)>0 ");
                }
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["EquipmentName"]}',C.ResourceName)>0 ");
                }
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($"  and EquipmentId='{queryParam["EquipmentId"]}' ");
                    sql.Append($"  and CHARINDEX('{queryParam["queryCode"]}',EquipmentId)>0 ");
                }
                if (!queryParam["queryName"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["queryName"]}',EquipmentName)>0 ");
                }
                if (!queryParam["TestMethodCoadin"].IsEmpty())
                {
                    sql.Append($"  and F.ResourceCode='{queryParam["TestMethodCoadin"]}' ");
                }
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    sql.Append($" and B.ItemValue='{queryParam["EquipmentType"]}' ");
                }
                if (!queryParam["EquipmentStatus"].IsEmpty())
                {
                    sql.Append($"  and EquipmentStatus='{queryParam["EquipmentStatus"]}' ");
                }
                if (!queryParam["SpedificationsMode"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["SpedificationsMode"]}',SpedificationsMode)>0");
                }
                if (!queryParam["InstallationSite"].IsEmpty())
                {
                    sql.Append($"  and E.ResourceCode='{queryParam["InstallationSite"]}' ");
                }
                if (!queryParam["ProcessBelong"].IsEmpty())
                {
                   sql.Append($"   and D.ResourceCode='{queryParam["ProcessBelong"]}' ");
                }
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($"  and F.ResourceCode='{queryParam["FactoryCode"]}' ");
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
        public IEnumerable<EP_EquipmentManage> Get_PageData1(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"select A.ID,A.TestMethodCoadin,D.ResourceName as TestMethodCoadinName,A.EquipmentId,A.EquipmentName,A.EquipmentType,A.EquipmentStatus,A.SpedificationsMode,A.InstallationSite,B.ResourceName as InstallationSiteName,A.ProcessBelong,C.ResourceName as ProcessBelongName,A.LineCode,E.ResourceName as LineName,
                        A.Factory,A.DateOfProduction,A.DateOfService,A.Remark,A.IsUsed,A.CreateTime,A.Creator,dbo.get_dicName('EquipmentStatus',A.EquipmentStatus) as EquipmentStatusName,dbo.get_dicName('EquipmentTypes',A.EquipmentType) as EquipmentTypeName
                        from EP_EquipmentManage as A
                        left join BS_ModelWithResource as B on A.InstallationSite=B.ResourceCode and B.ModelLeve='WorkShop' and B.EnabledMark=1
                        left join BS_ModelWithResource as C on A.ProcessBelong=C.ResourceCode and C.ModelLeve='Process' and C.EnabledMark=1
						left join BS_ModelWithResource as D on A.TestMethodCoadin=D.ResourceCode and D.ModelLeve='Factory' and D.EnabledMark=1
						left join BS_ModelWithResource as E on A.LineCode=E.ResourceCode and E.ModelLeve='Machine' and E.EnabledMark=1
                        where 1=1 and A.EnabledMark=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                if (!queryParam["EquipmentId"].IsEmpty())
                {
                    //sql.Append($"  and EquipmentId='{queryParam["EquipmentId"]}' ");
                    sql.Append($"  and CHARINDEX('{queryParam["EquipmentId"]}',EquipmentId)>0 ");
                }
                if (!queryParam["EquipmentName"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["EquipmentName"]}',EquipmentName)>0 ");
                }
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($"  and EquipmentId='{queryParam["EquipmentId"]}' ");
                    sql.Append($"  and CHARINDEX('{queryParam["queryCode"]}',EquipmentId)>0 ");
                }
                if (!queryParam["queryName"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["queryName"]}',EquipmentName)>0 ");
                }
                if (!queryParam["TestMethodCoadin"].IsEmpty())
                {
                    sql.Append($"  and TestMethodCoadin='{queryParam["TestMethodCoadin"]}' ");
                }
                if (!queryParam["EquipmentType"].IsEmpty())
                {
                    sql.Append($" and EquipmentType='{queryParam["EquipmentType"]}' ");
                }
                if (!queryParam["EquipmentStatus"].IsEmpty())
                {
                    sql.Append($"  and EquipmentStatus='{queryParam["EquipmentStatus"]}' ");
                }
                if (!queryParam["SpedificationsMode"].IsEmpty())
                {
                    sql.Append($"  and CHARINDEX('{queryParam["SpedificationsMode"]}',SpedificationsMode)>0");
                }
                if (!queryParam["InstallationSite"].IsEmpty())
                {
                    sql.Append($"  and InstallationSite='{queryParam["InstallationSite"]}' ");
                }
                if (!queryParam["ProcessBelong"].IsEmpty())
                {
                    sql.Append($"   and ProcessBelong='{queryParam["ProcessBelong"]}' ");
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
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        /// <returns></returns>
        public EP_EquipmentManage GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }
        /// <summary>
        /// 得到一个对象
        /// </summary>
        /// <param name="condition">主键值</param> 
        /// <returns></returns>
        public EP_EquipmentManage GetEntity(Expression<Func<EP_EquipmentManage, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int RemoveForm(string keyValue, out string msg)
        {
            msg = "";
            int n = 0;
            try
            {
                EP_EquipmentManage entity = this.BaseRepository().FindEntity(keyValue);
                if (entity != null)
                {
                    entity.EnabledMark = false;
                    n = this.BaseRepository().Update(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int SaveForm(string keyValue, EP_EquipmentManage entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {

                    entity.ID = Guid.NewGuid().ToString();
                    n = this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.ID = keyValue;
                    n = this.BaseRepository().Update(entity);
                }                
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 获取List列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public IEnumerable<EP_EquipmentManage> GetList(Expression<Func<EP_EquipmentManage, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }

    }
}
