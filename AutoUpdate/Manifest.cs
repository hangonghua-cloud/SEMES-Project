using System.Xml.Serialization;

namespace AutoUpdate
{
    /// <summary>
    ///     郑书磊 2017年9月19日
    ///     服务端配置文件
    /// </summary>
    [XmlRoot("manifest")]
    public class Manifest
    {
        [XmlElement("version")]
        public string Version { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("webpath")]
        public string WebPath { get; set; }

        [XmlElement("exepath")]
        public string ExePath { get; set; }
    }
}