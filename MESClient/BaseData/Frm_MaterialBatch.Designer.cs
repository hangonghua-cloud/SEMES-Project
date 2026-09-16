
namespace MESClient.BaseData
{
    partial class Frm_MaterialBatch
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMaterialName = new System.Windows.Forms.TextBox();
            this.txtMaterialCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvm = new System.Windows.Forms.DataGridView();
            this.pagerControl1 = new MESClient.PagerControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPalletCount = new System.Windows.Forms.TextBox();
            this.txtProductOrder = new System.Windows.Forms.TextBox();
            this.txtWhsName = new System.Windows.Forms.TextBox();
            this.btnPrintPallet = new System.Windows.Forms.Button();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BatchNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Unit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(1313, 19);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(102, 41);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(1193, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(102, 41);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(466, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "物料名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(226, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "物料编码";
            // 
            // txtMaterialName
            // 
            this.txtMaterialName.Location = new System.Drawing.Point(539, 29);
            this.txtMaterialName.Name = "txtMaterialName";
            this.txtMaterialName.Size = new System.Drawing.Size(154, 25);
            this.txtMaterialName.TabIndex = 3;
            // 
            // txtMaterialCode
            // 
            this.txtMaterialCode.Location = new System.Drawing.Point(299, 28);
            this.txtMaterialCode.Name = "txtMaterialCode";
            this.txtMaterialCode.Size = new System.Drawing.Size(152, 25);
            this.txtMaterialCode.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvm);
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1805, 665);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "小组列表";
            // 
            // dgvm
            // 
            this.dgvm.AllowUserToAddRows = false;
            this.dgvm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.MaterialCode,
            this.MaterialName,
            this.Spec,
            this.BatchNo,
            this.WhsCode,
            this.WhsName,
            this.LocationCode,
            this.LocationName,
            this.Qty,
            this.Unit});
            this.dgvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvm.Location = new System.Drawing.Point(3, 21);
            this.dgvm.Name = "dgvm";
            this.dgvm.RowHeadersWidth = 51;
            this.dgvm.RowTemplate.Height = 27;
            this.dgvm.Size = new System.Drawing.Size(1799, 598);
            this.dgvm.TabIndex = 1;
            this.dgvm.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvm_RowStateChanged);
            // 
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 619);
            this.pagerControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1799, 43);
            this.pagerControl1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtPalletCount);
            this.groupBox1.Controls.Add(this.txtProductOrder);
            this.groupBox1.Controls.Add(this.txtWhsName);
            this.groupBox1.Controls.Add(this.btnPrintPallet);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtMaterialName);
            this.groupBox1.Controls.Add(this.txtMaterialCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1805, 66);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 12;
            this.label5.Text = "工厂";
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(53, 28);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1457, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 15);
            this.label6.TabIndex = 10;
            this.label6.Text = "托数";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(961, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "订单号";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(716, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "仓库名称";
            // 
            // txtPalletCount
            // 
            this.txtPalletCount.Location = new System.Drawing.Point(1500, 28);
            this.txtPalletCount.Name = "txtPalletCount";
            this.txtPalletCount.Size = new System.Drawing.Size(72, 25);
            this.txtPalletCount.TabIndex = 9;
            // 
            // txtProductOrder
            // 
            this.txtProductOrder.Location = new System.Drawing.Point(1019, 28);
            this.txtProductOrder.Name = "txtProductOrder";
            this.txtProductOrder.Size = new System.Drawing.Size(154, 25);
            this.txtProductOrder.TabIndex = 9;
            // 
            // txtWhsName
            // 
            this.txtWhsName.Location = new System.Drawing.Point(789, 29);
            this.txtWhsName.Name = "txtWhsName";
            this.txtWhsName.Size = new System.Drawing.Size(154, 25);
            this.txtWhsName.TabIndex = 9;
            // 
            // btnPrintPallet
            // 
            this.btnPrintPallet.Location = new System.Drawing.Point(1589, 19);
            this.btnPrintPallet.Name = "btnPrintPallet";
            this.btnPrintPallet.Size = new System.Drawing.Size(102, 41);
            this.btnPrintPallet.TabIndex = 7;
            this.btnPrintPallet.Text = "打印多托";
            this.btnPrintPallet.UseVisualStyleBackColor = true;
            this.btnPrintPallet.Click += new System.EventHandler(this.btnPrintPallet_Click);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FactoryName";
            this.dataGridViewTextBoxColumn1.HeaderText = "工厂名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 125;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProcessCode";
            this.dataGridViewTextBoxColumn2.HeaderText = "工序编码";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ProcessName";
            this.dataGridViewTextBoxColumn3.HeaderText = "工序名称";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PTeamCode";
            this.dataGridViewTextBoxColumn4.HeaderText = "生产小组编码";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PTeamName";
            this.dataGridViewTextBoxColumn5.HeaderText = "生产小组名称";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "WhsName";
            this.dataGridViewTextBoxColumn6.HeaderText = "仓库名称";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 125;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "LocationCode";
            this.dataGridViewTextBoxColumn7.HeaderText = "库位编码";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.Width = 125;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "LocationName";
            this.dataGridViewTextBoxColumn8.HeaderText = "库位名称";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 125;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Qty";
            this.dataGridViewTextBoxColumn9.HeaderText = "数量";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 75;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 75;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Unit";
            this.dataGridViewTextBoxColumn10.HeaderText = "单位";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 75;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 75;
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.Visible = false;
            this.Id.Width = 52;
            // 
            // MaterialCode
            // 
            this.MaterialCode.DataPropertyName = "MaterialCode";
            this.MaterialCode.HeaderText = "物料编码";
            this.MaterialCode.MinimumWidth = 100;
            this.MaterialCode.Name = "MaterialCode";
            // 
            // MaterialName
            // 
            this.MaterialName.DataPropertyName = "MaterialName";
            this.MaterialName.HeaderText = "物料名称";
            this.MaterialName.MinimumWidth = 100;
            this.MaterialName.Name = "MaterialName";
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.HeaderText = "规格";
            this.Spec.MinimumWidth = 75;
            this.Spec.Name = "Spec";
            this.Spec.Width = 75;
            // 
            // BatchNo
            // 
            this.BatchNo.DataPropertyName = "BatchNo";
            this.BatchNo.HeaderText = "批次号";
            this.BatchNo.MinimumWidth = 80;
            this.BatchNo.Name = "BatchNo";
            this.BatchNo.Width = 81;
            // 
            // WhsCode
            // 
            this.WhsCode.DataPropertyName = "WhsCode";
            this.WhsCode.HeaderText = "仓库编码";
            this.WhsCode.MinimumWidth = 100;
            this.WhsCode.Name = "WhsCode";
            // 
            // WhsName
            // 
            this.WhsName.DataPropertyName = "WhsName";
            this.WhsName.HeaderText = "仓库名称";
            this.WhsName.MinimumWidth = 100;
            this.WhsName.Name = "WhsName";
            // 
            // LocationCode
            // 
            this.LocationCode.DataPropertyName = "LocationCode";
            this.LocationCode.HeaderText = "库位编码";
            this.LocationCode.MinimumWidth = 100;
            this.LocationCode.Name = "LocationCode";
            // 
            // LocationName
            // 
            this.LocationName.DataPropertyName = "LocationName";
            this.LocationName.HeaderText = "库位名称";
            this.LocationName.MinimumWidth = 100;
            this.LocationName.Name = "LocationName";
            // 
            // Qty
            // 
            this.Qty.DataPropertyName = "Qty";
            this.Qty.HeaderText = "数量";
            this.Qty.MinimumWidth = 75;
            this.Qty.Name = "Qty";
            this.Qty.Width = 75;
            // 
            // Unit
            // 
            this.Unit.DataPropertyName = "Unit";
            this.Unit.HeaderText = "单位";
            this.Unit.MinimumWidth = 75;
            this.Unit.Name = "Unit";
            this.Unit.Width = 75;
            // 
            // Frm_MaterialBatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1805, 731);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_MaterialBatch";
            this.Text = "物料批次";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_MaterialBatch_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private PagerControl pagerControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMaterialName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.TextBox txtMaterialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvm;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWhsName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProductOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPalletCount;
        private System.Windows.Forms.Button btnPrintPallet;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhsName;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn Unit;
    }
}