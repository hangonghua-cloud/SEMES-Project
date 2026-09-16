using ALP.Application.Busines.ProduceManage;
using ALP.Application.Entity.ProduceManage;
using Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.ProduceManage
{
    public partial class Frm_OwnProductBGEdit : Form
    {
        PM_MaterialBatchUpRecordBLL _rawMaterialBatchUpBLL = new PM_MaterialBatchUpRecordBLL();

        public PM_OwnProductBGEntity entity = new PM_OwnProductBGEntity();

        public Frm_OwnProductBGEdit()
        {
            InitializeComponent();
        }

        private void Frm_OwnProductBGEdit_Load(object sender, EventArgs e)
        {
            try
            {
                txtRollCode.Text = entity.RollCode;
                txtBGQty.Text = entity.BGQty.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var bgQty = txtBGQty.Text;
                var flag = Regex.IsMatch(bgQty, "^[0-9]*$");//整数
                if (!flag)
                {
                    MessageUtil.ShowWarning("不良数量格式不对");
                    return;
                }
                entity.BGQty = Convert.ToInt32(bgQty);
                var result= _rawMaterialBatchUpBLL.OwnProductBGEditRoallQty(entity);
                if (result.Success)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show(result.returnMsg);
                    return;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
