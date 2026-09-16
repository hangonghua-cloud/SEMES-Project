using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lib.Common
{
    public class SerialHelper
    {
        public SerialPort _serialPort = new SerialPort();
        //private static string SerialPort = ConfigurationManager.AppSettings["Port"]?.ToString();
        //private static string BaudRate = ConfigurationManager.AppSettings["BaudRate"]?.ToString();
        private Action<string> action;
        public SerialHelper(Action<string> act, string serialPort, string baudRate, string isSet = "")//初始化
        {
            //关闭串口时回抛异常
            try
            {
                _serialPort.PortName = serialPort;//ports[0].ToString();//串口号//
                _serialPort.BaudRate = Convert.ToInt32(baudRate ?? "9600");//波特率
                _serialPort.DataBits = 8;//数据位
                _serialPort.StopBits = StopBits.One;//停止位
                _serialPort.Parity = Parity.None;//校验位
                if (!string.IsNullOrEmpty(isSet))
                {
                    _serialPort.WriteBufferSize = 2048;
                    _serialPort.Handshake = Handshake.None;
                    _serialPort.ReceivedBytesThreshold = 18;
                }
                //_serialPort.ReadTimeout = -1;
                //_serialPort.DTREnable = true;
                //_serialPort.RTSEnable = true;

                SerialOpen();
                action = act;

            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning("串口报错:" + ex.Message);
            }
        }


        public void SerialOpen()//串口开
        {
            try
            {
                _serialPort.Open();
                _serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);//添加数据接收事件
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning("串口报错:" + ex.Message);
            }
        }

        public void SerialClose()//串口关
        {
            try
            {
                _serialPort.DataReceived -= DataReceivedHandler;
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning("串口报错:" + ex.Message);
            }
        }

        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)//读取下位机的数据
        {
            try
            {
                int len = _serialPort.BytesToRead;
                byte[] buffer = new byte[len];
                _serialPort.Read(buffer, 0, len);
                //MessageUtil.ShowWarning(string.Join(",",buffer));
               var result = Encoding.ASCII.GetString(buffer);
                action(result);
                //string strData = BitConverter.ToString(buffer, 0, len);
                //if (!result.Contains("zero"))
                //{
                //    var reg = new Regex(@"(?<=\+)\d*", RegexOptions.Multiline);
                //    result = reg.Match(result).Value;

                //    action(result);
                //}
                //else
                //{
                //action("");
                //}

            }
            catch (Exception ex)
            {
                if(ex.Message.IndexOf("Input string was not in a correct forma")==-1)
                    MessageUtil.ShowWarning("串口报错:" + ex.Message);
            }

        }

        public void SerialSend(string SendData)//发送按钮
        {
            try
            {
                //string a = SendData.Trim();
                SendData = SendData.Replace(" ", "");
                byte[] Data = new byte[SendData.Length / 2];
                for (int i = 0; i < SendData.Length / 2; i++)
                {
                    //每次取两位字符组成一个16进制
                    Data[i] = Convert.ToByte(SendData.Substring(i * 2, 2), 16);
                }
                _serialPort.Write(Data, 0, Data.Length);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning("串口报错:" + ex.Message);

                //Global.ShowMessge(ex.Message);
            }

        }
        public void SerialStopSend()//停止接收数据
        {
            try
            {
                byte[] data = { 0x99 };
                _serialPort.DataReceived -= DataReceivedHandler;
                _serialPort.Write(data, 0, data.Length);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning("串口报错:" + ex.Message);
            }

        }
    }
}
