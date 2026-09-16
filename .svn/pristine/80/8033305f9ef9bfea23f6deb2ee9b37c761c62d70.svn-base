using ALP.Application.Entity.BaseManage;
using ALP.Application.Entity.SystemManage;
using ALP.Application.IService.SystemManage;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using ALP.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ALP.Application.IService.SystemManage
{
    /// <summary>
    /// 人员管理接口
    /// </summary>
    public class Sys_PersonService : RepositoryFactory<BS_PeopleEntity>
    {
        #region 获取数据 
        /// <summary>
        /// 查询工单分页列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public DataTable Get_PageData(Pagination pagination, string queryJson)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@" SELECT DISTINCT
                                   BS.[ID],
                                   BS.[Sex],
                                   BS.[CertificateCode],
                                   BS.[MobilePhone],
                                   BS.[Department_ID],
                                   depart.[Name] DepartName,
                                   BS.[Position_ID],
                                   BS.[Job_ID],
                                   BS.[Code],
                                   BS.[Name],
                                   BS.IsEnabled,
                                   BS.[CreateTime],
                                   V.ItemName PositionName,
	                               bs.FactoryCode,bs.FactoryName
                            -- ,S.Id as PrintId
                            --  ,S.PrintServer+'('+S.IP+')' as Server
                            FROM [dbo].[BS_People] BS
                                LEFT JOIN [dbo].[BS_Departments] depart
                                    ON depart.Code = BS.[Department_ID]
                                -- left join BS_PeopleByPrintServer as P on BS.Code=P.PeopleCode
                                --  left join BS_PrintServer as S on P.PrintServerId=S.Id
                                LEFT JOIN dbo.V_DataDictionary V
                                    ON V.EnCode = 'Position'
                                       AND BS.Position_ID = V.ItemValue
                            WHERE 1 = 1  ");
            var parameter = new List<DbParameter>();
            if (!string.IsNullOrEmpty(queryJson))
            {
                Newtonsoft.Json.Linq.JObject queryParam = queryJson.ToJObject();
                //查询条件 
                //工厂编码
                if (!queryParam["FactoryCode"].IsEmpty())
                {
                    sql.Append($" AND BS.FactoryCode = '{queryParam["FactoryCode"]}'");
                }
                //人名
                if (!queryParam["Name"].IsEmpty())
                {
                    sql.Append($" AND BS.Name like '%{queryParam["Name"]}%'");
                }
                if (!queryParam["queryName"].IsEmpty())
                {
                    sql.Append($" AND BS.Name like '%{queryParam["queryName"]}%'");
                }
                //编码
                if (!queryParam["Code"].IsEmpty())
                {
                    sql.Append($" AND BS.Code like '%{queryParam["Code"]}%'");
                }
                if (!queryParam["queryCode"].IsEmpty())
                {
                    sql.Append($" AND BS.Code like '%{queryParam["queryCode"]}%'");
                }
                //部门
                if (!queryParam["Department_ID"].IsEmpty())
                {
                    sql.Append($" AND BS.Department_ID = '{queryParam["Department_ID"]}'");
                }
                if (!queryParam["DeptName"].IsEmpty())
                {
                    sql.Append($" AND depart.Name like '%{queryParam["DeptName"]}%'");
                }
                //岗位
                if (!queryParam["Position_ID"].IsEmpty())
                {
                    sql.Append($" AND BS.Position_ID = '{queryParam["Position_ID"]}'");
                }
                //职务
                if (!queryParam["Job_ID"].IsEmpty())
                {
                    sql.Append($" AND BS.Job_ID = '{queryParam["Job_ID"]}'");
                }
                //身份证号
                if (!queryParam["CertificateCode"].IsEmpty())
                {
                    sql.Append($" AND BS.CertificateCode = '{queryParam["CertificateCode"]}'");
                }
                //手机号
                if (!queryParam["MobilePhone"].IsEmpty())
                {
                    sql.Append($" AND BS.MobilePhone = '{queryParam["MobilePhone"]}'");
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

        #endregion

        #region 获取实体
        public BS_PeopleEntity GetEntity(Expression<Func<BS_PeopleEntity, bool>> condition)
        {
            return this.BaseRepository().FindEntity(condition);
        }
        #endregion

        #region 新增修改
        public void SaveForm(BS_PeopleEntity entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
            else
            {
                entity.Modify(entity.ID);
                this.BaseRepository().Update(entity);
            }
        }
        #endregion

        #region 删除
        public void DeleteForm(string keyvalue)
        {
            this.BaseRepository().Delete(keyvalue);
        }
        #endregion
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
        /// 模糊查询人员
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DataTable GetListUser(string name, Pagination pagination, string loginUserCode, string factoryCode = "")
        {
            var sql = $@" SELECT P.Code,
                               P.Name,
                               D.Name DeptName
                        FROM dbo.BS_People P
                            INNER JOIN dbo.BS_Departments D
                                ON D.Code = P.Department_ID
                        WHERE (
                                  P.Code LIKE '%{name}%'
                                  OR P.Name LIKE '%{name}%'
                                  OR D.Name LIKE '%{name}%'
                              )
                              AND P.FactoryCode IN
                                  (
                                      SELECT Code
                                      FROM dbo.fn_Split(
                                           (
                                               SELECT FactoryCode FROM BS_People WHERE Code = '{loginUserCode}'
                                           ),
                                           ','
                                                       )
                                  )";

            if (!string.IsNullOrEmpty(factoryCode))
            {
                sql += $@"  AND P.FactoryCode='{factoryCode}' ";
            }
            return this.BaseRepository().FindTable(sql, null, pagination);
        }
    }
}
