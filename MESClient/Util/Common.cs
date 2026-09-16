using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient
{
    public class Common
    {
        /// <summary>
        /// 遍历dgv ，赋值给 models 
        /// </summary>
        /// <param name="dgv">需要赋值的 实体类的模型</param>
        /// <param name="idColumnName">主要列名，控件列名要和实体列名一致</param>
        /// <returns>返回 已赋值的 实体类合集</returns>
        public static List<T> FillModelsByDgv<T>(DataGridView dgv, string idColumnName) where T : class, new()
        {
            //用于返回的结果
            List<T> models = new List<T>();
            T model = null;
            //遍历 DataGridView 所有行
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //判断关键列是否是空，空就跳过
                if (dgr.Cells[idColumnName].EditedFormattedValue == null || dgr.Cells[idColumnName].EditedFormattedValue.ToString() == "") continue;
                //定义个中间obj

                model = new T();
                //遍历 DataGridView 所有列
                foreach (DataGridViewColumn dgc in dgv.Columns)
                {
                    try
                    {
                        //判断当前单元格是否无值，无则跳过
                        if (dgr.Cells[dgc.Name].EditedFormattedValue == null || dgr.Cells[dgc.Name].EditedFormattedValue.ToString() == "")
                            continue;

                        //反射模型的属性
                        PropertyInfo field = model.GetType().GetProperty(dgc.Name);
                        //判断当前列类型
                        var dataType = field.PropertyType.FullName;
                        switch (dataType)
                        {
                            //给模型的属性赋值
                            case "System.Int32": { field.SetValue(model, int.Parse(dgr.Cells[dgc.Name].EditedFormattedValue.ToString()), null); } break;
                            case "System.String": { field.SetValue(model, dgr.Cells[dgc.Name].EditedFormattedValue.ToString(), null); } break;
                            case "System.Decimal": { field.SetValue(model, Decimal.Parse(dgr.Cells[dgc.Name].EditedFormattedValue.ToString()), null); } break;
                            case "System.DateTime": { field.SetValue(model, DateTime.Parse(dgr.Cells[dgc.Name].EditedFormattedValue.ToString()), null); } break;
                            case "System.Boolean": { field.SetValue(model, Boolean.Parse(dgr.Cells[dgc.Name].EditedFormattedValue.ToString()), null); } break;
                            case "System.Char": { field.SetValue(model, dgr.Cells[dgc.Name].EditedFormattedValue.ToString(), null); } break;
                            default: { } break;
                        }

                    }
                    catch (Exception ex) { throw new Exception(ex.Message); }
                }
                //将赋值后的模型添加到 结果集中
                models.Add(model);

            }
            return models;
        }
        /// <summary>
        /// DataGridView转Dynamic
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="idColumnName"></param>
        /// <returns></returns>
        public static List<dynamic> GetModelsByDgv(DataGridView dgv, string idColumnName)
        {
            //用于返回的结果
            List<dynamic> models = new List<dynamic>();
            dynamic model = new ExpandoObject();
            //遍历 DataGridView 所有行
            foreach (DataGridViewRow dgr in dgv.Rows)
            {
                //判断关键列是否是空，空就跳过
                if (dgr.Cells[idColumnName].EditedFormattedValue == null || dgr.Cells[idColumnName].EditedFormattedValue.ToString() == "") continue;
                //定义个中间obj

                model = new ExpandoObject();
                //遍历 DataGridView 所有列
                foreach (DataGridViewColumn dgc in dgv.Columns)
                {
                    try
                    {
                        //判断当前单元格是否无值，无则跳过
                        if (dgr.Cells[dgc.Name].EditedFormattedValue == null || dgr.Cells[dgc.Name].EditedFormattedValue.ToString() == "")
                            continue;

                        //判断当前列类型
                        (model as ICollection<KeyValuePair<string, object>>).Add(new KeyValuePair<string, object>(dgc.Name, dgr.Cells[dgc.Name].EditedFormattedValue.ToString()));
                    }
                    catch { }
                }
                //将赋值后的模型添加到 结果集中
                models.Add(model);

            }
            return models;
        }
        /// <summary>
        /// 深拷贝
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 服务器文件下载到本地
        /// </summary>
        /// <param name="serverPath"></param>
        /// <param name="filePath"></param>
        public static void Download(string serverPath, string filePath)
        {
            WebClient client = new WebClient();
            string fileName = serverPath.Substring(serverPath.LastIndexOf("/") + 1); ;//被下载的文件名
            string path = filePath + fileName;//另存为地址

            client.DownloadFile(serverPath, path);
        }
    }
}
