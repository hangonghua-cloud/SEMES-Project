using ALP.Application.Entity.Calendar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ALP.Application.Service.Calendar
{
    public class BS_HolidayManageService : Data.Repository.RepositoryFactory<BS_HolidayManage>
    {

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Data(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();

                sql.Append($@"SELECT  [Id]
                                  ,[TheYear]
                                  ,[HolidayName]
                                  ,[HolidayStart]
                                  ,[HolidayEnd]
                                  ,A.Creator
								  ,pe1.Name as [CreatorName]
                                  ,[CreatDate]
                                  ,A.Modifier
                                  ,pe2.Name as [ModifierName]
                                  ,[ModifyDate]
                                  ,[IsEnable]
                              FROM [SIT_UA_MES].[dbo].[BS_HolidayManage] as A
							  LEFT JOIN BS_People pe1 ON pe1.Code = A.Creator COLLATE Chinese_PRC_CI_AS
							  LEFT JOIN BS_People pe2 ON pe1.Code = A.Modifier COLLATE Chinese_PRC_CI_AS
							  where [IsEnable]=1
                        ");
                if (map != null)
                {

                    if (map.ContainsKey("Not_Id"))
                    {
                        sql.Append($" AND [Id]!={map["Not_Id"]}");
                    }
                    if (map.ContainsKey("TheYear"))
                    {
                        sql.Append($" AND [TheYear]='{map["TheYear"]}'");
                    }
                    if (map.ContainsKey("HolidayName"))
                    {
                        sql.Append($" AND [HolidayName] like'%{map["HolidayName"]}%'");
                    }
                    if (map.ContainsKey("HolidayStart") && map.ContainsKey("HolidayEnd"))
                    {
                        sql.Append($" AND '{map["HolidayStart"]}'>=[HolidayStart] AND '{map["HolidayEnd"]}'<=[HolidayEnd] ");
                    }
                    if (map.ContainsKey("HolidayStart") && !map.ContainsKey("HolidayEnd"))
                    {
                        sql.Append($" AND '{map["HolidayStart"]}' between HolidayStart and HolidayEnd ");
                    }
                    if (map.ContainsKey("HolidayEnd") && !map.ContainsKey("HolidayStart"))
                    {
                        sql.Append($" AND '{map["HolidayEnd"]}' between HolidayStart and HolidayEnd ");
                    }
                }
                sql.Append($"  order by Id asc");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 保存工单表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int SaveEntity(string keyValue, BS_HolidayManage entity, out string msg)
        {
            msg = "";
            int n = 0;
            try
            {

                if (!string.IsNullOrEmpty(keyValue))
                {
                    //entity.Modify(keyValue);
                    //判断对象是否存在 
                    BS_HolidayManage model = GetEntity(keyValue);
                    if (model == null)
                    {

                        msg = keyValue + " 对象不存在";
                        n = -1;
                        return n;
                    }
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    //验证名称是否重复 
                    /*map.Add("Not_Id", keyValue);
                    map.Add("TheYear", entity.TheYear.ToString());
                    map.Add("HolidayName", entity.HolidayName);
                    var dt = Get_Data(map, out msg);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.HolidayName + " 重复";
                        return n;
                    } */
                    //验证日期是否重合
                    map.Clear();
                    map.Add("Not_Id", keyValue);
                    map.Add("HolidayStart", entity.HolidayStart.ToString("yyyy-MM-dd"));
                    map.Add("HolidayEnd", entity.HolidayEnd.ToString("yyyy-MM-dd")); 
                    var dt = Get_Data(map, out msg);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.HolidayStart.ToString("yyyy-MM-dd") + $" -- {entity.HolidayEnd.ToString("yyyy-MM-dd")} 与假日【{dt.Rows[0]["HolidayName"].ToString()}】有重叠";
                        return n;
                    }
                    n = this.BaseRepository().Update(entity);
                }
                else
                {
                    //entity.Create();
                    //验证名称是否重复
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    /*map.Add("TheYear", entity.TheYear.ToString());
                    map.Add("HolidayName", entity.HolidayName);
                    var dt = Get_Data(map, out msg);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.HolidayName + " 重复";
                        return n;
                    }*/
                    //验证日期是否重合
                    map.Clear();
                    map.Add("HolidayStart", entity.HolidayStart.ToString("yyyy-MM-dd"));
                    map.Add("HolidayEnd", entity.HolidayEnd.ToString("yyyy-MM-dd"));
                    var dt = Get_Data(map, out msg);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        n = -1;
                        msg = entity.HolidayStart.ToString("yyyy-MM-dd") + $" -- {entity.HolidayEnd.ToString("yyyy-MM-dd")} 与假日【{dt.Rows[0]["HolidayName"].ToString()}】有重叠";
                        return n;
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
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public int DeleteEntity(string keyValue, BS_HolidayManage entity, out string msg)
        {
            msg = "";
            int n = 0;
            try
            { 
                if (!string.IsNullOrEmpty(keyValue))
                { 
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
        /// 得到一个对象
        /// </summary>
        /// <param name="keyValue">主键值</param> 
        /// <returns></returns>
        public BS_HolidayManage GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(Convert.ToInt32(keyValue));
        }
    }
}
