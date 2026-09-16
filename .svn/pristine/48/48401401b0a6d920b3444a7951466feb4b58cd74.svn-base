using Lib.Bll;
using Lib.Model;
using Lib.Model.Dto;
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
    public partial class Frm_PrintLog : Form
    {
        PrintLogBll _printLogBll = new PrintLogBll();//打印日志
        BaseDataBll _baseDataBll = new BaseDataBll();

        public Frm_PrintLog()
        {
            InitializeComponent();
            Init();
        }

        private void Frm_PrintLog_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized; //设置窗体最大化
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Bind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 行选中（主）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvm_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //当点击表头部的列时，e.RowIndex==-1 
                if (e.RowIndex > -1)
                {
                    txtCodes1.Text = this.dgvm.Rows[e.RowIndex].Cells["Code"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void dgvm_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            #region 工厂
            var lstFactory = _baseDataBll.GetFactorySelect(CurrentUser.UserCode);
            lstFactory.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbFactory.DataSource = lstFactory;
            this.cmbFactory.ValueMember = "ItemCode";
            this.cmbFactory.DisplayMember = "ItemName";
            if (lstFactory.Count > 1)
                this.cmbFactory.SelectedIndex = 1;
            else
                this.cmbFactory.SelectedIndex = 0;
            #endregion

            #region 日志类型
            List<Select> lstBusinessType = new List<Select>();
            //lstBusinessType.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            lstBusinessType.Add(new Select() { ItemCode = "1", ItemName = Lib.Common.Language.GetText("Frm_PrintLog.MessageTips_1", "流转卡") });
            lstBusinessType.Add(new Select() { ItemCode = "2", ItemName = Lib.Common.Language.GetText("Frm_PrintLog.MessageTips_2", "唛头") });
            this.cmbBusinessType.DataSource = lstBusinessType;
            this.cmbBusinessType.ValueMember = "ItemCode";
            this.cmbBusinessType.DisplayMember = "ItemName";
            #endregion

            pagerControl1.BindSource += new PagerControl.BindHandle(Bind);//绑定事件
            dtStart.Value = DateTime.Now.AddDays(-7);
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        private void Bind()
        {
            if (string.IsNullOrEmpty(cmbFactory.SelectedValue.ToString()))
            {
                MessageBox.Show(Lib.Common.Language.GetText("Frm_PrintLog.MessageTips_3", "请选择工厂！"));
                return;
            }

            #region 参数赋值
            PrintLogDto query = new PrintLogDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.BusinessType = cmbBusinessType.SelectedValue.ToString();
            query.StartTime = dtStart.Value;
            query.EndTime = dtEnd.Value;
            query.Code = txtCode.Text.Trim();
            query.Operator = txtOperator.Text.Trim();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "CreateTime Desc";
            int record = 0;
            var data = _printLogBll.GetDataTableWithPage(query, out record);
            pagerControl1.Record = record;

            dgvm.AutoGenerateColumns = false;
            dgvm.DataSource = data;
            dgvm.ClearSelection();
        }
        #endregion
    }
}
