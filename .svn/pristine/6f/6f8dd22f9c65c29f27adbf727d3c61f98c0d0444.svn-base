using ALP.Application.Entity.BaseManage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Service.ModelLevel
{
    public class LeverService : Data.Repository.RepositoryFactory
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

                sql.Append($@"SELECT [ResourceCode]
                                  ,[ResourceName]
                                  ,[ModelLeve]
                                  ,[ParentResource]
                                  ,[Describe] ,SortCode
                              FROM [dbo].[BS_ModelWithResource] where [EnabledMark]=1 
                        ");
                if (map != null)
                {
                    if (map.ContainsKey("ResourceCode"))
                    {
                        sql.Append($" AND [ResourceCode]='{map["ResourceCode"]}'");
                    }
                    if (map.ContainsKey("ResourceName"))
                    {
                        if (!string.IsNullOrEmpty(map["ResourceName"]))
                            sql.Append($" AND [ResourceName]='{map["ResourceName"]}'");
                    }
                    if (map.ContainsKey("ModelLeve"))
                    {
                        sql.Append($" AND [ModelLeve]='{map["ModelLeve"]}'");
                    }
                    if (map.ContainsKey("ParentResource") && map.ContainsKey("ParentResource"))
                    {
                        sql.Append($" AND [ParentResource]='{map["ParentResource"]}'");
                    }
                }
                sql.Append($" order by SortCode ");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 查询层级的值
        /// </summary>
        /// <returns></returns>
        public DataTable Get_FieldData(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"select b.FieldType,b.FieldCode,b.FieldName,m.FieldValue,'{map["ResourceCode"]}' as ResourceCode  from [dbo].[BS_ModelLevelExtendFields] b
                               left join BS_ModelResourceExtendInfo m on b.FieldCode=m.FieldCode and m.EnabledMark=1 and m.ResourceCode='{map["ResourceCode"]}'
                                where 1=1 and b.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("LevelCode"))
                    {
                        if (!string.IsNullOrEmpty(map["LevelCode"]))
                            sql.Append($" AND  b.LevelCode='{map["LevelCode"]}'");
                    }
                }
                sql.Append($" order by b.CreateDate");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// 查询层级的值
        /// </summary>
        /// <returns></returns>
        public DataTable Get_FieldData2(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"select b.FieldType,b.FieldCode,b.FieldName,m.FieldValue,'{map["ResourceCode"]}' as ResourceCode,b.LevelCode  from [dbo].[BS_ModelLevelExtendFields] b
                               left join BS_ModelResourceExtendInfo m on b.FieldCode=m.FieldCode and m.ResourceCode='{map["ResourceCode"]}'
                                where 1=1 and b.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("LevelCode"))
                    {
                        if (!string.IsNullOrEmpty(map["LevelCode"]))
                            sql.Append($" AND  [LevelCode]='{map["LevelCode"]}'");
                    }
                }
                sql.Append($" order by b.CreateDate");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 取指定层级下的所有资源
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ModelResourceExtendInfo_ByLevelCode(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                var sql1 = new StringBuilder();
                var sql2 = new StringBuilder();
                sql.Append($@"SELECT M1.ResourceCode EnCode,M1.ResourceName EnName,M.ResourceCode,M.ResourceName,M.ModelLeve,M.SortCode
                                from  [dbo].[BS_ModelWithResource] M 
								LEFT JOIN [dbo].[BS_ModelWithResource] M1 ON m1.ResourceCode=M.ParentResource
                                 ");
                sql1.Append(@" WHERE M.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("LevelCode"))
                    {
                        if (!string.IsNullOrEmpty(map["LevelCode"]))
                            sql1.Append($" AND  M.ModelLeve='{map["LevelCode"]}'");

                        if (map["LevelCode"] == "Factory")
                        {
                            //如果不是管理员，需限制工厂权限
                            if (!map.ContainsKey("role") || map["role"] != "admin")
                                sql1.Append($" AND M.ResourceCode IN(SELECT Code FROM dbo.fn_Split((SELECT FactoryCode FROM BS_People WHERE Code='{map["userCode"]}'),',')) ");
                        }
                    }
                    if (map.ContainsKey("Name"))
                    {
                        if (!string.IsNullOrEmpty(map["Name"]))
                            sql1.Append($" AND ( M.ResourceCode like '%{map["Name"]}%' OR M.ResourceName like '%{map["Name"]}%')");
                    }
                }
                sql.Append(sql2);
                sql.Append(sql1);
                sql.Append(@" ORDER BY M.SortCode ");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }




        /// <summary>
        /// 取指定层级下的所有资源
        /// </summary>
        /// <returns></returns>
        public DataTable Get_ResourceExtendByLevelCode(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT M1.ResourceCode EnCode,M1.ResourceName EnName,M.ResourceCode,M.ResourceName,M.SortCode,N.FieldCode,isnull(N.FieldValue,'') as FieldValue ,M.ModelLeve
                                from  [dbo].[BS_ModelWithResource] M 
								LEFT JOIN [dbo].[BS_ModelWithResource] M1 ON m1.ResourceCode=M.ParentResource
                                left join [dbo].[BS_ModelResourceExtendInfo] N on M.ResourceCode=N.ResourceCode 
                                WHERE M.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("LevelCode"))
                    {
                        if (!string.IsNullOrEmpty(map["LevelCode"]))
                            sql.Append($" AND  M.ModelLeve='{map["LevelCode"]}'");
                    }
                    if (map.ContainsKey("FieldCode"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldCode"]))
                            sql.Append($" AND  N.FieldCode='{map["FieldCode"]}'");
                    }
                    if (map.ContainsKey("FieldValue"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldValue"]))
                            sql.Append($" AND  N.FieldValue='{map["FieldValue"]}'");
                    }
                    if (map.ContainsKey("Describe"))
                    {
                        if (!string.IsNullOrEmpty(map["Describe"]))
                            sql.Append($" AND  M.Describe='{map["Describe"]}'");
                    }
                }
                sql.Append(@" ORDER BY M.SortCode ");
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据编码 数量 值，查询指定资源
        /// </summary>
        /// <param name="map"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public dynamic GetDynamicModelWithResource(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            dynamic dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT M1.ResourceCode EnCode,M1.ResourceName EnName,M.ResourceCode,M.ResourceName,N.FieldCode,isnull(N.FieldValue,'') as FieldValue ,M.ModelLeve
                                from  [dbo].[BS_ModelWithResource] M 
								LEFT JOIN [dbo].[BS_ModelWithResource] M1 ON m1.ResourceCode=M.ParentResource
                                left join [dbo].[BS_ModelResourceExtendInfo] N on M.ResourceCode=N.ResourceCode 
                                WHERE M.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("ResourceCode"))
                    {
                        if (!string.IsNullOrEmpty(map["ResourceCode"]))
                            sql.Append($" AND  M.ResourceCode='{map["ResourceCode"]}'");
                    }
                    if (map.ContainsKey("FieldCode"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldCode"]))
                            sql.Append($" AND  N.FieldCode='{map["FieldCode"]}'");
                    }
                    if (map.ContainsKey("FieldValue"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldValue"]))
                            sql.Append($" AND  N.FieldValue='{map["FieldValue"]}'");
                    }
                }
                dt = this.BaseRepository().Query(sql.ToString()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// 获取指定厂库
        /// </summary>
        /// <param name="map"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetDynamicModelWithResource1(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT M1.ResourceCode EnCode,M1.ResourceName EnName,M.ResourceCode,M.ResourceName,N.FieldCode,isnull(N.FieldValue,'') as FieldValue ,M.ModelLeve
                                from  [dbo].[BS_ModelWithResource] M 
								LEFT JOIN [dbo].[BS_ModelWithResource] M1 ON m1.ResourceCode=M.ParentResource
                                left join [dbo].[BS_ModelResourceExtendInfo] N on M.ResourceCode=N.ResourceCode 
                                WHERE M.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("ResourceCode"))
                    {
                        if (!string.IsNullOrEmpty(map["ResourceCode"]))
                            sql.Append($" AND  M.ResourceCode='{map["ResourceCode"]}'");
                    }
                    if (map.ContainsKey("FieldCode"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldCode"]))
                            sql.Append($" AND  N.FieldCode='{map["FieldCode"]}'");
                    }
                    if (map.ContainsKey("FieldValue"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldValue"]))
                            sql.Append($" AND  N.FieldValue='{map["FieldValue"]}'");
                    }
                }
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// 根据工厂取出工序
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessByFactory(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                string factoryCode = map["LevelCode"];
                if (string.IsNullOrEmpty(factoryCode))
                {
                    return null;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT ResourceCode,
                                   ResourceName
                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Process'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelWithResource
                                              WHERE ModelLeve = 'Factory'
                                                    AND EnabledMark = 1
                                                    AND ResourceCode = '{factoryCode}'
                                          )
                                                AND EnabledMark = 1
                                      )
                            ORDER BY SortCode ");

                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据工厂、属性取出工序
        /// </summary>
        /// <returns></returns>
        public DataTable GetProcessByFactoryExtendInfo(string factoryCode, string fieldCode, string fieldValue, string name = "")
        {
            DataTable dt = null;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT a.ResourceCode,
                                   a.ResourceName
                            FROM dbo.BS_ModelWithResource a
                                INNER JOIN dbo.BS_ModelResourceExtendInfo b
                                    ON a.ResourceCode = b.ResourceCode
                                       AND b.FieldCode = '{fieldCode}'
                                       AND b.FieldValue = '{fieldValue}'
                            WHERE a.ModelLeve = 'Process'
                                  AND a.EnabledMark = 1
                                  AND a.ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource = '{factoryCode}'
                                                AND EnabledMark = 1
                                      )
                            ORDER BY a.SortCode ");

                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        /// <summary>
        /// 通过 工厂查找虚拟仓库仓位
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetWhoseLocationByFac(string factoryCode, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {

                if (string.IsNullOrEmpty(factoryCode))
                {
                    return null;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append($@"			 SELECT  c.ResourceName as 'ckResourceName',c.ResourceCode as 'ckResourceCode',d.ResourceName as 'kwResourceName',d.ResourceCode as 'kwResourceCode' FROM  BS_ModelWithResource A
			 LEFT JOIN BS_ModelWithResource b ON b.ParentResource=a.ResourceCode
			 LEFT JOIN BS_ModelWithResource c ON b.ResourceCode=c.ParentResource
			 LEFT JOIN BS_ModelWithResource d ON c.ResourceCode=d.ParentResource
		 WHERE  a.ResourceCode =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = '{factoryCode}'
                                                    AND EnabledMark = 1
                                          )
										  AND c.ResourceCode IN (SELECT ResourceCode  FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'CKSX'
                                                    AND FieldValue = '3'
                                                    AND EnabledMark = 1 )
 ");

                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }
        /// <summary>
        /// jpf
        /// 通过工厂查找仓库层级
        /// 2022-12-15 
        /// </summary>
        /// <param name="factoryCode"></param>
        /// <returns></returns>
        public DataTable GetWhsNameByFactory(string factoryCode)
        {
            DataTable dt = null;

            if (string.IsNullOrEmpty(factoryCode))
            {
                return null;
            }
            StringBuilder sql = new StringBuilder();
            sql.Append($@"			SELECT ResourceCode,
                                   ResourceName
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = '{factoryCode}'
                                                    AND EnabledMark = 1
                                          )
                                                AND EnabledMark = 1
												 ORDER BY SortCode ");

            dt = this.BaseRepository().FindTable(sql.ToString());
            return dt;
        }

        /// <summary>
        /// 根据工厂找仓库下一 层级
        /// </summary>
        /// <returns></returns>
        public DataTable GetWarehouseByFactory(string factoryCode, string name = "")
        {
            DataTable dt = null;

            if (string.IsNullOrEmpty(factoryCode))
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT ResourceCode,
                                   ResourceName
                            FROM dbo.BS_ModelWithResource
                            WHERE ModelLeve = 'Warehouse'
                                  AND EnabledMark = 1
                                  AND ParentResource IN
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelWithResource
                                          WHERE ParentResource =
                                          (
                                              SELECT ResourceCode
                                              FROM dbo.BS_ModelResourceExtendInfo
                                              WHERE FieldCode = 'GLGC'
                                                    AND FieldValue = '{factoryCode}'
                                                    AND EnabledMark = 1
                                          )
                                                AND EnabledMark = 1
                                      ) ");
            if (!string.IsNullOrEmpty(name))
                sql.Append($@" AND (ResourceCode LIKE '%{name}%' OR ResourceName LIKE '%{name}%') ");

            sql.Append($@" ORDER BY SortCode ");

            dt = this.BaseRepository().FindTable(sql.ToString());
            return dt;
        }

        /// <summary>
        /// 根据工厂、属性找仓库
        /// </summary>
        /// <returns></returns>
        public DataTable GetWarehouseByFactoryExtendInfo(string factoryCode, string fieldCode, string fieldValue, string name = "")
        {
            DataTable dt = null;

            if (string.IsNullOrEmpty(factoryCode))
            {
                return null;
            }

            StringBuilder sql = new StringBuilder();
            sql.Append($@"SELECT a.ResourceCode,
                               a.ResourceName,
                               b.FieldCode
                        FROM dbo.BS_ModelWithResource a
                            INNER JOIN dbo.BS_ModelResourceExtendInfo b
                                ON a.ResourceCode = b.ResourceCode
                                   AND b.FieldCode = '{fieldCode}'
                                   AND b.FieldValue = '{fieldValue}'
                        WHERE a.ModelLeve = 'Warehouse'
                              AND a.EnabledMark = 1
                              AND a.ParentResource IN
                                  (
                                      SELECT ResourceCode
                                      FROM dbo.BS_ModelWithResource
                                      WHERE ParentResource =
                                      (
                                          SELECT ResourceCode
                                          FROM dbo.BS_ModelResourceExtendInfo
                                          WHERE FieldCode = 'GLGC'
                                                AND FieldValue = '{factoryCode}'
                                                AND EnabledMark = 1
                                      )
                                            AND EnabledMark = 1
                                  ) ");
            if (!string.IsNullOrEmpty(name))
                sql.Append($@" AND (a.ResourceCode LIKE '%{name}%' OR a.ResourceName LIKE '%{name}%') ");

            sql.Append($@" ORDER BY SortCode ");

            dt = this.BaseRepository().FindTable(sql.ToString());
            return dt;
        }
        /// <summary>
        /// 获取工厂对应工序对应机台列表
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public DataTable GetAllFactoryProcessMachine(out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {

                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT M1.ResourceCode FactoryCode,
                                   M1.ResourceName FactoryName,
                                   M3.ResourceCode ProcessCode,
                                   M3.ResourceName ProcessName,
                                   M4.ResourceCode MachineCode,
                                   M4.ResourceName MachineName
                            FROM dbo.BS_ModelWithResource M1 --gongc 
                                INNER JOIN dbo.BS_ModelWithResource M2
                                    ON M1.ResourceCode = M2.ParentResource --gongx 
                                INNER JOIN dbo.BS_ModelWithResource M3
                                    ON M2.ResourceCode = M3.ParentResource --jit 
                                INNER JOIN dbo.BS_ModelWithResource M4
                                    ON M3.ResourceCode = M4.ParentResource
		                            WHERE M1.ModelLeve='Factory' ");

                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据ParentSource取出列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByParentResource(string parentResource, string resourceName, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(parentResource))
                {
                    return null;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append($@"SELECT ResourceCode,ResourceName FROM dbo.BS_ModelWithResource 
                    WHERE ParentResource='{parentResource}' AND EnabledMark=1 ");
                if (!string.IsNullOrEmpty(resourceName))
                {
                    sql.Append($" and ResourceName Like '%{resourceName}%'");
                }


                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 根据ParentSource取出列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetListByProductionMachine(string parentResource, string resourceName, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                if (string.IsNullOrEmpty(parentResource))
                {
                    return null;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append($@"  SELECT A.ResourceCode,A.ResourceName FROM  BS_ModelWithResource A
                  LEFT JOIN  BS_ModelWithResource B
                  ON A.ResourceCode=B.ParentResource 
                    WHERE  B.ResourceCode ='{parentResource}' AND A.EnabledMark=1 ");
                if (!string.IsNullOrEmpty(resourceName))
                {
                    sql.Append($" and ResourceName Like '%{resourceName}%'");
                }


                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }


        /// <summary>
        /// 取指定产品类型的产线
        /// </summary>
        /// <returns></returns>
        public DataTable Get_Lines_ByProductType(Dictionary<string, string> map, out string msg)
        {
            msg = "";
            DataTable dt = null;
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append($@" SELECT N.ResourceCode, N.ResourceName,M.FieldValue as FieldName, O.FieldValue  
								 FROM [dbo].[BS_ModelResourceExtendInfo] M 
								 inner join [dbo].[BS_ModelWithResource] N on M.ResourceCode=N.ResourceCode
								 inner join [dbo].[BS_ModelResourceExtendInfo] O on M.ResourceCode=O.ResourceCode AND O.FieldCode='capacity'
								 WHERE  M.EnabledMark=1  and N.EnabledMark=1 ");
                if (map != null)
                {
                    if (map.ContainsKey("ModelLeve"))
                    {
                        if (!string.IsNullOrEmpty(map["ModelLeve"]))
                            sql.Append($" AND N.ModelLeve='{map["ModelLeve"]}'");
                    }
                    if (map.ContainsKey("FieldCode"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldCode"]))
                            sql.Append($" AND M.FieldCode='{map["FieldCode"]}'");
                    }
                    if (map.ContainsKey("FieldValue"))
                    {
                        if (!string.IsNullOrEmpty(map["FieldValue"]))
                            sql.Append($" AND  M.FieldValue='{map["FieldValue"]}'");
                    }
                }
                dt = this.BaseRepository().FindTable(sql.ToString());
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return dt;
        }

        /// <summary>
        /// 保存层级的值
        /// </summary>
        /// <returns></returns>
        public bool Save_FieldData(BsModelResourceExtendInfoEntity entity, out string msg)
        {
            msg = "";
            bool b = false;
            try
            {
                string StrSQL = "";
                string ExistSql = $@"select Id from  [dbo].[BS_ModelResourceExtendInfo] where ResourceCode='{entity.ResourceCode}' and FieldCode='{entity.FieldCode}' ";

                var ExistDt = this.BaseRepository().FindTable(ExistSql.ToString());
                if (ExistDt == null || ExistDt.Rows.Count == 0)
                {

                    StrSQL = $@"INSERT INTO [dbo].[BS_ModelResourceExtendInfo]
                                   ([Id]
                                   ,[ResourceCode]
                                   ,[FieldCode]
                                   ,[FieldValue]
                                   ,[EnabledMark]
                                   ,CreateDate
                                   ,[CreateUser])
                             VALUES
                                   ('{entity.Id}'
                                   ,'{entity.ResourceCode}'
                                   ,'{entity.FieldCode}'
                                   ,'{entity.FieldValue}'
                                   ,1
                                   ,getdate()
                                   ,'{entity.CreateUser}')";

                }
                else
                {
                    string id = ExistDt.Rows[0][0].ToString();
                    StrSQL = $@" UPDATE [dbo].[BS_ModelResourceExtendInfo]
                                   SET [FieldValue] = '{entity.FieldValue}'
                                      ,[CreateDate] = '{entity.CreateDate}'
                                      ,[ModifyUser] = '{entity.ModifyUser}'
                                      ,[ModifyDate] = getdate()
                                 WHERE [Id]='{id}'";
                }
                int n = this.BaseRepository().ExecuteBySql(StrSQL);
                b = n > 0 ? true : false;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }
            return b;
        }

        public BsModelWithResourceEntity GetModelResourceByChild(string ResourceCode)
        {
            var sql = $@"
		   SELECT M1.* FROM dbo.BS_ModelWithResource M1
		   INNER JOIN dbo.BS_ModelWithResource M2 ON M1.ResourceCode=M2.ParentResource
		   WHERE M2.ResourceCode='{ResourceCode}'";
            return this.BaseRepository().FindList<BsModelWithResourceEntity>(sql).FirstOrDefault();
        }
        public IEnumerable<BsModelWithResourceEntity> GetModelResourceByFactory(string ModelLeve, string PResourceName, string ResourceName)
        {
            var sql = $@"SELECT a.* FROM  BS_ModelWithResource a
LEFT JOIN BS_ModelWithResource b ON a.ParentResource=b.ResourceCode
LEFT JOIN BS_ModelWithResource c ON b.ParentResource=c.ResourceCode
WHERE a.ModelLeve='{ModelLeve}' AND c.ResourceName='{PResourceName}' AND a.ResourceName='{ResourceName}'";
            return this.BaseRepository().FindList<BsModelWithResourceEntity>(sql);

        }

        public IEnumerable<BsModelWithResourceEntity> GetListExprocess(Expression<Func<BsModelWithResourceEntity, bool>> condition)
        {
            return this.BaseRepository().IQueryable(condition);
        }
    }
}
