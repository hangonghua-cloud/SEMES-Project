using System;
using System.IO;
using System.Security.AccessControl;
using System.Xml;

namespace AutoUpdate
{
    /// <summary>
    ///     郑书磊 日期
    ///     本地配置文件
    /// </summary>
    public class LocalConf
    {
        private static readonly XmlDocument document = new XmlDocument();
        private static readonly string xmlFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "update.xml");

        static LocalConf()
        {
            document.Load(xmlFileName);
        }

        public string Version
        {
            get { return document.SelectSingleNode("localconf").SelectSingleNode("version").InnerText; }
            set
            {
                document.SelectSingleNode("localconf").SelectSingleNode("version").InnerText = value;

                //获取文件信息
                FileInfo fileInfo = new FileInfo(xmlFileName);
                //获得该文件的访问权限
                System.Security.AccessControl.FileSecurity fileSecurity = fileInfo.GetAccessControl();
                //添加ereryone用户组的访问权限规则 完全控制权限
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                //添加Users用户组的访问权限规则 完全控制权限
                fileSecurity.AddAccessRule(new FileSystemAccessRule("Users", FileSystemRights.FullControl, AccessControlType.Allow));
                //设置访问权限
                fileInfo.SetAccessControl(fileSecurity);
                document.Save(xmlFileName);
            }
        }


        public string Manifest
        {
            get { return document.SelectSingleNode("localconf")?.SelectSingleNode("manifest")?.InnerText; }
            set
            {
                document.SelectSingleNode("localconf").SelectSingleNode("manifest").InnerText = value;
                document.Save(xmlFileName);
            }
        }

        public string Update
        {
            get { return document.SelectSingleNode("localconf")?.SelectSingleNode("update")?.InnerText; }
            set
            {
                document.SelectSingleNode("localconf").SelectSingleNode("update").InnerText = value;
                document.Save(xmlFileName);
            }
        }
    }
}