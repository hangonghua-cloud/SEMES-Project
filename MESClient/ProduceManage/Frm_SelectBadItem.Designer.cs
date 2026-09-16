
namespace MESClient.ProduceManage
{
    partial class Frm_SelectBadItem
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
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtKeyWord = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv1
            // 
            this.dgv1.AllowUserToAddRows = false;
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.value,
            this.label});
            this.dgv1.Location = new System.Drawing.Point(26, 78);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowHeadersWidth = 51;
            this.dgv1.RowTemplate.Height = 27;
            this.dgv1.Size = new System.Drawing.Size(471, 305);
            this.dgv1.TabIndex = 0;
            this.dgv1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv1_CellDoubleClick);
            // 
            // value
            // 
            this.value.DataPropertyName = "value";
            this.value.HeaderText = "不良项目编码";
            this.value.MinimumWidth = 125;
            this.value.Name = "value";
            this.value.Width = 125;
            // 
            // label
            // 
            this.label.DataPropertyName = "label";
            this.label.HeaderText = "不良项目名称";
            this.label.MinimumWidth = 125;
            this.label.Name = "label";
            this.label.Width = 125;
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.Location = new System.Drawing.Point(165, 31);
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(207, 25);
            this.txtKeyWord.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "不良项目名称";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(398, 21);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(99, 40);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Frm_SelectBadItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 395);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtKeyWord);
            this.Controls.Add(this.dgv1);
            this.Name = "Frm_SelectBadItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选择不良项目";
            this.Load += new System.EventHandler(this.Frm_SelectBadItem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.TextBox txtKeyWord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn label;
    }
}