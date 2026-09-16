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
using System.Windows.Forms;

namespace MESClient.WorkOrderExcute
{
    public partial class Frm_WorkOrderExcute : Form
    {

        WorkOrderExcuteBll bll = new WorkOrderExcuteBll();
        BaseDataBll _baseBll = new BaseDataBll();//基础数据

        public Frm_WorkOrderExcute()
        {
            InitializeComponent();
            Init();
        }

        #region 按钮事件
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_WorkOrderExcute_Load(object sender, EventArgs e)
        {
            try
            {

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
                if (string.IsNullOrEmpty(this.cmbProcess.SelectedValue.ToString()))
                {
                    MessageBox.Show("请选择工序");
                    return;
                }
                if (string.IsNullOrEmpty(this.cmbMachine.SelectedValue.ToString()))
                {
                    MessageBox.Show("请选择机台");
                    return;
                }

                Bind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 原料批次上机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 挤出开工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选中要操作的行");
                    return;
                }
                var currentRow = (dynamic)dgv1.SelectedRows[0].DataBoundItem;
                if (currentRow.SWSatus != "1")
                {
                    MessageBox.Show("已开工，不能再次开工");
                    return;
                }

                if (MessageBox.Show("确认开工？", "开工确认", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    var result = bll.Start(currentRow.Id, currentRow.ExeWorkOrder, CurrentUser.UserCode);
                    if (result > 0)
                    {
                        Bind();
                        MessageBox.Show("操作成功");
                    }
                    else
                    {
                        MessageBox.Show("操作失败");
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 过程检验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选中要操作的行");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 挤出报工
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBG_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgv1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("请选中要操作的行");
                    return;
                }

                Frm_WorkOrderExcuteJCBG jcbg = new Frm_WorkOrderExcuteJCBG();
                jcbg.entity = (dynamic)dgv1.SelectedRows[0].DataBoundItem;
                jcbg.ShowDialog();
                if (jcbg.DialogResult == DialogResult.OK)
                {
                    this.Bind();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        /// <summary>
        /// 工厂change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] arrFilter = new string[] { "FHJC", "FHKC" };
            //工序下拉列表
            var lstProcess = _baseBll.GetProcessSelectByFactory(this.cmbFactory.SelectedValue.ToString());
            lstProcess = lstProcess.FindAll(t => arrFilter.Contains(t.ItemCode));
            lstProcess.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbProcess.DataSource = lstProcess;
            this.cmbProcess.ValueMember = "ItemCode";
            this.cmbProcess.DisplayMember = "ItemName";

        }
        /// <summary>
        /// 工序change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbProcess_SelectedIndexChanged(object sender, EventArgs e)
        {
            //机台下拉列表
            var lstMachine = _baseBll.GetMachineSelectByProcess(this.cmbProcess.SelectedValue.ToString());
            lstMachine.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbMachine.DataSource = lstMachine;
            this.cmbMachine.ValueMember = "ItemCode";
            this.cmbMachine.DisplayMember = "ItemName";

        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pagerControl1_BindSource(object sender, EventArgs e)
        {
            Bind();
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
            lstFactory.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbFactory.DataSource = lstFactory;
            this.cmbFactory.ValueMember = "ItemCode";
            this.cmbFactory.DisplayMember = "ItemName";
            this.cmbFactory.SelectedIndex = 1;
            #endregion

            #region 派工生产状态
            var lstSWStatus = _baseBll.GetDictionarySelect("SWStatus");
            lstSWStatus.Remove(lstSWStatus.Find(t => t.ItemCode == "3"));
            lstSWStatus.Insert(0, new Select() { ItemCode = "", ItemName = "-请选择-" });
            this.cmbSWStatus.DataSource = lstSWStatus;
            this.cmbSWStatus.ValueMember = "ItemCode";
            this.cmbSWStatus.DisplayMember = "ItemName";

            #endregion
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        private void Bind()
        {
            #region 参数赋值
            WorkOrderExeSWDto query = new WorkOrderExeSWDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.ProcessCode = cmbProcess.SelectedValue.ToString();
            query.MachineCode = cmbMachine.SelectedValue.ToString();
            query.SWStatus = cmbSWStatus.SelectedValue.ToString();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "SWSeq";
            int record = 0;
            var data = bll.GetListWithPage(query, out record);
            pagerControl1.Record = record;

            dgv1.AutoGenerateColumns = false;
            dgv1.DataSource = data;
        }

        #endregion


    }
}
