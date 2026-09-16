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
using ALP.Application.UtilExtend.Offices;
using ALP.Application.IService.Material;
using ALP.Application.UtilExtend.Util;
using ALP.Data;

namespace ALP.Application.Service.Material
{
    /// <summary>
    /// 1.创建日期: 2021-07-23
    /// 2.创建作者: liyongguo
    /// 3.功能描述: Base_MaterialFacetService 业务服务类
    /// 4.任务编号: 物料主数据
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class Base_MaterialFacet_Service : RepositoryFactory<Base_MaterialFacetEntity>, Base_MaterialFacetIService
    {
        /// <summary>
        /// 功能描述: 查询分页列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialFacetEntity> GetPageList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[MaterialFactoryId]
                      ,[AttrCode] ,[AttrName]
                      ,[AttrType]
                      ,[AttrValue]
                  FROM [dbo].[Base_MaterialFacet] where 1=1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["MaterialFactoryId"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND MaterialFactoryId = '{queryParam["Id"]}'");
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
                    return this.BaseRepository().FindList(sql.ToString(), parameter.ToArray(), pagination);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 功能描述: 查询分页列表(DataTable)
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetPageDataTableList(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@" SELECT BMF.Id,
                               BM.MaterialCode,
                               BM.MaterialName,
                               BM.Spec,
                               BM.MaterialClass,
                               BM.SmallClass,
                               BMF.AttrCode,
                               BMF.AttrName,
                               BMF.AttrValue,
                               BMF.AttrType,
                               V.ItemName AttrTypeName,
                               bmt.Sort,
                               bmt.MateriaBindTempId,
                               BMF.MaterialFactoryId
                        FROM dbo.Base_Material BM
                            INNER JOIN dbo.Base_MaterialFactory a
                                ON BM.MaterialCode = a.MaterialCode
                            INNER JOIN [dbo].[Base_MaterialFacet] BMF
                                ON a.Id = BMF.MaterialFactoryId
                            LEFT JOIN dbo.V_DataDictionary V
                                ON V.EnCode = 'AttrType'
                                   AND V.ItemValue = BMF.AttrType
								   LEFT JOIN Base_MaterialBindTemp f
								   ON a.TemplateCode=f.TempCode
                            LEFT JOIN dbo.Base_MaterialBindTempFacet bmt
                                ON BMF.AttrCode = bmt.AttrCode AND f.id=bmt.MateriaBindTempId WHERE 1 = 1 ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //Id 是否为空进行查询
                if (!queryParam["Id"].IsEmpty())
                {
                    //sql.Append($" AND Id = N'{queryParam["Id"]}'");
                    sql.Append($" AND BMF.Id = N'{queryParam["Id"]}'");
                }
                //物料主键 是否为空进行查询
                if (!queryParam["MaterialFactoryId"].IsEmpty())
                {
                    sql.Append($" AND BMF.MaterialFactoryId = N'{queryParam["MaterialFactoryId"]}'");
                }
                //属性名称 是否为空进行查询
                if (!queryParam["AttrCode"].IsEmpty())
                {
                    //sql.Append($" AND AttrCode = N'{queryParam["AttrCode"]}'");
                    sql.Append($" AND BMF.AttrCode like N'%{queryParam["AttrCode"]}%'");
                }
                //属性类型 是否为空进行查询
                if (!queryParam["AttrType"].IsEmpty())
                {
                    //sql.Append($" AND AttrType = N'{queryParam["AttrType"]}'");
                    sql.Append($" AND BMF.AttrType like N'%{queryParam["AttrType"]}%'");
                }
                //属性值 是否为空进行查询
                if (!queryParam["AttrValue"].IsEmpty())
                {
                    //sql.Append($" AND AttrValue = N'{queryParam["AttrValue"]}'");
                    sql.Append($" AND BMF.AttrValue like N'%{queryParam["AttrValue"]}%'");
                }
                //queryName(选择弹窗关键名称) 是否为空进行查询
                if (!queryParam["MaterialCode"].IsEmpty())
                {
                    sql.Append($" AND BM.MaterialCode = N'{queryParam["MaterialCode"]}'");
                }
                //queryCode(选择弹窗关键编码) 是否为空进行查询
                if (!queryParam["queryCode"].IsEmpty())
                {
                    //sql.Append($" AND 关键编码 = N'{queryParam["queryCode"]}'");
                    //sql.Append($" AND 关键编码 like N'%{queryParam["queryCode"]}%'");
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
        /// 功能描述: 查询列表, 不分页, 适用于下拉列表使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialFacetEntity> GetList(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [Id]
                      ,[MaterialFactoryId]
                      ,[AttrCode]
,[AttrName]
                      ,[AttrType]
                      ,[AttrValue]
                  FROM [dbo].[Base_MaterialFacet] where 1=1 ");
            if (!checkType.IsEmpty())
            {
                sql.Append($@" and MaterialFactoryId = N'{checkType}' ");
            }
            msg = "";
            try
            {
                return this.BaseRepository().FindList(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity(string keyValue, Base_MaterialFacetEntity entity, out string msg)
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
                    entity.Create();
                    n = this.BaseRepository().Insert(entity);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 导入 保存表单（新增、修改）
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="IsUpdate">是否更新</param>
        /// <param name="CreatedByName">创建人</param>
        /// <param name="List<Base_MaterialFacetEntity>">实体对象数组</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int SaveEntity_List(bool IsUpdate, string CreatedByName, List<Base_MaterialFacetEntity> entity_list, out string msg)
        {
            int n = 0;
            msg = "";
            //try
            //{
            if (IsUpdate)
            {
                //n = this.BaseRepository().Update(entity_list);
                StringBuilder sql = new StringBuilder();
                if (entity_list.Count > 0)
                {
                    foreach (var Save_obj in entity_list)
                    {
                        StringBuilder sql_temp = new StringBuilder();
                        sql_temp.Append("UPDATE [dbo].[Base_MaterialFacet] set ");
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
                                    if (x.GetValue(Save_obj, null) != null && x.GetValue(Save_obj, null).ToString() != "")
                                    {
                                        sql_temp.Append(x.Name + "=N'" + (x.GetValue(Save_obj, null) == null ? "" : x.GetValue(Save_obj, null).ToString()) + "',");
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
                //n = this.BaseRepository().Insert(entity_list);
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(DbConstSettings.BaseDbString, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.UseInternalTransaction))
                {
                    bulkCopy.DestinationTableName = "Base_MaterialFacet";
                    //foreach(var item in new MMWhsBindMaterialDetailsEntity().GetType().GetProperties())
                    //{
                    //   bulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    //}
                    bulkCopy.WriteToServer(Tools.ToDataTableNoMap(entity_list));//将数据源数据写入到数据库中
                }
                return 1;
            }
            //}
            //catch (Exception ex)
            //{
            //    msg = ex.Message;
            //}
            return n;
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="UpdateByName">删除操作人</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int RemoveForm(string keyValue, string UpdateByName = "")
        {
            //Base_MaterialFacetEntity entity = this.BaseRepository().FindEntity(keyValue);
            //删除禁用标记
            //entity.IsDeleted = true;
            return this.BaseRepository().Delete(keyValue);
        }
        public int RemoveForm(Expression<Func<Base_MaterialFacetEntity, bool>> condition)
        {
            //Base_MaterialFacetEntity entity = this.BaseRepository().FindEntity(keyValue);
            //删除禁用标记
            //entity.IsDeleted = true;
            return this.BaseRepository().Delete(condition);
        }

        /// <summary>
        /// 功能描述: 删除, 通过主键删除, 使用SQL方式, 更新也可以使用
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="msg">输出错误内容</param>
        /// <returns>返回int 成功1, 失败0 </returns>
        public int Delete_SQL(string keyValue, out string msg)
        {
            int n = 0;
            msg = "";
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"DELETE FROM [dbo].[Base_MaterialFacet] WHERE Id=N'{keyValue}'");
                n = this.BaseRepository().ExecuteBySql(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return n;
        }

        /// <summary>
        /// 功能描述: 根据主键得到一个实体对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回Base_MaterialFacetEntity</returns>
        public Base_MaterialFacetEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(keyValue);
        }

        /// <summary>
        /// 功能描述: 通过某字段(不是主键)查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="QueryField">查询条件字段内容</param>
        /// <returns>返回Base_MaterialFacetEntity</returns>
        public Base_MaterialFacetEntity GetEntityByQuery(string QueryField)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(t => t.Id == QueryField);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询对象
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_MaterialFacetEntity 对象</returns>
        public Base_MaterialFacetEntity Get_ExpressionEntity(Expression<Func<Base_MaterialFacetEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().FindEntity(condition);
            //调用示例 var data = _Service.Get_ExpressionEntity(t => t.PlanProTime == PlanProTime && t.Line == Line&& t.IsDeleted == false);
        }

        /// <summary>
        /// 功能描述:  根据条件（linq）方法查询列表
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// /// <param name="condition">Expression 条件</param>
        /// <returns>返回Base_MaterialFacetEntity 列表</returns>
        public IEnumerable<Base_MaterialFacetEntity> Get_ExpressionList(Expression<Func<Base_MaterialFacetEntity, bool>> condition)
        {
            // 根据实际情况更换某字段, 这个字段内容在列表是唯一值
            return this.BaseRepository().IQueryable(condition).ToList();
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
        //    RepositoryFactory<Base_MaterialFacetEntity> bomService = new RepositoryFactory<Base_MaterialFacetEntity>();

        //    Base_MaterialFacetEntity entity = this.BaseRepository().FindEntity(keyValue);
        //    //根据主表在子表的ID与主表主键查找实体, 如果是多条,使用循环遍历删除
        //    Base_MaterialFacetDetailEntity bomEntity = bomService.BaseRepository().IQueryable(t => t.Base_MaterialFacet_Id == entity.Id).FirstOrDefault();
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
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Base_MaterialFacetEntity> GetList_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                IEnumerable<Base_MaterialFacetEntity> Base_MaterialFacetEntity_list = db2.FindList<Base_MaterialFacetEntity>(sql.ToString());
                return Base_MaterialFacetEntity_list;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 功能描述: 查询列表, 不分页,返回不是当前实体,使用一个未定义表进行返回 参考示例
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <param name="msg">错误消息输出</param>
        /// <returns>返回分页列表</returns>
        public DataTable GetDataTable_TestOtherEntity(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT getdate() as CreatedDateTime ");
            if (string.IsNullOrEmpty(checkType) == false)
            {
                //sql.Append($@" and ID = '{checkType}'";
            }
            msg = "";
            try
            {
                //执行 
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                //实体映射查询
                DataTable Base_MaterialFacetEntity_DataTable = db2.FindTable(sql.ToString());
                return Base_MaterialFacetEntity_DataTable;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
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
            bool b = false;
            returnNum = "";
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                new SqlParameter("@SeqCode", SqlDbType.VarChar,60),
                new SqlParameter("@ReturnNum", SqlDbType.VarChar,40),
                new SqlParameter("@MessageCode", SqlDbType.VarChar,800)
            };
            parameters[0].Value = SeqCode;
            parameters[1].Direction = ParameterDirection.Output;
            parameters[2].Direction = ParameterDirection.Output;

            try
            {
                //执行存储过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //返回参数值
                returnNum = parameters[1].Value.ToString();
                messageCode = parameters[2].Value.ToString();
                b = true;
            }
            catch (Exception ex)
            {
                messageCode = ex.Message;
            }
            return b;
        }

        /// <summary>
        /// 功能描述: 导出 列表到EXCEL 
        /// 创　　建: liyongguo
        /// 创建日期: 2021-07-23 14:38:58
        /// 任务编号: 物料主数据
        /// </summary>
        /// <param name="checkType">查询条件</param>
        /// <returns>链接地址</returns>
        public string GetList_export(string checkType, out string msg)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append(@"SELECT 
                      [MaterialFactoryId] as '物料主键'
                      ,[AttrCode] as '属性编码'
,[AttrName] as '属性名称'
                      ,[AttrType] as '属性类型'
                      ,[AttrValue] as '属性值'
                  FROM [dbo].[Base_MaterialFacet] where 1=1 ");
            msg = "成功!";
            if (!checkType.IsEmpty())
            {
                //此处换上你的关键查询条件 也可以为空 查询全部
                sql.Append($@" and CreatedByCode = '{checkType}' ");
            }
            try
            {
                DataTable dt = this.BaseRepository().FindTable(sql.ToString());
                var virtualPath = "~/";
                var dirPath = "Upload/";
                string folder = DateTime.Now.ToString("yyyyMM") + "/";
                //文件全路径
                var fullDirPath = System.Web.HttpContext.Current.Server.MapPath(virtualPath + dirPath + folder);

                string sServerDir = fullDirPath;
                if (!Directory.Exists(sServerDir))
                {
                    Directory.CreateDirectory(sServerDir);
                }
                string saveFileName = "物料主数据_" + DateTime.Now.ToString("yyyy-MM-dd-HHmm") + ".xls";

                ExcelHelper Excel = new ExcelHelper();
                MemoryStream ms = Excel.DataTableToExcel("物料主数据", dt, true);
                //保存
                Excel.saveTofle(ms, System.IO.Path.Combine(sServerDir, saveFileName));
                Excel.Dispose();
                return $@"{dirPath}{folder}{saveFileName}";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                return null;
            }
        }

    }
}
