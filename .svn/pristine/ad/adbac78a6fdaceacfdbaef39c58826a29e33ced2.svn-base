1、配置本地update.xml的内容
<?xml version="1.0" encoding="utf-8" ?>
<localconf>
  <!--版本号，无格式要求，更新时需要修改-->
  <version>1.7</version>
  <!--服务端信息存放的位置-->
  <manifest>http://172.16.175.145/AutoUpdate/manifest.xml</manifest>
  <!--基于此次应用不需要修改-->
  <update>AutoUpdate.exe</update>
</localconf>
2、配置服务端manifest.xml的内容
<?xml version="1.0" encoding="utf-8" ?>
<manifest>
  <!--版本号，无格式要求，更新时需要修改-->
  <version>1.8</version>
  <description>更新说明</description>
  <!--启动程序的名称-->
  <exepath>ConsoleAppTool.exe</exepath>
  <!--更新文件存放的web地址，文件名必须是新版本号.zip,此处只用写文件的web目录-->
  <webpath>http://172.16.175.145/AutoUpdate/</webpath>
</manifest>