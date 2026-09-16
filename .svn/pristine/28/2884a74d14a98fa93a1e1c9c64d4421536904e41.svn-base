
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
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.BaseData
{
    public partial class Frm_Machine : Form
    {
        BaseDataBll _baseDataBll = new BaseDataBll();

        public Frm_Machine()
        {
            InitializeComponent();
            Init();
        }

        private void Frm_Machine_Load(object sender, EventArgs e)
        {
            try
            {
                this.WindowState = FormWindowState.Maximized; //设置窗体最大化

                Bind();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证

                //选择行数
                List<string> lstMachineCode = new List<string>();
                for (int i = 0; i < dgvm.Rows.Count; i++)
                {
                    if ((bool)dgvm.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        lstMachineCode.Add(dgvm.Rows[i].Cells["MachineCode"].Value.ToString());
                    }
                }
                if (lstMachineCode.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_Machine.MessageTips_1", "请选择要打印的数据行！"));
                    return;
                }
                #endregion

                //var machineList = _baseDataBll.GetMachineEntityList(lstMachineCode).OrderBy(t => t.MachineCode).ToList();
                //foreach (var item in machineList)
                //{
                //    Print(item);
                //};

                string codes = string.Join(",", lstMachineCode);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", codes);
                string filename = Application.StartupPath + @"/Template/生产机台.frx";
                ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                Bind();
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
            try
            {
                string factoryCode = this.cmbFactory.SelectedValue.ToString();
                var lstOperation = _baseDataBll.GetProcessSelectByFactory(factoryCode);
                lstOperation.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
                this.cmbOperation.DataSource = lstOperation;
                this.cmbOperation.ValueMember = "ItemCode";
                this.cmbOperation.DisplayMember = "ItemName";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 工序change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbOperation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string operationCode = this.cmbOperation.SelectedValue.ToString();
                var lstMachine = _baseDataBll.GetListSelectByParentResource(operationCode);
                lstMachine.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
                this.cmbMachine.DataSource = lstMachine;
                this.cmbMachine.ValueMember = "ItemCode";
                this.cmbMachine.DisplayMember = "ItemName";
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
            dgvm.EndEdit();
            dgvm.Rows.OfType<DataGridViewRow>().ToList().ForEach(t => t.Cells[0].Value = state);
        }
        private void dgvm_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            e.Row.HeaderCell.Value = (e.Row.Index + 1).ToString();//添加行号
        }

        #region 公共方法
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

            //控件初始化
            DataGridViewCheckBoxColumn colCB = new DataGridViewCheckBoxColumn();
            DatagridViewCheckBoxHeaderCell cbHeader = new DatagridViewCheckBoxHeaderCell();
            colCB.HeaderCell = cbHeader;
            colCB.HeaderText = Lib.Common.Language.GetText("Common.SelectAll", "全选");
            colCB.MinimumWidth = 75;
            cbHeader.OnCheckBoxClicked += new CheckBoxClickedHandler(cbHeader_OnCheckBoxClicked);
            dgvm.Columns.Insert(0, colCB);

            pagerControl1.BindSource += new PagerControl.BindHandle(Bind);//绑定事件
        }
        private void Bind()
        {
            #region 参数赋值
            MachineDto query = new MachineDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.OperationCode = cmbOperation.SelectedValue.ToString();
            query.MachineCode = cmbMachine.SelectedValue.ToString();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "MachineCode";
            int record = 0;
            //var data = _baseDataBll.GetMachineListWithPage(query, out record);
            var data = _baseDataBll.GetMachineDataTabletWithPage(query, out record);
            pagerControl1.Record = record;

            dgvm.AutoGenerateColumns = false;
            dgvm.DataSource = data;
            dgvm.ClearSelection();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private void Print(MachineEntity item)
        {
            var entity = Common.Clone(item);

            foreach (var m in entity.GetType().GetProperties())
            {
                var val = m.GetValue(entity, null);
                if (val == null)
                {
                    if (m.PropertyType == typeof(DateTime?)) m.SetValue(entity, DateTime.Now);
                    else if (m.PropertyType == typeof(decimal?)) m.SetValue(entity, 0);
                    else if (m.PropertyType == typeof(string)) m.SetValue(entity, "");
                }
            }
            var postData = PubFunction.ToJson(entity);
            postData = postData.Substring(0, postData.Length - 1) + ",\"Image:Photo1\": {\"Type\": \"QRCode\",\"Value\": \"" + entity.MachineCode + "\",\"Width\": \"250\",\"Height\": \"250\"}}";
            PubFunction.Post(postData, Template.Machine.ToString());
            Thread.Sleep(500);//睡眠500毫秒
        }

        #endregion

        
    }
}
