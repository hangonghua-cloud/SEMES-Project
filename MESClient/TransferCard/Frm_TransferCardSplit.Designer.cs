
namespace MESClient.TransferCard
{
    partial class Frm_TransferCardSplit
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtProcutOrder = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtContainerNO = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCardName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvSplit = new System.Windows.Forms.DataGridView();
            this.txtSplitQty = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.CardCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplit)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "订单号";
            // 
            // txtProcutOrder
            // 
            this.txtProcutOrder.Location = new System.Drawing.Point(88, 30);
            this.txtProcutOrder.Name = "txtProcutOrder";
            this.txtProcutOrder.ReadOnly = true;
            this.txtProcutOrder.Size = new System.Drawing.Size(179, 25);
            this.txtProcutOrder.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtCardName);
            this.groupBox1.Controls.Add(this.txtContainerNO);
            this.groupBox1.Controls.Add(this.txtProcutOrder);
            this.groupBox1.Location = new System.Drawing.Point(-1, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(839, 83);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // txtContainerNO
            // 
            this.txtContainerNO.Location = new System.Drawing.Point(345, 30);
            this.txtContainerNO.Name = "txtContainerNO";
            this.txtContainerNO.ReadOnly = true;
            this.txtContainerNO.Size = new System.Drawing.Size(164, 25);
            this.txtContainerNO.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(302, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "柜号";
            // 
            // txtCardName
            // 
            this.txtCardName.Location = new System.Drawing.Point(630, 30);
            this.txtCardName.Name = "txtCardName";
            this.txtCardName.ReadOnly = true;
            this.txtCardName.Size = new System.Drawing.Size(163, 25);
            this.txtCardName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 36);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "流转卡托号";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRemove);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.txtSplitQty);
            this.groupBox2.Controls.Add(this.dgvSplit);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(-1, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(839, 438);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作区";
            // 
            // dgvSplit
            // 
            this.dgvSplit.AllowUserToAddRows = false;
            this.dgvSplit.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvSplit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSplit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CardCode,
            this.CardName});
            this.dgvSplit.Location = new System.Drawing.Point(33, 98);
            this.dgvSplit.Name = "dgvSplit";
            this.dgvSplit.RowHeadersWidth = 51;
            this.dgvSplit.RowTemplate.Height = 27;
            this.dgvSplit.Size = new System.Drawing.Size(760, 334);
            this.dgvSplit.TabIndex = 0;
            // 
            // txtSplitQty
            // 
            this.txtSplitQty.Location = new System.Drawing.Point(103, 46);
            this.txtSplitQty.Name = "txtSplitQty";
            this.txtSplitQty.Size = new System.Drawing.Size(164, 25);
            this.txtSplitQty.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "拆分托数";
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(305, 40);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 33);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "新增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(395, 40);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 33);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "清除";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(344, 544);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(112, 40);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "确认拆分";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // CardCode
            // 
            this.CardCode.DataPropertyName = "CardCode";
            this.CardCode.HeaderText = "流转卡编码";
            this.CardCode.MinimumWidth = 100;
            this.CardCode.Name = "CardCode";
            this.CardCode.Width = 111;
            // 
            // CardName
            // 
            this.CardName.DataPropertyName = "CardName";
            this.CardName.HeaderText = "流转卡名称";
            this.CardName.MinimumWidth = 100;
            this.CardName.Name = "CardName";
            this.CardName.Width = 111;
            // 
            // Frm_TransferCardSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 599);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frm_TransferCardSplit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "流转卡拆分";
            this.Load += new System.EventHandler(this.Frm_TransferCardSplit_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSplit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProcutOrder;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCardName;
        private System.Windows.Forms.TextBox txtContainerNO;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgvSplit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtSplitQty;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
    }
}