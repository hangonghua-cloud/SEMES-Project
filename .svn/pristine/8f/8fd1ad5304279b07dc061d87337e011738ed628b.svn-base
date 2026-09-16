using Lib.Bll;
using Lib.Common;
using Lib.Model;
using Lib.Model.Dto;
using MESClient.Util;
using ReportLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.PackingBG
{
    public partial class Frm_PackingBG : Form
    {
        PackingBGBll bll = new PackingBGBll();
        BaseDataBll _baseBll = new BaseDataBll();//基础数据
        PrintLogBll _printLogBll = new PrintLogBll();//打印日志

        public Frm_PackingBG()
        {
            InitializeComponent();
            Init();
        }

        #region 窗体控件事件
        private void Frm_PackingBG_Load(object sender, EventArgs e)
        {
            try
            {
                tabControl1.SelectedIndex = 1;
                txtProductOrder.GotFocus += new EventHandler(txtProductOrder_GotFocus);  //获取焦点前发生事件
                txtContainerNO.GotFocus += new EventHandler(txtContainerNO_GotFocus);  //获取焦点前发生事件
                this.WindowState = FormWindowState.Maximized; //设置窗体最大化

                Bind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 唛头码打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMarkCodePrint_Click(object sender, EventArgs e)
        {
            //选择行数
            List<string> lstMarkCode = new List<string>();
            for (int i = 0; i < dgvd2.Rows.Count; i++)
            {
                if ((bool)dgvd2.Rows[i].Cells[0].EditedFormattedValue == true)
                {
                    lstMarkCode.Add(dgvd2.Rows[i].Cells["PackTransferCode2"].Value.ToString());
                }
            }
            if (lstMarkCode.Count == 0)
            {
                MessageBox.Show(Language.GetText("Frm_PackingBG.MessageTips_2", "请选择要打印的唛头码!"));
                return;
            }
            string codes = string.Join(",", lstMarkCode);

            #region 记录打印日志
            //记录打印日志
            dynamic logEntity = new ExpandoObject();
            logEntity.Id = Guid.NewGuid().ToString();
            logEntity.FactoryCode = CurrentUser.FactoryCode;
            logEntity.FactoryName = CurrentUser.FactoryName;
            logEntity.BusinessType = "2"; //1：唛头
            logEntity.Code = codes;
            logEntity.Creator = CurrentUser.UserCode;
            logEntity.Operator = CurrentUser.UserName;
            _printLogBll.InsertPrintLog(logEntity);
            #endregion

            #region 打印
            var workOrder = dgvd2.Rows[0].Cells["WorkOrder2"].Value.ToString();
            var factoryCode = cmbFactory.SelectedValue.ToString();
            var dt = bll.GetMarkCodeTemplate(workOrder, factoryCode);
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show(Language.GetText("Frm_PackingBG.MessageTips_4", "未找到打印模板"));
                return;
            }
            foreach (DataRow item in dt.Rows)
            {
                var templateName = item["MarkName"].ToString();
                //将原有的文件进行删除
                var filePath = Application.StartupPath + @"/Template/";
                string filename = filePath + templateName;
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);

                string serverAddress = Lib.Http.AddressUrl;
                string serverPath = serverAddress + templateName;
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);

                Common.Download(serverPath, filePath);

                //打印
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", codes);
                ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);
            }
            #endregion

            //数据绑定
            DataRow dr = ((DataRowView)dgvm.CurrentRow.DataBoundItem).Row;
            BindDetail2(dr["WorkOrder"].ToString());
        }
        /// <summary>
        /// 唛头信息打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                //选择行数
                List<string> lstMarkCode = new List<string>();
                for (int i = 0; i < dgvd2.Rows.Count; i++)
                {
                    if ((bool)dgvd2.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        lstMarkCode.Add(dgvd2.Rows[i].Cells["PackTransferCode2"].Value.ToString());
                    }
                }
                if (lstMarkCode.Count == 0)
                {
                    MessageBox.Show(Language.GetText("Frm_PackingBG.MessageTips_3", "请选择要打印的唛头信息!"));
                    return;
                }
                string codes = string.Join(",", lstMarkCode);

                string templateName = "";
                string bjgy = "";
                string lotNo = "";
                DateTime setDate = DateTime.Now;
                FormSelect formSelect = new FormSelect();
                formSelect.WorkOrder = dgvd2.Rows[0].Cells["WorkOrder2"].Value.ToString();
                formSelect.FactoryCode = cmbFactory.SelectedValue.ToString();
                formSelect.ShowDialog();
                if (formSelect.DialogResult == DialogResult.OK)
                {
                    //打印
                    templateName = formSelect.templateName;
                    bjgy = formSelect.bjgy;
                    lotNo = formSelect.lotNo;
                    setDate = formSelect.setDate;

                    ////将原有的文件进行删除
                    string filename = Application.StartupPath + @"/Template/" + templateName;
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);

                    string serverAddress = Lib.Http.AddressUrl;
                    string serverPath = serverAddress + templateName;
                    string downloadaddr = Application.StartupPath + @"/Template/";
                    if (!System.IO.Directory.Exists(downloadaddr))
                        System.IO.Directory.CreateDirectory(downloadaddr);
                    Common.Download(serverPath, downloadaddr);

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("Code", codes);
                    dic.Add("BJGY", bjgy);
                    dic.Add("LotNo", lotNo);
                    dic.Add("SetDate", setDate);
                    ReportHelper.Print(filename, dic, true, 2, false, DbConstSettings.BaseDbString);

                    #region 更新状态和日志
                    var markList = bll.GetMarkList(lstMarkCode).OrderBy(t => t.Mark).ToList();
                    markList.ForEach(item =>
                    {
                        item.PrintStatus = "3";//已打印
                        item.ModifyBy = CurrentUser.UserCode;
                        item.ModifyTime = DateTime.Now;
                    });
                    //更新打印状态
                    bll.UpdatePrintStatus(markList);

                    //记录打印日志
                    dynamic logEntity = new ExpandoObject();
                    logEntity.Id = Guid.NewGuid().ToString();
                    logEntity.FactoryCode = CurrentUser.FactoryCode;
                    logEntity.FactoryName = CurrentUser.FactoryName;
                    logEntity.BusinessType = "2"; //1：唛头
                    logEntity.Code = codes;
                    logEntity.Creator = CurrentUser.UserCode;
                    logEntity.Operator = CurrentUser.UserName;
                    _printLogBll.InsertPrintLog(logEntity);
                    #endregion

                    //数据加载
                    DataRow dr = ((DataRowView)dgvm.CurrentRow.DataBoundItem).Row;
                    BindDetail2(dr["WorkOrder"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                    var workOrder = this.dgvm.Rows[e.RowIndex].Cells["WorkOrder"].Value.ToString();
                    BindDetail(workOrder);//包装报工信息
                    BindDetail2(workOrder);//唛头信息
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 全选/取消全选
        /// </summary>
        /// <param name="state"></param>
        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            //这一句很重要结束编辑状态
            dgvd2.EndEdit();
            dgvd2.Rows.OfType<DataGridViewRow>().ToList().ForEach(t => t.Cells[0].Value = state);
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            #region 工厂
            var lstFactory = _baseBll.GetFactorySelect(CurrentUser.UserCode);
            lstFactory.Insert(0, new Select() { ItemCode = "", ItemName = Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbFactory.DataSource = lstFactory;
            this.cmbFactory.ValueMember = "ItemCode";
            this.cmbFactory.DisplayMember = "ItemName";
            if (lstFactory.Count > 1)
                this.cmbFactory.SelectedIndex = 1;
            else
                this.cmbFactory.SelectedIndex = 0;
            #endregion

            #region 包装状态
            var lstPackingStatus = _baseBll.GetDictionarySelect("PackingStatus");
            lstPackingStatus.Insert(0, new Select() { ItemCode = "", ItemName = Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbPackingStatus.DataSource = lstPackingStatus;
            this.cmbPackingStatus.ValueMember = "ItemCode";
            this.cmbPackingStatus.DisplayMember = "ItemName";
            this.cmbPackingStatus.SelectedValue = "2";//默认包装中
            #endregion

            #region 唛头打印状态
            var lstMarkPrintStatus = _baseBll.GetDictionarySelect("ShippingMarkPrinting");
            lstMarkPrintStatus.Insert(0, new Select() { ItemCode = "", ItemName = Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbPrintStatus.DataSource = lstMarkPrintStatus;
            this.cmbPrintStatus.ValueMember = "ItemCode";
            this.cmbPrintStatus.DisplayMember = "ItemName";
            this.cmbPrintStatus.SelectedValue = "1";
            #endregion

            //控件初始化
            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
            colCB.HeaderCell = cbHeader;
            colCB.HeaderText = Language.GetText("Common.SelectAll", "全选");
            colCB.MinimumWidth = 75;
            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dgvd2.Columns.Insert(0, colCB);

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
                MessageBox.Show(Language.GetText("Frm_PackingBG.MessageTips_1", "请选择工厂！"));
                return;
            }

            #region 参数赋值
            PackingBGDto query = new PackingBGDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.ProductOrder = txtProductOrder.Text.Trim();
            query.CustomerPO = txtCustomerPO.Text.Trim();
            query.PackingStatus = cmbPackingStatus.SelectedValue.ToString();
            query.MaterialCode = txtMaterialCode.Text.Trim();
            query.MaterialName = txtMaterialName.Text.Trim();
            query.Spec = txtSpec.Text.Trim();
            query.ContainerNO = txtContainerNO.Text.Trim();
            query.PaperBox = txtPaperBoxModel.Text.Trim();
            //query.StartTime = dtStart.Value;
            //query.EndTime = dtEnd.Value;
            query.MachineCode = txtMachine.Text.Trim();
            query.PrintStatus = cmbPrintStatus.SelectedValue.ToString();
            query.CardCode = txtCardCode.Text.Trim();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "ProductOrder DESC,ContainerNO";
            int record = 0;
            //var data = bll.GetListWithPage(query, out record);
            var data = bll.GetDataTableWithPage(query, out record);
            pagerControl1.Record = record;

            dgvm.AutoGenerateColumns = false;
            dgvm.DataSource = data;
            dgvm.ClearSelection();
            dgvd1.DataSource = null;
            dgvd2.DataSource = null;
        }
        /// <summary>
        /// 包装报工记录
        /// </summary>
        private void BindDetail(string workOrder)
        {
            //var detail = bll.GetPackingBGList(workOrder);
            var detail = bll.GetPackingBGDataTable(workOrder);
            dgvd1.AutoGenerateColumns = false;
            dgvd1.DataSource = detail;
        }
        /// <summary>
        /// 唛头信息
        /// </summary>
        /// <param name="workOrder"></param>
        private void BindDetail2(string workOrder)
        {
            //var detail = bll.GetPrintMarkList(workOrder);
            var detail = bll.GetPrintMarkDataTable(workOrder);
            dgvd2.AutoGenerateColumns = false;
            dgvd2.DataSource = detail;
        }

        private void txtProductOrder_GotFocus(object sender, EventArgs e)
        {
            txtProductOrder.Text = "";
        }
        private void txtContainerNO_GotFocus(object sender, EventArgs e)
        {
            txtContainerNO.Text = "";
        }

        #endregion
    }
}
