using Lib.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.PackingBG
{
    public partial class FormSelect : Form
    {
        PackingBGBll _packBGBLL = new PackingBGBll();

        public string templateName = "";//模板名称
        public string bjgy = "";//背胶工艺
        public string lotNo = "";
        public DateTime setDate = DateTime.Now;

        public string WorkOrder = "";
        public string FactoryCode = "";
        public FormSelect()
        {
            InitializeComponent();
        }
        private void FormSelect_Load(object sender, EventArgs e)
        {
            try
            {
                var dt = _packBGBLL.GetMarkTemplate(WorkOrder, FactoryCode);
                this.cmbTemplate.DataSource = dt;
                cmbTemplate.ValueMember = "唛头编码";
                cmbTemplate.DisplayMember = "唛头名称";
           

                dtpSetDate.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        private void btnMarkPrint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.cmbTemplate.Text))
            {
                MessageBox.Show("请选择打印模板");
                return;
            }
            templateName = this.cmbTemplate.Text;
            bjgy = this.txtBJGY.Text.Trim();
            lotNo = this.dtpSetDate.Value.ToString("ddMMyy");
            setDate = this.dtpSetDate.Value;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
