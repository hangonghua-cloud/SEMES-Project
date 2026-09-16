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
using System.ComponentModel.DataAnnotations.Schema;

namespace ALP.Application.Service.PlanManage
{ 
    public class PL_MaterialFacet_Service : RepositoryFactory<PL_MaterialFacetEntity>, PL_MaterialFacetIService
    { 
        
        public PL_MaterialFacetEntity Get_ExpressionEntity(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        public IEnumerable<PL_MaterialFacetEntity> Get_ExpressionList(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition);
        }
        public void RemoveForm(Expression<Func<PL_MaterialFacetEntity, bool>> condition)
        {
            this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-08-26 14:25:31
        /// 任务编号: 自制半成品报工
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<PM_OwnProductBGEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<PL_MaterialFacetEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                if (IsUpdate)
                {
                    //n = this.BaseRepository().Update(entity_list);
                    StringBuilder sql = new StringBuilder();
                    if (entity_list.Count > 0)
                    {
                        foreach (var Save_obj in entity_list)
                        {
                            StringBuilder sql_temp = new StringBuilder();
                            sql_temp.Append("UPDATE [dbo].[PL_MaterialFacet] set ");
                            string keyValue = "";
                            //循环实体
                            Save_obj.GetType().GetProperties().ToList().ForEach(x =>
                            {
                                if (x.Name == "Id")
                                {
                                    keyValue = x.GetValue(Save_obj, null).ToString();
                                }
                                else
                                {
                                    if (x.Name == "IsDeleted")
                                    {
                                        if (x.GetValue(Save_obj, null) != null)
                                        {
                                            sql_temp.Append(x.Name + "=" + (x.GetValue(Save_obj, null) == null ? 0 : (x.GetValue(Save_obj, null).ToString() == "true" ? 1 : 0)) + ",");
                                        }
                                    }
                                    else
                                    {
                                        var hasNotMapped = Attribute.IsDefined(x, typeof(NotMappedAttribute));
                                        if (!hasNotMapped)
                                        {
                                            if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                            {
                                                sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
                                            }
                                        }
                                    }
                                }

                            });
                            sql.Append(sql_temp.ToString().TrimEnd(',') + $" WHERE Id='{keyValue}';");
                        }
                    }
                    //批量执行更新语句
                    n = this.BaseRepository().ExecuteBySql(sql.ToString());
                }
                else
                {
                    n = this.BaseRepository().Insert(entity_list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return n;
        }

        public int Delete(List<PL_MaterialFacetEntity> lstEntity)
        {
            return this.BaseRepository().Delete(lstEntity);
        }

        /// <summary>
        /// 获取最新物料属性
        /// jpf 2022-11-22add
        /// </summary>
        /// <returns></returns>
        public string VCGetMaterialFact(string WorkOrder , string userCode)
        {
            string msg = "";

            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@WorkOrder", SqlDbType.VarChar, 50),
                    new SqlParameter("@Creator", SqlDbType.VarChar, 50),
                    new SqlParameter("@Resultmsg",SqlDbType.VarChar, 8000)
            };
            parameters[0].Value = WorkOrder;
            parameters[1].Value = userCode;
            parameters[2].Direction = ParameterDirection.Output;
            try
            {

                //执行存贮过程
                Data.Dapper.SqlDatabase db = new Data.Dapper.SqlDatabase();
                db.ExecuteProcedure("PL_VCGetMaterialFact", parameters);
                var Resultmsg = parameters[2].Value;
                if (@Resultmsg != null && !string.IsNullOrEmpty(@Resultmsg.ToString()))
                {
                    return @Resultmsg.ToString();
                }
                return msg;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 15600)
                {
                    throw new Exception("获取最新物料属性失败");
                }
            }
            return msg;
        }
    }
}
