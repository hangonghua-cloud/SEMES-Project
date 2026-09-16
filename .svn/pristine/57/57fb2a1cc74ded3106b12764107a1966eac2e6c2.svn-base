
using Lib.Bll;
using Lib.Model;
using Lib.Model.Dto;
using ReportLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MESClient.BaseData
{
    public partial class Frm_Location : Form
    {
        BaseDataBll _baseDataBll = new BaseDataBll();
        public Frm_Location()
        {
            InitializeComponent();
            Init();
        }
        /// <summary>
        /// 窗体加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_Location_Load(object sender, EventArgs e)
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
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证

                //选择行数
                List<string> lstLocationCode = new List<string>();
                for (int i = 0; i < dgvm.Rows.Count; i++)
                {
                    if ((bool)dgvm.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        lstLocationCode.Add(dgvm.Rows[i].Cells["LocationCode"].Value.ToString());
                    }
                }
                if (lstLocationCode.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_Location.MessageTips_1", "请选择要打印的数据行！"));
                    return;
                }
                #endregion

                string codes = string.Join(",", lstLocationCode);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", codes);
                string filename = Application.StartupPath + @"/Template/库位.frx";
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
                var lstWarehouse = _baseDataBll.GetWarehouseSelectByFactory(factoryCode);
                lstWarehouse.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
                this.cmbWarehouse.DataSource = lstWarehouse;
                this.cmbWarehouse.ValueMember = "ItemCode";
                this.cmbWarehouse.DisplayMember = "ItemName";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /// <summary>
        /// 仓库change事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string whsCode = this.cmbWarehouse.SelectedValue.ToString();
                var lstLocation = _baseDataBll.GetListSelectByParentResource(whsCode);
                lstLocation.Insert(0, new Select() { ItemCode = "", ItemName = Lib.Common.Language.GetText("Common.DropDownAll", "-请选择-") });
                this.cmbLocation.DataSource = lstLocation;
                this.cmbLocation.ValueMember = "ItemCode";
                this.cmbLocation.DisplayMember = "ItemName";
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
        /// <summary>
        /// 数据源绑定
        /// </summary>
        private void Bind()
        {
            if (string.IsNullOrEmpty(cmbFactory.SelectedValue.ToString()))
            {
                MessageBox.Show(Lib.Common.Language.GetText("Frm_Location.MessageTips_2", "请选择工厂！"));
                return;
            }

            #region 参数赋值
            LocationDto query = new LocationDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.Warehouse = cmbWarehouse.SelectedValue.ToString();
            query.Location = cmbLocation.SelectedValue.ToString();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "LocationCode";
            int record = 0;
            //var data = _baseDataBll.GetLocationListWithPage(query, out record);
            var data = _baseDataBll.GetLocationDataTableWithPage(query, out record);
            pagerControl1.Record = record;

            dgvm.AutoGenerateColumns = false;
            dgvm.DataSource = data;
            dgvm.ClearSelection();
        }

        #endregion


    }
}
