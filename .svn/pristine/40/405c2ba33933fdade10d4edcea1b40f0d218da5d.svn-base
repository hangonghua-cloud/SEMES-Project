using Lib.Common;
using MESClient.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.SystemManage
{
    public partial class Frm_Version : Form
    {
        public Frm_Version()
        {
            InitializeComponent();
        }

        private void Frm_Version_Load(object sender, EventArgs e)
        {
            try
            {
                lblVersion.Text = Application.ProductVersion.ToString();

                var postData = new
                {
                    businessType = "2",
                    version = lblVersion.Text.Trim()
                };
                var requestContent = PubFunction.ToJson(postData);
                var responseContent = PubFunction.Post(requestContent, WebApi.UpdateLog.ToString());
                var jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                if (jo["statusCode"].ToString() == "200")
                {
                    if (!string.IsNullOrEmpty(jo["resultData"].ToString()))
                    {
                        string[] arrContent = jo["resultData"]["Content"].ToString().Split('#');
                        foreach (var item in arrContent)
                        {
                            listView1.Items.Add(item);
                        }
                    }
                }
                else
                {
                    MessageBox.Show(jo["returnMsg"].ToString());
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
