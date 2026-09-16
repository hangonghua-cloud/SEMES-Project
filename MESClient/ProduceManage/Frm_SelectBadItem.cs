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

namespace MESClient.ProduceManage
{
    public partial class Frm_SelectBadItem : Form
    {
        public string processCode = "";//工序编码
        public string badItemCode = "";//不良项目编码
        public string badItemName = "";//不良项目名称

        BaseDataBll _baseBLL = new BaseDataBll();

        public Frm_SelectBadItem()
        {
            InitializeComponent();
        }

        private void Frm_SelectBadItem_Load(object sender, EventArgs e)
        {
            try
            {
                GetPMProcessBadItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 工序报工不良项目配置
        /// </summary>
        private void GetPMProcessBadItem()
        {
            var result = _baseBLL.GetPMProcessBadItem(processCode);
            if (result.Success)
            {
                var data = result.resultData;
                if (!string.IsNullOrEmpty(txtKeyWord.Text.Trim()))
                {
                    var keyWord = txtKeyWord.Text.Trim();
                    data = data.Where(t => t.label.Contains(keyWord)).ToList();
                }

                dgv1.AutoGenerateColumns = false;
                dgv1.DataSource = data;
            }
            else
            {
                MessageBox.Show(result.returnMsg);
                return;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetPMProcessBadItem();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }     
        }

        private void dgv1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //当点击表头部的列时，e.RowIndex==-1 
                if (e.RowIndex > -1)
                {
                    badItemCode = this.dgv1.Rows[e.RowIndex].Cells["value"].Value.ToString();
                    badItemName = this.dgv1.Rows[e.RowIndex].Cells["label"].Value.ToString();

                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
