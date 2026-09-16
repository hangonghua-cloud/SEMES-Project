using Lib.Bll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.TransferCard
{
    public partial class Frm_TransferCardSplit : Form
    {
        public dynamic main = new ExpandoObject();
        public dynamic mainCard = new ExpandoObject();

        TransferCardBll bll = new TransferCardBll();

        public Frm_TransferCardSplit()
        {
            InitializeComponent();
        }

        private void Frm_TransferCardSplit_Load(object sender, EventArgs e)
        {
            try
            {
                Assignment();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证
                if (string.IsNullOrEmpty(txtSplitQty.Text.Trim()))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_1", "请输入拆分托数"));
                    return;
                }
                Regex rex = new Regex(@"^\d+$");//^开始，\d匹配一个数字字符，+出现至少一次，$结尾
                if (!rex.IsMatch(txtSplitQty.Text.Trim()))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_2", "拆分托数格式不正确"));
                    return;
                }
                var num = Convert.ToInt32(txtSplitQty.Text.Trim());
                if (num > 7)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_3", "拆分托数不能大于7"));
                    return;
                }
                #endregion

                DataTable dt = new DataTable();
                dt.Columns.Add("CardCode", typeof(string));
                dt.Columns.Add("CardName", typeof(string));

                string[] arrLetter = new string[] { "A", "B", "C", "D", "E", "F", "G" };
                for (int i = 0; i < num; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["CardCode"] = mainCard.CardCode + "-" + arrLetter[i];
                    dr["CardName"] = mainCard.CardName + "-" + arrLetter[i];
                    dt.Rows.Add(dr);
                }
                dgvSplit.AutoGenerateColumns = false;
                dgvSplit.DataSource = dt;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                dgvSplit.DataSource = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证
                if (dgvSplit.Rows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_4", "没有要拆分的记录，无法保存"));
                    return;
                }
                #endregion

                var oldCard = bll.GetCardEntity(mainCard.CardCode);

                List<dynamic> lstCard = new List<dynamic>();
                foreach (DataGridViewRow item in dgvSplit.Rows)
                {
                    var card = Common.Clone(oldCard);
                    card.CardCode = item.Cells["CardCode"].Value.ToString();
                    card.CardName = item.Cells["CardName"].Value.ToString();
                    lstCard.Add(card);
                }
                var result = bll.SplitTransferCard(lstCard, oldCard);
                if (result > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_5", "拆分成功"));
                    this.Close();
                }
                else
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCardSplit.MessageTips_6", "拆分失败"));
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 公共方法
        /// <summary>
        /// 赋值
        /// </summary>
        private void Assignment()
        {
            txtProcutOrder.Text = main.ProductOrder;
            txtContainerNO.Text = main.ContainerNO;
            txtCardName.Text = mainCard.CardName;
        }
        #endregion
    }
}
