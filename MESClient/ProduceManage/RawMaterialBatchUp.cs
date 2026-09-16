using ALP.Application.Busines.ProduceManage;
using ALP.Application.Entity.ProduceManage;
using Lib;
using Lib.Bll;
using Lib.Model;
using ReportLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.ProduceManage
{
    public partial class RawMaterialBatchUp : Form
    {

        BaseDataBll _baseDataBLL = new BaseDataBll(); //基础数
        PackingBGBll _packingBGBLL = new PackingBGBll();//包装报工
        BaseMaterialBll _baseMaterialBLL = new BaseMaterialBll();//物料主数据
        PM_MaterialBatchUpRecordBLL _rawMaterialBatchUpBLL = new PM_MaterialBatchUpRecordBLL();//半成品报工

        List<PM_TeamPerson_ItemsEntity> teamItemList = new List<PM_TeamPerson_ItemsEntity>();
        List<PostEntity> postList = new List<PostEntity>();
        string teamPersonName = "";

        public RawMaterialBatchUp()
        {
            InitializeComponent();
        }
        private void RawMaterialBatchUp_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized; //设置窗体最大化

                Init();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 公共方法
        private void Init()
        {
            txtMachine1.Focus(); //设置焦点
            ResetRightFrom();
            lblProcessCode.Text = "";
            GetLoginInfo();
        }
        private void GetLoginInfo()
        {
            var result = _baseDataBLL.GetLoginInfo(CurrentUser.UserCode);
            if (result.Success)
            {
                var data = result.resultData;

                txtMachine2.Text = data.MachineCode;
                txtTeam2.Text = data.PTeamCode;

                if (!string.IsNullOrEmpty(data.PTeamCode))
                    GetPTeamInfo();

                if (!string.IsNullOrEmpty(data.MachineCode))
                    GetMachineInfo();

            }
            else
            {
                MessageBox.Show(result.returnMsg);
                return;
            }
        }
        /// <summary>
        /// 右侧-文本信息重置
        /// </summary>
        private void ResetRightFrom()
        {
            lblOwnProductId.Text = "";
            lblWorkOrder.Text = "";
            lblTransferCode.Text = "";
            lblTransferName.Text = "";
            lblMaterialCode.Text = "";
            lblMaterialName.Text = "";
            lblSmallClass.Text = "";
            lblSmallClassName.Text = "";
            lblBatchNo.Text = "";
            lblBadItemCode.Text = "";

            txtQty2.Text = "";
            txtBadQty1.Text = "";
            txtInspector.Text = "";
            txtProductInfo2.Text = "";
            txtBadItemCode2.Text = "";
            txtBadQty2.Text = "";
            this.dgv3.Rows.Clear();
        }

        private void ResetPartRightForm()
        {
            lblBadItemCode.Text = "";
            txtBadItemCode2.Text = "";
            txtBadQty2.Text = "";
            this.dgv3.Rows.Clear();
        }

        /// <summary>
        /// 获取生产小组信息
        /// </summary>
        private void GetPTeamInfo()
        {
            string pTeamCode = txtTeam2.Text.Trim();
            var result = _rawMaterialBatchUpBLL.TransferCardBGPTeamScan(pTeamCode);
            if (result.Success)
            {
                var data = result.resultData;

                var arrUserName = data.Select(t => t.UserName);
                txtUsers2.Text = string.Join(",", arrUserName);
            }
            else
            {
                MessageBox.Show(result.returnMsg);
                return;
            }
        }
        /// <summary>
        /// 获取机台信息
        /// </summary>
        private void GetMachineInfo()
        {
            var machineCode = txtMachine2.Text.Trim();
            var result = _baseDataBLL.GetModelResourceByChild(machineCode);
            if (result.Success)
            {
                var data = result.resultData;

                lblProcessCode.Text = data?.ResourceCode;
            }
            else
            {
                lblProcessCode.Text = "";
                MessageUtil.ShowWarning(result.returnMsg);
                return;
            }
        }
        private void GetOwnProductBGList()
        {
            var cardCode = txtCardCode2.Text.Trim();
            var result = _rawMaterialBatchUpBLL.GetOwnProductBGList(cardCode);
            if (result.Success)
            {
                var data = result.resultData;
                data = data.Where(t => !string.IsNullOrEmpty(t.RollCode)).ToList();
                data.ForEach(item =>
                {
                    item.CreateTime = item.CreateTime.Value.AddHours(-8);
                });

                var taotalBGQty = data.Sum(t => t.BGQty);
                lblTotalBGQty.Text = Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_1", "合计报工数量") + "：" + taotalBGQty.ToString();

                this.dgv4.AutoGenerateColumns = false;
                this.dgv4.DataSource = data;
            }
            else
            {
                MessageUtil.ShowWarning(result.returnMsg);
                return;
            }
        }
        /// <summary>
        /// 打印卷码
        /// </summary>
        /// <param name="workOrder"></param>
        /// <param name="factoryCode"></param>
        private void PrintRollCode(string materialCode, string rollCode)
        {
            var materialEntity = _baseMaterialBLL.GetEntity(materialCode);
            if (materialEntity == null)
            {

                MessageUtil.ShowWarning(Lib.Common.Language.GetTextWithParams("RawMaterialBatchUp.MessageTips_2", $"物料【{materialCode}】不存在！", materialCode));
                return;
            }
            string templateName = "";
            if (materialEntity.SmallClass == "NMC")
                templateName = "耐磨层流转卡001.frx";
            else
                templateName = "白料流转卡001.frx";

            string filename = Application.StartupPath + @"/Template/" + templateName;
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("RollCode", rollCode);
            string zzhm = dtZZHM.Value.ToString("yyyy-MM-dd");
            dic.Add("ZZHM", zzhm);
            ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);//默认打印机
        }

        private string GetSerialNo(string seqCode)
        {
            string serialNo = "";

            var result = _baseDataBLL.GetSerialNO(seqCode);
            if (result.Success)
                serialNo = result.resultData;

            return serialNo;
        }
        #endregion

        #region 左侧窗体事件
        /// <summary>
        /// 机台回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMachine1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    MachineScan();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 机台扫描
        /// </summary>
        private void MachineScan()
        {
            string machineCode = txtMachine1.Text.Trim();
            var result = _rawMaterialBatchUpBLL.RawMBatchUpMachineScan(machineCode);
            if (result.Success)
            {
                var data = result.resultData;
                this.dgv1.AutoGenerateColumns = false;
                this.dgv1.DataSource = data;

                txtCodeBar.Focus();
            }
            else
            {
                MessageUtil.ShowWarning(result.returnMsg);
                return;
            }
        }
        /// <summary>
        /// 关键件回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCodeBar_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var codeBar = txtCodeBar.Text.Trim();
                    var result = _rawMaterialBatchUpBLL.RawMBatchUpCodeBarScan(codeBar);
                    if (result.Success)
                    {
                        var data = result.resultData;
                        txtMaterialCode1.Text = data.MaterialCode;
                        txtMaterialName1.Text = data.MaterialName;
                        txtBatchNo1.Text = data.BatchNo;

                        btnSave_Click(null, null);
                    }
                    else
                    {
                        MessageUtil.ShowWarning(result.returnMsg);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 取消绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancelBind_Click(object sender, EventArgs e)
        {
            try
            {
                var data = Common.FillModelsByDgv<PM_MaterialBatchUpRecordEntity>(dgv1, "Id");//Id
                var result = _rawMaterialBatchUpBLL.RawMBatchUpMachineRemove(CurrentUser.UserCode, CurrentUser.UserName, data);
                if (result.Success)
                {
                    MessageUtil.ShowTips(Lib.Common.Language.GetText("Common.Success", "操作成功"));
                    this.dgv1.DataSource = null;
                    return;
                }
                else
                {
                    MessageUtil.ShowTips(result.returnMsg);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 关键件绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtMachine1.Text.Trim()))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_3", "机台不能为空！"));
                    return;
                }
                if (string.IsNullOrEmpty(txtCodeBar.Text.Trim()))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_4", "关键件不能为空！"));
                    return;
                }
                var entity = new PM_MaterialBatchUpRecordEntity();
                entity.MachineCode = txtMachine1.Text.Trim();
                entity.CodeBar = txtCodeBar.Text.Trim();
                entity.BatchNo = txtBatchNo1.Text.Trim();
                entity.MaterialCode = txtMaterialCode1.Text.Trim();
                entity.MaterialName = txtMaterialName1.Text.Trim();
                var result = _rawMaterialBatchUpBLL.RawMBatchUpCodeBarSave(CurrentUser.UserCode, CurrentUser.UserName, entity);
                if (result.Success)
                {
                    //MessageUtil.ShowTips("保存成功！");
                    //添加一行
                    //var index = this.dgv2.Rows.Add();
                    //this.dgv2.Rows[index].Cells["Column1"].Value = entity.MaterialName;//物料名称
                    //this.dgv2.Rows[index].Cells["Column2"].Value = entity.MaterialCode;//物料编码
                    //this.dgv2.Rows[index].Cells["Column3"].Value = entity.BatchNo;//物料批次
                    //清空窗体数据
                    txtMaterialCode1.Text = "";
                    txtMaterialName1.Text = "";
                    txtCodeBar.Text = "";
                    txtBatchNo1.Text = "";

                    MachineScan();

                    return;
                }
                else
                {
                    MessageUtil.ShowWarning(result.returnMsg);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 右侧窗体事件
        /// <summary>
        /// 流转卡回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCardCode2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string cardCode = txtCardCode2.Text.Trim();
                    var result = _rawMaterialBatchUpBLL.OwnProductBGCardScan(cardCode);
                    if (result.Success)
                    {
                        var data = result.resultData;

                        lblHasBGQty.Text = data.HasBGQty.ToString();
                        //var hasBGQty = Convert.ToDecimal(lblHasBGQty.Text);
                        //if (hasBGQty > 0)
                        //{
                        //string message = $"当前流转卡已进行过报工，已报工数量{lblHasBGQty.Text},请确认是否再次进行报工";
                        //DialogResult dialogResult = MessageBox.Show(message, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (dialogResult == DialogResult.Yes)
                        //{
                        //        lblOwnProductId.Text = data.OwnProductId;
                        //        lblWorkOrder.Text = data.WorkOrder;
                        //        lblTransferCode.Text = data.TransferCode;
                        //        lblTransferName.Text = data.TransferName;
                        //        lblMaterialCode.Text = data.MaterialCode;
                        //        lblMaterialName.Text = data.MaterialName;
                        //        lblSmallClass.Text = data.SmallClass;
                        //        lblSmallClassName.Text = data.SmallClassName;
                        //        lblBatchNo.Text = data.BatchNo;
                        //        txtProductInfo2.Text = "物料编码：" + lblMaterialCode.Text + Environment.NewLine;
                        //        txtProductInfo2.Text += "流转卡号：" + lblTransferName.Text + " " + lblSmallClassName.Text + Environment.NewLine;
                        //        txtProductInfo2.Text += "已报工数量：" + lblHasBGQty.Text;
                        //    }
                        //    else
                        //    {
                        //        txtCardCode2.Text = "";
                        //    }
                        //}
                        //else
                        //{
                        lblOwnProductId.Text = data.OwnProductId;
                        lblFactoryCode.Text = data.FactoryCode;
                        lblWorkOrder.Text = data.WorkOrder;
                        lblTransferCode.Text = data.TransferCode;
                        lblTransferName.Text = data.TransferName;
                        lblMaterialCode.Text = data.MaterialCode;
                        lblMaterialName.Text = data.MaterialName;
                        lblSmallClass.Text = data.SmallClass;
                        lblSmallClassName.Text = data.SmallClassName;
                        lblBatchNo.Text = data.BatchNo;
                        txtProductInfo2.Text = Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_5", "物料编码") + "：" + lblMaterialCode.Text + Environment.NewLine;
                        txtProductInfo2.Text += Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_6", "流转卡号") + "：" + lblTransferName.Text + " " + lblSmallClassName.Text + Environment.NewLine;
                        txtProductInfo2.Text += Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_7", "已报工数量") + "：" + lblHasBGQty.Text;
                        txtQty2.Focus(); //设置焦点
                        //}
                        //加载报工记录
                        GetOwnProductBGList();
                        //获取卷号
                        //var serialNo = GetSerialNo("RollCode");
                        //var rollCount = this.dgv4.Rows.Count;
                        //txtRollCode.Text = (rollCount + 1).ToString().PadLeft(3, '0');
                    }
                    else
                    {
                        MessageUtil.ShowWarning(result.returnMsg);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 机台回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMachine2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    GetMachineInfo();
                    txtTeam2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 生产小组回车事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTeam2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    GetPTeamInfo();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 选择不良项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectBadItem_Click(object sender, EventArgs e)
        {
            try
            {
                var processCode = lblProcessCode.Text.Trim();
                if (string.IsNullOrEmpty(processCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_8", "工序不能为空，请重新扫描机台！"));
                    return;
                }

                Frm_SelectBadItem formSelect = new Frm_SelectBadItem();
                formSelect.processCode = processCode;
                formSelect.ShowDialog();
                if (formSelect.DialogResult == DialogResult.OK)
                {
                    lblBadItemCode.Text = formSelect.badItemCode;
                    txtBadItemCode2.Text = formSelect.badItemName;
                    txtBadQty2.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 不良添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd2_Click(object sender, EventArgs e)
        {
            try
            {
                var badItemCode = lblBadItemCode.Text;
                var badItemName = txtBadItemCode2.Text.Trim();
                var badQty = txtBadQty2.Text.Trim();

                if (string.IsNullOrEmpty(badItemName))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_9", "请选择不良项目"));
                    return;
                }
                if (string.IsNullOrEmpty(txtBadQty2.Text.Trim()))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_10", "请填写不良数量"));
                    return;
                }
                var flag = Regex.IsMatch(badQty, "^[0-9]*$");//整数
                if (!flag)
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_11", "不良数量格式不对"));
                    return;
                }

                var badItemList = Common.FillModelsByDgv<PM_BGBadRecordEntity>(dgv3, "BadItemCode");
                if (badItemList.Any(t => t.BadItemCode == badItemCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_12", "已存在，不允许重复录入"));
                    return;
                }
                //添加一行
                var rowIndex = this.dgv3.Rows.Add();
                this.dgv3.Rows[rowIndex].Cells["BadItemCode"].Value = badItemCode;
                this.dgv3.Rows[rowIndex].Cells["BadItemName"].Value = badItemName;
                this.dgv3.Rows[rowIndex].Cells["BadQty"].Value = badQty;
                //计算总不良
                var totalBadQty = badItemList.Sum(t => t.BadQty);
                var currentBadQty = Convert.ToInt32(badQty);
                txtBadQty1.Text = (totalBadQty + currentBadQty).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 不良删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btbDelete2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv3.SelectedRows.Count > 0)
                {
                    this.dgv3.Rows.Remove(this.dgv3.SelectedRows[0]);

                    var badItemList = Common.FillModelsByDgv<PM_BGBadRecordEntity>(dgv3, "BadItemCode");
                    var totalBadQty = badItemList.Sum(t => t.BadQty);
                    txtBadQty1.Text = totalBadQty.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 生产报工+打印单卷条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave2_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据校验
                var ownProductId = lblOwnProductId.Text;
                var workOrder = lblWorkOrder.Text;
                var factoryCode = lblFactoryCode.Text;
                var transferCode = lblTransferCode.Text;
                var transferName = lblTransferName.Text;
                var materialCode = lblMaterialCode.Text;
                var materialName = lblMaterialName.Text;
                var smallClass = lblSmallClass.Text;
                var smallClassName = lblSmallClassName.Text;
                var batchNo = lblBatchNo.Text;
                var processCode = lblProcessCode.Text;
                var pTeamCode = txtTeam2.Text;
                var machiCode = txtMachine2.Text;
                var bgQty = txtQty2.Text;
                var meters = txtMeters.Text.Trim();
                var badQty = string.IsNullOrEmpty(txtBadQty1.Text) ? "0" : txtBadQty1.Text;
                var userNames = txtUsers2.Text;
                var inspector = txtInspector.Text;
                var rollCode = txtRollCode.Text.Trim();

                if (string.IsNullOrEmpty(transferCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_13", "流转卡不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(txtCardCode2.Text.Trim()))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_14", "流转卡信息不一致，请重新扫描"));
                    return;
                }
                if (string.IsNullOrEmpty(rollCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_15", "卷号不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(machiCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_16", "机台不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(pTeamCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_17", "生产小组不能为空"));
                    return;
                }
                #endregion

                #region 报工记录
                PM_OwnProductBGEntity entity = new PM_OwnProductBGEntity();
                entity.OwnProductId = ownProductId;
                entity.WorkOrder = workOrder;
                entity.TransferCode = transferCode;
                entity.TransferName = transferName;
                entity.MaterialCode = materialCode;
                entity.MaterialName = materialName;
                entity.BatchNo = batchNo;
                entity.BGMachine = machiCode;
                entity.BGProcess = processCode;
                entity.UserGroup = pTeamCode;
                entity.BGQty = Convert.ToInt32(bgQty);
                if (!string.IsNullOrEmpty(meters))
                    entity.Meters = Convert.ToInt32(meters);
                entity.BadQty = Convert.ToInt32(badQty);
                entity.Inspector = inspector;
                //entity.RollCode = entity.TransferCode + "-" + rollCode; 
                rollCode = rollCode.PadLeft(3, '0');
                entity.RollCode = entity.MaterialCode + DateTime.Now.ToString("yyMMdd") + entity.BGMachine + "-" + rollCode;//按照物料编码、日期、机台生成卷号

                #endregion

                var bgBadItemList = Common.FillModelsByDgv<PM_BGBadRecordEntity>(this.dgv3, "BadItemCode");

                var result = _rawMaterialBatchUpBLL.OwnProductBGSave(CurrentUser.UserCode, CurrentUser.UserName, userNames, badQty, entity, bgBadItemList);
                if (result.Success)
                {
                    //MessageUtil.ShowTips("保存成功！");

                    ResetPartRightForm();
                    GetOwnProductBGList();
                    //卷号自动加1
                    //var rollCount = this.dgv4.Rows.Count;
                    var rollCount = Convert.ToInt32(rollCode);
                    txtRollCode.Text = (rollCount + 1).ToString().PadLeft(3, '0');

                    //PrintRollCode(materialCode, result.resultData.ToString());
                    PrintRollCode(materialCode, entity.RollCode);

                }
                else
                {
                    MessageUtil.ShowWarning(result.returnMsg);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 生产报工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSCBG_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据校验
                var ownProductId = lblOwnProductId.Text;
                var workOrder = lblWorkOrder.Text;
                var factoryCode = lblFactoryCode.Text;
                var transferCode = lblTransferCode.Text;
                var transferName = lblTransferName.Text;
                var materialCode = lblMaterialCode.Text;
                var materialName = lblMaterialName.Text;
                var smallClass = lblSmallClass.Text;
                var smallClassName = lblSmallClassName.Text;
                var batchNo = lblBatchNo.Text;
                var processCode = lblProcessCode.Text;
                var pTeamCode = txtTeam2.Text;
                var machiCode = txtMachine2.Text;
                var bgQty = txtQty2.Text;
                var meters = txtMeters.Text.Trim();
                var badQty = string.IsNullOrEmpty(txtBadQty1.Text) ? "0" : txtBadQty1.Text;
                var userNames = txtUsers2.Text;
                var inspector = txtInspector.Text;
                var rollCode = txtRollCode.Text.Trim();

                if (string.IsNullOrEmpty(transferCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_13", "流转卡不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(txtCardCode2.Text.Trim()))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_14", "流转卡信息不一致，请重新扫描"));
                    return;
                }
                if (string.IsNullOrEmpty(rollCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_15", "卷号不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(machiCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_16", "机台不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(pTeamCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_17", "生产小组不能为空"));
                    return;
                }
                #endregion

                #region 报工记录
                PM_OwnProductBGEntity entity = new PM_OwnProductBGEntity();
                entity.OwnProductId = ownProductId;
                entity.WorkOrder = workOrder;
                entity.TransferCode = transferCode;
                entity.TransferName = transferName;
                entity.MaterialCode = materialCode;
                entity.MaterialName = materialName;
                entity.BatchNo = batchNo;
                entity.BGMachine = machiCode;
                entity.BGProcess = processCode;
                entity.UserGroup = pTeamCode;
                entity.BGQty = Convert.ToInt32(bgQty);
                if (!string.IsNullOrEmpty(meters))
                    entity.Meters = Convert.ToInt32(meters);
                entity.BadQty = Convert.ToInt32(badQty);
                entity.Inspector = inspector;
                //entity.RollCode = entity.TransferCode + "-" + rollCode;
                rollCode = rollCode.PadLeft(3, '0');
                entity.RollCode = entity.MaterialCode + DateTime.Now.ToString("yyMMdd") + entity.BGMachine + "-" + rollCode;//按照物料编码、日期、机台生成卷号
                #endregion

                var bgBadItemList = Common.FillModelsByDgv<PM_BGBadRecordEntity>(this.dgv3, "BadItemCode");

                var result = _rawMaterialBatchUpBLL.OwnProductBGSave(CurrentUser.UserCode, CurrentUser.UserName, userNames, badQty, entity, bgBadItemList);
                if (result.Success)
                {
                    //MessageUtil.ShowTips("保存成功！");

                    ResetPartRightForm();
                    GetOwnProductBGList();

                    //卷号自动加1
                    //var rollCount = this.dgv4.Rows.Count;
                    var rollCount = Convert.ToInt32(rollCode);
                    txtRollCode.Text = (rollCount + 1).ToString().PadLeft(3, '0');
                }
                else
                {
                    MessageUtil.ShowWarning(result.returnMsg);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 单卷条码编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv4.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_19", "请选择一条数据!"));
                    return;
                }
                var entity = this.dgv4.CurrentRow.DataBoundItem as PM_OwnProductBGEntity;
                Frm_OwnProductBGEdit frmEdit = new Frm_OwnProductBGEdit();
                frmEdit.entity = entity;
                frmEdit.ShowDialog();
                if (frmEdit.DialogResult == DialogResult.OK)
                {
                    GetOwnProductBGList();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 单卷条码打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint2_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv4.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_19", "请选择一条数据!"));
                    return;
                }
                var entity = this.dgv4.CurrentRow.DataBoundItem as PM_OwnProductBGEntity;
                PrintRollCode(entity.MaterialCode, entity.RollCode);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 唛头打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintMark_Click(object sender, EventArgs e)
        {
            try
            {
                var workOrder = lblWorkOrder.Text.Trim();
                var factoryCode = lblFactoryCode.Text.Trim();
                var cardCode = txtCardCode2.Text.Trim();

                if (string.IsNullOrEmpty(cardCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_13", "流转卡不能为空"));
                    return;
                }
                if (string.IsNullOrEmpty(workOrder) || string.IsNullOrEmpty(factoryCode))
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_18", "工单或者工厂不能为空，请重新扫描流转卡"));
                    return;
                }

                //打印功能
                var dtTemplate = _packingBGBLL.GetMarkTemplate(workOrder, factoryCode);
                if (dtTemplate.Rows.Count == 0)
                {
                    MessageUtil.ShowWarning(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_20", "未找到唛头模板"));
                    return;
                }
                var templateName = dtTemplate.Rows[0]["唛头名称"].ToString();
                string filename = Application.StartupPath + @"/Template/" + templateName;
                if (System.IO.File.Exists(filename))
                    System.IO.File.Delete(filename);

                string uploadAddress = Lib.Http.AddressUrl;
                string uploadname = uploadAddress + templateName;
                string downloadaddr = Application.StartupPath + @"/Template/";
                if (!System.IO.Directory.Exists(downloadaddr))
                    System.IO.Directory.CreateDirectory(downloadaddr);
                Common.Download(uploadname, downloadaddr);

                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("TransferCode", cardCode);
                ReportHelper.Print(filename, dic, true, DbConstSettings.BaseDbString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 生产小组人员绑定

        /// <summary>
        /// 生产小组人员扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTeam1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(txtTeam1.Text.Trim()))
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_17", "生产小组不能为空"));
                        return;
                    }

                    var result = _rawMaterialBatchUpBLL.PTeamCodeScan(txtTeam1.Text.Trim());
                    if (result.Success)
                    {
                        var data = result.resultData;
                        teamItemList = data.ItemList;

                        postList = data.PostList;
                        postList.Insert(0, new PostEntity() { PostCode = "", PostName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
                        this.cmbPost1.DataSource = postList;
                        this.cmbPost1.ValueMember = "PostCode";
                        this.cmbPost1.DisplayMember = "PostName";

                        this.dgv2.AutoGenerateColumns = false;
                        this.dgv2.DataSource = teamItemList;

                        txtPerson1.Focus();
                    }
                    else
                        MessageBox.Show(result.returnMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// 生产人员绑定小组-人员扫描
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPerson1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var scanUserCode = txtPerson1.Text.Trim();
                    if (string.IsNullOrEmpty(scanUserCode))
                    {
                        MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_22", "扫描的人员编码不能为空！"));
                        return;
                    }

                    var result = _baseDataBLL.GetUserList(CurrentUser.UserCode, scanUserCode, "1");
                    if (result.Success)
                    {
                        var data = result.resultData;
                        teamPersonName = data.FirstOrDefault()?.Name;
                    }
                    else
                        MessageBox.Show(result.returnMsg);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 生产小组绑定-保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeamAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var teamCode = txtTeam1.Text.Trim();
                var teamPersonCode = txtPerson1.Text.Trim();
                var postCode = cmbPost1.SelectedValue.ToString();
                var postName = cmbPost1.Text.Trim();

                if (string.IsNullOrEmpty(teamCode))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_17", "生产小组不能为空！"));
                    return;
                }
                if (string.IsNullOrEmpty(teamPersonCode))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_21", "小组人员不能为空！"));
                    return;
                }
                if (string.IsNullOrEmpty(postCode))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_23", "请选择岗位"));
                    return;
                }

                List<PM_TeamPerson_ItemsEntity> lstTeamItem = new List<PM_TeamPerson_ItemsEntity>();
                lstTeamItem.Add(new PM_TeamPerson_ItemsEntity()
                {
                    PTeamCode = teamCode,
                    PostCode = cmbPost1.SelectedValue.ToString(),
                    PostName = cmbPost1.Text.Trim(),
                    UserCode = txtPerson1.Text.Trim(),
                    UserName = teamPersonName
                });
                var result = _rawMaterialBatchUpBLL.PTeamSave(teamCode, CurrentUser.UserCode, CurrentUser.UserName, lstTeamItem);
                if (result.Success)
                {
                    txtPerson1.Text = "";
                    //teamPersonName = "";
                    //txtTeam1.Text = "";
                    cmbPost1.SelectedIndex = 0;

                    teamItemList.AddRange(lstTeamItem);
                    this.dgv2.AutoGenerateColumns = false;
                    this.dgv2.DataSource = new BindingList<PM_TeamPerson_ItemsEntity>(this.teamItemList);

                    txtPerson1.Focus();
                }
                else
                    MessageBox.Show(result.returnMsg);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 小组人员删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTeamDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgv2.SelectedRows.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("RawMaterialBatchUp.MessageTips_24", "请选择要删除的人员"));
                    return;
                }

                var entity = this.dgv2.CurrentRow.DataBoundItem as PM_TeamPerson_ItemsEntity;
                var pTeamCode = entity.PTeamCode;
                var teamUserCode = entity.UserCode;

                var result = _rawMaterialBatchUpBLL.PTeamPersonDelete(pTeamCode, teamUserCode);
                if (result.Success)
                {
                    var teamUserEntity = teamItemList.Find(t => t.PTeamCode == pTeamCode && t.UserCode == teamUserCode);
                    teamItemList.Remove(teamUserEntity);
                    this.dgv2.DataSource = new BindingList<PM_TeamPerson_ItemsEntity>(this.teamItemList);
                }
                else
                    MessageBox.Show(result.returnMsg);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

    }
}
