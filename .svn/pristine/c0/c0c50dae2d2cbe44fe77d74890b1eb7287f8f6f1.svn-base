using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Lib.Model;
using Lib.Common;
using MESClient.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MESClient
{
    public partial class Frm_Login : Form
    {
        databaseoperate myoperate = new databaseoperate();
        public Frm_Login()
        {
            InitializeComponent();
        }
        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Frm_login_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btn_ok_Click(object sender, EventArgs e)
        {
            try
            {
                var postData = new
                {
                    UserCode = txtUserCode.Text.Trim(),
                    password = txtPassword.Text.Trim()
                };
                var requestContent = PubFunction.ToJson(postData);
                var responseContent = PubFunction.Post(requestContent, WebApi.Login.ToString());
                var jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                if (jo["code"].ToString() == "100")
                {
                    CurrentUser.UserCode = jo["data"]["result"]["UserCode"].ToString();
                    CurrentUser.UserName = jo["data"]["result"]["UserName"].ToString();
                    CurrentUser.FactoryCode = jo["data"]["result"]["FactoryCode"].ToString();
                    CurrentUser.FactoryName = jo["data"]["result"]["FactoryName"].ToString();

                    Frm_Main newfrm = new Frm_Main();
                    this.Hide();
                    newfrm.Show();
                }
                else
                {
                    MessageBox.Show(jo["ret"].ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //按回车,焦点跳到密码文本框
        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                txtPassword.Focus();
            }
        }
        //按回车,直接登陆
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btn_ok_Click(null, null);
            }
        }
        ConfigOperationHelper configOperationHelper = new ConfigOperationHelper();
        private void Frm_Login_Load(object sender, EventArgs e)
        {
            #region 多国语言图标样式调整
            btnLanguage.FlatStyle = FlatStyle.Flat;
            btnLanguage.BackColor = Color.Transparent;
            btnLanguage.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnLanguage.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnLanguage.FlatAppearance.CheckedBackColor = Color.Transparent;
            btnLanguage.FlatAppearance.BorderSize = 0;
            #endregion
            //string language = configOperationHelper.GetValue<string>("language");
            //语言
            string language = RegistryHelper.GetValue(Consts.Language);//存储到注册表里面
            //所在地
            string location = RegistryHelper.GetValue(Consts.Location);//存储到注册表里面

            cmb_DataInput(comboBox1, string.IsNullOrEmpty(language) ? "zh-CN" : language, new List<dynamic>() {
                new { key = "简体中文", value = "zh-CN", },
                new { key = "Tiếng Việt", value = "vi-VN", },
                new { key = "ภาษาไทย", value = "th-TH", },
                new { key = "English", value = "en-US", }});
            cmb_DataInput(comboBox2, string.IsNullOrEmpty(location) ? "CN" : location, new List<dynamic>() {
                new { key =Lib.Common.Language.GetText("Location.CN", "中国工厂") , value = "CN", },
                new { key =Lib.Common.Language.GetText("Location.VN", "越南工厂") , value = "VN", },
                new { key =Lib.Common.Language.GetText("Location.TH", "泰国工厂") , value = "TH", }
            });

            Lib.Common.Language.SetLanguage(comboBox1.SelectedValue.ToString());
            Lib.Common.Language.SetLocation(comboBox2.SelectedValue.ToString());


            Lib.Resources.FormLanguage.SetFormLanguage(this);

            //FormLayout.SetFormLayout(this);
        }

        /// <summary>
        /// 绑定下拉菜单通用
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="DefaultSelectCode"></param>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void cmb_DataInput(ComboBox cmb, string DefaultSelectCode = "", dynamic list = null, string key = "key", string value = "value")
        {
            if (list == null)
            {
                return;
            }
            var dt = new DataTable();
            dt.Columns.Add("Key", typeof(string));
            dt.Columns.Add("Value", typeof(string));
            //dt.Rows.Add("--请选择--", "");
            foreach (var item in list)
            {
                dt.Rows.Add(
                    item.GetType().GetProperty(key).GetValue(item, null),
                    item.GetType().GetProperty(value).GetValue(item, null)
                    );
            }
            cmb.DataSource = dt;
            cmb.ValueMember = "Value";
            cmb.DisplayMember = "Key";
            cmb.AutoCompleteSource = AutoCompleteSource.ListItems;   //设置自动完成的源
            cmb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;   //设置自动完成的的形式

            #region 默认选择
            if (!string.IsNullOrEmpty(DefaultSelectCode))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Value"].ToString() == DefaultSelectCode)
                    {
                        cmb.SelectedIndex = i;
                    }
                }
            }
            #endregion

        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int x = (int)(0.5 * (this.Width - label3.Width));
            int y = label3.Location.Y;
            label3.Location = new System.Drawing.Point(x, y);
        }
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            Lib.Common.Language.SetLanguage(comboBox1.SelectedValue.ToString());
            //configOperationHelper.SetValue("language", comboBox1.SelectedValue.ToString());
            RegistryHelper.SaveValue(Consts.Language, comboBox1.SelectedValue.ToString());

            //所在地
            string location = RegistryHelper.GetValue(Consts.Location);//存储到注册表里面
            cmb_DataInput(comboBox2, string.IsNullOrEmpty(location) ? "CN" : location, new List<dynamic>() {
                new { key =Lib.Common.Language.GetText("Location.CN", "中国工厂") , value = "CN", },
                new { key =Lib.Common.Language.GetText("Location.VN", "越南工厂") , value = "VN", },
                new { key =Lib.Common.Language.GetText("Location.TH", "泰国工厂") , value = "TH", }
            });

            Lib.Resources.FormLanguage.SetFormLanguage(this);

            FormLayout.SetFormLayout(this);
            //方法体结构少简写也不妨碍阅读，如果多的话，建议还是写个方法，阅读起来方便
            //Language.SetVoidLanguage(() =>
            //{
            //    this.label1.Left = this.txtUserCode.Left - this.label1.Width - 2;
            //    this.label2.Left = this.txtPassword.Left - this.label2.Width - 2;
            //}, () =>
            //{

            //    this.label1.Left = this.txtUserCode.Left - this.label1.Width - 2;
            //    this.label2.Left = this.txtPassword.Left - this.label2.Width - 2;
            //});
            this.Refresh();
            this.OnResize(e);
        }

        private void comboBox2_SelectedValueChanged(object sender, EventArgs e)
        {
            Lib.Common.Language.SetLocation(comboBox2.SelectedValue.ToString());
            RegistryHelper.SaveValue(Consts.Location, comboBox2.SelectedValue.ToString());
        }
    }
}