using Lib.Bll;
using Lib.Model;
using Lib.Model.Dto;
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

namespace MESClient.WorkOrderExcute
{
    public partial class Frm_WorkOrderExcuteJCBG : Form
    {
        public dynamic entity = new ExpandoObject();

        BaseDataBll _baseBll = new BaseDataBll();//基础数据
        WorkOrderExcuteBll _exeWorkOrderBll = new WorkOrderExcuteBll();

        public Frm_WorkOrderExcuteJCBG()
        {
            InitializeComponent();
        }
        #region 按钮事件
        private void Frm_WorkOrderExcute_JCBG_Load(object sender, EventArgs e)
        {
            try
            {
                Init();
                Assignment();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 添加到不良项目列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(this.cmbBadItem.SelectedValue.ToString()))
                {
                    MessageBox.Show("请选择不良项目");
                    return;
                }
                if (string.IsNullOrEmpty(txtBadQty.Text))
                {
                    MessageBox.Show("请填写不良数量");
                    return;
                }
                Regex rex = new Regex(@"^\d+$");//^开始，\d匹配一个数字字符，+出现至少一次，$结尾
                if (!rex.IsMatch(txtBadQty.Text))
                {
                    MessageBox.Show("不良数量格式不正确");
                    return;
                }

                int index = this.dgvBadItem.Rows.Add();
                this.dgvBadItem.Rows[index].Cells["BadItemCode"].Value = this.cmbBadItem.SelectedValue.ToString();
                this.dgvBadItem.Rows[index].Cells["BadItemName"].Value = this.cmbBadItem.Text;
                this.dgvBadItem.Rows[index].Cells["BadQty"].Value = this.txtBadQty.Text;

                ClearForm();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 删除不良项目行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.dgvBadItem.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选择要删除的行");
                    return;
                }

                DataGridViewRow dataGridViewRow = this.dgvBadItem.SelectedRows[0];
                this.dgvBadItem.Rows.Remove(dataGridViewRow);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 报工提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证
                if (string.IsNullOrEmpty(this.cmbCard.SelectedValue.ToString()))
                {
                    MessageBox.Show("请选择流转卡");
                    return;
                }
                if (string.IsNullOrEmpty(this.cmbPTeam.SelectedValue.ToString()))
                {
                    MessageBox.Show("请选择人员组别");
                    return;
                }
                if (string.IsNullOrEmpty(txtQty.Text))
                {
                    MessageBox.Show("请填写数量");
                    return;
                }
                Regex rex = new Regex(@"^\d+$");//^开始，\d匹配一个数字字符，+出现至少一次，$结尾
                if (!rex.IsMatch(txtQty.Text))
                {
                    MessageBox.Show("数量格式不正确");
                    return;
                }
                #endregion

                List<dynamic> lstBadItem = Common.GetModelsByDgv(dgvBadItem, "BadItemCode");

                //报工记录主表
                dynamic cardRecordEntity = new ExpandoObject();
                cardRecordEntity.Id = Guid.NewGuid().ToString();
                cardRecordEntity.CardCode = this.cmbCard.SelectedValue.ToString();
                cardRecordEntity.ProcessCode = entity.ProcessCode;
                cardRecordEntity.MachineCode = entity.EquipCode;
                cardRecordEntity.Qty = Convert.ToDecimal(txtQty.Text);
                cardRecordEntity.BadQty = lstBadItem.Sum(t => Convert.ToInt32(t.BadQty));
                cardRecordEntity.Creator = CurrentUser.UserCode;
                cardRecordEntity.BGUser = CurrentUser.UserName;

                //报工不良记录
                List<dynamic> lstCardBGBadRecord = new List<dynamic>();
                lstBadItem.ForEach(t =>
                {
                    dynamic badItemEntity = new ExpandoObject();
                    badItemEntity.BGID = cardRecordEntity.Id;
                    badItemEntity.BadItemCode = t.BadItemCode;
                    badItemEntity.BadItemName = t.BadItemName;
                    badItemEntity.BadQty = t.BadQty;
                    badItemEntity.Creator = CurrentUser.UserCode;
                    lstCardBGBadRecord.Add(badItemEntity);
                });
                //生产小组
                var processCode = entity.ProcessCode;
                var pTeamCode = this.cmbPTeam.SelectedValue.ToString();
                List<dynamic> lstCardBGPerson = _exeWorkOrderBll.GetPTeamPersonList(processCode, pTeamCode);
                lstCardBGPerson.ForEach(t =>
                {
                    t.BGID = cardRecordEntity.Id;
                    t.Creator = CurrentUser.UserCode;
                });
                //物料批次绑定记录
                var cardCode = cardRecordEntity.CardCode;
                var machineCode = entity.EquipCode;
                List<dynamic> lstCardBGMB = _exeWorkOrderBll.GetMaterialBatchBindRecordBy(cardCode, processCode, machineCode);
                lstCardBGMB.ForEach(t =>
                {
                    t.BGID = cardRecordEntity.Id;
                    t.Creator = CurrentUser.UserCode;
                });
                var result = _exeWorkOrderBll.SaveBGRecord(cardRecordEntity, lstCardBGBadRecord, lstCardBGPerson, lstCardBGMB);
                if (result > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    MessageBox.Show("报工成功");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("报工失败");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cmbPTeam_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var processCode = entity.ProcessCode;
                var pTeamCode = this.cmbPTeam.SelectedValue.ToString();
                List<dynamic> lstPTeamPerson = _exeWorkOrderBll.GetPTeamPersonList(processCode, pTeamCode);
                if (lstPTeamPerson.Count > 0)
                {
                    var userNames = string.Join(",", lstPTeamPerson.Select(t => t.UserName).ToArray());
                    this.txtUserInfo.Text = userNames;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region 公共方法
        public void Init()
        {
            var processCode = entity.ProcessCode;
            var exeWorkOrder = entity.ExeWorkOrder;
            //机台
            var lstMachine = _baseBll.GetMachineSelectByProcess(processCode);
            lstMachine.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbMachine.DataSource = lstMachine;
            this.cmbMachine.ValueMember = "ItemCode";
            this.cmbMachine.DisplayMember = "ItemName";

            //不良项目
            var lstBadItem = _baseBll.GetBadItemSelect(processCode);
            lstBadItem.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbBadItem.DataSource = lstBadItem;
            this.cmbBadItem.ValueMember = "ItemCode";
            this.cmbBadItem.DisplayMember = "ItemName";

            //生产小组
            var lstPTeam = _baseBll.GetPTeamSelect(processCode);
            lstPTeam.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbPTeam.DataSource = lstPTeam;
            this.cmbPTeam.ValueMember = "ItemCode";
            this.cmbPTeam.DisplayMember = "ItemName";

            //流转卡
            var lstCard = _baseBll.GetCardSelect(exeWorkOrder);
            lstCard.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbCard.DataSource = lstCard;
            this.cmbCard.ValueMember = "ItemCode";
            this.cmbCard.DisplayMember = "ItemName";
        }
        /// <summary>
        /// 清空表单
        /// </summary>
        public void ClearForm()
        {
            this.cmbBadItem.SelectedIndex = 0;
            this.txtBadQty.Text = "";
        }
        /// <summary>
        /// 赋值
        /// </summary>
        public void Assignment()
        {
            this.txtExeWorkOrder.Text = entity.ExeWorkOrder;
            this.txtContainerNO.Text = entity.ContainerNO;
            this.cmbMachine.SelectedValue = entity.EquipCode;
        }

        #endregion

    }
}
