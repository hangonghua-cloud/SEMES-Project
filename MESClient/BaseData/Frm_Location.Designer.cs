
namespace MESClient.BaseData
{
    partial class Frm_Location
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvm = new System.Windows.Forms.DataGridView();
            this.LocationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WhsName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl1 = new MESClient.PagerControl();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.cmbWarehouse = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbLocation = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbLocation);
            this.groupBox1.Controls.Add(this.cmbWarehouse);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1232, 76);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(845, 29);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(101, 41);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(727, 29);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 41);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvm);
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 76);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1232, 659);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列表";
            // 
            // dgvm
            // 
            this.dgvm.AllowUserToAddRows = false;
            this.dgvm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationCode,
            this.MachineName,
            this.WhsCode,
            this.WhsName});
            this.dgvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvm.Location = new System.Drawing.Point(3, 21);
            this.dgvm.Name = "dgvm";
            this.dgvm.RowHeadersWidth = 51;
            this.dgvm.RowTemplate.Height = 27;
            this.dgvm.Size = new System.Drawing.Size(1226, 592);
            this.dgvm.TabIndex = 3;
            this.dgvm.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvm_RowStateChanged);
            // 
            // LocationCode
            // 
            this.LocationCode.DataPropertyName = "LocationCode";
            this.LocationCode.HeaderText = "库位编码";
            this.LocationCode.MinimumWidth = 100;
            this.LocationCode.Name = "LocationCode";
            // 
            // MachineName
            // 
            this.MachineName.DataPropertyName = "LocationName";
            this.MachineName.HeaderText = "库位名称";
            this.MachineName.MinimumWidth = 100;
            this.MachineName.Name = "MachineName";
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
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 613);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1226, 43);
            this.pagerControl1.TabIndex = 2;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "FactoryName";
            this.dataGridViewTextBoxColumn1.HeaderText = "工厂名称";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 10;
            this.label3.Text = "工厂";
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(64, 36);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 9;
            this.cmbFactory.SelectedIndexChanged += new System.EventHandler(this.cmbFactory_SelectedIndexChanged);
            // 
            // cmbWarehouse
            // 
            this.cmbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWarehouse.FormattingEnabled = true;
            this.cmbWarehouse.Location = new System.Drawing.Point(297, 36);
            this.cmbWarehouse.Name = "cmbWarehouse";
            this.cmbWarehouse.Size = new System.Drawing.Size(159, 23);
            this.cmbWarehouse.TabIndex = 9;
            this.cmbWarehouse.SelectedIndexChanged += new System.EventHandler(this.cmbWarehouse_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(254, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "仓库";
            // 
            // cmbLocation
            // 
            this.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLocation.FormattingEnabled = true;
            this.cmbLocation.Location = new System.Drawing.Point(532, 36);
            this.cmbLocation.Name = "cmbLocation";
            this.cmbLocation.Size = new System.Drawing.Size(159, 23);
            this.cmbLocation.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(489, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 10;
            this.label2.Text = "库位";
            // 
            // Frm_Location
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1232, 735);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Location";
            this.Text = "库位打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Location_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvm;
        private PagerControl pagerControl1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn LocationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn WhsName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLocation;
        private System.Windows.Forms.ComboBox cmbWarehouse;
    }
}