using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace ALP.Application.WebApi.Common
{
    public class DtoHelper
    {

        public static string SetSql(string sql, object obj)
        {

            obj.GetType().GetProperties().ToList().ForEach(x =>
            {
                string str = "@" + x.Name;
                sql = sql.Replace(str, x.GetValue(obj, null) == null ? "" : x.GetValue(obj, null).ToString());
            });

            return sql;
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static T GetEntity<T>(DataTable table) where T : new()
        {
            var entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties().Where(item => row.Table.Columns.Contains(item.Name)).Where(item => DBNull.Value != row[item.Name]))
                {
                    try
                    {
                        item.SetValue(entity, row[item.Name] == DBNull.Value ? null : Convert.ChangeType(row[item.Name], item.PropertyType), null);

                    }
                    catch (Exception)
                    {
                    }
                }
            }
            return entity;
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public static IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                var entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    try
                    {
                        item.SetValue(entity, row[item.Name] == DBNull.Value ? null : Convert.ChangeType(row[item.Name], item.PropertyType), null);
                    }
                    catch (Exception ex)
                    {
                    }
                }
                entities.Add(entity);
            }
            return entities;
        }

        /// <summary>
        /// 从IDataReader中获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static T GetEntity<T>(IDataReader reader) where T : new()
        {
            var entity = new T();
            while (reader.Read())
            {
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    foreach (
                        var item in
                            entity.GetType()
                                .GetProperties()
                                .Where(item => reader.GetName(i).ToUpper() == item.Name.ToUpper()))
                    {
                        item.SetValue(entity, reader[item.Name] == DBNull.Value ? null : Convert.ChangeType(reader[item.Name], item.PropertyType), null);
                    }
                }

            }
            reader.Close();

            return entity;
        }

        /// <summary>
        /// 从DataReader获取对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IList<T> GetEntities<T>(IDataReader reader) where T : new()
        {
            IList<T> entities = new List<T>();
            while (reader.Read())
            {
                var entity = new T();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    foreach (
                        var item in
                            entity.GetType()
                                .GetProperties()
                                .Where(item => reader.GetName(i).ToUpper() == item.Name.ToUpper()))
                    {
                        item.SetValue(entity, reader[item.Name] == DBNull.Value ? "" : Convert.ChangeType(reader[item.Name], item.PropertyType), null);
                    }
                }
                entities.Add(entity);
            }
            reader.Close();
            return entities;
        }

        /// <summary>
        /// 从DataReader获取对象列表(分页)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataReader"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public static IList<T> GetPageByIDataReader<T>(IDataReader dataReader, int pageSize, int pageIndex) where T : new()
        {
            var rowIndexFrom = pageSize * (pageIndex - 1);
            var rowIndexTo = pageSize * pageIndex - 1;
            return GetEntitisPageByIDataReader<T>(dataReader, rowIndexFrom, rowIndexTo);
        }
        private static IList<T> GetEntitisPageByIDataReader<T>(IDataReader dataReader, int rowIndexFrom, int rowIndexTo) where T : new()
        {
            var list = new List<T>();
            int rowIndex = 0;
            while (dataReader.Read())
            {
                if (rowIndexFrom > rowIndex)
                {
                    rowIndex++;
                    continue;
                }
                if (rowIndexTo < rowIndex)
                    break;
                var entity = new T();

                for (var i = 0; i < dataReader.FieldCount; i++)
                {
                    foreach (
                        var item in
                            entity.GetType()
                                .GetProperties()
                                .Where(item => dataReader.GetName(i).ToUpper() == item.Name.ToUpper()))
                    {
                        item.SetValue(entity, dataReader[item.Name] == DBNull.Value ? null : Convert.ChangeType(dataReader[item.Name], item.PropertyType), null);
                    }
                }
                list.Add(entity);
                rowIndex++;
            }

            dataReader.Close();

            return list;
        }


        /// <summary>
        /// 日志文件写入每天生成一个文件 txt
        /// </summary>
        /// <param name="fstyle"></param>
        /// <param name="messagestr"></param>
        public static void WriteLogWorkDate(string fstyle, string messagestr)
        {
            #region  日志文件写入每天生成一个文件 txt
            try
            {
                //创建一个文件流，用以写入或者创建一个StreamWriter
                string filenamenew = fstyle + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                //包装箱文件夹不存在创建
                string xhpath = AppDomain.CurrentDomain.BaseDirectory + "log\\" + date + "\\";
                //文件夹不存在创建
                if (!Directory.Exists(xhpath))
                {
                    Directory.CreateDirectory(xhpath);
                }

                filenamenew = xhpath + filenamenew;

                FileStream fs = new FileStream(filenamenew, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter m_streamWriter = new StreamWriter(fs);
                m_streamWriter.Flush();
                // 使用StreamWriter来往文件中写入内容
                m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
                // 把richTextBox1中的内容写入文件
                m_streamWriter.Write(messagestr + ";\r\n");
                //关闭此文件
                m_streamWriter.Flush();
                m_streamWriter.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
            }
            #endregion
        }

        internal static void WriteLogWorkDate(string v, object p)
        {
            throw new NotImplementedException();
        }
    }
}
