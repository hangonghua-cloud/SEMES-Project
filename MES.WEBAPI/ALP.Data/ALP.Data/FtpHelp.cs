
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace ALP.Data
{
    public class FtpHelp
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="fileinfo">需要上传的文件</param>
        /// <param name="targetDir">目标路径</param>
        /// <param name="hostname">ftp地址</param>
        /// <param name="username">ftp用户名</param>
        /// <param name="password">ftp密码</param>
        public static void UploadFile(FileInfo fileinfo, string targetDir, string hostname, string username, string password)
        {
            //1. check target
            string oldFileName = "Timeconfig.txt";
            string target;
            if (targetDir.Trim() == "")
            {
                return;
            }
            target = Guid.NewGuid().ToString();  //使用临时文件名
            string oldURI = "FTP://" + hostname + "/" + targetDir + "/" + oldFileName;
            string URI = "FTP://" + hostname + "/" + targetDir + "/" + target;

            System.Net.FtpWebRequest ftp = GetRequest(URI, username, password);

            ftp.Method = System.Net.WebRequestMethods.Ftp.UploadFile;
            //指定文件传输的数据类型
            ftp.UseBinary = true;
            ftp.UsePassive = false;

            //告诉ftp文件大小
            ftp.ContentLength = fileinfo.Length;
            //缓冲大小设置为2KB
            const int BufferSize = 2048;
            byte[] content = new byte[BufferSize - 1 + 1];
            int dataRead;

            //打开一个文件流 (System.IO.FileStream) 去读上传的文件
            using (FileStream fs = fileinfo.OpenRead())
            {
                try
                {
                    //把上传的文件写入流
                    using (Stream rs = ftp.GetRequestStream())
                    {
                        do
                        { //每次读文件流的2KB
                            dataRead = fs.Read(content, 0, BufferSize);
                            rs.Write(content, 0, dataRead);
                        } while (!(dataRead < BufferSize));
                        rs.Close();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"读上传的文件出错!错误信息如下：{ex.Message}");
                }
                finally
                {
                    fs.Close();
                }

            }

            try
            {
                var ftpIsExistsFile = IsftpExistsFile(oldFileName, "10.17.2.9", "ftpuser", "1qaz2wsx!@",targetDir);
                if (ftpIsExistsFile)
                {
                    ftp = null;
                    ftp = GetRequest(oldURI, username, password);
                    ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                    ftp.GetResponse();
                }
                ftp = GetRequest(URI, username, password);
                ftp.Method = System.Net.WebRequestMethods.Ftp.Rename;
                ftp.RenameTo = fileinfo.Name;
                ftp.GetResponse();
            }
            catch (Exception ex)
            {
                ftp = GetRequest(URI, username, password);
                ftp.Method = System.Net.WebRequestMethods.Ftp.DeleteFile; //删除
                ftp.GetResponse();
                throw ex;
            }
            finally
            {
                ftp = null;

            }

        }


        /// <summary>
        /// 判断ftp服务器上该目录是否存在
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="ftpHostIP"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private static bool IsftpExistsFile(string dirName, string ftpHostIP, string username, string password,string dir= "MaterialPullConfig")
        {
            bool flag = true;
            try
            {
                string uri = "ftp://" + ftpHostIP + "/"+dir+"/" + dirName;
                System.Net.FtpWebRequest ftp = GetRequest(uri, username, password);
                ftp.Method = WebRequestMethods.Ftp.ListDirectory;

                FtpWebResponse response = (FtpWebResponse)ftp.GetResponse();
                response.Close();
            }
            catch (Exception)
            {
                flag = false;
            }
            return flag;
        }

        private static FtpWebRequest GetRequest(string URI, string username, string password)
        {
            //根据服务器信息FtpWebRequest创建类的对象
            FtpWebRequest result = (FtpWebRequest)FtpWebRequest.Create(URI);
            //提供身份验证信息
            result.Credentials = new System.Net.NetworkCredential(username, password);
            //设置请求完成之后是否保持到FTP服务器的控制连接，默认值为true
            result.KeepAlive = false;
            result.UsePassive = false;
            return result;
        }
    }
}