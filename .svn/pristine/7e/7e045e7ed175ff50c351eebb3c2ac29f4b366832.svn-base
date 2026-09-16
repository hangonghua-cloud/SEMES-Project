
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
    public partial class Frm_MaterialBatch : Form
    {
        BaseDataBll _baseDataBll = new BaseDataBll();
        public Frm_MaterialBatch()
        {
            InitializeComponent();
            Init();
        }

        private void Frm_MaterialBatch_Load(object sender, EventArgs e)
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
                List<string> lstId = new List<string>();
                for (int i = 0; i < dgvm.Rows.Count; i++)
                {
                    if ((bool)dgvm.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        lstId.Add(dgvm.Rows[i].Cells["Id"].Value.ToString());
                    }
                }
                if (lstId.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_1", "请选择要打印的数据行!"));
                    return;
                }
                #endregion

                //var batchList = _baseDataBll.GetMaterialBatchEntityList(lstId).OrderBy(t => t.MaterialCode).ToList();
                //foreach (var item in batchList)
                //{
                //    Print(item);
                //};

                string codes = string.Join(",", lstId);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", codes);
                string filename = Application.StartupPath + @"/Template/物料批次.frx";
                ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                Bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 打印多托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintPallet_Click(object sender, EventArgs e)
        {
            try
            {
                #region 数据验证

                //选择行数
                List<string> lstId = new List<string>();
                for (int i = 0; i < dgvm.Rows.Count; i++)
                {
                    if ((bool)dgvm.Rows[i].Cells[0].EditedFormattedValue == true)
                    {
                        lstId.Add(dgvm.Rows[i].Cells["Id"].Value.ToString());
                    }
                }
                if (lstId.Count == 0)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_1", "请选择要打印的数据行!"));
                    return;
                }

                if (lstId.Count !=1)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_3", "请选择一行数据!"));
                    return;
                }

                string strBatchCount = txtPalletCount.Text.Trim();
                if (string.IsNullOrEmpty(strBatchCount))
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_4", "请输入托数!"));
                    return;
                }
                int num = 0;
                var flag= int.TryParse(strBatchCount, out num);
                if (!flag)
                {
                    MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_5", "托数的格式不正确!"));
                    return;
                }
                
                #endregion


                string codes = string.Join(",", lstId);
                Dictionary<string, object> dic = new Dictionary<string, object>();
                dic.Add("Code", codes);
                dic.Add("BatchCount", strBatchCount);
                string filename = Application.StartupPath + @"/Template/物料批次Sub.frx";
                ReportHelper.Print(filename, dic, DbConstSettings.BaseDbString);

                Bind();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            if (string.IsNullOrEmpty(cmbFactory.SelectedValue.ToString()))
            {
                MessageBox.Show(Lib.Common.Language.GetText("Frm_MaterialBatch.MessageTips_2", "请选择工厂!"));
                return;
            }

            #region 参数赋值
            MaterialBatchDto query = new MaterialBatchDto();
            query.FactoryCode = cmbFactory.SelectedValue.ToString();
            query.MaterialCode = txtMaterialCode.Text.Trim();
            query.MaterialName = txtMaterialName.Text.Trim();
            query.WhsName = txtWhsName.Text.Trim();
            query.ProductOrder = txtProductOrder.Text.Trim();
            #endregion

            query.CurrentPage = pagerControl1.CurrentPage;
            query.PageSize = pagerControl1.PageSize;
            query.Sidx = "MaterialCode";
            int record = 0;
            //var data = _baseDataBll.GetMaterialBatchListWithPage(query, out record);
            var data = _baseDataBll.GetMaterialBatchDataTableWithPage(query, out record);
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
        private void Print(RawMaterialStockEntity item)
        {
            var entity = Common.Clone(item);
            entity.PrintTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
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
            postData = postData.Substring(0, postData.Length - 1) + ",\"Image:Photo1\": {\"Type\": \"QRCode\",\"Value\": \"" + entity.BatchNo + "\",\"Width\": \"250\",\"Height\": \"250\"}}";
            PubFunction.Post(postData, Template.MaterialBatch.ToString());
            Thread.Sleep(500);//睡眠500毫秒
        }
        #endregion

        
    }
}
