
namespace MESClient.BaseData
{
    partial class Frm_Machine
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvm = new System.Windows.Forms.DataGridView();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.cmbOperation = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl1 = new MESClient.PagerControl();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MachineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OperationName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvm);
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1228, 658);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "列表";
            // 
            // dgvm
            // 
            this.dgvm.AllowUserToAddRows = false;
            this.dgvm.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MachineCode,
            this.MachineName,
            this.OperationCode,
            this.OperationName});
            this.dgvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvm.Location = new System.Drawing.Point(3, 21);
            this.dgvm.Name = "dgvm";
            this.dgvm.RowHeadersWidth = 51;
            this.dgvm.RowTemplate.Height = 27;
            this.dgvm.Size = new System.Drawing.Size(1222, 591);
            this.dgvm.TabIndex = 1;
            this.dgvm.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvm_RowStateChanged);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(847, 19);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(101, 41);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(729, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(96, 41);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbMachine);
            this.groupBox1.Controls.Add(this.cmbOperation);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1228, 66);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 7;
            this.label3.Text = "工厂";
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(69, 26);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 6;
            this.cmbFactory.SelectedIndexChanged += new System.EventHandler(this.cmbFactory_SelectedIndexChanged);
            // 
            // cmbOperation
            // 
            this.cmbOperation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbOperation.FormattingEnabled = true;
            this.cmbOperation.Location = new System.Drawing.Point(302, 26);
            this.cmbOperation.Name = "cmbOperation";
            this.cmbOperation.Size = new System.Drawing.Size(159, 23);
            this.cmbOperation.TabIndex = 6;
            this.cmbOperation.SelectedIndexChanged += new System.EventHandler(this.cmbOperation_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(259, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "工序";
            // 
            // cmbMachine
            // 
            this.cmbMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(543, 26);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(159, 23);
            this.cmbMachine.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(500, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "机台";
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
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 612);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1222, 43);
            this.pagerControl1.TabIndex = 0;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "PTeamName";
            this.dataGridViewTextBoxColumn5.HeaderText = "生产小组名称";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 125;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "PTeamCode";
            this.dataGridViewTextBoxColumn4.HeaderText = "生产小组编码";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 125;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "ProcessName";
            this.dataGridViewTextBoxColumn3.HeaderText = "工序名称";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 125;
            // 
            // MachineCode
            // 
            this.MachineCode.DataPropertyName = "MachineCode";
            this.MachineCode.HeaderText = "机台编码";
            this.MachineCode.MinimumWidth = 100;
            this.MachineCode.Name = "MachineCode";
            // 
            // MachineName
            // 
            this.MachineName.DataPropertyName = "MachineName";
            this.MachineName.HeaderText = "机台名称";
            this.MachineName.MinimumWidth = 100;
            this.MachineName.Name = "MachineName";
            // 
            // OperationCode
            // 
            this.OperationCode.DataPropertyName = "OperationCode";
            this.OperationCode.HeaderText = "工序编码";
            this.OperationCode.MinimumWidth = 100;
            this.OperationCode.Name = "OperationCode";
            // 
            // OperationName
            // 
            this.OperationName.DataPropertyName = "OperationName";
            this.OperationName.HeaderText = "工序名称";
            this.OperationName.MinimumWidth = 100;
            this.OperationName.Name = "OperationName";
            // 
            // Frm_Machine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1228, 724);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_Machine";
            this.Text = "机台打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_Machine_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private PagerControl pagerControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvm;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.ComboBox cmbOperation;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn MachineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn OperationName;
    }
}