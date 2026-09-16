
namespace MESClient.BaseData
{
    partial class Frm_PTeam
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPTeamName = new System.Windows.Forms.TextBox();
            this.txtPTeamCode = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvm = new System.Windows.Forms.DataGridView();
            this.FactoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PTeamCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PTeamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl1 = new MESClient.PagerControl();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPTeamName);
            this.groupBox1.Controls.Add(this.txtPTeamCode);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1319, 66);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(935, 19);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(89, 41);
            this.btnPrint.TabIndex = 2;
            this.btnPrint.Text = "打印";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(818, 19);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(92, 41);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(530, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "小组名称";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(244, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "小组编码";
            // 
            // txtPTeamName
            // 
            this.txtPTeamName.Location = new System.Drawing.Point(603, 26);
            this.txtPTeamName.Name = "txtPTeamName";
            this.txtPTeamName.Size = new System.Drawing.Size(194, 25);
            this.txtPTeamName.TabIndex = 0;
            // 
            // txtPTeamCode
            // 
            this.txtPTeamCode.Location = new System.Drawing.Point(317, 25);
            this.txtPTeamCode.Name = "txtPTeamCode";
            this.txtPTeamCode.Size = new System.Drawing.Size(194, 25);
            this.txtPTeamCode.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvm);
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1319, 679);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "小组列表";
            // 
            // dgvm
            // 
            this.dgvm.AllowUserToAddRows = false;
            this.dgvm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvm.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactoryName,
            this.ProcessCode,
            this.ProcessName,
            this.PTeamCode,
            this.PTeamName});
            this.dgvm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvm.Location = new System.Drawing.Point(3, 21);
            this.dgvm.Name = "dgvm";
            this.dgvm.RowHeadersWidth = 51;
            this.dgvm.RowTemplate.Height = 27;
            this.dgvm.Size = new System.Drawing.Size(1313, 612);
            this.dgvm.TabIndex = 1;
            this.dgvm.RowStateChanged += new System.Windows.Forms.DataGridViewRowStateChangedEventHandler(this.dgvm_RowStateChanged);
            // 
            // FactoryName
            // 
            this.FactoryName.DataPropertyName = "FactoryName";
            this.FactoryName.HeaderText = "工厂名称";
            this.FactoryName.MinimumWidth = 100;
            this.FactoryName.Name = "FactoryName";
            this.FactoryName.Width = 125;
            // 
            // ProcessCode
            // 
            this.ProcessCode.DataPropertyName = "ProcessCode";
            this.ProcessCode.HeaderText = "工序编码";
            this.ProcessCode.MinimumWidth = 100;
            this.ProcessCode.Name = "ProcessCode";
            this.ProcessCode.Width = 125;
            // 
            // ProcessName
            // 
            this.ProcessName.DataPropertyName = "ProcessName";
            this.ProcessName.HeaderText = "工序名称";
            this.ProcessName.MinimumWidth = 100;
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.Width = 125;
            // 
            // PTeamCode
            // 
            this.PTeamCode.DataPropertyName = "PTeamCode";
            this.PTeamCode.HeaderText = "生产小组编码";
            this.PTeamCode.MinimumWidth = 100;
            this.PTeamCode.Name = "PTeamCode";
            this.PTeamCode.Width = 125;
            // 
            // PTeamName
            // 
            this.PTeamName.DataPropertyName = "PTeamName";
            this.PTeamName.HeaderText = "生产小组名称";
            this.PTeamName.MinimumWidth = 100;
            this.PTeamName.Name = "PTeamName";
            this.PTeamName.Width = 125;
            // 
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 633);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1313, 43);
            this.pagerControl1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "工厂";
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(55, 26);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 4;
            // 
            // Frm_PTeam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1319, 745);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_PTeam";
            this.Text = "生产小组打印";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_PTeam_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvm)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPTeamCode;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvm;
        private PagerControl pagerControl1;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn PTeamCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn PTeamName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPTeamName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbFactory;
    }
}