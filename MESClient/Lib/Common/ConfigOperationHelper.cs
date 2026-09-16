using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lib.Common
{
    public class ConfigOperationHelper : ConfigurationSection
    {
        #region Config读写
        public string PortName
        {
            get { return this["COM"].ToString(); }
            set { this["COM"] = value; }
        }

        public int BaudRate
        {
            get { return (int)this["BaudRate"]; }
            set { this["BaudRate"] = value; }
        }
        public int DataBits
        {
            get { return (int)this["DataBits"]; }
            set { this["DataBits"] = value; }
        }

        //ConfigOperationHelper data = new ConfigOperationHelper();

        public Type GetValue<Type>(string key)
        {
            var converter = TypeDescriptor.GetConverter(typeof(Type));
            var value = ConfigurationManager.AppSettings[key];
            return (Type)converter.ConvertTo(value, typeof(Type));
        }
        public void SetValue(string key, dynamic value)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings[key] != null)
            {
                config.AppSettings.Settings[key].Value = value;
            }
            else
            {
                config.AppSettings.Settings.Add(key, value);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
        #endregion


        #region XML读写 

        Dictionary<string, string> dic = new Dictionary<string, string>();
        public const string filePath = "setting.xml";
        /// <summary>
        /// 读取Xml的节点信息
        /// </summary>
        /// <param name="filePath"></param>
        public void ReadXml()
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(@filePath);
                XmlElement root = xml.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                foreach (XmlNode node in nodeList)
                {
                    XmlNodeList childNoteList = node.ChildNodes;
                    foreach (XmlNode childNote in childNoteList)
                    {
                        if (childNote.NodeType.ToString() != "Comment")
                        {
                            dic[childNote.Name] = childNote.InnerText;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
            }

        }
        //public void ReadXml(string path)
        //{
        //    try
        //    {
        //        XmlDocument xml = new XmlDocument();
        //        xml.Load(@path);
        //        XmlElement root = xml.DocumentElement;
        //        XmlNodeList nodeList = root.ChildNodes;
        //        foreach (XmlNode node in nodeList)
        //        {
        //            XmlNodeList childNoteList = node.ChildNodes;
        //            foreach (XmlNode childNote in childNoteList)
        //            {
        //                if (childNote.NodeType.ToString() != "Comment")
        //                {
        //                    dic[childNote.Name] = childNote.InnerText;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //}
        /// <summary>
        /// 读取根据node名获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetKeyValuesXML(string name)
        {
            this.ReadXml();
            return dic.ContainsKey(name) ? dic[name] : "";
        }
        /// <summary>
        /// 更新xml文件值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SaveUpdateXml(string key, string value)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(@filePath);
            XmlNode xnuser = doc.SelectSingleNode("appSettings/appSetting");
            XmlNodeList nodeList = xnuser.ChildNodes;
            bool isCreate = true;
            foreach (XmlNode item in nodeList)
            {
                if (item.Name == key)
                {
                    isCreate = false;
                    item.InnerText = value;
                }
            }
            if (isCreate)
            {
                XmlNode newNode = doc.CreateElement(key);
                newNode.InnerText = value;
                doc.SelectSingleNode("appSettings/appSetting").AppendChild(newNode);
            }
            doc.Save(@filePath);
        }

        #endregion
    }
}
