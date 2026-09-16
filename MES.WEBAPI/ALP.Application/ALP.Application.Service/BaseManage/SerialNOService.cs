using ALP.Application.Entity;
using ALP.Application.Entity.BaseManage;
using ALP.Application.IService;
using ALP.Application.IService.BaseManage;
using ALP.Data;
using ALP.Data.Repository;
using ALP.Util;
using ALP.Util.Extension;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;

namespace ALP.Application.Service.BaseManage
{
    public class SerialNOService : RepositoryFactory<BaseSequenceEntity>, ISerialNOService
    {

        /// <summary>
        ///  增加一条数据
        /// </summary>
        //public int Add(Maticsoft.Model.Base_DataItem model)
        //{
        //    int rowsAffected;
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@Id", SqlDbType.Int,4),
        //            new SqlParameter("@ItemId", SqlDbType.VarChar,50),
        //            new SqlParameter("@ParentId", SqlDbType.VarChar,50),
        //            new SqlParameter("@ItemCode", SqlDbType.VarChar,50),
        //            new SqlParameter("@ItemName", SqlDbType.VarChar,50),
        //            new SqlParameter("@IsTree", SqlDbType.Int,4),
        //            new SqlParameter("@IsNav", SqlDbType.Int,4),
        //            new SqlParameter("@SortCode", SqlDbType.Int,4),
        //            new SqlParameter("@DeleteMark", SqlDbType.Int,4),
        //            new SqlParameter("@EnabledMark", SqlDbType.Int,4),
        //            new SqlParameter("@Description", SqlDbType.VarChar,200),
        //            new SqlParameter("@CreateDate", SqlDbType.DateTime),
        //            new SqlParameter("@CreateUserId", SqlDbType.VarChar,50),
        //            new SqlParameter("@CreateUserName", SqlDbType.VarChar,50),
        //            new SqlParameter("@ModifyDate", SqlDbType.DateTime),
        //            new SqlParameter("@ModifyUserId", SqlDbType.VarChar,50),
        //            new SqlParameter("@ModifyUserName", SqlDbType.VarChar,50)};
        //    parameters[0].Direction = ParameterDirection.Output;
        //    parameters[1].Value = model.ItemId;
        //    parameters[2].Value = model.ParentId;
        //    parameters[3].Value = model.ItemCode;
        //    parameters[4].Value = model.ItemName;
        //    parameters[5].Value = model.IsTree;
        //    parameters[6].Value = model.IsNav;
        //    parameters[7].Value = model.SortCode;
        //    parameters[8].Value = model.DeleteMark;
        //    parameters[9].Value = model.EnabledMark;
        //    parameters[10].Value = model.Description;
        //    parameters[11].Value = model.CreateDate;
        //    parameters[12].Value = model.CreateUserId;
        //    parameters[13].Value = model.CreateUserName;
        //    parameters[14].Value = model.ModifyDate;
        //    parameters[15].Value = model.ModifyUserId;
        //    parameters[16].Value = model.ModifyUserName;

        //    DbHelperSQL.RunProcedure("Base_DataItem_ADD", parameters, out rowsAffected);
        //    return (int)parameters[0].Value;
        //}
        /// <summary>
        /// 同步信息
        /// </summary>
        /// <param name="batch"></param>
        /// <param name="tableName"></param>
        public void Sync_InterfaceToMOM(string batch, string tableName)
        {
            //var parameter = new List<DbParameter>
            //{
            //    DbParameters.CreateDbParameter("@Batch", batch),
            //    DbParameters.CreateDbParameter("@TableName", tableName)
            //};
            this.BaseRepository().ExecuteBySql(" exec [P_Sync_InterfaceToMOM] '" + batch + "','" + tableName + "'");
        }

        /// <summary>
        /// 根据单据类型获取流水号
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
                //执行存贮过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                db2.ExecuteProcedure("P_GetSerialNO", parameters);
                //完善代码
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
        /// 查询按钮权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="Funcntions"></param>
        /// <param name="messageCode"></param>
        /// <returns></returns>
        public DataTable FucntionAuthority(string userId, string Funcntions, out string messageCode)
        {

            DataTable dt = new DataTable();
            messageCode = "";
            //调用存储过程
            SqlParameter[] parameters = {
                    new SqlParameter("@UserId", SqlDbType.VarChar,60),
                    new SqlParameter("@Funcntions", SqlDbType.VarChar,800)
            };
            parameters[0].Value = userId;
            parameters[1].Value = Funcntions;
            //parameters[2].Direction = ParameterDirection.Output;

            try
            {
                //执行存贮过程
                Data.Dapper.SqlDatabase db2 = new Data.Dapper.SqlDatabase();
                dt = db2.ExecuteProc_Table("P_FucntionAuthority", parameters);
                //完善代码 

            }
            catch (Exception ex)
            {
                messageCode = ex.Message;
            }
            return dt;
        }
    }
}
