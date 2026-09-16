using Lib.Common;
using Lib.Model;
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
    public partial class Frm_UpdatePassword : Form
    {
        public Frm_UpdatePassword()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtConfirm.Text.Trim() != txtNew.Text.Trim())
                {
                    MessageBox.Show("新密码和确认密码不一致");
                    return;
                }

                var postData = new
                {
                    UserCode = CurrentUser.UserCode,
                    OldPassword = txtOld.Text.Trim(),
                    NewPassword = txtNew.Text.Trim()
                };
                var requestContent = PubFunction.ToJson(postData);
                var responseContent = PubFunction.Post(requestContent, WebApi.UpdatePassword.ToString());
                var jo = (JObject)JsonConvert.DeserializeObject(responseContent);
                MessageBox.Show(jo["ret"].ToString());
                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
