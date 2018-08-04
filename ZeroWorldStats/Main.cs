using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ZeroWorldStats
{
	public partial class Main : Form
	{
		public Main()
		{
			InitializeComponent();
		}

		public string worldReqFilePath;

		private void Main_Load(object sender, EventArgs e)
		{
			ResetCountLabels();
		}

		private void btn_Browse_Click(object sender, EventArgs e)
		{
			if (openDlg_WorldReqFile.ShowDialog() == DialogResult.OK)
			{
				worldReqFilePath = openDlg_WorldReqFile.FileName;
				txt_WorldReqFilePath.Text = worldReqFilePath;
			}
		}

		private void btn_GetObjectCnt_Click(object sender, EventArgs e)
		{

		}

		private void btn_GetRegionCnt_Click(object sender, EventArgs e)
		{

		}

		private void btn_GetPlanConnectionCnt_Click(object sender, EventArgs e)
		{

		}

		private void btn_GetPlanHubCnt_Click(object sender, EventArgs e)
		{

		}

		private void ResetCountLabels()
		{
			lbl_ObjectCnt.Text = "0";
			lbl_RegionCnt.Text = "0";
			lbl_PlanConnectionCnt.Text = "0";
			lbl_PlanHubCnt.Text = "0";
		}
	}
}
