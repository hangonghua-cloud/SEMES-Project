
using Lib.Bll;
using Lib.Common;
using Lib.Model;
using Lib.Model.Dto;
using MESClient.Util;
using ReportLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.TransferCard
{
    public partial class Frm_TransferCard : Form
    {

        TransferCardBll bll = new TransferCardBll();
        BaseDataBll _baseBll = new BaseDataBll();//基础数据
        WorkOrderExcuteBll _exeWorkOrderBll = new WorkOrderExcuteBll();//执行工单
        PrintLogBll _printLogBll = new PrintLogBll();//打印日志

        public Frm_TransferCard()
        {
            InitializeComponent();
            Init();
        }

        private void Frm_TransferCard_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized;

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
        /// 新建流转卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvm.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_1", "请选择执行工单!"));
                    return;
                }
                List<string> lstCardCode = new List<string>();
                for (int i = 0; i < dgvd.Rows.Count; i++)
                {
                    string cardCode = dgvd.Rows[i].Cells["CardCode"].Value.ToString();

                    if ((bool)dgvd.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        if (dgvd.Rows[i].Cells["PrintStatus2"].Value.ToString() == "1")
                        {
                            MessageBox.Show(Lib.Common.Language.GetTextWithParams("Frm_TransferCard.MessageTips_2", $"流转卡【{cardCode}】不可打印！", cardCode));
                            return;
                        }
                        lstCardCode.Add(cardCode);
                    }
                }
                if (lstCardCode.Count != 1)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_3", "请选择一张流转卡!"));
                    return;
                }
                DataRow dr = ((DataRowView)dgvm.CurrentRow.DataBoundItem).Row;
                Frm_TransferCardAdd addForm = new Frm_TransferCardAdd();
                addForm.main.FactoryCode = dr["FactoryCode"];
                addForm.main.FactoryName = dr["FactoryName"];
                addForm.main.ExeWorkOrder = dr["ExeWorkOrder"];
                addForm.main.MMCJ = dr["MMCJ"];
                addForm.main.SelectedCardCode = lstCardCode.First();

                Lib.Resources.FormLanguage.SetFormLanguage(addForm);
                Util.FormLayout.SetFormLayout(addForm);
                addForm.ShowDialog();
                if (addForm.DialogResult == DialogResult.OK)
                {
                    BindDetail(dr["ExeWorkOrder"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 流转卡拆分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void btnSplit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        #region 数据验证
        //        if (dgvm.SelectedRows.Count == 0)
        //        {
        //            MessageBox.Show("请选择执行工单!");
        //            return;
        //        }
        //        //选择行数
        //        int selected = 0;
        //        dynamic mainCard = new ExpandoObject();
        //        for (int i = 0; i < dgvd.Rows.Count; i++)
        //        {
        //            if ((bool)dgvd.Rows[i].Cells[0].EditedFormattedValue == true)
        //            {
        //                mainCard = (dynamic)dgvd.Rows[i].DataBoundItem;
        //                selected++;
        //            }
        //        }
        //        if (selected != 1)
        //        {
        //            MessageBox.Show("请选择一个流转卡!");
        //            return;
        //        }
        //        var result = bll.IsExistBGRecord(mainCard.CardCode);
        //        if (result)
        //        {
        //            MessageBox.Show("该流转卡已报工，无法拆分!");
        //            return;
        //        }
        //        #endregion

        //        dynamic main = (dynamic)dgvm.SelectedRows[0].DataBoundItem;
        //        Frm_TransferCardSplit splitForm = new Frm_TransferCardSplit();
        //        splitForm.main = main;
        //        splitForm.mainCard = mainCard;
        //Lib.Resources.FormLanguage.SetFormLanguage(splitForm);
        //        splitForm.ShowDialog();
        //        if (splitForm.DialogResult == DialogResult.OK)
        //        {
        //            BindDetail(main.ExeWorkOrder);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvm.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_1", "请选择执行工单!"));
                    return;
                }
                //选择行数
                List<string> lstCardCode = new List<string>();
                for (int i = 0; i < dgvd.Rows.Count; i++)
                {
                    string cardCode = dgvd.Rows[i].Cells["CardCode"].Value.ToString();

                    if ((bool)dgvd.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        if (dgvd.Rows[i].Cells["PrintStatus2"].Value.ToString() == "1")
                        {
                            MessageBox.Show(Lib.Common.Language.GetTextWithParams("Frm_TransferCard.MessageTips_2", $"流转卡【{cardCode}】不可打印！", cardCode));
                            return;
                        }
                        lstCardCode.Add(cardCode);
                    }
                }
                if (lstCardCode.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_4", "请选择要打印的流转卡!"));

                    return;
                }

                string cardCodes = string.Join(",", lstCardCode);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("CardCode", cardCodes);
                //所在地
                string location = RegistryHelper.GetValue(Consts.Location);//获取工厂标记
                string filename = Application.StartupPath + $@"/Template/流转卡_{location}.frx";
                ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                var cardList = bll.GetCardEntityList(lstCardCode).OrderBy(t => t.CardName).ToList();
                foreach (var item in cardList)
                {
                    item.PrintStatus = "2";//已打印
                    item.ModifyBy = CurrentUser.UserCode;
                    item.ModifyTime = DateTime.Now;
                };
                //更新打印状态
                bll.UpdatePrintStatus(cardList);
                //记录打印日志
                dynamic logEntity = new ExpandoObject();
                logEntity.Id = Guid.NewGuid().ToString();
                logEntity.FactoryCode = CurrentUser.FactoryCode;
                logEntity.FactoryName = CurrentUser.FactoryName;
                logEntity.BusinessType = "1"; //1：流转卡
                logEntity.Code = cardCodes;
                logEntity.Creator = CurrentUser.UserCode;
                logEntity.Operator = CurrentUser.UserName;
                _printLogBll.InsertPrintLog(logEntity);
                //数据加载
                DataRow dr = ((DataRowView)dgvm.CurrentRow.DataBoundItem).Row;
                BindDetail(dr["ExeWorkOrder"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 执行工单列表选中行事件
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
                    //DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
                    //checkColumn.DataPropertyName = "check";
                    //checkColumn.HeaderText = "";
                    //dgvd.Columns.Add(checkColumn);

                    var exeWorkOrder = this.dgvm.Rows[e.RowIndex].Cells["ExeWorkOrder"].Value.ToString();
                    BindDetail(exeWorkOrder);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void dgvd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;

            string action = dgvd.Columns[e.ColumnIndex].Name;//操作类型
            switch (action)
            {
                case "RePrint":
                    var cardCode = dgvd.Rows[e.RowIndex].Cells["CardCode"].Value.ToString();

                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("CardCode", cardCode);
                    //所在地
                    string location = RegistryHelper.GetValue(Consts.Location);//获取工厂标记
                    string filename = Application.StartupPath + $@"/Template/流转卡_{location}.frx";
                    ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                    //记录打印日志
                    dynamic logEntity = new ExpandoObject();
                    logEntity.Id = Guid.NewGuid().ToString();
                    logEntity.FactoryCode = CurrentUser.FactoryCode;
                    logEntity.FactoryName = CurrentUser.FactoryName;
                    logEntity.BusinessType = "1"; //1：流转卡
                    logEntity.Code = cardCode;
                    logEntity.Creator = CurrentUser.UserCode;
                    logEntity.Operator = CurrentUser.UserName;
                    _printLogBll.InsertPrintLog(logEntity);
                    break;
                case "Delete":
                    if (MessageBox.Show(Lib.Common.Language.GetText("Common.IsDelete", "确定删除吗?"), Lib.Common.Language.GetText("Common.DeleteTip", "删除提示"), MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        //获取相应列的数据ID,删除此数据记录     

                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 全选/取消全选
        /// </summary>
        /// <param name="state"></param>
        private void cbHeader_OnCheckBoxClicked(bool state)
        {
            //这一句很重要结束编辑状态
            dgvd.EndEdit();
            dgvd.Rows.OfType<DataGridViewRow>().ToList().ForEach(t =>
            {
                if (t.Cells["PrintStatus2"].Value.ToString() == "3" || t.Cells["PrintStatus2"].Value.ToString() == "2")
                    t.Cells[0].Value = state;
            });
        }
        private void dgvm_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        private void txtCodeBar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtCodeBar.Text.Trim()))
                        return;

                    var cardCode = txtCodeBar.Text.Trim();
                    var cardEntity = bll.GetCardEntity(cardCode);
                    if (cardEntity == null)
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_5", "流转卡不存在！"));
                        return;
                    }
                    var cardList = bll.GetCardListBySerialNumber(cardEntity.SerialNumber).Where(t => t.PrintStatus == "3").ToList();
                    if (cardList.Count == 0)
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_6", "没有可打印的流转卡！"));
                        return;
                    }
                    var lstCardCode = cardList.Select(t => t.CardCode).Distinct();
                    string cardCodes = string.Join(",", lstCardCode);
                    Dictionary<string, object> dic = new Dictionary<string, object>();
                    dic.Add("CardCode", cardCodes);
                    string filename = Application.StartupPath + @"/Template/流转卡.frx";
                    ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                    foreach (var item in cardList)
                    {
                        item.PrintStatus = "2";//已打印
                        item.ModifyBy = CurrentUser.UserCode;
                        item.ModifyTime = DateTime.Now;
                    };
                    //更新打印状态
                    bll.UpdatePrintStatus(cardList);
                    //记录打印日志
                    dynamic logEntity = new ExpandoObject();
                    logEntity.Id = Guid.NewGuid().ToString();
                    logEntity.FactoryCode = CurrentUser.FactoryCode;
                    logEntity.FactoryName = CurrentUser.FactoryName;
                    logEntity.BusinessType = "1"; //1：流转卡
                    logEntity.Code = cardCodes;
                    logEntity.Creator = CurrentUser.UserCode;
                    logEntity.Operator = CurrentUser.UserName;
                    _printLogBll.InsertPrintLog(logEntity);

                    txtCodeBar.Text = "";
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 公共方法
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            #region 工厂
            var lstFactory = _baseBll.GetFactorySelect(CurrentUser.UserCode);
            lstFactory.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbFactory.DataSource = lstFactory;
            this.cmbFactory.ValueMember = "ItemCode";
            this.cmbFactory.DisplayMember = "ItemName";
            this.cmbFactory.SelectedIndex = 1;
            if (lstFactory.Count > 1)
                this.cmbFactory.SelectedIndex = 1;
            else
                this.cmbFactory.SelectedIndex = 0;
            #endregion

            #region 执行工单类型
            var lstExeWorkOrderType = _baseBll.GetDictionarySelect("ExecuteWorkOrderType");
            lstExeWorkOrderType.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbExeWorkOrderType.DataSource = lstExeWorkOrderType;
            this.cmbExeWorkOrderType.ValueMember = "ItemCode";
            this.cmbExeWorkOrderType.DisplayMember = "ItemName";
            #endregion

            #region 执行工单状态
            var lstExeWorkOrderStatus = _baseBll.GetDictionarySelect("ExeWorkOrderStatus");
            lstExeWorkOrderStatus.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
            this.cmbExeWorkOrderStatus.DataSource = lstExeWorkOrderStatus;
            this.cmbExeWorkOrderStatus.ValueMember = "ItemCode";
            this.cmbExeWorkOrderStatus.DisplayMember = "ItemName";
            #endregion

            //控件初始化
            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
            colCB.HeaderCell = cbHeader;
            colCB.HeaderText = Lib.Common.Language.GetText("Common.SelectAll", "全选");
            colCB.MinimumWidth = 75;
            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dgvd.Columns.Insert(0, colCB);
            //dgvd.Columns["RePrint"].Visible = false;

            pagerControl1.BindSource += new PagerControl.BindHandle(Bind);//绑定事件
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        private void Bind()
        {
            if (string.IsNullOrEmpty(cmbFactory.SelectedValue.ToString()))
            {
                MessageBox.Show(Lib.Common.Language.GetText("Frm_TransferCard.MessageTips_7", "请选择工厂!"));
                return;
            }

            #region 参数赋值
            TransferCardDto query = new TransferCardDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.ProductOrder = txtProductOrder.Text.Trim();
            query.WorkOrder = txtWorkOrder.Text.Trim();
            query.CustomerPO = txtCustomerPO.Text.Trim();
            query.ExecuteWorkOrderType = cmbExeWorkOrderType.SelectedValue.ToString();
            query.ExeWorkOrderStatus = cmbExeWorkOrderStatus.SelectedValue.ToString();
            query.MaterialName = txtMaterialName.Text.Trim();
            query.Spec = txtSpec.Text.Trim();
            query.ContainerNO = txtContainerNO.Text.Trim();
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
            dgvd.DataSource = null;
        }
        /// <summary>
        /// 绑定明细列表
        /// </summary>
        private void BindDetail(string exeWorkOrder)
        {
            //var detail = bll.GetTransferCardList(exeWorkOrder);
            var detail = bll.GetTransferCardDataTable(exeWorkOrder);
            dgvd.AutoGenerateColumns = false;
            dgvd.DataSource = detail;

            if (detail.Rows.Count > 0)
            {
                //DataRow[] drs = detail.Select("PrintStatus='2'");
                //if (drs.Length == 0)
                //{
                cbHeader_OnCheckBoxClicked(true);
                //}
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void Print(TransferCardEntity item)
        {
            var entity = Common.Clone(item);
            entity.ProductOrder = entity.ProductOrder.Substring(entity.ProductOrder.Length - 5);//订单取后面5位 
            //超产品数量
            WorkOrderExeDto query = new WorkOrderExeDto();
            query.WorkOrder = entity.WorkOrder;
            query.ExeWorkOrderType = "5";
            var exeWorkOrderList = _exeWorkOrderBll.GetList(query);
            if (exeWorkOrderList.Count > 0)
            {
                entity.SuperQty = "超 " + exeWorkOrderList.Sum(t => (int)t.PiecesQty).ToString();
            }
            else if (entity.CardType == "2")
                entity.SuperQty = "补";
            else if (entity.CardType == "3")
                entity.SuperQty = "拣";

            foreach (var m in entity.GetType().GetProperties())
            {
                var val = m.GetValue(entity, null);
                if (val == null)
                {
                    if (m.PropertyType == typeof(DateTime?)) m.SetValue(entity, DateTime.Now);
                    else if (m.PropertyType == typeof(decimal?)) m.SetValue(entity, 0m);
                    else if (m.PropertyType == typeof(string)) m.SetValue(entity, "");
                }
            }
            var postData = PubFunction.ToJson(entity);
            postData = postData.Substring(0, postData.Length - 1) + ",\"Image:Photo1\": {\"Type\": \"QRCode\",\"Value\": \"" + entity.CardCode + "\",\"Width\": \"200\",\"Height\": \"200\"}}";
            PubFunction.Post(postData, Template.TransferCard.ToString());

        }
        #endregion


    }
}
