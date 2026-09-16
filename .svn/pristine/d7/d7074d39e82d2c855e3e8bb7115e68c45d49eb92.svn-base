using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using AutoUpdate.Internals;
using Ionic.Zip;
using log4net;
using System.Xml.Serialization;
using System.Security.AccessControl;

namespace AutoUpdate
{
    /// <summary>
    ///     郑书磊 2017年9月19日
    ///     主界面
    /// </summary>
    public partial class MainForm : Form
    {
        //日志记录
        static readonly new ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        ///     实例化本地配置文件
        /// </summary>
        private  LocalConf localConf = new LocalConf();

        private  string tmpFilePath;

        private readonly Thread Reset;
        private readonly Thread mainThread;
        private AutoResetEvent autoResetEvent = new AutoResetEvent(false);


        /// <summary>
        ///     本地缓存目录
        /// </summary>
        private readonly string tmpPath = Path.Combine(Environment.CurrentDirectory, "tmp");

        private Manifest manifest;
        private bool falg=false;

        public MainForm()
        {
            
            Form.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            string url = localConf.Manifest;
            string ip = url.Substring(url.IndexOf("//") + 2, url.IndexOf("/", url.IndexOf("//") + 2) - (url.IndexOf("//") + 2));
            if (!NetTest.CheckServeStatus(ip))
            {
                MessageBox.Show("网络异常，请联系管理员！"+ ip);
                Application.Exit();
                Environment.Exit(0);
            }
            mainThread =new Thread(a =>
            {
                //检查版本数据
                CheckAndUpdate();

            });
            mainThread.Start();
            
            Reset = new Thread(a => {
                while (true)
                {
                    if (falg)
                    {
                        try
                        {
                            Process.Start(Path.Combine(Environment.CurrentDirectory, manifest.ExePath));
                            localConf.Version = manifest.Version;
                            logger.Info("唤起MesClient进程成功，修改本地版本号！！！");
                            Application.Exit();
                            Environment.Exit(0);
                            break;
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex);
                            logger.Info("唤起MesClient进程失败，详细原因请查看运行日志！！！");
                            Application.Exit();
                            Environment.Exit(0);
                        }
                       
                    }
                    Thread.Sleep(100);
                }
            });
            Reset.Start();
            autoResetEvent.WaitOne();
        }

        private void CheckAndUpdate()
        {
            logger.Info("开始匹配服务器版本！");
            //下载服务端配置文件并解析
            var uri = new Uri(localConf.Manifest);
            var doc = GetManifest(uri);
            logger.Info("获取服务端配置文件成功！");
            var xser = new XmlSerializer(typeof(Manifest));
            manifest = xser.Deserialize(new XmlTextReader(doc, XmlNodeType.Document, null)) as Manifest;
            lbRemark.Text = manifest.Description;
            lbVersion.Text = manifest.Version;
            if (manifest.Version != localConf.Version)
            {
                autoResetEvent.Set();
                //设置进度条
                SetProcessBar(20);
                logger.Info("版本号不一致，执行更新！！！");
                //关闭主程序进程
                var cmd = "taskkill /im " + Path.Combine(Environment.CurrentDirectory, manifest.ExePath) + " /f ";
                ExeCommand(cmd);
                logger.Info("关闭MesClient进程成功！！！");
                tmpFilePath = Path.Combine(tmpPath, "files");
                //创建本地缓存目录
                BuildDir(tmpPath);
                //创建本地缓存目录文件
                BuildDir(tmpFilePath);
                ProcessUpdate(); //更新线程 
            }
            else
            {
                
                Process.Start(Path.Combine(Environment.CurrentDirectory, manifest.ExePath));
                Reset.Abort();
                Application.Exit();
                Environment.Exit(0);
            }
        }

        public void ProcessUpdate()
        {
            var serPath = Path.Combine(manifest.WebPath, manifest.Version + ".zip");
            var cliPath = Path.Combine(tmpPath, manifest.Version + ".zip");
            //TODO 判断是完全更新还是部分更新
            if (File.Exists(Path.Combine(Environment.CurrentDirectory, "MESClient.exe")))
            {
                DownZip(serPath, cliPath);
            }
            else
            {
                //DownZip(Path.Combine(manifest.WebPath, manifest.Version + "_all.zip"), cliPath);
                DownZip(Path.Combine(manifest.WebPath, manifest.Version + ".zip"), cliPath);
            }
            SetProcessBar(40);
            UnZip(cliPath, tmpFilePath);
            SetProcessBar(60);
            CopyDirectory(tmpFilePath, Environment.CurrentDirectory);
            logger.Info("覆盖本地文件成功！！！");
            SetProcessBar(90);
            DeleteFolder(tmpPath);
            logger.Info("删除缓存文件成功！！！");
            SetProcessBar(100);
            Thread.Sleep(1000);
            falg = true;
           
            
        }


        /// <summary>
        ///     删除文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="directoryPath"></param>
        public static void DeleteFolder(string directoryPath)
        {
            try
            {
                foreach (var d in Directory.GetFileSystemEntries(directoryPath))
                    if (File.Exists(d))
                    {
                        var fi = new FileInfo(d);
                        if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                            fi.Attributes = FileAttributes.Normal;
                        File.Delete(d); //删除文件   
                    }
                    else
                    {
                        DeleteFolder(d); //删除文件夹
                    }
                Directory.Delete(directoryPath); //删除空文件夹
               
            }
            catch (Exception e)
            {
                logger.ErrorFormat("删除缓存文件失败，错误原因：{0}", e.ToString());
            }
            
        }

        /// <summary>
        ///     复制文件夹（及文件夹下所有子文件夹和文件）
        /// </summary>
        /// <param name="sourcePath">待复制的文件夹路径</param>
        /// <param name="destinationPath">目标路径</param>
        public static void CopyDirectory(string sourcePath, string destinationPath)
        {
            try
            {
                var info = new DirectoryInfo(sourcePath);
                foreach (var fsi in info.GetFileSystemInfos())
                {
                    var destName = Path.Combine(destinationPath, fsi.Name);

                    if (fsi is FileInfo) //如果是文件，复制文件
                    {
                        if(!fsi.FullName.Contains("log4net"))
                             File.Copy(fsi.FullName, destName, true);
                    }
                    else //如果是文件夹，新建文件夹，递归
                    {
                        Directory.CreateDirectory(destName);
                        CopyDirectory(fsi.FullName, destName);
                    }
                }
                
            }
            catch (Exception e)
            {
                logger.ErrorFormat("覆盖本地文件失败，错误原因：{0}",e.ToString());
            }
           
        }

        private void SetProcessBar(int current)
        {
            if (progressBar1.InvokeRequired)
            {
                SetProcessBarCallBack cb = SetProcessBar;
                Invoke(cb, current);
            }
            else
            {
                if (current > 100)
                    current = 100;
                progressBar1.Value = current;
            }
        }


        public void BuildDir(string path)
        {
            try
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
            }
            catch (Exception e)
            {
                logger.ErrorFormat("创建本地缓存目录失败，错误信息：{0}",e.ToString());
            }
           
        }

        public void UnZip(string file, string dir)
        {
            try
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT && Environment.OSVersion.Version.Major == 5 &&
                    Environment.OSVersion.Version.Minor == 1)
                    using (var unzip = new Unzip(file))
                    {
                        unzip.ExtractToDirectory(dir);
                        return;
                    }
                using (var zip = new ZipFile(file, Encoding.Default))
                {
                    zip.ExtractAll(dir, ExtractExistingFileAction.OverwriteSilently);
                }
                logger.Info("解压文件成功！！！");
            }
            catch (Exception e)
            {
                logger.ErrorFormat("解压文件失败，错误原因：{0}",e.ToString());
            }
           
        }

        public static void ExeCommand(string commandText)
        {
            var p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            try
            {
                p.Start();
                p.StandardInput.WriteLine(commandText);
                p.StandardInput.WriteLine("exit");
                //p.StandardOutput.ReadToEnd();
            }
            catch
            {
            }
        }

        public void DownZip(string ser, string cli)
        {
            try
            {
                
                var webClient = new WebClient();
                var uri = new Uri(ser);
                webClient.DownloadFile(uri, cli);
                logger.Info("下载更新文件成功！！！");
            }
            catch (Exception e)
            {

                logger.ErrorFormat("下载更新文件失败，即将重新下载，错误原因：{0}",e.ToString());
                MessageBox.Show("下载更新文件失败，即将重新下载，错误原因："+e.Message);
                DeleteFolder(tmpPath);
                try
                {
                    HttpDownloadFile(ser, cli);
                    logger.Info("下载更新文件成功！！！");
                }
                catch (Exception)
                {
                    logger.ErrorFormat("下载更新文件失败，请联系管理员！，错误原因：{0}", e.ToString());
                    MessageBox.Show("下载更新文件失败，请联系管理员！，错误原因：" + e.Message);
                    Application.Exit();
                    Environment.Exit(0);
                }
                
            }
            
        }
        public static string HttpDownloadFile(string url, string path)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 60000;
            //发送请求并获取相应回应数据
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            Stream responseStream = response.GetResponseStream();
            //创建本地文件写入流
            Stream stream = new FileStream(path, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = responseStream.Read(bArr, 0, (int)bArr.Length);
            while (size > 0)
            {
                stream.Write(bArr, 0, size);
                size = responseStream.Read(bArr, 0, (int)bArr.Length);
            }
            stream.Close();
            responseStream.Close();
            return path;
        }

        private string GetManifest1(Uri uri)
        {
            var response = string.Empty;
            int i =0;
            while (true)
            {
                i++;
                var request = WebRequest.Create(uri);
                request.Credentials = CredentialCache.DefaultCredentials;
            
                request.Timeout = 60000;
                try
                {
                    using (HttpWebResponse res = (HttpWebResponse)request.GetResponse())
                    {
                        if (res.StatusCode!=HttpStatusCode.OK)
                        {
                            continue;
                        }
                        using (var reader = new StreamReader(res.GetResponseStream(), true))
                        {
                            response = reader.ReadToEnd();
                        }
                        break;
                    }
                }
                catch (Exception e)
                {
                    logger.ErrorFormat("访问服务器文件超时，请联系管理员！，错误原因：{0}", e.ToString());
                    if (i>10)
                    {
                        MessageBox.Show("访问服务器文件超时，请联系管理员！"+ e.ToString());
                        Application.Exit();
                        Environment.Exit(0);
                    }
                    else
                        continue;
                    
                }
            }

            return response;
        }

        private string GetManifest(Uri uri)
        {
            var request = WebRequest.Create(uri);
            request.Credentials = CredentialCache.DefaultCredentials;
            var response = string.Empty;
            request.Timeout = 60000;
            try
            {
                using (var res = request.GetResponse())
                {
                    using (var reader = new StreamReader(res.GetResponseStream(), true))
                    {
                        response = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception e)
            {
                logger.ErrorFormat("访问服务器文件超时，请联系管理员！，错误原因：{0}", e.ToString());
                MessageBox.Show("访问服务器文件超时，请联系管理员！");
                Application.Exit();
                Environment.Exit(0);
            }

            return response;
        }

        private delegate void SetProcessBarCallBack(int current);

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            System.Environment.Exit(System.Environment.ExitCode);  
  
            this.Dispose();  
  
            this.Close(); 
        }
    }
}