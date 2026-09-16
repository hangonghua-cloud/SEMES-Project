using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace MESClient
{
    public partial class Frm_GenerateJson : Form
    {
        public Frm_GenerateJson()
        {
            InitializeComponent();
        }

        Dictionary<string, string> buttonDic = new Dictionary<string, string>();
        Dictionary<string, string> lableDic = new Dictionary<string, string>();
        Dictionary<string, string> otherDic = new Dictionary<string, string>();
        Dictionary<string, dynamic> tableDic = new Dictionary<string, dynamic>();
        private void button1_Click(object sender, EventArgs e)
        {

            var name = this.textBox1.Text + "." + this.textBox2.Text;
            if (!string.IsNullOrEmpty(name))
            {
                var form = Assembly.GetExecutingAssembly().CreateInstance(name) as Form;//此方式name是命名空间+form名称
                buttonDic = new Dictionary<string, string>();
                lableDic = new Dictionary<string, string>();
                otherDic = new Dictionary<string, string>();
                tableDic = new Dictionary<string, dynamic>();
                LoadAllControlJson(form, form.Controls);
            }
            else
            {
                MessageBox.Show("No Find Form Name");
            }
        }

        private void LoadAllControlJson(Form form, Control.ControlCollection sonControls)
        {
            //遍历所有控件
            foreach (Control control in sonControls)
            {
                /*
                control 中有HasChildren 可以判断组件是否含有其他组件，
                但是datagridview也会进入判断，不确定其他的控件会不会有此类问题，
                此项目暂时只考虑用到的容器控件。
                */
                if (control is DataGridView)
                {
                    Dictionary<string, string> dgvDic = new Dictionary<string, string>();
                    var dgv = (DataGridView)control;
                    foreach (var column in dgv.Columns)
                    {
                        if (column is DataGridViewTextBoxColumn)
                        {
                            var item = (DataGridViewTextBoxColumn)column;

                            dgvDic.Add(item.Name, item.HeaderText);

                        }
                    }
                    tableDic.Add(control.Name, dgvDic);
                }
                else
                if (control is GroupBox || control is Panel || control is TabControl || control is SplitContainer)
                {
                    if (!string.IsNullOrEmpty(control.Name))
                    {
                        otherDic.Add(control.Name, control.Text);
                    }
                    LoadAllControlJson(form, control.Controls);
                }
                else
                {
                    if (control is Button)
                    {
                        var c = (Button)control;
                        buttonDic.Add(c.Name, c.Text);
                    }
                    else if (control is Label)
                    {
                        var c = (Label)control;
                        lableDic.Add(c.Name, c.Text);
                    }
                }

            }
            var json = new
            {
                Title = form.Text,
                Button = buttonDic,
                Label = lableDic,
                Table = tableDic,
                Other = otherDic,
            };
            var jsonStr = JsonConvert.SerializeObject(json);
            this.textBox3.Text = "\"" + form.Name + "\":" + jsonStr + "";

            this.textBox3.Refresh();
        }

        private void Frm_GenerateJson_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(GetLanIp());
        }
        /// <summary>
        /// 获取局域网IP
        /// </summary>
        private static string GetLanIp()
        {
            var Ani = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var ni in Ani)
            {
                var ua = ni.GetIPProperties().UnicastAddresses.ToArray();
                foreach (var va in ua)
                {
                    if (va.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return va.Address.ToString();
                    }
                }
            }
            return "";
        }
    }
}
