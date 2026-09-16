
namespace MESClient.WorkOrderExcute
{
    partial class Frm_WorkOrderExcute
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
            this.btnBG = new System.Windows.Forms.Button();
            this.btnCheck = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbSWStatus = new System.Windows.Forms.ComboBox();
            this.cmbMachine = new System.Windows.Forms.ComboBox();
            this.cmbProcess = new System.Windows.Forms.ComboBox();
            this.cmbFactory = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactoryCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactoryName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EquipName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PlanProductTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WorkOrderTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExeWorkOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SWStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SWStatusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MMXH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MMCJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Spec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BWXH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PiecesQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContainerNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UV = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KCKX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SmallClass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SheetsQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PalletQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BGQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pagerControl1 = new MESClient.PagerControl();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBG);
            this.groupBox1.Controls.Add(this.btnCheck);
            this.groupBox1.Controls.Add(this.btnStart);
            this.groupBox1.Controls.Add(this.btnUp);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cmbSWStatus);
            this.groupBox1.Controls.Add(this.cmbMachine);
            this.groupBox1.Controls.Add(this.cmbProcess);
            this.groupBox1.Controls.Add(this.cmbFactory);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1214, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnBG
            // 
            this.btnBG.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnBG.Location = new System.Drawing.Point(1101, 60);
            this.btnBG.Name = "btnBG";
            this.btnBG.Size = new System.Drawing.Size(84, 40);
            this.btnBG.TabIndex = 2;
            this.btnBG.Text = "挤出报工";
            this.btnBG.UseVisualStyleBackColor = true;
            this.btnBG.Click += new System.EventHandler(this.btnBG_Click);
            // 
            // btnCheck
            // 
            this.btnCheck.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnCheck.Location = new System.Drawing.Point(998, 60);
            this.btnCheck.Name = "btnCheck";
            this.btnCheck.Size = new System.Drawing.Size(86, 40);
            this.btnCheck.TabIndex = 2;
            this.btnCheck.Text = "过程检验";
            this.btnCheck.UseVisualStyleBackColor = true;
            this.btnCheck.Click += new System.EventHandler(this.btnCheck_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnStart.Location = new System.Drawing.Point(893, 60);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(88, 40);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "挤出开工";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnUp
            // 
            this.btnUp.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnUp.Location = new System.Drawing.Point(761, 60);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(115, 40);
            this.btnUp.TabIndex = 2;
            this.btnUp.Text = "原料批次上机";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSearch.Font = new System.Drawing.Font("宋体", 9F);
            this.btnSearch.Location = new System.Drawing.Point(667, 60);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 40);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(752, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "生产状态";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(515, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 15);
            this.label3.TabIndex = 1;
            this.label3.Text = "机台";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(274, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "工序";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "工厂";
            // 
            // cmbSWStatus
            // 
            this.cmbSWStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSWStatus.FormattingEnabled = true;
            this.cmbSWStatus.Location = new System.Drawing.Point(825, 21);
            this.cmbSWStatus.Name = "cmbSWStatus";
            this.cmbSWStatus.Size = new System.Drawing.Size(159, 23);
            this.cmbSWStatus.TabIndex = 0;
            // 
            // cmbMachine
            // 
            this.cmbMachine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMachine.FormattingEnabled = true;
            this.cmbMachine.Location = new System.Drawing.Point(558, 21);
            this.cmbMachine.Name = "cmbMachine";
            this.cmbMachine.Size = new System.Drawing.Size(159, 23);
            this.cmbMachine.TabIndex = 0;
            // 
            // cmbProcess
            // 
            this.cmbProcess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProcess.FormattingEnabled = true;
            this.cmbProcess.Location = new System.Drawing.Point(317, 21);
            this.cmbProcess.Name = "cmbProcess";
            this.cmbProcess.Size = new System.Drawing.Size(159, 23);
            this.cmbProcess.TabIndex = 0;
            this.cmbProcess.SelectedIndexChanged += new System.EventHandler(this.cmbProcess_SelectedIndexChanged);
            // 
            // cmbFactory
            // 
            this.cmbFactory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFactory.FormattingEnabled = true;
            this.cmbFactory.Location = new System.Drawing.Point(78, 21);
            this.cmbFactory.Name = "cmbFactory";
            this.cmbFactory.Size = new System.Drawing.Size(159, 23);
            this.cmbFactory.TabIndex = 0;
            this.cmbFactory.SelectedIndexChanged += new System.EventHandler(this.cmbFactory_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgv1);
            this.groupBox2.Controls.Add(this.pagerControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 106);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1214, 625);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            // 
            // dgv1
            // 
            this.dgv1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ProductOrder,
            this.FactoryCode,
            this.FactoryName,
            this.ProcessCode,
            this.ProcessName,
            this.EquipCode,
            this.EquipName,
            this.CreateTime,
            this.PlanProductTime,
            this.WorkOrderTypeName,
            this.ExeWorkOrder,
            this.SWStatus,
            this.SWStatusName,
            this.MMXH,
            this.MMCJ,
            this.Spec,
            this.BWXH,
            this.PiecesQty,
            this.ContainerNO,
            this.UV,
            this.KCKX,
            this.SmallClass,
            this.SheetsQty,
            this.PalletQty,
            this.BGQty});
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(3, 21);
            this.dgv1.MultiSelect = false;
            this.dgv1.Name = "dgv1";
            this.dgv1.ReadOnly = true;
            this.dgv1.RowHeadersWidth = 51;
            this.dgv1.RowTemplate.Height = 27;
            this.dgv1.Size = new System.Drawing.Size(1208, 564);
            this.dgv1.TabIndex = 1;
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.MinimumWidth = 6;
            this.Id.Name = "Id";
            this.Id.ReadOnly = true;
            this.Id.Visible = false;
            this.Id.Width = 52;
            // 
            // ProductOrder
            // 
            this.ProductOrder.DataPropertyName = "ProductOrder";
            this.ProductOrder.HeaderText = "生产订单";
            this.ProductOrder.MinimumWidth = 6;
            this.ProductOrder.Name = "ProductOrder";
            this.ProductOrder.ReadOnly = true;
            this.ProductOrder.Visible = false;
            this.ProductOrder.Width = 96;
            // 
            // FactoryCode
            // 
            this.FactoryCode.DataPropertyName = "FactoryCode";
            this.FactoryCode.HeaderText = "工厂编码";
            this.FactoryCode.MinimumWidth = 6;
            this.FactoryCode.Name = "FactoryCode";
            this.FactoryCode.ReadOnly = true;
            this.FactoryCode.Visible = false;
            this.FactoryCode.Width = 96;
            // 
            // FactoryName
            // 
            this.FactoryName.DataPropertyName = "FactoryName";
            this.FactoryName.FillWeight = 101.9913F;
            this.FactoryName.HeaderText = "工厂";
            this.FactoryName.MinimumWidth = 75;
            this.FactoryName.Name = "FactoryName";
            this.FactoryName.ReadOnly = true;
            this.FactoryName.Width = 75;
            // 
            // ProcessCode
            // 
            this.ProcessCode.DataPropertyName = "ProcessCode";
            this.ProcessCode.HeaderText = "工序编码";
            this.ProcessCode.MinimumWidth = 6;
            this.ProcessCode.Name = "ProcessCode";
            this.ProcessCode.ReadOnly = true;
            this.ProcessCode.Visible = false;
            this.ProcessCode.Width = 96;
            // 
            // ProcessName
            // 
            this.ProcessName.DataPropertyName = "ProcessName";
            this.ProcessName.FillWeight = 101.7977F;
            this.ProcessName.HeaderText = "工序";
            this.ProcessName.MinimumWidth = 75;
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            this.ProcessName.Width = 75;
            // 
            // EquipCode
            // 
            this.EquipCode.DataPropertyName = "EquipCode";
            this.EquipCode.HeaderText = "机台编码";
            this.EquipCode.MinimumWidth = 6;
            this.EquipCode.Name = "EquipCode";
            this.EquipCode.ReadOnly = true;
            this.EquipCode.Visible = false;
            this.EquipCode.Width = 96;
            // 
            // EquipName
            // 
            this.EquipName.DataPropertyName = "EquipName";
            this.EquipName.FillWeight = 101.6145F;
            this.EquipName.HeaderText = "机台";
            this.EquipName.MinimumWidth = 75;
            this.EquipName.Name = "EquipName";
            this.EquipName.ReadOnly = true;
            this.EquipName.Width = 75;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.FillWeight = 101.441F;
            this.CreateTime.HeaderText = "派工时间";
            this.CreateTime.MinimumWidth = 100;
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            // 
            // PlanProductTime
            // 
            this.PlanProductTime.DataPropertyName = "PlanProductTime";
            this.PlanProductTime.FillWeight = 101.2769F;
            this.PlanProductTime.HeaderText = "计划生产时间";
            this.PlanProductTime.MinimumWidth = 100;
            this.PlanProductTime.Name = "PlanProductTime";
            this.PlanProductTime.ReadOnly = true;
            // 
            // WorkOrderTypeName
            // 
            this.WorkOrderTypeName.DataPropertyName = "WorkOrderTypeName";
            this.WorkOrderTypeName.FillWeight = 101.1217F;
            this.WorkOrderTypeName.HeaderText = "工单类型";
            this.WorkOrderTypeName.MinimumWidth = 80;
            this.WorkOrderTypeName.Name = "WorkOrderTypeName";
            this.WorkOrderTypeName.ReadOnly = true;
            this.WorkOrderTypeName.Width = 80;
            // 
            // ExeWorkOrder
            // 
            this.ExeWorkOrder.DataPropertyName = "ExeWorkOrder";
            this.ExeWorkOrder.FillWeight = 100.9747F;
            this.ExeWorkOrder.HeaderText = "执行工单";
            this.ExeWorkOrder.MinimumWidth = 80;
            this.ExeWorkOrder.Name = "ExeWorkOrder";
            this.ExeWorkOrder.ReadOnly = true;
            this.ExeWorkOrder.Width = 80;
            // 
            // SWStatus
            // 
            this.SWStatus.DataPropertyName = "SWStatus";
            this.SWStatus.HeaderText = "生产状态编码";
            this.SWStatus.MinimumWidth = 6;
            this.SWStatus.Name = "SWStatus";
            this.SWStatus.ReadOnly = true;
            this.SWStatus.Visible = false;
            this.SWStatus.Width = 89;
            // 
            // SWStatusName
            // 
            this.SWStatusName.DataPropertyName = "SWStatusName";
            this.SWStatusName.FillWeight = 100.8356F;
            this.SWStatusName.HeaderText = "生产状态";
            this.SWStatusName.MinimumWidth = 80;
            this.SWStatusName.Name = "SWStatusName";
            this.SWStatusName.ReadOnly = true;
            this.SWStatusName.Width = 80;
            // 
            // MMXH
            // 
            this.MMXH.DataPropertyName = "MMXH";
            this.MMXH.FillWeight = 100.704F;
            this.MMXH.HeaderText = "面膜型号";
            this.MMXH.MinimumWidth = 80;
            this.MMXH.Name = "MMXH";
            this.MMXH.ReadOnly = true;
            this.MMXH.Width = 80;
            // 
            // MMCJ
            // 
            this.MMCJ.DataPropertyName = "MMCJ";
            this.MMCJ.FillWeight = 100.5795F;
            this.MMCJ.HeaderText = "面膜厂家";
            this.MMCJ.MinimumWidth = 80;
            this.MMCJ.Name = "MMCJ";
            this.MMCJ.ReadOnly = true;
            this.MMCJ.Width = 80;
            // 
            // Spec
            // 
            this.Spec.DataPropertyName = "Spec";
            this.Spec.FillWeight = 100.4616F;
            this.Spec.HeaderText = "规格型号";
            this.Spec.MinimumWidth = 80;
            this.Spec.Name = "Spec";
            this.Spec.ReadOnly = true;
            this.Spec.Width = 80;
            // 
            // BWXH
            // 
            this.BWXH.DataPropertyName = "BWXH";
            this.BWXH.FillWeight = 100.3501F;
            this.BWXH.HeaderText = "压纹";
            this.BWXH.MinimumWidth = 75;
            this.BWXH.Name = "BWXH";
            this.BWXH.ReadOnly = true;
            this.BWXH.Width = 75;
            // 
            // PiecesQty
            // 
            this.PiecesQty.DataPropertyName = "PiecesQty";
            this.PiecesQty.FillWeight = 100.2446F;
            this.PiecesQty.HeaderText = "总片数";
            this.PiecesQty.MinimumWidth = 75;
            this.PiecesQty.Name = "PiecesQty";
            this.PiecesQty.ReadOnly = true;
            this.PiecesQty.Width = 75;
            // 
            // ContainerNO
            // 
            this.ContainerNO.DataPropertyName = "ContainerNO";
            this.ContainerNO.FillWeight = 100.1447F;
            this.ContainerNO.HeaderText = "柜号";
            this.ContainerNO.MinimumWidth = 75;
            this.ContainerNO.Name = "ContainerNO";
            this.ContainerNO.ReadOnly = true;
            this.ContainerNO.Width = 75;
            // 
            // UV
            // 
            this.UV.DataPropertyName = "UV";
            this.UV.FillWeight = 83.91303F;
            this.UV.HeaderText = "UV";
            this.UV.MinimumWidth = 75;
            this.UV.Name = "UV";
            this.UV.ReadOnly = true;
            this.UV.Width = 75;
            // 
            // KCKX
            // 
            this.KCKX.DataPropertyName = "KCKX";
            this.KCKX.FillWeight = 100.827F;
            this.KCKX.HeaderText = "扣型";
            this.KCKX.MinimumWidth = 75;
            this.KCKX.Name = "KCKX";
            this.KCKX.ReadOnly = true;
            this.KCKX.Width = 75;
            // 
            // SmallClass
            // 
            this.SmallClass.DataPropertyName = "SmallClass";
            this.SmallClass.FillWeight = 100.6958F;
            this.SmallClass.HeaderText = "规格";
            this.SmallClass.MinimumWidth = 75;
            this.SmallClass.Name = "SmallClass";
            this.SmallClass.ReadOnly = true;
            this.SmallClass.Width = 75;
            // 
            // SheetsQty
            // 
            this.SheetsQty.DataPropertyName = "SheetsQty";
            this.SheetsQty.FillWeight = 100.5718F;
            this.SheetsQty.HeaderText = "生产张数";
            this.SheetsQty.MinimumWidth = 80;
            this.SheetsQty.Name = "SheetsQty";
            this.SheetsQty.ReadOnly = true;
            this.SheetsQty.Width = 80;
            // 
            // PalletQty
            // 
            this.PalletQty.DataPropertyName = "PalletQty";
            this.PalletQty.HeaderText = "托数";
            this.PalletQty.MinimumWidth = 75;
            this.PalletQty.Name = "PalletQty";
            this.PalletQty.ReadOnly = true;
            this.PalletQty.Width = 75;
            // 
            // BGQty
            // 
            this.BGQty.DataPropertyName = "BGQty";
            this.BGQty.FillWeight = 100.4543F;
            this.BGQty.HeaderText = "报工数量";
            this.BGQty.MinimumWidth = 80;
            this.BGQty.Name = "BGQty";
            this.BGQty.ReadOnly = true;
            this.BGQty.Width = 80;
            // 
            // pagerControl1
            // 
            this.pagerControl1.CurrentPage = 1;
            this.pagerControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pagerControl1.Location = new System.Drawing.Point(3, 585);
            this.pagerControl1.Name = "pagerControl1";
            this.pagerControl1.Record = 0;
            this.pagerControl1.Size = new System.Drawing.Size(1208, 37);
            this.pagerControl1.TabIndex = 0;
            
            // 
            // Frm_WorkOrderExcute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 731);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Frm_WorkOrderExcute";
            this.Text = "派工工单执行";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Frm_WorkOrderExcute_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbFactory;
        private System.Windows.Forms.Button btnBG;
        private System.Windows.Forms.Button btnCheck;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnUp;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cmbMachine;
        private System.Windows.Forms.ComboBox cmbProcess;
        private PagerControl pagerControl1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSWStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactoryName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn EquipName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlanProductTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn WorkOrderTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExeWorkOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn SWStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn SWStatusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MMXH;
        private System.Windows.Forms.DataGridViewTextBoxColumn MMCJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn Spec;
        private System.Windows.Forms.DataGridViewTextBoxColumn BWXH;
        private System.Windows.Forms.DataGridViewTextBoxColumn PiecesQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContainerNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn UV;
        private System.Windows.Forms.DataGridViewTextBoxColumn KCKX;
        private System.Windows.Forms.DataGridViewTextBoxColumn SmallClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheetsQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn PalletQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn BGQty;
    }
}