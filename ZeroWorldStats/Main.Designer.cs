namespace ZeroWorldStats
{
	partial class Main
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.btn_Browse = new System.Windows.Forms.Button();
			this.txt_WorldReqFilePath = new System.Windows.Forms.TextBox();
			this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
			this.btn_GetObjectCnt = new System.Windows.Forms.Button();
			this.lbl_ObjectCnt = new System.Windows.Forms.Label();
			this.btn_GetRegionCnt = new System.Windows.Forms.Button();
			this.lbl_RegionCnt = new System.Windows.Forms.Label();
			this.btn_GetPlanConnectionCnt = new System.Windows.Forms.Button();
			this.lbl_PlanConnectionCnt = new System.Windows.Forms.Label();
			this.btn_GetPlanHubCnt = new System.Windows.Forms.Button();
			this.lbl_PlanHubCnt = new System.Windows.Forms.Label();
			this.btn_GetCPNamesAndPaths = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.openDlg_WorldReqFile = new System.Windows.Forms.OpenFileDialog();
			this.dd_ModeMrq = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btn_GetAllCounts = new System.Windows.Forms.Button();
			this.dd_PlanFile = new System.Windows.Forms.ComboBox();
			this.lbl_AppVersion = new System.Windows.Forms.Label();
			this.rtb_OutputLog = new System.Windows.Forms.RichTextBox();
			this.flowLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_Browse
			// 
			this.btn_Browse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_Browse.Location = new System.Drawing.Point(437, 10);
			this.btn_Browse.Name = "btn_Browse";
			this.btn_Browse.Size = new System.Drawing.Size(75, 23);
			this.btn_Browse.TabIndex = 1;
			this.btn_Browse.Text = "Browse...";
			this.btn_Browse.UseVisualStyleBackColor = true;
			this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click);
			// 
			// txt_WorldReqFilePath
			// 
			this.txt_WorldReqFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_WorldReqFilePath.Location = new System.Drawing.Point(112, 12);
			this.txt_WorldReqFilePath.Name = "txt_WorldReqFilePath";
			this.txt_WorldReqFilePath.Size = new System.Drawing.Size(319, 20);
			this.txt_WorldReqFilePath.TabIndex = 0;
			this.txt_WorldReqFilePath.TextChanged += new System.EventHandler(this.txt_WorldReqFilePath_TextChanged);
			// 
			// flowLayoutPanel2
			// 
			this.flowLayoutPanel2.Controls.Add(this.btn_GetObjectCnt);
			this.flowLayoutPanel2.Controls.Add(this.lbl_ObjectCnt);
			this.flowLayoutPanel2.Controls.Add(this.btn_GetRegionCnt);
			this.flowLayoutPanel2.Controls.Add(this.lbl_RegionCnt);
			this.flowLayoutPanel2.Controls.Add(this.btn_GetPlanConnectionCnt);
			this.flowLayoutPanel2.Controls.Add(this.lbl_PlanConnectionCnt);
			this.flowLayoutPanel2.Controls.Add(this.btn_GetPlanHubCnt);
			this.flowLayoutPanel2.Controls.Add(this.lbl_PlanHubCnt);
			this.flowLayoutPanel2.Controls.Add(this.btn_GetCPNamesAndPaths);
			this.flowLayoutPanel2.Controls.Add(this.label3);
			this.flowLayoutPanel2.Location = new System.Drawing.Point(12, 95);
			this.flowLayoutPanel2.Name = "flowLayoutPanel2";
			this.flowLayoutPanel2.Size = new System.Drawing.Size(373, 147);
			this.flowLayoutPanel2.TabIndex = 3;
			// 
			// btn_GetObjectCnt
			// 
			this.btn_GetObjectCnt.Location = new System.Drawing.Point(3, 3);
			this.btn_GetObjectCnt.Name = "btn_GetObjectCnt";
			this.btn_GetObjectCnt.Size = new System.Drawing.Size(199, 23);
			this.btn_GetObjectCnt.TabIndex = 10;
			this.btn_GetObjectCnt.Text = "Get Object Count";
			this.btn_GetObjectCnt.UseVisualStyleBackColor = true;
			this.btn_GetObjectCnt.Click += new System.EventHandler(this.btn_GetObjectCnt_Click);
			// 
			// lbl_ObjectCnt
			// 
			this.lbl_ObjectCnt.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.lbl_ObjectCnt.Location = new System.Drawing.Point(208, 8);
			this.lbl_ObjectCnt.Name = "lbl_ObjectCnt";
			this.lbl_ObjectCnt.Size = new System.Drawing.Size(137, 13);
			this.lbl_ObjectCnt.TabIndex = 1;
			this.lbl_ObjectCnt.Text = "objectCount";
			this.lbl_ObjectCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn_GetRegionCnt
			// 
			this.btn_GetRegionCnt.Location = new System.Drawing.Point(3, 32);
			this.btn_GetRegionCnt.Name = "btn_GetRegionCnt";
			this.btn_GetRegionCnt.Size = new System.Drawing.Size(199, 23);
			this.btn_GetRegionCnt.TabIndex = 11;
			this.btn_GetRegionCnt.Text = "Get Region Count";
			this.btn_GetRegionCnt.UseVisualStyleBackColor = true;
			this.btn_GetRegionCnt.Click += new System.EventHandler(this.btn_GetRegionCnt_Click);
			// 
			// lbl_RegionCnt
			// 
			this.lbl_RegionCnt.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbl_RegionCnt.Location = new System.Drawing.Point(208, 37);
			this.lbl_RegionCnt.Name = "lbl_RegionCnt";
			this.lbl_RegionCnt.Size = new System.Drawing.Size(137, 13);
			this.lbl_RegionCnt.TabIndex = 3;
			this.lbl_RegionCnt.Text = "regionCount";
			this.lbl_RegionCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn_GetPlanConnectionCnt
			// 
			this.btn_GetPlanConnectionCnt.Location = new System.Drawing.Point(3, 61);
			this.btn_GetPlanConnectionCnt.Name = "btn_GetPlanConnectionCnt";
			this.btn_GetPlanConnectionCnt.Size = new System.Drawing.Size(199, 23);
			this.btn_GetPlanConnectionCnt.TabIndex = 12;
			this.btn_GetPlanConnectionCnt.Text = "Get Plan Connection Count";
			this.btn_GetPlanConnectionCnt.UseVisualStyleBackColor = true;
			this.btn_GetPlanConnectionCnt.Click += new System.EventHandler(this.btn_GetPlanConnectionCnt_Click);
			// 
			// lbl_PlanConnectionCnt
			// 
			this.lbl_PlanConnectionCnt.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbl_PlanConnectionCnt.Location = new System.Drawing.Point(208, 66);
			this.lbl_PlanConnectionCnt.Name = "lbl_PlanConnectionCnt";
			this.lbl_PlanConnectionCnt.Size = new System.Drawing.Size(137, 13);
			this.lbl_PlanConnectionCnt.TabIndex = 5;
			this.lbl_PlanConnectionCnt.Text = "planConnectionCount";
			this.lbl_PlanConnectionCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn_GetPlanHubCnt
			// 
			this.btn_GetPlanHubCnt.Location = new System.Drawing.Point(3, 90);
			this.btn_GetPlanHubCnt.Name = "btn_GetPlanHubCnt";
			this.btn_GetPlanHubCnt.Size = new System.Drawing.Size(199, 23);
			this.btn_GetPlanHubCnt.TabIndex = 13;
			this.btn_GetPlanHubCnt.Text = "Get Plan Hub Count";
			this.btn_GetPlanHubCnt.UseVisualStyleBackColor = true;
			this.btn_GetPlanHubCnt.Click += new System.EventHandler(this.btn_GetPlanHubCnt_Click);
			// 
			// lbl_PlanHubCnt
			// 
			this.lbl_PlanHubCnt.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.lbl_PlanHubCnt.Location = new System.Drawing.Point(208, 95);
			this.lbl_PlanHubCnt.Name = "lbl_PlanHubCnt";
			this.lbl_PlanHubCnt.Size = new System.Drawing.Size(137, 13);
			this.lbl_PlanHubCnt.TabIndex = 7;
			this.lbl_PlanHubCnt.Text = "planHubCount";
			this.lbl_PlanHubCnt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btn_GetCPNamesAndPaths
			// 
			this.btn_GetCPNamesAndPaths.Location = new System.Drawing.Point(3, 119);
			this.btn_GetCPNamesAndPaths.Name = "btn_GetCPNamesAndPaths";
			this.btn_GetCPNamesAndPaths.Size = new System.Drawing.Size(199, 23);
			this.btn_GetCPNamesAndPaths.TabIndex = 15;
			this.btn_GetCPNamesAndPaths.Text = "Get CP Names && Spawn Paths";
			this.btn_GetCPNamesAndPaths.UseVisualStyleBackColor = true;
			this.btn_GetCPNamesAndPaths.Click += new System.EventHandler(this.btn_GetCPNamesAndPaths_Click);
			// 
			// label3
			// 
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label3.Location = new System.Drawing.Point(208, 124);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(137, 13);
			this.label3.TabIndex = 14;
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(12, 69);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(91, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "World PLN name:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "World REQ path:";
			// 
			// openDlg_WorldReqFile
			// 
			this.openDlg_WorldReqFile.Filter = "REQ files|*.req";
			this.openDlg_WorldReqFile.InitialDirectory = "C:\\BF2_ModTools";
			this.openDlg_WorldReqFile.RestoreDirectory = true;
			// 
			// dd_ModeMrq
			// 
			this.dd_ModeMrq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dd_ModeMrq.FormattingEnabled = true;
			this.dd_ModeMrq.Items.AddRange(new object[] {
            "[Base]"});
			this.dd_ModeMrq.Location = new System.Drawing.Point(112, 38);
			this.dd_ModeMrq.Name = "dd_ModeMrq";
			this.dd_ModeMrq.Size = new System.Drawing.Size(319, 21);
			this.dd_ModeMrq.TabIndex = 3;
			this.dd_ModeMrq.SelectionChangeCommitted += new System.EventHandler(this.dd_ModeMrq_SelectionChangeCommitted);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 41);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(94, 13);
			this.label2.TabIndex = 8;
			this.label2.Text = "Mode MRQ name:";
			// 
			// btn_GetAllCounts
			// 
			this.btn_GetAllCounts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_GetAllCounts.Location = new System.Drawing.Point(437, 36);
			this.btn_GetAllCounts.Name = "btn_GetAllCounts";
			this.btn_GetAllCounts.Size = new System.Drawing.Size(75, 23);
			this.btn_GetAllCounts.TabIndex = 4;
			this.btn_GetAllCounts.Text = "Get All";
			this.btn_GetAllCounts.UseVisualStyleBackColor = true;
			this.btn_GetAllCounts.Click += new System.EventHandler(this.btn_GetAllCounts_Click);
			// 
			// dd_PlanFile
			// 
			this.dd_PlanFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dd_PlanFile.FormattingEnabled = true;
			this.dd_PlanFile.Location = new System.Drawing.Point(112, 66);
			this.dd_PlanFile.Name = "dd_PlanFile";
			this.dd_PlanFile.Size = new System.Drawing.Size(319, 21);
			this.dd_PlanFile.TabIndex = 9;
			this.dd_PlanFile.SelectionChangeCommitted += new System.EventHandler(this.dd_PlanFile_SelectionChangeCommitted);
			// 
			// lbl_AppVersion
			// 
			this.lbl_AppVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_AppVersion.AutoSize = true;
			this.lbl_AppVersion.Location = new System.Drawing.Point(475, 359);
			this.lbl_AppVersion.Name = "lbl_AppVersion";
			this.lbl_AppVersion.Size = new System.Drawing.Size(37, 13);
			this.lbl_AppVersion.TabIndex = 10;
			this.lbl_AppVersion.Text = "v0.0.0";
			this.lbl_AppVersion.TextAlign = System.Drawing.ContentAlignment.BottomRight;
			// 
			// rtb_OutputLog
			// 
			this.rtb_OutputLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtb_OutputLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtb_OutputLog.Location = new System.Drawing.Point(12, 249);
			this.rtb_OutputLog.Name = "rtb_OutputLog";
			this.rtb_OutputLog.ReadOnly = true;
			this.rtb_OutputLog.Size = new System.Drawing.Size(419, 120);
			this.rtb_OutputLog.TabIndex = 11;
			this.rtb_OutputLog.Text = "";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(524, 381);
			this.Controls.Add(this.rtb_OutputLog);
			this.Controls.Add(this.lbl_AppVersion);
			this.Controls.Add(this.dd_PlanFile);
			this.Controls.Add(this.btn_GetAllCounts);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.dd_ModeMrq);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txt_WorldReqFilePath);
			this.Controls.Add(this.btn_Browse);
			this.Controls.Add(this.flowLayoutPanel2);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(440, 380);
			this.Name = "Main";
			this.Text = "Zero World Stats";
			this.Load += new System.EventHandler(this.Main_Load);
			this.flowLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btn_Browse;
		private System.Windows.Forms.TextBox txt_WorldReqFilePath;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
		private System.Windows.Forms.Button btn_GetObjectCnt;
		private System.Windows.Forms.Label lbl_ObjectCnt;
		private System.Windows.Forms.Button btn_GetRegionCnt;
		private System.Windows.Forms.Label lbl_RegionCnt;
		private System.Windows.Forms.Button btn_GetPlanHubCnt;
		private System.Windows.Forms.Label lbl_PlanHubCnt;
		private System.Windows.Forms.Button btn_GetPlanConnectionCnt;
		private System.Windows.Forms.Label lbl_PlanConnectionCnt;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.OpenFileDialog openDlg_WorldReqFile;
		private System.Windows.Forms.ComboBox dd_ModeMrq;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btn_GetAllCounts;
		private System.Windows.Forms.ComboBox dd_PlanFile;
		private System.Windows.Forms.Label lbl_AppVersion;
		private System.Windows.Forms.Button btn_GetCPNamesAndPaths;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.RichTextBox rtb_OutputLog;
	}
}

