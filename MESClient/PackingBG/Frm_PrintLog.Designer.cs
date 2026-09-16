
namespace MESClient.PackingBG
{
    partial class Frm_PrintLog
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
            this.txtCodes1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOperator = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtEnd = new System.Windows.Forms.DateTimePicker();
            this.dtStart = new System.Windows.Forms.DateTimePicker();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBusinessType = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dgvm = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Creator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Operator = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl1 = new MESClient.PagerControl();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtCodes1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 624);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1567, 102);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "编码记录";
            // 
            // txtCodes1
            // 
            this.txtCodes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCodes1.Location = new System.Drawing.Point(3, 21);
            this.txtCodes1.Multiline = true;
            this.txtCodes1.Name = "txtCodes1";
            this.txtCodes1.ReadOnly = true;
            this.txtCodes1.Size = new System.Drawing.Size(1561, 78);
            this.txtCodes1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Controls.Add(this.txtCode);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOperator);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dtEnd);
            this.groupBox1.Controls.Add(this.dtStart);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbBusinessType);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1567, 66);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(1011, 27);
            this.txtCode.Name = "txtCode";
            this.txtCode.Size = new System.Drawing.Size(236, 25);
            this.txtCode.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1263, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 14;
            this.label4.Text = "操作人";
            // 
            // txtOperator
            // 
            this.txtOperator.Location = new System.Drawing.Point(1321, 27);
            this.txtOperator.Name = "txtOperator";
            this.txtOperator.Size = new System.Drawing.Size(133, 25);
            this.txtOperator.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(968, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 14;
            this.label2.Text = "编码";
            // 
            // dtEnd
            // 
            this.dtEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtEnd.Location = new System.Drawing.Point(773, 27);
            this.dtEnd.Name = "dtEnd";
            this.dtEnd.Size = new System.Drawing.Size(175, 25);
            this.dtEnd.TabIndex = 12;
            // 
            // dtStart
            // 
            this.dtStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dtStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtStart.Location = new System.Drawing.Point(558, 27);
            this.dtStart.Name = "dtStart";
            this.dtStart.Size = new System.Drawing.Size(177, 25);
            this.dtStart.TabIndex = 13;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(741, 32);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(22, 15);
            this.label11.TabIndex = 10;
            this.label11.Text = "至";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(485, 32);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(67, 15);
            this.label10.TabIndex = 11;
            this.label10.Text = "打印时间";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSearch.Location = new System.Drawing.Point(1481, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 40);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(237, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "日志类型";
            // 
            // cmbBusinessType
            // 
            this.cmbBusinessType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBusinessType.FormattingEnabled = true;
            this.cmbBusinessType.Location = new System.Drawing.Point(310, 26);
            this.cmbBusinessType.Name = "cmbBusinessType";
            this.cmbBusinessType.Size = new System.Drawing.Size(159, 23);
            this.cmbBusinessType.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dgvm);
            this.groupBox3.Controls.Add(this.pagerControl1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(0, 66);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1567, 558);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "查询结果";
            // 
            // dgvm
            // 
            this.dgvm.AllowUserToAddRows = false;
            this.dgvm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.Code,
            this.Creator,
            this.CreateTime,
            this.Operator});
            this.dgvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvm.Location = new System.Drawing.Point(3, 21);
            this.dgvm.MultiSelect = false;
            this.dgvm.Name = "dgvm";
            this.dgvm.ReadOnly = true;
            this.dgvm.RowHeadersWidth = 51;
            this.dgvm.RowTemplate.Height = 27;
            this.dgvm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvm.Size = new System.Drawing.Size(1561, 491);
            this.dgvm.TabIndex = 2;
            this.dgvm.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvm_CellClick);
            this.dgvm.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvm_RowStateChanged);
            // 
            // Id
            // 
            this.Id.DataPropertyName = "Id";
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 52;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "编码明细";
            this.Code.MinimumWidth = 800;
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            this.Code.Width = 800;
            // 
            // Creator
            // 
            this.Creator.DataPropertyName = "Creator";
            this.Creator.HeaderText = "创建人编码";
            this.Creator.MinimumWidth = 100;
            this.Creator.Name = "Creator";
            this.Creator.ReadOnly = true;
            this.Creator.Width = 111;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "打印时间";
            this.CreateTime.MinimumWidth = 160;
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            this.CreateTime.Width = 160;
            // 
            // Operator
            // 
            this.Operator.DataPropertyName = "Operator";
            this.Operator.HeaderText = "操作人";
            this.Operator.MinimumWidth = 120;
            this.Operator.Name = "Operator";
            this.Operator.ReadOnly = true;
            this.Operator.Width = 120;
            // 
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 512);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1561, 43);
            this.pagerControl1.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Id";
            this.dataGridViewTextBoxColumn1.HeaderText = "Id";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 52;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Code";
            this.dataGridViewTextBoxColumn2.HeaderText = "编码明细";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 800;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 800;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Creator";
            this.dataGridViewTextBoxColumn3.HeaderText = "创建人编码";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 100;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 111;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "CreateTime";
            this.dataGridViewTextBoxColumn4.HeaderText = "打印时间";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 160;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 160;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Operator";
            this.dataGridViewTextBoxColumn5.HeaderText = "操作人";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 120;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 120;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 15);
            this.label5.TabIndex = 17;
            this.label5.Text = "工厂";
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(59, 26);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 16;
            // 
            // Frm_PrintLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1567, 726);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Frm_PrintLog";
            this.Text = "打印日志";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_PrintLog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbBusinessType;
        private System.Windows.Forms.GroupBox groupBox3;
        private PagerControl pagerControl1;
        private System.Windows.Forms.DataGridView dgvm;
        private System.Windows.Forms.TextBox txtOperator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtEnd;
        private System.Windows.Forms.DateTimePicker dtStart;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCodes1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Creator;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Operator;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFactory;
    }
}