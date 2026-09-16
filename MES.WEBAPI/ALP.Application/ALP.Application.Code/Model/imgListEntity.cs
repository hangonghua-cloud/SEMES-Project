using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALP.Application.Entity.FileManage
{
    /// <summary>
    /// 1.创建日期: 2021-02-26
    /// 2.创建作者: 刘万军
    /// 3.功能描述: UploadFile实体
    /// 4.任务编号: 
    /// 5.最后修改日期: 
    /// 6.最后修改作者: 
    /// </summary>
    public class imgListEntity
    {
        /// <summary>
        /// 文件内容
        /// </summary>
        public Byte[] ImgData { get; set; } 

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = "";

        /// <summary>
        /// 文件类型(扩展名)
        /// </summary>
        public string ImgType { get; set; } = "";

        /// <summary>
        /// 文件保存路径
        /// </summary>
        public string FilePath { get; set; } = "";
    }
}
