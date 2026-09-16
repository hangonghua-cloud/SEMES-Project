using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using ALP.Application.Entity.PlanManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using Newtonsoft.Json.Linq;
using ALP.Application.Service.Common;
using System.IO;
using ALP.Application.IService.PlanManage;

namespace ALP.Application.Service.PlanManage
{ 
    public class PL_Material_Service : RepositoryFactory<PL_MaterialEntity>, PL_MaterialIService
    { 
        /// <summary>
        /// 获取工单物料信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetWorkOrderMaterial(Pagination pagination,string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT PM.FactoryCode,
                               PM.FactoryName,
                               PM.WorkOrder,
                               PM.MaterialCode,
                               PM.MaterialName,
                               PM.Spec,
                               PM.MaterialClass,
                               PM.SmallClass,
                               PM.Unit,
                               PM.Warehouse,
                               PM.ProcureType,
                               PM.ProcessRoute,
                               PF.AttrCode,
                               PF.AttrType,
                               PF.AttrValue
                        FROM [dbo].[PL_Material] PM
                            LEFT JOIN dbo.PL_MaterialFacet PF
                                ON PM.Id = PF.MaterialId
                        WHERE PM.IsDeleted = 0 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["WorkOrder"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PM.WorkOrder = '{queryParam["WorkOrder"]}'");
                }
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND PM.MaterialCode = '{queryParam["MaterialCode"]}'");
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

        public List<dynamic> Get_ExpressionEntity1(string WorkOrder)
        {
            var sql = $@"SELECT a.Id,
                               a.WorkOrder,
                               b.GroupCode,
							   c.GroupName
                        FROM PL_Material a
                            LEFT JOIN [Base_MaterialGroupBindMaterial] b
                                ON a.MaterialCode = b.MaterialCode
						    LEFT JOIN dbo.Base_MaterialGroup c ON b.GroupCode=c.GroupCode
                        WHERE a.WorkOrder = '{WorkOrder}'
                        AND a.IsDeleted=0 ";
            var dy = this.BaseRepository().Query(sql);
            return dy;
        }

        public PL_MaterialEntity Get_ExpressionEntity(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<PL_MaterialEntity> Get_ExpressionList(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
        }
        public void RemoveForm(Expression<Func<PL_MaterialEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }
        /// <summary>
        /// 工单插入物料
        /// </summary>
        /// <param name="factory"></param>
        /// <param name="materialCode"></param>
        /// <param name="processRoute"></param>
        /// <param name="workOrder"></param>
        /// <returns></returns>
        public bool InsertPLMaterial(string factory, string materialCode, string processRoute, string workOrder)
        {

            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@FactoryCode", SqlDbType.VarChar,30),
                new SqlParameter("@MaterialCode", SqlDbType.VarChar,30),
                new SqlParameter("@ProcessRoute", SqlDbType.VarChar,30),
                new SqlParameter("@WorkOrder", SqlDbType.VarChar,30),
                new SqlParameter("@Resultmsg", SqlDbType.VarChar,100)
            };
            parameters[0].Value = factory;
            parameters[1].Value = materialCode;
            parameters[2].Value = processRoute;
            parameters[3].Value = workOrder;
            parameters[4].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("PL_InsertPLMaterial", parameters);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, PL_MaterialEntity entity, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    if (string.IsNullOrEmpty(entity.Id))
                    {
                        entity.Create();
                    }
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }
        public int Delete(List<PL_MaterialEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

    }
}
